using RockBoxPlaylistTool.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WinForms = System.Windows.Forms;

namespace RockBoxPlaylistTool.Playlist
{
    public class PlaylistViewModel : BindableBase
    {
        private string path;
        private string filename;
        private ObservableCollection<SongData> items;
        private ObservableCollection<SongData> itemsView;
        private SongData selected;
        private DelegateCommand browseCommand;
        private DelegateCommand browseFileCommand;
        private string searchQuery;
        private Dispatcher dispatcher;
        public PlaylistViewModel()
        { 
            path = ConfigurationManager.AppSettings[FolderNames.PlaylistsDir];
            browseCommand = new DelegateCommand(browseExecute);
            browseFileCommand = new DelegateCommand(browseFileExecute);
            items = new ObservableCollection<SongData>();
            itemsView = [.. items];
            Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                h => this.PropertyChanged += h,
                h => this.PropertyChanged -= h)
                .Where(e => e.EventArgs.PropertyName == nameof(this.SearchQuery))
                .Throttle(TimeSpan.FromMilliseconds(250)) // delay
                .Subscribe(e => System.Windows.Application.Current.Dispatcher.BeginInvoke((Action)(
                () => StartSearch())));
        }
        public string Path 
        { 
            get { return path; } 
            set { SetProperty(ref path, value); } 
        }
        public string Filename
        {
            get { return filename; } 
            set { SetProperty(ref filename, value); } 
        }
        public string FilenameWithExtension
        {
            get { return filename + ".m3u8"; }
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
        public string SearchQuery 
        { 
            get { return searchQuery; } 
            set { SetProperty(ref searchQuery, value); } 
        }
        public ICommand BrowseCommand {
            get { return browseCommand; }
        }
        public ICommand BrowseFileCommand {
            get { return browseFileCommand; }
        }
        public bool AppendSong(SongData song)
        {
            if (song == null) { return false; }

            var clone = song.Clone();
            items.Add(clone);
            itemsView.Add(clone);
            return true;
        }
        public bool RemoveSong(SongData song)
        {
            if (song == null) { return false; }

            items.Remove(song);
            itemsView.Remove(song);
            return true;
        }
        public bool MoveSongUp(SongData song)
        {
            if (song == null) { return false; }

            int idx = items.IndexOf(song);
            int vidx = itemsView.IndexOf(song);
            if (idx == -1 || idx == 0 || vidx == -1 || vidx == 0) { return false; }

            items.Move(idx, idx-1);
            itemsView.Move(vidx, vidx-1);
            return true; 
        }
        public bool MoveSongDown(SongData song)
        {
            if (song == null) { return false; }

            int idx = items.IndexOf(song);
            int vidx = itemsView.IndexOf(song);
            if (idx == -1 || idx == items.Count - 1 || vidx == -1 || vidx == itemsView.Count) { return false; }

            items.Move(idx, idx+1);
            itemsView.Move(vidx, vidx+1);
            return true; 
        }
        public bool SavePlaylist()
        {
            return true;
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
        private void browseExecute()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = path;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            var result = folderBrowserDialog.ShowDialog();
            if (result == WinForms.DialogResult.OK || result == WinForms.DialogResult.Yes)
            {
                ConfigurationManager.AppSettings[FolderNames.PlaylistsDir] = folderBrowserDialog.SelectedPath;
            }
            Path = folderBrowserDialog.SelectedPath;
        }
        private void browseFileExecute()
        {
            var fileBrowserDialog = new OpenFileDialog();
            fileBrowserDialog.Filter = "m3u8 files (*.m3u8)|*.m3u8";
            fileBrowserDialog.InitialDirectory = path;

            var result = fileBrowserDialog.ShowDialog();
            if (result == WinForms.DialogResult.OK || result == WinForms.DialogResult.Yes)
            {
                ConfigurationManager.AppSettings[FolderNames.PlaylistsDir] = System.IO.Path.GetDirectoryName(fileBrowserDialog.FileName);
                Path = System.IO.Path.GetDirectoryName(fileBrowserDialog.FileName);
                Filename = System.IO.Path.GetFileName(fileBrowserDialog.FileName).Replace(".m3u8", "");
                items.Clear();
                itemsView.Clear();
                var newItems = SongCollectionBuilder.BuildFromFile(fileBrowserDialog.FileName);
                items.AddRange(newItems);
                itemsView.AddRange(newItems);
            }
        }
    }
}
