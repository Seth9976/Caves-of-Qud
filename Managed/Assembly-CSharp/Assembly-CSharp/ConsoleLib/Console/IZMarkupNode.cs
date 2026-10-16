using System;
using Cysharp.Text;
using XRL.World.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200154C RID: 5452
	public abstract class IZMarkupNode
	{
		// Token: 0x0600D4A2 RID: 54434
		public abstract void release();

		// Token: 0x0600D4A3 RID: 54435
		public abstract void ToTextBuilder(TextBuilder SB, ReadOnlySpan<char> Source, bool RefreshAtNewline, ref char? lastForeground, ref char? lastBackground);

		// Token: 0x0600D4A4 RID: 54436
		public abstract void ToTextBuilder(TextBuilder SB, ReadOnlySpan<char> Source, bool RefreshAtNewline);

		// Token: 0x0600D4A5 RID: 54437
		public abstract string DebugDump(int depth = 0);

		// Token: 0x0600D4A6 RID: 54438
		public abstract void DebugDump(ref Utf16ValueStringBuilder SB, int depth);
	}
}
