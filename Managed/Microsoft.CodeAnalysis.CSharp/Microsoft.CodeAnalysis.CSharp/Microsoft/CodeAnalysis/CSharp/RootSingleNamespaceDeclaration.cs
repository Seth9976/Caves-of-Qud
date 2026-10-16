using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200012A RID: 298
	internal sealed class RootSingleNamespaceDeclaration : SingleNamespaceDeclaration
	{
		// Token: 0x060018A7 RID: 6311 RVA: 0x00084DB6 File Offset: 0x00082FB6
		public RootSingleNamespaceDeclaration(bool hasGlobalUsings, bool hasUsings, bool hasExternAliases, SyntaxReference treeNode, ImmutableArray<SingleNamespaceOrTypeDeclaration> children, ImmutableArray<ReferenceDirective> referenceDirectives, bool hasAssemblyAttributes, ImmutableArray<Diagnostic> diagnostics)
			: base(string.Empty, treeNode, new SourceLocation(treeNode), children, diagnostics)
		{
			this._referenceDirectives = referenceDirectives;
			this._hasAssemblyAttributes = hasAssemblyAttributes;
			this._hasGlobalUsings = hasGlobalUsings;
			this._hasUsings = hasUsings;
			this._hasExternAliases = hasExternAliases;
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x00084DF5 File Offset: 0x00082FF5
		public ImmutableArray<ReferenceDirective> ReferenceDirectives
		{
			get
			{
				return this._referenceDirectives;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00084DFD File Offset: 0x00082FFD
		public bool HasAssemblyAttributes
		{
			get
			{
				return this._hasAssemblyAttributes;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x00084E05 File Offset: 0x00083005
		public override bool HasGlobalUsings
		{
			get
			{
				return this._hasGlobalUsings;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00084E0D File Offset: 0x0008300D
		public override bool HasUsings
		{
			get
			{
				return this._hasUsings;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x00084E15 File Offset: 0x00083015
		public override bool HasExternAliases
		{
			get
			{
				return this._hasExternAliases;
			}
		}

		// Token: 0x040006CD RID: 1741
		private readonly ImmutableArray<ReferenceDirective> _referenceDirectives;

		// Token: 0x040006CE RID: 1742
		private readonly bool _hasAssemblyAttributes;

		// Token: 0x040006CF RID: 1743
		private readonly bool _hasGlobalUsings;

		// Token: 0x040006D0 RID: 1744
		private readonly bool _hasUsings;

		// Token: 0x040006D1 RID: 1745
		private readonly bool _hasExternAliases;
	}
}
