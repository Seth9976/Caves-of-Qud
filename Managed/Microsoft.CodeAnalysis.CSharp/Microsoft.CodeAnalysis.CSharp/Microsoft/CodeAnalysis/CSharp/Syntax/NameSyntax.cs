using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x0200026F RID: 623
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public abstract class NameSyntax : TypeSyntax
	{
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003B72 RID: 15218 RVA: 0x00102E48 File Offset: 0x00101048
		public int Arity
		{
			get
			{
				if (!(this is GenericNameSyntax))
				{
					return 0;
				}
				return ((GenericNameSyntax)this).TypeArgumentList.Arguments.Count;
			}
		}

		// Token: 0x06003B73 RID: 15219
		internal abstract SimpleNameSyntax GetUnqualifiedName();

		// Token: 0x06003B74 RID: 15220
		internal abstract string ErrorDisplayName();

		// Token: 0x06003B75 RID: 15221 RVA: 0x00102E78 File Offset: 0x00101078
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal string GetAliasQualifierOpt()
		{
			NameSyntax nameSyntax = this;
			SyntaxKind syntaxKind;
			for (;;)
			{
				syntaxKind = nameSyntax.Kind();
				if (syntaxKind != SyntaxKind.QualifiedName)
				{
					break;
				}
				nameSyntax = ((QualifiedNameSyntax)nameSyntax).Left;
			}
			if (syntaxKind != SyntaxKind.AliasQualifiedName)
			{
				return null;
			}
			return ((AliasQualifiedNameSyntax)nameSyntax).Alias.Identifier.ValueText;
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x00102EC8 File Offset: 0x001010C8
		internal NameSyntax(CSharpSyntaxNode green, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxNode parent, int position)
			: base(green, parent, position)
		{
		}
	}
}
