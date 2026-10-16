using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Syntax;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200022B RID: 555
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
	public abstract class CSharpSyntaxRewriter : CSharpSyntaxVisitor<SyntaxNode>
	{
		// Token: 0x06002BB5 RID: 11189 RVA: 0x000E9D74 File Offset: 0x000E7F74
		public CSharpSyntaxRewriter(bool visitIntoStructuredTrivia = false)
		{
			this._visitIntoStructuredTrivia = visitIntoStructuredTrivia;
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002BB6 RID: 11190 RVA: 0x000E9D83 File Offset: 0x000E7F83
		public virtual bool VisitIntoStructuredTrivia
		{
			get
			{
				return this._visitIntoStructuredTrivia;
			}
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000E9D8B File Offset: 0x000E7F8B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		[return: NotNullIfNotNull("node")]
		public override SyntaxNode Visit(SyntaxNode node)
		{
			if (node != null)
			{
				this._recursionDepth++;
				StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);
				SyntaxNode syntaxNode = ((CSharpSyntaxNode)node).Accept<SyntaxNode>(this);
				this._recursionDepth--;
				return syntaxNode;
			}
			return null;
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000E9DC8 File Offset: 0x000E7FC8
		public virtual SyntaxToken VisitToken(SyntaxToken token)
		{
			GreenNode node = token.Node;
			if (node == null)
			{
				return token;
			}
			GreenNode leadingTriviaCore = node.GetLeadingTriviaCore();
			GreenNode trailingTriviaCore = node.GetTrailingTriviaCore();
			if (leadingTriviaCore != null)
			{
				SyntaxTriviaList syntaxTriviaList = this.VisitList(new SyntaxTriviaList(in token, leadingTriviaCore));
				if (trailingTriviaCore != null)
				{
					int num = (leadingTriviaCore.IsList ? leadingTriviaCore.SlotCount : 1);
					SyntaxTriviaList syntaxTriviaList2 = this.VisitList(new SyntaxTriviaList(in token, trailingTriviaCore, token.Position + node.FullWidth - trailingTriviaCore.FullWidth, num));
					if (syntaxTriviaList.Node != leadingTriviaCore)
					{
						token = token.WithLeadingTrivia(syntaxTriviaList);
					}
					if (syntaxTriviaList2.Node == trailingTriviaCore)
					{
						return token;
					}
					return token.WithTrailingTrivia(syntaxTriviaList2);
				}
				else
				{
					if (syntaxTriviaList.Node == leadingTriviaCore)
					{
						return token;
					}
					return token.WithLeadingTrivia(syntaxTriviaList);
				}
			}
			else
			{
				if (trailingTriviaCore == null)
				{
					return token;
				}
				SyntaxTriviaList syntaxTriviaList3 = this.VisitList(new SyntaxTriviaList(in token, trailingTriviaCore, token.Position + node.FullWidth - trailingTriviaCore.FullWidth, 0));
				if (syntaxTriviaList3.Node == trailingTriviaCore)
				{
					return token;
				}
				return token.WithTrailingTrivia(syntaxTriviaList3);
			}
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000E9EC4 File Offset: 0x000E80C4
		public virtual SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
		{
			if (this.VisitIntoStructuredTrivia && trivia.HasStructure)
			{
				CSharpSyntaxNode csharpSyntaxNode = (CSharpSyntaxNode)trivia.GetStructure();
				StructuredTriviaSyntax structuredTriviaSyntax = (StructuredTriviaSyntax)this.Visit(csharpSyntaxNode);
				if (structuredTriviaSyntax != csharpSyntaxNode)
				{
					if (structuredTriviaSyntax != null)
					{
						return SyntaxFactory.Trivia(structuredTriviaSyntax);
					}
					return default(SyntaxTrivia);
				}
			}
			return trivia;
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000E9F18 File Offset: 0x000E8118
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public virtual SyntaxList<TNode> VisitList<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SyntaxList<TNode> list) where TNode : SyntaxNode
		{
			SyntaxListBuilder syntaxListBuilder = null;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				TNode tnode = list[i];
				TNode tnode2 = this.VisitListElement<TNode>(tnode);
				if (tnode != tnode2 && syntaxListBuilder == null)
				{
					syntaxListBuilder = new SyntaxListBuilder(count);
					syntaxListBuilder.AddRange<TNode>(list, 0, i);
				}
				if (syntaxListBuilder != null && tnode2 != null && !tnode2.IsKind(SyntaxKind.None))
				{
					syntaxListBuilder.Add(tnode2);
				}
				i++;
			}
			if (syntaxListBuilder != null)
			{
				return syntaxListBuilder.ToList();
			}
			return list;
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000E9FA7 File Offset: 0x000E81A7
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public virtual TNode VisitListElement<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(TNode node) where TNode : SyntaxNode
		{
			return (TNode)((object)this.Visit(node));
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000E9FBC File Offset: 0x000E81BC
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public virtual SeparatedSyntaxList<TNode> VisitList<TNode>([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] SeparatedSyntaxList<TNode> list) where TNode : SyntaxNode
		{
			int count = list.Count;
			int separatorCount = list.SeparatorCount;
			SeparatedSyntaxListBuilder<TNode> separatedSyntaxListBuilder = default(SeparatedSyntaxListBuilder<TNode>);
			int i;
			for (i = 0; i < separatorCount; i++)
			{
				TNode tnode = list[i];
				TNode tnode2 = this.VisitListElement<TNode>(tnode);
				SyntaxToken separator = list.GetSeparator(i);
				SyntaxToken syntaxToken = this.VisitListSeparator(separator);
				if (separatedSyntaxListBuilder.IsNull && (tnode != tnode2 || separator != syntaxToken))
				{
					separatedSyntaxListBuilder = new SeparatedSyntaxListBuilder<TNode>(count);
					separatedSyntaxListBuilder.AddRange(in list, i);
				}
				if (!separatedSyntaxListBuilder.IsNull)
				{
					if (tnode2 != null)
					{
						separatedSyntaxListBuilder.Add(tnode2);
						if (syntaxToken.RawKind == 0)
						{
							throw new InvalidOperationException(CodeAnalysisResources.SeparatorIsExpected);
						}
						separatedSyntaxListBuilder.AddSeparator(in syntaxToken);
					}
					else if (tnode2 == null)
					{
						throw new InvalidOperationException(CodeAnalysisResources.ElementIsExpected);
					}
				}
			}
			if (i < count)
			{
				TNode tnode3 = list[i];
				TNode tnode4 = this.VisitListElement<TNode>(tnode3);
				if (separatedSyntaxListBuilder.IsNull && tnode3 != tnode4)
				{
					separatedSyntaxListBuilder = new SeparatedSyntaxListBuilder<TNode>(count);
					separatedSyntaxListBuilder.AddRange(in list, i);
				}
				if (!separatedSyntaxListBuilder.IsNull && tnode4 != null)
				{
					separatedSyntaxListBuilder.Add(tnode4);
				}
			}
			if (!separatedSyntaxListBuilder.IsNull)
			{
				return separatedSyntaxListBuilder.ToList();
			}
			return list;
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000EA114 File Offset: 0x000E8314
		public virtual SyntaxToken VisitListSeparator(SyntaxToken separator)
		{
			return this.VisitToken(separator);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x000EA120 File Offset: 0x000E8320
		public virtual SyntaxTokenList VisitList(SyntaxTokenList list)
		{
			SyntaxTokenListBuilder syntaxTokenListBuilder = null;
			int count = list.Count;
			int num = -1;
			foreach (SyntaxToken syntaxToken in list)
			{
				num++;
				SyntaxToken syntaxToken2 = this.VisitToken(syntaxToken);
				if (syntaxToken != syntaxToken2 && syntaxTokenListBuilder == null)
				{
					syntaxTokenListBuilder = new SyntaxTokenListBuilder(count);
					syntaxTokenListBuilder.Add(list, 0, num);
				}
				if (syntaxTokenListBuilder != null && syntaxToken2.Kind() != SyntaxKind.None)
				{
					syntaxTokenListBuilder.Add(syntaxToken2);
				}
			}
			if (syntaxTokenListBuilder != null)
			{
				return syntaxTokenListBuilder.ToList();
			}
			return list;
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x000EA1A0 File Offset: 0x000E83A0
		public virtual SyntaxTriviaList VisitList(SyntaxTriviaList list)
		{
			int count = list.Count;
			if (count != 0)
			{
				SyntaxTriviaListBuilder syntaxTriviaListBuilder = null;
				int num = -1;
				foreach (SyntaxTrivia syntaxTrivia in list)
				{
					num++;
					SyntaxTrivia syntaxTrivia2 = this.VisitListElement(syntaxTrivia);
					if (syntaxTrivia2 != syntaxTrivia && syntaxTriviaListBuilder == null)
					{
						syntaxTriviaListBuilder = new SyntaxTriviaListBuilder(count);
						syntaxTriviaListBuilder.Add(in list, 0, num);
					}
					if (syntaxTriviaListBuilder != null && syntaxTrivia2.Kind() != SyntaxKind.None)
					{
						syntaxTriviaListBuilder.Add(syntaxTrivia2);
					}
				}
				if (syntaxTriviaListBuilder != null)
				{
					return syntaxTriviaListBuilder.ToList();
				}
			}
			return list;
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x000EA225 File Offset: 0x000E8425
		public virtual SyntaxTrivia VisitListElement(SyntaxTrivia element)
		{
			return this.VisitTrivia(element);
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x000EA22E File Offset: 0x000E842E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
		{
			return node.Update(this.VisitToken(node.Identifier));
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000EA244 File Offset: 0x000E8444
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitQualifiedName(QualifiedNameSyntax node)
		{
			NameSyntax nameSyntax = (NameSyntax)this.Visit(node.Left);
			if (nameSyntax == null)
			{
				throw new ArgumentNullException("left");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.DotToken);
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)this.Visit(node.Right);
			if (simpleNameSyntax == null)
			{
				throw new ArgumentNullException("right");
			}
			return node.Update(nameSyntax, syntaxToken, simpleNameSyntax);
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000EA2A3 File Offset: 0x000E84A3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitGenericName(GenericNameSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Identifier);
			TypeArgumentListSyntax typeArgumentListSyntax = (TypeArgumentListSyntax)this.Visit(node.TypeArgumentList);
			if (typeArgumentListSyntax == null)
			{
				throw new ArgumentNullException("typeArgumentList");
			}
			return node.Update(syntaxToken, typeArgumentListSyntax);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000EA2D7 File Offset: 0x000E84D7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypeArgumentList(TypeArgumentListSyntax node)
		{
			return node.Update(this.VisitToken(node.LessThanToken), this.VisitList<TypeSyntax>(node.Arguments), this.VisitToken(node.GreaterThanToken));
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000EA304 File Offset: 0x000E8504
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAliasQualifiedName(AliasQualifiedNameSyntax node)
		{
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)this.Visit(node.Alias);
			if (identifierNameSyntax == null)
			{
				throw new ArgumentNullException("alias");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.ColonColonToken);
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)this.Visit(node.Name);
			if (simpleNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(identifierNameSyntax, syntaxToken, simpleNameSyntax);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000EA363 File Offset: 0x000E8563
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPredefinedType(PredefinedTypeSyntax node)
		{
			return node.Update(this.VisitToken(node.Keyword));
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000EA377 File Offset: 0x000E8577
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitArrayType(ArrayTypeSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.ElementType);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("elementType");
			}
			return node.Update(typeSyntax, this.VisitList<ArrayRankSpecifierSyntax>(node.RankSpecifiers));
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000EA3AB File Offset: 0x000E85AB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitArrayRankSpecifier(ArrayRankSpecifierSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBracketToken), this.VisitList<ExpressionSyntax>(node.Sizes), this.VisitToken(node.CloseBracketToken));
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000EA3D7 File Offset: 0x000E85D7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPointerType(PointerTypeSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.ElementType);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("elementType");
			}
			return node.Update(typeSyntax, this.VisitToken(node.AsteriskToken));
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000EA40C File Offset: 0x000E860C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFunctionPointerType(FunctionPointerTypeSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.DelegateKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.AsteriskToken);
			FunctionPointerCallingConventionSyntax functionPointerCallingConventionSyntax = (FunctionPointerCallingConventionSyntax)this.Visit(node.CallingConvention);
			FunctionPointerParameterListSyntax functionPointerParameterListSyntax = (FunctionPointerParameterListSyntax)this.Visit(node.ParameterList);
			if (functionPointerParameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxToken, syntaxToken2, functionPointerCallingConventionSyntax, functionPointerParameterListSyntax);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000EA468 File Offset: 0x000E8668
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFunctionPointerParameterList(FunctionPointerParameterListSyntax node)
		{
			return node.Update(this.VisitToken(node.LessThanToken), this.VisitList<FunctionPointerParameterSyntax>(node.Parameters), this.VisitToken(node.GreaterThanToken));
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000EA494 File Offset: 0x000E8694
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFunctionPointerCallingConvention(FunctionPointerCallingConventionSyntax node)
		{
			return node.Update(this.VisitToken(node.ManagedOrUnmanagedKeyword), (FunctionPointerUnmanagedCallingConventionListSyntax)this.Visit(node.UnmanagedCallingConventionList));
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000EA4B9 File Offset: 0x000E86B9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFunctionPointerUnmanagedCallingConventionList(FunctionPointerUnmanagedCallingConventionListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBracketToken), this.VisitList<FunctionPointerUnmanagedCallingConventionSyntax>(node.CallingConventions), this.VisitToken(node.CloseBracketToken));
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000EA4E5 File Offset: 0x000E86E5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFunctionPointerUnmanagedCallingConvention(FunctionPointerUnmanagedCallingConventionSyntax node)
		{
			return node.Update(this.VisitToken(node.Name));
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000EA4F9 File Offset: 0x000E86F9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitNullableType(NullableTypeSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.ElementType);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("elementType");
			}
			return node.Update(typeSyntax, this.VisitToken(node.QuestionToken));
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000EA52D File Offset: 0x000E872D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTupleType(TupleTypeSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<TupleElementSyntax>(node.Elements), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000EA559 File Offset: 0x000E8759
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTupleElement(TupleElementSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(typeSyntax, this.VisitToken(node.Identifier));
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000EA58D File Offset: 0x000E878D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitOmittedTypeArgument(OmittedTypeArgumentSyntax node)
		{
			return node.Update(this.VisitToken(node.OmittedTypeArgumentToken));
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000EA5A1 File Offset: 0x000E87A1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRefType(RefTypeSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.RefKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.ReadOnlyKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, syntaxToken2, typeSyntax);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000EA5E1 File Offset: 0x000E87E1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitParenthesizedExpression(ParenthesizedExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000EA621 File Offset: 0x000E8821
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTupleExpression(TupleExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<ArgumentSyntax>(node.Arguments), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000EA64D File Offset: 0x000E884D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Operand);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("operand");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000EA681 File Offset: 0x000E8881
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAwaitExpression(AwaitExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.AwaitKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000EA6B5 File Offset: 0x000E88B5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Operand);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("operand");
			}
			return node.Update(expressionSyntax, this.VisitToken(node.OperatorToken));
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000EA6EC File Offset: 0x000E88EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)this.Visit(node.Name);
			if (simpleNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(expressionSyntax, syntaxToken, simpleNameSyntax);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000EA74C File Offset: 0x000E894C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)this.Visit(node.WhenNotNull);
			if (expressionSyntax2 == null)
			{
				throw new ArgumentNullException("whenNotNull");
			}
			return node.Update(expressionSyntax, syntaxToken, expressionSyntax2);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000EA7AB File Offset: 0x000E89AB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitMemberBindingExpression(MemberBindingExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			SimpleNameSyntax simpleNameSyntax = (SimpleNameSyntax)this.Visit(node.Name);
			if (simpleNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxToken, simpleNameSyntax);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000EA7DF File Offset: 0x000E89DF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitElementBindingExpression(ElementBindingExpressionSyntax node)
		{
			BracketedArgumentListSyntax bracketedArgumentListSyntax = (BracketedArgumentListSyntax)this.Visit(node.ArgumentList);
			if (bracketedArgumentListSyntax == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return node.Update(bracketedArgumentListSyntax);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000EA807 File Offset: 0x000E8A07
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRangeExpression(RangeExpressionSyntax node)
		{
			return node.Update((ExpressionSyntax)this.Visit(node.LeftOperand), this.VisitToken(node.OperatorToken), (ExpressionSyntax)this.Visit(node.RightOperand));
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000EA83D File Offset: 0x000E8A3D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitImplicitElementAccess(ImplicitElementAccessSyntax node)
		{
			BracketedArgumentListSyntax bracketedArgumentListSyntax = (BracketedArgumentListSyntax)this.Visit(node.ArgumentList);
			if (bracketedArgumentListSyntax == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return node.Update(bracketedArgumentListSyntax);
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000EA868 File Offset: 0x000E8A68
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBinaryExpression(BinaryExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Left);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("left");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)this.Visit(node.Right);
			if (expressionSyntax2 == null)
			{
				throw new ArgumentNullException("right");
			}
			return node.Update(expressionSyntax, syntaxToken, expressionSyntax2);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000EA8C8 File Offset: 0x000E8AC8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAssignmentExpression(AssignmentExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Left);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("left");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)this.Visit(node.Right);
			if (expressionSyntax2 == null)
			{
				throw new ArgumentNullException("right");
			}
			return node.Update(expressionSyntax, syntaxToken, expressionSyntax2);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000EA928 File Offset: 0x000E8B28
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConditionalExpression(ConditionalExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.QuestionToken);
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)this.Visit(node.WhenTrue);
			if (expressionSyntax2 == null)
			{
				throw new ArgumentNullException("whenTrue");
			}
			SyntaxToken syntaxToken2 = this.VisitToken(node.ColonToken);
			ExpressionSyntax expressionSyntax3 = (ExpressionSyntax)this.Visit(node.WhenFalse);
			if (expressionSyntax3 == null)
			{
				throw new ArgumentNullException("whenFalse");
			}
			return node.Update(expressionSyntax, syntaxToken, expressionSyntax2, syntaxToken2, expressionSyntax3);
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000EA9B3 File Offset: 0x000E8BB3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitThisExpression(ThisExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.Token));
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000EA9C7 File Offset: 0x000E8BC7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBaseExpression(BaseExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.Token));
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000EA9DB File Offset: 0x000E8BDB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLiteralExpression(LiteralExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.Token));
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000EA9F0 File Offset: 0x000E8BF0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitMakeRefExpression(MakeRefExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, syntaxToken2, expressionSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000EAA48 File Offset: 0x000E8C48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRefTypeExpression(RefTypeExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, syntaxToken2, expressionSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000EAAA0 File Offset: 0x000E8CA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRefValueExpression(RefValueExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken3 = this.VisitToken(node.Comma);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, typeSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000EAB24 File Offset: 0x000E8D24
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCheckedExpression(CheckedExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, syntaxToken2, expressionSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000EAB7C File Offset: 0x000E8D7C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDefaultExpression(DefaultExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, syntaxToken2, typeSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000EABD4 File Offset: 0x000E8DD4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypeOfExpression(TypeOfExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, syntaxToken2, typeSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x000EAC2C File Offset: 0x000E8E2C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSizeOfExpression(SizeOfExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, syntaxToken2, typeSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x000EAC84 File Offset: 0x000E8E84
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInvocationExpression(InvocationExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)this.Visit(node.ArgumentList);
			if (argumentListSyntax == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return node.Update(expressionSyntax, argumentListSyntax);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000EACD8 File Offset: 0x000E8ED8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitElementAccessExpression(ElementAccessExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			BracketedArgumentListSyntax bracketedArgumentListSyntax = (BracketedArgumentListSyntax)this.Visit(node.ArgumentList);
			if (bracketedArgumentListSyntax == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return node.Update(expressionSyntax, bracketedArgumentListSyntax);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000EAD2B File Offset: 0x000E8F2B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitArgumentList(ArgumentListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<ArgumentSyntax>(node.Arguments), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000EAD57 File Offset: 0x000E8F57
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBracketedArgumentList(BracketedArgumentListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBracketToken), this.VisitList<ArgumentSyntax>(node.Arguments), this.VisitToken(node.CloseBracketToken));
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000EAD84 File Offset: 0x000E8F84
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitArgument(ArgumentSyntax node)
		{
			NameColonSyntax nameColonSyntax = (NameColonSyntax)this.Visit(node.NameColon);
			SyntaxToken syntaxToken = this.VisitToken(node.RefKindKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(nameColonSyntax, syntaxToken, expressionSyntax);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000EADD4 File Offset: 0x000E8FD4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitNameColon(NameColonSyntax node)
		{
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)this.Visit(node.Name);
			if (identifierNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(identifierNameSyntax, this.VisitToken(node.ColonToken));
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000EAE08 File Offset: 0x000E9008
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDeclarationExpression(DeclarationExpressionSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			VariableDesignationSyntax variableDesignationSyntax = (VariableDesignationSyntax)this.Visit(node.Designation);
			if (variableDesignationSyntax == null)
			{
				throw new ArgumentNullException("designation");
			}
			return node.Update(typeSyntax, variableDesignationSyntax);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000EAE5C File Offset: 0x000E905C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCastExpression(CastExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OpenParenToken);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			SyntaxToken syntaxToken2 = this.VisitToken(node.CloseParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, typeSyntax, syntaxToken2, expressionSyntax);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000EAEC8 File Offset: 0x000E90C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
		{
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.DelegateKeyword);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			BlockSyntax blockSyntax = (BlockSyntax)this.Visit(node.Block);
			if (blockSyntax == null)
			{
				throw new ArgumentNullException("block");
			}
			return node.Update(syntaxTokenList, syntaxToken, parameterListSyntax, blockSyntax, (ExpressionSyntax)this.Visit(node.ExpressionBody));
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000EAF38 File Offset: 0x000E9138
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			ParameterSyntax parameterSyntax = (ParameterSyntax)this.Visit(node.Parameter);
			if (parameterSyntax == null)
			{
				throw new ArgumentNullException("parameter");
			}
			return node.Update(syntaxList, syntaxTokenList, parameterSyntax, this.VisitToken(node.ArrowToken), (BlockSyntax)this.Visit(node.Block), (ExpressionSyntax)this.Visit(node.ExpressionBody));
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000EAFB1 File Offset: 0x000E91B1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRefExpression(RefExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.RefKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000EAFE8 File Offset: 0x000E91E8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, parameterListSyntax, this.VisitToken(node.ArrowToken), (BlockSyntax)this.Visit(node.Block), (ExpressionSyntax)this.Visit(node.ExpressionBody));
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000EB061 File Offset: 0x000E9261
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInitializerExpression(InitializerExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBraceToken), this.VisitList<ExpressionSyntax>(node.Expressions), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000EB090 File Offset: 0x000E9290
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.NewKeyword);
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)this.Visit(node.ArgumentList);
			if (argumentListSyntax == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return node.Update(syntaxToken, argumentListSyntax, (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000EB0E0 File Offset: 0x000E92E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.NewKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, typeSyntax, (ArgumentListSyntax)this.Visit(node.ArgumentList), (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000EB144 File Offset: 0x000E9344
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitWithExpression(WithExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.WithKeyword);
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)this.Visit(node.Initializer);
			if (initializerExpressionSyntax == null)
			{
				throw new ArgumentNullException("initializer");
			}
			return node.Update(expressionSyntax, syntaxToken, initializerExpressionSyntax);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000EB1A3 File Offset: 0x000E93A3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax node)
		{
			NameEqualsSyntax nameEqualsSyntax = (NameEqualsSyntax)this.Visit(node.NameEquals);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(nameEqualsSyntax, expressionSyntax);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000EB1DC File Offset: 0x000E93DC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.NewKeyword), this.VisitToken(node.OpenBraceToken), this.VisitList<AnonymousObjectMemberDeclaratorSyntax>(node.Initializers), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000EB214 File Offset: 0x000E9414
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitArrayCreationExpression(ArrayCreationExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.NewKeyword);
			ArrayTypeSyntax arrayTypeSyntax = (ArrayTypeSyntax)this.Visit(node.Type);
			if (arrayTypeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, arrayTypeSyntax, (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000EB264 File Offset: 0x000E9464
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.NewKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenBracketToken);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Commas);
			SyntaxToken syntaxToken3 = this.VisitToken(node.CloseBracketToken);
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)this.Visit(node.Initializer);
			if (initializerExpressionSyntax == null)
			{
				throw new ArgumentNullException("initializer");
			}
			return node.Update(syntaxToken, syntaxToken2, syntaxTokenList, syntaxToken3, initializerExpressionSyntax);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000EB2C8 File Offset: 0x000E94C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.StackAllocKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, typeSyntax, (InitializerExpressionSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000EB318 File Offset: 0x000E9518
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.StackAllocKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenBracketToken);
			SyntaxToken syntaxToken3 = this.VisitToken(node.CloseBracketToken);
			InitializerExpressionSyntax initializerExpressionSyntax = (InitializerExpressionSyntax)this.Visit(node.Initializer);
			if (initializerExpressionSyntax == null)
			{
				throw new ArgumentNullException("initializer");
			}
			return node.Update(syntaxToken, syntaxToken2, syntaxToken3, initializerExpressionSyntax);
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000EB370 File Offset: 0x000E9570
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitQueryExpression(QueryExpressionSyntax node)
		{
			FromClauseSyntax fromClauseSyntax = (FromClauseSyntax)this.Visit(node.FromClause);
			if (fromClauseSyntax == null)
			{
				throw new ArgumentNullException("fromClause");
			}
			QueryBodySyntax queryBodySyntax = (QueryBodySyntax)this.Visit(node.Body);
			if (queryBodySyntax == null)
			{
				throw new ArgumentNullException("body");
			}
			return node.Update(fromClauseSyntax, queryBodySyntax);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000EB3C4 File Offset: 0x000E95C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitQueryBody(QueryBodySyntax node)
		{
			SyntaxList<QueryClauseSyntax> syntaxList = this.VisitList<QueryClauseSyntax>(node.Clauses);
			SelectOrGroupClauseSyntax selectOrGroupClauseSyntax = (SelectOrGroupClauseSyntax)this.Visit(node.SelectOrGroup);
			if (selectOrGroupClauseSyntax == null)
			{
				throw new ArgumentNullException("selectOrGroup");
			}
			return node.Update(syntaxList, selectOrGroupClauseSyntax, (QueryContinuationSyntax)this.Visit(node.Continuation));
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000EB414 File Offset: 0x000E9614
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFromClause(FromClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.FromKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			SyntaxToken syntaxToken2 = this.VisitToken(node.Identifier);
			SyntaxToken syntaxToken3 = this.VisitToken(node.InKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, typeSyntax, syntaxToken2, syntaxToken3, expressionSyntax);
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000EB47C File Offset: 0x000E967C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLetClause(LetClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.LetKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.Identifier);
			SyntaxToken syntaxToken3 = this.VisitToken(node.EqualsToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, syntaxToken2, syntaxToken3, expressionSyntax);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000EB4D4 File Offset: 0x000E96D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitJoinClause(JoinClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.JoinKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			SyntaxToken syntaxToken2 = this.VisitToken(node.Identifier);
			SyntaxToken syntaxToken3 = this.VisitToken(node.InKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.InExpression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("inExpression");
			}
			SyntaxToken syntaxToken4 = this.VisitToken(node.OnKeyword);
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)this.Visit(node.LeftExpression);
			if (expressionSyntax2 == null)
			{
				throw new ArgumentNullException("leftExpression");
			}
			SyntaxToken syntaxToken5 = this.VisitToken(node.EqualsKeyword);
			ExpressionSyntax expressionSyntax3 = (ExpressionSyntax)this.Visit(node.RightExpression);
			if (expressionSyntax3 == null)
			{
				throw new ArgumentNullException("rightExpression");
			}
			return node.Update(syntaxToken, typeSyntax, syntaxToken2, syntaxToken3, expressionSyntax, syntaxToken4, expressionSyntax2, syntaxToken5, expressionSyntax3, (JoinIntoClauseSyntax)this.Visit(node.Into));
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000EB5A5 File Offset: 0x000E97A5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitJoinIntoClause(JoinIntoClauseSyntax node)
		{
			return node.Update(this.VisitToken(node.IntoKeyword), this.VisitToken(node.Identifier));
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000EB5C5 File Offset: 0x000E97C5
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitWhereClause(WhereClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.WhereKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000EB5F9 File Offset: 0x000E97F9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitOrderByClause(OrderByClauseSyntax node)
		{
			return node.Update(this.VisitToken(node.OrderByKeyword), this.VisitList<OrderingSyntax>(node.Orderings));
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000EB619 File Offset: 0x000E9819
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitOrdering(OrderingSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(expressionSyntax, this.VisitToken(node.AscendingOrDescendingKeyword));
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000EB64D File Offset: 0x000E984D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSelectClause(SelectClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.SelectKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000EB684 File Offset: 0x000E9884
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitGroupClause(GroupClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.GroupKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.GroupExpression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("groupExpression");
			}
			SyntaxToken syntaxToken2 = this.VisitToken(node.ByKeyword);
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)this.Visit(node.ByExpression);
			if (expressionSyntax2 == null)
			{
				throw new ArgumentNullException("byExpression");
			}
			return node.Update(syntaxToken, expressionSyntax, syntaxToken2, expressionSyntax2);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000EB6EF File Offset: 0x000E98EF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitQueryContinuation(QueryContinuationSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.IntoKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.Identifier);
			QueryBodySyntax queryBodySyntax = (QueryBodySyntax)this.Visit(node.Body);
			if (queryBodySyntax == null)
			{
				throw new ArgumentNullException("body");
			}
			return node.Update(syntaxToken, syntaxToken2, queryBodySyntax);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000EB72F File Offset: 0x000E992F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.OmittedArraySizeExpressionToken));
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000EB743 File Offset: 0x000E9943
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
		{
			return node.Update(this.VisitToken(node.StringStartToken), this.VisitList<InterpolatedStringContentSyntax>(node.Contents), this.VisitToken(node.StringEndToken));
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000EB770 File Offset: 0x000E9970
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitIsPatternExpression(IsPatternExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.IsKeyword);
			PatternSyntax patternSyntax = (PatternSyntax)this.Visit(node.Pattern);
			if (patternSyntax == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return node.Update(expressionSyntax, syntaxToken, patternSyntax);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000EB7CF File Offset: 0x000E99CF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitThrowExpression(ThrowExpressionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.ThrowKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000EB803 File Offset: 0x000E9A03
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitWhenClause(WhenClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.WhenKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000EB837 File Offset: 0x000E9A37
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDiscardPattern(DiscardPatternSyntax node)
		{
			return node.Update(this.VisitToken(node.UnderscoreToken));
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000EB84C File Offset: 0x000E9A4C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDeclarationPattern(DeclarationPatternSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			VariableDesignationSyntax variableDesignationSyntax = (VariableDesignationSyntax)this.Visit(node.Designation);
			if (variableDesignationSyntax == null)
			{
				throw new ArgumentNullException("designation");
			}
			return node.Update(typeSyntax, variableDesignationSyntax);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000EB89F File Offset: 0x000E9A9F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitVarPattern(VarPatternSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.VarKeyword);
			VariableDesignationSyntax variableDesignationSyntax = (VariableDesignationSyntax)this.Visit(node.Designation);
			if (variableDesignationSyntax == null)
			{
				throw new ArgumentNullException("designation");
			}
			return node.Update(syntaxToken, variableDesignationSyntax);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000EB8D4 File Offset: 0x000E9AD4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRecursivePattern(RecursivePatternSyntax node)
		{
			return node.Update((TypeSyntax)this.Visit(node.Type), (PositionalPatternClauseSyntax)this.Visit(node.PositionalPatternClause), (PropertyPatternClauseSyntax)this.Visit(node.PropertyPatternClause), (VariableDesignationSyntax)this.Visit(node.Designation));
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000EB92B File Offset: 0x000E9B2B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPositionalPatternClause(PositionalPatternClauseSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<SubpatternSyntax>(node.Subpatterns), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x000EB957 File Offset: 0x000E9B57
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPropertyPatternClause(PropertyPatternClauseSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBraceToken), this.VisitList<SubpatternSyntax>(node.Subpatterns), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x000EB983 File Offset: 0x000E9B83
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSubpattern(SubpatternSyntax node)
		{
			NameColonSyntax nameColonSyntax = (NameColonSyntax)this.Visit(node.NameColon);
			PatternSyntax patternSyntax = (PatternSyntax)this.Visit(node.Pattern);
			if (patternSyntax == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return node.Update(nameColonSyntax, patternSyntax);
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x000EB9BC File Offset: 0x000E9BBC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConstantPattern(ConstantPatternSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(expressionSyntax);
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x000EB9E4 File Offset: 0x000E9BE4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitParenthesizedPattern(ParenthesizedPatternSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OpenParenToken);
			PatternSyntax patternSyntax = (PatternSyntax)this.Visit(node.Pattern);
			if (patternSyntax == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return node.Update(syntaxToken, patternSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x000EBA24 File Offset: 0x000E9C24
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRelationalPattern(RelationalPatternSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000EBA58 File Offset: 0x000E9C58
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypePattern(TypePatternSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(typeSyntax);
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000EBA80 File Offset: 0x000E9C80
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBinaryPattern(BinaryPatternSyntax node)
		{
			PatternSyntax patternSyntax = (PatternSyntax)this.Visit(node.Left);
			if (patternSyntax == null)
			{
				throw new ArgumentNullException("left");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			PatternSyntax patternSyntax2 = (PatternSyntax)this.Visit(node.Right);
			if (patternSyntax2 == null)
			{
				throw new ArgumentNullException("right");
			}
			return node.Update(patternSyntax, syntaxToken, patternSyntax2);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x000EBADF File Offset: 0x000E9CDF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitUnaryPattern(UnaryPatternSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorToken);
			PatternSyntax patternSyntax = (PatternSyntax)this.Visit(node.Pattern);
			if (patternSyntax == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return node.Update(syntaxToken, patternSyntax);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000EBB13 File Offset: 0x000E9D13
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInterpolatedStringText(InterpolatedStringTextSyntax node)
		{
			return node.Update(this.VisitToken(node.TextToken));
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x000EBB28 File Offset: 0x000E9D28
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInterpolation(InterpolationSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OpenBraceToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax, (InterpolationAlignmentClauseSyntax)this.Visit(node.AlignmentClause), (InterpolationFormatClauseSyntax)this.Visit(node.FormatClause), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x000EBB95 File Offset: 0x000E9D95
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.CommaToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Value);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("value");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x000EBBC9 File Offset: 0x000E9DC9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInterpolationFormatClause(InterpolationFormatClauseSyntax node)
		{
			return node.Update(this.VisitToken(node.ColonToken), this.VisitToken(node.FormatStringToken));
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x000EBBE9 File Offset: 0x000E9DE9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitGlobalStatement(GlobalStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxTokenList, statementSyntax);
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000EBC29 File Offset: 0x000E9E29
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBlock(BlockSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.OpenBraceToken), this.VisitList<StatementSyntax>(node.Statements), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000EBC64 File Offset: 0x000E9E64
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.ReturnType);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("returnType");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.Identifier);
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)this.Visit(node.TypeParameterList);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, typeSyntax, syntaxToken, typeParameterListSyntax, parameterListSyntax, this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000EBD28 File Offset: 0x000E9F28
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.AwaitKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.UsingKeyword);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)this.Visit(node.Declaration);
			if (variableDeclarationSyntax == null)
			{
				throw new ArgumentNullException("declaration");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, syntaxTokenList, variableDeclarationSyntax, this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000EBD97 File Offset: 0x000E9F97
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitVariableDeclaration(VariableDeclarationSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(typeSyntax, this.VisitList<VariableDeclaratorSyntax>(node.Variables));
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000EBDCB File Offset: 0x000E9FCB
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitVariableDeclarator(VariableDeclaratorSyntax node)
		{
			return node.Update(this.VisitToken(node.Identifier), (BracketedArgumentListSyntax)this.Visit(node.ArgumentList), (EqualsValueClauseSyntax)this.Visit(node.Initializer));
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000EBE01 File Offset: 0x000EA001
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEqualsValueClause(EqualsValueClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.EqualsToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Value);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("value");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000EBE35 File Offset: 0x000EA035
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSingleVariableDesignation(SingleVariableDesignationSyntax node)
		{
			return node.Update(this.VisitToken(node.Identifier));
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000EBE49 File Offset: 0x000EA049
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDiscardDesignation(DiscardDesignationSyntax node)
		{
			return node.Update(this.VisitToken(node.UnderscoreToken));
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000EBE5D File Offset: 0x000EA05D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<VariableDesignationSyntax>(node.Variables), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000EBE89 File Offset: 0x000EA089
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitExpressionStatement(ExpressionStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxList, expressionSyntax, this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000EBEC9 File Offset: 0x000EA0C9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000EBEEC File Offset: 0x000EA0EC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLabeledStatement(LabeledStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.Identifier);
			SyntaxToken syntaxToken2 = this.VisitToken(node.ColonToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, statementSyntax);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000EBF44 File Offset: 0x000EA144
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitGotoStatement(GotoStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.GotoKeyword), this.VisitToken(node.CaseOrDefaultKeyword), (ExpressionSyntax)this.Visit(node.Expression), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000EBF98 File Offset: 0x000EA198
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBreakStatement(BreakStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.BreakKeyword), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000EBFC4 File Offset: 0x000EA1C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitContinueStatement(ContinueStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.ContinueKeyword), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000EBFF0 File Offset: 0x000EA1F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitReturnStatement(ReturnStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.ReturnKeyword), (ExpressionSyntax)this.Visit(node.Expression), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000EC02D File Offset: 0x000EA22D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitThrowStatement(ThrowStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.ThrowKeyword), (ExpressionSyntax)this.Visit(node.Expression), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000EC06C File Offset: 0x000EA26C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitYieldStatement(YieldStatementSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.YieldKeyword), this.VisitToken(node.ReturnOrBreakKeyword), (ExpressionSyntax)this.Visit(node.Expression), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000EC0C0 File Offset: 0x000EA2C0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitWhileStatement(WhileStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.WhileKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			SyntaxToken syntaxToken3 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, statementSyntax);
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000EC144 File Offset: 0x000EA344
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDoStatement(DoStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.DoKeyword);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			SyntaxToken syntaxToken2 = this.VisitToken(node.WhileKeyword);
			SyntaxToken syntaxToken3 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			return node.Update(syntaxList, syntaxToken, statementSyntax, syntaxToken2, syntaxToken3, expressionSyntax, this.VisitToken(node.CloseParenToken), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000EC1E0 File Offset: 0x000EA3E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitForStatement(ForStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.ForKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)this.Visit(node.Declaration);
			SeparatedSyntaxList<ExpressionSyntax> separatedSyntaxList = this.VisitList<ExpressionSyntax>(node.Initializers);
			SyntaxToken syntaxToken3 = this.VisitToken(node.FirstSemicolonToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			SyntaxToken syntaxToken4 = this.VisitToken(node.SecondSemicolonToken);
			SeparatedSyntaxList<ExpressionSyntax> separatedSyntaxList2 = this.VisitList<ExpressionSyntax>(node.Incrementors);
			SyntaxToken syntaxToken5 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, variableDeclarationSyntax, separatedSyntaxList, syntaxToken3, expressionSyntax, syntaxToken4, separatedSyntaxList2, syntaxToken5, statementSyntax);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000EC298 File Offset: 0x000EA498
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitForEachStatement(ForEachStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.AwaitKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.ForEachKeyword);
			SyntaxToken syntaxToken3 = this.VisitToken(node.OpenParenToken);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			SyntaxToken syntaxToken4 = this.VisitToken(node.Identifier);
			SyntaxToken syntaxToken5 = this.VisitToken(node.InKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken6 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, syntaxToken3, typeSyntax, syntaxToken4, syntaxToken5, expressionSyntax, syntaxToken6, statementSyntax);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000EC360 File Offset: 0x000EA560
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.AwaitKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.ForEachKeyword);
			SyntaxToken syntaxToken3 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Variable);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("variable");
			}
			SyntaxToken syntaxToken4 = this.VisitToken(node.InKeyword);
			ExpressionSyntax expressionSyntax2 = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax2 == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken5 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, syntaxToken3, expressionSyntax, syntaxToken4, expressionSyntax2, syntaxToken5, statementSyntax);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000EC41C File Offset: 0x000EA61C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitUsingStatement(UsingStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.AwaitKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.UsingKeyword);
			SyntaxToken syntaxToken3 = this.VisitToken(node.OpenParenToken);
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)this.Visit(node.Declaration);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			SyntaxToken syntaxToken4 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, syntaxToken3, variableDeclarationSyntax, expressionSyntax, syntaxToken4, statementSyntax);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000EC4B0 File Offset: 0x000EA6B0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFixedStatement(FixedStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.FixedKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)this.Visit(node.Declaration);
			if (variableDeclarationSyntax == null)
			{
				throw new ArgumentNullException("declaration");
			}
			SyntaxToken syntaxToken3 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, variableDeclarationSyntax, syntaxToken3, statementSyntax);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000EC533 File Offset: 0x000EA733
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCheckedStatement(CheckedStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			BlockSyntax blockSyntax = (BlockSyntax)this.Visit(node.Block);
			if (blockSyntax == null)
			{
				throw new ArgumentNullException("block");
			}
			return node.Update(syntaxList, syntaxToken, blockSyntax);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000EC573 File Offset: 0x000EA773
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitUnsafeStatement(UnsafeStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.UnsafeKeyword);
			BlockSyntax blockSyntax = (BlockSyntax)this.Visit(node.Block);
			if (blockSyntax == null)
			{
				throw new ArgumentNullException("block");
			}
			return node.Update(syntaxList, syntaxToken, blockSyntax);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000EC5B4 File Offset: 0x000EA7B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLockStatement(LockStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.LockKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			SyntaxToken syntaxToken3 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, statementSyntax);
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000EC638 File Offset: 0x000EA838
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitIfStatement(IfStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.IfKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			SyntaxToken syntaxToken3 = this.VisitToken(node.CloseParenToken);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, expressionSyntax, syntaxToken3, statementSyntax, (ElseClauseSyntax)this.Visit(node.Else));
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000EC6CC File Offset: 0x000EA8CC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitElseClause(ElseClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.ElseKeyword);
			StatementSyntax statementSyntax = (StatementSyntax)this.Visit(node.Statement);
			if (statementSyntax == null)
			{
				throw new ArgumentNullException("statement");
			}
			return node.Update(syntaxToken, statementSyntax);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000EC700 File Offset: 0x000EA900
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSwitchStatement(SwitchStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.SwitchKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxList, syntaxToken, syntaxToken2, expressionSyntax, this.VisitToken(node.CloseParenToken), this.VisitToken(node.OpenBraceToken), this.VisitList<SwitchSectionSyntax>(node.Sections), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000EC787 File Offset: 0x000EA987
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSwitchSection(SwitchSectionSyntax node)
		{
			return node.Update(this.VisitList<SwitchLabelSyntax>(node.Labels), this.VisitList<StatementSyntax>(node.Statements));
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			PatternSyntax patternSyntax = (PatternSyntax)this.Visit(node.Pattern);
			if (patternSyntax == null)
			{
				throw new ArgumentNullException("pattern");
			}
			return node.Update(syntaxToken, patternSyntax, (WhenClauseSyntax)this.Visit(node.WhenClause), this.VisitToken(node.ColonToken));
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000EC804 File Offset: 0x000EAA04
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCaseSwitchLabel(CaseSwitchLabelSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.Keyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Value);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("value");
			}
			return node.Update(syntaxToken, expressionSyntax, this.VisitToken(node.ColonToken));
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000EC844 File Offset: 0x000EAA44
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax node)
		{
			return node.Update(this.VisitToken(node.Keyword), this.VisitToken(node.ColonToken));
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000EC864 File Offset: 0x000EAA64
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSwitchExpression(SwitchExpressionSyntax node)
		{
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.GoverningExpression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("governingExpression");
			}
			return node.Update(expressionSyntax, this.VisitToken(node.SwitchKeyword), this.VisitToken(node.OpenBraceToken), this.VisitList<SwitchExpressionArmSyntax>(node.Arms), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000EC8C8 File Offset: 0x000EAAC8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSwitchExpressionArm(SwitchExpressionArmSyntax node)
		{
			PatternSyntax patternSyntax = (PatternSyntax)this.Visit(node.Pattern);
			if (patternSyntax == null)
			{
				throw new ArgumentNullException("pattern");
			}
			WhenClauseSyntax whenClauseSyntax = (WhenClauseSyntax)this.Visit(node.WhenClause);
			SyntaxToken syntaxToken = this.VisitToken(node.EqualsGreaterThanToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(patternSyntax, whenClauseSyntax, syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000EC938 File Offset: 0x000EAB38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTryStatement(TryStatementSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxToken syntaxToken = this.VisitToken(node.TryKeyword);
			BlockSyntax blockSyntax = (BlockSyntax)this.Visit(node.Block);
			if (blockSyntax == null)
			{
				throw new ArgumentNullException("block");
			}
			return node.Update(syntaxList, syntaxToken, blockSyntax, this.VisitList<CatchClauseSyntax>(node.Catches), (FinallyClauseSyntax)this.Visit(node.Finally));
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000EC9A0 File Offset: 0x000EABA0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCatchClause(CatchClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.CatchKeyword);
			CatchDeclarationSyntax catchDeclarationSyntax = (CatchDeclarationSyntax)this.Visit(node.Declaration);
			CatchFilterClauseSyntax catchFilterClauseSyntax = (CatchFilterClauseSyntax)this.Visit(node.Filter);
			BlockSyntax blockSyntax = (BlockSyntax)this.Visit(node.Block);
			if (blockSyntax == null)
			{
				throw new ArgumentNullException("block");
			}
			return node.Update(syntaxToken, catchDeclarationSyntax, catchFilterClauseSyntax, blockSyntax);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000ECA04 File Offset: 0x000EAC04
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCatchDeclaration(CatchDeclarationSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.OpenParenToken);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, typeSyntax, this.VisitToken(node.Identifier), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000ECA5C File Offset: 0x000EAC5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCatchFilterClause(CatchFilterClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.WhenKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OpenParenToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.FilterExpression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("filterExpression");
			}
			return node.Update(syntaxToken, syntaxToken2, expressionSyntax, this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000ECAB3 File Offset: 0x000EACB3
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFinallyClause(FinallyClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.FinallyKeyword);
			BlockSyntax blockSyntax = (BlockSyntax)this.Visit(node.Block);
			if (blockSyntax == null)
			{
				throw new ArgumentNullException("block");
			}
			return node.Update(syntaxToken, blockSyntax);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000ECAE8 File Offset: 0x000EACE8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCompilationUnit(CompilationUnitSyntax node)
		{
			return node.Update(this.VisitList<ExternAliasDirectiveSyntax>(node.Externs), this.VisitList<UsingDirectiveSyntax>(node.Usings), this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList<MemberDeclarationSyntax>(node.Members), this.VisitToken(node.EndOfFileToken));
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000ECB37 File Offset: 0x000EAD37
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitExternAliasDirective(ExternAliasDirectiveSyntax node)
		{
			return node.Update(this.VisitToken(node.ExternKeyword), this.VisitToken(node.AliasKeyword), this.VisitToken(node.Identifier), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000ECB70 File Offset: 0x000EAD70
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitUsingDirective(UsingDirectiveSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.GlobalKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.UsingKeyword);
			SyntaxToken syntaxToken3 = this.VisitToken(node.StaticKeyword);
			NameEqualsSyntax nameEqualsSyntax = (NameEqualsSyntax)this.Visit(node.Alias);
			NameSyntax nameSyntax = (NameSyntax)this.Visit(node.Name);
			if (nameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxToken, syntaxToken2, syntaxToken3, nameEqualsSyntax, nameSyntax, this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000ECBE4 File Offset: 0x000EADE4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.NamespaceKeyword);
			NameSyntax nameSyntax = (NameSyntax)this.Visit(node.Name);
			if (nameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxList, syntaxTokenList, syntaxToken, nameSyntax, this.VisitToken(node.OpenBraceToken), this.VisitList<ExternAliasDirectiveSyntax>(node.Externs), this.VisitList<UsingDirectiveSyntax>(node.Usings), this.VisitList<MemberDeclarationSyntax>(node.Members), this.VisitToken(node.CloseBraceToken), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000ECC83 File Offset: 0x000EAE83
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAttributeList(AttributeListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBracketToken), (AttributeTargetSpecifierSyntax)this.Visit(node.Target), this.VisitList<AttributeSyntax>(node.Attributes), this.VisitToken(node.CloseBracketToken));
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000ECCC0 File Offset: 0x000EAEC0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax node)
		{
			return node.Update(this.VisitToken(node.Identifier), this.VisitToken(node.ColonToken));
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000ECCE0 File Offset: 0x000EAEE0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAttribute(AttributeSyntax node)
		{
			NameSyntax nameSyntax = (NameSyntax)this.Visit(node.Name);
			if (nameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(nameSyntax, (AttributeArgumentListSyntax)this.Visit(node.ArgumentList));
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000ECD19 File Offset: 0x000EAF19
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAttributeArgumentList(AttributeArgumentListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<AttributeArgumentSyntax>(node.Arguments), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000ECD48 File Offset: 0x000EAF48
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAttributeArgument(AttributeArgumentSyntax node)
		{
			NameEqualsSyntax nameEqualsSyntax = (NameEqualsSyntax)this.Visit(node.NameEquals);
			NameColonSyntax nameColonSyntax = (NameColonSyntax)this.Visit(node.NameColon);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(nameEqualsSyntax, nameColonSyntax, expressionSyntax);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000ECD9D File Offset: 0x000EAF9D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitNameEquals(NameEqualsSyntax node)
		{
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)this.Visit(node.Name);
			if (identifierNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(identifierNameSyntax, this.VisitToken(node.EqualsToken));
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000ECDD1 File Offset: 0x000EAFD1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypeParameterList(TypeParameterListSyntax node)
		{
			return node.Update(this.VisitToken(node.LessThanToken), this.VisitList<TypeParameterSyntax>(node.Parameters), this.VisitToken(node.GreaterThanToken));
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000ECDFD File Offset: 0x000EAFFD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypeParameter(TypeParameterSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitToken(node.VarianceKeyword), this.VisitToken(node.Identifier));
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000ECE2C File Offset: 0x000EB02C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), this.VisitToken(node.Keyword), this.VisitToken(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), this.VisitToken(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), this.VisitToken(node.CloseBraceToken), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000ECED0 File Offset: 0x000EB0D0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitStructDeclaration(StructDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), this.VisitToken(node.Keyword), this.VisitToken(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), this.VisitToken(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), this.VisitToken(node.CloseBraceToken), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000ECF74 File Offset: 0x000EB174
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), this.VisitToken(node.Keyword), this.VisitToken(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), this.VisitToken(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), this.VisitToken(node.CloseBraceToken), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000ED018 File Offset: 0x000EB218
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRecordDeclaration(RecordDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), this.VisitToken(node.Keyword), this.VisitToken(node.ClassOrStructKeyword), this.VisitToken(node.Identifier), (TypeParameterListSyntax)this.Visit(node.TypeParameterList), (ParameterListSyntax)this.Visit(node.ParameterList), (BaseListSyntax)this.Visit(node.BaseList), this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), this.VisitToken(node.OpenBraceToken), this.VisitList<MemberDeclarationSyntax>(node.Members), this.VisitToken(node.CloseBraceToken), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000ED0D8 File Offset: 0x000EB2D8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEnumDeclaration(EnumDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), this.VisitToken(node.EnumKeyword), this.VisitToken(node.Identifier), (BaseListSyntax)this.Visit(node.BaseList), this.VisitToken(node.OpenBraceToken), this.VisitList<EnumMemberDeclarationSyntax>(node.Members), this.VisitToken(node.CloseBraceToken), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000ED15C File Offset: 0x000EB35C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDelegateDeclaration(DelegateDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.DelegateKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.ReturnType);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("returnType");
			}
			SyntaxToken syntaxToken2 = this.VisitToken(node.Identifier);
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)this.Visit(node.TypeParameterList);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, syntaxToken, typeSyntax, syntaxToken2, typeParameterListSyntax, parameterListSyntax, this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000ED208 File Offset: 0x000EB408
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), this.VisitToken(node.Identifier), (EqualsValueClauseSyntax)this.Visit(node.EqualsValue));
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000ED245 File Offset: 0x000EB445
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBaseList(BaseListSyntax node)
		{
			return node.Update(this.VisitToken(node.ColonToken), this.VisitList<BaseTypeSyntax>(node.Types));
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000ED265 File Offset: 0x000EB465
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSimpleBaseType(SimpleBaseTypeSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(typeSyntax);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000ED290 File Offset: 0x000EB490
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)this.Visit(node.ArgumentList);
			if (argumentListSyntax == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return node.Update(typeSyntax, argumentListSyntax);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000ED2E4 File Offset: 0x000EB4E4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.WhereKeyword);
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)this.Visit(node.Name);
			if (identifierNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxToken, identifierNameSyntax, this.VisitToken(node.ColonToken), this.VisitList<TypeParameterConstraintSyntax>(node.Constraints));
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000ED33B File Offset: 0x000EB53B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConstructorConstraint(ConstructorConstraintSyntax node)
		{
			return node.Update(this.VisitToken(node.NewKeyword), this.VisitToken(node.OpenParenToken), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000ED367 File Offset: 0x000EB567
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitClassOrStructConstraint(ClassOrStructConstraintSyntax node)
		{
			return node.Update(this.VisitToken(node.ClassOrStructKeyword), this.VisitToken(node.QuestionToken));
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000ED387 File Offset: 0x000EB587
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypeConstraint(TypeConstraintSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(typeSyntax);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000ED3AF File Offset: 0x000EB5AF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDefaultConstraint(DefaultConstraintSyntax node)
		{
			return node.Update(this.VisitToken(node.DefaultKeyword));
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000ED3C4 File Offset: 0x000EB5C4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)this.Visit(node.Declaration);
			if (variableDeclarationSyntax == null)
			{
				throw new ArgumentNullException("declaration");
			}
			return node.Update(syntaxList, syntaxTokenList, variableDeclarationSyntax, this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000ED41C File Offset: 0x000EB61C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.EventKeyword);
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)this.Visit(node.Declaration);
			if (variableDeclarationSyntax == null)
			{
				throw new ArgumentNullException("declaration");
			}
			return node.Update(syntaxList, syntaxTokenList, syntaxToken, variableDeclarationSyntax, this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000ED47F File Offset: 0x000EB67F
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
		{
			NameSyntax nameSyntax = (NameSyntax)this.Visit(node.Name);
			if (nameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(nameSyntax, this.VisitToken(node.DotToken));
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x000ED4B4 File Offset: 0x000EB6B4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.ReturnType);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("returnType");
			}
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier);
			SyntaxToken syntaxToken = this.VisitToken(node.Identifier);
			TypeParameterListSyntax typeParameterListSyntax = (TypeParameterListSyntax)this.Visit(node.TypeParameterList);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken, typeParameterListSyntax, parameterListSyntax, this.VisitList<TypeParameterConstraintClauseSyntax>(node.ConstraintClauses), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x000ED588 File Offset: 0x000EB788
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitOperatorDeclaration(OperatorDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.ReturnType);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("returnType");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.OperatorKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OperatorToken);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, typeSyntax, syntaxToken, syntaxToken2, parameterListSyntax, (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x000ED63C File Offset: 0x000EB83C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.ImplicitOrExplicitKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OperatorKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, syntaxToken, syntaxToken2, typeSyntax, parameterListSyntax, (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x000ED6F0 File Offset: 0x000EB8F0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.Identifier);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, syntaxToken, parameterListSyntax, (ConstructorInitializerSyntax)this.Visit(node.Initializer), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000ED786 File Offset: 0x000EB986
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConstructorInitializer(ConstructorInitializerSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.ColonToken);
			SyntaxToken syntaxToken2 = this.VisitToken(node.ThisOrBaseKeyword);
			ArgumentListSyntax argumentListSyntax = (ArgumentListSyntax)this.Visit(node.ArgumentList);
			if (argumentListSyntax == null)
			{
				throw new ArgumentNullException("argumentList");
			}
			return node.Update(syntaxToken, syntaxToken2, argumentListSyntax);
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000ED7C8 File Offset: 0x000EB9C8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDestructorDeclaration(DestructorDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.TildeToken);
			SyntaxToken syntaxToken2 = this.VisitToken(node.Identifier);
			ParameterListSyntax parameterListSyntax = (ParameterListSyntax)this.Visit(node.ParameterList);
			if (parameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, syntaxToken, syntaxToken2, parameterListSyntax, (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000ED85C File Offset: 0x000EBA5C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxList, syntaxTokenList, typeSyntax, (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier), this.VisitToken(node.Identifier), (AccessorListSyntax)this.Visit(node.AccessorList), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), (EqualsValueClauseSyntax)this.Visit(node.Initializer), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x000ED903 File Offset: 0x000EBB03
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitArrowExpressionClause(ArrowExpressionClauseSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.ArrowToken);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Expression);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("expression");
			}
			return node.Update(syntaxToken, expressionSyntax);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x000ED938 File Offset: 0x000EBB38
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEventDeclaration(EventDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			SyntaxToken syntaxToken = this.VisitToken(node.EventKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxList, syntaxTokenList, syntaxToken, typeSyntax, (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier), this.VisitToken(node.Identifier), (AccessorListSyntax)this.Visit(node.AccessorList), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x000ED9CC File Offset: 0x000EBBCC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitIndexerDeclaration(IndexerDeclarationSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifierSyntax = (ExplicitInterfaceSpecifierSyntax)this.Visit(node.ExplicitInterfaceSpecifier);
			SyntaxToken syntaxToken = this.VisitToken(node.ThisKeyword);
			BracketedParameterListSyntax bracketedParameterListSyntax = (BracketedParameterListSyntax)this.Visit(node.ParameterList);
			if (bracketedParameterListSyntax == null)
			{
				throw new ArgumentNullException("parameterList");
			}
			return node.Update(syntaxList, syntaxTokenList, typeSyntax, explicitInterfaceSpecifierSyntax, syntaxToken, bracketedParameterListSyntax, (AccessorListSyntax)this.Visit(node.AccessorList), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000EDA82 File Offset: 0x000EBC82
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAccessorList(AccessorListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBraceToken), this.VisitList<AccessorDeclarationSyntax>(node.Accessors), this.VisitToken(node.CloseBraceToken));
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x000EDAB0 File Offset: 0x000EBCB0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitAccessorDeclaration(AccessorDeclarationSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), this.VisitToken(node.Keyword), (BlockSyntax)this.Visit(node.Body), (ArrowExpressionClauseSyntax)this.Visit(node.ExpressionBody), this.VisitToken(node.SemicolonToken));
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x000EDB15 File Offset: 0x000EBD15
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitParameterList(ParameterListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<ParameterSyntax>(node.Parameters), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x000EDB41 File Offset: 0x000EBD41
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBracketedParameterList(BracketedParameterListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBracketToken), this.VisitList<ParameterSyntax>(node.Parameters), this.VisitToken(node.CloseBracketToken));
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x000EDB70 File Offset: 0x000EBD70
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitParameter(ParameterSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), (TypeSyntax)this.Visit(node.Type), this.VisitToken(node.Identifier), (EqualsValueClauseSyntax)this.Visit(node.Default));
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x000EDBC9 File Offset: 0x000EBDC9
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitFunctionPointerParameter(FunctionPointerParameterSyntax node)
		{
			SyntaxList<AttributeListSyntax> syntaxList = this.VisitList<AttributeListSyntax>(node.AttributeLists);
			SyntaxTokenList syntaxTokenList = this.VisitList(node.Modifiers);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxList, syntaxTokenList, typeSyntax);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x000EDC09 File Offset: 0x000EBE09
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitIncompleteMember(IncompleteMemberSyntax node)
		{
			return node.Update(this.VisitList<AttributeListSyntax>(node.AttributeLists), this.VisitList(node.Modifiers), (TypeSyntax)this.Visit(node.Type));
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x000EDC3A File Offset: 0x000EBE3A
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax node)
		{
			return node.Update(this.VisitList(node.Tokens));
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000EDC4E File Offset: 0x000EBE4E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax node)
		{
			return node.Update(this.VisitList<XmlNodeSyntax>(node.Content), this.VisitToken(node.EndOfComment));
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x000EDC6E File Offset: 0x000EBE6E
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitTypeCref(TypeCrefSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(typeSyntax);
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x000EDC98 File Offset: 0x000EBE98
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitQualifiedCref(QualifiedCrefSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Container);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("container");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.DotToken);
			MemberCrefSyntax memberCrefSyntax = (MemberCrefSyntax)this.Visit(node.Member);
			if (memberCrefSyntax == null)
			{
				throw new ArgumentNullException("member");
			}
			return node.Update(typeSyntax, syntaxToken, memberCrefSyntax);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x000EDCF7 File Offset: 0x000EBEF7
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitNameMemberCref(NameMemberCrefSyntax node)
		{
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Name);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(typeSyntax, (CrefParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x000EDD30 File Offset: 0x000EBF30
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitIndexerMemberCref(IndexerMemberCrefSyntax node)
		{
			return node.Update(this.VisitToken(node.ThisKeyword), (CrefBracketedParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x000EDD55 File Offset: 0x000EBF55
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitOperatorMemberCref(OperatorMemberCrefSyntax node)
		{
			return node.Update(this.VisitToken(node.OperatorKeyword), this.VisitToken(node.OperatorToken), (CrefParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x000EDD88 File Offset: 0x000EBF88
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.ImplicitOrExplicitKeyword);
			SyntaxToken syntaxToken2 = this.VisitToken(node.OperatorKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, syntaxToken2, typeSyntax, (CrefParameterListSyntax)this.Visit(node.Parameters));
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x000EDDE4 File Offset: 0x000EBFE4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCrefParameterList(CrefParameterListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenParenToken), this.VisitList<CrefParameterSyntax>(node.Parameters), this.VisitToken(node.CloseParenToken));
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x000EDE10 File Offset: 0x000EC010
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax node)
		{
			return node.Update(this.VisitToken(node.OpenBracketToken), this.VisitList<CrefParameterSyntax>(node.Parameters), this.VisitToken(node.CloseBracketToken));
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x000EDE3C File Offset: 0x000EC03C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitCrefParameter(CrefParameterSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.RefKindKeyword);
			TypeSyntax typeSyntax = (TypeSyntax)this.Visit(node.Type);
			if (typeSyntax == null)
			{
				throw new ArgumentNullException("type");
			}
			return node.Update(syntaxToken, typeSyntax);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x000EDE70 File Offset: 0x000EC070
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlElement(XmlElementSyntax node)
		{
			XmlElementStartTagSyntax xmlElementStartTagSyntax = (XmlElementStartTagSyntax)this.Visit(node.StartTag);
			if (xmlElementStartTagSyntax == null)
			{
				throw new ArgumentNullException("startTag");
			}
			SyntaxList<XmlNodeSyntax> syntaxList = this.VisitList<XmlNodeSyntax>(node.Content);
			XmlElementEndTagSyntax xmlElementEndTagSyntax = (XmlElementEndTagSyntax)this.Visit(node.EndTag);
			if (xmlElementEndTagSyntax == null)
			{
				throw new ArgumentNullException("endTag");
			}
			return node.Update(xmlElementStartTagSyntax, syntaxList, xmlElementEndTagSyntax);
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x000EDED0 File Offset: 0x000EC0D0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlElementStartTag(XmlElementStartTagSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.LessThanToken);
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)this.Visit(node.Name);
			if (xmlNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxToken, xmlNameSyntax, this.VisitList<XmlAttributeSyntax>(node.Attributes), this.VisitToken(node.GreaterThanToken));
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x000EDF27 File Offset: 0x000EC127
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlElementEndTag(XmlElementEndTagSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.LessThanSlashToken);
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)this.Visit(node.Name);
			if (xmlNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxToken, xmlNameSyntax, this.VisitToken(node.GreaterThanToken));
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x000EDF68 File Offset: 0x000EC168
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlEmptyElement(XmlEmptyElementSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.LessThanToken);
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)this.Visit(node.Name);
			if (xmlNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxToken, xmlNameSyntax, this.VisitList<XmlAttributeSyntax>(node.Attributes), this.VisitToken(node.SlashGreaterThanToken));
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x000EDFBF File Offset: 0x000EC1BF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlName(XmlNameSyntax node)
		{
			return node.Update((XmlPrefixSyntax)this.Visit(node.Prefix), this.VisitToken(node.LocalName));
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x000EDFE4 File Offset: 0x000EC1E4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlPrefix(XmlPrefixSyntax node)
		{
			return node.Update(this.VisitToken(node.Prefix), this.VisitToken(node.ColonToken));
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000EE004 File Offset: 0x000EC204
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlTextAttribute(XmlTextAttributeSyntax node)
		{
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)this.Visit(node.Name);
			if (xmlNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(xmlNameSyntax, this.VisitToken(node.EqualsToken), this.VisitToken(node.StartQuoteToken), this.VisitList(node.TextTokens), this.VisitToken(node.EndQuoteToken));
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000EE068 File Offset: 0x000EC268
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlCrefAttribute(XmlCrefAttributeSyntax node)
		{
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)this.Visit(node.Name);
			if (xmlNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.EqualsToken);
			SyntaxToken syntaxToken2 = this.VisitToken(node.StartQuoteToken);
			CrefSyntax crefSyntax = (CrefSyntax)this.Visit(node.Cref);
			if (crefSyntax == null)
			{
				throw new ArgumentNullException("cref");
			}
			return node.Update(xmlNameSyntax, syntaxToken, syntaxToken2, crefSyntax, this.VisitToken(node.EndQuoteToken));
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000EE0E0 File Offset: 0x000EC2E0
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlNameAttribute(XmlNameAttributeSyntax node)
		{
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)this.Visit(node.Name);
			if (xmlNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			SyntaxToken syntaxToken = this.VisitToken(node.EqualsToken);
			SyntaxToken syntaxToken2 = this.VisitToken(node.StartQuoteToken);
			IdentifierNameSyntax identifierNameSyntax = (IdentifierNameSyntax)this.Visit(node.Identifier);
			if (identifierNameSyntax == null)
			{
				throw new ArgumentNullException("identifier");
			}
			return node.Update(xmlNameSyntax, syntaxToken, syntaxToken2, identifierNameSyntax, this.VisitToken(node.EndQuoteToken));
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000EE157 File Offset: 0x000EC357
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlText(XmlTextSyntax node)
		{
			return node.Update(this.VisitList(node.TextTokens));
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000EE16B File Offset: 0x000EC36B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlCDataSection(XmlCDataSectionSyntax node)
		{
			return node.Update(this.VisitToken(node.StartCDataToken), this.VisitList(node.TextTokens), this.VisitToken(node.EndCDataToken));
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x000EE198 File Offset: 0x000EC398
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.StartProcessingInstructionToken);
			XmlNameSyntax xmlNameSyntax = (XmlNameSyntax)this.Visit(node.Name);
			if (xmlNameSyntax == null)
			{
				throw new ArgumentNullException("name");
			}
			return node.Update(syntaxToken, xmlNameSyntax, this.VisitList(node.TextTokens), this.VisitToken(node.EndProcessingInstructionToken));
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x000EE1EF File Offset: 0x000EC3EF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitXmlComment(XmlCommentSyntax node)
		{
			return node.Update(this.VisitToken(node.LessThanExclamationMinusMinusToken), this.VisitList(node.TextTokens), this.VisitToken(node.MinusMinusGreaterThanToken));
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000EE21C File Offset: 0x000EC41C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.HashToken);
			SyntaxToken syntaxToken2 = this.VisitToken(node.IfKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			return node.Update(syntaxToken, syntaxToken2, expressionSyntax, this.VisitToken(node.EndOfDirectiveToken), node.IsActive, node.BranchTaken, node.ConditionValue);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x000EE288 File Offset: 0x000EC488
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax node)
		{
			SyntaxToken syntaxToken = this.VisitToken(node.HashToken);
			SyntaxToken syntaxToken2 = this.VisitToken(node.ElifKeyword);
			ExpressionSyntax expressionSyntax = (ExpressionSyntax)this.Visit(node.Condition);
			if (expressionSyntax == null)
			{
				throw new ArgumentNullException("condition");
			}
			return node.Update(syntaxToken, syntaxToken2, expressionSyntax, this.VisitToken(node.EndOfDirectiveToken), node.IsActive, node.BranchTaken, node.ConditionValue);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x000EE2F1 File Offset: 0x000EC4F1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.ElseKeyword), this.VisitToken(node.EndOfDirectiveToken), node.IsActive, node.BranchTaken);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000EE329 File Offset: 0x000EC529
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.EndIfKeyword), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x000EE35B File Offset: 0x000EC55B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.RegionKeyword), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000EE38D File Offset: 0x000EC58D
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.EndRegionKeyword), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x000EE3BF File Offset: 0x000EC5BF
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.ErrorKeyword), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x000EE3F1 File Offset: 0x000EC5F1
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.WarningKeyword), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x000EE423 File Offset: 0x000EC623
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.Identifier), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x000EE455 File Offset: 0x000EC655
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.DefineKeyword), this.VisitToken(node.Name), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x000EE493 File Offset: 0x000EC693
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.UndefKeyword), this.VisitToken(node.Name), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x000EE4D4 File Offset: 0x000EC6D4
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.LineKeyword), this.VisitToken(node.Line), this.VisitToken(node.File), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x000EE52C File Offset: 0x000EC72C
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.PragmaKeyword), this.VisitToken(node.WarningKeyword), this.VisitToken(node.DisableOrRestoreKeyword), this.VisitList<ExpressionSyntax>(node.ErrorCodes), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000EE590 File Offset: 0x000EC790
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.PragmaKeyword), this.VisitToken(node.ChecksumKeyword), this.VisitToken(node.File), this.VisitToken(node.Guid), this.VisitToken(node.Bytes), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000EE5FD File Offset: 0x000EC7FD
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.ReferenceKeyword), this.VisitToken(node.File), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000EE63B File Offset: 0x000EC83B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.LoadKeyword), this.VisitToken(node.File), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000EE679 File Offset: 0x000EC879
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.ExclamationToken), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000EE6AC File Offset: 0x000EC8AC
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax node)
		{
			return node.Update(this.VisitToken(node.HashToken), this.VisitToken(node.NullableKeyword), this.VisitToken(node.SettingToken), this.VisitToken(node.TargetToken), this.VisitToken(node.EndOfDirectiveToken), node.IsActive);
		}

		// Token: 0x0400114E RID: 4430
		private readonly bool _visitIntoStructuredTrivia;

		// Token: 0x0400114F RID: 4431
		private int _recursionDepth;
	}
}
