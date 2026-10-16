using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000423 RID: 1059
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BaseTypeDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005ABA RID: 23226 RVA: 0x00162989 File Offset: 0x00160B89
		internal BaseTypeDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x00162994 File Offset: 0x00160B94
		internal BaseTypeDeclarationSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0016299D File Offset: 0x00160B9D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BaseTypeDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06005ABD RID: 23229
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract SyntaxToken Identifier
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06005ABE RID: 23230
		public abstract BaseListSyntax BaseList { get; }

		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06005ABF RID: 23231
		public abstract SyntaxToken OpenBraceToken { get; }

		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x06005AC0 RID: 23232
		public abstract SyntaxToken CloseBraceToken { get; }

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06005AC1 RID: 23233
		public abstract SyntaxToken SemicolonToken { get; }
	}
}
