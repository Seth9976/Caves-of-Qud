using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200012C RID: 300
	internal sealed class SingleNamespaceDeclarationEx : SingleNamespaceDeclaration
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x00084E6B File Offset: 0x0008306B
		public SingleNamespaceDeclarationEx(string name, bool hasUsings, bool hasExternAliases, SyntaxReference syntaxReference, SourceLocation nameLocation, ImmutableArray<SingleNamespaceOrTypeDeclaration> children, ImmutableArray<Diagnostic> diagnostics)
			: base(name, syntaxReference, nameLocation, children, diagnostics)
		{
			this._hasUsings = hasUsings;
			this._hasExternAliases = hasExternAliases;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x00084E8A File Offset: 0x0008308A
		public override bool HasUsings
		{
			get
			{
				return this._hasUsings;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x00084E92 File Offset: 0x00083092
		public override bool HasExternAliases
		{
			get
			{
				return this._hasExternAliases;
			}
		}

		// Token: 0x040006D3 RID: 1747
		private readonly bool _hasUsings;

		// Token: 0x040006D4 RID: 1748
		private readonly bool _hasExternAliases;
	}
}
