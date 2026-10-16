using System;
using System.Collections.Generic;

namespace UnityEngine.SpatialTracking
{
	// Token: 0x02000003 RID: 3
	internal class TrackedPoseDriverDataDescription
	{
		// Token: 0x04000001 RID: 1
		internal static List<TrackedPoseDriverDataDescription.PoseData> DeviceData = new List<TrackedPoseDriverDataDescription.PoseData>
		{
			new TrackedPoseDriverDataDescription.PoseData
			{
				PoseNames = new List<string> { "Left Eye", "Right Eye", "Center Eye - HMD Reference", "Head", "Color Camera" },
				Poses = new List<TrackedPoseDriver.TrackedPose>
				{
					TrackedPoseDriver.TrackedPose.LeftEye,
					TrackedPoseDriver.TrackedPose.RightEye,
					TrackedPoseDriver.TrackedPose.Center,
					TrackedPoseDriver.TrackedPose.Head,
					TrackedPoseDriver.TrackedPose.ColorCamera
				}
			},
			new TrackedPoseDriverDataDescription.PoseData
			{
				PoseNames = new List<string> { "Left Controller", "Right Controller" },
				Poses = new List<TrackedPoseDriver.TrackedPose>
				{
					TrackedPoseDriver.TrackedPose.LeftPose,
					TrackedPoseDriver.TrackedPose.RightPose
				}
			},
			new TrackedPoseDriverDataDescription.PoseData
			{
				PoseNames = new List<string> { "Device Pose" },
				Poses = new List<TrackedPoseDriver.TrackedPose> { TrackedPoseDriver.TrackedPose.RemotePose }
			}
		};

		// Token: 0x0200000A RID: 10
		internal struct PoseData
		{
			// Token: 0x04000015 RID: 21
			public List<string> PoseNames;

			// Token: 0x04000016 RID: 22
			public List<TrackedPoseDriver.TrackedPose> Poses;
		}
	}
}
