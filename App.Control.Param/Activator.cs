using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using UIShell.OSGi;
using Workspace.Utility.Platform;

namespace App.Control.Param
{
    public class Activator : IBundleActivator
    {
        public void Start(IBundleContext context)
        {
            IWindow window = context.GetFirstOrDefaultService<IWindow>();

            MenuItem menuItem = new MenuItem();
            menuItem.Header = "MDI";
            menuItem.Click += (sender, e) =>
            {
                MainWindow main = new MainWindow();
                main.Show();
            };

            window.AddMenuItem(menuItem);
        }

        public void Stop(IBundleContext context)
        {
            throw new NotImplementedException();
        }
    }
}
