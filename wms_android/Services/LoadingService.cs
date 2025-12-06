using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace wms_android.Services
{
    public class LoadingService : INotifyPropertyChanged
    {
        private static readonly Lazy<LoadingService> _instance = new Lazy<LoadingService>(() => new LoadingService());
        
        public static LoadingService Instance => _instance.Value;
        
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private string _loadingMessage = "Loading...";
        public string LoadingMessage
        {
            get => _loadingMessage;
            private set
            {
                if (_loadingMessage != value)
                {
                    _loadingMessage = value;
                    OnPropertyChanged();
                }
            }
        }
        
        // Show the loading indicator with an optional message
        public void Show(string message = "Loading...")
        {
            LoadingMessage = message;
            IsLoading = true;
        }
        
        // Hide the loading indicator
        public void Hide()
        {
            IsLoading = false;
        }
        
        // Helper method to execute a task with loading indicator
        public async Task ExecuteWithLoadingAsync(Func<Task> task, string loadingMessage = "Loading...")
        {
            try
            {
                Show(loadingMessage);
                await task();
            }
            finally
            {
                Hide();
            }
        }
        
        // Helper method to execute a task with loading indicator and return a result
        public async Task<T> ExecuteWithLoadingAsync<T>(Func<Task<T>> task, string loadingMessage = "Loading...")
        {
            try
            {
                Show(loadingMessage);
                return await task();
            }
            finally
            {
                Hide();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 