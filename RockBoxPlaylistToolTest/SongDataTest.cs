using RockBoxPlaylistTool;
using RockBoxPlaylistTool.Data;

namespace RockBoxPlaylistToolTest
{
    [TestClass]
    public sealed class SongDataTest
    {
        [TestMethod]
        public void CheckFileMp3Tags()
        {
            var filepath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\MP3File.mp3";
            var tfile = TagLib.File.Create(filepath);
            var song = new SongData();
            var tag = tfile.Tag;
            if (tag != null)
            {
                song.Title = tag.Title;
                song.Album = tag.Album;
                song.Artist = tag.Performers.FirstOrDefault();
            }
            Assert.AreEqual("Trinity", song.Title);
            Assert.AreEqual("009 Sound System", song.Artist);
            Assert.AreEqual("009 Sound System", song.Album);
        }

        [TestMethod]
        public void CheckFileFlacTags()
        {
            var filepath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\FLACFile.flac";
            var tfile = TagLib.File.Create(filepath);
            var song = new SongData();
            var tag = tfile.Tag;
            if (tag != null)
            {
                song.Title = tag.Title;
                song.Album = tag.Album;
                song.Artist = tag.Performers.FirstOrDefault();
            }
            Assert.AreEqual("California Love (original version)", song.Title);
            Assert.AreEqual("2Pac", song.Artist);
            Assert.AreEqual("Greatest Hits", song.Album);
        }
        [TestMethod]
        public void OpenAsFile()
        {
            var filepath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\MP3File.mp3";
            var file = new FileInfo(filepath);
            Assert.IsNotNull(file);
            Assert.IsTrue(file.Exists);
        }
    }
}
