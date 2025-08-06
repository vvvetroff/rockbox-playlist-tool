using RockBoxPlaylistTool.Data;
using RockBoxPlaylistTool.Main;
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


namespace RockBoxPlaylistTool.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string musicDir;
        //private string playlistsDir;
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(null);
            DataContext = mainViewModel;

            // HACK: remove later
            //for (int i = 0; i < 15; i++)
            //{
            //    var song = new SongData() { Album = i.ToString(), Artist = i.ToString(), Title = i.ToString(), Path = i.ToString() };
            //    mainViewModel.MusicViewModel.Items.Add(song);
            //}

            //musicDir = ConfigurationManager.AppSettings[FolderNames.MusicDir];
            //playlistsDir = ConfigurationManager.AppSettings[FolderNames.PlaylistsDir];
            //txtMusic.Text = musicDir;
            //btnLoadMusic.PreviewMouseLeftButtonUp += BtnLoadMusic_PreviewMouseLeftButtonUp;
        }

        //private void BtnLoadMusic_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    var dataModel = new DataModel();
        //    txtMusic.Text = dataModel.GetMusicFolder(musicDir);
        //}
    }
}