import React from 'react';
import { X } from 'lucide-react';
import { clsx } from 'clsx';

interface FilterChipProps {
  label: string;
  value: string;
  onRemove: () => void;
  variant?: 'default' | 'primary' | 'success' | 'warning' | 'error';
  size?: 'sm' | 'md';
  className?: string;
}

const FilterChip: React.FC<FilterChipProps> = ({
  label,
  value,
  onRemove,
  variant = 'default',
  size = 'sm',
  className
}) => {
  const variantClasses = {
    default: 'bg-gray-100 text-gray-800 hover:bg-gray-200 dark:bg-gray-700 dark:text-gray-200 dark:hover:bg-gray-600',
    primary: 'bg-brand-100 text-brand-800 hover:bg-brand-200 dark:bg-brand-500/20 dark:text-brand-400 dark:hover:bg-brand-500/30',
    success: 'bg-success-100 text-success-800 hover:bg-success-200 dark:bg-success-500/20 dark:text-success-400 dark:hover:bg-success-500/30',
    warning: 'bg-warning-100 text-warning-800 hover:bg-warning-200 dark:bg-warning-500/20 dark:text-warning-400 dark:hover:bg-warning-500/30',
    error: 'bg-error-100 text-error-800 hover:bg-error-200 dark:bg-error-500/20 dark:text-error-400 dark:hover:bg-error-500/30'
  };

  const sizeClasses = {
    sm: 'px-2 py-1 text-xs',
    md: 'px-3 py-1.5 text-sm'
  };

  return (
    <div
      className={clsx(
        'inline-flex items-center gap-1 rounded-full font-medium transition-colors duration-200',
        variantClasses[variant],
        sizeClasses[size],
        className
      )}
    >
      <span className="font-medium">{label}:</span>
      <span>{value}</span>
      <button
        onClick={onRemove}
        className="ml-1 rounded-full p-0.5 hover:bg-black/10 dark:hover:bg-white/10 transition-colors duration-200"
        title={`Remove ${label} filter`}
      >
        <X className="w-3 h-3" />
      </button>
    </div>
  );
};

export default FilterChip;