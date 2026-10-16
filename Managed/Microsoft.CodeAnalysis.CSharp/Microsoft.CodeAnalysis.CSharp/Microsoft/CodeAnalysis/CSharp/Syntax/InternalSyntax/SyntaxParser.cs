using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000368 RID: 872
	internal abstract class SyntaxParser : IDisposable
	{
		// Token: 0x06004B03 RID: 19203 RVA: 0x00136CB4 File Offset: 0x00134EB4
		protected SyntaxParser(Lexer lexer, LexerMode mode, CSharpSyntaxNode oldTree, IEnumerable<TextChangeRange> changes, bool allowModeReset, bool preLexIfNotIncremental = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			this.lexer = lexer;
			this._mode = mode;
			this._allowModeReset = allowModeReset;
			this.cancellationToken = cancellationToken;
			this._currentNode = default(BlendedNode);
			this._isIncremental = oldTree != null;
			if (this.IsIncremental || allowModeReset)
			{
				this._firstBlender = new Blender(lexer, oldTree, changes);
				this._blendedTokens = SyntaxParser.s_blendedNodesPool.Allocate();
			}
			else
			{
				this._firstBlender = default(Blender);
				this._lexedTokens = new ArrayElement<SyntaxToken>[32];
			}
			if (preLexIfNotIncremental && !this.IsIncremental && !cancellationToken.CanBeCanceled)
			{
				this.PreLex();
			}
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00136D5C File Offset: 0x00134F5C
		public void Dispose()
		{
			BlendedNode[] blendedTokens = this._blendedTokens;
			if (blendedTokens != null)
			{
				this._blendedTokens = null;
				if (blendedTokens.Length < 4096)
				{
					Array.Clear(blendedTokens, 0, blendedTokens.Length);
					SyntaxParser.s_blendedNodesPool.Free(blendedTokens);
				}
			}
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x00136D9C File Offset: 0x00134F9C
		protected void ReInitialize()
		{
			this._firstToken = 0;
			this._tokenOffset = 0;
			this._tokenCount = 0;
			this._resetCount = 0;
			this._resetStart = 0;
			this._currentToken = null;
			this._prevTokenTrailingTrivia = null;
			if (this.IsIncremental || this._allowModeReset)
			{
				this._firstBlender = new Blender(this.lexer, null, null);
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x00136DFD File Offset: 0x00134FFD
		protected bool IsIncremental
		{
			get
			{
				return this._isIncremental;
			}
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00136E08 File Offset: 0x00135008
		private void PreLex()
		{
			int num = Math.Min(4096, Math.Max(32, this.lexer.TextWindow.Text.Length / 2));
			this._lexedTokens = new ArrayElement<SyntaxToken>[num];
			Lexer lexer = this.lexer;
			LexerMode mode = this._mode;
			for (int i = 0; i < num; i++)
			{
				SyntaxToken syntaxToken = lexer.Lex(mode);
				this.AddLexedToken(syntaxToken);
				if (syntaxToken.Kind == SyntaxKind.EndOfFileToken)
				{
					break;
				}
			}
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x00136E84 File Offset: 0x00135084
		protected SyntaxParser.ResetPoint GetResetPoint()
		{
			int currentTokenPosition = this.CurrentTokenPosition;
			if (this._resetCount == 0)
			{
				this._resetStart = currentTokenPosition;
			}
			this._resetCount++;
			return new SyntaxParser.ResetPoint(this._resetCount, this._mode, currentTokenPosition, this._prevTokenTrailingTrivia);
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00136ED0 File Offset: 0x001350D0
		protected void Reset(ref SyntaxParser.ResetPoint point)
		{
			int num = point.Position - this._firstToken;
			if (num >= this._tokenCount)
			{
				this.PeekToken(num - this._tokenOffset);
				num = point.Position - this._firstToken;
			}
			this._mode = point.Mode;
			this._tokenOffset = num;
			this._currentToken = null;
			this._currentNode = default(BlendedNode);
			this._prevTokenTrailingTrivia = point.PrevTokenTrailingTrivia;
			if (this._blendedTokens != null)
			{
				int i = this._tokenOffset;
				while (i < this._tokenCount)
				{
					if (this._blendedTokens[i].Token == null)
					{
						this._tokenCount = i;
						if (this._tokenCount == this._tokenOffset)
						{
							this.FetchCurrentToken();
							return;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00136F91 File Offset: 0x00135191
		protected void Release(ref SyntaxParser.ResetPoint point)
		{
			this._resetCount--;
			if (this._resetCount == 0)
			{
				this._resetStart = -1;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06004B0B RID: 19211 RVA: 0x00136FB0 File Offset: 0x001351B0
		public CSharpParseOptions Options
		{
			get
			{
				return this.lexer.Options;
			}
		}

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06004B0C RID: 19212 RVA: 0x00136FBD File Offset: 0x001351BD
		public bool IsScript
		{
			get
			{
				return this.Options.Kind == SourceCodeKind.Script;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06004B0D RID: 19213 RVA: 0x00136FCD File Offset: 0x001351CD
		// (set) Token: 0x06004B0E RID: 19214 RVA: 0x00136FD5 File Offset: 0x001351D5
		protected LexerMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				if (this._mode != value)
				{
					this._mode = value;
					this._currentToken = null;
					this._currentNode = default(BlendedNode);
					this._tokenCount = this._tokenOffset;
				}
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06004B0F RID: 19215 RVA: 0x00137008 File Offset: 0x00135208
		protected CSharpSyntaxNode CurrentNode
		{
			get
			{
				CSharpSyntaxNode node = this._currentNode.Node;
				if (node != null)
				{
					return node;
				}
				this.ReadCurrentNode();
				return this._currentNode.Node;
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06004B10 RID: 19216 RVA: 0x00137038 File Offset: 0x00135238
		protected SyntaxKind CurrentNodeKind
		{
			get
			{
				CSharpSyntaxNode currentNode = this.CurrentNode;
				if (currentNode == null)
				{
					return SyntaxKind.None;
				}
				return currentNode.Kind();
			}
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00137058 File Offset: 0x00135258
		private void ReadCurrentNode()
		{
			if (this._tokenOffset == 0)
			{
				this._currentNode = this._firstBlender.ReadNode(this._mode);
				return;
			}
			this._currentNode = this._blendedTokens[this._tokenOffset - 1].Blender.ReadNode(this._mode);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x001370B0 File Offset: 0x001352B0
		protected GreenNode EatNode()
		{
			GreenNode green = this.CurrentNode.Green;
			if (this._tokenOffset >= this._blendedTokens.Length)
			{
				this.AddTokenSlot();
			}
			BlendedNode[] blendedTokens = this._blendedTokens;
			int tokenOffset = this._tokenOffset;
			this._tokenOffset = tokenOffset + 1;
			blendedTokens[tokenOffset] = this._currentNode;
			this._tokenCount = this._tokenOffset;
			this._currentNode = default(BlendedNode);
			this._currentToken = null;
			return green;
		}

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06004B13 RID: 19219 RVA: 0x00137120 File Offset: 0x00135320
		protected SyntaxToken CurrentToken
		{
			get
			{
				SyntaxToken syntaxToken;
				if ((syntaxToken = this._currentToken) == null)
				{
					syntaxToken = (this._currentToken = this.FetchCurrentToken());
				}
				return syntaxToken;
			}
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x00137148 File Offset: 0x00135348
		private SyntaxToken FetchCurrentToken()
		{
			if (this._tokenOffset >= this._tokenCount)
			{
				this.AddNewToken();
			}
			if (this._blendedTokens != null)
			{
				return this._blendedTokens[this._tokenOffset].Token;
			}
			return this._lexedTokens[this._tokenOffset];
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x001371A0 File Offset: 0x001353A0
		private void AddNewToken()
		{
			if (this._blendedTokens == null)
			{
				this.AddLexedToken(this.lexer.Lex(this._mode));
				return;
			}
			BlendedNode blendedNode;
			if (this._tokenCount > 0)
			{
				blendedNode = this._blendedTokens[this._tokenCount - 1].Blender.ReadToken(this._mode);
				this.AddToken(in blendedNode);
				return;
			}
			if (this._currentNode.Token != null)
			{
				this.AddToken(in this._currentNode);
				return;
			}
			blendedNode = this._firstBlender.ReadToken(this._mode);
			this.AddToken(in blendedNode);
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x00137237 File Offset: 0x00135437
		private void AddToken(in BlendedNode tokenResult)
		{
			if (this._tokenCount >= this._blendedTokens.Length)
			{
				this.AddTokenSlot();
			}
			this._blendedTokens[this._tokenCount] = tokenResult;
			this._tokenCount++;
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x00137274 File Offset: 0x00135474
		private void AddLexedToken(SyntaxToken token)
		{
			if (this._tokenCount >= this._lexedTokens.Length)
			{
				this.AddLexedTokenSlot();
			}
			this._lexedTokens[this._tokenCount].Value = token;
			this._tokenCount++;
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x001372B4 File Offset: 0x001354B4
		private void AddTokenSlot()
		{
			if (this._tokenOffset > this._blendedTokens.Length >> 1 && (this._resetStart == -1 || this._resetStart > this._firstToken))
			{
				int num = ((this._resetStart == -1) ? this._tokenOffset : (this._resetStart - this._firstToken));
				int num2 = this._tokenCount - num;
				this._firstBlender = this._blendedTokens[num - 1].Blender;
				if (num2 > 0)
				{
					Array.Copy(this._blendedTokens, num, this._blendedTokens, 0, num2);
				}
				this._firstToken += num;
				this._tokenCount -= num;
				this._tokenOffset -= num;
				return;
			}
			BlendedNode[] blendedTokens = this._blendedTokens;
			Array.Resize<BlendedNode>(ref this._blendedTokens, this._blendedTokens.Length * 2);
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x00137390 File Offset: 0x00135590
		private void AddLexedTokenSlot()
		{
			if (this._tokenOffset > this._lexedTokens.Length >> 1 && (this._resetStart == -1 || this._resetStart > this._firstToken))
			{
				int num = ((this._resetStart == -1) ? this._tokenOffset : (this._resetStart - this._firstToken));
				int num2 = this._tokenCount - num;
				if (num2 > 0)
				{
					Array.Copy(this._lexedTokens, num, this._lexedTokens, 0, num2);
				}
				this._firstToken += num;
				this._tokenCount -= num;
				this._tokenOffset -= num;
				return;
			}
			ArrayElement<SyntaxToken>[] array = new ArrayElement<SyntaxToken>[this._lexedTokens.Length * 2];
			Array.Copy(this._lexedTokens, array, this._lexedTokens.Length);
			this._lexedTokens = array;
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x00137460 File Offset: 0x00135660
		protected SyntaxToken PeekToken(int n)
		{
			while (this._tokenOffset + n >= this._tokenCount)
			{
				this.AddNewToken();
			}
			if (this._blendedTokens != null)
			{
				return this._blendedTokens[this._tokenOffset + n].Token;
			}
			return this._lexedTokens[this._tokenOffset + n];
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x001374BE File Offset: 0x001356BE
		protected SyntaxToken EatToken()
		{
			SyntaxToken currentToken = this.CurrentToken;
			this.MoveToNextToken();
			return currentToken;
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x001374CC File Offset: 0x001356CC
		protected SyntaxToken TryEatToken(SyntaxKind kind)
		{
			if (this.CurrentToken.Kind != kind)
			{
				return null;
			}
			return this.EatToken();
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x001374E4 File Offset: 0x001356E4
		private void MoveToNextToken()
		{
			this._prevTokenTrailingTrivia = this._currentToken.GetTrailingTrivia();
			this._currentToken = null;
			if (this._blendedTokens != null)
			{
				this._currentNode = default(BlendedNode);
			}
			this._tokenOffset++;
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x00137520 File Offset: 0x00135720
		protected void ForceEndOfFile()
		{
			this._currentToken = SyntaxFactory.Token(SyntaxKind.EndOfFileToken);
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x00137534 File Offset: 0x00135734
		protected SyntaxToken EatToken(SyntaxKind kind)
		{
			SyntaxToken currentToken = this.CurrentToken;
			if (currentToken.Kind == kind)
			{
				this.MoveToNextToken();
				return currentToken;
			}
			return this.CreateMissingToken(kind, this.CurrentToken.Kind, true);
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x0013756C File Offset: 0x0013576C
		protected SyntaxToken EatTokenAsKind(SyntaxKind expected)
		{
			SyntaxToken currentToken = this.CurrentToken;
			if (currentToken.Kind == expected)
			{
				this.MoveToNextToken();
				return currentToken;
			}
			SyntaxToken syntaxToken = this.CreateMissingToken(expected, this.CurrentToken.Kind, true);
			return this.AddTrailingSkippedSyntax<SyntaxToken>(syntaxToken, this.EatToken());
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x001375B4 File Offset: 0x001357B4
		private SyntaxToken CreateMissingToken(SyntaxKind expected, SyntaxKind actual, bool reportError)
		{
			SyntaxToken syntaxToken = SyntaxFactory.MissingToken(expected);
			if (reportError)
			{
				syntaxToken = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken, new DiagnosticInfo[] { this.GetExpectedTokenError(expected, actual) });
			}
			return syntaxToken;
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x001375E8 File Offset: 0x001357E8
		private SyntaxToken CreateMissingToken(SyntaxKind expected, ErrorCode code, bool reportError)
		{
			SyntaxToken syntaxToken = SyntaxFactory.MissingToken(expected);
			if (reportError)
			{
				syntaxToken = this.AddError<SyntaxToken>(syntaxToken, code);
			}
			return syntaxToken;
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x00137609 File Offset: 0x00135809
		protected SyntaxToken EatToken(SyntaxKind kind, bool reportError)
		{
			if (reportError)
			{
				return this.EatToken(kind);
			}
			if (this.CurrentToken.Kind != kind)
			{
				return SyntaxFactory.MissingToken(kind);
			}
			return this.EatToken();
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x00137631 File Offset: 0x00135831
		protected SyntaxToken EatToken(SyntaxKind kind, ErrorCode code, bool reportError = true)
		{
			if (this.CurrentToken.Kind != kind)
			{
				return this.CreateMissingToken(kind, code, reportError);
			}
			return this.EatToken();
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x00137654 File Offset: 0x00135854
		protected SyntaxToken EatTokenWithPrejudice(SyntaxKind kind)
		{
			SyntaxToken syntaxToken = this.CurrentToken;
			if (syntaxToken.Kind != kind)
			{
				syntaxToken = this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken, new DiagnosticInfo[] { this.GetExpectedTokenError(kind, syntaxToken.Kind) });
			}
			this.MoveToNextToken();
			return syntaxToken;
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x00137698 File Offset: 0x00135898
		protected SyntaxToken EatTokenWithPrejudice(ErrorCode errorCode, params object[] args)
		{
			SyntaxToken syntaxToken = this.EatToken();
			return this.WithAdditionalDiagnostics<SyntaxToken>(syntaxToken, new DiagnosticInfo[] { SyntaxParser.MakeError(syntaxToken.GetLeadingTriviaWidth(), syntaxToken.Width, errorCode, args) });
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x001376D1 File Offset: 0x001358D1
		protected SyntaxToken EatContextualToken(SyntaxKind kind, ErrorCode code, bool reportError = true)
		{
			if (this.CurrentToken.ContextualKind != kind)
			{
				return this.CreateMissingToken(kind, code, reportError);
			}
			return SyntaxParser.ConvertToKeyword(this.EatToken());
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x001376F8 File Offset: 0x001358F8
		protected SyntaxToken EatContextualToken(SyntaxKind kind, bool reportError = true)
		{
			SyntaxKind contextualKind = this.CurrentToken.ContextualKind;
			if (contextualKind != kind)
			{
				return this.CreateMissingToken(kind, contextualKind, reportError);
			}
			return SyntaxParser.ConvertToKeyword(this.EatToken());
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x0013772C File Offset: 0x0013592C
		protected virtual SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual, int offset, int width)
		{
			ErrorCode expectedTokenErrorCode = SyntaxParser.GetExpectedTokenErrorCode(expected, actual);
			if (expectedTokenErrorCode == ErrorCode.ERR_SyntaxError || expectedTokenErrorCode == ErrorCode.ERR_IdentifierExpectedKW)
			{
				return new SyntaxDiagnosticInfo(offset, width, expectedTokenErrorCode, new object[]
				{
					SyntaxFacts.GetText(expected),
					SyntaxFacts.GetText(actual)
				});
			}
			return new SyntaxDiagnosticInfo(offset, width, expectedTokenErrorCode);
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x0013777C File Offset: 0x0013597C
		protected virtual SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual)
		{
			int num;
			int num2;
			this.GetDiagnosticSpanForMissingToken(out num, out num2);
			return this.GetExpectedTokenError(expected, actual, num, num2);
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x001377A0 File Offset: 0x001359A0
		private static ErrorCode GetExpectedTokenErrorCode(SyntaxKind expected, SyntaxKind actual)
		{
			if (expected <= SyntaxKind.OpenBraceToken)
			{
				if (expected == SyntaxKind.CloseParenToken)
				{
					return ErrorCode.ERR_CloseParenExpected;
				}
				if (expected == SyntaxKind.OpenBraceToken)
				{
					return ErrorCode.ERR_LbraceExpected;
				}
			}
			else
			{
				if (expected == SyntaxKind.CloseBraceToken)
				{
					return ErrorCode.ERR_RbraceExpected;
				}
				if (expected == SyntaxKind.SemicolonToken)
				{
					return ErrorCode.ERR_SemicolonExpected;
				}
				if (expected == SyntaxKind.IdentifierToken)
				{
					if (SyntaxFacts.IsReservedKeyword(actual))
					{
						return ErrorCode.ERR_IdentifierExpectedKW;
					}
					return ErrorCode.ERR_IdentifierExpected;
				}
			}
			return ErrorCode.ERR_SyntaxError;
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x00137810 File Offset: 0x00135A10
		protected void GetDiagnosticSpanForMissingToken(out int offset, out int width)
		{
			GreenNode prevTokenTrailingTrivia = this._prevTokenTrailingTrivia;
			if (prevTokenTrailingTrivia != null)
			{
				SyntaxList<CSharpSyntaxNode> syntaxList = new SyntaxList<CSharpSyntaxNode>(prevTokenTrailingTrivia);
				if (syntaxList.Any(8539))
				{
					offset = -prevTokenTrailingTrivia.FullWidth;
					width = 0;
					return;
				}
			}
			SyntaxToken currentToken = this.CurrentToken;
			offset = currentToken.GetLeadingTriviaWidth();
			width = currentToken.Width;
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x00137864 File Offset: 0x00135A64
		protected virtual TNode WithAdditionalDiagnostics<TNode>(TNode node, params DiagnosticInfo[] diagnostics) where TNode : GreenNode
		{
			DiagnosticInfo[] diagnostics2 = node.GetDiagnostics();
			int num = diagnostics2.Length;
			if (num == 0)
			{
				return node.WithDiagnosticsGreen(diagnostics);
			}
			DiagnosticInfo[] array = new DiagnosticInfo[diagnostics2.Length + diagnostics.Length];
			diagnostics2.CopyTo(array, 0);
			diagnostics.CopyTo(array, num);
			return node.WithDiagnosticsGreen(array);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x001378B0 File Offset: 0x00135AB0
		protected TNode AddError<TNode>(TNode node, ErrorCode code) where TNode : GreenNode
		{
			return this.AddError<TNode>(node, code, Array.Empty<object>());
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x001378C0 File Offset: 0x00135AC0
		protected TNode AddError<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : GreenNode
		{
			if (!node.IsMissing)
			{
				return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(node, code, args) });
			}
			SyntaxToken syntaxToken = node as SyntaxToken;
			int num;
			int num2;
			if (syntaxToken != null && syntaxToken.ContainsSkippedText)
			{
				num = syntaxToken.GetLeadingTriviaWidth();
				num2 = 0;
				bool flag = false;
				foreach (CSharpSyntaxNode csharpSyntaxNode in syntaxToken.TrailingTrivia)
				{
					if (csharpSyntaxNode.Kind == SyntaxKind.SkippedTokensTrivia)
					{
						flag = true;
						num2 += csharpSyntaxNode.Width;
					}
					else
					{
						if (flag)
						{
							break;
						}
						num += csharpSyntaxNode.Width;
					}
				}
			}
			else
			{
				this.GetDiagnosticSpanForMissingToken(out num, out num2);
			}
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(num, num2, code, args) });
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x00137990 File Offset: 0x00135B90
		protected TNode AddError<TNode>(TNode node, int offset, int length, ErrorCode code, params object[] args) where TNode : CSharpSyntaxNode
		{
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(offset, length, code, args) });
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x001379B8 File Offset: 0x00135BB8
		protected TNode AddError<TNode>(TNode node, CSharpSyntaxNode location, ErrorCode code, params object[] args) where TNode : CSharpSyntaxNode
		{
			int num;
			this.FindOffset(node, location, out num);
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(num, location.Width, code, args) });
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x001379F4 File Offset: 0x00135BF4
		protected TNode AddErrorToFirstToken<TNode>(TNode node, ErrorCode code) where TNode : CSharpSyntaxNode
		{
			SyntaxToken firstToken = node.GetFirstToken();
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(firstToken.GetLeadingTriviaWidth(), firstToken.Width, code) });
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x00137A30 File Offset: 0x00135C30
		protected TNode AddErrorToFirstToken<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : CSharpSyntaxNode
		{
			SyntaxToken firstToken = node.GetFirstToken();
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(firstToken.GetLeadingTriviaWidth(), firstToken.Width, code, args) });
		}

		// Token: 0x06004B34 RID: 19252 RVA: 0x00137A6C File Offset: 0x00135C6C
		protected TNode AddErrorToLastToken<TNode>(TNode node, ErrorCode code) where TNode : CSharpSyntaxNode
		{
			int num;
			int num2;
			SyntaxParser.GetOffsetAndWidthForLastToken<TNode>(node, out num, out num2);
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(num, num2, code) });
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x00137A9C File Offset: 0x00135C9C
		protected TNode AddErrorToLastToken<TNode>(TNode node, ErrorCode code, params object[] args) where TNode : CSharpSyntaxNode
		{
			int num;
			int num2;
			SyntaxParser.GetOffsetAndWidthForLastToken<TNode>(node, out num, out num2);
			return this.WithAdditionalDiagnostics<TNode>(node, new DiagnosticInfo[] { SyntaxParser.MakeError(num, num2, code, args) });
		}

		// Token: 0x06004B36 RID: 19254 RVA: 0x00137ACC File Offset: 0x00135CCC
		private static void GetOffsetAndWidthForLastToken<TNode>(TNode node, out int offset, out int width) where TNode : CSharpSyntaxNode
		{
			SyntaxToken lastNonmissingToken = node.GetLastNonmissingToken();
			offset = node.FullWidth;
			width = 0;
			if (lastNonmissingToken != null)
			{
				offset -= lastNonmissingToken.FullWidth;
				offset += lastNonmissingToken.GetLeadingTriviaWidth();
				width += lastNonmissingToken.Width;
			}
		}

		// Token: 0x06004B37 RID: 19255 RVA: 0x00137B19 File Offset: 0x00135D19
		protected static SyntaxDiagnosticInfo MakeError(int offset, int width, ErrorCode code)
		{
			return new SyntaxDiagnosticInfo(offset, width, code);
		}

		// Token: 0x06004B38 RID: 19256 RVA: 0x00137B23 File Offset: 0x00135D23
		protected static SyntaxDiagnosticInfo MakeError(int offset, int width, ErrorCode code, params object[] args)
		{
			return new SyntaxDiagnosticInfo(offset, width, code, args);
		}

		// Token: 0x06004B39 RID: 19257 RVA: 0x00137B2E File Offset: 0x00135D2E
		protected static SyntaxDiagnosticInfo MakeError(GreenNode node, ErrorCode code, params object[] args)
		{
			return new SyntaxDiagnosticInfo(node.GetLeadingTriviaWidth(), node.Width, code, args);
		}

		// Token: 0x06004B3A RID: 19258 RVA: 0x00137B43 File Offset: 0x00135D43
		protected static SyntaxDiagnosticInfo MakeError(ErrorCode code, params object[] args)
		{
			return new SyntaxDiagnosticInfo(code, args);
		}

		// Token: 0x06004B3B RID: 19259 RVA: 0x00137B4C File Offset: 0x00135D4C
		protected TNode AddLeadingSkippedSyntax<TNode>(TNode node, GreenNode skippedSyntax) where TNode : CSharpSyntaxNode
		{
			SyntaxToken syntaxToken = (node as SyntaxToken) ?? node.GetFirstToken();
			SyntaxToken syntaxToken2 = this.AddSkippedSyntax(syntaxToken, skippedSyntax, false);
			return SyntaxFirstTokenReplacer.Replace<TNode>(node, syntaxToken, syntaxToken2, skippedSyntax.FullWidth);
		}

		// Token: 0x06004B3C RID: 19260 RVA: 0x00137B8C File Offset: 0x00135D8C
		protected void AddTrailingSkippedSyntax(SyntaxListBuilder list, GreenNode skippedSyntax)
		{
			list[list.Count - 1] = this.AddTrailingSkippedSyntax<CSharpSyntaxNode>((CSharpSyntaxNode)list[list.Count - 1], skippedSyntax);
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00137BB6 File Offset: 0x00135DB6
		protected void AddTrailingSkippedSyntax<TNode>(SyntaxListBuilder<TNode> list, GreenNode skippedSyntax) where TNode : CSharpSyntaxNode
		{
			list[list.Count - 1] = this.AddTrailingSkippedSyntax<TNode>(list[list.Count - 1], skippedSyntax);
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x00137BE0 File Offset: 0x00135DE0
		protected TNode AddTrailingSkippedSyntax<TNode>(TNode node, GreenNode skippedSyntax) where TNode : CSharpSyntaxNode
		{
			SyntaxToken syntaxToken = node as SyntaxToken;
			if (syntaxToken != null)
			{
				return (TNode)((object)this.AddSkippedSyntax(syntaxToken, skippedSyntax, true));
			}
			SyntaxToken lastToken = node.GetLastToken();
			SyntaxToken syntaxToken2 = this.AddSkippedSyntax(lastToken, skippedSyntax, true);
			return SyntaxLastTokenReplacer.Replace<TNode>(node, syntaxToken2);
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x00137C28 File Offset: 0x00135E28
		internal SyntaxToken AddSkippedSyntax(SyntaxToken target, GreenNode skippedSyntax, bool trailing)
		{
			SyntaxListBuilder syntaxListBuilder = new SyntaxListBuilder(4);
			SyntaxDiagnosticInfo syntaxDiagnosticInfo = null;
			int num = 0;
			int num2 = 0;
			foreach (GreenNode greenNode in skippedSyntax.EnumerateNodes())
			{
				SyntaxToken syntaxToken = greenNode as SyntaxToken;
				if (syntaxToken != null)
				{
					syntaxListBuilder.Add(syntaxToken.GetLeadingTrivia());
					if (syntaxToken.Width > 0)
					{
						SyntaxToken syntaxToken2 = syntaxToken.TokenWithLeadingTrivia(null).TokenWithTrailingTrivia(null);
						int leadingTriviaWidth = syntaxToken.GetLeadingTriviaWidth();
						if (leadingTriviaWidth > 0)
						{
							DiagnosticInfo[] diagnostics = syntaxToken2.GetDiagnostics();
							for (int i = 0; i < diagnostics.Length; i++)
							{
								SyntaxDiagnosticInfo syntaxDiagnosticInfo2 = (SyntaxDiagnosticInfo)diagnostics[i];
								diagnostics[i] = new SyntaxDiagnosticInfo(syntaxDiagnosticInfo2.Offset - leadingTriviaWidth, syntaxDiagnosticInfo2.Width, (ErrorCode)syntaxDiagnosticInfo2.Code, syntaxDiagnosticInfo2.Arguments);
							}
						}
						syntaxListBuilder.Add(SyntaxFactory.SkippedTokensTrivia(syntaxToken2));
					}
					else
					{
						SyntaxDiagnosticInfo syntaxDiagnosticInfo3 = (SyntaxDiagnosticInfo)Enumerable.FirstOrDefault<DiagnosticInfo>(syntaxToken.GetDiagnostics());
						if (syntaxDiagnosticInfo3 != null)
						{
							syntaxDiagnosticInfo = syntaxDiagnosticInfo3;
							num = num2;
						}
					}
					syntaxListBuilder.Add(syntaxToken.GetTrailingTrivia());
					num2 += syntaxToken.FullWidth;
				}
				else if (greenNode.ContainsDiagnostics && syntaxDiagnosticInfo == null)
				{
					SyntaxDiagnosticInfo syntaxDiagnosticInfo4 = (SyntaxDiagnosticInfo)Enumerable.FirstOrDefault<DiagnosticInfo>(greenNode.GetDiagnostics());
					if (syntaxDiagnosticInfo4 != null)
					{
						syntaxDiagnosticInfo = syntaxDiagnosticInfo4;
						num = num2;
					}
				}
			}
			int num3 = num2;
			GreenNode greenNode2 = syntaxListBuilder.ToListNode();
			int num4;
			if (trailing)
			{
				GreenNode trailingTrivia = target.GetTrailingTrivia();
				num4 = target.FullWidth;
				target = target.TokenWithTrailingTrivia(SyntaxList.Concat(trailingTrivia, greenNode2));
			}
			else
			{
				if (num3 > 0)
				{
					DiagnosticInfo[] diagnostics2 = target.GetDiagnostics();
					for (int j = 0; j < diagnostics2.Length; j++)
					{
						SyntaxDiagnosticInfo syntaxDiagnosticInfo5 = (SyntaxDiagnosticInfo)diagnostics2[j];
						diagnostics2[j] = new SyntaxDiagnosticInfo(syntaxDiagnosticInfo5.Offset + num3, syntaxDiagnosticInfo5.Width, (ErrorCode)syntaxDiagnosticInfo5.Code, syntaxDiagnosticInfo5.Arguments);
					}
				}
				GreenNode leadingTrivia = target.GetLeadingTrivia();
				target = target.TokenWithLeadingTrivia(SyntaxList.Concat(greenNode2, leadingTrivia));
				num4 = 0;
			}
			if (syntaxDiagnosticInfo != null)
			{
				int num5 = num4 + num + syntaxDiagnosticInfo.Offset;
				target = this.WithAdditionalDiagnostics<SyntaxToken>(target, new DiagnosticInfo[]
				{
					new SyntaxDiagnosticInfo(num5, syntaxDiagnosticInfo.Width, (ErrorCode)syntaxDiagnosticInfo.Code, syntaxDiagnosticInfo.Arguments)
				});
			}
			return target;
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x00137E7C File Offset: 0x0013607C
		private bool FindOffset(GreenNode root, CSharpSyntaxNode location, out int offset)
		{
			int num = 0;
			offset = 0;
			if (root != null)
			{
				int i = 0;
				int slotCount = root.SlotCount;
				while (i < slotCount)
				{
					GreenNode slot = root.GetSlot(i);
					if (slot != null)
					{
						if (slot == location)
						{
							offset = num;
							return true;
						}
						if (this.FindOffset(slot, location, out offset))
						{
							offset += slot.GetLeadingTriviaWidth() + num;
							return true;
						}
						num += slot.FullWidth;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x00137EDC File Offset: 0x001360DC
		protected static SyntaxToken ConvertToKeyword(SyntaxToken token)
		{
			if (token.Kind != token.ContextualKind)
			{
				SyntaxToken syntaxToken = (token.IsMissing ? SyntaxFactory.MissingToken(token.LeadingTrivia.Node, token.ContextualKind, token.TrailingTrivia.Node) : SyntaxFactory.Token(token.LeadingTrivia.Node, token.ContextualKind, token.TrailingTrivia.Node));
				DiagnosticInfo[] diagnostics = token.GetDiagnostics();
				if (diagnostics != null && diagnostics.Length != 0)
				{
					syntaxToken = syntaxToken.WithDiagnosticsGreen(diagnostics);
				}
				return syntaxToken;
			}
			return token;
		}

		// Token: 0x06004B42 RID: 19266 RVA: 0x00137F6C File Offset: 0x0013616C
		protected static SyntaxToken ConvertToIdentifier(SyntaxToken token)
		{
			return SyntaxToken.Identifier(token.Kind, token.LeadingTrivia.Node, token.Text, token.ValueText, token.TrailingTrivia.Node);
		}

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06004B43 RID: 19267 RVA: 0x00137FAC File Offset: 0x001361AC
		internal DirectiveStack Directives
		{
			get
			{
				return this.lexer.Directives;
			}
		}

		// Token: 0x06004B44 RID: 19268 RVA: 0x00137FBC File Offset: 0x001361BC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		protected TNode CheckFeatureAvailability<[global::System.Runtime.CompilerServices.Nullable(0)] TNode>(TNode node, MessageID feature, bool forceWarning = false) where TNode : GreenNode
		{
			LanguageVersion languageVersion = this.Options.LanguageVersion;
			LanguageVersion languageVersion2 = feature.RequiredVersion();
			if (feature != MessageID.IDS_FeatureModuleAttrLoc)
			{
				if (feature != MessageID.IDS_FeatureAltInterpolatedVerbatimStrings)
				{
					CSDiagnosticInfo featureAvailabilityDiagnosticInfo = feature.GetFeatureAvailabilityDiagnosticInfo(this.Options);
					if (featureAvailabilityDiagnosticInfo == null)
					{
						return node;
					}
					if (forceWarning)
					{
						return this.AddError<TNode>(node, ErrorCode.WRN_ErrorOverride, new object[]
						{
							featureAvailabilityDiagnosticInfo,
							(int)featureAvailabilityDiagnosticInfo.Code
						});
					}
					return this.AddError<TNode>(node, featureAvailabilityDiagnosticInfo.Code, featureAvailabilityDiagnosticInfo.Arguments);
				}
				else
				{
					if (languageVersion < languageVersion2)
					{
						return this.AddError<TNode>(node, ErrorCode.ERR_AltInterpolatedVerbatimStringsNotAvailable, new object[]
						{
							new CSharpRequiredLanguageVersion(languageVersion2)
						});
					}
					return node;
				}
			}
			else
			{
				if (languageVersion < LanguageVersion.CSharp2)
				{
					return this.AddError<TNode>(node, ErrorCode.WRN_NonECMAFeature, new object[] { feature.Localize() });
				}
				return node;
			}
		}

		// Token: 0x06004B45 RID: 19269 RVA: 0x00138084 File Offset: 0x00136284
		protected bool IsFeatureEnabled(MessageID feature)
		{
			return this.Options.IsFeatureEnabled(feature);
		}

		// Token: 0x06004B46 RID: 19270 RVA: 0x00138094 File Offset: 0x00136294
		protected bool IsMakingProgress(ref int lastTokenPosition, bool assertIfFalse = true)
		{
			int currentTokenPosition = this.CurrentTokenPosition;
			if (currentTokenPosition > lastTokenPosition)
			{
				lastTokenPosition = currentTokenPosition;
				return true;
			}
			return false;
		}

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06004B47 RID: 19271 RVA: 0x001380B3 File Offset: 0x001362B3
		private int CurrentTokenPosition
		{
			get
			{
				return this._firstToken + this._tokenOffset;
			}
		}

		// Token: 0x040015AA RID: 5546
		protected readonly Lexer lexer;

		// Token: 0x040015AB RID: 5547
		private readonly bool _isIncremental;

		// Token: 0x040015AC RID: 5548
		private readonly bool _allowModeReset;

		// Token: 0x040015AD RID: 5549
		protected readonly CancellationToken cancellationToken;

		// Token: 0x040015AE RID: 5550
		private LexerMode _mode;

		// Token: 0x040015AF RID: 5551
		private Blender _firstBlender;

		// Token: 0x040015B0 RID: 5552
		private BlendedNode _currentNode;

		// Token: 0x040015B1 RID: 5553
		private SyntaxToken _currentToken;

		// Token: 0x040015B2 RID: 5554
		private ArrayElement<SyntaxToken>[] _lexedTokens;

		// Token: 0x040015B3 RID: 5555
		private GreenNode _prevTokenTrailingTrivia;

		// Token: 0x040015B4 RID: 5556
		private int _firstToken;

		// Token: 0x040015B5 RID: 5557
		private int _tokenOffset;

		// Token: 0x040015B6 RID: 5558
		private int _tokenCount;

		// Token: 0x040015B7 RID: 5559
		private int _resetCount;

		// Token: 0x040015B8 RID: 5560
		private int _resetStart;

		// Token: 0x040015B9 RID: 5561
		private static readonly ObjectPool<BlendedNode[]> s_blendedNodesPool = new ObjectPool<BlendedNode[]>(() => new BlendedNode[32], 2);

		// Token: 0x040015BA RID: 5562
		private BlendedNode[] _blendedTokens;

		// Token: 0x020007B0 RID: 1968
		protected struct ResetPoint
		{
			// Token: 0x06008913 RID: 35091 RVA: 0x001EFCFB File Offset: 0x001EDEFB
			internal ResetPoint(int resetCount, LexerMode mode, int position, GreenNode prevTokenTrailingTrivia)
			{
				this.ResetCount = resetCount;
				this.Mode = mode;
				this.Position = position;
				this.PrevTokenTrailingTrivia = prevTokenTrailingTrivia;
			}

			// Token: 0x040024D3 RID: 9427
			internal readonly int ResetCount;

			// Token: 0x040024D4 RID: 9428
			internal readonly LexerMode Mode;

			// Token: 0x040024D5 RID: 9429
			internal readonly int Position;

			// Token: 0x040024D6 RID: 9430
			internal readonly GreenNode PrevTokenTrailingTrivia;
		}
	}
}
