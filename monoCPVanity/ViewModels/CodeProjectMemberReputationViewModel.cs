﻿using System;
using System.Collections.Generic;
//using be.trojkasoftware.Ripit.Core;
using System.Threading.Tasks;
using be.trojkasoftware.monoCPVanity.Data;
using be.trojkasoftware.monoCPVanity.Util;
using System.Threading;
using System.IO;

namespace be.trojkasoftware.portableCPVanity.ViewModels
{
	public delegate void ReputationGraphLoaded(byte[] graph);

	public partial class CodeProjectMemberReputationViewModel
	{
		public ReputationGraphLoaded ReputationGraphLoaded;

		public string MemberReputationGraphUrl {
			get;
			set;
		}

		public void LoadMemberReputation(TaskScheduler uiContext) {
			WebImageRetriever imageDownloader = new WebImageRetriever ();

			Task<byte[]> loadGraphTask = imageDownloader.GetImageStreamAsync (new Uri (MemberReputationGraphUrl));

			loadGraphTask.ContinueWith (t => ReputationGraphLoaded(t.Result), uiContext);
		}
	}
}

