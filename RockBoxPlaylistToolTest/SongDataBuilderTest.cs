using RockBoxPlaylistTool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistToolTest
{
    [TestClass]
    public sealed class SongDataBuilderTest
    {
        [TestMethod]
        public void BuildMp3()
        {
            var filepath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\MP3File.mp3";
            var song = SongDataBuilder.Build(filepath);

            Assert.AreEqual("Trinity", song.Title);
            Assert.AreEqual("009 Sound System", song.Artist);
            Assert.AreEqual("009 Sound System", song.Album);
            Assert.AreEqual(filepath, song.Path);
        }
        [TestMethod]
        public void BuildFlac()
        {
            var filepath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\FLACFile.flac";
            var song = SongDataBuilder.Build(filepath);

            Assert.AreEqual("California Love (original version)", song.Title);
            Assert.AreEqual("2Pac", song.Artist);
            Assert.AreEqual("Greatest Hits", song.Album);
            Assert.AreEqual(filepath, song.Path);
        }
        
        [TestMethod]
        public void BuildNull()
        {
            var song = SongDataBuilder.Build(null);

            Assert.IsNull(song);
        }
        [TestMethod]
        public void BuildEmptyPath()
        {
            var filepath = "";
            var song = SongDataBuilder.Build(filepath);

            Assert.IsNull(song);
        }
        [TestMethod]
        public void BuildInvalid()
        {
            var filepath = "blahblahblah";
            var song = SongDataBuilder.Build(filepath);

            Assert.IsNull(song);
        }
    }
}
