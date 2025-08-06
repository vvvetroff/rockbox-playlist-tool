using RockBoxPlaylistTool.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WinForms = System.Windows.Forms;

namespace RockBoxPlaylistTool.Music
{
    public class MusicViewModel : BindableBase
    {
        private string path;
        private ObservableCollection<SongData> items;
        private SongData selected;
        private DelegateCommand browseCommand;
        public MusicViewModel()
        {
            path = ConfigurationManager.AppSettings[FolderNames.MusicDir];
            browseCommand = new DelegateCommand(browseExecute);
            items = SongCollectionBuilder.Build(path);
        }
        public string Path 
        { 
            get { return path; } 
            set { SetProperty(ref path, value); } 
        }
        public ObservableCollection<SongData> Items 
        { 
            get { return items; } 
            set { SetProperty(ref items, value); } 
        }
        public SongData Selected 
        { 
            get { return selected; } 
            set { SetProperty(ref selected, value); } 
        }
        public ICommand BrowseCommand {
            get { return browseCommand; }
        }
        private void browseExecute()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = path;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            var result = folderBrowserDialog.ShowDialog();
            if (result == WinForms.DialogResult.OK || result == WinForms.DialogResult.Yes)
            {
                ConfigurationManager.AppSettings[FolderNames.MusicDir] = folderBrowserDialog.SelectedPath;
            }
            path = folderBrowserDialog.SelectedPath;
            items.Clear();
            items.AddRange(SongCollectionBuilder.Build(path));
        }
    }
}
