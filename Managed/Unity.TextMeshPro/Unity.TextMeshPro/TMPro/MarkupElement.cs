using System;

namespace TMPro
{
	// Token: 0x02000067 RID: 103
	internal struct MarkupElement
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0002B1FD File Offset: 0x000293FD
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0002B21A File Offset: 0x0002941A
		public int NameHashCode
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].NameHashCode;
				}
				return 0;
			}
			set
			{
				if (this.m_Attributes == null)
				{
					this.m_Attributes = new MarkupAttribute[8];
				}
				this.m_Attributes[0].NameHashCode = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0002B242 File Offset: 0x00029442
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0002B25F File Offset: 0x0002945F
		public int ValueHashCode
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].ValueHashCode;
				}
				return 0;
			}
			set
			{
				this.m_Attributes[0].ValueHashCode = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0002B273 File Offset: 0x00029473
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0002B290 File Offset: 0x00029490
		public int ValueStartIndex
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].ValueStartIndex;
				}
				return 0;
			}
			set
			{
				this.m_Attributes[0].ValueStartIndex = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0002B2A4 File Offset: 0x000294A4
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0002B2C1 File Offset: 0x000294C1
		public int ValueLength
		{
			get
			{
				if (this.m_Attributes != null)
				{
					return this.m_Attributes[0].ValueLength;
				}
				return 0;
			}
			set
			{
				this.m_Attributes[0].ValueLength = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0002B2D5 File Offset: 0x000294D5
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0002B2DD File Offset: 0x000294DD
		public MarkupAttribute[] Attributes
		{
			get
			{
				return this.m_Attributes;
			}
			set
			{
				this.m_Attributes = value;
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0002B2E8 File Offset: 0x000294E8
		public MarkupElement(int nameHashCode, int startIndex, int length)
		{
			this.m_Attributes = new MarkupAttribute[8];
			this.m_Attributes[0].NameHashCode = nameHashCode;
			this.m_Attributes[0].ValueStartIndex = startIndex;
			this.m_Attributes[0].ValueLength = length;
		}

		// Token: 0x040004BF RID: 1215
		private MarkupAttribute[] m_Attributes;
	}
}
