// Data caching service for WMS API
import React from 'react';
interface CacheEntry<T> {
  data: T;
  timestamp: number;
  key: string;
}

interface CacheOptions {
  ttl?: number; // Time to live in milliseconds
  maxSize?: number; // Maximum number of entries
}

class DataCacheService {
  private cache = new Map<string, CacheEntry<any>>();
  private defaultTTL = 5 * 60 * 1000; // 5 minutes
  private maxSize = 100;

  constructor(options?: CacheOptions) {
    if (options?.ttl) this.defaultTTL = options.ttl;
    if (options?.maxSize) this.maxSize = options.maxSize;
  }

  // Generate cache key from parameters
  private generateKey(prefix: string, params?: Record<string, any>): string {
    if (!params) return prefix;
    const sortedParams = Object.keys(params)
      .sort()
      .map(key => `${key}:${params[key]}`)
      .join('|');
    return `${prefix}:${sortedParams}`;
  }

  // Check if cache entry is still valid
  private isValid(entry: CacheEntry<any>, ttl?: number): boolean {
    const maxAge = ttl || this.defaultTTL;
    return Date.now() - entry.timestamp < maxAge;
  }

  // Clean up expired entries
  private cleanup(): void {
    const now = Date.now();
    const entries = Array.from(this.cache.entries());
    for (const [key, entry] of entries) {
      if (!this.isValid(entry)) {
        this.cache.delete(key);
      }
    }
  }

  // Enforce cache size limit
  private enforceMaxSize(): void {
    if (this.cache.size <= this.maxSize) return;

    // Remove oldest entries
    const entries = Array.from(this.cache.entries())
      .sort(([, a], [, b]) => a.timestamp - b.timestamp);
    
    const toRemove = entries.slice(0, this.cache.size - this.maxSize);
    toRemove.forEach(([key]) => this.cache.delete(key));
  }

  // Get data from cache
  get<T>(key: string, params?: Record<string, any>, ttl?: number): T | null {
    this.cleanup();
    
    const cacheKey = this.generateKey(key, params);
    const entry = this.cache.get(cacheKey);
    
    if (!entry || !this.isValid(entry, ttl)) {
      return null;
    }
    
    return entry.data;
  }

  // Set data in cache
  set<T>(key: string, data: T, params?: Record<string, any>): void {
    this.cleanup();
    this.enforceMaxSize();
    
    const cacheKey = this.generateKey(key, params);
    const entry: CacheEntry<T> = {
      data,
      timestamp: Date.now(),
      key: cacheKey
    };
    
    this.cache.set(cacheKey, entry);
  }

  // Invalidate specific cache entries
  invalidate(key: string, params?: Record<string, any>): void {
    if (params) {
      const cacheKey = this.generateKey(key, params);
      this.cache.delete(cacheKey);
    } else {
      // Invalidate all entries with this prefix
      const keys = Array.from(this.cache.keys());
      for (const cacheKey of keys) {
        if (cacheKey.startsWith(key)) {
          this.cache.delete(cacheKey);
        }
      }
    }
  }

  // Clear all cache
  clear(): void {
    this.cache.clear();
  }

  // Get cache stats
  getStats() {
    return {
      size: this.cache.size,
      maxSize: this.maxSize,
      entries: Array.from(this.cache.keys())
    };
  }

  // Check if data exists in cache
  has(key: string, params?: Record<string, any>): boolean {
    const cacheKey = this.generateKey(key, params);
    const entry = this.cache.get(cacheKey);
    return entry !== undefined && this.isValid(entry);
  }
}

// Create singleton instance
export const dataCache = new DataCacheService({
  ttl: 5 * 60 * 1000, // 5 minutes
  maxSize: 50
});

// Cache keys constants
export const CACHE_KEYS = {
  PARCELS: 'parcels',
  DISPATCHES: 'dispatches',
  PARCEL_BY_ID: 'parcel_by_id',
  DISPATCH_BY_ID: 'dispatch_by_id',
  USER_PROFILE: 'user_profile'
} as const;

// Helper hook for cached data fetching
export const useCachedData = <T>(
  cacheKey: string,
  fetchFn: () => Promise<T>,
  params?: Record<string, any>,
  options?: { ttl?: number; enabled?: boolean }
) => {
  const [data, setData] = React.useState<T | null>(null);
  const [loading, setLoading] = React.useState(false);
  const [error, setError] = React.useState<string>('');

  const fetchData = React.useCallback(async (forceRefresh = false) => {
    try {
      setLoading(true);
      setError('');

      // Check cache first unless forcing refresh
      if (!forceRefresh) {
        const cachedData = dataCache.get<T>(cacheKey, params, options?.ttl);
        if (cachedData) {
          setData(cachedData);
          setLoading(false);
          return cachedData;
        }
      }

      // Fetch fresh data
      const freshData = await fetchFn();
      
      // Cache the result
      dataCache.set(cacheKey, freshData, params);
      setData(freshData);
      
      return freshData;
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to fetch data');
      throw err;
    } finally {
      setLoading(false);
    }
  }, [cacheKey, fetchFn, params, options?.ttl]);

  React.useEffect(() => {
    if (options?.enabled !== false) {
      fetchData();
    }
  }, [fetchData, options?.enabled]);

  return {
    data,
    loading,
    error,
    refetch: fetchData,
    invalidate: () => dataCache.invalidate(cacheKey, params)
  };
};

