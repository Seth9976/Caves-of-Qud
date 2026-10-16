using System;
using System.Collections.Generic;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;

namespace UnityEngine.XR.LegacyInputHelpers
{
	// Token: 0x02000005 RID: 5
	public class ArmModel : BasePoseProvider
	{
		// Token: 0x0600001B RID: 27 RVA: 0x000024E6 File Offset: 0x000006E6
		public override PoseDataFlags GetPoseFromProvider(out Pose output)
		{
			if (this.OnControllerInputUpdated())
			{
				output = this.finalPose;
				return PoseDataFlags.Position | PoseDataFlags.Rotation;
			}
			output = Pose.identity;
			return PoseDataFlags.NoData;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000250A File Offset: 0x0000070A
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002512 File Offset: 0x00000712
		public Pose finalPose
		{
			get
			{
				return this.m_FinalPose;
			}
			set
			{
				this.m_FinalPose = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000251B File Offset: 0x0000071B
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002523 File Offset: 0x00000723
		public XRNode poseSource
		{
			get
			{
				return this.m_PoseSource;
			}
			set
			{
				this.m_PoseSource = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000252C File Offset: 0x0000072C
		// (set) Token: 0x06000021 RID: 33 RVA: 0x00002534 File Offset: 0x00000734
		public XRNode headGameObject
		{
			get
			{
				return this.m_HeadPoseSource;
			}
			set
			{
				this.m_HeadPoseSource = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000022 RID: 34 RVA: 0x0000253D File Offset: 0x0000073D
		// (set) Token: 0x06000023 RID: 35 RVA: 0x00002545 File Offset: 0x00000745
		public Vector3 elbowRestPosition
		{
			get
			{
				return this.m_ElbowRestPosition;
			}
			set
			{
				this.m_ElbowRestPosition = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000254E File Offset: 0x0000074E
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002556 File Offset: 0x00000756
		public Vector3 wristRestPosition
		{
			get
			{
				return this.m_WristRestPosition;
			}
			set
			{
				this.m_WristRestPosition = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000255F File Offset: 0x0000075F
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002567 File Offset: 0x00000767
		public Vector3 controllerRestPosition
		{
			get
			{
				return this.m_ControllerRestPosition;
			}
			set
			{
				this.m_ControllerRestPosition = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002570 File Offset: 0x00000770
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002578 File Offset: 0x00000778
		public Vector3 armExtensionOffset
		{
			get
			{
				return this.m_ArmExtensionOffset;
			}
			set
			{
				this.m_ArmExtensionOffset = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002581 File Offset: 0x00000781
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002589 File Offset: 0x00000789
		public float elbowBendRatio
		{
			get
			{
				return this.m_ElbowBendRatio;
			}
			set
			{
				this.m_ElbowBendRatio = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002592 File Offset: 0x00000792
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000259A File Offset: 0x0000079A
		public bool isLockedToNeck
		{
			get
			{
				return this.m_IsLockedToNeck;
			}
			set
			{
				this.m_IsLockedToNeck = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025A3 File Offset: 0x000007A3
		public Vector3 neckPosition
		{
			get
			{
				return this.m_NeckPosition;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000025AB File Offset: 0x000007AB
		public Vector3 shoulderPosition
		{
			get
			{
				return this.m_NeckPosition + this.m_TorsoRotation * Vector3.Scale(ArmModel.SHOULDER_POSITION, this.m_HandedMultiplier);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000025D3 File Offset: 0x000007D3
		public Quaternion shoulderRotation
		{
			get
			{
				return this.m_TorsoRotation;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025DB File Offset: 0x000007DB
		public Vector3 elbowPosition
		{
			get
			{
				return this.m_ElbowPosition;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025E3 File Offset: 0x000007E3
		public Quaternion elbowRotation
		{
			get
			{
				return this.m_ElbowRotation;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000025EB File Offset: 0x000007EB
		public Vector3 wristPosition
		{
			get
			{
				return this.m_WristPosition;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025F3 File Offset: 0x000007F3
		public Quaternion wristRotation
		{
			get
			{
				return this.m_WristRotation;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000025FB File Offset: 0x000007FB
		public Vector3 controllerPosition
		{
			get
			{
				return this.m_ControllerPosition;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002603 File Offset: 0x00000803
		public Quaternion controllerRotation
		{
			get
			{
				return this.m_ControllerRotation;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000260B File Offset: 0x0000080B
		protected virtual void OnEnable()
		{
			this.UpdateTorsoDirection(true);
			this.OnControllerInputUpdated();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000261C File Offset: 0x0000081C
		protected virtual void OnDisable()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000261E File Offset: 0x0000081E
		public virtual bool OnControllerInputUpdated()
		{
			this.UpdateHandedness();
			return this.UpdateTorsoDirection(false) && this.UpdateNeckPosition() && this.ApplyArmModel();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002644 File Offset: 0x00000844
		protected virtual void UpdateHandedness()
		{
			this.m_HandedMultiplier.Set(0f, 1f, 1f);
			if (this.m_PoseSource == XRNode.RightHand || this.m_PoseSource == XRNode.TrackingReference)
			{
				this.m_HandedMultiplier.x = 1f;
				return;
			}
			if (this.m_PoseSource == XRNode.LeftHand)
			{
				this.m_HandedMultiplier.x = -1f;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026A8 File Offset: 0x000008A8
		protected virtual bool UpdateTorsoDirection(bool forceImmediate)
		{
			Vector3 vector = default(Vector3);
			if (this.TryGetForwardVector(this.m_HeadPoseSource, out vector))
			{
				vector.y = 0f;
				vector.Normalize();
				Vector3 vector2;
				if (forceImmediate)
				{
					this.m_TorsoDirection = vector;
				}
				else if (this.TryGetAngularAcceleration(this.poseSource, out vector2))
				{
					float num = Mathf.Clamp((vector2.magnitude - 0.2f) / 45f, 0f, 0.1f);
					this.m_TorsoDirection = Vector3.Slerp(this.m_TorsoDirection, vector, num);
				}
				this.m_TorsoRotation = Quaternion.FromToRotation(Vector3.forward, this.m_TorsoDirection);
				return true;
			}
			return false;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000274B File Offset: 0x0000094B
		protected virtual bool UpdateNeckPosition()
		{
			if (this.m_IsLockedToNeck && this.TryGetPosition(this.m_HeadPoseSource, out this.m_NeckPosition))
			{
				return this.ApplyInverseNeckModel(this.m_NeckPosition, out this.m_NeckPosition);
			}
			this.m_NeckPosition = Vector3.zero;
			return true;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002788 File Offset: 0x00000988
		protected virtual bool ApplyArmModel()
		{
			this.SetUntransformedJointPositions();
			Quaternion quaternion;
			Quaternion quaternion2;
			float num;
			if (this.GetControllerRotation(out quaternion, out quaternion2, out num))
			{
				float num2 = this.CalculateExtensionRatio(num);
				this.ApplyExtensionOffset(num2);
				Quaternion quaternion3 = this.CalculateLerpRotation(quaternion2, num2);
				this.CalculateFinalJointRotations(quaternion, quaternion2, quaternion3);
				this.ApplyRotationToJoints();
				this.m_FinalPose.position = this.m_ControllerPosition;
				this.m_FinalPose.rotation = this.m_ControllerRotation;
				return true;
			}
			return false;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027F8 File Offset: 0x000009F8
		protected virtual void SetUntransformedJointPositions()
		{
			this.m_ElbowPosition = Vector3.Scale(this.m_ElbowRestPosition, this.m_HandedMultiplier);
			this.m_WristPosition = Vector3.Scale(this.m_WristRestPosition, this.m_HandedMultiplier);
			this.m_ControllerPosition = Vector3.Scale(this.m_ControllerRestPosition, this.m_HandedMultiplier);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000284A File Offset: 0x00000A4A
		protected virtual float CalculateExtensionRatio(float xAngle)
		{
			return Mathf.Clamp((xAngle - 7f) / 53f, 0f, 1f);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002868 File Offset: 0x00000A68
		protected virtual void ApplyExtensionOffset(float extensionRatio)
		{
			Vector3 vector = Vector3.Scale(this.m_ArmExtensionOffset, this.m_HandedMultiplier);
			this.m_ElbowPosition += vector * extensionRatio;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028A0 File Offset: 0x00000AA0
		protected virtual Quaternion CalculateLerpRotation(Quaternion xyRotation, float extensionRatio)
		{
			float num = Quaternion.Angle(xyRotation, Quaternion.identity);
			float num2 = 1f - Mathf.Pow(num / 180f, 6f);
			float num3 = 1f - this.m_ElbowBendRatio + this.m_ElbowBendRatio * extensionRatio * 0.4f;
			num3 *= num2;
			return Quaternion.Lerp(Quaternion.identity, xyRotation, num3);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002900 File Offset: 0x00000B00
		protected virtual void CalculateFinalJointRotations(Quaternion controllerOrientation, Quaternion xyRotation, Quaternion lerpRotation)
		{
			this.m_ElbowRotation = this.m_TorsoRotation * Quaternion.Inverse(lerpRotation) * xyRotation;
			this.m_WristRotation = this.m_ElbowRotation * lerpRotation;
			this.m_ControllerRotation = this.m_TorsoRotation * controllerOrientation;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002950 File Offset: 0x00000B50
		protected virtual void ApplyRotationToJoints()
		{
			this.m_ElbowPosition = this.m_NeckPosition + this.m_TorsoRotation * this.m_ElbowPosition;
			this.m_WristPosition = this.m_ElbowPosition + this.m_ElbowRotation * this.m_WristPosition;
			this.m_ControllerPosition = this.m_WristPosition + this.m_WristRotation * this.m_ControllerPosition;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029C4 File Offset: 0x00000BC4
		protected virtual bool ApplyInverseNeckModel(Vector3 headPosition, out Vector3 calculatedPosition)
		{
			Quaternion quaternion = default(Quaternion);
			if (this.TryGetRotation(this.m_HeadPoseSource, out quaternion))
			{
				Vector3 vector = quaternion * ArmModel.NECK_OFFSET - ArmModel.NECK_OFFSET.y * Vector3.up;
				headPosition -= vector;
				calculatedPosition = headPosition;
				return true;
			}
			calculatedPosition = Vector3.zero;
			return false;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A2C File Offset: 0x00000C2C
		protected bool TryGetForwardVector(XRNode node, out Vector3 forward)
		{
			Pose pose = default(Pose);
			if (this.TryGetRotation(node, out pose.rotation) && this.TryGetPosition(node, out pose.position))
			{
				forward = pose.forward;
				return true;
			}
			forward = Vector3.zero;
			return false;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A7C File Offset: 0x00000C7C
		protected bool TryGetRotation(XRNode node, out Quaternion rotation)
		{
			InputTracking.GetNodeStates(this.xrNodeStateListOrientation);
			int count = this.xrNodeStateListOrientation.Count;
			for (int i = 0; i < count; i++)
			{
				XRNodeState xrnodeState = this.xrNodeStateListOrientation[i];
				if (xrnodeState.nodeType == node && xrnodeState.TryGetRotation(out rotation))
				{
					return true;
				}
			}
			rotation = Quaternion.identity;
			return false;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002ADC File Offset: 0x00000CDC
		protected bool TryGetPosition(XRNode node, out Vector3 position)
		{
			InputTracking.GetNodeStates(this.xrNodeStateListPosition);
			int count = this.xrNodeStateListPosition.Count;
			for (int i = 0; i < count; i++)
			{
				XRNodeState xrnodeState = this.xrNodeStateListPosition[i];
				if (xrnodeState.nodeType == node && xrnodeState.TryGetPosition(out position))
				{
					return true;
				}
			}
			position = Vector3.zero;
			return false;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B3C File Offset: 0x00000D3C
		protected bool TryGetAngularAcceleration(XRNode node, out Vector3 angularAccel)
		{
			InputTracking.GetNodeStates(this.xrNodeStateListAngularAcceleration);
			int count = this.xrNodeStateListAngularAcceleration.Count;
			for (int i = 0; i < count; i++)
			{
				XRNodeState xrnodeState = this.xrNodeStateListAngularAcceleration[i];
				if (xrnodeState.nodeType == node && xrnodeState.TryGetAngularAcceleration(out angularAccel))
				{
					return true;
				}
			}
			angularAccel = Vector3.zero;
			return false;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B9C File Offset: 0x00000D9C
		protected bool TryGetAngularVelocity(XRNode node, out Vector3 angVel)
		{
			InputTracking.GetNodeStates(this.xrNodeStateListAngularVelocity);
			int count = this.xrNodeStateListAngularVelocity.Count;
			for (int i = 0; i < count; i++)
			{
				XRNodeState xrnodeState = this.xrNodeStateListAngularVelocity[i];
				if (xrnodeState.nodeType == node && xrnodeState.TryGetAngularVelocity(out angVel))
				{
					return true;
				}
			}
			angVel = Vector3.zero;
			return false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BFC File Offset: 0x00000DFC
		protected bool GetControllerRotation(out Quaternion rotation, out Quaternion xyRotation, out float xAngle)
		{
			if (this.TryGetRotation(this.poseSource, out rotation))
			{
				rotation = Quaternion.Inverse(this.m_TorsoRotation) * rotation;
				Vector3 vector = rotation * Vector3.forward;
				xAngle = 90f - Vector3.Angle(vector, Vector3.up);
				xyRotation = Quaternion.FromToRotation(Vector3.forward, vector);
				return true;
			}
			rotation = Quaternion.identity;
			xyRotation = Quaternion.identity;
			xAngle = 0f;
			return false;
		}

		// Token: 0x0400000E RID: 14
		private Pose m_FinalPose;

		// Token: 0x0400000F RID: 15
		[SerializeField]
		private XRNode m_PoseSource = XRNode.LeftHand;

		// Token: 0x04000010 RID: 16
		[SerializeField]
		private XRNode m_HeadPoseSource = XRNode.CenterEye;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		private Vector3 m_ElbowRestPosition = ArmModel.DEFAULT_ELBOW_REST_POSITION;

		// Token: 0x04000012 RID: 18
		[SerializeField]
		private Vector3 m_WristRestPosition = ArmModel.DEFAULT_WRIST_REST_POSITION;

		// Token: 0x04000013 RID: 19
		[SerializeField]
		private Vector3 m_ControllerRestPosition = ArmModel.DEFAULT_CONTROLLER_REST_POSITION;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		private Vector3 m_ArmExtensionOffset = ArmModel.DEFAULT_ARM_EXTENSION_OFFSET;

		// Token: 0x04000015 RID: 21
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ElbowBendRatio = 0.6f;

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private bool m_IsLockedToNeck = true;

		// Token: 0x04000017 RID: 23
		protected Vector3 m_NeckPosition;

		// Token: 0x04000018 RID: 24
		protected Vector3 m_ElbowPosition;

		// Token: 0x04000019 RID: 25
		protected Quaternion m_ElbowRotation;

		// Token: 0x0400001A RID: 26
		protected Vector3 m_WristPosition;

		// Token: 0x0400001B RID: 27
		protected Quaternion m_WristRotation;

		// Token: 0x0400001C RID: 28
		protected Vector3 m_ControllerPosition;

		// Token: 0x0400001D RID: 29
		protected Quaternion m_ControllerRotation;

		// Token: 0x0400001E RID: 30
		protected Vector3 m_HandedMultiplier;

		// Token: 0x0400001F RID: 31
		protected Vector3 m_TorsoDirection;

		// Token: 0x04000020 RID: 32
		protected Quaternion m_TorsoRotation;

		// Token: 0x04000021 RID: 33
		protected static readonly Vector3 DEFAULT_ELBOW_REST_POSITION = new Vector3(0.195f, -0.5f, 0.005f);

		// Token: 0x04000022 RID: 34
		protected static readonly Vector3 DEFAULT_WRIST_REST_POSITION = new Vector3(0f, 0f, 0.25f);

		// Token: 0x04000023 RID: 35
		protected static readonly Vector3 DEFAULT_CONTROLLER_REST_POSITION = new Vector3(0f, 0f, 0.05f);

		// Token: 0x04000024 RID: 36
		protected static readonly Vector3 DEFAULT_ARM_EXTENSION_OFFSET = new Vector3(-0.13f, 0.14f, 0.08f);

		// Token: 0x04000025 RID: 37
		protected const float DEFAULT_ELBOW_BEND_RATIO = 0.6f;

		// Token: 0x04000026 RID: 38
		protected const float EXTENSION_WEIGHT = 0.4f;

		// Token: 0x04000027 RID: 39
		protected static readonly Vector3 SHOULDER_POSITION = new Vector3(0.17f, -0.2f, -0.03f);

		// Token: 0x04000028 RID: 40
		protected static readonly Vector3 NECK_OFFSET = new Vector3(0f, 0.075f, 0.08f);

		// Token: 0x04000029 RID: 41
		protected const float MIN_EXTENSION_ANGLE = 7f;

		// Token: 0x0400002A RID: 42
		protected const float MAX_EXTENSION_ANGLE = 60f;

		// Token: 0x0400002B RID: 43
		private List<XRNodeState> xrNodeStateListOrientation = new List<XRNodeState>();

		// Token: 0x0400002C RID: 44
		private List<XRNodeState> xrNodeStateListPosition = new List<XRNodeState>();

		// Token: 0x0400002D RID: 45
		private List<XRNodeState> xrNodeStateListAngularAcceleration = new List<XRNodeState>();

		// Token: 0x0400002E RID: 46
		private List<XRNodeState> xrNodeStateListAngularVelocity = new List<XRNodeState>();
	}
}
