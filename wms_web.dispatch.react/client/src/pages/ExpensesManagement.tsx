import React, { useState, useEffect, useMemo } from 'react';
import {
  Receipt,
  Plus,
  Search,
  Filter,
  Download,
  Eye,
  CheckCircle,
  XCircle,
  Clock,
  AlertCircle,
  DollarSign,
  Calendar,
  User,
  Building2,
  Car,
  Fuel,
  Wrench,
  RefreshCw,
  FileText,
  ThumbsUp,
  ThumbsDown
} from 'lucide-react';
import { Card, Button, Badge, Input, Modal, Select } from '../components/ui';
import { VirtualScrollTable } from '../components/VirtualScrollTable';
import { wmsApi, DailyExpense, CreateDailyExpenseDto, ApproveExpenseDto } from '../services/wmsApi';
import { useAuth } from '../context/AuthContext';

interface ExpenseFilters {
  search: string;
  category: string;
  status: string;
  branchId: string;
  clerkId: string;
  dateRange: string;
}

interface ExpenseStats {
  totalExpenses: number;
  pendingExpenses: number;
  approvedExpenses: number;
  rejectedExpenses: number;
  monthlyTotal: number;
  dailyAverage: number;
}

const ExpensesManagement: React.FC = () => {
  const { user } = useAuth();
  const [expenses, setExpenses] = useState<DailyExpense[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [filters, setFilters] = useState<ExpenseFilters>({
    search: '',
    category: '',
    status: '',
    branchId: '',
    clerkId: '',
    dateRange: ''
  });
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showApprovalModal, setShowApprovalModal] = useState(false);
  const [selectedExpense, setSelectedExpense] = useState<DailyExpense | null>(null);

  const expenseCategories = [
    { value: 'fuel', label: 'Fuel', icon: Fuel },
    { value: 'casual_labor', label: 'Casual Labor', icon: User },
    { value: 'hired_cars', label: 'Hired Cars', icon: Car },
    { value: 'maintenance', label: 'Maintenance', icon: Wrench },
    { value: 'office_supplies', label: 'Office Supplies', icon: FileText },
    { value: 'utilities', label: 'Utilities', icon: Building2 },
    { value: 'transport', label: 'Transport', icon: Car },
    { value: 'other', label: 'Other', icon: Receipt }
  ];

  useEffect(() => {
    loadExpenseData();
  }, []);

  const loadExpenseData = async () => {
    try {
      setIsLoading(true);
      setError(null);
      const expenseData = await wmsApi.getDailyExpenses();
      setExpenses(expenseData);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load expense data');
    } finally {
      setIsLoading(false);
    }
  };

  const calculateStats = (): ExpenseStats => {
    const totalExpenses = expenses.reduce((sum, exp) => sum + exp.amount, 0);
    const pendingExpenses = expenses
      .filter(exp => exp.status === 'pending')
      .reduce((sum, exp) => sum + exp.amount, 0);
    const approvedExpenses = expenses
      .filter(exp => exp.status === 'approved')
      .reduce((sum, exp) => sum + exp.amount, 0);
    const rejectedExpenses = expenses
      .filter(exp => exp.status === 'rejected')
      .reduce((sum, exp) => sum + exp.amount, 0);

    // Calculate monthly total (current month)
    const currentMonth = new Date().getMonth();
    const currentYear = new Date().getFullYear();
    const monthlyTotal = expenses
      .filter(exp => {
        const expDate = new Date(exp.date);
        return expDate.getMonth() === currentMonth && expDate.getFullYear() === currentYear;
      })
      .reduce((sum, exp) => sum + exp.amount, 0);

    const dailyAverage = expenses.length > 0 ? totalExpenses / expenses.length : 0;

    return {
      totalExpenses,
      pendingExpenses,
      approvedExpenses,
      rejectedExpenses,
      monthlyTotal,
      dailyAverage
    };
  };

  const stats = calculateStats();

  const canApproveExpenses = (): boolean => {
    if (!user) return false;
    
    // Check by roleId (most reliable)
    if (user.roleId === 1) return true; // Admin
    
    // Check by role name
    const roleName = typeof user.role === 'string' ? user.role : (user.role as any)?.Name || '';
    const allowedRoles = ['admin', 'administrator', 'accountant'];
    return allowedRoles.includes(roleName.toLowerCase());
  };

  const formatCurrency = (amount: number) => {
    return new Intl.NumberFormat('en-KE', {
      style: 'currency',
      currency: 'KES'
    }).format(amount);
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-KE');
  };

  const getCategoryIcon = (category: string) => {
    const categoryConfig = expenseCategories.find(cat => cat.value === category);
    return categoryConfig ? categoryConfig.icon : Receipt;
  };

  const getStatusBadge = (status: DailyExpense['status']) => {
    const statusConfig = {
      pending: { color: 'yellow' as const, icon: Clock },
      approved: { color: 'green' as const, icon: CheckCircle },
      rejected: { color: 'red' as const, icon: XCircle }
    };
    
    const config = statusConfig[status];
    const Icon = config.icon;
    
    return (
      <Badge color={config.color} className="flex items-center gap-1">
        <Icon className="w-3 h-3" />
        {status.charAt(0).toUpperCase() + status.slice(1)}
      </Badge>
    );
  };

  const filteredExpenses = useMemo(() => {
    return expenses.filter(expense => {
      if (filters.search && !expense.description.toLowerCase().includes(filters.search.toLowerCase()) &&
          !expense.vendor?.toLowerCase().includes(filters.search.toLowerCase()) &&
          !expense.receiptNumber?.toLowerCase().includes(filters.search.toLowerCase())) {
        return false;
      }
      if (filters.category && expense.category !== filters.category) {
        return false;
      }
      if (filters.status && expense.status !== filters.status) {
        return false;
      }
      return true;
    });
  }, [expenses, filters]);

  const columns = [
    {
      key: 'date',
      header: 'Date',
      sortable: true,
      render: (expense: DailyExpense) => formatDate(expense.date)
    },
    {
      key: 'category',
      header: 'Category',
      sortable: true,
      render: (expense: DailyExpense) => {
        const Icon = getCategoryIcon(expense.category);
        return (
          <div className="flex items-center space-x-2">
            <Icon className="w-4 h-4 text-gray-500" />
            <span className="capitalize">{expense.category.replace('_', ' ')}</span>
          </div>
        );
      }
    },
    {
      key: 'description',
      header: 'Description',
      render: (expense: DailyExpense) => (
        <div>
          <div className="font-medium text-gray-900">{expense.description}</div>
          {expense.vendor && (
            <div className="text-sm text-gray-500">Vendor: {expense.vendor}</div>
          )}
          {expense.receiptNumber && (
            <div className="text-sm text-blue-600">Receipt: {expense.receiptNumber}</div>
          )}
        </div>
      )
    },
    {
      key: 'amount',
      header: 'Amount',
      sortable: true,
      render: (expense: DailyExpense) => (
        <div className="font-medium">{formatCurrency(expense.amount)}</div>
      )
    },
    {
      key: 'requestedBy',
      header: 'Requested By',
      render: (expense: DailyExpense) => (
        <div>
          <div className="font-medium text-gray-900">{expense.createdByName || 'Unknown'}</div>
          {expense.branchName && (
            <div className="text-sm text-gray-500">{expense.branchName}</div>
          )}
          {expense.clerkName && (
            <div className="text-sm text-gray-500">Clerk: {expense.clerkName}</div>
          )}
        </div>
      )
    },
    {
      key: 'status',
      header: 'Status',
      sortable: true,
      render: (expense: DailyExpense) => (
        <div>
          {getStatusBadge(expense.status)}
          {expense.approvedByName && (
            <div className="text-xs text-gray-500 mt-1">By: {expense.approvedByName}</div>
          )}
          {expense.rejectionReason && (
            <div className="text-xs text-red-600 mt-1">{expense.rejectionReason}</div>
          )}
        </div>
      )
    },
    {
      key: 'actions',
      header: 'Actions',
      render: (expense: DailyExpense) => (
        <div className="flex items-center space-x-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => setSelectedExpense(expense)}
            className="p-2"
          >
            <Eye className="w-4 h-4" />
          </Button>
          {expense.status === 'pending' && canApproveExpenses() && (
            <>
              <Button
                variant="outline"
                size="sm"
                onClick={() => handleApproveExpense(expense, true)}
                className="p-2 text-green-600 hover:text-green-700"
              >
                <ThumbsUp className="w-4 h-4" />
              </Button>
              <Button
                variant="outline"
                size="sm"
                onClick={() => handleApproveExpense(expense, false)}
                className="p-2 text-red-600 hover:text-red-700"
              >
                <ThumbsDown className="w-4 h-4" />
              </Button>
            </>
          )}
        </div>
      )
    }
  ];

  const handleApproveExpense = async (expense: DailyExpense, approved: boolean) => {
    if (!approved) {
      setSelectedExpense(expense);
      setShowApprovalModal(true);
      return;
    }

    try {
      await wmsApi.approveExpense({
        expenseId: expense.id,
        approved: true
      });
      await loadExpenseData();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to approve expense');
    }
  };

  if (isLoading) {
    return (
      <div className="flex items-center justify-center min-h-screen">
        <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-brand-500"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex flex-col items-center justify-center min-h-screen space-y-4">
        <AlertCircle className="w-16 h-16 text-red-500" />
        <h2 className="text-xl font-semibold text-gray-900">Error Loading Expenses</h2>
        <p className="text-gray-600">{error}</p>
        <Button onClick={loadExpenseData}>Try Again</Button>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center space-y-4 sm:space-y-0">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">Expense Management</h1>
          <p className="text-gray-600 mt-1">Track and approve daily operational expenses</p>
        </div>
        <div className="flex items-center space-x-2">
          <Button 
            onClick={loadExpenseData}
            variant="outline"
            className="flex items-center space-x-2"
          >
            <RefreshCw className="w-4 h-4" />
            <span>Refresh</span>
          </Button>
          <Button onClick={() => setShowCreateModal(true)} className="flex items-center space-x-2">
            <Plus className="w-4 h-4" />
            <span>Add Expense</span>
          </Button>
        </div>
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Total Expenses</p>
              <p className="text-2xl font-bold text-gray-900">{formatCurrency(stats.totalExpenses)}</p>
              <p className="text-sm text-gray-500 mt-1">All time</p>
            </div>
            <Receipt className="w-8 h-8 text-gray-500" />
          </div>
        </Card>

        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Pending Approval</p>
              <p className="text-2xl font-bold text-yellow-600">{formatCurrency(stats.pendingExpenses)}</p>
              <p className="text-sm text-gray-500 mt-1">Awaiting review</p>
            </div>
            <Clock className="w-8 h-8 text-yellow-500" />
          </div>
        </Card>

        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Approved</p>
              <p className="text-2xl font-bold text-green-600">{formatCurrency(stats.approvedExpenses)}</p>
              <p className="text-sm text-gray-500 mt-1">Ready for processing</p>
            </div>
            <CheckCircle className="w-8 h-8 text-green-500" />
          </div>
        </Card>

        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Monthly Total</p>
              <p className="text-2xl font-bold text-blue-600">{formatCurrency(stats.monthlyTotal)}</p>
              <p className="text-sm text-gray-500 mt-1">Current month</p>
            </div>
            <Calendar className="w-8 h-8 text-blue-500" />
          </div>
        </Card>
      </div>

      {/* Filters */}
      <Card className="p-4">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="relative">
            <Search className="absolute left-3 top-3 h-4 w-4 text-gray-400" />
            <Input
              placeholder="Search expenses..."
              value={filters.search}
              onChange={(e) => setFilters(prev => ({ ...prev, search: e.target.value }))}
              className="pl-10"
            />
          </div>
          <Select
            value={filters.category}
            onChange={(value) => setFilters(prev => ({ ...prev, category: (value as unknown) as string }))}
            options={[
              { value: "", label: "All Categories" },
              ...expenseCategories.map(cat => ({
                value: cat.value,
                label: cat.label
              }))
            ]}
          />
          <Select
            value={filters.status}
            onChange={(value) => setFilters(prev => ({ ...prev, status: (value as unknown) as string }))}
            options={[
              { value: "", label: "All Statuses" },
              { value: "pending", label: "Pending" },
              { value: "approved", label: "Approved" },
              { value: "rejected", label: "Rejected" }
            ]}
          />
          <Button
            variant="outline"
            onClick={() => setFilters({ search: '', category: '', status: '', branchId: '', clerkId: '', dateRange: '' })}
            className="flex items-center space-x-2"
          >
            <Filter className="w-4 h-4" />
            <span>Clear Filters</span>
          </Button>
        </div>
      </Card>

      {/* Expenses Table */}
      <Card>
        <div className="p-6 border-b border-gray-200">
          <div className="flex items-center justify-between">
            <h2 className="text-lg font-medium text-gray-900">
              Expense Records ({filteredExpenses.length})
            </h2>
            <Button variant="outline" className="flex items-center space-x-2">
              <Download className="w-4 h-4" />
              <span>Export</span>
            </Button>
          </div>
        </div>
        <VirtualScrollTable
          data={filteredExpenses}
          columns={columns}
          containerHeight={600}
          className="border-0"
        />
      </Card>
    </div>
  );
};

export default ExpensesManagement;