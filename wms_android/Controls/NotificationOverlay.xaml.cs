using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace wms_android.Controls
{
    public partial class NotificationOverlay : Grid
    {
        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(NotificationOverlay), false);

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(NotificationOverlay), "Success");

        public static readonly BindableProperty MessageProperty =
            BindableProperty.Create(nameof(Message), typeof(string), typeof(NotificationOverlay), "Operation completed successfully");

        public new bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public NotificationOverlay()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async Task ShowAsync(string title, string message, int durationSeconds = 6)
        {
            Title = title;
            Message = message;
            
            // Animate in
            Opacity = 0;
            IsVisible = true;
            await this.FadeTo(1, 300);
            
            // Wait for duration
            await Task.Delay(durationSeconds * 1000);
            
            // Animate out
            await this.FadeTo(0, 300);
            IsVisible = false;
        }
    }
}