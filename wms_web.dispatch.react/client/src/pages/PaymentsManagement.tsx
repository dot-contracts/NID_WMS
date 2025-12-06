import React, { useState, useEffect, useMemo } from 'react';
import {
  CreditCard,
  Plus,
  Search,
  Filter,
  Download,
  Eye,
  CheckCircle,
  XCircle,
  AlertCircle,
  DollarSign,
  Calendar,
  Truck,
  FileCheck,
  Clock,
  RefreshCw
} from 'lucide-react';
import { Card, Button, Badge, Input, Modal, Select } from '../components/ui';
import { VirtualScrollTable } from '../components/VirtualScrollTable';
import { wmsApi, CODCollection, ChequeDeposit, CreateCODCollectionDto, CreateChequeDepositDto } from '../services/wmsApi';
import { useAuth } from '../context/AuthContext';

interface PaymentFilters {
  search: string;
  paymentType: 'all' | 'cod' | 'cheque';
  status: string;
  branchId: string;
  dateRange: string;
}

interface PaymentStats {
  totalCODCollected: number;
  totalCODDeposited: number;
  codShortfall: number;
  totalCheques: number;
  chequesCleared: number;
  chequesPending: number;
  chequesBounced: number;
}

const PaymentsManagement: React.FC = () => {
  const { user } = useAuth();
  const [codCollections, setCodCollections] = useState<CODCollection[]>([]);
  const [chequeDeposits, setChequeDeposits] = useState<ChequeDeposit[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [filters, setFilters] = useState<PaymentFilters>({
    search: '',
    paymentType: 'all',
    status: '',
    branchId: '',
    dateRange: ''
  });
  const [showCODModal, setShowCODModal] = useState(false);
  const [showChequeModal, setShowChequeModal] = useState(false);
  const [selectedPayment, setSelectedPayment] = useState<CODCollection | ChequeDeposit | null>(null);

  useEffect(() => {
    loadPaymentData();
  }, []);

  const loadPaymentData = async () => {
    try {
      setIsLoading(true);
      setError(null);
      const [codData, chequeData] = await Promise.all([
        wmsApi.getCODCollections(),
        wmsApi.getChequeDeposits()
      ]);
      setCodCollections(codData);
      setChequeDeposits(chequeData);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load payment data');
    } finally {
      setIsLoading(false);
    }
  };

  const calculateStats = (): PaymentStats => {
    const totalCODCollected = codCollections.reduce((sum, cod) => sum + cod.totalCODAmount, 0);
    const totalCODDeposited = codCollections.reduce((sum, cod) => sum + cod.depositedAmount, 0);
    const codShortfall = totalCODCollected - totalCODDeposited;

    const totalCheques = chequeDeposits.reduce((sum, cheque) => sum + cheque.amount, 0);
    const chequesCleared = chequeDeposits
      .filter(c => c.status === 'cleared')
      .reduce((sum, cheque) => sum + cheque.amount, 0);
    const chequesPending = chequeDeposits
      .filter(c => c.status === 'deposited')
      .reduce((sum, cheque) => sum + cheque.amount, 0);
    const chequesBounced = chequeDeposits
      .filter(c => c.status === 'bounced')
      .reduce((sum, cheque) => sum + cheque.amount, 0);

    return {
      totalCODCollected,
      totalCODDeposited,
      codShortfall,
      totalCheques,
      chequesCleared,
      chequesPending,
      chequesBounced
    };
  };

  const stats = calculateStats();

  const formatCurrency = (amount: number) => {
    return new Intl.NumberFormat('en-KE', {
      style: 'currency',
      currency: 'KES'
    }).format(amount);
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-KE');
  };

  const getStatusBadge = (status: string, type: 'cod' | 'cheque') => {
    const statusConfig = {
      cod: {
        collected: { color: 'blue' as const, icon: Truck },
        deposited: { color: 'green' as const, icon: CheckCircle },
        reconciled: { color: 'green' as const, icon: FileCheck },
        shortfall: { color: 'red' as const, icon: AlertCircle }
      },
      cheque: {
        deposited: { color: 'blue' as const, icon: CreditCard },
        cleared: { color: 'green' as const, icon: CheckCircle },
        bounced: { color: 'red' as const, icon: XCircle },
        cancelled: { color: 'red' as const, icon: XCircle }
      }
    };

    const config = statusConfig[type][status as keyof typeof statusConfig[typeof type]];
    if (!config) return null;

    const Icon = config.icon;
    return (
      <Badge color={config.color} className="flex items-center gap-1">
        <Icon className="w-3 h-3" />
        {status.charAt(0).toUpperCase() + status.slice(1)}
      </Badge>
    );
  };

  // Combined data for the table
  const combinedPayments = useMemo(() => {
    let combined: Array<(CODCollection | ChequeDeposit) & { paymentType: 'cod' | 'cheque' }> = [];

    if (filters.paymentType === 'all' || filters.paymentType === 'cod') {
      combined = [...combined, ...codCollections.map(cod => ({ ...cod, paymentType: 'cod' as const }))];
    }
    
    if (filters.paymentType === 'all' || filters.paymentType === 'cheque') {
      combined = [...combined, ...chequeDeposits.map(cheque => ({ ...cheque, paymentType: 'cheque' as const }))];
    }

    return combined.filter(payment => {
      if (filters.search) {
        const searchTerm = filters.search.toLowerCase();
        if (payment.paymentType === 'cod') {
          const codPayment = payment as CODCollection & { paymentType: 'cod' };
          return codPayment.dispatchCode.toLowerCase().includes(searchTerm) ||
                 codPayment.driverName.toLowerCase().includes(searchTerm) ||
                 codPayment.branchName.toLowerCase().includes(searchTerm);
        } else {
          const chequePayment = payment as ChequeDeposit & { paymentType: 'cheque' };
          return chequePayment.chequeNumber.toLowerCase().includes(searchTerm) ||
                 chequePayment.drawerName.toLowerCase().includes(searchTerm) ||
                 chequePayment.bankName.toLowerCase().includes(searchTerm);
        }
      }
      if (filters.status && payment.status !== filters.status) {
        return false;
      }
      return true;
    });
  }, [codCollections, chequeDeposits, filters]);

  const columns = [
    {
      key: 'type',
      header: 'Type',
      render: (payment: any) => (
        <Badge color={payment.paymentType === 'cod' ? 'blue' : 'green'}>
          {payment.paymentType === 'cod' ? 'COD' : 'Cheque'}
        </Badge>
      )
    },
    {
      key: 'reference',
      header: 'Reference',
      sortable: true,
      render: (payment: any) => (
        <div className="font-medium text-gray-900">
          {payment.paymentType === 'cod' ? payment.dispatchCode : payment.chequeNumber}
        </div>
      )
    },
    {
      key: 'details',
      header: 'Details',
      render: (payment: any) => (
        <div>
          {payment.paymentType === 'cod' ? (
            <>
              <div className="font-medium text-gray-900">{payment.driverName}</div>
              <div className="text-sm text-gray-500">{payment.vehicleNumber}</div>
              <div className="text-sm text-gray-500">{payment.branchName}</div>
            </>
          ) : (
            <>
              <div className="font-medium text-gray-900">{payment.drawerName}</div>
              <div className="text-sm text-gray-500">{payment.bankName}</div>
              {payment.relatedInvoiceNumber && (
                <div className="text-sm text-blue-600">Invoice: {payment.relatedInvoiceNumber}</div>
              )}
            </>
          )}
        </div>
      )
    },
    {
      key: 'date',
      header: 'Date',
      sortable: true,
      render: (payment: any) => formatDate(
        payment.paymentType === 'cod' ? payment.collectionDate : payment.depositDate
      )
    },
    {
      key: 'amount',
      header: 'Amount',
      sortable: true,
      render: (payment: any) => (
        <div>
          <div className="font-medium">
            {formatCurrency(payment.paymentType === 'cod' ? payment.totalCODAmount : payment.amount)}
          </div>
          {payment.paymentType === 'cod' && payment.depositedAmount > 0 && (
            <div className="text-sm text-green-600">
              Deposited: {formatCurrency(payment.depositedAmount)}
            </div>
          )}
          {payment.paymentType === 'cod' && payment.shortfall > 0 && (
            <div className="text-sm text-red-600">
              Shortfall: {formatCurrency(payment.shortfall)}
            </div>
          )}
        </div>
      )
    },
    {
      key: 'status',
      header: 'Status',
      sortable: true,
      render: (payment: any) => getStatusBadge(payment.status, payment.paymentType)
    },
    {
      key: 'actions',
      header: 'Actions',
      render: (payment: any) => (
        <div className="flex items-center space-x-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => setSelectedPayment(payment)}
            className="p-2"
          >
            <Eye className="w-4 h-4" />
          </Button>
          {payment.paymentType === 'cod' && payment.status === 'collected' && (
            <Button
              variant="outline"
              size="sm"
              onClick={() => handleUpdateCODDeposit(payment)}
              className="p-2"
            >
              <DollarSign className="w-4 h-4" />
            </Button>
          )}
          {payment.paymentType === 'cheque' && payment.status === 'deposited' && (
            <Button
              variant="outline"
              size="sm"
              onClick={() => handleUpdateChequeStatus(payment)}
              className="p-2"
            >
              <CheckCircle className="w-4 h-4" />
            </Button>
          )}
        </div>
      )
    }
  ];

  const handleUpdateCODDeposit = (payment: CODCollection) => {
    // Open modal to update COD deposit
    setSelectedPayment(payment);
    setShowCODModal(true);
  };

  const handleUpdateChequeStatus = (payment: ChequeDeposit) => {
    // Open modal to update cheque status
    setSelectedPayment(payment);
    setShowChequeModal(true);
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
        <h2 className="text-xl font-semibold text-gray-900">Error Loading Payments</h2>
        <p className="text-gray-600">{error}</p>
        <Button onClick={loadPaymentData}>Try Again</Button>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center space-y-4 sm:space-y-0">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">Payments Management</h1>
          <p className="text-gray-600 mt-1">Track COD collections and cheque deposits</p>
        </div>
        <div className="flex items-center space-x-2">
          <Button 
            onClick={loadPaymentData}
            variant="outline"
            className="flex items-center space-x-2"
          >
            <RefreshCw className="w-4 h-4" />
            <span>Refresh</span>
          </Button>
          <Button onClick={() => setShowCODModal(true)} className="flex items-center space-x-2">
            <Plus className="w-4 h-4" />
            <span>Add COD Collection</span>
          </Button>
          <Button onClick={() => setShowChequeModal(true)} className="flex items-center space-x-2">
            <Plus className="w-4 h-4" />
            <span>Add Cheque Deposit</span>
          </Button>
        </div>
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">COD Collected</p>
              <p className="text-2xl font-bold text-blue-600">{formatCurrency(stats.totalCODCollected)}</p>
              <p className="text-sm text-gray-500 mt-1">Total collections</p>
            </div>
            <Truck className="w-8 h-8 text-blue-500" />
          </div>
        </Card>

        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">COD Deposited</p>
              <p className="text-2xl font-bold text-green-600">{formatCurrency(stats.totalCODDeposited)}</p>
              {stats.codShortfall > 0 && (
                <p className="text-sm text-red-600 mt-1">Shortfall: {formatCurrency(stats.codShortfall)}</p>
              )}
            </div>
            <CheckCircle className="w-8 h-8 text-green-500" />
          </div>
        </Card>

        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Cheques Deposited</p>
              <p className="text-2xl font-bold text-purple-600">{formatCurrency(stats.totalCheques)}</p>
              <p className="text-sm text-gray-500 mt-1">{chequeDeposits.length} cheques</p>
            </div>
            <CreditCard className="w-8 h-8 text-purple-500" />
          </div>
        </Card>

        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Cheques Cleared</p>
              <p className="text-2xl font-bold text-green-600">{formatCurrency(stats.chequesCleared)}</p>
              {stats.chequesPending > 0 && (
                <p className="text-sm text-yellow-600 mt-1">Pending: {formatCurrency(stats.chequesPending)}</p>
              )}
            </div>
            <FileCheck className="w-8 h-8 text-green-500" />
          </div>
        </Card>
      </div>

      {/* Filters */}
      <Card className="p-4">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="relative">
            <Search className="absolute left-3 top-3 h-4 w-4 text-gray-400" />
            <Input
              placeholder="Search payments..."
              value={filters.search}
              onChange={(e) => setFilters(prev => ({ ...prev, search: e.target.value }))}
              className="pl-10"
            />
          </div>
          <Select
            value={filters.paymentType}
            onChange={(value) => setFilters(prev => ({ ...prev, paymentType: (value as unknown) as string as 'all' | 'cod' | 'cheque' }))}
            options={[
              { value: "all", label: "All Types" },
              { value: "cod", label: "COD Collections" },
              { value: "cheque", label: "Cheque Deposits" }
            ]}
          />
          <Select
            value={filters.status}
            onChange={(value) => setFilters(prev => ({ ...prev, status: (value as unknown) as string }))}
            options={[
              { value: "", label: "All Statuses" },
              { value: "collected", label: "Collected" },
              { value: "deposited", label: "Deposited" },
              { value: "cleared", label: "Cleared" },
              { value: "bounced", label: "Bounced" },
              { value: "reconciled", label: "Reconciled" }
            ]}
          />
          <Button
            variant="outline"
            onClick={() => setFilters({ search: '', paymentType: 'all', status: '', branchId: '', dateRange: '' })}
            className="flex items-center space-x-2"
          >
            <Filter className="w-4 h-4" />
            <span>Clear Filters</span>
          </Button>
        </div>
      </Card>

      {/* Payments Table */}
      <Card>
        <div className="p-6 border-b border-gray-200">
          <div className="flex items-center justify-between">
            <h2 className="text-lg font-medium text-gray-900">
              Payment Records ({combinedPayments.length})
            </h2>
            <Button variant="outline" className="flex items-center space-x-2">
              <Download className="w-4 h-4" />
              <span>Export</span>
            </Button>
          </div>
        </div>
        <VirtualScrollTable
          data={combinedPayments}
          columns={columns}
          containerHeight={600}
          className="border-0"
        />
      </Card>
    </div>
  );
};

export default PaymentsManagement;