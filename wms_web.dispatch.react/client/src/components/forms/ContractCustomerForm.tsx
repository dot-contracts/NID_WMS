import React from 'react';
import { useForm } from 'react-hook-form';
import { Save } from 'lucide-react';
import { Input, Button } from '../ui';
import { ContractCustomer } from '../../services/wmsApi';

interface ContractCustomerFormData {
  name: string;
  companyName: string;
  email: string;
  phone: string;
  address: string;
  contactPerson: string;
  contractNumber: string;
  paymentTerms: string;
  taxRate: number;
  isActive: boolean;
}

interface ContractCustomerFormProps {
  onSubmit: (data: ContractCustomerFormData) => Promise<void>;
  customer?: ContractCustomer | null;
  isLoading?: boolean;
}

const ContractCustomerForm: React.FC<ContractCustomerFormProps> = ({
  onSubmit,
  customer,
}) => {
  const isEditing = !!customer;

  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm<ContractCustomerFormData>({
    defaultValues: {
      name: customer?.name || '',
      companyName: customer?.companyName || '',
      email: customer?.email || '',
      phone: customer?.phone || '',
      address: customer?.address || '',
      contactPerson: customer?.contactPerson || '',
      contractNumber: customer?.contractNumber || '',
      paymentTerms: customer?.paymentTerms || 'Net 30',
      taxRate: customer?.taxRate || 0,
      isActive: customer?.isActive ?? true,
    },
  });

  // Reset form when customer changes
  React.useEffect(() => {
    if (customer) {
      reset({
        name: customer.name || '',
        companyName: customer.companyName || '',
        email: customer.email || '',
        phone: customer.phone || '',
        address: customer.address || '',
        contactPerson: customer.contactPerson || '',
        contractNumber: customer.contractNumber || '',
        paymentTerms: customer.paymentTerms || 'Net 30',
        taxRate: customer.taxRate || 0,
        isActive: customer.isActive ?? true,
      });
    } else {
      reset({
        name: '',
        companyName: '',
        email: '',
        phone: '',
        address: '',
        contactPerson: '',
        contractNumber: '',
        paymentTerms: 'Net 30',
        taxRate: 0,
        isActive: true,
      });
    }
  }, [customer, reset]);

  const handleFormSubmit = async (data: ContractCustomerFormData) => {
    try {
      await onSubmit(data);
      reset();
    } catch (error) {
      throw error;
    }
  };

  return (
    <form onSubmit={handleSubmit(handleFormSubmit)} className="space-y-6">
          {/* Basic Information */}
          <div>
            <h4 className="text-md font-medium text-gray-900 dark:text-white mb-4">
              Basic Information
            </h4>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <Input
                label="Customer Name *"
                {...register('name', {
                  required: 'Customer name is required',
                  minLength: {
                    value: 2,
                    message: 'Name must be at least 2 characters',
                  },
                })}
                error={errors.name?.message}
                placeholder="Enter customer name"
              />

              <Input
                label="Company Name"
                {...register('companyName')}
                error={errors.companyName?.message}
                placeholder="Enter company name"
              />

              <Input
                label="Email Address *"
                type="email"
                {...register('email', {
                  required: 'Email is required',
                  pattern: {
                    value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
                    message: 'Invalid email address',
                  },
                })}
                error={errors.email?.message}
                placeholder="customer@company.com"
              />

              <Input
                label="Phone Number"
                type="tel"
                {...register('phone')}
                error={errors.phone?.message}
                placeholder="+1-555-0123"
              />

              <Input
                label="Contact Person"
                {...register('contactPerson')}
                error={errors.contactPerson?.message}
                placeholder="Primary contact name"
              />

              <div className="md:col-span-2">
                <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                  Address
                </label>
                <textarea
                  {...register('address')}
                  rows={3}
                  className="w-full px-3 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-4 focus:ring-brand-100 focus:border-brand-500 transition-all duration-200 dark:bg-gray-800 dark:border-gray-700 dark:text-white"
                  placeholder="Enter full address"
                />
                {errors.address && (
                  <p className="mt-1 text-sm text-error-600 dark:text-error-400">
                    {errors.address.message}
                  </p>
                )}
              </div>
            </div>
          </div>

          {/* Contract Information */}
          <div>
            <h4 className="text-md font-medium text-gray-900 dark:text-white mb-4">
              Contract Information
            </h4>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
              <Input
                label="Contract Number (Optional)"
                {...register('contractNumber')}
                error={errors.contractNumber?.message}
                placeholder="Leave empty for auto-generation (FHL-2024-CTR001)"
              />

              <div>
                <label className="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">
                  Payment Terms *
                </label>
                <select
                  {...register('paymentTerms', {
                    required: 'Payment terms are required',
                  })}
                  className="w-full px-3 py-2.5 border border-gray-200 rounded-lg focus:outline-none focus:ring-4 focus:ring-brand-100 focus:border-brand-500 transition-all duration-200 dark:bg-gray-800 dark:border-gray-700 dark:text-white"
                >
                  <option value="Net 15">Net 15</option>
                  <option value="Net 30">Net 30</option>
                  <option value="Net 45">Net 45</option>
                  <option value="Net 60">Net 60</option>
                  <option value="Cash on Delivery">Cash on Delivery</option>
                  <option value="Prepaid">Prepaid</option>
                </select>
                {errors.paymentTerms && (
                  <p className="mt-1 text-sm text-error-600 dark:text-error-400">
                    {errors.paymentTerms.message}
                  </p>
                )}
              </div>

              <Input
                label="Tax Rate (%)"
                type="number"
                step="0.01"
                min="0"
                max="100"
                {...register('taxRate', {
                  valueAsNumber: true,
                  min: {
                    value: 0,
                    message: 'Tax rate cannot be negative',
                  },
                  max: {
                    value: 100,
                    message: 'Tax rate cannot exceed 100%',
                  },
                })}
                error={errors.taxRate?.message}
                placeholder="8.5"
              />
            </div>
          </div>

          {/* Status */}
          <div>
            <h4 className="text-md font-medium text-gray-900 dark:text-white mb-4">
              Status
            </h4>
            <div className="flex items-center">
              <input
                type="checkbox"
                id="isActive"
                {...register('isActive')}
                className="w-4 h-4 text-brand-600 bg-gray-100 border-gray-300 rounded focus:ring-brand-500 dark:focus:ring-brand-600 dark:ring-offset-gray-800 focus:ring-2 dark:bg-gray-700 dark:border-gray-600"
              />
              <label
                htmlFor="isActive"
                className="ml-2 text-sm font-medium text-gray-900 dark:text-gray-300"
              >
                Active Customer
              </label>
            </div>
            <p className="mt-1 text-sm text-gray-500 dark:text-gray-400">
              Inactive customers cannot receive new invoices
            </p>
          </div>
        {/* Form Actions */}
        <div className="flex justify-end space-x-3 pt-6 border-t">
          <Button
            type="submit"
            variant="primary"
            disabled={isSubmitting}
            isLoading={isSubmitting}
          >
            <Save className="w-4 h-4 mr-2" />
            {isEditing ? 'Update Customer' : 'Create Customer'}
          </Button>
        </div>
      </form>
  );
};

export default ContractCustomerForm;