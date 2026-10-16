using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000040 RID: 64
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct NamespaceOrTypeAndUsingDirective
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x0003BC8B File Offset: 0x00039E8B
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public NamespaceOrTypeAndUsingDirective(NamespaceOrTypeSymbol namespaceOrType, [global::System.Runtime.CompilerServices.Nullable(2)] UsingDirectiveSyntax usingDirective, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<AssemblySymbol> dependencies)
		{
			this.NamespaceOrType = namespaceOrType;
			this.UsingDirectiveReference = ((usingDirective != null) ? usingDirective.GetReference() : null);
			this.Dependencies = dependencies.NullToEmpty<AssemblySymbol>();
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		public UsingDirectiveSyntax UsingDirective
		{
			get
			{
				SyntaxReference usingDirectiveReference = this.UsingDirectiveReference;
				return (UsingDirectiveSyntax)((usingDirectiveReference != null) ? usingDirectiveReference.GetSyntax(default(CancellationToken)) : null);
			}
		}

		// Token: 0x040000CE RID: 206
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public readonly NamespaceOrTypeSymbol NamespaceOrType;

		// Token: 0x040000CF RID: 207
		public readonly SyntaxReference UsingDirectiveReference;

		// Token: 0x040000D0 RID: 208
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<AssemblySymbol> Dependencies;
	}
}
