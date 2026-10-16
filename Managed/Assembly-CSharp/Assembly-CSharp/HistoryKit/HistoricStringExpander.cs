using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEngine;
using XRL.Language;

namespace HistoryKit
{
	// Token: 0x02000370 RID: 880
	public static class HistoricStringExpander
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x000A18E0 File Offset: 0x0009FAE0
		public static Dictionary<string, string> GetVariableCache()
		{
			HistoricStringExpander.VariableCache.Clear();
			return HistoricStringExpander.VariableCache;
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000A18F4 File Offset: 0x0009FAF4
		public static string ExpandQuery(string query, HistoricEntitySnapshot entity, History history, Dictionary<string, string> vars, Dictionary<string, JToken> nodeVars, global::System.Random R = null)
		{
			string text = query;
			string text2 = query;
			string text3 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			if (history == null)
			{
				if (HistoricStringExpander.nullHistory == null)
				{
					HistoricStringExpander.nullHistory = new History(0L);
				}
				history = HistoricStringExpander.nullHistory;
			}
			if (R == null)
			{
				R = history.r;
			}
			if (query.Length > 0 && query[0] == '<')
			{
				query = query.Substring(1, query.Length - 2);
			}
			if (query.EndsWith(".capitalize"))
			{
				query = query.Remove(query.Length - ".capitalize".Length);
				flag2 = true;
			}
			if (query.EndsWith(".article"))
			{
				query = query.Remove(query.Length - ".article".Length);
				flag = true;
			}
			if (query.EndsWith(".pluralize"))
			{
				query = query.Remove(query.Length - ".pluralize".Length);
				flag3 = true;
			}
			string[] array = query.Split('.', StringSplitOptions.None);
			if (!vars.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					string text4;
					if (vars.TryGetValue(array[i], out text4))
					{
						array[i] = text4;
					}
					i++;
				}
			}
			if (array.Length < 2)
			{
				if (array[0].Contains("="))
				{
					string[] array2 = array[0].Split('=', StringSplitOptions.None);
					array[0] = array2[1];
					text3 = array2[0];
				}
				else
				{
					text3 = null;
				}
				string text5 = array[0];
				JToken jtoken;
				if (nodeVars.TryGetValue(text5, out jtoken))
				{
					text2 = jtoken.ToString();
				}
				else if (!vars.TryGetValue(text5, out text2))
				{
					text2 = text5;
				}
			}
			else
			{
				if (query.Contains("="))
				{
					string[] array3 = query.Split('=', StringSplitOptions.None);
					text3 = array3[0];
					array[0] = array3[1].Split('.', StringSplitOptions.None)[0];
				}
				else
				{
					text3 = null;
				}
				string text6 = array[0];
				if (text6.StartsWith("spice") || text6[0] == '$')
				{
					JToken jtoken2;
					if (text6 == "spice")
					{
						jtoken2 = HistoricSpice.root;
					}
					else
					{
						jtoken2 = nodeVars[text6];
					}
					for (int j = 1; j < array.Length; j++)
					{
						JObject jobject = jtoken2 as JObject;
						if (array[j] == "!random")
						{
							if (j == array.Length - 1)
							{
								IList<JToken> list = jtoken2 as IList<JToken>;
								if (list != null && list.Count > 0)
								{
									JToken jtoken3 = list[R.Next(0, list.Count)];
									JProperty jproperty = jtoken3 as JProperty;
									if (jproperty != null)
									{
										text2 = jproperty.Name;
										jtoken3 = jproperty.Value;
									}
									else
									{
										text2 = jtoken3.ToString();
									}
									if (!string.IsNullOrEmpty(text3) && text3[0] == '$')
									{
										nodeVars[text3] = jtoken3;
										return "";
									}
									break;
								}
								else
								{
									JValue jvalue = jtoken2 as JValue;
									if (jvalue != null)
									{
										text2 = (string)jvalue;
										break;
									}
									Debug.LogError("Invalid randomization root: " + text);
								}
							}
							else
							{
								IList<JToken> list2 = jtoken2 as IList<JToken>;
								if (list2 == null || list2.Count <= 0)
								{
									Debug.LogError("no spice root after random in " + text);
									return "";
								}
								jtoken2 = list2[R.Next(0, list2.Count)];
								JProperty jproperty2 = jtoken2 as JProperty;
								if (jproperty2 != null)
								{
									jtoken2 = jproperty2.Value;
								}
							}
						}
						else
						{
							if (array[j].StartsWith("entity$"))
							{
								string[] array4 = array[j].Split('$', StringSplitOptions.None);
								if (array4[1].Contains("["))
								{
									string[] array5 = array4[1].Split('[', StringSplitOptions.None);
									array5[1] = array5[1].Replace("]", "");
									if (!entity.listProperties.ContainsKey(array5[0]))
									{
										if (entity.properties.ContainsKey(array4[1]))
										{
											return "<undefined entity list " + array5[0] + ">";
										}
										if (((jobject != null) ? jobject["_failureredirect"] : null) != null)
										{
											StringBuilder stringBuilder = new StringBuilder(jobject["_failureredirect"].ToString());
											for (int k = j; k < array.Length; k++)
											{
												stringBuilder.Append(".");
												stringBuilder.Append(array[k]);
											}
											return HistoricStringExpander.ExpandQuery(stringBuilder.ToString(), entity, history, vars, nodeVars, null);
										}
										if (((jobject != null) ? jobject["_staticfailureredirect"] : null) != null)
										{
											return HistoricStringExpander.ExpandQuery(jobject["_staticfailureredirect"].ToString(), entity, history, vars, nodeVars, null);
										}
										return "<undefined entity property " + array4[1] + ">";
									}
									else
									{
										List<string> list3 = entity.GetList(array5[0]);
										if (list3.Count == 0)
										{
											return "<empty entity list " + array5[0] + ">";
										}
										int num2;
										if (array5[1] == "random")
										{
											num2 = R.Next(0, list3.Count);
										}
										else
										{
											num2 = int.Parse(array5[1]);
										}
										jtoken2 = jtoken2[list3[num2]];
									}
								}
								else if (!entity.properties.ContainsKey(array4[1]))
								{
									if (((jobject != null) ? jobject["_failureredirect"] : null) != null)
									{
										StringBuilder stringBuilder2 = new StringBuilder(jobject["_failureredirect"].ToString());
										for (int l = j; l < array.Length; l++)
										{
											stringBuilder2.Append(".");
											stringBuilder2.Append(array[l]);
										}
										return HistoricStringExpander.ExpandQuery(stringBuilder2.ToString(), entity, history, vars, nodeVars, null);
									}
									if (((jobject != null) ? jobject["_staticfailureredirect"] : null) != null)
									{
										return HistoricStringExpander.ExpandQuery(jobject["_staticfailureredirect"].ToString(), entity, history, vars, nodeVars, null);
									}
									return "<undefined entity property " + array4[1] + ">";
								}
								else
								{
									jtoken2 = jtoken2[entity.GetProperty(array4[1], "unknown")];
								}
							}
							else
							{
								try
								{
									jtoken2 = jtoken2[array[j]];
								}
								catch (Exception)
								{
									jtoken2 = null;
								}
								if (jtoken2 == null)
								{
									if (((jobject != null) ? jobject["_failureredirect"] : null) != null)
									{
										StringBuilder stringBuilder3 = new StringBuilder(jobject["_failureredirect"].ToString());
										for (int m = j; m < array.Length; m++)
										{
											stringBuilder3.Append('.').Append(array[m]);
										}
										return HistoricStringExpander.ExpandQuery(stringBuilder3.ToString(), entity, history, vars, nodeVars, null);
									}
									if (((jobject != null) ? jobject["_staticfailureredirect"] : null) != null)
									{
										return HistoricStringExpander.ExpandQuery(jobject["_staticfailureredirect"].ToString(), entity, history, vars, nodeVars, null);
									}
								}
							}
							if (jtoken2 == null)
							{
								MetricsManager.LogError(string.Concat(new string[]
								{
									"spice reference ",
									array[j],
									" in ",
									text,
									" wasn't a node"
								}));
								return "";
							}
							if (j == array.Length - 1)
							{
								JProperty jproperty3 = jtoken2 as JProperty;
								if (jproperty3 != null)
								{
									jtoken2 = jproperty3.Value;
								}
								JValue jvalue2 = jtoken2 as JValue;
								if (jvalue2 != null)
								{
									text2 = (string)jvalue2;
								}
								else
								{
									Debug.LogError(string.Concat(new string[]
									{
										"Invalid value at '",
										array[j],
										"' in '",
										text,
										"' "
									}));
								}
								if (!string.IsNullOrEmpty(text3) && text3[0] == '$')
								{
									nodeVars[text3] = jtoken2;
									return "";
								}
								break;
							}
						}
					}
				}
				else if (text6.StartsWith("entity"))
				{
					HistoricEntitySnapshot historicEntitySnapshot = entity;
					if (text6.StartsWith("entity["))
					{
						historicEntitySnapshot = history.GetEntity(text6.Split('[', StringSplitOptions.None)[1].Replace("]", "")).GetSnapshotAtYear(history.currentYear);
					}
					if (historicEntitySnapshot == null)
					{
						return "<unknown entity>";
					}
					if (array.Length != 2)
					{
						return "<unknown format " + query + ">";
					}
					if (array[1].Contains("["))
					{
						string[] array6 = array[1].Split('[', StringSplitOptions.None);
						array6[1] = array6[1].Replace("]", "");
						if (!entity.listProperties.ContainsKey(array6[0]))
						{
							return "<undefined entity list " + array6[0] + ">";
						}
						if (entity.GetList(array6[0]).Count == 0)
						{
							return "<empty entity list " + array6[0] + ">";
						}
						int num3;
						if (array6[1] == "random")
						{
							num3 = R.Next(0, entity.GetList(array6[0]).Count);
						}
						else
						{
							num3 = int.Parse(array6[1]);
						}
						text2 = historicEntitySnapshot.GetList(array6[0])[num3];
					}
					else
					{
						text2 = historicEntitySnapshot.GetProperty(array[1], "unknown");
					}
				}
				else
				{
					text6.StartsWith("history");
				}
			}
			if (text2 == " ")
			{
				text2 = "";
			}
			if (flag3 && text2.Length > 0)
			{
				if (text2[0] == '<')
				{
					if (text2.Substring(text2.IndexOf('>') - ".pluralize".Length) != ".pluralize")
					{
						text2 = text2.Insert(text2.IndexOf('>'), ".pluralize");
					}
				}
				else if (text2[0] == '=')
				{
					text2 = "=pluralize=" + text2;
				}
				else
				{
					text2 = Grammar.Pluralize(text2);
				}
			}
			if (flag && text2.Length > 0)
			{
				if (text2[0] == '=')
				{
					text2 = (flag2 ? "=Article=" : "=article=") + text2;
				}
				else
				{
					text2 = Grammar.A(text2, flag2);
				}
				flag2 = false;
			}
			if (flag2 && text2.Length > 0)
			{
				if (text2[0] == '<')
				{
					if (text2.Substring(text2.IndexOf('>') - ".capitalize".Length) != ".capitalize")
					{
						text2 = text2.Insert(text2.IndexOf('>'), ".capitalize");
					}
				}
				else if (text2[0] == '=')
				{
					text2 = "=capitalize=" + text2;
				}
				else
				{
					text2 = char.ToUpper(text2[0]).ToString() + text2.Substring(1);
				}
			}
			if (text3 != null)
			{
				if (vars.ContainsKey(text3))
				{
					vars[text3] = text2;
				}
				else
				{
					vars.Add(text3, text2);
				}
				return "";
			}
			if (vars != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in vars)
				{
					int num4 = 0;
					while (text2.Contains(keyValuePair.Key) && ++num4 < 5)
					{
						text2 = text2.Replace(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
			return text2;
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000A23E8 File Offset: 0x000A05E8
		public static string ExpandString(string input, global::System.Random Random = null)
		{
			return HistoricStringExpander.ExpandString(input, null, null, null, Random);
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000A23F4 File Offset: 0x000A05F4
		public static string ExpandString(string input, HistoricEntitySnapshot entity, History history, Dictionary<string, string> vars = null, global::System.Random Random = null)
		{
			if (string.IsNullOrEmpty(input))
			{
				return "";
			}
			if (history == null)
			{
				if (HistoricStringExpander.nullHistory == null)
				{
					HistoricStringExpander.nullHistory = new History(0L);
				}
				history = HistoricStringExpander.nullHistory;
			}
			StringBuilder stringBuilder = new StringBuilder(input);
			if (vars == null)
			{
				vars = new Dictionary<string, string>();
			}
			Dictionary<string, JToken> dictionary = new Dictionary<string, JToken>();
			Match match = Regex.Match(input, "<.*?>");
			int num = 0;
			while (match != null && !string.IsNullOrEmpty(match.Value))
			{
				int index = match.Groups[0].Index;
				int length = match.Groups[0].Length;
				stringBuilder.Remove(index, length);
				stringBuilder.Insert(index, HistoricStringExpander.ExpandQuery(match.Groups[0].Value, entity, history, vars, dictionary, Random));
				if (stringBuilder.Equals(input))
				{
					break;
				}
				num++;
				if (num > 25)
				{
					MetricsManager.LogError("Maximum recursion reached on " + input + " current " + stringBuilder.ToString());
					break;
				}
				match = Regex.Match(stringBuilder.ToString(), "<.*?>");
			}
			if (vars != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in vars)
				{
					num = 0;
					while (stringBuilder.Contains(keyValuePair.Key) && num < 5)
					{
						stringBuilder.Replace(keyValuePair.Key, keyValuePair.Value);
						num++;
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400109C RID: 4252
		public static History nullHistory = new History(0L);

		// Token: 0x0400109D RID: 4253
		private static Dictionary<string, string> VariableCache = new Dictionary<string, string>();
	}
}
