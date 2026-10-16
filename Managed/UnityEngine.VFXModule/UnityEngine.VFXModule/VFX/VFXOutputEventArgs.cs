using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000020 RID: 32
	public struct VFXOutputEventArgs
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000362F File Offset: 0x0000182F
		public readonly int nameId { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003637 File Offset: 0x00001837
		public readonly VFXEventAttribute eventAttribute { get; }

		// Token: 0x06000103 RID: 259 RVA: 0x0000363F File Offset: 0x0000183F
		public VFXOutputEventArgs(int nameId, VFXEventAttribute eventAttribute)
		{
			this.nameId = nameId;
			this.eventAttribute = eventAttribute;
		}
	}
}
