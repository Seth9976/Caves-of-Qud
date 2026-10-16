using System;
using System.Collections.Generic;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	public class XRDeviceDescriptor
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x0003899E File Offset: 0x00036B9E
		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000389A6 File Offset: 0x00036BA6
		public static XRDeviceDescriptor FromJson(string json)
		{
			return JsonUtility.FromJson<XRDeviceDescriptor>(json);
		}

		// Token: 0x0400037B RID: 891
		public string deviceName;

		// Token: 0x0400037C RID: 892
		public string manufacturer;

		// Token: 0x0400037D RID: 893
		public string serialNumber;

		// Token: 0x0400037E RID: 894
		public InputDeviceCharacteristics characteristics;

		// Token: 0x0400037F RID: 895
		public int deviceId;

		// Token: 0x04000380 RID: 896
		public List<XRFeatureDescriptor> inputFeatures;
	}
}
