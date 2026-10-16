using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000441 RID: 1089
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BasePropertyDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005CA3 RID: 23715 RVA: 0x00169831 File Offset: 0x00167A31
		internal BasePropertyDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x0016983C File Offset: 0x00167A3C
		internal BasePropertyDeclarationSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x00169845 File Offset: 0x00167A45
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected BasePropertyDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06005CA6 RID: 23718
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public abstract TypeSyntax Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
		}

		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06005CA7 RID: 23719
		public abstract ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier { get; }

		// Token: 0x1700157E RID: 5502
		// (get) Token: 0x06005CA8 RID: 23720
		public abstract AccessorListSyntax AccessorList { get; }
	}
}
