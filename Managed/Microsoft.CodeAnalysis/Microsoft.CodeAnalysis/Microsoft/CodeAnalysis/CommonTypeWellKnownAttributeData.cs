using System;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000226 RID: 550
	internal class CommonTypeWellKnownAttributeData : WellKnownAttributeData, ISecurityAttributeTarget
	{
		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0004430D File Offset: 0x0004250D
		// (set) Token: 0x06001586 RID: 5510 RVA: 0x00044315 File Offset: 0x00042515
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

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x0004431E File Offset: 0x0004251E
		// (set) Token: 0x06001588 RID: 5512 RVA: 0x00044326 File Offset: 0x00042526
		public bool HasSerializableAttribute
		{
			get
			{
				return this._hasSerializableAttribute;
			}
			set
			{
				this._hasSerializableAttribute = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x0004432F File Offset: 0x0004252F
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x00044337 File Offset: 0x00042537
		public bool HasDefaultMemberAttribute
		{
			get
			{
				return this._hasDefaultMemberAttribute;
			}
			set
			{
				this._hasDefaultMemberAttribute = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00044340 File Offset: 0x00042540
		// (set) Token: 0x0600158C RID: 5516 RVA: 0x00044348 File Offset: 0x00042548
		public bool HasSuppressUnmanagedCodeSecurityAttribute
		{
			get
			{
				return this._hasSuppressUnmanagedCodeSecurityAttribute;
			}
			set
			{
				this._hasSuppressUnmanagedCodeSecurityAttribute = value;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00044351 File Offset: 0x00042551
		SecurityWellKnownAttributeData ISecurityAttributeTarget.GetOrCreateData()
		{
			if (this._lazySecurityAttributeData == null)
			{
				this._lazySecurityAttributeData = new SecurityWellKnownAttributeData();
			}
			return this._lazySecurityAttributeData;
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x0004436C File Offset: 0x0004256C
		internal bool HasDeclarativeSecurity
		{
			get
			{
				return this._lazySecurityAttributeData != null || this.HasSuppressUnmanagedCodeSecurityAttribute;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0004437E File Offset: 0x0004257E
		public SecurityWellKnownAttributeData SecurityInformation
		{
			get
			{
				return this._lazySecurityAttributeData;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00044386 File Offset: 0x00042586
		// (set) Token: 0x06001591 RID: 5521 RVA: 0x0004438E File Offset: 0x0004258E
		public bool HasWindowsRuntimeImportAttribute
		{
			get
			{
				return this._hasWindowsRuntimeImportAttribute;
			}
			set
			{
				this._hasWindowsRuntimeImportAttribute = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00044397 File Offset: 0x00042597
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x0004439F File Offset: 0x0004259F
		public string GuidString
		{
			get
			{
				return this._guidString;
			}
			set
			{
				this._guidString = value;
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x000443A8 File Offset: 0x000425A8
		public void SetStructLayout(TypeLayout layout, CharSet charSet)
		{
			this._layout = layout;
			this._charSet = charSet;
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x000443B8 File Offset: 0x000425B8
		public bool HasStructLayoutAttribute
		{
			get
			{
				return this._charSet > 0;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x000443C3 File Offset: 0x000425C3
		public TypeLayout Layout
		{
			get
			{
				return this._layout;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000443CB File Offset: 0x000425CB
		public CharSet MarshallingCharSet
		{
			get
			{
				return this._charSet;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x000443D3 File Offset: 0x000425D3
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x000443DB File Offset: 0x000425DB
		public bool HasSecurityCriticalAttributes
		{
			get
			{
				return this._hasSecurityCriticalAttributes;
			}
			set
			{
				this._hasSecurityCriticalAttributes = value;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x000443E4 File Offset: 0x000425E4
		// (set) Token: 0x0600159B RID: 5531 RVA: 0x000443EC File Offset: 0x000425EC
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

		// Token: 0x040008DE RID: 2270
		private bool _hasSpecialNameAttribute;

		// Token: 0x040008DF RID: 2271
		private bool _hasSerializableAttribute;

		// Token: 0x040008E0 RID: 2272
		private bool _hasDefaultMemberAttribute;

		// Token: 0x040008E1 RID: 2273
		private bool _hasSuppressUnmanagedCodeSecurityAttribute;

		// Token: 0x040008E2 RID: 2274
		private SecurityWellKnownAttributeData _lazySecurityAttributeData;

		// Token: 0x040008E3 RID: 2275
		private bool _hasWindowsRuntimeImportAttribute;

		// Token: 0x040008E4 RID: 2276
		private string _guidString;

		// Token: 0x040008E5 RID: 2277
		private TypeLayout _layout;

		// Token: 0x040008E6 RID: 2278
		private CharSet _charSet;

		// Token: 0x040008E7 RID: 2279
		private bool _hasSecurityCriticalAttributes;

		// Token: 0x040008E8 RID: 2280
		private bool _hasExcludeFromCodeCoverageAttribute;
	}
}
