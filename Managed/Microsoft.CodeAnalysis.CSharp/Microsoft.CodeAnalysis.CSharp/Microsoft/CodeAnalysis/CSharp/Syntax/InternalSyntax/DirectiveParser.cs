using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200035A RID: 858
	internal class DirectiveParser : SyntaxParser
	{
		// Token: 0x06004831 RID: 18481 RVA: 0x0011B74C File Offset: 0x0011994C
		internal DirectiveParser(Lexer lexer, DirectiveStack context)
			: base(lexer, LexerMode.Directive, null, null, false, false, default(CancellationToken))
		{
			this._context = context;
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x0011B778 File Offset: 0x00119978
		public CSharpSyntaxNode ParseDirective(bool isActive, bool endIsActive, bool isAfterFirstTokenInFile, bool isAfterNonWhitespaceOnLine)
		{
			int position = this.lexer.TextWindow.Position;
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.HashToken, false);
			if (isAfterNonWhitespaceOnLine)
			{
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_BadDirectivePlacement);
			}
			SyntaxKind contextualKind = base.CurrentToken.ContextualKind;
			CSharpSyntaxNode csharpSyntaxNode;
			if (contextualKind != SyntaxKind.IfKeyword)
			{
				if (contextualKind != SyntaxKind.ElseKeyword)
				{
					switch (contextualKind)
					{
					case SyntaxKind.ElifKeyword:
						return this.ParseElifDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive, endIsActive);
					case SyntaxKind.EndIfKeyword:
						return this.ParseEndIfDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive, endIsActive);
					case SyntaxKind.RegionKeyword:
						return this.ParseRegionDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive);
					case SyntaxKind.EndRegionKeyword:
						return this.ParseEndRegionDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive);
					case SyntaxKind.DefineKeyword:
					case SyntaxKind.UndefKeyword:
						return this.ParseDefineOrUndefDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive, isAfterFirstTokenInFile && !isAfterNonWhitespaceOnLine);
					case SyntaxKind.WarningKeyword:
					case SyntaxKind.ErrorKeyword:
						return this.ParseErrorOrWarningDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive);
					case SyntaxKind.LineKeyword:
						return this.ParseLineDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive);
					case SyntaxKind.PragmaKeyword:
						return this.ParsePragmaDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive);
					case SyntaxKind.ReferenceKeyword:
						return this.ParseReferenceDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive, isAfterFirstTokenInFile && !isAfterNonWhitespaceOnLine);
					case SyntaxKind.LoadKeyword:
						return this.ParseLoadDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive, isAfterFirstTokenInFile && !isAfterNonWhitespaceOnLine);
					case SyntaxKind.NullableKeyword:
						return this.ParseNullableDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive);
					}
					if (this.lexer.Options.Kind == SourceCodeKind.Script && contextualKind == SyntaxKind.ExclamationToken && position == 0 && !syntaxToken.HasTrailingTrivia)
					{
						csharpSyntaxNode = this.ParseShebangDirective(syntaxToken, base.EatToken(SyntaxKind.ExclamationToken), isActive);
					}
					else
					{
						SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.IdentifierToken, false);
						SyntaxToken syntaxToken3 = this.ParseEndOfDirective(true, false, false);
						if (!isAfterNonWhitespaceOnLine)
						{
							if (!syntaxToken2.IsMissing)
							{
								syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_PPDirectiveExpected);
							}
							else
							{
								syntaxToken = base.AddError<SyntaxToken>(syntaxToken, ErrorCode.ERR_PPDirectiveExpected);
							}
						}
						csharpSyntaxNode = SyntaxFactory.BadDirectiveTrivia(syntaxToken, syntaxToken2, syntaxToken3, isActive);
					}
				}
				else
				{
					csharpSyntaxNode = this.ParseElseDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive, endIsActive);
				}
			}
			else
			{
				csharpSyntaxNode = this.ParseIfDirective(syntaxToken, base.EatContextualToken(contextualKind, true), isActive);
			}
			return csharpSyntaxNode;
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x0011BA04 File Offset: 0x00119C04
		private DirectiveTriviaSyntax ParseIfDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive)
		{
			ExpressionSyntax expressionSyntax = this.ParseExpression();
			SyntaxToken syntaxToken = this.ParseEndOfDirective(false, false, false);
			bool flag = this.EvaluateBool(expressionSyntax);
			bool flag2 = isActive && flag;
			return SyntaxFactory.IfDirectiveTrivia(hash, keyword, expressionSyntax, syntaxToken, isActive, flag2, flag);
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x0011BA3C File Offset: 0x00119C3C
		private DirectiveTriviaSyntax ParseElifDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive, bool endIsActive)
		{
			ExpressionSyntax expressionSyntax = this.ParseExpression();
			SyntaxToken syntaxToken = this.ParseEndOfDirective(false, false, false);
			if (this._context.HasPreviousIfOrElif())
			{
				bool flag = this.EvaluateBool(expressionSyntax);
				bool flag2 = endIsActive && flag && !this._context.PreviousBranchTaken();
				return SyntaxFactory.ElifDirectiveTrivia(hash, keyword, expressionSyntax, syntaxToken, endIsActive, flag2, flag);
			}
			syntaxToken = syntaxToken.TokenWithLeadingTrivia(SyntaxList.Concat(SyntaxFactory.DisabledText(expressionSyntax.ToFullString()), syntaxToken.GetLeadingTrivia()));
			if (this._context.HasUnfinishedRegion())
			{
				return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_EndRegionDirectiveExpected);
			}
			if (this._context.HasUnfinishedIf())
			{
				return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_EndifDirectiveExpected);
			}
			return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_UnexpectedDirective);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x0011BB18 File Offset: 0x00119D18
		private DirectiveTriviaSyntax ParseElseDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive, bool endIsActive)
		{
			SyntaxToken syntaxToken = this.ParseEndOfDirective(false, false, false);
			if (this._context.HasPreviousIfOrElif())
			{
				bool flag = endIsActive && !this._context.PreviousBranchTaken();
				return SyntaxFactory.ElseDirectiveTrivia(hash, keyword, syntaxToken, endIsActive, flag);
			}
			if (this._context.HasUnfinishedRegion())
			{
				return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_EndRegionDirectiveExpected);
			}
			if (this._context.HasUnfinishedIf())
			{
				return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_EndifDirectiveExpected);
			}
			return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_UnexpectedDirective);
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0011BBC4 File Offset: 0x00119DC4
		private DirectiveTriviaSyntax ParseEndIfDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive, bool endIsActive)
		{
			SyntaxToken syntaxToken = this.ParseEndOfDirective(false, false, false);
			if (this._context.HasUnfinishedIf())
			{
				return SyntaxFactory.EndIfDirectiveTrivia(hash, keyword, syntaxToken, endIsActive);
			}
			if (this._context.HasUnfinishedRegion())
			{
				return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_EndRegionDirectiveExpected);
			}
			return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_UnexpectedDirective);
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x0011BC2F File Offset: 0x00119E2F
		private DirectiveTriviaSyntax ParseRegionDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive)
		{
			return SyntaxFactory.RegionDirectiveTrivia(hash, keyword, this.ParseEndOfDirectiveWithOptionalPreprocessingMessage(), isActive);
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x0011BC40 File Offset: 0x00119E40
		private DirectiveTriviaSyntax ParseEndRegionDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive)
		{
			SyntaxToken syntaxToken = this.ParseEndOfDirectiveWithOptionalPreprocessingMessage();
			if (this._context.HasUnfinishedRegion())
			{
				return SyntaxFactory.EndRegionDirectiveTrivia(hash, keyword, syntaxToken, isActive);
			}
			if (this._context.HasUnfinishedIf())
			{
				return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_EndifDirectiveExpected);
			}
			return base.AddError<BadDirectiveTriviaSyntax>(SyntaxFactory.BadDirectiveTrivia(hash, keyword, syntaxToken, isActive), ErrorCode.ERR_UnexpectedDirective);
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x0011BCA8 File Offset: 0x00119EA8
		private DirectiveTriviaSyntax ParseDefineOrUndefDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive, bool isFollowingToken)
		{
			if (isFollowingToken)
			{
				keyword = base.AddError<SyntaxToken>(keyword, ErrorCode.ERR_PPDefFollowsToken);
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.IdentifierToken, ErrorCode.ERR_IdentifierExpected, true);
			syntaxToken = DirectiveParser.TruncateIdentifier(syntaxToken);
			SyntaxToken syntaxToken2 = this.ParseEndOfDirective(syntaxToken.IsMissing, false, false);
			if (keyword.Kind == SyntaxKind.DefineKeyword)
			{
				return SyntaxFactory.DefineDirectiveTrivia(hash, keyword, syntaxToken, syntaxToken2, isActive);
			}
			return SyntaxFactory.UndefDirectiveTrivia(hash, keyword, syntaxToken, syntaxToken2, isActive);
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x0011BD14 File Offset: 0x00119F14
		private DirectiveTriviaSyntax ParseErrorOrWarningDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive)
		{
			SyntaxToken syntaxToken = this.ParseEndOfDirectiveWithOptionalPreprocessingMessage();
			bool flag = keyword.Kind == SyntaxKind.ErrorKeyword;
			if (isActive)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				int num = 0;
				bool flag2 = true;
				foreach (CSharpSyntaxNode csharpSyntaxNode in keyword.TrailingTrivia)
				{
					if (flag2)
					{
						if (csharpSyntaxNode.Kind == SyntaxKind.WhitespaceTrivia)
						{
							continue;
						}
						flag2 = false;
					}
					csharpSyntaxNode.WriteTo(stringWriter, true, true);
					num += csharpSyntaxNode.FullWidth;
				}
				foreach (CSharpSyntaxNode csharpSyntaxNode2 in syntaxToken.LeadingTrivia)
				{
					csharpSyntaxNode2.WriteTo(stringWriter, true, true);
					num += csharpSyntaxNode2.FullWidth;
				}
				int num2 = syntaxToken.GetLeadingTriviaWidth() - num;
				string text = stringWriter.ToString();
				syntaxToken = base.AddError<SyntaxToken>(syntaxToken, num2, num, flag ? ErrorCode.ERR_ErrorDirective : ErrorCode.WRN_WarningDirective, new object[] { text });
				if (flag)
				{
					LanguageVersion languageVersion2;
					if (text.Equals("version", 4))
					{
						string productVersion = CommonCompiler.GetProductVersion(typeof(CSharpCompiler));
						LanguageVersion specifiedLanguageVersion = base.Options.SpecifiedLanguageVersion;
						LanguageVersion languageVersion = specifiedLanguageVersion.MapSpecifiedToEffectiveVersion();
						string text2 = ((specifiedLanguageVersion == languageVersion) ? specifiedLanguageVersion.ToDisplayString() : (specifiedLanguageVersion.ToDisplayString() + " (" + languageVersion.ToDisplayString() + ")"));
						syntaxToken = base.AddError<SyntaxToken>(syntaxToken, num2, num, ErrorCode.ERR_CompilerAndLanguageVersion, new object[] { productVersion, text2 });
					}
					else if (base.Options.LanguageVersion != LanguageVersion.Preview && text.StartsWith("version:", 4) && LanguageVersionFacts.TryParse(text.Substring("version:".Length), out languageVersion2))
					{
						ErrorCode errorCode = base.Options.LanguageVersion.GetErrorCode();
						syntaxToken = base.AddError<SyntaxToken>(syntaxToken, num2, num, errorCode, new object[]
						{
							"version",
							new CSharpRequiredLanguageVersion(languageVersion2)
						});
					}
				}
			}
			if (flag)
			{
				return SyntaxFactory.ErrorDirectiveTrivia(hash, keyword, syntaxToken, isActive);
			}
			return SyntaxFactory.WarningDirectiveTrivia(hash, keyword, syntaxToken, isActive);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x0011BF20 File Offset: 0x0011A120
		private DirectiveTriviaSyntax ParseLineDirective(SyntaxToken hash, SyntaxToken id, bool isActive)
		{
			SyntaxToken syntaxToken = null;
			bool flag = false;
			SyntaxKind kind = base.CurrentToken.Kind;
			SyntaxToken syntaxToken2;
			if (kind == SyntaxKind.DefaultKeyword || kind == SyntaxKind.HiddenKeyword)
			{
				syntaxToken2 = base.EatToken();
			}
			else
			{
				syntaxToken2 = base.EatToken(SyntaxKind.NumericLiteralToken, ErrorCode.ERR_InvalidLineNumber, isActive);
				flag = true;
				if (isActive && !syntaxToken2.IsMissing && syntaxToken2.Kind == SyntaxKind.NumericLiteralToken)
				{
					if ((int)syntaxToken2.Value < 1)
					{
						syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.ERR_InvalidLineNumber);
					}
					else if ((int)syntaxToken2.Value > 16707565)
					{
						syntaxToken2 = base.AddError<SyntaxToken>(syntaxToken2, ErrorCode.WRN_TooManyLinesForDebugger);
					}
				}
				if (base.CurrentToken.Kind == SyntaxKind.StringLiteralToken && (syntaxToken2.IsMissing || syntaxToken2.GetTrailingTriviaWidth() > 0 || base.CurrentToken.GetLeadingTriviaWidth() > 0))
				{
					syntaxToken = base.EatToken();
					flag = false;
				}
			}
			SyntaxToken syntaxToken3 = this.ParseEndOfDirective(syntaxToken2.IsMissing || !isActive, false, flag);
			return SyntaxFactory.LineDirectiveTrivia(hash, id, syntaxToken2, syntaxToken, syntaxToken3, isActive);
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x0011C024 File Offset: 0x0011A224
		private DirectiveTriviaSyntax ParseReferenceDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive, bool isFollowingToken)
		{
			if (isActive)
			{
				if (base.Options.Kind == SourceCodeKind.Regular)
				{
					keyword = base.AddError<SyntaxToken>(keyword, ErrorCode.ERR_ReferenceDirectiveOnlyAllowedInScripts);
				}
				else if (isFollowingToken)
				{
					keyword = base.AddError<SyntaxToken>(keyword, ErrorCode.ERR_PPReferenceFollowsToken);
				}
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.StringLiteralToken, ErrorCode.ERR_ExpectedPPFile, isActive);
			SyntaxToken syntaxToken2 = this.ParseEndOfDirective(syntaxToken.IsMissing || !isActive, false, false);
			return SyntaxFactory.ReferenceDirectiveTrivia(hash, keyword, syntaxToken, syntaxToken2, isActive);
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x0011C098 File Offset: 0x0011A298
		private DirectiveTriviaSyntax ParseLoadDirective(SyntaxToken hash, SyntaxToken keyword, bool isActive, bool isFollowingToken)
		{
			if (isActive)
			{
				if (base.Options.Kind == SourceCodeKind.Regular)
				{
					keyword = base.AddError<SyntaxToken>(keyword, ErrorCode.ERR_LoadDirectiveOnlyAllowedInScripts);
				}
				else if (isFollowingToken)
				{
					keyword = base.AddError<SyntaxToken>(keyword, ErrorCode.ERR_PPLoadFollowsToken);
				}
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.StringLiteralToken, ErrorCode.ERR_ExpectedPPFile, isActive);
			SyntaxToken syntaxToken2 = this.ParseEndOfDirective(syntaxToken.IsMissing || !isActive, false, false);
			return SyntaxFactory.LoadDirectiveTrivia(hash, keyword, syntaxToken, syntaxToken2, isActive);
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x0011C10C File Offset: 0x0011A30C
		private DirectiveTriviaSyntax ParseNullableDirective(SyntaxToken hash, SyntaxToken token, bool isActive)
		{
			if (isActive)
			{
				token = base.CheckFeatureAvailability<SyntaxToken>(token, MessageID.IDS_FeatureNullableReferenceTypes, false);
			}
			SyntaxKind syntaxKind = base.CurrentToken.Kind;
			SyntaxToken syntaxToken;
			if (syntaxKind != SyntaxKind.DisableKeyword)
			{
				if (syntaxKind != SyntaxKind.RestoreKeyword)
				{
					if (syntaxKind == SyntaxKind.EnableKeyword)
					{
						syntaxToken = base.EatToken();
					}
					else
					{
						syntaxToken = base.EatToken(SyntaxKind.DisableKeyword, ErrorCode.ERR_NullableDirectiveQualifierExpected, isActive);
					}
				}
				else
				{
					syntaxToken = base.EatToken();
				}
			}
			else
			{
				syntaxToken = base.EatToken();
			}
			SyntaxToken syntaxToken2 = syntaxToken;
			syntaxKind = base.CurrentToken.Kind;
			if (syntaxKind <= SyntaxKind.AnnotationsKeyword)
			{
				if (syntaxKind == SyntaxKind.WarningsKeyword)
				{
					syntaxToken = base.EatToken();
					goto IL_00DD;
				}
				if (syntaxKind == SyntaxKind.AnnotationsKeyword)
				{
					syntaxToken = base.EatToken();
					goto IL_00DD;
				}
			}
			else
			{
				if (syntaxKind == SyntaxKind.EndOfDirectiveToken)
				{
					syntaxToken = null;
					goto IL_00DD;
				}
				if (syntaxKind == SyntaxKind.EndOfFileToken)
				{
					syntaxToken = null;
					goto IL_00DD;
				}
			}
			syntaxToken = base.EatToken(SyntaxKind.WarningsKeyword, ErrorCode.ERR_NullableDirectiveTargetExpected, !syntaxToken2.IsMissing && isActive);
			IL_00DD:
			SyntaxToken syntaxToken3 = syntaxToken;
			SyntaxToken syntaxToken4 = this.ParseEndOfDirective(syntaxToken2.IsMissing || (syntaxToken3 != null && syntaxToken3.IsMissing) || !isActive, false, false);
			return SyntaxFactory.NullableDirectiveTrivia(hash, token, syntaxToken2, syntaxToken3, syntaxToken4, isActive);
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x0011C228 File Offset: 0x0011A428
		private DirectiveTriviaSyntax ParsePragmaDirective(SyntaxToken hash, SyntaxToken pragma, bool isActive)
		{
			if (isActive)
			{
				pragma = base.CheckFeatureAvailability<SyntaxToken>(pragma, MessageID.IDS_FeaturePragma, false);
			}
			bool flag = false;
			if (base.CurrentToken.ContextualKind == SyntaxKind.WarningKeyword)
			{
				SyntaxToken syntaxToken = base.EatContextualToken(SyntaxKind.WarningKeyword, true);
				SyntaxToken syntaxToken2;
				if (base.CurrentToken.Kind == SyntaxKind.DisableKeyword || base.CurrentToken.Kind == SyntaxKind.RestoreKeyword)
				{
					syntaxToken2 = base.EatToken();
					SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = new SeparatedSyntaxListBuilder<ExpressionSyntax>(10);
					while (base.CurrentToken.Kind != SyntaxKind.EndOfDirectiveToken)
					{
						SyntaxToken syntaxToken3;
						ExpressionSyntax expressionSyntax;
						if (base.CurrentToken.Kind == SyntaxKind.NumericLiteralToken)
						{
							syntaxToken3 = base.EatToken();
							expressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, syntaxToken3);
						}
						else if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken)
						{
							syntaxToken3 = base.EatToken();
							expressionSyntax = SyntaxFactory.IdentifierName(syntaxToken3);
						}
						else
						{
							syntaxToken3 = base.EatToken(SyntaxKind.NumericLiteralToken, ErrorCode.WRN_IdentifierOrNumericLiteralExpected, isActive);
							expressionSyntax = SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, syntaxToken3);
						}
						flag = flag || syntaxToken3.ContainsDiagnostics;
						separatedSyntaxListBuilder.Add(expressionSyntax);
						if (base.CurrentToken.Kind != SyntaxKind.CommaToken)
						{
							break;
						}
						separatedSyntaxListBuilder.AddSeparator(base.EatToken());
					}
					SyntaxToken syntaxToken4 = this.ParseEndOfDirective(flag || !isActive, true, false);
					return SyntaxFactory.PragmaWarningDirectiveTrivia(hash, pragma, syntaxToken, syntaxToken2, separatedSyntaxListBuilder.ToList(), syntaxToken4, isActive);
				}
				syntaxToken2 = base.EatToken(SyntaxKind.DisableKeyword, ErrorCode.WRN_IllegalPPWarning, isActive);
				SyntaxToken syntaxToken5 = this.ParseEndOfDirective(true, true, false);
				return SyntaxFactory.PragmaWarningDirectiveTrivia(hash, pragma, syntaxToken, syntaxToken2, default(SeparatedSyntaxList<ExpressionSyntax>), syntaxToken5, isActive);
			}
			else
			{
				if (base.CurrentToken.Kind == SyntaxKind.ChecksumKeyword)
				{
					SyntaxToken syntaxToken6 = base.EatToken();
					SyntaxToken syntaxToken7 = base.EatToken(SyntaxKind.StringLiteralToken, ErrorCode.WRN_IllegalPPChecksum, isActive);
					SyntaxToken syntaxToken8 = base.EatToken(SyntaxKind.StringLiteralToken, ErrorCode.WRN_IllegalPPChecksum, isActive && !syntaxToken7.IsMissing);
					Guid guid;
					if (isActive && !syntaxToken8.IsMissing && !Guid.TryParse(syntaxToken8.ValueText, ref guid))
					{
						syntaxToken8 = base.AddError<SyntaxToken>(syntaxToken8, ErrorCode.WRN_IllegalPPChecksum);
					}
					SyntaxToken syntaxToken9 = base.EatToken(SyntaxKind.StringLiteralToken, ErrorCode.WRN_IllegalPPChecksum, isActive && !syntaxToken8.IsMissing);
					if (isActive && !syntaxToken9.IsMissing)
					{
						if (syntaxToken9.ValueText.Length % 2 != 0)
						{
							syntaxToken9 = base.AddError<SyntaxToken>(syntaxToken9, ErrorCode.WRN_IllegalPPChecksum);
						}
						else
						{
							string valueText = syntaxToken9.ValueText;
							for (int i = 0; i < valueText.Length; i++)
							{
								if (!SyntaxFacts.IsHexDigit(valueText.get_Chars(i)))
								{
									syntaxToken9 = base.AddError<SyntaxToken>(syntaxToken9, ErrorCode.WRN_IllegalPPChecksum);
									break;
								}
							}
						}
					}
					flag = syntaxToken7.ContainsDiagnostics | syntaxToken8.ContainsDiagnostics | syntaxToken9.ContainsDiagnostics;
					SyntaxToken syntaxToken10 = this.ParseEndOfDirective(flag, true, false);
					return SyntaxFactory.PragmaChecksumDirectiveTrivia(hash, pragma, syntaxToken6, syntaxToken7, syntaxToken8, syntaxToken9, syntaxToken10, isActive);
				}
				SyntaxToken syntaxToken11 = base.EatToken(SyntaxKind.WarningKeyword, ErrorCode.WRN_IllegalPragma, isActive);
				SyntaxToken syntaxToken12 = base.EatToken(SyntaxKind.DisableKeyword, false);
				SyntaxToken syntaxToken13 = this.ParseEndOfDirective(true, true, false);
				return SyntaxFactory.PragmaWarningDirectiveTrivia(hash, pragma, syntaxToken11, syntaxToken12, default(SeparatedSyntaxList<ExpressionSyntax>), syntaxToken13, isActive);
			}
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x0011C545 File Offset: 0x0011A745
		private DirectiveTriviaSyntax ParseShebangDirective(SyntaxToken hash, SyntaxToken exclamation, bool isActive)
		{
			return SyntaxFactory.ShebangDirectiveTrivia(hash, exclamation, this.ParseEndOfDirectiveWithOptionalPreprocessingMessage(), isActive);
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x0011C558 File Offset: 0x0011A758
		private SyntaxToken ParseEndOfDirectiveWithOptionalPreprocessingMessage()
		{
			StringBuilder stringBuilder = null;
			if (base.CurrentToken.Kind != SyntaxKind.EndOfDirectiveToken && base.CurrentToken.Kind != SyntaxKind.EndOfFileToken)
			{
				stringBuilder = new StringBuilder(base.CurrentToken.FullWidth);
				while (base.CurrentToken.Kind != SyntaxKind.EndOfDirectiveToken && base.CurrentToken.Kind != SyntaxKind.EndOfFileToken)
				{
					SyntaxToken syntaxToken = base.EatToken();
					stringBuilder.Append(syntaxToken.ToFullString());
				}
			}
			SyntaxToken syntaxToken2 = ((base.CurrentToken.Kind == SyntaxKind.EndOfDirectiveToken) ? base.EatToken() : SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken));
			if (stringBuilder != null)
			{
				syntaxToken2 = syntaxToken2.TokenWithLeadingTrivia(SyntaxFactory.PreprocessingMessage(stringBuilder.ToString()));
			}
			return syntaxToken2;
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x0011C614 File Offset: 0x0011A814
		private SyntaxToken ParseEndOfDirective(bool ignoreErrors, bool afterPragma = false, bool afterLineNumber = false)
		{
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = default(SyntaxListBuilder<SyntaxToken>);
			if (base.CurrentToken.Kind != SyntaxKind.EndOfDirectiveToken && base.CurrentToken.Kind != SyntaxKind.EndOfFileToken)
			{
				syntaxListBuilder = new SyntaxListBuilder<SyntaxToken>(10);
				if (!ignoreErrors)
				{
					ErrorCode errorCode = ErrorCode.ERR_EndOfPPLineExpected;
					if (afterPragma)
					{
						errorCode = ErrorCode.WRN_EndOfPPLineExpected;
					}
					else if (afterLineNumber)
					{
						errorCode = ErrorCode.ERR_MissingPPFile;
					}
					syntaxListBuilder.Add(base.AddError<SyntaxToken>(base.EatToken().WithoutDiagnosticsGreen<SyntaxToken>(), errorCode));
				}
				while (base.CurrentToken.Kind != SyntaxKind.EndOfDirectiveToken && base.CurrentToken.Kind != SyntaxKind.EndOfFileToken)
				{
					syntaxListBuilder.Add(base.EatToken().WithoutDiagnosticsGreen<SyntaxToken>());
				}
			}
			SyntaxToken syntaxToken = ((base.CurrentToken.Kind == SyntaxKind.EndOfDirectiveToken) ? base.EatToken() : SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken));
			if (!syntaxListBuilder.IsNull)
			{
				syntaxToken = syntaxToken.TokenWithLeadingTrivia(SyntaxFactory.SkippedTokensTrivia(syntaxListBuilder.ToList()));
			}
			return syntaxToken;
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x0011C70B File Offset: 0x0011A90B
		private ExpressionSyntax ParseExpression()
		{
			return this.ParseLogicalOr();
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0011C714 File Offset: 0x0011A914
		private ExpressionSyntax ParseLogicalOr()
		{
			ExpressionSyntax expressionSyntax = this.ParseLogicalAnd();
			while (base.CurrentToken.Kind == SyntaxKind.BarBarToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				ExpressionSyntax expressionSyntax2 = this.ParseLogicalAnd();
				expressionSyntax = SyntaxFactory.BinaryExpression(SyntaxKind.LogicalOrExpression, expressionSyntax, syntaxToken, expressionSyntax2);
			}
			return expressionSyntax;
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x0011C75C File Offset: 0x0011A95C
		private ExpressionSyntax ParseLogicalAnd()
		{
			ExpressionSyntax expressionSyntax = this.ParseEquality();
			while (base.CurrentToken.Kind == SyntaxKind.AmpersandAmpersandToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				ExpressionSyntax expressionSyntax2 = this.ParseEquality();
				expressionSyntax = SyntaxFactory.BinaryExpression(SyntaxKind.LogicalAndExpression, expressionSyntax, syntaxToken, expressionSyntax2);
			}
			return expressionSyntax;
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x0011C7A4 File Offset: 0x0011A9A4
		private ExpressionSyntax ParseEquality()
		{
			ExpressionSyntax expressionSyntax = this.ParseLogicalNot();
			while (base.CurrentToken.Kind == SyntaxKind.EqualsEqualsToken || base.CurrentToken.Kind == SyntaxKind.ExclamationEqualsToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				ExpressionSyntax expressionSyntax2 = this.ParseEquality();
				expressionSyntax = SyntaxFactory.BinaryExpression(SyntaxFacts.GetBinaryExpression(syntaxToken.Kind), expressionSyntax, syntaxToken, expressionSyntax2);
			}
			return expressionSyntax;
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x0011C804 File Offset: 0x0011AA04
		private ExpressionSyntax ParseLogicalNot()
		{
			if (base.CurrentToken.Kind == SyntaxKind.ExclamationToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				return SyntaxFactory.PrefixUnaryExpression(SyntaxKind.LogicalNotExpression, syntaxToken, this.ParseLogicalNot());
			}
			return this.ParsePrimary();
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0011C844 File Offset: 0x0011AA44
		private ExpressionSyntax ParsePrimary()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind == SyntaxKind.OpenParenToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				ExpressionSyntax expressionSyntax = this.ParseExpression();
				SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseParenToken);
				return SyntaxFactory.ParenthesizedExpression(syntaxToken, expressionSyntax, syntaxToken2);
			}
			if (kind - SyntaxKind.TrueKeyword <= 1)
			{
				return SyntaxFactory.LiteralExpression(SyntaxFacts.GetLiteralExpression(kind), base.EatToken());
			}
			if (kind != SyntaxKind.IdentifierToken)
			{
				return SyntaxFactory.IdentifierName(base.EatToken(SyntaxKind.IdentifierToken, ErrorCode.ERR_InvalidPreprocExpr, true));
			}
			return SyntaxFactory.IdentifierName(DirectiveParser.TruncateIdentifier(base.EatToken()));
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0011C8D4 File Offset: 0x0011AAD4
		private static SyntaxToken TruncateIdentifier(SyntaxToken identifier)
		{
			if (identifier.Width > 128)
			{
				GreenNode leadingTrivia = identifier.GetLeadingTrivia();
				GreenNode trailingTrivia = identifier.GetTrailingTrivia();
				string text = identifier.ToString();
				string text2 = text.Substring(0, 128);
				identifier = SyntaxFactory.Identifier(SyntaxKind.IdentifierToken, leadingTrivia, text, text2, trailingTrivia);
			}
			return identifier;
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x0011C924 File Offset: 0x0011AB24
		private bool EvaluateBool(ExpressionSyntax expr)
		{
			object obj = this.Evaluate(expr);
			return obj is bool && (bool)obj;
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0011C94C File Offset: 0x0011AB4C
		private object Evaluate(ExpressionSyntax expr)
		{
			SyntaxKind kind = expr.Kind;
			if (kind <= SyntaxKind.ParenthesizedExpression)
			{
				if (kind != SyntaxKind.IdentifierName)
				{
					if (kind == SyntaxKind.ParenthesizedExpression)
					{
						return this.Evaluate(((ParenthesizedExpressionSyntax)expr).Expression);
					}
				}
				else
				{
					string valueText = ((IdentifierNameSyntax)expr).Identifier.ValueText;
					bool flag;
					if (bool.TryParse(valueText, ref flag))
					{
						return flag;
					}
					return this.IsDefined(valueText);
				}
			}
			else
			{
				switch (kind)
				{
				case SyntaxKind.LogicalOrExpression:
				case SyntaxKind.BitwiseOrExpression:
					return this.EvaluateBool(((BinaryExpressionSyntax)expr).Left) || this.EvaluateBool(((BinaryExpressionSyntax)expr).Right);
				case SyntaxKind.LogicalAndExpression:
				case SyntaxKind.BitwiseAndExpression:
					return this.EvaluateBool(((BinaryExpressionSyntax)expr).Left) && this.EvaluateBool(((BinaryExpressionSyntax)expr).Right);
				case SyntaxKind.ExclusiveOrExpression:
					break;
				case SyntaxKind.EqualsExpression:
					return object.Equals(this.Evaluate(((BinaryExpressionSyntax)expr).Left), this.Evaluate(((BinaryExpressionSyntax)expr).Right));
				case SyntaxKind.NotEqualsExpression:
					return !object.Equals(this.Evaluate(((BinaryExpressionSyntax)expr).Left), this.Evaluate(((BinaryExpressionSyntax)expr).Right));
				default:
					if (kind == SyntaxKind.LogicalNotExpression)
					{
						return !this.EvaluateBool(((PrefixUnaryExpressionSyntax)expr).Operand);
					}
					if (kind - SyntaxKind.TrueLiteralExpression <= 1)
					{
						return ((LiteralExpressionSyntax)expr).Token.Value;
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0011CAEC File Offset: 0x0011ACEC
		private bool IsDefined(string id)
		{
			switch (this._context.IsDefined(id))
			{
			case DefineState.Defined:
				return true;
			case DefineState.Undefined:
				return false;
			default:
				return base.Options.PreprocessorSymbols.Contains(id);
			}
		}

		// Token: 0x0400153A RID: 5434
		private const int MAX_DIRECTIVE_IDENTIFIER_WIDTH = 128;

		// Token: 0x0400153B RID: 5435
		private readonly DirectiveStack _context;
	}
}
