using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000687 RID: 1671
	internal struct BitmapAllocator32
	{
		// Token: 0x0600308E RID: 12430 RVA: 0x000B80C0 File Offset: 0x000B62C0
		public void Construct(int pageHeight, int entryWidth = 1, int entryHeight = 1)
		{
			this.m_PageHeight = pageHeight;
			this.m_Pages = new List<BitmapAllocator32.Page>(1);
			this.m_AllocMap = new List<uint>(this.m_PageHeight * this.m_Pages.Capacity);
			this.m_EntryWidth = entryWidth;
			this.m_EntryHeight = entryHeight;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000B810C File Offset: 0x000B630C
		public void ForceFirstAlloc(ushort firstPageX, ushort firstPageY)
		{
			this.m_AllocMap.Add(4294967294U);
			for (int i = 1; i < this.m_PageHeight; i++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = firstPageX,
				y = firstPageY,
				freeSlots = 32 * this.m_PageHeight - 1
			});
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000B8184 File Offset: 0x000B6384
		public BMPAlloc Allocate(BaseShaderInfoStorage storage)
		{
			int count = this.m_Pages.Count;
			for (int i = 0; i < count; i++)
			{
				BitmapAllocator32.Page page = this.m_Pages[i];
				bool flag = page.freeSlots == 0;
				if (!flag)
				{
					int j = i * this.m_PageHeight;
					int num = j + this.m_PageHeight;
					while (j < num)
					{
						uint num2 = this.m_AllocMap[j];
						bool flag2 = num2 == 0U;
						if (!flag2)
						{
							byte b = BitmapAllocator32.CountTrailingZeroes(num2);
							this.m_AllocMap[j] = num2 & ~(1U << (int)b);
							page.freeSlots--;
							this.m_Pages[i] = page;
							return new BMPAlloc
							{
								page = i,
								pageLine = (ushort)(j - i * this.m_PageHeight),
								bitIndex = b,
								ownedState = OwnedState.Owned
							};
						}
						j++;
					}
				}
			}
			RectInt rectInt;
			bool flag3 = storage == null || !storage.AllocateRect(32 * this.m_EntryWidth, this.m_PageHeight * this.m_EntryHeight, out rectInt);
			if (flag3)
			{
				return BMPAlloc.Invalid;
			}
			this.m_AllocMap.Capacity += this.m_PageHeight;
			this.m_AllocMap.Add(4294967294U);
			for (int k = 1; k < this.m_PageHeight; k++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = (ushort)rectInt.xMin,
				y = (ushort)rectInt.yMin,
				freeSlots = 32 * this.m_PageHeight - 1
			});
			return new BMPAlloc
			{
				page = this.m_Pages.Count - 1,
				ownedState = OwnedState.Owned
			};
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000B8398 File Offset: 0x000B6598
		public void Free(BMPAlloc alloc)
		{
			Debug.Assert(alloc.ownedState == OwnedState.Owned);
			int num = alloc.page * this.m_PageHeight + (int)alloc.pageLine;
			this.m_AllocMap[num] = this.m_AllocMap[num] | (1U << (int)alloc.bitIndex);
			BitmapAllocator32.Page page = this.m_Pages[alloc.page];
			page.freeSlots++;
			this.m_Pages[alloc.page] = page;
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000B8420 File Offset: 0x000B6620
		public int entryWidth
		{
			get
			{
				return this.m_EntryWidth;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x000B8438 File Offset: 0x000B6638
		public int entryHeight
		{
			get
			{
				return this.m_EntryHeight;
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000B8450 File Offset: 0x000B6650
		internal void GetAllocPageAtlasLocation(int page, out ushort x, out ushort y)
		{
			BitmapAllocator32.Page page2 = this.m_Pages[page];
			x = page2.x;
			y = page2.y;
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000B847C File Offset: 0x000B667C
		private static byte CountTrailingZeroes(uint val)
		{
			byte b = 0;
			bool flag = (val & 65535U) == 0U;
			if (flag)
			{
				val >>= 16;
				b = 16;
			}
			bool flag2 = (val & 255U) == 0U;
			if (flag2)
			{
				val >>= 8;
				b += 8;
			}
			bool flag3 = (val & 15U) == 0U;
			if (flag3)
			{
				val >>= 4;
				b += 4;
			}
			bool flag4 = (val & 3U) == 0U;
			if (flag4)
			{
				val >>= 2;
				b += 2;
			}
			bool flag5 = (val & 1U) == 0U;
			if (flag5)
			{
				b += 1;
			}
			return b;
		}

		// Token: 0x040017E9 RID: 6121
		public const int kPageWidth = 32;

		// Token: 0x040017EA RID: 6122
		private int m_PageHeight;

		// Token: 0x040017EB RID: 6123
		private List<BitmapAllocator32.Page> m_Pages;

		// Token: 0x040017EC RID: 6124
		private List<uint> m_AllocMap;

		// Token: 0x040017ED RID: 6125
		private int m_EntryWidth;

		// Token: 0x040017EE RID: 6126
		private int m_EntryHeight;

		// Token: 0x02000688 RID: 1672
		private struct Page
		{
			// Token: 0x040017EF RID: 6127
			public ushort x;

			// Token: 0x040017F0 RID: 6128
			public ushort y;

			// Token: 0x040017F1 RID: 6129
			public int freeSlots;
		}
	}
}
