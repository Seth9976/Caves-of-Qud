using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;

namespace XRL.UI
{
	// Token: 0x020014D4 RID: 5332
	public class StringFormat
	{
		// Token: 0x0600D0DB RID: 53467 RVA: 0x004D70C0 File Offset: 0x004D52C0
		public static int GetNextWordLength(string Input, ref int Position, ref bool Newline, ref bool inControl, ref int controlDepth, ref int numControls)
		{
			int num = 0;
			Newline = false;
			while (Position < Input.Length)
			{
				char c = Input[Position];
				if (inControl)
				{
					if (c == '|')
					{
						inControl = false;
					}
					Position++;
				}
				else
				{
					if (Position < Input.Length - 1)
					{
						if (c == '{' && Input[Position + 1] == '{')
						{
							inControl = true;
							controlDepth++;
							numControls++;
							Position += 2;
							continue;
						}
						if (controlDepth > 0 && c == '}' && Input[Position + 1] == '}')
						{
							controlDepth--;
							Position += 2;
							continue;
						}
					}
					if (c == ' ')
					{
						break;
					}
					if (c == '\r')
					{
						Position++;
						if (Position < Input.Length && Input[Position] == '\n')
						{
							Newline = true;
							break;
						}
						num++;
					}
					else
					{
						if (c == '\n')
						{
							Newline = true;
							break;
						}
						if (c == '&' || c == '^')
						{
							Position++;
							if (Position >= Input.Length || Input[Position] == c)
							{
								num++;
							}
						}
						else
						{
							num++;
						}
						Position++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600D0DC RID: 53468 RVA: 0x004D71DC File Offset: 0x004D53DC
		public static int GetNextWordLength(StringBuilder Input, ref int Position, ref bool Newline, ref bool inControl, ref int controlDepth, ref int numControls)
		{
			int num = 0;
			Newline = false;
			while (Position < Input.Length)
			{
				char c = Input[Position];
				if (inControl)
				{
					if (c == '|')
					{
						inControl = false;
					}
					Position++;
				}
				else
				{
					if (Position < Input.Length - 1)
					{
						if (c == '{' && Input[Position + 1] == '{')
						{
							inControl = true;
							controlDepth++;
							numControls++;
							Position += 2;
							continue;
						}
						if (controlDepth > 0 && c == '}' && Input[Position + 1] == '}')
						{
							controlDepth--;
							Position += 2;
							continue;
						}
					}
					if (c == ' ')
					{
						break;
					}
					if (c == '\n')
					{
						Newline = true;
						break;
					}
					if (c == '&' || c == '^')
					{
						Position++;
						if (Position >= Input.Length || Input[Position] == c)
						{
							num++;
						}
					}
					else
					{
						num++;
					}
					Position++;
				}
			}
			return num;
		}

		// Token: 0x0600D0DD RID: 53469 RVA: 0x004D72C8 File Offset: 0x004D54C8
		public static string SubstringNotCountingFormat(string Input, int Start, int Length)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			int num3 = 0;
			while (num2 < Start && num < Input.Length)
			{
				char c = Input[num];
				if (flag)
				{
					if (c == '|')
					{
						flag = false;
					}
					num++;
				}
				else
				{
					if (num < Input.Length - 1)
					{
						if (c == '{' && Input[num + 1] == '{')
						{
							flag = true;
							num3++;
							num += 2;
							continue;
						}
						if (num3 > 0 && c == '}' && Input[num + 1] == '}')
						{
							num3--;
							num += 2;
							continue;
						}
					}
					if (c == '&' || c == '^')
					{
						num++;
						if (num < Input.Length && Input[num] != c)
						{
							num++;
							continue;
						}
					}
					num2++;
					num++;
				}
			}
			int num4 = num;
			num2 = 0;
			while (num2 < Length && num < Input.Length)
			{
				char c2 = Input[num];
				if (flag)
				{
					if (c2 == '|')
					{
						flag = false;
					}
					num++;
				}
				else
				{
					if (num < Input.Length - 1)
					{
						if (c2 == '{' && Input[num + 1] == '{')
						{
							flag = true;
							num3++;
							num += 2;
							continue;
						}
						if (num3 > 0 && c2 == '}' && Input[num + 1] == '}')
						{
							num3--;
							num += 2;
							continue;
						}
					}
					if (c2 == '&' || c2 == '^')
					{
						num++;
						if (num < Input.Length && Input[num] != c2)
						{
							num++;
							continue;
						}
					}
					num2++;
					num++;
				}
			}
			int num5 = num;
			return Input.Substring(num4, num5);
		}

		// Token: 0x0600D0DE RID: 53470 RVA: 0x004D7448 File Offset: 0x004D5648
		public static List<string> ClipTextToArray(string Input, int MaxWidth, out int MaxClippedWidth, bool KeepNewlines = false, bool KeepColorsAcrossNewlines = true, bool TransformMarkup = true, bool TransformMarkupIfMultipleLines = false)
		{
			StringBuilder stringBuilder = new StringBuilder((MaxWidth == int.MaxValue) ? 256 : MaxWidth);
			List<string> list = new List<string>(64);
			MaxClippedWidth = 0;
			int num = 0;
			int num2 = 0;
			int i = 0;
			bool flag = false;
			int num3 = 0;
			int num4 = 0;
			while (i < Input.Length)
			{
				int num5 = i;
				bool flag2 = false;
				int nextWordLength = StringFormat.GetNextWordLength(Input, ref i, ref flag2, ref flag, ref num3, ref num4);
				if (nextWordLength + num + ((num2 > 0) ? 1 : 0) > MaxWidth)
				{
					if (stringBuilder.Length > 0)
					{
						list.Add(stringBuilder.ToString());
					}
					if (num > MaxClippedWidth)
					{
						MaxClippedWidth = num;
					}
					stringBuilder.Clear().Append(Input.Substring(num5, i - num5));
					if (flag2 && KeepNewlines)
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Clear();
						if (num > MaxClippedWidth)
						{
							MaxClippedWidth = num;
						}
						num = 0;
						num2 = 0;
					}
					else
					{
						num = nextWordLength + 4;
						num2 = 1;
					}
				}
				else
				{
					if (num2 > 0)
					{
						stringBuilder.Append(' ');
						num++;
					}
					if (i > num5)
					{
						stringBuilder.Append(Input.Substring(num5, i - num5));
					}
					if (KeepNewlines && flag2)
					{
						list.Add(stringBuilder.ToString());
						stringBuilder.Length = 0;
						num += nextWordLength;
						if (num > MaxClippedWidth)
						{
							MaxClippedWidth = num;
						}
						num = 0;
						num2 = 0;
					}
					else if (nextWordLength > 0)
					{
						num += nextWordLength;
						num2++;
					}
				}
				i++;
			}
			for (int j = 0; j < num3; j++)
			{
				stringBuilder.Append("}}");
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(stringBuilder.ToString());
			}
			if (num > MaxClippedWidth)
			{
				MaxClippedWidth = num;
			}
			if ((TransformMarkup || (TransformMarkupIfMultipleLines && list.Count > 1)) && num4 > 0)
			{
				list = Markup.Transform(list, false);
			}
			if (KeepColorsAcrossNewlines)
			{
				char? c = null;
				char? c2 = null;
				for (int k = 0; k < list.Count; k++)
				{
					if (c != null)
					{
						List<string> list2 = list;
						int num6 = k;
						string text = "&";
						char? c3 = c;
						list2[num6] = text + c3.ToString() + list[k];
					}
					if (c2 != null)
					{
						List<string> list3 = list;
						int num7 = k;
						string text2 = "&";
						char? c3 = c2;
						list3[num7] = text2 + c3.ToString() + list[k];
					}
					for (int l = 0; l < list[k].Length - 1; l++)
					{
						if (list[k][l] == '&')
						{
							l++;
							if (list[k][l] != '&')
							{
								c = new char?(list[k][l]);
							}
						}
						if (list[k][l] == '^')
						{
							l++;
							if (list[k][l] != '^')
							{
								c2 = new char?(list[k][l]);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600D0DF RID: 53471 RVA: 0x004D7730 File Offset: 0x004D5930
		public static List<string> ClipTextToArray(string Input, int MaxWidth, bool KeepNewlines = false, bool KeepColorsAcrossNewlines = true)
		{
			int num;
			return StringFormat.ClipTextToArray(Input, MaxWidth, out num, KeepNewlines, KeepColorsAcrossNewlines, true, false);
		}

		// Token: 0x0600D0E0 RID: 53472 RVA: 0x004D774A File Offset: 0x004D594A
		public static string ClipLine(string Input, int MaxWidth, bool AddEllipsis = true, StringBuilder Return = null)
		{
			bool flag = Return != null;
			if (Return == null)
			{
				Return = new StringBuilder();
			}
			StringFormat.ClipLine(new StringBuilder(Input), MaxWidth, AddEllipsis, Return);
			if (!flag)
			{
				return Return.ToString();
			}
			return null;
		}

		// Token: 0x0600D0E1 RID: 53473 RVA: 0x004D7774 File Offset: 0x004D5974
		public static string ClipLine(StringBuilder Input, int MaxWidth, bool AddEllipsis = true, StringBuilder Return = null)
		{
			bool flag = Return != null;
			if (Return == null)
			{
				Return = new StringBuilder();
			}
			Return.Length = 0;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = 0;
			int i = 0;
			bool flag2 = false;
			int num3 = 0;
			int num4 = 0;
			while (i < Input.Length)
			{
				int num5 = i;
				bool flag3 = false;
				int num6 = num3;
				int nextWordLength = StringFormat.GetNextWordLength(Input, ref i, ref flag3, ref flag2, ref num6, ref num4);
				if (nextWordLength + num + ((num2 > 0) ? 1 : 0) > MaxWidth)
				{
					if (!AddEllipsis)
					{
						break;
					}
					if (nextWordLength + 3 <= MaxWidth)
					{
						for (int j = 0; j < num3; j++)
						{
							Return.Append("}}");
						}
						Return.Append("&y...");
						return Return.ToString();
					}
					StringBuilder stringBuilder2 = new StringBuilder();
					Return.Substring(0, Return.Length - 3, stringBuilder2);
					for (int k = 0; k < num3; k++)
					{
						stringBuilder2.Append("}}");
					}
					stringBuilder2.Append("&y...");
				}
				else
				{
					num3 = num6;
					if (num2 > 0)
					{
						Return.Append(' ');
						num++;
					}
					if (i > num5)
					{
						stringBuilder.Length = 0;
						Input.Substring(num5, i - num5, stringBuilder);
						Return.Append(stringBuilder);
					}
					if (flag3)
					{
						Return.Append('\n');
						num = 0;
						num2 = 0;
					}
					else
					{
						num += nextWordLength;
						num2++;
					}
				}
				i++;
			}
			for (int l = 0; l < num3; l++)
			{
				Return.Append("}}");
			}
			if (!flag)
			{
				return Return.ToString();
			}
			return null;
		}

		// Token: 0x0600D0E2 RID: 53474 RVA: 0x004D78F8 File Offset: 0x004D5AF8
		public static string ClipText(string Input, int MaxWidth, bool KeepNewlines = false, bool TransformMarkup = true, bool TransformMarkupIfMultipleLines = false)
		{
			StringBuilder stringBuilder = new StringBuilder(Input.Length);
			int num = 0;
			int num2 = 0;
			int i = 0;
			bool flag = false;
			int num3 = 0;
			int num4 = 0;
			int num5 = 1;
			while (i < Input.Length)
			{
				int num6 = i;
				bool flag2 = false;
				int nextWordLength = StringFormat.GetNextWordLength(Input, ref i, ref flag2, ref flag, ref num3, ref num4);
				if (nextWordLength + num + ((num2 > 0) ? 1 : 0) > MaxWidth)
				{
					stringBuilder.Append('\n');
					num5++;
					stringBuilder.Append(Input.Substring(num6, i - num6));
					if (flag2 && KeepNewlines)
					{
						stringBuilder.Append('\n');
						num5++;
						num = 0;
					}
					else
					{
						num = nextWordLength + 4;
					}
				}
				else
				{
					if (num2 > 0)
					{
						stringBuilder.Append(' ');
						num++;
					}
					if (i > num6)
					{
						stringBuilder.Append(Input.Substring(num6, i - num6));
					}
					if (KeepNewlines && flag2)
					{
						stringBuilder.Append('\n');
						num5++;
						num = 0;
						num2 = 0;
					}
					else
					{
						num += nextWordLength;
						num2++;
					}
				}
				i++;
			}
			if ((TransformMarkup || (TransformMarkupIfMultipleLines && num5 > 1)) && num4 > 0)
			{
				Markup.Transform(stringBuilder, false);
			}
			return stringBuilder.ToString();
		}
	}
}
