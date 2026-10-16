using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x0200036A RID: 874
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal abstract class CSharpSyntaxNode : GreenNode
	{
		// Token: 0x06004C32 RID: 19506 RVA: 0x0013B9EE File Offset: 0x00139BEE
		internal CSharpSyntaxNode(SyntaxKind kind)
			: base((ushort)kind)
		{
			GreenStats.NoteGreen(this);
		}

		// Token: 0x06004C33 RID: 19507 RVA: 0x0013B9FD File Offset: 0x00139BFD
		internal CSharpSyntaxNode(SyntaxKind kind, int fullWidth)
			: base((ushort)kind, fullWidth)
		{
			GreenStats.NoteGreen(this);
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x0013BA0D File Offset: 0x00139C0D
		internal CSharpSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics)
			: base((ushort)kind, diagnostics)
		{
			GreenStats.NoteGreen(this);
		}

		// Token: 0x06004C35 RID: 19509 RVA: 0x0013BA1D File Offset: 0x00139C1D
		internal CSharpSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics, int fullWidth)
			: base((ushort)kind, diagnostics, fullWidth)
		{
			GreenStats.NoteGreen(this);
		}

		// Token: 0x06004C36 RID: 19510 RVA: 0x0013BA2E File Offset: 0x00139C2E
		internal CSharpSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations)
			: base((ushort)kind, diagnostics, annotations)
		{
			GreenStats.NoteGreen(this);
		}

		// Token: 0x06004C37 RID: 19511 RVA: 0x0013BA3F File Offset: 0x00139C3F
		internal CSharpSyntaxNode(SyntaxKind kind, DiagnosticInfo[] diagnostics, SyntaxAnnotation[] annotations, int fullWidth)
			: base((ushort)kind, diagnostics, annotations, fullWidth)
		{
			GreenStats.NoteGreen(this);
		}

		// Token: 0x06004C38 RID: 19512 RVA: 0x0013BA52 File Offset: 0x00139C52
		internal CSharpSyntaxNode(ObjectReader reader)
			: base(reader)
		{
		}

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x0013BA5B File Offset: 0x00139C5B
		public override string Language
		{
			get
			{
				return "C#";
			}
		}

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x0013BA62 File Offset: 0x00139C62
		public SyntaxKind Kind
		{
			get
			{
				return (SyntaxKind)base.RawKind;
			}
		}

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06004C3B RID: 19515 RVA: 0x0013BA6C File Offset: 0x00139C6C
		public override string KindText
		{
			get
			{
				return this.Kind.ToString();
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x0013BA8D File Offset: 0x00139C8D
		public override int RawContextualKind
		{
			get
			{
				return base.RawKind;
			}
		}

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06004C3D RID: 19517 RVA: 0x0013BA95 File Offset: 0x00139C95
		public override bool IsStructuredTrivia
		{
			get
			{
				return this is StructuredTriviaSyntax;
			}
		}

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x0013BAA0 File Offset: 0x00139CA0
		public override bool IsDirective
		{
			get
			{
				return this is DirectiveTriviaSyntax;
			}
		}

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x0013BAAB File Offset: 0x00139CAB
		public override bool IsSkippedTokensTrivia
		{
			get
			{
				return this.Kind == SyntaxKind.SkippedTokensTrivia;
			}
		}

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x0013BABA File Offset: 0x00139CBA
		public override bool IsDocumentationCommentTrivia
		{
			get
			{
				return SyntaxFacts.IsDocumentationCommentTrivia(this.Kind);
			}
		}

		// Token: 0x06004C41 RID: 19521 RVA: 0x0013BAC8 File Offset: 0x00139CC8
		public override int GetSlotOffset(int index)
		{
			int num = 0;
			for (int i = 0; i < index; i++)
			{
				GreenNode slot = this.GetSlot(i);
				if (slot != null)
				{
					num += slot.FullWidth;
				}
			}
			return num;
		}

		// Token: 0x06004C42 RID: 19522 RVA: 0x0013BAF8 File Offset: 0x00139CF8
		public SyntaxToken GetFirstToken()
		{
			return (SyntaxToken)base.GetFirstTerminal();
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x0013BB05 File Offset: 0x00139D05
		public SyntaxToken GetLastToken()
		{
			return (SyntaxToken)base.GetLastTerminal();
		}

		// Token: 0x06004C44 RID: 19524 RVA: 0x0013BB12 File Offset: 0x00139D12
		public SyntaxToken GetLastNonmissingToken()
		{
			return (SyntaxToken)base.GetLastNonmissingTerminal();
		}

		// Token: 0x06004C45 RID: 19525 RVA: 0x0013BB1F File Offset: 0x00139D1F
		public virtual GreenNode GetLeadingTrivia()
		{
			return null;
		}

		// Token: 0x06004C46 RID: 19526 RVA: 0x0013BB22 File Offset: 0x00139D22
		public override GreenNode GetLeadingTriviaCore()
		{
			return this.GetLeadingTrivia();
		}

		// Token: 0x06004C47 RID: 19527 RVA: 0x0013BB2A File Offset: 0x00139D2A
		public virtual GreenNode GetTrailingTrivia()
		{
			return null;
		}

		// Token: 0x06004C48 RID: 19528 RVA: 0x0013BB2D File Offset: 0x00139D2D
		public override GreenNode GetTrailingTriviaCore()
		{
			return this.GetTrailingTrivia();
		}

		// Token: 0x06004C49 RID: 19529
		public abstract TResult Accept<TResult>(CSharpSyntaxVisitor<TResult> visitor);

		// Token: 0x06004C4A RID: 19530
		public abstract void Accept(CSharpSyntaxVisitor visitor);

		// Token: 0x06004C4B RID: 19531 RVA: 0x0013BB35 File Offset: 0x00139D35
		internal virtual DirectiveStack ApplyDirectives(DirectiveStack stack)
		{
			return CSharpSyntaxNode.ApplyDirectives(this, stack);
		}

		// Token: 0x06004C4C RID: 19532 RVA: 0x0013BB40 File Offset: 0x00139D40
		internal static DirectiveStack ApplyDirectives(GreenNode node, DirectiveStack stack)
		{
			if (node.ContainsDirectives)
			{
				int i = 0;
				int slotCount = node.SlotCount;
				while (i < slotCount)
				{
					GreenNode slot = node.GetSlot(i);
					if (slot != null)
					{
						stack = CSharpSyntaxNode.ApplyDirectivesToListOrNode(slot, stack);
					}
					i++;
				}
			}
			return stack;
		}

		// Token: 0x06004C4D RID: 19533 RVA: 0x0013BB7D File Offset: 0x00139D7D
		internal static DirectiveStack ApplyDirectivesToListOrNode(GreenNode listOrNode, DirectiveStack stack)
		{
			if (listOrNode.RawKind == 1)
			{
				return CSharpSyntaxNode.ApplyDirectives(listOrNode, stack);
			}
			return ((CSharpSyntaxNode)listOrNode).ApplyDirectives(stack);
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x0013BB9C File Offset: 0x00139D9C
		internal virtual IList<DirectiveTriviaSyntax> GetDirectives()
		{
			if ((this.flags & GreenNode.NodeFlags.ContainsDirectives) != GreenNode.NodeFlags.None)
			{
				List<DirectiveTriviaSyntax> list = new List<DirectiveTriviaSyntax>(32);
				CSharpSyntaxNode.GetDirectives(this, list);
				return list;
			}
			return SpecializedCollections.EmptyList<DirectiveTriviaSyntax>();
		}

		// Token: 0x06004C4F RID: 19535 RVA: 0x0013BBCC File Offset: 0x00139DCC
		private static void GetDirectives(GreenNode node, List<DirectiveTriviaSyntax> directives)
		{
			if (node != null && node.ContainsDirectives)
			{
				DirectiveTriviaSyntax directiveTriviaSyntax = node as DirectiveTriviaSyntax;
				if (directiveTriviaSyntax != null)
				{
					directives.Add(directiveTriviaSyntax);
					return;
				}
				SyntaxToken syntaxToken = node as SyntaxToken;
				if (syntaxToken != null)
				{
					CSharpSyntaxNode.GetDirectives(syntaxToken.GetLeadingTrivia(), directives);
					CSharpSyntaxNode.GetDirectives(syntaxToken.GetTrailingTrivia(), directives);
					return;
				}
				int i = 0;
				int slotCount = node.SlotCount;
				while (i < slotCount)
				{
					CSharpSyntaxNode.GetDirectives(node.GetSlot(i), directives);
					i++;
				}
			}
		}

		// Token: 0x06004C50 RID: 19536 RVA: 0x0013BC39 File Offset: 0x00139E39
		protected void SetFactoryContext(SyntaxFactoryContext context)
		{
			if (context.IsInAsync)
			{
				this.flags |= GreenNode.NodeFlags.FactoryContextIsInAsync;
			}
			if (context.IsInQuery)
			{
				this.flags |= GreenNode.NodeFlags.FactoryContextIsInQuery;
			}
		}

		// Token: 0x06004C51 RID: 19537 RVA: 0x0013BC6C File Offset: 0x00139E6C
		internal static GreenNode.NodeFlags SetFactoryContext(GreenNode.NodeFlags flags, SyntaxFactoryContext context)
		{
			if (context.IsInAsync)
			{
				flags |= GreenNode.NodeFlags.FactoryContextIsInAsync;
			}
			if (context.IsInQuery)
			{
				flags |= GreenNode.NodeFlags.FactoryContextIsInQuery;
			}
			return flags;
		}

		// Token: 0x06004C52 RID: 19538 RVA: 0x0013BC8E File Offset: 0x00139E8E
		public override SyntaxToken CreateSeparator<TNode>(SyntaxNode element)
		{
			return SyntaxFactory.Token(SyntaxKind.CommaToken);
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x0013BC9A File Offset: 0x00139E9A
		public override bool IsTriviaWithEndOfLine()
		{
			return this.Kind == SyntaxKind.EndOfLineTrivia || this.Kind == SyntaxKind.SingleLineCommentTrivia;
		}

		// Token: 0x06004C54 RID: 19540 RVA: 0x0013BCB8 File Offset: 0x00139EB8
		public override SyntaxNode GetStructure(SyntaxTrivia trivia)
		{
			if (!trivia.HasStructure)
			{
				return null;
			}
			SyntaxNode parent = trivia.Token.Parent;
			if (parent != null)
			{
				Dictionary<SyntaxTrivia, SyntaxNode> orCreateValue = CSharpSyntaxNode.s_structuresTable.GetOrCreateValue(parent);
				Dictionary<SyntaxTrivia, SyntaxNode> dictionary = orCreateValue;
				SyntaxNode syntaxNode;
				lock (dictionary)
				{
					if (!orCreateValue.TryGetValue(trivia, ref syntaxNode))
					{
						syntaxNode = StructuredTriviaSyntax.Create(trivia);
						orCreateValue.Add(trivia, syntaxNode);
					}
				}
				return syntaxNode;
			}
			return StructuredTriviaSyntax.Create(trivia);
		}

		// Token: 0x040015BC RID: 5564
		private static readonly ConditionalWeakTable<SyntaxNode, Dictionary<SyntaxTrivia, SyntaxNode>> s_structuresTable = new ConditionalWeakTable<SyntaxNode, Dictionary<SyntaxTrivia, SyntaxNode>>();
	}
}
