using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200013A RID: 314
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class LazyDiagnosticInfo : DiagnosticInfo
	{
		// Token: 0x06001913 RID: 6419 RVA: 0x00087305 File Offset: 0x00085505
		protected LazyDiagnosticInfo()
			: base(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, -1)
		{
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x00087313 File Offset: 0x00085513
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal sealed override DiagnosticInfo GetResolvedInfo()
		{
			if (this._lazyInfo == null)
			{
				Interlocked.CompareExchange<DiagnosticInfo>(ref this._lazyInfo, this.ResolveInfo() ?? CSDiagnosticInfo.VoidDiagnosticInfo, null);
			}
			return this._lazyInfo;
		}

		// Token: 0x06001915 RID: 6421
		protected abstract DiagnosticInfo ResolveInfo();

		// Token: 0x04000D06 RID: 3334
		private DiagnosticInfo _lazyInfo;
	}
}
