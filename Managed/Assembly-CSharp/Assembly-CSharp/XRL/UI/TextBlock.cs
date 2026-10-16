using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Cysharp.Text;
using Qud.UI;
using XRL.World.Text;

namespace XRL.UI
{
	// Token: 0x02001478 RID: 5240
	public class TextBlock
	{
		// Token: 0x0600CE47 RID: 52807 RVA: 0x004AE6EC File Offset: 0x004AC8EC
		public unsafe static void ParseSpan(ReadOnlySpan<char> String, TextBuilder Output, int Width, int MaxLines = 35, int AdjustFirstLine = 0)
		{
			int num = -AdjustFirstLine;
			int num2 = 0;
			int num3 = 0;
			Output.Clear();
			char? c = null;
			char? c2 = null;
			int i = 0;
			bool flag = false;
			while (i < String.Length)
			{
				int num4 = String.IndexOfAnyStartFrom(i + 1, " \n");
				bool flag2 = num4 != -1;
				int num5;
				if (num4 == -1)
				{
					num5 = String.Length - i;
				}
				else
				{
					num5 = num4 - i;
				}
				ReadOnlySpan<char> readOnlySpan = String.Slice(i, num5);
				int num6 = ColorUtility.LengthExceptFormatting(readOnlySpan);
				if (num6 + num + 1 > Width)
				{
					if (flag)
					{
						num6--;
						num5--;
						i++;
						readOnlySpan = String.Slice(i, num5);
					}
					if (Output.Length != num3)
					{
						num2++;
						if (num2 >= MaxLines)
						{
							break;
						}
						Output.Append('\n');
						num3 = Output.Length;
					}
					if (i >= String.Length)
					{
						break;
					}
					num = 0;
					if (c != null)
					{
						Output.Append('&');
						Output.Append(c);
					}
					if (c2 != null)
					{
						Output.Append('^');
						Output.Append(c2);
					}
				}
				num += num6;
				Output.Append(readOnlySpan);
				flag = flag2;
				i += num5;
				TextBlock.FindLastColorCodes(readOnlySpan, ref c, ref c2);
				while (i < String.Length && *String[i] == 10)
				{
					num2++;
					num = 0;
					if (num2 >= MaxLines)
					{
						break;
					}
					Output.Append('\n');
					num3 = Output.Length;
					if (c != null)
					{
						Output.Append('&');
						Output.Append(c);
					}
					if (c2 != null)
					{
						Output.Append('^');
						Output.Append(c2);
					}
					i++;
				}
			}
		}

		// Token: 0x0600CE48 RID: 52808 RVA: 0x004AE8B0 File Offset: 0x004ACAB0
		public void Format(string String)
		{
			if (String == null || String.Length == 0)
			{
				this.Lines.Clear();
				return;
			}
			this.ParseSpan(Markup.Transform(String, false));
		}

		// Token: 0x0600CE49 RID: 52809 RVA: 0x004AE8E0 File Offset: 0x004ACAE0
		public void Format(ReadOnlySpan<char> String)
		{
			this.Lines.Clear();
			this.ParseSpan(Markup.Transform(new string(String), false));
		}

		// Token: 0x0600CE4A RID: 52810 RVA: 0x004AE909 File Offset: 0x004ACB09
		public void Format(StringBuilder stringBuilder)
		{
			this.Lines.Clear();
			if (stringBuilder == null)
			{
				return;
			}
			this.ParseSpan(Markup.Transform(stringBuilder.ToString(), false));
		}

		// Token: 0x0600CE4B RID: 52811 RVA: 0x004AE938 File Offset: 0x004ACB38
		private unsafe void ParseSpan(ReadOnlySpan<char> String)
		{
			int num = -this.AdjustFirstLine;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				char? c = null;
				char? c2 = null;
				int i = 0;
				int width = this.Width;
				bool flag = false;
				while (i < String.Length)
				{
					int num2 = String.IndexOfAnyStartFrom(i + 1, " \n");
					bool flag2 = num2 != -1;
					int num3;
					if (num2 == -1)
					{
						num3 = String.Length - i;
					}
					else
					{
						num3 = num2 - i;
					}
					ReadOnlySpan<char> readOnlySpan = String.Slice(i, num3);
					int num4 = ColorUtility.LengthExceptFormatting(readOnlySpan);
					if (num4 + num + 1 > this.Width)
					{
						if (flag)
						{
							num4--;
							num3--;
							i++;
							readOnlySpan = String.Slice(i, num3);
						}
						if (this.Lines.Count > 0 || utf16ValueStringBuilder.Length > 0)
						{
							this.Lines.Add(utf16ValueStringBuilder.ToString());
						}
						utf16ValueStringBuilder.Clear();
						num = 0;
						if (this.Lines.Count >= this.MaxLines || i >= String.Length)
						{
							break;
						}
						if (c != null)
						{
							utf16ValueStringBuilder.Append('&');
							utf16ValueStringBuilder.Append<char?>(c);
						}
						if (c2 != null)
						{
							utf16ValueStringBuilder.Append('^');
							utf16ValueStringBuilder.Append<char?>(c2);
						}
					}
					num += num4;
					utf16ValueStringBuilder.Append(readOnlySpan);
					flag = flag2;
					i += num3;
					TextBlock.FindLastColorCodes(readOnlySpan, ref c, ref c2);
					while (i < String.Length && *String[i] == 10)
					{
						this.Lines.Add(utf16ValueStringBuilder.ToString());
						utf16ValueStringBuilder.Clear();
						num = 0;
						if (this.Lines.Count >= this.MaxLines)
						{
							break;
						}
						if (c != null)
						{
							utf16ValueStringBuilder.Append('&');
							utf16ValueStringBuilder.Append<char?>(c);
						}
						if (c2 != null)
						{
							utf16ValueStringBuilder.Append('^');
							utf16ValueStringBuilder.Append<char?>(c2);
						}
						i++;
					}
				}
				if (utf16ValueStringBuilder.Length > 0)
				{
					this.Lines.Add(utf16ValueStringBuilder.ToString());
				}
				if (this.ReverseBlocks)
				{
					this.Lines.Reverse();
				}
			}
		}

		// Token: 0x0600CE4C RID: 52812 RVA: 0x004AEBAC File Offset: 0x004ACDAC
		private unsafe static void FindLastColorCodes(ReadOnlySpan<char> span, ref char? Foreground, ref char? Background)
		{
			int i = 0;
			while (i < span.Length - 1)
			{
				if (*span[i] != 38)
				{
					goto IL_003C;
				}
				if (*span[i + 1] != 38)
				{
					Foreground = new char?((char)(*span[i + 1]));
					goto IL_003C;
				}
				i++;
				IL_0074:
				i++;
				continue;
				IL_003C:
				if (*span[i] != 94)
				{
					goto IL_0074;
				}
				if (*span[i + 1] == 94)
				{
					i++;
					goto IL_0074;
				}
				Background = new char?((char)(*span[i + 1]));
				goto IL_0074;
			}
		}

		// Token: 0x0600CE4D RID: 52813 RVA: 0x004AEC3D File Offset: 0x004ACE3D
		public TextBlock(string String, int Width, int MaxLines, bool ReverseBlocks = false, int AdjustFirstLine = 0)
		{
			this._Width = Width;
			this.MaxLines = MaxLines;
			this.ReverseBlocks = ReverseBlocks;
			this.AdjustFirstLine = AdjustFirstLine;
			this.Format(String);
		}

		// Token: 0x0600CE4E RID: 52814 RVA: 0x004AEC7C File Offset: 0x004ACE7C
		public TextBlock(ReadOnlySpan<char> Span, int Width, int MaxLines, bool ReverseBlocks = false, int AdjustFirstLine = 0)
		{
			this._Width = Width;
			this.MaxLines = MaxLines;
			this.ReverseBlocks = ReverseBlocks;
			this.AdjustFirstLine = AdjustFirstLine;
			this.Format(Span);
		}

		// Token: 0x0600CE4F RID: 52815 RVA: 0x004AECBB File Offset: 0x004ACEBB
		public TextBlock(StringBuilder String, int Width, int MaxLines, bool ReverseBlocks = false, int AdjustFirstLine = 0)
		{
			this._Width = Width;
			this.MaxLines = MaxLines;
			this.ReverseBlocks = ReverseBlocks;
			this.AdjustFirstLine = AdjustFirstLine;
			this.Format(String);
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x0600CE50 RID: 52816 RVA: 0x004AECFC File Offset: 0x004ACEFC
		public int Width
		{
			get
			{
				if (this._Width == -1)
				{
					foreach (string text in this.Lines)
					{
						if (text.Length > this._Width)
						{
							this._Width = text.Length;
						}
					}
				}
				return this._Width;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x0600CE51 RID: 52817 RVA: 0x004AED74 File Offset: 0x004ACF74
		public int Height
		{
			get
			{
				return this.Lines.Count;
			}
		}

		// Token: 0x0600CE52 RID: 52818 RVA: 0x004AED84 File Offset: 0x004ACF84
		public override string ToString()
		{
			string text;
			using (TextBuilder textBuilder = TextBuilder.Get(64))
			{
				for (int i = 0; i < this.Lines.Count; i++)
				{
					if (i > 0)
					{
						textBuilder.Append('\n');
					}
					textBuilder.Append(this.Lines[i]);
				}
				text = textBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600CE53 RID: 52819 RVA: 0x004AEDF4 File Offset: 0x004ACFF4
		public string GetRTFBlock()
		{
			string text;
			using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
			{
				for (int i = 0; i < this.Lines.Count; i++)
				{
					if (i > 0)
					{
						utf16ValueStringBuilder.Append('\n');
					}
					utf16ValueStringBuilder.Append(this.Lines[i]);
				}
				text = RTF.FormatToRTF(utf16ValueStringBuilder.ToString(), "FF", -1, false);
			}
			return text;
		}

		// Token: 0x04004FF1 RID: 20465
		public List<string> Lines = new List<string>();

		// Token: 0x04004FF2 RID: 20466
		private int MaxLines;

		// Token: 0x04004FF3 RID: 20467
		private bool ReverseBlocks;

		// Token: 0x04004FF4 RID: 20468
		public int AdjustFirstLine;

		// Token: 0x04004FF5 RID: 20469
		public int _Width = -1;
	}
}
