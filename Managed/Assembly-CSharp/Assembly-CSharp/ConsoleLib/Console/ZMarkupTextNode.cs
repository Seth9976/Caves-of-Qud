using System;
using System.Collections.Generic;
using Cysharp.Text;
using XRL.World.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200154E RID: 5454
	public class ZMarkupTextNode : IZMarkupNode
	{
		// Token: 0x0600D4B9 RID: 54457 RVA: 0x004F13A8 File Offset: 0x004EF5A8
		public int GetTextLengthWithoutFormatting(ReadOnlySpan<char> Input)
		{
			if (this._TextLength == -1)
			{
				this._TextLength = ColorUtility.LengthExceptFormatting(Input.Slice(this.Start, this.Length));
			}
			return this._TextLength;
		}

		// Token: 0x0600D4BA RID: 54458 RVA: 0x004F13D8 File Offset: 0x004EF5D8
		public ReadOnlySpan<char> GetSpan(ReadOnlySpan<char> Input)
		{
			ReadOnlySpan<char> readOnlySpan = Input;
			int start = this.Start;
			return readOnlySpan.Slice(start, this.Start + this.Length - start);
		}

		// Token: 0x0600D4BB RID: 54459 RVA: 0x004F1408 File Offset: 0x004EF608
		public static ZMarkupTextNode next(ReadOnlySpan<char> text, int Start, int Length)
		{
			Queue<ZMarkupTextNode> queue = ZMarkupTextNode.markupTextNodePool;
			ZMarkupTextNode zmarkupTextNode;
			lock (queue)
			{
				zmarkupTextNode = ((ZMarkupTextNode.markupTextNodePool.Count > 0) ? ZMarkupTextNode.markupTextNodePool.Dequeue() : null) ?? new ZMarkupTextNode();
			}
			zmarkupTextNode.set(Start, Length);
			return zmarkupTextNode;
		}

		// Token: 0x0600D4BC RID: 54460 RVA: 0x004F1470 File Offset: 0x004EF670
		public override void release()
		{
			this.Start = 0;
			this.Length = 0;
			this._TextLength = -1;
			Queue<ZMarkupTextNode> queue = ZMarkupTextNode.markupTextNodePool;
			lock (queue)
			{
				ZMarkupTextNode.markupTextNodePool.Enqueue(this);
			}
		}

		// Token: 0x0600D4BD RID: 54461 RVA: 0x004F14CC File Offset: 0x004EF6CC
		public void set(int Start, int Length)
		{
			this.Start = Start;
			this.Length = Length;
		}

		// Token: 0x0600D4BF RID: 54463 RVA: 0x004F14EB File Offset: 0x004EF6EB
		public override void ToTextBuilder(TextBuilder SB, ReadOnlySpan<char> Source, bool RefreshAtNewline, ref char? lastForeground, ref char? lastBackground)
		{
			SB.Append(Source, this.Start, this.Length);
			ColorUtility.FindLastForegroundAndBackground(Source.Slice(this.Start, this.Length), ref lastForeground, ref lastBackground);
		}

		// Token: 0x0600D4C0 RID: 54464 RVA: 0x004F151D File Offset: 0x004EF71D
		public override void ToTextBuilder(TextBuilder SB, ReadOnlySpan<char> Source, bool RefreshAtNewline)
		{
			SB.Append(Source, this.Start, this.Length);
		}

		// Token: 0x0600D4C1 RID: 54465 RVA: 0x004F1534 File Offset: 0x004EF734
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

		// Token: 0x0600D4C2 RID: 54466 RVA: 0x004F157C File Offset: 0x004EF77C
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
			SB.Append(this.ToString().Replace("\n", "\\n"));
			SB.Append('\n');
		}

		// Token: 0x04005557 RID: 21847
		private int _TextLength = -1;

		// Token: 0x04005558 RID: 21848
		private int Start;

		// Token: 0x04005559 RID: 21849
		public int Length;

		// Token: 0x0400555A RID: 21850
		private static Queue<ZMarkupTextNode> markupTextNodePool = new Queue<ZMarkupTextNode>();
	}
}
