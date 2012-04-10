using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using VBFileIO = Microsoft.VisualBasic.FileIO;

namespace DelCurBackground
{
    class Deleter
    {
        private static String getCurWallpaperPath()
        {
            // NOTE: Only works for Windows 7!
            // Get the path of the current wallpaper
            var wpReg = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\Desktop\\General\\", false);
            var wallpaperPath = wpReg.GetValue("WallpaperSource").ToString();
            wpReg.Close();
            return wallpaperPath;
        }

        private static void advanceBackgroundSlider()
        {
            //Display the Desktop via the COM component Shell32.dll
            Shell32.Shell objShell = new Shell32.Shell();
            objShell.ToggleDesktop();
            // Simulate Ctrl + Space to deselect anything that may be selected
            SendKeys.SendWait("^( )");
            // Simulate pressing Shift + F10 to open Desktop context menu
            SendKeys.SendWait("+{F10}");
            // Simulate pressing N to execute the “Next desktop background” command
            SendKeys.SendWait("{N}");
        }

        private static void sendToRecycleBin(String path)
        {
            VBFileIO.FileSystem.DeleteFile(path, VBFileIO.UIOption.OnlyErrorDialogs, VBFileIO.RecycleOption.SendToRecycleBin);
        }

        static void Main(string[] args)
        {
            String wallPaperPath = getCurWallpaperPath();

            advanceBackgroundSlider();

            sendToRecycleBin(wallPaperPath);
        }
    }
}
