using System;

namespace Mono.Cecil
{
	// Token: 0x020001C3 RID: 451
	internal abstract class MetadataTable<TRow> : MetadataTable where TRow : struct
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00020C75 File Offset: 0x0001EE75
		public sealed override int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00020C80 File Offset: 0x0001EE80
		public int AddRow(TRow row)
		{
			if (this.rows.Length == this.length)
			{
				this.Grow();
			}
			TRow[] array = this.rows;
			int num = this.length;
			this.length = num + 1;
			array[num] = row;
			return this.length;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00020CC8 File Offset: 0x0001EEC8
		private void Grow()
		{
			TRow[] array = new TRow[this.rows.Length * 2];
			Array.Copy(this.rows, array, this.rows.Length);
			this.rows = array;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00018659 File Offset: 0x00016859
		public override void Sort()
		{
		}

		// Token: 0x0400029F RID: 671
		internal TRow[] rows = new TRow[2];

		// Token: 0x040002A0 RID: 672
		internal int length;
	}
}
