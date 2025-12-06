using System;
using System.Threading.Tasks;

namespace wms_android.shared.Interfaces
{
    public interface ISmsService
    {
        /// <summary>
        /// Sends an SMS message to the specified phone number
        /// </summary>
        /// <param name="phoneNumber">The recipient's phone number</param>
        /// <param name="message">The message content</param>
        /// <returns>True if message was sent successfully, false otherwise</returns>
        Task<bool> SendSmsAsync(string phoneNumber, string message);
        
        /// <summary>
        /// Sends a parcel pickup notification SMS to the specified phone number
        /// </summary>
        /// <param name="receiverPhone">Receiver's phone number</param>
        /// <param name="receiverName">Receiver's name</param>
        /// <param name="senderName">Sender's name</param>
        /// <param name="senderPhone">Sender's phone number</param>
        /// <param name="quantity">Number of parcels</param>
        /// <param name="destination">Destination location</param>
        /// <param name="amount">Amount to be paid</param>
        /// <param name="paymentMethod">Payment method (COD, Paid, etc.)</param>
        /// <param name="waybillNumber">The waybill number</param>
        /// <returns>True if message was sent successfully, false otherwise</returns>
        Task<bool> SendParcelPickupNotificationAsync(
            string receiverPhone,
            string receiverName,
            string senderName,
            string senderPhone,
            int quantity,
            string destination,
            decimal amount,
            string paymentMethod,
            string waybillNumber);
            
        /// <summary>
        /// Sends a templated parcel pickup notification SMS with message format based on payment method
        /// </summary>
        /// <param name="parcel">The parcel object containing all necessary data</param>
        /// <returns>True if message was sent successfully, false otherwise</returns>
        Task<bool> SendTemplatedParcelNotificationAsync(
            shared.Models.Parcel parcel);
            
        /// <summary>
        /// Sends a templated parcel delivery confirmation SMS to the sender
        /// </summary>
        /// <param name="parcel">The parcel object containing all necessary data</param>
        /// <returns>True if message was sent successfully, false otherwise</returns>
        Task<bool> SendParcelDeliveryConfirmationAsync(
            shared.Models.Parcel parcel);
    }
} 