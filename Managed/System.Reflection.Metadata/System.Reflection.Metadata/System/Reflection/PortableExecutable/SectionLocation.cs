using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000026 RID: 38
	public readonly struct SectionLocation
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000511A File Offset: 0x0000331A
		public int RelativeVirtualAddress { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00005122 File Offset: 0x00003322
		public int PointerToRawData { get; }

		/// <param name="relativeVirtualAddress" />
		/// <param name="pointerToRawData" />
		// Token: 0x06000171 RID: 369 RVA: 0x0000512A File Offset: 0x0000332A
		public SectionLocation(int relativeVirtualAddress, int pointerToRawData)
		{
			this.RelativeVirtualAddress = relativeVirtualAddress;
			this.PointerToRawData = pointerToRawData;
		}
	}
}
