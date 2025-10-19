using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockBoxPlaylistTool.Data
{
    public class Helper
    {
        /* To convert the path of a given file to its corresponding Rockbox path in a m3u8 file */
        public static string WinToRockPath(string path)
        {
            if (string.IsNullOrEmpty(path)) { return path; }
            string newPath = path;
            newPath = newPath.Replace(@"\", "/");
            newPath = newPath[2..];
            return newPath;

        }
        /* To convert the paths within a m3u8 file to the corresponding Windows path */
        public static string RockToWinPath(string path)
        {
            // TODO: Make this a global somehow, through configuration
            char driveLetter = 'C';

            if (string.IsNullOrEmpty(path)) { return path; }
            string newPath = path;
            newPath = newPath.Replace("/", @"\");
            newPath = newPath.Insert(0, driveLetter + ":");
            return newPath;
        }
    }
}
