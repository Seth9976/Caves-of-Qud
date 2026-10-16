using System;
using System.IO;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000CE RID: 206
	internal class BinaryStreamReader : BinaryReader
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001C612 File Offset: 0x0001A812
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x0001C620 File Offset: 0x0001A820
		public int Position
		{
			get
			{
				return (int)this.BaseStream.Position;
			}
			set
			{
				this.BaseStream.Position = (long)value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001C62F File Offset: 0x0001A82F
		public int Length
		{
			get
			{
				return (int)this.BaseStream.Length;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001C63D File Offset: 0x0001A83D
		public BinaryStreamReader(Stream stream)
			: base(stream)
		{
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0001C646 File Offset: 0x0001A846
		public void Advance(int bytes)
		{
			this.BaseStream.Seek((long)bytes, 1);
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0001C657 File Offset: 0x0001A857
		public void MoveTo(uint position)
		{
			this.BaseStream.Seek((long)((ulong)position), 0);
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0001C668 File Offset: 0x0001A868
		public void Align(int align)
		{
			align--;
			int position = this.Position;
			this.Advance(((position + align) & ~align) - position);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0001C68F File Offset: 0x0001A88F
		public DataDirectory ReadDataDirectory()
		{
			return new DataDirectory(this.ReadUInt32(), this.ReadUInt32());
		}
	}
}
