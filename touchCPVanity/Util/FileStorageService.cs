using System;
using System.IO;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace touchCPVanity.Util
{
	public class FileStorageService
	{
		public string BaseFolder {
			get {
				return Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			}
		}

		public void WriteBytes(byte[] data, string filename)
		{
			var filePath = Path.Combine (BaseFolder, filename);
			File.WriteAllBytes (filePath, data);
		}

		public byte[] ReadBytes(string filename)
		{
			var filePath = Path.Combine (BaseFolder, filename);
			return File.ReadAllBytes (filePath);
		}

		public bool FileExists(string filename)
		{
			var filePath = Path.Combine (BaseFolder, filename);
			return File.Exists (filePath);
		}
	}
}

