using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Workspace.Utility.Platform
{
    public interface IWindow
    {
        void AddChildrenToLeftSide(string title, FrameworkElement element);

        void AddChildrenToRightSide(string title, FrameworkElement element);

        void AddChildrenToLayoutRootPanel(string title, FrameworkElement element);

        void AddMenuItem(MenuItem menuItem);
    }
}
