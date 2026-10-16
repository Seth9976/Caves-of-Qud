using System;

namespace UnityEngine
{
	// Token: 0x02000028 RID: 40
	internal struct ModifiableContactPatch
	{
		// Token: 0x04000097 RID: 151
		public ModifiableMassProperties massProperties;

		// Token: 0x04000098 RID: 152
		public Vector3 normal;

		// Token: 0x04000099 RID: 153
		public float restitution;

		// Token: 0x0400009A RID: 154
		public float dynamicFriction;

		// Token: 0x0400009B RID: 155
		public float staticFriction;

		// Token: 0x0400009C RID: 156
		public byte startContactIndex;

		// Token: 0x0400009D RID: 157
		public byte contactCount;

		// Token: 0x0400009E RID: 158
		public byte materialFlags;

		// Token: 0x0400009F RID: 159
		public byte internalFlags;

		// Token: 0x040000A0 RID: 160
		public ushort materialIndex;

		// Token: 0x040000A1 RID: 161
		public ushort otherMaterialIndex;

		// Token: 0x02000029 RID: 41
		public enum Flags
		{
			// Token: 0x040000A3 RID: 163
			HasFaceIndices = 1,
			// Token: 0x040000A4 RID: 164
			HasModifiedMassRatios = 8,
			// Token: 0x040000A5 RID: 165
			HasTargetVelocity = 16,
			// Token: 0x040000A6 RID: 166
			HasMaxImpulse = 32,
			// Token: 0x040000A7 RID: 167
			RegeneratePatches = 64
		}
	}
}
