namespace MoneyFox.Win;

using System;
using System.Threading.Tasks;
using Autofac;
using CommonServiceLocator;
using Core._Pending_.Common.Facades;
using Core.ApplicationCore.UseCases.BackupUpload;
using Core.ApplicationCore.UseCases.DbBackup;
using Core.Commands.Payments.ClearPayments;
using Core.Commands.Payments.CreateRecurringPayments;
using Core.Common.Interfaces;
using Core.Resources;
using MediatR;
using Microsoft.UI.Xaml;
using Serilog;
using Services;

public partial class App : Application
{
    private bool isRunning;

    public App()
    {
        LoggerService.Initialize();
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule<WindowsModule>();
        ViewModelLocator.RegisterServices(builder);
        var mainWindow = new MainWindow();
        mainWindow.Activate();
        ExecuteStartupTasks();
    }

    private void ExecuteStartupTasks()
    {
        Task.Run(async () => { await StartupTasksAsync(); }).ConfigureAwait(false);
    }

    private async Task StartupTasksAsync()
    {
        // Don't execute this again when already running
        if (isRunning)
        {
            return;
        }

        isRunning = true;
        var toastService = ServiceLocator.Current.GetInstance<IToastService>();
        var settingsFacade = ServiceLocator.Current.GetInstance<ISettingsFacade>();
        var mediator = ServiceLocator.Current.GetInstance<IMediator>();

        try
        {
            if (settingsFacade.IsBackupAutouploadEnabled && settingsFacade.IsLoggedInToBackupService)
            {
                var backupService = ServiceLocator.Current.GetInstance<IBackupService>();
                await backupService.RestoreBackupAsync();
            }

            await mediator.Send(new ClearPaymentsCommand());
            await mediator.Send(new CreateRecurringPaymentsCommand());

            var uploadResult = await mediator.Send(new UploadBackup.Command());
            if (uploadResult == UploadBackup.UploadResult.Successful)
            {
                await toastService.ShowToastAsync(Strings.BackupCreatedMessage);
            }
        }
        catch (Exception ex)
        {
            Log.Fatal(exception: ex, messageTemplate: "Error during startup");
        }
        finally
        {
            settingsFacade.LastExecutionTimeStampSyncBackup = DateTime.Now;
            isRunning = false;
        }
    }
}
