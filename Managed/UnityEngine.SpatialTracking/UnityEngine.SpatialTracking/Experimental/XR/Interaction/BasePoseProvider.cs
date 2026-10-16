using System;
using UnityEngine.SpatialTracking;

namespace UnityEngine.Experimental.XR.Interaction
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public abstract class BasePoseProvider : MonoBehaviour
	{
		// Token: 0x06000027 RID: 39 RVA: 0x0000260C File Offset: 0x0000080C
		public virtual PoseDataFlags GetPoseFromProvider(out Pose output)
		{
			if (this.TryGetPoseFromProvider(out output))
			{
				return PoseDataFlags.Position | PoseDataFlags.Rotation;
			}
			return PoseDataFlags.NoData;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000261A File Offset: 0x0000081A
		[Obsolete("This function is provided for backwards compatibility with the BasePoseProvider found in com.unity.xr.legacyinputhelpers v1.3.X. Please do not implement this function, instead use the new API via GetPoseFromProvider", false)]
		public virtual bool TryGetPoseFromProvider(out Pose output)
		{
			output = Pose.identity;
			return false;
		}
	}
}
