using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool.Data
{
    public class SongDataBuilder
    {
        public static SongData Build(string path)
        {
            if (string.IsNullOrEmpty(path)) { return null; }

            var file = new FileInfo(path);
            if (!file.Exists) { return null; }

            TagLib.File tfile = null;
            try
            {
                tfile = TagLib.File.Create(path);
            }
            catch (Exception ex)
            {
                return null;
            }
            var tag = tfile.Tag; // alias
            var song = new SongData() { Path = path };
            if (tag != null)
            {
                song.Title = tag.Title;
                song.Album = tag.Album;
                song.Artist = tag.Performers.FirstOrDefault();
            }
            return song;
        }
    }
}
