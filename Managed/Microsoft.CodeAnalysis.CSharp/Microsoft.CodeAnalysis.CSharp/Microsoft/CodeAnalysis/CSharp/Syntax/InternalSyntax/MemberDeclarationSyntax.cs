using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000419 RID: 1049
	internal abstract class MemberDeclarationSyntax : CSharpSyntaxNode
	{
		// Token: 0x06005A20 RID: 23072 RVA: 0x00160D21 File Offset: 0x0015EF21
		internal MemberDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005A21 RID: 23073 RVA: 0x00160D2C File Offset: 0x0015EF2C
		internal MemberDeclarationSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005A22 RID: 23074 RVA: 0x00160D35 File Offset: 0x0015EF35
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected MemberDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x06005A23 RID: 23075
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<AttributeListSyntax> AttributeLists
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170014BC RID: 5308
		// (get) Token: 0x06005A24 RID: 23076
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<SyntaxToken> Modifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
