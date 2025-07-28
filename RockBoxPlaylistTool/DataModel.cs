using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms = System.Windows.Forms;

namespace RockBoxPlaylistTool
{
    internal class DataModel
    {
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        public string GetMusicFolder(string currentMusicFolder)
        {
            folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = currentMusicFolder;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

            var result = folderBrowserDialog.ShowDialog();
            if (result == WinForms.DialogResult.OK || result == WinForms.DialogResult.Yes)
            {
                ConfigurationManager.AppSettings[FolderNames.MusicDir] = folderBrowserDialog.SelectedPath;
            }
            return folderBrowserDialog.SelectedPath;
        }
    }
}
