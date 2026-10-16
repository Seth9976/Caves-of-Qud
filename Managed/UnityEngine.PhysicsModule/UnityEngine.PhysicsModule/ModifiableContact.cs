using System;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	internal struct ModifiableContact
	{
		// Token: 0x0400008C RID: 140
		public Vector3 contact;

		// Token: 0x0400008D RID: 141
		public float separation;

		// Token: 0x0400008E RID: 142
		public Vector3 targetVelocity;

		// Token: 0x0400008F RID: 143
		public float maxImpulse;

		// Token: 0x04000090 RID: 144
		public Vector3 normal;

		// Token: 0x04000091 RID: 145
		public float restitution;

		// Token: 0x04000092 RID: 146
		public uint materialFlags;

		// Token: 0x04000093 RID: 147
		public ushort materialIndex;

		// Token: 0x04000094 RID: 148
		public ushort otherMaterialIndex;

		// Token: 0x04000095 RID: 149
		public float staticFriction;

		// Token: 0x04000096 RID: 150
		public float dynamicFriction;
	}
}
