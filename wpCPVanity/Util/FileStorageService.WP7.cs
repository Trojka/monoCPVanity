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
                    //fileStream.Write(data, 0, data.Length);
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
                    //long size = fileStream.Length;
                    //// limited to 2GB according to http://msdn.microsoft.com/en-us/library/system.io.file.readallbytes.aspx 
                    //if (size > Int32.MaxValue)
                    //    throw new IOException("Reading more than 2GB with this call is not supported");


                    //int pos = 0;
                    //int count = (int)size;
                    //byte[] result = new byte[size];
                    //while (count > 0)
                    //{
                    //    int n = fileStream.Read(result, pos, count);
                    //    if (n == 0)
                    //        throw new IOException("Unexpected end of stream");
                    //    pos += n;
                    //    count -= n;
                    //}
                    //return result;
                    using (BinaryReader reader = new BinaryReader(fileStream))
                    {
                        int numRead = reader.Read(buffer, 0, buffer.Length);
                        //Debug.Assert (numRead == 10);
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
            //return false;
        }

        //public static void WriteAllBytes(string path, byte[] bytes)
        //{
        //}

        //public static byte[] ReadAllBytes(string path)
        //{
        //    return null;
        //}
    }
}
