using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000359 RID: 857
	internal readonly struct Blender
	{
		// Token: 0x0600482A RID: 18474 RVA: 0x0011B4F4 File Offset: 0x001196F4
		public Blender(Lexer lexer, CSharpSyntaxNode oldTree, IEnumerable<TextChangeRange> changes)
		{
			this._lexer = lexer;
			this._changes = ImmutableStack.Create<TextChangeRange>();
			if (changes != null)
			{
				TextChangeRange textChangeRange = TextChangeRange.Collapse(changes);
				TextChangeRange textChangeRange2 = Blender.ExtendToAffectedRange(oldTree, textChangeRange);
				this._changes = this._changes.Push(textChangeRange2);
			}
			if (oldTree == null)
			{
				this._oldTreeCursor = default(Blender.Cursor);
				this._newPosition = lexer.TextWindow.Position;
			}
			else
			{
				this._oldTreeCursor = Blender.Cursor.FromRoot(oldTree).MoveToFirstChild();
				this._newPosition = 0;
			}
			this._changeDelta = 0;
			this._newDirectives = default(DirectiveStack);
			this._oldDirectives = default(DirectiveStack);
			this._newLexerDrivenMode = LexerMode.XmlDocCommentLocationStart;
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x0011B59C File Offset: 0x0011979C
		private Blender(Lexer lexer, Blender.Cursor oldTreeCursor, ImmutableStack<TextChangeRange> changes, int newPosition, int changeDelta, DirectiveStack newDirectives, DirectiveStack oldDirectives, LexerMode newLexerDrivenMode)
		{
			this._lexer = lexer;
			this._oldTreeCursor = oldTreeCursor;
			this._changes = changes;
			this._newPosition = newPosition;
			this._changeDelta = changeDelta;
			this._newDirectives = newDirectives;
			this._oldDirectives = oldDirectives;
			this._newLexerDrivenMode = newLexerDrivenMode & (LexerMode)4128768;
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x0011B5EC File Offset: 0x001197EC
		private static TextChangeRange ExtendToAffectedRange(CSharpSyntaxNode oldTree, TextChangeRange changeRange)
		{
			int num = oldTree.FullWidth - 1;
			int num2 = Math.Max(Math.Min(changeRange.Span.Start, num), 0);
			int num3 = 0;
			while (num2 > 0 && num3 <= 1)
			{
				SyntaxToken syntaxToken = oldTree.FindToken(num2, false);
				num2 = Math.Max(0, syntaxToken.Position - 1);
				if (syntaxToken.FullWidth > 0)
				{
					num3++;
				}
			}
			if (Blender.IsInsideInterpolation(oldTree, num2))
			{
				int character = oldTree.SyntaxTree.GetLineSpan(new TextSpan(num2, 0), default(CancellationToken)).Span.Start.Character;
				num2 = Math.Max(num2 - character, 0);
			}
			TextSpan textSpan = TextSpan.FromBounds(num2, changeRange.Span.End);
			int num4 = changeRange.NewLength + (changeRange.Span.Start - num2);
			return new TextChangeRange(textSpan, num4);
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x0011B6D8 File Offset: 0x001198D8
		private static bool IsInsideInterpolation(CSharpSyntaxNode oldTree, int start)
		{
			for (SyntaxNode syntaxNode = oldTree.FindToken(start, false).Parent; syntaxNode != null; syntaxNode = syntaxNode.Parent)
			{
				if (syntaxNode.Kind() == SyntaxKind.InterpolatedStringExpression)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x0011B712 File Offset: 0x00119912
		public BlendedNode ReadNode(LexerMode mode)
		{
			return this.ReadNodeOrToken(mode, false);
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x0011B71C File Offset: 0x0011991C
		public BlendedNode ReadToken(LexerMode mode)
		{
			return this.ReadNodeOrToken(mode, true);
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x0011B728 File Offset: 0x00119928
		private BlendedNode ReadNodeOrToken(LexerMode mode, bool asToken)
		{
			Blender.Reader reader = new Blender.Reader(this);
			return reader.ReadNodeOrToken(mode, asToken);
		}

		// Token: 0x04001532 RID: 5426
		private readonly Lexer _lexer;

		// Token: 0x04001533 RID: 5427
		private readonly Blender.Cursor _oldTreeCursor;

		// Token: 0x04001534 RID: 5428
		private readonly ImmutableStack<TextChangeRange> _changes;

		// Token: 0x04001535 RID: 5429
		private readonly int _newPosition;

		// Token: 0x04001536 RID: 5430
		private readonly int _changeDelta;

		// Token: 0x04001537 RID: 5431
		private readonly DirectiveStack _newDirectives;

		// Token: 0x04001538 RID: 5432
		private readonly DirectiveStack _oldDirectives;

		// Token: 0x04001539 RID: 5433
		private readonly LexerMode _newLexerDrivenMode;

		// Token: 0x02000790 RID: 1936
		private struct Cursor
		{
			// Token: 0x060088A9 RID: 34985 RVA: 0x001EE2BF File Offset: 0x001EC4BF
			private Cursor(SyntaxNodeOrToken node, int indexInParent)
			{
				this.CurrentNodeOrToken = node;
				this._indexInParent = indexInParent;
			}

			// Token: 0x060088AA RID: 34986 RVA: 0x001EE2CF File Offset: 0x001EC4CF
			public static Blender.Cursor FromRoot(CSharpSyntaxNode node)
			{
				return new Blender.Cursor(node, 0);
			}

			// Token: 0x17002738 RID: 10040
			// (get) Token: 0x060088AB RID: 34987 RVA: 0x001EE2DD File Offset: 0x001EC4DD
			public bool IsFinished
			{
				get
				{
					return this.CurrentNodeOrToken.Kind() == SyntaxKind.None || this.CurrentNodeOrToken.Kind() == SyntaxKind.EndOfFileToken;
				}
			}

			// Token: 0x060088AC RID: 34988 RVA: 0x001EE300 File Offset: 0x001EC500
			private static bool IsNonZeroWidthOrIsEndOfFile(SyntaxNodeOrToken token)
			{
				return token.Kind() == SyntaxKind.EndOfFileToken || token.FullWidth != 0;
			}

			// Token: 0x060088AD RID: 34989 RVA: 0x001EE31C File Offset: 0x001EC51C
			public Blender.Cursor MoveToNextSibling()
			{
				if (this.CurrentNodeOrToken.Parent != null)
				{
					ChildSyntaxList childSyntaxList = this.CurrentNodeOrToken.Parent.ChildNodesAndTokens();
					int i = this._indexInParent + 1;
					int count = childSyntaxList.Count;
					while (i < count)
					{
						SyntaxNodeOrToken syntaxNodeOrToken = childSyntaxList[i];
						if (Blender.Cursor.IsNonZeroWidthOrIsEndOfFile(syntaxNodeOrToken))
						{
							return new Blender.Cursor(syntaxNodeOrToken, i);
						}
						i++;
					}
					return this.MoveToParent().MoveToNextSibling();
				}
				return default(Blender.Cursor);
			}

			// Token: 0x060088AE RID: 34990 RVA: 0x001EE398 File Offset: 0x001EC598
			private Blender.Cursor MoveToParent()
			{
				SyntaxNode parent = this.CurrentNodeOrToken.Parent;
				int num = Blender.Cursor.IndexOfNodeInParent(parent);
				return new Blender.Cursor(parent, num);
			}

			// Token: 0x060088AF RID: 34991 RVA: 0x001EE3C4 File Offset: 0x001EC5C4
			private static int IndexOfNodeInParent(SyntaxNode node)
			{
				if (node.Parent == null)
				{
					return 0;
				}
				ChildSyntaxList childSyntaxList = node.Parent.ChildNodesAndTokens();
				int i = SyntaxNodeOrToken.GetFirstChildIndexSpanningPosition(childSyntaxList, ((CSharpSyntaxNode)node).Position);
				int count = childSyntaxList.Count;
				while (i < count)
				{
					if (childSyntaxList[i] == node)
					{
						return i;
					}
					i++;
				}
				throw ExceptionUtilities.Unreachable;
			}

			// Token: 0x060088B0 RID: 34992 RVA: 0x001EE428 File Offset: 0x001EC628
			public Blender.Cursor MoveToFirstChild()
			{
				SyntaxNode syntaxNode = this.CurrentNodeOrToken.AsNode();
				if (syntaxNode.Kind() == SyntaxKind.InterpolatedStringExpression)
				{
					SyntaxToken syntaxToken = Lexer.RescanInterpolatedString((InterpolatedStringExpressionSyntax)syntaxNode.Green);
					return new Blender.Cursor(new SyntaxToken(syntaxNode.Parent, syntaxToken, syntaxNode.Position, this._indexInParent), this._indexInParent);
				}
				if (syntaxNode.SlotCount > 0)
				{
					SyntaxNodeOrToken syntaxNodeOrToken = ChildSyntaxList.ItemInternal(syntaxNode, 0);
					if (Blender.Cursor.IsNonZeroWidthOrIsEndOfFile(syntaxNodeOrToken))
					{
						return new Blender.Cursor(syntaxNodeOrToken, 0);
					}
				}
				int num = 0;
				foreach (SyntaxNodeOrToken syntaxNodeOrToken2 in this.CurrentNodeOrToken.ChildNodesAndTokens())
				{
					if (Blender.Cursor.IsNonZeroWidthOrIsEndOfFile(syntaxNodeOrToken2))
					{
						return new Blender.Cursor(syntaxNodeOrToken2, num);
					}
					num++;
				}
				return default(Blender.Cursor);
			}

			// Token: 0x060088B1 RID: 34993 RVA: 0x001EE4F8 File Offset: 0x001EC6F8
			public Blender.Cursor MoveToFirstToken()
			{
				Blender.Cursor cursor = this;
				if (!cursor.IsFinished)
				{
					SyntaxNodeOrToken syntaxNodeOrToken = cursor.CurrentNodeOrToken;
					while (syntaxNodeOrToken.Kind() != SyntaxKind.None && !SyntaxFacts.IsAnyToken(syntaxNodeOrToken.Kind()))
					{
						cursor = cursor.MoveToFirstChild();
						syntaxNodeOrToken = cursor.CurrentNodeOrToken;
					}
				}
				return cursor;
			}

			// Token: 0x040023EA RID: 9194
			public readonly SyntaxNodeOrToken CurrentNodeOrToken;

			// Token: 0x040023EB RID: 9195
			private readonly int _indexInParent;
		}

		// Token: 0x02000791 RID: 1937
		private struct Reader
		{
			// Token: 0x060088B2 RID: 34994 RVA: 0x001EE544 File Offset: 0x001EC744
			public Reader(Blender blender)
			{
				this._lexer = blender._lexer;
				this._oldTreeCursor = blender._oldTreeCursor;
				this._changes = blender._changes;
				this._newPosition = blender._newPosition;
				this._changeDelta = blender._changeDelta;
				this._newDirectives = blender._newDirectives;
				this._oldDirectives = blender._oldDirectives;
				this._newLexerDrivenMode = blender._newLexerDrivenMode;
			}

			// Token: 0x060088B3 RID: 34995 RVA: 0x001EE5B4 File Offset: 0x001EC7B4
			internal BlendedNode ReadNodeOrToken(LexerMode mode, bool asToken)
			{
				while (!this._oldTreeCursor.IsFinished)
				{
					if (this._changeDelta < 0)
					{
						this.SkipOldToken();
					}
					else
					{
						if (this._changeDelta > 0)
						{
							return this.ReadNewToken(mode);
						}
						BlendedNode blendedNode;
						if (this.TryTakeOldNodeOrToken(asToken, out blendedNode))
						{
							return blendedNode;
						}
						if (this._oldTreeCursor.CurrentNodeOrToken.IsNode)
						{
							this._oldTreeCursor = this._oldTreeCursor.MoveToFirstChild();
						}
						else
						{
							this.SkipOldToken();
						}
					}
				}
				return this.ReadNewToken(mode);
			}

			// Token: 0x060088B4 RID: 34996 RVA: 0x001EE634 File Offset: 0x001EC834
			private void SkipOldToken()
			{
				this._oldTreeCursor = this._oldTreeCursor.MoveToFirstToken();
				SyntaxNodeOrToken currentNodeOrToken = this._oldTreeCursor.CurrentNodeOrToken;
				this._changeDelta += currentNodeOrToken.FullWidth;
				this._oldDirectives = currentNodeOrToken.ApplyDirectives(this._oldDirectives);
				this._oldTreeCursor = this._oldTreeCursor.MoveToNextSibling();
				this.SkipPastChanges();
			}

			// Token: 0x060088B5 RID: 34997 RVA: 0x001EE69C File Offset: 0x001EC89C
			private void SkipPastChanges()
			{
				int position = this._oldTreeCursor.CurrentNodeOrToken.Position;
				while (!this._changes.IsEmpty && position >= this._changes.Peek().Span.End)
				{
					TextChangeRange textChangeRange = this._changes.Peek();
					this._changes = this._changes.Pop();
					this._changeDelta += textChangeRange.NewLength - textChangeRange.Span.Length;
				}
			}

			// Token: 0x060088B6 RID: 34998 RVA: 0x001EE728 File Offset: 0x001EC928
			private BlendedNode ReadNewToken(LexerMode mode)
			{
				SyntaxToken syntaxToken = this.LexNewToken(mode);
				int fullWidth = syntaxToken.FullWidth;
				this._newPosition += fullWidth;
				this._changeDelta -= fullWidth;
				this.SkipPastChanges();
				return this.CreateBlendedNode(null, syntaxToken);
			}

			// Token: 0x060088B7 RID: 34999 RVA: 0x001EE770 File Offset: 0x001EC970
			private SyntaxToken LexNewToken(LexerMode mode)
			{
				if (this._lexer.TextWindow.Position != this._newPosition)
				{
					this._lexer.Reset(this._newPosition, this._newDirectives);
				}
				if (mode >= LexerMode.XmlDocComment)
				{
					mode |= this._newLexerDrivenMode;
				}
				SyntaxToken syntaxToken = this._lexer.Lex(ref mode);
				this._newDirectives = this._lexer.Directives;
				this._newLexerDrivenMode = mode & (LexerMode)4128768;
				return syntaxToken;
			}

			// Token: 0x060088B8 RID: 35000 RVA: 0x001EE7E8 File Offset: 0x001EC9E8
			private bool TryTakeOldNodeOrToken(bool asToken, out BlendedNode blendedNode)
			{
				if (asToken)
				{
					this._oldTreeCursor = this._oldTreeCursor.MoveToFirstToken();
				}
				SyntaxNodeOrToken currentNodeOrToken = this._oldTreeCursor.CurrentNodeOrToken;
				if (!this.CanReuse(currentNodeOrToken))
				{
					blendedNode = default(BlendedNode);
					return false;
				}
				this._newPosition += currentNodeOrToken.FullWidth;
				this._oldTreeCursor = this._oldTreeCursor.MoveToNextSibling();
				this._newDirectives = currentNodeOrToken.ApplyDirectives(this._newDirectives);
				this._oldDirectives = currentNodeOrToken.ApplyDirectives(this._oldDirectives);
				blendedNode = this.CreateBlendedNode((CSharpSyntaxNode)currentNodeOrToken.AsNode(), (SyntaxToken)currentNodeOrToken.AsToken().Node);
				return true;
			}

			// Token: 0x060088B9 RID: 35001 RVA: 0x001EE8A0 File Offset: 0x001ECAA0
			private bool CanReuse(SyntaxNodeOrToken nodeOrToken)
			{
				return nodeOrToken.FullWidth != 0 && !nodeOrToken.ContainsAnnotations && !this.IntersectsNextChange(nodeOrToken) && !nodeOrToken.ContainsDiagnostics && (!nodeOrToken.IsToken || !((CSharpSyntaxNode)nodeOrToken.AsToken().Node).ContainsSkippedText || !nodeOrToken.Parent.ContainsDiagnostics) && !Blender.Reader.IsFabricatedToken(nodeOrToken.Kind()) && (!nodeOrToken.IsToken || !nodeOrToken.AsToken().IsMissing) && (!nodeOrToken.IsNode || !Blender.Reader.IsIncomplete((CSharpSyntaxNode)nodeOrToken.AsNode())) && (!nodeOrToken.ContainsDirectives || this._newDirectives.IncrementallyEquivalent(this._oldDirectives));
			}

			// Token: 0x060088BA RID: 35002 RVA: 0x001EE970 File Offset: 0x001ECB70
			private bool IntersectsNextChange(SyntaxNodeOrToken nodeOrToken)
			{
				if (this._changes.IsEmpty)
				{
					return false;
				}
				TextSpan fullSpan = nodeOrToken.FullSpan;
				TextSpan span = this._changes.Peek().Span;
				return fullSpan.IntersectsWith(span);
			}

			// Token: 0x060088BB RID: 35003 RVA: 0x001EE9B0 File Offset: 0x001ECBB0
			private static bool IsIncomplete(CSharpSyntaxNode node)
			{
				return node.Green.GetLastTerminal().IsMissing;
			}

			// Token: 0x060088BC RID: 35004 RVA: 0x001EE9C2 File Offset: 0x001ECBC2
			private static bool IsFabricatedToken(SyntaxKind kind)
			{
				return kind - SyntaxKind.GreaterThanGreaterThanToken <= 1 || SyntaxFacts.IsContextualKeyword(kind);
			}

			// Token: 0x060088BD RID: 35005 RVA: 0x001EE9D8 File Offset: 0x001ECBD8
			private BlendedNode CreateBlendedNode(CSharpSyntaxNode node, SyntaxToken token)
			{
				return new BlendedNode(node, token, new Blender(this._lexer, this._oldTreeCursor, this._changes, this._newPosition, this._changeDelta, this._newDirectives, this._oldDirectives, this._newLexerDrivenMode));
			}

			// Token: 0x040023EC RID: 9196
			private readonly Lexer _lexer;

			// Token: 0x040023ED RID: 9197
			private Blender.Cursor _oldTreeCursor;

			// Token: 0x040023EE RID: 9198
			private ImmutableStack<TextChangeRange> _changes;

			// Token: 0x040023EF RID: 9199
			private int _newPosition;

			// Token: 0x040023F0 RID: 9200
			private int _changeDelta;

			// Token: 0x040023F1 RID: 9201
			private DirectiveStack _newDirectives;

			// Token: 0x040023F2 RID: 9202
			private DirectiveStack _oldDirectives;

			// Token: 0x040023F3 RID: 9203
			private LexerMode _newLexerDrivenMode;
		}
	}
}
