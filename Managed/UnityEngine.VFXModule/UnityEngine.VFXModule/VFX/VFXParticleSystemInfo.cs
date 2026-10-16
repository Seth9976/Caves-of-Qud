using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000024 RID: 36
	[UsedByNativeCode]
	[NativeHeader("Modules/VFX/Public/Systems/VFXParticleSystem.h")]
	public struct VFXParticleSystemInfo
	{
		// Token: 0x060001EA RID: 490 RVA: 0x00004C04 File Offset: 0x00002E04
		public VFXParticleSystemInfo(uint aliveCount, uint capacity, bool sleeping, Bounds bounds)
		{
			this.aliveCount = aliveCount;
			this.capacity = capacity;
			this.sleeping = sleeping;
			this.bounds = bounds;
		}

		// Token: 0x04000135 RID: 309
		public uint aliveCount;

		// Token: 0x04000136 RID: 310
		public uint capacity;

		// Token: 0x04000137 RID: 311
		public bool sleeping;

		// Token: 0x04000138 RID: 312
		public Bounds bounds;
	}
}
