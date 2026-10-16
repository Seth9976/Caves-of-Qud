using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleLib.Console;
using Ionic.Zip;
using SimpleJSON;

namespace XRL.Language
{
	// Token: 0x020005E8 RID: 1512
	public static class WordDataManager
	{
		// Token: 0x0600411E RID: 16670 RVA: 0x00146F80 File Offset: 0x00145180
		public static string GetBaseText(string text)
		{
			if (text == null)
			{
				return null;
			}
			string text2;
			if (!WordDataManager.BaseTextCache.TryGetValue(text, out text2))
			{
				if (ColorUtility.HasFormatting(text))
				{
					text = ColorUtility.StripFormatting(text);
				}
				text2 = Regex.Replace(text.ToLower(), "[^a-z]+", " ").Trim();
				WordDataManager.BaseTextCache[text] = text2;
			}
			return text2;
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x00146FDC File Offset: 0x001451DC
		public static List<string> GetRelatedWords(ref string Input, bool ExcludeProper = true, string ExcludePartOfSpeech = null, string RequirePartOfSpeech = null, int MinimumEditDistanceFromEachOther = 0, int MinimumEditDistanceFromInput = 0, int Maximum = 2147483647)
		{
			JSONNode relatedWordsJson = WordDataManager.GetRelatedWordsJson(ref Input);
			if (relatedWordsJson == null)
			{
				return null;
			}
			if (ExcludePartOfSpeech == "noun")
			{
				ExcludePartOfSpeech = "n";
			}
			else if (ExcludePartOfSpeech == "verb")
			{
				ExcludePartOfSpeech = "v";
			}
			List<string> list = new List<string>();
			int i = 0;
			int count = relatedWordsJson.Count;
			while (i < count)
			{
				JSONNode jsonnode = relatedWordsJson[i];
				JSONNode jsonnode2 = jsonnode["tags"];
				bool flag = true;
				if (jsonnode2 != null)
				{
					if (flag && ExcludeProper)
					{
						int j = 0;
						int count2 = jsonnode2.Count;
						while (j < count2)
						{
							if (jsonnode2[j].Value == "prop")
							{
								flag = false;
								break;
							}
							j++;
						}
					}
					if (flag && ExcludePartOfSpeech != null)
					{
						int k = 0;
						int count3 = jsonnode2.Count;
						while (k < count3)
						{
							if (jsonnode2[k].Value == ExcludePartOfSpeech)
							{
								flag = false;
								break;
							}
							k++;
						}
					}
					if (flag && RequirePartOfSpeech != null)
					{
						bool flag2 = false;
						int l = 0;
						int count4 = jsonnode2.Count;
						while (l < count4)
						{
							if (jsonnode2[l].Value == RequirePartOfSpeech)
							{
								flag2 = true;
								break;
							}
							l++;
						}
						if (!flag2)
						{
							flag = false;
						}
					}
				}
				else if (RequirePartOfSpeech != null)
				{
					flag = false;
				}
				if (flag)
				{
					string value = jsonnode["word"].Value;
					if (flag && MinimumEditDistanceFromInput > 0 && Grammar.LevenshteinDistance(Input, value, true) < MinimumEditDistanceFromInput)
					{
						flag = false;
					}
					if (flag && MinimumEditDistanceFromEachOther > 0 && list.Count > 0)
					{
						int m = 0;
						int count5 = list.Count;
						while (m < count5)
						{
							if (Grammar.LevenshteinDistance(list[m], value, true) < MinimumEditDistanceFromEachOther)
							{
								flag = false;
								break;
							}
							m++;
						}
					}
					if (flag)
					{
						list.Add(value);
						if (list.Count >= Maximum)
						{
							break;
						}
					}
				}
				i++;
			}
			return list;
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x001471C3 File Offset: 0x001453C3
		public static List<string> GetRelatedWords(string Input, bool ExcludeProper = true, string ExcludePartOfSpeech = null, string RequirePartOfSpeech = null, int MinimumEditDistanceFromEachOther = 0, int MinimumEditDistanceFromInput = 0, int Maximum = 2147483647)
		{
			return WordDataManager.GetRelatedWords(ref Input, ExcludeProper, ExcludePartOfSpeech, RequirePartOfSpeech, MinimumEditDistanceFromEachOther, MinimumEditDistanceFromInput, Maximum);
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x001471D8 File Offset: 0x001453D8
		private static JSONNode GetRelatedWordsJson(ref string text)
		{
			text = WordDataManager.GetBaseText(text);
			if (text == null)
			{
				return null;
			}
			JSONNode relatedWordsJsonInner;
			if (!WordDataManager.RelatedWordsJsonCache.TryGetValue(text, out relatedWordsJsonInner))
			{
				relatedWordsJsonInner = WordDataManager.GetRelatedWordsJsonInner(text);
				WordDataManager.RelatedWordsJsonCache[text] = relatedWordsJsonInner;
			}
			return relatedWordsJsonInner;
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x0014721C File Offset: 0x0014541C
		private static JSONNode GetRelatedWordsJsonInner(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string text2 = text.Replace(" ", "_");
			JSONNode jsonnode = null;
			string text3 = Uri.EscapeUriString(text);
			string text4 = Path.Combine("WordData", "Related", text.Substring(0, 1));
			string text5 = DataManager.FilePath(text4);
			WebClient webClient = new WebClient();
			string text6 = Path.Combine(text5, text2 + ".zip");
			if (File.Exists(text6))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (IEnumerator<ZipEntry> enumerator = ZipFile.Read(text6).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							enumerator.Current.Extract(memoryStream);
						}
					}
					string @string = Encoding.UTF8.GetString(memoryStream.ToArray());
					if (!string.IsNullOrEmpty(@string))
					{
						jsonnode = JSON.Parse(@string);
					}
				}
			}
			bool flag = false;
			if (jsonnode == null)
			{
				string text7 = DataManager.SavePath(text4);
				string text8 = Path.Combine(text7, text3 + ".zip");
				if (Directory.Exists(text7) && File.Exists(text8))
				{
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						using (IEnumerator<ZipEntry> enumerator = ZipFile.Read(text8).GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								enumerator.Current.Extract(memoryStream2);
							}
						}
						string string2 = Encoding.UTF8.GetString(memoryStream2.ToArray());
						if (!string.IsNullOrEmpty(string2))
						{
							jsonnode = JSON.Parse(string2);
						}
					}
				}
				if (jsonnode == null && !flag)
				{
					string text9 = Path.Combine(text7, text3 + ".json");
					try
					{
						Directory.CreateDirectory(text7);
						flag = true;
						string text10 = "https://api.datamuse.com/words?ml=" + text3;
						webClient.DownloadFile(text10, text9);
						if (File.Exists(text9))
						{
							string text11 = File.ReadAllText(text9, Encoding.UTF8);
							if (!string.IsNullOrEmpty(text11))
							{
								jsonnode = JSON.Parse(text11);
							}
							if (jsonnode != null)
							{
								using (ZipFile zipFile = new ZipFile())
								{
									zipFile.AddFile(text9, "");
									zipFile.Save(text8);
								}
							}
							File.Delete(text9);
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogError("WordData Local", ex);
					}
				}
			}
			return jsonnode;
		}

		// Token: 0x04002002 RID: 8194
		private static Dictionary<string, string> BaseTextCache = new Dictionary<string, string>();

		// Token: 0x04002003 RID: 8195
		private static Dictionary<string, JSONNode> RelatedWordsJsonCache = new Dictionary<string, JSONNode>();
	}
}
