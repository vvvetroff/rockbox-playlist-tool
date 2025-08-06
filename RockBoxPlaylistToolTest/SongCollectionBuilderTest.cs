using RockBoxPlaylistTool.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistToolTest
{
    [TestClass]
    public sealed class SongCollectionBuilderTest
    {
        [TestMethod]
        public void BuildFromDir()
        {
            var dirPath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\";
            ObservableCollection<SongData> collection = SongCollectionBuilder.Build(dirPath);
            Assert.AreEqual(2, collection.Count);
        }
        [TestMethod]
        public void BuildFromNull()
        {
            ObservableCollection<SongData> collection = SongCollectionBuilder.Build(null);
            Assert.AreEqual(0, collection.Count);
        }
        [TestMethod]
        public void BuildFromEmpty()
        {
            var dirPath = "";
            ObservableCollection<SongData> collection = SongCollectionBuilder.Build(dirPath);
            Assert.AreEqual(0, collection.Count);
        }
        [TestMethod]
        public void BuildFromInvalid()
        {
            var invalidDirPath = @"C:\Users\creat\Documents\Projects\RockBoxPlaylistTool\RockBoxPlaylistTool\Sample\FLACFile.flac";
            ObservableCollection<SongData> collection = SongCollectionBuilder.Build(invalidDirPath);
            Assert.AreEqual(0, collection.Count);
        }
    }
}
