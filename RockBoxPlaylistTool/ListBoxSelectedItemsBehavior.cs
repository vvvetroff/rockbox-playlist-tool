using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RockBoxPlaylistTool
{

    // In your Attached Property/Behavior (simplified)
    public static class ListBoxSelectedItemsBehavior
    {
        public static readonly DependencyProperty SelectedItemsSourceProperty =
            DependencyProperty.RegisterAttached("SelectedItemsSource", typeof(ObservableCollection<object>),
                typeof(ListBoxSelectedItemsBehavior), new PropertyMetadata(OnSelectedItemsSourceChanged));

        public static ObservableCollection<object> GetSelectedItemsSource(DependencyObject obj)
        {
            return (ObservableCollection<object>)obj.GetValue(SelectedItemsSourceProperty);
        }

        public static void SetSelectedItemsSource(DependencyObject obj, ObservableCollection<object> value)
        {
            obj.SetValue(SelectedItemsSourceProperty, value);
        }

        private static void OnSelectedItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Controls.ListBox listBox)
            {
                // Attach/detach event handlers to synchronize selected items
                // with the bound ObservableCollection in the ViewModel.
            }
        }
    }
}
