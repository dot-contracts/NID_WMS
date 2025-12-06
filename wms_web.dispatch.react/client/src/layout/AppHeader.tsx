import React from 'react';
import { Menu, Bell, Search, Sun, Moon } from 'lucide-react';
import { useSidebar } from '../context/SidebarContext';
import { useAuth } from '../context/AuthContext';
import clsx from 'clsx';


interface AppHeaderProps {
  darkMode?: boolean;
  toggleDarkMode?: () => void;
}

const AppHeader: React.FC<AppHeaderProps> = ({ darkMode, toggleDarkMode }) => {
  const { toggleUnified } = useSidebar();
  const { user } = useAuth();


  return (
    <header className="bg-white dark:bg-gray-900 border-b border-gray-200 dark:border-gray-800 sticky top-0 z-30">
      <div className="flex items-center justify-between px-4 py-3">
        {/* Left side - Menu and Search */}
        <div className="flex items-center space-x-4">
          {/* Unified menu toggle - works on all devices */}
          <button
            onClick={toggleUnified}
            className="p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
            aria-label="Toggle menu"
          >
            <Menu className="w-5 h-5 text-gray-600 dark:text-gray-400" />
          </button>
          

          {/* Search bar */}
          <div className="hidden md:flex items-center relative">
            <Search className="w-4 h-4 text-gray-400 absolute left-3" />
            <input
              type="text"
              placeholder="Search..."
              className="pl-10 pr-4 py-2 w-80 bg-gray-50 dark:bg-gray-800 border border-gray-200 dark:border-gray-700 rounded-lg focus:outline-none focus:ring-2 focus:ring-brand-500 focus:border-transparent text-sm"
            />
          </div>
        </div>

        {/* Right side - Notifications, Theme toggle, User menu */}
        <div className="flex items-center space-x-3">
          {/* Mobile search button */}
          <button className="md:hidden p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors">
            <Search className="w-5 h-5 text-gray-600 dark:text-gray-400" />
          </button>

          {/* Notifications */}
          <button className="relative p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors">
            <Bell className="w-5 h-5 text-gray-600 dark:text-gray-400" />
            <span className="absolute -top-1 -right-1 w-3 h-3 bg-error-500 rounded-full"></span>
          </button>

          {/* Dark mode toggle */}
          {toggleDarkMode && (
            <button
              onClick={toggleDarkMode}
              className="p-2 rounded-lg hover:bg-gray-100 dark:hover:bg-gray-800 transition-colors"
            >
              {darkMode ? (
                <Sun className="w-5 h-5 text-gray-600 dark:text-gray-400" />
              ) : (
                <Moon className="w-5 h-5 text-gray-600 dark:text-gray-400" />
              )}
            </button>
          )}

          {/* User avatar and name */}
          {user && (
            <div className="flex items-center space-x-3 pl-3 border-l border-gray-200 dark:border-gray-700">
              <div className="hidden sm:flex flex-col items-end">
                <span className="text-sm font-medium text-gray-900 dark:text-white">
                  {user.username}
                </span>
                <span className="text-xs text-gray-500 dark:text-gray-400">
                  {typeof user.role === 'string' ? user.role : (user.role as any)?.name || 'User'}
                </span>
              </div>
              <div className="w-8 h-8 bg-brand-100 rounded-full flex items-center justify-center">
                <span className="text-brand-600 font-semibold text-sm">
                  {user.username.charAt(0).toUpperCase()}
                </span>
              </div>
            </div>
          )}
        </div>
      </div>
    </header>
  );
};

export default AppHeader;