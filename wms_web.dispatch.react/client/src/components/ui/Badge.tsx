import React from 'react';
import { clsx } from 'clsx';

interface BadgeProps {
  children: React.ReactNode;
  variant?: 'primary' | 'success' | 'warning' | 'error' | 'gray';
  color?: 'primary' | 'success' | 'warning' | 'error' | 'gray' | 'blue' | 'green' | 'red' | 'yellow';
  size?: 'sm' | 'md';
  className?: string;
}

const Badge: React.FC<BadgeProps> = ({ 
  children, 
  variant = 'gray',
  color,
  size = 'md',
  className 
}) => {
  const baseClasses = 'inline-flex items-center rounded-full font-medium';
  
  const variantClasses = {
    primary: 'bg-brand-100 text-brand-800',
    success: 'bg-success-100 text-success-800',
    warning: 'bg-warning-100 text-warning-800',
    error: 'bg-error-100 text-error-800',
    gray: 'bg-gray-100 text-gray-800 dark:bg-gray-800 dark:text-gray-300',
  };

  const colorClasses = {
    primary: 'bg-brand-100 text-brand-800',
    success: 'bg-success-100 text-success-800',
    warning: 'bg-warning-100 text-warning-800',
    error: 'bg-error-100 text-error-800',
    gray: 'bg-gray-100 text-gray-800 dark:bg-gray-800 dark:text-gray-300',
    blue: 'bg-blue-100 text-blue-800',
    green: 'bg-green-100 text-green-800',
    red: 'bg-red-100 text-red-800',
    yellow: 'bg-yellow-100 text-yellow-800',
  };

  const sizeClasses = {
    sm: 'px-2 py-0.5 text-xs',
    md: 'px-2.5 py-0.5 text-xs',
  };

  const selectedClasses = color ? colorClasses[color] : variantClasses[variant];

  return (
    <span
      className={clsx(
        baseClasses,
        selectedClasses,
        sizeClasses[size],
        className
      )}
    >
      {children}
    </span>
  );
};

export default Badge;