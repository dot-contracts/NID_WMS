import React from 'react';
import { clsx } from 'clsx';

interface TableProps {
  children: React.ReactNode;
  className?: string;
}

interface TableHeaderProps {
  children: React.ReactNode;
  className?: string;
}

interface TableBodyProps {
  children: React.ReactNode;
  className?: string;
}

interface TableRowProps {
  children: React.ReactNode;
  className?: string;
  onClick?: () => void;
}

interface TableHeadProps {
  children: React.ReactNode;
  className?: string;
  sortable?: boolean;
  sortDirection?: 'asc' | 'desc';
  onSort?: () => void;
}

interface TableCellProps {
  children: React.ReactNode;
  className?: string;
}

interface TableComponent extends React.FC<TableProps> {
  Header: React.FC<TableHeaderProps>;
  Body: React.FC<TableBodyProps>;
  Row: React.FC<TableRowProps>;
  Head: React.FC<TableHeadProps>;
  Cell: React.FC<TableCellProps>;
}

const Table: TableComponent = ({ children, className }) => {
  return (
    <div className="overflow-x-auto">
      <table className={clsx('w-full text-sm text-left text-gray-500 dark:text-gray-400', className)}>
        {children}
      </table>
    </div>
  );
};

const TableHeader: React.FC<TableHeaderProps> = ({ children, className }) => {
  return (
    <thead className={clsx('text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400', className)}>
      {children}
    </thead>
  );
};

const TableBody: React.FC<TableBodyProps> = ({ children, className }) => {
  return (
    <tbody className={className}>
      {children}
    </tbody>
  );
};

const TableRow: React.FC<TableRowProps> = ({ children, className, onClick }) => {
  return (
    <tr 
      className={clsx(
        'bg-white border-b dark:bg-gray-800 dark:border-gray-700',
        onClick && 'hover:bg-gray-50 dark:hover:bg-gray-600 cursor-pointer',
        !onClick && 'hover:bg-gray-50 dark:hover:bg-gray-600',
        className
      )}
      onClick={onClick}
    >
      {children}
    </tr>
  );
};

const TableHead: React.FC<TableHeadProps> = ({ 
  children, 
  className, 
  sortable = false, 
  sortDirection,
  onSort 
}) => {
  return (
    <th
      className={clsx(
        'px-6 py-3',
        sortable && 'cursor-pointer select-none hover:text-gray-700 dark:hover:text-gray-200',
        className
      )}
      onClick={sortable ? onSort : undefined}
    >
      <div className="flex items-center space-x-1">
        <span>{children}</span>
        {sortable && (
          <div className="flex flex-col">
            <svg
              className={clsx(
                'w-3 h-3 transform transition-colors',
                sortDirection === 'asc' ? 'text-brand-500' : 'text-gray-400'
              )}
              fill="currentColor"
              viewBox="0 0 20 20"
            >
              <path
                fillRule="evenodd"
                d="M14.707 12.707a1 1 0 01-1.414 0L10 9.414l-3.293 3.293a1 1 0 01-1.414-1.414l4-4a1 1 0 011.414 0l4 4a1 1 0 010 1.414z"
                clipRule="evenodd"
              />
            </svg>
            <svg
              className={clsx(
                'w-3 h-3 transform rotate-180 transition-colors -mt-1',
                sortDirection === 'desc' ? 'text-brand-500' : 'text-gray-400'
              )}
              fill="currentColor"
              viewBox="0 0 20 20"
            >
              <path
                fillRule="evenodd"
                d="M14.707 12.707a1 1 0 01-1.414 0L10 9.414l-3.293 3.293a1 1 0 01-1.414-1.414l4-4a1 1 0 011.414 0l4 4a1 1 0 010 1.414z"
                clipRule="evenodd"
              />
            </svg>
          </div>
        )}
      </div>
    </th>
  );
};

const TableCell: React.FC<TableCellProps> = ({ children, className }) => {
  return (
    <td className={clsx('px-6 py-4', className)}>
      {children}
    </td>
  );
};

Table.Header = TableHeader;
Table.Body = TableBody;
Table.Row = TableRow;
Table.Head = TableHead;
Table.Cell = TableCell;

export default Table;