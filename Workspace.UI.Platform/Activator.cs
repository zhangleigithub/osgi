using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UIShell.OSGi;
using Workspace.Utility.Platform;

namespace Workspace.UI.Platform
{
    public class Activator : IBundleActivator
    {
        public void Start(IBundleContext context)
        {
            context.AddService<IWindow>(new MainWindow());
        }

        public void Stop(IBundleContext context)
        {
            //todo
        }
    }
}
