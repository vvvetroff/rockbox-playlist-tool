using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool
{
    public class SongCollectionBuilder
    {
        public static ObservableCollection<SongData> Build(string path)
        {
            ObservableCollection<SongData> collection = [];
            if (string.IsNullOrEmpty(path)) { return collection; }

            // TODO: Change this???
            // I feel like if you were to open ONLY a music file (not a dir of music files),
            // then the ObservableCollection should contain just the one file.
            DirectoryInfo dir = new(path);
            if (!dir.Exists) { return collection; }

            foreach (FileInfo file in dir.GetFiles()) {
                collection.Add(SongDataBuilder.Build(file.FullName));
            }
            return collection;
        }
    }
}

