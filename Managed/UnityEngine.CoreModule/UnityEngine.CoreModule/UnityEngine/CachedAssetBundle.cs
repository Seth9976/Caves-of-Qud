using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000118 RID: 280
	[UsedByNativeCode]
	public struct CachedAssetBundle
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x0000B43A File Offset: 0x0000963A
		public CachedAssetBundle(string name, Hash128 hash)
		{
			this.m_Name = name;
			this.m_Hash = hash;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000B44C File Offset: 0x0000964C
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000B464 File Offset: 0x00009664
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000B470 File Offset: 0x00009670
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000B488 File Offset: 0x00009688
		public Hash128 hash
		{
			get
			{
				return this.m_Hash;
			}
			set
			{
				this.m_Hash = value;
			}
		}

		// Token: 0x040003B9 RID: 953
		private string m_Name;

		// Token: 0x040003BA RID: 954
		private Hash128 m_Hash;
	}
}
