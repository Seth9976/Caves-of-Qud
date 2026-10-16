using System;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000220 RID: 544
	internal abstract class CommonParameterEarlyWellKnownAttributeData : EarlyWellKnownAttributeData
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00044127 File Offset: 0x00042327
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x0004412F File Offset: 0x0004232F
		public ConstantValue DefaultParameterValue
		{
			get
			{
				return this._defaultParameterValue;
			}
			set
			{
				this._defaultParameterValue = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00044138 File Offset: 0x00042338
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x00044140 File Offset: 0x00042340
		public bool HasCallerLineNumberAttribute
		{
			get
			{
				return this._hasCallerLineNumberAttribute;
			}
			set
			{
				this._hasCallerLineNumberAttribute = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00044149 File Offset: 0x00042349
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x00044151 File Offset: 0x00042351
		public bool HasCallerFilePathAttribute
		{
			get
			{
				return this._hasCallerFilePathAttribute;
			}
			set
			{
				this._hasCallerFilePathAttribute = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x0004415A File Offset: 0x0004235A
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x00044162 File Offset: 0x00042362
		public bool HasCallerMemberNameAttribute
		{
			get
			{
				return this._hasCallerMemberNameAttribute;
			}
			set
			{
				this._hasCallerMemberNameAttribute = value;
			}
		}

		// Token: 0x040008CC RID: 2252
		private ConstantValue _defaultParameterValue = ConstantValue.Unset;

		// Token: 0x040008CD RID: 2253
		private bool _hasCallerLineNumberAttribute;

		// Token: 0x040008CE RID: 2254
		private bool _hasCallerFilePathAttribute;

		// Token: 0x040008CF RID: 2255
		private bool _hasCallerMemberNameAttribute;
	}
}
