using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Workspace.Utility.Platform;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Layout;

namespace Workspace.UI.Platform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddChildrenToLeftSide(string title, FrameworkElement element)
        {
            LayoutAnchorGroup dp = this.DManager.Layout.LeftSide.Children[0] as LayoutAnchorGroup;
            LayoutAnchorable d = new LayoutAnchorable();
            d.Title = title;
            d.Content = element;
            dp.Children.Add(d);
        }

        public void AddChildrenToRightSide(string title, FrameworkElement element)
        {
            LayoutAnchorGroup dp = this.DManager.Layout.RightSide.Children[0] as LayoutAnchorGroup;
            LayoutAnchorable d = new LayoutAnchorable();
            d.Title = title;
            d.Content = element;
            dp.Children.Add(d);
        }

        public void AddChildrenToLayoutRootPanel(string title, FrameworkElement element)
        {
            LayoutDocumentPane dp = this.DManager.Layout.RootPanel.Children[0] as LayoutDocumentPane;
            LayoutDocument d = new LayoutDocument();
            d.Title = title;
            d.Content = element;
            dp.Children.Add(d);
            this.DManager.ActiveContent = d;
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            MenuItem subMeunItem = this.MainMenu.Items[2] as MenuItem;
            subMeunItem.Items.Add(menuItem);
        }
    }
}
