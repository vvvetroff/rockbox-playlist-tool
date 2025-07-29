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
        private SongData selected;
        public PlaylistViewModel()
        { 
            items = new ObservableCollection<SongData>();
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
        public SongData Selected 
        { 
            get { return selected; } 
            set { SetProperty(ref selected, value); } 
        }
        public bool AppendSong(SongData song)
        {
            if (items == null) return false;
            items.Add(song);
            return true;
        }
        public bool RemoveSong(SongData song)
        {
            return false;
        }
        public bool MoveSongUp(SongData song)
        {  
            return false; 
        }
        public bool MoveSongDown(SongData song)
        {  
            return false; 
        }
        public bool SavePlaylist()
        {
            return true;
        }
    }
}
