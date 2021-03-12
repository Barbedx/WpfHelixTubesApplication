using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppDatagridGroupingHeader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs events)
        {
            base.OnStartup(events);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
          LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
                e.SetObserved();
            };

        }
        private void LogUnhandledException(Exception exception, string source)
        {
            string message = $"Unhandled exception ({source})";
            try
            {
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
            }
            catch (Exception ex)
            {
                Debugger.Log(10, "Unhandled", "Exception in LogUnhandledException" + ex.Message + Environment.NewLine + ex.StackTrace);
       //         _logger.Error(ex, "Exception in LogUnhandledException");
            }
            finally
            {
                Debugger.Log(10, "Unhandled", message + exception.Message + Environment.NewLine + exception.StackTrace);
                ;
            }
        }
    }
    
}
