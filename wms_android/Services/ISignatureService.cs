using System.Threading.Tasks;
using Android.Graphics;
using wms_android.shared.Models;

namespace wms_android.Services
{
    public interface ISignatureService
    {
        Task<bool> InitializeSignaturePadAsync();
        Task<Bitmap> CaptureSignatureAsync(string signCode);
        Task<byte[]> GetSignatureDataAsync();
        Task<bool> SaveSignatureToDatabaseAsync(string waybillNumber, byte[] signatureData);
        Task<byte[]> GetSignatureFromDatabaseAsync(string waybillNumber);
        Task<SignatureData> GetSignatureAsync(int parcelId);
        Task<bool> SaveSignatureAsync(int parcelId, SignatureData signatureData);
    }
} 