using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Cysharp.Text;
using Platform.IO;
using SimpleJSON;
using UnityEngine;
using XRL;
using XRL.World.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200152E RID: 5422
	[HasModSensitiveStaticCache]
	public static class ColorUtility
	{
		// Token: 0x0600D2DD RID: 53981 RVA: 0x004E3CA0 File Offset: 0x004E1EA0
		public static Color ColorFromString(string str)
		{
			Color color;
			if (str.IsNullOrEmpty() || !ColorUtility.ColorMap.TryGetValue(str[0], out color))
			{
				return ColorUtility.Colors.Gray;
			}
			return color;
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600D2DE RID: 53982 RVA: 0x004E3CD6 File Offset: 0x004E1ED6
		// (set) Token: 0x0600D2DF RID: 53983 RVA: 0x004E3CEC File Offset: 0x004E1EEC
		public static Dictionary<char, Color> ColorMap
		{
			get
			{
				if (ColorUtility._ColorMap == null)
				{
					ColorUtility.Init(true, true, true);
				}
				return ColorUtility._ColorMap;
			}
			set
			{
				ColorUtility._ColorMap = value;
			}
		}

		// Token: 0x0600D2E0 RID: 53984 RVA: 0x004E3CF4 File Offset: 0x004E1EF4
		public static Color colorFromTextColor(TextColor C)
		{
			return ColorUtility.usfColorMap[(int)C];
		}

		// Token: 0x0600D2E1 RID: 53985 RVA: 0x004E3D04 File Offset: 0x004E1F04
		public static Color colorFromChar(char c)
		{
			if (c == '\0')
			{
				return Color.clear;
			}
			Color color;
			if (ColorUtility.ColorMap.TryGetValue(c, out color))
			{
				return color;
			}
			MetricsManager.LogError(new Exception(string.Format("Invalid color char '{0}'", c)));
			if (c == 'g')
			{
				return new Color(0f, 0.578f, 0.117f);
			}
			return ColorUtility.Colors.Gray;
		}

		// Token: 0x0600D2E2 RID: 53986 RVA: 0x004E3D6C File Offset: 0x004E1F6C
		public static string StripBackgroundFormatting(string s)
		{
			StringBuilder stringBuilder = The.StringBuilder;
			int i = 0;
			int length = s.Length;
			while (i < length)
			{
				if (s[i] != '^')
				{
					stringBuilder.Append(s[i]);
				}
				else if (s[i] == '^')
				{
					i++;
					if (s[i] == '^')
					{
						stringBuilder.Append('^');
					}
				}
				i++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600D2E3 RID: 53987 RVA: 0x004E3DD8 File Offset: 0x004E1FD8
		public static StringBuilder StripFormatting(StringBuilder Text)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			ColorUtility.StripSB.Clear();
			ColorUtility.StripSB.Append(Text);
			Text.Clear();
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(ColorUtility.StripSB, length, num2, ref num3, ref flag, ref num, out c, out flag2, null);
				if (flag2)
				{
					Text.Append(c);
				}
			}
			while (flag2);
			ColorUtility.StripSB.Clear();
			return Text;
		}

		// Token: 0x0600D2E4 RID: 53988 RVA: 0x004E3E50 File Offset: 0x004E2050
		public static string StripFormatting(string Text)
		{
			if (Text == null)
			{
				return "";
			}
			Dictionary<string, string> dictionary = ColorUtility.StripCache;
			lock (dictionary)
			{
				string text;
				if (ColorUtility.StripCache.TryGetValue(Text, out text))
				{
					return text;
				}
			}
			bool flag2 = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			ColorUtility.StripSB.Clear();
			bool flag3;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag2, ref num, out c, out flag3, null);
				if (flag3)
				{
					ColorUtility.StripSB.Append(c);
				}
			}
			while (flag3);
			string text2 = ColorUtility.StripSB.ToString();
			ColorUtility.StripSB.Clear();
			dictionary = ColorUtility.StripCache;
			lock (dictionary)
			{
				if (ColorUtility.StripCache.Count > 50)
				{
					ColorUtility.StripCache.Clear();
				}
				ColorUtility.StripCache[Text] = text2;
			}
			return text2;
		}

		// Token: 0x0600D2E5 RID: 53989 RVA: 0x004E3F64 File Offset: 0x004E2164
		public static string GetMainForegroundColor(string s)
		{
			if (s == null)
			{
				return null;
			}
			s = Markup.Transform(s, false);
			ColorUtility.ForegroundColorCounts.Clear();
			char c = 'y';
			int num = 0;
			char c2 = c;
			int num2 = 0;
			int i = 0;
			int length = s.Length;
			while (i < length)
			{
				bool flag = false;
				if (s[i] == '&')
				{
					i++;
					if (i < length)
					{
						if (s[i] == '&')
						{
							flag = true;
						}
						else if (s[i] != c)
						{
							ColorUtility.ForegroundColorCounts[c] = num;
							c = s[i];
							ColorUtility.ForegroundColorCounts.TryGetValue(c, out num);
						}
					}
				}
				else if (s[i] == '^')
				{
					i++;
					if (i < length && s[i] == '^')
					{
						flag = true;
					}
				}
				else if (s[i] != ' ')
				{
					flag = true;
				}
				if (flag)
				{
					num++;
					if (c == c2)
					{
						num2++;
					}
					else if (num > num2)
					{
						c2 = c;
						num2 = num;
					}
				}
				i++;
			}
			return c2.ToString() ?? "";
		}

		// Token: 0x0600D2E6 RID: 53990 RVA: 0x004E4074 File Offset: 0x004E2274
		public static string GetMainForegroundColor(StringBuilder s)
		{
			return ColorUtility.GetMainForegroundColor(s.ToString());
		}

		// Token: 0x0600D2E7 RID: 53991 RVA: 0x004E4084 File Offset: 0x004E2284
		public static bool HasFormatting(string Text)
		{
			if (Text.Contains("{{"))
			{
				return true;
			}
			int i = 0;
			int num = Text.Length - 1;
			while (i < num)
			{
				if (Text[i] == '&')
				{
					i++;
					if (i < Text.Length && Text[i] != '&')
					{
						return true;
					}
				}
				else if (Text[i] == '^')
				{
					i++;
					if (i < Text.Length && Text[i] != '^')
					{
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600D2E8 RID: 53992 RVA: 0x004E4100 File Offset: 0x004E2300
		public static bool HasFormatting(StringBuilder Text)
		{
			if (Text.Contains("{{"))
			{
				return true;
			}
			int i = 0;
			int num = Text.Length - 1;
			while (i < num)
			{
				if (Text[i] == '&')
				{
					i++;
					if (i < Text.Length && Text[i] != '&')
					{
						return true;
					}
				}
				else if (Text[i] == '^')
				{
					i++;
					if (i < Text.Length && Text[i] != '^')
					{
						return true;
					}
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600D2E9 RID: 53993 RVA: 0x004E417C File Offset: 0x004E237C
		private static void GetNext(string Text, int Len, int LenM1, ref int Pos, ref bool InControl, ref int ControlDepth, out char Ch, out bool Found, StringBuilder ControlStore = null)
		{
			Ch = '\0';
			Found = false;
			while (Pos < Len)
			{
				char c = Text[Pos];
				if (!InControl)
				{
					if (Pos < LenM1)
					{
						if (c == '{' && Text[Pos + 1] == '{')
						{
							if (ControlStore != null)
							{
								ControlStore.Append("{{");
							}
							InControl = true;
							ControlDepth++;
							Pos++;
							Pos++;
							continue;
						}
						if (ControlDepth > 0 && c == '}' && Text[Pos + 1] == '}')
						{
							if (ControlStore != null)
							{
								ControlStore.Append("}}");
							}
							ControlDepth--;
							Pos++;
							Pos++;
							continue;
						}
					}
					if (Pos < LenM1)
					{
						if (c == '&')
						{
							if (ControlStore != null)
							{
								ControlStore.Append(c);
							}
							Pos++;
							c = Text[Pos];
							if (c != '&')
							{
								if (ControlStore != null)
								{
									ControlStore.Append(c);
								}
								Pos++;
								continue;
							}
						}
						else if (c == '^')
						{
							if (ControlStore != null)
							{
								ControlStore.Append(c);
							}
							Pos++;
							c = Text[Pos];
							if (c != '^')
							{
								if (ControlStore != null)
								{
									ControlStore.Append(c);
								}
								Pos++;
								continue;
							}
						}
					}
					Ch = c;
					Found = true;
					Pos++;
					return;
				}
				if (ControlStore != null)
				{
					ControlStore.Append(c);
				}
				if (c == '|')
				{
					InControl = false;
				}
				Pos++;
			}
		}

		// Token: 0x0600D2EA RID: 53994 RVA: 0x004E42E4 File Offset: 0x004E24E4
		private static void GetNext(StringBuilder Text, int Len, int LenM1, ref int Pos, ref bool InControl, ref int ControlDepth, out char Ch, out bool Found, StringBuilder ControlStore = null)
		{
			Ch = '\0';
			Found = false;
			while (Pos < Len)
			{
				char c = Text[Pos];
				if (!InControl)
				{
					if (Pos < LenM1)
					{
						if (c == '{' && Text[Pos + 1] == '{')
						{
							if (ControlStore != null)
							{
								ControlStore.Append("{{");
							}
							InControl = true;
							ControlDepth++;
							Pos++;
							Pos++;
							continue;
						}
						if (ControlDepth > 0 && c == '}' && Text[Pos + 1] == '}')
						{
							if (ControlStore != null)
							{
								ControlStore.Append("}}");
							}
							ControlDepth--;
							Pos++;
							Pos++;
							continue;
						}
					}
					if (Pos < LenM1)
					{
						if (c == '&')
						{
							if (ControlStore != null)
							{
								ControlStore.Append(c);
							}
							Pos++;
							c = Text[Pos];
							if (c != '&')
							{
								if (ControlStore != null)
								{
									ControlStore.Append(c);
								}
								Pos++;
								continue;
							}
						}
						else if (c == '^')
						{
							if (ControlStore != null)
							{
								ControlStore.Append(c);
							}
							Pos++;
							c = Text[Pos];
							if (c != '^')
							{
								if (ControlStore != null)
								{
									ControlStore.Append(c);
								}
								Pos++;
								continue;
							}
						}
					}
					Ch = c;
					Found = true;
					Pos++;
					return;
				}
				if (ControlStore != null)
				{
					ControlStore.Append(c);
				}
				if (c == '|')
				{
					InControl = false;
				}
				Pos++;
			}
		}

		// Token: 0x0600D2EB RID: 53995 RVA: 0x004E444C File Offset: 0x004E264C
		public static char FirstCharacterExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return '\0';
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			char c;
			bool flag2;
			ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, null);
			return c;
		}

		// Token: 0x0600D2EC RID: 53996 RVA: 0x004E4488 File Offset: 0x004E2688
		public static char LastCharacterExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return '\0';
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			char c = '\0';
			bool flag2;
			do
			{
				char c2;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c2, out flag2, null);
				if (flag2)
				{
					c = c2;
				}
			}
			while (flag2);
			return c;
		}

		// Token: 0x0600D2ED RID: 53997 RVA: 0x004E44D0 File Offset: 0x004E26D0
		public static int LengthExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return 0;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			int num4 = 0;
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, null);
				if (flag2)
				{
					num4++;
				}
			}
			while (flag2);
			return num4;
		}

		// Token: 0x0600D2EE RID: 53998 RVA: 0x004E451C File Offset: 0x004E271C
		public static int LengthExceptFormatting(StringBuilder Text)
		{
			if (Text == null)
			{
				return 0;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			int num4 = 0;
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, null);
				if (flag2)
				{
					num4++;
				}
			}
			while (flag2);
			return num4;
		}

		// Token: 0x0600D2EF RID: 53999 RVA: 0x004E4568 File Offset: 0x004E2768
		public static string ClipExceptFormatting(string Text, int Want)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			ColorUtility.ClipSB.Clear();
			int num4 = 0;
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, ColorUtility.ClipSB);
				if (flag2)
				{
					ColorUtility.ClipSB.Append(c);
					num4++;
				}
			}
			while (flag2 && num4 < Want);
			for (int i = 0; i < num; i++)
			{
				ColorUtility.ClipSB.Append("}}");
			}
			return ColorUtility.ClipSB.ToString();
		}

		// Token: 0x0600D2F0 RID: 54000 RVA: 0x004E45FC File Offset: 0x004E27FC
		public static string ClipExceptFormatting(StringBuilder Text, int Want)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			ColorUtility.ClipSB.Clear();
			int num4 = 0;
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, ColorUtility.ClipSB);
				if (flag2)
				{
					ColorUtility.ClipSB.Append(c);
					num4++;
				}
			}
			while (flag2 && num4 < Want);
			for (int i = 0; i < num; i++)
			{
				ColorUtility.ClipSB.Append("}}");
			}
			return ColorUtility.ClipSB.ToString();
		}

		// Token: 0x0600D2F1 RID: 54001 RVA: 0x004E4690 File Offset: 0x004E2890
		public static string ClipToFirstExceptFormatting(string Text, char Ch)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			ColorUtility.ClipSB.Clear();
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, ColorUtility.ClipSB);
				if (flag2)
				{
					if (c == Ch)
					{
						break;
					}
					ColorUtility.ClipSB.Append(c);
				}
			}
			while (flag2);
			for (int i = 0; i < num; i++)
			{
				ColorUtility.ClipSB.Append("}}");
			}
			return ColorUtility.ClipSB.ToString();
		}

		// Token: 0x0600D2F2 RID: 54002 RVA: 0x004E471C File Offset: 0x004E291C
		public static string ClipToFirstExceptFormatting(StringBuilder Text, char Ch)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			ColorUtility.ClipSB.Clear();
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, ColorUtility.ClipSB);
				if (flag2)
				{
					if (c == Ch)
					{
						break;
					}
					ColorUtility.ClipSB.Append(c);
				}
			}
			while (flag2);
			for (int i = 0; i < num; i++)
			{
				ColorUtility.ClipSB.Append("}}");
			}
			return ColorUtility.ClipSB.ToString();
		}

		// Token: 0x0600D2F3 RID: 54003 RVA: 0x004E47A8 File Offset: 0x004E29A8
		public static string EscapeFormatting(string s)
		{
			if (s == null)
			{
				return null;
			}
			ColorUtility.EscSB.Clear();
			bool flag = false;
			int num = 0;
			int i = 0;
			int length = s.Length;
			int num2 = length - 1;
			while (i < length)
			{
				ColorUtility.EscSB.Append(s[i]);
				if (flag)
				{
					if (s[i] == '|')
					{
						flag = false;
					}
				}
				else
				{
					if (i < num2)
					{
						if (s[i] == '{' && s[i + 1] == '{')
						{
							ColorUtility.EscSB.Append("\\{");
							flag = true;
							num++;
							i++;
							goto IL_00F2;
						}
						if (num > 0 && s[i] == '}' && s[i + 1] == '}')
						{
							ColorUtility.EscSB.Append("\\}");
							num--;
							i++;
							goto IL_00F2;
						}
					}
					if (s[i] == '&')
					{
						ColorUtility.EscSB.Append('&');
					}
					else if (s[i] == '^')
					{
						ColorUtility.EscSB.Append('^');
					}
				}
				IL_00F2:
				i++;
			}
			return ColorUtility.EscSB.ToString();
		}

		// Token: 0x0600D2F4 RID: 54004 RVA: 0x004E48BC File Offset: 0x004E2ABC
		public static string EscapeFormatting(StringBuilder s)
		{
			if (s == null)
			{
				return null;
			}
			ColorUtility.EscSB.Clear();
			bool flag = false;
			int num = 0;
			int i = 0;
			int length = s.Length;
			int num2 = length - 1;
			while (i < length)
			{
				ColorUtility.EscSB.Append(s[i]);
				if (flag)
				{
					if (s[i] == '|')
					{
						flag = false;
					}
				}
				else
				{
					if (i < num2)
					{
						if (s[i] == '{' && s[i + 1] == '{')
						{
							ColorUtility.EscSB.Append("\\{");
							flag = true;
							num++;
							i++;
							goto IL_00F2;
						}
						if (num > 0 && s[i] == '}' && s[i + 1] == '}')
						{
							ColorUtility.EscSB.Append("\\}");
							num--;
							i++;
							goto IL_00F2;
						}
					}
					if (s[i] == '&')
					{
						ColorUtility.EscSB.Append('&');
					}
					else if (s[i] == '^')
					{
						ColorUtility.EscSB.Append('^');
					}
				}
				IL_00F2:
				i++;
			}
			return ColorUtility.EscSB.ToString();
		}

		// Token: 0x0600D2F5 RID: 54005 RVA: 0x004E49D0 File Offset: 0x004E2BD0
		public static string EscapeNonMarkupFormatting(string s)
		{
			if (s == null)
			{
				return null;
			}
			ColorUtility.EscSB.Clear();
			bool flag = false;
			int num = 0;
			int i = 0;
			int length = s.Length;
			int num2 = length - 1;
			while (i < length)
			{
				ColorUtility.EscSB.Append(s[i]);
				if (flag)
				{
					if (s[i] == '|')
					{
						flag = false;
					}
				}
				else
				{
					if (i < num2)
					{
						if (s[i] == '{' && s[i + 1] == '{')
						{
							ColorUtility.EscSB.Append('{');
							flag = true;
							num++;
							i++;
							goto IL_00EC;
						}
						if (num > 0 && s[i] == '}' && s[i + 1] == '}')
						{
							ColorUtility.EscSB.Append('}');
							num--;
							i++;
							goto IL_00EC;
						}
					}
					if (s[i] == '&')
					{
						ColorUtility.EscSB.Append('&');
					}
					else if (s[i] == '^')
					{
						ColorUtility.EscSB.Append('^');
					}
				}
				IL_00EC:
				i++;
			}
			return ColorUtility.EscSB.ToString();
		}

		// Token: 0x0600D2F6 RID: 54006 RVA: 0x004E4AE0 File Offset: 0x004E2CE0
		public static string EscapeNonMarkupFormatting(StringBuilder s)
		{
			if (s == null)
			{
				return null;
			}
			ColorUtility.EscSB.Clear();
			bool flag = false;
			int num = 0;
			int i = 0;
			int length = s.Length;
			int num2 = length - 1;
			while (i < length)
			{
				ColorUtility.EscSB.Append(s[i]);
				if (flag)
				{
					if (s[i] == '|')
					{
						flag = false;
					}
				}
				else
				{
					if (i < num2)
					{
						if (s[i] == '{' && s[i + 1] == '{')
						{
							ColorUtility.EscSB.Append('{');
							flag = true;
							num++;
							i++;
							goto IL_00EC;
						}
						if (num > 0 && s[i] == '}' && s[i + 1] == '}')
						{
							ColorUtility.EscSB.Append('}');
							num--;
							i++;
							goto IL_00EC;
						}
					}
					if (s[i] == '&')
					{
						ColorUtility.EscSB.Append('&');
					}
					else if (s[i] == '^')
					{
						ColorUtility.EscSB.Append('^');
					}
				}
				IL_00EC:
				i++;
			}
			return ColorUtility.EscSB.ToString();
		}

		// Token: 0x0600D2F7 RID: 54007 RVA: 0x004E4BF0 File Offset: 0x004E2DF0
		public static string ToUpperExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			StringBuilder stringBuilder = The.StringBuilder;
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, stringBuilder);
				if (flag2)
				{
					stringBuilder.Append(char.ToUpper(c));
				}
			}
			while (flag2);
			return stringBuilder.ToString();
		}

		// Token: 0x0600D2F8 RID: 54008 RVA: 0x004E4C50 File Offset: 0x004E2E50
		public static string ToLowerExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			StringBuilder stringBuilder = The.StringBuilder;
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, stringBuilder);
				if (flag2)
				{
					stringBuilder.Append(char.ToLower(c));
				}
			}
			while (flag2);
			return stringBuilder.ToString();
		}

		// Token: 0x0600D2F9 RID: 54009 RVA: 0x004E4CB0 File Offset: 0x004E2EB0
		public static string ReplaceExceptFormatting(string Text, char Search, char Replace)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			StringBuilder stringBuilder = The.StringBuilder;
			bool flag2;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, stringBuilder);
				if (flag2)
				{
					if (c == Search)
					{
						c = Replace;
					}
					stringBuilder.Append(c);
				}
			}
			while (flag2);
			return stringBuilder.ToString();
		}

		// Token: 0x0600D2FA RID: 54010 RVA: 0x004E4D10 File Offset: 0x004E2F10
		public static string CapitalizeExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			bool flag2 = true;
			StringBuilder stringBuilder = The.StringBuilder;
			bool flag3;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag3, stringBuilder);
				if (flag3)
				{
					if (flag2)
					{
						c = char.ToUpper(c);
						flag2 = false;
					}
					stringBuilder.Append(c);
				}
			}
			while (flag3);
			return stringBuilder.ToString();
		}

		// Token: 0x0600D2FB RID: 54011 RVA: 0x004E4D7C File Offset: 0x004E2F7C
		public static string UncapitalizeExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			bool flag2 = true;
			StringBuilder stringBuilder = The.StringBuilder;
			bool flag3;
			do
			{
				char c;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag3, stringBuilder);
				if (flag3)
				{
					if (flag2)
					{
						if (char.IsUpper(c))
						{
							c = char.ToLower(c);
							flag2 = false;
						}
						else if (char.IsLower(c) || char.IsDigit(c))
						{
							flag2 = false;
						}
					}
					stringBuilder.Append(c);
				}
			}
			while (flag3);
			return stringBuilder.ToString();
		}

		// Token: 0x0600D2FC RID: 54012 RVA: 0x004E4E08 File Offset: 0x004E3008
		public static string ApplyColor(string String, string Color)
		{
			if (Color.IsNullOrEmpty())
			{
				return String;
			}
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				utf16ValueStringBuilder.Append("{{");
				utf16ValueStringBuilder.Append(Color);
				utf16ValueStringBuilder.Append('|');
				utf16ValueStringBuilder.Append(String);
				utf16ValueStringBuilder.Append("}}");
				text = utf16ValueStringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600D2FD RID: 54013 RVA: 0x004E4E88 File Offset: 0x004E3088
		public static string ApplyColor(string String, char Color)
		{
			if (Color == ' ')
			{
				return String;
			}
			return ColorUtility.ColorSB.Clear().Append("{{").Append(Color)
				.Append('|')
				.Append(String)
				.Append("}}")
				.ToString();
		}

		// Token: 0x0600D2FE RID: 54014 RVA: 0x004E4EC7 File Offset: 0x004E30C7
		public static void SortExceptFormatting(List<string> List)
		{
			List.Sort(new Comparison<string>(ColorUtility.CompareExceptFormatting));
		}

		// Token: 0x0600D2FF RID: 54015 RVA: 0x004E4EDC File Offset: 0x004E30DC
		public static int CompareExceptFormattingNoCase(string A, string B)
		{
			if (A == null)
			{
				if (B == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (B == null)
				{
					return 1;
				}
				bool flag = false;
				int num = 0;
				bool flag2 = false;
				int num2 = 0;
				int length = A.Length;
				int num3 = length - 1;
				int length2 = B.Length;
				int num4 = length2 - 1;
				int num5 = 0;
				int num6 = 0;
				char c = ' ';
				char c2 = ' ';
				bool flag3 = false;
				bool flag4 = false;
				while (num5 < length || num6 < length2)
				{
					ColorUtility.GetNext(A, length, num3, ref num5, ref flag, ref num, out c, out flag3, null);
					ColorUtility.GetNext(B, length2, num4, ref num6, ref flag2, ref num2, out c2, out flag4, null);
					if (flag3 && flag4)
					{
						int num7 = char.ToUpperInvariant(c).CompareTo(char.ToUpperInvariant(c2));
						if (num7 != 0)
						{
							return num7;
						}
					}
					else
					{
						if (flag3)
						{
							return 1;
						}
						if (flag4)
						{
							return -1;
						}
						return 0;
					}
				}
				return 0;
			}
		}

		// Token: 0x0600D300 RID: 54016 RVA: 0x004E4FA0 File Offset: 0x004E31A0
		public static int CompareExceptFormatting(string A, string B)
		{
			if (A == null)
			{
				if (B == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (B == null)
				{
					return 1;
				}
				bool flag = false;
				int num = 0;
				bool flag2 = false;
				int num2 = 0;
				int length = A.Length;
				int num3 = length - 1;
				int length2 = B.Length;
				int num4 = length2 - 1;
				int num5 = 0;
				int num6 = 0;
				char c = ' ';
				char c2 = ' ';
				bool flag3 = false;
				bool flag4 = false;
				while (num5 < length || num6 < length2)
				{
					ColorUtility.GetNext(A, length, num3, ref num5, ref flag, ref num, out c, out flag3, null);
					ColorUtility.GetNext(B, length2, num4, ref num6, ref flag2, ref num2, out c2, out flag4, null);
					if (flag3 && flag4)
					{
						int num7 = c.CompareTo(c2);
						if (num7 != 0)
						{
							return num7;
						}
					}
					else
					{
						if (flag3)
						{
							return 1;
						}
						if (flag4)
						{
							return -1;
						}
						return 0;
					}
				}
				return 0;
			}
		}

		// Token: 0x0600D301 RID: 54017 RVA: 0x004E5054 File Offset: 0x004E3254
		public static void SortExceptFormattingAndCase(List<string> List)
		{
			if (List != null)
			{
				List.Sort(new Comparison<string>(ColorUtility.CompareExceptFormattingAndCase));
			}
		}

		// Token: 0x0600D302 RID: 54018 RVA: 0x004E506C File Offset: 0x004E326C
		public static int CompareExceptFormattingAndCase(string A, string B)
		{
			if (A == null)
			{
				if (B == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (B == null)
				{
					return 1;
				}
				bool flag = false;
				int num = 0;
				bool flag2 = false;
				int num2 = 0;
				int length = A.Length;
				int num3 = length - 1;
				int length2 = B.Length;
				int num4 = length2 - 1;
				int num5 = 0;
				int num6 = 0;
				char c = ' ';
				char c2 = ' ';
				bool flag3 = false;
				bool flag4 = false;
				while (num5 < length || num6 < length2)
				{
					ColorUtility.GetNext(A, length, num3, ref num5, ref flag, ref num, out c, out flag3, null);
					ColorUtility.GetNext(B, length2, num4, ref num6, ref flag2, ref num2, out c2, out flag4, null);
					if (flag3 && flag4)
					{
						if (char.IsLetter(c) && char.IsLetter(c2))
						{
							c = char.ToUpper(c);
							c2 = char.ToUpper(c2);
						}
						int num7 = c.CompareTo(c2);
						if (num7 != 0)
						{
							return num7;
						}
					}
					else
					{
						if (flag3)
						{
							return 1;
						}
						if (flag4)
						{
							return -1;
						}
						return 0;
					}
				}
				return 0;
			}
		}

		// Token: 0x0600D303 RID: 54019 RVA: 0x004E5147 File Offset: 0x004E3347
		public static bool EqualsExceptFormatting(string A, string B)
		{
			return ColorUtility.CompareExceptFormatting(A, B) == 0;
		}

		// Token: 0x0600D304 RID: 54020 RVA: 0x004E5153 File Offset: 0x004E3353
		public static bool EqualsExceptFormattingAndCase(string A, string B)
		{
			return ColorUtility.CompareExceptFormattingAndCase(A, B) == 0;
		}

		// Token: 0x0600D305 RID: 54021 RVA: 0x004E5160 File Offset: 0x004E3360
		public static bool BeginsWithExceptFormatting(string A, string B)
		{
			if (A == null)
			{
				return false;
			}
			if (B == null)
			{
				return false;
			}
			if (B == "")
			{
				return true;
			}
			if (A == "")
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			bool flag2 = false;
			int num2 = 0;
			int length = A.Length;
			int num3 = length - 1;
			int length2 = B.Length;
			int num4 = length2 - 1;
			int num5 = 0;
			int num6 = 0;
			char c = ' ';
			char c2 = ' ';
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			while (num5 < length || num6 < length2)
			{
				ColorUtility.GetNext(A, length, num3, ref num5, ref flag, ref num, out c, out flag3, null);
				ColorUtility.GetNext(B, length2, num4, ref num6, ref flag2, ref num2, out c2, out flag4, null);
				if (flag3 && flag4)
				{
					if (c != c2)
					{
						return false;
					}
					flag5 = true;
				}
				else
				{
					if (flag3 && !flag4)
					{
						flag5 = true;
						break;
					}
					break;
				}
			}
			return flag5;
		}

		// Token: 0x0600D306 RID: 54022 RVA: 0x004E522C File Offset: 0x004E342C
		public static bool HasUpperExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int i = 0;
			char c = ' ';
			bool flag2 = false;
			while (i < length)
			{
				ColorUtility.GetNext(Text, length, num2, ref i, ref flag, ref num, out c, out flag2, null);
				if (!flag2)
				{
					break;
				}
				if (char.IsUpper(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600D307 RID: 54023 RVA: 0x004E5284 File Offset: 0x004E3484
		public static bool HasLowerExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int i = 0;
			char c = ' ';
			bool flag2 = false;
			while (i < length)
			{
				ColorUtility.GetNext(Text, length, num2, ref i, ref flag, ref num, out c, out flag2, null);
				if (!flag2)
				{
					break;
				}
				if (char.IsLower(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600D308 RID: 54024 RVA: 0x004E52DC File Offset: 0x004E34DC
		public static bool IsAllUpperExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int i = 0;
			char c = ' ';
			bool flag2 = false;
			bool flag3 = false;
			while (i < length)
			{
				ColorUtility.GetNext(Text, length, num2, ref i, ref flag, ref num, out c, out flag2, null);
				if (!flag2)
				{
					break;
				}
				if (char.IsUpper(c))
				{
					flag3 = true;
				}
				else if (char.IsLower(c))
				{
					return false;
				}
			}
			return flag3;
		}

		// Token: 0x0600D309 RID: 54025 RVA: 0x004E5344 File Offset: 0x004E3544
		public static bool IsAllLowerExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int i = 0;
			char c = ' ';
			bool flag2 = false;
			bool flag3 = false;
			while (i < length)
			{
				ColorUtility.GetNext(Text, length, num2, ref i, ref flag, ref num, out c, out flag2, null);
				if (!flag2)
				{
					break;
				}
				if (char.IsLower(c))
				{
					flag3 = true;
				}
				else if (char.IsUpper(c))
				{
					return false;
				}
			}
			return flag3;
		}

		// Token: 0x0600D30A RID: 54026 RVA: 0x004E53AC File Offset: 0x004E35AC
		public static bool IsFirstUpperExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			char c;
			bool flag2;
			ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, null);
			return flag2 && char.IsUpper(c);
		}

		// Token: 0x0600D30B RID: 54027 RVA: 0x004E53F0 File Offset: 0x004E35F0
		public static bool IsFirstLowerExceptFormatting(string Text)
		{
			if (Text == null)
			{
				return false;
			}
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			char c;
			bool flag2;
			ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out c, out flag2, null);
			return flag2 && char.IsLower(c);
		}

		// Token: 0x0600D30C RID: 54028 RVA: 0x004E5434 File Offset: 0x004E3634
		public static void ComponentsFromAttributeColor(ushort Color, out float R, out float G, out float B)
		{
			R = 0f;
			G = 0f;
			B = 0f;
			if (Color != 0)
			{
				if (Color == 1)
				{
					B = 128f;
					return;
				}
				if (Color == 2)
				{
					G = 128f;
					return;
				}
				if (Color == 3)
				{
					B = 128f;
					G = 128f;
					return;
				}
				if (Color == 4)
				{
					R = 128f;
					return;
				}
				if (Color == 5)
				{
					R = 128f;
					B = 128f;
					return;
				}
				if (Color == 6)
				{
					G = 128f;
					R = 128f;
					return;
				}
				if (Color == 7)
				{
					R = 192f;
					G = 192f;
					B = 192f;
					return;
				}
				if (Color == 8)
				{
					R = 128f;
					G = 128f;
					B = 128f;
					return;
				}
				if (Color == 9)
				{
					B = 255f;
					return;
				}
				if (Color == 10)
				{
					G = 255f;
					return;
				}
				if (Color == 11)
				{
					B = 255f;
					G = 255f;
					return;
				}
				if (Color == 12)
				{
					R = 255f;
					return;
				}
				if (Color == 13)
				{
					R = 255f;
					B = 255f;
					return;
				}
				if (Color == 14)
				{
					G = 255f;
					R = 255f;
					return;
				}
				if (Color == 15)
				{
					R = 255f;
					G = 255f;
					B = 255f;
				}
			}
		}

		// Token: 0x0600D30D RID: 54029 RVA: 0x004E556A File Offset: 0x004E376A
		public static void ForegroundFromAttribute(ushort A, out float R, out float G, out float B)
		{
			ColorUtility.ComponentsFromAttributeColor(ColorUtility.GetForeground(A), out R, out G, out B);
		}

		// Token: 0x0600D30E RID: 54030 RVA: 0x004E557A File Offset: 0x004E377A
		public static void BackgroundFromAttribute(ushort A, out float R, out float G, out float B)
		{
			ColorUtility.ComponentsFromAttributeColor(ColorUtility.GetBackground(A), out R, out G, out B);
		}

		// Token: 0x0600D30F RID: 54031 RVA: 0x004E558C File Offset: 0x004E378C
		public static Color FromWebColor(string s)
		{
			if (s.StartsWith("#"))
			{
				s = s.Substring(1);
			}
			Color color;
			if (ColorUtility.webColors.TryGetValue(s, out color))
			{
				return color;
			}
			float num = (float)byte.Parse(s.Substring(0, 2), NumberStyles.HexNumber);
			float num2 = (float)byte.Parse(s.Substring(2, 2), NumberStyles.HexNumber);
			float num3 = (float)byte.Parse(s.Substring(4, 2), NumberStyles.HexNumber);
			ColorUtility.webColors.Add(s, new Color(num / 255f, num2 / 255f, num3 / 255f));
			return ColorUtility.webColors[s];
		}

		// Token: 0x0600D310 RID: 54032 RVA: 0x004E562C File Offset: 0x004E382C
		[ModSensitiveCacheInit]
		public static void Reinit()
		{
			if (!ColorUtility.ModInitialized)
			{
				ColorUtility.Init(!ColorUtility.BaseInitialized, true, false);
				return;
			}
			ColorUtility.Init(true, true, true);
		}

		// Token: 0x0600D311 RID: 54033 RVA: 0x004E5650 File Offset: 0x004E3850
		[return: TupleElementNames(new string[] { "path", "mod" })]
		private static List<ValueTuple<string, ModInfo>> GetPaths(bool Base, bool Mod)
		{
			List<ValueTuple<string, ModInfo>> paths = new List<ValueTuple<string, ModInfo>>();
			if (Base)
			{
				paths.Add(new ValueTuple<string, ModInfo>(DataManager.FilePath("Display.txt"), null));
			}
			if (Mod)
			{
				ModManager.ForEachFile("Display.txt", delegate(string path, ModInfo mod)
				{
					paths.Add(new ValueTuple<string, ModInfo>(path, mod));
				}, false, false);
			}
			try
			{
				string text = DataManager.SavePath("Display.txt");
				if (File.Exists(text))
				{
					paths.Add(new ValueTuple<string, ModInfo>(text, null));
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Error getting save path for Display.txt", ex);
			}
			return paths;
		}

		// Token: 0x0600D312 RID: 54034 RVA: 0x004E56F4 File Offset: 0x004E38F4
		private static void LoadDisplaySettings(string Path, GameManager Manager, GameObject MainCamera)
		{
			JSONClass jsonclass = JSON.Parse(File.ReadAllText(Path)) as JSONClass;
			JSONClass jsonclass2 = jsonclass["colors"] as JSONClass;
			if (jsonclass2 != null)
			{
				foreach (object obj in jsonclass2)
				{
					KeyValuePair<string, JSONNode> keyValuePair = (KeyValuePair<string, JSONNode>)obj;
					Color color = ColorUtility.FromWebColor(keyValuePair.Value);
					if (ColorUtility.ColorMap.ContainsKey(keyValuePair.Key[0]))
					{
						ColorUtility.ColorToCharMap.Remove(ColorUtility.ColorMap[keyValuePair.Key[0]]);
						ColorUtility.ColorMap.Remove(keyValuePair.Key[0]);
					}
					ColorUtility.FastColorMap[(int)keyValuePair.Key[0]] = color;
					ColorUtility.ColorMap.Add(keyValuePair.Key[0], color);
					ColorUtility.ColorToCharMap.Add(color, keyValuePair.Key[0]);
				}
			}
			if (!MainCamera)
			{
				return;
			}
			JSONClass jsonclass3 = jsonclass["camera"] as JSONClass;
			if (jsonclass3 != null && jsonclass3["background"] != null)
			{
				Camera component = MainCamera.GetComponent<Camera>();
				component.backgroundColor = ColorUtility.FromWebColor(jsonclass3["background"]);
				ColorUtility.ColorAliasMap[ColorUtility.CAMERA_BACKGROUND] = component.backgroundColor;
			}
			JSONClass jsonclass4 = jsonclass["tiles"] as JSONClass;
			if (jsonclass4 != null)
			{
				if (jsonclass4["width"] != null)
				{
					Manager.tileWidth = Convert.ToInt32(jsonclass4["width"]);
					LetterboxCamera component2 = MainCamera.GetComponent<LetterboxCamera>();
					component2.InitialDesiredWidth = (float)(Manager.tileWidth * 80);
					component2.DesiredWidth = (float)(Manager.tileWidth * 80);
					component2.Refresh();
				}
				if (jsonclass4["height"] != null)
				{
					Manager.tileHeight = Convert.ToInt32(jsonclass4["height"]);
				}
			}
			JSONClass jsonclass5 = jsonclass["shaders"] as JSONClass;
			if (jsonclass5 != null)
			{
				if (jsonclass5["scanlines"] != null)
				{
					CC_AnalogTV component3 = MainCamera.GetComponent<CC_AnalogTV>();
					if (jsonclass5["scanlines"]["enable"] != null)
					{
						if (jsonclass5["scanlines"]["enable"].Value.EqualsNoCase("true"))
						{
							component3.enabled = true;
						}
						else
						{
							component3.enabled = false;
						}
					}
					if (jsonclass5["scanlines"]["greyscale"] != null)
					{
						component3.grayscale = jsonclass5["scanlines"]["greyscale"].Value.EqualsNoCase("true");
					}
					if (jsonclass5["scanlines"]["noise"] != null)
					{
						component3.noiseIntensity = Convert.ToSingle(jsonclass5["scanlines"]["noise"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["scanlines"]["intensity"] != null)
					{
						component3.scanlinesIntensity = Convert.ToSingle(jsonclass5["scanlines"]["intensity"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["scanlines"]["count"] != null)
					{
						component3.scanlinesCount = Convert.ToSingle(jsonclass5["scanlines"]["count"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["scanlines"]["offset"] != null)
					{
						component3.scanlinesOffset = Convert.ToSingle(jsonclass5["scanlines"]["offset"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["scanlines"]["distortion"] != null)
					{
						component3.distortion = Convert.ToSingle(jsonclass5["scanlines"]["distortion"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["scanlines"]["cubicdistortion"] != null)
					{
						component3.cubicDistortion = Convert.ToSingle(jsonclass5["scanlines"]["cubicdistortion"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["scanlines"]["zoom"] != null)
					{
						component3.scale = Convert.ToSingle(jsonclass5["scanlines"]["zoom"].Value, CultureInfo.InvariantCulture);
					}
				}
				if (jsonclass5["vignette"] != null)
				{
					CC_FastVignette component4 = MainCamera.GetComponent<CC_FastVignette>();
					if (jsonclass5["vignette"]["enable"] != null)
					{
						if (jsonclass5["vignette"]["enable"].Value.EqualsNoCase("true"))
						{
							component4.enabled = true;
						}
						else
						{
							component4.enabled = false;
						}
					}
					if (jsonclass5["vignette"]["sharpness"] != null)
					{
						component4.sharpness = Convert.ToSingle(jsonclass5["vignette"]["sharpness"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["vignette"]["darkness"] != null)
					{
						component4.darkness = Convert.ToSingle(jsonclass5["vignette"]["darkness"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["vignette"]["desaturate"] != null)
					{
						component4.desaturate = jsonclass5["vignette"]["desaturate"].Value.EqualsNoCase("true");
					}
				}
				if (jsonclass5["settings"] != null)
				{
					CC_BrightnessContrastGamma component5 = MainCamera.GetComponent<CC_BrightnessContrastGamma>();
					if (jsonclass5["settings"]["enable"] != null)
					{
						if (jsonclass5["settings"]["enable"].Value.EqualsNoCase("true"))
						{
							component5.enabled = true;
						}
						else
						{
							component5.enabled = false;
						}
					}
					if (jsonclass5["settings"]["brightness"] != null)
					{
						component5.brightness = Convert.ToSingle(jsonclass5["settings"]["brightness"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["settings"]["contrast"] != null)
					{
						component5.contrast = Convert.ToSingle(jsonclass5["settings"]["contrast"].Value, CultureInfo.InvariantCulture);
					}
					if (jsonclass5["settings"]["gamma"] != null)
					{
						component5.gamma = Convert.ToSingle(jsonclass5["settings"]["gamma"].Value, CultureInfo.InvariantCulture);
					}
				}
			}
		}

		// Token: 0x0600D313 RID: 54035 RVA: 0x004E5EB8 File Offset: 0x004E40B8
		internal static void LoadBaseColors()
		{
			if (ColorUtility.BaseInitialized)
			{
				return;
			}
			ColorUtility.Init(true, false, false);
			ColorUtility.BaseInitialized = true;
		}

		// Token: 0x0600D314 RID: 54036 RVA: 0x004E5ED0 File Offset: 0x004E40D0
		internal static void LoadModColors()
		{
			if (ColorUtility.ModInitialized)
			{
				return;
			}
			ColorUtility.Init(false, true, false);
			ColorUtility.ModInitialized = true;
		}

		// Token: 0x0600D315 RID: 54037 RVA: 0x004E5EE8 File Offset: 0x004E40E8
		private static void ClearMaps()
		{
			ColorUtility.ColorMap.Clear();
			ColorUtility.ColorToCharMap.Clear();
			ColorUtility.CharToColorMap.Clear();
			ColorUtility.ColorAttributeToCharMap.Clear();
			ColorUtility.ColorAliasMap.Clear();
			ColorUtility.usfColorMap.Fill(default(Color));
		}

		// Token: 0x0600D316 RID: 54038 RVA: 0x004E5F3C File Offset: 0x004E413C
		private static void Init(bool Base = true, bool Mod = true, bool Clear = true)
		{
			if (Clear)
			{
				ColorUtility.ClearMaps();
			}
			GameObject gameObject = GameObject.Find("GameManager");
			GameManager gameManager = ((gameObject != null) ? gameObject.GetComponent<GameManager>() : null);
			GameObject gameObject2 = GameObject.Find("Main Camera");
			foreach (ValueTuple<string, ModInfo> valueTuple in ColorUtility.GetPaths(Base, Mod))
			{
				string item = valueTuple.Item1;
				ModInfo item2 = valueTuple.Item2;
				try
				{
					ColorUtility.LoadDisplaySettings(item, gameManager, gameObject2);
				}
				catch (Exception ex)
				{
					if (item2 != null)
					{
						item2.Error(string.Format("Error Loading {0}\n{1}", item, ex));
					}
					else
					{
						MetricsManager.LogException("Error loading " + item, ex, "game_exception");
					}
				}
			}
			ColorUtility.ColorCollection.Load(ColorUtility.Colors);
			ColorUtility.usfColorMap[0] = ColorUtility.Colors.DarkBlack;
			ColorUtility.usfColorMap[1] = ColorUtility.Colors.DarkBlue;
			ColorUtility.usfColorMap[4] = ColorUtility.Colors.DarkRed;
			ColorUtility.usfColorMap[2] = ColorUtility.Colors.DarkGreen;
			ColorUtility.usfColorMap[3] = ColorUtility.Colors.DarkCyan;
			ColorUtility.usfColorMap[5] = ColorUtility.Colors.DarkMagenta;
			ColorUtility.usfColorMap[6] = ColorUtility.Colors.Brown;
			ColorUtility.usfColorMap[7] = ColorUtility.Colors.Gray;
			ColorUtility.usfColorMap[8] = ColorUtility.Colors.DarkOrange;
			ColorUtility.usfColorMap[16] = ColorUtility.Colors.Black;
			ColorUtility.usfColorMap[17] = ColorUtility.Colors.Blue;
			ColorUtility.usfColorMap[20] = ColorUtility.Colors.Red;
			ColorUtility.usfColorMap[18] = ColorUtility.Colors.Green;
			ColorUtility.usfColorMap[19] = ColorUtility.Colors.Cyan;
			ColorUtility.usfColorMap[21] = ColorUtility.Colors.Magenta;
			ColorUtility.usfColorMap[22] = ColorUtility.Colors.Yellow;
			ColorUtility.usfColorMap[23] = ColorUtility.Colors.White;
			ColorUtility.usfColorMap[24] = ColorUtility.Colors.Orange;
			ColorUtility.CharToColorMap['k'] = 0;
			ColorUtility.CharToColorMap['b'] = 1;
			ColorUtility.CharToColorMap['r'] = 4;
			ColorUtility.CharToColorMap['g'] = 2;
			ColorUtility.CharToColorMap['c'] = 3;
			ColorUtility.CharToColorMap['m'] = 5;
			ColorUtility.CharToColorMap['w'] = 6;
			ColorUtility.CharToColorMap['y'] = 7;
			ColorUtility.CharToColorMap['o'] = 8;
			ColorUtility.CharToColorMap['K'] = ColorUtility.Bright(0);
			ColorUtility.CharToColorMap['B'] = ColorUtility.Bright(1);
			ColorUtility.CharToColorMap['R'] = ColorUtility.Bright(4);
			ColorUtility.CharToColorMap['G'] = ColorUtility.Bright(2);
			ColorUtility.CharToColorMap['C'] = ColorUtility.Bright(3);
			ColorUtility.CharToColorMap['M'] = ColorUtility.Bright(5);
			ColorUtility.CharToColorMap['W'] = ColorUtility.Bright(6);
			ColorUtility.CharToColorMap['Y'] = ColorUtility.Bright(7);
			ColorUtility.CharToColorMap['O'] = ColorUtility.Bright(8);
			ColorUtility.ColorAttributeToCharMap[0] = 'k';
			ColorUtility.ColorAttributeToCharMap[1] = 'b';
			ColorUtility.ColorAttributeToCharMap[4] = 'r';
			ColorUtility.ColorAttributeToCharMap[2] = 'g';
			ColorUtility.ColorAttributeToCharMap[3] = 'c';
			ColorUtility.ColorAttributeToCharMap[5] = 'm';
			ColorUtility.ColorAttributeToCharMap[6] = 'w';
			ColorUtility.ColorAttributeToCharMap[7] = 'y';
			ColorUtility.ColorAttributeToCharMap[8] = 'o';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(0)] = 'K';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(1)] = 'B';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(4)] = 'R';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(2)] = 'G';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(3)] = 'C';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(5)] = 'M';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(6)] = 'W';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(7)] = 'Y';
			ColorUtility.ColorAttributeToCharMap[ColorUtility.Bright(8)] = 'O';
			ColorUtility.ColorAliasMap[ColorUtility.DEFAULT_FOREGROUND] = ColorUtility.Colors.DefaultForeground;
			ColorUtility.ColorAliasMap[ColorUtility.DEFAULT_BACKGROUND] = ColorUtility.Colors.DefaultBackground;
			ColorUtility.ColorAliasMap[ColorUtility.DEFAULT_DETAIL] = ColorUtility.Colors.DefaultDetail;
		}

		// Token: 0x0600D317 RID: 54039 RVA: 0x004E6414 File Offset: 0x004E4614
		public static ushort MakeColor(ushort foreground, ushort background)
		{
			return (ushort)((int)foreground + ((int)background << 5));
		}

		// Token: 0x0600D318 RID: 54040 RVA: 0x004E641C File Offset: 0x004E461C
		public static ushort MakeColor(TextColor foreground, TextColor background)
		{
			return (ushort)(foreground + (ushort)(background << 5));
		}

		// Token: 0x0600D319 RID: 54041 RVA: 0x004E6424 File Offset: 0x004E4624
		public static ushort MakeColor(ushort foreground, TextColor background)
		{
			return (ushort)(foreground + (background << 5));
		}

		// Token: 0x0600D31A RID: 54042 RVA: 0x004E642C File Offset: 0x004E462C
		public static ushort MakeColor(TextColor foreground, ushort background)
		{
			return (ushort)(foreground + (ushort)(background << 5));
		}

		// Token: 0x0600D31B RID: 54043 RVA: 0x004E6434 File Offset: 0x004E4634
		public static ushort Bright(ushort c)
		{
			return c | 16;
		}

		// Token: 0x0600D31C RID: 54044 RVA: 0x004E643B File Offset: 0x004E463B
		public static ushort Bright(TextColor c)
		{
			return (ushort)(c | (TextColor)16);
		}

		// Token: 0x0600D31D RID: 54045 RVA: 0x004E6442 File Offset: 0x004E4642
		public static ushort MakeBackgroundColor(ushort c)
		{
			return (ushort)(c << 5);
		}

		// Token: 0x0600D31E RID: 54046 RVA: 0x004E6448 File Offset: 0x004E4648
		public static ushort MakeBackgroundColor(TextColor c)
		{
			return (ushort)(c << 5);
		}

		// Token: 0x0600D31F RID: 54047 RVA: 0x004E6450 File Offset: 0x004E4650
		public static char ParseForegroundColor(string str, char defaultColor = 'y')
		{
			if (string.IsNullOrEmpty(str))
			{
				return 'y';
			}
			char c = defaultColor;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == '&')
				{
					i++;
					if (i < str.Length && str[i] != '&')
					{
						c = str[i];
					}
				}
			}
			return c;
		}

		// Token: 0x0600D320 RID: 54048 RVA: 0x004E64A8 File Offset: 0x004E46A8
		public static char? FindLastForeground(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			char? c = null;
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				if (text[i] == '&')
				{
					i++;
					if (i < length && text[i] != '&')
					{
						c = new char?(text[i]);
					}
				}
				i++;
			}
			return c;
		}

		// Token: 0x0600D321 RID: 54049 RVA: 0x004E6510 File Offset: 0x004E4710
		public static void FindLastForeground(string text, ref char? result)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				if (text[i] == '&')
				{
					i++;
					if (i < length && text[i] != '&')
					{
						result = new char?(text[i]);
					}
				}
				i++;
			}
		}

		// Token: 0x0600D322 RID: 54050 RVA: 0x004E656C File Offset: 0x004E476C
		public static char? FindLastBackground(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			char? c = null;
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				if (text[i] == '^')
				{
					i++;
					if (i < length && text[i] != '^')
					{
						c = new char?(text[i]);
					}
				}
				i++;
			}
			return c;
		}

		// Token: 0x0600D323 RID: 54051 RVA: 0x004E65D4 File Offset: 0x004E47D4
		public static void FindLastBackground(string text, ref char? result)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				if (text[i] == '^')
				{
					i++;
					if (i < length && text[i] != '^')
					{
						result = new char?(text[i]);
					}
				}
				i++;
			}
		}

		// Token: 0x0600D324 RID: 54052 RVA: 0x004E6630 File Offset: 0x004E4830
		public unsafe static void FindLastForegroundAndBackground(ReadOnlySpan<char> text, ref char? foreground, ref char? background)
		{
			if (text.IsEmpty)
			{
				return;
			}
			int i = 0;
			int length = text.Length;
			while (i < length)
			{
				if (*text[i] == 38)
				{
					i++;
					if (i < length && *text[i] != 38)
					{
						foreground = new char?((char)(*text[i]));
					}
				}
				else if (*text[i] == 94)
				{
					i++;
					if (i < length && *text[i] != 94)
					{
						background = new char?((char)(*text[i]));
					}
				}
				i++;
			}
		}

		// Token: 0x0600D325 RID: 54053 RVA: 0x004E66C9 File Offset: 0x004E48C9
		public static ushort GetForeground(ushort c)
		{
			return c & 31;
		}

		// Token: 0x0600D326 RID: 54054 RVA: 0x004E66D0 File Offset: 0x004E48D0
		public static ushort GetBackground(ushort c)
		{
			return (ushort)(c >> 5);
		}

		// Token: 0x0600D327 RID: 54055 RVA: 0x004E66D8 File Offset: 0x004E48D8
		public static List<string> CachedForegroundExpansion(string Text, char Separator = ',')
		{
			if (Text == null)
			{
				return null;
			}
			if (Text == ColorUtility.LastForegroundExpansionRequest)
			{
				return ColorUtility.LastForegroundExpansionResult;
			}
			List<string> list;
			if (!ColorUtility.CachedForegroundExpansions.TryGetValue(Text, out list))
			{
				string[] array = Text.Split(Separator, StringSplitOptions.None);
				list = new List<string>(array.Length);
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					list.Add("&" + array[i]);
					i++;
				}
				ColorUtility.CachedForegroundExpansions.Add(Text, list);
			}
			ColorUtility.LastForegroundExpansionRequest = Text;
			return ColorUtility.LastForegroundExpansionResult = list;
		}

		// Token: 0x0600D328 RID: 54056 RVA: 0x004E675C File Offset: 0x004E495C
		public static List<string> CachedBackgroundExpansion(string Text, char Separator = ',')
		{
			if (Text == null)
			{
				return null;
			}
			if (Text == ColorUtility.LastBackgroundExpansionRequest)
			{
				return ColorUtility.LastBackgroundExpansionResult;
			}
			List<string> list;
			if (!ColorUtility.CachedBackgroundExpansions.TryGetValue(Text, out list))
			{
				string[] array = Text.Split(Separator, StringSplitOptions.None);
				list = new List<string>(array.Length);
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					list.Add("^" + array[i]);
					i++;
				}
				ColorUtility.CachedBackgroundExpansions.Add(Text, list);
			}
			ColorUtility.LastBackgroundExpansionRequest = Text;
			return ColorUtility.LastBackgroundExpansionResult = list;
		}

		// Token: 0x0600D329 RID: 54057 RVA: 0x004E67E0 File Offset: 0x004E49E0
		public static void StripFormatting(ReadOnlySpan<char> Input, TextBuilder Output)
		{
			bool flag = false;
			int num = 0;
			int length = Input.Length;
			int num2 = length - 1;
			int num3 = 0;
			Output.Clear();
			bool flag2;
			do
			{
				ReadOnlySpan<char> readOnlySpan;
				ColorUtility.GetNext(Input, length, num2, ref num3, ref flag, ref num, out readOnlySpan, out flag2, null);
				if (flag2)
				{
					Output.Append(readOnlySpan);
				}
			}
			while (flag2);
		}

		// Token: 0x0600D32A RID: 54058 RVA: 0x004E682C File Offset: 0x004E4A2C
		public static int LengthExceptFormatting(ReadOnlySpan<char> Text)
		{
			bool flag = false;
			int num = 0;
			int length = Text.Length;
			int num2 = length - 1;
			int num3 = 0;
			int num4 = 0;
			bool flag2;
			do
			{
				ReadOnlySpan<char> readOnlySpan;
				ColorUtility.GetNext(Text, length, num2, ref num3, ref flag, ref num, out readOnlySpan, out flag2, null);
				if (flag2)
				{
					num4++;
				}
			}
			while (flag2);
			return num4;
		}

		// Token: 0x0600D32B RID: 54059 RVA: 0x004E6874 File Offset: 0x004E4A74
		private unsafe static void GetNext(ReadOnlySpan<char> Text, int Len, int LenM1, ref int Pos, ref bool InControl, ref int ControlDepth, out ReadOnlySpan<char> Ch, out bool Found, StringBuilder ControlStore = null)
		{
			Ch = null;
			Found = false;
			while (Pos < Len)
			{
				int num = Pos;
				Pos = num + 1;
				ReadOnlySpan<char> readOnlySpan = Text.Slice(num, 1);
				if (!InControl)
				{
					if (Pos < Len)
					{
						if (*readOnlySpan[0] == 123 && *Text[Pos] == 123)
						{
							if (ControlStore != null)
							{
								ControlStore.Append("{{");
							}
							InControl = true;
							ControlDepth++;
							Pos++;
							continue;
						}
						if (ControlDepth > 0 && *readOnlySpan[0] == 125 && *Text[Pos] == 125)
						{
							if (ControlStore != null)
							{
								ControlStore.Append("}}");
							}
							ControlDepth--;
							Pos++;
							continue;
						}
					}
					if (Pos < Len)
					{
						if (*readOnlySpan[0] == 38)
						{
							if (ControlStore != null)
							{
								ControlStore.Append(readOnlySpan);
							}
							num = Pos;
							Pos = num + 1;
							readOnlySpan = Text.Slice(num, 1);
							if (*readOnlySpan[0] != 38)
							{
								if (ControlStore != null)
								{
									ControlStore.Append(readOnlySpan);
									continue;
								}
								continue;
							}
						}
						else if (*readOnlySpan[0] == 94)
						{
							if (ControlStore != null)
							{
								ControlStore.Append(readOnlySpan);
							}
							num = Pos;
							Pos = num + 1;
							readOnlySpan = Text.Slice(num, 1);
							if (*readOnlySpan[0] != 94)
							{
								if (ControlStore != null)
								{
									ControlStore.Append(readOnlySpan);
									continue;
								}
								continue;
							}
						}
					}
					Ch = readOnlySpan;
					Found = true;
					return;
				}
				if (ControlStore != null)
				{
					ControlStore.Append(readOnlySpan);
				}
				if (*readOnlySpan[0] == 124)
				{
					InControl = false;
				}
			}
		}

		// Token: 0x040053B3 RID: 21427
		public static readonly string DEFAULT_FOREGROUND = "default foreground";

		// Token: 0x040053B4 RID: 21428
		public static readonly string DEFAULT_BACKGROUND = "default background";

		// Token: 0x040053B5 RID: 21429
		public static readonly string DEFAULT_DETAIL = "default detail";

		// Token: 0x040053B6 RID: 21430
		public static readonly string CAMERA_BACKGROUND = "camera background";

		// Token: 0x040053B7 RID: 21431
		public static ColorUtility.ColorCollection Colors = new ColorUtility.ColorCollection();

		// Token: 0x040053B8 RID: 21432
		public static Dictionary<char, ushort> CharToColorMap = new Dictionary<char, ushort>();

		// Token: 0x040053B9 RID: 21433
		public static Dictionary<ushort, char> ColorAttributeToCharMap = new Dictionary<ushort, char>();

		// Token: 0x040053BA RID: 21434
		public static Dictionary<Color, char> ColorToCharMap = new Dictionary<Color, char>();

		// Token: 0x040053BB RID: 21435
		public static Dictionary<string, Color> ColorAliasMap = new Dictionary<string, Color>();

		// Token: 0x040053BC RID: 21436
		public static Dictionary<char, Color> _ColorMap = new Dictionary<char, Color>();

		// Token: 0x040053BD RID: 21437
		public static Color[] usfColorMap = new Color[32];

		// Token: 0x040053BE RID: 21438
		private static bool BaseInitialized;

		// Token: 0x040053BF RID: 21439
		private static bool ModInitialized;

		// Token: 0x040053C0 RID: 21440
		public static Color[] FastColorMap = new Color[257];

		// Token: 0x040053C1 RID: 21441
		private static StringBuilder StripSB = new StringBuilder(256);

		// Token: 0x040053C2 RID: 21442
		private static Dictionary<string, string> StripCache = new Dictionary<string, string>();

		// Token: 0x040053C3 RID: 21443
		private static Dictionary<char, int> ForegroundColorCounts = new Dictionary<char, int>();

		// Token: 0x040053C4 RID: 21444
		private static StringBuilder ClipSB = new StringBuilder(256);

		// Token: 0x040053C5 RID: 21445
		private static StringBuilder EscSB = new StringBuilder(256);

		// Token: 0x040053C6 RID: 21446
		private static StringBuilder ColorSB = new StringBuilder(256);

		// Token: 0x040053C7 RID: 21447
		private static Dictionary<string, Color> webColors = new Dictionary<string, Color>();

		// Token: 0x040053C8 RID: 21448
		private static Dictionary<string, List<string>> CachedForegroundExpansions = new Dictionary<string, List<string>>(16);

		// Token: 0x040053C9 RID: 21449
		private static string LastForegroundExpansionRequest;

		// Token: 0x040053CA RID: 21450
		private static List<string> LastForegroundExpansionResult;

		// Token: 0x040053CB RID: 21451
		private static Dictionary<string, List<string>> CachedBackgroundExpansions = new Dictionary<string, List<string>>(16);

		// Token: 0x040053CC RID: 21452
		private static string LastBackgroundExpansionRequest;

		// Token: 0x040053CD RID: 21453
		private static List<string> LastBackgroundExpansionResult;

		// Token: 0x02001E36 RID: 7734
		public class ColorCollection
		{
			// Token: 0x17001005 RID: 4101
			// (get) Token: 0x0600F688 RID: 63112 RVA: 0x0055A55F File Offset: 0x0055875F
			// (set) Token: 0x0600F689 RID: 63113 RVA: 0x0055A567 File Offset: 0x00558767
			public Color DarkBlack { get; private set; }

			// Token: 0x17001006 RID: 4102
			// (get) Token: 0x0600F68A RID: 63114 RVA: 0x0055A570 File Offset: 0x00558770
			// (set) Token: 0x0600F68B RID: 63115 RVA: 0x0055A578 File Offset: 0x00558778
			public Color DarkBlue { get; private set; }

			// Token: 0x17001007 RID: 4103
			// (get) Token: 0x0600F68C RID: 63116 RVA: 0x0055A581 File Offset: 0x00558781
			// (set) Token: 0x0600F68D RID: 63117 RVA: 0x0055A589 File Offset: 0x00558789
			public Color DarkGreen { get; private set; }

			// Token: 0x17001008 RID: 4104
			// (get) Token: 0x0600F68E RID: 63118 RVA: 0x0055A592 File Offset: 0x00558792
			// (set) Token: 0x0600F68F RID: 63119 RVA: 0x0055A59A File Offset: 0x0055879A
			public Color DarkCyan { get; private set; }

			// Token: 0x17001009 RID: 4105
			// (get) Token: 0x0600F690 RID: 63120 RVA: 0x0055A5A3 File Offset: 0x005587A3
			// (set) Token: 0x0600F691 RID: 63121 RVA: 0x0055A5AB File Offset: 0x005587AB
			public Color DarkRed { get; private set; }

			// Token: 0x1700100A RID: 4106
			// (get) Token: 0x0600F692 RID: 63122 RVA: 0x0055A5B4 File Offset: 0x005587B4
			// (set) Token: 0x0600F693 RID: 63123 RVA: 0x0055A5BC File Offset: 0x005587BC
			public Color DarkMagenta { get; private set; }

			// Token: 0x1700100B RID: 4107
			// (get) Token: 0x0600F694 RID: 63124 RVA: 0x0055A5C5 File Offset: 0x005587C5
			// (set) Token: 0x0600F695 RID: 63125 RVA: 0x0055A5CD File Offset: 0x005587CD
			public Color Brown { get; private set; }

			// Token: 0x1700100C RID: 4108
			// (get) Token: 0x0600F696 RID: 63126 RVA: 0x0055A5D6 File Offset: 0x005587D6
			// (set) Token: 0x0600F697 RID: 63127 RVA: 0x0055A5DE File Offset: 0x005587DE
			public Color Gray { get; private set; }

			// Token: 0x1700100D RID: 4109
			// (get) Token: 0x0600F698 RID: 63128 RVA: 0x0055A5E7 File Offset: 0x005587E7
			// (set) Token: 0x0600F699 RID: 63129 RVA: 0x0055A5EF File Offset: 0x005587EF
			public Color DarkOrange { get; private set; }

			// Token: 0x1700100E RID: 4110
			// (get) Token: 0x0600F69A RID: 63130 RVA: 0x0055A5F8 File Offset: 0x005587F8
			// (set) Token: 0x0600F69B RID: 63131 RVA: 0x0055A600 File Offset: 0x00558800
			public Color Black { get; private set; }

			// Token: 0x1700100F RID: 4111
			// (get) Token: 0x0600F69C RID: 63132 RVA: 0x0055A609 File Offset: 0x00558809
			// (set) Token: 0x0600F69D RID: 63133 RVA: 0x0055A611 File Offset: 0x00558811
			public Color Blue { get; private set; }

			// Token: 0x17001010 RID: 4112
			// (get) Token: 0x0600F69E RID: 63134 RVA: 0x0055A61A File Offset: 0x0055881A
			// (set) Token: 0x0600F69F RID: 63135 RVA: 0x0055A622 File Offset: 0x00558822
			public Color Green { get; private set; }

			// Token: 0x17001011 RID: 4113
			// (get) Token: 0x0600F6A0 RID: 63136 RVA: 0x0055A62B File Offset: 0x0055882B
			// (set) Token: 0x0600F6A1 RID: 63137 RVA: 0x0055A633 File Offset: 0x00558833
			public Color Cyan { get; private set; }

			// Token: 0x17001012 RID: 4114
			// (get) Token: 0x0600F6A2 RID: 63138 RVA: 0x0055A63C File Offset: 0x0055883C
			// (set) Token: 0x0600F6A3 RID: 63139 RVA: 0x0055A644 File Offset: 0x00558844
			public Color Red { get; private set; }

			// Token: 0x17001013 RID: 4115
			// (get) Token: 0x0600F6A4 RID: 63140 RVA: 0x0055A64D File Offset: 0x0055884D
			// (set) Token: 0x0600F6A5 RID: 63141 RVA: 0x0055A655 File Offset: 0x00558855
			public Color Magenta { get; private set; }

			// Token: 0x17001014 RID: 4116
			// (get) Token: 0x0600F6A6 RID: 63142 RVA: 0x0055A65E File Offset: 0x0055885E
			// (set) Token: 0x0600F6A7 RID: 63143 RVA: 0x0055A666 File Offset: 0x00558866
			public Color Yellow { get; private set; }

			// Token: 0x17001015 RID: 4117
			// (get) Token: 0x0600F6A8 RID: 63144 RVA: 0x0055A66F File Offset: 0x0055886F
			// (set) Token: 0x0600F6A9 RID: 63145 RVA: 0x0055A677 File Offset: 0x00558877
			public Color White { get; private set; }

			// Token: 0x17001016 RID: 4118
			// (get) Token: 0x0600F6AA RID: 63146 RVA: 0x0055A680 File Offset: 0x00558880
			// (set) Token: 0x0600F6AB RID: 63147 RVA: 0x0055A688 File Offset: 0x00558888
			public Color Orange { get; private set; }

			// Token: 0x17001017 RID: 4119
			// (get) Token: 0x0600F6AC RID: 63148 RVA: 0x0055A691 File Offset: 0x00558891
			// (set) Token: 0x0600F6AD RID: 63149 RVA: 0x0055A699 File Offset: 0x00558899
			public Color k { get; private set; }

			// Token: 0x17001018 RID: 4120
			// (get) Token: 0x0600F6AE RID: 63150 RVA: 0x0055A6A2 File Offset: 0x005588A2
			// (set) Token: 0x0600F6AF RID: 63151 RVA: 0x0055A6AA File Offset: 0x005588AA
			public Color b { get; private set; }

			// Token: 0x17001019 RID: 4121
			// (get) Token: 0x0600F6B0 RID: 63152 RVA: 0x0055A6B3 File Offset: 0x005588B3
			// (set) Token: 0x0600F6B1 RID: 63153 RVA: 0x0055A6BB File Offset: 0x005588BB
			public Color g { get; private set; }

			// Token: 0x1700101A RID: 4122
			// (get) Token: 0x0600F6B2 RID: 63154 RVA: 0x0055A6C4 File Offset: 0x005588C4
			// (set) Token: 0x0600F6B3 RID: 63155 RVA: 0x0055A6CC File Offset: 0x005588CC
			public Color c { get; private set; }

			// Token: 0x1700101B RID: 4123
			// (get) Token: 0x0600F6B4 RID: 63156 RVA: 0x0055A6D5 File Offset: 0x005588D5
			// (set) Token: 0x0600F6B5 RID: 63157 RVA: 0x0055A6DD File Offset: 0x005588DD
			public Color r { get; private set; }

			// Token: 0x1700101C RID: 4124
			// (get) Token: 0x0600F6B6 RID: 63158 RVA: 0x0055A6E6 File Offset: 0x005588E6
			// (set) Token: 0x0600F6B7 RID: 63159 RVA: 0x0055A6EE File Offset: 0x005588EE
			public Color m { get; private set; }

			// Token: 0x1700101D RID: 4125
			// (get) Token: 0x0600F6B8 RID: 63160 RVA: 0x0055A6F7 File Offset: 0x005588F7
			// (set) Token: 0x0600F6B9 RID: 63161 RVA: 0x0055A6FF File Offset: 0x005588FF
			public Color w { get; private set; }

			// Token: 0x1700101E RID: 4126
			// (get) Token: 0x0600F6BA RID: 63162 RVA: 0x0055A708 File Offset: 0x00558908
			// (set) Token: 0x0600F6BB RID: 63163 RVA: 0x0055A710 File Offset: 0x00558910
			public Color y { get; private set; }

			// Token: 0x1700101F RID: 4127
			// (get) Token: 0x0600F6BC RID: 63164 RVA: 0x0055A719 File Offset: 0x00558919
			// (set) Token: 0x0600F6BD RID: 63165 RVA: 0x0055A721 File Offset: 0x00558921
			public Color o { get; private set; }

			// Token: 0x17001020 RID: 4128
			// (get) Token: 0x0600F6BE RID: 63166 RVA: 0x0055A72A File Offset: 0x0055892A
			// (set) Token: 0x0600F6BF RID: 63167 RVA: 0x0055A732 File Offset: 0x00558932
			public Color K { get; private set; }

			// Token: 0x17001021 RID: 4129
			// (get) Token: 0x0600F6C0 RID: 63168 RVA: 0x0055A73B File Offset: 0x0055893B
			// (set) Token: 0x0600F6C1 RID: 63169 RVA: 0x0055A743 File Offset: 0x00558943
			public Color B { get; private set; }

			// Token: 0x17001022 RID: 4130
			// (get) Token: 0x0600F6C2 RID: 63170 RVA: 0x0055A74C File Offset: 0x0055894C
			// (set) Token: 0x0600F6C3 RID: 63171 RVA: 0x0055A754 File Offset: 0x00558954
			public Color G { get; private set; }

			// Token: 0x17001023 RID: 4131
			// (get) Token: 0x0600F6C4 RID: 63172 RVA: 0x0055A75D File Offset: 0x0055895D
			// (set) Token: 0x0600F6C5 RID: 63173 RVA: 0x0055A765 File Offset: 0x00558965
			public Color C { get; private set; }

			// Token: 0x17001024 RID: 4132
			// (get) Token: 0x0600F6C6 RID: 63174 RVA: 0x0055A76E File Offset: 0x0055896E
			// (set) Token: 0x0600F6C7 RID: 63175 RVA: 0x0055A776 File Offset: 0x00558976
			public Color R { get; private set; }

			// Token: 0x17001025 RID: 4133
			// (get) Token: 0x0600F6C8 RID: 63176 RVA: 0x0055A77F File Offset: 0x0055897F
			// (set) Token: 0x0600F6C9 RID: 63177 RVA: 0x0055A787 File Offset: 0x00558987
			public Color M { get; private set; }

			// Token: 0x17001026 RID: 4134
			// (get) Token: 0x0600F6CA RID: 63178 RVA: 0x0055A790 File Offset: 0x00558990
			// (set) Token: 0x0600F6CB RID: 63179 RVA: 0x0055A798 File Offset: 0x00558998
			public Color W { get; private set; }

			// Token: 0x17001027 RID: 4135
			// (get) Token: 0x0600F6CC RID: 63180 RVA: 0x0055A7A1 File Offset: 0x005589A1
			// (set) Token: 0x0600F6CD RID: 63181 RVA: 0x0055A7A9 File Offset: 0x005589A9
			public Color Y { get; private set; }

			// Token: 0x17001028 RID: 4136
			// (get) Token: 0x0600F6CE RID: 63182 RVA: 0x0055A7B2 File Offset: 0x005589B2
			// (set) Token: 0x0600F6CF RID: 63183 RVA: 0x0055A7BA File Offset: 0x005589BA
			public Color O { get; private set; }

			// Token: 0x17001029 RID: 4137
			// (get) Token: 0x0600F6D0 RID: 63184 RVA: 0x0055A7C3 File Offset: 0x005589C3
			// (set) Token: 0x0600F6D1 RID: 63185 RVA: 0x0055A7CB File Offset: 0x005589CB
			public Color DefaultForeground { get; private set; }

			// Token: 0x1700102A RID: 4138
			// (get) Token: 0x0600F6D2 RID: 63186 RVA: 0x0055A7D4 File Offset: 0x005589D4
			// (set) Token: 0x0600F6D3 RID: 63187 RVA: 0x0055A7DC File Offset: 0x005589DC
			public Color DefaultBackground { get; private set; }

			// Token: 0x1700102B RID: 4139
			// (get) Token: 0x0600F6D4 RID: 63188 RVA: 0x0055A7E5 File Offset: 0x005589E5
			// (set) Token: 0x0600F6D5 RID: 63189 RVA: 0x0055A7ED File Offset: 0x005589ED
			public Color DefaultDetail { get; private set; }

			// Token: 0x1700102C RID: 4140
			// (get) Token: 0x0600F6D6 RID: 63190 RVA: 0x0055A7F6 File Offset: 0x005589F6
			// (set) Token: 0x0600F6D7 RID: 63191 RVA: 0x0055A7FE File Offset: 0x005589FE
			public Color CameraBackground { get; private set; }

			// Token: 0x1700102D RID: 4141
			public Color this[char Key]
			{
				get
				{
					return ColorUtility.ColorMap[Key];
				}
			}

			// Token: 0x0600F6D9 RID: 63193 RVA: 0x0055A814 File Offset: 0x00558A14
			private void Load()
			{
				this.DarkBlack = (this.k = ColorUtility.ColorMap['k']);
				this.DarkBlue = (this.b = ColorUtility.ColorMap['b']);
				this.DarkGreen = (this.g = ColorUtility.ColorMap['g']);
				this.DarkCyan = (this.c = ColorUtility.ColorMap['c']);
				this.DarkRed = (this.r = ColorUtility.ColorMap['r']);
				this.DarkMagenta = (this.m = ColorUtility.ColorMap['m']);
				this.Brown = (this.w = ColorUtility.ColorMap['w']);
				this.Gray = (this.y = ColorUtility.ColorMap['y']);
				this.DarkOrange = (this.o = ColorUtility.ColorMap['o']);
				this.Black = (this.K = ColorUtility.ColorMap['K']);
				this.Blue = (this.B = ColorUtility.ColorMap['B']);
				this.Green = (this.G = ColorUtility.ColorMap['G']);
				this.Cyan = (this.C = ColorUtility.ColorMap['C']);
				this.Red = (this.R = ColorUtility.ColorMap['R']);
				this.Magenta = (this.M = ColorUtility.ColorMap['M']);
				this.Yellow = (this.W = ColorUtility.ColorMap['W']);
				this.White = (this.Y = ColorUtility.ColorMap['Y']);
				this.Orange = (this.O = ColorUtility.ColorMap['O']);
				this.DefaultForeground = this.Gray;
				this.DefaultBackground = this.DarkBlack;
				this.DefaultDetail = this.Yellow;
				this.CameraBackground = ColorUtility.ColorAliasMap.GetValue(ColorUtility.CAMERA_BACKGROUND, default(Color));
			}

			// Token: 0x0600F6DA RID: 63194 RVA: 0x0055AA49 File Offset: 0x00558C49
			public static void Load(ColorUtility.ColorCollection Colors)
			{
				Colors.Load();
			}
		}
	}
}
