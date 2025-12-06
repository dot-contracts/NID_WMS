using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using wms_android.Interfaces;

namespace wms_android.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        private string _title;
        private bool _isBusy;
        protected readonly ISessionTimeoutService _sessionTimeout;

        public BaseViewModel(ISessionTimeoutService sessionTimeout = null)
        {
            _sessionTimeout = sessionTimeout;
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        protected new bool SetProperty<T>(ref T storage, T value, string propertyName = null)
        {
            var changed = base.SetProperty(ref storage, value, propertyName);
            if (changed)
            {
                OnUserActivity();
            }
            return changed;
        }

        /// <summary>
        /// Call this method to indicate user activity and reset the session timeout
        /// </summary>
        protected virtual void OnUserActivity()
        {
            _sessionTimeout?.ResetSession();
        }
    }
}
