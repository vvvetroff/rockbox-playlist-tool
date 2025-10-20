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
            if (string.IsNullOrEmpty(path) || items == null || items.Count == 0 || path.Contains("/.m3u8")) { return false; }

            try
            {
                FileInfo fileInfo = new(path);
                if (fileInfo.Exists)
                {
                    System.IO.File.Delete(fileInfo.FullName);
                }
                using (FileStream writeStream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter writer = new StreamWriter(writeStream))
                    {
                        foreach (SongData item in items)
                        {
                            var rPath = Helper.WinToRockPath(item.Path);
                            writer.WriteLine(rPath);
                        }
                    }
                }
            }
            catch { return false; }
            return true;
        }
    }
}
