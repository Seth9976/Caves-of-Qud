using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000231 RID: 561
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class NamespaceDeclarationSyntaxReference : TranslationSyntaxReference
	{
		// Token: 0x06002ECE RID: 11982 RVA: 0x000F0CD7 File Offset: 0x000EEED7
		public NamespaceDeclarationSyntaxReference(SyntaxReference reference)
			: base(reference)
		{
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000F0CE0 File Offset: 0x000EEEE0
		protected override SyntaxNode Translate(SyntaxReference reference, CancellationToken cancellationToken)
		{
			return NamespaceDeclarationSyntaxReference.GetSyntax(reference, cancellationToken);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x000F0CEC File Offset: 0x000EEEEC
		internal static SyntaxNode GetSyntax(SyntaxReference reference, CancellationToken cancellationToken)
		{
			CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)reference.GetSyntax(cancellationToken);
			while (csharpSyntaxNode is NameSyntax)
			{
				csharpSyntaxNode = csharpSyntaxNode.Parent;
			}
			return csharpSyntaxNode;
		}
	}
}
