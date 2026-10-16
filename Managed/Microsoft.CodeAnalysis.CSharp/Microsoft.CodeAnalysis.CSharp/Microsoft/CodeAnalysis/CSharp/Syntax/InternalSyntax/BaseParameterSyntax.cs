using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200044B RID: 1099
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BaseParameterSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005D40 RID: 23872 RVA: 0x0016BABA File Offset: 0x00169CBA
		internal BaseParameterSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x0016BAC5 File Offset: 0x00169CC5
		internal BaseParameterSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x0016BACE File Offset: 0x00169CCE
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BaseParameterSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06005D43 RID: 23875
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06005D44 RID: 23876
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06005D45 RID: 23877
		public abstract TypeSyntax Type { get; }
	}
}
