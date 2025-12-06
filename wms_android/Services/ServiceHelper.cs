using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace wms_android.Services
{
    public static class ServiceHelper
    {
        public static T GetService<T>() where T : class
        {
            if (Application.Current?.Handler?.MauiContext == null)
                return null;
                
            return Application.Current.Handler.MauiContext.Services.GetService<T>();
        }
    }
}
