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
using System.IO.IsolatedStorage;
using System.IO;

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
            using (IsolatedStorageFile userIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(filename + ".bin", FileMode.Create, userIsolatedStorage))
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        writer.Write(data);
                    }
                }
            }
        }

        public byte[] ReadBytes(string filename)
        {
            byte[] buffer = null;
            using (IsolatedStorageFile userIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(filename + ".bin", FileMode.Open, userIsolatedStorage))
                {
                    buffer = new byte[fileStream.Length];
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        int numRead = reader.Read(buffer, 0, buffer.Length);
                    }
                }
            }
            return buffer;
        }

        public void DeleteFile(string filename)
        {
            IsolatedStorageFile userIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            userIsolatedStorage.DeleteFile(filename + ".bin");
        }

        public bool FileExists(string filename)
        {
            IsolatedStorageFile userIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();
            return userIsolatedStorage.FileExists(filename + ".bin");
        }
    }
}
