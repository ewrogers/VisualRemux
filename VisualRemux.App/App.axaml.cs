using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using VisualRemux.App.Logging;
using VisualRemux.App.Services;
using VisualRemux.App.ViewModels;
using VisualRemux.App.ViewModels.Logging;
using VisualRemux.App.ViewModels.Remux;
using VisualRemux.App.ViewModels.Settings;
using VisualRemux.App.Views;

namespace VisualRemux.App;

public partial class App : Application
{
    public new static App? Current => Application.Current as App;
    
    public IServiceProvider Services { get; private set; } = null!;
    public Window? MainWindow { get; private set; }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Services = ConfigureServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();

            desktop.MainWindow = new MainWindow
            {
                Title = "VisualRemux",
                DataContext = Services.GetService<MainWindowViewModel>()
            };
            
            MainWindow = desktop.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Register view models
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<RemuxToolViewModel>();
        services.AddTransient<OutputLogViewModel>();
        services.AddTransient<ApplicationSettingsViewModel>();

        // Register services
        services.AddSingleton<ILogger, Logger>();
        services.AddSingleton<IFileService, FileService>(_ => new FileService(() => Current!.MainWindow!));

        return services.BuildServiceProvider();
    }

    private static void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}