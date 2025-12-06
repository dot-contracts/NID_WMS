using System;

namespace wms_android.shared.Models;

public class SmsRequest
{
    public string UserId { get; set; }
    public string Password { get; set; }
    public string Message { get; set; }
    public string MobileNumbers { get; set; }
    public string SenderId { get; set; }
    public string MessageType { get; set; }
    public string DuplicateCheck { get; set; }
}
