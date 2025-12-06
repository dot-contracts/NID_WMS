using Android.Content;
using Android.OS;

namespace CH10PosSdk.Additions
{
    public class CH10PrinterHelper
    {
        public static Intent CreatePrinterServiceIntent()
        {
            var intent = new Intent();
            intent.SetClassName("recieptservice.com.recieptservice", "recieptservice.com.recieptservice.service.PrinterService");
            return intent;
        }
    }
}