using System;
using Unity.Collections;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x02000069 RID: 105
	public struct ParticleSystemNativeArray3
	{
		// Token: 0x17000246 RID: 582
		public Vector3 this[int index]
		{
			get
			{
				return new Vector3(this.x[index], this.y[index], this.z[index]);
			}
			set
			{
				this.x[index] = value.x;
				this.y[index] = value.y;
				this.z[index] = value.z;
			}
		}

		// Token: 0x0400019F RID: 415
		public NativeArray<float> x;

		// Token: 0x040001A0 RID: 416
		public NativeArray<float> y;

		// Token: 0x040001A1 RID: 417
		public NativeArray<float> z;
	}
}
