import React, { useState } from 'react';
import { Calendar, ChevronDown, X } from 'lucide-react';
import { clsx } from 'clsx';

interface DatePickerProps extends Omit<React.InputHTMLAttributes<HTMLInputElement>, 'size'> {
  label?: string;
  error?: string;
  helperText?: string;
  size?: 'sm' | 'md' | 'lg';
  placeholder?: string;
  showClearButton?: boolean;
  onClear?: () => void;
}

interface DateRangePickerProps {
  label?: string;
  startDate: string;
  endDate: string;
  onStartDateChange: (date: string) => void;
  onEndDateChange: (date: string) => void;
  onClear?: () => void;
  error?: string;
  helperText?: string;
  size?: 'sm' | 'md' | 'lg';
  placeholder?: {
    start?: string;
    end?: string;
  };
  showClearButton?: boolean;
  className?: string;
}

interface DateRangePreset {
  label: string;
  value: string;
  getDates: () => { start: string; end: string };
}

const DatePicker = React.forwardRef<HTMLInputElement, DatePickerProps>(
  ({ 
    label, 
    error, 
    helperText, 
    size = 'md',
    placeholder = 'Select date',
    showClearButton = false,
    onClear,
    className, 
    id,
    value,
    ...props 
  }, ref) => {
    const inputId = id || `datepicker-${Math.random().toString(36).slice(2, 11)}`;

    const sizeClasses = {
      sm: 'px-3 py-2 text-sm',
      md: 'px-3 py-2.5 text-sm', 
      lg: 'px-4 py-3 text-base',
    };

    const handleClear = () => {
      if (onClear) {
        onClear();
      }
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
          <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
            <Calendar className="w-4 h-4 text-gray-400 dark:text-gray-500" />
          </div>
          
          <input
            ref={ref}
            id={inputId}
            type="date"
            className={clsx(
              'block w-full pl-10 pr-10 shadow-sm placeholder-gray-400 focus:outline-none transition-all duration-200 dark:text-white dark:placeholder-gray-500',
              'border border-gray-200 rounded-lg bg-white dark:bg-gray-800 dark:border-gray-700',
              sizeClasses[size],
              error 
                ? 'border-error-500 focus:border-error-500 focus:ring-4 focus:ring-error-100' 
                : 'focus:ring-4 focus:ring-brand-100 focus:border-brand-500 dark:focus:border-brand-400',
              className
            )}
            placeholder={placeholder}
            value={value}
            {...props}
          />
          
          {showClearButton && value && (
            <button
              type="button"
              onClick={handleClear}
              className="absolute inset-y-0 right-0 pr-3 flex items-center text-gray-400 hover:text-gray-600 dark:hover:text-gray-300"
            >
              <X className="w-4 h-4" />
            </button>
          )}
        </div>
        
        {error && (
          <p className="mt-1 text-sm text-error-600 dark:text-error-400">{error}</p>
        )}
        
        {helperText && !error && (
          <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">{helperText}</p>
        )}
      </div>
    );
  }
);

DatePicker.displayName = 'DatePicker';

const DateRangePicker: React.FC<DateRangePickerProps> = ({
  label,
  startDate,
  endDate,
  onStartDateChange,
  onEndDateChange,
  onClear,
  error,
  helperText,
  size = 'md',
  placeholder = { start: 'Start date', end: 'End date' },
  showClearButton = false,
  className
}) => {
  const [showPresets, setShowPresets] = useState(false);

  const sizeClasses = {
    sm: 'px-3 py-2 text-sm',
    md: 'px-3 py-2.5 text-sm', 
    lg: 'px-4 py-3 text-base',
  };

  const presets: DateRangePreset[] = [
    {
      label: 'Today',
      value: 'today',
      getDates: () => {
        const today = new Date().toISOString().split('T')[0];
        return { start: today, end: today };
      }
    },
    {
      label: 'Yesterday',
      value: 'yesterday',
      getDates: () => {
        const yesterday = new Date();
        yesterday.setDate(yesterday.getDate() - 1);
        const dateStr = yesterday.toISOString().split('T')[0];
        return { start: dateStr, end: dateStr };
      }
    },
    {
      label: 'Last 7 days',
      value: 'last7days',
      getDates: () => {
        const end = new Date();
        const start = new Date();
        start.setDate(start.getDate() - 6);
        return { 
          start: start.toISOString().split('T')[0], 
          end: end.toISOString().split('T')[0] 
        };
      }
    },
    {
      label: 'Last 30 days',
      value: 'last30days',
      getDates: () => {
        const end = new Date();
        const start = new Date();
        start.setDate(start.getDate() - 29);
        return { 
          start: start.toISOString().split('T')[0], 
          end: end.toISOString().split('T')[0] 
        };
      }
    },
    {
      label: 'This month',
      value: 'thismonth',
      getDates: () => {
        const now = new Date();
        const start = new Date(now.getFullYear(), now.getMonth(), 1);
        const end = new Date(now.getFullYear(), now.getMonth() + 1, 0);
        return { 
          start: start.toISOString().split('T')[0], 
          end: end.toISOString().split('T')[0] 
        };
      }
    },
    {
      label: 'Last month',
      value: 'lastmonth',
      getDates: () => {
        const now = new Date();
        const start = new Date(now.getFullYear(), now.getMonth() - 1, 1);
        const end = new Date(now.getFullYear(), now.getMonth(), 0);
        return { 
          start: start.toISOString().split('T')[0], 
          end: end.toISOString().split('T')[0] 
        };
      }
    }
  ];

  const handlePresetSelect = (preset: DateRangePreset) => {
    const dates = preset.getDates();
    onStartDateChange(dates.start);
    onEndDateChange(dates.end);
    setShowPresets(false);
  };

  const handleClear = () => {
    onStartDateChange('');
    onEndDateChange('');
    if (onClear) {
      onClear();
    }
  };

  const hasValue = startDate || endDate;

  return (
    <div className={clsx('w-full', className)}>
      {label && (
        <label className="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">
          {label}
        </label>
      )}
      
      <div className="relative">
        {/* Preset Dropdown */}
        <div className="relative mb-2">
          <button
            type="button"
            onClick={() => setShowPresets(!showPresets)}
            className={clsx(
              'flex items-center justify-between w-full px-3 py-2 text-sm border border-gray-200 rounded-lg bg-white dark:bg-gray-800 dark:border-gray-700 hover:border-gray-300 dark:hover:border-gray-600',
              sizeClasses[size]
            )}
          >
            <span className="text-gray-500 dark:text-gray-400">Quick Select</span>
            <ChevronDown className={clsx('w-4 h-4 text-gray-400 transition-transform', showPresets && 'rotate-180')} />
          </button>
          
          {showPresets && (
            <div className="absolute top-full left-0 right-0 z-10 mt-1 bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg shadow-lg">
              {presets.map((preset) => (
                <button
                  key={preset.value}
                  type="button"
                  onClick={() => handlePresetSelect(preset)}
                  className="w-full px-3 py-2 text-left text-sm text-gray-700 dark:text-gray-300 hover:bg-gray-100 dark:hover:bg-gray-700 first:rounded-t-lg last:rounded-b-lg"
                >
                  {preset.label}
                </button>
              ))}
            </div>
          )}
        </div>

        {/* Date Inputs */}
        <div className="grid grid-cols-2 gap-2">
          <div className="relative">
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <Calendar className="w-4 h-4 text-gray-400 dark:text-gray-500" />
            </div>
            <input
              type="date"
              value={startDate}
              onChange={(e) => onStartDateChange(e.target.value)}
              placeholder={placeholder.start}
              className={clsx(
                'block w-full pl-10 pr-3 shadow-sm placeholder-gray-400 focus:outline-none transition-all duration-200 dark:text-white dark:placeholder-gray-500',
                'border border-gray-200 rounded-lg bg-white dark:bg-gray-800 dark:border-gray-700',
                sizeClasses[size],
                error 
                  ? 'border-error-500 focus:border-error-500 focus:ring-4 focus:ring-error-100' 
                  : 'focus:ring-4 focus:ring-brand-100 focus:border-brand-500 dark:focus:border-brand-400'
              )}
            />
          </div>
          
          <div className="relative">
            <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <Calendar className="w-4 h-4 text-gray-400 dark:text-gray-500" />
            </div>
            <input
              type="date"
              value={endDate}
              onChange={(e) => onEndDateChange(e.target.value)}
              placeholder={placeholder.end}
              className={clsx(
                'block w-full pl-10 pr-3 shadow-sm placeholder-gray-400 focus:outline-none transition-all duration-200 dark:text-white dark:placeholder-gray-500',
                'border border-gray-200 rounded-lg bg-white dark:bg-gray-800 dark:border-gray-700',
                sizeClasses[size],
                error 
                  ? 'border-error-500 focus:border-error-500 focus:ring-4 focus:ring-error-100' 
                  : 'focus:ring-4 focus:ring-brand-100 focus:border-brand-500 dark:focus:border-brand-400'
              )}
            />
          </div>
        </div>

        {/* Clear Button */}
        {showClearButton && hasValue && (
          <button
            type="button"
            onClick={handleClear}
            className="absolute -top-8 right-0 p-1 text-gray-400 hover:text-gray-600 dark:hover:text-gray-300"
          >
            <X className="w-4 h-4" />
          </button>
        )}
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

export default DatePicker;
export { DateRangePicker };