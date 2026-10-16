using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Cysharp.Text;
using Newtonsoft.Json.Linq;
using XRL;
using XRL.Collections;
using XRL.UI;

namespace HistoryKit
{
	// Token: 0x0200036F RID: 879
	[HasModSensitiveStaticCache]
	public static class HistoricSpice
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x000A140B File Offset: 0x0009F60B
		public static JObject root
		{
			get
			{
				HistoricSpice.CheckInit();
				return HistoricSpice._root;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x000A1417 File Offset: 0x0009F617
		public static Dictionary<string, JContainer> roots
		{
			get
			{
				HistoricSpice.CheckInit();
				return HistoricSpice._roots;
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000A1423 File Offset: 0x0009F623
		public static void CheckInit()
		{
			if (HistoricSpice._roots == null)
			{
				Loading.LoadTask("Loading HistorySpice.json", new Action(HistoricSpice.Init), true);
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000A1444 File Offset: 0x0009F644
		private static void Init()
		{
			if (HistoricSpice._roots != null)
			{
				return;
			}
			JObject jobject = JObject.Parse(File.ReadAllText(DataManager.FilePath("HistorySpice.json")));
			JsonMergeSettings jsonMergeSettings = new JsonMergeSettings
			{
				MergeArrayHandling = MergeArrayHandling.Union,
				MergeNullValueHandling = MergeNullValueHandling.Ignore,
				PropertyNameComparison = StringComparison.Ordinal
			};
			foreach (ModInfo modInfo in ModManager.ActiveMods)
			{
				foreach (ModFile modFile in modInfo.Files)
				{
					if (modFile.Type == ModFileType.JSON && modFile.Name == "historyspice.json")
					{
						try
						{
							JObject jobject2 = JObject.Parse(File.ReadAllText(modFile.OriginalName));
							jobject.Merge(jobject2, jsonMergeSettings);
						}
						catch (Exception ex)
						{
							MetricsManager.LogModError(modInfo, ex);
						}
					}
				}
			}
			JObject jobject3 = (JObject)jobject["spice"];
			Dictionary<string, JContainer> dictionary = new Dictionary<string, JContainer>();
			HistoricSpice._root = jobject3;
			HistoricSpice._roots = dictionary;
			foreach (JProperty jproperty in jobject3.Properties())
			{
				dictionary.Add(jproperty.Name, (JContainer)jproperty.Value);
			}
			RingDeque<string> ringDeque = new RingDeque<string>();
			ringDeque.Enqueue("spice");
			foreach (KeyValuePair<string, JContainer> keyValuePair in dictionary)
			{
				string text;
				JContainer jcontainer;
				keyValuePair.Deconstruct(out text, out jcontainer);
				string text2 = text;
				JContainer jcontainer2 = jcontainer;
				ringDeque.Enqueue(text2);
				HistoricSpice.ResolveRelativeLinks(ringDeque, jcontainer2);
				ringDeque.Eject();
			}
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000A164C File Offset: 0x0009F84C
		private static void ResolveRelativeLinks(RingDeque<string> Stack, JContainer Container)
		{
			foreach (JToken jtoken in Container.Children())
			{
				if (jtoken.Type == JTokenType.String)
				{
					string text = (string)jtoken;
					Match match = Regex.Match(text, "<\\^.*?>");
					if (match.Success)
					{
						using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
						{
							using (Utf16ValueStringBuilder utf16ValueStringBuilder2 = ZString.CreateStringBuilder())
							{
								utf16ValueStringBuilder.Append(text);
								while (!match.Value.IsNullOrEmpty())
								{
									string value = match.Value;
									int num = value.IndexOf("^.", StringComparison.Ordinal);
									if (num == -1)
									{
										match = match.NextMatch();
									}
									else
									{
										int num2 = 1;
										int num3 = num - 1;
										while (num3 >= 0 && value[num3] == '^')
										{
											num3--;
											num2++;
										}
										utf16ValueStringBuilder2.Clear();
										utf16ValueStringBuilder2.Append('<');
										int i = 0;
										int num4 = Stack.Count - num2;
										while (i < num4)
										{
											utf16ValueStringBuilder2.Append(Stack[i]);
											utf16ValueStringBuilder2.Append('.');
											i++;
										}
										utf16ValueStringBuilder2.Append(value, num + 2, value.Length - 3);
										int num5 = utf16ValueStringBuilder.Length - text.Length;
										utf16ValueStringBuilder.Remove(match.Index + num5, match.Length);
										utf16ValueStringBuilder.Insert(match.Index + num5, utf16ValueStringBuilder2.AsSpan(), 1);
										match = match.NextMatch();
									}
								}
								((JValue)jtoken).Value = utf16ValueStringBuilder.ToString();
							}
						}
					}
				}
			}
			JObject jobject = Container as JObject;
			if (jobject != null)
			{
				foreach (JProperty jproperty in jobject.Properties())
				{
					JContainer jcontainer = jproperty.Value as JContainer;
					if (jcontainer != null)
					{
						Stack.Enqueue(jproperty.Name);
						HistoricSpice.ResolveRelativeLinks(Stack, jcontainer);
						Stack.Eject();
					}
				}
			}
		}

		// Token: 0x0400109A RID: 4250
		[ModSensitiveStaticCache(false)]
		private static JObject _root;

		// Token: 0x0400109B RID: 4251
		[ModSensitiveStaticCache(false)]
		public static Dictionary<string, JContainer> _roots;
	}
}
