using System;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001EE RID: 494
	internal sealed class CompiledRegexRunner : RegexRunner
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x000358A4 File Offset: 0x00033AA4
		public void SetDelegates(Action<RegexRunner> go, Func<RegexRunner, bool> firstChar, Action<RegexRunner> trackCount)
		{
			this._goMethod = go;
			this._findFirstCharMethod = firstChar;
			this._initTrackCountMethod = trackCount;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x000358BB File Offset: 0x00033ABB
		protected override void Go()
		{
			this._goMethod(this);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000358C9 File Offset: 0x00033AC9
		protected override bool FindFirstChar()
		{
			return this._findFirstCharMethod(this);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000358D7 File Offset: 0x00033AD7
		protected override void InitTrackCount()
		{
			this._initTrackCountMethod(this);
		}

		// Token: 0x040007FD RID: 2045
		private Action<RegexRunner> _goMethod;

		// Token: 0x040007FE RID: 2046
		private Func<RegexRunner, bool> _findFirstCharMethod;

		// Token: 0x040007FF RID: 2047
		private Action<RegexRunner> _initTrackCountMethod;
	}
}
