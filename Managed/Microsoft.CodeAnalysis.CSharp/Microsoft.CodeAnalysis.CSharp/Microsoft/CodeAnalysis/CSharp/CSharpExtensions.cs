using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200011E RID: 286
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public static class CSharpExtensions
	{
		// Token: 0x060017CC RID: 6092 RVA: 0x00081E20 File Offset: 0x00080020
		internal static bool IsCSharpKind(int rawKind)
		{
			return rawKind - 2 > 8190;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00081E2C File Offset: 0x0008002C
		public static SyntaxKind Kind(this SyntaxToken token)
		{
			int rawKind = token.RawKind;
			if (!CSharpExtensions.IsCSharpKind(rawKind))
			{
				return SyntaxKind.None;
			}
			return (SyntaxKind)rawKind;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00081E50 File Offset: 0x00080050
		public static SyntaxKind Kind(this SyntaxTrivia trivia)
		{
			int rawKind = trivia.RawKind;
			if (!CSharpExtensions.IsCSharpKind(rawKind))
			{
				return SyntaxKind.None;
			}
			return (SyntaxKind)rawKind;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00081E74 File Offset: 0x00080074
		public static SyntaxKind Kind(this SyntaxNode node)
		{
			int rawKind = node.RawKind;
			if (!CSharpExtensions.IsCSharpKind(rawKind))
			{
				return SyntaxKind.None;
			}
			return (SyntaxKind)rawKind;
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00081E94 File Offset: 0x00080094
		public static SyntaxKind Kind(this SyntaxNodeOrToken nodeOrToken)
		{
			int rawKind = nodeOrToken.RawKind;
			if (!CSharpExtensions.IsCSharpKind(rawKind))
			{
				return SyntaxKind.None;
			}
			return (SyntaxKind)rawKind;
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00081EB5 File Offset: 0x000800B5
		public static bool IsKeyword(this SyntaxToken token)
		{
			return SyntaxFacts.IsKeywordKind(token.Kind());
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00081EC2 File Offset: 0x000800C2
		public static bool IsContextualKeyword(this SyntaxToken token)
		{
			return SyntaxFacts.IsContextualKeyword(token.Kind());
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00081ECF File Offset: 0x000800CF
		public static bool IsReservedKeyword(this SyntaxToken token)
		{
			return SyntaxFacts.IsReservedKeyword(token.Kind());
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00081EDC File Offset: 0x000800DC
		public static bool IsVerbatimStringLiteral(this SyntaxToken token)
		{
			return token.IsKind(SyntaxKind.StringLiteralToken) && token.Text.Length > 0 && token.Text.get_Chars(0) == '@';
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00081F0D File Offset: 0x0008010D
		public static bool IsVerbatimIdentifier(this SyntaxToken token)
		{
			return token.IsKind(SyntaxKind.IdentifierToken) && token.Text.Length > 0 && token.Text.get_Chars(0) == '@';
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00081F40 File Offset: 0x00080140
		public static VarianceKind VarianceKindFromToken(this SyntaxToken node)
		{
			SyntaxKind syntaxKind = node.Kind();
			if (syntaxKind == SyntaxKind.OutKeyword)
			{
				return VarianceKind.Out;
			}
			if (syntaxKind != SyntaxKind.InKeyword)
			{
				return VarianceKind.None;
			}
			return VarianceKind.In;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00081F6C File Offset: 0x0008016C
		public static SyntaxTokenList Insert(this SyntaxTokenList list, int index, params SyntaxToken[] items)
		{
			if (index < 0 || index > list.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			if (list.Count == 0)
			{
				return SyntaxFactory.TokenList(items);
			}
			SyntaxTokenListBuilder syntaxTokenListBuilder = new SyntaxTokenListBuilder(list.Count + items.Length);
			if (index > 0)
			{
				syntaxTokenListBuilder.Add(list, 0, index);
			}
			syntaxTokenListBuilder.Add(items);
			if (index < list.Count)
			{
				syntaxTokenListBuilder.Add(list, index, list.Count - index);
			}
			return syntaxTokenListBuilder.ToList();
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00081FF6 File Offset: 0x000801F6
		public static SyntaxToken ReplaceTrivia(this SyntaxToken token, IEnumerable<SyntaxTrivia> trivia, Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia)
		{
			return SyntaxReplacer.Replace(token, null, null, null, null, trivia, computeReplacementTrivia);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00082004 File Offset: 0x00080204
		public static SyntaxToken ReplaceTrivia(this SyntaxToken token, SyntaxTrivia oldTrivia, SyntaxTrivia newTrivia)
		{
			return SyntaxReplacer.Replace(token, null, null, null, null, new SyntaxTrivia[] { oldTrivia }, (SyntaxTrivia o, SyntaxTrivia r) => newTrivia);
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00082042 File Offset: 0x00080242
		internal static DirectiveStack ApplyDirectives(this SyntaxNode node, DirectiveStack stack)
		{
			return ((CSharpSyntaxNode)node.Green).ApplyDirectives(stack);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00082055 File Offset: 0x00080255
		internal static DirectiveStack ApplyDirectives(this SyntaxToken token, DirectiveStack stack)
		{
			return ((CSharpSyntaxNode)token.Node).ApplyDirectives(stack);
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0008206C File Offset: 0x0008026C
		internal static DirectiveStack ApplyDirectives(this SyntaxNodeOrToken nodeOrToken, DirectiveStack stack)
		{
			if (nodeOrToken.IsToken)
			{
				return nodeOrToken.AsToken().ApplyDirectives(stack);
			}
			SyntaxNode syntaxNode;
			if (nodeOrToken.AsNode(out syntaxNode))
			{
				return syntaxNode.ApplyDirectives(stack);
			}
			return stack;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x000820A4 File Offset: 0x000802A4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static SeparatedSyntaxList<TOther> AsSeparatedList<TOther>(this SyntaxNodeOrTokenList list) where TOther : SyntaxNode
		{
			SeparatedSyntaxListBuilder<TOther> separatedSyntaxListBuilder = SeparatedSyntaxListBuilder<TOther>.Create();
			foreach (SyntaxNodeOrToken syntaxNodeOrToken in list)
			{
				SyntaxNode syntaxNode = syntaxNodeOrToken.AsNode();
				if (syntaxNode != null)
				{
					separatedSyntaxListBuilder.Add((TOther)((object)syntaxNode));
				}
				else
				{
					SyntaxToken syntaxToken = syntaxNodeOrToken.AsToken();
					separatedSyntaxListBuilder.AddSeparator(in syntaxToken);
				}
			}
			return separatedSyntaxListBuilder.ToList();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00082128 File Offset: 0x00080328
		internal static IList<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax> GetDirectives(this SyntaxNode node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax, bool> filter = null)
		{
			return ((CSharpSyntaxNode)node).GetDirectives(filter);
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00082136 File Offset: 0x00080336
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax GetFirstDirective(this SyntaxNode node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax, bool> predicate = null)
		{
			return ((CSharpSyntaxNode)node).GetFirstDirective(predicate);
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00082144 File Offset: 0x00080344
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax GetLastDirective(this SyntaxNode node, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1 })] Func<Microsoft.CodeAnalysis.CSharp.Syntax.DirectiveTriviaSyntax, bool> predicate = null)
		{
			return ((CSharpSyntaxNode)node).GetLastDirective(predicate);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00082152 File Offset: 0x00080352
		public static Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax GetCompilationUnitRoot(this SyntaxTree tree, CancellationToken cancellationToken = default(CancellationToken))
		{
			return (Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax)tree.GetRoot(cancellationToken);
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00082160 File Offset: 0x00080360
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool HasReferenceDirectives([NotNullWhen(true)] this SyntaxTree tree)
		{
			CSharpSyntaxTree csharpSyntaxTree = tree as CSharpSyntaxTree;
			return csharpSyntaxTree != null && csharpSyntaxTree.HasReferenceDirectives;
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00082180 File Offset: 0x00080380
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool HasReferenceOrLoadDirectives([NotNullWhen(true)] this SyntaxTree tree)
		{
			CSharpSyntaxTree csharpSyntaxTree = tree as CSharpSyntaxTree;
			return csharpSyntaxTree != null && csharpSyntaxTree.HasReferenceOrLoadDirectives;
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x000821A0 File Offset: 0x000803A0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal static bool IsAnyPreprocessorSymbolDefined([NotNullWhen(true)] this SyntaxTree tree, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> conditionalSymbols)
		{
			CSharpSyntaxTree csharpSyntaxTree = tree as CSharpSyntaxTree;
			return csharpSyntaxTree != null && csharpSyntaxTree.IsAnyPreprocessorSymbolDefined(conditionalSymbols);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x000821C0 File Offset: 0x000803C0
		internal static bool IsPreprocessorSymbolDefined([global::System.Runtime.CompilerServices.Nullable(2)] [NotNullWhen(true)] this SyntaxTree tree, string symbolName, int position)
		{
			CSharpSyntaxTree csharpSyntaxTree = tree as CSharpSyntaxTree;
			return csharpSyntaxTree != null && csharpSyntaxTree.IsPreprocessorSymbolDefined(symbolName, position);
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x000821E1 File Offset: 0x000803E1
		internal static PragmaWarningState GetPragmaDirectiveWarningState(this SyntaxTree tree, string id, int position)
		{
			return ((CSharpSyntaxTree)tree).GetPragmaDirectiveWarningState(id, position);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x000821F0 File Offset: 0x000803F0
		public static Conversion ClassifyConversion([global::System.Runtime.CompilerServices.Nullable(2)] this Compilation compilation, ITypeSymbol source, ITypeSymbol destination)
		{
			CSharpCompilation csharpCompilation = compilation as CSharpCompilation;
			if (csharpCompilation != null)
			{
				return csharpCompilation.ClassifyConversion(source, destination);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00082218 File Offset: 0x00080418
		public static SymbolInfo GetSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.OrderingSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSymbolInfo(node, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00082240 File Offset: 0x00080440
		public static SymbolInfo GetSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSymbolInfo(node, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00082268 File Offset: 0x00080468
		public static SymbolInfo GetSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSymbolInfo(expression, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00082290 File Offset: 0x00080490
		public static SymbolInfo GetCollectionInitializerSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetCollectionInitializerSymbolInfo(expression, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000822B8 File Offset: 0x000804B8
		public static SymbolInfo GetSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSymbolInfo(constructorInitializer, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x000822E0 File Offset: 0x000804E0
		public static SymbolInfo GetSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.PrimaryConstructorBaseTypeSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSymbolInfo(constructorInitializer, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00082308 File Offset: 0x00080508
		public static SymbolInfo GetSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax attributeSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSymbolInfo(attributeSyntax, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00082330 File Offset: 0x00080530
		public static SymbolInfo GetSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax crefSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSymbolInfo(crefSyntax, cancellationToken);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00082358 File Offset: 0x00080558
		public static SymbolInfo GetSpeculativeSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSpeculativeSymbolInfo(position, expression, bindingOption);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00082380 File Offset: 0x00080580
		public static SymbolInfo GetSpeculativeSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax expression, SpeculativeBindingOption bindingOption)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSpeculativeSymbolInfo(position, expression, bindingOption);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000823A8 File Offset: 0x000805A8
		public static SymbolInfo GetSpeculativeSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax attribute)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSpeculativeSymbolInfo(position, attribute);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000823D0 File Offset: 0x000805D0
		public static SymbolInfo GetSpeculativeSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax constructorInitializer)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSpeculativeSymbolInfo(position, constructorInitializer);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000823F8 File Offset: 0x000805F8
		public static SymbolInfo GetSpeculativeSymbolInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.PrimaryConstructorBaseTypeSyntax constructorInitializer)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSpeculativeSymbolInfo(position, constructorInitializer);
			}
			return SymbolInfo.None;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00082420 File Offset: 0x00080620
		public static TypeInfo GetTypeInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetTypeInfo(constructorInitializer, cancellationToken);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0008244C File Offset: 0x0008064C
		public static TypeInfo GetTypeInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.SelectOrGroupClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetTypeInfo(node, cancellationToken);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00082478 File Offset: 0x00080678
		public static TypeInfo GetTypeInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetTypeInfo(expression, cancellationToken);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x000824A4 File Offset: 0x000806A4
		public static TypeInfo GetTypeInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax attributeSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetTypeInfo(attributeSyntax, cancellationToken);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x000824D0 File Offset: 0x000806D0
		public static TypeInfo GetSpeculativeTypeInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSpeculativeTypeInfo(position, expression, bindingOption);
			}
			return CSharpTypeInfo.None;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000824FC File Offset: 0x000806FC
		public static Conversion GetConversion([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, SyntaxNode expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetConversion(expression, cancellationToken);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x00082524 File Offset: 0x00080724
		public static Conversion GetConversion(this IConversionOperation conversionExpression)
		{
			if (conversionExpression == null)
			{
				throw new ArgumentNullException("conversionExpression");
			}
			if (conversionExpression.Language == "C#")
			{
				return (Conversion)((ConversionOperation)conversionExpression).ConversionConvertible;
			}
			throw new ArgumentException(string.Format(CSharpResources.IConversionExpressionIsNotCSharpConversion, "IConversionOperation"), "conversionExpression");
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0008257C File Offset: 0x0008077C
		public static Conversion GetInConversion(this ICompoundAssignmentOperation compoundAssignment)
		{
			if (compoundAssignment == null)
			{
				throw new ArgumentNullException("compoundAssignment");
			}
			if (compoundAssignment.Language == "C#")
			{
				return (Conversion)((CompoundAssignmentOperation)compoundAssignment).InConversionConvertible;
			}
			throw new ArgumentException(string.Format(CSharpResources.ICompoundAssignmentOperationIsNotCSharpCompoundAssignment, "compoundAssignment"), "compoundAssignment");
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000825D4 File Offset: 0x000807D4
		public static Conversion GetOutConversion(this ICompoundAssignmentOperation compoundAssignment)
		{
			if (compoundAssignment == null)
			{
				throw new ArgumentNullException("compoundAssignment");
			}
			if (compoundAssignment.Language == "C#")
			{
				return (Conversion)((CompoundAssignmentOperation)compoundAssignment).OutConversionConvertible;
			}
			throw new ArgumentException(string.Format(CSharpResources.ICompoundAssignmentOperationIsNotCSharpCompoundAssignment, "compoundAssignment"), "compoundAssignment");
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0008262C File Offset: 0x0008082C
		public static Conversion GetSpeculativeConversion([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, SpeculativeBindingOption bindingOption)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetSpeculativeConversion(position, expression, bindingOption);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00082654 File Offset: 0x00080854
		public static ForEachStatementInfo GetForEachStatementInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax forEachStatement)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetForEachStatementInfo(forEachStatement);
			}
			return default(ForEachStatementInfo);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0008267C File Offset: 0x0008087C
		public static ForEachStatementInfo GetForEachStatementInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.CommonForEachStatementSyntax forEachStatement)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetForEachStatementInfo(forEachStatement);
			}
			return default(ForEachStatementInfo);
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000826A4 File Offset: 0x000808A4
		public static DeconstructionInfo GetDeconstructionInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.AssignmentExpressionSyntax assignment)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return default(DeconstructionInfo);
			}
			return csharpSemanticModel.GetDeconstructionInfo(assignment);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000826CC File Offset: 0x000808CC
		public static DeconstructionInfo GetDeconstructionInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ForEachVariableStatementSyntax @foreach)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return default(DeconstructionInfo);
			}
			return csharpSemanticModel.GetDeconstructionInfo(@foreach);
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000826F4 File Offset: 0x000808F4
		public static AwaitExpressionInfo GetAwaitExpressionInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.AwaitExpressionSyntax awaitExpression)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetAwaitExpressionInfo(awaitExpression);
			}
			return default(AwaitExpressionInfo);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0008271C File Offset: 0x0008091C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ISymbol> GetMemberGroup([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetMemberGroup(expression, cancellationToken);
			}
			return ImmutableArray.Create<ISymbol>();
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00082744 File Offset: 0x00080944
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ISymbol> GetMemberGroup([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax attribute, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetMemberGroup(attribute, cancellationToken);
			}
			return ImmutableArray.Create<ISymbol>();
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0008276C File Offset: 0x0008096C
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<ISymbol> GetMemberGroup([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax initializer, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetMemberGroup(initializer, cancellationToken);
			}
			return ImmutableArray.Create<ISymbol>();
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00082794 File Offset: 0x00080994
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<IPropertySymbol> GetIndexerGroup([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetIndexerGroup(expression, cancellationToken);
			}
			return ImmutableArray.Create<IPropertySymbol>();
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000827BC File Offset: 0x000809BC
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public static Optional<object> GetConstantValue([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetConstantValue(expression, cancellationToken);
			}
			return default(Optional<object>);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x000827E8 File Offset: 0x000809E8
		public static QueryClauseInfo GetQueryClauseInfo([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.QueryClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.GetQueryClauseInfo(node, cancellationToken);
			}
			return default(QueryClauseInfo);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00082811 File Offset: 0x00080A11
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IAliasSymbol GetAliasInfo(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax nameSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetAliasInfo(nameSyntax, cancellationToken);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00082826 File Offset: 0x00080A26
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IAliasSymbol GetSpeculativeAliasInfo(this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.IdentifierNameSyntax nameSyntax, SpeculativeBindingOption bindingOption)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetSpeculativeAliasInfo(position, nameSyntax, bindingOption);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0008283C File Offset: 0x00080A3C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static ControlFlowAnalysis AnalyzeControlFlow([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax firstStatement, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax lastStatement)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.AnalyzeControlFlow(firstStatement, lastStatement);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00082851 File Offset: 0x00080A51
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ControlFlowAnalysis AnalyzeControlFlow(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.AnalyzeControlFlow(statement);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00082865 File Offset: 0x00080A65
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static DataFlowAnalysis AnalyzeDataFlow(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.AnalyzeDataFlow(expression);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00082879 File Offset: 0x00080A79
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public static DataFlowAnalysis AnalyzeDataFlow([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax firstStatement, Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax lastStatement)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.AnalyzeDataFlow(firstStatement, lastStatement);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0008288E File Offset: 0x00080A8E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static DataFlowAnalysis AnalyzeDataFlow(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.AnalyzeDataFlow(statement);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000828A4 File Offset: 0x00080AA4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModelForMethodBody([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.BaseMethodDeclarationSyntax method, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModelForMethodBody(position, method, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000828CC File Offset: 0x00080ACC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModelForMethodBody([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax accessor, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModelForMethodBody(position, accessor, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000828F4 File Offset: 0x00080AF4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax type, [NotNullWhen(true)] out SemanticModel speculativeModel, SpeculativeBindingOption bindingOption = SpeculativeBindingOption.BindAsExpression)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, type, out speculativeModel, bindingOption);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0008291C File Offset: 0x00080B1C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax crefSyntax, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, crefSyntax, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00082944 File Offset: 0x00080B44
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax statement, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, statement, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x0008296C File Offset: 0x00080B6C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.EqualsValueClauseSyntax initializer, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, initializer, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00082994 File Offset: 0x00080B94
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ArrowExpressionClauseSyntax expressionBody, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, expressionBody, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000829BC File Offset: 0x00080BBC
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ConstructorInitializerSyntax constructorInitializer, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, constructorInitializer, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x000829E4 File Offset: 0x00080BE4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.PrimaryConstructorBaseTypeSyntax constructorInitializer, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, constructorInitializer, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00082A0C File Offset: 0x00080C0C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool TryGetSpeculativeSemanticModel([NotNullWhen(true)] this SemanticModel semanticModel, int position, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.AttributeSyntax attribute, [NotNullWhen(true)] out SemanticModel speculativeModel)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.TryGetSpeculativeSemanticModel(position, attribute, out speculativeModel);
			}
			speculativeModel = null;
			return false;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00082A34 File Offset: 0x00080C34
		public static Conversion ClassifyConversion([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, ITypeSymbol destination, bool isExplicitInSource = false)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.ClassifyConversion(expression, destination, isExplicitInSource);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00082A5C File Offset: 0x00080C5C
		public static Conversion ClassifyConversion([global::System.Runtime.CompilerServices.Nullable(2)] this SemanticModel semanticModel, int position, Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax expression, ITypeSymbol destination, bool isExplicitInSource = false)
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel != null)
			{
				return csharpSemanticModel.ClassifyConversion(position, expression, destination, isExplicitInSource);
			}
			return Conversion.NoConversion;
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00082A84 File Offset: 0x00080C84
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ISymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00082A99 File Offset: 0x00080C99
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IMethodSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00082AAE File Offset: 0x00080CAE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static INamespaceSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.NamespaceDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00082AC3 File Offset: 0x00080CC3
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static INamedTypeSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.BaseTypeDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00082AD8 File Offset: 0x00080CD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static INamedTypeSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.DelegateDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00082AED File Offset: 0x00080CED
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IFieldSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.EnumMemberDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00082B02 File Offset: 0x00080D02
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IMethodSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.BaseMethodDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00082B17 File Offset: 0x00080D17
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ISymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.BasePropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00082B2C File Offset: 0x00080D2C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IPropertySymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00082B41 File Offset: 0x00080D41
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IPropertySymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.IndexerDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00082B56 File Offset: 0x00080D56
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IEventSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.EventDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00082B6B File Offset: 0x00080D6B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IPropertySymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectMemberDeclaratorSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00082B80 File Offset: 0x00080D80
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static INamedTypeSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.AnonymousObjectCreationExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00082B95 File Offset: 0x00080D95
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static INamedTypeSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TupleExpressionSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00082BAA File Offset: 0x00080DAA
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ISymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ArgumentSyntax declaratorSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declaratorSyntax, cancellationToken);
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00082BBF File Offset: 0x00080DBF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IMethodSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00082BD4 File Offset: 0x00080DD4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ISymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.SingleVariableDesignationSyntax designationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(designationSyntax, cancellationToken);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00082BE9 File Offset: 0x00080DE9
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ISymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.VariableDeclaratorSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00082BFE File Offset: 0x00080DFE
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ISymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TupleElementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00082C13 File Offset: 0x00080E13
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ILabelSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.LabeledStatementSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00082C28 File Offset: 0x00080E28
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ILabelSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.SwitchLabelSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00082C3D File Offset: 0x00080E3D
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IAliasSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.UsingDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x00082C52 File Offset: 0x00080E52
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IAliasSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ExternAliasDirectiveSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x00082C67 File Offset: 0x00080E67
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IParameterSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ParameterSyntax declarationSyntax, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(declarationSyntax, cancellationToken);
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00082C7C File Offset: 0x00080E7C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ITypeParameterSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterSyntax typeParameter, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(typeParameter, cancellationToken);
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x00082C91 File Offset: 0x00080E91
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ILocalSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.ForEachStatementSyntax forEachStatement, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(forEachStatement, cancellationToken);
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x00082CA6 File Offset: 0x00080EA6
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static ILocalSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.CatchDeclarationSyntax catchDeclaration, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(catchDeclaration, cancellationToken);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00082CBB File Offset: 0x00080EBB
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IRangeVariableSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.QueryClauseSyntax queryClause, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(queryClause, cancellationToken);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00082CD0 File Offset: 0x00080ED0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IRangeVariableSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.JoinIntoClauseSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(node, cancellationToken);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00082CE5 File Offset: 0x00080EE5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static IRangeVariableSymbol GetDeclaredSymbol(this SemanticModel semanticModel, [global::System.Runtime.CompilerServices.Nullable(1)] Microsoft.CodeAnalysis.CSharp.Syntax.QueryContinuationSyntax node, CancellationToken cancellationToken = default(CancellationToken))
		{
			CSharpSemanticModel csharpSemanticModel = semanticModel as CSharpSemanticModel;
			if (csharpSemanticModel == null)
			{
				return null;
			}
			return csharpSemanticModel.GetDeclaredSymbol(node, cancellationToken);
		}
	}
}
