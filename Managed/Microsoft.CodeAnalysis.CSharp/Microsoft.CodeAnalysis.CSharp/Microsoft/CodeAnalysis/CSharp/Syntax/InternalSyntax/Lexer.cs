using System;
using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000364 RID: 868
	internal class Lexer : AbstractLexer
	{
		// Token: 0x06004A6C RID: 19052 RVA: 0x001306D4 File Offset: 0x0012E8D4
		public Lexer(SourceText text, CSharpParseOptions options, bool allowPreprocessorDirectives = true, bool interpolationFollowedByColon = false)
			: base(text)
		{
			this._options = options;
			this._builder = new StringBuilder();
			this._identBuffer = new char[32];
			this._cache = new LexerCache();
			this._createQuickTokenFunction = new Func<SyntaxToken>(this.CreateQuickToken);
			this._allowPreprocessorDirectives = allowPreprocessorDirectives;
			this._interpolationFollowedByColon = interpolationFollowedByColon;
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x0013074D File Offset: 0x0012E94D
		public override void Dispose()
		{
			this._cache.Free();
			if (this._xmlParser != null)
			{
				this._xmlParser.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x00130773 File Offset: 0x0012E973
		public bool SuppressDocumentationCommentParse
		{
			get
			{
				return this._options.DocumentationMode < DocumentationMode.Parse;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06004A6F RID: 19055 RVA: 0x00130783 File Offset: 0x0012E983
		public CSharpParseOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06004A70 RID: 19056 RVA: 0x0013078B File Offset: 0x0012E98B
		public DirectiveStack Directives
		{
			get
			{
				return this._directives;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x00130793 File Offset: 0x0012E993
		public bool InterpolationFollowedByColon
		{
			get
			{
				return this._interpolationFollowedByColon;
			}
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x0013079B File Offset: 0x0012E99B
		public void Reset(int position, DirectiveStack directives)
		{
			this.TextWindow.Reset(position);
			this._directives = directives;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x001307B0 File Offset: 0x0012E9B0
		private static LexerMode ModeOf(LexerMode mode)
		{
			return mode & LexerMode.MaskLexMode;
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x001307B9 File Offset: 0x0012E9B9
		private bool ModeIs(LexerMode mode)
		{
			return Lexer.ModeOf(this._mode) == mode;
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x001307C9 File Offset: 0x0012E9C9
		private static XmlDocCommentLocation LocationOf(LexerMode mode)
		{
			return (XmlDocCommentLocation)((mode & LexerMode.MaskXmlDocCommentLocation) >> 16);
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x001307D5 File Offset: 0x0012E9D5
		private bool LocationIs(XmlDocCommentLocation location)
		{
			return Lexer.LocationOf(this._mode) == location;
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x001307E5 File Offset: 0x0012E9E5
		private void MutateLocation(XmlDocCommentLocation location)
		{
			this._mode &= (LexerMode)(-983041);
			this._mode |= (LexerMode)(location << 16);
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x0013080A File Offset: 0x0012EA0A
		private static XmlDocCommentStyle StyleOf(LexerMode mode)
		{
			return (XmlDocCommentStyle)((mode & LexerMode.MaskXmlDocCommentStyle) >> 20);
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x00130816 File Offset: 0x0012EA16
		private bool StyleIs(XmlDocCommentStyle style)
		{
			return Lexer.StyleOf(this._mode) == style;
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x00130828 File Offset: 0x0012EA28
		private bool InDocumentationComment
		{
			get
			{
				LexerMode lexerMode = Lexer.ModeOf(this._mode);
				if (lexerMode <= LexerMode.XmlCrefDoubleQuote)
				{
					if (lexerMode <= LexerMode.XmlAttributeTextQuote)
					{
						if (lexerMode != LexerMode.XmlDocComment && lexerMode != LexerMode.XmlElementTag && lexerMode != LexerMode.XmlAttributeTextQuote)
						{
							return false;
						}
					}
					else if (lexerMode != LexerMode.XmlAttributeTextDoubleQuote && lexerMode != LexerMode.XmlCrefQuote && lexerMode != LexerMode.XmlCrefDoubleQuote)
					{
						return false;
					}
				}
				else if (lexerMode <= LexerMode.XmlCDataSectionText)
				{
					if (lexerMode != LexerMode.XmlNameQuote && lexerMode != LexerMode.XmlNameDoubleQuote && lexerMode != LexerMode.XmlCDataSectionText)
					{
						return false;
					}
				}
				else if (lexerMode != LexerMode.XmlCommentText && lexerMode != LexerMode.XmlProcessingInstructionText && lexerMode != LexerMode.XmlCharacter)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x001308B2 File Offset: 0x0012EAB2
		public SyntaxToken Lex(ref LexerMode mode)
		{
			SyntaxToken syntaxToken = this.Lex(mode);
			mode = this._mode;
			return syntaxToken;
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x001308C4 File Offset: 0x0012EAC4
		public SyntaxToken Lex(LexerMode mode)
		{
			this._mode = mode;
			LexerMode lexerMode = this._mode;
			if (lexerMode - LexerMode.Syntax <= 1)
			{
				return this.QuickScanSyntaxToken() ?? this.LexSyntaxToken();
			}
			if (lexerMode != LexerMode.Directive)
			{
				lexerMode = Lexer.ModeOf(this._mode);
				if (lexerMode <= LexerMode.XmlCrefDoubleQuote)
				{
					if (lexerMode <= LexerMode.XmlAttributeTextQuote)
					{
						if (lexerMode == LexerMode.XmlDocComment)
						{
							return this.LexXmlToken();
						}
						if (lexerMode == LexerMode.XmlElementTag)
						{
							return this.LexXmlElementTagToken();
						}
						if (lexerMode != LexerMode.XmlAttributeTextQuote)
						{
							goto IL_00F0;
						}
					}
					else if (lexerMode != LexerMode.XmlAttributeTextDoubleQuote)
					{
						if (lexerMode != LexerMode.XmlCrefQuote && lexerMode != LexerMode.XmlCrefDoubleQuote)
						{
							goto IL_00F0;
						}
						return this.LexXmlCrefOrNameToken();
					}
					return this.LexXmlAttributeTextToken();
				}
				if (lexerMode <= LexerMode.XmlCDataSectionText)
				{
					if (lexerMode == LexerMode.XmlNameQuote || lexerMode == LexerMode.XmlNameDoubleQuote)
					{
						return this.LexXmlCrefOrNameToken();
					}
					if (lexerMode == LexerMode.XmlCDataSectionText)
					{
						return this.LexXmlCDataSectionTextToken();
					}
				}
				else
				{
					if (lexerMode == LexerMode.XmlCommentText)
					{
						return this.LexXmlCommentTextToken();
					}
					if (lexerMode == LexerMode.XmlProcessingInstructionText)
					{
						return this.LexXmlProcessingInstructionTextToken();
					}
					if (lexerMode == LexerMode.XmlCharacter)
					{
						return this.LexXmlCharacter();
					}
				}
				IL_00F0:
				throw ExceptionUtilities.UnexpectedValue(Lexer.ModeOf(this._mode));
			}
			return this.LexDirectiveToken();
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x001309D8 File Offset: 0x0012EBD8
		private static int GetFullWidth(SyntaxListBuilder builder)
		{
			int num = 0;
			if (builder != null)
			{
				for (int i = 0; i < builder.Count; i++)
				{
					num += builder[i].FullWidth;
				}
			}
			return num;
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x00130A0C File Offset: 0x0012EC0C
		private SyntaxToken LexSyntaxToken()
		{
			this._leadingTriviaCache.Clear();
			this.LexSyntaxTrivia(this.TextWindow.Position > 0, false, ref this._leadingTriviaCache);
			SyntaxListBuilder leadingTriviaCache = this._leadingTriviaCache;
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			base.Start();
			this.ScanSyntaxToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(leadingTriviaCache));
			this._trailingTriviaCache.Clear();
			this.LexSyntaxTrivia(true, true, ref this._trailingTriviaCache);
			SyntaxListBuilder trailingTriviaCache = this._trailingTriviaCache;
			return this.Create(ref tokenInfo, leadingTriviaCache, trailingTriviaCache, errors);
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x00130A94 File Offset: 0x0012EC94
		internal SyntaxTriviaList LexSyntaxLeadingTrivia()
		{
			this._leadingTriviaCache.Clear();
			this.LexSyntaxTrivia(this.TextWindow.Position > 0, false, ref this._leadingTriviaCache);
			SyntaxToken syntaxToken = default(SyntaxToken);
			return new SyntaxTriviaList(in syntaxToken, this._leadingTriviaCache.ToListNode(), 0, 0);
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00130AE4 File Offset: 0x0012ECE4
		internal SyntaxTriviaList LexSyntaxTrailingTrivia()
		{
			this._trailingTriviaCache.Clear();
			this.LexSyntaxTrivia(true, true, ref this._trailingTriviaCache);
			SyntaxToken syntaxToken = default(SyntaxToken);
			return new SyntaxTriviaList(in syntaxToken, this._trailingTriviaCache.ToListNode(), 0, 0);
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x00130B28 File Offset: 0x0012ED28
		private SyntaxToken Create(ref Lexer.TokenInfo info, SyntaxListBuilder leading, SyntaxListBuilder trailing, SyntaxDiagnosticInfo[] errors)
		{
			GreenNode greenNode = ((leading != null) ? leading.ToListNode() : null);
			GreenNode greenNode2 = ((trailing != null) ? trailing.ToListNode() : null);
			SyntaxToken syntaxToken;
			if (info.RequiresTextForXmlEntity)
			{
				syntaxToken = SyntaxFactory.Token(greenNode, info.Kind, info.Text, info.StringValue, greenNode2);
			}
			else
			{
				SyntaxKind kind = info.Kind;
				if (kind != SyntaxKind.None)
				{
					if (kind - SyntaxKind.EndOfDocumentationCommentToken > 1)
					{
						switch (kind)
						{
						case SyntaxKind.IdentifierToken:
							syntaxToken = SyntaxFactory.Identifier(info.ContextualKind, greenNode, info.Text, info.StringValue, greenNode2);
							break;
						case SyntaxKind.NumericLiteralToken:
							switch (info.ValueKind)
							{
							case SpecialType.System_Int32:
								syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.IntValue, greenNode2);
								break;
							case SpecialType.System_UInt32:
								syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.UintValue, greenNode2);
								break;
							case SpecialType.System_Int64:
								syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.LongValue, greenNode2);
								break;
							case SpecialType.System_UInt64:
								syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.UlongValue, greenNode2);
								break;
							case SpecialType.System_Decimal:
								syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.DecimalValue, greenNode2);
								break;
							case SpecialType.System_Single:
								syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.FloatValue, greenNode2);
								break;
							case SpecialType.System_Double:
								syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.DoubleValue, greenNode2);
								break;
							default:
								throw ExceptionUtilities.UnexpectedValue(info.ValueKind);
							}
							break;
						case SyntaxKind.CharacterLiteralToken:
							syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.CharValue, greenNode2);
							break;
						case SyntaxKind.StringLiteralToken:
							syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.Kind, info.StringValue, greenNode2);
							break;
						case SyntaxKind.XmlEntityLiteralToken:
							syntaxToken = SyntaxFactory.XmlEntity(greenNode, info.Text, info.StringValue, greenNode2);
							break;
						case SyntaxKind.XmlTextLiteralToken:
							syntaxToken = SyntaxFactory.XmlTextLiteral(greenNode, info.Text, info.StringValue, greenNode2);
							break;
						case SyntaxKind.XmlTextLiteralNewLineToken:
							syntaxToken = SyntaxFactory.XmlTextNewLine(greenNode, info.Text, info.StringValue, greenNode2);
							break;
						case SyntaxKind.InterpolatedStringToken:
							syntaxToken = SyntaxFactory.Literal(greenNode, info.Text, info.Kind, info.Text, greenNode2);
							break;
						default:
							syntaxToken = SyntaxFactory.Token(greenNode, info.Kind, greenNode2);
							break;
						}
					}
					else
					{
						syntaxToken = SyntaxFactory.Token(greenNode, info.Kind, greenNode2);
					}
				}
				else
				{
					syntaxToken = SyntaxFactory.BadToken(greenNode, info.Text, greenNode2);
				}
			}
			if (errors != null && (this._options.DocumentationMode >= DocumentationMode.Diagnose || !this.InDocumentationComment))
			{
				syntaxToken = syntaxToken.WithDiagnosticsGreen(errors);
			}
			return syntaxToken;
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x00130DC0 File Offset: 0x0012EFC0
		private void ScanSyntaxToken(ref Lexer.TokenInfo info)
		{
			info.Kind = SyntaxKind.None;
			info.ContextualKind = SyntaxKind.None;
			info.Text = null;
			char maxValue = char.MaxValue;
			bool flag = false;
			int position = this.TextWindow.Position;
			char c = this.TextWindow.PeekChar();
			switch (c)
			{
			case '!':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.ExclamationEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.ExclamationToken;
				return;
			case '"':
			case '\'':
				this.ScanStringLiteral(ref info, true);
				return;
			case '#':
			case '`':
				goto IL_08E5;
			case '$':
				if (this.TextWindow.PeekChar(1) == '"')
				{
					this.ScanInterpolatedStringLiteral(false, ref info);
					this.CheckFeatureAvailability(MessageID.IDS_FeatureInterpolatedStrings);
					return;
				}
				if (this.TextWindow.PeekChar(1) == '@' && this.TextWindow.PeekChar(2) == '"')
				{
					this.ScanInterpolatedStringLiteral(true, ref info);
					this.CheckFeatureAvailability(MessageID.IDS_FeatureInterpolatedStrings);
					return;
				}
				if (!this.ModeIs(LexerMode.DebuggerSyntax))
				{
					goto IL_08E5;
				}
				break;
			case '%':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.PercentEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.PercentToken;
				return;
			case '&':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.AmpersandEqualsToken;
					return;
				}
				if (this.TextWindow.PeekChar() == '&')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.AmpersandAmpersandToken;
					return;
				}
				info.Kind = SyntaxKind.AmpersandToken;
				return;
			case '(':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.OpenParenToken;
				return;
			case ')':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.CloseParenToken;
				return;
			case '*':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.AsteriskEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.AsteriskToken;
				return;
			case '+':
				this.TextWindow.AdvanceChar();
				if ((c = this.TextWindow.PeekChar()) == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.PlusEqualsToken;
					return;
				}
				if (c == '+')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.PlusPlusToken;
					return;
				}
				info.Kind = SyntaxKind.PlusToken;
				return;
			case ',':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.CommaToken;
				return;
			case '-':
				this.TextWindow.AdvanceChar();
				if ((c = this.TextWindow.PeekChar()) == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.MinusEqualsToken;
					return;
				}
				if (c == '-')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.MinusMinusToken;
					return;
				}
				if (c == '>')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.MinusGreaterThanToken;
					return;
				}
				info.Kind = SyntaxKind.MinusToken;
				return;
			case '.':
				if (this.ScanNumericLiteral(ref info))
				{
					return;
				}
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '.')
				{
					this.TextWindow.AdvanceChar();
					if (this.TextWindow.PeekChar() == '.')
					{
						base.AddError(ErrorCode.ERR_TripleDotNotAllowed);
					}
					info.Kind = SyntaxKind.DotDotToken;
					return;
				}
				info.Kind = SyntaxKind.DotToken;
				return;
			case '/':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.SlashEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.SlashToken;
				return;
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				this.ScanNumericLiteral(ref info);
				return;
			case ':':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == ':')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.ColonColonToken;
					return;
				}
				info.Kind = SyntaxKind.ColonToken;
				return;
			case ';':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.SemicolonToken;
				return;
			case '<':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.LessThanEqualsToken;
					return;
				}
				if (this.TextWindow.PeekChar() != '<')
				{
					info.Kind = SyntaxKind.LessThanToken;
					return;
				}
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.LessThanLessThanEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.LessThanLessThanToken;
				return;
			case '=':
				this.TextWindow.AdvanceChar();
				if ((c = this.TextWindow.PeekChar()) == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.EqualsEqualsToken;
					return;
				}
				if (c == '>')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.EqualsGreaterThanToken;
					return;
				}
				info.Kind = SyntaxKind.EqualsToken;
				return;
			case '>':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.GreaterThanEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.GreaterThanToken;
				return;
			case '?':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() != '?')
				{
					info.Kind = SyntaxKind.QuestionToken;
					return;
				}
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.QuestionQuestionEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.QuestionQuestionToken;
				return;
			case '@':
				if (this.TextWindow.PeekChar(1) == '"')
				{
					this.ScanVerbatimStringLiteral(ref info, true);
					return;
				}
				if (this.TextWindow.PeekChar(1) == '$' && this.TextWindow.PeekChar(2) == '"')
				{
					this.ScanInterpolatedStringLiteral(true, ref info);
					this.CheckFeatureAvailability(MessageID.IDS_FeatureAltInterpolatedVerbatimStrings);
					return;
				}
				if (!this.ScanIdentifierOrKeyword(ref info))
				{
					this.TextWindow.AdvanceChar();
					info.Text = this.TextWindow.GetText(true);
					base.AddError(ErrorCode.ERR_ExpectedVerbatimLiteral);
					return;
				}
				return;
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				break;
			case '[':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.OpenBracketToken;
				return;
			case '\\':
				c = this.TextWindow.PeekCharOrUnicodeEscape(out maxValue);
				flag = true;
				if (!SyntaxFacts.IsIdentifierStartCharacter(c))
				{
					goto IL_08E5;
				}
				break;
			case ']':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.CloseBracketToken;
				return;
			case '^':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.CaretEqualsToken;
					return;
				}
				info.Kind = SyntaxKind.CaretToken;
				return;
			case '{':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.OpenBraceToken;
				return;
			case '|':
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '=')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.BarEqualsToken;
					return;
				}
				if (this.TextWindow.PeekChar() == '|')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.BarBarToken;
					return;
				}
				info.Kind = SyntaxKind.BarToken;
				return;
			case '}':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.CloseBraceToken;
				return;
			case '~':
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.TildeToken;
				return;
			default:
				if (c != '\uffff')
				{
					goto IL_08E5;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					if (this._directives.HasUnfinishedIf())
					{
						base.AddError(ErrorCode.ERR_EndifDirectiveExpected);
					}
					if (this._directives.HasUnfinishedRegion())
					{
						base.AddError(ErrorCode.ERR_EndRegionDirectiveExpected);
					}
					info.Kind = SyntaxKind.EndOfFileToken;
					return;
				}
				goto IL_08E5;
			}
			IL_0870:
			this.ScanIdentifierOrKeyword(ref info);
			return;
			IL_08E5:
			if (SyntaxFacts.IsIdentifierStartCharacter(c))
			{
				goto IL_0870;
			}
			if (flag)
			{
				SyntaxDiagnosticInfo syntaxDiagnosticInfo;
				this.TextWindow.NextCharOrUnicodeEscape(out maxValue, out syntaxDiagnosticInfo);
				base.AddError(syntaxDiagnosticInfo);
			}
			else
			{
				this.TextWindow.AdvanceChar();
			}
			int badTokenCount = this._badTokenCount;
			this._badTokenCount = badTokenCount + 1;
			if (badTokenCount > 200)
			{
				int length = this.TextWindow.Text.Length;
				int num = length - position;
				info.Text = this.TextWindow.Text.ToString(new TextSpan(position, num));
				this.TextWindow.Reset(length);
			}
			else
			{
				info.Text = this.TextWindow.GetText(true);
			}
			base.AddError(ErrorCode.ERR_UnexpectedCharacter, new object[] { info.Text });
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x00131770 File Offset: 0x0012F970
		private void CheckFeatureAvailability(MessageID feature)
		{
			CSDiagnosticInfo featureAvailabilityDiagnosticInfo = feature.GetFeatureAvailabilityDiagnosticInfo(this.Options);
			if (featureAvailabilityDiagnosticInfo != null)
			{
				base.AddError(featureAvailabilityDiagnosticInfo.Code, featureAvailabilityDiagnosticInfo.Arguments);
			}
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x001317A0 File Offset: 0x0012F9A0
		private bool ScanInteger()
		{
			int position = this.TextWindow.Position;
			char c;
			while ((c = this.TextWindow.PeekChar()) >= '0' && c <= '9')
			{
				this.TextWindow.AdvanceChar();
			}
			return position < this.TextWindow.Position;
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x001317EC File Offset: 0x0012F9EC
		private void ScanNumericLiteralSingleInteger(ref bool underscoreInWrongPlace, ref bool usedUnderscore, ref bool firstCharWasUnderscore, bool isHex, bool isBinary)
		{
			if (this.TextWindow.PeekChar() == '_')
			{
				if (isHex || isBinary)
				{
					firstCharWasUnderscore = true;
				}
				else
				{
					underscoreInWrongPlace = true;
				}
			}
			bool flag = false;
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c == '_')
				{
					usedUnderscore = true;
					flag = true;
				}
				else
				{
					if (!(isHex ? SyntaxFacts.IsHexDigit(c) : (isBinary ? SyntaxFacts.IsBinaryDigit(c) : SyntaxFacts.IsDecDigit(c))))
					{
						break;
					}
					this._builder.Append(c);
					flag = false;
				}
				this.TextWindow.AdvanceChar();
			}
			if (flag)
			{
				underscoreInWrongPlace = true;
			}
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x00131874 File Offset: 0x0012FA74
		private bool ScanNumericLiteral(ref Lexer.TokenInfo info)
		{
			int position = this.TextWindow.Position;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			info.Text = null;
			info.ValueKind = SpecialType.None;
			this._builder.Clear();
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			char c = this.TextWindow.PeekChar();
			if (c == '0')
			{
				c = this.TextWindow.PeekChar(1);
				if (c == 'x' || c == 'X')
				{
					this.TextWindow.AdvanceChar(2);
					flag = true;
				}
				else if (c == 'b' || c == 'B')
				{
					this.CheckFeatureAvailability(MessageID.IDS_FeatureBinaryLiteral);
					this.TextWindow.AdvanceChar(2);
					flag2 = true;
				}
			}
			if (flag || flag2)
			{
				this.ScanNumericLiteralSingleInteger(ref flag7, ref flag8, ref flag9, flag, flag2);
				if ((c = this.TextWindow.PeekChar()) == 'L' || c == 'l')
				{
					if (c == 'l')
					{
						base.AddError(this.TextWindow.Position, 1, ErrorCode.WRN_LowercaseEllSuffix);
					}
					this.TextWindow.AdvanceChar();
					flag6 = true;
					if ((c = this.TextWindow.PeekChar()) == 'u' || c == 'U')
					{
						this.TextWindow.AdvanceChar();
						flag5 = true;
					}
				}
				else if ((c = this.TextWindow.PeekChar()) == 'u' || c == 'U')
				{
					this.TextWindow.AdvanceChar();
					flag5 = true;
					if ((c = this.TextWindow.PeekChar()) == 'L' || c == 'l')
					{
						this.TextWindow.AdvanceChar();
						flag6 = true;
					}
				}
			}
			else
			{
				this.ScanNumericLiteralSingleInteger(ref flag7, ref flag8, ref flag9, false, false);
				if (this.ModeIs(LexerMode.DebuggerSyntax) && this.TextWindow.PeekChar() == '#')
				{
					this.TextWindow.AdvanceChar();
					info.StringValue = (info.Text = this.TextWindow.GetText(true));
					info.Kind = SyntaxKind.IdentifierToken;
					base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_LegacyObjectIdSyntax));
					return true;
				}
				if ((c = this.TextWindow.PeekChar()) == '.')
				{
					char c2 = this.TextWindow.PeekChar(1);
					if (c2 >= '0' && c2 <= '9')
					{
						flag3 = true;
						this._builder.Append(c);
						this.TextWindow.AdvanceChar();
						this.ScanNumericLiteralSingleInteger(ref flag7, ref flag8, ref flag9, false, false);
					}
					else if (this._builder.Length == 0)
					{
						this.TextWindow.Reset(position);
						return false;
					}
				}
				if ((c = this.TextWindow.PeekChar()) == 'E' || c == 'e')
				{
					this._builder.Append(c);
					this.TextWindow.AdvanceChar();
					flag4 = true;
					if ((c = this.TextWindow.PeekChar()) == '-' || c == '+')
					{
						this._builder.Append(c);
						this.TextWindow.AdvanceChar();
					}
					if (((c = this.TextWindow.PeekChar()) < '0' || c > '9') && c != '_')
					{
						base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_InvalidReal));
						this._builder.Append('0');
					}
					else
					{
						this.ScanNumericLiteralSingleInteger(ref flag7, ref flag8, ref flag9, false, false);
					}
				}
				if (flag4 || flag3)
				{
					if ((c = this.TextWindow.PeekChar()) == 'f' || c == 'F')
					{
						this.TextWindow.AdvanceChar();
						info.ValueKind = SpecialType.System_Single;
					}
					else if (c == 'D' || c == 'd')
					{
						this.TextWindow.AdvanceChar();
						info.ValueKind = SpecialType.System_Double;
					}
					else if (c == 'm' || c == 'M')
					{
						this.TextWindow.AdvanceChar();
						info.ValueKind = SpecialType.System_Decimal;
					}
					else
					{
						info.ValueKind = SpecialType.System_Double;
					}
				}
				else if ((c = this.TextWindow.PeekChar()) == 'f' || c == 'F')
				{
					this.TextWindow.AdvanceChar();
					info.ValueKind = SpecialType.System_Single;
				}
				else if (c == 'D' || c == 'd')
				{
					this.TextWindow.AdvanceChar();
					info.ValueKind = SpecialType.System_Double;
				}
				else if (c == 'm' || c == 'M')
				{
					this.TextWindow.AdvanceChar();
					info.ValueKind = SpecialType.System_Decimal;
				}
				else if (c == 'L' || c == 'l')
				{
					if (c == 'l')
					{
						base.AddError(this.TextWindow.Position, 1, ErrorCode.WRN_LowercaseEllSuffix);
					}
					this.TextWindow.AdvanceChar();
					flag6 = true;
					if ((c = this.TextWindow.PeekChar()) == 'u' || c == 'U')
					{
						this.TextWindow.AdvanceChar();
						flag5 = true;
					}
				}
				else if (c == 'u' || c == 'U')
				{
					flag5 = true;
					this.TextWindow.AdvanceChar();
					if ((c = this.TextWindow.PeekChar()) == 'L' || c == 'l')
					{
						this.TextWindow.AdvanceChar();
						flag6 = true;
					}
				}
			}
			if (flag7)
			{
				base.AddError(base.MakeError(position, this.TextWindow.Position - position, ErrorCode.ERR_InvalidNumber));
			}
			else if (flag9)
			{
				this.CheckFeatureAvailability(MessageID.IDS_FeatureLeadingDigitSeparator);
			}
			else if (flag8)
			{
				this.CheckFeatureAvailability(MessageID.IDS_FeatureDigitSeparator);
			}
			info.Kind = SyntaxKind.NumericLiteralToken;
			info.Text = this.TextWindow.GetText(true);
			string text = this.TextWindow.Intern(this._builder);
			switch (info.ValueKind)
			{
			case SpecialType.System_Decimal:
				info.DecimalValue = this.GetValueDecimal(text, position, this.TextWindow.Position);
				break;
			case SpecialType.System_Single:
				info.FloatValue = this.GetValueSingle(text);
				break;
			case SpecialType.System_Double:
				info.DoubleValue = this.GetValueDouble(text);
				break;
			default:
			{
				ulong num;
				if (string.IsNullOrEmpty(text))
				{
					if (!flag7)
					{
						base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_InvalidNumber));
					}
					num = 0UL;
				}
				else
				{
					num = this.GetValueUInt64(text, flag, flag2);
				}
				if (!flag5 && !flag6)
				{
					if (num <= 2147483647UL)
					{
						info.ValueKind = SpecialType.System_Int32;
						info.IntValue = (int)num;
					}
					else if (num <= (ulong)(-1))
					{
						info.ValueKind = SpecialType.System_UInt32;
						info.UintValue = (uint)num;
					}
					else if (num <= 9223372036854775807UL)
					{
						info.ValueKind = SpecialType.System_Int64;
						info.LongValue = (long)num;
					}
					else
					{
						info.ValueKind = SpecialType.System_UInt64;
						info.UlongValue = num;
					}
				}
				else if (flag5 && !flag6)
				{
					if (num <= (ulong)(-1))
					{
						info.ValueKind = SpecialType.System_UInt32;
						info.UintValue = (uint)num;
					}
					else
					{
						info.ValueKind = SpecialType.System_UInt64;
						info.UlongValue = num;
					}
				}
				else if (!flag5 && flag6)
				{
					if (num <= 9223372036854775807UL)
					{
						info.ValueKind = SpecialType.System_Int64;
						info.LongValue = (long)num;
					}
					else
					{
						info.ValueKind = SpecialType.System_UInt64;
						info.UlongValue = num;
					}
				}
				else
				{
					info.ValueKind = SpecialType.System_UInt64;
					info.UlongValue = num;
				}
				break;
			}
			}
			return true;
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x00131F00 File Offset: 0x00130100
		private static bool TryParseBinaryUInt64(string text, out ulong value)
		{
			value = 0UL;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text.get_Chars(i);
				if ((value & 9223372036854775808UL) != 0UL)
				{
					return false;
				}
				ulong num = (ulong)((long)SyntaxFacts.BinaryValue(c));
				value = (value << 1) | num;
			}
			return true;
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x00131F50 File Offset: 0x00130150
		private int GetValueInt32(string text, bool isHex)
		{
			int num;
			if (!int.TryParse(text, isHex ? 512 : 0, CultureInfo.InvariantCulture, ref num))
			{
				base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_IntOverflow));
			}
			return num;
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x00131F88 File Offset: 0x00130188
		private ulong GetValueUInt64(string text, bool isHex, bool isBinary)
		{
			ulong num;
			if (isBinary)
			{
				if (!Lexer.TryParseBinaryUInt64(text, out num))
				{
					base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_IntOverflow));
				}
			}
			else if (!ulong.TryParse(text, isHex ? 512 : 0, CultureInfo.InvariantCulture, ref num))
			{
				base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_IntOverflow));
			}
			return num;
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x00131FE0 File Offset: 0x001301E0
		private double GetValueDouble(string text)
		{
			double num;
			if (!RealParser.TryParseDouble(text, out num))
			{
				base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_FloatOverflow, new object[] { "double" }));
			}
			return num;
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x00132018 File Offset: 0x00130218
		private float GetValueSingle(string text)
		{
			float num;
			if (!RealParser.TryParseFloat(text, out num))
			{
				base.AddError(AbstractLexer.MakeError(ErrorCode.ERR_FloatOverflow, new object[] { "float" }));
			}
			return num;
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x00132050 File Offset: 0x00130250
		private decimal GetValueDecimal(string text, int start, int end)
		{
			decimal num;
			if (!decimal.TryParse(text, 160, CultureInfo.InvariantCulture, ref num))
			{
				base.AddError(base.MakeError(start, end - start, ErrorCode.ERR_FloatOverflow, new object[] { "decimal" }));
			}
			return num;
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x00132095 File Offset: 0x00130295
		private void ResetIdentBuffer()
		{
			this._identLen = 0;
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x001320A0 File Offset: 0x001302A0
		private void AddIdentChar(char ch)
		{
			if (this._identLen >= this._identBuffer.Length)
			{
				this.GrowIdentBuffer();
			}
			char[] identBuffer = this._identBuffer;
			int identLen = this._identLen;
			this._identLen = identLen + 1;
			identBuffer[identLen] = ch;
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x001320DC File Offset: 0x001302DC
		private void GrowIdentBuffer()
		{
			char[] array = new char[this._identBuffer.Length * 2];
			Array.Copy(this._identBuffer, array, this._identBuffer.Length);
			this._identBuffer = array;
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x00132114 File Offset: 0x00130314
		private bool ScanIdentifier(ref Lexer.TokenInfo info)
		{
			if (this.ScanIdentifier_FastPath(ref info))
			{
				return true;
			}
			if (!this.InXmlCrefOrNameAttributeValue)
			{
				return this.ScanIdentifier_SlowPath(ref info);
			}
			return this.ScanIdentifier_CrefSlowPath(ref info);
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x00132138 File Offset: 0x00130338
		private bool ScanIdentifier_FastPath(ref Lexer.TokenInfo info)
		{
			if ((this._mode & LexerMode.MaskLexMode) == LexerMode.DebuggerSyntax)
			{
				return false;
			}
			int num = this.TextWindow.Offset;
			char[] characterWindow = this.TextWindow.CharacterWindow;
			int characterWindowCount = this.TextWindow.CharacterWindowCount;
			int num2 = num;
			while (num != characterWindowCount)
			{
				switch (characterWindow[num])
				{
				case '\0':
				case '\t':
				case '\n':
				case '\r':
				case ' ':
				case '!':
				case '"':
				case '%':
				case '\'':
				case '(':
				case ')':
				case '*':
				case '+':
				case ',':
				case '-':
				case '.':
				case '/':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '[':
				case ']':
				case '^':
				case '{':
				case '|':
				case '}':
				case '~':
					goto IL_0251;
				case '\u0001':
				case '\u0002':
				case '\u0003':
				case '\u0004':
				case '\u0005':
				case '\u0006':
				case '\a':
				case '\b':
				case '\v':
				case '\f':
				case '\u000e':
				case '\u000f':
				case '\u0010':
				case '\u0011':
				case '\u0012':
				case '\u0013':
				case '\u0014':
				case '\u0015':
				case '\u0016':
				case '\u0017':
				case '\u0018':
				case '\u0019':
				case '\u001a':
				case '\u001b':
				case '\u001c':
				case '\u001d':
				case '\u001e':
				case '\u001f':
				case '#':
				case '$':
				case '@':
				case '\\':
				case '`':
					return false;
				case '&':
					if (this.InXmlCrefOrNameAttributeValue)
					{
						return false;
					}
					goto IL_0251;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					if (num == num2)
					{
						return false;
					}
					break;
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
				case 'G':
				case 'H':
				case 'I':
				case 'J':
				case 'K':
				case 'L':
				case 'M':
				case 'N':
				case 'O':
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
				case 'U':
				case 'V':
				case 'W':
				case 'X':
				case 'Y':
				case 'Z':
				case '_':
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
				case 'g':
				case 'h':
				case 'i':
				case 'j':
				case 'k':
				case 'l':
				case 'm':
				case 'n':
				case 'o':
				case 'p':
				case 'q':
				case 'r':
				case 's':
				case 't':
				case 'u':
				case 'v':
				case 'w':
				case 'x':
				case 'y':
				case 'z':
					break;
				default:
					return false;
				}
				num++;
				continue;
				IL_0251:
				int num3 = num - num2;
				this.TextWindow.AdvanceChar(num3);
				info.Text = (info.StringValue = this.TextWindow.Intern(characterWindow, num2, num3));
				info.IsVerbatim = false;
				return true;
			}
			return false;
		}

		// Token: 0x06004A92 RID: 19090 RVA: 0x001323E4 File Offset: 0x001305E4
		private bool ScanIdentifier_SlowPath(ref Lexer.TokenInfo info)
		{
			int position = this.TextWindow.Position;
			this.ResetIdentBuffer();
			info.IsVerbatim = this.TextWindow.PeekChar() == '@';
			if (info.IsVerbatim)
			{
				this.TextWindow.AdvanceChar();
			}
			bool flag = false;
			for (;;)
			{
				char maxValue = char.MaxValue;
				bool flag2 = false;
				char c = this.TextWindow.PeekChar();
				for (;;)
				{
					switch (c)
					{
					case '\t':
					case ' ':
					case '(':
					case ')':
					case ',':
					case '.':
					case ';':
						goto IL_03EF;
					case '\n':
					case '\v':
					case '\f':
					case '\r':
					case '\u000e':
					case '\u000f':
					case '\u0010':
					case '\u0011':
					case '\u0012':
					case '\u0013':
					case '\u0014':
					case '\u0015':
					case '\u0016':
					case '\u0017':
					case '\u0018':
					case '\u0019':
					case '\u001a':
					case '\u001b':
					case '\u001c':
					case '\u001d':
					case '\u001e':
					case '\u001f':
					case '!':
					case '"':
					case '#':
					case '%':
					case '&':
					case '\'':
					case '*':
					case '+':
					case '-':
					case '/':
					case ':':
					case '=':
					case '>':
					case '?':
					case '@':
					case '[':
					case ']':
					case '^':
					case '`':
						goto IL_0335;
					case '$':
						goto IL_0267;
					case '0':
						goto IL_0299;
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						goto IL_02DA;
					case '<':
						goto IL_02EA;
					case 'A':
					case 'B':
					case 'C':
					case 'D':
					case 'E':
					case 'F':
					case 'G':
					case 'H':
					case 'I':
					case 'J':
					case 'K':
					case 'L':
					case 'M':
					case 'N':
					case 'O':
					case 'P':
					case 'Q':
					case 'R':
					case 'S':
					case 'T':
					case 'U':
					case 'V':
					case 'W':
					case 'X':
					case 'Y':
					case 'Z':
					case '_':
					case 'a':
					case 'b':
					case 'c':
					case 'd':
					case 'e':
					case 'f':
					case 'g':
					case 'h':
					case 'i':
					case 'j':
					case 'k':
					case 'l':
					case 'm':
					case 'n':
					case 'o':
					case 'p':
					case 'q':
					case 'r':
					case 's':
					case 't':
					case 'u':
					case 'v':
					case 'w':
					case 'x':
					case 'y':
					case 'z':
						goto IL_03A7;
					case '\\':
						if (!flag2 && this.TextWindow.IsUnicodeEscape())
						{
							info.HasIdentifierEscapeSequence = true;
							flag2 = true;
							c = this.TextWindow.PeekUnicodeEscape(out maxValue);
							continue;
						}
						goto IL_0335;
					}
					goto Block_2;
				}
				IL_02EA:
				if (this._identLen == 0 && this.ModeIs(LexerMode.DebuggerSyntax) && this.TextWindow.PeekChar(1) == '>')
				{
					this.TextWindow.AdvanceChar(2);
					this.AddIdentChar('<');
					this.AddIdentChar('>');
					continue;
				}
				break;
				IL_0335:
				if (this._identLen == 0 && c > '\u007f' && SyntaxFacts.IsIdentifierStartCharacter(c))
				{
					goto IL_03A7;
				}
				if (this._identLen <= 0 || c <= '\u007f' || !SyntaxFacts.IsIdentifierPartCharacter(c))
				{
					break;
				}
				if (!UnicodeCharacterUtilities.IsFormattingChar(c))
				{
					goto IL_03A7;
				}
				if (flag2)
				{
					SyntaxDiagnosticInfo syntaxDiagnosticInfo;
					this.TextWindow.NextCharOrUnicodeEscape(out maxValue, out syntaxDiagnosticInfo);
					base.AddError(syntaxDiagnosticInfo);
					continue;
				}
				this.TextWindow.AdvanceChar();
				continue;
				Block_2:
				if (c != '\uffff')
				{
					goto IL_0335;
				}
				if (!this.TextWindow.IsReallyAtEnd())
				{
					goto IL_0335;
				}
				break;
				IL_03A7:
				if (flag2)
				{
					SyntaxDiagnosticInfo syntaxDiagnosticInfo2;
					this.TextWindow.NextCharOrUnicodeEscape(out maxValue, out syntaxDiagnosticInfo2);
					base.AddError(syntaxDiagnosticInfo2);
				}
				else
				{
					this.TextWindow.AdvanceChar();
				}
				this.AddIdentChar(c);
				if (maxValue != '\uffff')
				{
					this.AddIdentChar(maxValue);
					continue;
				}
				continue;
				IL_0299:
				if (this._identLen != 0)
				{
					goto IL_03A7;
				}
				if (info.IsVerbatim && this.ModeIs(LexerMode.DebuggerSyntax) && char.ToLower(this.TextWindow.PeekChar(1)) == 'x')
				{
					flag = true;
					goto IL_03A7;
				}
				break;
				IL_0267:
				if (!this.ModeIs(LexerMode.DebuggerSyntax))
				{
					break;
				}
				if (this._identLen > 0)
				{
					break;
				}
				goto IL_03A7;
				IL_02DA:
				if (this._identLen == 0)
				{
					break;
				}
				goto IL_03A7;
			}
			IL_03EF:
			int width = this.TextWindow.Width;
			if (this._identLen > 0)
			{
				info.Text = this.TextWindow.GetInternedText();
				if (this._identLen == width)
				{
					info.StringValue = info.Text;
				}
				else
				{
					info.StringValue = this.TextWindow.Intern(this._identBuffer, 0, this._identLen);
				}
				if (flag)
				{
					string text = this.TextWindow.Intern(this._identBuffer, 2, this._identLen - 2);
					if (text.Length == 0 || !text.All(new Predicate<char>(Lexer.IsValidHexDigit)))
					{
						goto IL_0497;
					}
					this.GetValueUInt64(text, true, false);
				}
				return true;
			}
			IL_0497:
			info.Text = null;
			info.StringValue = null;
			this.TextWindow.Reset(position);
			return false;
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x001328A3 File Offset: 0x00130AA3
		private static bool IsValidHexDigit(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return true;
			}
			c = char.ToLower(c);
			return c >= 'a' && c <= 'f';
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x001328C8 File Offset: 0x00130AC8
		private bool ScanIdentifier_CrefSlowPath(ref Lexer.TokenInfo info)
		{
			int position = this.TextWindow.Position;
			this.ResetIdentBuffer();
			if (this.AdvanceIfMatches('@'))
			{
				if (this.InXmlNameAttributeValue)
				{
					this.AddIdentChar('@');
				}
				else
				{
					info.IsVerbatim = true;
				}
			}
			int position2;
			for (;;)
			{
				position2 = this.TextWindow.Position;
				char c;
				char maxValue;
				if (this.TextWindow.PeekChar() == '&')
				{
					if (!this.TextWindow.TryScanXmlEntity(out c, out maxValue))
					{
						break;
					}
				}
				else
				{
					c = this.TextWindow.NextChar();
					maxValue = char.MaxValue;
				}
				bool flag = false;
				for (;;)
				{
					switch (c)
					{
					case '\t':
					case ' ':
					case '$':
					case '(':
					case ')':
					case ',':
					case '.':
					case ';':
					case '<':
						goto IL_02EF;
					case '\n':
					case '\v':
					case '\f':
					case '\r':
					case '\u000e':
					case '\u000f':
					case '\u0010':
					case '\u0011':
					case '\u0012':
					case '\u0013':
					case '\u0014':
					case '\u0015':
					case '\u0016':
					case '\u0017':
					case '\u0018':
					case '\u0019':
					case '\u001a':
					case '\u001b':
					case '\u001c':
					case '\u001d':
					case '\u001e':
					case '\u001f':
					case '!':
					case '"':
					case '#':
					case '%':
					case '&':
					case '\'':
					case '*':
					case '+':
					case '-':
					case '/':
					case ':':
					case '=':
					case '>':
					case '?':
					case '@':
					case '[':
					case ']':
					case '^':
					case '`':
						goto IL_0318;
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						goto IL_02D3;
					case 'A':
					case 'B':
					case 'C':
					case 'D':
					case 'E':
					case 'F':
					case 'G':
					case 'H':
					case 'I':
					case 'J':
					case 'K':
					case 'L':
					case 'M':
					case 'N':
					case 'O':
					case 'P':
					case 'Q':
					case 'R':
					case 'S':
					case 'T':
					case 'U':
					case 'V':
					case 'W':
					case 'X':
					case 'Y':
					case 'Z':
					case '_':
					case 'a':
					case 'b':
					case 'c':
					case 'd':
					case 'e':
					case 'f':
					case 'g':
					case 'h':
					case 'i':
					case 'j':
					case 'k':
					case 'l':
					case 'm':
					case 'n':
					case 'o':
					case 'p':
					case 'q':
					case 'r':
					case 's':
					case 't':
					case 'u':
					case 'v':
					case 'w':
					case 'x':
					case 'y':
					case 'z':
						goto IL_035E;
					case '\\':
						if (!flag && this.TextWindow.Position == position2 + 1 && (this.TextWindow.PeekChar() == 'u' || this.TextWindow.PeekChar() == 'U'))
						{
							info.HasIdentifierEscapeSequence = true;
							this.TextWindow.Reset(position2);
							flag = true;
							SyntaxDiagnosticInfo syntaxDiagnosticInfo;
							c = this.TextWindow.NextUnicodeEscape(out maxValue, out syntaxDiagnosticInfo);
							this.AddCrefError(syntaxDiagnosticInfo);
							continue;
						}
						goto IL_0318;
					}
					goto Block_5;
				}
				IL_0318:
				if (this._identLen != 0 || c <= '\u007f' || !SyntaxFacts.IsIdentifierStartCharacter(c))
				{
					if (this._identLen <= 0 || c <= '\u007f' || !SyntaxFacts.IsIdentifierPartCharacter(c))
					{
						goto IL_0350;
					}
					if (UnicodeCharacterUtilities.IsFormattingChar(c))
					{
						continue;
					}
				}
				IL_035E:
				this.AddIdentChar(c);
				if (maxValue != '\uffff')
				{
					this.AddIdentChar(maxValue);
					continue;
				}
				continue;
				IL_02D3:
				if (this._identLen == 0)
				{
					goto Block_10;
				}
				goto IL_035E;
				Block_5:
				if (c != '\uffff')
				{
					goto IL_0318;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					goto Block_11;
				}
				goto IL_0318;
			}
			this.TextWindow.Reset(position2);
			goto IL_037C;
			Block_10:
			this.TextWindow.Reset(position2);
			goto IL_037C;
			IL_02EF:
			this.TextWindow.Reset(position2);
			goto IL_037C;
			Block_11:
			this.TextWindow.Reset(position2);
			goto IL_037C;
			IL_0350:
			this.TextWindow.Reset(position2);
			IL_037C:
			if (this._identLen > 0)
			{
				int width = this.TextWindow.Width;
				if (this._identLen == width)
				{
					info.StringValue = this.TextWindow.GetInternedText();
					info.Text = info.StringValue;
				}
				else
				{
					info.StringValue = this.TextWindow.Intern(this._identBuffer, 0, this._identLen);
					info.Text = this.TextWindow.GetText(false);
				}
				return true;
			}
			info.Text = null;
			info.StringValue = null;
			this.TextWindow.Reset(position);
			return false;
		}

		// Token: 0x06004A95 RID: 19093 RVA: 0x00132CE0 File Offset: 0x00130EE0
		private bool ScanIdentifierOrKeyword(ref Lexer.TokenInfo info)
		{
			info.ContextualKind = SyntaxKind.None;
			if (this.ScanIdentifier(ref info))
			{
				if (!info.IsVerbatim && !info.HasIdentifierEscapeSequence)
				{
					if (this.ModeIs(LexerMode.Directive))
					{
						SyntaxKind preprocessorKeywordKind = SyntaxFacts.GetPreprocessorKeywordKind(info.Text);
						if (SyntaxFacts.IsPreprocessorContextualKeyword(preprocessorKeywordKind))
						{
							info.Kind = SyntaxKind.IdentifierToken;
							info.ContextualKind = preprocessorKeywordKind;
						}
						else
						{
							info.Kind = preprocessorKeywordKind;
						}
					}
					else if (!this._cache.TryGetKeywordKind(info.Text, out info.Kind))
					{
						info.ContextualKind = (info.Kind = SyntaxKind.IdentifierToken);
					}
					else if (SyntaxFacts.IsContextualKeyword(info.Kind))
					{
						info.ContextualKind = info.Kind;
						info.Kind = SyntaxKind.IdentifierToken;
					}
					if (info.Kind == SyntaxKind.None)
					{
						info.Kind = SyntaxKind.IdentifierToken;
					}
				}
				else
				{
					info.ContextualKind = (info.Kind = SyntaxKind.IdentifierToken);
				}
				return true;
			}
			info.Kind = SyntaxKind.None;
			return false;
		}

		// Token: 0x06004A96 RID: 19094 RVA: 0x00132DD8 File Offset: 0x00130FD8
		private void LexSyntaxTrivia(bool afterFirstToken, bool isTrailing, ref SyntaxListBuilder triviaList)
		{
			bool flag = !isTrailing;
			for (;;)
			{
				base.Start();
				char c = this.TextWindow.PeekChar();
				if (c == ' ')
				{
					this.AddTrivia(this.ScanWhitespace(), ref triviaList);
				}
				else
				{
					if (c > '\u007f')
					{
						if (SyntaxFacts.IsWhitespace(c))
						{
							c = ' ';
						}
						else if (SyntaxFacts.IsNewLine(c))
						{
							c = '\n';
						}
					}
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
						case '\v':
						case '\f':
							break;
						case '\n':
						case '\r':
							this.AddTrivia(this.ScanEndOfLine(), ref triviaList);
							if (isTrailing)
							{
								return;
							}
							flag = true;
							continue;
						default:
							if (c != '\u001a' && c != ' ')
							{
								return;
							}
							break;
						}
						this.AddTrivia(this.ScanWhitespace(), ref triviaList);
					}
					else if (c <= '/')
					{
						if (c != '#')
						{
							if (c != '/')
							{
								return;
							}
							if ((c = this.TextWindow.PeekChar(1)) == '/')
							{
								if (!this.SuppressDocumentationCommentParse && this.TextWindow.PeekChar(2) == '/' && this.TextWindow.PeekChar(3) != '/')
								{
									if (isTrailing)
									{
										return;
									}
									this.AddTrivia(this.LexXmlDocComment(XmlDocCommentStyle.SingleLine), ref triviaList);
								}
								else
								{
									this.ScanToEndOfLine();
									string text = this.TextWindow.GetText(false);
									this.AddTrivia(SyntaxFactory.Comment(text), ref triviaList);
									flag = false;
								}
							}
							else
							{
								if (c != '*')
								{
									return;
								}
								if (!this.SuppressDocumentationCommentParse && this.TextWindow.PeekChar(2) == '*' && this.TextWindow.PeekChar(3) != '*' && this.TextWindow.PeekChar(3) != '/')
								{
									if (isTrailing)
									{
										return;
									}
									this.AddTrivia(this.LexXmlDocComment(XmlDocCommentStyle.Delimited), ref triviaList);
								}
								else
								{
									bool flag2;
									this.ScanMultiLineComment(out flag2);
									if (!flag2)
									{
										base.AddError(ErrorCode.ERR_OpenEndedComment);
									}
									string text2 = this.TextWindow.GetText(false);
									this.AddTrivia(SyntaxFactory.Comment(text2), ref triviaList);
									flag = false;
								}
							}
						}
						else
						{
							if (!this._allowPreprocessorDirectives)
							{
								return;
							}
							this.LexDirectiveAndExcludedTrivia(afterFirstToken, isTrailing || !flag, ref triviaList);
						}
					}
					else
					{
						if (c != '<' && c != '=')
						{
							return;
						}
						if (isTrailing || !this.IsConflictMarkerTrivia())
						{
							return;
						}
						this.LexConflictMarkerTrivia(ref triviaList);
					}
				}
			}
		}

		// Token: 0x06004A97 RID: 19095 RVA: 0x00132FEC File Offset: 0x001311EC
		private bool IsConflictMarkerTrivia()
		{
			int position = this.TextWindow.Position;
			SourceText text = this.TextWindow.Text;
			if (position == 0 || SyntaxFacts.IsNewLine(text[position - 1]))
			{
				char c = text[position];
				if (position + Lexer.s_conflictMarkerLength <= text.Length)
				{
					int i = 0;
					int num = Lexer.s_conflictMarkerLength;
					while (i < num)
					{
						if (text[position + i] != c)
						{
							return false;
						}
						i++;
					}
					return c == '=' || (position + Lexer.s_conflictMarkerLength < text.Length && text[position + Lexer.s_conflictMarkerLength] == ' ');
				}
			}
			return false;
		}

		// Token: 0x06004A98 RID: 19096 RVA: 0x00133088 File Offset: 0x00131288
		private void LexConflictMarkerTrivia(ref SyntaxListBuilder triviaList)
		{
			base.Start();
			base.AddError(this.TextWindow.Position, Lexer.s_conflictMarkerLength, ErrorCode.ERR_Merge_conflict_marker_encountered);
			int num = (int)this.TextWindow.PeekChar();
			this.LexConflictMarkerHeader(ref triviaList);
			this.LexConflictMarkerEndOfLine(ref triviaList);
			if (num == 61)
			{
				this.LexConflictMarkerDisabledText(ref triviaList);
			}
		}

		// Token: 0x06004A99 RID: 19097 RVA: 0x001330DC File Offset: 0x001312DC
		private SyntaxListBuilder LexConflictMarkerDisabledText(ref SyntaxListBuilder triviaList)
		{
			base.Start();
			bool flag = false;
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c == '\uffff')
				{
					goto IL_003A;
				}
				if (c == '>' && this.IsConflictMarkerTrivia())
				{
					break;
				}
				this.TextWindow.AdvanceChar();
			}
			flag = true;
			IL_003A:
			if (this.TextWindow.Width > 0)
			{
				this.AddTrivia(SyntaxFactory.DisabledText(this.TextWindow.GetText(false)), ref triviaList);
			}
			if (flag)
			{
				this.LexConflictMarkerTrivia(ref triviaList);
			}
			return triviaList;
		}

		// Token: 0x06004A9A RID: 19098 RVA: 0x00133158 File Offset: 0x00131358
		private void LexConflictMarkerEndOfLine(ref SyntaxListBuilder triviaList)
		{
			base.Start();
			while (SyntaxFacts.IsNewLine(this.TextWindow.PeekChar()))
			{
				this.TextWindow.AdvanceChar();
			}
			if (this.TextWindow.Width > 0)
			{
				this.AddTrivia(SyntaxFactory.EndOfLine(this.TextWindow.GetText(false), false), ref triviaList);
			}
		}

		// Token: 0x06004A9B RID: 19099 RVA: 0x001331B4 File Offset: 0x001313B4
		private void LexConflictMarkerHeader(ref SyntaxListBuilder triviaList)
		{
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c == '\uffff' || SyntaxFacts.IsNewLine(c))
				{
					break;
				}
				this.TextWindow.AdvanceChar();
			}
			this.AddTrivia(SyntaxFactory.ConflictMarker(this.TextWindow.GetText(false)), ref triviaList);
		}

		// Token: 0x06004A9C RID: 19100 RVA: 0x00133204 File Offset: 0x00131404
		private void AddTrivia(CSharpSyntaxNode trivia, ref SyntaxListBuilder list)
		{
			if (base.HasErrors)
			{
				CSharpSyntaxNode csharpSyntaxNode = trivia;
				DiagnosticInfo[] errors = base.GetErrors(0);
				trivia = csharpSyntaxNode.WithDiagnosticsGreen(errors);
			}
			if (list == null)
			{
				list = new SyntaxListBuilder(8);
			}
			list.Add(trivia);
		}

		// Token: 0x06004A9D RID: 19101 RVA: 0x00133240 File Offset: 0x00131440
		private bool ScanMultiLineComment(out bool isTerminated)
		{
			if (this.TextWindow.PeekChar() == '/' && this.TextWindow.PeekChar(1) == '*')
			{
				this.TextWindow.AdvanceChar(2);
				char c;
				while ((c = this.TextWindow.PeekChar()) != '\uffff' || !this.TextWindow.IsReallyAtEnd())
				{
					if (c == '*' && this.TextWindow.PeekChar(1) == '/')
					{
						this.TextWindow.AdvanceChar(2);
						isTerminated = true;
						return true;
					}
					this.TextWindow.AdvanceChar();
				}
				isTerminated = false;
				return true;
			}
			isTerminated = false;
			return false;
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x001332D8 File Offset: 0x001314D8
		private void ScanToEndOfLine()
		{
			char c;
			while (!SyntaxFacts.IsNewLine(c = this.TextWindow.PeekChar()) && (c != '\uffff' || !this.TextWindow.IsReallyAtEnd()))
			{
				this.TextWindow.AdvanceChar();
			}
		}

		// Token: 0x06004A9F RID: 19103 RVA: 0x0013331C File Offset: 0x0013151C
		private CSharpSyntaxNode ScanEndOfLine()
		{
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 == '\n')
			{
				this.TextWindow.AdvanceChar();
				return SyntaxFactory.LineFeed;
			}
			if (c2 == '\r')
			{
				this.TextWindow.AdvanceChar();
				if (this.TextWindow.PeekChar() == '\n')
				{
					this.TextWindow.AdvanceChar();
					return SyntaxFactory.CarriageReturnLineFeed;
				}
				return SyntaxFactory.CarriageReturn;
			}
			else
			{
				if (SyntaxFacts.IsNewLine(c))
				{
					this.TextWindow.AdvanceChar();
					return SyntaxFactory.EndOfLine(c.ToString(), false);
				}
				return null;
			}
		}

		// Token: 0x06004AA0 RID: 19104 RVA: 0x001333A8 File Offset: 0x001315A8
		private SyntaxTrivia ScanWhitespace()
		{
			if (this._createWhitespaceTriviaFunction == null)
			{
				this._createWhitespaceTriviaFunction = new Func<SyntaxTrivia>(this.CreateWhitespaceTrivia);
			}
			int num = -2128831035;
			bool flag = true;
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				switch (c)
				{
				case '\t':
				case '\v':
				case '\f':
					goto IL_0057;
				case '\n':
				case '\r':
					goto IL_007B;
				default:
					if (c == '\u001a')
					{
						goto IL_0057;
					}
					if (c != ' ')
					{
						if (c <= '\u007f' || !SyntaxFacts.IsWhitespace(c))
						{
							goto IL_007B;
						}
						goto IL_0057;
					}
					break;
				}
				IL_0059:
				this.TextWindow.AdvanceChar();
				num = Hash.CombineFNVHash(num, c);
				continue;
				IL_0057:
				flag = false;
				goto IL_0059;
			}
			IL_007B:
			if (this.TextWindow.Width == 1 && flag)
			{
				return SyntaxFactory.Space;
			}
			int width = this.TextWindow.Width;
			if (width < 42)
			{
				return this._cache.LookupTrivia(this.TextWindow.CharacterWindow, this.TextWindow.LexemeRelativeStart, width, num, this._createWhitespaceTriviaFunction);
			}
			return this._createWhitespaceTriviaFunction.Invoke();
		}

		// Token: 0x06004AA1 RID: 19105 RVA: 0x0013348E File Offset: 0x0013168E
		private SyntaxTrivia CreateWhitespaceTrivia()
		{
			return SyntaxFactory.Whitespace(this.TextWindow.GetText(true), false);
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x001334A4 File Offset: 0x001316A4
		private void LexDirectiveAndExcludedTrivia(bool afterFirstToken, bool afterNonWhitespaceOnLine, ref SyntaxListBuilder triviaList)
		{
			BranchingDirectiveTriviaSyntax branchingDirectiveTriviaSyntax = this.LexSingleDirective(true, true, afterFirstToken, afterNonWhitespaceOnLine, ref triviaList) as BranchingDirectiveTriviaSyntax;
			if (branchingDirectiveTriviaSyntax != null && !branchingDirectiveTriviaSyntax.BranchTaken)
			{
				this.LexExcludedDirectivesAndTrivia(true, ref triviaList);
			}
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x001334D8 File Offset: 0x001316D8
		private void LexExcludedDirectivesAndTrivia(bool endIsActive, ref SyntaxListBuilder triviaList)
		{
			for (;;)
			{
				bool flag;
				CSharpSyntaxNode csharpSyntaxNode = this.LexDisabledText(out flag);
				if (csharpSyntaxNode != null)
				{
					this.AddTrivia(csharpSyntaxNode, ref triviaList);
				}
				if (!flag)
				{
					break;
				}
				CSharpSyntaxNode csharpSyntaxNode2 = this.LexSingleDirective(false, endIsActive, false, false, ref triviaList);
				BranchingDirectiveTriviaSyntax branchingDirectiveTriviaSyntax = csharpSyntaxNode2 as BranchingDirectiveTriviaSyntax;
				if (csharpSyntaxNode2.Kind == SyntaxKind.EndIfDirectiveTrivia || (branchingDirectiveTriviaSyntax != null && branchingDirectiveTriviaSyntax.BranchTaken))
				{
					break;
				}
				if (csharpSyntaxNode2.Kind == SyntaxKind.IfDirectiveTrivia)
				{
					this.LexExcludedDirectivesAndTrivia(false, ref triviaList);
				}
			}
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x00133540 File Offset: 0x00131740
		private CSharpSyntaxNode LexSingleDirective(bool isActive, bool endIsActive, bool afterFirstToken, bool afterNonWhitespaceOnLine, ref SyntaxListBuilder triviaList)
		{
			if (SyntaxFacts.IsWhitespace(this.TextWindow.PeekChar()))
			{
				base.Start();
				this.AddTrivia(this.ScanWhitespace(), ref triviaList);
			}
			LexerMode mode = this._mode;
			CSharpSyntaxNode csharpSyntaxNode;
			using (DirectiveParser directiveParser = new DirectiveParser(this, this._directives))
			{
				csharpSyntaxNode = directiveParser.ParseDirective(isActive, endIsActive, afterFirstToken, afterNonWhitespaceOnLine);
			}
			this.AddTrivia(csharpSyntaxNode, ref triviaList);
			this._directives = csharpSyntaxNode.ApplyDirectives(this._directives);
			this._mode = mode;
			return csharpSyntaxNode;
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x001335D4 File Offset: 0x001317D4
		private CSharpSyntaxNode LexDisabledText(out bool followedByDirective)
		{
			base.Start();
			int num = this.TextWindow.Position;
			int num2 = 0;
			bool flag = true;
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c <= '\r')
				{
					if (c != '\n' && c != '\r')
					{
						goto IL_00E9;
					}
				}
				else if (c != '#')
				{
					if (c == '\uffff' && this.TextWindow.IsReallyAtEnd())
					{
						break;
					}
					goto IL_00E9;
				}
				else
				{
					if (!this._allowPreprocessorDirectives)
					{
						goto IL_00E9;
					}
					followedByDirective = true;
					if (num >= this.TextWindow.Position || flag)
					{
						goto IL_009D;
					}
					goto IL_00E9;
				}
				IL_00CB:
				this.ScanEndOfLine();
				num = this.TextWindow.Position;
				flag = true;
				num2++;
				continue;
				IL_00E9:
				if (SyntaxFacts.IsNewLine(c))
				{
					goto IL_00CB;
				}
				flag = flag && SyntaxFacts.IsWhitespace(c);
				this.TextWindow.AdvanceChar();
			}
			followedByDirective = false;
			if (this.TextWindow.Width <= 0)
			{
				return null;
			}
			return SyntaxFactory.DisabledText(this.TextWindow.GetText(false));
			IL_009D:
			this.TextWindow.Reset(num);
			if (this.TextWindow.Width <= 0)
			{
				return null;
			}
			return SyntaxFactory.DisabledText(this.TextWindow.GetText(false));
		}

		// Token: 0x06004AA6 RID: 19110 RVA: 0x001336F0 File Offset: 0x001318F0
		private SyntaxToken LexDirectiveToken()
		{
			base.Start();
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			this.ScanDirectiveToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(0);
			SyntaxListBuilder syntaxListBuilder = this.LexDirectiveTrailingTrivia(tokenInfo.Kind == SyntaxKind.EndOfDirectiveToken);
			return this.Create(ref tokenInfo, null, syntaxListBuilder, errors);
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x0013373C File Offset: 0x0013193C
		private bool ScanDirectiveToken(ref Lexer.TokenInfo info)
		{
			bool flag = false;
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 <= '=')
			{
				if (c2 != '\n' && c2 != '\r')
				{
					switch (c2)
					{
					case '!':
						this.TextWindow.AdvanceChar();
						if (this.TextWindow.PeekChar() == '=')
						{
							this.TextWindow.AdvanceChar();
							info.Kind = SyntaxKind.ExclamationEqualsToken;
							goto IL_031D;
						}
						info.Kind = SyntaxKind.ExclamationToken;
						goto IL_031D;
					case '"':
						this.ScanStringLiteral(ref info, false);
						goto IL_031D;
					case '#':
						this.TextWindow.AdvanceChar();
						info.Kind = SyntaxKind.HashToken;
						goto IL_031D;
					case '$':
					case '%':
					case '\'':
					case '*':
					case '+':
					case '-':
					case '.':
					case '/':
					case ':':
					case ';':
					case '<':
						goto IL_02BC;
					case '&':
						if (this.TextWindow.PeekChar(1) == '&')
						{
							this.TextWindow.AdvanceChar(2);
							info.Kind = SyntaxKind.AmpersandAmpersandToken;
							goto IL_031D;
						}
						goto IL_02BC;
					case '(':
						this.TextWindow.AdvanceChar();
						info.Kind = SyntaxKind.OpenParenToken;
						goto IL_031D;
					case ')':
						this.TextWindow.AdvanceChar();
						info.Kind = SyntaxKind.CloseParenToken;
						goto IL_031D;
					case ',':
						this.TextWindow.AdvanceChar();
						info.Kind = SyntaxKind.CommaToken;
						goto IL_031D;
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						this.ScanInteger();
						info.Kind = SyntaxKind.NumericLiteralToken;
						info.Text = this.TextWindow.GetText(true);
						info.ValueKind = SpecialType.System_Int32;
						info.IntValue = this.GetValueInt32(info.Text, false);
						goto IL_031D;
					case '=':
						this.TextWindow.AdvanceChar();
						if (this.TextWindow.PeekChar() == '=')
						{
							this.TextWindow.AdvanceChar();
							info.Kind = SyntaxKind.EqualsEqualsToken;
							goto IL_031D;
						}
						info.Kind = SyntaxKind.EqualsToken;
						goto IL_031D;
					default:
						goto IL_02BC;
					}
				}
			}
			else if (c2 != '\\')
			{
				if (c2 != '|')
				{
					if (c2 == '\uffff' && this.TextWindow.IsReallyAtEnd())
					{
						info.Kind = SyntaxKind.EndOfDirectiveToken;
						goto IL_031D;
					}
					goto IL_02BC;
				}
				else
				{
					if (this.TextWindow.PeekChar(1) == '|')
					{
						this.TextWindow.AdvanceChar(2);
						info.Kind = SyntaxKind.BarBarToken;
						goto IL_031D;
					}
					goto IL_02BC;
				}
			}
			else
			{
				char c3;
				c = this.TextWindow.PeekCharOrUnicodeEscape(out c3);
				flag = true;
				if (SyntaxFacts.IsIdentifierStartCharacter(c))
				{
					this.ScanIdentifierOrKeyword(ref info);
					goto IL_031D;
				}
				goto IL_02BC;
			}
			IL_00E5:
			info.Kind = SyntaxKind.EndOfDirectiveToken;
			goto IL_031D;
			IL_02BC:
			if (!flag && SyntaxFacts.IsNewLine(c))
			{
				goto IL_00E5;
			}
			if (SyntaxFacts.IsIdentifierStartCharacter(c))
			{
				this.ScanIdentifierOrKeyword(ref info);
			}
			else
			{
				if (flag)
				{
					char c3;
					SyntaxDiagnosticInfo syntaxDiagnosticInfo;
					this.TextWindow.NextCharOrUnicodeEscape(out c3, out syntaxDiagnosticInfo);
					base.AddError(syntaxDiagnosticInfo);
				}
				else
				{
					this.TextWindow.AdvanceChar();
				}
				info.Kind = SyntaxKind.None;
				info.Text = this.TextWindow.GetText(true);
			}
			IL_031D:
			return info.Kind > SyntaxKind.None;
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x00133A70 File Offset: 0x00131C70
		private SyntaxListBuilder LexDirectiveTrailingTrivia(bool includeEndOfLine)
		{
			SyntaxListBuilder syntaxListBuilder = null;
			int position;
			CSharpSyntaxNode csharpSyntaxNode;
			for (;;)
			{
				position = this.TextWindow.Position;
				csharpSyntaxNode = this.LexDirectiveTrivia();
				if (csharpSyntaxNode == null)
				{
					return syntaxListBuilder;
				}
				if (csharpSyntaxNode.Kind == SyntaxKind.EndOfLineTrivia)
				{
					break;
				}
				this.AddTrivia(csharpSyntaxNode, ref syntaxListBuilder);
			}
			if (includeEndOfLine)
			{
				this.AddTrivia(csharpSyntaxNode, ref syntaxListBuilder);
			}
			else
			{
				this.TextWindow.Reset(position);
			}
			return syntaxListBuilder;
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x00133ACC File Offset: 0x00131CCC
		private CSharpSyntaxNode LexDirectiveTrivia()
		{
			CSharpSyntaxNode csharpSyntaxNode = null;
			base.Start();
			char c = this.TextWindow.PeekChar();
			switch (c)
			{
			case '\t':
			case '\v':
			case '\f':
				goto IL_006E;
			case '\n':
			case '\r':
				break;
			default:
				if (c == ' ')
				{
					goto IL_006E;
				}
				if (c == '/')
				{
					if (this.TextWindow.PeekChar(1) == '/')
					{
						this.ScanToEndOfLine();
						return SyntaxFactory.Comment(this.TextWindow.GetText(false));
					}
					return csharpSyntaxNode;
				}
				else
				{
					if (SyntaxFacts.IsWhitespace(c))
					{
						goto IL_006E;
					}
					if (!SyntaxFacts.IsNewLine(c))
					{
						return csharpSyntaxNode;
					}
				}
				break;
			}
			return this.ScanEndOfLine();
			IL_006E:
			csharpSyntaxNode = this.ScanWhitespace();
			return csharpSyntaxNode;
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00133B64 File Offset: 0x00131D64
		private CSharpSyntaxNode LexXmlDocComment(XmlDocCommentStyle style)
		{
			LexerMode mode = this._mode;
			LexerMode lexerMode = ((style == XmlDocCommentStyle.SingleLine) ? LexerMode.XmlDocCommentLocationStart : LexerMode.XmlDocCommentStyleDelimited);
			if (this._xmlParser == null)
			{
				this._xmlParser = new DocumentationCommentParser(this, lexerMode);
			}
			else
			{
				this._xmlParser.ReInitialize(lexerMode);
			}
			bool flag;
			CSharpSyntaxNode csharpSyntaxNode = this._xmlParser.ParseDocumentationComment(out flag);
			this._mode = mode;
			if (!flag)
			{
				base.AddError(this.TextWindow.LexemeStartPosition, this.TextWindow.Width, ErrorCode.ERR_OpenEndedComment);
			}
			return csharpSyntaxNode;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x00133BE0 File Offset: 0x00131DE0
		private SyntaxToken LexXmlToken()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTrivia(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x00133C28 File Offset: 0x00131E28
		private bool ScanXmlToken(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 <= '\r')
			{
				if (c2 != '\n' && c2 != '\r')
				{
					goto IL_0089;
				}
			}
			else
			{
				if (c2 == '&')
				{
					this.ScanXmlEntity(ref info);
					info.Kind = SyntaxKind.XmlEntityLiteralToken;
					goto IL_00A3;
				}
				if (c2 == '<')
				{
					this.ScanXmlTagStart(ref info);
					goto IL_00A3;
				}
				if (c2 != '\uffff')
				{
					goto IL_0089;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
					goto IL_00A3;
				}
				goto IL_0089;
			}
			IL_0066:
			this.ScanXmlTextLiteralNewLineToken(ref info);
			goto IL_00A3;
			IL_0089:
			if (SyntaxFacts.IsNewLine(c))
			{
				goto IL_0066;
			}
			this.ScanXmlText(ref info);
			info.Kind = SyntaxKind.XmlTextLiteralToken;
			IL_00A3:
			return info.Kind > SyntaxKind.None;
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x00133CE4 File Offset: 0x00131EE4
		private void ScanXmlTextLiteralNewLineToken(ref Lexer.TokenInfo info)
		{
			this.ScanEndOfLine();
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			info.Kind = SyntaxKind.XmlTextLiteralNewLineToken;
			this.MutateLocation(XmlDocCommentLocation.Exterior);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00133D28 File Offset: 0x00131F28
		private void ScanXmlTagStart(ref Lexer.TokenInfo info)
		{
			if (this.TextWindow.PeekChar(1) == '!')
			{
				if (this.TextWindow.PeekChar(2) == '-' && this.TextWindow.PeekChar(3) == '-')
				{
					this.TextWindow.AdvanceChar(4);
					info.Kind = SyntaxKind.XmlCommentStartToken;
					return;
				}
				if (this.TextWindow.PeekChar(2) == '[' && this.TextWindow.PeekChar(3) == 'C' && this.TextWindow.PeekChar(4) == 'D' && this.TextWindow.PeekChar(5) == 'A' && this.TextWindow.PeekChar(6) == 'T' && this.TextWindow.PeekChar(7) == 'A' && this.TextWindow.PeekChar(8) == '[')
				{
					this.TextWindow.AdvanceChar(9);
					info.Kind = SyntaxKind.XmlCDataStartToken;
					return;
				}
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.LessThanToken;
				return;
			}
			else
			{
				if (this.TextWindow.PeekChar(1) == '/')
				{
					this.TextWindow.AdvanceChar(2);
					info.Kind = SyntaxKind.LessThanSlashToken;
					return;
				}
				if (this.TextWindow.PeekChar(1) == '?')
				{
					this.TextWindow.AdvanceChar(2);
					info.Kind = SyntaxKind.XmlProcessingInstructionStartToken;
					return;
				}
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.LessThanToken;
				return;
			}
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00133E88 File Offset: 0x00132088
		private void ScanXmlEntity(ref Lexer.TokenInfo info)
		{
			info.StringValue = null;
			this.TextWindow.AdvanceChar();
			this._builder.Clear();
			XmlParseErrorCode? xmlParseErrorCode = default(XmlParseErrorCode?);
			object[] array = null;
			char c;
			if (Lexer.IsXmlNameStartChar(c = this.TextWindow.PeekChar()))
			{
				while (Lexer.IsXmlNameChar(c = this.TextWindow.PeekChar()))
				{
					this.TextWindow.AdvanceChar();
					this._builder.Append(c);
				}
				string text = this._builder.ToString();
				if (!(text == "lt"))
				{
					if (!(text == "gt"))
					{
						if (!(text == "amp"))
						{
							if (!(text == "apos"))
							{
								if (!(text == "quot"))
								{
									xmlParseErrorCode = new XmlParseErrorCode?(XmlParseErrorCode.XML_RefUndefinedEntity_1);
									object[] array2 = new string[] { this._builder.ToString() };
									array = array2;
								}
								else
								{
									info.StringValue = "\"";
								}
							}
							else
							{
								info.StringValue = "'";
							}
						}
						else
						{
							info.StringValue = "&";
						}
					}
					else
					{
						info.StringValue = ">";
					}
				}
				else
				{
					info.StringValue = "<";
				}
			}
			else if (c == '#')
			{
				this.TextWindow.AdvanceChar();
				bool flag = this.TextWindow.PeekChar() == 'x';
				uint num = 0U;
				if (flag)
				{
					this.TextWindow.AdvanceChar();
					while (SyntaxFacts.IsHexDigit(c = this.TextWindow.PeekChar()))
					{
						this.TextWindow.AdvanceChar();
						if (num <= 134217727U)
						{
							num = (num << 4) + (uint)SyntaxFacts.HexValue(c);
						}
					}
				}
				else
				{
					while (SyntaxFacts.IsDecDigit(c = this.TextWindow.PeekChar()))
					{
						this.TextWindow.AdvanceChar();
						if (num <= 134217727U)
						{
							num = (num << 3) + (num << 1) + (uint)SyntaxFacts.DecValue(c);
						}
					}
				}
				if (this.TextWindow.PeekChar() != ';')
				{
					xmlParseErrorCode = new XmlParseErrorCode?(XmlParseErrorCode.XML_InvalidCharEntity);
				}
				if (Lexer.MatchesProductionForXmlChar(num))
				{
					char c2;
					char charsFromUtf = SlidingTextWindow.GetCharsFromUtf32(num, out c2);
					this._builder.Append(charsFromUtf);
					if (c2 != '\uffff')
					{
						this._builder.Append(c2);
					}
					info.StringValue = this._builder.ToString();
				}
				else if (xmlParseErrorCode == null)
				{
					xmlParseErrorCode = new XmlParseErrorCode?(XmlParseErrorCode.XML_InvalidUnicodeChar);
				}
			}
			else if (SyntaxFacts.IsWhitespace(c) || SyntaxFacts.IsNewLine(c))
			{
				if (xmlParseErrorCode == null)
				{
					xmlParseErrorCode = new XmlParseErrorCode?(XmlParseErrorCode.XML_InvalidWhitespace);
				}
			}
			else if (xmlParseErrorCode == null)
			{
				xmlParseErrorCode = new XmlParseErrorCode?(XmlParseErrorCode.XML_InvalidToken);
				object[] array2 = new string[] { c.ToString() };
				array = array2;
			}
			c = this.TextWindow.PeekChar();
			if (c == ';')
			{
				this.TextWindow.AdvanceChar();
			}
			else if (xmlParseErrorCode == null)
			{
				xmlParseErrorCode = new XmlParseErrorCode?(XmlParseErrorCode.XML_InvalidToken);
				object[] array2 = new string[] { c.ToString() };
				array = array2;
			}
			info.Text = this.TextWindow.GetText(true);
			if (info.StringValue == null)
			{
				info.StringValue = info.Text;
			}
			if (xmlParseErrorCode != null)
			{
				base.AddError(xmlParseErrorCode.Value, array ?? Array.Empty<object>());
			}
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x001341B8 File Offset: 0x001323B8
		private static bool MatchesProductionForXmlChar(uint charValue)
		{
			return charValue == 9U || charValue == 10U || charValue == 13U || (charValue >= 32U && charValue <= 55295U) || (charValue >= 57344U && charValue <= 65533U) || (charValue >= 65536U && charValue <= 1114111U);
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00134208 File Offset: 0x00132408
		private void ScanXmlText(ref Lexer.TokenInfo info)
		{
			if (this.TextWindow.PeekChar() == ']' && this.TextWindow.PeekChar(1) == ']' && this.TextWindow.PeekChar(2) == '>')
			{
				this.TextWindow.AdvanceChar(3);
				info.StringValue = (info.Text = this.TextWindow.GetText(false));
				base.AddError(XmlParseErrorCode.XML_CDataEndTagNotAllowed);
				return;
			}
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c <= '&')
				{
					if (c == '\n' || c == '\r' || c == '&')
					{
						goto IL_00D7;
					}
				}
				else if (c <= '<')
				{
					if (c != '*')
					{
						if (c == '<')
						{
							goto IL_00D7;
						}
					}
					else if (this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar(1) == '/')
					{
						goto Block_15;
					}
				}
				else if (c != ']')
				{
					if (c == '\uffff' && this.TextWindow.IsReallyAtEnd())
					{
						break;
					}
				}
				else if (this.TextWindow.PeekChar(1) == ']' && this.TextWindow.PeekChar(2) == '>')
				{
					goto Block_17;
				}
				if (SyntaxFacts.IsNewLine(c))
				{
					goto IL_00D7;
				}
				this.TextWindow.AdvanceChar();
			}
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			IL_00D7:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_15:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_17:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x00134394 File Offset: 0x00132594
		private SyntaxToken LexXmlElementTagToken()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTriviaWithWhitespace(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlElementTagToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			if (errors == null && tokenInfo.ContextualKind == SyntaxKind.None && tokenInfo.Kind == SyntaxKind.IdentifierToken)
			{
				SyntaxToken syntaxToken = DocumentationCommentXmlTokens.LookupToken(tokenInfo.Text, syntaxListBuilder);
				if (syntaxToken != null)
				{
					return syntaxToken;
				}
			}
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x00134404 File Offset: 0x00132604
		private bool ScanXmlElementTagToken(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 <= '\'')
			{
				if (c2 <= '\r')
				{
					if (c2 == '\n' || c2 == '\r')
					{
						goto IL_01F6;
					}
				}
				else
				{
					if (c2 == '"')
					{
						this.TextWindow.AdvanceChar();
						info.Kind = SyntaxKind.DoubleQuoteToken;
						goto IL_01F6;
					}
					if (c2 == '\'')
					{
						this.TextWindow.AdvanceChar();
						info.Kind = SyntaxKind.SingleQuoteToken;
						goto IL_01F6;
					}
				}
			}
			else if (c2 <= '/')
			{
				if (c2 != '*')
				{
					if (c2 == '/')
					{
						if (this.TextWindow.PeekChar(1) == '>')
						{
							this.TextWindow.AdvanceChar(2);
							info.Kind = SyntaxKind.SlashGreaterThanToken;
							goto IL_01F6;
						}
					}
				}
				else if (this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar(1) == '/')
				{
					goto IL_01F6;
				}
			}
			else
			{
				switch (c2)
				{
				case ':':
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.ColonToken;
					goto IL_01F6;
				case ';':
					break;
				case '<':
					this.ScanXmlTagStart(ref info);
					goto IL_01F6;
				case '=':
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.EqualsToken;
					goto IL_01F6;
				case '>':
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.GreaterThanToken;
					goto IL_01F6;
				default:
					if (c2 == '\uffff')
					{
						if (this.TextWindow.IsReallyAtEnd())
						{
							info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
							goto IL_01F6;
						}
					}
					break;
				}
			}
			if (Lexer.IsXmlNameStartChar(c))
			{
				this.ScanXmlName(ref info);
				info.StringValue = info.Text;
				info.Kind = SyntaxKind.IdentifierToken;
			}
			else if (!SyntaxFacts.IsWhitespace(c) && !SyntaxFacts.IsNewLine(c))
			{
				this.TextWindow.AdvanceChar();
				info.Kind = SyntaxKind.None;
				info.StringValue = (info.Text = this.TextWindow.GetText(false));
			}
			IL_01F6:
			return info.Kind > SyntaxKind.None;
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00134610 File Offset: 0x00132810
		private void ScanXmlName(ref Lexer.TokenInfo info)
		{
			int position = this.TextWindow.Position;
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c == ':' || !Lexer.IsXmlNameChar(c))
				{
					break;
				}
				this.TextWindow.AdvanceChar();
			}
			info.Text = this.TextWindow.GetText(position, this.TextWindow.Position - position, true);
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0013466F File Offset: 0x0013286F
		private static bool IsXmlNameStartChar(char ch)
		{
			return XmlCharType.IsStartNCNameCharXml4e(ch);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x00134677 File Offset: 0x00132877
		private static bool IsXmlNameChar(char ch)
		{
			return XmlCharType.IsNCNameCharXml4e(ch);
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x00134680 File Offset: 0x00132880
		private SyntaxToken LexXmlAttributeTextToken()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTrivia(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlAttributeTextToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x001346C8 File Offset: 0x001328C8
		private bool ScanXmlAttributeTextToken(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 <= '"')
			{
				if (c2 != '\n' && c2 != '\r')
				{
					if (c2 != '"')
					{
						goto IL_0105;
					}
					if (this.ModeIs(LexerMode.XmlAttributeTextDoubleQuote))
					{
						this.TextWindow.AdvanceChar();
						info.Kind = SyntaxKind.DoubleQuoteToken;
						goto IL_011F;
					}
					goto IL_0105;
				}
			}
			else if (c2 <= '\'')
			{
				if (c2 == '&')
				{
					this.ScanXmlEntity(ref info);
					info.Kind = SyntaxKind.XmlEntityLiteralToken;
					goto IL_011F;
				}
				if (c2 != '\'')
				{
					goto IL_0105;
				}
				if (this.ModeIs(LexerMode.XmlAttributeTextQuote))
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.SingleQuoteToken;
					goto IL_011F;
				}
				goto IL_0105;
			}
			else
			{
				if (c2 == '<')
				{
					this.TextWindow.AdvanceChar();
					info.Kind = SyntaxKind.LessThanToken;
					goto IL_011F;
				}
				if (c2 != '\uffff')
				{
					goto IL_0105;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
					goto IL_011F;
				}
				goto IL_0105;
			}
			IL_00E2:
			this.ScanXmlTextLiteralNewLineToken(ref info);
			goto IL_011F;
			IL_0105:
			if (SyntaxFacts.IsNewLine(c))
			{
				goto IL_00E2;
			}
			this.ScanXmlAttributeText(ref info);
			info.Kind = SyntaxKind.XmlTextLiteralToken;
			IL_011F:
			return info.Kind > SyntaxKind.None;
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x00134800 File Offset: 0x00132A00
		private void ScanXmlAttributeText(ref Lexer.TokenInfo info)
		{
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c <= '"')
				{
					if (c == '\n' || c == '\r')
					{
						goto IL_00AC;
					}
					if (c == '"')
					{
						if (this.ModeIs(LexerMode.XmlAttributeTextDoubleQuote))
						{
							break;
						}
					}
				}
				else
				{
					switch (c)
					{
					case '&':
						goto IL_00AC;
					case '\'':
						if (this.ModeIs(LexerMode.XmlAttributeTextQuote))
						{
							goto Block_8;
						}
						break;
					case '(':
					case ')':
						break;
					case '*':
						if (this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar(1) == '/')
						{
							goto Block_11;
						}
						break;
					default:
						if (c == '<')
						{
							goto IL_00AC;
						}
						if (c == '\uffff')
						{
							if (this.TextWindow.IsReallyAtEnd())
							{
								goto Block_9;
							}
						}
						break;
					}
				}
				if (SyntaxFacts.IsNewLine(c))
				{
					goto IL_00AC;
				}
				this.TextWindow.AdvanceChar();
			}
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_8:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			IL_00AC:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_9:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_11:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x00134950 File Offset: 0x00132B50
		private SyntaxToken LexXmlCharacter()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTriviaWithWhitespace(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlCharacter(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x00134998 File Offset: 0x00132B98
		private bool ScanXmlCharacter(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			char c = this.TextWindow.PeekChar();
			if (c != '&')
			{
				if (c == '\uffff')
				{
					if (this.TextWindow.IsReallyAtEnd())
					{
						info.Kind = SyntaxKind.EndOfFileToken;
						return true;
					}
				}
				info.Kind = SyntaxKind.XmlTextLiteralToken;
				info.Text = (info.StringValue = this.TextWindow.NextChar().ToString());
			}
			else
			{
				this.ScanXmlEntity(ref info);
				info.Kind = SyntaxKind.XmlEntityLiteralToken;
			}
			return true;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00134A34 File Offset: 0x00132C34
		private SyntaxToken LexXmlCrefOrNameToken()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTriviaWithWhitespace(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlCrefToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00134A7C File Offset: 0x00132C7C
		private bool ScanXmlCrefToken(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			int position = this.TextWindow.Position;
			char c = this.TextWindow.NextChar();
			char maxValue = char.MaxValue;
			if (c <= '&')
			{
				if (c <= '\r')
				{
					if (c != '\n' && c != '\r')
					{
						goto IL_018D;
					}
				}
				else if (c != '"')
				{
					if (c != '&')
					{
						goto IL_018D;
					}
					this.TextWindow.Reset(position);
					if (!this.TextWindow.TryScanXmlEntity(out c, out maxValue))
					{
						this.TextWindow.Reset(position);
						this.ScanXmlEntity(ref info);
						info.Kind = SyntaxKind.XmlEntityLiteralToken;
						return true;
					}
					goto IL_0195;
				}
				else
				{
					if (this.ModeIs(LexerMode.XmlCrefDoubleQuote) || this.ModeIs(LexerMode.XmlNameDoubleQuote))
					{
						info.Kind = SyntaxKind.DoubleQuoteToken;
						return true;
					}
					goto IL_0195;
				}
			}
			else if (c <= '<')
			{
				if (c != '\'')
				{
					if (c != '<')
					{
						goto IL_018D;
					}
					info.Text = this.TextWindow.GetText(false);
					base.AddError(XmlParseErrorCode.XML_LessThanInAttributeValue, new object[] { info.Text });
					return true;
				}
				else
				{
					if (this.ModeIs(LexerMode.XmlCrefQuote) || this.ModeIs(LexerMode.XmlNameQuote))
					{
						info.Kind = SyntaxKind.SingleQuoteToken;
						return true;
					}
					goto IL_0195;
				}
			}
			else
			{
				if (c == '{')
				{
					c = '<';
					goto IL_0195;
				}
				if (c == '}')
				{
					c = '>';
					goto IL_0195;
				}
				if (c != '\uffff')
				{
					goto IL_018D;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
					return true;
				}
				goto IL_018D;
			}
			IL_0131:
			this.TextWindow.Reset(position);
			this.ScanXmlTextLiteralNewLineToken(ref info);
			goto IL_0195;
			IL_018D:
			if (SyntaxFacts.IsNewLine(c))
			{
				goto IL_0131;
			}
			IL_0195:
			switch (c)
			{
			case '!':
				if (this.AdvanceIfMatches('='))
				{
					info.Kind = SyntaxKind.ExclamationEqualsToken;
				}
				else
				{
					info.Kind = SyntaxKind.ExclamationToken;
				}
				break;
			case '"':
			case '#':
			case '$':
			case '\'':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case ';':
				break;
			case '%':
				info.Kind = SyntaxKind.PercentToken;
				break;
			case '&':
				info.Kind = SyntaxKind.AmpersandToken;
				break;
			case '(':
				info.Kind = SyntaxKind.OpenParenToken;
				break;
			case ')':
				info.Kind = SyntaxKind.CloseParenToken;
				break;
			case '*':
				info.Kind = SyntaxKind.AsteriskToken;
				break;
			case '+':
				if (this.AdvanceIfMatches('+'))
				{
					info.Kind = SyntaxKind.PlusPlusToken;
				}
				else
				{
					info.Kind = SyntaxKind.PlusToken;
				}
				break;
			case ',':
				info.Kind = SyntaxKind.CommaToken;
				break;
			case '-':
				if (this.AdvanceIfMatches('-'))
				{
					info.Kind = SyntaxKind.MinusMinusToken;
				}
				else
				{
					info.Kind = SyntaxKind.MinusToken;
				}
				break;
			case '.':
				if (this.AdvanceIfMatches('.'))
				{
					if (this.TextWindow.PeekChar() == '.')
					{
						this.AddCrefError(ErrorCode.ERR_UnexpectedCharacter, new object[] { "." });
					}
					info.Kind = SyntaxKind.DotDotToken;
				}
				else
				{
					info.Kind = SyntaxKind.DotToken;
				}
				break;
			case '/':
				info.Kind = SyntaxKind.SlashToken;
				break;
			case ':':
				if (this.AdvanceIfMatches(':'))
				{
					info.Kind = SyntaxKind.ColonColonToken;
				}
				else
				{
					info.Kind = SyntaxKind.ColonToken;
				}
				break;
			case '<':
				if (this.AdvanceIfMatches('='))
				{
					info.Kind = SyntaxKind.LessThanEqualsToken;
				}
				else if (this.AdvanceIfMatches('<'))
				{
					info.Kind = SyntaxKind.LessThanLessThanToken;
				}
				else
				{
					info.Kind = SyntaxKind.LessThanToken;
				}
				break;
			case '=':
				if (this.AdvanceIfMatches('='))
				{
					info.Kind = SyntaxKind.EqualsEqualsToken;
				}
				else
				{
					info.Kind = SyntaxKind.EqualsToken;
				}
				break;
			case '>':
				if (this.AdvanceIfMatches('='))
				{
					info.Kind = SyntaxKind.GreaterThanEqualsToken;
				}
				else
				{
					info.Kind = SyntaxKind.GreaterThanToken;
				}
				break;
			case '?':
				info.Kind = SyntaxKind.QuestionToken;
				break;
			default:
				switch (c)
				{
				case '[':
					info.Kind = SyntaxKind.OpenBracketToken;
					break;
				case '\\':
					break;
				case ']':
					info.Kind = SyntaxKind.CloseBracketToken;
					break;
				case '^':
					info.Kind = SyntaxKind.CaretToken;
					break;
				default:
					switch (c)
					{
					case '{':
						info.Kind = SyntaxKind.LessThanToken;
						break;
					case '|':
						info.Kind = SyntaxKind.BarToken;
						break;
					case '}':
						info.Kind = SyntaxKind.GreaterThanToken;
						break;
					case '~':
						info.Kind = SyntaxKind.TildeToken;
						break;
					}
					break;
				}
				break;
			}
			if (info.Kind != SyntaxKind.None)
			{
				string text = SyntaxFacts.GetText(info.Kind);
				string text2 = this.TextWindow.GetText(false);
				if (!string.IsNullOrEmpty(text) && text2 != text)
				{
					info.RequiresTextForXmlEntity = true;
					info.Text = text2;
					info.StringValue = text;
				}
			}
			else
			{
				this.TextWindow.Reset(position);
				if (this.ScanIdentifier(ref info) && info.Text.Length > 0)
				{
					SyntaxKind syntaxKind;
					if (!this.InXmlNameAttributeValue && !info.IsVerbatim && !info.HasIdentifierEscapeSequence && this._cache.TryGetKeywordKind(info.StringValue, out syntaxKind))
					{
						if (SyntaxFacts.IsContextualKeyword(syntaxKind))
						{
							info.Kind = SyntaxKind.IdentifierToken;
							info.ContextualKind = syntaxKind;
						}
						else
						{
							info.Kind = syntaxKind;
							info.RequiresTextForXmlEntity = info.Text != info.StringValue;
						}
					}
					else
					{
						info.ContextualKind = (info.Kind = SyntaxKind.IdentifierToken);
					}
				}
				else if (c == '@')
				{
					if (this.TextWindow.PeekChar() == '@')
					{
						this.TextWindow.NextChar();
						info.Text = this.TextWindow.GetText(true);
						info.StringValue = "";
					}
					else
					{
						this.ScanXmlEntity(ref info);
					}
					info.Kind = SyntaxKind.IdentifierToken;
					base.AddError(ErrorCode.ERR_ExpectedVerbatimLiteral);
				}
				else if (this.TextWindow.PeekChar() == '&')
				{
					this.ScanXmlEntity(ref info);
					info.Kind = SyntaxKind.XmlEntityLiteralToken;
					this.AddCrefError(ErrorCode.ERR_UnexpectedCharacter, new object[] { info.Text });
				}
				else
				{
					uint num = (uint)this.TextWindow.NextChar();
					info.Text = this.TextWindow.GetText(false);
					if (Lexer.MatchesProductionForXmlChar(num))
					{
						this.AddCrefError(ErrorCode.ERR_UnexpectedCharacter, new object[] { info.Text });
					}
					else
					{
						base.AddError(XmlParseErrorCode.XML_InvalidUnicodeChar);
					}
				}
			}
			return info.Kind > SyntaxKind.None;
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00135140 File Offset: 0x00133340
		private bool AdvanceIfMatches(char ch)
		{
			char c = this.TextWindow.PeekChar();
			if (c == ch || (c == '{' && ch == '<') || (c == '}' && ch == '>'))
			{
				this.TextWindow.AdvanceChar();
				return true;
			}
			if (c == '&')
			{
				int position = this.TextWindow.Position;
				char c2;
				char c3;
				if (this.TextWindow.TryScanXmlEntity(out c2, out c3) && c2 == ch && c3 == '\uffff')
				{
					return true;
				}
				this.TextWindow.Reset(position);
			}
			return false;
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06004ABF RID: 19135 RVA: 0x001351BC File Offset: 0x001333BC
		private bool InXmlCrefOrNameAttributeValue
		{
			get
			{
				LexerMode lexerMode = this._mode & LexerMode.MaskLexMode;
				if (lexerMode <= LexerMode.XmlCrefDoubleQuote)
				{
					if (lexerMode != LexerMode.XmlCrefQuote && lexerMode != LexerMode.XmlCrefDoubleQuote)
					{
						return false;
					}
				}
				else if (lexerMode != LexerMode.XmlNameQuote && lexerMode != LexerMode.XmlNameDoubleQuote)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06004AC0 RID: 19136 RVA: 0x00135204 File Offset: 0x00133404
		private bool InXmlNameAttributeValue
		{
			get
			{
				LexerMode lexerMode = this._mode & LexerMode.MaskLexMode;
				return lexerMode == LexerMode.XmlNameQuote || lexerMode == LexerMode.XmlNameDoubleQuote;
			}
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x00135231 File Offset: 0x00133431
		private void AddCrefError(ErrorCode code, params object[] args)
		{
			this.AddCrefError(AbstractLexer.MakeError(code, args));
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x00135240 File Offset: 0x00133440
		private void AddCrefError(DiagnosticInfo info)
		{
			if (info != null)
			{
				base.AddError(ErrorCode.WRN_ErrorOverride, new object[] { info, info.Code });
			}
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x00135268 File Offset: 0x00133468
		private SyntaxToken LexXmlCDataSectionTextToken()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTrivia(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlCDataSectionTextToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x001352B0 File Offset: 0x001334B0
		private bool ScanXmlCDataSectionTextToken(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 <= '\r')
			{
				if (c2 != '\n' && c2 != '\r')
				{
					goto IL_00A0;
				}
			}
			else if (c2 != ']')
			{
				if (c2 != '\uffff')
				{
					goto IL_00A0;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
					return true;
				}
				goto IL_00A0;
			}
			else
			{
				if (this.TextWindow.PeekChar(1) == ']' && this.TextWindow.PeekChar(2) == '>')
				{
					this.TextWindow.AdvanceChar(3);
					info.Kind = SyntaxKind.XmlCDataEndToken;
					return true;
				}
				goto IL_00A0;
			}
			IL_007D:
			this.ScanXmlTextLiteralNewLineToken(ref info);
			return true;
			IL_00A0:
			if (SyntaxFacts.IsNewLine(c))
			{
				goto IL_007D;
			}
			this.ScanXmlCDataSectionText(ref info);
			info.Kind = SyntaxKind.XmlTextLiteralToken;
			return true;
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00135378 File Offset: 0x00133578
		private void ScanXmlCDataSectionText(ref Lexer.TokenInfo info)
		{
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c <= '\r')
				{
					if (c == '\n' || c == '\r')
					{
						goto IL_007C;
					}
				}
				else if (c != '*')
				{
					if (c != ']')
					{
						if (c == '\uffff')
						{
							if (this.TextWindow.IsReallyAtEnd())
							{
								goto Block_8;
							}
						}
					}
					else if (this.TextWindow.PeekChar(1) == ']' && this.TextWindow.PeekChar(2) == '>')
					{
						break;
					}
				}
				else if (this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar(1) == '/')
				{
					goto Block_10;
				}
				if (SyntaxFacts.IsNewLine(c))
				{
					goto IL_007C;
				}
				this.TextWindow.AdvanceChar();
			}
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			IL_007C:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_8:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_10:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x00135498 File Offset: 0x00133698
		private SyntaxToken LexXmlCommentTextToken()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTrivia(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlCommentTextToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x001354E0 File Offset: 0x001336E0
		private bool ScanXmlCommentTextToken(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 <= '\r')
			{
				if (c2 != '\n' && c2 != '\r')
				{
					goto IL_00BC;
				}
			}
			else if (c2 != '-')
			{
				if (c2 != '\uffff')
				{
					goto IL_00BC;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
					return true;
				}
				goto IL_00BC;
			}
			else
			{
				if (this.TextWindow.PeekChar(1) != '-')
				{
					goto IL_00BC;
				}
				if (this.TextWindow.PeekChar(2) == '>')
				{
					this.TextWindow.AdvanceChar(3);
					info.Kind = SyntaxKind.XmlCommentEndToken;
					return true;
				}
				this.TextWindow.AdvanceChar(2);
				info.Kind = SyntaxKind.MinusMinusToken;
				return true;
			}
			IL_0099:
			this.ScanXmlTextLiteralNewLineToken(ref info);
			return true;
			IL_00BC:
			if (SyntaxFacts.IsNewLine(c))
			{
				goto IL_0099;
			}
			this.ScanXmlCommentText(ref info);
			info.Kind = SyntaxKind.XmlTextLiteralToken;
			return true;
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x001355C4 File Offset: 0x001337C4
		private void ScanXmlCommentText(ref Lexer.TokenInfo info)
		{
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c <= '\r')
				{
					if (c == '\n' || c == '\r')
					{
						goto IL_0069;
					}
				}
				else if (c != '*')
				{
					if (c != '-')
					{
						if (c == '\uffff')
						{
							if (this.TextWindow.IsReallyAtEnd())
							{
								goto Block_7;
							}
						}
					}
					else if (this.TextWindow.PeekChar(1) == '-')
					{
						break;
					}
				}
				else if (this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar(1) == '/')
				{
					goto Block_9;
				}
				if (SyntaxFacts.IsNewLine(c))
				{
					goto IL_0069;
				}
				this.TextWindow.AdvanceChar();
			}
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			IL_0069:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_7:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_9:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x001356D0 File Offset: 0x001338D0
		private SyntaxToken LexXmlProcessingInstructionTextToken()
		{
			Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
			SyntaxListBuilder syntaxListBuilder = null;
			this.LexXmlDocCommentLeadingTrivia(ref syntaxListBuilder);
			base.Start();
			this.ScanXmlProcessingInstructionTextToken(ref tokenInfo);
			SyntaxDiagnosticInfo[] errors = base.GetErrors(Lexer.GetFullWidth(syntaxListBuilder));
			return this.Create(ref tokenInfo, syntaxListBuilder, null, errors);
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x00135718 File Offset: 0x00133918
		private bool ScanXmlProcessingInstructionTextToken(ref Lexer.TokenInfo info)
		{
			if (this.LocationIs(XmlDocCommentLocation.End))
			{
				info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
				return true;
			}
			char c2;
			char c = (c2 = this.TextWindow.PeekChar());
			if (c2 <= '\r')
			{
				if (c2 != '\n' && c2 != '\r')
				{
					goto IL_0090;
				}
			}
			else if (c2 != '?')
			{
				if (c2 != '\uffff')
				{
					goto IL_0090;
				}
				if (this.TextWindow.IsReallyAtEnd())
				{
					info.Kind = SyntaxKind.EndOfDocumentationCommentToken;
					return true;
				}
				goto IL_0090;
			}
			else
			{
				if (this.TextWindow.PeekChar(1) == '>')
				{
					this.TextWindow.AdvanceChar(2);
					info.Kind = SyntaxKind.XmlProcessingInstructionEndToken;
					return true;
				}
				goto IL_0090;
			}
			IL_006D:
			this.ScanXmlTextLiteralNewLineToken(ref info);
			return true;
			IL_0090:
			if (SyntaxFacts.IsNewLine(c))
			{
				goto IL_006D;
			}
			this.ScanXmlProcessingInstructionText(ref info);
			info.Kind = SyntaxKind.XmlTextLiteralToken;
			return true;
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x001357D0 File Offset: 0x001339D0
		private void ScanXmlProcessingInstructionText(ref Lexer.TokenInfo info)
		{
			for (;;)
			{
				char c = this.TextWindow.PeekChar();
				if (c <= '\r')
				{
					if (c == '\n' || c == '\r')
					{
						goto IL_0069;
					}
				}
				else if (c != '*')
				{
					if (c != '?')
					{
						if (c == '\uffff')
						{
							if (this.TextWindow.IsReallyAtEnd())
							{
								goto Block_7;
							}
						}
					}
					else if (this.TextWindow.PeekChar(1) == '>')
					{
						break;
					}
				}
				else if (this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar(1) == '/')
				{
					goto Block_9;
				}
				if (SyntaxFacts.IsNewLine(c))
				{
					goto IL_0069;
				}
				this.TextWindow.AdvanceChar();
			}
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			IL_0069:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_7:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
			return;
			Block_9:
			info.StringValue = (info.Text = this.TextWindow.GetText(false));
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x001358DC File Offset: 0x00133ADC
		private void LexXmlDocCommentLeadingTrivia(ref SyntaxListBuilder trivia)
		{
			int position = this.TextWindow.Position;
			base.Start();
			if (this.LocationIs(XmlDocCommentLocation.Start) && this.StyleIs(XmlDocCommentStyle.Delimited))
			{
				if (this.TextWindow.PeekChar() == '/' && this.TextWindow.PeekChar(1) == '*' && this.TextWindow.PeekChar(2) == '*' && this.TextWindow.PeekChar(3) != '*')
				{
					this.TextWindow.AdvanceChar(3);
					string text = this.TextWindow.GetText(true);
					this.AddTrivia(SyntaxFactory.DocumentationCommentExteriorTrivia(text), ref trivia);
					this.MutateLocation(XmlDocCommentLocation.Interior);
					return;
				}
			}
			else if (this.LocationIs(XmlDocCommentLocation.Start) || this.LocationIs(XmlDocCommentLocation.Exterior))
			{
				for (;;)
				{
					char c = this.TextWindow.PeekChar();
					if (c <= ' ')
					{
						switch (c)
						{
						case '\t':
						case '\v':
						case '\f':
							break;
						case '\n':
							goto IL_0224;
						default:
							if (c != ' ')
							{
								goto IL_0224;
							}
							break;
						}
					}
					else if (c != '*')
					{
						if (c != '/')
						{
							goto IL_0224;
						}
						if (this.StyleIs(XmlDocCommentStyle.SingleLine) && this.TextWindow.PeekChar(1) == '/' && this.TextWindow.PeekChar(2) == '/' && this.TextWindow.PeekChar(3) != '/')
						{
							break;
						}
						goto IL_0224;
					}
					else
					{
						if (this.StyleIs(XmlDocCommentStyle.Delimited))
						{
							goto Block_17;
						}
						goto IL_0224;
					}
					IL_00FB:
					this.TextWindow.AdvanceChar();
					continue;
					IL_0224:
					if (!SyntaxFacts.IsWhitespace(c))
					{
						goto Block_22;
					}
					goto IL_00FB;
				}
				this.TextWindow.AdvanceChar(3);
				string text2 = this.TextWindow.GetText(true);
				this.AddTrivia(SyntaxFactory.DocumentationCommentExteriorTrivia(text2), ref trivia);
				this.MutateLocation(XmlDocCommentLocation.Interior);
				return;
				Block_17:
				while (this.TextWindow.PeekChar() == '*' && this.TextWindow.PeekChar(1) != '/')
				{
					this.TextWindow.AdvanceChar();
				}
				string text3 = this.TextWindow.GetText(true);
				if (!string.IsNullOrEmpty(text3))
				{
					this.AddTrivia(SyntaxFactory.DocumentationCommentExteriorTrivia(text3), ref trivia);
				}
				if (this.TextWindow.PeekChar() == '*' && this.TextWindow.PeekChar(1) == '/')
				{
					this.TextWindow.AdvanceChar(2);
					this.AddTrivia(SyntaxFactory.DocumentationCommentExteriorTrivia("*/"), ref trivia);
					this.MutateLocation(XmlDocCommentLocation.End);
					return;
				}
				this.MutateLocation(XmlDocCommentLocation.Interior);
				return;
				Block_22:
				if (this.StyleIs(XmlDocCommentStyle.SingleLine))
				{
					this.TextWindow.Reset(position);
					this.MutateLocation(XmlDocCommentLocation.End);
					return;
				}
				string text4 = this.TextWindow.GetText(true);
				if (!string.IsNullOrEmpty(text4))
				{
					this.AddTrivia(SyntaxFactory.DocumentationCommentExteriorTrivia(text4), ref trivia);
				}
				this.MutateLocation(XmlDocCommentLocation.Interior);
				return;
			}
			else if (!this.LocationIs(XmlDocCommentLocation.End) && this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar() == '*' && this.TextWindow.PeekChar(1) == '/')
			{
				this.TextWindow.AdvanceChar(2);
				string text5 = this.TextWindow.GetText(true);
				this.AddTrivia(SyntaxFactory.DocumentationCommentExteriorTrivia(text5), ref trivia);
				this.MutateLocation(XmlDocCommentLocation.End);
			}
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x00135BC4 File Offset: 0x00133DC4
		private void LexXmlDocCommentLeadingTriviaWithWhitespace(ref SyntaxListBuilder trivia)
		{
			for (;;)
			{
				this.LexXmlDocCommentLeadingTrivia(ref trivia);
				char c = this.TextWindow.PeekChar();
				if (!this.LocationIs(XmlDocCommentLocation.Interior) || (!SyntaxFacts.IsWhitespace(c) && !SyntaxFacts.IsNewLine(c)))
				{
					break;
				}
				this.LexXmlWhitespaceAndNewLineTrivia(ref trivia);
			}
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x00135C08 File Offset: 0x00133E08
		private void LexXmlWhitespaceAndNewLineTrivia(ref SyntaxListBuilder trivia)
		{
			base.Start();
			if (this.LocationIs(XmlDocCommentLocation.Interior))
			{
				char c = this.TextWindow.PeekChar();
				switch (c)
				{
				case '\t':
				case '\v':
				case '\f':
					break;
				case '\n':
				case '\r':
					goto IL_0055;
				default:
					if (c != ' ')
					{
						if (c == '*')
						{
							if (this.StyleIs(XmlDocCommentStyle.Delimited) && this.TextWindow.PeekChar(1) == '/')
							{
								return;
							}
						}
						if (!SyntaxFacts.IsWhitespace(c))
						{
							if (!SyntaxFacts.IsNewLine(c))
							{
								return;
							}
							goto IL_0055;
						}
					}
					break;
				}
				this.AddTrivia(this.ScanWhitespace(), ref trivia);
				return;
				IL_0055:
				this.AddTrivia(this.ScanEndOfLine(), ref trivia);
				this.MutateLocation(XmlDocCommentLocation.Exterior);
				return;
			}
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x00135CAC File Offset: 0x00133EAC
		private void ScanStringLiteral(ref Lexer.TokenInfo info, bool allowEscapes = true)
		{
			char c = this.TextWindow.PeekChar();
			if (c != '\'' && c != '"')
			{
				info.Kind = SyntaxKind.None;
				info.Text = null;
				return;
			}
			this.TextWindow.AdvanceChar();
			this._builder.Length = 0;
			for (;;)
			{
				char c2 = this.TextWindow.PeekChar();
				if (c2 == '\\' && allowEscapes)
				{
					char c3;
					c2 = this.ScanEscapeSequence(out c3);
					this._builder.Append(c2);
					if (c3 != '\uffff')
					{
						this._builder.Append(c3);
					}
				}
				else
				{
					if (c2 == c)
					{
						break;
					}
					if (SyntaxFacts.IsNewLine(c2) || (c2 == '\uffff' && this.TextWindow.IsReallyAtEnd()))
					{
						goto IL_00A0;
					}
					this.TextWindow.AdvanceChar();
					this._builder.Append(c2);
				}
			}
			this.TextWindow.AdvanceChar();
			goto IL_00CA;
			IL_00A0:
			base.AddError(ErrorCode.ERR_NewlineInConst);
			IL_00CA:
			info.Text = this.TextWindow.GetText(true);
			if (c == '\'')
			{
				info.Kind = SyntaxKind.CharacterLiteralToken;
				if (this._builder.Length != 1)
				{
					base.AddError((this._builder.Length != 0) ? ErrorCode.ERR_TooManyCharsInConst : ErrorCode.ERR_EmptyCharConst);
				}
				if (this._builder.Length > 0)
				{
					info.StringValue = this.TextWindow.Intern(this._builder);
					info.CharValue = info.StringValue.get_Chars(0);
					return;
				}
				info.StringValue = string.Empty;
				info.CharValue = char.MaxValue;
				return;
			}
			else
			{
				info.Kind = SyntaxKind.StringLiteralToken;
				if (this._builder.Length > 0)
				{
					info.StringValue = this.TextWindow.Intern(this._builder);
					return;
				}
				info.StringValue = string.Empty;
				return;
			}
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00135E70 File Offset: 0x00134070
		private char ScanEscapeSequence(out char surrogateCharacter)
		{
			int position = this.TextWindow.Position;
			surrogateCharacter = char.MaxValue;
			char c = this.TextWindow.NextChar();
			c = this.TextWindow.NextChar();
			if (c <= 'U')
			{
				if (c <= '\'')
				{
					if (c != '"' && c != '\'')
					{
						goto IL_00FD;
					}
					return c;
				}
				else
				{
					if (c == '0')
					{
						return '\0';
					}
					if (c != 'U')
					{
						goto IL_00FD;
					}
				}
			}
			else if (c <= 'a')
			{
				if (c == '\\')
				{
					return c;
				}
				if (c != 'a')
				{
					goto IL_00FD;
				}
				return '\a';
			}
			else
			{
				if (c == 'b')
				{
					return '\b';
				}
				if (c == 'f')
				{
					return '\f';
				}
				switch (c)
				{
				case 'n':
					return '\n';
				case 'o':
				case 'p':
				case 'q':
				case 's':
				case 'w':
					goto IL_00FD;
				case 'r':
					return '\r';
				case 't':
					return '\t';
				case 'u':
				case 'x':
					break;
				case 'v':
					return '\v';
				default:
					goto IL_00FD;
				}
			}
			this.TextWindow.Reset(position);
			SyntaxDiagnosticInfo syntaxDiagnosticInfo;
			c = this.TextWindow.NextUnicodeEscape(out surrogateCharacter, out syntaxDiagnosticInfo);
			base.AddError(syntaxDiagnosticInfo);
			return c;
			IL_00FD:
			base.AddError(position, this.TextWindow.Position - position, ErrorCode.ERR_IllegalEscape);
			return c;
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00135F94 File Offset: 0x00134194
		private void ScanVerbatimStringLiteral(ref Lexer.TokenInfo info, bool allowNewlines = true)
		{
			this._builder.Length = 0;
			if (this.TextWindow.PeekChar() == '@' && this.TextWindow.PeekChar(1) == '"')
			{
				this.TextWindow.AdvanceChar(2);
				bool flag = false;
				this._builder.Length = 0;
				while (!flag)
				{
					char c2;
					char c = (c2 = this.TextWindow.PeekChar());
					if (c2 != '"')
					{
						if (c2 == '\uffff')
						{
							if (this.TextWindow.IsReallyAtEnd())
							{
								base.AddError(ErrorCode.ERR_UnterminatedStringLit);
								flag = true;
								continue;
							}
						}
						if (!allowNewlines && SyntaxFacts.IsNewLine(c))
						{
							base.AddError(ErrorCode.ERR_UnterminatedStringLit);
							flag = true;
						}
						else
						{
							this.TextWindow.AdvanceChar();
							this._builder.Append(c);
						}
					}
					else
					{
						this.TextWindow.AdvanceChar();
						if (this.TextWindow.PeekChar() == '"')
						{
							this.TextWindow.AdvanceChar();
							this._builder.Append(c);
						}
						else
						{
							flag = true;
						}
					}
				}
				info.Kind = SyntaxKind.StringLiteralToken;
				info.Text = this.TextWindow.GetText(false);
				info.StringValue = this._builder.ToString();
				return;
			}
			info.Kind = SyntaxKind.None;
			info.Text = null;
			info.StringValue = null;
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x001360E0 File Offset: 0x001342E0
		private void ScanInterpolatedStringLiteral(bool isVerbatim, ref Lexer.TokenInfo info)
		{
			SyntaxDiagnosticInfo syntaxDiagnosticInfo = null;
			bool flag;
			this.ScanInterpolatedStringLiteralTop(null, isVerbatim, ref info, ref syntaxDiagnosticInfo, out flag);
			base.AddError(syntaxDiagnosticInfo);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x00136104 File Offset: 0x00134304
		internal void ScanInterpolatedStringLiteralTop(ArrayBuilder<Lexer.Interpolation> interpolations, bool isVerbatim, ref Lexer.TokenInfo info, ref SyntaxDiagnosticInfo error, out bool closeQuoteMissing)
		{
			Lexer.InterpolatedStringScanner interpolatedStringScanner = new Lexer.InterpolatedStringScanner(this, isVerbatim);
			interpolatedStringScanner.ScanInterpolatedStringLiteralTop(interpolations, ref info, out closeQuoteMissing);
			error = interpolatedStringScanner.error;
			info.Text = this.TextWindow.GetText(false);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x00136140 File Offset: 0x00134340
		internal static SyntaxToken RescanInterpolatedString(InterpolatedStringExpressionSyntax interpolatedString)
		{
			string text = interpolatedString.ToString();
			SyntaxKind syntaxKind = SyntaxKind.InterpolatedStringToken;
			return SyntaxFactory.Literal(interpolatedString.GetFirstToken().GetLeadingTrivia(), text, syntaxKind, text, interpolatedString.GetLastToken().GetTrailingTrivia());
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00136178 File Offset: 0x00134378
		private SyntaxToken QuickScanSyntaxToken()
		{
			base.Start();
			Lexer.QuickScanState quickScanState = Lexer.QuickScanState.Initial;
			int i = this.TextWindow.Offset;
			int num = this.TextWindow.CharacterWindowCount;
			num = Math.Min(num, i + 42);
			int num2 = -2128831035;
			char[] characterWindow = this.TextWindow.CharacterWindow;
			int num3 = Lexer.s_charProperties.Length;
			while (i < num)
			{
				int num4 = (int)characterWindow[i];
				Lexer.CharFlags charFlags = (Lexer.CharFlags)((num4 < num3) ? Lexer.s_charProperties[num4] : 9);
				quickScanState = (Lexer.QuickScanState)Lexer.s_stateTransitions[(int)quickScanState, (int)charFlags];
				if (quickScanState < Lexer.QuickScanState.Done)
				{
					num2 = (num2 ^ num4) * 16777619;
					i++;
				}
				else
				{
					IL_008C:
					this.TextWindow.AdvanceChar(i - this.TextWindow.Offset);
					if (quickScanState == Lexer.QuickScanState.Done)
					{
						return this._cache.LookupToken(this.TextWindow.CharacterWindow, this.TextWindow.LexemeRelativeStart, i - this.TextWindow.LexemeRelativeStart, num2, this._createQuickTokenFunction);
					}
					this.TextWindow.Reset(this.TextWindow.LexemeStartPosition);
					return null;
				}
			}
			quickScanState = Lexer.QuickScanState.Bad;
			goto IL_008C;
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x0013627B File Offset: 0x0013447B
		private SyntaxToken CreateQuickToken()
		{
			this.TextWindow.Reset(this.TextWindow.LexemeStartPosition);
			return this.LexSyntaxToken();
		}

		// Token: 0x04001584 RID: 5508
		private const int TriviaListInitialCapacity = 8;

		// Token: 0x04001585 RID: 5509
		private readonly CSharpParseOptions _options;

		// Token: 0x04001586 RID: 5510
		private LexerMode _mode;

		// Token: 0x04001587 RID: 5511
		private readonly StringBuilder _builder;

		// Token: 0x04001588 RID: 5512
		private char[] _identBuffer;

		// Token: 0x04001589 RID: 5513
		private int _identLen;

		// Token: 0x0400158A RID: 5514
		private DirectiveStack _directives;

		// Token: 0x0400158B RID: 5515
		private readonly LexerCache _cache;

		// Token: 0x0400158C RID: 5516
		private readonly bool _allowPreprocessorDirectives;

		// Token: 0x0400158D RID: 5517
		private readonly bool _interpolationFollowedByColon;

		// Token: 0x0400158E RID: 5518
		private DocumentationCommentParser _xmlParser;

		// Token: 0x0400158F RID: 5519
		private int _badTokenCount;

		// Token: 0x04001590 RID: 5520
		private SyntaxListBuilder _leadingTriviaCache = new SyntaxListBuilder(10);

		// Token: 0x04001591 RID: 5521
		private SyntaxListBuilder _trailingTriviaCache = new SyntaxListBuilder(10);

		// Token: 0x04001592 RID: 5522
		private static readonly int s_conflictMarkerLength = "<<<<<<<".Length;

		// Token: 0x04001593 RID: 5523
		private Func<SyntaxTrivia> _createWhitespaceTriviaFunction;

		// Token: 0x04001594 RID: 5524
		internal const int MaxCachedTokenSize = 42;

		// Token: 0x04001595 RID: 5525
		private static readonly byte[,] s_stateTransitions = new byte[,]
		{
			{
				0, 0, 0, 3, 4, 5, 6, 7, 10, 10,
				10
			},
			{
				1, 2, 8, 9, 9, 9, 9, 9, 10, 10,
				9
			},
			{
				9, 9, 8, 9, 9, 9, 9, 9, 9, 9,
				9
			},
			{
				1, 2, 8, 3, 3, 9, 9, 9, 10, 10,
				9
			},
			{
				1, 2, 8, 10, 4, 9, 10, 9, 10, 10,
				9
			},
			{
				1, 2, 8, 9, 9, 9, 9, 9, 10, 10,
				9
			},
			{
				1, 2, 8, 9, 4, 9, 10, 9, 10, 10,
				9
			},
			{
				1, 2, 8, 9, 9, 10, 9, 10, 10, 10,
				9
			},
			{
				9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
				9
			}
		};

		// Token: 0x04001596 RID: 5526
		private readonly Func<SyntaxToken> _createQuickTokenFunction;

		// Token: 0x04001597 RID: 5527
		private static readonly byte[] s_charProperties = new byte[]
		{
			9, 9, 9, 9, 9, 9, 9, 9, 9, 0,
			2, 0, 0, 1, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 0, 7, 9, 9, 9, 7, 7, 9,
			5, 5, 7, 7, 5, 7, 6, 8, 4, 4,
			4, 4, 4, 4, 4, 4, 4, 4, 7, 5,
			7, 7, 7, 7, 9, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 5, 9, 5, 7, 3, 9, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 5, 7, 5, 7, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			3, 9, 9, 9, 9, 9, 9, 9, 9, 9,
			9, 3, 9, 9, 9, 9, 3, 9, 9, 9,
			9, 9, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 9, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 9, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
			3, 3, 3, 3
		};

		// Token: 0x020007A9 RID: 1961
		internal struct TokenInfo
		{
			// Token: 0x040024A1 RID: 9377
			internal SyntaxKind Kind;

			// Token: 0x040024A2 RID: 9378
			internal SyntaxKind ContextualKind;

			// Token: 0x040024A3 RID: 9379
			internal string Text;

			// Token: 0x040024A4 RID: 9380
			internal SpecialType ValueKind;

			// Token: 0x040024A5 RID: 9381
			internal bool RequiresTextForXmlEntity;

			// Token: 0x040024A6 RID: 9382
			internal bool HasIdentifierEscapeSequence;

			// Token: 0x040024A7 RID: 9383
			internal string StringValue;

			// Token: 0x040024A8 RID: 9384
			internal char CharValue;

			// Token: 0x040024A9 RID: 9385
			internal int IntValue;

			// Token: 0x040024AA RID: 9386
			internal uint UintValue;

			// Token: 0x040024AB RID: 9387
			internal long LongValue;

			// Token: 0x040024AC RID: 9388
			internal ulong UlongValue;

			// Token: 0x040024AD RID: 9389
			internal float FloatValue;

			// Token: 0x040024AE RID: 9390
			internal double DoubleValue;

			// Token: 0x040024AF RID: 9391
			internal decimal DecimalValue;

			// Token: 0x040024B0 RID: 9392
			internal bool IsVerbatim;
		}

		// Token: 0x020007AA RID: 1962
		internal struct Interpolation
		{
			// Token: 0x17002739 RID: 10041
			// (get) Token: 0x060088FC RID: 35068 RVA: 0x001EF129 File Offset: 0x001ED329
			public bool ColonMissing
			{
				get
				{
					return this.ColonPosition <= 0;
				}
			}

			// Token: 0x1700273A RID: 10042
			// (get) Token: 0x060088FD RID: 35069 RVA: 0x001EF137 File Offset: 0x001ED337
			public bool HasColon
			{
				get
				{
					return this.ColonPosition > 0;
				}
			}

			// Token: 0x1700273B RID: 10043
			// (get) Token: 0x060088FE RID: 35070 RVA: 0x001EF142 File Offset: 0x001ED342
			public int LastPosition
			{
				get
				{
					if (!this.CloseBraceMissing)
					{
						return this.CloseBracePosition;
					}
					return this.CloseBracePosition - 1;
				}
			}

			// Token: 0x1700273C RID: 10044
			// (get) Token: 0x060088FF RID: 35071 RVA: 0x001EF15B File Offset: 0x001ED35B
			public int FormatEndPosition
			{
				get
				{
					return this.CloseBracePosition - 1;
				}
			}

			// Token: 0x06008900 RID: 35072 RVA: 0x001EF165 File Offset: 0x001ED365
			public Interpolation(int openBracePosition, int colonPosition, int closeBracePosition, bool closeBraceMissing)
			{
				this.OpenBracePosition = openBracePosition;
				this.ColonPosition = colonPosition;
				this.CloseBracePosition = closeBracePosition;
				this.CloseBraceMissing = closeBraceMissing;
			}

			// Token: 0x040024B1 RID: 9393
			public readonly int OpenBracePosition;

			// Token: 0x040024B2 RID: 9394
			public readonly int ColonPosition;

			// Token: 0x040024B3 RID: 9395
			public readonly int CloseBracePosition;

			// Token: 0x040024B4 RID: 9396
			public readonly bool CloseBraceMissing;
		}

		// Token: 0x020007AB RID: 1963
		private class InterpolatedStringScanner
		{
			// Token: 0x06008901 RID: 35073 RVA: 0x001EF184 File Offset: 0x001ED384
			public InterpolatedStringScanner(Lexer lexer, bool isVerbatim)
			{
				this.lexer = lexer;
				this.isVerbatim = isVerbatim;
				this.allowNewlines = isVerbatim;
			}

			// Token: 0x06008902 RID: 35074 RVA: 0x001EF1A1 File Offset: 0x001ED3A1
			private bool IsAtEnd()
			{
				return this.IsAtEnd(this.isVerbatim && this.allowNewlines);
			}

			// Token: 0x06008903 RID: 35075 RVA: 0x001EF1BC File Offset: 0x001ED3BC
			private bool IsAtEnd(bool allowNewline)
			{
				char c = this.lexer.TextWindow.PeekChar();
				return (!allowNewline && SyntaxFacts.IsNewLine(c)) || (c == char.MaxValue && this.lexer.TextWindow.IsReallyAtEnd());
			}

			// Token: 0x06008904 RID: 35076 RVA: 0x001EF204 File Offset: 0x001ED404
			internal void ScanInterpolatedStringLiteralTop(ArrayBuilder<Lexer.Interpolation> interpolations, ref Lexer.TokenInfo info, out bool closeQuoteMissing)
			{
				if (this.isVerbatim)
				{
					this.lexer.TextWindow.AdvanceChar();
					this.lexer.TextWindow.AdvanceChar();
				}
				else
				{
					this.lexer.TextWindow.AdvanceChar();
				}
				this.lexer.TextWindow.AdvanceChar();
				this.ScanInterpolatedStringLiteralContents(interpolations);
				if (this.lexer.TextWindow.PeekChar() != '"')
				{
					if (this.error == null)
					{
						int num = (this.IsAtEnd(true) ? (this.lexer.TextWindow.Position - 1) : this.lexer.TextWindow.Position);
						this.error = this.lexer.MakeError(num, 1, this.isVerbatim ? ErrorCode.ERR_UnterminatedStringLit : ErrorCode.ERR_NewlineInConst);
					}
					closeQuoteMissing = true;
				}
				else
				{
					this.lexer.TextWindow.AdvanceChar();
					closeQuoteMissing = false;
				}
				info.Kind = SyntaxKind.InterpolatedStringToken;
			}

			// Token: 0x06008905 RID: 35077 RVA: 0x001EF2F8 File Offset: 0x001ED4F8
			private void ScanInterpolatedStringLiteralContents(ArrayBuilder<Lexer.Interpolation> interpolations)
			{
				while (!this.IsAtEnd())
				{
					char c = this.lexer.TextWindow.PeekChar();
					if (c <= '\\')
					{
						if (c != '"')
						{
							if (c == '\\')
							{
								if (!this.isVerbatim)
								{
									int position = this.lexer.TextWindow.Position;
									char c3;
									char c2 = this.lexer.ScanEscapeSequence(out c3);
									if ((c2 == '{' || c2 == '}') && this.error == null)
									{
										this.error = this.lexer.MakeError(position, this.lexer.TextWindow.Position - position, ErrorCode.ERR_EscapedCurly, new object[] { c2 });
										continue;
									}
									continue;
								}
							}
						}
						else
						{
							if (this.RecoveringFromRunawayLexing())
							{
								return;
							}
							if (this.isVerbatim && this.lexer.TextWindow.PeekChar(1) == '"')
							{
								this.lexer.TextWindow.AdvanceChar();
								this.lexer.TextWindow.AdvanceChar();
								continue;
							}
							return;
						}
					}
					else if (c != '{')
					{
						if (c == '}')
						{
							int position2 = this.lexer.TextWindow.Position;
							this.lexer.TextWindow.AdvanceChar();
							if (this.lexer.TextWindow.PeekChar() == '}')
							{
								this.lexer.TextWindow.AdvanceChar();
								continue;
							}
							if (this.error == null)
							{
								this.error = this.lexer.MakeError(position2, 1, ErrorCode.ERR_UnescapedCurly, new object[] { "}" });
								continue;
							}
							continue;
						}
					}
					else
					{
						if (this.lexer.TextWindow.PeekChar(1) == '{')
						{
							this.lexer.TextWindow.AdvanceChar();
							this.lexer.TextWindow.AdvanceChar();
							continue;
						}
						int position3 = this.lexer.TextWindow.Position;
						this.lexer.TextWindow.AdvanceChar();
						int num = 0;
						this.ScanInterpolatedStringLiteralHoleBalancedText('}', true, ref num);
						int position4 = this.lexer.TextWindow.Position;
						bool flag = false;
						if (this.lexer.TextWindow.PeekChar() == '}')
						{
							this.lexer.TextWindow.AdvanceChar();
						}
						else
						{
							flag = true;
							if (this.error == null)
							{
								this.error = this.lexer.MakeError(position3 - 1, 2, ErrorCode.ERR_UnclosedExpressionHole);
							}
						}
						if (interpolations != null)
						{
							interpolations.Add(new Lexer.Interpolation(position3, num, position4, flag));
							continue;
						}
						continue;
					}
					this.lexer.TextWindow.AdvanceChar();
				}
			}

			// Token: 0x06008906 RID: 35078 RVA: 0x001EF594 File Offset: 0x001ED794
			private void ScanFormatSpecifier()
			{
				this.lexer.TextWindow.AdvanceChar();
				for (;;)
				{
					char c = this.lexer.TextWindow.PeekChar();
					if (c == '\\' && !this.isVerbatim)
					{
						int position = this.lexer.TextWindow.Position;
						char c2;
						c = this.lexer.ScanEscapeSequence(out c2);
						if ((c == '{' || c == '}') && this.error == null)
						{
							this.error = this.lexer.MakeError(position, 1, ErrorCode.ERR_EscapedCurly, new object[] { c });
						}
					}
					else if (c == '"')
					{
						if (!this.isVerbatim || this.lexer.TextWindow.PeekChar(1) != '"')
						{
							break;
						}
						this.lexer.TextWindow.AdvanceChar();
						this.lexer.TextWindow.AdvanceChar();
					}
					else if (c == '{')
					{
						int position2 = this.lexer.TextWindow.Position;
						this.lexer.TextWindow.AdvanceChar();
						if (this.lexer.TextWindow.PeekChar() == '{')
						{
							this.lexer.TextWindow.AdvanceChar();
						}
						else if (this.error == null)
						{
							this.error = this.lexer.MakeError(position2, 1, ErrorCode.ERR_UnescapedCurly, new object[] { "{" });
						}
					}
					else if (c == '}')
					{
						if (this.lexer.TextWindow.PeekChar(1) != '}')
						{
							return;
						}
						this.lexer.TextWindow.AdvanceChar();
						this.lexer.TextWindow.AdvanceChar();
					}
					else
					{
						if (this.IsAtEnd())
						{
							return;
						}
						this.lexer.TextWindow.AdvanceChar();
					}
				}
			}

			// Token: 0x06008907 RID: 35079 RVA: 0x001EF758 File Offset: 0x001ED958
			private void ScanInterpolatedStringLiteralHoleBalancedText(char endingChar, bool isHole, ref int colonPosition)
			{
				while (!this.IsAtEnd())
				{
					char c = this.lexer.TextWindow.PeekChar();
					char c2 = c;
					if (c2 <= '@')
					{
						if (c2 <= '/')
						{
							switch (c2)
							{
							case '"':
								if (this.RecoveringFromRunawayLexing())
								{
									return;
								}
								break;
							case '#':
								if (this.error == null)
								{
									this.error = this.lexer.MakeError(this.lexer.TextWindow.Position, 1, ErrorCode.ERR_SyntaxError, new object[] { endingChar.ToString() });
								}
								this.lexer.TextWindow.AdvanceChar();
								continue;
							case '$':
								if (this.lexer.TextWindow.PeekChar(1) == '"' || (this.lexer.TextWindow.PeekChar(1) == '@' && this.lexer.TextWindow.PeekChar(2) == '"'))
								{
									bool flag = this.lexer.TextWindow.PeekChar(1) == '@';
									ArrayBuilder<Lexer.Interpolation> arrayBuilder = null;
									Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
									bool flag2 = this.isVerbatim;
									bool flag3 = this.allowNewlines;
									try
									{
										this.isVerbatim = flag;
										this.allowNewlines &= this.isVerbatim;
										bool flag4;
										this.ScanInterpolatedStringLiteralTop(arrayBuilder, ref tokenInfo, out flag4);
										continue;
									}
									finally
									{
										this.isVerbatim = flag2;
										this.allowNewlines = flag3;
									}
									goto IL_01A4;
								}
								goto IL_03EC;
							case '%':
							case '&':
								goto IL_03EC;
							case '\'':
								break;
							case '(':
								this.ScanInterpolatedStringLiteralHoleBracketed('(', ')');
								continue;
							case ')':
								goto IL_01C3;
							default:
								if (c2 != '/')
								{
									goto IL_03EC;
								}
								goto IL_02D6;
							}
							this.ScanInterpolatedStringLiteralNestedString();
							continue;
						}
						if (c2 == ':')
						{
							goto IL_01A4;
						}
						if (c2 != '@')
						{
							goto IL_03EC;
						}
						if (this.lexer.TextWindow.PeekChar(1) == '"' && !this.RecoveringFromRunawayLexing())
						{
							this.ScanInterpolatedStringLiteralNestedVerbatimString();
							continue;
						}
						if (this.lexer.TextWindow.PeekChar(1) != '$' || this.lexer.TextWindow.PeekChar(2) != '"')
						{
							goto IL_03EC;
						}
						this.lexer.CheckFeatureAvailability(MessageID.IDS_FeatureAltInterpolatedVerbatimStrings);
						ArrayBuilder<Lexer.Interpolation> arrayBuilder2 = null;
						Lexer.TokenInfo tokenInfo2 = default(Lexer.TokenInfo);
						bool flag5 = this.isVerbatim;
						bool flag6 = this.allowNewlines;
						try
						{
							this.isVerbatim = true;
							this.allowNewlines = true;
							bool flag7;
							this.ScanInterpolatedStringLiteralTop(arrayBuilder2, ref tokenInfo2, out flag7);
							continue;
						}
						finally
						{
							this.isVerbatim = flag5;
							this.allowNewlines = flag6;
						}
						IL_02D6:
						char c3 = this.lexer.TextWindow.PeekChar(1);
						if (c3 == '*')
						{
							this.ScanInterpolatedStringLiteralNestedComment();
							continue;
						}
						if (c3 != '/')
						{
							this.lexer.TextWindow.AdvanceChar();
							continue;
						}
						if (this.isVerbatim && this.allowNewlines)
						{
							this.lexer.TextWindow.AdvanceChar();
							this.lexer.TextWindow.AdvanceChar();
							while (!this.IsAtEnd(false))
							{
								this.lexer.TextWindow.AdvanceChar();
							}
							continue;
						}
						if (this.error == null)
						{
							this.error = this.lexer.MakeError(this.lexer.TextWindow.Position, 2, ErrorCode.ERR_SingleLineCommentInExpressionHole);
						}
						this.lexer.TextWindow.AdvanceChar();
						this.lexer.TextWindow.AdvanceChar();
						continue;
						IL_01A4:
						if (isHole)
						{
							colonPosition = this.lexer.TextWindow.Position;
							this.ScanFormatSpecifier();
							return;
						}
					}
					else if (c2 <= ']')
					{
						if (c2 == '[')
						{
							this.ScanInterpolatedStringLiteralHoleBracketed('[', ']');
							continue;
						}
						if (c2 == ']')
						{
							goto IL_01C3;
						}
					}
					else
					{
						if (c2 == '{')
						{
							this.ScanInterpolatedStringLiteralHoleBracketed('{', '}');
							continue;
						}
						if (c2 == '}')
						{
							goto IL_01C3;
						}
					}
					IL_03EC:
					this.lexer.TextWindow.AdvanceChar();
					continue;
					IL_01C3:
					if (c == endingChar)
					{
						return;
					}
					if (this.error == null)
					{
						this.error = this.lexer.MakeError(this.lexer.TextWindow.Position, 1, ErrorCode.ERR_SyntaxError, new object[] { endingChar.ToString() });
						goto IL_03EC;
					}
					goto IL_03EC;
				}
			}

			// Token: 0x06008908 RID: 35080 RVA: 0x001EFB84 File Offset: 0x001EDD84
			private bool RecoveringFromRunawayLexing()
			{
				return this.error != null;
			}

			// Token: 0x06008909 RID: 35081 RVA: 0x001EFB90 File Offset: 0x001EDD90
			private void ScanInterpolatedStringLiteralNestedComment()
			{
				this.lexer.TextWindow.AdvanceChar();
				this.lexer.TextWindow.AdvanceChar();
				while (!this.IsAtEnd())
				{
					int num = (int)this.lexer.TextWindow.PeekChar();
					this.lexer.TextWindow.AdvanceChar();
					if (num == 42 && this.lexer.TextWindow.PeekChar() == '/')
					{
						this.lexer.TextWindow.AdvanceChar();
						return;
					}
				}
			}

			// Token: 0x0600890A RID: 35082 RVA: 0x001EFC10 File Offset: 0x001EDE10
			private void ScanInterpolatedStringLiteralNestedString()
			{
				Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
				this.lexer.ScanStringLiteral(ref tokenInfo, true);
			}

			// Token: 0x0600890B RID: 35083 RVA: 0x001EFC34 File Offset: 0x001EDE34
			private void ScanInterpolatedStringLiteralNestedVerbatimString()
			{
				Lexer.TokenInfo tokenInfo = default(Lexer.TokenInfo);
				this.lexer.ScanVerbatimStringLiteral(ref tokenInfo, this.allowNewlines);
			}

			// Token: 0x0600890C RID: 35084 RVA: 0x001EFC5C File Offset: 0x001EDE5C
			private void ScanInterpolatedStringLiteralHoleBracketed(char start, char end)
			{
				this.lexer.TextWindow.AdvanceChar();
				int num = 0;
				this.ScanInterpolatedStringLiteralHoleBalancedText(end, false, ref num);
				if (this.lexer.TextWindow.PeekChar() == end)
				{
					this.lexer.TextWindow.AdvanceChar();
				}
			}

			// Token: 0x040024B5 RID: 9397
			public readonly Lexer lexer;

			// Token: 0x040024B6 RID: 9398
			public bool isVerbatim;

			// Token: 0x040024B7 RID: 9399
			public bool allowNewlines;

			// Token: 0x040024B8 RID: 9400
			public SyntaxDiagnosticInfo error;
		}

		// Token: 0x020007AC RID: 1964
		private enum QuickScanState : byte
		{
			// Token: 0x040024BA RID: 9402
			Initial,
			// Token: 0x040024BB RID: 9403
			FollowingWhite,
			// Token: 0x040024BC RID: 9404
			FollowingCR,
			// Token: 0x040024BD RID: 9405
			Ident,
			// Token: 0x040024BE RID: 9406
			Number,
			// Token: 0x040024BF RID: 9407
			Punctuation,
			// Token: 0x040024C0 RID: 9408
			Dot,
			// Token: 0x040024C1 RID: 9409
			CompoundPunctStart,
			// Token: 0x040024C2 RID: 9410
			DoneAfterNext,
			// Token: 0x040024C3 RID: 9411
			Done,
			// Token: 0x040024C4 RID: 9412
			Bad
		}

		// Token: 0x020007AD RID: 1965
		private enum CharFlags : byte
		{
			// Token: 0x040024C6 RID: 9414
			White,
			// Token: 0x040024C7 RID: 9415
			CR,
			// Token: 0x040024C8 RID: 9416
			LF,
			// Token: 0x040024C9 RID: 9417
			Letter,
			// Token: 0x040024CA RID: 9418
			Digit,
			// Token: 0x040024CB RID: 9419
			Punct,
			// Token: 0x040024CC RID: 9420
			Dot,
			// Token: 0x040024CD RID: 9421
			CompoundPunctStart,
			// Token: 0x040024CE RID: 9422
			Slash,
			// Token: 0x040024CF RID: 9423
			Complex,
			// Token: 0x040024D0 RID: 9424
			EndOfFile
		}
	}
}
