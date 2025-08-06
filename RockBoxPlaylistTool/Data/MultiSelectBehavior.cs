using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RockBoxPlaylistTool.Data
{
    public static class MultiSelectBehavior
    {
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItems",
                typeof(IList),
                typeof(MultiSelectBehavior),
                new PropertyMetadata(null, OnSelectedItemsChanged));

        public static void SetSelectedItems(DependencyObject element, IList value)
        {
            element.SetValue(SelectedItemsProperty, value);
        }

        public static IList GetSelectedItems(DependencyObject element)
        {
            return (IList)element.GetValue(SelectedItemsProperty);
        }

        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is System.Windows.Controls.ListBox listBox)
            {
                listBox.SelectionChanged -= ListBox_SelectionChanged;
                listBox.SelectionChanged += ListBox_SelectionChanged;
            }
        }

        private static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Controls.ListBox listBox)
            {
                var selectedItems = GetSelectedItems(listBox);
                if (selectedItems != null)
                {
                    if (e.AddedItems != null)
                    {
                        foreach (var item in e.AddedItems)
                        {
                            if (item is SongData song)
                            {
                                song.IsSelected = true;
                                if (!selectedItems.Contains(song)) { selectedItems.Add(item); }
                            }
                        }
                    }
                    if (e.RemovedItems != null)
                    {
                        foreach (var item in e.RemovedItems)
                        {
                            if (item is SongData song)
                            {
                                song.IsSelected = false;
                                if (selectedItems.Contains(song)) { selectedItems.Remove(item); }
                            }
                        }
                    }
                }
            }
        }
    }
}
