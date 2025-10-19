using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool.Data
{
    public class SongCollectionBuilder
    {
        public static ObservableCollection<SongData> Build(string path)
        {
            ObservableCollection<SongData> collection = [];
            if (string.IsNullOrEmpty(path)) { return collection; }

            DirectoryInfo dir = new(path);
            if (!dir.Exists) { return collection; }

            foreach (FileInfo file in dir.GetFiles()) {
                var item = SongDataBuilder.Build(file.FullName);
                if (item != null) { 
                    collection.Add(item);
                }
            }
            return collection;
        }
        public static ObservableCollection<SongData> BuildFromFile(string path)
        {
            ObservableCollection<SongData> collection = [];
            if (string.IsNullOrEmpty(path)) { return collection; }

            try
            {
                FileInfo fileInfo = new FileInfo(path);
                using (FileStream readStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (StreamReader reader = new StreamReader(readStream))
                    {
                        string file;
                        while (reader.Peek() >= 0) {
                            file = Helper.RockToWinPath(reader.ReadLine());
                            var songData = SongDataBuilder.Build(file);
                            if (songData != null)
                            {
                                collection.Add(SongDataBuilder.Build(file));
                            }
                        }
                    }
                }
            }
            catch { return []; }

            return collection;
        }
    }
}

