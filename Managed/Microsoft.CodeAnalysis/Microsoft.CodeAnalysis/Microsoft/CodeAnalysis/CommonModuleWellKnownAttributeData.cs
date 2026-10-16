using System;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200021F RID: 543
	internal class CommonModuleWellKnownAttributeData : WellKnownAttributeData
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000440E2 File Offset: 0x000422E2
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x000440EA File Offset: 0x000422EA
		public bool HasDebuggableAttribute
		{
			get
			{
				return this._hasDebuggableAttribute;
			}
			set
			{
				this._hasDebuggableAttribute = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x000440F3 File Offset: 0x000422F3
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x000440FB File Offset: 0x000422FB
		internal CharSet DefaultCharacterSet
		{
			get
			{
				return this._defaultCharacterSet;
			}
			set
			{
				this._defaultCharacterSet = value;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00044105 File Offset: 0x00042305
		internal bool HasDefaultCharSetAttribute
		{
			get
			{
				return this._defaultCharacterSet > 0;
			}
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00044110 File Offset: 0x00042310
		internal static bool IsValidCharSet(CharSet value)
		{
			return value >= 1 && value <= 4;
		}

		// Token: 0x040008CA RID: 2250
		private bool _hasDebuggableAttribute;

		// Token: 0x040008CB RID: 2251
		private byte _defaultCharacterSet;
	}
}
