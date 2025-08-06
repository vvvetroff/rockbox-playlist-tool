using RockBoxPlaylistTool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistToolTest
{
    [TestClass]
    public sealed class HelperTest
    {
        [TestMethod]
        public void WinToRockPathValid()
        {
            string path = @"D:\Music\Phil Collins - Face Value\06 - Hand In Hand.mp3";
            string newPath = Helper.WinToRockPath(path);
            Assert.AreEqual("/Music/Phil Collins - Face Value/06 - Hand In Hand.mp3", newPath);
        }
        [TestMethod]
        public void WinToRockPathNull()
        {
            var newPath = Helper.WinToRockPath(null);
            Assert.IsNull(newPath);
        }
        [TestMethod]
        public void WinToRockPathEmpty()
        {
            string path = "";
            string newPath = Helper.WinToRockPath(path);
            Assert.AreEqual("", newPath);
        }
        [TestMethod]
        public void RockToWinPathValid()
        {
            string path = @"/Music/Phil Collins - Face Value/06 - Hand In Hand.mp3";
            string newPath = Helper.RockToWinPath(path);
            Assert.AreEqual(@"D:\Music\Phil Collins - Face Value\06 - Hand In Hand.mp3", newPath);
        }
        [TestMethod]
        public void RockToWinPathNull()
        {
            var newPath = Helper.RockToWinPath(null);
            Assert.IsNull(newPath);
        }
        [TestMethod]
        public void RockToWinPathEmpty()
        {
            string path = "";
            string newPath = Helper.RockToWinPath(path);
            Assert.AreEqual("", newPath);
        }
    }
}
