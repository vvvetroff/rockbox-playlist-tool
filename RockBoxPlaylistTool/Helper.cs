using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool
{
    public class Helper
    {
        public static string WinToRockPath(string path)
        {
            if (string.IsNullOrEmpty(path)) { return path; }
            string newPath = path;
            newPath = newPath.Replace(@"\", "/");
            newPath = newPath[2..];
            return newPath;

        }
        public static string RockToWinPath(string path)
        {
            // TODO: Make this a global somehow, through configuration
            char driveLetter = 'D';

            if (string.IsNullOrEmpty(path)) { return path; }
            string newPath = path;
            newPath = newPath.Replace("/", @"\");
            newPath = newPath.Insert(0, driveLetter + ":");
            return newPath;
        }
    }
}
