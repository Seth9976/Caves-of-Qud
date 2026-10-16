using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004C6 RID: 1222
	internal sealed class FieldWellKnownAttributeData : CommonFieldWellKnownAttributeData
	{
		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06006854 RID: 26708 RVA: 0x0018812D File Offset: 0x0018632D
		// (set) Token: 0x06006855 RID: 26709 RVA: 0x00188135 File Offset: 0x00186335
		public bool HasAllowNullAttribute
		{
			get
			{
				return this._hasAllowNullAttribute;
			}
			set
			{
				this._hasAllowNullAttribute = value;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x06006856 RID: 26710 RVA: 0x0018813E File Offset: 0x0018633E
		// (set) Token: 0x06006857 RID: 26711 RVA: 0x00188146 File Offset: 0x00186346
		public bool HasDisallowNullAttribute
		{
			get
			{
				return this._hasDisallowNullAttribute;
			}
			set
			{
				this._hasDisallowNullAttribute = value;
			}
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x06006858 RID: 26712 RVA: 0x0018814F File Offset: 0x0018634F
		// (set) Token: 0x06006859 RID: 26713 RVA: 0x00188157 File Offset: 0x00186357
		public bool HasMaybeNullAttribute
		{
			get
			{
				return this._hasMaybeNullAttribute;
			}
			set
			{
				this._hasMaybeNullAttribute = value;
			}
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x0600685A RID: 26714 RVA: 0x00188160 File Offset: 0x00186360
		// (set) Token: 0x0600685B RID: 26715 RVA: 0x00188168 File Offset: 0x00186368
		public bool? MaybeNullWhenAttribute
		{
			get
			{
				return this._maybeNullWhenAttribute;
			}
			set
			{
				this._maybeNullWhenAttribute = value;
			}
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x0600685C RID: 26716 RVA: 0x00188171 File Offset: 0x00186371
		// (set) Token: 0x0600685D RID: 26717 RVA: 0x00188179 File Offset: 0x00186379
		public bool HasNotNullAttribute
		{
			get
			{
				return this._hasNotNullAttribute;
			}
			set
			{
				this._hasNotNullAttribute = value;
			}
		}

		// Token: 0x040019D7 RID: 6615
		private bool _hasAllowNullAttribute;

		// Token: 0x040019D8 RID: 6616
		private bool _hasDisallowNullAttribute;

		// Token: 0x040019D9 RID: 6617
		private bool _hasMaybeNullAttribute;

		// Token: 0x040019DA RID: 6618
		private bool? _maybeNullWhenAttribute;

		// Token: 0x040019DB RID: 6619
		private bool _hasNotNullAttribute;
	}
}
