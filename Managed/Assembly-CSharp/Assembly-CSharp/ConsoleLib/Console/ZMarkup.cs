using System;
using System.Collections.Generic;
using System.Diagnostics;
using XRL.World.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200154B RID: 5451
	[DebuggerDisplay("Markup:{Action} {Children}")]
	public class ZMarkup : ZMarkupControlNode
	{
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600D492 RID: 54418 RVA: 0x004F01F0 File Offset: 0x004EE3F0
		private static string BaseColorPattern
		{
			get
			{
				if (ZMarkup._BaseColorPattern == null)
				{
					ZMarkup._BaseColorPattern = "&y^k";
				}
				return ZMarkup._BaseColorPattern;
			}
		}

		// Token: 0x0600D493 RID: 54419 RVA: 0x004F0208 File Offset: 0x004EE408
		public static char SignedValueTriColor(int value)
		{
			if (value > 0)
			{
				return 'G';
			}
			if (value >= 0)
			{
				return 'c';
			}
			return 'R';
		}

		// Token: 0x0600D494 RID: 54420 RVA: 0x004F021A File Offset: 0x004EE41A
		public ZMarkup()
		{
		}

		// Token: 0x0600D495 RID: 54421 RVA: 0x004F0222 File Offset: 0x004EE422
		public ZMarkup(string text)
			: this()
		{
			this.set(text);
		}

		// Token: 0x0600D496 RID: 54422 RVA: 0x004F0238 File Offset: 0x004EE438
		public new void set(ReadOnlySpan<char> text)
		{
			this.Action = ZMarkup.BaseColorPattern;
			int num = 0;
			ZMarkup.ParseText(text, ref num, this);
		}

		// Token: 0x0600D497 RID: 54423 RVA: 0x004F025C File Offset: 0x004EE45C
		private static ZMarkup Parse(ReadOnlySpan<char> text)
		{
			Queue<ZMarkup> queue = ZMarkup.markupNodePool;
			ZMarkup zmarkup;
			lock (queue)
			{
				zmarkup = ((ZMarkup.markupNodePool.Count > 0) ? ZMarkup.markupNodePool.Dequeue() : null) ?? new ZMarkup();
			}
			zmarkup.set(text);
			return zmarkup;
		}

		// Token: 0x0600D498 RID: 54424 RVA: 0x004F02C4 File Offset: 0x004EE4C4
		public static void Transform(TextBuilder Input, TextBuilder Output, bool refreshAtNewline = false)
		{
			Span<char> span = Input.AsSpan();
			ZMarkup zmarkup = ZMarkup.Parse(span);
			if (zmarkup.Children.Count == 1 && Input[0] != '{')
			{
				zmarkup.Children[0].ToTextBuilder(Output.Clear(), span, refreshAtNewline);
				return;
			}
			zmarkup.ToTextBuilder(Output.Clear(), span, refreshAtNewline);
			zmarkup.release();
		}

		// Token: 0x0600D499 RID: 54425 RVA: 0x004F0338 File Offset: 0x004EE538
		public static string Strip(string Text)
		{
			if (Text.IsNullOrEmpty())
			{
				return Text;
			}
			string text;
			using (TextBuilder textBuilder = TextBuilder.Get(Text))
			{
				ZMarkup.Strip(textBuilder);
				text = textBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600D49A RID: 54426 RVA: 0x004F0380 File Offset: 0x004EE580
		public static void Strip(TextBuilder Builder)
		{
			char[] array = Builder.GetArray();
			int i = 0;
			int num = -1;
			int num2 = 0;
			int num3 = Builder.Length;
			while (i < num3)
			{
				char c = array[i];
				if (c == '{')
				{
					if (i < num3 - 1 && array[i + 1] == '{')
					{
						num = i++;
					}
				}
				else if (c == '|')
				{
					if (num != -1)
					{
						i++;
						Builder.Remove(num, i - num);
						num3 -= i - num;
						i = num - 1;
						num2++;
						num = -1;
					}
				}
				else if (c == '}')
				{
					if (num2 > 0 && i < num3 - 1 && array[i + 1] == '}')
					{
						Builder.Remove(i, 2);
						num3 -= 2;
						i--;
						num2--;
					}
				}
				else if ((c == '&' || c == '^') && i < num3 - 1)
				{
					char c2 = array[i + 1];
					if ((c2 >= 'a' && c2 <= 'z') || (c2 >= 'A' && c2 <= 'Z'))
					{
						Builder.Remove(i, 2);
						num3 -= 2;
						i--;
					}
				}
				i++;
			}
		}

		// Token: 0x0600D49B RID: 54427 RVA: 0x004F0487 File Offset: 0x004EE687
		public static string Wrap(string Text)
		{
			if (Text != null && (Text.Contains("&") || Text.Contains("^")) && !Text.StartsWith("{{"))
			{
				Text = "{{|" + Text + "}}";
			}
			return Text;
		}

		// Token: 0x0600D49C RID: 54428 RVA: 0x004F04C6 File Offset: 0x004EE6C6
		public static string Color(string Color, string Text)
		{
			return string.Concat(new string[] { "{{", Color, "|", Text, "}}" });
		}

		// Token: 0x0600D49D RID: 54429 RVA: 0x004F04F4 File Offset: 0x004EE6F4
		private unsafe static void ParseText(ReadOnlySpan<char> text, ref int pos, ZMarkupControlNode parent)
		{
			int num = pos;
			while (pos < text.Length)
			{
				if (*text[pos] == 123 && pos < text.Length - 1 && *text[pos + 1] == 123)
				{
					if (pos > num)
					{
						parent.Add(text, num, pos - num);
					}
					pos += 2;
					ZMarkup.ParseControl(text, ref pos, parent);
					num = pos;
				}
				else
				{
					pos++;
				}
			}
			if (pos > num)
			{
				parent.Add(text, num, pos - num);
			}
		}

		// Token: 0x0600D49E RID: 54430 RVA: 0x004F0578 File Offset: 0x004EE778
		private unsafe static void ParseControl(ReadOnlySpan<char> text, ref int pos, ZMarkupControlNode parent)
		{
			int num = pos;
			ZMarkupControlNode zmarkupControlNode = ZMarkupControlNode.next();
			while (pos < text.Length)
			{
				char c = (char)(*text[pos]);
				if (c == '|' && zmarkupControlNode.Action == null)
				{
					zmarkupControlNode.Action = ((pos > num) ? text.Slice(num, pos - num).ToString() : "");
					pos++;
					num = pos;
				}
				else
				{
					if (c == '}' && pos < text.Length - 1 && *text[pos + 1] == 125)
					{
						if (zmarkupControlNode.Action != null)
						{
							zmarkupControlNode.Add(text, num, pos - num);
						}
						pos += 2;
						num = pos;
						break;
					}
					if (c == '{' && pos < text.Length - 1 && *text[pos + 1] == 123)
					{
						if (zmarkupControlNode.Action == null)
						{
							zmarkupControlNode.Action = "";
						}
						if (pos > num)
						{
							zmarkupControlNode.Add(text, num, pos - num);
						}
						pos += 2;
						ZMarkup.ParseControl(text, ref pos, zmarkupControlNode);
						num = pos;
					}
					else
					{
						pos++;
					}
				}
			}
			if (pos > num)
			{
				zmarkupControlNode.Add(text, num, pos - num);
			}
			if (zmarkupControlNode.Children.Count > 0)
			{
				parent.Add(zmarkupControlNode);
			}
		}

		// Token: 0x0600D49F RID: 54431 RVA: 0x004F06BC File Offset: 0x004EE8BC
		public new static ZMarkup next(ReadOnlySpan<char> text)
		{
			Queue<ZMarkup> queue = ZMarkup.markupNodePool;
			ZMarkup zmarkup;
			lock (queue)
			{
				zmarkup = ((ZMarkup.markupNodePool.Count > 0) ? ZMarkup.markupNodePool.Dequeue() : null) ?? new ZMarkup();
			}
			zmarkup.set(text);
			return zmarkup;
		}

		// Token: 0x0600D4A0 RID: 54432 RVA: 0x004F0724 File Offset: 0x004EE924
		public override void release()
		{
			foreach (IZMarkupNode izmarkupNode in this.Children)
			{
				izmarkupNode.release();
			}
			this.Action = null;
			this.Children.Clear();
			Queue<ZMarkup> queue = ZMarkup.markupNodePool;
			lock (queue)
			{
				ZMarkup.markupNodePool.Enqueue(this);
			}
		}

		// Token: 0x0400554B RID: 21835
		public const char BASE_FOREGROUND = 'y';

		// Token: 0x0400554C RID: 21836
		public const char BASE_BACKGROUND = 'k';

		// Token: 0x0400554D RID: 21837
		public static readonly char[] NORMAL_FOREGROUNDS = new char[]
		{
			'K', 'b', 'B', 'C', 'c', 'g', 'G', 'r', 'R', 'm',
			'M', 'W', 'w', 'Y', 'y'
		};

		// Token: 0x0400554E RID: 21838
		private static string _BaseColorPattern;

		// Token: 0x0400554F RID: 21839
		public static bool Enabled = true;

		// Token: 0x04005550 RID: 21840
		private static Queue<ZMarkup> markupNodePool = new Queue<ZMarkup>();
	}
}
