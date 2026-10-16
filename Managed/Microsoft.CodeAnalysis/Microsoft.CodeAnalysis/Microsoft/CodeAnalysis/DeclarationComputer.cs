using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000296 RID: 662
	[NullableContext(1)]
	[Nullable(0)]
	internal class DeclarationComputer
	{
		// Token: 0x06001B33 RID: 6963 RVA: 0x00052478 File Offset: 0x00050678
		internal static DeclarationInfo GetDeclarationInfo(SemanticModel model, SyntaxNode node, bool getSymbol, [Nullable(new byte[] { 2, 1 })] IEnumerable<SyntaxNode> executableCodeBlocks, CancellationToken cancellationToken)
		{
			ISymbol declaredSymbol = DeclarationComputer.GetDeclaredSymbol(model, node, getSymbol, cancellationToken);
			return DeclarationComputer.GetDeclarationInfo(node, declaredSymbol, executableCodeBlocks);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x00052498 File Offset: 0x00050698
		internal static DeclarationInfo GetDeclarationInfo(SyntaxNode node, [Nullable(2)] ISymbol declaredSymbol, [Nullable(new byte[] { 2, 1 })] IEnumerable<SyntaxNode> executableCodeBlocks)
		{
			ImmutableArray<SyntaxNode> immutableArray;
			if (executableCodeBlocks == null)
			{
				immutableArray = ImmutableArray<SyntaxNode>.Empty;
			}
			else
			{
				immutableArray = Enumerable.Where<SyntaxNode>(executableCodeBlocks, (SyntaxNode c) => c != null).AsImmutableOrEmpty<SyntaxNode>();
			}
			ImmutableArray<SyntaxNode> immutableArray2 = immutableArray;
			return new DeclarationInfo(node, immutableArray2, declaredSymbol);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000524E2 File Offset: 0x000506E2
		internal static DeclarationInfo GetDeclarationInfo(SemanticModel model, SyntaxNode node, bool getSymbol, CancellationToken cancellationToken)
		{
			return DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, null, cancellationToken);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000524EE File Offset: 0x000506EE
		internal static DeclarationInfo GetDeclarationInfo(SemanticModel model, SyntaxNode node, bool getSymbol, SyntaxNode executableCodeBlock, CancellationToken cancellationToken)
		{
			return DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, SpecializedCollections.SingletonEnumerable<SyntaxNode>(executableCodeBlock), cancellationToken);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x00052500 File Offset: 0x00050700
		internal static DeclarationInfo GetDeclarationInfo(SemanticModel model, SyntaxNode node, bool getSymbol, CancellationToken cancellationToken, params SyntaxNode[] executableCodeBlocks)
		{
			return DeclarationComputer.GetDeclarationInfo(model, node, getSymbol, Enumerable.AsEnumerable<SyntaxNode>(executableCodeBlocks), cancellationToken);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00052514 File Offset: 0x00050714
		[return: Nullable(2)]
		private static ISymbol GetDeclaredSymbol(SemanticModel model, SyntaxNode node, bool getSymbol, CancellationToken cancellationToken)
		{
			if (!getSymbol)
			{
				return null;
			}
			ISymbol symbol = model.GetDeclaredSymbol(node, cancellationToken);
			INamespaceSymbol namespaceSymbol = symbol as INamespaceSymbol;
			if (namespaceSymbol != null && namespaceSymbol.ConstituentNamespaces.Length > 1)
			{
				IAssemblySymbol assemblyToScope = model.Compilation.Assembly;
				INamespaceSymbol namespaceSymbol2 = namespaceSymbol.ConstituentNamespaces.FirstOrDefault<INamespaceSymbol>((INamespaceSymbol ns) => ns.ContainingAssembly == assemblyToScope);
				if (namespaceSymbol2 != null)
				{
					symbol = namespaceSymbol2;
				}
			}
			return symbol;
		}
	}
}
