using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool.Data
{
    public class SongData : BindableBase
    {
        private string fileArtist;
        private string fileAlbum;
        private string fileTitle;
        private string filePath;
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }
        public string Artist
        {
            get { return fileArtist; }
            set { SetProperty(ref fileArtist, value); }
        }
        public string Album
        {
            get { return fileAlbum; }
            set { SetProperty(ref fileAlbum, value); }
        }
        public string Title
        {
            get { return fileTitle; }
            set { SetProperty(ref fileTitle, value); }
        }
        public string Path
        {
            get { return filePath; }
            set { SetProperty(ref filePath, value); }
        }
        public void Print()
        {
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Artist: " + Artist);
            Console.WriteLine("Album: " + Album);
            Console.WriteLine("Path: " + Path);
        }
        public SongData Clone()
        {
            var song = new SongData
            {
                Title = Title,
                Artist = Artist,
                Album = Album,
                Path = Path
            };
            return song;
        }
    }
}
