using System;
using System.IO;

namespace be.trojkasoftware.monoCPVanity.Util
{
	public partial class FileStorageService
	{
        //public string BaseFolder {
        //    get {
        //        return Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
        //    }
        //}

		public void WriteBytes(byte[] data, string filename)
		{
			var filePath = Path.Combine (BaseFolder, filename);
            FileStorageService.WriteAllBytes(filePath, data);
		}

		public byte[] ReadBytes(string filename)
		{
			var filePath = Path.Combine (BaseFolder, filename);
            return FileStorageService.ReadAllBytes(filePath);
		}

		public void DeleteFile(string filename)
		{
			var filePath = Path.Combine (BaseFolder, filename);
			File.Delete (filePath);
		}

		public bool FileExists(string filename)
		{
			var filePath = Path.Combine (BaseFolder, filename);
			return File.Exists (filePath);
		}

  		public static void WriteAllBytes (string path, byte [] bytes) 
 		{ 
 			using (Stream stream = File.Create (path)) { 
 				stream.Write (bytes, 0, bytes.Length); 
 			} 
 		} 

        public static byte [] ReadAllBytes (string path) 
 		{ 
 			using (FileStream s = File.OpenRead (path)) { 
 				long size = s.Length; 
 				// limited to 2GB according to http://msdn.microsoft.com/en-us/library/system.io.file.readallbytes.aspx 
 				if (size > Int32.MaxValue) 
 					throw new IOException ("Reading more than 2GB with this call is not supported"); 
 
 
 				int pos = 0; 
 				int count = (int) size; 
 				byte [] result = new byte [size]; 
 				while (count > 0) { 
					int n = s.Read (result, pos, count); 
 					if (n == 0) 
 						throw new IOException ("Unexpected end of stream"); 
 					pos += n; 
					count -= n; 
 				} 
 				return result; 
 			} 
 		} 

	}
}

