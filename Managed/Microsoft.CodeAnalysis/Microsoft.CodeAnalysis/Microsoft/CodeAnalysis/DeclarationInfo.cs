using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000297 RID: 663
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct DeclarationInfo
	{
		// Token: 0x06001B3A RID: 6970 RVA: 0x0005258A File Offset: 0x0005078A
		internal DeclarationInfo(SyntaxNode declaredNode, [Nullable(new byte[] { 0, 1 })] ImmutableArray<SyntaxNode> executableCodeBlocks, [Nullable(2)] ISymbol declaredSymbol)
		{
			this.DeclaredNode = declaredNode;
			this.ExecutableCodeBlocks = executableCodeBlocks;
			this.DeclaredSymbol = declaredSymbol;
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001B3B RID: 6971 RVA: 0x000525A1 File Offset: 0x000507A1
		public SyntaxNode DeclaredNode { get; }

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x000525A9 File Offset: 0x000507A9
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<SyntaxNode> ExecutableCodeBlocks
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x000525B1 File Offset: 0x000507B1
		[Nullable(2)]
		public ISymbol DeclaredSymbol
		{
			[NullableContext(2)]
			get;
		}
	}
}
