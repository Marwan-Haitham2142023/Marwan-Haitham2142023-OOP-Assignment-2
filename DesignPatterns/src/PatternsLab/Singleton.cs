using System;


public sealed class AppConfig
{
    public static int LoadCount;

    private static readonly Lazy<AppConfig> _lazy =
        new Lazy<AppConfig>(() => new AppConfig());


    private AppConfig()
    {
        LoadCount++;
        Console.WriteLine($"[AppConfig] Loading settings from disk... (load #{LoadCount})");
  
    }

    public static AppConfig Instance => _lazy.Value;
}

public class DatabaseService
{
    public AppConfig Config { get; } = AppConfig.Instance;

    public void Connect()
    {
        Console.WriteLine($"Connecting to {Config.DbConnection}");
    }
}

public class UiService
{
    public AppConfig Config { get; } = AppConfig.Instance;

    public void Render()
    {
        Console.WriteLine($"UI is using theme: {Config.Theme}");
    }
}
