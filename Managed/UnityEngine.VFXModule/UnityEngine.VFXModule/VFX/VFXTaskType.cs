using System;

namespace UnityEngine.VFX
{
	// Token: 0x02000007 RID: 7
	internal enum VFXTaskType
	{
		// Token: 0x040000BD RID: 189
		None,
		// Token: 0x040000BE RID: 190
		Spawner = 268435456,
		// Token: 0x040000BF RID: 191
		Initialize = 536870912,
		// Token: 0x040000C0 RID: 192
		Update = 805306368,
		// Token: 0x040000C1 RID: 193
		Output = 1073741824,
		// Token: 0x040000C2 RID: 194
		CameraSort = 805306369,
		// Token: 0x040000C3 RID: 195
		PerCameraUpdate,
		// Token: 0x040000C4 RID: 196
		PerCameraSort,
		// Token: 0x040000C5 RID: 197
		PerOutputSort,
		// Token: 0x040000C6 RID: 198
		GlobalSort,
		// Token: 0x040000C7 RID: 199
		ParticlePointOutput = 1073741824,
		// Token: 0x040000C8 RID: 200
		ParticleLineOutput,
		// Token: 0x040000C9 RID: 201
		ParticleQuadOutput,
		// Token: 0x040000CA RID: 202
		ParticleHexahedronOutput,
		// Token: 0x040000CB RID: 203
		ParticleMeshOutput,
		// Token: 0x040000CC RID: 204
		ParticleTriangleOutput,
		// Token: 0x040000CD RID: 205
		ParticleOctagonOutput,
		// Token: 0x040000CE RID: 206
		ConstantRateSpawner = 268435456,
		// Token: 0x040000CF RID: 207
		BurstSpawner,
		// Token: 0x040000D0 RID: 208
		PeriodicBurstSpawner,
		// Token: 0x040000D1 RID: 209
		VariableRateSpawner,
		// Token: 0x040000D2 RID: 210
		CustomCallbackSpawner,
		// Token: 0x040000D3 RID: 211
		SetAttributeSpawner,
		// Token: 0x040000D4 RID: 212
		EvaluateExpressionsSpawner
	}
}
