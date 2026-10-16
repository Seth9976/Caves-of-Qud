using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200035E RID: 862
	internal class DocumentationCommentParser : SyntaxParser
	{
		// Token: 0x06004866 RID: 18534 RVA: 0x0011D21C File Offset: 0x0011B41C
		internal DocumentationCommentParser(Lexer lexer, LexerMode modeflags)
			: base(lexer, LexerMode.XmlDocComment | modeflags, null, null, true, false, default(CancellationToken))
		{
			this._isDelimited = (modeflags & LexerMode.XmlDocCommentStyleDelimited) > LexerMode.XmlDocCommentLocationStart;
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x0011D266 File Offset: 0x0011B466
		internal void ReInitialize(LexerMode modeflags)
		{
			base.ReInitialize();
			base.Mode = LexerMode.XmlDocComment | modeflags;
			this._isDelimited = (modeflags & LexerMode.XmlDocCommentStyleDelimited) > LexerMode.XmlDocCommentLocationStart;
		}

		// Token: 0x06004868 RID: 18536 RVA: 0x0011D288 File Offset: 0x0011B488
		private LexerMode SetMode(LexerMode mode)
		{
			LexerMode mode2 = base.Mode;
			base.Mode = mode | (mode2 & (LexerMode)4128768);
			return mode2;
		}

		// Token: 0x06004869 RID: 18537 RVA: 0x0011D2AC File Offset: 0x0011B4AC
		private void ResetMode(LexerMode mode)
		{
			base.Mode = mode;
		}

		// Token: 0x0600486A RID: 18538 RVA: 0x0011D2B8 File Offset: 0x0011B4B8
		public DocumentationCommentTriviaSyntax ParseDocumentationComment(out bool isTerminated)
		{
			SyntaxListBuilder<XmlNodeSyntax> syntaxListBuilder = this._pool.Allocate<XmlNodeSyntax>();
			DocumentationCommentTriviaSyntax documentationCommentTriviaSyntax;
			try
			{
				this.ParseXmlNodes(syntaxListBuilder);
				if (base.CurrentToken.Kind != SyntaxKind.EndOfDocumentationCommentToken)
				{
					this.ParseRemainder(syntaxListBuilder);
				}
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EndOfDocumentationCommentToken);
				isTerminated = !this._isDelimited || (syntaxToken.LeadingTrivia.Count > 0 && syntaxToken.LeadingTrivia[syntaxToken.LeadingTrivia.Count - 1].ToString() == "*/");
				documentationCommentTriviaSyntax = SyntaxFactory.DocumentationCommentTrivia(this._isDelimited ? SyntaxKind.MultiLineDocumentationCommentTrivia : SyntaxKind.SingleLineDocumentationCommentTrivia, syntaxListBuilder.ToList(), syntaxToken);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return documentationCommentTriviaSyntax;
		}

		// Token: 0x0600486B RID: 18539 RVA: 0x0011D394 File Offset: 0x0011B594
		public void ParseRemainder(SyntaxListBuilder<XmlNodeSyntax> nodes)
		{
			bool flag = base.CurrentToken.Kind == SyntaxKind.LessThanSlashToken;
			LexerMode lexerMode = this.SetMode(LexerMode.XmlCDataSectionText);
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			try
			{
				while (base.CurrentToken.Kind != SyntaxKind.EndOfDocumentationCommentToken)
				{
					SyntaxToken syntaxToken = base.EatToken();
					syntaxListBuilder.Add(syntaxToken);
				}
				XmlTextSyntax xmlTextSyntax = SyntaxFactory.XmlText(syntaxListBuilder.ToList());
				XmlParseErrorCode xmlParseErrorCode = (flag ? XmlParseErrorCode.XML_EndTagNotExpected : XmlParseErrorCode.XML_ExpectedEndOfXml);
				xmlTextSyntax = this.WithAdditionalDiagnostics<XmlTextSyntax>(xmlTextSyntax, new DiagnosticInfo[]
				{
					new XmlSyntaxDiagnosticInfo(0, 1, xmlParseErrorCode, Array.Empty<object>())
				});
				nodes.Add(xmlTextSyntax);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			this.ResetMode(lexerMode);
		}

		// Token: 0x0600486C RID: 18540 RVA: 0x0011D45C File Offset: 0x0011B65C
		private void ParseXmlNodes(SyntaxListBuilder<XmlNodeSyntax> nodes)
		{
			for (;;)
			{
				XmlNodeSyntax xmlNodeSyntax = this.ParseXmlNode();
				if (xmlNodeSyntax == null)
				{
					break;
				}
				nodes.Add(xmlNodeSyntax);
			}
		}

		// Token: 0x0600486D RID: 18541 RVA: 0x0011D480 File Offset: 0x0011B680
		private XmlNodeSyntax ParseXmlNode()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.XmlProcessingInstructionStartToken)
			{
				if (kind == SyntaxKind.LessThanToken)
				{
					return this.ParseXmlElement();
				}
				switch (kind)
				{
				case SyntaxKind.XmlCommentStartToken:
					return this.ParseXmlComment();
				case SyntaxKind.XmlCDataStartToken:
					return this.ParseXmlCDataSection();
				case SyntaxKind.XmlProcessingInstructionStartToken:
					return this.ParseXmlProcessingInstruction();
				}
			}
			else
			{
				if (kind == SyntaxKind.EndOfDocumentationCommentToken)
				{
					return null;
				}
				if (kind - SyntaxKind.XmlEntityLiteralToken <= 2)
				{
					return this.ParseXmlText();
				}
			}
			return null;
		}

		// Token: 0x0600486E RID: 18542 RVA: 0x0011D504 File Offset: 0x0011B704
		private bool IsXmlNodeStartOrStop()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind <= SyntaxKind.GreaterThanToken)
			{
				if (kind != SyntaxKind.LessThanToken && kind != SyntaxKind.GreaterThanToken)
				{
					return false;
				}
			}
			else
			{
				switch (kind)
				{
				case SyntaxKind.SlashGreaterThanToken:
				case SyntaxKind.LessThanSlashToken:
				case SyntaxKind.XmlCommentStartToken:
				case SyntaxKind.XmlCDataStartToken:
				case SyntaxKind.XmlProcessingInstructionStartToken:
					break;
				case SyntaxKind.XmlCommentEndToken:
				case SyntaxKind.XmlCDataEndToken:
					return false;
				default:
					if (kind != SyntaxKind.EndOfDocumentationCommentToken)
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x0600486F RID: 18543 RVA: 0x0011D56C File Offset: 0x0011B76C
		private XmlNodeSyntax ParseXmlText()
		{
			SyntaxListBuilder syntaxListBuilder = this._pool.Allocate();
			while (base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralToken || base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralNewLineToken || base.CurrentToken.Kind == SyntaxKind.XmlEntityLiteralToken)
			{
				syntaxListBuilder.Add(base.EatToken());
			}
			SyntaxList<GreenNode> syntaxList = syntaxListBuilder.ToList();
			this._pool.Free(syntaxListBuilder);
			return SyntaxFactory.XmlText(syntaxList);
		}

		// Token: 0x06004870 RID: 18544 RVA: 0x0011D5E8 File Offset: 0x0011B7E8
		private XmlNodeSyntax ParseXmlElement()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.LessThanToken);
			LexerMode lexerMode = this.SetMode(LexerMode.XmlElementTag);
			XmlNameSyntax xmlNameSyntax = this.ParseXmlName();
			if (syntaxToken.GetTrailingTriviaWidth() > 0 || xmlNameSyntax.GetLeadingTriviaWidth() > 0)
			{
				xmlNameSyntax = this.WithXmlParseError<XmlNameSyntax>(xmlNameSyntax, XmlParseErrorCode.XML_InvalidWhitespace);
			}
			SyntaxListBuilder<XmlAttributeSyntax> syntaxListBuilder = this._pool.Allocate<XmlAttributeSyntax>();
			XmlNodeSyntax xmlNodeSyntax;
			try
			{
				this.ParseXmlAttributes(ref xmlNameSyntax, syntaxListBuilder);
				if (base.CurrentToken.Kind == SyntaxKind.GreaterThanToken)
				{
					XmlElementStartTagSyntax xmlElementStartTagSyntax = SyntaxFactory.XmlElementStartTag(syntaxToken, xmlNameSyntax, syntaxListBuilder, base.EatToken());
					this.SetMode(LexerMode.XmlDocComment);
					SyntaxListBuilder<XmlNodeSyntax> syntaxListBuilder2 = this._pool.Allocate<XmlNodeSyntax>();
					try
					{
						this.ParseXmlNodes(syntaxListBuilder2);
						SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.LessThanSlashToken, false);
						XmlNameSyntax xmlNameSyntax2;
						SyntaxToken syntaxToken3;
						if (syntaxToken2.IsMissing)
						{
							this.ResetMode(lexerMode);
							syntaxToken2 = this.WithXmlParseError(syntaxToken2, XmlParseErrorCode.XML_EndTagExpected, new string[] { xmlNameSyntax.ToString() });
							xmlNameSyntax2 = SyntaxFactory.XmlName(null, SyntaxFactory.MissingToken(SyntaxKind.IdentifierToken));
							syntaxToken3 = SyntaxFactory.MissingToken(SyntaxKind.GreaterThanToken);
						}
						else
						{
							this.SetMode(LexerMode.XmlElementTag);
							xmlNameSyntax2 = this.ParseXmlName();
							if (syntaxToken2.GetTrailingTriviaWidth() > 0 || xmlNameSyntax2.GetLeadingTriviaWidth() > 0)
							{
								xmlNameSyntax2 = this.WithXmlParseError<XmlNameSyntax>(xmlNameSyntax2, XmlParseErrorCode.XML_InvalidWhitespace);
							}
							if (!xmlNameSyntax2.IsMissing && !DocumentationCommentParser.MatchingXmlNames(xmlNameSyntax, xmlNameSyntax2))
							{
								xmlNameSyntax2 = this.WithXmlParseError<XmlNameSyntax>(xmlNameSyntax2, XmlParseErrorCode.XML_ElementTypeMatch, new string[]
								{
									xmlNameSyntax2.ToString(),
									xmlNameSyntax.ToString()
								});
							}
							if (base.CurrentToken.Kind != SyntaxKind.GreaterThanToken)
							{
								this.SkipBadTokens<XmlNameSyntax>(ref xmlNameSyntax2, null, (DocumentationCommentParser p) => p.CurrentToken.Kind != SyntaxKind.GreaterThanToken, (DocumentationCommentParser p) => p.IsXmlNodeStartOrStop(), XmlParseErrorCode.XML_InvalidToken);
							}
							syntaxToken3 = base.EatToken(SyntaxKind.GreaterThanToken);
						}
						XmlElementEndTagSyntax xmlElementEndTagSyntax = SyntaxFactory.XmlElementEndTag(syntaxToken2, xmlNameSyntax2, syntaxToken3);
						this.ResetMode(lexerMode);
						return SyntaxFactory.XmlElement(xmlElementStartTagSyntax, syntaxListBuilder2.ToList(), xmlElementEndTagSyntax);
					}
					finally
					{
						this._pool.Free(syntaxListBuilder2);
					}
				}
				SyntaxToken syntaxToken4 = base.EatToken(SyntaxKind.SlashGreaterThanToken, false);
				if (syntaxToken4.IsMissing && !xmlNameSyntax.IsMissing)
				{
					syntaxToken4 = this.WithXmlParseError(syntaxToken4, XmlParseErrorCode.XML_ExpectedEndOfTag, new string[] { xmlNameSyntax.ToString() });
				}
				this.ResetMode(lexerMode);
				xmlNodeSyntax = SyntaxFactory.XmlEmptyElement(syntaxToken, xmlNameSyntax, syntaxListBuilder, syntaxToken4);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return xmlNodeSyntax;
		}

		// Token: 0x06004871 RID: 18545 RVA: 0x0011D88C File Offset: 0x0011BA8C
		private static bool MatchingXmlNames(XmlNameSyntax name, XmlNameSyntax endName)
		{
			return name == endName || (!name.HasLeadingTrivia && !endName.HasTrailingTrivia && name.IsEquivalentTo(endName)) || name.ToString() == endName.ToString();
		}

		// Token: 0x06004872 RID: 18546 RVA: 0x0011D8C0 File Offset: 0x0011BAC0
		private void ParseXmlAttributes(ref XmlNameSyntax elementName, SyntaxListBuilder<XmlAttributeSyntax> attrs)
		{
			this._attributesSeen.Clear();
			for (;;)
			{
				if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken)
				{
					XmlAttributeSyntax xmlAttributeSyntax = this.ParseXmlAttribute(elementName);
					string text = xmlAttributeSyntax.Name.ToString();
					if (this._attributesSeen.Contains(text))
					{
						xmlAttributeSyntax = this.WithXmlParseError<XmlAttributeSyntax>(xmlAttributeSyntax, XmlParseErrorCode.XML_DuplicateAttribute, new string[] { text });
					}
					else
					{
						this._attributesSeen.Add(text);
					}
					attrs.Add(xmlAttributeSyntax);
				}
				else if (this.SkipBadTokens<XmlNameSyntax>(ref elementName, attrs, (DocumentationCommentParser p) => p.CurrentToken.Kind != SyntaxKind.IdentifierName, (DocumentationCommentParser p) => p.CurrentToken.Kind == SyntaxKind.GreaterThanToken || p.CurrentToken.Kind == SyntaxKind.SlashGreaterThanToken || p.CurrentToken.Kind == SyntaxKind.LessThanToken || p.CurrentToken.Kind == SyntaxKind.LessThanSlashToken || p.CurrentToken.Kind == SyntaxKind.EndOfDocumentationCommentToken || p.CurrentToken.Kind == SyntaxKind.EndOfFileToken, XmlParseErrorCode.XML_InvalidToken) == DocumentationCommentParser.SkipResult.Abort)
				{
					break;
				}
			}
		}

		// Token: 0x06004873 RID: 18547 RVA: 0x0011D990 File Offset: 0x0011BB90
		private DocumentationCommentParser.SkipResult SkipBadTokens<T>(ref T startNode, SyntaxListBuilder list, Func<DocumentationCommentParser, bool> isNotExpectedFunction, Func<DocumentationCommentParser, bool> abortFunction, XmlParseErrorCode error) where T : CSharpSyntaxNode
		{
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = default(SyntaxListBuilder<SyntaxToken>);
			bool flag = false;
			DocumentationCommentParser.SkipResult skipResult2;
			try
			{
				DocumentationCommentParser.SkipResult skipResult = DocumentationCommentParser.SkipResult.Continue;
				while (isNotExpectedFunction.Invoke(this))
				{
					if (abortFunction.Invoke(this))
					{
						skipResult = DocumentationCommentParser.SkipResult.Abort;
						break;
					}
					if (syntaxListBuilder.IsNull)
					{
						syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
					}
					SyntaxToken syntaxToken = base.EatToken();
					if (!flag)
					{
						syntaxToken = this.WithXmlParseError(syntaxToken, error, new string[] { syntaxToken.ToString() });
						flag = true;
					}
					syntaxListBuilder.Add(syntaxToken);
				}
				if (!syntaxListBuilder.IsNull && syntaxListBuilder.Count > 0)
				{
					if (list == null || list.Count == 0)
					{
						startNode = base.AddTrailingSkippedSyntax<T>(startNode, syntaxListBuilder.ToListNode());
					}
					else
					{
						list[list.Count - 1] = base.AddTrailingSkippedSyntax<CSharpSyntaxNode>((CSharpSyntaxNode)list[list.Count - 1], syntaxListBuilder.ToListNode());
					}
					skipResult2 = skipResult;
				}
				else
				{
					skipResult2 = DocumentationCommentParser.SkipResult.Abort;
				}
			}
			finally
			{
				if (!syntaxListBuilder.IsNull)
				{
					this._pool.Free(syntaxListBuilder);
				}
			}
			return skipResult2;
		}

		// Token: 0x06004874 RID: 18548 RVA: 0x0011DAA4 File Offset: 0x0011BCA4
		private XmlAttributeSyntax ParseXmlAttribute(XmlNameSyntax elementName)
		{
			XmlNameSyntax xmlNameSyntax = this.ParseXmlName();
			if (xmlNameSyntax.GetLeadingTriviaWidth() == 0)
			{
				xmlNameSyntax = this.WithXmlParseError<XmlNameSyntax>(xmlNameSyntax, XmlParseErrorCode.XML_WhitespaceMissing);
			}
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.EqualsToken, false);
			if (syntaxToken.IsMissing)
			{
				syntaxToken = this.WithXmlParseError(syntaxToken, XmlParseErrorCode.XML_MissingEqualsAttribute, Array.Empty<string>());
				SyntaxKind kind = base.CurrentToken.Kind;
				if (kind - SyntaxKind.DoubleQuoteToken > 1)
				{
					return SyntaxFactory.XmlTextAttribute(xmlNameSyntax, syntaxToken, SyntaxFactory.MissingToken(SyntaxKind.DoubleQuoteToken), default(SyntaxList<SyntaxToken>), SyntaxFactory.MissingToken(SyntaxKind.DoubleQuoteToken));
				}
			}
			string valueText = xmlNameSyntax.LocalName.ValueText;
			bool flag = xmlNameSyntax.Prefix == null;
			if (flag && DocumentationCommentXmlNames.AttributeEquals(valueText, "cref") && !this.IsVerbatimCref())
			{
				SyntaxToken syntaxToken2;
				CrefSyntax crefSyntax;
				SyntaxToken syntaxToken3;
				this.ParseCrefAttribute(out syntaxToken2, out crefSyntax, out syntaxToken3);
				return SyntaxFactory.XmlCrefAttribute(xmlNameSyntax, syntaxToken, syntaxToken2, crefSyntax, syntaxToken3);
			}
			if (flag && DocumentationCommentXmlNames.AttributeEquals(valueText, "name") && DocumentationCommentParser.XmlElementSupportsNameAttribute(elementName))
			{
				SyntaxToken syntaxToken2;
				SyntaxToken syntaxToken3;
				IdentifierNameSyntax identifierNameSyntax;
				this.ParseNameAttribute(out syntaxToken2, out identifierNameSyntax, out syntaxToken3);
				return SyntaxFactory.XmlNameAttribute(xmlNameSyntax, syntaxToken, syntaxToken2, identifierNameSyntax, syntaxToken3);
			}
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
			XmlAttributeSyntax xmlAttributeSyntax;
			try
			{
				SyntaxToken syntaxToken2;
				SyntaxToken syntaxToken3;
				this.ParseXmlAttributeText(out syntaxToken2, syntaxListBuilder, out syntaxToken3);
				xmlAttributeSyntax = SyntaxFactory.XmlTextAttribute(xmlNameSyntax, syntaxToken, syntaxToken2, syntaxListBuilder, syntaxToken3);
			}
			finally
			{
				this._pool.Free(syntaxListBuilder);
			}
			return xmlAttributeSyntax;
		}

		// Token: 0x06004875 RID: 18549 RVA: 0x0011DBFC File Offset: 0x0011BDFC
		private static bool XmlElementSupportsNameAttribute(XmlNameSyntax elementName)
		{
			if (elementName.Prefix != null)
			{
				return false;
			}
			string valueText = elementName.LocalName.ValueText;
			return DocumentationCommentXmlNames.ElementEquals(valueText, "param", false) || DocumentationCommentXmlNames.ElementEquals(valueText, "paramref", false) || DocumentationCommentXmlNames.ElementEquals(valueText, "typeparam", false) || DocumentationCommentXmlNames.ElementEquals(valueText, "typeparamref", false);
		}

		// Token: 0x06004876 RID: 18550 RVA: 0x0011DC58 File Offset: 0x0011BE58
		private bool IsVerbatimCref()
		{
			bool flag = false;
			SyntaxParser.ResetPoint resetPoint = base.GetResetPoint();
			SyntaxToken syntaxToken = base.EatToken((base.CurrentToken.Kind == SyntaxKind.SingleQuoteToken) ? SyntaxKind.SingleQuoteToken : SyntaxKind.DoubleQuoteToken);
			this.SetMode(LexerMode.XmlCharacter);
			SyntaxToken syntaxToken2 = base.CurrentToken;
			if ((syntaxToken2.Kind == SyntaxKind.XmlTextLiteralToken || syntaxToken2.Kind == SyntaxKind.XmlEntityLiteralToken) && syntaxToken2.ValueText != SyntaxFacts.GetText(syntaxToken.Kind) && syntaxToken2.ValueText != ":")
			{
				base.EatToken();
				syntaxToken2 = base.CurrentToken;
				if ((syntaxToken2.Kind == SyntaxKind.XmlTextLiteralToken || syntaxToken2.Kind == SyntaxKind.XmlEntityLiteralToken) && syntaxToken2.ValueText == ":")
				{
					flag = true;
				}
			}
			base.Reset(ref resetPoint);
			base.Release(ref resetPoint);
			return flag;
		}

		// Token: 0x06004877 RID: 18551 RVA: 0x0011DD38 File Offset: 0x0011BF38
		private void ParseCrefAttribute(out SyntaxToken startQuote, out CrefSyntax cref, out SyntaxToken endQuote)
		{
			startQuote = this.ParseXmlAttributeStartQuote();
			SyntaxKind kind = startQuote.Kind;
			LexerMode lexerMode = this.SetMode((kind == SyntaxKind.SingleQuoteToken) ? LexerMode.XmlCrefQuote : LexerMode.XmlCrefDoubleQuote);
			cref = this.ParseCrefAttributeValue();
			this.ResetMode(lexerMode);
			endQuote = this.ParseXmlAttributeEndQuote(kind);
		}

		// Token: 0x06004878 RID: 18552 RVA: 0x0011DD88 File Offset: 0x0011BF88
		private void ParseNameAttribute(out SyntaxToken startQuote, out IdentifierNameSyntax identifier, out SyntaxToken endQuote)
		{
			startQuote = this.ParseXmlAttributeStartQuote();
			SyntaxKind kind = startQuote.Kind;
			LexerMode lexerMode = this.SetMode((kind == SyntaxKind.SingleQuoteToken) ? LexerMode.XmlNameQuote : LexerMode.XmlNameDoubleQuote);
			identifier = this.ParseNameAttributeValue();
			this.ResetMode(lexerMode);
			endQuote = this.ParseXmlAttributeEndQuote(kind);
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x0011DDD8 File Offset: 0x0011BFD8
		private void ParseXmlAttributeText(out SyntaxToken startQuote, SyntaxListBuilder<SyntaxToken> textTokens, out SyntaxToken endQuote)
		{
			startQuote = this.ParseXmlAttributeStartQuote();
			SyntaxKind kind = startQuote.Kind;
			if (startQuote.IsMissing && startQuote.FullWidth == 0)
			{
				endQuote = SyntaxFactory.MissingToken(kind);
				return;
			}
			LexerMode lexerMode = this.SetMode((kind == SyntaxKind.SingleQuoteToken) ? LexerMode.XmlAttributeTextQuote : LexerMode.XmlAttributeTextDoubleQuote);
			while (base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralToken || base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralNewLineToken || base.CurrentToken.Kind == SyntaxKind.XmlEntityLiteralToken || base.CurrentToken.Kind == SyntaxKind.LessThanToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				if (syntaxToken.Kind == SyntaxKind.LessThanToken)
				{
					syntaxToken = this.WithXmlParseError(syntaxToken, XmlParseErrorCode.XML_LessThanInAttributeValue, Array.Empty<string>());
				}
				textTokens.Add(syntaxToken);
			}
			this.ResetMode(lexerMode);
			endQuote = this.ParseXmlAttributeEndQuote(kind);
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x0011DEAC File Offset: 0x0011C0AC
		private SyntaxToken ParseXmlAttributeStartQuote()
		{
			if (DocumentationCommentParser.IsNonAsciiQuotationMark(base.CurrentToken))
			{
				return this.SkipNonAsciiQuotationMark();
			}
			SyntaxKind syntaxKind = ((base.CurrentToken.Kind == SyntaxKind.SingleQuoteToken) ? SyntaxKind.SingleQuoteToken : SyntaxKind.DoubleQuoteToken);
			SyntaxToken syntaxToken = base.EatToken(syntaxKind, false);
			if (syntaxToken.IsMissing)
			{
				syntaxToken = this.WithXmlParseError(syntaxToken, XmlParseErrorCode.XML_StringLiteralNoStartQuote, Array.Empty<string>());
			}
			return syntaxToken;
		}

		// Token: 0x0600487B RID: 18555 RVA: 0x0011DF0C File Offset: 0x0011C10C
		private SyntaxToken ParseXmlAttributeEndQuote(SyntaxKind quoteKind)
		{
			if (DocumentationCommentParser.IsNonAsciiQuotationMark(base.CurrentToken))
			{
				return this.SkipNonAsciiQuotationMark();
			}
			SyntaxToken syntaxToken = base.EatToken(quoteKind, false);
			if (syntaxToken.IsMissing)
			{
				syntaxToken = this.WithXmlParseError(syntaxToken, XmlParseErrorCode.XML_StringLiteralNoEndQuote, Array.Empty<string>());
			}
			return syntaxToken;
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x0011DF50 File Offset: 0x0011C150
		private SyntaxToken SkipNonAsciiQuotationMark()
		{
			SyntaxToken syntaxToken = SyntaxFactory.MissingToken(SyntaxKind.DoubleQuoteToken);
			syntaxToken = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken, base.EatToken());
			return this.WithXmlParseError(syntaxToken, XmlParseErrorCode.XML_StringLiteralNonAsciiQuote, Array.Empty<string>());
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x0011DF85 File Offset: 0x0011C185
		private static bool IsNonAsciiQuotationMark(SyntaxToken token)
		{
			return token.Text.Length == 1 && SyntaxFacts.IsNonAsciiQuotationMark(token.Text.get_Chars(0));
		}

		// Token: 0x0600487E RID: 18558 RVA: 0x0011DFA8 File Offset: 0x0011C1A8
		private XmlNameSyntax ParseXmlName()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.IdentifierToken);
			XmlPrefixSyntax xmlPrefixSyntax = null;
			if (base.CurrentToken.Kind == SyntaxKind.ColonToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken();
				int trailingTriviaWidth = syntaxToken.GetTrailingTriviaWidth();
				int leadingTriviaWidth = syntaxToken2.GetLeadingTriviaWidth();
				if (trailingTriviaWidth > 0 || leadingTriviaWidth > 0)
				{
					int num = -trailingTriviaWidth;
					int num2 = trailingTriviaWidth + leadingTriviaWidth;
					syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[]
					{
						new XmlSyntaxDiagnosticInfo(num, num2, XmlParseErrorCode.XML_InvalidWhitespace, Array.Empty<object>())
					});
				}
				xmlPrefixSyntax = SyntaxFactory.XmlPrefix(syntaxToken, syntaxToken2);
				syntaxToken = base.EatToken(SyntaxKind.IdentifierToken);
				int trailingTriviaWidth2 = syntaxToken2.GetTrailingTriviaWidth();
				int leadingTriviaWidth2 = syntaxToken.GetLeadingTriviaWidth();
				if (trailingTriviaWidth2 > 0 || leadingTriviaWidth2 > 0)
				{
					int num3 = -trailingTriviaWidth2;
					int num4 = trailingTriviaWidth2 + leadingTriviaWidth2;
					syntaxToken = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken, new DiagnosticInfo[]
					{
						new XmlSyntaxDiagnosticInfo(num3, num4, XmlParseErrorCode.XML_InvalidWhitespace, Array.Empty<object>())
					});
				}
			}
			return SyntaxFactory.XmlName(xmlPrefixSyntax, syntaxToken);
		}

		// Token: 0x0600487F RID: 18559 RVA: 0x0011E084 File Offset: 0x0011C284
		private XmlCommentSyntax ParseXmlComment()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.XmlCommentStartToken);
			LexerMode lexerMode = this.SetMode(LexerMode.XmlCommentText);
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
			while (base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralToken || base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralNewLineToken || base.CurrentToken.Kind == SyntaxKind.MinusMinusToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken();
				if (syntaxToken2.Kind == SyntaxKind.MinusMinusToken)
				{
					syntaxToken2 = this.WithXmlParseError(syntaxToken2, XmlParseErrorCode.XML_IncorrectComment, Array.Empty<string>());
				}
				syntaxListBuilder.Add(syntaxToken2);
			}
			SyntaxList<SyntaxToken> syntaxList = syntaxListBuilder.ToList();
			this._pool.Free(syntaxListBuilder);
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.XmlCommentEndToken);
			this.ResetMode(lexerMode);
			return SyntaxFactory.XmlComment(syntaxToken, syntaxList, syntaxToken3);
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x0011E154 File Offset: 0x0011C354
		private XmlCDataSectionSyntax ParseXmlCDataSection()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.XmlCDataStartToken);
			LexerMode lexerMode = this.SetMode(LexerMode.XmlCDataSectionText);
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = new SyntaxListBuilder<SyntaxToken>(10);
			while (base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralToken || base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralNewLineToken)
			{
				syntaxListBuilder.Add(base.EatToken());
			}
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.XmlCDataEndToken);
			this.ResetMode(lexerMode);
			return SyntaxFactory.XmlCDataSection(syntaxToken, syntaxListBuilder, syntaxToken2);
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x0011E1D8 File Offset: 0x0011C3D8
		private XmlProcessingInstructionSyntax ParseXmlProcessingInstruction()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.XmlProcessingInstructionStartToken);
			LexerMode lexerMode = this.SetMode(LexerMode.XmlElementTag);
			XmlNameSyntax xmlNameSyntax = this.ParseXmlName();
			this.SetMode(LexerMode.XmlProcessingInstructionText);
			SyntaxListBuilder<SyntaxToken> syntaxListBuilder = new SyntaxListBuilder<SyntaxToken>(10);
			while (base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralToken || base.CurrentToken.Kind == SyntaxKind.XmlTextLiteralNewLineToken)
			{
				SyntaxToken syntaxToken2 = base.EatToken();
				syntaxListBuilder.Add(syntaxToken2);
			}
			SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.XmlProcessingInstructionEndToken);
			this.ResetMode(lexerMode);
			return SyntaxFactory.XmlProcessingInstruction(syntaxToken, xmlNameSyntax, syntaxListBuilder, syntaxToken3);
		}

		// Token: 0x06004882 RID: 18562 RVA: 0x0011E274 File Offset: 0x0011C474
		protected override SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual, int offset, int length)
		{
			if (this.InCref)
			{
				SyntaxDiagnosticInfo expectedTokenError = base.GetExpectedTokenError(expected, actual, offset, length);
				return new SyntaxDiagnosticInfo(expectedTokenError.Offset, expectedTokenError.Width, ErrorCode.WRN_ErrorOverride, new object[] { expectedTokenError, expectedTokenError.Code });
			}
			if (expected == SyntaxKind.IdentifierToken)
			{
				return new XmlSyntaxDiagnosticInfo(offset, length, XmlParseErrorCode.XML_ExpectedIdentifier, Array.Empty<object>());
			}
			return new XmlSyntaxDiagnosticInfo(offset, length, XmlParseErrorCode.XML_InvalidToken, new object[] { SyntaxFacts.GetText(actual) });
		}

		// Token: 0x06004883 RID: 18563 RVA: 0x0011E2F8 File Offset: 0x0011C4F8
		protected override SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual)
		{
			if (this.InCref)
			{
				int num;
				int num2;
				base.GetDiagnosticSpanForMissingToken(out num, out num2);
				return this.GetExpectedTokenError(expected, actual, num, num2);
			}
			if (expected == SyntaxKind.IdentifierToken)
			{
				return new XmlSyntaxDiagnosticInfo(XmlParseErrorCode.XML_ExpectedIdentifier, Array.Empty<object>());
			}
			return new XmlSyntaxDiagnosticInfo(XmlParseErrorCode.XML_InvalidToken, new object[] { SyntaxFacts.GetText(actual) });
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x0011E350 File Offset: 0x0011C550
		private TNode WithXmlParseError<TNode>(TNode node, XmlParseErrorCode code) where TNode : CSharpSyntaxNode
		{
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[]
			{
				new XmlSyntaxDiagnosticInfo(0, node.Width, code, Array.Empty<object>())
			});
		}

		// Token: 0x06004885 RID: 18565 RVA: 0x0011E384 File Offset: 0x0011C584
		private TNode WithXmlParseError<TNode>(TNode node, XmlParseErrorCode code, params string[] args) where TNode : CSharpSyntaxNode
		{
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[]
			{
				new XmlSyntaxDiagnosticInfo(0, node.Width, code, args)
			});
		}

		// Token: 0x06004886 RID: 18566 RVA: 0x0011E3B8 File Offset: 0x0011C5B8
		private SyntaxToken WithXmlParseError(SyntaxToken node, XmlParseErrorCode code, params string[] args)
		{
			return this.WithAdditionalDiagnostics<SyntaxToken>(node, new DiagnosticInfo[]
			{
				new XmlSyntaxDiagnosticInfo(0, node.Width, code, args)
			});
		}

		// Token: 0x06004887 RID: 18567 RVA: 0x0011E3E5 File Offset: 0x0011C5E5
		protected override TNode WithAdditionalDiagnostics<TNode>(TNode node, params DiagnosticInfo[] diagnostics)
		{
			if (base.Options.DocumentationMode < DocumentationMode.Diagnose)
			{
				return node;
			}
			return base.WithAdditionalDiagnostics<TNode>(node, diagnostics);
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x0011E400 File Offset: 0x0011C600
		private CrefSyntax ParseCrefAttributeValue()
		{
			TypeSyntax typeSyntax = this.ParseCrefType(true, true);
			CrefSyntax crefSyntax;
			if (typeSyntax == null)
			{
				crefSyntax = this.ParseMemberCref();
			}
			else if (this.IsEndOfCrefAttribute)
			{
				crefSyntax = SyntaxFactory.TypeCref(typeSyntax);
			}
			else if (typeSyntax.Kind != SyntaxKind.QualifiedName && base.CurrentToken.Kind == SyntaxKind.OpenParenToken)
			{
				CrefParameterListSyntax crefParameterListSyntax = this.ParseCrefParameterList();
				crefSyntax = SyntaxFactory.NameMemberCref(typeSyntax, crefParameterListSyntax);
			}
			else
			{
				SyntaxToken syntaxToken = base.EatToken(SyntaxKind.DotToken);
				MemberCrefSyntax memberCrefSyntax = this.ParseMemberCref();
				crefSyntax = SyntaxFactory.QualifiedCref(typeSyntax, syntaxToken, memberCrefSyntax);
			}
			bool flag = !this.IsEndOfCrefAttribute || crefSyntax.ContainsDiagnostics;
			if (!this.IsEndOfCrefAttribute)
			{
				SyntaxListBuilder<SyntaxToken> syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
				while (!this.IsEndOfCrefAttribute)
				{
					syntaxListBuilder.Add(base.EatToken());
				}
				crefSyntax = base.AddTrailingSkippedSyntax<CrefSyntax>(crefSyntax, syntaxListBuilder.ToListNode());
				this._pool.Free(syntaxListBuilder);
			}
			if (flag)
			{
				crefSyntax = base.AddError<CrefSyntax>(crefSyntax, ErrorCode.WRN_BadXMLRefSyntax, new object[] { crefSyntax.ToFullString() });
			}
			return crefSyntax;
		}

		// Token: 0x06004889 RID: 18569 RVA: 0x0011E504 File Offset: 0x0011C704
		private MemberCrefSyntax ParseMemberCref()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind == SyntaxKind.ThisKeyword)
			{
				return this.ParseIndexerMemberCref();
			}
			if (kind == SyntaxKind.OperatorKeyword)
			{
				return this.ParseOperatorMemberCref();
			}
			if (kind - SyntaxKind.ExplicitKeyword > 1)
			{
				return this.ParseNameMemberCref();
			}
			return this.ParseConversionOperatorMemberCref();
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x0011E554 File Offset: 0x0011C754
		private NameMemberCrefSyntax ParseNameMemberCref()
		{
			TypeSyntax typeSyntax = this.ParseCrefName(true);
			CrefParameterListSyntax crefParameterListSyntax = this.ParseCrefParameterList();
			return SyntaxFactory.NameMemberCref(typeSyntax, crefParameterListSyntax);
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x0011E578 File Offset: 0x0011C778
		private IndexerMemberCrefSyntax ParseIndexerMemberCref()
		{
			SyntaxToken syntaxToken = base.EatToken();
			CrefBracketedParameterListSyntax crefBracketedParameterListSyntax = this.ParseBracketedCrefParameterList();
			return SyntaxFactory.IndexerMemberCref(syntaxToken, crefBracketedParameterListSyntax);
		}

		// Token: 0x0600488C RID: 18572 RVA: 0x0011E598 File Offset: 0x0011C798
		private OperatorMemberCrefSyntax ParseOperatorMemberCref()
		{
			SyntaxToken syntaxToken = base.EatToken();
			SyntaxToken syntaxToken2;
			if (SyntaxFacts.IsAnyOverloadableOperator(base.CurrentToken.Kind))
			{
				syntaxToken2 = base.EatToken();
			}
			else
			{
				syntaxToken2 = SyntaxFactory.MissingToken(SyntaxKind.PlusToken);
				int num;
				int num2;
				base.GetDiagnosticSpanForMissingToken(out num, out num2);
				if (SyntaxFacts.IsUnaryOperatorDeclarationToken(base.CurrentToken.Kind) || SyntaxFacts.IsBinaryExpressionOperatorToken(base.CurrentToken.Kind))
				{
					syntaxToken2 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken2, base.EatToken());
				}
				SyntaxDiagnosticInfo syntaxDiagnosticInfo = new SyntaxDiagnosticInfo(num, num2, ErrorCode.ERR_OvlOperatorExpected);
				SyntaxDiagnosticInfo syntaxDiagnosticInfo2 = new SyntaxDiagnosticInfo(num, num2, ErrorCode.WRN_ErrorOverride, new object[] { syntaxDiagnosticInfo, syntaxDiagnosticInfo.Code });
				syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { syntaxDiagnosticInfo2 });
			}
			if (syntaxToken2.Kind == SyntaxKind.GreaterThanToken && syntaxToken2.GetTrailingTriviaWidth() == 0 && base.CurrentToken.GetLeadingTriviaWidth() == 0)
			{
				if (base.CurrentToken.Kind == SyntaxKind.GreaterThanToken)
				{
					SyntaxToken syntaxToken3 = base.EatToken();
					syntaxToken2 = SyntaxFactory.Token(syntaxToken2.GetLeadingTrivia(), SyntaxKind.GreaterThanGreaterThanToken, syntaxToken2.Text + syntaxToken3.Text, syntaxToken2.ValueText + syntaxToken3.ValueText, syntaxToken3.GetTrailingTrivia());
				}
				else if (base.CurrentToken.Kind == SyntaxKind.EqualsToken)
				{
					SyntaxToken syntaxToken4 = base.EatToken();
					syntaxToken2 = SyntaxFactory.Token(syntaxToken2.GetLeadingTrivia(), SyntaxKind.GreaterThanEqualsToken, syntaxToken2.Text + syntaxToken4.Text, syntaxToken2.ValueText + syntaxToken4.ValueText, syntaxToken4.GetTrailingTrivia());
				}
				else if (base.CurrentToken.Kind == SyntaxKind.GreaterThanEqualsToken)
				{
					SyntaxToken syntaxToken5 = base.EatToken();
					SyntaxToken syntaxToken6 = SyntaxFactory.Token(syntaxToken2.GetLeadingTrivia(), SyntaxKind.GreaterThanGreaterThanEqualsToken, syntaxToken2.Text + syntaxToken5.Text, syntaxToken2.ValueText + syntaxToken5.ValueText, syntaxToken5.GetTrailingTrivia());
					syntaxToken2 = SyntaxFactory.MissingToken(SyntaxKind.PlusToken);
					syntaxToken2 = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken2, syntaxToken6);
					int width = syntaxToken6.Width;
					SyntaxDiagnosticInfo syntaxDiagnosticInfo3 = new SyntaxDiagnosticInfo(0, width, ErrorCode.ERR_OvlOperatorExpected);
					SyntaxDiagnosticInfo syntaxDiagnosticInfo4 = new SyntaxDiagnosticInfo(0, width, ErrorCode.WRN_ErrorOverride, new object[] { syntaxDiagnosticInfo3, syntaxDiagnosticInfo3.Code });
					syntaxToken2 = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken2, new DiagnosticInfo[] { syntaxDiagnosticInfo4 });
				}
			}
			CrefParameterListSyntax crefParameterListSyntax = this.ParseCrefParameterList();
			return SyntaxFactory.OperatorMemberCref(syntaxToken, syntaxToken2, crefParameterListSyntax);
		}

		// Token: 0x0600488D RID: 18573 RVA: 0x0011E810 File Offset: 0x0011CA10
		private ConversionOperatorMemberCrefSyntax ParseConversionOperatorMemberCref()
		{
			SyntaxToken syntaxToken = base.EatToken();
			SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.OperatorKeyword);
			TypeSyntax typeSyntax = this.ParseCrefType(false, false);
			CrefParameterListSyntax crefParameterListSyntax = this.ParseCrefParameterList();
			return SyntaxFactory.ConversionOperatorMemberCref(syntaxToken, syntaxToken2, typeSyntax, crefParameterListSyntax);
		}

		// Token: 0x0600488E RID: 18574 RVA: 0x0011E847 File Offset: 0x0011CA47
		private CrefParameterListSyntax ParseCrefParameterList()
		{
			return (CrefParameterListSyntax)this.ParseBaseCrefParameterList(false);
		}

		// Token: 0x0600488F RID: 18575 RVA: 0x0011E855 File Offset: 0x0011CA55
		private CrefBracketedParameterListSyntax ParseBracketedCrefParameterList()
		{
			return (CrefBracketedParameterListSyntax)this.ParseBaseCrefParameterList(true);
		}

		// Token: 0x06004890 RID: 18576 RVA: 0x0011E864 File Offset: 0x0011CA64
		private BaseCrefParameterListSyntax ParseBaseCrefParameterList(bool useSquareBrackets)
		{
			SyntaxKind syntaxKind = (useSquareBrackets ? SyntaxKind.OpenBracketToken : SyntaxKind.OpenParenToken);
			SyntaxKind syntaxKind2 = (useSquareBrackets ? SyntaxKind.CloseBracketToken : SyntaxKind.CloseParenToken);
			if (base.CurrentToken.Kind != syntaxKind)
			{
				return null;
			}
			SyntaxToken syntaxToken = base.EatToken(syntaxKind);
			SeparatedSyntaxListBuilder<CrefParameterSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<CrefParameterSyntax>();
			BaseCrefParameterListSyntax baseCrefParameterListSyntax;
			try
			{
				while (base.CurrentToken.Kind == SyntaxKind.CommaToken || this.IsPossibleCrefParameter())
				{
					separatedSyntaxListBuilder.Add(this.ParseCrefParameter());
					if (base.CurrentToken.Kind != syntaxKind2)
					{
						SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CommaToken);
						if (!syntaxToken2.IsMissing || this.IsPossibleCrefParameter())
						{
							separatedSyntaxListBuilder.AddSeparator(syntaxToken2);
						}
					}
				}
				SyntaxToken syntaxToken3 = base.EatToken(syntaxKind2);
				baseCrefParameterListSyntax = (useSquareBrackets ? SyntaxFactory.CrefBracketedParameterList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken3) : SyntaxFactory.CrefParameterList(syntaxToken, in separatedSyntaxListBuilder, syntaxToken3));
			}
			finally
			{
				this._pool.Free<CrefParameterSyntax>(in separatedSyntaxListBuilder);
			}
			return baseCrefParameterListSyntax;
		}

		// Token: 0x06004891 RID: 18577 RVA: 0x0011E964 File Offset: 0x0011CB64
		private bool IsPossibleCrefParameter()
		{
			SyntaxKind kind = base.CurrentToken.Kind;
			return kind - SyntaxKind.RefKeyword <= 2 || kind == SyntaxKind.IdentifierToken || SyntaxFacts.IsPredefinedType(kind);
		}

		// Token: 0x06004892 RID: 18578 RVA: 0x0011E998 File Offset: 0x0011CB98
		private CrefParameterSyntax ParseCrefParameter()
		{
			SyntaxToken syntaxToken = null;
			SyntaxKind kind = base.CurrentToken.Kind;
			if (kind - SyntaxKind.RefKeyword <= 2)
			{
				syntaxToken = base.EatToken();
			}
			TypeSyntax typeSyntax = this.ParseCrefType(false, false);
			return SyntaxFactory.CrefParameter(syntaxToken, typeSyntax);
		}

		// Token: 0x06004893 RID: 18579 RVA: 0x0011E9D4 File Offset: 0x0011CBD4
		private SimpleNameSyntax ParseCrefName(bool typeArgumentsMustBeIdentifiers)
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.IdentifierToken);
			if (base.CurrentToken.Kind != SyntaxKind.LessThanToken)
			{
				return SyntaxFactory.IdentifierName(syntaxToken);
			}
			SyntaxToken syntaxToken2 = base.EatToken();
			SeparatedSyntaxListBuilder<TypeSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<TypeSyntax>();
			SimpleNameSyntax simpleNameSyntax;
			try
			{
				for (;;)
				{
					TypeSyntax typeSyntax = this.ParseCrefType(typeArgumentsMustBeIdentifiers, false);
					if (typeArgumentsMustBeIdentifiers && typeSyntax.Kind != SyntaxKind.IdentifierName)
					{
						typeSyntax = base.AddError<TypeSyntax>(typeSyntax, ErrorCode.WRN_ErrorOverride, new object[]
						{
							new SyntaxDiagnosticInfo(ErrorCode.ERR_TypeParamMustBeIdentifier),
							string.Format("{0:d4}", 81)
						});
					}
					separatedSyntaxListBuilder.Add(typeSyntax);
					SyntaxKind kind = base.CurrentToken.Kind;
					if (kind != SyntaxKind.CommaToken && kind != SyntaxKind.IdentifierToken && !SyntaxFacts.IsPredefinedType(base.CurrentToken.Kind))
					{
						break;
					}
					separatedSyntaxListBuilder.AddSeparator(base.EatToken(SyntaxKind.CommaToken));
				}
				SyntaxToken syntaxToken3 = base.EatToken(SyntaxKind.GreaterThanToken);
				syntaxToken2 = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken2, MessageID.IDS_FeatureGenerics, true);
				simpleNameSyntax = SyntaxFactory.GenericName(syntaxToken, SyntaxFactory.TypeArgumentList(syntaxToken2, in separatedSyntaxListBuilder, syntaxToken3));
			}
			finally
			{
				this._pool.Free<TypeSyntax>(in separatedSyntaxListBuilder);
			}
			return simpleNameSyntax;
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x0011EB0C File Offset: 0x0011CD0C
		private TypeSyntax ParseCrefType(bool typeArgumentsMustBeIdentifiers, bool checkForMember = false)
		{
			TypeSyntax typeSyntax = this.ParseCrefTypeHelper(typeArgumentsMustBeIdentifiers, checkForMember);
			if (!typeArgumentsMustBeIdentifiers)
			{
				return this.ParseCrefTypeSuffix(typeSyntax);
			}
			return typeSyntax;
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x0011EB30 File Offset: 0x0011CD30
		private TypeSyntax ParseCrefTypeHelper(bool typeArgumentsMustBeIdentifiers, bool checkForMember = false)
		{
			if (SyntaxFacts.IsPredefinedType(base.CurrentToken.Kind))
			{
				return SyntaxFactory.PredefinedType(base.EatToken());
			}
			NameSyntax nameSyntax;
			if (base.CurrentToken.Kind == SyntaxKind.IdentifierToken && base.PeekToken(1).Kind == SyntaxKind.ColonColonToken)
			{
				SyntaxToken syntaxToken = base.EatToken();
				if (syntaxToken.ContextualKind == SyntaxKind.GlobalKeyword)
				{
					syntaxToken = SyntaxParser.ConvertToKeyword(syntaxToken);
				}
				syntaxToken = base.CheckFeatureAvailability<SyntaxToken>(syntaxToken, MessageID.IDS_FeatureGlobalNamespace, true);
				SyntaxToken syntaxToken2 = base.EatToken();
				SimpleNameSyntax simpleNameSyntax = this.ParseCrefName(typeArgumentsMustBeIdentifiers);
				nameSyntax = SyntaxFactory.AliasQualifiedName(SyntaxFactory.IdentifierName(syntaxToken), syntaxToken2, simpleNameSyntax);
			}
			else
			{
				SyntaxParser.ResetPoint resetPoint = base.GetResetPoint();
				nameSyntax = this.ParseCrefName(typeArgumentsMustBeIdentifiers);
				if (checkForMember && (nameSyntax.IsMissing || base.CurrentToken.Kind != SyntaxKind.DotToken))
				{
					base.Reset(ref resetPoint);
					base.Release(ref resetPoint);
					return null;
				}
				base.Release(ref resetPoint);
			}
			while (base.CurrentToken.Kind == SyntaxKind.DotToken)
			{
				SyntaxParser.ResetPoint resetPoint2 = base.GetResetPoint();
				SyntaxToken syntaxToken3 = base.EatToken();
				SimpleNameSyntax simpleNameSyntax2 = this.ParseCrefName(typeArgumentsMustBeIdentifiers);
				if (checkForMember && (simpleNameSyntax2.IsMissing || base.CurrentToken.Kind != SyntaxKind.DotToken))
				{
					base.Reset(ref resetPoint2);
					base.Release(ref resetPoint2);
					return nameSyntax;
				}
				base.Release(ref resetPoint2);
				nameSyntax = SyntaxFactory.QualifiedName(nameSyntax, syntaxToken3, simpleNameSyntax2);
			}
			return nameSyntax;
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x0011EC84 File Offset: 0x0011CE84
		private TypeSyntax ParseCrefTypeSuffix(TypeSyntax type)
		{
			if (base.CurrentToken.Kind == SyntaxKind.QuestionToken)
			{
				type = SyntaxFactory.NullableType(type, base.EatToken());
			}
			while (base.CurrentToken.Kind == SyntaxKind.AsteriskToken)
			{
				type = SyntaxFactory.PointerType(type, base.EatToken());
			}
			if (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
			{
				OmittedArraySizeExpressionSyntax omittedArraySizeExpressionSyntax = SyntaxFactory.OmittedArraySizeExpression(SyntaxFactory.Token(SyntaxKind.OmittedArraySizeExpressionToken));
				SyntaxListBuilder<ArrayRankSpecifierSyntax> syntaxListBuilder = this._pool.Allocate<ArrayRankSpecifierSyntax>();
				try
				{
					while (base.CurrentToken.Kind == SyntaxKind.OpenBracketToken)
					{
						SyntaxToken syntaxToken = base.EatToken();
						SeparatedSyntaxListBuilder<ExpressionSyntax> separatedSyntaxListBuilder = this._pool.AllocateSeparated<ExpressionSyntax>();
						try
						{
							while (base.CurrentToken.Kind != SyntaxKind.CloseBracketToken && base.CurrentToken.Kind == SyntaxKind.CommaToken)
							{
								separatedSyntaxListBuilder.Add(omittedArraySizeExpressionSyntax);
								separatedSyntaxListBuilder.AddSeparator(base.EatToken());
							}
							if ((separatedSyntaxListBuilder.Count & 1) == 0)
							{
								separatedSyntaxListBuilder.Add(omittedArraySizeExpressionSyntax);
							}
							SyntaxToken syntaxToken2 = base.EatToken(SyntaxKind.CloseBracketToken);
							syntaxListBuilder.Add(SyntaxFactory.ArrayRankSpecifier(syntaxToken, in separatedSyntaxListBuilder, syntaxToken2));
						}
						finally
						{
							this._pool.Free<ExpressionSyntax>(in separatedSyntaxListBuilder);
						}
					}
					type = SyntaxFactory.ArrayType(type, syntaxListBuilder);
				}
				finally
				{
					this._pool.Free(syntaxListBuilder);
				}
			}
			return type;
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x0011EDF4 File Offset: 0x0011CFF4
		private bool IsEndOfCrefAttribute
		{
			get
			{
				SyntaxKind kind = base.CurrentToken.Kind;
				if (kind <= SyntaxKind.SingleQuoteToken)
				{
					if (kind == SyntaxKind.DoubleQuoteToken)
					{
						return (base.Mode & LexerMode.XmlCrefDoubleQuote) == LexerMode.XmlCrefDoubleQuote;
					}
					if (kind == SyntaxKind.SingleQuoteToken)
					{
						return (base.Mode & LexerMode.XmlCrefQuote) == LexerMode.XmlCrefQuote;
					}
				}
				else
				{
					if (kind - SyntaxKind.EndOfDocumentationCommentToken <= 1)
					{
						return true;
					}
					if (kind == SyntaxKind.BadToken)
					{
						return base.CurrentToken.Text == SyntaxFacts.GetText(SyntaxKind.LessThanToken) || DocumentationCommentParser.IsNonAsciiQuotationMark(base.CurrentToken);
					}
				}
				return false;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x0011EE90 File Offset: 0x0011D090
		private bool InCref
		{
			get
			{
				LexerMode lexerMode = base.Mode & (LexerMode.XmlCrefQuote | LexerMode.XmlCrefDoubleQuote);
				return lexerMode == LexerMode.XmlCrefQuote || lexerMode == LexerMode.XmlCrefDoubleQuote;
			}
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x0011EEC0 File Offset: 0x0011D0C0
		private IdentifierNameSyntax ParseNameAttributeValue()
		{
			SyntaxToken syntaxToken = base.EatToken(SyntaxKind.IdentifierToken, false);
			if (!this.IsEndOfNameAttribute)
			{
				SyntaxListBuilder<SyntaxToken> syntaxListBuilder = this._pool.Allocate<SyntaxToken>();
				while (!this.IsEndOfNameAttribute)
				{
					syntaxListBuilder.Add(base.EatToken());
				}
				syntaxToken = base.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken, syntaxListBuilder.ToListNode());
				this._pool.Free(syntaxListBuilder);
			}
			return SyntaxFactory.IdentifierName(syntaxToken);
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x0011EF2C File Offset: 0x0011D12C
		private bool IsEndOfNameAttribute
		{
			get
			{
				SyntaxKind kind = base.CurrentToken.Kind;
				if (kind <= SyntaxKind.SingleQuoteToken)
				{
					if (kind == SyntaxKind.DoubleQuoteToken)
					{
						return (base.Mode & LexerMode.XmlNameDoubleQuote) == LexerMode.XmlNameDoubleQuote;
					}
					if (kind == SyntaxKind.SingleQuoteToken)
					{
						return (base.Mode & LexerMode.XmlNameQuote) == LexerMode.XmlNameQuote;
					}
				}
				else
				{
					if (kind - SyntaxKind.EndOfDocumentationCommentToken <= 1)
					{
						return true;
					}
					if (kind == SyntaxKind.BadToken)
					{
						return base.CurrentToken.Text == SyntaxFacts.GetText(SyntaxKind.LessThanToken) || DocumentationCommentParser.IsNonAsciiQuotationMark(base.CurrentToken);
					}
				}
				return false;
			}
		}

		// Token: 0x04001544 RID: 5444
		private readonly SyntaxListPool _pool = new SyntaxListPool();

		// Token: 0x04001545 RID: 5445
		private bool _isDelimited;

		// Token: 0x04001546 RID: 5446
		private readonly HashSet<string> _attributesSeen = new HashSet<string>();

		// Token: 0x02000792 RID: 1938
		private enum SkipResult
		{
			// Token: 0x040023F5 RID: 9205
			Continue,
			// Token: 0x040023F6 RID: 9206
			Abort
		}
	}
}
