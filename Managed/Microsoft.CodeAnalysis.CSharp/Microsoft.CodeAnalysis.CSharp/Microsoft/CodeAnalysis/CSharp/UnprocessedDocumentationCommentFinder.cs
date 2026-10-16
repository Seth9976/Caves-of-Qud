using System;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200011C RID: 284
	internal class UnprocessedDocumentationCommentFinder : CSharpSyntaxWalker
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x00080F6F File Offset: 0x0007F16F
		private UnprocessedDocumentationCommentFinder(DiagnosticBag diagnostics, TextSpan? filterSpanWithinTree, CancellationToken cancellationToken)
			: base(SyntaxWalkerDepth.Trivia)
		{
			this._diagnostics = diagnostics;
			this._filterSpanWithinTree = filterSpanWithinTree;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00080F8D File Offset: 0x0007F18D
		public static void ReportUnprocessed(SyntaxTree tree, TextSpan? filterSpanWithinTree, DiagnosticBag diagnostics, CancellationToken cancellationToken)
		{
			if (tree.ReportDocumentationCommentDiagnostics())
			{
				new UnprocessedDocumentationCommentFinder(diagnostics, filterSpanWithinTree, cancellationToken).Visit(tree.GetRoot(cancellationToken));
			}
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00080FAC File Offset: 0x0007F1AC
		private bool IsSyntacticallyFilteredOut(TextSpan fullSpan)
		{
			return this._filterSpanWithinTree != null && !this._filterSpanWithinTree.Value.Contains(fullSpan);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00080FE0 File Offset: 0x0007F1E0
		public override void DefaultVisit(SyntaxNode node)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.IsSyntacticallyFilteredOut(node.FullSpan))
			{
				return;
			}
			if (!node.HasStructuredTrivia)
			{
				if (node.Span.Length > 0)
				{
					this._isValidLocation = false;
				}
				return;
			}
			if (node is BaseTypeDeclarationSyntax || node is DelegateDeclarationSyntax || node is EnumMemberDeclarationSyntax || node is BaseMethodDeclarationSyntax || node is BasePropertyDeclarationSyntax || node is BaseFieldDeclarationSyntax)
			{
				this._isValidLocation = true;
			}
			base.DefaultVisit(node);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0008106C File Offset: 0x0007F26C
		public override void VisitLeadingTrivia(SyntaxToken token)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.IsSyntacticallyFilteredOut(token.FullSpan))
			{
				return;
			}
			base.VisitLeadingTrivia(token);
			this._isValidLocation = false;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000810A8 File Offset: 0x0007F2A8
		public override void VisitTrivia(SyntaxTrivia trivia)
		{
			this._cancellationToken.ThrowIfCancellationRequested();
			if (this.IsSyntacticallyFilteredOut(trivia.FullSpan))
			{
				return;
			}
			if (!this._isValidLocation && SyntaxFacts.IsDocumentationCommentTrivia(trivia.Kind()))
			{
				int position = trivia.Position;
				this._diagnostics.Add(ErrorCode.WRN_UnprocessedXMLComment, new SourceLocation(trivia.SyntaxTree, new TextSpan(position, 1)));
			}
			base.VisitTrivia(trivia);
		}

		// Token: 0x04000683 RID: 1667
		private readonly DiagnosticBag _diagnostics;

		// Token: 0x04000684 RID: 1668
		private readonly CancellationToken _cancellationToken;

		// Token: 0x04000685 RID: 1669
		private readonly TextSpan? _filterSpanWithinTree;

		// Token: 0x04000686 RID: 1670
		private bool _isValidLocation;
	}
}
