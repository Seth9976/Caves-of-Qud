using System;
using System.Text;
using Cysharp.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200153A RID: 5434
	public abstract class IMarkupNode
	{
		// Token: 0x0600D3BC RID: 54204
		public abstract void release();

		// Token: 0x0600D3BD RID: 54205
		public abstract void ToStringBuilder(StringBuilder SB, bool RefreshAtNewline, ref char? lastForeground, ref char? lastBackground);

		// Token: 0x0600D3BE RID: 54206
		public abstract void ToStringBuilder(StringBuilder SB, bool RefreshAtNewline);

		// Token: 0x0600D3BF RID: 54207
		public abstract string ToString(bool RefreshAtNewline);

		// Token: 0x0600D3C0 RID: 54208
		public abstract string DebugDump(int depth = 0);

		// Token: 0x0600D3C1 RID: 54209
		public abstract void DebugDump(ref Utf16ValueStringBuilder SB, int depth);
	}
}
