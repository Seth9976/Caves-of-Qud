using System;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000E1 RID: 225
	internal abstract class HeapBuffer : ByteBuffer
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		public bool IsLarge
		{
			get
			{
				return this.length > 65535;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600096C RID: 2412
		public abstract bool IsEmpty { get; }

		// Token: 0x0600096D RID: 2413 RVA: 0x0001FAD7 File Offset: 0x0001DCD7
		protected HeapBuffer(int length)
			: base(length)
		{
		}
	}
}
