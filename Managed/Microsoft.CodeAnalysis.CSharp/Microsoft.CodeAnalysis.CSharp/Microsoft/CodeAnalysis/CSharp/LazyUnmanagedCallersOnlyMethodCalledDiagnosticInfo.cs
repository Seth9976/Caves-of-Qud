using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200013D RID: 317
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class LazyUnmanagedCallersOnlyMethodCalledDiagnosticInfo : DiagnosticInfo
	{
		// Token: 0x0600191F RID: 6431 RVA: 0x0008754F File Offset: 0x0008574F
		internal LazyUnmanagedCallersOnlyMethodCalledDiagnosticInfo(MethodSymbol method, bool isDelegateConversion)
			: base(Microsoft.CodeAnalysis.CSharp.MessageProvider.Instance, -1)
		{
			this._method = method;
			this._lazyActualUnmanagedCallersOnlyDiagnostic = null;
			this._isDelegateConversion = isDelegateConversion;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00087574 File Offset: 0x00085774
		internal override DiagnosticInfo GetResolvedInfo()
		{
			if (this._lazyActualUnmanagedCallersOnlyDiagnostic == null)
			{
				DiagnosticInfo diagnosticInfo = ((this._method.GetUnmanagedCallersOnlyAttributeData(true) == null) ? CSDiagnosticInfo.VoidDiagnosticInfo : new CSDiagnosticInfo(this._isDelegateConversion ? ErrorCode.ERR_UnmanagedCallersOnlyMethodsCannotBeConvertedToDelegate : ErrorCode.ERR_UnmanagedCallersOnlyMethodsCannotBeCalledDirectly, new object[] { this._method }));
				Interlocked.CompareExchange<DiagnosticInfo>(ref this._lazyActualUnmanagedCallersOnlyDiagnostic, diagnosticInfo, null);
			}
			return this._lazyActualUnmanagedCallersOnlyDiagnostic;
		}

		// Token: 0x04000D0D RID: 3341
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private DiagnosticInfo _lazyActualUnmanagedCallersOnlyDiagnostic;

		// Token: 0x04000D0E RID: 3342
		private readonly MethodSymbol _method;

		// Token: 0x04000D0F RID: 3343
		private readonly bool _isDelegateConversion;
	}
}
