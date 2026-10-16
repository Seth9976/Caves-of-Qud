using System;
using System.Reflection;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200021E RID: 542
	internal class CommonMethodWellKnownAttributeData : WellKnownAttributeData, ISecurityAttributeTarget
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x00043F24 File Offset: 0x00042124
		public CommonMethodWellKnownAttributeData(bool preserveSigFirstWriteWins)
		{
			this._preserveSigFirstWriteWins = preserveSigFirstWriteWins;
			this._dllImportIndex = (this._methodImplIndex = (this._preserveSigIndex = -1));
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00043F57 File Offset: 0x00042157
		public CommonMethodWellKnownAttributeData()
			: this(false)
		{
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00043F60 File Offset: 0x00042160
		public void SetPreserveSignature(int attributeIndex)
		{
			this._preserveSigIndex = attributeIndex;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00043F69 File Offset: 0x00042169
		public void SetMethodImplementation(int attributeIndex, MethodImplAttributes attributes)
		{
			this._attributes = attributes;
			this._methodImplIndex = attributeIndex;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00043F79 File Offset: 0x00042179
		public void SetDllImport(int attributeIndex, string moduleName, string entryPointName, MethodImportAttributes flags, bool preserveSig)
		{
			this._platformInvokeInfo = new DllImportData(moduleName, entryPointName, flags);
			this._dllImportIndex = attributeIndex;
			this._dllImportPreserveSig = preserveSig;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00043F99 File Offset: 0x00042199
		public DllImportData DllImportPlatformInvokeData
		{
			get
			{
				return this._platformInvokeInfo;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00043FA4 File Offset: 0x000421A4
		public MethodImplAttributes MethodImplAttributes
		{
			get
			{
				MethodImplAttributes methodImplAttributes = this._attributes;
				if (this._dllImportPreserveSig || this._preserveSigIndex >= 0)
				{
					methodImplAttributes |= 128;
				}
				if (this._dllImportIndex >= 0 && !this._dllImportPreserveSig)
				{
					if (this._preserveSigFirstWriteWins)
					{
						if ((this._preserveSigIndex == -1 || this._dllImportIndex < this._preserveSigIndex) && (this._methodImplIndex == -1 || (this._attributes & 128) == null || this._dllImportIndex < this._methodImplIndex))
						{
							methodImplAttributes &= -129;
						}
					}
					else if (this._dllImportIndex > this._preserveSigIndex && (this._dllImportIndex > this._methodImplIndex || (this._attributes & 128) == null))
					{
						methodImplAttributes &= -129;
					}
				}
				return methodImplAttributes;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00044069 File Offset: 0x00042269
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x00044071 File Offset: 0x00042271
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

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0004407A File Offset: 0x0004227A
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x00044082 File Offset: 0x00042282
		public bool HasDynamicSecurityMethodAttribute
		{
			get
			{
				return this._hasDynamicSecurityMethodAttribute;
			}
			set
			{
				this._hasDynamicSecurityMethodAttribute = value;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0004408B File Offset: 0x0004228B
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x00044093 File Offset: 0x00042293
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

		// Token: 0x0600154F RID: 5455 RVA: 0x0004409C File Offset: 0x0004229C
		SecurityWellKnownAttributeData ISecurityAttributeTarget.GetOrCreateData()
		{
			if (this._lazySecurityAttributeData == null)
			{
				this._lazySecurityAttributeData = new SecurityWellKnownAttributeData();
			}
			return this._lazySecurityAttributeData;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000440B7 File Offset: 0x000422B7
		internal bool HasDeclarativeSecurity
		{
			get
			{
				return this._lazySecurityAttributeData != null || this.HasSuppressUnmanagedCodeSecurityAttribute;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000440C9 File Offset: 0x000422C9
		public SecurityWellKnownAttributeData SecurityInformation
		{
			get
			{
				return this._lazySecurityAttributeData;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000440D1 File Offset: 0x000422D1
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x000440D9 File Offset: 0x000422D9
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

		// Token: 0x040008BE RID: 2238
		private readonly bool _preserveSigFirstWriteWins;

		// Token: 0x040008BF RID: 2239
		private DllImportData _platformInvokeInfo;

		// Token: 0x040008C0 RID: 2240
		private bool _dllImportPreserveSig;

		// Token: 0x040008C1 RID: 2241
		private int _dllImportIndex;

		// Token: 0x040008C2 RID: 2242
		private int _methodImplIndex;

		// Token: 0x040008C3 RID: 2243
		private MethodImplAttributes _attributes;

		// Token: 0x040008C4 RID: 2244
		private int _preserveSigIndex;

		// Token: 0x040008C5 RID: 2245
		private bool _hasSpecialNameAttribute;

		// Token: 0x040008C6 RID: 2246
		private bool _hasDynamicSecurityMethodAttribute;

		// Token: 0x040008C7 RID: 2247
		private bool _hasSuppressUnmanagedCodeSecurityAttribute;

		// Token: 0x040008C8 RID: 2248
		private SecurityWellKnownAttributeData _lazySecurityAttributeData;

		// Token: 0x040008C9 RID: 2249
		private bool _hasExcludeFromCodeCoverageAttribute;
	}
}
