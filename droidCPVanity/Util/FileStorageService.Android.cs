using System;
using System.IO;

namespace be.trojkasoftware.monoCPVanity.Util
{
	public partial class FileStorageService
	{
		public string BaseFolder {
		    get {
		        return Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
		    }
		}

	}
}