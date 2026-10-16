using System;
using System.Collections.Generic;

namespace Mono.Cecil.Metadata
{
	// Token: 0x0200029A RID: 666
	internal sealed class GuidHeapBuffer : HeapBuffer
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00036EC3 File Offset: 0x000350C3
		public override bool IsEmpty
		{
			get
			{
				return this.length == 0;
			}
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x00036ECE File Offset: 0x000350CE
		public GuidHeapBuffer()
			: base(16)
		{
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00036EE4 File Offset: 0x000350E4
		public uint GetGuidIndex(Guid guid)
		{
			uint num;
			if (this.guids.TryGetValue(guid, out num))
			{
				return num;
			}
			num = (uint)(this.guids.Count + 1);
			this.WriteGuid(guid);
			this.guids.Add(guid, num);
			return num;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00036F26 File Offset: 0x00035126
		private void WriteGuid(Guid guid)
		{
			base.WriteBytes(guid.ToByteArray());
		}

		// Token: 0x0400064B RID: 1611
		private readonly Dictionary<Guid, uint> guids = new Dictionary<Guid, uint>();
	}
}
