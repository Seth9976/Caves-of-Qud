using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200021A RID: 538
	internal class CommonEventWellKnownAttributeData : WellKnownAttributeData, ISkipLocalsInitAttributeTarget
	{
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00043DB6 File Offset: 0x00041FB6
		// (set) Token: 0x06001529 RID: 5417 RVA: 0x00043DBE File Offset: 0x00041FBE
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600152A RID: 5418 RVA: 0x00043DC7 File Offset: 0x00041FC7
		// (set) Token: 0x0600152B RID: 5419 RVA: 0x00043DCF File Offset: 0x00041FCF
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

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00043DD8 File Offset: 0x00041FD8
		// (set) Token: 0x0600152D RID: 5421 RVA: 0x00043DE0 File Offset: 0x00041FE0
		public bool HasSkipLocalsInitAttribute
		{
			get
			{
				return this._hasSkipLocalsInitAttribute;
			}
			set
			{
				this._hasSkipLocalsInitAttribute = value;
			}
		}

		// Token: 0x040008B2 RID: 2226
		private bool _hasSpecialNameAttribute;

		// Token: 0x040008B3 RID: 2227
		private bool _hasExcludeFromCodeCoverageAttribute;

		// Token: 0x040008B4 RID: 2228
		private bool _hasSkipLocalsInitAttribute;
	}
}
