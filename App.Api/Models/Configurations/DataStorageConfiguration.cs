namespace App.Api.Models.Configurations;

public class DataStorageConfiguration
{
    public string ConnectionString { get; set; } = string.Empty;
    
    public bool IsInMemory { get; set; }
}