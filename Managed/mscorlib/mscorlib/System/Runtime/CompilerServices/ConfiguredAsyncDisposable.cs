using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007E4 RID: 2020
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredAsyncDisposable
	{
		// Token: 0x060045DC RID: 17884 RVA: 0x000E53BE File Offset: 0x000E35BE
		internal ConfiguredAsyncDisposable(IAsyncDisposable source, bool continueOnCapturedContext)
		{
			this._source = source;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x000E53D0 File Offset: 0x000E35D0
		public ConfiguredValueTaskAwaitable DisposeAsync()
		{
			return this._source.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
		}

		// Token: 0x04002D2B RID: 11563
		private readonly IAsyncDisposable _source;

		// Token: 0x04002D2C RID: 11564
		private readonly bool _continueOnCapturedContext;
	}
}
