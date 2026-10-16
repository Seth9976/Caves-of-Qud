using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200003F RID: 63
	internal sealed class NameofBinder : Binder
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x0003BC72 File Offset: 0x00039E72
		public NameofBinder(SyntaxNode nameofArgument, Binder next)
			: base(next, null)
		{
			this._nameofArgument = nameofArgument;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0003BC83 File Offset: 0x00039E83
		protected override SyntaxNode EnclosingNameofArgument
		{
			get
			{
				return this._nameofArgument;
			}
		}

		// Token: 0x040000CD RID: 205
		private readonly SyntaxNode _nameofArgument;
	}
}
