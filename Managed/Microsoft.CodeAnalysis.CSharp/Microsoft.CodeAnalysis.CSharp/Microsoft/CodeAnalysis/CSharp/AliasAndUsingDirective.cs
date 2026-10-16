using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200000D RID: 13
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct AliasAndUsingDirective
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00007009 File Offset: 0x00005209
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public AliasAndUsingDirective(AliasSymbol alias, [global::System.Runtime.CompilerServices.Nullable(2)] UsingDirectiveSyntax usingDirective)
		{
			this.Alias = alias;
			this.UsingDirectiveReference = ((usingDirective != null) ? usingDirective.GetReference() : null);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00007024 File Offset: 0x00005224
		public UsingDirectiveSyntax UsingDirective
		{
			get
			{
				SyntaxReference usingDirectiveReference = this.UsingDirectiveReference;
				return (UsingDirectiveSyntax)((usingDirectiveReference != null) ? usingDirectiveReference.GetSyntax(default(CancellationToken)) : null);
			}
		}

		// Token: 0x0400000B RID: 11
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public readonly AliasSymbol Alias;

		// Token: 0x0400000C RID: 12
		public readonly SyntaxReference UsingDirectiveReference;
	}
}
