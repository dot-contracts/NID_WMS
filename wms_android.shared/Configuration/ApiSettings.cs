namespace wms_android.shared.Configuration;

public class ApiSettings
{
    public const string ConfigSection = "ApiSettings";
    public string BaseUrl { get; set; } = string.Empty;
    public int Port { get; set; }
} 