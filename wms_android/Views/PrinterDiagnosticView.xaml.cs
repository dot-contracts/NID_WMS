using System;
using System.Diagnostics;
using System.Threading.Tasks;
using wms_android.Services;
using wms_android.Utils;
using wms_android.ViewModels;
using wms_android.Interfaces;

namespace wms_android.Views
{
    public partial class PrinterDiagnosticView : ContentPage
    {
        private PrinterDiagnosticViewModel _viewModel;
        private readonly IPrinterService _printerService;
        
        public PrinterDiagnosticView(PrinterDiagnosticViewModel viewModel, IPrinterService printerService)
        {
            InitializeComponent();
            
            _viewModel = viewModel;
            _printerService = printerService;
            BindingContext = _viewModel;
            
            // Subscribe to view model events
            _viewModel.DiagnosticCompleted += OnDiagnosticCompleted;
            _viewModel.PrinterStatusUpdated += OnPrinterStatusUpdated;
            _viewModel.ComprehensiveDiagnosticCompleted += OnComprehensiveDiagnosticCompleted;
            
            // Update printer status when page appears
            this.Appearing += async (sender, e) => 
            {
                await UpdatePrinterStatus();
            };
            
            Debug.WriteLine("PrinterDiagnosticView initialized with dependency injection");
        }
        
        private async Task UpdatePrinterStatus()
        {
            try
            {
                // Use the printer service to get status
                var status = await _printerService.GetPrinterStatusAsync();
                PrinterStatusLabel.Text = $"Printer Status: {status}";
                
                // Display printer service type
                PrinterTypeLabel.Text = $"Printer Service: {_printerService.GetType().Name}";
                
                // Display device type
                if (_printerService is VanstonePrinterService)
                {
                    PrinterTypeLabel.Text += " (A90 Device)";
                }
                else if (_printerService is CS30PrinterService)
                {
                    PrinterTypeLabel.Text += " (CS30 Device)";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating printer status: {ex.Message}");
                PrinterStatusLabel.Text = $"Error: {ex.Message}";
                PrinterTypeLabel.Text = "Printer status unavailable";
            }
        }
        
        private void OnDiagnosticCompleted(object sender, DiagnosticResult result)
        {
            DiagnosticResultsLabel.Text = result.ToString();
        }
        
        private void OnComprehensiveDiagnosticCompleted(object sender, ComprehensiveDiagnosticResult result)
        {
            // Display the comprehensive diagnostic results in the results label
            DiagnosticResultsLabel.Text = result.ToString();
        }
        
        private void OnPrinterStatusUpdated(object sender, string status)
        {
            PrinterStatusLabel.Text = status;
        }
    }
} 