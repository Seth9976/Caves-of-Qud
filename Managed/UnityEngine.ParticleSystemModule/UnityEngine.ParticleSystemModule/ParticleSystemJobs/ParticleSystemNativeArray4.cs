using System;
using Unity.Collections;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200006A RID: 106
	public struct ParticleSystemNativeArray4
	{
		// Token: 0x17000247 RID: 583
		public Vector4 this[int index]
		{
			get
			{
				return new Vector4(this.x[index], this.y[index], this.z[index], this.w[index]);
			}
			set
			{
				this.x[index] = value.x;
				this.y[index] = value.y;
				this.z[index] = value.z;
				this.w[index] = value.w;
			}
		}

		// Token: 0x040001A2 RID: 418
		public NativeArray<float> x;

		// Token: 0x040001A3 RID: 419
		public NativeArray<float> y;

		// Token: 0x040001A4 RID: 420
		public NativeArray<float> z;

		// Token: 0x040001A5 RID: 421
		public NativeArray<float> w;
	}
}
