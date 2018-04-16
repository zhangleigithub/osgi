using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UIShell.OSGi;
using Workspace.Utility.Platform;

namespace VS.Coverage.Analysis
{
    public class Activator : IBundleActivator
    {
        public void Start(IBundleContext context)
        {
            IWindow window = context.GetFirstOrDefaultService<IWindow>();

            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Coverage";
            menuItem.Click += (sender, e) =>
            {
                window.AddChildrenToLayoutRootPanel("Coverage Reader", new CustomControl());
            };

            window.AddMenuItem(menuItem);
        }

        public void Stop(IBundleContext context)
        {
            //todo
        }
    }
}
