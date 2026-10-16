using System;
using System.Collections.Immutable;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000121 RID: 289
	internal abstract class Declaration
	{
		// Token: 0x06001859 RID: 6233 RVA: 0x0008307F File Offset: 0x0008127F
		protected Declaration(string name)
		{
			this.name = name;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0008308E File Offset: 0x0008128E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x00083096 File Offset: 0x00081296
		public ImmutableArray<Declaration> Children
		{
			get
			{
				return this.GetDeclarationChildren();
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600185C RID: 6236
		public abstract DeclarationKind Kind { get; }

		// Token: 0x0600185D RID: 6237
		protected abstract ImmutableArray<Declaration> GetDeclarationChildren();

		// Token: 0x04000690 RID: 1680
		protected readonly string name;
	}
}
