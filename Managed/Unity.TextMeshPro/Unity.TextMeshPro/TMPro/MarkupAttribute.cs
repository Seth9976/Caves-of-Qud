using System;

namespace TMPro
{
	// Token: 0x02000066 RID: 102
	internal struct MarkupAttribute
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0002B1B9 File Offset: 0x000293B9
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0002B1C1 File Offset: 0x000293C1
		public int NameHashCode
		{
			get
			{
				return this.m_NameHashCode;
			}
			set
			{
				this.m_NameHashCode = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0002B1CA File Offset: 0x000293CA
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0002B1D2 File Offset: 0x000293D2
		public int ValueHashCode
		{
			get
			{
				return this.m_ValueHashCode;
			}
			set
			{
				this.m_ValueHashCode = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0002B1DB File Offset: 0x000293DB
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0002B1E3 File Offset: 0x000293E3
		public int ValueStartIndex
		{
			get
			{
				return this.m_ValueStartIndex;
			}
			set
			{
				this.m_ValueStartIndex = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0002B1EC File Offset: 0x000293EC
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0002B1F4 File Offset: 0x000293F4
		public int ValueLength
		{
			get
			{
				return this.m_ValueLength;
			}
			set
			{
				this.m_ValueLength = value;
			}
		}

		// Token: 0x040004BB RID: 1211
		private int m_NameHashCode;

		// Token: 0x040004BC RID: 1212
		private int m_ValueHashCode;

		// Token: 0x040004BD RID: 1213
		private int m_ValueStartIndex;

		// Token: 0x040004BE RID: 1214
		private int m_ValueLength;
	}
}
