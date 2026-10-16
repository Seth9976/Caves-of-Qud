using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000031 RID: 49
	internal sealed class LocalInProgressBinder : Binder
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x0003A651 File Offset: 0x00038851
		internal LocalInProgressBinder(LocalSymbol inProgress, Binder next)
			: base(next, null)
		{
			this._inProgress = inProgress;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x0003A662 File Offset: 0x00038862
		internal override LocalSymbol LocalInProgress
		{
			get
			{
				return this._inProgress;
			}
		}

		// Token: 0x0400008D RID: 141
		private readonly LocalSymbol _inProgress;
	}
}
