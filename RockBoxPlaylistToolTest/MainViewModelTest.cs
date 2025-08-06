using NSubstitute;
using RockBoxPlaylistTool.Data;
using RockBoxPlaylistTool.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace RockBoxPlaylistToolTest
{
    [TestClass]
    public sealed class MainViewModelTest
    {
        [TestMethod]
        public void CannotDoAnythingWithEmptyPlaylist()
        {
            IFileWriter fileWriter = Substitute.For<IFileWriter>();
            var mainViewModel = new MainViewModel(fileWriter);
            Assert.IsFalse(mainViewModel.AddToPlaylistCommand.CanExecute(null));
            Assert.IsFalse(mainViewModel.RemoveFromPlaylistCommand.CanExecute(null));
            Assert.IsFalse(mainViewModel.MoveSongUpCommand.CanExecute(null));
            Assert.IsFalse(mainViewModel.MoveSongDownCommand.CanExecute(null));
            Assert.IsFalse(mainViewModel.SavePlaylistCommand.CanExecute(null));
        }
        [TestMethod]
        public void SavePlaylistWithOneSong()
        {
            IFileWriter fileWriter = Substitute.For<IFileWriter>();
            var mainViewModel = new MainViewModel(fileWriter);

            mainViewModel.PlaylistViewModel.Path = "blah";

            SongData song = new();
            mainViewModel.PlaylistViewModel.AppendSong(song);
            Assert.IsTrue(mainViewModel.SavePlaylistCommand.CanExecute(null));

            mainViewModel.SavePlaylistCommand.Execute(null);
            fileWriter.Received().SavePlaylist("blah", mainViewModel.PlaylistViewModel.Items);
        }
        [TestMethod]
        public void SavePlaylistWithParticularSong()
        {
            IFileWriter fileWriter = Substitute.For<IFileWriter>();
            var mainViewModel = new MainViewModel(fileWriter);

            string path = null;
            ObservableCollection<SongData> items = null;

            fileWriter.When(
                x => x.SavePlaylist(
                    Arg.Do<string>(xx => path = xx),
                    Arg.Do<ObservableCollection<SongData>>(yy => items = yy)
                    )
                ).Do(x => { });

            mainViewModel.PlaylistViewModel.Path = "blah";

            SongData song = new() { Title = "test1", Artist = "test2", Album = "test3", Path = "test4" };
            mainViewModel.PlaylistViewModel.AppendSong(song);
            mainViewModel.SavePlaylistCommand.Execute(null);

            Assert.AreEqual("blah", path);
            Assert.IsNotNull(items);
            Assert.AreEqual(1, items.Count);

            var tempSong = items[0];
            Assert.AreEqual("test1", tempSong.Title);
            Assert.AreEqual("test2", tempSong.Artist);
            Assert.AreEqual("test3", tempSong.Album);
            Assert.AreEqual("test4", tempSong.Path);
        }

        [TestMethod]
        public void TestDummy()
        {
            IFileWriter fileWriter = Substitute.For<IFileWriter>();
            string path = null;
            fileWriter.When(
                x => x.Dummy(Arg.Do<string>(xx => path = xx))
                ).Do(x => { });

            fileWriter.Dummy("blah");
            Assert.AreEqual("blah", path);
        }
    }
}
