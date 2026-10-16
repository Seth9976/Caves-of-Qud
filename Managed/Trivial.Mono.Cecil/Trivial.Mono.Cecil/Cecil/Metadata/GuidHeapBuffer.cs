using System;
using System.Collections.Generic;

namespace Trivial.Mono.Cecil.Metadata
{
	// Token: 0x020000DF RID: 223
	internal sealed class GuidHeapBuffer : HeapBuffer
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001FA47 File Offset: 0x0001DC47
		public override bool IsEmpty
		{
			get
			{
				return this.length == 0;
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0001FA52 File Offset: 0x0001DC52
		public GuidHeapBuffer()
			: base(16)
		{
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0001FA68 File Offset: 0x0001DC68
		public uint GetGuidIndex(Guid guid)
		{
			uint num;
			if (this.guids.TryGetValue(guid, ref num))
			{
				return num;
			}
			num = (uint)(this.guids.Count + 1);
			this.WriteGuid(guid);
			this.guids.Add(guid, num);
			return num;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0001FAAA File Offset: 0x0001DCAA
		private void WriteGuid(Guid guid)
		{
			base.WriteBytes(guid.ToByteArray());
		}

		// Token: 0x040003B1 RID: 945
		private readonly Dictionary<Guid, uint> guids = new Dictionary<Guid, uint>();
	}
}
