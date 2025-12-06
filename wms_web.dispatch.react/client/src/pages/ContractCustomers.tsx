import React, { useState, useEffect, useMemo } from 'react';
import { 
  Users, 
  Plus, 
  Search, 
  Filter, 
  Edit, 
  Eye, 
  Trash2,
  Building2,
  Mail,
  Phone,
  Calendar,
  AlertCircle,
  CheckCircle,
  XCircle
} from 'lucide-react';
import { Card, Button, Badge, Input, Modal, Select } from '../components/ui';
import { VirtualScrollTable } from '../components/VirtualScrollTable';
import { wmsApi, ContractCustomer } from '../services/wmsApi';
import ContractCustomerForm from '../components/forms/ContractCustomerForm';

interface CustomerFilters {
  search: string;
  isActive: string;
  sortBy: string;
}

const ContractCustomers: React.FC = () => {
  const [customers, setCustomers] = useState<ContractCustomer[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [filters, setFilters] = useState<CustomerFilters>({
    search: '',
    isActive: '',
    sortBy: 'name'
  });
  const [showCreateModal, setShowCreateModal] = useState(false);
  const [showEditModal, setShowEditModal] = useState(false);
  const [showViewModal, setShowViewModal] = useState(false);
  const [selectedCustomer, setSelectedCustomer] = useState<ContractCustomer | null>(null);

  useEffect(() => {
    loadCustomers();
  }, []);

  const loadCustomers = async () => {
    try {
      setIsLoading(true);
      setError(null);
      const data = await wmsApi.getContractCustomers();
      setCustomers(data);
    } catch (err) {
      console.error('Failed to load contract customers:', err);
      setError('Failed to load contract customers. Please try again.');
    } finally {
      setIsLoading(false);
    }
  };

  const handleCreateCustomer = async (customerData: Omit<ContractCustomer, 'id' | 'createdAt' | 'updatedAt'>) => {
    try {
      await wmsApi.createContractCustomer(customerData);
      setShowCreateModal(false);
      await loadCustomers();
    } catch (err) {
      console.error('Failed to create customer:', err);
      throw new Error('Failed to create customer');
    }
  };

  const handleEditCustomer = async (customerData: Partial<ContractCustomer>) => {
    try {
      if (!selectedCustomer?.id) return;
      await wmsApi.updateContractCustomer(selectedCustomer.id, customerData);
      setShowEditModal(false);
      setSelectedCustomer(null);
      await loadCustomers();
    } catch (err) {
      console.error('Failed to update customer:', err);
      throw new Error('Failed to update customer');
    }
  };

  const handleDeleteCustomer = async (customer: ContractCustomer) => {
    if (!window.confirm(`Are you sure you want to delete "${customer.name}"? This action cannot be undone.`)) {
      return;
    }

    try {
      await wmsApi.deleteContractCustomer(customer.id);
      await loadCustomers();
    } catch (err) {
      console.error('Failed to delete customer:', err);
      alert('Failed to delete customer. They may have associated invoices.');
    }
  };

  const filteredAndSortedCustomers = useMemo(() => {
    let result = [...customers];

    // Apply search filter
    if (filters.search) {
      const searchLower = filters.search.toLowerCase();
      result = result.filter(customer =>
        customer.name.toLowerCase().includes(searchLower) ||
        customer.companyName?.toLowerCase().includes(searchLower) ||
        customer.email?.toLowerCase().includes(searchLower) ||
        customer.contractNumber?.toLowerCase().includes(searchLower) ||
        customer.contactPerson?.toLowerCase().includes(searchLower)
      );
    }

    // Apply status filter
    if (filters.isActive !== '') {
      const isActive = filters.isActive === 'true';
      result = result.filter(customer => customer.isActive === isActive);
    }

    // Apply sorting
    result.sort((a, b) => {
      switch (filters.sortBy) {
        case 'name':
          return a.name.localeCompare(b.name);
        case 'company':
          return (a.companyName || '').localeCompare(b.companyName || '');
        case 'created':
          return new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime();
        case 'contract':
          return (a.contractNumber || '').localeCompare(b.contractNumber || '');
        default:
          return 0;
      }
    });

    return result;
  }, [customers, filters]);

  const getStatusBadge = (isActive: boolean) => {
    if (isActive) {
      return <Badge variant="success" className="flex items-center gap-1"><CheckCircle className="w-3 h-3" /> Active</Badge>;
    }
    return <Badge variant="gray" className="flex items-center gap-1"><XCircle className="w-3 h-3" /> Inactive</Badge>;
  };

  const columns = [
    {
      key: 'name',
      header: 'Customer Name',
      render: (customer: ContractCustomer) => (
        <div className="flex flex-col">
          <span className="font-medium text-gray-900">{customer.name}</span>
          {customer.companyName && (
            <span className="text-sm text-gray-500 flex items-center gap-1">
              <Building2 className="w-3 h-3" />
              {customer.companyName}
            </span>
          )}
        </div>
      )
    },
    {
      key: 'contact',
      header: 'Contact Info',
      render: (customer: ContractCustomer) => (
        <div className="flex flex-col space-y-1">
          {customer.email && (
            <span className="text-sm flex items-center gap-1">
              <Mail className="w-3 h-3 text-gray-400" />
              {customer.email}
            </span>
          )}
          {customer.phone && (
            <span className="text-sm flex items-center gap-1">
              <Phone className="w-3 h-3 text-gray-400" />
              {customer.phone}
            </span>
          )}
        </div>
      )
    },
    {
      key: 'contract',
      header: 'Contract',
      render: (customer: ContractCustomer) => (
        <div className="flex flex-col">
          {customer.contractNumber && (
            <span className="font-mono text-sm">{customer.contractNumber}</span>
          )}
          <span className="text-xs text-gray-500">
            {customer.paymentTerms || 'Net 30'}
          </span>
        </div>
      )
    },
    {
      key: 'taxRate',
      header: 'Tax Rate',
      render: (customer: ContractCustomer) => (
        <span className="font-mono">{customer.taxRate}%</span>
      )
    },
    {
      key: 'status',
      header: 'Status',
      render: (customer: ContractCustomer) => getStatusBadge(customer.isActive)
    },
    {
      key: 'created',
      header: 'Created',
      render: (customer: ContractCustomer) => (
        <span className="text-sm text-gray-500 flex items-center gap-1">
          <Calendar className="w-3 h-3" />
          {new Date(customer.createdAt).toLocaleDateString()}
        </span>
      )
    },
    {
      key: 'actions',
      header: 'Actions',
      render: (customer: ContractCustomer) => (
        <div className="flex items-center space-x-2">
          <Button
            variant="outline"
            size="sm"
            onClick={() => {
              setSelectedCustomer(customer);
              setShowViewModal(true);
            }}
            className="text-blue-600 hover:text-blue-700"
          >
            <Eye className="w-4 h-4" />
          </Button>
          <Button
            variant="outline"
            size="sm"
            onClick={() => {
              setSelectedCustomer(customer);
              setShowEditModal(true);
            }}
            className="text-green-600 hover:text-green-700"
          >
            <Edit className="w-4 h-4" />
          </Button>
          <Button
            variant="outline"
            size="sm"
            onClick={() => handleDeleteCustomer(customer)}
            className="text-red-600 hover:text-red-700"
          >
            <Trash2 className="w-4 h-4" />
          </Button>
        </div>
      )
    }
  ];

  if (isLoading) {
    return (
      <div className="flex items-center justify-center h-64">
        <div className="text-center">
          <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600 mx-auto mb-2"></div>
          <p className="text-gray-600">Loading contract customers...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div className="flex items-center space-x-3">
          <Users className="w-8 h-8 text-blue-600" />
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Contract Customers</h1>
            <p className="text-gray-600">Manage your contract customers for invoicing</p>
          </div>
        </div>
        <Button
          onClick={() => setShowCreateModal(true)}
          className="bg-blue-600 hover:bg-blue-700 text-white"
        >
          <Plus className="w-4 h-4 mr-2" />
          Add Customer
        </Button>
      </div>

      {/* Summary Cards */}
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <Card className="p-4">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm text-gray-600">Total Customers</p>
              <p className="text-2xl font-bold text-gray-900">{customers.length}</p>
            </div>
            <Users className="w-8 h-8 text-blue-500" />
          </div>
        </Card>
        <Card className="p-4">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm text-gray-600">Active Customers</p>
              <p className="text-2xl font-bold text-green-600">
                {customers.filter(c => c.isActive).length}
              </p>
            </div>
            <CheckCircle className="w-8 h-8 text-green-500" />
          </div>
        </Card>
        <Card className="p-4">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-sm text-gray-600">Inactive Customers</p>
              <p className="text-2xl font-bold text-red-600">
                {customers.filter(c => !c.isActive).length}
              </p>
            </div>
            <XCircle className="w-8 h-8 text-red-500" />
          </div>
        </Card>
      </div>

      {/* Error Message */}
      {error && (
        <Card className="p-4 border-red-200 bg-red-50">
          <div className="flex items-center space-x-2 text-red-700">
            <AlertCircle className="w-4 h-4" />
            <span>{error}</span>
          </div>
        </Card>
      )}

      {/* Filters */}
      <Card className="p-4">
        <div className="flex flex-col sm:flex-row gap-4">
          <div className="flex-1">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 w-4 h-4" />
              <Input
                type="text"
                placeholder="Search customers, companies, emails, contracts..."
                value={filters.search}
                onChange={(e) => setFilters(prev => ({ ...prev, search: e.target.value }))}
                className="pl-10"
              />
            </div>
          </div>
          <div className="flex gap-2">
            <Select
              value={filters.isActive}
              onChange={(e) => setFilters(prev => ({ ...prev, isActive: e.target.value }))}
              className="w-32"
              options={[
                { value: '', label: 'All Status' },
                { value: 'true', label: 'Active' },
                { value: 'false', label: 'Inactive' }
              ]}
            />
            <Select
              value={filters.sortBy}
              onChange={(e) => setFilters(prev => ({ ...prev, sortBy: e.target.value }))}
              className="w-36"
              options={[
                { value: 'name', label: 'Sort by Name' },
                { value: 'company', label: 'Sort by Company' },
                { value: 'created', label: 'Sort by Created' },
                { value: 'contract', label: 'Sort by Contract' }
              ]}
            />
          </div>
        </div>
      </Card>

      {/* Customers Table */}
      <Card>
        <div className="p-4 border-b">
          <div className="flex items-center justify-between">
            <h2 className="text-lg font-semibold">
              Contract Customers ({filteredAndSortedCustomers.length})
            </h2>
            <div className="flex items-center space-x-2 text-sm text-gray-500">
              <Filter className="w-4 h-4" />
              <span>
                {filters.search || filters.isActive || filters.sortBy !== 'name' ? 'Filtered' : 'All'}
              </span>
            </div>
          </div>
        </div>
        
        <VirtualScrollTable
          data={filteredAndSortedCustomers}
          columns={columns}
          containerHeight={600}
          rowHeight={80}
        />
      </Card>

      {/* Create Customer Modal */}
      <Modal
        isOpen={showCreateModal}
        onClose={() => setShowCreateModal(false)}
        title="Add New Contract Customer"
        size="lg"
      >
        <ContractCustomerForm
          onSubmit={handleCreateCustomer}
        />
      </Modal>

      {/* Edit Customer Modal */}
      <Modal
        isOpen={showEditModal}
        onClose={() => {
          setShowEditModal(false);
          setSelectedCustomer(null);
        }}
        title="Edit Contract Customer"
        size="lg"
      >
        {selectedCustomer && (
          <ContractCustomerForm
            customer={selectedCustomer}
            onSubmit={handleEditCustomer}
          />
        )}
      </Modal>

      {/* View Customer Modal */}
      <Modal
        isOpen={showViewModal}
        onClose={() => {
          setShowViewModal(false);
          setSelectedCustomer(null);
        }}
        title="Customer Details"
        size="lg"
      >
        {selectedCustomer && (
          <div className="space-y-6">
            <div className="grid grid-cols-2 gap-4">
              <div>
                <label className="text-sm font-medium text-gray-500">Customer Name</label>
                <p className="text-lg font-semibold">{selectedCustomer.name}</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Company Name</label>
                <p className="text-lg">{selectedCustomer.companyName || 'N/A'}</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Email</label>
                <p>{selectedCustomer.email || 'N/A'}</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Phone</label>
                <p>{selectedCustomer.phone || 'N/A'}</p>
              </div>
              <div className="col-span-2">
                <label className="text-sm font-medium text-gray-500">Address</label>
                <p>{selectedCustomer.address || 'N/A'}</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Contact Person</label>
                <p>{selectedCustomer.contactPerson || 'N/A'}</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Contract Number</label>
                <p className="font-mono">{selectedCustomer.contractNumber || 'N/A'}</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Payment Terms</label>
                <p>{selectedCustomer.paymentTerms}</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Tax Rate</label>
                <p>{selectedCustomer.taxRate}%</p>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Status</label>
                <div className="mt-1">{getStatusBadge(selectedCustomer.isActive)}</div>
              </div>
              <div>
                <label className="text-sm font-medium text-gray-500">Created</label>
                <p>{new Date(selectedCustomer.createdAt).toLocaleString()}</p>
              </div>
            </div>
            
            <div className="flex justify-end space-x-2 pt-4 border-t">
              <Button
                variant="secondary"
                onClick={() => {
                  setShowViewModal(false);
                  setSelectedCustomer(null);
                }}
              >
                Close
              </Button>
              <Button
                onClick={() => {
                  setShowViewModal(false);
                  setShowEditModal(true);
                }}
                className="bg-blue-600 hover:bg-blue-700 text-white"
              >
                <Edit className="w-4 h-4 mr-2" />
                Edit Customer
              </Button>
            </div>
          </div>
        )}
      </Modal>
    </div>
  );
};

export default ContractCustomers;