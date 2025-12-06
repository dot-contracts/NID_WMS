import React, { useState, useRef, useEffect } from 'react';
import { ChevronDown, Search, X, Check } from 'lucide-react';
import { clsx } from 'clsx';

interface SelectOption {
  value: string;
  label: string;
  disabled?: boolean;
  icon?: React.ReactNode;
  description?: string;
}

interface SelectProps extends Omit<React.SelectHTMLAttributes<HTMLSelectElement>, 'size'> {
  label?: string;
  error?: string;
  helperText?: string;
  size?: 'sm' | 'md' | 'lg';
  options: SelectOption[];
  placeholder?: string;
  searchable?: boolean;
  clearable?: boolean;
  multiple?: boolean;
  onClear?: () => void;
  loading?: boolean;
  emptyMessage?: string;
}

interface MultiSelectProps extends Omit<SelectProps, 'multiple' | 'value' | 'onChange'> {
  value: string[];
  onChange: (value: string[]) => void;
}

const Select = React.forwardRef<HTMLSelectElement, SelectProps>(
  ({ 
    label, 
    error, 
    helperText, 
    size = 'md',
    options,
    placeholder,
    searchable = false,
    clearable = false,
    multiple = false,
    onClear,
    loading = false,
    emptyMessage = 'No options available',
    className, 
    id,
    value,
    onChange,
    ...props 
  }, ref) => {
    const inputId = id || `select-${Math.random().toString(36).slice(2, 11)}`;
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [focusedIndex, setFocusedIndex] = useState(-1);
    const dropdownRef = useRef<HTMLDivElement>(null);
    const searchInputRef = useRef<HTMLInputElement>(null);

    const sizeClasses = {
      sm: 'px-3 py-2 text-sm',
      md: 'px-3 py-2.5 text-sm', 
      lg: 'px-4 py-3 text-base',
    };

    // Filter options based on search term
    const filteredOptions = options.filter(option =>
      option.label.toLowerCase().includes(searchTerm.toLowerCase()) ||
      option.value.toLowerCase().includes(searchTerm.toLowerCase())
    );

    // Handle click outside to close dropdown
    useEffect(() => {
      const handleClickOutside = (event: MouseEvent) => {
        if (dropdownRef.current && !dropdownRef.current.contains(event.target as Node)) {
          setIsOpen(false);
          setSearchTerm('');
          setFocusedIndex(-1);
        }
      };

      document.addEventListener('mousedown', handleClickOutside);
      return () => document.removeEventListener('mousedown', handleClickOutside);
    }, []);

    // Handle keyboard navigation
    const handleKeyDown = (event: React.KeyboardEvent) => {
      if (!isOpen) {
        if (event.key === 'Enter' || event.key === ' ' || event.key === 'ArrowDown') {
          event.preventDefault();
          setIsOpen(true);
          if (searchable && searchInputRef.current) {
            searchInputRef.current.focus();
          }
        }
        return;
      }

      switch (event.key) {
        case 'ArrowDown':
          event.preventDefault();
          setFocusedIndex(prev => 
            prev < filteredOptions.length - 1 ? prev + 1 : 0
          );
          break;
        case 'ArrowUp':
          event.preventDefault();
          setFocusedIndex(prev => 
            prev > 0 ? prev - 1 : filteredOptions.length - 1
          );
          break;
        case 'Enter':
          event.preventDefault();
          if (focusedIndex >= 0 && focusedIndex < filteredOptions.length) {
            handleOptionSelect(filteredOptions[focusedIndex]);
          }
          break;
        case 'Escape':
          event.preventDefault();
          setIsOpen(false);
          setSearchTerm('');
          setFocusedIndex(-1);
          break;
      }
    };

    const handleOptionSelect = (option: SelectOption) => {
      if (option.disabled) return;
      
      if (multiple) {
        const currentValues = Array.isArray(value) ? value : [];
        const newValues = currentValues.includes(option.value)
          ? currentValues.filter(v => v !== option.value)
          : [...currentValues, option.value];
        (onChange as any)(newValues);
      } else {
        onChange?.(option.value as any);
        setIsOpen(false);
        setSearchTerm('');
        setFocusedIndex(-1);
      }
    };

    const handleClear = () => {
      if (multiple) {
        (onChange as any)([]);
      } else {
        onChange?.('' as any);
      }
      if (onClear) {
        onClear();
      }
    };

    const getDisplayValue = () => {
      if (multiple && Array.isArray(value)) {
        if (value.length === 0) return placeholder || 'Select options';
        if (value.length === 1) {
          const option = options.find(opt => opt.value === value[0]);
          return option?.label || value[0];
        }
        return `${value.length} selected`;
      } else {
        const option = options.find(opt => opt.value === value);
        return option?.label || placeholder || 'Select an option';
      }
    };

    const isSelected = (optionValue: string) => {
      if (multiple && Array.isArray(value)) {
        return value.includes(optionValue);
      }
      return value === optionValue;
    };

    const hasValue = multiple ? Array.isArray(value) && value.length > 0 : !!value;

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
        
        <div className="relative" ref={dropdownRef}>
          <button
            type="button"
            onClick={() => setIsOpen(!isOpen)}
            onKeyDown={handleKeyDown}
            className={clsx(
              'block w-full appearance-none shadow-sm focus:outline-none transition-all duration-200 dark:text-white',
              'border border-gray-200 rounded-lg bg-white dark:bg-gray-800 dark:border-gray-700',
              'cursor-pointer text-left',
              sizeClasses[size],
              error 
                ? 'border-error-500 focus:border-error-500 focus:ring-4 focus:ring-error-100' 
                : 'focus:ring-4 focus:ring-brand-100 focus:border-brand-500 dark:focus:border-brand-400',
              className
            )}
            disabled={props.disabled}
          >
            <div className="flex items-center justify-between">
              <span className={clsx(
                'truncate',
                !hasValue && 'text-gray-500 dark:text-gray-400'
              )}>
                {getDisplayValue()}
              </span>
              <div className="flex items-center gap-2">
                {clearable && hasValue && (
                  <button
                    type="button"
                    onClick={(e) => {
                      e.stopPropagation();
                      handleClear();
                    }}
                    className="text-gray-400 hover:text-gray-600 dark:hover:text-gray-300"
                  >
                    <X className="w-4 h-4" />
                  </button>
                )}
                <ChevronDown className={clsx(
                  'w-4 h-4 text-gray-400 dark:text-gray-500 transition-transform',
                  isOpen && 'rotate-180'
                )} />
              </div>
            </div>
          </button>
          
          {isOpen && (
            <div className="absolute top-full left-0 right-0 z-50 mt-1 bg-white dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg shadow-lg max-h-60 overflow-hidden">
              {searchable && (
                <div className="p-2 border-b border-gray-200 dark:border-gray-700">
                  <div className="relative">
                    <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 w-4 h-4 text-gray-400" />
                    <input
                      ref={searchInputRef}
                      type="text"
                      value={searchTerm}
                      onChange={(e) => setSearchTerm(e.target.value)}
                      placeholder="Search options..."
                      className="w-full pl-10 pr-3 py-2 text-sm border border-gray-200 dark:border-gray-600 rounded-md bg-white dark:bg-gray-700 dark:text-white focus:outline-none focus:ring-2 focus:ring-brand-500"
                    />
                  </div>
                </div>
              )}
              
              <div className="max-h-48 overflow-y-auto">
                {loading ? (
                  <div className="p-3 text-center text-gray-500 dark:text-gray-400">
                    <div className="animate-spin rounded-full h-6 w-6 border-b-2 border-brand-500 mx-auto"></div>
                    <p className="mt-2 text-sm">Loading...</p>
                  </div>
                ) : filteredOptions.length === 0 ? (
                  <div className="p-3 text-center text-gray-500 dark:text-gray-400">
                    <p className="text-sm">{emptyMessage}</p>
                  </div>
                ) : (
                  filteredOptions.map((option, index) => (
                    <button
                      key={option.value}
                      type="button"
                      onClick={() => handleOptionSelect(option)}
                      className={clsx(
                        'w-full px-3 py-2 text-left text-sm transition-colors',
                        'hover:bg-gray-100 dark:hover:bg-gray-700',
                        index === focusedIndex && 'bg-gray-100 dark:bg-gray-700',
                        option.disabled && 'opacity-50 cursor-not-allowed',
                        isSelected(option.value) && 'bg-brand-50 dark:bg-brand-500/20 text-brand-700 dark:text-brand-400'
                      )}
                      disabled={option.disabled}
                    >
                      <div className="flex items-center justify-between">
                        <div className="flex items-center gap-2">
                          {option.icon && (
                            <span className="text-gray-400 dark:text-gray-500">
                              {option.icon}
                            </span>
                          )}
                          <div>
                            <div className="font-medium">{option.label}</div>
                            {option.description && (
                              <div className="text-xs text-gray-500 dark:text-gray-400">
                                {option.description}
                              </div>
                            )}
                          </div>
                        </div>
                        {isSelected(option.value) && (
                          <Check className="w-4 h-4 text-brand-600 dark:text-brand-400" />
                        )}
                      </div>
                    </button>
                  ))
                )}
              </div>
            </div>
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

Select.displayName = 'Select';

const MultiSelect: React.FC<MultiSelectProps> = ({
  value,
  onChange,
  ...props
}) => {
  const handleChange = (newValue: string | string[]) => {
    if (Array.isArray(newValue)) {
      onChange(newValue);
    }
  };

  return (
    <Select
      {...props}
      multiple={true}
      value={value}
      onChange={handleChange as any}
    />
  );
};

export default Select;
export { MultiSelect };