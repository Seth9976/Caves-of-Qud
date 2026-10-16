using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000107 RID: 263
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class CSharpScriptCompilationInfo : ScriptCompilationInfo
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0006FDAC File Offset: 0x0006DFAC
		public new CSharpCompilation PreviousScriptCompilation { get; }

		// Token: 0x06001495 RID: 5269 RVA: 0x0006FDB4 File Offset: 0x0006DFB4
		internal CSharpScriptCompilationInfo(CSharpCompilation previousCompilationOpt, Type returnType, Type globalsType)
			: base(returnType, globalsType)
		{
			this.PreviousScriptCompilation = previousCompilationOpt;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x0006FDC5 File Offset: 0x0006DFC5
		internal override Compilation CommonPreviousScriptCompilation
		{
			get
			{
				return this.PreviousScriptCompilation;
			}
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0006FDCD File Offset: 0x0006DFCD
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public CSharpScriptCompilationInfo WithPreviousScriptCompilation([global::System.Runtime.CompilerServices.Nullable(2)] CSharpCompilation compilation)
		{
			if (compilation != this.PreviousScriptCompilation)
			{
				return new CSharpScriptCompilationInfo(compilation, base.ReturnTypeOpt, base.GlobalsType);
			}
			return this;
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0006FDEC File Offset: 0x0006DFEC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override ScriptCompilationInfo CommonWithPreviousScriptCompilation([global::System.Runtime.CompilerServices.Nullable(2)] Compilation compilation)
		{
			return this.WithPreviousScriptCompilation((CSharpCompilation)compilation);
		}
	}
}
