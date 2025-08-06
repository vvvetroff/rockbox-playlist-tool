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

            // HACK: I think this is unnecessary; the dialog looks at dir.
            //var oneFile = new FileInfo(path);
            //if (oneFile.Exists)
            //{
            //    var song = SongDataBuilder.Build(oneFile.FullName);
            //    if (song != null) { collection.Add(song); }
            //}
            
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
    }
}

