using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using UIShell.OSGi;
using Workspace.Utility.Platform;

namespace BCD.Reader
{
    public class Activator : IBundleActivator
    {
        public void Start(IBundleContext context)
        {
            IWindow window = context.GetFirstOrDefaultService<IWindow>();

            MenuItem menuItem = new MenuItem();
            menuItem.Header = "cs";
            menuItem.Click += (sender, e) =>
            {
                WebBrowser browser = new WebBrowser();
                browser.Navigate("https://www.baidu.com/");
                //Frame frame = new Frame();
                //frame.Source = new Uri("https://www.baidu.com/");
                window.AddChildrenToLayoutRootPanel("BCD Reader", browser);
            };

            window.AddMenuItem(menuItem);

            window.AddChildrenToLeftSide("Text1", new TextBlock() { Text="Text01"});
            window.AddChildrenToRightSide("Text1", new TextBlock() { Text = "Text02" });
        }

        public void Stop(IBundleContext context)
        {
            //todo
        }
    }
}
