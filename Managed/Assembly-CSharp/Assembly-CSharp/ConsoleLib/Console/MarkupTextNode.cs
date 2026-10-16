using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200153C RID: 5436
	public class MarkupTextNode : IMarkupNode
	{
		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x0600D3D7 RID: 54231 RVA: 0x004EB93C File Offset: 0x004E9B3C
		// (set) Token: 0x0600D3D8 RID: 54232 RVA: 0x004EB944 File Offset: 0x004E9B44
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
				this._TextLength = -1;
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x0600D3D9 RID: 54233 RVA: 0x004EB954 File Offset: 0x004E9B54
		public int TextLength
		{
			get
			{
				if (this._TextLength == -1)
				{
					this._TextLength = ColorUtility.LengthExceptFormatting(this._Text);
				}
				return this._TextLength;
			}
		}

		// Token: 0x0600D3DA RID: 54234 RVA: 0x004EB978 File Offset: 0x004E9B78
		public static MarkupTextNode next(ReadOnlySpan<char> text)
		{
			Queue<MarkupTextNode> queue = MarkupTextNode.markupTextNodePool;
			MarkupTextNode markupTextNode;
			lock (queue)
			{
				markupTextNode = ((MarkupTextNode.markupTextNodePool.Count > 0) ? MarkupTextNode.markupTextNodePool.Dequeue() : null) ?? new MarkupTextNode();
			}
			markupTextNode.set(text);
			return markupTextNode;
		}

		// Token: 0x0600D3DB RID: 54235 RVA: 0x004EB9E0 File Offset: 0x004E9BE0
		public override void release()
		{
			this._Text = null;
			this._TextLength = -1;
			Queue<MarkupTextNode> queue = MarkupTextNode.markupTextNodePool;
			lock (queue)
			{
				MarkupTextNode.markupTextNodePool.Enqueue(this);
			}
		}

		// Token: 0x0600D3DC RID: 54236 RVA: 0x004EBA34 File Offset: 0x004E9C34
		public void set(ReadOnlySpan<char> text)
		{
			this.Text = text.ToString();
		}

		// Token: 0x0600D3DE RID: 54238 RVA: 0x004EBA58 File Offset: 0x004E9C58
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x0600D3DF RID: 54239 RVA: 0x004EBA61 File Offset: 0x004E9C61
		public override string ToString(bool RefreshAtNewline)
		{
			return this.Text;
		}

		// Token: 0x0600D3E0 RID: 54240 RVA: 0x004EBA69 File Offset: 0x004E9C69
		public override void ToStringBuilder(StringBuilder SB, bool RefreshAtNewline, ref char? lastForeground, ref char? lastBackground)
		{
			SB.Append(this.Text);
			ColorUtility.FindLastForegroundAndBackground(this.Text, ref lastForeground, ref lastBackground);
		}

		// Token: 0x0600D3E1 RID: 54241 RVA: 0x004EBA8B File Offset: 0x004E9C8B
		public override void ToStringBuilder(StringBuilder SB, bool RefreshAtNewline)
		{
			SB.Append(this.Text);
		}

		// Token: 0x0600D3E2 RID: 54242 RVA: 0x004EBA9C File Offset: 0x004E9C9C
		public override string DebugDump(int depth = 0)
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			string text;
			try
			{
				this.DebugDump(ref utf16ValueStringBuilder, depth);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x0600D3E3 RID: 54243 RVA: 0x004EBAE4 File Offset: 0x004E9CE4
		public override void DebugDump(ref Utf16ValueStringBuilder SB, int depth)
		{
			int i = 0;
			int num = depth * 2;
			while (i < num)
			{
				SB.Append('_');
				i++;
			}
			SB.Append("text: ");
			SB.Append(this.Text.Replace("\n", "\\n"));
			SB.Append('\n');
		}

		// Token: 0x040054FE RID: 21758
		private string _Text;

		// Token: 0x040054FF RID: 21759
		private int _TextLength = -1;

		// Token: 0x04005500 RID: 21760
		private static Queue<MarkupTextNode> markupTextNodePool = new Queue<MarkupTextNode>();
	}
}
