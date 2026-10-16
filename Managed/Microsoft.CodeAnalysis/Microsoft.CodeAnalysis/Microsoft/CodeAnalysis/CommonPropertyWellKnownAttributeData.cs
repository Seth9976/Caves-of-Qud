using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000223 RID: 547
	internal class CommonPropertyWellKnownAttributeData : WellKnownAttributeData
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x00044220 File Offset: 0x00042420
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x00044228 File Offset: 0x00042428
		public bool HasSpecialNameAttribute
		{
			get
			{
				return this._hasSpecialNameAttribute;
			}
			set
			{
				this._hasSpecialNameAttribute = value;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00044231 File Offset: 0x00042431
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x00044239 File Offset: 0x00042439
		public bool HasExcludeFromCodeCoverageAttribute
		{
			get
			{
				return this._hasExcludeFromCodeCoverageAttribute;
			}
			set
			{
				this._hasExcludeFromCodeCoverageAttribute = value;
			}
		}

		// Token: 0x040008D6 RID: 2262
		private bool _hasSpecialNameAttribute;

		// Token: 0x040008D7 RID: 2263
		private bool _hasExcludeFromCodeCoverageAttribute;
	}
}
