using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000372 RID: 882
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class SyntaxFactory
	{
		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x0014146E File Offset: 0x0013F66E
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private static SyntaxToken XmlCarriageReturnLineFeed
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				SyntaxToken syntaxToken;
				if ((syntaxToken = SyntaxFactory.s_xmlCarriageReturnLineFeed) == null)
				{
					syntaxToken = (SyntaxFactory.s_xmlCarriageReturnLineFeed = SyntaxFactory.XmlTextNewLine("\r\n"));
				}
				return syntaxToken;
			}
		}

		// Token: 0x06004F37 RID: 20279 RVA: 0x0014148C File Offset: 0x0013F68C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxTrivia EndOfLine(string text, bool elastic = false)
		{
			SyntaxTrivia syntaxTrivia = null;
			if (!(text == "\r"))
			{
				if (!(text == "\n"))
				{
					if (text == "\r\n")
					{
						syntaxTrivia = (elastic ? SyntaxFactory.ElasticCarriageReturnLineFeed : SyntaxFactory.CarriageReturnLineFeed);
					}
				}
				else
				{
					syntaxTrivia = (elastic ? SyntaxFactory.ElasticLineFeed : SyntaxFactory.LineFeed);
				}
			}
			else
			{
				syntaxTrivia = (elastic ? SyntaxFactory.ElasticCarriageReturn : SyntaxFactory.CarriageReturn);
			}
			if (syntaxTrivia != null)
			{
				return syntaxTrivia;
			}
			syntaxTrivia = SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, text);
			if (!elastic)
			{
				return syntaxTrivia;
			}
			return syntaxTrivia.WithAnnotationsGreen(new SyntaxAnnotation[] { SyntaxAnnotation.ElasticAnnotation });
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x00141524 File Offset: 0x0013F724
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxTrivia Whitespace(string text, bool elastic = false)
		{
			SyntaxTrivia syntaxTrivia = SyntaxTrivia.Create(SyntaxKind.WhitespaceTrivia, text);
			if (!elastic)
			{
				return syntaxTrivia;
			}
			return syntaxTrivia.WithAnnotationsGreen(new SyntaxAnnotation[] { SyntaxAnnotation.ElasticAnnotation });
		}

		// Token: 0x06004F39 RID: 20281 RVA: 0x00141556 File Offset: 0x0013F756
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxTrivia Comment(string text)
		{
			if (text.StartsWith("/*", 4))
			{
				return SyntaxTrivia.Create(SyntaxKind.MultiLineCommentTrivia, text);
			}
			return SyntaxTrivia.Create(SyntaxKind.SingleLineCommentTrivia, text);
		}

		// Token: 0x06004F3A RID: 20282 RVA: 0x0014157D File Offset: 0x0013F77D
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxTrivia ConflictMarker(string text)
		{
			return SyntaxTrivia.Create(SyntaxKind.ConflictMarkerTrivia, text);
		}

		// Token: 0x06004F3B RID: 20283 RVA: 0x0014158A File Offset: 0x0013F78A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxTrivia DisabledText(string text)
		{
			return SyntaxTrivia.Create(SyntaxKind.DisabledTextTrivia, text);
		}

		// Token: 0x06004F3C RID: 20284 RVA: 0x00141597 File Offset: 0x0013F797
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxTrivia PreprocessingMessage(string text)
		{
			return SyntaxTrivia.Create(SyntaxKind.PreprocessingMessageTrivia, text);
		}

		// Token: 0x06004F3D RID: 20285 RVA: 0x001415A4 File Offset: 0x0013F7A4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SyntaxToken Token(SyntaxKind kind)
		{
			return SyntaxToken.Create(kind);
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x001415AC File Offset: 0x0013F7AC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Token(GreenNode leading, SyntaxKind kind, GreenNode trailing)
		{
			return SyntaxToken.Create(kind, leading, trailing);
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x001415B8 File Offset: 0x0013F7B8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Token(GreenNode leading, SyntaxKind kind, string text, string valueText, GreenNode trailing)
		{
			string text2 = SyntaxFacts.GetText(kind);
			if (kind < SyntaxKind.TildeToken || kind > SyntaxKind.EndOfFileToken || !(text == text2) || !(valueText == text2))
			{
				return SyntaxToken.WithValue<string>(kind, leading, text, valueText, trailing);
			}
			return SyntaxFactory.Token(leading, kind, trailing);
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x00141603 File Offset: 0x0013F803
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken MissingToken(SyntaxKind kind)
		{
			return SyntaxToken.CreateMissing(kind, null, null);
		}

		// Token: 0x06004F41 RID: 20289 RVA: 0x0014160D File Offset: 0x0013F80D
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken MissingToken(GreenNode leading, SyntaxKind kind, GreenNode trailing)
		{
			return SyntaxToken.CreateMissing(kind, leading, trailing);
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x00141617 File Offset: 0x0013F817
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Identifier(string text)
		{
			return SyntaxFactory.Identifier(SyntaxKind.IdentifierToken, null, text, text, null);
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x00141627 File Offset: 0x0013F827
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Identifier(GreenNode leading, string text, GreenNode trailing)
		{
			return SyntaxFactory.Identifier(SyntaxKind.IdentifierToken, leading, text, text, trailing);
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x00141637 File Offset: 0x0013F837
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Identifier(SyntaxKind contextualKind, GreenNode leading, string text, string valueText, GreenNode trailing)
		{
			return SyntaxToken.Identifier(contextualKind, leading, text, valueText, trailing);
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x00141644 File Offset: 0x0013F844
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, int value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<int>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x00141654 File Offset: 0x0013F854
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, uint value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<uint>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x00141664 File Offset: 0x0013F864
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, long value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<long>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x00141674 File Offset: 0x0013F874
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, ulong value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<ulong>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x00141684 File Offset: 0x0013F884
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, float value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<float>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x00141694 File Offset: 0x0013F894
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, double value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<double>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x001416A4 File Offset: 0x0013F8A4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, decimal value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<decimal>(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x001416B4 File Offset: 0x0013F8B4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, string value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<string>(SyntaxKind.StringLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x001416C4 File Offset: 0x0013F8C4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, SyntaxKind kind, string value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<string>(kind, leading, text, value, trailing);
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x001416D1 File Offset: 0x0013F8D1
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken Literal(GreenNode leading, string text, char value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<char>(SyntaxKind.CharacterLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x001416E1 File Offset: 0x0013F8E1
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken BadToken(GreenNode leading, string text, GreenNode trailing)
		{
			return SyntaxToken.WithValue<string>(SyntaxKind.BadToken, leading, text, text, trailing);
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x001416F1 File Offset: 0x0013F8F1
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken XmlTextLiteral(GreenNode leading, string text, string value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<string>(SyntaxKind.XmlTextLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x00141701 File Offset: 0x0013F901
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken XmlTextNewLine(GreenNode leading, string text, string value, GreenNode trailing)
		{
			if (leading == null && trailing == null && text == "\r\n" && value == "\r\n")
			{
				return SyntaxFactory.XmlCarriageReturnLineFeed;
			}
			return SyntaxToken.WithValue<string>(SyntaxKind.XmlTextLiteralNewLineToken, leading, text, value, trailing);
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x00141737 File Offset: 0x0013F937
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken XmlTextNewLine(string text)
		{
			return SyntaxToken.WithValue<string>(SyntaxKind.XmlTextLiteralNewLineToken, null, text, text, null);
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x00141747 File Offset: 0x0013F947
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxToken XmlEntity(GreenNode leading, string text, string value, GreenNode trailing)
		{
			return SyntaxToken.WithValue<string>(SyntaxKind.XmlEntityLiteralToken, leading, text, value, trailing);
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x00141757 File Offset: 0x0013F957
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static SyntaxTrivia DocumentationCommentExteriorTrivia(string text)
		{
			return SyntaxTrivia.Create(SyntaxKind.DocumentationCommentExteriorTrivia, text);
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x00141764 File Offset: 0x0013F964
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SyntaxList<TNode> List<TNode>() where TNode : CSharpSyntaxNode
		{
			return default(SyntaxList<TNode>);
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x0014177A File Offset: 0x0013F97A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SyntaxList<TNode> List<TNode>(TNode node) where TNode : CSharpSyntaxNode
		{
			return new SyntaxList<TNode>(SyntaxList.List(node));
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x0014178C File Offset: 0x0013F98C
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SyntaxList<TNode> List<TNode>(TNode node0, TNode node1) where TNode : CSharpSyntaxNode
		{
			return new SyntaxList<TNode>(SyntaxList.List(node0, node1));
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x001417A4 File Offset: 0x0013F9A4
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static GreenNode ListNode(CSharpSyntaxNode node0, CSharpSyntaxNode node1)
		{
			return SyntaxList.List(node0, node1);
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x001417AD File Offset: 0x0013F9AD
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SyntaxList<TNode> List<TNode>(TNode node0, TNode node1, TNode node2) where TNode : CSharpSyntaxNode
		{
			return new SyntaxList<TNode>(SyntaxList.List(node0, node1, node2));
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x001417CB File Offset: 0x0013F9CB
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static GreenNode ListNode(CSharpSyntaxNode node0, CSharpSyntaxNode node1, CSharpSyntaxNode node2)
		{
			return SyntaxList.List(node0, node1, node2);
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x001417D8 File Offset: 0x0013F9D8
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SyntaxList<TNode> List<TNode>(params TNode[] nodes) where TNode : CSharpSyntaxNode
		{
			if (nodes != null)
			{
				return new SyntaxList<TNode>(SyntaxList.List(nodes));
			}
			return default(SyntaxList<TNode>);
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x001417FF File Offset: 0x0013F9FF
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static GreenNode ListNode(params ArrayElement<GreenNode>[] nodes)
		{
			return SyntaxList.List(nodes);
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x00141807 File Offset: 0x0013FA07
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(TNode node) where TNode : CSharpSyntaxNode
		{
			return new SeparatedSyntaxList<TNode>(new SyntaxList<CSharpSyntaxNode>(node));
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0014181E File Offset: 0x0013FA1E
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxToken token) where TNode : CSharpSyntaxNode
		{
			return new SeparatedSyntaxList<TNode>(new SyntaxList<CSharpSyntaxNode>(token));
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x00141830 File Offset: 0x0013FA30
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(TNode node1, SyntaxToken token, TNode node2) where TNode : CSharpSyntaxNode
		{
			return new SeparatedSyntaxList<TNode>(new SyntaxList<CSharpSyntaxNode>(SyntaxList.List(node1, token, node2)));
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x00141854 File Offset: 0x0013FA54
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(params CSharpSyntaxNode[] nodes) where TNode : CSharpSyntaxNode
		{
			if (nodes != null)
			{
				return new SeparatedSyntaxList<TNode>(SyntaxList.List(nodes));
			}
			return default(SeparatedSyntaxList<TNode>);
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x00141880 File Offset: 0x0013FA80
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static IEnumerable<SyntaxTrivia> GetWellKnownTrivia()
		{
			yield return SyntaxFactory.CarriageReturnLineFeed;
			yield return SyntaxFactory.LineFeed;
			yield return SyntaxFactory.CarriageReturn;
			yield return SyntaxFactory.Space;
			yield return SyntaxFactory.Tab;
			yield return SyntaxFactory.ElasticCarriageReturnLineFeed;
			yield return SyntaxFactory.ElasticLineFeed;
			yield return SyntaxFactory.ElasticCarriageReturn;
			yield return SyntaxFactory.ElasticSpace;
			yield return SyntaxFactory.ElasticTab;
			yield return SyntaxFactory.ElasticZeroSpace;
			yield break;
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x00141889 File Offset: 0x0013FA89
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		internal static IEnumerable<SyntaxToken> GetWellKnownTokens()
		{
			return SyntaxToken.GetWellKnownTokens();
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x00141890 File Offset: 0x0013FA90
		public static IdentifierNameSyntax IdentifierName(SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8616, identifier, out num);
			if (greenNode != null)
			{
				return (IdentifierNameSyntax)greenNode;
			}
			IdentifierNameSyntax identifierNameSyntax = new IdentifierNameSyntax(SyntaxKind.IdentifierName, identifier);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(identifierNameSyntax, num);
			}
			return identifierNameSyntax;
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x001418D0 File Offset: 0x0013FAD0
		public static QualifiedNameSyntax QualifiedName(NameSyntax left, SyntaxToken dotToken, SimpleNameSyntax right)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8617, left, dotToken, right, out num);
			if (greenNode != null)
			{
				return (QualifiedNameSyntax)greenNode;
			}
			QualifiedNameSyntax qualifiedNameSyntax = new QualifiedNameSyntax(SyntaxKind.QualifiedName, left, dotToken, right);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(qualifiedNameSyntax, num);
			}
			return qualifiedNameSyntax;
		}

		// Token: 0x06004F65 RID: 20325 RVA: 0x00141914 File Offset: 0x0013FB14
		public static GenericNameSyntax GenericName(SyntaxToken identifier, TypeArgumentListSyntax typeArgumentList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8618, identifier, typeArgumentList, out num);
			if (greenNode != null)
			{
				return (GenericNameSyntax)greenNode;
			}
			GenericNameSyntax genericNameSyntax = new GenericNameSyntax(SyntaxKind.GenericName, identifier, typeArgumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(genericNameSyntax, num);
			}
			return genericNameSyntax;
		}

		// Token: 0x06004F66 RID: 20326 RVA: 0x00141954 File Offset: 0x0013FB54
		public static TypeArgumentListSyntax TypeArgumentList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeSyntax> arguments, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8619, lessThanToken, arguments.Node, greaterThanToken, out num);
			if (greenNode != null)
			{
				return (TypeArgumentListSyntax)greenNode;
			}
			TypeArgumentListSyntax typeArgumentListSyntax = new TypeArgumentListSyntax(SyntaxKind.TypeArgumentList, lessThanToken, arguments.Node, greaterThanToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeArgumentListSyntax, num);
			}
			return typeArgumentListSyntax;
		}

		// Token: 0x06004F67 RID: 20327 RVA: 0x001419A4 File Offset: 0x0013FBA4
		public static AliasQualifiedNameSyntax AliasQualifiedName(IdentifierNameSyntax alias, SyntaxToken colonColonToken, SimpleNameSyntax name)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8620, alias, colonColonToken, name, out num);
			if (greenNode != null)
			{
				return (AliasQualifiedNameSyntax)greenNode;
			}
			AliasQualifiedNameSyntax aliasQualifiedNameSyntax = new AliasQualifiedNameSyntax(SyntaxKind.AliasQualifiedName, alias, colonColonToken, name);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(aliasQualifiedNameSyntax, num);
			}
			return aliasQualifiedNameSyntax;
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x001419E8 File Offset: 0x0013FBE8
		public static PredefinedTypeSyntax PredefinedType(SyntaxToken keyword)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8621, keyword, out num);
			if (greenNode != null)
			{
				return (PredefinedTypeSyntax)greenNode;
			}
			PredefinedTypeSyntax predefinedTypeSyntax = new PredefinedTypeSyntax(SyntaxKind.PredefinedType, keyword);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(predefinedTypeSyntax, num);
			}
			return predefinedTypeSyntax;
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00141A28 File Offset: 0x0013FC28
		public static ArrayTypeSyntax ArrayType(TypeSyntax elementType, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ArrayRankSpecifierSyntax> rankSpecifiers)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8622, elementType, rankSpecifiers.Node, out num);
			if (greenNode != null)
			{
				return (ArrayTypeSyntax)greenNode;
			}
			ArrayTypeSyntax arrayTypeSyntax = new ArrayTypeSyntax(SyntaxKind.ArrayType, elementType, rankSpecifiers.Node);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrayTypeSyntax, num);
			}
			return arrayTypeSyntax;
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00141A74 File Offset: 0x0013FC74
		public static ArrayRankSpecifierSyntax ArrayRankSpecifier(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> sizes, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8623, openBracketToken, sizes.Node, closeBracketToken, out num);
			if (greenNode != null)
			{
				return (ArrayRankSpecifierSyntax)greenNode;
			}
			ArrayRankSpecifierSyntax arrayRankSpecifierSyntax = new ArrayRankSpecifierSyntax(SyntaxKind.ArrayRankSpecifier, openBracketToken, sizes.Node, closeBracketToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrayRankSpecifierSyntax, num);
			}
			return arrayRankSpecifierSyntax;
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x00141AC4 File Offset: 0x0013FCC4
		public static PointerTypeSyntax PointerType(TypeSyntax elementType, SyntaxToken asteriskToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8624, elementType, asteriskToken, out num);
			if (greenNode != null)
			{
				return (PointerTypeSyntax)greenNode;
			}
			PointerTypeSyntax pointerTypeSyntax = new PointerTypeSyntax(SyntaxKind.PointerType, elementType, asteriskToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(pointerTypeSyntax, num);
			}
			return pointerTypeSyntax;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00141B03 File Offset: 0x0013FD03
		public static FunctionPointerTypeSyntax FunctionPointerType(SyntaxToken delegateKeyword, SyntaxToken asteriskToken, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerCallingConventionSyntax callingConvention, FunctionPointerParameterListSyntax parameterList)
		{
			return new FunctionPointerTypeSyntax(SyntaxKind.FunctionPointerType, delegateKeyword, asteriskToken, callingConvention, parameterList);
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x00141B14 File Offset: 0x0013FD14
		public static FunctionPointerParameterListSyntax FunctionPointerParameterList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9058, lessThanToken, parameters.Node, greaterThanToken, out num);
			if (greenNode != null)
			{
				return (FunctionPointerParameterListSyntax)greenNode;
			}
			FunctionPointerParameterListSyntax functionPointerParameterListSyntax = new FunctionPointerParameterListSyntax(SyntaxKind.FunctionPointerParameterList, lessThanToken, parameters.Node, greaterThanToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerParameterListSyntax, num);
			}
			return functionPointerParameterListSyntax;
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x00141B64 File Offset: 0x0013FD64
		public static FunctionPointerCallingConventionSyntax FunctionPointerCallingConvention(SyntaxToken managedOrUnmanagedKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] FunctionPointerUnmanagedCallingConventionListSyntax unmanagedCallingConventionList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9059, managedOrUnmanagedKeyword, unmanagedCallingConventionList, out num);
			if (greenNode != null)
			{
				return (FunctionPointerCallingConventionSyntax)greenNode;
			}
			FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax = new FunctionPointerCallingConventionSyntax(SyntaxKind.FunctionPointerCallingConvention, managedOrUnmanagedKeyword, unmanagedCallingConventionList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerCallingConventionSyntax, num);
			}
			return functionPointerCallingConventionSyntax;
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x00141BA4 File Offset: 0x0013FDA4
		public static FunctionPointerUnmanagedCallingConventionListSyntax FunctionPointerUnmanagedCallingConventionList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<FunctionPointerUnmanagedCallingConventionSyntax> callingConventions, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9066, openBracketToken, callingConventions.Node, closeBracketToken, out num);
			if (greenNode != null)
			{
				return (FunctionPointerUnmanagedCallingConventionListSyntax)greenNode;
			}
			FunctionPointerUnmanagedCallingConventionListSyntax functionPointerUnmanagedCallingConventionListSyntax = new FunctionPointerUnmanagedCallingConventionListSyntax(SyntaxKind.FunctionPointerUnmanagedCallingConventionList, openBracketToken, callingConventions.Node, closeBracketToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerUnmanagedCallingConventionListSyntax, num);
			}
			return functionPointerUnmanagedCallingConventionListSyntax;
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x00141BF4 File Offset: 0x0013FDF4
		public static FunctionPointerUnmanagedCallingConventionSyntax FunctionPointerUnmanagedCallingConvention(SyntaxToken name)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9067, name, out num);
			if (greenNode != null)
			{
				return (FunctionPointerUnmanagedCallingConventionSyntax)greenNode;
			}
			FunctionPointerUnmanagedCallingConventionSyntax functionPointerUnmanagedCallingConventionSyntax = new FunctionPointerUnmanagedCallingConventionSyntax(SyntaxKind.FunctionPointerUnmanagedCallingConvention, name);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerUnmanagedCallingConventionSyntax, num);
			}
			return functionPointerUnmanagedCallingConventionSyntax;
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x00141C34 File Offset: 0x0013FE34
		public static NullableTypeSyntax NullableType(TypeSyntax elementType, SyntaxToken questionToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8625, elementType, questionToken, out num);
			if (greenNode != null)
			{
				return (NullableTypeSyntax)greenNode;
			}
			NullableTypeSyntax nullableTypeSyntax = new NullableTypeSyntax(SyntaxKind.NullableType, elementType, questionToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nullableTypeSyntax, num);
			}
			return nullableTypeSyntax;
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x00141C74 File Offset: 0x0013FE74
		public static TupleTypeSyntax TupleType(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TupleElementSyntax> elements, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8924, openParenToken, elements.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (TupleTypeSyntax)greenNode;
			}
			TupleTypeSyntax tupleTypeSyntax = new TupleTypeSyntax(SyntaxKind.TupleType, openParenToken, elements.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(tupleTypeSyntax, num);
			}
			return tupleTypeSyntax;
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x00141CC4 File Offset: 0x0013FEC4
		public static TupleElementSyntax TupleElement(TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8925, type, identifier, out num);
			if (greenNode != null)
			{
				return (TupleElementSyntax)greenNode;
			}
			TupleElementSyntax tupleElementSyntax = new TupleElementSyntax(SyntaxKind.TupleElement, type, identifier);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(tupleElementSyntax, num);
			}
			return tupleElementSyntax;
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x00141D04 File Offset: 0x0013FF04
		public static OmittedTypeArgumentSyntax OmittedTypeArgument(SyntaxToken omittedTypeArgumentToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8626, omittedTypeArgumentToken, out num);
			if (greenNode != null)
			{
				return (OmittedTypeArgumentSyntax)greenNode;
			}
			OmittedTypeArgumentSyntax omittedTypeArgumentSyntax = new OmittedTypeArgumentSyntax(SyntaxKind.OmittedTypeArgument, omittedTypeArgumentToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(omittedTypeArgumentSyntax, num);
			}
			return omittedTypeArgumentSyntax;
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x00141D44 File Offset: 0x0013FF44
		public static RefTypeSyntax RefType(SyntaxToken refKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken readOnlyKeyword, TypeSyntax type)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9051, refKeyword, readOnlyKeyword, type, out num);
			if (greenNode != null)
			{
				return (RefTypeSyntax)greenNode;
			}
			RefTypeSyntax refTypeSyntax = new RefTypeSyntax(SyntaxKind.RefType, refKeyword, readOnlyKeyword, type);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(refTypeSyntax, num);
			}
			return refTypeSyntax;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x00141D88 File Offset: 0x0013FF88
		public static ParenthesizedExpressionSyntax ParenthesizedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8632, openParenToken, expression, closeParenToken, out num);
			if (greenNode != null)
			{
				return (ParenthesizedExpressionSyntax)greenNode;
			}
			ParenthesizedExpressionSyntax parenthesizedExpressionSyntax = new ParenthesizedExpressionSyntax(SyntaxKind.ParenthesizedExpression, openParenToken, expression, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parenthesizedExpressionSyntax, num);
			}
			return parenthesizedExpressionSyntax;
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x00141DCC File Offset: 0x0013FFCC
		public static TupleExpressionSyntax TupleExpression(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8926, openParenToken, arguments.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (TupleExpressionSyntax)greenNode;
			}
			TupleExpressionSyntax tupleExpressionSyntax = new TupleExpressionSyntax(SyntaxKind.TupleExpression, openParenToken, arguments.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(tupleExpressionSyntax, num);
			}
			return tupleExpressionSyntax;
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x00141E1C File Offset: 0x0014001C
		public static PrefixUnaryExpressionSyntax PrefixUnaryExpression(SyntaxKind kind, SyntaxToken operatorToken, ExpressionSyntax operand)
		{
			if (kind - SyntaxKind.UnaryPlusExpression > 7 && kind != SyntaxKind.IndexExpression)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, operatorToken, operand, out num);
			if (greenNode != null)
			{
				return (PrefixUnaryExpressionSyntax)greenNode;
			}
			PrefixUnaryExpressionSyntax prefixUnaryExpressionSyntax = new PrefixUnaryExpressionSyntax(kind, operatorToken, operand);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(prefixUnaryExpressionSyntax, num);
			}
			return prefixUnaryExpressionSyntax;
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x00141E70 File Offset: 0x00140070
		public static AwaitExpressionSyntax AwaitExpression(SyntaxToken awaitKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8740, awaitKeyword, expression, out num);
			if (greenNode != null)
			{
				return (AwaitExpressionSyntax)greenNode;
			}
			AwaitExpressionSyntax awaitExpressionSyntax = new AwaitExpressionSyntax(SyntaxKind.AwaitExpression, awaitKeyword, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(awaitExpressionSyntax, num);
			}
			return awaitExpressionSyntax;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00141EB0 File Offset: 0x001400B0
		public static PostfixUnaryExpressionSyntax PostfixUnaryExpression(SyntaxKind kind, ExpressionSyntax operand, SyntaxToken operatorToken)
		{
			if (kind - SyntaxKind.PostIncrementExpression > 1 && kind != SyntaxKind.SuppressNullableWarningExpression)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, operand, operatorToken, out num);
			if (greenNode != null)
			{
				return (PostfixUnaryExpressionSyntax)greenNode;
			}
			PostfixUnaryExpressionSyntax postfixUnaryExpressionSyntax = new PostfixUnaryExpressionSyntax(kind, operand, operatorToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(postfixUnaryExpressionSyntax, num);
			}
			return postfixUnaryExpressionSyntax;
		}

		// Token: 0x06004F7B RID: 20347 RVA: 0x00141F04 File Offset: 0x00140104
		public static MemberAccessExpressionSyntax MemberAccessExpression(SyntaxKind kind, ExpressionSyntax expression, SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			if (kind - SyntaxKind.SimpleMemberAccessExpression > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, expression, operatorToken, name, out num);
			if (greenNode != null)
			{
				return (MemberAccessExpressionSyntax)greenNode;
			}
			MemberAccessExpressionSyntax memberAccessExpressionSyntax = new MemberAccessExpressionSyntax(kind, expression, operatorToken, name);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(memberAccessExpressionSyntax, num);
			}
			return memberAccessExpressionSyntax;
		}

		// Token: 0x06004F7C RID: 20348 RVA: 0x00141F54 File Offset: 0x00140154
		public static ConditionalAccessExpressionSyntax ConditionalAccessExpression(ExpressionSyntax expression, SyntaxToken operatorToken, ExpressionSyntax whenNotNull)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8691, expression, operatorToken, whenNotNull, out num);
			if (greenNode != null)
			{
				return (ConditionalAccessExpressionSyntax)greenNode;
			}
			ConditionalAccessExpressionSyntax conditionalAccessExpressionSyntax = new ConditionalAccessExpressionSyntax(SyntaxKind.ConditionalAccessExpression, expression, operatorToken, whenNotNull);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(conditionalAccessExpressionSyntax, num);
			}
			return conditionalAccessExpressionSyntax;
		}

		// Token: 0x06004F7D RID: 20349 RVA: 0x00141F98 File Offset: 0x00140198
		public static MemberBindingExpressionSyntax MemberBindingExpression(SyntaxToken operatorToken, SimpleNameSyntax name)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8707, operatorToken, name, out num);
			if (greenNode != null)
			{
				return (MemberBindingExpressionSyntax)greenNode;
			}
			MemberBindingExpressionSyntax memberBindingExpressionSyntax = new MemberBindingExpressionSyntax(SyntaxKind.MemberBindingExpression, operatorToken, name);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(memberBindingExpressionSyntax, num);
			}
			return memberBindingExpressionSyntax;
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x00141FD8 File Offset: 0x001401D8
		public static ElementBindingExpressionSyntax ElementBindingExpression(BracketedArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8708, argumentList, out num);
			if (greenNode != null)
			{
				return (ElementBindingExpressionSyntax)greenNode;
			}
			ElementBindingExpressionSyntax elementBindingExpressionSyntax = new ElementBindingExpressionSyntax(SyntaxKind.ElementBindingExpression, argumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(elementBindingExpressionSyntax, num);
			}
			return elementBindingExpressionSyntax;
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x00142018 File Offset: 0x00140218
		public static RangeExpressionSyntax RangeExpression([global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax leftOperand, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax rightOperand)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8658, leftOperand, operatorToken, rightOperand, out num);
			if (greenNode != null)
			{
				return (RangeExpressionSyntax)greenNode;
			}
			RangeExpressionSyntax rangeExpressionSyntax = new RangeExpressionSyntax(SyntaxKind.RangeExpression, leftOperand, operatorToken, rightOperand);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(rangeExpressionSyntax, num);
			}
			return rangeExpressionSyntax;
		}

		// Token: 0x06004F80 RID: 20352 RVA: 0x0014205C File Offset: 0x0014025C
		public static ImplicitElementAccessSyntax ImplicitElementAccess(BracketedArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8656, argumentList, out num);
			if (greenNode != null)
			{
				return (ImplicitElementAccessSyntax)greenNode;
			}
			ImplicitElementAccessSyntax implicitElementAccessSyntax = new ImplicitElementAccessSyntax(SyntaxKind.ImplicitElementAccess, argumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(implicitElementAccessSyntax, num);
			}
			return implicitElementAccessSyntax;
		}

		// Token: 0x06004F81 RID: 20353 RVA: 0x0014209C File Offset: 0x0014029C
		public static BinaryExpressionSyntax BinaryExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (kind - SyntaxKind.AddExpression > 20)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, left, operatorToken, right, out num);
			if (greenNode != null)
			{
				return (BinaryExpressionSyntax)greenNode;
			}
			BinaryExpressionSyntax binaryExpressionSyntax = new BinaryExpressionSyntax(kind, left, operatorToken, right);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(binaryExpressionSyntax, num);
			}
			return binaryExpressionSyntax;
		}

		// Token: 0x06004F82 RID: 20354 RVA: 0x001420EC File Offset: 0x001402EC
		public static AssignmentExpressionSyntax AssignmentExpression(SyntaxKind kind, ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
		{
			if (kind - SyntaxKind.SimpleAssignmentExpression > 11)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, left, operatorToken, right, out num);
			if (greenNode != null)
			{
				return (AssignmentExpressionSyntax)greenNode;
			}
			AssignmentExpressionSyntax assignmentExpressionSyntax = new AssignmentExpressionSyntax(kind, left, operatorToken, right);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(assignmentExpressionSyntax, num);
			}
			return assignmentExpressionSyntax;
		}

		// Token: 0x06004F83 RID: 20355 RVA: 0x0014213B File Offset: 0x0014033B
		public static ConditionalExpressionSyntax ConditionalExpression(ExpressionSyntax condition, SyntaxToken questionToken, ExpressionSyntax whenTrue, SyntaxToken colonToken, ExpressionSyntax whenFalse)
		{
			return new ConditionalExpressionSyntax(SyntaxKind.ConditionalExpression, condition, questionToken, whenTrue, colonToken, whenFalse);
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x00142150 File Offset: 0x00140350
		public static ThisExpressionSyntax ThisExpression(SyntaxToken token)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8746, token, out num);
			if (greenNode != null)
			{
				return (ThisExpressionSyntax)greenNode;
			}
			ThisExpressionSyntax thisExpressionSyntax = new ThisExpressionSyntax(SyntaxKind.ThisExpression, token);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(thisExpressionSyntax, num);
			}
			return thisExpressionSyntax;
		}

		// Token: 0x06004F85 RID: 20357 RVA: 0x00142190 File Offset: 0x00140390
		public static BaseExpressionSyntax BaseExpression(SyntaxToken token)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8747, token, out num);
			if (greenNode != null)
			{
				return (BaseExpressionSyntax)greenNode;
			}
			BaseExpressionSyntax baseExpressionSyntax = new BaseExpressionSyntax(SyntaxKind.BaseExpression, token);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(baseExpressionSyntax, num);
			}
			return baseExpressionSyntax;
		}

		// Token: 0x06004F86 RID: 20358 RVA: 0x001421D0 File Offset: 0x001403D0
		public static LiteralExpressionSyntax LiteralExpression(SyntaxKind kind, SyntaxToken token)
		{
			if (kind - SyntaxKind.ArgListExpression > 7)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, token, out num);
			if (greenNode != null)
			{
				return (LiteralExpressionSyntax)greenNode;
			}
			LiteralExpressionSyntax literalExpressionSyntax = new LiteralExpressionSyntax(kind, token);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(literalExpressionSyntax, num);
			}
			return literalExpressionSyntax;
		}

		// Token: 0x06004F87 RID: 20359 RVA: 0x0014221A File Offset: 0x0014041A
		public static MakeRefExpressionSyntax MakeRefExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			return new MakeRefExpressionSyntax(SyntaxKind.MakeRefExpression, keyword, openParenToken, expression, closeParenToken);
		}

		// Token: 0x06004F88 RID: 20360 RVA: 0x0014222A File Offset: 0x0014042A
		public static RefTypeExpressionSyntax RefTypeExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			return new RefTypeExpressionSyntax(SyntaxKind.RefTypeExpression, keyword, openParenToken, expression, closeParenToken);
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x0014223A File Offset: 0x0014043A
		public static RefValueExpressionSyntax RefValueExpression(SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken comma, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new RefValueExpressionSyntax(SyntaxKind.RefValueExpression, keyword, openParenToken, expression, comma, type, closeParenToken);
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x0014224E File Offset: 0x0014044E
		public static CheckedExpressionSyntax CheckedExpression(SyntaxKind kind, SyntaxToken keyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
		{
			if (kind - SyntaxKind.CheckedExpression > 1)
			{
				throw new ArgumentException("kind");
			}
			return new CheckedExpressionSyntax(kind, keyword, openParenToken, expression, closeParenToken);
		}

		// Token: 0x06004F8B RID: 20363 RVA: 0x00142270 File Offset: 0x00140470
		public static DefaultExpressionSyntax DefaultExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new DefaultExpressionSyntax(SyntaxKind.DefaultExpression, keyword, openParenToken, type, closeParenToken);
		}

		// Token: 0x06004F8C RID: 20364 RVA: 0x00142280 File Offset: 0x00140480
		public static TypeOfExpressionSyntax TypeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new TypeOfExpressionSyntax(SyntaxKind.TypeOfExpression, keyword, openParenToken, type, closeParenToken);
		}

		// Token: 0x06004F8D RID: 20365 RVA: 0x00142290 File Offset: 0x00140490
		public static SizeOfExpressionSyntax SizeOfExpression(SyntaxToken keyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken)
		{
			return new SizeOfExpressionSyntax(SyntaxKind.SizeOfExpression, keyword, openParenToken, type, closeParenToken);
		}

		// Token: 0x06004F8E RID: 20366 RVA: 0x001422A0 File Offset: 0x001404A0
		public static InvocationExpressionSyntax InvocationExpression(ExpressionSyntax expression, ArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8634, expression, argumentList, out num);
			if (greenNode != null)
			{
				return (InvocationExpressionSyntax)greenNode;
			}
			InvocationExpressionSyntax invocationExpressionSyntax = new InvocationExpressionSyntax(SyntaxKind.InvocationExpression, expression, argumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(invocationExpressionSyntax, num);
			}
			return invocationExpressionSyntax;
		}

		// Token: 0x06004F8F RID: 20367 RVA: 0x001422E0 File Offset: 0x001404E0
		public static ElementAccessExpressionSyntax ElementAccessExpression(ExpressionSyntax expression, BracketedArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8635, expression, argumentList, out num);
			if (greenNode != null)
			{
				return (ElementAccessExpressionSyntax)greenNode;
			}
			ElementAccessExpressionSyntax elementAccessExpressionSyntax = new ElementAccessExpressionSyntax(SyntaxKind.ElementAccessExpression, expression, argumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(elementAccessExpressionSyntax, num);
			}
			return elementAccessExpressionSyntax;
		}

		// Token: 0x06004F90 RID: 20368 RVA: 0x00142320 File Offset: 0x00140520
		public static ArgumentListSyntax ArgumentList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8636, openParenToken, arguments.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (ArgumentListSyntax)greenNode;
			}
			ArgumentListSyntax argumentListSyntax = new ArgumentListSyntax(SyntaxKind.ArgumentList, openParenToken, arguments.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(argumentListSyntax, num);
			}
			return argumentListSyntax;
		}

		// Token: 0x06004F91 RID: 20369 RVA: 0x00142370 File Offset: 0x00140570
		public static BracketedArgumentListSyntax BracketedArgumentList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ArgumentSyntax> arguments, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8637, openBracketToken, arguments.Node, closeBracketToken, out num);
			if (greenNode != null)
			{
				return (BracketedArgumentListSyntax)greenNode;
			}
			BracketedArgumentListSyntax bracketedArgumentListSyntax = new BracketedArgumentListSyntax(SyntaxKind.BracketedArgumentList, openBracketToken, arguments.Node, closeBracketToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(bracketedArgumentListSyntax, num);
			}
			return bracketedArgumentListSyntax;
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x001423C0 File Offset: 0x001405C0
		public static ArgumentSyntax Argument([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8638, nameColon, refKindKeyword, expression, out num);
			if (greenNode != null)
			{
				return (ArgumentSyntax)greenNode;
			}
			ArgumentSyntax argumentSyntax = new ArgumentSyntax(SyntaxKind.Argument, nameColon, refKindKeyword, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(argumentSyntax, num);
			}
			return argumentSyntax;
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x00142404 File Offset: 0x00140604
		public static NameColonSyntax NameColon(IdentifierNameSyntax name, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8639, name, colonToken, out num);
			if (greenNode != null)
			{
				return (NameColonSyntax)greenNode;
			}
			NameColonSyntax nameColonSyntax = new NameColonSyntax(SyntaxKind.NameColon, name, colonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nameColonSyntax, num);
			}
			return nameColonSyntax;
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x00142444 File Offset: 0x00140644
		public static DeclarationExpressionSyntax DeclarationExpression(TypeSyntax type, VariableDesignationSyntax designation)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9040, type, designation, out num);
			if (greenNode != null)
			{
				return (DeclarationExpressionSyntax)greenNode;
			}
			DeclarationExpressionSyntax declarationExpressionSyntax = new DeclarationExpressionSyntax(SyntaxKind.DeclarationExpression, type, designation);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(declarationExpressionSyntax, num);
			}
			return declarationExpressionSyntax;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x00142483 File Offset: 0x00140683
		public static CastExpressionSyntax CastExpression(SyntaxToken openParenToken, TypeSyntax type, SyntaxToken closeParenToken, ExpressionSyntax expression)
		{
			return new CastExpressionSyntax(SyntaxKind.CastExpression, openParenToken, type, closeParenToken, expression);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00142493 File Offset: 0x00140693
		public static AnonymousMethodExpressionSyntax AnonymousMethodExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken delegateKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ParameterListSyntax parameterList, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return new AnonymousMethodExpressionSyntax(SyntaxKind.AnonymousMethodExpression, modifiers.Node, delegateKeyword, parameterList, block, expressionBody);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x001424AB File Offset: 0x001406AB
		public static SimpleLambdaExpressionSyntax SimpleLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, ParameterSyntax parameter, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return new SimpleLambdaExpressionSyntax(SyntaxKind.SimpleLambdaExpression, attributeLists.Node, modifiers.Node, parameter, arrowToken, block, expressionBody);
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x001424CC File Offset: 0x001406CC
		public static RefExpressionSyntax RefExpression(SyntaxToken refKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9050, refKeyword, expression, out num);
			if (greenNode != null)
			{
				return (RefExpressionSyntax)greenNode;
			}
			RefExpressionSyntax refExpressionSyntax = new RefExpressionSyntax(SyntaxKind.RefExpression, refKeyword, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(refExpressionSyntax, num);
			}
			return refExpressionSyntax;
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x0014250B File Offset: 0x0014070B
		public static ParenthesizedLambdaExpressionSyntax ParenthesizedLambdaExpression([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, ParameterListSyntax parameterList, SyntaxToken arrowToken, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expressionBody)
		{
			return new ParenthesizedLambdaExpressionSyntax(SyntaxKind.ParenthesizedLambdaExpression, attributeLists.Node, modifiers.Node, parameterList, arrowToken, block, expressionBody);
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x0014252C File Offset: 0x0014072C
		public static InitializerExpressionSyntax InitializerExpression(SyntaxKind kind, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
		{
			if (kind - SyntaxKind.ObjectInitializerExpression > 2 && kind != SyntaxKind.ComplexElementInitializerExpression && kind != SyntaxKind.WithInitializerExpression)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, openBraceToken, expressions.Node, closeBraceToken, out num);
			if (greenNode != null)
			{
				return (InitializerExpressionSyntax)greenNode;
			}
			InitializerExpressionSyntax initializerExpressionSyntax = new InitializerExpressionSyntax(kind, openBraceToken, expressions.Node, closeBraceToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(initializerExpressionSyntax, num);
			}
			return initializerExpressionSyntax;
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x00142598 File Offset: 0x00140798
		public static ImplicitObjectCreationExpressionSyntax ImplicitObjectCreationExpression(SyntaxToken newKeyword, ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8659, newKeyword, argumentList, initializer, out num);
			if (greenNode != null)
			{
				return (ImplicitObjectCreationExpressionSyntax)greenNode;
			}
			ImplicitObjectCreationExpressionSyntax implicitObjectCreationExpressionSyntax = new ImplicitObjectCreationExpressionSyntax(SyntaxKind.ImplicitObjectCreationExpression, newKeyword, argumentList, initializer);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(implicitObjectCreationExpressionSyntax, num);
			}
			return implicitObjectCreationExpressionSyntax;
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x001425D9 File Offset: 0x001407D9
		public static ObjectCreationExpressionSyntax ObjectCreationExpression(SyntaxToken newKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			return new ObjectCreationExpressionSyntax(SyntaxKind.ObjectCreationExpression, newKeyword, type, argumentList, initializer);
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x001425EC File Offset: 0x001407EC
		public static WithExpressionSyntax WithExpression(ExpressionSyntax expression, SyntaxToken withKeyword, InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9061, expression, withKeyword, initializer, out num);
			if (greenNode != null)
			{
				return (WithExpressionSyntax)greenNode;
			}
			WithExpressionSyntax withExpressionSyntax = new WithExpressionSyntax(SyntaxKind.WithExpression, expression, withKeyword, initializer);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(withExpressionSyntax, num);
			}
			return withExpressionSyntax;
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00142630 File Offset: 0x00140830
		public static AnonymousObjectMemberDeclaratorSyntax AnonymousObjectMemberDeclarator([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8647, nameEquals, expression, out num);
			if (greenNode != null)
			{
				return (AnonymousObjectMemberDeclaratorSyntax)greenNode;
			}
			AnonymousObjectMemberDeclaratorSyntax anonymousObjectMemberDeclaratorSyntax = new AnonymousObjectMemberDeclaratorSyntax(SyntaxKind.AnonymousObjectMemberDeclarator, nameEquals, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(anonymousObjectMemberDeclaratorSyntax, num);
			}
			return anonymousObjectMemberDeclaratorSyntax;
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0014266F File Offset: 0x0014086F
		public static AnonymousObjectCreationExpressionSyntax AnonymousObjectCreationExpression(SyntaxToken newKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AnonymousObjectMemberDeclaratorSyntax> initializers, SyntaxToken closeBraceToken)
		{
			return new AnonymousObjectCreationExpressionSyntax(SyntaxKind.AnonymousObjectCreationExpression, newKeyword, openBraceToken, initializers.Node, closeBraceToken);
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00142688 File Offset: 0x00140888
		public static ArrayCreationExpressionSyntax ArrayCreationExpression(SyntaxToken newKeyword, ArrayTypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8651, newKeyword, type, initializer, out num);
			if (greenNode != null)
			{
				return (ArrayCreationExpressionSyntax)greenNode;
			}
			ArrayCreationExpressionSyntax arrayCreationExpressionSyntax = new ArrayCreationExpressionSyntax(SyntaxKind.ArrayCreationExpression, newKeyword, type, initializer);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrayCreationExpressionSyntax, num);
			}
			return arrayCreationExpressionSyntax;
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x001426C9 File Offset: 0x001408C9
		public static ImplicitArrayCreationExpressionSyntax ImplicitArrayCreationExpression(SyntaxToken newKeyword, SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> commas, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			return new ImplicitArrayCreationExpressionSyntax(SyntaxKind.ImplicitArrayCreationExpression, newKeyword, openBracketToken, commas.Node, closeBracketToken, initializer);
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x001426E4 File Offset: 0x001408E4
		public static StackAllocArrayCreationExpressionSyntax StackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializer)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8653, stackAllocKeyword, type, initializer, out num);
			if (greenNode != null)
			{
				return (StackAllocArrayCreationExpressionSyntax)greenNode;
			}
			StackAllocArrayCreationExpressionSyntax stackAllocArrayCreationExpressionSyntax = new StackAllocArrayCreationExpressionSyntax(SyntaxKind.StackAllocArrayCreationExpression, stackAllocKeyword, type, initializer);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(stackAllocArrayCreationExpressionSyntax, num);
			}
			return stackAllocArrayCreationExpressionSyntax;
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x00142725 File Offset: 0x00140925
		public static ImplicitStackAllocArrayCreationExpressionSyntax ImplicitStackAllocArrayCreationExpression(SyntaxToken stackAllocKeyword, SyntaxToken openBracketToken, SyntaxToken closeBracketToken, InitializerExpressionSyntax initializer)
		{
			return new ImplicitStackAllocArrayCreationExpressionSyntax(SyntaxKind.ImplicitStackAllocArrayCreationExpression, stackAllocKeyword, openBracketToken, closeBracketToken, initializer);
		}

		// Token: 0x06004FA4 RID: 20388 RVA: 0x00142738 File Offset: 0x00140938
		public static QueryExpressionSyntax QueryExpression(FromClauseSyntax fromClause, QueryBodySyntax body)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8774, fromClause, body, out num);
			if (greenNode != null)
			{
				return (QueryExpressionSyntax)greenNode;
			}
			QueryExpressionSyntax queryExpressionSyntax = new QueryExpressionSyntax(SyntaxKind.QueryExpression, fromClause, body);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(queryExpressionSyntax, num);
			}
			return queryExpressionSyntax;
		}

		// Token: 0x06004FA5 RID: 20389 RVA: 0x00142778 File Offset: 0x00140978
		public static QueryBodySyntax QueryBody([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<QueryClauseSyntax> clauses, SelectOrGroupClauseSyntax selectOrGroup, [global::System.Runtime.CompilerServices.Nullable(2)] QueryContinuationSyntax continuation)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8775, clauses.Node, selectOrGroup, continuation, out num);
			if (greenNode != null)
			{
				return (QueryBodySyntax)greenNode;
			}
			QueryBodySyntax queryBodySyntax = new QueryBodySyntax(SyntaxKind.QueryBody, clauses.Node, selectOrGroup, continuation);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(queryBodySyntax, num);
			}
			return queryBodySyntax;
		}

		// Token: 0x06004FA6 RID: 20390 RVA: 0x001427C5 File Offset: 0x001409C5
		public static FromClauseSyntax FromClause(SyntaxToken fromKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression)
		{
			return new FromClauseSyntax(SyntaxKind.FromClause, fromKeyword, type, identifier, inKeyword, expression);
		}

		// Token: 0x06004FA7 RID: 20391 RVA: 0x001427D7 File Offset: 0x001409D7
		public static LetClauseSyntax LetClause(SyntaxToken letKeyword, SyntaxToken identifier, SyntaxToken equalsToken, ExpressionSyntax expression)
		{
			return new LetClauseSyntax(SyntaxKind.LetClause, letKeyword, identifier, equalsToken, expression);
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x001427E8 File Offset: 0x001409E8
		public static JoinClauseSyntax JoinClause(SyntaxToken joinKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax inExpression, SyntaxToken onKeyword, ExpressionSyntax leftExpression, SyntaxToken equalsKeyword, ExpressionSyntax rightExpression, [global::System.Runtime.CompilerServices.Nullable(2)] JoinIntoClauseSyntax into)
		{
			return new JoinClauseSyntax(SyntaxKind.JoinClause, joinKeyword, type, identifier, inKeyword, inExpression, onKeyword, leftExpression, equalsKeyword, rightExpression, into);
		}

		// Token: 0x06004FA9 RID: 20393 RVA: 0x00142810 File Offset: 0x00140A10
		public static JoinIntoClauseSyntax JoinIntoClause(SyntaxToken intoKeyword, SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8779, intoKeyword, identifier, out num);
			if (greenNode != null)
			{
				return (JoinIntoClauseSyntax)greenNode;
			}
			JoinIntoClauseSyntax joinIntoClauseSyntax = new JoinIntoClauseSyntax(SyntaxKind.JoinIntoClause, intoKeyword, identifier);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(joinIntoClauseSyntax, num);
			}
			return joinIntoClauseSyntax;
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x00142850 File Offset: 0x00140A50
		public static WhereClauseSyntax WhereClause(SyntaxToken whereKeyword, ExpressionSyntax condition)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8780, whereKeyword, condition, out num);
			if (greenNode != null)
			{
				return (WhereClauseSyntax)greenNode;
			}
			WhereClauseSyntax whereClauseSyntax = new WhereClauseSyntax(SyntaxKind.WhereClause, whereKeyword, condition);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(whereClauseSyntax, num);
			}
			return whereClauseSyntax;
		}

		// Token: 0x06004FAB RID: 20395 RVA: 0x00142890 File Offset: 0x00140A90
		public static OrderByClauseSyntax OrderByClause(SyntaxToken orderByKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<OrderingSyntax> orderings)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8781, orderByKeyword, orderings.Node, out num);
			if (greenNode != null)
			{
				return (OrderByClauseSyntax)greenNode;
			}
			OrderByClauseSyntax orderByClauseSyntax = new OrderByClauseSyntax(SyntaxKind.OrderByClause, orderByKeyword, orderings.Node);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(orderByClauseSyntax, num);
			}
			return orderByClauseSyntax;
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x001428DC File Offset: 0x00140ADC
		public static OrderingSyntax Ordering(SyntaxKind kind, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken ascendingOrDescendingKeyword)
		{
			if (kind - SyntaxKind.AscendingOrdering > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, expression, ascendingOrDescendingKeyword, out num);
			if (greenNode != null)
			{
				return (OrderingSyntax)greenNode;
			}
			OrderingSyntax orderingSyntax = new OrderingSyntax(kind, expression, ascendingOrDescendingKeyword);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(orderingSyntax, num);
			}
			return orderingSyntax;
		}

		// Token: 0x06004FAD RID: 20397 RVA: 0x00142928 File Offset: 0x00140B28
		public static SelectClauseSyntax SelectClause(SyntaxToken selectKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8784, selectKeyword, expression, out num);
			if (greenNode != null)
			{
				return (SelectClauseSyntax)greenNode;
			}
			SelectClauseSyntax selectClauseSyntax = new SelectClauseSyntax(SyntaxKind.SelectClause, selectKeyword, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(selectClauseSyntax, num);
			}
			return selectClauseSyntax;
		}

		// Token: 0x06004FAE RID: 20398 RVA: 0x00142967 File Offset: 0x00140B67
		public static GroupClauseSyntax GroupClause(SyntaxToken groupKeyword, ExpressionSyntax groupExpression, SyntaxToken byKeyword, ExpressionSyntax byExpression)
		{
			return new GroupClauseSyntax(SyntaxKind.GroupClause, groupKeyword, groupExpression, byKeyword, byExpression);
		}

		// Token: 0x06004FAF RID: 20399 RVA: 0x00142978 File Offset: 0x00140B78
		public static QueryContinuationSyntax QueryContinuation(SyntaxToken intoKeyword, SyntaxToken identifier, QueryBodySyntax body)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8786, intoKeyword, identifier, body, out num);
			if (greenNode != null)
			{
				return (QueryContinuationSyntax)greenNode;
			}
			QueryContinuationSyntax queryContinuationSyntax = new QueryContinuationSyntax(SyntaxKind.QueryContinuation, intoKeyword, identifier, body);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(queryContinuationSyntax, num);
			}
			return queryContinuationSyntax;
		}

		// Token: 0x06004FB0 RID: 20400 RVA: 0x001429BC File Offset: 0x00140BBC
		public static OmittedArraySizeExpressionSyntax OmittedArraySizeExpression(SyntaxToken omittedArraySizeExpressionToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8654, omittedArraySizeExpressionToken, out num);
			if (greenNode != null)
			{
				return (OmittedArraySizeExpressionSyntax)greenNode;
			}
			OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax = new OmittedArraySizeExpressionSyntax(SyntaxKind.OmittedArraySizeExpression, omittedArraySizeExpressionToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(omittedArraySizeExpressionSyntax, num);
			}
			return omittedArraySizeExpressionSyntax;
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x001429FC File Offset: 0x00140BFC
		public static InterpolatedStringExpressionSyntax InterpolatedStringExpression(SyntaxToken stringStartToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<InterpolatedStringContentSyntax> contents, SyntaxToken stringEndToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8655, stringStartToken, contents.Node, stringEndToken, out num);
			if (greenNode != null)
			{
				return (InterpolatedStringExpressionSyntax)greenNode;
			}
			InterpolatedStringExpressionSyntax interpolatedStringExpressionSyntax = new InterpolatedStringExpressionSyntax(SyntaxKind.InterpolatedStringExpression, stringStartToken, contents.Node, stringEndToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolatedStringExpressionSyntax, num);
			}
			return interpolatedStringExpressionSyntax;
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x00142A4C File Offset: 0x00140C4C
		public static IsPatternExpressionSyntax IsPatternExpression(ExpressionSyntax expression, SyntaxToken isKeyword, PatternSyntax pattern)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8657, expression, isKeyword, pattern, out num);
			if (greenNode != null)
			{
				return (IsPatternExpressionSyntax)greenNode;
			}
			IsPatternExpressionSyntax isPatternExpressionSyntax = new IsPatternExpressionSyntax(SyntaxKind.IsPatternExpression, expression, isKeyword, pattern);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(isPatternExpressionSyntax, num);
			}
			return isPatternExpressionSyntax;
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x00142A90 File Offset: 0x00140C90
		public static ThrowExpressionSyntax ThrowExpression(SyntaxToken throwKeyword, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9052, throwKeyword, expression, out num);
			if (greenNode != null)
			{
				return (ThrowExpressionSyntax)greenNode;
			}
			ThrowExpressionSyntax throwExpressionSyntax = new ThrowExpressionSyntax(SyntaxKind.ThrowExpression, throwKeyword, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(throwExpressionSyntax, num);
			}
			return throwExpressionSyntax;
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00142AD0 File Offset: 0x00140CD0
		public static WhenClauseSyntax WhenClause(SyntaxToken whenKeyword, ExpressionSyntax condition)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9013, whenKeyword, condition, out num);
			if (greenNode != null)
			{
				return (WhenClauseSyntax)greenNode;
			}
			WhenClauseSyntax whenClauseSyntax = new WhenClauseSyntax(SyntaxKind.WhenClause, whenKeyword, condition);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(whenClauseSyntax, num);
			}
			return whenClauseSyntax;
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00142B10 File Offset: 0x00140D10
		public static DiscardPatternSyntax DiscardPattern(SyntaxToken underscoreToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9024, underscoreToken, out num);
			if (greenNode != null)
			{
				return (DiscardPatternSyntax)greenNode;
			}
			DiscardPatternSyntax discardPatternSyntax = new DiscardPatternSyntax(SyntaxKind.DiscardPattern, underscoreToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(discardPatternSyntax, num);
			}
			return discardPatternSyntax;
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00142B50 File Offset: 0x00140D50
		public static DeclarationPatternSyntax DeclarationPattern(TypeSyntax type, VariableDesignationSyntax designation)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9000, type, designation, out num);
			if (greenNode != null)
			{
				return (DeclarationPatternSyntax)greenNode;
			}
			DeclarationPatternSyntax declarationPatternSyntax = new DeclarationPatternSyntax(SyntaxKind.DeclarationPattern, type, designation);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(declarationPatternSyntax, num);
			}
			return declarationPatternSyntax;
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x00142B90 File Offset: 0x00140D90
		public static VarPatternSyntax VarPattern(SyntaxToken varKeyword, VariableDesignationSyntax designation)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9027, varKeyword, designation, out num);
			if (greenNode != null)
			{
				return (VarPatternSyntax)greenNode;
			}
			VarPatternSyntax varPatternSyntax = new VarPatternSyntax(SyntaxKind.VarPattern, varKeyword, designation);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(varPatternSyntax, num);
			}
			return varPatternSyntax;
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x00142BCF File Offset: 0x00140DCF
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static RecursivePatternSyntax RecursivePattern(TypeSyntax type, PositionalPatternClauseSyntax positionalPatternClause, PropertyPatternClauseSyntax propertyPatternClause, VariableDesignationSyntax designation)
		{
			return new RecursivePatternSyntax(SyntaxKind.RecursivePattern, type, positionalPatternClause, propertyPatternClause, designation);
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x00142BE0 File Offset: 0x00140DE0
		public static PositionalPatternClauseSyntax PositionalPatternClause(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9023, openParenToken, subpatterns.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (PositionalPatternClauseSyntax)greenNode;
			}
			PositionalPatternClauseSyntax positionalPatternClauseSyntax = new PositionalPatternClauseSyntax(SyntaxKind.PositionalPatternClause, openParenToken, subpatterns.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(positionalPatternClauseSyntax, num);
			}
			return positionalPatternClauseSyntax;
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x00142C30 File Offset: 0x00140E30
		public static PropertyPatternClauseSyntax PropertyPatternClause(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SubpatternSyntax> subpatterns, SyntaxToken closeBraceToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9021, openBraceToken, subpatterns.Node, closeBraceToken, out num);
			if (greenNode != null)
			{
				return (PropertyPatternClauseSyntax)greenNode;
			}
			PropertyPatternClauseSyntax propertyPatternClauseSyntax = new PropertyPatternClauseSyntax(SyntaxKind.PropertyPatternClause, openBraceToken, subpatterns.Node, closeBraceToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(propertyPatternClauseSyntax, num);
			}
			return propertyPatternClauseSyntax;
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x00142C80 File Offset: 0x00140E80
		public static SubpatternSyntax Subpattern([global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, PatternSyntax pattern)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9022, nameColon, pattern, out num);
			if (greenNode != null)
			{
				return (SubpatternSyntax)greenNode;
			}
			SubpatternSyntax subpatternSyntax = new SubpatternSyntax(SyntaxKind.Subpattern, nameColon, pattern);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(subpatternSyntax, num);
			}
			return subpatternSyntax;
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x00142CC0 File Offset: 0x00140EC0
		public static ConstantPatternSyntax ConstantPattern(ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9002, expression, out num);
			if (greenNode != null)
			{
				return (ConstantPatternSyntax)greenNode;
			}
			ConstantPatternSyntax constantPatternSyntax = new ConstantPatternSyntax(SyntaxKind.ConstantPattern, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(constantPatternSyntax, num);
			}
			return constantPatternSyntax;
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x00142D00 File Offset: 0x00140F00
		public static ParenthesizedPatternSyntax ParenthesizedPattern(SyntaxToken openParenToken, PatternSyntax pattern, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9028, openParenToken, pattern, closeParenToken, out num);
			if (greenNode != null)
			{
				return (ParenthesizedPatternSyntax)greenNode;
			}
			ParenthesizedPatternSyntax parenthesizedPatternSyntax = new ParenthesizedPatternSyntax(SyntaxKind.ParenthesizedPattern, openParenToken, pattern, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parenthesizedPatternSyntax, num);
			}
			return parenthesizedPatternSyntax;
		}

		// Token: 0x06004FBE RID: 20414 RVA: 0x00142D44 File Offset: 0x00140F44
		public static RelationalPatternSyntax RelationalPattern(SyntaxToken operatorToken, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9029, operatorToken, expression, out num);
			if (greenNode != null)
			{
				return (RelationalPatternSyntax)greenNode;
			}
			RelationalPatternSyntax relationalPatternSyntax = new RelationalPatternSyntax(SyntaxKind.RelationalPattern, operatorToken, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(relationalPatternSyntax, num);
			}
			return relationalPatternSyntax;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x00142D84 File Offset: 0x00140F84
		public static TypePatternSyntax TypePattern(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9030, type, out num);
			if (greenNode != null)
			{
				return (TypePatternSyntax)greenNode;
			}
			TypePatternSyntax typePatternSyntax = new TypePatternSyntax(SyntaxKind.TypePattern, type);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typePatternSyntax, num);
			}
			return typePatternSyntax;
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x00142DC4 File Offset: 0x00140FC4
		public static BinaryPatternSyntax BinaryPattern(SyntaxKind kind, PatternSyntax left, SyntaxToken operatorToken, PatternSyntax right)
		{
			if (kind - SyntaxKind.OrPattern > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, left, operatorToken, right, out num);
			if (greenNode != null)
			{
				return (BinaryPatternSyntax)greenNode;
			}
			BinaryPatternSyntax binaryPatternSyntax = new BinaryPatternSyntax(kind, left, operatorToken, right);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(binaryPatternSyntax, num);
			}
			return binaryPatternSyntax;
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x00142E14 File Offset: 0x00141014
		public static UnaryPatternSyntax UnaryPattern(SyntaxToken operatorToken, PatternSyntax pattern)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9033, operatorToken, pattern, out num);
			if (greenNode != null)
			{
				return (UnaryPatternSyntax)greenNode;
			}
			UnaryPatternSyntax unaryPatternSyntax = new UnaryPatternSyntax(SyntaxKind.NotPattern, operatorToken, pattern);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(unaryPatternSyntax, num);
			}
			return unaryPatternSyntax;
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x00142E54 File Offset: 0x00141054
		public static InterpolatedStringTextSyntax InterpolatedStringText(SyntaxToken textToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8919, textToken, out num);
			if (greenNode != null)
			{
				return (InterpolatedStringTextSyntax)greenNode;
			}
			InterpolatedStringTextSyntax interpolatedStringTextSyntax = new InterpolatedStringTextSyntax(SyntaxKind.InterpolatedStringText, textToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolatedStringTextSyntax, num);
			}
			return interpolatedStringTextSyntax;
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x00142E91 File Offset: 0x00141091
		public static InterpolationSyntax Interpolation(SyntaxToken openBraceToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationAlignmentClauseSyntax alignmentClause, [global::System.Runtime.CompilerServices.Nullable(2)] InterpolationFormatClauseSyntax formatClause, SyntaxToken closeBraceToken)
		{
			return new InterpolationSyntax(SyntaxKind.Interpolation, openBraceToken, expression, alignmentClause, formatClause, closeBraceToken);
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x00142EA4 File Offset: 0x001410A4
		public static InterpolationAlignmentClauseSyntax InterpolationAlignmentClause(SyntaxToken commaToken, ExpressionSyntax value)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8920, commaToken, value, out num);
			if (greenNode != null)
			{
				return (InterpolationAlignmentClauseSyntax)greenNode;
			}
			InterpolationAlignmentClauseSyntax interpolationAlignmentClauseSyntax = new InterpolationAlignmentClauseSyntax(SyntaxKind.InterpolationAlignmentClause, commaToken, value);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolationAlignmentClauseSyntax, num);
			}
			return interpolationAlignmentClauseSyntax;
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x00142EE4 File Offset: 0x001410E4
		public static InterpolationFormatClauseSyntax InterpolationFormatClause(SyntaxToken colonToken, SyntaxToken formatStringToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8921, colonToken, formatStringToken, out num);
			if (greenNode != null)
			{
				return (InterpolationFormatClauseSyntax)greenNode;
			}
			InterpolationFormatClauseSyntax interpolationFormatClauseSyntax = new InterpolationFormatClauseSyntax(SyntaxKind.InterpolationFormatClause, colonToken, formatStringToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(interpolationFormatClauseSyntax, num);
			}
			return interpolationFormatClauseSyntax;
		}

		// Token: 0x06004FC6 RID: 20422 RVA: 0x00142F24 File Offset: 0x00141124
		public static GlobalStatementSyntax GlobalStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, StatementSyntax statement)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8841, attributeLists.Node, modifiers.Node, statement, out num);
			if (greenNode != null)
			{
				return (GlobalStatementSyntax)greenNode;
			}
			GlobalStatementSyntax globalStatementSyntax = new GlobalStatementSyntax(SyntaxKind.GlobalStatement, attributeLists.Node, modifiers.Node, statement);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(globalStatementSyntax, num);
			}
			return globalStatementSyntax;
		}

		// Token: 0x06004FC7 RID: 20423 RVA: 0x00142F7D File Offset: 0x0014117D
		public static BlockSyntax Block([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements, SyntaxToken closeBraceToken)
		{
			return new BlockSyntax(SyntaxKind.Block, attributeLists.Node, openBraceToken, statements.Node, closeBraceToken);
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x00142F9C File Offset: 0x0014119C
		public static LocalFunctionStatementSyntax LocalFunctionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new LocalFunctionStatementSyntax(SyntaxKind.LocalFunctionStatement, attributeLists.Node, modifiers.Node, returnType, identifier, typeParameterList, parameterList, constraintClauses.Node, body, expressionBody, semicolonToken);
		}

		// Token: 0x06004FC9 RID: 20425 RVA: 0x00142FD4 File Offset: 0x001411D4
		public static LocalDeclarationStatementSyntax LocalDeclarationStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return new LocalDeclarationStatementSyntax(SyntaxKind.LocalDeclarationStatement, attributeLists.Node, awaitKeyword, usingKeyword, modifiers.Node, declaration, semicolonToken);
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x00142FF4 File Offset: 0x001411F4
		public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8794, type, variables.Node, out num);
			if (greenNode != null)
			{
				return (VariableDeclarationSyntax)greenNode;
			}
			VariableDeclarationSyntax variableDeclarationSyntax = new VariableDeclarationSyntax(SyntaxKind.VariableDeclaration, type, variables.Node);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(variableDeclarationSyntax, num);
			}
			return variableDeclarationSyntax;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x00143040 File Offset: 0x00141240
		public static VariableDeclaratorSyntax VariableDeclarator(SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] BracketedArgumentListSyntax argumentList, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax initializer)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8795, identifier, argumentList, initializer, out num);
			if (greenNode != null)
			{
				return (VariableDeclaratorSyntax)greenNode;
			}
			VariableDeclaratorSyntax variableDeclaratorSyntax = new VariableDeclaratorSyntax(SyntaxKind.VariableDeclarator, identifier, argumentList, initializer);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(variableDeclaratorSyntax, num);
			}
			return variableDeclaratorSyntax;
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x00143084 File Offset: 0x00141284
		public static EqualsValueClauseSyntax EqualsValueClause(SyntaxToken equalsToken, ExpressionSyntax value)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8796, equalsToken, value, out num);
			if (greenNode != null)
			{
				return (EqualsValueClauseSyntax)greenNode;
			}
			EqualsValueClauseSyntax equalsValueClauseSyntax = new EqualsValueClauseSyntax(SyntaxKind.EqualsValueClause, equalsToken, value);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(equalsValueClauseSyntax, num);
			}
			return equalsValueClauseSyntax;
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x001430C4 File Offset: 0x001412C4
		public static SingleVariableDesignationSyntax SingleVariableDesignation(SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8927, identifier, out num);
			if (greenNode != null)
			{
				return (SingleVariableDesignationSyntax)greenNode;
			}
			SingleVariableDesignationSyntax singleVariableDesignationSyntax = new SingleVariableDesignationSyntax(SyntaxKind.SingleVariableDesignation, identifier);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(singleVariableDesignationSyntax, num);
			}
			return singleVariableDesignationSyntax;
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x00143104 File Offset: 0x00141304
		public static DiscardDesignationSyntax DiscardDesignation(SyntaxToken underscoreToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9014, underscoreToken, out num);
			if (greenNode != null)
			{
				return (DiscardDesignationSyntax)greenNode;
			}
			DiscardDesignationSyntax discardDesignationSyntax = new DiscardDesignationSyntax(SyntaxKind.DiscardDesignation, underscoreToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(discardDesignationSyntax, num);
			}
			return discardDesignationSyntax;
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x00143144 File Offset: 0x00141344
		public static ParenthesizedVariableDesignationSyntax ParenthesizedVariableDesignation(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<VariableDesignationSyntax> variables, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8928, openParenToken, variables.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (ParenthesizedVariableDesignationSyntax)greenNode;
			}
			ParenthesizedVariableDesignationSyntax parenthesizedVariableDesignationSyntax = new ParenthesizedVariableDesignationSyntax(SyntaxKind.ParenthesizedVariableDesignation, openParenToken, variables.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parenthesizedVariableDesignationSyntax, num);
			}
			return parenthesizedVariableDesignationSyntax;
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x00143194 File Offset: 0x00141394
		public static ExpressionStatementSyntax ExpressionStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8797, attributeLists.Node, expression, semicolonToken, out num);
			if (greenNode != null)
			{
				return (ExpressionStatementSyntax)greenNode;
			}
			ExpressionStatementSyntax expressionStatementSyntax = new ExpressionStatementSyntax(SyntaxKind.ExpressionStatement, attributeLists.Node, expression, semicolonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(expressionStatementSyntax, num);
			}
			return expressionStatementSyntax;
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x001431E4 File Offset: 0x001413E4
		public static EmptyStatementSyntax EmptyStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8798, attributeLists.Node, semicolonToken, out num);
			if (greenNode != null)
			{
				return (EmptyStatementSyntax)greenNode;
			}
			EmptyStatementSyntax emptyStatementSyntax = new EmptyStatementSyntax(SyntaxKind.EmptyStatement, attributeLists.Node, semicolonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(emptyStatementSyntax, num);
			}
			return emptyStatementSyntax;
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0014322F File Offset: 0x0014142F
		public static LabeledStatementSyntax LabeledStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken identifier, SyntaxToken colonToken, StatementSyntax statement)
		{
			return new LabeledStatementSyntax(SyntaxKind.LabeledStatement, attributeLists.Node, identifier, colonToken, statement);
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x00143245 File Offset: 0x00141445
		public static GotoStatementSyntax GotoStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken gotoKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken caseOrDefaultKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.GotoStatement > 2)
			{
				throw new ArgumentException("kind");
			}
			return new GotoStatementSyntax(kind, attributeLists.Node, gotoKeyword, caseOrDefaultKeyword, expression, semicolonToken);
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x00143270 File Offset: 0x00141470
		public static BreakStatementSyntax BreakStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken breakKeyword, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8803, attributeLists.Node, breakKeyword, semicolonToken, out num);
			if (greenNode != null)
			{
				return (BreakStatementSyntax)greenNode;
			}
			BreakStatementSyntax breakStatementSyntax = new BreakStatementSyntax(SyntaxKind.BreakStatement, attributeLists.Node, breakKeyword, semicolonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(breakStatementSyntax, num);
			}
			return breakStatementSyntax;
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x001432C0 File Offset: 0x001414C0
		public static ContinueStatementSyntax ContinueStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken continueKeyword, SyntaxToken semicolonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8804, attributeLists.Node, continueKeyword, semicolonToken, out num);
			if (greenNode != null)
			{
				return (ContinueStatementSyntax)greenNode;
			}
			ContinueStatementSyntax continueStatementSyntax = new ContinueStatementSyntax(SyntaxKind.ContinueStatement, attributeLists.Node, continueKeyword, semicolonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(continueStatementSyntax, num);
			}
			return continueStatementSyntax;
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0014330D File Offset: 0x0014150D
		public static ReturnStatementSyntax ReturnStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken returnKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return new ReturnStatementSyntax(SyntaxKind.ReturnStatement, attributeLists.Node, returnKeyword, expression, semicolonToken);
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x00143323 File Offset: 0x00141523
		public static ThrowStatementSyntax ThrowStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken throwKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			return new ThrowStatementSyntax(SyntaxKind.ThrowStatement, attributeLists.Node, throwKeyword, expression, semicolonToken);
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x00143339 File Offset: 0x00141539
		public static YieldStatementSyntax YieldStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken yieldKeyword, SyntaxToken returnOrBreakKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.YieldReturnStatement > 1)
			{
				throw new ArgumentException("kind");
			}
			return new YieldStatementSyntax(kind, attributeLists.Node, yieldKeyword, returnOrBreakKeyword, expression, semicolonToken);
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x00143363 File Offset: 0x00141563
		public static WhileStatementSyntax WhileStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new WhileStatementSyntax(SyntaxKind.WhileStatement, attributeLists.Node, whileKeyword, openParenToken, condition, closeParenToken, statement);
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x00143380 File Offset: 0x00141580
		public static DoStatementSyntax DoStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken doKeyword, StatementSyntax statement, SyntaxToken whileKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, SyntaxToken semicolonToken)
		{
			return new DoStatementSyntax(SyntaxKind.DoStatement, attributeLists.Node, doKeyword, statement, whileKeyword, openParenToken, condition, closeParenToken, semicolonToken);
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x001433AC File Offset: 0x001415AC
		public static ForStatementSyntax ForStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken forKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> initializers, SyntaxToken firstSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax condition, SyntaxToken secondSemicolonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> incrementors, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new ForStatementSyntax(SyntaxKind.ForStatement, attributeLists.Node, forKeyword, openParenToken, declaration, initializers.Node, firstSemicolonToken, condition, secondSemicolonToken, incrementors.Node, closeParenToken, statement);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x001433E8 File Offset: 0x001415E8
		public static ForEachStatementSyntax ForEachStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, TypeSyntax type, SyntaxToken identifier, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new ForEachStatementSyntax(SyntaxKind.ForEachStatement, attributeLists.Node, awaitKeyword, forEachKeyword, openParenToken, type, identifier, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x00143418 File Offset: 0x00141618
		public static ForEachVariableStatementSyntax ForEachVariableStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken forEachKeyword, SyntaxToken openParenToken, ExpressionSyntax variable, SyntaxToken inKeyword, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new ForEachVariableStatementSyntax(SyntaxKind.ForEachVariableStatement, attributeLists.Node, awaitKeyword, forEachKeyword, openParenToken, variable, inKeyword, expression, closeParenToken, statement);
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x00143444 File Offset: 0x00141644
		public static UsingStatementSyntax UsingStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken awaitKeyword, SyntaxToken usingKeyword, SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(2)] VariableDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new UsingStatementSyntax(SyntaxKind.UsingStatement, attributeLists.Node, awaitKeyword, usingKeyword, openParenToken, declaration, expression, closeParenToken, statement);
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0014346D File Offset: 0x0014166D
		public static FixedStatementSyntax FixedStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken fixedKeyword, SyntaxToken openParenToken, VariableDeclarationSyntax declaration, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new FixedStatementSyntax(SyntaxKind.FixedStatement, attributeLists.Node, fixedKeyword, openParenToken, declaration, closeParenToken, statement);
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x00143488 File Offset: 0x00141688
		public static CheckedStatementSyntax CheckedStatement(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken keyword, BlockSyntax block)
		{
			if (kind - SyntaxKind.CheckedStatement > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, attributeLists.Node, keyword, block, out num);
			if (greenNode != null)
			{
				return (CheckedStatementSyntax)greenNode;
			}
			CheckedStatementSyntax checkedStatementSyntax = new CheckedStatementSyntax(kind, attributeLists.Node, keyword, block);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(checkedStatementSyntax, num);
			}
			return checkedStatementSyntax;
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x001434E4 File Offset: 0x001416E4
		public static UnsafeStatementSyntax UnsafeStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken unsafeKeyword, BlockSyntax block)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8817, attributeLists.Node, unsafeKeyword, block, out num);
			if (greenNode != null)
			{
				return (UnsafeStatementSyntax)greenNode;
			}
			UnsafeStatementSyntax unsafeStatementSyntax = new UnsafeStatementSyntax(SyntaxKind.UnsafeStatement, attributeLists.Node, unsafeKeyword, block);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(unsafeStatementSyntax, num);
			}
			return unsafeStatementSyntax;
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x00143531 File Offset: 0x00141731
		public static LockStatementSyntax LockStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken lockKeyword, SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken, StatementSyntax statement)
		{
			return new LockStatementSyntax(SyntaxKind.LockStatement, attributeLists.Node, lockKeyword, openParenToken, expression, closeParenToken, statement);
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x0014354B File Offset: 0x0014174B
		public static IfStatementSyntax IfStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken ifKeyword, SyntaxToken openParenToken, ExpressionSyntax condition, SyntaxToken closeParenToken, StatementSyntax statement, [global::System.Runtime.CompilerServices.Nullable(2)] ElseClauseSyntax @else)
		{
			return new IfStatementSyntax(SyntaxKind.IfStatement, attributeLists.Node, ifKeyword, openParenToken, condition, closeParenToken, statement, @else);
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x00143568 File Offset: 0x00141768
		public static ElseClauseSyntax ElseClause(SyntaxToken elseKeyword, StatementSyntax statement)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8820, elseKeyword, statement, out num);
			if (greenNode != null)
			{
				return (ElseClauseSyntax)greenNode;
			}
			ElseClauseSyntax elseClauseSyntax = new ElseClauseSyntax(SyntaxKind.ElseClause, elseKeyword, statement);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(elseClauseSyntax, num);
			}
			return elseClauseSyntax;
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x001435A8 File Offset: 0x001417A8
		public static SwitchStatementSyntax SwitchStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken switchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken openParenToken, ExpressionSyntax expression, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken closeParenToken, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchSectionSyntax> sections, SyntaxToken closeBraceToken)
		{
			return new SwitchStatementSyntax(SyntaxKind.SwitchStatement, attributeLists.Node, switchKeyword, openParenToken, expression, closeParenToken, openBraceToken, sections.Node, closeBraceToken);
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x001435D8 File Offset: 0x001417D8
		public static SwitchSectionSyntax SwitchSection([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SwitchLabelSyntax> labels, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<StatementSyntax> statements)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8822, labels.Node, statements.Node, out num);
			if (greenNode != null)
			{
				return (SwitchSectionSyntax)greenNode;
			}
			SwitchSectionSyntax switchSectionSyntax = new SwitchSectionSyntax(SyntaxKind.SwitchSection, labels.Node, statements.Node);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(switchSectionSyntax, num);
			}
			return switchSectionSyntax;
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0014362F File Offset: 0x0014182F
		public static CasePatternSwitchLabelSyntax CasePatternSwitchLabel(SyntaxToken keyword, PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken colonToken)
		{
			return new CasePatternSwitchLabelSyntax(SyntaxKind.CasePatternSwitchLabel, keyword, pattern, whenClause, colonToken);
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x00143640 File Offset: 0x00141840
		public static CaseSwitchLabelSyntax CaseSwitchLabel(SyntaxToken keyword, ExpressionSyntax value, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8823, keyword, value, colonToken, out num);
			if (greenNode != null)
			{
				return (CaseSwitchLabelSyntax)greenNode;
			}
			CaseSwitchLabelSyntax caseSwitchLabelSyntax = new CaseSwitchLabelSyntax(SyntaxKind.CaseSwitchLabel, keyword, value, colonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(caseSwitchLabelSyntax, num);
			}
			return caseSwitchLabelSyntax;
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x00143684 File Offset: 0x00141884
		public static DefaultSwitchLabelSyntax DefaultSwitchLabel(SyntaxToken keyword, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8824, keyword, colonToken, out num);
			if (greenNode != null)
			{
				return (DefaultSwitchLabelSyntax)greenNode;
			}
			DefaultSwitchLabelSyntax defaultSwitchLabelSyntax = new DefaultSwitchLabelSyntax(SyntaxKind.DefaultSwitchLabel, keyword, colonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(defaultSwitchLabelSyntax, num);
			}
			return defaultSwitchLabelSyntax;
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x001436C3 File Offset: 0x001418C3
		public static SwitchExpressionSyntax SwitchExpression(ExpressionSyntax governingExpression, SyntaxToken switchKeyword, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<SwitchExpressionArmSyntax> arms, SyntaxToken closeBraceToken)
		{
			return new SwitchExpressionSyntax(SyntaxKind.SwitchExpression, governingExpression, switchKeyword, openBraceToken, arms.Node, closeBraceToken);
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x001436DB File Offset: 0x001418DB
		public static SwitchExpressionArmSyntax SwitchExpressionArm(PatternSyntax pattern, [global::System.Runtime.CompilerServices.Nullable(2)] WhenClauseSyntax whenClause, SyntaxToken equalsGreaterThanToken, ExpressionSyntax expression)
		{
			return new SwitchExpressionArmSyntax(SyntaxKind.SwitchExpressionArm, pattern, whenClause, equalsGreaterThanToken, expression);
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x001436EB File Offset: 0x001418EB
		public static TryStatementSyntax TryStatement([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, SyntaxToken tryKeyword, BlockSyntax block, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<CatchClauseSyntax> catches, [global::System.Runtime.CompilerServices.Nullable(2)] FinallyClauseSyntax @finally)
		{
			return new TryStatementSyntax(SyntaxKind.TryStatement, attributeLists.Node, tryKeyword, block, catches.Node, @finally);
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x00143709 File Offset: 0x00141909
		public static CatchClauseSyntax CatchClause(SyntaxToken catchKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CatchDeclarationSyntax declaration, [global::System.Runtime.CompilerServices.Nullable(2)] CatchFilterClauseSyntax filter, BlockSyntax block)
		{
			return new CatchClauseSyntax(SyntaxKind.CatchClause, catchKeyword, declaration, filter, block);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x00143719 File Offset: 0x00141919
		public static CatchDeclarationSyntax CatchDeclaration(SyntaxToken openParenToken, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken identifier, SyntaxToken closeParenToken)
		{
			return new CatchDeclarationSyntax(SyntaxKind.CatchDeclaration, openParenToken, type, identifier, closeParenToken);
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x00143729 File Offset: 0x00141929
		public static CatchFilterClauseSyntax CatchFilterClause(SyntaxToken whenKeyword, SyntaxToken openParenToken, ExpressionSyntax filterExpression, SyntaxToken closeParenToken)
		{
			return new CatchFilterClauseSyntax(SyntaxKind.CatchFilterClause, whenKeyword, openParenToken, filterExpression, closeParenToken);
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0014373C File Offset: 0x0014193C
		public static FinallyClauseSyntax FinallyClause(SyntaxToken finallyKeyword, BlockSyntax block)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8829, finallyKeyword, block, out num);
			if (greenNode != null)
			{
				return (FinallyClauseSyntax)greenNode;
			}
			FinallyClauseSyntax finallyClauseSyntax = new FinallyClauseSyntax(SyntaxKind.FinallyClause, finallyKeyword, block);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(finallyClauseSyntax, num);
			}
			return finallyClauseSyntax;
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0014377B File Offset: 0x0014197B
		public static CompilationUnitSyntax CompilationUnit([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken endOfFileToken)
		{
			return new CompilationUnitSyntax(SyntaxKind.CompilationUnit, externs.Node, usings.Node, attributeLists.Node, members.Node, endOfFileToken);
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x001437A5 File Offset: 0x001419A5
		public static ExternAliasDirectiveSyntax ExternAliasDirective(SyntaxToken externKeyword, SyntaxToken aliasKeyword, SyntaxToken identifier, SyntaxToken semicolonToken)
		{
			return new ExternAliasDirectiveSyntax(SyntaxKind.ExternAliasDirective, externKeyword, aliasKeyword, identifier, semicolonToken);
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x001437B5 File Offset: 0x001419B5
		public static UsingDirectiveSyntax UsingDirective([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken globalKeyword, SyntaxToken usingKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken staticKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax alias, NameSyntax name, SyntaxToken semicolonToken)
		{
			return new UsingDirectiveSyntax(SyntaxKind.UsingDirective, globalKeyword, usingKeyword, staticKeyword, alias, name, semicolonToken);
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x001437CC File Offset: 0x001419CC
		public static NamespaceDeclarationSyntax NamespaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken namespaceKeyword, NameSyntax name, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<ExternAliasDirectiveSyntax> externs, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<UsingDirectiveSyntax> usings, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new NamespaceDeclarationSyntax(SyntaxKind.NamespaceDeclaration, attributeLists.Node, modifiers.Node, namespaceKeyword, name, openBraceToken, externs.Node, usings.Node, members.Node, closeBraceToken, semicolonToken);
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x0014380E File Offset: 0x00141A0E
		public static AttributeListSyntax AttributeList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeTargetSpecifierSyntax target, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeSyntax> attributes, SyntaxToken closeBracketToken)
		{
			return new AttributeListSyntax(SyntaxKind.AttributeList, openBracketToken, target, attributes.Node, closeBracketToken);
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x00143824 File Offset: 0x00141A24
		public static AttributeTargetSpecifierSyntax AttributeTargetSpecifier(SyntaxToken identifier, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8848, identifier, colonToken, out num);
			if (greenNode != null)
			{
				return (AttributeTargetSpecifierSyntax)greenNode;
			}
			AttributeTargetSpecifierSyntax attributeTargetSpecifierSyntax = new AttributeTargetSpecifierSyntax(SyntaxKind.AttributeTargetSpecifier, identifier, colonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeTargetSpecifierSyntax, num);
			}
			return attributeTargetSpecifierSyntax;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x00143864 File Offset: 0x00141A64
		public static AttributeSyntax Attribute(NameSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] AttributeArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8849, name, argumentList, out num);
			if (greenNode != null)
			{
				return (AttributeSyntax)greenNode;
			}
			AttributeSyntax attributeSyntax = new AttributeSyntax(SyntaxKind.Attribute, name, argumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeSyntax, num);
			}
			return attributeSyntax;
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x001438A4 File Offset: 0x00141AA4
		public static AttributeArgumentListSyntax AttributeArgumentList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<AttributeArgumentSyntax> arguments, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8850, openParenToken, arguments.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (AttributeArgumentListSyntax)greenNode;
			}
			AttributeArgumentListSyntax attributeArgumentListSyntax = new AttributeArgumentListSyntax(SyntaxKind.AttributeArgumentList, openParenToken, arguments.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeArgumentListSyntax, num);
			}
			return attributeArgumentListSyntax;
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x001438F4 File Offset: 0x00141AF4
		public static AttributeArgumentSyntax AttributeArgument([global::System.Runtime.CompilerServices.Nullable(2)] NameEqualsSyntax nameEquals, [global::System.Runtime.CompilerServices.Nullable(2)] NameColonSyntax nameColon, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8851, nameEquals, nameColon, expression, out num);
			if (greenNode != null)
			{
				return (AttributeArgumentSyntax)greenNode;
			}
			AttributeArgumentSyntax attributeArgumentSyntax = new AttributeArgumentSyntax(SyntaxKind.AttributeArgument, nameEquals, nameColon, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(attributeArgumentSyntax, num);
			}
			return attributeArgumentSyntax;
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x00143938 File Offset: 0x00141B38
		public static NameEqualsSyntax NameEquals(IdentifierNameSyntax name, SyntaxToken equalsToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8852, name, equalsToken, out num);
			if (greenNode != null)
			{
				return (NameEqualsSyntax)greenNode;
			}
			NameEqualsSyntax nameEqualsSyntax = new NameEqualsSyntax(SyntaxKind.NameEquals, name, equalsToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nameEqualsSyntax, num);
			}
			return nameEqualsSyntax;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x00143978 File Offset: 0x00141B78
		public static TypeParameterListSyntax TypeParameterList(SyntaxToken lessThanToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterSyntax> parameters, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8909, lessThanToken, parameters.Node, greaterThanToken, out num);
			if (greenNode != null)
			{
				return (TypeParameterListSyntax)greenNode;
			}
			TypeParameterListSyntax typeParameterListSyntax = new TypeParameterListSyntax(SyntaxKind.TypeParameterList, lessThanToken, parameters.Node, greaterThanToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeParameterListSyntax, num);
			}
			return typeParameterListSyntax;
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x001439C8 File Offset: 0x00141BC8
		public static TypeParameterSyntax TypeParameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken varianceKeyword, SyntaxToken identifier)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8910, attributeLists.Node, varianceKeyword, identifier, out num);
			if (greenNode != null)
			{
				return (TypeParameterSyntax)greenNode;
			}
			TypeParameterSyntax typeParameterSyntax = new TypeParameterSyntax(SyntaxKind.TypeParameter, attributeLists.Node, varianceKeyword, identifier);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeParameterSyntax, num);
			}
			return typeParameterSyntax;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x00143A18 File Offset: 0x00141C18
		public static ClassDeclarationSyntax ClassDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new ClassDeclarationSyntax(SyntaxKind.ClassDeclaration, attributeLists.Node, modifiers.Node, keyword, identifier, typeParameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken);
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x00143A58 File Offset: 0x00141C58
		public static StructDeclarationSyntax StructDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new StructDeclarationSyntax(SyntaxKind.StructDeclaration, attributeLists.Node, modifiers.Node, keyword, identifier, typeParameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken);
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x00143A98 File Offset: 0x00141C98
		public static InterfaceDeclarationSyntax InterfaceDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken keyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new InterfaceDeclarationSyntax(SyntaxKind.InterfaceDeclaration, attributeLists.Node, modifiers.Node, keyword, identifier, typeParameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken);
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x00143AD8 File Offset: 0x00141CD8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static RecordDeclarationSyntax RecordDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, BaseListSyntax baseList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<MemberDeclarationSyntax> members, SyntaxToken closeBraceToken, SyntaxToken semicolonToken)
		{
			if (kind != SyntaxKind.RecordDeclaration && kind != SyntaxKind.RecordStructDeclaration)
			{
				throw new ArgumentException("kind");
			}
			return new RecordDeclarationSyntax(kind, attributeLists.Node, modifiers.Node, keyword, classOrStructKeyword, identifier, typeParameterList, parameterList, baseList, constraintClauses.Node, openBraceToken, members.Node, closeBraceToken, semicolonToken);
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x00143B34 File Offset: 0x00141D34
		public static EnumDeclarationSyntax EnumDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken enumKeyword, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] BaseListSyntax baseList, SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<EnumMemberDeclarationSyntax> members, SyntaxToken closeBraceToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new EnumDeclarationSyntax(SyntaxKind.EnumDeclaration, attributeLists.Node, modifiers.Node, enumKeyword, identifier, baseList, openBraceToken, members.Node, closeBraceToken, semicolonToken);
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x00143B6C File Offset: 0x00141D6C
		public static DelegateDeclarationSyntax DelegateDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken delegateKeyword, TypeSyntax returnType, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] TypeParameterListSyntax typeParameterList, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, SyntaxToken semicolonToken)
		{
			return new DelegateDeclarationSyntax(SyntaxKind.DelegateDeclaration, attributeLists.Node, modifiers.Node, delegateKeyword, returnType, identifier, typeParameterList, parameterList, constraintClauses.Node, semicolonToken);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x00143BA2 File Offset: 0x00141DA2
		public static EnumMemberDeclarationSyntax EnumMemberDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax equalsValue)
		{
			return new EnumMemberDeclarationSyntax(SyntaxKind.EnumMemberDeclaration, attributeLists.Node, modifiers.Node, identifier, equalsValue);
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x00143BC0 File Offset: 0x00141DC0
		public static BaseListSyntax BaseList(SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<BaseTypeSyntax> types)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8864, colonToken, types.Node, out num);
			if (greenNode != null)
			{
				return (BaseListSyntax)greenNode;
			}
			BaseListSyntax baseListSyntax = new BaseListSyntax(SyntaxKind.BaseList, colonToken, types.Node);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(baseListSyntax, num);
			}
			return baseListSyntax;
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x00143C0C File Offset: 0x00141E0C
		public static SimpleBaseTypeSyntax SimpleBaseType(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8865, type, out num);
			if (greenNode != null)
			{
				return (SimpleBaseTypeSyntax)greenNode;
			}
			SimpleBaseTypeSyntax simpleBaseTypeSyntax = new SimpleBaseTypeSyntax(SyntaxKind.SimpleBaseType, type);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(simpleBaseTypeSyntax, num);
			}
			return simpleBaseTypeSyntax;
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x00143C4C File Offset: 0x00141E4C
		public static PrimaryConstructorBaseTypeSyntax PrimaryConstructorBaseType(TypeSyntax type, ArgumentListSyntax argumentList)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9065, type, argumentList, out num);
			if (greenNode != null)
			{
				return (PrimaryConstructorBaseTypeSyntax)greenNode;
			}
			PrimaryConstructorBaseTypeSyntax primaryConstructorBaseTypeSyntax = new PrimaryConstructorBaseTypeSyntax(SyntaxKind.PrimaryConstructorBaseType, type, argumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(primaryConstructorBaseTypeSyntax, num);
			}
			return primaryConstructorBaseTypeSyntax;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x00143C8B File Offset: 0x00141E8B
		public static TypeParameterConstraintClauseSyntax TypeParameterConstraintClause(SyntaxToken whereKeyword, IdentifierNameSyntax name, SyntaxToken colonToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TypeParameterConstraintSyntax> constraints)
		{
			return new TypeParameterConstraintClauseSyntax(SyntaxKind.TypeParameterConstraintClause, whereKeyword, name, colonToken, constraints.Node);
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x00143CA4 File Offset: 0x00141EA4
		public static ConstructorConstraintSyntax ConstructorConstraint(SyntaxToken newKeyword, SyntaxToken openParenToken, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8867, newKeyword, openParenToken, closeParenToken, out num);
			if (greenNode != null)
			{
				return (ConstructorConstraintSyntax)greenNode;
			}
			ConstructorConstraintSyntax constructorConstraintSyntax = new ConstructorConstraintSyntax(SyntaxKind.ConstructorConstraint, newKeyword, openParenToken, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(constructorConstraintSyntax, num);
			}
			return constructorConstraintSyntax;
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x00143CE8 File Offset: 0x00141EE8
		public static ClassOrStructConstraintSyntax ClassOrStructConstraint(SyntaxKind kind, SyntaxToken classOrStructKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken questionToken)
		{
			if (kind - SyntaxKind.ClassConstraint > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, classOrStructKeyword, questionToken, out num);
			if (greenNode != null)
			{
				return (ClassOrStructConstraintSyntax)greenNode;
			}
			ClassOrStructConstraintSyntax classOrStructConstraintSyntax = new ClassOrStructConstraintSyntax(kind, classOrStructKeyword, questionToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(classOrStructConstraintSyntax, num);
			}
			return classOrStructConstraintSyntax;
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x00143D34 File Offset: 0x00141F34
		public static TypeConstraintSyntax TypeConstraint(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8870, type, out num);
			if (greenNode != null)
			{
				return (TypeConstraintSyntax)greenNode;
			}
			TypeConstraintSyntax typeConstraintSyntax = new TypeConstraintSyntax(SyntaxKind.TypeConstraint, type);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeConstraintSyntax, num);
			}
			return typeConstraintSyntax;
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x00143D74 File Offset: 0x00141F74
		public static DefaultConstraintSyntax DefaultConstraint(SyntaxToken defaultKeyword)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9064, defaultKeyword, out num);
			if (greenNode != null)
			{
				return (DefaultConstraintSyntax)greenNode;
			}
			DefaultConstraintSyntax defaultConstraintSyntax = new DefaultConstraintSyntax(SyntaxKind.DefaultConstraint, defaultKeyword);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(defaultConstraintSyntax, num);
			}
			return defaultConstraintSyntax;
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x00143DB1 File Offset: 0x00141FB1
		public static FieldDeclarationSyntax FieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return new FieldDeclarationSyntax(SyntaxKind.FieldDeclaration, attributeLists.Node, modifiers.Node, declaration, semicolonToken);
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x00143DCD File Offset: 0x00141FCD
		public static EventFieldDeclarationSyntax EventFieldDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken eventKeyword, VariableDeclarationSyntax declaration, SyntaxToken semicolonToken)
		{
			return new EventFieldDeclarationSyntax(SyntaxKind.EventFieldDeclaration, attributeLists.Node, modifiers.Node, eventKeyword, declaration, semicolonToken);
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x00143DEC File Offset: 0x00141FEC
		public static ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier(NameSyntax name, SyntaxToken dotToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8871, name, dotToken, out num);
			if (greenNode != null)
			{
				return (ExplicitInterfaceSpecifierSyntax)greenNode;
			}
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = new ExplicitInterfaceSpecifierSyntax(SyntaxKind.ExplicitInterfaceSpecifier, name, dotToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(explicitInterfaceSpecifierSyntax, num);
			}
			return explicitInterfaceSpecifierSyntax;
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00143E2C File Offset: 0x0014202C
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static MethodDeclarationSyntax MethodDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax returnType, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, TypeParameterListSyntax typeParameterList, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TypeParameterConstraintClauseSyntax> constraintClauses, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return new MethodDeclarationSyntax(SyntaxKind.MethodDeclaration, attributeLists.Node, modifiers.Node, returnType, explicitInterfaceSpecifier, identifier, typeParameterList, parameterList, constraintClauses.Node, body, expressionBody, semicolonToken);
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x00143E68 File Offset: 0x00142068
		public static OperatorDeclarationSyntax OperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax returnType, SyntaxToken operatorKeyword, SyntaxToken operatorToken, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new OperatorDeclarationSyntax(SyntaxKind.OperatorDeclaration, attributeLists.Node, modifiers.Node, returnType, operatorKeyword, operatorToken, parameterList, body, expressionBody, semicolonToken);
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x00143E9C File Offset: 0x0014209C
		public static ConversionOperatorDeclarationSyntax ConversionOperatorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new ConversionOperatorDeclarationSyntax(SyntaxKind.ConversionOperatorDeclaration, attributeLists.Node, modifiers.Node, implicitOrExplicitKeyword, operatorKeyword, type, parameterList, body, expressionBody, semicolonToken);
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x00143ED0 File Offset: 0x001420D0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static ConstructorDeclarationSyntax ConstructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(1)] ParameterListSyntax parameterList, ConstructorInitializerSyntax initializer, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			return new ConstructorDeclarationSyntax(SyntaxKind.ConstructorDeclaration, attributeLists.Node, modifiers.Node, identifier, parameterList, initializer, body, expressionBody, semicolonToken);
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x00143F00 File Offset: 0x00142100
		public static ConstructorInitializerSyntax ConstructorInitializer(SyntaxKind kind, SyntaxToken colonToken, SyntaxToken thisOrBaseKeyword, ArgumentListSyntax argumentList)
		{
			if (kind - SyntaxKind.BaseConstructorInitializer > 1)
			{
				throw new ArgumentException("kind");
			}
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode((int)kind, colonToken, thisOrBaseKeyword, argumentList, out num);
			if (greenNode != null)
			{
				return (ConstructorInitializerSyntax)greenNode;
			}
			ConstructorInitializerSyntax constructorInitializerSyntax = new ConstructorInitializerSyntax(kind, colonToken, thisOrBaseKeyword, argumentList);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(constructorInitializerSyntax, num);
			}
			return constructorInitializerSyntax;
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00143F50 File Offset: 0x00142150
		public static DestructorDeclarationSyntax DestructorDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken tildeToken, SyntaxToken identifier, ParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] BlockSyntax body, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new DestructorDeclarationSyntax(SyntaxKind.DestructorDeclaration, attributeLists.Node, modifiers.Node, tildeToken, identifier, parameterList, body, expressionBody, semicolonToken);
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x00143F80 File Offset: 0x00142180
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static PropertyDeclarationSyntax PropertyDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] TypeSyntax type, ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken identifier, AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody, EqualsValueClauseSyntax initializer, SyntaxToken semicolonToken)
		{
			return new PropertyDeclarationSyntax(SyntaxKind.PropertyDeclaration, attributeLists.Node, modifiers.Node, type, explicitInterfaceSpecifier, identifier, accessorList, expressionBody, initializer, semicolonToken);
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x00143FB4 File Offset: 0x001421B4
		public static ArrowExpressionClauseSyntax ArrowExpressionClause(SyntaxToken arrowToken, ExpressionSyntax expression)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8917, arrowToken, expression, out num);
			if (greenNode != null)
			{
				return (ArrowExpressionClauseSyntax)greenNode;
			}
			ArrowExpressionClauseSyntax arrowExpressionClauseSyntax = new ArrowExpressionClauseSyntax(SyntaxKind.ArrowExpressionClause, arrowToken, expression);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(arrowExpressionClauseSyntax, num);
			}
			return arrowExpressionClauseSyntax;
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x00143FF4 File Offset: 0x001421F4
		public static EventDeclarationSyntax EventDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, SyntaxToken eventKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new EventDeclarationSyntax(SyntaxKind.EventDeclaration, attributeLists.Node, modifiers.Node, eventKeyword, type, explicitInterfaceSpecifier, identifier, accessorList, semicolonToken);
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x00144024 File Offset: 0x00142224
		public static IndexerDeclarationSyntax IndexerDeclaration([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier, SyntaxToken thisKeyword, BracketedParameterListSyntax parameterList, [global::System.Runtime.CompilerServices.Nullable(2)] AccessorListSyntax accessorList, [global::System.Runtime.CompilerServices.Nullable(2)] ArrowExpressionClauseSyntax expressionBody, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken semicolonToken)
		{
			return new IndexerDeclarationSyntax(SyntaxKind.IndexerDeclaration, attributeLists.Node, modifiers.Node, type, explicitInterfaceSpecifier, thisKeyword, parameterList, accessorList, expressionBody, semicolonToken);
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x00144058 File Offset: 0x00142258
		public static AccessorListSyntax AccessorList(SyntaxToken openBraceToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AccessorDeclarationSyntax> accessors, SyntaxToken closeBraceToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8895, openBraceToken, accessors.Node, closeBraceToken, out num);
			if (greenNode != null)
			{
				return (AccessorListSyntax)greenNode;
			}
			AccessorListSyntax accessorListSyntax = new AccessorListSyntax(SyntaxKind.AccessorList, openBraceToken, accessors.Node, closeBraceToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(accessorListSyntax, num);
			}
			return accessorListSyntax;
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x001440A5 File Offset: 0x001422A5
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public static AccessorDeclarationSyntax AccessorDeclaration(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxToken keyword, BlockSyntax body, ArrowExpressionClauseSyntax expressionBody, SyntaxToken semicolonToken)
		{
			if (kind - SyntaxKind.GetAccessorDeclaration > 4 && kind != SyntaxKind.InitAccessorDeclaration)
			{
				throw new ArgumentException("kind");
			}
			return new AccessorDeclarationSyntax(kind, attributeLists.Node, modifiers.Node, keyword, body, expressionBody, semicolonToken);
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x001440E0 File Offset: 0x001422E0
		public static ParameterListSyntax ParameterList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8906, openParenToken, parameters.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (ParameterListSyntax)greenNode;
			}
			ParameterListSyntax parameterListSyntax = new ParameterListSyntax(SyntaxKind.ParameterList, openParenToken, parameters.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(parameterListSyntax, num);
			}
			return parameterListSyntax;
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x00144130 File Offset: 0x00142330
		public static BracketedParameterListSyntax BracketedParameterList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8907, openBracketToken, parameters.Node, closeBracketToken, out num);
			if (greenNode != null)
			{
				return (BracketedParameterListSyntax)greenNode;
			}
			BracketedParameterListSyntax bracketedParameterListSyntax = new BracketedParameterListSyntax(SyntaxKind.BracketedParameterList, openBracketToken, parameters.Node, closeBracketToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(bracketedParameterListSyntax, num);
			}
			return bracketedParameterListSyntax;
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x0014417D File Offset: 0x0014237D
		public static ParameterSyntax Parameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type, SyntaxToken identifier, [global::System.Runtime.CompilerServices.Nullable(2)] EqualsValueClauseSyntax @default)
		{
			return new ParameterSyntax(SyntaxKind.Parameter, attributeLists.Node, modifiers.Node, type, identifier, @default);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x0014419C File Offset: 0x0014239C
		public static FunctionPointerParameterSyntax FunctionPointerParameter([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, TypeSyntax type)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(9057, attributeLists.Node, modifiers.Node, type, out num);
			if (greenNode != null)
			{
				return (FunctionPointerParameterSyntax)greenNode;
			}
			FunctionPointerParameterSyntax functionPointerParameterSyntax = new FunctionPointerParameterSyntax(SyntaxKind.FunctionPointerParameter, attributeLists.Node, modifiers.Node, type);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(functionPointerParameterSyntax, num);
			}
			return functionPointerParameterSyntax;
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x001441F5 File Offset: 0x001423F5
		public static IncompleteMemberSyntax IncompleteMember([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<AttributeListSyntax> attributeLists, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> modifiers, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSyntax type)
		{
			return new IncompleteMemberSyntax(SyntaxKind.IncompleteMember, attributeLists.Node, modifiers.Node, type);
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x00144210 File Offset: 0x00142410
		public static SkippedTokensTriviaSyntax SkippedTokensTrivia([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> tokens)
		{
			return new SkippedTokensTriviaSyntax(SyntaxKind.SkippedTokensTrivia, tokens.Node);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x00144223 File Offset: 0x00142423
		public static DocumentationCommentTriviaSyntax DocumentationCommentTrivia(SyntaxKind kind, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, SyntaxToken endOfComment)
		{
			if (kind - SyntaxKind.SingleLineDocumentationCommentTrivia > 1)
			{
				throw new ArgumentException("kind");
			}
			return new DocumentationCommentTriviaSyntax(kind, content.Node, endOfComment);
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00144248 File Offset: 0x00142448
		public static TypeCrefSyntax TypeCref(TypeSyntax type)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8597, type, out num);
			if (greenNode != null)
			{
				return (TypeCrefSyntax)greenNode;
			}
			TypeCrefSyntax typeCrefSyntax = new TypeCrefSyntax(SyntaxKind.TypeCref, type);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(typeCrefSyntax, num);
			}
			return typeCrefSyntax;
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00144288 File Offset: 0x00142488
		public static QualifiedCrefSyntax QualifiedCref(TypeSyntax container, SyntaxToken dotToken, MemberCrefSyntax member)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8598, container, dotToken, member, out num);
			if (greenNode != null)
			{
				return (QualifiedCrefSyntax)greenNode;
			}
			QualifiedCrefSyntax qualifiedCrefSyntax = new QualifiedCrefSyntax(SyntaxKind.QualifiedCref, container, dotToken, member);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(qualifiedCrefSyntax, num);
			}
			return qualifiedCrefSyntax;
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x001442CC File Offset: 0x001424CC
		public static NameMemberCrefSyntax NameMemberCref(TypeSyntax name, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8599, name, parameters, out num);
			if (greenNode != null)
			{
				return (NameMemberCrefSyntax)greenNode;
			}
			NameMemberCrefSyntax nameMemberCrefSyntax = new NameMemberCrefSyntax(SyntaxKind.NameMemberCref, name, parameters);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(nameMemberCrefSyntax, num);
			}
			return nameMemberCrefSyntax;
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x0014430C File Offset: 0x0014250C
		public static IndexerMemberCrefSyntax IndexerMemberCref(SyntaxToken thisKeyword, [global::System.Runtime.CompilerServices.Nullable(2)] CrefBracketedParameterListSyntax parameters)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8600, thisKeyword, parameters, out num);
			if (greenNode != null)
			{
				return (IndexerMemberCrefSyntax)greenNode;
			}
			IndexerMemberCrefSyntax indexerMemberCrefSyntax = new IndexerMemberCrefSyntax(SyntaxKind.IndexerMemberCref, thisKeyword, parameters);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(indexerMemberCrefSyntax, num);
			}
			return indexerMemberCrefSyntax;
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0014434C File Offset: 0x0014254C
		public static OperatorMemberCrefSyntax OperatorMemberCref(SyntaxToken operatorKeyword, SyntaxToken operatorToken, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8601, operatorKeyword, operatorToken, parameters, out num);
			if (greenNode != null)
			{
				return (OperatorMemberCrefSyntax)greenNode;
			}
			OperatorMemberCrefSyntax operatorMemberCrefSyntax = new OperatorMemberCrefSyntax(SyntaxKind.OperatorMemberCref, operatorKeyword, operatorToken, parameters);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(operatorMemberCrefSyntax, num);
			}
			return operatorMemberCrefSyntax;
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0014438D File Offset: 0x0014258D
		public static ConversionOperatorMemberCrefSyntax ConversionOperatorMemberCref(SyntaxToken implicitOrExplicitKeyword, SyntaxToken operatorKeyword, TypeSyntax type, [global::System.Runtime.CompilerServices.Nullable(2)] CrefParameterListSyntax parameters)
		{
			return new ConversionOperatorMemberCrefSyntax(SyntaxKind.ConversionOperatorMemberCref, implicitOrExplicitKeyword, operatorKeyword, type, parameters);
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x001443A0 File Offset: 0x001425A0
		public static CrefParameterListSyntax CrefParameterList(SyntaxToken openParenToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeParenToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8603, openParenToken, parameters.Node, closeParenToken, out num);
			if (greenNode != null)
			{
				return (CrefParameterListSyntax)greenNode;
			}
			CrefParameterListSyntax crefParameterListSyntax = new CrefParameterListSyntax(SyntaxKind.CrefParameterList, openParenToken, parameters.Node, closeParenToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(crefParameterListSyntax, num);
			}
			return crefParameterListSyntax;
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x001443F0 File Offset: 0x001425F0
		public static CrefBracketedParameterListSyntax CrefBracketedParameterList(SyntaxToken openBracketToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<CrefParameterSyntax> parameters, SyntaxToken closeBracketToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8604, openBracketToken, parameters.Node, closeBracketToken, out num);
			if (greenNode != null)
			{
				return (CrefBracketedParameterListSyntax)greenNode;
			}
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax = new CrefBracketedParameterListSyntax(SyntaxKind.CrefBracketedParameterList, openBracketToken, parameters.Node, closeBracketToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(crefBracketedParameterListSyntax, num);
			}
			return crefBracketedParameterListSyntax;
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x00144440 File Offset: 0x00142640
		public static CrefParameterSyntax CrefParameter([global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken refKindKeyword, TypeSyntax type)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8605, refKindKeyword, type, out num);
			if (greenNode != null)
			{
				return (CrefParameterSyntax)greenNode;
			}
			CrefParameterSyntax crefParameterSyntax = new CrefParameterSyntax(SyntaxKind.CrefParameter, refKindKeyword, type);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(crefParameterSyntax, num);
			}
			return crefParameterSyntax;
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x00144480 File Offset: 0x00142680
		public static XmlElementSyntax XmlElement(XmlElementStartTagSyntax startTag, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlNodeSyntax> content, XmlElementEndTagSyntax endTag)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8574, startTag, content.Node, endTag, out num);
			if (greenNode != null)
			{
				return (XmlElementSyntax)greenNode;
			}
			XmlElementSyntax xmlElementSyntax = new XmlElementSyntax(SyntaxKind.XmlElement, startTag, content.Node, endTag);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlElementSyntax, num);
			}
			return xmlElementSyntax;
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x001444CD File Offset: 0x001426CD
		public static XmlElementStartTagSyntax XmlElementStartTag(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken greaterThanToken)
		{
			return new XmlElementStartTagSyntax(SyntaxKind.XmlElementStartTag, lessThanToken, name, attributes.Node, greaterThanToken);
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x001444E4 File Offset: 0x001426E4
		public static XmlElementEndTagSyntax XmlElementEndTag(SyntaxToken lessThanSlashToken, XmlNameSyntax name, SyntaxToken greaterThanToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8576, lessThanSlashToken, name, greaterThanToken, out num);
			if (greenNode != null)
			{
				return (XmlElementEndTagSyntax)greenNode;
			}
			XmlElementEndTagSyntax xmlElementEndTagSyntax = new XmlElementEndTagSyntax(SyntaxKind.XmlElementEndTag, lessThanSlashToken, name, greaterThanToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlElementEndTagSyntax, num);
			}
			return xmlElementEndTagSyntax;
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x00144525 File Offset: 0x00142725
		public static XmlEmptyElementSyntax XmlEmptyElement(SyntaxToken lessThanToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<XmlAttributeSyntax> attributes, SyntaxToken slashGreaterThanToken)
		{
			return new XmlEmptyElementSyntax(SyntaxKind.XmlEmptyElement, lessThanToken, name, attributes.Node, slashGreaterThanToken);
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x0014453C File Offset: 0x0014273C
		public static XmlNameSyntax XmlName([global::System.Runtime.CompilerServices.Nullable(2)] XmlPrefixSyntax prefix, SyntaxToken localName)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8581, prefix, localName, out num);
			if (greenNode != null)
			{
				return (XmlNameSyntax)greenNode;
			}
			XmlNameSyntax xmlNameSyntax = new XmlNameSyntax(SyntaxKind.XmlName, prefix, localName);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlNameSyntax, num);
			}
			return xmlNameSyntax;
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x0014457C File Offset: 0x0014277C
		public static XmlPrefixSyntax XmlPrefix(SyntaxToken prefix, SyntaxToken colonToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8582, prefix, colonToken, out num);
			if (greenNode != null)
			{
				return (XmlPrefixSyntax)greenNode;
			}
			XmlPrefixSyntax xmlPrefixSyntax = new XmlPrefixSyntax(SyntaxKind.XmlPrefix, prefix, colonToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlPrefixSyntax, num);
			}
			return xmlPrefixSyntax;
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x001445BB File Offset: 0x001427BB
		public static XmlTextAttributeSyntax XmlTextAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endQuoteToken)
		{
			return new XmlTextAttributeSyntax(SyntaxKind.XmlTextAttribute, name, equalsToken, startQuoteToken, textTokens.Node, endQuoteToken);
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x001445D3 File Offset: 0x001427D3
		public static XmlCrefAttributeSyntax XmlCrefAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, CrefSyntax cref, SyntaxToken endQuoteToken)
		{
			return new XmlCrefAttributeSyntax(SyntaxKind.XmlCrefAttribute, name, equalsToken, startQuoteToken, cref, endQuoteToken);
		}

		// Token: 0x06005033 RID: 20531 RVA: 0x001445E5 File Offset: 0x001427E5
		public static XmlNameAttributeSyntax XmlNameAttribute(XmlNameSyntax name, SyntaxToken equalsToken, SyntaxToken startQuoteToken, IdentifierNameSyntax identifier, SyntaxToken endQuoteToken)
		{
			return new XmlNameAttributeSyntax(SyntaxKind.XmlNameAttribute, name, equalsToken, startQuoteToken, identifier, endQuoteToken);
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x001445F8 File Offset: 0x001427F8
		public static XmlTextSyntax XmlText([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8583, textTokens.Node, out num);
			if (greenNode != null)
			{
				return (XmlTextSyntax)greenNode;
			}
			XmlTextSyntax xmlTextSyntax = new XmlTextSyntax(SyntaxKind.XmlText, textTokens.Node);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlTextSyntax, num);
			}
			return xmlTextSyntax;
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x00144644 File Offset: 0x00142844
		public static XmlCDataSectionSyntax XmlCDataSection(SyntaxToken startCDataToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endCDataToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8584, startCDataToken, textTokens.Node, endCDataToken, out num);
			if (greenNode != null)
			{
				return (XmlCDataSectionSyntax)greenNode;
			}
			XmlCDataSectionSyntax xmlCDataSectionSyntax = new XmlCDataSectionSyntax(SyntaxKind.XmlCDataSection, startCDataToken, textTokens.Node, endCDataToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlCDataSectionSyntax, num);
			}
			return xmlCDataSectionSyntax;
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x00144691 File Offset: 0x00142891
		public static XmlProcessingInstructionSyntax XmlProcessingInstruction(SyntaxToken startProcessingInstructionToken, XmlNameSyntax name, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken endProcessingInstructionToken)
		{
			return new XmlProcessingInstructionSyntax(SyntaxKind.XmlProcessingInstruction, startProcessingInstructionToken, name, textTokens.Node, endProcessingInstructionToken);
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x001446A8 File Offset: 0x001428A8
		public static XmlCommentSyntax XmlComment(SyntaxToken lessThanExclamationMinusMinusToken, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<SyntaxToken> textTokens, SyntaxToken minusMinusGreaterThanToken)
		{
			int num;
			GreenNode greenNode = SyntaxNodeCache.TryGetNode(8585, lessThanExclamationMinusMinusToken, textTokens.Node, minusMinusGreaterThanToken, out num);
			if (greenNode != null)
			{
				return (XmlCommentSyntax)greenNode;
			}
			XmlCommentSyntax xmlCommentSyntax = new XmlCommentSyntax(SyntaxKind.XmlComment, lessThanExclamationMinusMinusToken, textTokens.Node, minusMinusGreaterThanToken);
			if (num >= 0)
			{
				SyntaxNodeCache.AddNode(xmlCommentSyntax, num);
			}
			return xmlCommentSyntax;
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x001446F5 File Offset: 0x001428F5
		public static IfDirectiveTriviaSyntax IfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken ifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			return new IfDirectiveTriviaSyntax(SyntaxKind.IfDirectiveTrivia, hashToken, ifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x0014470B File Offset: 0x0014290B
		public static ElifDirectiveTriviaSyntax ElifDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elifKeyword, ExpressionSyntax condition, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken, bool conditionValue)
		{
			return new ElifDirectiveTriviaSyntax(SyntaxKind.ElifDirectiveTrivia, hashToken, elifKeyword, condition, endOfDirectiveToken, isActive, branchTaken, conditionValue);
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x00144721 File Offset: 0x00142921
		public static ElseDirectiveTriviaSyntax ElseDirectiveTrivia(SyntaxToken hashToken, SyntaxToken elseKeyword, SyntaxToken endOfDirectiveToken, bool isActive, bool branchTaken)
		{
			return new ElseDirectiveTriviaSyntax(SyntaxKind.ElseDirectiveTrivia, hashToken, elseKeyword, endOfDirectiveToken, isActive, branchTaken);
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x00144733 File Offset: 0x00142933
		public static EndIfDirectiveTriviaSyntax EndIfDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endIfKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new EndIfDirectiveTriviaSyntax(SyntaxKind.EndIfDirectiveTrivia, hashToken, endIfKeyword, endOfDirectiveToken, isActive);
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x00144743 File Offset: 0x00142943
		public static RegionDirectiveTriviaSyntax RegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken regionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new RegionDirectiveTriviaSyntax(SyntaxKind.RegionDirectiveTrivia, hashToken, regionKeyword, endOfDirectiveToken, isActive);
		}

		// Token: 0x0600503D RID: 20541 RVA: 0x00144753 File Offset: 0x00142953
		public static EndRegionDirectiveTriviaSyntax EndRegionDirectiveTrivia(SyntaxToken hashToken, SyntaxToken endRegionKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new EndRegionDirectiveTriviaSyntax(SyntaxKind.EndRegionDirectiveTrivia, hashToken, endRegionKeyword, endOfDirectiveToken, isActive);
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x00144763 File Offset: 0x00142963
		public static ErrorDirectiveTriviaSyntax ErrorDirectiveTrivia(SyntaxToken hashToken, SyntaxToken errorKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new ErrorDirectiveTriviaSyntax(SyntaxKind.ErrorDirectiveTrivia, hashToken, errorKeyword, endOfDirectiveToken, isActive);
		}

		// Token: 0x0600503F RID: 20543 RVA: 0x00144773 File Offset: 0x00142973
		public static WarningDirectiveTriviaSyntax WarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken warningKeyword, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new WarningDirectiveTriviaSyntax(SyntaxKind.WarningDirectiveTrivia, hashToken, warningKeyword, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005040 RID: 20544 RVA: 0x00144783 File Offset: 0x00142983
		public static BadDirectiveTriviaSyntax BadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken identifier, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new BadDirectiveTriviaSyntax(SyntaxKind.BadDirectiveTrivia, hashToken, identifier, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00144793 File Offset: 0x00142993
		public static DefineDirectiveTriviaSyntax DefineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken defineKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new DefineDirectiveTriviaSyntax(SyntaxKind.DefineDirectiveTrivia, hashToken, defineKeyword, name, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005042 RID: 20546 RVA: 0x001447A5 File Offset: 0x001429A5
		public static UndefDirectiveTriviaSyntax UndefDirectiveTrivia(SyntaxToken hashToken, SyntaxToken undefKeyword, SyntaxToken name, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new UndefDirectiveTriviaSyntax(SyntaxKind.UndefDirectiveTrivia, hashToken, undefKeyword, name, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005043 RID: 20547 RVA: 0x001447B7 File Offset: 0x001429B7
		public static LineDirectiveTriviaSyntax LineDirectiveTrivia(SyntaxToken hashToken, SyntaxToken lineKeyword, SyntaxToken line, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new LineDirectiveTriviaSyntax(SyntaxKind.LineDirectiveTrivia, hashToken, lineKeyword, line, file, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005044 RID: 20548 RVA: 0x001447CB File Offset: 0x001429CB
		public static PragmaWarningDirectiveTriviaSyntax PragmaWarningDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken warningKeyword, SyntaxToken disableOrRestoreKeyword, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<ExpressionSyntax> errorCodes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new PragmaWarningDirectiveTriviaSyntax(SyntaxKind.PragmaWarningDirectiveTrivia, hashToken, pragmaKeyword, warningKeyword, disableOrRestoreKeyword, errorCodes.Node, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x001447E8 File Offset: 0x001429E8
		public static PragmaChecksumDirectiveTriviaSyntax PragmaChecksumDirectiveTrivia(SyntaxToken hashToken, SyntaxToken pragmaKeyword, SyntaxToken checksumKeyword, SyntaxToken file, SyntaxToken guid, SyntaxToken bytes, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new PragmaChecksumDirectiveTriviaSyntax(SyntaxKind.PragmaChecksumDirectiveTrivia, hashToken, pragmaKeyword, checksumKeyword, file, guid, bytes, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x0014480B File Offset: 0x00142A0B
		public static ReferenceDirectiveTriviaSyntax ReferenceDirectiveTrivia(SyntaxToken hashToken, SyntaxToken referenceKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new ReferenceDirectiveTriviaSyntax(SyntaxKind.ReferenceDirectiveTrivia, hashToken, referenceKeyword, file, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x0014481D File Offset: 0x00142A1D
		public static LoadDirectiveTriviaSyntax LoadDirectiveTrivia(SyntaxToken hashToken, SyntaxToken loadKeyword, SyntaxToken file, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new LoadDirectiveTriviaSyntax(SyntaxKind.LoadDirectiveTrivia, hashToken, loadKeyword, file, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005048 RID: 20552 RVA: 0x0014482F File Offset: 0x00142A2F
		public static ShebangDirectiveTriviaSyntax ShebangDirectiveTrivia(SyntaxToken hashToken, SyntaxToken exclamationToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new ShebangDirectiveTriviaSyntax(SyntaxKind.ShebangDirectiveTrivia, hashToken, exclamationToken, endOfDirectiveToken, isActive);
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0014483F File Offset: 0x00142A3F
		public static NullableDirectiveTriviaSyntax NullableDirectiveTrivia(SyntaxToken hashToken, SyntaxToken nullableKeyword, SyntaxToken settingToken, [global::System.Runtime.CompilerServices.Nullable(2)] SyntaxToken targetToken, SyntaxToken endOfDirectiveToken, bool isActive)
		{
			return new NullableDirectiveTriviaSyntax(SyntaxKind.NullableDirectiveTrivia, hashToken, nullableKeyword, settingToken, targetToken, endOfDirectiveToken, isActive);
		}

		// Token: 0x0600504A RID: 20554 RVA: 0x00144854 File Offset: 0x00142A54
		internal static IEnumerable<Type> GetNodeTypes()
		{
			return new Type[]
			{
				typeof(IdentifierNameSyntax),
				typeof(QualifiedNameSyntax),
				typeof(GenericNameSyntax),
				typeof(TypeArgumentListSyntax),
				typeof(AliasQualifiedNameSyntax),
				typeof(PredefinedTypeSyntax),
				typeof(ArrayTypeSyntax),
				typeof(ArrayRankSpecifierSyntax),
				typeof(PointerTypeSyntax),
				typeof(FunctionPointerTypeSyntax),
				typeof(FunctionPointerParameterListSyntax),
				typeof(FunctionPointerCallingConventionSyntax),
				typeof(FunctionPointerUnmanagedCallingConventionListSyntax),
				typeof(FunctionPointerUnmanagedCallingConventionSyntax),
				typeof(NullableTypeSyntax),
				typeof(TupleTypeSyntax),
				typeof(TupleElementSyntax),
				typeof(OmittedTypeArgumentSyntax),
				typeof(RefTypeSyntax),
				typeof(ParenthesizedExpressionSyntax),
				typeof(TupleExpressionSyntax),
				typeof(PrefixUnaryExpressionSyntax),
				typeof(AwaitExpressionSyntax),
				typeof(PostfixUnaryExpressionSyntax),
				typeof(MemberAccessExpressionSyntax),
				typeof(ConditionalAccessExpressionSyntax),
				typeof(MemberBindingExpressionSyntax),
				typeof(ElementBindingExpressionSyntax),
				typeof(RangeExpressionSyntax),
				typeof(ImplicitElementAccessSyntax),
				typeof(BinaryExpressionSyntax),
				typeof(AssignmentExpressionSyntax),
				typeof(ConditionalExpressionSyntax),
				typeof(ThisExpressionSyntax),
				typeof(BaseExpressionSyntax),
				typeof(LiteralExpressionSyntax),
				typeof(MakeRefExpressionSyntax),
				typeof(RefTypeExpressionSyntax),
				typeof(RefValueExpressionSyntax),
				typeof(CheckedExpressionSyntax),
				typeof(DefaultExpressionSyntax),
				typeof(TypeOfExpressionSyntax),
				typeof(SizeOfExpressionSyntax),
				typeof(InvocationExpressionSyntax),
				typeof(ElementAccessExpressionSyntax),
				typeof(ArgumentListSyntax),
				typeof(BracketedArgumentListSyntax),
				typeof(ArgumentSyntax),
				typeof(NameColonSyntax),
				typeof(DeclarationExpressionSyntax),
				typeof(CastExpressionSyntax),
				typeof(AnonymousMethodExpressionSyntax),
				typeof(SimpleLambdaExpressionSyntax),
				typeof(RefExpressionSyntax),
				typeof(ParenthesizedLambdaExpressionSyntax),
				typeof(InitializerExpressionSyntax),
				typeof(ImplicitObjectCreationExpressionSyntax),
				typeof(ObjectCreationExpressionSyntax),
				typeof(WithExpressionSyntax),
				typeof(AnonymousObjectMemberDeclaratorSyntax),
				typeof(AnonymousObjectCreationExpressionSyntax),
				typeof(ArrayCreationExpressionSyntax),
				typeof(ImplicitArrayCreationExpressionSyntax),
				typeof(StackAllocArrayCreationExpressionSyntax),
				typeof(ImplicitStackAllocArrayCreationExpressionSyntax),
				typeof(QueryExpressionSyntax),
				typeof(QueryBodySyntax),
				typeof(FromClauseSyntax),
				typeof(LetClauseSyntax),
				typeof(JoinClauseSyntax),
				typeof(JoinIntoClauseSyntax),
				typeof(WhereClauseSyntax),
				typeof(OrderByClauseSyntax),
				typeof(OrderingSyntax),
				typeof(SelectClauseSyntax),
				typeof(GroupClauseSyntax),
				typeof(QueryContinuationSyntax),
				typeof(OmittedArraySizeExpressionSyntax),
				typeof(InterpolatedStringExpressionSyntax),
				typeof(IsPatternExpressionSyntax),
				typeof(ThrowExpressionSyntax),
				typeof(WhenClauseSyntax),
				typeof(DiscardPatternSyntax),
				typeof(DeclarationPatternSyntax),
				typeof(VarPatternSyntax),
				typeof(RecursivePatternSyntax),
				typeof(PositionalPatternClauseSyntax),
				typeof(PropertyPatternClauseSyntax),
				typeof(SubpatternSyntax),
				typeof(ConstantPatternSyntax),
				typeof(ParenthesizedPatternSyntax),
				typeof(RelationalPatternSyntax),
				typeof(TypePatternSyntax),
				typeof(BinaryPatternSyntax),
				typeof(UnaryPatternSyntax),
				typeof(InterpolatedStringTextSyntax),
				typeof(InterpolationSyntax),
				typeof(InterpolationAlignmentClauseSyntax),
				typeof(InterpolationFormatClauseSyntax),
				typeof(GlobalStatementSyntax),
				typeof(BlockSyntax),
				typeof(LocalFunctionStatementSyntax),
				typeof(LocalDeclarationStatementSyntax),
				typeof(VariableDeclarationSyntax),
				typeof(VariableDeclaratorSyntax),
				typeof(EqualsValueClauseSyntax),
				typeof(SingleVariableDesignationSyntax),
				typeof(DiscardDesignationSyntax),
				typeof(ParenthesizedVariableDesignationSyntax),
				typeof(ExpressionStatementSyntax),
				typeof(EmptyStatementSyntax),
				typeof(LabeledStatementSyntax),
				typeof(GotoStatementSyntax),
				typeof(BreakStatementSyntax),
				typeof(ContinueStatementSyntax),
				typeof(ReturnStatementSyntax),
				typeof(ThrowStatementSyntax),
				typeof(YieldStatementSyntax),
				typeof(WhileStatementSyntax),
				typeof(DoStatementSyntax),
				typeof(ForStatementSyntax),
				typeof(ForEachStatementSyntax),
				typeof(ForEachVariableStatementSyntax),
				typeof(UsingStatementSyntax),
				typeof(FixedStatementSyntax),
				typeof(CheckedStatementSyntax),
				typeof(UnsafeStatementSyntax),
				typeof(LockStatementSyntax),
				typeof(IfStatementSyntax),
				typeof(ElseClauseSyntax),
				typeof(SwitchStatementSyntax),
				typeof(SwitchSectionSyntax),
				typeof(CasePatternSwitchLabelSyntax),
				typeof(CaseSwitchLabelSyntax),
				typeof(DefaultSwitchLabelSyntax),
				typeof(SwitchExpressionSyntax),
				typeof(SwitchExpressionArmSyntax),
				typeof(TryStatementSyntax),
				typeof(CatchClauseSyntax),
				typeof(CatchDeclarationSyntax),
				typeof(CatchFilterClauseSyntax),
				typeof(FinallyClauseSyntax),
				typeof(CompilationUnitSyntax),
				typeof(ExternAliasDirectiveSyntax),
				typeof(UsingDirectiveSyntax),
				typeof(NamespaceDeclarationSyntax),
				typeof(AttributeListSyntax),
				typeof(AttributeTargetSpecifierSyntax),
				typeof(AttributeSyntax),
				typeof(AttributeArgumentListSyntax),
				typeof(AttributeArgumentSyntax),
				typeof(NameEqualsSyntax),
				typeof(TypeParameterListSyntax),
				typeof(TypeParameterSyntax),
				typeof(ClassDeclarationSyntax),
				typeof(StructDeclarationSyntax),
				typeof(InterfaceDeclarationSyntax),
				typeof(RecordDeclarationSyntax),
				typeof(EnumDeclarationSyntax),
				typeof(DelegateDeclarationSyntax),
				typeof(EnumMemberDeclarationSyntax),
				typeof(BaseListSyntax),
				typeof(SimpleBaseTypeSyntax),
				typeof(PrimaryConstructorBaseTypeSyntax),
				typeof(TypeParameterConstraintClauseSyntax),
				typeof(ConstructorConstraintSyntax),
				typeof(ClassOrStructConstraintSyntax),
				typeof(TypeConstraintSyntax),
				typeof(DefaultConstraintSyntax),
				typeof(FieldDeclarationSyntax),
				typeof(EventFieldDeclarationSyntax),
				typeof(ExplicitInterfaceSpecifierSyntax),
				typeof(MethodDeclarationSyntax),
				typeof(OperatorDeclarationSyntax),
				typeof(ConversionOperatorDeclarationSyntax),
				typeof(ConstructorDeclarationSyntax),
				typeof(ConstructorInitializerSyntax),
				typeof(DestructorDeclarationSyntax),
				typeof(PropertyDeclarationSyntax),
				typeof(ArrowExpressionClauseSyntax),
				typeof(EventDeclarationSyntax),
				typeof(IndexerDeclarationSyntax),
				typeof(AccessorListSyntax),
				typeof(AccessorDeclarationSyntax),
				typeof(ParameterListSyntax),
				typeof(BracketedParameterListSyntax),
				typeof(ParameterSyntax),
				typeof(FunctionPointerParameterSyntax),
				typeof(IncompleteMemberSyntax),
				typeof(SkippedTokensTriviaSyntax),
				typeof(DocumentationCommentTriviaSyntax),
				typeof(TypeCrefSyntax),
				typeof(QualifiedCrefSyntax),
				typeof(NameMemberCrefSyntax),
				typeof(IndexerMemberCrefSyntax),
				typeof(OperatorMemberCrefSyntax),
				typeof(ConversionOperatorMemberCrefSyntax),
				typeof(CrefParameterListSyntax),
				typeof(CrefBracketedParameterListSyntax),
				typeof(CrefParameterSyntax),
				typeof(XmlElementSyntax),
				typeof(XmlElementStartTagSyntax),
				typeof(XmlElementEndTagSyntax),
				typeof(XmlEmptyElementSyntax),
				typeof(XmlNameSyntax),
				typeof(XmlPrefixSyntax),
				typeof(XmlTextAttributeSyntax),
				typeof(XmlCrefAttributeSyntax),
				typeof(XmlNameAttributeSyntax),
				typeof(XmlTextSyntax),
				typeof(XmlCDataSectionSyntax),
				typeof(XmlProcessingInstructionSyntax),
				typeof(XmlCommentSyntax),
				typeof(IfDirectiveTriviaSyntax),
				typeof(ElifDirectiveTriviaSyntax),
				typeof(ElseDirectiveTriviaSyntax),
				typeof(EndIfDirectiveTriviaSyntax),
				typeof(RegionDirectiveTriviaSyntax),
				typeof(EndRegionDirectiveTriviaSyntax),
				typeof(ErrorDirectiveTriviaSyntax),
				typeof(WarningDirectiveTriviaSyntax),
				typeof(BadDirectiveTriviaSyntax),
				typeof(DefineDirectiveTriviaSyntax),
				typeof(UndefDirectiveTriviaSyntax),
				typeof(LineDirectiveTriviaSyntax),
				typeof(PragmaWarningDirectiveTriviaSyntax),
				typeof(PragmaChecksumDirectiveTriviaSyntax),
				typeof(ReferenceDirectiveTriviaSyntax),
				typeof(LoadDirectiveTriviaSyntax),
				typeof(ShebangDirectiveTriviaSyntax),
				typeof(NullableDirectiveTriviaSyntax)
			};
		}

		// Token: 0x040015BF RID: 5567
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private const string CrLf = "\r\n";

		// Token: 0x040015C0 RID: 5568
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia CarriageReturnLineFeed = SyntaxFactory.EndOfLine("\r\n", false);

		// Token: 0x040015C1 RID: 5569
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia LineFeed = SyntaxFactory.EndOfLine("\n", false);

		// Token: 0x040015C2 RID: 5570
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia CarriageReturn = SyntaxFactory.EndOfLine("\r", false);

		// Token: 0x040015C3 RID: 5571
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia Space = SyntaxFactory.Whitespace(" ", false);

		// Token: 0x040015C4 RID: 5572
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia Tab = SyntaxFactory.Whitespace("\t", false);

		// Token: 0x040015C5 RID: 5573
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia ElasticCarriageReturnLineFeed = SyntaxFactory.EndOfLine("\r\n", true);

		// Token: 0x040015C6 RID: 5574
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia ElasticLineFeed = SyntaxFactory.EndOfLine("\n", true);

		// Token: 0x040015C7 RID: 5575
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia ElasticCarriageReturn = SyntaxFactory.EndOfLine("\r", true);

		// Token: 0x040015C8 RID: 5576
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia ElasticSpace = SyntaxFactory.Whitespace(" ", true);

		// Token: 0x040015C9 RID: 5577
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia ElasticTab = SyntaxFactory.Whitespace("\t", true);

		// Token: 0x040015CA RID: 5578
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal static readonly SyntaxTrivia ElasticZeroSpace = SyntaxFactory.Whitespace(string.Empty, true);

		// Token: 0x040015CB RID: 5579
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private static SyntaxToken s_xmlCarriageReturnLineFeed;
	}
}
