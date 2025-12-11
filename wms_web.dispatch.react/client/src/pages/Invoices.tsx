import React, { useState, useEffect, useMemo } from 'react';
import { 
  FileText, 
  Plus, 
  Search, 
  Filter, 
  Download, 
  Eye, 
  Edit, 
  DollarSign,
  Calendar,
  Building2,
  AlertCircle,
  CheckCircle,
  Clock,
  Printer
} from 'lucide-react';
import { Card, Button, Badge, Table, Input, Modal, Select } from '../components/ui';
import { VirtualScrollTable } from '../components/VirtualScrollTable';
import { InvoiceViewModal } from '../components/InvoiceViewModal';
import { PaymentModal } from '../components/PaymentModal';
import { printInvoice } from '../components/InvoicePrintView';
import { wmsApi, Invoice, ContractCustomer } from '../services/wmsApi';
import { useAuth } from '../context/AuthContext';
import { useNavigate } from 'react-router-dom';

interface InvoiceFilters {
  search: string;
  status: string;
  contractCustomerId: string;
  dateRange: string;
}


const Invoices: React.FC = () => {
  const { user } = useAuth();
  const navigate = useNavigate();
  const [invoices, setInvoices] = useState<Invoice[]>([]);
  const [contractCustomers, setContractCustomers] = useState<ContractCustomer[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [filters, setFilters] = useState<InvoiceFilters>({
    search: '',
    status: '',
    contractCustomerId: '',
    dateRange: ''
  });
  const [showPaymentModal, setShowPaymentModal] = useState(false);
  const [showViewModal, setShowViewModal] = useState(false);
  const [selectedInvoice, setSelectedInvoice] = useState<Invoice | null>(null);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setIsLoading(true);
      setError(null);
      const [invoicesData, customersData] = await Promise.all([
        wmsApi.getInvoices(),
        wmsApi.getContractCustomers()
      ]);
      setInvoices(invoicesData);
      setContractCustomers(customersData);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to load invoices');
    } finally {
      setIsLoading(false);
    }
  };

  const filteredInvoices = useMemo(() => {
    return invoices.filter(invoice => {
      if (filters.search && !invoice.invoiceNumber.toLowerCase().includes(filters.search.toLowerCase()) &&
          !invoice.customer?.name.toLowerCase().includes(filters.search.toLowerCase())) {
        return false;
      }
      if (filters.status && invoice.status !== filters.status) {
        return false;
      }
      if (filters.contractCustomerId && invoice.contractCustomerId.toString() !== filters.contractCustomerId) {
        return false;
      }
      return true;
    });
  }, [invoices, filters]);

  const getStatusBadge = (status: Invoice['status']) => {
    const statusConfig = {
      draft: { color: 'gray' as const, icon: Edit },
      sent: { color: 'blue' as const, icon: FileText },
      paid: { color: 'green' as const, icon: CheckCircle },
      overdue: { color: 'red' as const, icon: AlertCircle },
      cancelled: { color: 'red' as const, icon: AlertCircle },
      partial: { color: 'yellow' as const, icon: Clock }
    };
    
    // Fallback for unknown statuses
    const config = statusConfig[status] || { color: 'gray' as const, icon: AlertCircle };
    const Icon = config.icon;
    
    return (
      <Badge color={config.color} className="flex items-center gap-1">
        <Icon className="w-3 h-3" />
        {status ? status.charAt(0).toUpperCase() + status.slice(1) : 'Unknown'}
      </Badge>
    );
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

  const calculateTotalStats = () => {
    const total = filteredInvoices.reduce((acc, inv) => acc + inv.totalAmount, 0);
    const paid = filteredInvoices.reduce((acc, inv) => acc + inv.paidAmount, 0);
    const outstanding = total - paid;
    const overdue = filteredInvoices.filter(inv => 
      inv.status === 'overdue' || 
      (inv.status === 'sent' && new Date(inv.dueDate) < new Date())
    ).reduce((acc, inv) => acc + (inv.totalAmount - inv.paidAmount), 0);

    return { total, paid, outstanding, overdue };
  };

  const stats = calculateTotalStats();

  const columns = [
    {
      key: 'invoiceNumber',
      header: 'Invoice #',
      sortable: true,
      render: (invoice: Invoice) => (
        <div className="font-medium text-gray-900">
          {invoice.invoiceNumber}
        </div>
      )
    },
    {
      key: 'customer',
      header: 'Customer',
      sortable: true,
      render: (invoice: Invoice) => (
        <div>
          <div className="font-medium text-gray-900">
            {invoice.customer?.name || 'Unknown Customer'}
          </div>
          <div className="text-sm text-gray-500">
            {invoice.customer?.companyName}
          </div>
        </div>
      )
    },
    {
      key: 'billingPeriod',
      header: 'Billing Period',
      render: (invoice: Invoice) => (
        <div className="text-sm">
          {formatDate(invoice.billingPeriodStart)} - {formatDate(invoice.billingPeriodEnd)}
        </div>
      )
    },
    {
      key: 'issueDate',
      header: 'Issue Date',
      sortable: true,
      render: (invoice: Invoice) => formatDate(invoice.issueDate)
    },
    {
      key: 'dueDate',
      header: 'Due Date',
      sortable: true,
      render: (invoice: Invoice) => (
        <div className={new Date(invoice.dueDate) < new Date() && invoice.status !== 'paid' ? 'text-red-600 font-medium' : ''}>
          {formatDate(invoice.dueDate)}
        </div>
      )
    },
    {
      key: 'totalAmount',
      header: 'Amount',
      sortable: true,
      render: (invoice: Invoice) => (
        <div>
          <div className="font-medium">{formatCurrency(invoice.totalAmount)}</div>
          {invoice.paidAmount > 0 && (
            <div className="text-sm text-green-600">
              Paid: {formatCurrency(invoice.paidAmount)}
            </div>
          )}
        </div>
      )
    },
    {
      key: 'status',
      header: 'Status',
      sortable: true,
      render: (invoice: Invoice) => getStatusBadge(invoice.status)
    },
    {
      key: 'actions',
      header: 'Actions',
      render: (invoice: Invoice) => (
        <div className="flex items-center space-x-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => handleViewInvoice(invoice)}
            className="p-2"
            title="View Invoice Details"
          >
            <Eye className="w-4 h-4" />
          </Button>
          {invoice.status !== 'paid' && (
            <Button
              variant="outline"
              size="sm"
              onClick={() => handlePaymentClick(invoice)}
              className="p-2"
              title="Record Payment"
            >
              <DollarSign className="w-4 h-4" />
            </Button>
          )}
          <Button
            variant="outline"
            size="sm"
            onClick={() => handlePrintInvoice(invoice)}
            className="p-2"
            title="Print Invoice"
          >
            <Printer className="w-4 h-4" />
          </Button>
        </div>
      )
    }
  ];

  const handleCreateInvoice = () => {
    navigate('/invoices/create');
  };

  const handleViewInvoice = (invoice: Invoice) => {
    setSelectedInvoice(invoice);
    setShowViewModal(true);
  };

  const handlePaymentClick = (invoice: Invoice) => {
    setSelectedInvoice(invoice);
    setShowPaymentModal(true);
  };

  const handlePrintInvoice = (invoice: Invoice) => {
    printInvoice(invoice);
  };

  const handleRecordPayment = async (paymentData: any) => {
    try {
      await wmsApi.recordInvoicePayment(paymentData.invoiceId, {
        amount: paymentData.amount,
        paymentMethod: paymentData.paymentMethod,
        paymentDate: paymentData.paymentDate,
        paymentReference: paymentData.paymentReference,
        notes: paymentData.notes
      });
      
      // Reload invoices to get updated data
      await loadData();
    } catch (error) {
      throw new Error(error instanceof Error ? error.message : 'Failed to record payment');
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
        <h2 className="text-xl font-semibold text-gray-900">Error Loading Invoices</h2>
        <p className="text-gray-600">{error}</p>
        <Button onClick={loadData}>Try Again</Button>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center space-y-4 sm:space-y-0">
        <div>
          <h1 className="text-3xl font-bold text-gray-900">Invoice Management</h1>
          <p className="text-gray-600 mt-1">Manage contract customer invoices and payments</p>
        </div>
        <Button onClick={handleCreateInvoice} className="flex items-center space-x-2">
          <Plus className="w-4 h-4" />
          <span>Create Invoice</span>
        </Button>
      </div>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Total Invoiced</p>
              <p className="text-2xl font-bold text-gray-900">{formatCurrency(stats.total)}</p>
            </div>
            <FileText className="w-8 h-8 text-blue-500" />
          </div>
        </Card>
        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Amount Paid</p>
              <p className="text-2xl font-bold text-green-600">{formatCurrency(stats.paid)}</p>
            </div>
            <CheckCircle className="w-8 h-8 text-green-500" />
          </div>
        </Card>
        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Outstanding</p>
              <p className="text-2xl font-bold text-yellow-600">{formatCurrency(stats.outstanding)}</p>
            </div>
            <Clock className="w-8 h-8 text-yellow-500" />
          </div>
        </Card>
        <Card className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm font-medium text-gray-600">Overdue</p>
              <p className="text-2xl font-bold text-red-600">{formatCurrency(stats.overdue)}</p>
            </div>
            <AlertCircle className="w-8 h-8 text-red-500" />
          </div>
        </Card>
      </div>

      {/* Filters */}
      <Card className="p-4">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-4">
          <div className="relative">
            <Search className="absolute left-3 top-3 h-4 w-4 text-gray-400" />
            <Input
              placeholder="Search invoices..."
              value={filters.search}
              onChange={(e) => setFilters(prev => ({ ...prev, search: e.target.value }))}
              className="pl-10"
            />
          </div>
          <Select
            value={filters.status}
            onChange={(value) => setFilters(prev => ({ ...prev, status: (value as unknown) as string }))}
            options={[
              { value: "", label: "All Statuses" },
              { value: "draft", label: "Draft" },
              { value: "sent", label: "Sent" },
              { value: "paid", label: "Paid" },
              { value: "overdue", label: "Overdue" },
              { value: "cancelled", label: "Cancelled" }
            ]}
          />
          <Select
            value={filters.contractCustomerId}
            onChange={(value) => setFilters(prev => ({ ...prev, contractCustomerId: (value as unknown) as string }))}
            options={[
              { value: "", label: "All Customers" },
              ...contractCustomers.map(customer => ({
                value: customer.id.toString(),
                label: customer.name
              }))
            ]}
          />
          <Button
            variant="outline"
            onClick={() => setFilters({ search: '', status: '', contractCustomerId: '', dateRange: '' })}
            className="flex items-center space-x-2"
          >
            <Filter className="w-4 h-4" />
            <span>Clear Filters</span>
          </Button>
        </div>
      </Card>

      {/* Invoices Table */}
      <Card>
        <div className="p-6 border-b border-gray-200">
          <div className="flex items-center justify-between">
            <h2 className="text-lg font-medium text-gray-900">
              Invoices ({filteredInvoices.length})
            </h2>
            <Button variant="outline" className="flex items-center space-x-2">
              <Download className="w-4 h-4" />
              <span>Export</span>
            </Button>
          </div>
        </div>
        <VirtualScrollTable
          data={filteredInvoices}
          columns={columns}
          containerHeight={600}
          className="border-0"
        />
      </Card>

      {/* Invoice View Modal */}
      <InvoiceViewModal
        invoice={selectedInvoice}
        isOpen={showViewModal}
        onClose={() => {
          setShowViewModal(false);
          setSelectedInvoice(null);
        }}
        onPrint={() => {
          if (selectedInvoice) {
            handlePrintInvoice(selectedInvoice);
          }
        }}
      />

      {/* Payment Modal */}
      <PaymentModal
        invoice={selectedInvoice}
        isOpen={showPaymentModal}
        onClose={() => {
          setShowPaymentModal(false);
          setSelectedInvoice(null);
        }}
        onPaymentRecord={handleRecordPayment}
      />
    </div>
  );
};

export default Invoices;