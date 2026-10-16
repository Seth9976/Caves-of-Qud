using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using XRL.World.Text;

namespace ConsoleLib.Console
{
	// Token: 0x02001539 RID: 5433
	[DebuggerDisplay("Markup:{Action} {Children}")]
	public class Markup : MarkupControlNode
	{
		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x0600D3A9 RID: 54185 RVA: 0x004EA5C7 File Offset: 0x004E87C7
		private static string BaseColorPattern
		{
			get
			{
				if (Markup._BaseColorPattern == null)
				{
					Markup._BaseColorPattern = "&y^k";
				}
				return Markup._BaseColorPattern;
			}
		}

		// Token: 0x0600D3AA RID: 54186 RVA: 0x004EA5DF File Offset: 0x004E87DF
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

		// Token: 0x0600D3AB RID: 54187 RVA: 0x004EA5F1 File Offset: 0x004E87F1
		public Markup()
		{
		}

		// Token: 0x0600D3AC RID: 54188 RVA: 0x004EA5F9 File Offset: 0x004E87F9
		public Markup(string text)
			: this()
		{
			this.set(text);
		}

		// Token: 0x0600D3AD RID: 54189 RVA: 0x004EA610 File Offset: 0x004E8810
		public new void set(ReadOnlySpan<char> text)
		{
			this.Action = Markup.BaseColorPattern;
			int num = 0;
			Markup.ParseText(text, ref num, this);
		}

		// Token: 0x0600D3AE RID: 54190 RVA: 0x004EA634 File Offset: 0x004E8834
		private static Markup Parse(ReadOnlySpan<char> text)
		{
			Queue<Markup> queue = Markup.markupNodePool;
			Markup markup;
			lock (queue)
			{
				markup = ((Markup.markupNodePool.Count > 0) ? Markup.markupNodePool.Dequeue() : null) ?? new Markup();
			}
			markup.set(text);
			return markup;
		}

		// Token: 0x0600D3AF RID: 54191 RVA: 0x004EA69C File Offset: 0x004E889C
		public static string Transform(ReadOnlySpan<char> text, bool refreshAtNewline = false)
		{
			if (Markup.Enabled && text != null && text.Contains("{{"))
			{
				Markup markup = Markup.Parse(text);
				string text2 = markup.ToString(refreshAtNewline);
				markup.release();
				return text2;
			}
			return text.ToString();
		}

		// Token: 0x0600D3B0 RID: 54192 RVA: 0x004EA6F4 File Offset: 0x004E88F4
		public static void Transform(TextBuilder text, bool refreshAtNewline = false)
		{
			if (Markup.Enabled && text != null && text.Contains("{{", StringComparison.Ordinal))
			{
				Markup markup = Markup.Parse(text.AsSpan());
				string text2 = markup.ToString(refreshAtNewline);
				text.Length = 0;
				text.Append(text2);
				markup.release();
			}
		}

		// Token: 0x0600D3B1 RID: 54193 RVA: 0x004EA748 File Offset: 0x004E8948
		public static void Transform(StringBuilder text, bool refreshAtNewline = false)
		{
			if (Markup.Enabled && text != null && text.Contains("{{"))
			{
				Markup markup = Markup.Parse(text.ToString());
				string text2 = markup.ToString(refreshAtNewline);
				text.Length = 0;
				text.Append(text2);
				markup.release();
			}
		}

		// Token: 0x0600D3B2 RID: 54194 RVA: 0x004EA798 File Offset: 0x004E8998
		public static List<string> Transform(List<string> text, bool refreshAtNewline = false)
		{
			return new List<string>(Markup.Transform(string.Join("\n", text.ToArray()), false).Split('\n', StringSplitOptions.None));
		}

		// Token: 0x0600D3B3 RID: 54195 RVA: 0x004EA7C4 File Offset: 0x004E89C4
		public static string Strip(string Text)
		{
			if (Text.IsNullOrEmpty())
			{
				return Text;
			}
			string text;
			using (TextBuilder textBuilder = TextBuilder.Get(Text))
			{
				Markup.Strip(textBuilder);
				text = textBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600D3B4 RID: 54196 RVA: 0x004EA80C File Offset: 0x004E8A0C
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

		// Token: 0x0600D3B5 RID: 54197 RVA: 0x004EA913 File Offset: 0x004E8B13
		public static string Wrap(string Text)
		{
			if (Text != null && (Text.Contains("&") || Text.Contains("^")) && !Text.StartsWith("{{"))
			{
				Text = "{{|" + Text + "}}";
			}
			return Text;
		}

		// Token: 0x0600D3B6 RID: 54198 RVA: 0x004EA952 File Offset: 0x004E8B52
		public static string Color(string Color, string Text)
		{
			return string.Concat(new string[] { "{{", Color, "|", Text, "}}" });
		}

		// Token: 0x0600D3B7 RID: 54199 RVA: 0x004EA980 File Offset: 0x004E8B80
		private unsafe static void ParseText(ReadOnlySpan<char> text, ref int pos, MarkupControlNode parent)
		{
			int num = pos;
			while (pos < text.Length)
			{
				if (*text[pos] == 123 && pos < text.Length - 1 && *text[pos + 1] == 123)
				{
					if (pos > num)
					{
						parent.Add(text.Slice(num, pos - num));
					}
					pos += 2;
					Markup.ParseControl(text, ref pos, parent);
					num = pos;
				}
				else
				{
					pos++;
				}
			}
			if (pos > num)
			{
				parent.Add(text.Slice(num, pos - num));
			}
		}

		// Token: 0x0600D3B8 RID: 54200 RVA: 0x004EAA10 File Offset: 0x004E8C10
		private unsafe static void ParseControl(ReadOnlySpan<char> text, ref int pos, MarkupControlNode parent)
		{
			int num = pos;
			MarkupControlNode markupControlNode = MarkupControlNode.next();
			while (pos < text.Length)
			{
				char c = (char)(*text[pos]);
				if (c == '|' && markupControlNode.Action == null)
				{
					markupControlNode.Action = ((pos > num) ? text.Slice(num, pos - num).ToString() : "");
					pos++;
					num = pos;
				}
				else
				{
					if (c == '}' && pos < text.Length - 1 && *text[pos + 1] == 125)
					{
						if (markupControlNode.Action != null)
						{
							markupControlNode.Add(text.Slice(num, pos - num));
						}
						pos += 2;
						num = pos;
						break;
					}
					if (c == '{' && pos < text.Length - 1 && *text[pos + 1] == 123)
					{
						if (markupControlNode.Action == null)
						{
							markupControlNode.Action = "";
						}
						if (pos > num)
						{
							markupControlNode.Add(text.Slice(num, pos - num));
						}
						pos += 2;
						Markup.ParseControl(text, ref pos, markupControlNode);
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
				markupControlNode.Add(text.Slice(num, pos - num));
			}
			if (markupControlNode.Children.Count > 0)
			{
				parent.Add(markupControlNode);
			}
		}

		// Token: 0x0600D3B9 RID: 54201 RVA: 0x004EAB64 File Offset: 0x004E8D64
		public new static Markup next(ReadOnlySpan<char> text)
		{
			Queue<Markup> queue = Markup.markupNodePool;
			Markup markup;
			lock (queue)
			{
				markup = ((Markup.markupNodePool.Count > 0) ? Markup.markupNodePool.Dequeue() : null) ?? new Markup();
			}
			markup.set(text);
			return markup;
		}

		// Token: 0x0600D3BA RID: 54202 RVA: 0x004EABCC File Offset: 0x004E8DCC
		public override void release()
		{
			foreach (IMarkupNode markupNode in this.Children)
			{
				markupNode.release();
			}
			this.Action = null;
			this.Children.Clear();
			Queue<Markup> queue = Markup.markupNodePool;
			lock (queue)
			{
				Markup.markupNodePool.Enqueue(this);
			}
		}

		// Token: 0x040054F2 RID: 21746
		public const char BASE_FOREGROUND = 'y';

		// Token: 0x040054F3 RID: 21747
		public const char BASE_BACKGROUND = 'k';

		// Token: 0x040054F4 RID: 21748
		public static readonly char[] NORMAL_FOREGROUNDS = new char[]
		{
			'K', 'b', 'B', 'C', 'c', 'g', 'G', 'r', 'R', 'm',
			'M', 'W', 'w', 'Y', 'y'
		};

		// Token: 0x040054F5 RID: 21749
		private static string _BaseColorPattern;

		// Token: 0x040054F6 RID: 21750
		public static bool Enabled = true;

		// Token: 0x040054F7 RID: 21751
		private static Queue<Markup> markupNodePool = new Queue<Markup>();
	}
}
