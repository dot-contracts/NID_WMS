import React, { useState, useEffect, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { Package, Truck, BarChart3, Users, Plus, TrendingUp, CheckCircle, Clock, Building2, FileText, ShieldCheck, Edit, UserPlus, ArrowRightLeft } from 'lucide-react';
import { useAuth } from '../context/AuthContext';
import { Card, Button, Badge } from '../components/ui';
import { wmsApi, Parcel, Dispatch, User } from '../services/wmsApi';

interface ActivityItem {
  id: string;
  type: 'parcel_created' | 'parcel_updated' | 'parcel_delivered' | 'dispatch_created' | 'dispatch_updated' | 'user_created' | 'user_updated' | 'parcel_status_change';
  action: string;
  time: string;
  timestamp: number;
  status: 'success' | 'warning' | 'info' | 'error';
  icon: React.ComponentType<any>;
  entity?: string;
  details?: any;
}

const Dashboard: React.FC = () => {
  const { user, isAdmin, isBranchManager, isAccountant } = useAuth();
  const [parcels, setParcels] = useState<Parcel[]>([]);
  const [dispatches, setDispatches] = useState<Dispatch[]>([]);
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);
  const [recentActivity, setRecentActivity] = useState<ActivityItem[]>([]);
  const [lastRefresh, setLastRefresh] = useState<Date>(new Date());
  const [timeUpdateTrigger, setTimeUpdateTrigger] = useState<number>(0);

  const fetchDashboardData = useCallback(async () => {
    try {
      setLoading(true);
      
      // Determine if we should filter by branch
      const branchName = isBranchManager() && user?.branch?.name ? user.branch.name : undefined;
      
      const [parcelsData, dispatchesData, usersData] = await Promise.all([
        wmsApi.getParcels(branchName),
        wmsApi.getDispatches(branchName),
        // Only fetch users if admin (for activity tracking)
        isAdmin() ? wmsApi.getUsers() : Promise.resolve([])
      ]);
      
      setParcels(parcelsData);
      setDispatches(dispatchesData);
      setUsers(usersData);
      setLastRefresh(new Date());
      
      // Generate comprehensive recent activity
      const activities = generateEnhancedRecentActivity(parcelsData, dispatchesData, usersData);
      setRecentActivity(activities);
      
    } catch (error) {
    } finally {
      setLoading(false);
    }
  }, [user, isBranchManager, isAdmin]);

  useEffect(() => {
    fetchDashboardData();
    
    // Set up auto-refresh every 30 seconds for real-time updates
    const dataRefreshInterval = setInterval(() => {
      fetchDashboardData();
    }, 30000);
    
    // Set up time update every minute to refresh relative times
    const timeUpdateInterval = setInterval(() => {
      setTimeUpdateTrigger(prev => prev + 1);
    }, 60000);
    
    return () => {
      clearInterval(dataRefreshInterval);
      clearInterval(timeUpdateInterval);
    };
  }, [fetchDashboardData]);

  // Update relative times when timeUpdateTrigger changes
  useEffect(() => {
    if (recentActivity.length > 0) {
      const updatedActivity = recentActivity.map(activity => ({
        ...activity,
        time: formatRelativeTime(new Date(activity.timestamp)).text
      }));
      setRecentActivity(updatedActivity);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [timeUpdateTrigger]);

  const formatRelativeTime = (date: string | Date): { text: string; timestamp: number } => {
    const now = Date.now();
    let timestamp: number;
    
    // Handle different date formats
    if (date instanceof Date) {
      timestamp = date.getTime();
    } else if (typeof date === 'string') {
      // Handle ISO strings and other formats
      // Check if the date string lacks timezone info and treat it as UTC
      let dateToProcess = date;
      
      // If the date string doesn't end with 'Z' or have timezone offset, treat as UTC
      if (!/[+-]\d{2}:?\d{2}$|Z$/.test(date)) {
        // Add 'Z' to treat as UTC time
        dateToProcess = date + 'Z';
      }
      
      let parsedDate = new Date(dateToProcess);
      timestamp = parsedDate.getTime();
      
      // If parsing failed, try alternative parsing
      if (isNaN(timestamp)) {
        // Try parsing as a timestamp if it looks like one
        if (/^\d+$/.test(date)) {
          timestamp = parseInt(date);
          // Convert to milliseconds if it looks like seconds
          if (timestamp < 10000000000) {
            timestamp *= 1000;
          }
        } else {
          // Try original string without timezone modification
          parsedDate = new Date(date);
          timestamp = parsedDate.getTime();
          
          if (isNaN(timestamp)) {
            return { text: 'Unknown time', timestamp: 0 };
          }
        }
      }
    } else {
      timestamp = Number(date);
    }
    
    let timeDiff = now - timestamp;
    
    // Handle potential timezone issues by checking if the difference is too large
    // If the server time is significantly different, adjust
    const maxReasonableDiff = 7 * 24 * 60 * 60 * 1000; // 7 days
    if (Math.abs(timeDiff) > maxReasonableDiff) {
      // Something is wrong with the time calculation, fallback to a simpler approach
      
      // Try using local time calculation
      const localDate = new Date(date);
      const localNow = new Date();
      timeDiff = localNow.getTime() - localDate.getTime();
    }
    
    // Handle invalid dates
    if (isNaN(timestamp) || timestamp === 0) {
      return { text: 'Unknown time', timestamp: 0 };
    }
    
    // If the time difference is negative (future date), handle it
    if (timeDiff < 0) {
      return { text: 'Just now', timestamp };
    }
    
    const seconds = Math.floor(timeDiff / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);
    const weeks = Math.floor(days / 7);
    const months = Math.floor(days / 30);
    
    let timeText = '';
    
    if (months > 0) {
      timeText = `${months} month${months > 1 ? 's' : ''} ago`;
    } else if (weeks > 0) {
      timeText = `${weeks} week${weeks > 1 ? 's' : ''} ago`;
    } else if (days > 0) {
      timeText = `${days} day${days > 1 ? 's' : ''} ago`;
    } else if (hours > 0) {
      timeText = `${hours} hour${hours > 1 ? 's' : ''} ago`;
    } else if (minutes > 0) {
      timeText = `${minutes} minute${minutes > 1 ? 's' : ''} ago`;
    } else if (seconds > 10) {
      timeText = `${seconds} second${seconds > 1 ? 's' : ''} ago`;
    } else {
      timeText = 'Just now';
    }
    
    
    return { text: timeText, timestamp };
  };

  const generateEnhancedRecentActivity = (parcelsData: Parcel[], dispatchesData: Dispatch[], usersData: User[]): ActivityItem[] => {
    const activities: ActivityItem[] = [];
    const now = Date.now();
    const last24Hours = now - (24 * 60 * 60 * 1000);
    
    
    // Recent parcel activities (get latest parcels regardless of 24h filter for now)
    const recentParcels = parcelsData
      .sort((a, b) => new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime())
      .slice(0, 8); // Get the 8 most recent parcels

    
    recentParcels.forEach((parcel) => {
      const timeInfo = formatRelativeTime(parcel.createdAt);
      

      // Parcel creation activity
      activities.push({
        id: `parcel-created-${parcel.id}`,
        type: 'parcel_created',
        action: `New parcel ${parcel.waybillNumber} created for ${parcel.destination}`,
        time: timeInfo.text,
        timestamp: new Date(parcel.createdAt).getTime(), // Use the original date directly
        status: 'info',
        icon: Package,
        entity: parcel.waybillNumber,
        details: { destination: parcel.destination, amount: parcel.totalAmount }
      });
      
      // Status change activities
      if (parcel.status === 3 && parcel.dispatchedAt) {
        const deliveryTime = formatRelativeTime(parcel.dispatchedAt);
        if (new Date(parcel.dispatchedAt).getTime() > last24Hours) {
          activities.push({
            id: `parcel-delivered-${parcel.id}`,
            type: 'parcel_delivered',
            action: `Parcel ${parcel.waybillNumber} delivered successfully`,
            time: deliveryTime.text,
            timestamp: deliveryTime.timestamp,
            status: 'success',
            icon: CheckCircle,
            entity: parcel.waybillNumber,
            details: { destination: parcel.destination }
          });
        }
      } else if (parcel.status === 2) {
        activities.push({
          id: `parcel-transit-${parcel.id}`,
          type: 'parcel_status_change',
          action: `Parcel ${parcel.waybillNumber} is now in transit`,
          time: timeInfo.text,
          timestamp: timeInfo.timestamp,
          status: 'warning',
          icon: ArrowRightLeft,
          entity: parcel.waybillNumber,
          details: { destination: parcel.destination }
        });
      }
      
      // Recent updates (if updatedAt is different from createdAt)
      if (parcel.updatedAt && parcel.updatedAt !== parcel.createdAt) {
        const updateTime = formatRelativeTime(parcel.updatedAt);
        if (new Date(parcel.updatedAt).getTime() > last24Hours) {
          activities.push({
            id: `parcel-updated-${parcel.id}`,
            type: 'parcel_updated',
            action: `Parcel ${parcel.waybillNumber} details updated`,
            time: updateTime.text,
            timestamp: updateTime.timestamp,
            status: 'info',
            icon: Edit,
            entity: parcel.waybillNumber,
            details: { destination: parcel.destination }
          });
        }
      }
    });
    
    // Recent dispatch activities
    const recentDispatches = dispatchesData
      .filter(dispatch => new Date(dispatch.dispatchTime).getTime() > last24Hours)
      .sort((a, b) => new Date(b.dispatchTime).getTime() - new Date(a.dispatchTime).getTime())
      .slice(0, 5);
    
    recentDispatches.forEach((dispatch) => {
      const timeInfo = formatRelativeTime(dispatch.dispatchTime);
      
      activities.push({
        id: `dispatch-created-${dispatch.id}`,
        type: 'dispatch_created',
        action: `Dispatch ${dispatch.dispatchCode} created to ${dispatch.destination}`,
        time: timeInfo.text,
        timestamp: timeInfo.timestamp,
        status: 'info',
        icon: Truck,
        entity: dispatch.dispatchCode,
        details: { 
          destination: dispatch.destination, 
          vehicle: dispatch.vehicleNumber, 
          driver: dispatch.driver,
          parcelCount: Array.isArray(dispatch.parcelIds) ? dispatch.parcelIds.length : 0
        }
      });
    });
    
    // Recent user activities (only for admins)
    if (isAdmin() && usersData.length > 0) {
      const recentUsers = usersData
        .filter(user => user.createdAt && new Date(user.createdAt).getTime() > last24Hours)
        .sort((a, b) => new Date(b.createdAt!).getTime() - new Date(a.createdAt!).getTime())
        .slice(0, 3);
      
      recentUsers.forEach((userData) => {
        const timeInfo = formatRelativeTime(userData.createdAt!);
        
        activities.push({
          id: `user-created-${userData.id}`,
          type: 'user_created',
          action: `New user ${userData.firstName} ${userData.lastName} added to system`,
          time: timeInfo.text,
          timestamp: timeInfo.timestamp,
          status: 'success',
          icon: UserPlus,
          entity: userData.username,
          details: { 
            role: typeof userData.role === 'string' ? userData.role : userData.role?.name,
            branch: typeof userData.branch === 'string' ? userData.branch : userData.branch?.name
          }
        });
      });
    }
    
    // Sort all activities by timestamp (most recent first) and limit to 8
    return activities
      .sort((a, b) => b.timestamp - a.timestamp)
      .slice(0, 8);
  };

  const calculateStats = () => {
    const today = new Date();
    today.setHours(0, 0, 0, 0);
    
    // Get yesterday for comparison
    const yesterday = new Date(today);
    yesterday.setDate(yesterday.getDate() - 1);
    
    // Current counts
    const totalParcels = parcels.length;
    const pendingParcels = parcels.filter(p => p.status === 0).length;
    const inTransitParcels = parcels.filter(p => p.status === 2).length;
    const deliveredToday = parcels.filter(p => {
      const deliveredDate = p.dispatchedAt ? new Date(p.dispatchedAt) : null;
      return deliveredDate && deliveredDate >= today && p.status === 3;
    }).length;
    
    // Yesterday counts for comparison
    const parcelsCreatedToday = parcels.filter(p => {
      const createdDate = new Date(p.createdAt);
      return createdDate >= today;
    }).length;
    
    const parcelsCreatedYesterday = parcels.filter(p => {
      const createdDate = new Date(p.createdAt);
      return createdDate >= yesterday && createdDate < today;
    }).length;
    
    // Active dispatches (in transit or pending)
    const activeDispatches = dispatches.filter(d => 
      d.status === 'in_transit' || d.status === 'pending'
    ).length;
    
    const dispatchesToday = dispatches.filter(d => {
      const dispatchDate = new Date(d.dispatchTime);
      return dispatchDate >= today;
    }).length;
    
    const dispatchesYesterday = dispatches.filter(d => {
      const dispatchDate = new Date(d.dispatchTime);
      return dispatchDate >= yesterday && dispatchDate < today;
    }).length;
    
    // Calculate percentage changes
    const parcelChange = parcelsCreatedYesterday === 0 ? '+100%' : 
      parcelsCreatedToday > parcelsCreatedYesterday ? 
        `+${(((parcelsCreatedToday - parcelsCreatedYesterday) / parcelsCreatedYesterday) * 100).toFixed(1)}%` :
        `${(((parcelsCreatedToday - parcelsCreatedYesterday) / parcelsCreatedYesterday) * 100).toFixed(1)}%`;
    
    const dispatchChange = dispatchesYesterday === 0 ? '+100%' : 
      dispatchesToday > dispatchesYesterday ? 
        `+${(((dispatchesToday - dispatchesYesterday) / dispatchesYesterday) * 100).toFixed(1)}%` :
        `${(((dispatchesToday - dispatchesYesterday) / dispatchesYesterday) * 100).toFixed(1)}%`;
    
    return [
      {
        title: isBranchManager() ? 'Branch Parcels' : 'Total Parcels',
        value: totalParcels.toLocaleString(),
        change: parcelChange,
        changeType: parcelsCreatedToday >= parcelsCreatedYesterday ? 'increase' as const : 'decrease' as const,
        icon: Package,
        color: 'text-brand-600'
      },
      {
        title: 'Active Dispatches',
        value: activeDispatches.toString(),
        change: dispatchChange,
        changeType: dispatchesToday >= dispatchesYesterday ? 'increase' as const : 'decrease' as const,
        icon: Truck,
        color: 'text-blue-600'
      },
      {
        title: 'Delivered Today',
        value: deliveredToday.toString(),
        change: deliveredToday > 0 ? '+100%' : '0%',
        changeType: 'increase' as const,
        icon: CheckCircle,
        color: 'text-success-600'
      },
      {
        title: 'Pending Parcels',
        value: pendingParcels.toString(),
        change: pendingParcels > inTransitParcels ? `+${pendingParcels - inTransitParcels}` : `${pendingParcels - inTransitParcels}`,
        changeType: pendingParcels > inTransitParcels ? 'increase' as const : 'decrease' as const,
        icon: Clock,
        color: 'text-warning-600'
      }
    ];
  };

  const stats = calculateStats();

  const allQuickActions = [
    {
      title: 'View All Parcels',
      description: 'Manage and track parcels',
      href: '/parcels',
      icon: Package,
      roles: ['admin', 'manager']
    },
    {
      title: 'Create Dispatch',
      description: 'Create new dispatch orders',
      href: '/dispatches/create',
      icon: Plus,
      roles: ['admin', 'manager']
    },
    {
      title: 'View Dispatches',
      description: 'Track dispatch operations',
      href: '/dispatches/view',
      icon: Truck,
      roles: ['admin', 'manager']
    },
    {
      title: 'View Reports',
      description: 'Analytics and insights',
      href: '/reports',
      icon: BarChart3,
      roles: ['admin']
    },
    {
      title: 'User Management',
      description: 'Manage system users',
      href: '/user-management',
      icon: Users,
      roles: ['admin']
    },
    {
      title: 'Contract Customers',
      description: 'Manage contract customers',
      href: '/contract-customers',
      icon: Building2,
      roles: ['admin']
    },
    {
      title: 'Invoices',
      description: 'Manage invoices',
      href: '/invoices',
      icon: FileText,
      roles: ['admin']
    },
    {
      title: 'Manage Parcels',
      description: 'Admin parcel management',
      href: '/admin/parcels',
      icon: ShieldCheck,
      roles: ['admin']
    }
  ];

  // Filter quick actions based on user role
  const quickActions = allQuickActions.filter(action => {
    const userRole = typeof user?.role === 'string' ? user.role : (user?.role as any)?.Name || '';
    const roleName = userRole.toLowerCase();
    return action.roles.includes(roleName) || (isAdmin() && action.roles.includes('admin'));
  });

  return (
    <div className="space-y-8">
      {/* Welcome Header */}
      <Card>
        <div className="flex items-center justify-between">
          <div>
            <h1 className="text-title-md font-bold text-gray-900 dark:text-white">
              Welcome back, {user?.firstName && user?.lastName ? `${user.firstName} ${user.lastName}` : user?.username}!
            </h1>
            <p className="mt-2 text-gray-600 dark:text-gray-400">
              {isBranchManager() && user?.branch?.name 
                ? `Here's an overview of ${user.branch.name} branch operations`
                : "Here's an overview of your warehouse management system"
              }
            </p>
          </div>
          <div className="flex items-center space-x-2">
            <Badge variant="primary">
              {(() => {
                const roleName = typeof user?.role === 'string' ? user.role : (user?.role as any)?.Name || '';
                return roleName.replace('_', ' ').toUpperCase();
              })()}
            </Badge>
            {isBranchManager() && user?.branch?.name && (
              <Badge variant="gray">
                {user.branch.name}
              </Badge>
            )}
          </div>
        </div>
      </Card>

      {/* Statistics Grid - Hidden for Accountants */}
      {!isAccountant() && (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
          {loading ? (
            // Loading skeleton
            Array.from({ length: 4 }).map((_, index) => (
              <Card key={index}>
                <div className="flex items-center justify-between">
                  <div className="flex-1">
                    <div className="h-4 bg-gray-200 dark:bg-gray-700 rounded animate-pulse mb-2"></div>
                    <div className="h-8 bg-gray-200 dark:bg-gray-700 rounded animate-pulse mb-2"></div>
                    <div className="h-3 bg-gray-200 dark:bg-gray-700 rounded animate-pulse w-24"></div>
                  </div>
                  <div className="w-12 h-12 bg-gray-200 dark:bg-gray-700 rounded-lg animate-pulse"></div>
                </div>
              </Card>
            ))
          ) : (
            stats.map((stat) => {
              const IconComponent = stat.icon;
              return (
                <Card key={stat.title}>
                  <div className="flex items-center justify-between">
                    <div>
                      <p className="text-theme-sm font-medium text-gray-600 dark:text-gray-400">
                        {stat.title}
                      </p>
                      <p className="text-2xl font-bold text-gray-900 dark:text-white mt-1">
                        {stat.value}
                      </p>
                      <div className="flex items-center mt-2">
                        <TrendingUp className={`w-4 h-4 mr-1 ${
                          stat.changeType === 'increase' ? 'text-success-600' : 'text-error-600'
                        }`} />
                        <span className={`text-theme-xs font-medium ${
                          stat.changeType === 'increase' ? 'text-success-600' : 'text-error-600'
                        }`}>
                          {stat.change}
                        </span>
                        <span className="text-theme-xs text-gray-500 ml-1">vs last month</span>
                      </div>
                    </div>
                    <div className={`p-3 rounded-lg bg-gray-50 dark:bg-gray-800`}>
                      <IconComponent className={`w-6 h-6 ${stat.color}`} />
                    </div>
                  </div>
                </Card>
              );
            })
          )}
        </div>
      )}

      {/* Quick Actions and Recent Activity */}
      <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
        {/* Quick Actions */}
        <Card padding={false}>
          <Card.Header>
            <h2 className="text-lg font-semibold text-gray-900 dark:text-white">Quick Actions</h2>
          </Card.Header>
          <Card.Content>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
              {quickActions.map((action) => {
                const IconComponent = action.icon;
                return (
                  <Link
                    key={action.title}
                    to={action.href}
                    className="p-4 border border-gray-200 dark:border-gray-700 rounded-lg hover:border-brand-300 hover:bg-brand-50 dark:hover:bg-brand-500/5 transition-all duration-200 group"
                  >
                    <div className="flex items-start space-x-3">
                      <div className="p-2 rounded-md bg-brand-100 dark:bg-brand-500/20 group-hover:bg-brand-200 dark:group-hover:bg-brand-500/30 transition-colors">
                        <IconComponent className="w-5 h-5 text-brand-600 dark:text-brand-400" />
                      </div>
                      <div>
                        <h3 className="font-medium text-gray-900 dark:text-white group-hover:text-brand-700 dark:group-hover:text-brand-300">
                          {action.title}
                        </h3>
                        <p className="text-theme-sm text-gray-600 dark:text-gray-400">
                          {action.description}
                        </p>
                      </div>
                    </div>
                  </Link>
                );
              })}
            </div>
          </Card.Content>
        </Card>

        {/* Recent Activity */}
        <Card padding={false}>
          <Card.Header>
            <div className="flex items-center justify-between">
              <h2 className="text-lg font-semibold text-gray-900 dark:text-white">Recent Activity</h2>
              <div className="flex items-center space-x-2 text-xs text-gray-500 dark:text-gray-400">
                <div className="w-2 h-2 rounded-full bg-green-500 animate-pulse"></div>
                <span>Live</span>
              </div>
            </div>
          </Card.Header>
          <Card.Content>
            <div className="space-y-3">
              {loading ? (
                // Loading skeleton
                Array.from({ length: 4 }).map((_, index) => (
                  <div key={index} className="flex items-start space-x-3">
                    <div className="w-8 h-8 bg-gray-200 dark:bg-gray-700 rounded-lg animate-pulse"></div>
                    <div className="flex-1 min-w-0">
                      <div className="h-4 bg-gray-200 dark:bg-gray-700 rounded animate-pulse mb-1"></div>
                      <div className="h-3 bg-gray-200 dark:bg-gray-700 rounded animate-pulse w-20"></div>
                    </div>
                  </div>
                ))
              ) : recentActivity.length > 0 ? (
                recentActivity.map((activity) => {
                  const IconComponent = activity.icon;
                  return (
                    <div key={activity.id} className="flex items-start space-x-3 group hover:bg-gray-50 dark:hover:bg-gray-800/50 rounded-lg p-2 -m-2 transition-colors">
                      <div className={`p-1.5 rounded-lg ${
                        activity.status === 'success' ? 'bg-green-100 dark:bg-green-500/20' :
                        activity.status === 'warning' ? 'bg-yellow-100 dark:bg-yellow-500/20' :
                        activity.status === 'error' ? 'bg-red-100 dark:bg-red-500/20' :
                        'bg-blue-100 dark:bg-blue-500/20'
                      }`}>
                        <IconComponent className={`w-4 h-4 ${
                          activity.status === 'success' ? 'text-green-600 dark:text-green-400' :
                          activity.status === 'warning' ? 'text-yellow-600 dark:text-yellow-400' :
                          activity.status === 'error' ? 'text-red-600 dark:text-red-400' :
                          'text-blue-600 dark:text-blue-400'
                        }`} />
                      </div>
                      <div className="flex-1 min-w-0">
                        <p className="text-sm text-gray-900 dark:text-white group-hover:text-gray-700 dark:group-hover:text-gray-200">
                          {activity.action}
                        </p>
                        <div className="flex items-center justify-between mt-1">
                          <p className="text-xs text-gray-500 dark:text-gray-400">
                            {activity.time}
                          </p>
                          {activity.entity && (
                            <Badge variant="gray" className="text-xs">
                              {activity.entity}
                            </Badge>
                          )}
                        </div>
                      </div>
                    </div>
                  );
                })
              ) : (
                <div className="text-center py-8">
                  <Clock className="mx-auto h-8 w-8 text-gray-400 mb-2" />
                  <p className="text-sm text-gray-500 dark:text-gray-400">No recent activity</p>
                  <p className="text-xs text-gray-400 dark:text-gray-500">Activities from the last 24 hours will appear here</p>
                </div>
              )}
            </div>
            {recentActivity.length > 0 && (
              <div className="mt-4 pt-4 border-t border-gray-200 dark:border-gray-700">
                <div className="flex items-center justify-between text-xs text-gray-500 dark:text-gray-400">
                  <span>Last updated: {lastRefresh.toLocaleTimeString()}</span>
                  <Button 
                    variant="outline" 
                    size="sm" 
                    onClick={fetchDashboardData}
                    disabled={loading}
                    className="text-xs"
                  >
                    {loading ? 'Refreshing...' : 'Refresh'}
                  </Button>
                </div>
              </div>
            )}
          </Card.Content>
        </Card>
      </div>

      {/* System Status */}
      <Card>
        <div className="flex items-center justify-between">
          <div className="flex items-center space-x-3">
            <div className="w-3 h-3 bg-success-500 rounded-full"></div>
            <div>
              <h3 className="font-medium text-gray-900 dark:text-white">System Status</h3>
              <p className="text-theme-sm text-gray-600 dark:text-gray-400">
                All systems operational. Last updated: {new Date().toLocaleTimeString()}
              </p>
            </div>
          </div>
          <Button variant="outline" size="sm">
            View Details
          </Button>
        </div>
      </Card>
    </div>
  );
};

export default Dashboard; 