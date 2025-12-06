import React, { useState, useEffect } from 'react';
import { SidebarProvider, useSidebar } from '../context/SidebarContext';
import AppSidebar from './AppSidebar';
import AppHeader from './AppHeader';
import clsx from 'clsx';

interface AppLayoutProps {
  children: React.ReactNode;
}

const AppLayoutContent: React.FC<AppLayoutProps> = ({ children }) => {
  const { isExpanded, isMobileOpen, setIsMobileOpen } = useSidebar();
  const [darkMode, setDarkMode] = useState(false);

  // Initialize dark mode from localStorage
  useEffect(() => {
    const savedDarkMode = localStorage.getItem('darkMode') === 'true';
    setDarkMode(savedDarkMode);
    if (savedDarkMode) {
      document.documentElement.classList.add('dark');
    }
  }, []);

  const toggleDarkMode = () => {
    const newDarkMode = !darkMode;
    setDarkMode(newDarkMode);
    localStorage.setItem('darkMode', newDarkMode.toString());
    
    if (newDarkMode) {
      document.documentElement.classList.add('dark');
    } else {
      document.documentElement.classList.remove('dark');
    }
  };

  return (
    <div className="min-h-screen bg-gray-50 dark:bg-gray-900">
      {/* Sidebar */}
      <AppSidebar />

      {/* Mobile overlay */}
      {isMobileOpen && (
        <div
          className="fixed inset-0 z-30 bg-gray-900/50 lg:hidden"
          onClick={() => setIsMobileOpen(false)}
        />
      )}

      {/* Main content area */}
      <div
        className={clsx(
          "transition-all duration-300",
          {
            "lg:ml-64": isExpanded,
            "lg:ml-20": !isExpanded,
            "ml-0": isMobileOpen, // Mobile: no margin when sidebar is open (overlay mode)
          }
        )}
      >
        {/* Header */}
        <AppHeader darkMode={darkMode} toggleDarkMode={toggleDarkMode} />

        {/* Page content */}
        <main className="p-4 lg:p-6">
          {children}
        </main>
      </div>
    </div>
  );
};

const AppLayout: React.FC<AppLayoutProps> = ({ children }) => {
  return (
    <SidebarProvider>
      <AppLayoutContent>{children}</AppLayoutContent>
    </SidebarProvider>
  );
};

export default AppLayout;