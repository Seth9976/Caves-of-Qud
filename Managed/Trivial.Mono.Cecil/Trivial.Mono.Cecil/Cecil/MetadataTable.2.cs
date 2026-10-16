using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000076 RID: 118
	internal abstract class MetadataTable<TRow> : MetadataTable where TRow : struct
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		public sealed override int Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000FA00 File Offset: 0x0000DC00
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

		// Token: 0x0600047C RID: 1148 RVA: 0x0000FA48 File Offset: 0x0000DC48
		private void Grow()
		{
			TRow[] array = new TRow[this.rows.Length * 2];
			Array.Copy(this.rows, array, this.rows.Length);
			this.rows = array;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00002921 File Offset: 0x00000B21
		public override void Sort()
		{
		}

		// Token: 0x0400011B RID: 283
		internal TRow[] rows = new TRow[2];

		// Token: 0x0400011C RID: 284
		internal int length;
	}
}
