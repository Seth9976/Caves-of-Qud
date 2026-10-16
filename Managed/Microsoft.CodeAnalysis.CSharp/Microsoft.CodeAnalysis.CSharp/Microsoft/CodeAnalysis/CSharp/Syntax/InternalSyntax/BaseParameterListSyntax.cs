using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000448 RID: 1096
	internal abstract class BaseParameterListSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005D1C RID: 23836 RVA: 0x0016B4ED File Offset: 0x001696ED
		internal BaseParameterListSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005D1D RID: 23837 RVA: 0x0016B4F8 File Offset: 0x001696F8
		internal BaseParameterListSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005D1E RID: 23838 RVA: 0x0016B501 File Offset: 0x00169701
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BaseParameterListSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170015A4 RID: 5540
		// (get) Token: 0x06005D1F RID: 23839
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SeparatedSyntaxList<ParameterSyntax> Parameters
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
