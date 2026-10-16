using System;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000373 RID: 883
	internal class SyntaxFirstTokenReplacer : CSharpSyntaxRewriter
	{
		// Token: 0x0600504C RID: 20556 RVA: 0x001456F9 File Offset: 0x001438F9
		private SyntaxFirstTokenReplacer(SyntaxToken oldToken, SyntaxToken newToken, int diagnosticOffsetDelta)
			: base(false)
		{
			this._oldToken = oldToken;
			this._newToken = newToken;
			this._diagnosticOffsetDelta = diagnosticOffsetDelta;
			this._foundOldToken = false;
		}

		// Token: 0x0600504D RID: 20557 RVA: 0x0014571E File Offset: 0x0014391E
		internal static TRoot Replace<TRoot>(TRoot root, SyntaxToken oldToken, SyntaxToken newToken, int diagnosticOffsetDelta) where TRoot : CSharpSyntaxNode
		{
			return (TRoot)((object)new SyntaxFirstTokenReplacer(oldToken, newToken, diagnosticOffsetDelta).Visit(root));
		}

		// Token: 0x0600504E RID: 20558 RVA: 0x00145738 File Offset: 0x00143938
		public override CSharpSyntaxNode Visit(CSharpSyntaxNode node)
		{
			if (node == null || this._foundOldToken)
			{
				return node;
			}
			if (node is SyntaxToken)
			{
				this._foundOldToken = true;
				return this._newToken;
			}
			return SyntaxFirstTokenReplacer.UpdateDiagnosticOffset<CSharpSyntaxNode>(base.Visit(node), this._diagnosticOffsetDelta);
		}

		// Token: 0x0600504F RID: 20559 RVA: 0x00145770 File Offset: 0x00143970
		private static TSyntax UpdateDiagnosticOffset<TSyntax>(TSyntax node, int diagnosticOffsetDelta) where TSyntax : CSharpSyntaxNode
		{
			DiagnosticInfo[] diagnostics = node.GetDiagnostics();
			if (diagnostics == null || diagnostics.Length == 0)
			{
				return node;
			}
			int num = diagnostics.Length;
			DiagnosticInfo[] array = new DiagnosticInfo[num];
			for (int i = 0; i < num; i++)
			{
				DiagnosticInfo diagnosticInfo = diagnostics[i];
				SyntaxDiagnosticInfo syntaxDiagnosticInfo = diagnosticInfo as SyntaxDiagnosticInfo;
				array[i] = ((syntaxDiagnosticInfo == null) ? diagnosticInfo : new SyntaxDiagnosticInfo(syntaxDiagnosticInfo.Offset + diagnosticOffsetDelta, syntaxDiagnosticInfo.Width, (ErrorCode)syntaxDiagnosticInfo.Code, syntaxDiagnosticInfo.Arguments));
			}
			return node.WithDiagnosticsGreen(array);
		}

		// Token: 0x040015CC RID: 5580
		private readonly SyntaxToken _oldToken;

		// Token: 0x040015CD RID: 5581
		private readonly SyntaxToken _newToken;

		// Token: 0x040015CE RID: 5582
		private readonly int _diagnosticOffsetDelta;

		// Token: 0x040015CF RID: 5583
		private bool _foundOldToken;
	}
}
