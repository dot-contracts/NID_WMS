using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using wms_android.Models;
using System.Threading.Tasks;

namespace wms_android.Controls
{
    public partial class UpdateNotificationDialog : Grid
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(UpdateNotificationDialog), false);

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public UpdateNotificationViewModel _viewModel { get; private set; }

        public UpdateNotificationDialog()
        {
            InitializeComponent();
            _viewModel = new UpdateNotificationViewModel();
            BindingContext = _viewModel;
        }

        public async Task ShowUpdateDialogAsync(UpdateInfo updateInfo, Func<Task> onAccept, Func<Task> onDeny)
        {
            _viewModel.UpdateInfo = updateInfo;
            _viewModel.OnAccept = onAccept;
            _viewModel.OnDeny = onDeny;
            
            _viewModel.Message = $"A new version is available with latest changes.";
            _viewModel.VersionInfo = $"Version: {updateInfo.Version}\nCommit: {updateInfo.CommitSha[..8]}\n{updateInfo.CommitDate:MMM dd, yyyy}";

            // Animate in
            Opacity = 0;
            IsVisible = true;
            await this.FadeTo(1, 300);
        }

        public async Task HideDialogAsync()
        {
            await this.FadeTo(0, 300);
            IsVisible = false;
        }
    }

    public partial class UpdateNotificationViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _message = string.Empty;

        [ObservableProperty]
        private string _versionInfo = string.Empty;

        [ObservableProperty]
        private bool _isDownloading;

        [ObservableProperty]
        private double _downloadProgress;

        [ObservableProperty]
        private string _progressText = string.Empty;

        [ObservableProperty]
        private bool _showButtons = true;

        public UpdateInfo UpdateInfo { get; set; }
        public Func<Task> OnAccept { get; set; }
        public Func<Task> OnDeny { get; set; }

        [RelayCommand]
        public async Task Accept()
        {
            if (OnAccept != null)
            {
                ShowButtons = false;
                IsDownloading = true;
                await OnAccept();
            }
        }

        [RelayCommand]
        public async Task Deny()
        {
            if (OnDeny != null)
            {
                await OnDeny();
            }
        }

        public void UpdateDownloadProgress(double progress)
        {
            DownloadProgress = progress / 100.0;
            ProgressText = $"Downloading... {progress:F0}%";
        }
    }
}