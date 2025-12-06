using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wms_android.shared.Models
{
    /// <summary>
    /// Class to generate SMS template messages
    /// </summary>
    public static class SmsTemplate
    {
        /// <summary>
        /// Generates a parcel pickup notification SMS template
        /// </summary>
        /// <param name="receiver">Name of the parcel receiver</param>
        /// <param name="sender">Name of the parcel sender</param>
        /// <param name="senderPhone">Phone number of the sender</param>
        /// <param name="quantity">Number of parcels</param>
        /// <param name="destination">Destination location</param>
        /// <param name="amount">Amount to be paid</param>
        /// <param name="paymentMethod">Method of payment (e.g., "Cash on Delivery")</param>
        /// <param name="waybillNumber">The waybill number of the parcel</param>
        /// <returns>Formatted SMS message</returns>
        public static string GenerateParcelPickupMessage(
            string receiver,
            string sender,
            string senderPhone,
            int quantity,
            string destination,
            decimal amount,
            string paymentMethod,
            string waybillNumber)
        {
            Debug.WriteLine($"Generating SMS for {receiver} from {sender}, waybill: {waybillNumber}");
            
            // Ensure receiver name isn't null
            receiver = string.IsNullOrEmpty(receiver) ? "Customer" : receiver;
            
            // Mask the sender's phone number for privacy
            var maskedPhone = MaskPhoneNumber(senderPhone);
            
            // Create payment information text based on the payment method
            string amountInfo;
            switch (paymentMethod)
            {
                case "COD":
                    amountInfo = $"Amount to pay on delivery: KES {amount:N2}";
                    break;
                case "Paid":
                    amountInfo = "The parcel has been fully paid for";
                    break;
                case "Contract":
                    amountInfo = "This parcel is on contract basis";
                    break;
                default:
                    amountInfo = "Payment has been processed";
                    break;
            }
            
            // Build the complete message
            var message = $"Dear {receiver}, your parcel {waybillNumber} from {sender} ({maskedPhone}) containing {quantity} item(s)" +
                         $"has been picked up for deliverly to {destination}. {amountInfo}" + 
                         $"Thank you for choosing NID Logistics.";
            
            Debug.WriteLine($"Generated message: {message}");
            return message;
        }

        /// <summary>
        /// Masks part of the phone number for privacy
        /// </summary>
        /// <param name="phoneNumber">The phone number to mask</param>
        /// <returns>Masked phone number</returns>
        private static string MaskPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return "N/A";
            
            // Keep the first 4 and last 2 digits visible, mask the rest
            if (phoneNumber.Length > 6)
            {
                int visibleEnd = phoneNumber.Length - 2;
                return phoneNumber.Substring(0, 4) + new string('*', visibleEnd - 4) + phoneNumber.Substring(visibleEnd);
            }
            
            // For very short numbers, just return as is
            return phoneNumber;
        }
    }
} 