using System;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	public enum ParticleSystemShapeType
	{
		// Token: 0x040000AF RID: 175
		Sphere,
		// Token: 0x040000B0 RID: 176
		[Obsolete("SphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		SphereShell,
		// Token: 0x040000B1 RID: 177
		Hemisphere,
		// Token: 0x040000B2 RID: 178
		[Obsolete("HemisphereShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		HemisphereShell,
		// Token: 0x040000B3 RID: 179
		Cone,
		// Token: 0x040000B4 RID: 180
		Box,
		// Token: 0x040000B5 RID: 181
		Mesh,
		// Token: 0x040000B6 RID: 182
		[Obsolete("ConeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeShell,
		// Token: 0x040000B7 RID: 183
		ConeVolume,
		// Token: 0x040000B8 RID: 184
		[Obsolete("ConeVolumeShell is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		ConeVolumeShell,
		// Token: 0x040000B9 RID: 185
		Circle,
		// Token: 0x040000BA RID: 186
		[Obsolete("CircleEdge is deprecated and does nothing. Please use ShapeModule.radiusThickness instead, to control edge emission.", false)]
		CircleEdge,
		// Token: 0x040000BB RID: 187
		SingleSidedEdge,
		// Token: 0x040000BC RID: 188
		MeshRenderer,
		// Token: 0x040000BD RID: 189
		SkinnedMeshRenderer,
		// Token: 0x040000BE RID: 190
		BoxShell,
		// Token: 0x040000BF RID: 191
		BoxEdge,
		// Token: 0x040000C0 RID: 192
		Donut,
		// Token: 0x040000C1 RID: 193
		Rectangle,
		// Token: 0x040000C2 RID: 194
		Sprite,
		// Token: 0x040000C3 RID: 195
		SpriteRenderer
	}
}
