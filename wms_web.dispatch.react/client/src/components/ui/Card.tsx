import React from 'react';
import { clsx } from 'clsx';

interface CardProps {
  children: React.ReactNode;
  className?: string;
  padding?: boolean;
}

interface CardHeaderProps {
  children: React.ReactNode;
  className?: string;
}

interface CardContentProps {
  children: React.ReactNode;
  className?: string;
}

interface CardComponent extends React.FC<CardProps> {
  Header: React.FC<CardHeaderProps>;
  Content: React.FC<CardContentProps>;
}

const Card: CardComponent = ({ children, className, padding = true }) => {
  return (
    <div
      className={clsx(
        'bg-white rounded-xl border border-gray-200 shadow-theme-sm dark:bg-gray-900 dark:border-gray-800',
        padding && 'p-6',
        className
      )}
    >
      {children}
    </div>
  );
};

const CardHeader: React.FC<CardHeaderProps> = ({ children, className }) => {
  return (
    <div
      className={clsx(
        'px-6 py-4 border-b border-gray-200 dark:border-gray-800',
        className
      )}
    >
      {children}
    </div>
  );
};

const CardContent: React.FC<CardContentProps> = ({ children, className }) => {
  return <div className={clsx('p-6', className)}>{children}</div>;
};

Card.Header = CardHeader;
Card.Content = CardContent;

export default Card;