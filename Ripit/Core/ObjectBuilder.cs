using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using be.trojkasoftware.Ripit.Attributes;

namespace be.trojkasoftware.Ripit.Core
{
	public class ObjectBuilder
	{
		ManualResetEvent allDone = new ManualResetEvent(false);
		string pageText = "";
		Stream responseStream = null;

		public Task<IList<T>> FillListAsync<T>(IList<T> listToFill, Dictionary<String, String> paramList, Func<T> itemFactory, CancellationToken ct) where T: class
		{
			Task<IList<T>> returnValue = new Task<IList<T>> (x => FillList (listToFill, paramList, itemFactory, ct), ct);
			return returnValue;
		}

		public IList<T> FillList<T>(IList<T> listToFill, Dictionary<String, String> paramList, Func<T> itemFactory, CancellationToken ct) where T: class
		{
			Dictionary<int, string> globalSources = GetSources(listToFill, paramList, CancellationToken.None);

			Type objectType = listToFill.GetType();
			Attribute[] objectAttrs = (System.Attribute[])objectType.GetCustomAttributes (false);
			CollectionCaptureAttribute captureAttribute = objectAttrs.OfType<CollectionCaptureAttribute> ().SingleOrDefault ();
			if(captureAttribute == null)
			{
				return listToFill;
			}

			MatchCollection matches = Regex.Matches(globalSources[0], captureAttribute.CaptureExpression, RegexOptions.IgnoreCase);
			foreach (Match match in matches) {

				if (ct != CancellationToken.None && ct.IsCancellationRequested) 
				{
					return null;
				}

				Dictionary<int, string> targetSources = new Dictionary<int, string>();
				targetSources.Add (0, match.Groups [0].Value);

				T objectToFill = itemFactory ();

				T filledObject = (T)FillFromSources(objectToFill, targetSources, ct);

				listToFill.Add (filledObject);
			}

			return listToFill;
		}

		public Task<object> FillAsync(object objectToFill, Dictionary<String, String> paramList, CancellationToken ct) 
		{
			Task<object> returnValue = new Task<object> (x => Fill (objectToFill, paramList, ct), ct);
			return returnValue;
		}

		public object Fill(object objectToFill, Dictionary<String, String> paramList)
		{
			return Fill (objectToFill, paramList, CancellationToken.None);
		}

		public object Fill(object objectToFill, Dictionary<String, String> paramList, CancellationToken ct)
		{

			Dictionary<int, string> globalSources = GetSources(objectToFill, paramList, ct);
			if (ct == CancellationToken.None && globalSources == null)
				return null;

			return FillFromSources(objectToFill, globalSources, ct);
		}

		public Task<IList<RSSItem>> FillFeedAsync(IList<RSSItem> feedToFill, Dictionary<String, String> paramList, CancellationToken ct)
		{
			Task<IList<RSSItem>> returnValue = new Task<IList<RSSItem>> (x => FillFeed (feedToFill, paramList, ct), ct);
			return returnValue;
		}

		public IList<RSSItem> FillFeed(IList<RSSItem> feedToFill, Dictionary<String, String> paramList, CancellationToken ct)
		{
			Dictionary<int, string> globalSources = GetSourceUrls(feedToFill, paramList, ct);

			HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create (globalSources[1]);
			myReq.BeginGetResponse (new AsyncCallback (FinishWebRequest), myReq);

			allDone.WaitOne ();
			allDone.Reset ();

			List<RSSItem> result = LoadFeed (responseStream);

			foreach(var item in result)
				feedToFill.Add (item);

			return feedToFill;
		}

		private List<RSSItem> LoadFeed(Stream url) 
		{
			// http://stackoverflow.com/questions/5889954/linq-to-xml-parse-rss-feed
			List<RSSItem> results = null;

			XDocument xdoc = XDocument.Load(url);
			results = (from feed in xdoc.Descendants("item")
				let desc = feed.Element("description").Value
				select new RSSItem
				{
					Title = feed.Element("title").Value,
					Author = feed.Element("author").Value,
					Description = desc,
					Link = feed.Element("link").Value
				}).ToList();

			return results;
		}

		private Dictionary<int, string> GetSourceUrls(object objectToFill, Dictionary<String, String> paramList, CancellationToken ct)
		{
			Dictionary<int, string> urlSources = new Dictionary<int, string> ();

			Type objectType = objectToFill.GetType();
			Attribute[] objectAttrs = (System.Attribute[])objectType.GetCustomAttributes (false);
			foreach (HttpSourceAttribute httpSource in objectAttrs.ToList().OfType<HttpSourceAttribute>()) {

				if (ct != CancellationToken.None && ct.IsCancellationRequested) 
				{
					return null;
				}

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

		private Dictionary<int, string> GetSources(object objectToFill, Dictionary<String, String> paramList, CancellationToken ct)
		{
			Dictionary<int, string> urlSources = GetSourceUrls(objectToFill, paramList, ct);
			if(urlSources == null) 
			{
			}
			Dictionary<int, string> globalSources = new Dictionary<int, string> ();

			foreach (KeyValuePair<int, string> entry in urlSources) {

				if (ct != CancellationToken.None && ct.IsCancellationRequested) 
				{
					return null;
				}

				HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create (entry.Value);
				/*IAsyncResult result = */ myReq.BeginGetResponse (new AsyncCallback (FinishWebRequest), myReq);

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

		private object FillFromSources(object objectToFill, Dictionary<int, string> globalSources, CancellationToken ct)
		{
			Type objectType = objectToFill.GetType();
			foreach (PropertyInfo property in objectType.GetProperties ()) {

				if (ct != CancellationToken.None && ct.IsCancellationRequested) 
				{
					return null;
				}

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
				foreach (Attribute textActionAttribute in propertyAttrList) {
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

				if (property.PropertyType == typeof(string)) {
					property.SetValue (objectToFill, sourceText, null);
				}
				else if (property.PropertyType == typeof(int)) {
					int sourceAsInt = 0;
					if (int.TryParse(sourceText, out sourceAsInt)) {
						property.SetValue (objectToFill, sourceAsInt, null);
					}
					else {
							throw new InvalidCastException();
					}
				}
				else if (property.PropertyType == typeof(DateTime)) {
					DateTime sourceAsDt = DateTime.Now;
					if (DateTime.TryParse(sourceText, out sourceAsDt)) {
						property.SetValue (objectToFill, sourceAsDt, null);
					}
					else {
						throw new InvalidCastException();
					}
				}
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

