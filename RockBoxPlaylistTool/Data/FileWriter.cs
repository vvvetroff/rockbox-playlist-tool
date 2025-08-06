using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool.Data
{
    public class FileWriter : IFileWriter
    {
        public bool SavePlaylist(string path, ObservableCollection<SongData> items)
        {
            if (string.IsNullOrEmpty(path) || items == null) { return false; }

            //FileStream writeStream = null;
            //StreamWriter writer = null;

            FileInfo fileInfo = new(path);
            try
            {
                using FileStream writeStream = new(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write);
                using StreamWriter writer = new(writeStream);
                foreach (SongData item in items)
                {
                    var rPath = Helper.WinToRockPath(item.Path);
                    writer.WriteLine(rPath);
                }
            }
            catch { return false; }
            return true;
        }
    }
}
