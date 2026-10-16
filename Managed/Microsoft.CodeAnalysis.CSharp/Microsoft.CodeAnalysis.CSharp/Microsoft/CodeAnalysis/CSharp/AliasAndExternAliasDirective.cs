using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200000C RID: 12
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct AliasAndExternAliasDirective
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00006FB8 File Offset: 0x000051B8
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public AliasAndExternAliasDirective(AliasSymbol alias, [global::System.Runtime.CompilerServices.Nullable(2)] ExternAliasDirectiveSyntax externAliasDirective, bool skipInLookup)
		{
			this.Alias = alias;
			this.ExternAliasDirectiveReference = ((externAliasDirective != null) ? externAliasDirective.GetReference() : null);
			this.SkipInLookup = skipInLookup;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00006FDC File Offset: 0x000051DC
		public ExternAliasDirectiveSyntax ExternAliasDirective
		{
			get
			{
				SyntaxReference externAliasDirectiveReference = this.ExternAliasDirectiveReference;
				return (ExternAliasDirectiveSyntax)((externAliasDirectiveReference != null) ? externAliasDirectiveReference.GetSyntax(default(CancellationToken)) : null);
			}
		}

		// Token: 0x04000008 RID: 8
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public readonly AliasSymbol Alias;

		// Token: 0x04000009 RID: 9
		public readonly SyntaxReference ExternAliasDirectiveReference;

		// Token: 0x0400000A RID: 10
		public readonly bool SkipInLookup;
	}
}
