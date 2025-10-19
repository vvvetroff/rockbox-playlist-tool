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

            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) { return null; }

            TagLib.File file;
            try
            {
                file = TagLib.File.Create(path);
            }
            catch (TagLib.UnsupportedFormatException)
            {
                return null;
            }
            if (file.Properties.MediaTypes != TagLib.MediaTypes.Audio) { return null; }

            var tag = file.Tag; // alias
            var songData = new SongData() { Path = path };
            if (tag != null)
            {
                songData.Title = tag.Title;
                songData.Album = tag.Album;
                songData.Artist = tag.Performers.FirstOrDefault();
            }
            return songData;
        }
    }
}
