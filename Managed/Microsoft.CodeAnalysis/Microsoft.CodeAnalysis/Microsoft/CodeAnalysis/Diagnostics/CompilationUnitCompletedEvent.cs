using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x02000314 RID: 788
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompilationUnitCompletedEvent : CompilationEvent
	{
		// Token: 0x06002271 RID: 8817 RVA: 0x0006D956 File Offset: 0x0006BB56
		public CompilationUnitCompletedEvent(Compilation compilation, SyntaxTree compilationUnit)
			: base(compilation)
		{
			this.CompilationUnit = compilationUnit;
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0006D966 File Offset: 0x0006BB66
		public SyntaxTree CompilationUnit { get; }

		// Token: 0x06002273 RID: 8819 RVA: 0x0006D96E File Offset: 0x0006BB6E
		public override string ToString()
		{
			return "CompilationUnitCompletedEvent(" + this.CompilationUnit.FilePath + ")";
		}
	}
}
