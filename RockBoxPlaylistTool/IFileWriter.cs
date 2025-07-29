using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool
{
    public interface IFileWriter
    {
        bool SavePlaylist(string path, ObservableCollection<SongData> items);
        bool Dummy(string bruh);
    }
}
