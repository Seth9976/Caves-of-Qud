using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000017 RID: 23
	internal sealed class ConstantFieldsInProgressBinder : Binder
	{
		// Token: 0x06000520 RID: 1312 RVA: 0x000333F0 File Offset: 0x000315F0
		internal ConstantFieldsInProgressBinder(ConstantFieldsInProgress inProgress, Binder next)
			: base(next, BinderFlags.FieldInitializer | next.Flags)
		{
			this._inProgress = inProgress;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00033408 File Offset: 0x00031608
		internal override ConstantFieldsInProgress ConstantFieldsInProgress
		{
			get
			{
				return this._inProgress;
			}
		}

		// Token: 0x04000049 RID: 73
		private readonly ConstantFieldsInProgress _inProgress;
	}
}
