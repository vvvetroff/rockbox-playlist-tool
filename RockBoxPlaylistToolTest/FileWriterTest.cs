using RockBoxPlaylistTool.Data;
using RockBoxPlaylistTool.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistToolTest
{
    [TestClass]
    public class FileWriterTest
    {
        [TestMethod]
        public void SavePlaylistWithTwoSongsFromSampleDir()
        {
            var samplePath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\";
            var playlistPath =@"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\test1.m3u8";
            ObservableCollection<SongData> collection = SongCollectionBuilder.Build(samplePath);

            FileWriter fileWriter = new();
            fileWriter.SavePlaylist(playlistPath, collection);

            FileInfo file = new(playlistPath);
            Assert.IsTrue(file.Exists);
            file.Delete();
        }
        [TestMethod]
        public void OverwriteAlreadyExistingPlaylistThatHasRandomText()
        {
            var playlistPath =@"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\test2.m3u8";
            var samplePath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\";
            ObservableCollection<SongData> collection = SongCollectionBuilder.Build(samplePath);

            FileInfo file = new(playlistPath);
            using (StreamWriter w = new(playlistPath)) { w.WriteLine("blah"); }

            Assert.IsTrue(file.Exists);

            FileWriter fileWriter = new();
            fileWriter.SavePlaylist(playlistPath, collection);

            Assert.IsTrue(file.Exists);

            var strings = File.ReadLines(playlistPath).ToList();

            Assert.AreEqual(2, strings.Count);

            file.Delete();
        }
    }
}
