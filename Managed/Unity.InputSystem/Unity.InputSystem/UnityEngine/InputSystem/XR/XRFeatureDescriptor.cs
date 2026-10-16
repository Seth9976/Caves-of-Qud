using System;
using System.Collections.Generic;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public struct XRFeatureDescriptor
	{
		// Token: 0x04000377 RID: 887
		public string name;

		// Token: 0x04000378 RID: 888
		public List<UsageHint> usageHints;

		// Token: 0x04000379 RID: 889
		public FeatureType featureType;

		// Token: 0x0400037A RID: 890
		public uint customSize;
	}
}
