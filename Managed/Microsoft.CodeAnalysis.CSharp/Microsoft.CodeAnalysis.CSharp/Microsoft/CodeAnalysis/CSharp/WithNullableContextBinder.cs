using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200007E RID: 126
	internal sealed class WithNullableContextBinder : Binder
	{
		// Token: 0x06000B02 RID: 2818 RVA: 0x000517CC File Offset: 0x0004F9CC
		internal WithNullableContextBinder(SyntaxTree syntaxTree, int position, Binder next)
			: base(next, null)
		{
			this._syntaxTree = syntaxTree;
			this._position = position;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000517E4 File Offset: 0x0004F9E4
		internal override bool AreNullableAnnotationsGloballyEnabled()
		{
			return base.Next.AreNullableAnnotationsEnabled(this._syntaxTree, this._position);
		}

		// Token: 0x04000481 RID: 1153
		private readonly SyntaxTree _syntaxTree;

		// Token: 0x04000482 RID: 1154
		private readonly int _position;
	}
}
