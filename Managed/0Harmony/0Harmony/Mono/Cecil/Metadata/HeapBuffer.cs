using System;
using Mono.Cecil.PE;

namespace Mono.Cecil.Metadata
{
	// Token: 0x02000299 RID: 665
	internal abstract class HeapBuffer : ByteBuffer
	{
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00036EAB File Offset: 0x000350AB
		public bool IsLarge
		{
			get
			{
				return this.length > 65535;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060010FD RID: 4349
		public abstract bool IsEmpty { get; }

		// Token: 0x060010FE RID: 4350 RVA: 0x00036EBA File Offset: 0x000350BA
		protected HeapBuffer(int length)
			: base(length)
		{
		}
	}
}
