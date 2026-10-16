using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E3 RID: 739
	public interface ITransform
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001521 RID: 5409
		// (set) Token: 0x06001522 RID: 5410
		Vector3 position { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001523 RID: 5411
		// (set) Token: 0x06001524 RID: 5412
		Quaternion rotation { get; set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001525 RID: 5413
		// (set) Token: 0x06001526 RID: 5414
		Vector3 scale { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001527 RID: 5415
		Matrix4x4 matrix { get; }
	}
}
