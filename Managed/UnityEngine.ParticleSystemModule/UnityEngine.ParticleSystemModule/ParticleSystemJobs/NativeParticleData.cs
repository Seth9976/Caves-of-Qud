using System;

namespace UnityEngine.ParticleSystemJobs
{
	// Token: 0x0200006C RID: 108
	internal struct NativeParticleData
	{
		// Token: 0x040001B4 RID: 436
		internal int count;

		// Token: 0x040001B5 RID: 437
		internal NativeParticleData.Array3 positions;

		// Token: 0x040001B6 RID: 438
		internal NativeParticleData.Array3 velocities;

		// Token: 0x040001B7 RID: 439
		internal NativeParticleData.Array3 axisOfRotations;

		// Token: 0x040001B8 RID: 440
		internal NativeParticleData.Array3 rotations;

		// Token: 0x040001B9 RID: 441
		internal NativeParticleData.Array3 rotationalSpeeds;

		// Token: 0x040001BA RID: 442
		internal NativeParticleData.Array3 sizes;

		// Token: 0x040001BB RID: 443
		internal unsafe void* startColors;

		// Token: 0x040001BC RID: 444
		internal unsafe void* aliveTimePercent;

		// Token: 0x040001BD RID: 445
		internal unsafe void* inverseStartLifetimes;

		// Token: 0x040001BE RID: 446
		internal unsafe void* randomSeeds;

		// Token: 0x040001BF RID: 447
		internal NativeParticleData.Array4 customData1;

		// Token: 0x040001C0 RID: 448
		internal NativeParticleData.Array4 customData2;

		// Token: 0x040001C1 RID: 449
		internal unsafe void* meshIndices;

		// Token: 0x0200006D RID: 109
		internal struct Array3
		{
			// Token: 0x040001C2 RID: 450
			internal unsafe float* x;

			// Token: 0x040001C3 RID: 451
			internal unsafe float* y;

			// Token: 0x040001C4 RID: 452
			internal unsafe float* z;
		}

		// Token: 0x0200006E RID: 110
		internal struct Array4
		{
			// Token: 0x040001C5 RID: 453
			internal unsafe float* x;

			// Token: 0x040001C6 RID: 454
			internal unsafe float* y;

			// Token: 0x040001C7 RID: 455
			internal unsafe float* z;

			// Token: 0x040001C8 RID: 456
			internal unsafe float* w;
		}
	}
}
