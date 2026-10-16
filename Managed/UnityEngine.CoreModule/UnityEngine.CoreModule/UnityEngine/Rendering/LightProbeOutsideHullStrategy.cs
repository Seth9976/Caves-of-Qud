using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004B8 RID: 1208
	public enum LightProbeOutsideHullStrategy
	{
		// Token: 0x04000E5B RID: 3675
		[InspectorName("Find closest Light Probe")]
		kLightProbeSearchTetrahedralHull,
		// Token: 0x04000E5C RID: 3676
		[InspectorName("Use Ambient Probe")]
		kLightProbeUseAmbientProbe
	}
}
