using System;
using System.Collections.Generic;
using System.IO;

namespace NAudio.SoundFont
{
	// Token: 0x020000B3 RID: 179
	internal abstract class StructureBuilder<T>
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		public StructureBuilder()
		{
			this.Reset();
		}

		// Token: 0x060003E9 RID: 1001
		public abstract T Read(BinaryReader br);

		// Token: 0x060003EA RID: 1002
		public abstract void Write(BinaryWriter bw, T o);

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003EB RID: 1003
		public abstract int Length { get; }

		// Token: 0x060003EC RID: 1004 RVA: 0x0000D706 File Offset: 0x0000B906
		public void Reset()
		{
			this.data = new List<T>();
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000D713 File Offset: 0x0000B913
		public T[] Data
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x04000498 RID: 1176
		protected List<T> data;
	}
}
