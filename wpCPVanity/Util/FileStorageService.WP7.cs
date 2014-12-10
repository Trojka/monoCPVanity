using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace be.trojkasoftware.monoCPVanity.Util
{
    public partial class FileStorageService
    {
        public string BaseFolder
        {
            get
            {
                return "";
            }
        }

        public void WriteBytes(byte[] data, string filename)
        {
        }

        public byte[] ReadBytes(string filename)
        {
            return null;
        }

        public void DeleteFile(string filename)
        {
        }

        public bool FileExists(string filename)
        {
            return false;
        }

        public static void WriteAllBytes(string path, byte[] bytes)
        {
        }

        public static byte[] ReadAllBytes(string path)
        {
            return null;
        }
    }
}
