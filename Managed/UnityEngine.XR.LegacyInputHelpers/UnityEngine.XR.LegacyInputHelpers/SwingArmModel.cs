using System;

namespace UnityEngine.XR.LegacyInputHelpers
{
	// Token: 0x02000006 RID: 6
	public class SwingArmModel : ArmModel
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002DBB File Offset: 0x00000FBB
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002DC3 File Offset: 0x00000FC3
		public float shoulderRotationRatio
		{
			get
			{
				return this.m_ShoulderRotationRatio;
			}
			set
			{
				this.m_ShoulderRotationRatio = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public float elbowRotationRatio
		{
			get
			{
				return this.m_ElbowRotationRatio;
			}
			set
			{
				this.m_ElbowRotationRatio = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002DDD File Offset: 0x00000FDD
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002DE5 File Offset: 0x00000FE5
		public float wristRotationRatio
		{
			get
			{
				return this.m_WristRotationRatio;
			}
			set
			{
				this.m_WristRotationRatio = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002DEE File Offset: 0x00000FEE
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002DFB File Offset: 0x00000FFB
		public float minJointShiftAngle
		{
			get
			{
				return this.m_JointShiftAngle.x;
			}
			set
			{
				this.m_JointShiftAngle.x = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002E09 File Offset: 0x00001009
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002E16 File Offset: 0x00001016
		public float maxJointShiftAngle
		{
			get
			{
				return this.m_JointShiftAngle.y;
			}
			set
			{
				this.m_JointShiftAngle.y = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002E24 File Offset: 0x00001024
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002E2C File Offset: 0x0000102C
		public float jointShiftExponent
		{
			get
			{
				return this.m_JointShiftExponent;
			}
			set
			{
				this.m_JointShiftExponent = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E35 File Offset: 0x00001035
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002E3D File Offset: 0x0000103D
		public float shiftedShoulderRotationRatio
		{
			get
			{
				return this.m_ShiftedShoulderRotationRatio;
			}
			set
			{
				this.m_ShiftedShoulderRotationRatio = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E46 File Offset: 0x00001046
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002E4E File Offset: 0x0000104E
		public float shiftedElbowRotationRatio
		{
			get
			{
				return this.m_ShiftedElbowRotationRatio;
			}
			set
			{
				this.m_ShiftedElbowRotationRatio = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002E57 File Offset: 0x00001057
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002E5F File Offset: 0x0000105F
		public float shiftedWristRotationRatio
		{
			get
			{
				return this.m_ShiftedWristRotationRatio;
			}
			set
			{
				this.m_ShiftedWristRotationRatio = value;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E68 File Offset: 0x00001068
		protected override void CalculateFinalJointRotations(Quaternion controllerOrientation, Quaternion xyRotation, Quaternion lerpRotation)
		{
			float num = Quaternion.Angle(xyRotation, Quaternion.identity);
			float num2 = this.maxJointShiftAngle - this.minJointShiftAngle;
			float num3 = Mathf.Pow(Mathf.Clamp01((num - this.minJointShiftAngle) / num2), this.m_JointShiftExponent);
			float num4 = Mathf.Lerp(this.m_ShoulderRotationRatio, this.m_ShiftedShoulderRotationRatio, num3);
			float num5 = Mathf.Lerp(this.m_ElbowRotationRatio, this.m_ShiftedElbowRotationRatio, num3);
			float num6 = Mathf.Lerp(this.m_WristRotationRatio, this.m_ShiftedWristRotationRatio, num3);
			Quaternion quaternion = Quaternion.Lerp(Quaternion.identity, xyRotation, num4);
			Quaternion quaternion2 = Quaternion.Lerp(Quaternion.identity, xyRotation, num5);
			Quaternion quaternion3 = Quaternion.Lerp(Quaternion.identity, xyRotation, num6);
			Quaternion quaternion4 = this.m_TorsoRotation * quaternion;
			this.m_ElbowRotation = quaternion4 * quaternion2;
			this.m_WristRotation = base.elbowRotation * quaternion3;
			this.m_ControllerRotation = this.m_TorsoRotation * controllerOrientation;
			this.m_TorsoRotation = quaternion4;
		}

		// Token: 0x0400002F RID: 47
		[Tooltip("Portion of controller rotation applied to the shoulder joint.")]
		[SerializeField]
		[Range(0f, 1f)]
		private float m_ShoulderRotationRatio = 0.5f;

		// Token: 0x04000030 RID: 48
		[Tooltip("Portion of controller rotation applied to the elbow joint.")]
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ElbowRotationRatio = 0.3f;

		// Token: 0x04000031 RID: 49
		[Tooltip("Portion of controller rotation applied to the wrist joint.")]
		[Range(0f, 1f)]
		[SerializeField]
		private float m_WristRotationRatio = 0.2f;

		// Token: 0x04000032 RID: 50
		[SerializeField]
		private Vector2 m_JointShiftAngle = new Vector2(160f, 180f);

		// Token: 0x04000033 RID: 51
		[Tooltip("Exponent applied to the joint shift ratio to control the curve of the shift.")]
		[Range(1f, 20f)]
		[SerializeField]
		private float m_JointShiftExponent = 6f;

		// Token: 0x04000034 RID: 52
		[Tooltip("Portion of controller rotation applied to the shoulder joint when the controller is backwards.")]
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShiftedShoulderRotationRatio = 0.1f;

		// Token: 0x04000035 RID: 53
		[Tooltip("Portion of controller rotation applied to the elbow joint when the controller is backwards.")]
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShiftedElbowRotationRatio = 0.4f;

		// Token: 0x04000036 RID: 54
		[Tooltip("Portion of controller rotation applied to the wrist joint when the controller is backwards.")]
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShiftedWristRotationRatio = 0.5f;
	}
}
