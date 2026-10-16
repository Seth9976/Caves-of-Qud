using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
	// Token: 0x02000283 RID: 643
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class SyntaxNormalizer : CSharpSyntaxRewriter
	{
		// Token: 0x06003CDE RID: 15582 RVA: 0x00106714 File Offset: 0x00104914
		private SyntaxNormalizer(TextSpan consideredSpan, int initialDepth, string indentWhitespace, string eolWhitespace, bool useElasticTrivia)
			: base(true)
		{
			this._consideredSpan = consideredSpan;
			this._initialDepth = initialDepth;
			this._indentWhitespace = indentWhitespace;
			this._useElasticTrivia = useElasticTrivia;
			this._eolTrivia = (useElasticTrivia ? SyntaxFactory.ElasticEndOfLine(eolWhitespace) : SyntaxFactory.EndOfLine(eolWhitespace));
			this._afterLineBreak = true;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x00106768 File Offset: 0x00104968
		internal static TNode Normalize<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(TNode node, string indentWhitespace, string eolWhitespace, bool useElasticTrivia = false) where TNode : SyntaxNode
		{
			SyntaxNormalizer syntaxNormalizer = new SyntaxNormalizer(node.FullSpan, SyntaxNormalizer.GetDeclarationDepth(node), indentWhitespace, eolWhitespace, useElasticTrivia);
			TNode tnode = (TNode)((object)syntaxNormalizer.Visit(node));
			syntaxNormalizer.Free();
			return tnode;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x001067AC File Offset: 0x001049AC
		internal static SyntaxToken Normalize(SyntaxToken token, string indentWhitespace, string eolWhitespace, bool useElasticTrivia = false)
		{
			SyntaxNormalizer syntaxNormalizer = new SyntaxNormalizer(token.FullSpan, SyntaxNormalizer.GetDeclarationDepth(token), indentWhitespace, eolWhitespace, useElasticTrivia);
			SyntaxToken syntaxToken = syntaxNormalizer.VisitToken(token);
			syntaxNormalizer.Free();
			return syntaxToken;
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x001067DC File Offset: 0x001049DC
		internal static SyntaxTriviaList Normalize(SyntaxTriviaList trivia, string indentWhitespace, string eolWhitespace, bool useElasticTrivia = false)
		{
			SyntaxNormalizer syntaxNormalizer = new SyntaxNormalizer(trivia.FullSpan, SyntaxNormalizer.GetDeclarationDepth(trivia.Token), indentWhitespace, eolWhitespace, useElasticTrivia);
			SyntaxTriviaList syntaxTriviaList = syntaxNormalizer.RewriteTrivia(trivia, SyntaxNormalizer.GetDeclarationDepth(trivia.ElementAt(0).Token), false, false, false, 0);
			syntaxNormalizer.Free();
			return syntaxTriviaList;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x0010682B File Offset: 0x00104A2B
		private void Free()
		{
			if (this._indentations != null)
			{
				this._indentations.Free();
			}
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x00106840 File Offset: 0x00104A40
		public override SyntaxToken VisitToken(SyntaxToken token)
		{
			if (token.Kind() == SyntaxKind.None || (token.IsMissing && token.FullWidth == 0))
			{
				return token;
			}
			SyntaxToken syntaxToken2;
			try
			{
				SyntaxToken syntaxToken = token;
				int declarationDepth = SyntaxNormalizer.GetDeclarationDepth(token);
				syntaxToken = syntaxToken.WithLeadingTrivia(this.RewriteTrivia(token.LeadingTrivia, declarationDepth, false, SyntaxNormalizer.NeedsIndentAfterLineBreak(token), false, 0));
				SyntaxToken nextRelevantToken = this.GetNextRelevantToken(token);
				this._afterLineBreak = SyntaxNormalizer.IsLineBreak(token);
				this._afterIndentation = false;
				int num = this.LineBreaksAfter(token, nextRelevantToken);
				bool flag = SyntaxNormalizer.NeedsSeparator(token, nextRelevantToken);
				syntaxToken = syntaxToken.WithTrailingTrivia(this.RewriteTrivia(token.TrailingTrivia, declarationDepth, true, false, flag, num));
				syntaxToken2 = syntaxToken;
			}
			finally
			{
				this._previousToken = token;
			}
			return syntaxToken2;
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x001068FC File Offset: 0x00104AFC
		private SyntaxToken GetNextRelevantToken(SyntaxToken token)
		{
			SyntaxToken nextToken = token.GetNextToken((SyntaxToken t) => SyntaxToken.NonZeroWidth.Invoke(t) || t.Kind() == SyntaxKind.EndOfDirectiveToken, (SyntaxTrivia t) => t.Kind() == SyntaxKind.SkippedTokensTrivia);
			if (this._consideredSpan.Contains(nextToken.FullSpan))
			{
				return nextToken;
			}
			return default(SyntaxToken);
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x00106970 File Offset: 0x00104B70
		private SyntaxTrivia GetIndentation(int count)
		{
			count = Math.Max(count - this._initialDepth, 0);
			int num = count + 1;
			if (this._indentations == null)
			{
				this._indentations = ArrayBuilder<SyntaxTrivia>.GetInstance(num);
			}
			else
			{
				this._indentations.EnsureCapacity(num);
			}
			for (int i = this._indentations.Count; i <= count; i++)
			{
				string text = ((i == 0) ? "" : (this._indentations[i - 1].ToString() + this._indentWhitespace));
				this._indentations.Add(this._useElasticTrivia ? SyntaxFactory.ElasticWhitespace(text) : SyntaxFactory.Whitespace(text));
			}
			return this._indentations[count];
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x00106A28 File Offset: 0x00104C28
		private static bool NeedsIndentAfterLineBreak(SyntaxToken token)
		{
			return !token.IsKind(SyntaxKind.EndOfFileToken);
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x00106A38 File Offset: 0x00104C38
		private int LineBreaksAfter(SyntaxToken currentToken, SyntaxToken nextToken)
		{
			if (this._inSingleLineInterpolation)
			{
				return 0;
			}
			if (currentToken.IsKind(SyntaxKind.EndOfDirectiveToken))
			{
				return 1;
			}
			if (nextToken.Kind() == SyntaxKind.None)
			{
				return 0;
			}
			if (this._isInStructuredTrivia)
			{
				return 0;
			}
			if (nextToken.IsKind(SyntaxKind.CloseBraceToken))
			{
				SyntaxNode parent = currentToken.Parent;
				if (SyntaxNormalizer.IsAccessorListWithoutAccessorsWithBlockBody((parent != null) ? parent.Parent : null))
				{
					return 0;
				}
			}
			SyntaxKind syntaxKind = currentToken.Kind();
			if (syntaxKind <= SyntaxKind.SemicolonToken)
			{
				if (syntaxKind == SyntaxKind.None)
				{
					return 0;
				}
				if (syntaxKind != SyntaxKind.CloseParenToken)
				{
					switch (syntaxKind)
					{
					case SyntaxKind.OpenBraceToken:
						return SyntaxNormalizer.LineBreaksAfterOpenBrace(currentToken, nextToken);
					case SyntaxKind.CloseBraceToken:
						return SyntaxNormalizer.LineBreaksAfterCloseBrace(currentToken, nextToken);
					case SyntaxKind.CloseBracketToken:
						if (currentToken.Parent is AttributeListSyntax && !(currentToken.Parent.Parent is ParameterSyntax))
						{
							return 1;
						}
						break;
					case SyntaxKind.ColonToken:
						if (currentToken.Parent is LabeledStatementSyntax || currentToken.Parent is SwitchLabelSyntax)
						{
							return 1;
						}
						break;
					case SyntaxKind.SemicolonToken:
						return SyntaxNormalizer.LineBreaksAfterSemicolon(currentToken, nextToken);
					}
				}
				else
				{
					if ((!(currentToken.Parent is StatementSyntax) || nextToken.Parent == currentToken.Parent) && nextToken.Kind() != SyntaxKind.OpenBraceToken && nextToken.Kind() != SyntaxKind.WhereKeyword)
					{
						return 0;
					}
					return 1;
				}
			}
			else if (syntaxKind != SyntaxKind.CommaToken)
			{
				if (syntaxKind != SyntaxKind.ElseKeyword)
				{
					if (syntaxKind == SyntaxKind.FinallyKeyword)
					{
						return 1;
					}
				}
				else
				{
					if (nextToken.Kind() == SyntaxKind.IfKeyword)
					{
						return 0;
					}
					return 1;
				}
			}
			else
			{
				if (!(currentToken.Parent is EnumDeclarationSyntax))
				{
					return 0;
				}
				return 1;
			}
			if ((nextToken.IsKind(SyntaxKind.FromKeyword) && nextToken.Parent.IsKind(SyntaxKind.FromClause)) || (nextToken.IsKind(SyntaxKind.LetKeyword) && nextToken.Parent.IsKind(SyntaxKind.LetClause)) || (nextToken.IsKind(SyntaxKind.WhereKeyword) && nextToken.Parent.IsKind(SyntaxKind.WhereClause)) || (nextToken.IsKind(SyntaxKind.JoinKeyword) && nextToken.Parent.IsKind(SyntaxKind.JoinClause)) || (nextToken.IsKind(SyntaxKind.JoinKeyword) && nextToken.Parent.IsKind(SyntaxKind.JoinIntoClause)) || (nextToken.IsKind(SyntaxKind.OrderByKeyword) && nextToken.Parent.IsKind(SyntaxKind.OrderByClause)) || (nextToken.IsKind(SyntaxKind.SelectKeyword) && nextToken.Parent.IsKind(SyntaxKind.SelectClause)) || (nextToken.IsKind(SyntaxKind.GroupKeyword) && nextToken.Parent.IsKind(SyntaxKind.GroupClause)))
			{
				return 1;
			}
			syntaxKind = nextToken.Kind();
			if (syntaxKind <= SyntaxKind.ElseKeyword)
			{
				switch (syntaxKind)
				{
				case SyntaxKind.OpenBraceToken:
					return SyntaxNormalizer.LineBreaksBeforeOpenBrace(nextToken);
				case SyntaxKind.CloseBraceToken:
					return SyntaxNormalizer.LineBreaksBeforeCloseBrace(nextToken);
				case SyntaxKind.OpenBracketToken:
					if (!(nextToken.Parent is AttributeListSyntax) || nextToken.Parent.Parent is ParameterSyntax)
					{
						return 0;
					}
					return 1;
				default:
					if (syntaxKind != SyntaxKind.ElseKeyword)
					{
						return 0;
					}
					break;
				}
			}
			else if (syntaxKind != SyntaxKind.FinallyKeyword)
			{
				if (syntaxKind != SyntaxKind.WhereKeyword)
				{
					return 0;
				}
				if (!(currentToken.Parent is TypeParameterListSyntax))
				{
					return 0;
				}
				return 1;
			}
			return 1;
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x00106D60 File Offset: 0x00104F60
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool IsAccessorListWithoutAccessorsWithBlockBody(SyntaxNode node)
		{
			AccessorListSyntax accessorListSyntax = node as AccessorListSyntax;
			if (accessorListSyntax != null)
			{
				return accessorListSyntax.Accessors.All((AccessorDeclarationSyntax a) => a.Body == null);
			}
			return false;
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x00106DA8 File Offset: 0x00104FA8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static bool IsAccessorListFollowedByInitializer([NotNullWhen(true)] SyntaxNode node)
		{
			if (node is AccessorListSyntax)
			{
				PropertyDeclarationSyntax propertyDeclarationSyntax = node.Parent as PropertyDeclarationSyntax;
				if (propertyDeclarationSyntax != null)
				{
					return propertyDeclarationSyntax.Initializer != null;
				}
			}
			return false;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00106DD7 File Offset: 0x00104FD7
		private static int LineBreaksBeforeOpenBrace(SyntaxToken openBraceToken)
		{
			if (openBraceToken.Parent.IsKind(SyntaxKind.Interpolation) || openBraceToken.Parent is InitializerExpressionSyntax || SyntaxNormalizer.IsAccessorListWithoutAccessorsWithBlockBody(openBraceToken.Parent))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x00106E0B File Offset: 0x0010500B
		private static int LineBreaksBeforeCloseBrace(SyntaxToken closeBraceToken)
		{
			if (closeBraceToken.Parent.IsKind(SyntaxKind.Interpolation) || closeBraceToken.Parent is InitializerExpressionSyntax)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x00106E31 File Offset: 0x00105031
		private static int LineBreaksAfterOpenBrace(SyntaxToken currentToken, SyntaxToken nextToken)
		{
			if (currentToken.Parent is InitializerExpressionSyntax || currentToken.Parent.IsKind(SyntaxKind.Interpolation) || SyntaxNormalizer.IsAccessorListWithoutAccessorsWithBlockBody(currentToken.Parent))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x00106E68 File Offset: 0x00105068
		private static int LineBreaksAfterCloseBrace(SyntaxToken currentToken, SyntaxToken nextToken)
		{
			if (!(currentToken.Parent is InitializerExpressionSyntax) && !currentToken.Parent.IsKind(SyntaxKind.Interpolation))
			{
				SyntaxNode parent = currentToken.Parent;
				if (!(((parent != null) ? parent.Parent : null) is AnonymousFunctionExpressionSyntax) && !SyntaxNormalizer.IsAccessorListFollowedByInitializer(currentToken.Parent))
				{
					SyntaxKind syntaxKind = nextToken.Kind();
					if (syntaxKind <= SyntaxKind.ElseKeyword)
					{
						if (syntaxKind != SyntaxKind.CloseBraceToken && syntaxKind != SyntaxKind.ElseKeyword)
						{
							goto IL_0080;
						}
					}
					else if (syntaxKind - SyntaxKind.CatchKeyword > 1 && syntaxKind != SyntaxKind.EndOfFileToken)
					{
						goto IL_0080;
					}
					return 1;
					IL_0080:
					if (syntaxKind == SyntaxKind.WhileKeyword && nextToken.Parent.IsKind(SyntaxKind.DoStatement))
					{
						return 1;
					}
					return 2;
				}
			}
			return 0;
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x00106F14 File Offset: 0x00105114
		private static int LineBreaksAfterSemicolon(SyntaxToken currentToken, SyntaxToken nextToken)
		{
			if (currentToken.Parent.IsKind(SyntaxKind.ForStatement))
			{
				return 0;
			}
			if (nextToken.Kind() == SyntaxKind.CloseBraceToken)
			{
				return 1;
			}
			if (currentToken.Parent.IsKind(SyntaxKind.UsingDirective))
			{
				if (!nextToken.Parent.IsKind(SyntaxKind.UsingDirective))
				{
					return 2;
				}
				return 1;
			}
			else if (currentToken.Parent.IsKind(SyntaxKind.ExternAliasDirective))
			{
				if (!nextToken.Parent.IsKind(SyntaxKind.ExternAliasDirective))
				{
					return 2;
				}
				return 1;
			}
			else
			{
				if (currentToken.Parent is AccessorDeclarationSyntax && SyntaxNormalizer.IsAccessorListWithoutAccessorsWithBlockBody(currentToken.Parent.Parent))
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00106FC0 File Offset: 0x001051C0
		private static bool NeedsSeparator(SyntaxToken token, SyntaxToken next)
		{
			if (token.Parent == null || next.Parent == null)
			{
				return false;
			}
			if (SyntaxNormalizer.IsAccessorListWithoutAccessorsWithBlockBody(next.Parent) || SyntaxNormalizer.IsAccessorListWithoutAccessorsWithBlockBody(next.Parent.Parent))
			{
				return !next.IsKind(SyntaxKind.SemicolonToken);
			}
			if (SyntaxNormalizer.IsXmlTextToken(token.Kind()) || SyntaxNormalizer.IsXmlTextToken(next.Kind()))
			{
				return false;
			}
			if (next.Kind() == SyntaxKind.EndOfDirectiveToken)
			{
				return SyntaxNormalizer.IsKeyword(token.Kind()) && next.LeadingWidth > 0;
			}
			if ((token.Parent is AssignmentExpressionSyntax && SyntaxNormalizer.AssignmentTokenNeedsSeparator(token.Kind())) || (next.Parent is AssignmentExpressionSyntax && SyntaxNormalizer.AssignmentTokenNeedsSeparator(next.Kind())) || (token.Parent is BinaryExpressionSyntax && SyntaxNormalizer.BinaryTokenNeedsSeparator(token.Kind())) || (next.Parent is BinaryExpressionSyntax && SyntaxNormalizer.BinaryTokenNeedsSeparator(next.Kind())))
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.GreaterThanToken) && token.Parent.IsKind(SyntaxKind.TypeArgumentList) && !SyntaxFacts.IsPunctuation(next.Kind()))
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.GreaterThanToken) && token.Parent.IsKind(SyntaxKind.FunctionPointerParameterList))
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.CommaToken) && !next.IsKind(SyntaxKind.CommaToken) && !token.Parent.IsKind(SyntaxKind.EnumDeclaration))
			{
				return true;
			}
			if (token.Kind() == SyntaxKind.SemicolonToken && next.Kind() != SyntaxKind.SemicolonToken && next.Kind() != SyntaxKind.CloseParenToken)
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.QuestionToken) && (token.Parent.IsKind(SyntaxKind.ConditionalExpression) || token.Parent is TypeSyntax) && !token.Parent.Parent.IsKind(SyntaxKind.TypeArgumentList))
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.ColonToken))
			{
				return !token.Parent.IsKind(SyntaxKind.InterpolationFormatClause) && !token.Parent.IsKind(SyntaxKind.XmlPrefix);
			}
			if (next.IsKind(SyntaxKind.ColonToken) && (next.Parent.IsKind(SyntaxKind.BaseList) || next.Parent.IsKind(SyntaxKind.TypeParameterConstraintClause)))
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.CloseBracketToken) && SyntaxNormalizer.IsWord(next.Kind()))
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.CloseParenToken) && SyntaxNormalizer.IsWord(next.Kind()) && token.Parent.IsKind(SyntaxKind.TupleType))
			{
				return true;
			}
			if ((next.IsKind(SyntaxKind.QuestionToken) || next.IsKind(SyntaxKind.ColonToken)) && next.Parent.IsKind(SyntaxKind.ConditionalExpression))
			{
				return true;
			}
			if (token.IsKind(SyntaxKind.EqualsToken))
			{
				return !token.Parent.IsKind(SyntaxKind.XmlTextAttribute);
			}
			if (next.IsKind(SyntaxKind.EqualsToken))
			{
				return !next.Parent.IsKind(SyntaxKind.XmlTextAttribute);
			}
			if (token.Parent.IsKind(SyntaxKind.FunctionPointerType))
			{
				if (next.IsKind(SyntaxKind.AsteriskToken) && token.IsKind(SyntaxKind.DelegateKeyword))
				{
					return false;
				}
				if (token.IsKind(SyntaxKind.AsteriskToken) && next.Parent.IsKind(SyntaxKind.FunctionPointerCallingConvention))
				{
					SyntaxKind syntaxKind = next.Kind();
					if (syntaxKind - SyntaxKind.ManagedKeyword <= 1 || syntaxKind == SyntaxKind.IdentifierToken)
					{
						return true;
					}
				}
			}
			if (next.Parent.IsKind(SyntaxKind.FunctionPointerParameterList) && next.IsKind(SyntaxKind.LessThanToken))
			{
				SyntaxKind syntaxKind = token.Kind();
				if (syntaxKind != SyntaxKind.AsteriskToken)
				{
					if (syntaxKind != SyntaxKind.CloseBracketToken)
					{
						if (syntaxKind - SyntaxKind.ManagedKeyword > 1)
						{
							goto IL_03D2;
						}
					}
					else if (!token.Parent.IsKind(SyntaxKind.FunctionPointerUnmanagedCallingConventionList))
					{
						goto IL_03D2;
					}
				}
				return false;
			}
			IL_03D2:
			if (token.Parent.IsKind(SyntaxKind.FunctionPointerCallingConvention) && next.Parent.IsKind(SyntaxKind.FunctionPointerUnmanagedCallingConventionList) && next.IsKind(SyntaxKind.OpenBracketToken))
			{
				return false;
			}
			if (next.Parent.IsKind(SyntaxKind.FunctionPointerUnmanagedCallingConventionList) && token.Parent.IsKind(SyntaxKind.FunctionPointerUnmanagedCallingConventionList))
			{
				if (next.IsKind(SyntaxKind.IdentifierToken))
				{
					if (token.IsKind(SyntaxKind.OpenBracketToken))
					{
						return false;
					}
					if (token.IsKind(SyntaxKind.CommaToken))
					{
						return true;
					}
				}
				if (next.IsKind(SyntaxKind.CommaToken))
				{
					return false;
				}
				if (next.IsKind(SyntaxKind.CloseBracketToken))
				{
					return false;
				}
			}
			if (token.IsKind(SyntaxKind.LessThanToken) && token.Parent.IsKind(SyntaxKind.FunctionPointerParameterList))
			{
				return false;
			}
			if (next.IsKind(SyntaxKind.GreaterThanToken) && next.Parent.IsKind(SyntaxKind.FunctionPointerParameterList))
			{
				return false;
			}
			if (token.IsKind(SyntaxKind.EqualsGreaterThanToken) || next.IsKind(SyntaxKind.EqualsGreaterThanToken))
			{
				return true;
			}
			if (SyntaxFacts.IsLiteral(token.Kind()) && SyntaxFacts.IsLiteral(next.Kind()))
			{
				return true;
			}
			if (next.IsKind(SyntaxKind.AsteriskToken) && next.Parent is PointerTypeSyntax)
			{
				return false;
			}
			if (token.IsKind(SyntaxKind.AsteriskToken) && token.Parent is PointerTypeSyntax && (next.IsKind(SyntaxKind.IdentifierToken) || next.Parent.IsKind(SyntaxKind.IndexerDeclaration)))
			{
				return true;
			}
			if (SyntaxNormalizer.IsKeyword(token.Kind()) && !next.IsKind(SyntaxKind.ColonToken) && !next.IsKind(SyntaxKind.DotToken) && !next.IsKind(SyntaxKind.QuestionToken) && !next.IsKind(SyntaxKind.SemicolonToken) && !next.IsKind(SyntaxKind.OpenBracketToken) && (!next.IsKind(SyntaxKind.OpenParenToken) || SyntaxNormalizer.KeywordNeedsSeparatorBeforeOpenParen(token.Kind()) || next.Parent.IsKind(SyntaxKind.TupleType)) && !next.IsKind(SyntaxKind.CloseParenToken) && !next.IsKind(SyntaxKind.CloseBraceToken) && !next.IsKind(SyntaxKind.ColonColonToken) && !next.IsKind(SyntaxKind.GreaterThanToken) && !next.IsKind(SyntaxKind.CommaToken))
			{
				return true;
			}
			if (SyntaxNormalizer.IsWord(token.Kind()) && SyntaxNormalizer.IsWord(next.Kind()))
			{
				return true;
			}
			if (token.Width > 1 && next.Width > 1)
			{
				char c = token.Text.Last();
				char c2 = next.Text.First();
				if (c == c2 && SyntaxNormalizer.TokenCharacterCanBeDoubled(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x0010763C File Offset: 0x0010583C
		private static bool KeywordNeedsSeparatorBeforeOpenParen(SyntaxKind kind)
		{
			if (kind <= SyntaxKind.NewKeyword)
			{
				if (kind - SyntaxKind.TypeOfKeyword > 1 && kind != SyntaxKind.DefaultKeyword && kind != SyntaxKind.NewKeyword)
				{
					return true;
				}
			}
			else if (kind != SyntaxKind.ArgListKeyword && kind - SyntaxKind.ThisKeyword > 1 && kind - SyntaxKind.CheckedKeyword > 1)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x0010768C File Offset: 0x0010588C
		private static bool IsXmlTextToken(SyntaxKind kind)
		{
			return kind - SyntaxKind.XmlTextLiteralToken <= 1;
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x0010769B File Offset: 0x0010589B
		private static bool BinaryTokenNeedsSeparator(SyntaxKind kind)
		{
			return kind != SyntaxKind.DotToken && kind != SyntaxKind.MinusGreaterThanToken && SyntaxFacts.GetBinaryExpression(kind) > SyntaxKind.None;
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x001076B8 File Offset: 0x001058B8
		private static bool AssignmentTokenNeedsSeparator(SyntaxKind kind)
		{
			return SyntaxFacts.GetAssignmentExpression(kind) > SyntaxKind.None;
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x001076C4 File Offset: 0x001058C4
		private SyntaxTriviaList RewriteTrivia(SyntaxTriviaList triviaList, int depth, bool isTrailing, bool indentAfterLineBreak, bool mustHaveSeparator, int lineBreaksAfter)
		{
			ArrayBuilder<SyntaxTrivia> instance = ArrayBuilder<SyntaxTrivia>.GetInstance(triviaList.Count);
			SyntaxTriviaList syntaxTriviaList;
			try
			{
				foreach (SyntaxTrivia syntaxTrivia in triviaList)
				{
					if (!syntaxTrivia.IsKind(SyntaxKind.WhitespaceTrivia) && !syntaxTrivia.IsKind(SyntaxKind.EndOfLineTrivia) && syntaxTrivia.FullWidth != 0)
					{
						bool flag = (instance.Count > 0 && SyntaxNormalizer.NeedsSeparatorBetween(instance.Last())) || (instance.Count == 0 && isTrailing);
						if ((SyntaxNormalizer.NeedsLineBreakBefore(syntaxTrivia, isTrailing) || (instance.Count > 0 && SyntaxNormalizer.NeedsLineBreakBetween(instance.Last(), syntaxTrivia, isTrailing))) && !this._afterLineBreak)
						{
							instance.Add(this.GetEndOfLine());
							this._afterLineBreak = true;
							this._afterIndentation = false;
						}
						if (this._afterLineBreak)
						{
							if (!this._afterIndentation && SyntaxNormalizer.NeedsIndentAfterLineBreak(syntaxTrivia))
							{
								instance.Add(this.GetIndentation(SyntaxNormalizer.GetDeclarationDepth(syntaxTrivia)));
								this._afterIndentation = true;
							}
						}
						else if (flag)
						{
							instance.Add(this.GetSpace());
							this._afterLineBreak = false;
							this._afterIndentation = false;
						}
						if (syntaxTrivia.HasStructure)
						{
							SyntaxTrivia syntaxTrivia2 = this.VisitStructuredTrivia(syntaxTrivia);
							instance.Add(syntaxTrivia2);
						}
						else if (syntaxTrivia.IsKind(SyntaxKind.DocumentationCommentExteriorTrivia))
						{
							instance.Add(SyntaxNormalizer.s_trimmedDocCommentExterior);
						}
						else
						{
							instance.Add(syntaxTrivia);
						}
						if (SyntaxNormalizer.NeedsLineBreakAfter(syntaxTrivia, isTrailing) && (instance.Count == 0 || !SyntaxNormalizer.EndsInLineBreak(instance.Last())))
						{
							instance.Add(this.GetEndOfLine());
							this._afterLineBreak = true;
							this._afterIndentation = false;
						}
					}
				}
				if (lineBreaksAfter > 0)
				{
					if (instance.Count > 0 && SyntaxNormalizer.EndsInLineBreak(instance.Last()))
					{
						lineBreaksAfter--;
					}
					for (int i = 0; i < lineBreaksAfter; i++)
					{
						instance.Add(this.GetEndOfLine());
						this._afterLineBreak = true;
						this._afterIndentation = false;
					}
				}
				else if (indentAfterLineBreak && this._afterLineBreak && !this._afterIndentation)
				{
					instance.Add(this.GetIndentation(depth));
					this._afterIndentation = true;
				}
				else if (mustHaveSeparator)
				{
					instance.Add(this.GetSpace());
					this._afterLineBreak = false;
					this._afterIndentation = false;
				}
				if (instance.Count == 0)
				{
					syntaxTriviaList = default(SyntaxTriviaList);
					syntaxTriviaList = syntaxTriviaList;
				}
				else if (instance.Count == 1)
				{
					syntaxTriviaList = SyntaxFactory.TriviaList(instance.First());
				}
				else
				{
					syntaxTriviaList = SyntaxFactory.TriviaList(instance);
				}
			}
			finally
			{
				instance.Free();
			}
			return syntaxTriviaList;
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x00107954 File Offset: 0x00105B54
		private SyntaxTrivia GetSpace()
		{
			if (!this._useElasticTrivia)
			{
				return SyntaxFactory.Space;
			}
			return SyntaxFactory.ElasticSpace;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x00107969 File Offset: 0x00105B69
		private SyntaxTrivia GetEndOfLine()
		{
			return this._eolTrivia;
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x00107974 File Offset: 0x00105B74
		private SyntaxTrivia VisitStructuredTrivia(SyntaxTrivia trivia)
		{
			bool isInStructuredTrivia = this._isInStructuredTrivia;
			this._isInStructuredTrivia = true;
			SyntaxToken previousToken = this._previousToken;
			this._previousToken = default(SyntaxToken);
			SyntaxTrivia syntaxTrivia = this.VisitTrivia(trivia);
			this._isInStructuredTrivia = isInStructuredTrivia;
			this._previousToken = previousToken;
			return syntaxTrivia;
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x001079B8 File Offset: 0x00105BB8
		private static bool NeedsSeparatorBetween(SyntaxTrivia trivia)
		{
			SyntaxKind syntaxKind = trivia.Kind();
			return syntaxKind != SyntaxKind.None && syntaxKind != SyntaxKind.WhitespaceTrivia && syntaxKind != SyntaxKind.DocumentationCommentExteriorTrivia && !SyntaxFacts.IsPreprocessorDirective(trivia.Kind());
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x001079EF File Offset: 0x00105BEF
		private static bool NeedsLineBreakBetween(SyntaxTrivia trivia, SyntaxTrivia next, bool isTrailingTrivia)
		{
			return SyntaxNormalizer.NeedsLineBreakAfter(trivia, isTrailingTrivia) || SyntaxNormalizer.NeedsLineBreakBefore(next, isTrailingTrivia);
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x00107A04 File Offset: 0x00105C04
		private static bool NeedsLineBreakBefore(SyntaxTrivia trivia, bool isTrailingTrivia)
		{
			SyntaxKind syntaxKind = trivia.Kind();
			if (syntaxKind == SyntaxKind.DocumentationCommentExteriorTrivia)
			{
				return !isTrailingTrivia;
			}
			return SyntaxFacts.IsPreprocessorDirective(syntaxKind);
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x00107A2C File Offset: 0x00105C2C
		private static bool NeedsLineBreakAfter(SyntaxTrivia trivia, bool isTrailingTrivia)
		{
			SyntaxKind syntaxKind = trivia.Kind();
			if (syntaxKind == SyntaxKind.SingleLineCommentTrivia)
			{
				return true;
			}
			if (syntaxKind != SyntaxKind.MultiLineCommentTrivia)
			{
				return SyntaxFacts.IsPreprocessorDirective(syntaxKind);
			}
			return !isTrailingTrivia;
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x00107A60 File Offset: 0x00105C60
		private static bool NeedsIndentAfterLineBreak(SyntaxTrivia trivia)
		{
			SyntaxKind syntaxKind = trivia.Kind();
			return syntaxKind - SyntaxKind.SingleLineCommentTrivia <= 4;
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x00107A81 File Offset: 0x00105C81
		private static bool IsLineBreak(SyntaxToken token)
		{
			return token.Kind() == SyntaxKind.XmlTextLiteralNewLineToken;
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x00107A90 File Offset: 0x00105C90
		private static bool EndsInLineBreak(SyntaxTrivia trivia)
		{
			if (trivia.Kind() == SyntaxKind.EndOfLineTrivia)
			{
				return true;
			}
			if (trivia.Kind() == SyntaxKind.PreprocessingMessageTrivia || trivia.Kind() == SyntaxKind.DisabledTextTrivia)
			{
				string text = trivia.ToFullString();
				return text.Length > 0 && SyntaxFacts.IsNewLine(text.Last());
			}
			if (!trivia.HasStructure)
			{
				return false;
			}
			SyntaxNode structure = trivia.GetStructure();
			SyntaxTriviaList trailingTrivia = structure.GetTrailingTrivia();
			if (trailingTrivia.Count > 0)
			{
				return SyntaxNormalizer.EndsInLineBreak(trailingTrivia.Last());
			}
			return SyntaxNormalizer.IsLineBreak(structure.GetLastToken(false, false, false, false));
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x00107B25 File Offset: 0x00105D25
		private static bool IsWord(SyntaxKind kind)
		{
			return kind == SyntaxKind.IdentifierToken || SyntaxNormalizer.IsKeyword(kind);
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x00107B37 File Offset: 0x00105D37
		private static bool IsKeyword(SyntaxKind kind)
		{
			return SyntaxFacts.IsKeywordKind(kind) || SyntaxFacts.IsPreprocessorKeyword(kind);
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x00107B49 File Offset: 0x00105D49
		private static bool TokenCharacterCanBeDoubled(char c)
		{
			if (c <= '+')
			{
				if (c != '"' && c != '+')
				{
					return false;
				}
			}
			else if (c != '-')
			{
				switch (c)
				{
				case ':':
				case '<':
				case '=':
				case '?':
					break;
				case ';':
				case '>':
					return false;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x00107B87 File Offset: 0x00105D87
		private static int GetDeclarationDepth(SyntaxToken token)
		{
			return SyntaxNormalizer.GetDeclarationDepth(token.Parent);
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x00107B95 File Offset: 0x00105D95
		private static int GetDeclarationDepth(SyntaxTrivia trivia)
		{
			if (SyntaxFacts.IsPreprocessorDirective(trivia.Kind()))
			{
				return 0;
			}
			return SyntaxNormalizer.GetDeclarationDepth(trivia.Token);
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x00107BB4 File Offset: 0x00105DB4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private static int GetDeclarationDepth(SyntaxNode node)
		{
			if (node != null)
			{
				if (node.IsStructuredTrivia)
				{
					return SyntaxNormalizer.GetDeclarationDepth(((StructuredTriviaSyntax)node).ParentTrivia);
				}
				if (node.Parent != null)
				{
					if (node.Parent.IsKind(SyntaxKind.CompilationUnit))
					{
						return 0;
					}
					int declarationDepth = SyntaxNormalizer.GetDeclarationDepth(node.Parent);
					if (node.Parent.IsKind(SyntaxKind.GlobalStatement))
					{
						return declarationDepth;
					}
					if (node.IsKind(SyntaxKind.IfStatement) && node.Parent.IsKind(SyntaxKind.ElseClause))
					{
						return declarationDepth;
					}
					if (node.Parent is BlockSyntax || (node is StatementSyntax && !(node is BlockSyntax)))
					{
						return declarationDepth + 1;
					}
					if (node is MemberDeclarationSyntax || node is AccessorDeclarationSyntax || node is TypeParameterConstraintClauseSyntax || node is SwitchSectionSyntax || node is UsingDirectiveSyntax || node is ExternAliasDirectiveSyntax || node is QueryExpressionSyntax || node is QueryContinuationSyntax)
					{
						return declarationDepth + 1;
					}
					return declarationDepth;
				}
			}
			return 0;
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x00107CA8 File Offset: 0x00105EA8
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override SyntaxNode VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax node)
		{
			if (node.StringStartToken.Kind() == SyntaxKind.InterpolatedStringStartToken)
			{
				bool inSingleLineInterpolation = this._inSingleLineInterpolation;
				this._inSingleLineInterpolation = true;
				try
				{
					return base.VisitInterpolatedStringExpression(node);
				}
				finally
				{
					this._inSingleLineInterpolation = inSingleLineInterpolation;
				}
			}
			return base.VisitInterpolatedStringExpression(node);
		}

		// Token: 0x04001428 RID: 5160
		private readonly TextSpan _consideredSpan;

		// Token: 0x04001429 RID: 5161
		private readonly int _initialDepth;

		// Token: 0x0400142A RID: 5162
		private readonly string _indentWhitespace;

		// Token: 0x0400142B RID: 5163
		private readonly bool _useElasticTrivia;

		// Token: 0x0400142C RID: 5164
		private readonly SyntaxTrivia _eolTrivia;

		// Token: 0x0400142D RID: 5165
		private bool _isInStructuredTrivia;

		// Token: 0x0400142E RID: 5166
		private SyntaxToken _previousToken;

		// Token: 0x0400142F RID: 5167
		private bool _afterLineBreak;

		// Token: 0x04001430 RID: 5168
		private bool _afterIndentation;

		// Token: 0x04001431 RID: 5169
		private bool _inSingleLineInterpolation;

		// Token: 0x04001432 RID: 5170
		[global::System.Runtime.CompilerServices.Nullable(2)]
		private ArrayBuilder<SyntaxTrivia> _indentations;

		// Token: 0x04001433 RID: 5171
		private static readonly SyntaxTrivia s_trimmedDocCommentExterior = SyntaxFactory.DocumentationCommentExterior("///");
	}
}
