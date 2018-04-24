using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using MDIContainer.Control.Events;
using MDIContainer.Control.Commands;
using System.Windows.Media;
using System;
using MDIContainer.Control.Extensions;

namespace MDIContainer.Control
{
    public sealed class MDIContainer : System.Windows.Controls.Primitives.Selector
    {
        private IList InternalItemSource { get; set; }
        internal int MinimizedWindowsCount { get; private set; }

        static MDIContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MDIContainer), new FrameworkPropertyMetadata(typeof(MDIContainer)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new MDIWindow();
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var window = element as MDIWindow;
            if (window != null)
            {
                window.IsCloseButtonEnabled = this.InternalItemSource != null;
                window.FocusChanged += OnWindowFocusChanged;
                window.Closing += OnWindowClosing;
                window.WindowStateChanged += OnWindowStateChanged;
                window.Initialize(this);

                Canvas.SetTop(window, 32 * this.Items.Count);
                Canvas.SetLeft(window, 32 * this.Items.Count);

                window.Focus();
            }

            base.PrepareContainerForItemOverride(element, item);
        }

        private void OnWindowStateChanged(object sender, WindowStateChangedEventArgs e)
        {
            if (e.NewValue == WindowState.Minimized)
            {
                this.MinimizedWindowsCount++;
            }
            else if (e.OldValue == WindowState.Minimized)
            {
                this.MinimizedWindowsCount--;
            }
        }

        private void OnWindowClosing(object sender, RoutedEventArgs e)
        {
            var window = sender as MDIWindow;
            if (window != null && window.DataContext != null)
            {
                if (this.InternalItemSource != null)
                {
                    this.InternalItemSource.Remove(window.DataContext);
                }

                // clear
                window.FocusChanged -= OnWindowFocusChanged;
                window.Closing -= OnWindowClosing;
                window.WindowStateChanged -= OnWindowStateChanged;
                window.DataContext = null;
            }
        }

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);

            if (newValue != null && newValue is IList)
            {
                this.InternalItemSource = newValue as IList;
            }
        }

        private void OnWindowFocusChanged(object sender, RoutedEventArgs e)
        {
            if (((MDIWindow)sender).IsFocused)
            {
                this.SelectedItem = e.OriginalSource;
            }
        }

        public RelayCommand CascadeCommand { get; private set; }

        public RelayCommand VerticalCommand { get; private set; }

        public RelayCommand HorizontalCommand { get; private set; }

        public RelayCommand CloseAllCommand { get; private set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (CascadeCommand == null)
            {
                this.CascadeCommand = new RelayCommand(Cascade);
            }

            if (VerticalCommand == null)
            {
                this.VerticalCommand = new RelayCommand(Vertical);
            }

            if (HorizontalCommand == null)
            {
                this.HorizontalCommand = new RelayCommand(Horizontal);
            }

            if (CloseAllCommand == null)
            {
                this.CloseAllCommand = new RelayCommand(CloseAll);
            }
        }

        private Canvas GetCanvas()
        {
            var border = VisualTreeHelper.GetChild(this, 0) as Border;

            var itemsPresenter = VisualTreeHelper.GetChild(border, 0) as ItemsPresenter;

            return VisualTreeHelper.GetChild(itemsPresenter, 0) as Canvas;
        }

        private void Cascade(object obj)
        {
            var canvas = this.GetCanvas();

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(canvas); i++)
            {
                var window = VisualTreeHelper.GetChild(canvas, i) as MDIWindow;

                window.Width = this.ActualWidth / 2;
                window.Height = this.ActualHeight / 2;
                Canvas.SetTop(window, 32 * i);
                Canvas.SetLeft(window, 32 * i);
            }
        }

        private void Vertical(object obj)
        {
            var canvas = this.GetCanvas();

            int count = VisualTreeHelper.GetChildrenCount(canvas);

            if (count == 0)
            {
                return;
            }

            int rows = (int)Math.Sqrt(count);
            int rowItemCount = count / rows;
            int index = 0;

            for (int i = 0; i < rows; i++)
            {
                if (i == rows - 1 && count % rows != 0)
                {
                    rowItemCount += count % rows;
                }

                for (int j = 0; j < rowItemCount; j++)
                {
                    var window = VisualTreeHelper.GetChild(canvas, index) as MDIWindow;

                    if (window.WindowState != WindowState.Normal)
                    {
                        WindowBehaviorExtension.Normalize(window);
                    }

                    window.Width = canvas.ActualWidth / rowItemCount - 2;
                    window.Height = canvas.ActualHeight / rows - 2;
                    Canvas.SetTop(window, canvas.ActualHeight / rows * i);
                    Canvas.SetLeft(window, canvas.ActualWidth / rowItemCount * j);

                    index++;
                }
            }
        }

        private void Horizontal(object obj)
        {
            var canvas = this.GetCanvas();

            int count = VisualTreeHelper.GetChildrenCount(canvas);

            if (count == 0)
            {
                return;
            }

            int columns = (int)Math.Sqrt(count);
            int columnItemCount = count / columns;
            int index = 0;

            for (int i = 0; i < columns; i++)
            {
                if (i == columns - 1 && count % columns != 0)
                {
                    columnItemCount += count % columns;
                }

                for (int j = 0; j < columnItemCount; j++)
                {
                    var window = VisualTreeHelper.GetChild(canvas, index) as MDIWindow;

                    if (window.WindowState != WindowState.Normal)
                    {
                        WindowBehaviorExtension.Normalize(window);
                    }

                    window.Width = canvas.ActualWidth / columns - 2;
                    window.Height = canvas.ActualHeight / columnItemCount - 2;
                    Canvas.SetTop(window, canvas.ActualHeight / columnItemCount * j);
                    Canvas.SetLeft(window, canvas.ActualWidth / columns * i);

                    index++;
                }
            }
        }

        private void CloseAll(object obj)
        {
            var canvas = this.GetCanvas();

            for (int i = VisualTreeHelper.GetChildrenCount(canvas) - 1; i >= 0; i--)
            {
                var window = VisualTreeHelper.GetChild(canvas, i) as MDIWindow;
                window.closeButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, window.closeButton));
            }
        }
    }
}
