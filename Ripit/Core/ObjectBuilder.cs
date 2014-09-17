using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using be.trojkasoftware.Ripit.Attributes;

namespace be.trojkasoftware.Ripit.Core
{
	public class ObjectBuilder
	{
		ManualResetEvent allDone = new ManualResetEvent(false);
		string pageText = "";
		Stream responseStream = null;

		public IList<T> FillList<T>(IList<T> listToFill, Dictionary<String, String> paramList, Func<T> itemFactory ) where T: class
		{
			Dictionary<int, string> globalSources = GetSources(listToFill, paramList);

			Type objectType = listToFill.GetType();
			Attribute[] objectAttrs = (System.Attribute[])objectType.GetCustomAttributes (false);
			CollectionCaptureAttribute captureAttribute = objectAttrs.OfType<CollectionCaptureAttribute> ().SingleOrDefault ();
			if(captureAttribute == null)
			{
				return listToFill;
			}

			MatchCollection matches = Regex.Matches(globalSources[captureAttribute.Index], captureAttribute.CaptureExpression, RegexOptions.IgnoreCase);
			foreach (Match match in matches) {
				Dictionary<int, string> targetSources = new Dictionary<int, string>();
				targetSources.Add (0, match.Groups [0].Value);

				T objectToFill = itemFactory ();

				T filledObject = (T)FillFromSources(objectToFill, targetSources);

				listToFill.Add (filledObject);
			}

			return listToFill;
		}

		public object Fill(object objectToFill, Dictionary<String, String> paramList)
		{

			Dictionary<int, string> globalSources = GetSources(objectToFill, paramList);

			return FillFromSources(objectToFill, globalSources);
		}

		public object FillFeed(object feedToFill, Dictionary<String, String> paramList)
		{
			List<RSSItem> feedToFillAsRSSFeed = feedToFill as List<RSSItem>;
			Dictionary<int, string> globalSources = GetSourceUrls(feedToFill, paramList);

			HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create (globalSources[1]);
			IAsyncResult reqResult = myReq.BeginGetResponse (new AsyncCallback (FinishWebRequest), myReq);

			allDone.WaitOne ();
			allDone.Reset ();

			List<RSSItem> result = LoadFeed (responseStream);
			//responseStream.Dispose();

			feedToFillAsRSSFeed.AddRange (result);

			return feedToFill;
		}

		private List<RSSItem> LoadFeed(Stream url) 
		{
			// http://stackoverflow.com/questions/5889954/linq-to-xml-parse-rss-feed
			List<RSSItem> results = null;

			XNamespace ns = "http://purl.org/rss/1.0/";
			XDocument xdoc = XDocument.Load(url);
			results = (from feed in xdoc.Descendants(/*ns +*/ "item")
				//orderby int.Parse(feed.Element(/*ns +*/  "guid").Value) descending
				let desc = feed.Element(/*ns +*/  "description").Value
				select new RSSItem
				{
					Title = feed.Element(/*ns +*/  "title").Value,
					Description = desc,
					Link = feed.Element(/*ns +*/  "link").Value
				}).ToList();

			return results;
		}

		private Dictionary<int, string> GetSourceUrls(object objectToFill, Dictionary<String, String> paramList)
		{
			Dictionary<int, string> urlSources = new Dictionary<int, string> ();

			Type objectType = objectToFill.GetType();
			Attribute[] objectAttrs = (System.Attribute[])objectType.GetCustomAttributes (false);
			foreach (HttpSourceAttribute httpSource in objectAttrs.ToList().OfType<HttpSourceAttribute>()) {

				string mainUrl = httpSource.Url;
				int startOfParam = httpSource.Url.LastIndexOf ("?");
				if (startOfParam >= 0) {
					mainUrl = httpSource.Url.Substring (0, startOfParam);
					string paramString = httpSource.Url.Substring (startOfParam + 1);

					bool isFirsParam = true;
					foreach (string param in paramString.Split(new string[]{"&"}, StringSplitOptions.RemoveEmptyEntries)) {
						string paramName = param.Substring (0, param.IndexOf ("="));
						string paramValuePlaceHolder = param.Substring (param.IndexOf ("=") + 1);
						if (paramList.ContainsKey (paramValuePlaceHolder)) {
							mainUrl = mainUrl + (isFirsParam ? "?" : "&") + paramName + "=" + paramList [paramValuePlaceHolder];
						} else {
							mainUrl = mainUrl + (isFirsParam ? "?" : "&") + param;
						}

						isFirsParam = false;
					}
				}

				urlSources.Add (httpSource.Id, mainUrl);
			}

			return urlSources;
		}

		private Dictionary<int, string> GetSources(object objectToFill, Dictionary<String, String> paramList)
		{
			Dictionary<int, string> urlSources = GetSourceUrls(objectToFill, paramList);
			Dictionary<int, string> globalSources = new Dictionary<int, string> ();

			foreach (KeyValuePair<int, string> entry in urlSources) {

				HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create (entry.Value);
				IAsyncResult result = myReq.BeginGetResponse (new AsyncCallback (FinishWebRequest), myReq);

				allDone.WaitOne ();
				allDone.Reset ();

				using (var reader = new StreamReader (responseStream)) {
					pageText = reader.ReadToEnd ();
				}
				responseStream.Dispose ();

				globalSources.Add (entry.Key, pageText);
			}

			return globalSources;
		}

		private object FillFromSources(object objectToFill, Dictionary<int, string> globalSources)
		{
			Type objectType = objectToFill.GetType();
			foreach (PropertyInfo property in objectType.GetProperties ()) {
				pageText = "";

				Attribute[] propertyAttrs = (System.Attribute[])property.GetCustomAttributes(false);
				if (propertyAttrs.Length == 0)
					continue;

				List<Attribute> propertyAttrList = propertyAttrs.ToList ();

				SourceRefAttribute sourceRef = (SourceRefAttribute)propertyAttrList.OfType<SourceRefAttribute>().SingleOrDefault();
				if (sourceRef == null || !globalSources.ContainsKey(sourceRef.SourceRefId)) {
					throw new Exception ();
				}

				string sourceText = globalSources[sourceRef.SourceRefId];
				bool foundValue = true;
				foreach (Attribute textActionAttribute in propertyAttrList.OfType<TextActionInterface>().OrderBy(x => x.Index)) {
					if((textActionAttribute is PropertyCaptureAttribute) && foundValue)
					{
						PropertyCaptureAttribute capture = (PropertyCaptureAttribute)textActionAttribute;
						Match match = Regex.Match(sourceText, capture.CaptureExpression, RegexOptions.IgnoreCase);

						if (match.Success) {
							string key = match.Groups [capture.Group].Value;
							sourceText = key;
						} else if (capture.IsOptional) {
							foundValue = false;
						} else {
							throw new Exception ();
						}
					}
				}

				if (!foundValue) {
					DefaultValueAttribute defaultValue = (DefaultValueAttribute)propertyAttrList.OfType<DefaultValueAttribute>().SingleOrDefault();
					if (defaultValue != null) {
						sourceText = defaultValue.Value;
					}
				}

				property.SetValue (objectToFill, sourceText, null);
			}

			return objectToFill;
		}

		private void FinishWebRequest(IAsyncResult result)
		{
			HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
			responseStream = response.GetResponseStream ();

			allDone.Set();

		}
	}


}

