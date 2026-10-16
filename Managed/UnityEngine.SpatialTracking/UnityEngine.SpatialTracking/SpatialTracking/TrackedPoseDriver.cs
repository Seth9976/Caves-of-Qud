using System;
using UnityEngine.Experimental.XR.Interaction;

namespace UnityEngine.SpatialTracking
{
	// Token: 0x02000006 RID: 6
	[DefaultExecutionOrder(-30000)]
	[AddComponentMenu("XR/Tracked Pose Driver")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.xr.legacyinputhelpers@2.1/manual/index.html")]
	[Serializable]
	public class TrackedPoseDriver : MonoBehaviour
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002356 File Offset: 0x00000556
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000235E File Offset: 0x0000055E
		public TrackedPoseDriver.DeviceType deviceType
		{
			get
			{
				return this.m_Device;
			}
			internal set
			{
				this.m_Device = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002367 File Offset: 0x00000567
		// (set) Token: 0x0600000C RID: 12 RVA: 0x0000236F File Offset: 0x0000056F
		public TrackedPoseDriver.TrackedPose poseSource
		{
			get
			{
				return this.m_PoseSource;
			}
			internal set
			{
				this.m_PoseSource = value;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002378 File Offset: 0x00000578
		public bool SetPoseSource(TrackedPoseDriver.DeviceType deviceType, TrackedPoseDriver.TrackedPose pose)
		{
			if (deviceType < (TrackedPoseDriver.DeviceType)TrackedPoseDriverDataDescription.DeviceData.Count)
			{
				TrackedPoseDriverDataDescription.PoseData poseData = TrackedPoseDriverDataDescription.DeviceData[(int)deviceType];
				for (int i = 0; i < poseData.Poses.Count; i++)
				{
					if (poseData.Poses[i] == pose)
					{
						this.deviceType = deviceType;
						this.poseSource = pose;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000023D4 File Offset: 0x000005D4
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000023DC File Offset: 0x000005DC
		public BasePoseProvider poseProviderComponent
		{
			get
			{
				return this.m_PoseProviderComponent;
			}
			set
			{
				this.m_PoseProviderComponent = value;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023E5 File Offset: 0x000005E5
		private PoseDataFlags GetPoseData(TrackedPoseDriver.DeviceType device, TrackedPoseDriver.TrackedPose poseSource, out Pose resultPose)
		{
			if (!(this.m_PoseProviderComponent != null))
			{
				return PoseDataSource.GetDataFromSource(poseSource, out resultPose);
			}
			return this.m_PoseProviderComponent.GetPoseFromProvider(out resultPose);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002409 File Offset: 0x00000609
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002411 File Offset: 0x00000611
		public TrackedPoseDriver.TrackingType trackingType
		{
			get
			{
				return this.m_TrackingType;
			}
			set
			{
				this.m_TrackingType = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000241A File Offset: 0x0000061A
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002422 File Offset: 0x00000622
		public TrackedPoseDriver.UpdateType updateType
		{
			get
			{
				return this.m_UpdateType;
			}
			set
			{
				this.m_UpdateType = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000242B File Offset: 0x0000062B
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002433 File Offset: 0x00000633
		public bool UseRelativeTransform
		{
			get
			{
				return this.m_UseRelativeTransform;
			}
			set
			{
				this.m_UseRelativeTransform = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000243C File Offset: 0x0000063C
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002444 File Offset: 0x00000644
		public Pose originPose
		{
			get
			{
				return this.m_OriginPose;
			}
			set
			{
				this.m_OriginPose = value;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000244D File Offset: 0x0000064D
		private void CacheLocalPosition()
		{
			this.m_OriginPose.position = base.transform.localPosition;
			this.m_OriginPose.rotation = base.transform.localRotation;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000247B File Offset: 0x0000067B
		private void ResetToCachedLocalPosition()
		{
			this.SetLocalTransform(this.m_OriginPose.position, this.m_OriginPose.rotation, PoseDataFlags.Position | PoseDataFlags.Rotation);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000249A File Offset: 0x0000069A
		protected virtual void Awake()
		{
			this.CacheLocalPosition();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024A2 File Offset: 0x000006A2
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024A4 File Offset: 0x000006A4
		protected virtual void OnEnable()
		{
			Application.onBeforeRender += this.OnBeforeRender;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024B8 File Offset: 0x000006B8
		protected virtual void OnDisable()
		{
			this.ResetToCachedLocalPosition();
			Application.onBeforeRender -= this.OnBeforeRender;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024D2 File Offset: 0x000006D2
		protected virtual void FixedUpdate()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.Update || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024EB File Offset: 0x000006EB
		protected virtual void Update()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.Update || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002504 File Offset: 0x00000704
		[BeforeRenderOrder(-30000)]
		protected virtual void OnBeforeRender()
		{
			if (this.m_UpdateType == TrackedPoseDriver.UpdateType.BeforeRender || this.m_UpdateType == TrackedPoseDriver.UpdateType.UpdateAndBeforeRender)
			{
				this.PerformUpdate();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002520 File Offset: 0x00000720
		protected virtual void SetLocalTransform(Vector3 newPosition, Quaternion newRotation, PoseDataFlags poseFlags)
		{
			if ((this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationAndPosition || this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationOnly) && (poseFlags & PoseDataFlags.Rotation) > PoseDataFlags.NoData)
			{
				base.transform.localRotation = newRotation;
			}
			if ((this.m_TrackingType == TrackedPoseDriver.TrackingType.RotationAndPosition || this.m_TrackingType == TrackedPoseDriver.TrackingType.PositionOnly) && (poseFlags & PoseDataFlags.Position) > PoseDataFlags.NoData)
			{
				base.transform.localPosition = newPosition;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002573 File Offset: 0x00000773
		protected Pose TransformPoseByOriginIfNeeded(Pose pose)
		{
			if (this.m_UseRelativeTransform)
			{
				return pose.GetTransformedBy(this.m_OriginPose);
			}
			return pose;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000258C File Offset: 0x0000078C
		private bool HasStereoCamera()
		{
			Camera component = base.GetComponent<Camera>();
			return component != null && component.stereoEnabled;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025B4 File Offset: 0x000007B4
		protected virtual void PerformUpdate()
		{
			if (!base.enabled)
			{
				return;
			}
			Pose pose;
			PoseDataFlags poseData = this.GetPoseData(this.m_Device, this.m_PoseSource, out pose);
			if (poseData != PoseDataFlags.NoData)
			{
				Pose pose2 = this.TransformPoseByOriginIfNeeded(pose);
				this.SetLocalTransform(pose2.position, pose2.rotation, poseData);
			}
		}

		// Token: 0x04000007 RID: 7
		[SerializeField]
		private TrackedPoseDriver.DeviceType m_Device;

		// Token: 0x04000008 RID: 8
		[SerializeField]
		private TrackedPoseDriver.TrackedPose m_PoseSource = TrackedPoseDriver.TrackedPose.Center;

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private BasePoseProvider m_PoseProviderComponent;

		// Token: 0x0400000A RID: 10
		[SerializeField]
		private TrackedPoseDriver.TrackingType m_TrackingType;

		// Token: 0x0400000B RID: 11
		[SerializeField]
		private TrackedPoseDriver.UpdateType m_UpdateType;

		// Token: 0x0400000C RID: 12
		[SerializeField]
		private bool m_UseRelativeTransform;

		// Token: 0x0400000D RID: 13
		protected Pose m_OriginPose;

		// Token: 0x0200000B RID: 11
		public enum DeviceType
		{
			// Token: 0x04000018 RID: 24
			GenericXRDevice,
			// Token: 0x04000019 RID: 25
			GenericXRController,
			// Token: 0x0400001A RID: 26
			GenericXRRemote
		}

		// Token: 0x0200000C RID: 12
		public enum TrackedPose
		{
			// Token: 0x0400001C RID: 28
			LeftEye,
			// Token: 0x0400001D RID: 29
			RightEye,
			// Token: 0x0400001E RID: 30
			Center,
			// Token: 0x0400001F RID: 31
			Head,
			// Token: 0x04000020 RID: 32
			LeftPose,
			// Token: 0x04000021 RID: 33
			RightPose,
			// Token: 0x04000022 RID: 34
			ColorCamera,
			// Token: 0x04000023 RID: 35
			DepthCameraDeprecated,
			// Token: 0x04000024 RID: 36
			FisheyeCameraDeprected,
			// Token: 0x04000025 RID: 37
			DeviceDeprecated,
			// Token: 0x04000026 RID: 38
			RemotePose
		}

		// Token: 0x0200000D RID: 13
		public enum TrackingType
		{
			// Token: 0x04000028 RID: 40
			RotationAndPosition,
			// Token: 0x04000029 RID: 41
			RotationOnly,
			// Token: 0x0400002A RID: 42
			PositionOnly
		}

		// Token: 0x0200000E RID: 14
		public enum UpdateType
		{
			// Token: 0x0400002C RID: 44
			UpdateAndBeforeRender,
			// Token: 0x0400002D RID: 45
			Update,
			// Token: 0x0400002E RID: 46
			BeforeRender
		}
	}
}
