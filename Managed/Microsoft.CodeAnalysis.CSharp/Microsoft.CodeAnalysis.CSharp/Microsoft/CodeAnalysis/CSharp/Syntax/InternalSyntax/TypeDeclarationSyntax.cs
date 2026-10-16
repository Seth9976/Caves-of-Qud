using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000424 RID: 1060
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class TypeDeclarationSyntax : BaseTypeDeclarationSyntax
	{
		// Token: 0x06005AC2 RID: 23234 RVA: 0x001629A6 File Offset: 0x00160BA6
		internal TypeDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x001629B1 File Offset: 0x00160BB1
		internal TypeDeclarationSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x001629BA File Offset: 0x00160BBA
		protected TypeDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x06005AC5 RID: 23237
		public abstract SyntaxToken Keyword { get; }

		// Token: 0x170014E3 RID: 5347
		// (get) Token: 0x06005AC6 RID: 23238
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public abstract TypeParameterListSyntax TypeParameterList
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170014E4 RID: 5348
		// (get) Token: 0x06005AC7 RID: 23239
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<TypeParameterConstraintClauseSyntax> ConstraintClauses
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170014E5 RID: 5349
		// (get) Token: 0x06005AC8 RID: 23240
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public abstract SyntaxList<MemberDeclarationSyntax> Members
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}
	}
}
