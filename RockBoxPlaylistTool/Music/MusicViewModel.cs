using RockBoxPlaylistTool.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WinForms = System.Windows.Forms;

namespace RockBoxPlaylistTool.Music
{
    public class MusicViewModel : BindableBase
    {
        private string path;
        private ObservableCollection<SongData> items;
        // TODO: change to itemsVisible
        private ObservableCollection<SongData> itemsView;
        private SongData selected;
        private DelegateCommand browseCommand;
        private string searchQuery;
        private Dispatcher dispatcher;
        public MusicViewModel()
        {
            path = ConfigurationManager.AppSettings[FolderNames.MusicDir];
            browseCommand = new DelegateCommand(browseExecute);
            items = SongCollectionBuilder.Build(path);
            itemsView = [.. items];

            Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h => this.PropertyChanged += h,
                h => this.PropertyChanged -= h)
                .Where(e => e.EventArgs.PropertyName == nameof(this.SearchQuery))
                .Throttle(TimeSpan.FromMilliseconds(250)) // delay
                .Subscribe(e => System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(
                () => StartSearch())));
        }
        public void StartSearch()
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                itemsView.Clear();
                itemsView.AddRange(items);
                return;
            }
            var newItems = new List<SongData>();
            foreach (var item in items)
            {
                if (item.Artist.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase) ||
                    item.Album.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase) || 
                    item.Title.Contains(searchQuery, StringComparison.CurrentCultureIgnoreCase))
                {
                    newItems.Add(item);
                }
            }
            itemsView.Clear();
            itemsView.AddRange(newItems);
        }

        public string Path 
        { 
            get { return path; } 
            set { SetProperty(ref path, value); } 
        }
        public string SearchQuery 
        { 
            get { return searchQuery; } 
            set { SetProperty(ref searchQuery, value); } 
        }
        public ObservableCollection<SongData> Items 
        { 
            get { return items; } 
            set { SetProperty(ref items, value); } 
        }
        public ObservableCollection<SongData> ItemsView
        {

            get { return itemsView; } 
            set { SetProperty(ref itemsView, value); } 
        }
        public SongData Selected 
        { 
            get { return selected; } 
            set { SetProperty(ref selected, value); } 
        }
        public ICommand BrowseCommand {
            get { return browseCommand; }
        }
        private void browseExecute()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = path;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            var result = folderBrowserDialog.ShowDialog();
            if (result == WinForms.DialogResult.OK || result == WinForms.DialogResult.Yes)
            {
                ConfigurationManager.AppSettings[FolderNames.MusicDir] = folderBrowserDialog.SelectedPath;
            }
            Path = folderBrowserDialog.SelectedPath;
            items.Clear();
            items.AddRange(SongCollectionBuilder.Build(path));
        }
    }
}
