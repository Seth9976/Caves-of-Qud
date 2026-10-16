using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200012D RID: 301
	internal abstract class SingleNamespaceOrTypeDeclaration : Declaration
	{
		// Token: 0x060018B7 RID: 6327 RVA: 0x00084E9A File Offset: 0x0008309A
		protected SingleNamespaceOrTypeDeclaration(string name, SyntaxReference syntaxReference, SourceLocation nameLocation, ImmutableArray<Diagnostic> diagnostics)
			: base(name)
		{
			this._syntaxReference = syntaxReference;
			this._nameLocation = nameLocation;
			this.Diagnostics = diagnostics;
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x00084EB9 File Offset: 0x000830B9
		public SourceLocation Location
		{
			get
			{
				return new SourceLocation(this.SyntaxReference);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x00084EC6 File Offset: 0x000830C6
		public SyntaxReference SyntaxReference
		{
			get
			{
				return this._syntaxReference;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x00084ECE File Offset: 0x000830CE
		public SourceLocation NameLocation
		{
			get
			{
				return this._nameLocation;
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00084ED6 File Offset: 0x000830D6
		protected override ImmutableArray<Declaration> GetDeclarationChildren()
		{
			return StaticCast<Declaration>.From<SingleNamespaceOrTypeDeclaration>(this.GetNamespaceOrTypeDeclarationChildren());
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00084EE3 File Offset: 0x000830E3
		public new ImmutableArray<SingleNamespaceOrTypeDeclaration> Children
		{
			get
			{
				return this.GetNamespaceOrTypeDeclarationChildren();
			}
		}

		// Token: 0x060018BD RID: 6333
		protected abstract ImmutableArray<SingleNamespaceOrTypeDeclaration> GetNamespaceOrTypeDeclarationChildren();

		// Token: 0x040006D5 RID: 1749
		private readonly SyntaxReference _syntaxReference;

		// Token: 0x040006D6 RID: 1750
		private readonly SourceLocation _nameLocation;

		// Token: 0x040006D7 RID: 1751
		public readonly ImmutableArray<Diagnostic> Diagnostics;
	}
}
