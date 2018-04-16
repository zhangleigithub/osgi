using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using UIShell.OSGi;
using Workspace.Utility.Platform;

namespace Workspace.Platform
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;

            using (BundleRuntime runtime = new BundleRuntime())
            {
                runtime.Start();
                Window win = runtime.GetFirstOrDefaultService<IWindow>() as Window;
                win.ShowDialog();
                runtime.Stop();
            }

            base.OnStartup(e);
        }

    }
}
