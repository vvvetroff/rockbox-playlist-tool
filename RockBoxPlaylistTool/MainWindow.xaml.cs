using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace RockBoxPlaylistTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string musicDir;
        private string playlistsDir;
        
        public MainWindow()
        {
            InitializeComponent();
            musicDir = ConfigurationManager.AppSettings[FolderNames.MusicDir];
            playlistsDir = ConfigurationManager.AppSettings[FolderNames.PlaylistsDir];
            txtMusic.Text = musicDir;
            btnLoadMusic.PreviewMouseLeftButtonUp += BtnLoadMusic_PreviewMouseLeftButtonUp;
        }

        private void BtnLoadMusic_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dataModel = new DataModel();
            txtMusic.Text = dataModel.GetMusicFolder(musicDir);
        }
    }
}