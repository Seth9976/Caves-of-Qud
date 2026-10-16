using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000313 RID: 787
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilationStartedEvent : CompilationEvent
	{
		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x0006D91B File Offset: 0x0006BB1B
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<AdditionalText> AdditionalFiles
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0006D923 File Offset: 0x0006BB23
		private CompilationStartedEvent(Compilation compilation, [Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles)
			: base(compilation)
		{
			this.AdditionalFiles = additionalFiles;
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0006D933 File Offset: 0x0006BB33
		public CompilationStartedEvent(Compilation compilation)
			: this(compilation, ImmutableArray<AdditionalText>.Empty)
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0006D941 File Offset: 0x0006BB41
		public override string ToString()
		{
			return "CompilationStartedEvent";
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0006D948 File Offset: 0x0006BB48
		public CompilationStartedEvent WithAdditionalFiles([Nullable(new byte[] { 0, 1 })] ImmutableArray<AdditionalText> additionalFiles)
		{
			return new CompilationStartedEvent(base.Compilation, additionalFiles);
		}
	}
}
