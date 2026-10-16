using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C7 RID: 1223
	internal sealed class MethodEarlyWellKnownAttributeData : CommonMethodEarlyWellKnownAttributeData
	{
		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x0600685F RID: 26719 RVA: 0x0018818A File Offset: 0x0018638A
		// (set) Token: 0x06006860 RID: 26720 RVA: 0x00188192 File Offset: 0x00186392
		public bool UnmanagedCallersOnlyAttributePresent
		{
			get
			{
				return this._unmanagedCallersOnlyAttributePresent;
			}
			set
			{
				this._unmanagedCallersOnlyAttributePresent = value;
			}
		}

		// Token: 0x040019DC RID: 6620
		private bool _unmanagedCallersOnlyAttributePresent;
	}
}
