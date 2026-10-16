using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x02000053 RID: 83
	[NativeHeader("Modules/Animation/Director/AnimationHumanStream.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationHumanStream.bindings.h")]
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[RequiredByNativeCode]
	public struct AnimationHumanStream
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x000087F4 File Offset: 0x000069F4
		public bool isValid
		{
			get
			{
				return this.stream != IntPtr.Zero;
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00008818 File Offset: 0x00006A18
		private void ThrowIfInvalid()
		{
			bool flag = !this.isValid;
			if (flag)
			{
				throw new InvalidOperationException("The AnimationHumanStream is invalid.");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00008840 File Offset: 0x00006A40
		public float humanScale
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetHumanScale();
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00008860 File Offset: 0x00006A60
		public float leftFootHeight
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetFootHeight(true);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00008880 File Offset: 0x00006A80
		public float rightFootHeight
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetFootHeight(false);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000088A0 File Offset: 0x00006AA0
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x000088BF File Offset: 0x00006ABF
		public Vector3 bodyLocalPosition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyLocalPosition();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyLocalPosition(value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000088D4 File Offset: 0x00006AD4
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x000088F3 File Offset: 0x00006AF3
		public Quaternion bodyLocalRotation
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyLocalRotation();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyLocalRotation(value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00008908 File Offset: 0x00006B08
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x00008927 File Offset: 0x00006B27
		public Vector3 bodyPosition
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyPosition();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyPosition(value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000893C File Offset: 0x00006B3C
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000895B File Offset: 0x00006B5B
		public Quaternion bodyRotation
		{
			get
			{
				this.ThrowIfInvalid();
				return this.InternalGetBodyRotation();
			}
			set
			{
				this.ThrowIfInvalid();
				this.InternalSetBodyRotation(value);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00008970 File Offset: 0x00006B70
		public float GetMuscle(MuscleHandle muscle)
		{
			this.ThrowIfInvalid();
			return this.InternalGetMuscle(muscle);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00008990 File Offset: 0x00006B90
		public void SetMuscle(MuscleHandle muscle, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetMuscle(muscle, value);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000089A4 File Offset: 0x00006BA4
		public Vector3 leftFootVelocity
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetLeftFootVelocity();
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000089C4 File Offset: 0x00006BC4
		public Vector3 rightFootVelocity
		{
			get
			{
				this.ThrowIfInvalid();
				return this.GetRightFootVelocity();
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000089E3 File Offset: 0x00006BE3
		public void ResetToStancePose()
		{
			this.ThrowIfInvalid();
			this.InternalResetToStancePose();
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000089F4 File Offset: 0x00006BF4
		public Vector3 GetGoalPositionFromPose(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalPositionFromPose(index);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00008A14 File Offset: 0x00006C14
		public Quaternion GetGoalRotationFromPose(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalRotationFromPose(index);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00008A34 File Offset: 0x00006C34
		public Vector3 GetGoalLocalPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalLocalPosition(index);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00008A54 File Offset: 0x00006C54
		public void SetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalLocalPosition(index, pos);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00008A68 File Offset: 0x00006C68
		public Quaternion GetGoalLocalRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalLocalRotation(index);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00008A88 File Offset: 0x00006C88
		public void SetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalLocalRotation(index, rot);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00008A9C File Offset: 0x00006C9C
		public Vector3 GetGoalPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalPosition(index);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x00008ABC File Offset: 0x00006CBC
		public void SetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalPosition(index, pos);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00008AD0 File Offset: 0x00006CD0
		public Quaternion GetGoalRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalRotation(index);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public void SetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalRotation(index, rot);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00008B03 File Offset: 0x00006D03
		public void SetGoalWeightPosition(AvatarIKGoal index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalWeightPosition(index, value);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00008B16 File Offset: 0x00006D16
		public void SetGoalWeightRotation(AvatarIKGoal index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetGoalWeightRotation(index, value);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00008B2C File Offset: 0x00006D2C
		public float GetGoalWeightPosition(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalWeightPosition(index);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00008B4C File Offset: 0x00006D4C
		public float GetGoalWeightRotation(AvatarIKGoal index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetGoalWeightRotation(index);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00008B6C File Offset: 0x00006D6C
		public Vector3 GetHintPosition(AvatarIKHint index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetHintPosition(index);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00008B8C File Offset: 0x00006D8C
		public void SetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			this.ThrowIfInvalid();
			this.InternalSetHintPosition(index, pos);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00008B9F File Offset: 0x00006D9F
		public void SetHintWeightPosition(AvatarIKHint index, float value)
		{
			this.ThrowIfInvalid();
			this.InternalSetHintWeightPosition(index, value);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00008BB4 File Offset: 0x00006DB4
		public float GetHintWeightPosition(AvatarIKHint index)
		{
			this.ThrowIfInvalid();
			return this.InternalGetHintWeightPosition(index);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtPosition(lookAtPosition);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00008BE6 File Offset: 0x00006DE6
		public void SetLookAtClampWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtClampWeight(weight);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00008BF8 File Offset: 0x00006DF8
		public void SetLookAtBodyWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtBodyWeight(weight);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00008C0A File Offset: 0x00006E0A
		public void SetLookAtHeadWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtHeadWeight(weight);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00008C1C File Offset: 0x00006E1C
		public void SetLookAtEyesWeight(float weight)
		{
			this.ThrowIfInvalid();
			this.InternalSetLookAtEyesWeight(weight);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00008C2E File Offset: 0x00006E2E
		public void SolveIK()
		{
			this.ThrowIfInvalid();
			this.InternalSolveIK();
		}

		// Token: 0x06000451 RID: 1105
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetHumanScale();

		// Token: 0x06000452 RID: 1106
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetFootHeight(bool left);

		// Token: 0x06000453 RID: 1107
		[NativeMethod(Name = "ResetToStancePose", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalResetToStancePose();

		// Token: 0x06000454 RID: 1108 RVA: 0x00008C40 File Offset: 0x00006E40
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPositionFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPositionFromPose(AvatarIKGoal index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetGoalPositionFromPose_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00008C58 File Offset: 0x00006E58
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotationFromPose", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotationFromPose(AvatarIKGoal index)
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetGoalRotationFromPose_Injected(ref this, index, out quaternion);
			return quaternion;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00008C70 File Offset: 0x00006E70
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyLocalPosition()
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetBodyLocalPosition_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00008C88 File Offset: 0x00006E88
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalPosition(Vector3 value)
		{
			AnimationHumanStream.InternalSetBodyLocalPosition_Injected(ref this, ref value);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00008CA0 File Offset: 0x00006EA0
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyLocalRotation()
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetBodyLocalRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00008CB8 File Offset: 0x00006EB8
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyLocalRotation(Quaternion value)
		{
			AnimationHumanStream.InternalSetBodyLocalRotation_Injected(ref this, ref value);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00008CD0 File Offset: 0x00006ED0
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetBodyPosition()
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetBodyPosition_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00008CE8 File Offset: 0x00006EE8
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyPosition(Vector3 value)
		{
			AnimationHumanStream.InternalSetBodyPosition_Injected(ref this, ref value);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00008D00 File Offset: 0x00006F00
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetBodyRotation()
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetBodyRotation_Injected(ref this, out quaternion);
			return quaternion;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00008D18 File Offset: 0x00006F18
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetBodyRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetBodyRotation(Quaternion value)
		{
			AnimationHumanStream.InternalSetBodyRotation_Injected(ref this, ref value);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00008D30 File Offset: 0x00006F30
		[NativeMethod(Name = "GetMuscle", IsThreadSafe = true)]
		private float InternalGetMuscle(MuscleHandle muscle)
		{
			return AnimationHumanStream.InternalGetMuscle_Injected(ref this, ref muscle);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00008D48 File Offset: 0x00006F48
		[NativeMethod(Name = "SetMuscle", IsThreadSafe = true)]
		private void InternalSetMuscle(MuscleHandle muscle, float value)
		{
			AnimationHumanStream.InternalSetMuscle_Injected(ref this, ref muscle, value);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00008D60 File Offset: 0x00006F60
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetLeftFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetLeftFootVelocity()
		{
			Vector3 vector;
			AnimationHumanStream.GetLeftFootVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00008D78 File Offset: 0x00006F78
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetRightFootVelocity", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 GetRightFootVelocity()
		{
			Vector3 vector;
			AnimationHumanStream.GetRightFootVelocity_Injected(ref this, out vector);
			return vector;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00008D90 File Offset: 0x00006F90
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalLocalPosition(AvatarIKGoal index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetGoalLocalPosition_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00008DA8 File Offset: 0x00006FA8
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalPosition(AvatarIKGoal index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetGoalLocalPosition_Injected(ref this, index, ref pos);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00008DC0 File Offset: 0x00006FC0
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalLocalRotation(AvatarIKGoal index)
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetGoalLocalRotation_Injected(ref this, index, out quaternion);
			return quaternion;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00008DD8 File Offset: 0x00006FD8
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalLocalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalLocalRotation(AvatarIKGoal index, Quaternion rot)
		{
			AnimationHumanStream.InternalSetGoalLocalRotation_Injected(ref this, index, ref rot);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00008DF0 File Offset: 0x00006FF0
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetGoalPosition(AvatarIKGoal index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetGoalPosition_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00008E08 File Offset: 0x00007008
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalPosition(AvatarIKGoal index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetGoalPosition_Injected(ref this, index, ref pos);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00008E20 File Offset: 0x00007020
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Quaternion InternalGetGoalRotation(AvatarIKGoal index)
		{
			Quaternion quaternion;
			AnimationHumanStream.InternalGetGoalRotation_Injected(ref this, index, out quaternion);
			return quaternion;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00008E38 File Offset: 0x00007038
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetGoalRotation", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetGoalRotation(AvatarIKGoal index, Quaternion rot)
		{
			AnimationHumanStream.InternalSetGoalRotation_Injected(ref this, index, ref rot);
		}

		// Token: 0x0600046A RID: 1130
		[NativeMethod(Name = "SetGoalWeightPosition", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetGoalWeightPosition(AvatarIKGoal index, float value);

		// Token: 0x0600046B RID: 1131
		[NativeMethod(Name = "SetGoalWeightRotation", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetGoalWeightRotation(AvatarIKGoal index, float value);

		// Token: 0x0600046C RID: 1132
		[NativeMethod(Name = "GetGoalWeightPosition", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float InternalGetGoalWeightPosition(AvatarIKGoal index);

		// Token: 0x0600046D RID: 1133
		[NativeMethod(Name = "GetGoalWeightRotation", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float InternalGetGoalWeightRotation(AvatarIKGoal index);

		// Token: 0x0600046E RID: 1134 RVA: 0x00008E50 File Offset: 0x00007050
		[NativeMethod(Name = "AnimationHumanStreamBindings::GetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private Vector3 InternalGetHintPosition(AvatarIKHint index)
		{
			Vector3 vector;
			AnimationHumanStream.InternalGetHintPosition_Injected(ref this, index, out vector);
			return vector;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00008E68 File Offset: 0x00007068
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetHintPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetHintPosition(AvatarIKHint index, Vector3 pos)
		{
			AnimationHumanStream.InternalSetHintPosition_Injected(ref this, index, ref pos);
		}

		// Token: 0x06000470 RID: 1136
		[NativeMethod(Name = "SetHintWeightPosition", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetHintWeightPosition(AvatarIKHint index, float value);

		// Token: 0x06000471 RID: 1137
		[NativeMethod(Name = "GetHintWeightPosition", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float InternalGetHintWeightPosition(AvatarIKHint index);

		// Token: 0x06000472 RID: 1138 RVA: 0x00008E80 File Offset: 0x00007080
		[NativeMethod(Name = "AnimationHumanStreamBindings::SetLookAtPosition", IsFreeFunction = true, IsThreadSafe = true, HasExplicitThis = true)]
		private void InternalSetLookAtPosition(Vector3 lookAtPosition)
		{
			AnimationHumanStream.InternalSetLookAtPosition_Injected(ref this, ref lookAtPosition);
		}

		// Token: 0x06000473 RID: 1139
		[NativeMethod(Name = "SetLookAtClampWeight", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetLookAtClampWeight(float weight);

		// Token: 0x06000474 RID: 1140
		[NativeMethod(Name = "SetLookAtBodyWeight", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetLookAtBodyWeight(float weight);

		// Token: 0x06000475 RID: 1141
		[NativeMethod(Name = "SetLookAtHeadWeight", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetLookAtHeadWeight(float weight);

		// Token: 0x06000476 RID: 1142
		[NativeMethod(Name = "SetLookAtEyesWeight", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSetLookAtEyesWeight(float weight);

		// Token: 0x06000477 RID: 1143
		[NativeMethod(Name = "SolveIK", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InternalSolveIK();

		// Token: 0x06000478 RID: 1144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalPositionFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		// Token: 0x06000479 RID: 1145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalRotationFromPose_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		// Token: 0x0600047A RID: 1146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x0600047B RID: 1147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyLocalPosition_Injected(ref AnimationHumanStream _unity_self, [In] ref Vector3 value);

		// Token: 0x0600047C RID: 1148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		// Token: 0x0600047D RID: 1149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyLocalRotation_Injected(ref AnimationHumanStream _unity_self, [In] ref Quaternion value);

		// Token: 0x0600047E RID: 1150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyPosition_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x0600047F RID: 1151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyPosition_Injected(ref AnimationHumanStream _unity_self, [In] ref Vector3 value);

		// Token: 0x06000480 RID: 1152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetBodyRotation_Injected(ref AnimationHumanStream _unity_self, out Quaternion ret);

		// Token: 0x06000481 RID: 1153
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBodyRotation_Injected(ref AnimationHumanStream _unity_self, [In] ref Quaternion value);

		// Token: 0x06000482 RID: 1154
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float InternalGetMuscle_Injected(ref AnimationHumanStream _unity_self, [In] ref MuscleHandle muscle);

		// Token: 0x06000483 RID: 1155
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetMuscle_Injected(ref AnimationHumanStream _unity_self, [In] ref MuscleHandle muscle, float value);

		// Token: 0x06000484 RID: 1156
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLeftFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x06000485 RID: 1157
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRightFootVelocity_Injected(ref AnimationHumanStream _unity_self, out Vector3 ret);

		// Token: 0x06000486 RID: 1158
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		// Token: 0x06000487 RID: 1159
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalLocalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, [In] ref Vector3 pos);

		// Token: 0x06000488 RID: 1160
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		// Token: 0x06000489 RID: 1161
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalLocalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, [In] ref Quaternion rot);

		// Token: 0x0600048A RID: 1162
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Vector3 ret);

		// Token: 0x0600048B RID: 1163
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, [In] ref Vector3 pos);

		// Token: 0x0600048C RID: 1164
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, out Quaternion ret);

		// Token: 0x0600048D RID: 1165
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetGoalRotation_Injected(ref AnimationHumanStream _unity_self, AvatarIKGoal index, [In] ref Quaternion rot);

		// Token: 0x0600048E RID: 1166
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalGetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, out Vector3 ret);

		// Token: 0x0600048F RID: 1167
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetHintPosition_Injected(ref AnimationHumanStream _unity_self, AvatarIKHint index, [In] ref Vector3 pos);

		// Token: 0x06000490 RID: 1168
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetLookAtPosition_Injected(ref AnimationHumanStream _unity_self, [In] ref Vector3 lookAtPosition);

		// Token: 0x04000158 RID: 344
		private IntPtr stream;
	}
}
