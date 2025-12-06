import React, { createContext, useContext, useState, useEffect, ReactNode } from 'react';

interface SidebarContextType {
  isExpanded: boolean;
  isMobileOpen: boolean;
  isHovered: boolean;
  setIsExpanded: (expanded: boolean) => void;
  setIsMobileOpen: (open: boolean) => void;
  setIsHovered: (hovered: boolean) => void;
  toggleSidebar: () => void;
  toggleMobileSidebar: () => void;
  toggleUnified: () => void;
}

const SidebarContext = createContext<SidebarContextType | undefined>(undefined);

export const useSidebar = (): SidebarContextType => {
  const context = useContext(SidebarContext);
  if (!context) {
    throw new Error('useSidebar must be used within a SidebarProvider');
  }
  return context;
};

interface SidebarProviderProps {
  children: ReactNode;
}

export const SidebarProvider: React.FC<SidebarProviderProps> = ({ children }) => {
  const [isExpanded, setIsExpanded] = useState(true);
  const [isMobileOpen, setIsMobileOpen] = useState(false);
  const [isHovered, setIsHovered] = useState(false);

  // Handle window resize to close mobile menu when switching to desktop
  useEffect(() => {
    const handleResize = () => {
      if (window.innerWidth >= 1024 && isMobileOpen) {
        setIsMobileOpen(false);
      }
    };

    window.addEventListener('resize', handleResize);
    return () => window.removeEventListener('resize', handleResize);
  }, [isMobileOpen]);

  const toggleSidebar = () => {
    setIsExpanded(!isExpanded);
  };

  const toggleMobileSidebar = () => {
    setIsMobileOpen(!isMobileOpen);
  };

  const toggleUnified = () => {
    // Check if we're on mobile or desktop
    if (window.innerWidth < 1024) {
      // Mobile: toggle mobile sidebar
      setIsMobileOpen(!isMobileOpen);
    } else {
      // Desktop: toggle expanded state
      setIsExpanded(!isExpanded);
    }
  };

  const value: SidebarContextType = {
    isExpanded,
    isMobileOpen,
    isHovered,
    setIsExpanded,
    setIsMobileOpen,
    setIsHovered,
    toggleSidebar,
    toggleMobileSidebar,
    toggleUnified,
  };

  return (
    <SidebarContext.Provider value={value}>
      {children}
    </SidebarContext.Provider>
  );
};