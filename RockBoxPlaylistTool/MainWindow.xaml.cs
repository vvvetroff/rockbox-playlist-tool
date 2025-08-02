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
        //private string musicDir;
        //private string playlistsDir;
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(null);
            DataContext = mainViewModel;

            // HACK: remove later
            var song1 = new SongData() { Album = "test1", Artist = "test2", Title = "Test", Path = "blah" };
            var song2 = new SongData() { Album = "this should clip under the screen", Artist = "lenghty artist", Title = "jkdksdfhasdf", Path = "blah" };
            var song3 = new SongData() { Album = "oop", Artist = "hee", Title = "hee", Path = "blah" };
            var song4 = new SongData() { Album = "jkfhsdkjfa", Artist = "kajshfsfj", Title = "akjshfg", Path = "blah" };
            mainViewModel.MusicViewModel.Items.Add(song1);
            mainViewModel.MusicViewModel.Items.Add(song2);
            mainViewModel.MusicViewModel.Items.Add(song3);
            mainViewModel.MusicViewModel.Items.Add(song3);
            mainViewModel.MusicViewModel.Items.Add(song3);
            mainViewModel.MusicViewModel.Items.Add(song3);
            mainViewModel.MusicViewModel.Items.Add(song3);
            mainViewModel.MusicViewModel.Items.Add(song3);
            mainViewModel.MusicViewModel.Items.Add(song3);
            mainViewModel.MusicViewModel.Items.Add(song4);
            mainViewModel.MusicViewModel.Items.Add(song4);
            mainViewModel.MusicViewModel.Items.Add(song4);
            mainViewModel.MusicViewModel.Items.Add(song4);
            mainViewModel.MusicViewModel.Items.Add(song4);
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