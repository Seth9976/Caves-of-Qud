using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200040B RID: 1035
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class SwitchLabelSyntax : CSharpSyntaxNode
	{
		// Token: 0x0600593E RID: 22846 RVA: 0x0015E18F File Offset: 0x0015C38F
		internal SwitchLabelSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0015E19A File Offset: 0x0015C39A
		internal SwitchLabelSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x0015E1A3 File Offset: 0x0015C3A3
		protected SwitchLabelSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06005941 RID: 22849
		public abstract SyntaxToken Keyword { get; }

		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06005942 RID: 22850
		public abstract SyntaxToken ColonToken { get; }
	}
}
