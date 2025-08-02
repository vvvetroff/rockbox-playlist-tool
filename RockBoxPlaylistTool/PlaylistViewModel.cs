using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool
{
    public class PlaylistViewModel : BindableBase
    {
        private string path;
        private ObservableCollection<SongData> items;
        private ObservableCollection<SongData> selectedItems;
        private SongData selected;
        public PlaylistViewModel()
        { 
            items = new ObservableCollection<SongData>();
            selectedItems = new ObservableCollection<SongData>();
        }
        public string Path 
        { 
            get { return path; } 
            set { SetProperty(ref path, value); } 
        }
        public ObservableCollection<SongData> Items 
        { 
            get { return items; } 
            set { SetProperty(ref items, value); } 
        }
        public ObservableCollection<SongData> SelectedItems 
        { 
            get { return selectedItems; } 
            set { SetProperty(ref selectedItems, value); } 
        }
        public SongData Selected 
        { 
            get { return selected; } 
            set { SetProperty(ref selected, value); } 
        }
        public bool AppendSong(SongData song)
        {
            if (song == null) return false;
            // creates shallow copy of selected songs
            items.Add(song.Clone());
            return true;
        }
        public bool RemoveSong(SongData song)
        {
            if (song == null) return false;
            items.Remove(song);
            return true;
        }
        public bool MoveSongUp(SongData song)
        {  
            if (song == null) return false;
            int idx = items.IndexOf(song);
            if (idx == -1 || idx == 0) return false;
            items.Remove(song);
            selectedItems.Remove(song);
            song.IsSelected = false;
            items.Insert(idx - 1, song);
            song.IsSelected = true;
            selectedItems.Add(song);
            return true; 
        }
        public bool MoveSongDown(SongData song)
        {  
            if (song == null) return false;
            int idx = items.IndexOf(song);
            if (idx == -1 || idx == items.Count - 1) return false;
            items.Remove(song);
            selectedItems.Remove(song);
            song.IsSelected = false;
            items.Insert(idx + 1, song);
            song.IsSelected = true;
            selectedItems.Add(song);
            return true; 
        }
        public bool SavePlaylist()
        {
            return true;
        }
    }
}
