using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.Collections;
using XRL.Language;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Text;
using XRL.World.Text.Delegates;

namespace XRL
{
	// Token: 0x0200053C RID: 1340
	public static class GameText
	{
		// Token: 0x06003972 RID: 14706 RVA: 0x0011B040 File Offset: 0x00119240
		public static string VariableReplace(string Message, string ExplicitSubject, bool ExplicitSubjectPlural = false, string ExplicitObject = null, bool ExplicitObjectPlural = false, bool StripColors = false)
		{
			if (Message.IsNullOrEmpty())
			{
				return Message;
			}
			return Message.StartReplace().AddExplicit(ExplicitSubject, ExplicitSubjectPlural).AddExplicit(ExplicitObject, ExplicitObjectPlural)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x0011B06D File Offset: 0x0011926D
		public static string VariableReplace(StringBuilder Message, string ExplicitSubject, bool ExplicitSubjectPlural = false, string ExplicitObject = null, bool ExplicitObjectPlural = false, bool StripColors = false)
		{
			return Message.StartReplace().AddExplicit(ExplicitSubject, ExplicitSubjectPlural).AddExplicit(ExplicitObject, ExplicitObjectPlural)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x0011B090 File Offset: 0x00119290
		public static string VariableReplace(string Message, TextArgument Subject, TextArgument Object = default(TextArgument), bool StripColors = false)
		{
			if (Message.IsNullOrEmpty())
			{
				return Message;
			}
			return Message.StartReplace().AddArgument(Subject, null).AddArgument(Object, null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x0011B0BB File Offset: 0x001192BB
		public static string VariableReplace(StringBuilder Message, TextArgument Subject, TextArgument Object = default(TextArgument), bool StripColors = false)
		{
			return Message.StartReplace().AddArgument(Subject, null).AddArgument(Object, null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x0011B0DC File Offset: 0x001192DC
		public static string VariableReplace(StringBuilder Message, TextArgument Object1, TextArgument Object2, TextArgument Object3, TextArgument Object4 = default(TextArgument), TextArgument Object5 = default(TextArgument), int DefaultObject = -1, bool StripColors = false)
		{
			return Message.StartReplace().SetDefaultArgument(DefaultObject).AddArgument(Object1, null)
				.AddArgument(Object2, null)
				.AddArgument(Object3, null)
				.AddArgument(Object4, null)
				.AddArgument(Object5, null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x0011B11C File Offset: 0x0011931C
		public static string VariableReplace(string Message, GameObject Subject, GameObject Object = null, bool StripColors = false)
		{
			if (Message.IsNullOrEmpty())
			{
				return Message;
			}
			return Message.StartReplace().AddObject(Subject, null).AddObject(Object, null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x0011B147 File Offset: 0x00119347
		public static string VariableReplace(StringBuilder Message, GameObject Subject, GameObject Object = null, bool StripColors = false)
		{
			return Message.StartReplace().AddObject(Subject, null).AddObject(Object, null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x0011B168 File Offset: 0x00119368
		public static string VariableReplace(StringBuilder Message, GameObject Object1, GameObject Object2, GameObject Object3, GameObject Object4 = null, GameObject Object5 = null, int DefaultObject = -1, bool StripColors = false)
		{
			return Message.StartReplace().SetDefaultArgument(DefaultObject).AddObject(Object1, null)
				.AddObject(Object2, null)
				.AddObject(Object3, null)
				.AddObject(Object4, null)
				.AddObject(Object5, null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x0011B1A8 File Offset: 0x001193A8
		public static string VariableReplace(StringBuilder Message, GameObject Subject, string ExplicitSubject, bool ExplicitSubjectPlural, GameObject Object, string ExplicitObject, bool ExplicitObjectPlural, bool StripColors = false)
		{
			return Message.StartReplace().AddArgument(new TextArgument(Subject, ExplicitSubject, ExplicitSubjectPlural), null).AddArgument(new TextArgument(Object, ExplicitObject, ExplicitObjectPlural), null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x0011B1DB File Offset: 0x001193DB
		public static string VariableReplace(string Message, string ExplicitSubject, bool ExplicitSubjectPlural, GameObject Object, bool StripColors = false)
		{
			if (Message.IsNullOrEmpty())
			{
				return Message;
			}
			return Message.StartReplace().AddExplicit(ExplicitSubject, ExplicitSubjectPlural).AddObject(Object, null)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x0011B207 File Offset: 0x00119407
		public static string VariableReplace(string Message, GameObject Subject, string ExplicitObject, bool ExplicitObjectPlural, bool StripColors = false)
		{
			if (Message.IsNullOrEmpty())
			{
				return Message;
			}
			return Message.StartReplace().AddObject(Subject, null).AddExplicit(ExplicitObject, ExplicitObjectPlural)
				.StripColors(StripColors)
				.ToString();
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x0011B233 File Offset: 0x00119433
		public static string VariableReplace(string Message, bool StripColors = false)
		{
			if (Message.IsNullOrEmpty())
			{
				return Message;
			}
			return Message.StartReplace().StripColors(StripColors).ToString();
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x0011B250 File Offset: 0x00119450
		public static string VariableReplace(StringBuilder Message, bool StripColors = false)
		{
			GameText.Process(Message, null, null, null, -1, StripColors);
			return Message.ToString();
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x0011B264 File Offset: 0x00119464
		public static void Process(ref string Message, StringMap<ReplacerEntry> Replacers = null, StringMap<int> Aliases = null, IList<TextArgument> Arguments = null, int DefaultArgument = -1, bool StripColors = false)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(Message);
			GameText.Process(stringBuilder, Replacers, Aliases, Arguments, DefaultArgument, StripColors);
			Message = stringBuilder.ToString();
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x0011B290 File Offset: 0x00119490
		public static void Process(StringBuilder Message, StringMap<ReplacerEntry> Replacers = null, StringMap<int> Aliases = null, IList<TextArgument> Arguments = null, int DefaultArgument = -1, bool StripColors = false)
		{
			if (!VariableReplacers.Initialized)
			{
				VariableReplacers.LoadReplacers();
			}
			using (GameText.ProcessContext processContext = GameText.ProcessContext.Get())
			{
				StringBuilder valueBuilder = processContext.ValueBuilder;
				List<string> replaceParameters = processContext.ReplaceParameters;
				List<ReplacerEntry> postProcessors = processContext.PostProcessors;
				StringMap<ReplacerEntry> map = VariableReplacers.Map;
				StringMap<ReplacerEntry> postMap = VariableReplacers.PostMap;
				char[] array = processContext.Buffer;
				int num = processContext.BufferCapacity;
				int num2 = ((Arguments == null) ? 0 : Arguments.Count);
				TextArgument textArgument = ((DefaultArgument >= 0) ? Arguments[DefaultArgument] : default(TextArgument));
				int num3 = -1;
				int num4 = -1;
				int num5 = -1;
				int num6 = -1;
				int num7 = -1;
				int num8 = -1;
				int i = 0;
				int num9 = Message.Length;
				while (i < num9)
				{
					char c = Message[i];
					if (num4 == -1)
					{
						if (c == '=')
						{
							num4 = (num5 = i);
							num6 = (num7 = (num3 = (num8 = -1)));
							replaceParameters.Clear();
							postProcessors.Clear();
						}
					}
					else
					{
						int num10 = i - num4 - 1;
						if (num10 >= num)
						{
							char[] array2 = new char[num * 2];
							Array.Copy(array, 0, array2, 0, num);
							array = (processContext.Buffer = array2);
							num = (processContext.BufferCapacity = num * 2);
						}
						array[num10] = c;
						if (c == ' ' || c == '\n')
						{
							if (num6 == -1)
							{
								num4 = -1;
							}
						}
						else if (c == ':')
						{
							if (num6 == -1)
							{
								if (num8 == -1)
								{
									num8 = i;
								}
								num6 = i + 1;
							}
							else
							{
								replaceParameters.Add(new string(array, num6 - num4 - 1, i - num6));
								num6 = i + 1;
							}
						}
						else if (c == '|')
						{
							if (num7 == -1)
							{
								if (num6 != -1)
								{
									replaceParameters.Add(new string(array, num6 - num4 - 1, i - num6));
									num6 = -1;
								}
								else if (num8 == -1)
								{
									num8 = i;
								}
								num7 = i + 1;
							}
							else
							{
								Span<char> span = array.AsSpan<char>(num7 - num4 - 1, i - num7);
								ReplacerEntry replacerEntry;
								if (postMap.TryGetValue(span, out replacerEntry))
								{
									postProcessors.Add(replacerEntry);
								}
								else
								{
									MetricsManager.LogError("No variable post processor by key '" + new string(span) + "' found.");
								}
								num7 = i + 1;
							}
						}
						else if (c == '.')
						{
							if (num3 == -1)
							{
								num3 = -3;
								int num11;
								if (GameText.TryParseTarget(array, Aliases, num10, out num11))
								{
									num3 = num11;
									num5 = i;
								}
							}
						}
						else if (c == '=')
						{
							if (num7 != -1)
							{
								Span<char> span2 = array.AsSpan<char>(num7 - num4 - 1, i - num7);
								ReplacerEntry replacerEntry2;
								if (postMap.TryGetValue(span2, out replacerEntry2))
								{
									postProcessors.Add(replacerEntry2);
								}
								else
								{
									MetricsManager.LogError("No variable post processor by key '" + new string(span2) + "' found.");
								}
								num7 = -1;
							}
							else if (num6 != -1)
							{
								replaceParameters.Add(new string(array, num6 - num4 - 1, i - num6));
								num6 = -1;
							}
							else if (num8 == -1)
							{
								num8 = i;
							}
							if (num5 >= num8 - 1)
							{
								num4 = -1;
							}
							else
							{
								ReadOnlySpan<char> readOnlySpan = array.AsSpan<char>(num5 - num4, num8 - num5 - 1);
								TargetType targetType = TargetType.None;
								TextArgument textArgument2;
								if (num3 == -2)
								{
									textArgument2 = new TextArgument(The.Player, null, null);
									GameObject @object = textArgument2.Object;
									textArgument2.Pronouns = ((@object != null && @object.IsPlural) ? PronounSet.DefaultPlayerPlural : PronounSet.DefaultPlayer);
									targetType = TargetType.Player;
								}
								else if (num3 < 0)
								{
									textArgument2 = textArgument;
								}
								else if (num3 >= num2)
								{
									MetricsManager.LogWarning(string.Format("Object index {0} for key '{1}' is out of bounds.", num3, new string(readOnlySpan)));
									textArgument2 = default(TextArgument);
								}
								else
								{
									textArgument2 = Arguments[num3];
									targetType = TargetType.Object;
								}
								ReplacerEntry replacerEntry3;
								if ((Replacers != null && Replacers.TryGetValue(readOnlySpan, out replacerEntry3)) || map.TryGetValue(readOnlySpan, out replacerEntry3))
								{
									valueBuilder.Clear();
									DelegateContext delegateContext = DelegateContext.Set(valueBuilder, replaceParameters, textArgument2.Object, textArgument2.Pronouns, targetType, textArgument2.Explicit, replacerEntry3.Default, replacerEntry3.Capitalize, replacerEntry3.Flags);
									string text = replacerEntry3.Delegate(delegateContext) ?? valueBuilder.ToString();
									int count = postProcessors.Count;
									if (count > 0)
									{
										valueBuilder.Clear();
										valueBuilder.Append(text);
										for (int j = 0; j < count; j++)
										{
											ReplacerEntry replacerEntry4 = postProcessors[j];
											delegateContext = DelegateContext.Set(valueBuilder, replaceParameters, textArgument2.Object, textArgument2.Pronouns, targetType, textArgument2.Explicit, replacerEntry4.Default, replacerEntry4.Capitalize, replacerEntry4.Flags);
											replacerEntry4.Delegate(delegateContext);
										}
										text = valueBuilder.ToString();
									}
									Message.Remove(num4, i - num4 + 1);
									Message.Insert(num4, text);
									i = num4 - 1 + text.Length;
									num9 = Message.Length;
								}
								else
								{
									MetricsManager.LogError("No variable replacer by key '" + new string(readOnlySpan) + "' found.");
								}
								num4 = -1;
							}
						}
					}
					i++;
				}
				if (StripColors)
				{
					ColorUtility.StripFormatting(Message);
				}
			}
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x0011B7B8 File Offset: 0x001199B8
		private static bool TryParseTarget(char[] Buffer, StringMap<int> Aliases, int Position, out int Index)
		{
			if (Buffer[Position - 1] == ']')
			{
				int num = Array.LastIndexOf<char>(Buffer, '[', Position - 3);
				if (num != -1)
				{
					Span<char> span = Buffer.AsSpan<char>(0, num);
					Span<char> span2 = Buffer.AsSpan<char>(num, Position - num);
					int num2;
					if (GameText.Targets.ContainsKey(span) && int.TryParse(span2, out num2))
					{
						Index = num2;
						return true;
					}
				}
			}
			else
			{
				Span<char> span3 = Buffer.AsSpan<char>(0, Position);
				int num3;
				if ((Aliases != null && Aliases.TryGetValue(span3, out num3)) || GameText.Targets.TryGetValue(span3, out num3))
				{
					Index = num3;
					return true;
				}
			}
			Index = -1;
			return false;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x0011B858 File Offset: 0x00119A58
		public static string GenerateMarkovMessageParagraph()
		{
			string text = "LibraryCorpus.json";
			MarkovBook.EnsureCorpusLoaded(text);
			return MarkovChain.GenerateParagraph(MarkovBook.CorpusData[text]);
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x0011B884 File Offset: 0x00119A84
		public static string GenerateMarkovMessageSentence()
		{
			string text = "LibraryCorpus.json";
			MarkovBook.EnsureCorpusLoaded(text);
			return MarkovChain.GenerateSentence(MarkovBook.CorpusData[text], null);
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x0011B8B0 File Offset: 0x00119AB0
		public static string RoughConvertSecondPersonToThirdPerson(string text, GameObject who)
		{
			if (text.IsNullOrEmpty())
			{
				return null;
			}
			if (text.StartsWith("You were"))
			{
				string text2 = ((The.Player != null) ? ("by " + The.Player.ShortDisplayName + ".") : null);
				if (text2 != null && text.EndsWith(text2))
				{
					text = text.Substring(0, text.Length - text2.Length);
					if (text.Contains(" to death "))
					{
						text = text.Replace(" to death ", " ").Replace("You were", "You") + who.them + " to death.";
					}
					else
					{
						text = text.Replace("You were", "You") + who.them + ".";
					}
				}
				else
				{
					text = text.Replace("You were", who.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false));
				}
			}
			else if (text.StartsWith("You"))
			{
				text = text.Replace("You", who.It);
			}
			else
			{
				text = who.Does("were", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " " + Grammar.InitLower(text);
			}
			if (text.Contains("yourself"))
			{
				text = text.Replace("yourself", who.itself);
			}
			if (text.Contains("yourselves"))
			{
				text = text.Replace("yourselves", who.itself);
			}
			if (text.Contains(" caused by "))
			{
				string text3 = ((The.Player != null) ? (" caused by " + The.Player.ShortDisplayName + ".") : null);
				if (text3 != null && text.EndsWith(text3))
				{
					text = text.Replace(text3, ", which you caused.");
				}
			}
			return text;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x0011BAB4 File Offset: 0x00119CB4
		public static ReplaceBuilder StartReplace(this StringBuilder Text)
		{
			return ReplaceBuilder.Get().Start(Text);
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x0011BAC1 File Offset: 0x00119CC1
		public static ReplaceBuilder StartReplace(this string Text)
		{
			return ReplaceBuilder.Get().Start(Text);
		}

		// Token: 0x04001CF1 RID: 7409
		private static StringMap<int> Targets = new StringMap<int>
		{
			{ "player", -2 },
			{ "subject", 0 },
			{ "pronouns", 0 },
			{ "objpronouns", 1 },
			{ "object", 1 }
		};

		// Token: 0x02001967 RID: 6503
		private class ProcessContext : IDisposable
		{
			// Token: 0x0600E385 RID: 58245 RVA: 0x00527DB8 File Offset: 0x00525FB8
			public static GameText.ProcessContext Get()
			{
				GameText.ProcessContext processContext;
				if (GameText.ProcessContext.Repository.TryEject(out processContext))
				{
					return processContext;
				}
				return new GameText.ProcessContext();
			}

			// Token: 0x0600E386 RID: 58246 RVA: 0x00527DDA File Offset: 0x00525FDA
			public void Dispose()
			{
				GameText.ProcessContext.Repository.Enqueue(this);
			}

			// Token: 0x040065BD RID: 26045
			private static RingDeque<GameText.ProcessContext> Repository = new RingDeque<GameText.ProcessContext>();

			// Token: 0x040065BE RID: 26046
			public int BufferCapacity = 64;

			// Token: 0x040065BF RID: 26047
			public char[] Buffer = new char[64];

			// Token: 0x040065C0 RID: 26048
			public List<string> ReplaceParameters = new List<string>();

			// Token: 0x040065C1 RID: 26049
			public List<ReplacerEntry> PostProcessors = new List<ReplacerEntry>();

			// Token: 0x040065C2 RID: 26050
			public StringBuilder ValueBuilder = new StringBuilder(128);
		}
	}
}
