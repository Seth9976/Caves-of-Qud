using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000436 RID: 1078
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BaseFieldDeclarationSyntax : MemberDeclarationSyntax
	{
		// Token: 0x06005BE7 RID: 23527 RVA: 0x00166B6B File Offset: 0x00164D6B
		internal BaseFieldDeclarationSyntax(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] DiagnosticInfo[] diagnostics, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] SyntaxAnnotation[] annotations)
			: base(kind, diagnostics, annotations)
		{
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00166B76 File Offset: 0x00164D76
		internal BaseFieldDeclarationSyntax(SyntaxKind kind)
			: base(kind)
		{
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x00166B7F File Offset: 0x00164D7F
		protected BaseFieldDeclarationSyntax(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x06005BEA RID: 23530
		public abstract VariableDeclarationSyntax Declaration { get; }

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x06005BEB RID: 23531
		public abstract SyntaxToken SemicolonToken { get; }
	}
}
