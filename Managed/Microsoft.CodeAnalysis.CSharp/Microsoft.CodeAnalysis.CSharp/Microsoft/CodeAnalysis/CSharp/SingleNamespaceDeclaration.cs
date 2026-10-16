using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200012B RID: 299
	internal class SingleNamespaceDeclaration : SingleNamespaceOrTypeDeclaration
	{
		// Token: 0x060018AD RID: 6317 RVA: 0x00084E1D File Offset: 0x0008301D
		protected SingleNamespaceDeclaration(string name, SyntaxReference syntaxReference, SourceLocation nameLocation, ImmutableArray<SingleNamespaceOrTypeDeclaration> children, ImmutableArray<Diagnostic> diagnostics)
			: base(name, syntaxReference, nameLocation, diagnostics)
		{
			this._children = children;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x00084E32 File Offset: 0x00083032
		public override DeclarationKind Kind
		{
			get
			{
				return DeclarationKind.Namespace;
			}
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00084E35 File Offset: 0x00083035
		protected override ImmutableArray<SingleNamespaceOrTypeDeclaration> GetNamespaceOrTypeDeclarationChildren()
		{
			return this._children;
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x00084E3D File Offset: 0x0008303D
		public virtual bool HasGlobalUsings
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x00084E40 File Offset: 0x00083040
		public virtual bool HasUsings
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x00084E43 File Offset: 0x00083043
		public virtual bool HasExternAliases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00084E46 File Offset: 0x00083046
		public static SingleNamespaceDeclaration Create(string name, bool hasUsings, bool hasExternAliases, SyntaxReference syntaxReference, SourceLocation nameLocation, ImmutableArray<SingleNamespaceOrTypeDeclaration> children, ImmutableArray<Diagnostic> diagnostics)
		{
			if (!hasUsings && !hasExternAliases)
			{
				return new SingleNamespaceDeclaration(name, syntaxReference, nameLocation, children, diagnostics);
			}
			return new SingleNamespaceDeclarationEx(name, hasUsings, hasExternAliases, syntaxReference, nameLocation, children, diagnostics);
		}

		// Token: 0x040006D2 RID: 1746
		private readonly ImmutableArray<SingleNamespaceOrTypeDeclaration> _children;
	}
}
