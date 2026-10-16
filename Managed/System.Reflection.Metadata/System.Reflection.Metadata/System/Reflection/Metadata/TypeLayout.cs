using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000DA RID: 218
	public readonly struct TypeLayout
	{
		/// <param name="size" />
		/// <param name="packingSize" />
		// Token: 0x060007ED RID: 2029 RVA: 0x00012F41 File Offset: 0x00011141
		public TypeLayout(int size, int packingSize)
		{
			this._size = size;
			this._packingSize = packingSize;
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00012F51 File Offset: 0x00011151
		public int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00012F59 File Offset: 0x00011159
		public int PackingSize
		{
			get
			{
				return this._packingSize;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00012F61 File Offset: 0x00011161
		public bool IsDefault
		{
			get
			{
				return this._size == 0 && this._packingSize == 0;
			}
		}

		// Token: 0x040004E3 RID: 1251
		private readonly int _size;

		// Token: 0x040004E4 RID: 1252
		private readonly int _packingSize;
	}
}
