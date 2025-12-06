using System;
using System.Threading.Tasks;
using wms_android.Interfaces;

namespace wms_android.Services
{
    public class MainThreadService : IMainThreadService
    {
        public void BeginInvokeOnMainThread(Action action)
        {
            MainThread.BeginInvokeOnMainThread(action);
        }

        public Task InvokeOnMainThreadAsync(Action action)
        {
            return MainThread.InvokeOnMainThreadAsync(action);
        }

        public Task<T> InvokeOnMainThreadAsync<T>(Func<T> func)
        {
            return MainThread.InvokeOnMainThreadAsync(func);
        }
    }
} 