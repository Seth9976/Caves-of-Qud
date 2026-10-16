using System;
using System.Reflection.Emit;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001EF RID: 495
	internal sealed class CompiledRegexRunnerFactory : RegexRunnerFactory
	{
		// Token: 0x06000CFA RID: 3322 RVA: 0x000358E5 File Offset: 0x00033AE5
		public CompiledRegexRunnerFactory(DynamicMethod go, DynamicMethod firstChar, DynamicMethod trackCount)
		{
			this._goMethod = go;
			this._findFirstCharMethod = firstChar;
			this._initTrackCountMethod = trackCount;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00035904 File Offset: 0x00033B04
		protected internal override RegexRunner CreateInstance()
		{
			CompiledRegexRunner compiledRegexRunner = new CompiledRegexRunner();
			compiledRegexRunner.SetDelegates((Action<RegexRunner>)this._goMethod.CreateDelegate(typeof(Action<RegexRunner>)), (Func<RegexRunner, bool>)this._findFirstCharMethod.CreateDelegate(typeof(Func<RegexRunner, bool>)), (Action<RegexRunner>)this._initTrackCountMethod.CreateDelegate(typeof(Action<RegexRunner>)));
			return compiledRegexRunner;
		}

		// Token: 0x04000800 RID: 2048
		private readonly DynamicMethod _goMethod;

		// Token: 0x04000801 RID: 2049
		private readonly DynamicMethod _findFirstCharMethod;

		// Token: 0x04000802 RID: 2050
		private readonly DynamicMethod _initTrackCountMethod;
	}
}
