import React from 'react';
import { clsx } from 'clsx';

interface InputProps extends Omit<React.InputHTMLAttributes<HTMLInputElement>, 'size'> {
  label?: string;
  error?: string;
  helperText?: string;
  icon?: React.ReactNode;
  variant?: 'default' | 'filled' | 'underlined';
  size?: 'sm' | 'md' | 'lg';
}

const Input: React.FC<InputProps> = ({
  label,
  error,
  helperText,
  icon,
  variant = 'default',
  size = 'md',
  className,
  id,
  ...props
}) => {
  const inputId = id || `input-${Math.random().toString(36).slice(2, 11)}`;

  const sizeClasses = {
    sm: 'px-3 py-2 text-sm',
    md: 'px-3 py-2.5 text-sm', 
    lg: 'px-4 py-3 text-base',
  };

  const variantClasses = {
    default: 'border border-gray-200 rounded-lg bg-white dark:bg-gray-800 dark:border-gray-700',
    filled: 'border-0 rounded-lg bg-gray-100 dark:bg-gray-800',
    underlined: 'border-0 border-b-2 border-gray-200 rounded-none bg-transparent dark:border-gray-700'
  };

  return (
    <div className="w-full">
      {label && (
        <label 
          htmlFor={inputId}
          className="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300"
        >
          {label}
        </label>
      )}
      
      <div className="relative">
        {icon && (
          <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
            <span className="text-gray-400 dark:text-gray-500">{icon}</span>
          </div>
        )}
        
        <input
          id={inputId}
          className={clsx(
            'block w-full shadow-sm placeholder-gray-400 focus:outline-none transition-all duration-200 dark:text-white dark:placeholder-gray-500',
            sizeClasses[size],
            variantClasses[variant],
            icon && 'pl-10',
            error 
              ? 'border-error-500 focus:border-error-500 focus:ring-4 focus:ring-error-100' 
              : 'focus:ring-4 focus:ring-brand-100 focus:border-brand-500 dark:focus:border-brand-400',
            variant === 'underlined' && 'focus:border-brand-500',
            className
          )}
          {...props}
        />
      </div>
      
      {error && (
        <p className="mt-1 text-sm text-error-600 dark:text-error-400">{error}</p>
      )}
      
      {helperText && !error && (
        <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">{helperText}</p>
      )}
    </div>
  );
};

export default Input;