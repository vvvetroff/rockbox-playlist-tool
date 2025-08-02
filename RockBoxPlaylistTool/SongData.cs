using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool
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
        public string AlbumFormatted
        {
            get { 
                if (!String.IsNullOrEmpty(fileAlbum) && fileAlbum.Length > 10)
                {
                    return fileAlbum.Substring(0,10) + "...";
                }
                return fileAlbum;
            }
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
            Console.WriteLine("Title: " + this.Title);
            Console.WriteLine("Artist: " + this.Artist);
            Console.WriteLine("Album: " + this.Album);
            Console.WriteLine("Path: " + this.Path);
        }
        public SongData Clone()
        {
            var song = new SongData
            {
                Title = this.Title,
                Artist = this.Artist,
                Album = this.Album,
                Path = this.Path
            };
            return song;
        }
    }
}
