using System;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000036 RID: 54
	[NativeHeader("Modules/Terrain/Public/SpeedTreeWindManager.h")]
	internal enum SpeedTreeWindParamIndex
	{
		// Token: 0x040000C8 RID: 200
		WindVector,
		// Token: 0x040000C9 RID: 201
		WindGlobal,
		// Token: 0x040000CA RID: 202
		TreeExtents_SharedHeightStart = 1,
		// Token: 0x040000CB RID: 203
		WindBranch,
		// Token: 0x040000CC RID: 204
		BranchStretchLimits = 2,
		// Token: 0x040000CD RID: 205
		WindBranchTwitch,
		// Token: 0x040000CE RID: 206
		Shared_NoisePosTurbulence_Independence = 3,
		// Token: 0x040000CF RID: 207
		WindBranchWhip,
		// Token: 0x040000D0 RID: 208
		Shared_Bend_Oscillation_Turbulence_Flexibility = 4,
		// Token: 0x040000D1 RID: 209
		WindBranchAnchor,
		// Token: 0x040000D2 RID: 210
		Branch1_NoisePosTurbulence_Independence = 5,
		// Token: 0x040000D3 RID: 211
		WindBranchAdherences,
		// Token: 0x040000D4 RID: 212
		Branch1_Bend_Oscillation_Turbulence_Flexibility = 6,
		// Token: 0x040000D5 RID: 213
		WindTurbulences,
		// Token: 0x040000D6 RID: 214
		Branch2_NoisePosTurbulence_Independence = 7,
		// Token: 0x040000D7 RID: 215
		WindLeaf1Ripple,
		// Token: 0x040000D8 RID: 216
		Branch2_Bend_Oscillation_Turbulence_Flexibility = 8,
		// Token: 0x040000D9 RID: 217
		WindLeaf1Tumble,
		// Token: 0x040000DA RID: 218
		Ripple_NoisePosTurbulence_Independence = 9,
		// Token: 0x040000DB RID: 219
		WindLeaf1Twitch,
		// Token: 0x040000DC RID: 220
		Ripple_Planar_Directional_Flexibility_Shimmer = 10,
		// Token: 0x040000DD RID: 221
		WindLeaf2Ripple,
		// Token: 0x040000DE RID: 222
		WindLeaf2Tumble,
		// Token: 0x040000DF RID: 223
		WindLeaf2Twitch,
		// Token: 0x040000E0 RID: 224
		WindFrondRipple,
		// Token: 0x040000E1 RID: 225
		WindParamsCount_v8,
		// Token: 0x040000E2 RID: 226
		WindParamsCount_v9 = 11,
		// Token: 0x040000E3 RID: 227
		MaxWindParamsCount = 16
	}
}
