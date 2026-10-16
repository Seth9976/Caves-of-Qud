using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	public class Animator : Behaviour
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000040C0 File Offset: 0x000022C0
		public bool isOptimizable
		{
			[NativeMethod("IsOptimizable")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isOptimizable_Injected(intPtr);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000040E4 File Offset: 0x000022E4
		public bool isHuman
		{
			[NativeMethod("IsHuman")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isHuman_Injected(intPtr);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004108 File Offset: 0x00002308
		public bool hasRootMotion
		{
			[NativeMethod("HasRootMotion")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_hasRootMotion_Injected(intPtr);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000412C File Offset: 0x0000232C
		internal bool isRootPositionOrRotationControlledByCurves
		{
			[NativeMethod("IsRootTranslationOrRotationControllerByCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isRootPositionOrRotationControlledByCurves_Injected(intPtr);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00004150 File Offset: 0x00002350
		public float humanScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_humanScale_Injected(intPtr);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004174 File Offset: 0x00002374
		public bool isInitialized
		{
			[NativeMethod("IsInitialized")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isInitialized_Injected(intPtr);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004198 File Offset: 0x00002398
		public float GetFloat(string name)
		{
			return this.GetFloatString(name);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000041B4 File Offset: 0x000023B4
		public float GetFloat(int id)
		{
			return this.GetFloatID(id);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000041CD File Offset: 0x000023CD
		public void SetFloat(string name, float value)
		{
			this.SetFloatString(name, value);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000041D9 File Offset: 0x000023D9
		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			this.SetFloatStringDamp(name, value, dampTime, deltaTime);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000041E8 File Offset: 0x000023E8
		public void SetFloat(int id, float value)
		{
			this.SetFloatID(id, value);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000041F4 File Offset: 0x000023F4
		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			this.SetFloatIDDamp(id, value, dampTime, deltaTime);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004204 File Offset: 0x00002404
		public bool GetBool(string name)
		{
			return this.GetBoolString(name);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004220 File Offset: 0x00002420
		public bool GetBool(int id)
		{
			return this.GetBoolID(id);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004239 File Offset: 0x00002439
		public void SetBool(string name, bool value)
		{
			this.SetBoolString(name, value);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004245 File Offset: 0x00002445
		public void SetBool(int id, bool value)
		{
			this.SetBoolID(id, value);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004254 File Offset: 0x00002454
		public int GetInteger(string name)
		{
			return this.GetIntegerString(name);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004270 File Offset: 0x00002470
		public int GetInteger(int id)
		{
			return this.GetIntegerID(id);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004289 File Offset: 0x00002489
		public void SetInteger(string name, int value)
		{
			this.SetIntegerString(name, value);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004295 File Offset: 0x00002495
		public void SetInteger(int id, int value)
		{
			this.SetIntegerID(id, value);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000042A1 File Offset: 0x000024A1
		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000042AC File Offset: 0x000024AC
		public void SetTrigger(int id)
		{
			this.SetTriggerID(id);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000042B7 File Offset: 0x000024B7
		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000042C2 File Offset: 0x000024C2
		public void ResetTrigger(int id)
		{
			this.ResetTriggerID(id);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000042D0 File Offset: 0x000024D0
		public bool IsParameterControlledByCurve(string name)
		{
			return this.IsParameterControlledByCurveString(name);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000042EC File Offset: 0x000024EC
		public bool IsParameterControlledByCurve(int id)
		{
			return this.IsParameterControlledByCurveID(id);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004308 File Offset: 0x00002508
		public Vector3 deltaPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_deltaPosition_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00004330 File Offset: 0x00002530
		public Quaternion deltaRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_deltaRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004358 File Offset: 0x00002558
		public Vector3 velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_velocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00004380 File Offset: 0x00002580
		public Vector3 angularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_angularVelocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000043A8 File Offset: 0x000025A8
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000043D0 File Offset: 0x000025D0
		public Vector3 rootPosition
		{
			[NativeMethod("GetAvatarPosition")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_rootPosition_Injected(intPtr, out vector);
				return vector;
			}
			[NativeMethod("SetAvatarPosition")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_rootPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000043F4 File Offset: 0x000025F4
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000441C File Offset: 0x0000261C
		public Quaternion rootRotation
		{
			[NativeMethod("GetAvatarRotation")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_rootRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			[NativeMethod("SetAvatarRotation")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_rootRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00004440 File Offset: 0x00002640
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00004464 File Offset: 0x00002664
		public bool applyRootMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_applyRootMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_applyRootMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004488 File Offset: 0x00002688
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000044AC File Offset: 0x000026AC
		[Obsolete("Animator.linearVelocityBlending is no longer used and has been deprecated.")]
		public bool linearVelocityBlending
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_linearVelocityBlending_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_linearVelocityBlending_Injected(intPtr, value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000044D0 File Offset: 0x000026D0
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000044F4 File Offset: 0x000026F4
		public bool animatePhysics
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_animatePhysics_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_animatePhysics_Injected(intPtr, value);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00004518 File Offset: 0x00002718
		// (set) Token: 0x06000150 RID: 336 RVA: 0x0000453C File Offset: 0x0000273C
		public AnimatorUpdateMode updateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_updateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_updateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00004560 File Offset: 0x00002760
		public bool hasTransformHierarchy
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_hasTransformHierarchy_Injected(intPtr);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00004584 File Offset: 0x00002784
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000045A8 File Offset: 0x000027A8
		internal bool allowConstantClipSamplingOptimization
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_allowConstantClipSamplingOptimization_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_allowConstantClipSamplingOptimization_Injected(intPtr, value);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000045CC File Offset: 0x000027CC
		public float gravityWeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_gravityWeight_Injected(intPtr);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000045F0 File Offset: 0x000027F0
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000460F File Offset: 0x0000280F
		public Vector3 bodyPosition
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyPositionInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyPositionInternal = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00004624 File Offset: 0x00002824
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000464C File Offset: 0x0000284C
		internal Vector3 bodyPositionInternal
		{
			[NativeMethod("GetBodyPosition")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_bodyPositionInternal_Injected(intPtr, out vector);
				return vector;
			}
			[NativeMethod("SetBodyPosition")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_bodyPositionInternal_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00004670 File Offset: 0x00002870
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000468F File Offset: 0x0000288F
		public Quaternion bodyRotation
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyRotationInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyRotationInternal = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000046A4 File Offset: 0x000028A4
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000046CC File Offset: 0x000028CC
		internal Quaternion bodyRotationInternal
		{
			[NativeMethod("GetBodyRotation")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_bodyRotationInternal_Injected(intPtr, out quaternion);
				return quaternion;
			}
			[NativeMethod("SetBodyRotation")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_bodyRotationInternal_Injected(intPtr, ref value);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000046F0 File Offset: 0x000028F0
		public Vector3 GetIKPosition(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalPosition(goal);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004710 File Offset: 0x00002910
		private Vector3 GetGoalPosition(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Animator.GetGoalPosition_Injected(intPtr, goal, out vector);
			return vector;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00004736 File Offset: 0x00002936
		public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			this.CheckIfInIKPass();
			this.SetGoalPosition(goal, goalPosition);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000474C File Offset: 0x0000294C
		private void SetGoalPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalPosition_Injected(intPtr, goal, ref goalPosition);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00004774 File Offset: 0x00002974
		public Quaternion GetIKRotation(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalRotation(goal);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00004794 File Offset: 0x00002994
		private Quaternion GetGoalRotation(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Quaternion quaternion;
			Animator.GetGoalRotation_Injected(intPtr, goal, out quaternion);
			return quaternion;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000047BA File Offset: 0x000029BA
		public void SetIKRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			this.CheckIfInIKPass();
			this.SetGoalRotation(goal, goalRotation);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000047D0 File Offset: 0x000029D0
		private void SetGoalRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalRotation_Injected(intPtr, goal, ref goalRotation);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000047F8 File Offset: 0x000029F8
		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightPosition(goal);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004818 File Offset: 0x00002A18
		private float GetGoalWeightPosition(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetGoalWeightPosition_Injected(intPtr, goal);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000483B File Offset: 0x00002A3B
		public void SetIKPositionWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightPosition(goal, value);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00004850 File Offset: 0x00002A50
		private void SetGoalWeightPosition(AvatarIKGoal goal, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalWeightPosition_Injected(intPtr, goal, value);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004874 File Offset: 0x00002A74
		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightRotation(goal);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004894 File Offset: 0x00002A94
		private float GetGoalWeightRotation(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetGoalWeightRotation_Injected(intPtr, goal);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000048B7 File Offset: 0x00002AB7
		public void SetIKRotationWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightRotation(goal, value);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000048CC File Offset: 0x00002ACC
		private void SetGoalWeightRotation(AvatarIKGoal goal, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalWeightRotation_Injected(intPtr, goal, value);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000048F0 File Offset: 0x00002AF0
		public Vector3 GetIKHintPosition(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintPosition(hint);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004910 File Offset: 0x00002B10
		private Vector3 GetHintPosition(AvatarIKHint hint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Animator.GetHintPosition_Injected(intPtr, hint, out vector);
			return vector;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004936 File Offset: 0x00002B36
		public void SetIKHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			this.CheckIfInIKPass();
			this.SetHintPosition(hint, hintPosition);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000494C File Offset: 0x00002B4C
		private void SetHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetHintPosition_Injected(intPtr, hint, ref hintPosition);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004974 File Offset: 0x00002B74
		public float GetIKHintPositionWeight(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintWeightPosition(hint);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004994 File Offset: 0x00002B94
		private float GetHintWeightPosition(AvatarIKHint hint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetHintWeightPosition_Injected(intPtr, hint);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000049B7 File Offset: 0x00002BB7
		public void SetIKHintPositionWeight(AvatarIKHint hint, float value)
		{
			this.CheckIfInIKPass();
			this.SetHintWeightPosition(hint, value);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000049CC File Offset: 0x00002BCC
		private void SetHintWeightPosition(AvatarIKHint hint, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetHintWeightPosition_Injected(intPtr, hint, value);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000049F0 File Offset: 0x00002BF0
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.CheckIfInIKPass();
			this.SetLookAtPositionInternal(lookAtPosition);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00004A04 File Offset: 0x00002C04
		[NativeMethod("SetLookAtPosition")]
		private void SetLookAtPositionInternal(Vector3 lookAtPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetLookAtPositionInternal_Injected(intPtr, ref lookAtPosition);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004A28 File Offset: 0x00002C28
		public void SetLookAtWeight(float weight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, 0f, 1f, 0f, 0.5f);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004A4E File Offset: 0x00002C4E
		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, 1f, 0f, 0.5f);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004A70 File Offset: 0x00002C70
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, 0f, 0.5f);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00004A8E File Offset: 0x00002C8E
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, 0.5f);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004AA9 File Offset: 0x00002CA9
		public void SetLookAtWeight(float weight, [DefaultValue("0.0f")] float bodyWeight, [DefaultValue("1.0f")] float headWeight, [DefaultValue("0.0f")] float eyesWeight, [DefaultValue("0.5f")] float clampWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004AC4 File Offset: 0x00002CC4
		[NativeMethod("SetLookAtWeight")]
		private void SetLookAtWeightInternal(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetLookAtWeightInternal_Injected(intPtr, weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004AED File Offset: 0x00002CED
		public void SetBoneLocalRotation(HumanBodyBones humanBoneId, Quaternion rotation)
		{
			this.CheckIfInIKPass();
			this.SetBoneLocalRotationInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId), rotation);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004B08 File Offset: 0x00002D08
		[NativeMethod("SetBoneLocalRotation")]
		private void SetBoneLocalRotationInternal(int humanBoneId, Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetBoneLocalRotationInternal_Injected(intPtr, humanBoneId, ref rotation);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004B30 File Offset: 0x00002D30
		private ScriptableObject GetBehaviour([NotNull] Type type)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<ScriptableObject>(Animator.GetBehaviour_Injected(intPtr, type));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004B68 File Offset: 0x00002D68
		public T GetBehaviour<T>() where T : StateMachineBehaviour
		{
			return this.GetBehaviour(typeof(T)) as T;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004B94 File Offset: 0x00002D94
		private static T[] ConvertStateMachineBehaviour<T>(ScriptableObject[] rawObjects) where T : StateMachineBehaviour
		{
			bool flag = rawObjects == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				T[] array2 = new T[rawObjects.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (T)((object)rawObjects[i]);
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004BE0 File Offset: 0x00002DE0
		public T[] GetBehaviours<T>() where T : StateMachineBehaviour
		{
			return Animator.ConvertStateMachineBehaviour<T>(this.InternalGetBehaviours(typeof(T)));
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004C08 File Offset: 0x00002E08
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehaviours", HasExplicitThis = true)]
		internal ScriptableObject[] InternalGetBehaviours([NotNull] Type type)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.InternalGetBehaviours_Injected(intPtr, type);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004C3C File Offset: 0x00002E3C
		public StateMachineBehaviour[] GetBehaviours(int fullPathHash, int layerIndex)
		{
			return this.InternalGetBehavioursByKey(fullPathHash, layerIndex, typeof(StateMachineBehaviour)) as StateMachineBehaviour[];
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004C68 File Offset: 0x00002E68
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehavioursByKey", HasExplicitThis = true)]
		internal ScriptableObject[] InternalGetBehavioursByKey(int fullPathHash, int layerIndex, [NotNull] Type type)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.InternalGetBehavioursByKey_Injected(intPtr, fullPathHash, layerIndex, type);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004C9C File Offset: 0x00002E9C
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public bool stabilizeFeet
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_stabilizeFeet_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_stabilizeFeet_Injected(intPtr, value);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public int layerCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_layerCount_Injected(intPtr);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004D08 File Offset: 0x00002F08
		public string GetLayerName(int layerIndex)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.GetLayerName_Injected(intPtr, layerIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004D48 File Offset: 0x00002F48
		public unsafe int GetLayerIndex(string layerName)
		{
			int layerIndex_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(layerName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = layerName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				layerIndex_Injected = Animator.GetLayerIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return layerIndex_Injected;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public float GetLayerWeight(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetLayerWeight_Injected(intPtr, layerIndex);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public void SetLayerWeight(int layerIndex, float weight)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetLayerWeight_Injected(intPtr, layerIndex, weight);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004DF8 File Offset: 0x00002FF8
		private void GetAnimatorStateInfo(int layerIndex, StateInfoIndex stateInfoIndex, out AnimatorStateInfo info)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetAnimatorStateInfo_Injected(intPtr, layerIndex, stateInfoIndex, out info);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00004E20 File Offset: 0x00003020
		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo animatorStateInfo;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.CurrentState, out animatorStateInfo);
			return animatorStateInfo;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004E40 File Offset: 0x00003040
		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo animatorStateInfo;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.NextState, out animatorStateInfo);
			return animatorStateInfo;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004E60 File Offset: 0x00003060
		private void GetAnimatorTransitionInfo(int layerIndex, out AnimatorTransitionInfo info)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetAnimatorTransitionInfo_Injected(intPtr, layerIndex, out info);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004E84 File Offset: 0x00003084
		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			AnimatorTransitionInfo animatorTransitionInfo;
			this.GetAnimatorTransitionInfo(layerIndex, out animatorTransitionInfo);
			return animatorTransitionInfo;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004EA4 File Offset: 0x000030A4
		internal int GetAnimatorClipInfoCount(int layerIndex, bool current)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetAnimatorClipInfoCount_Injected(intPtr, layerIndex, current);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004EC8 File Offset: 0x000030C8
		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, true);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004EE4 File Offset: 0x000030E4
		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, false);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004F00 File Offset: 0x00003100
		[FreeFunction(Name = "AnimatorBindings::GetCurrentAnimatorClipInfo", HasExplicitThis = true)]
		[return: Unmarshalled]
		public AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetCurrentAnimatorClipInfo_Injected(intPtr, layerIndex);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004F24 File Offset: 0x00003124
		[FreeFunction(Name = "AnimatorBindings::GetNextAnimatorClipInfo", HasExplicitThis = true)]
		[return: Unmarshalled]
		public AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetNextAnimatorClipInfo_Injected(intPtr, layerIndex);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004F48 File Offset: 0x00003148
		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, true, clips);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004F74 File Offset: 0x00003174
		[FreeFunction(Name = "AnimatorBindings::GetAnimatorClipInfoInternal", HasExplicitThis = true)]
		private void GetAnimatorClipInfoInternal(int layerIndex, bool isCurrent, object clips)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetAnimatorClipInfoInternal_Injected(intPtr, layerIndex, isCurrent, clips);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004F9C File Offset: 0x0000319C
		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, false, clips);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004FC8 File Offset: 0x000031C8
		public bool IsInTransition(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsInTransition_Injected(intPtr, layerIndex);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00004FEC File Offset: 0x000031EC
		public AnimatorControllerParameter[] parameters
		{
			[FreeFunction(Name = "AnimatorBindings::GetParameters", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_parameters_Injected(intPtr);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005010 File Offset: 0x00003210
		public int parameterCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_parameterCount_Injected(intPtr);
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005034 File Offset: 0x00003234
		[FreeFunction(Name = "AnimatorBindings::GetParameterInternal", HasExplicitThis = true)]
		private AnimatorControllerParameter GetParameterInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetParameterInternal_Injected(intPtr, index);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005058 File Offset: 0x00003258
		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter parameterInternal = this.GetParameterInternal(index);
			bool flag = parameterInternal.m_Type == (AnimatorControllerParameterType)0;
			if (flag)
			{
				throw new IndexOutOfRangeException("Index must be between 0 and " + this.parameterCount.ToString());
			}
			return parameterInternal;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000050A0 File Offset: 0x000032A0
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000050C4 File Offset: 0x000032C4
		public float feetPivotActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_feetPivotActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_feetPivotActive_Injected(intPtr, value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000050E8 File Offset: 0x000032E8
		public float pivotWeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_pivotWeight_Injected(intPtr);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000510C File Offset: 0x0000330C
		public Vector3 pivotPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_pivotPosition_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005134 File Offset: 0x00003334
		private void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, int targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.MatchTarget_Injected(intPtr, ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00005163 File Offset: 0x00003363
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, 1f, true);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000517A File Offset: 0x0000337A
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [DefaultValue("1")] float targetNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, true);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000518E File Offset: 0x0000338E
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [DefaultValue("1")] float targetNormalizedTime, [DefaultValue("true")] bool completeMatch)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000051A3 File Offset: 0x000033A3
		public void InterruptMatchTarget()
		{
			this.InterruptMatchTarget(true);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000051B0 File Offset: 0x000033B0
		public void InterruptMatchTarget([DefaultValue("true")] bool completeMatch)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.InterruptMatchTarget_Injected(intPtr, completeMatch);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000051D4 File Offset: 0x000033D4
		public bool isMatchingTarget
		{
			[NativeMethod("IsMatchingTarget")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isMatchingTarget_Injected(intPtr);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000051F8 File Offset: 0x000033F8
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000521C File Offset: 0x0000341C
		public float speed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_speed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_speed_Injected(intPtr, value);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000523F File Offset: 0x0000343F
		[Obsolete("ForceStateNormalizedTime is deprecated. Please use Play or CrossFade instead.")]
		public void ForceStateNormalizedTime(float normalizedTime)
		{
			this.Play(0, 0, normalizedTime);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000524C File Offset: 0x0000344C
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration)
		{
			float num = 0f;
			float num2 = 0f;
			int num3 = -1;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, num3, num2, num);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000527C File Offset: 0x0000347C
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer)
		{
			float num = 0f;
			float num2 = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, num2, num);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000052A8 File Offset: 0x000034A8
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float num = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, num);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000052CE File Offset: 0x000034CE
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTimeOffset, [DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000052E4 File Offset: 0x000034E4
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float num = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, num);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005308 File Offset: 0x00003508
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer)
		{
			float num = 0f;
			float num2 = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, num2, num);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00005330 File Offset: 0x00003530
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration)
		{
			float num = 0f;
			float num2 = 0f;
			int num3 = -1;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, num3, num2, num);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005358 File Offset: 0x00003558
		[FreeFunction(Name = "AnimatorBindings::CrossFadeInFixedTime", HasExplicitThis = true)]
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTimeOffset, [DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.CrossFadeInFixedTime_Injected(intPtr, stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00005384 File Offset: 0x00003584
		[FreeFunction(Name = "AnimatorBindings::WriteDefaultValues", HasExplicitThis = true)]
		public void WriteDefaultValues()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.WriteDefaultValues_Injected(intPtr);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000053A8 File Offset: 0x000035A8
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float num = 0f;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, num);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000053CC File Offset: 0x000035CC
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer)
		{
			float num = 0f;
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, negativeInfinity, num);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000053F4 File Offset: 0x000035F4
		public void CrossFade(string stateName, float normalizedTransitionDuration)
		{
			float num = 0f;
			float negativeInfinity = float.NegativeInfinity;
			int num2 = -1;
			this.CrossFade(stateName, normalizedTransitionDuration, num2, negativeInfinity, num);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000541C File Offset: 0x0000361C
		public void CrossFade(string stateName, float normalizedTransitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTimeOffset, [DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFade(Animator.StringToHash(stateName), normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005434 File Offset: 0x00003634
		[FreeFunction(Name = "AnimatorBindings::CrossFade", HasExplicitThis = true)]
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float normalizedTimeOffset, [DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.CrossFade_Injected(intPtr, stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005460 File Offset: 0x00003660
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float num = 0f;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, num);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005484 File Offset: 0x00003684
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer)
		{
			float num = 0f;
			float negativeInfinity = float.NegativeInfinity;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, negativeInfinity, num);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000054AC File Offset: 0x000036AC
		public void CrossFade(int stateHashName, float normalizedTransitionDuration)
		{
			float num = 0f;
			float negativeInfinity = float.NegativeInfinity;
			int num2 = -1;
			this.CrossFade(stateHashName, normalizedTransitionDuration, num2, negativeInfinity, num);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000054D4 File Offset: 0x000036D4
		public void PlayInFixedTime(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateName, layer, negativeInfinity);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000054F4 File Offset: 0x000036F4
		public void PlayInFixedTime(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.PlayInFixedTime(stateName, num, negativeInfinity);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005514 File Offset: 0x00003714
		public void PlayInFixedTime(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			this.PlayInFixedTime(Animator.StringToHash(stateName), layer, fixedTime);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005528 File Offset: 0x00003728
		[FreeFunction(Name = "AnimatorBindings::PlayInFixedTime", HasExplicitThis = true)]
		public void PlayInFixedTime(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.PlayInFixedTime_Injected(intPtr, stateNameHash, layer, fixedTime);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005550 File Offset: 0x00003750
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.PlayInFixedTime(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005570 File Offset: 0x00003770
		public void PlayInFixedTime(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.PlayInFixedTime(stateNameHash, num, negativeInfinity);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005590 File Offset: 0x00003790
		public void Play(string stateName, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateName, layer, negativeInfinity);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000055B0 File Offset: 0x000037B0
		public void Play(string stateName)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.Play(stateName, num, negativeInfinity);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000055D0 File Offset: 0x000037D0
		public void Play(string stateName, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(Animator.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000055E4 File Offset: 0x000037E4
		[FreeFunction(Name = "AnimatorBindings::Play", HasExplicitThis = true)]
		public void Play(int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.Play_Injected(intPtr, stateNameHash, layer, normalizedTime);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000560C File Offset: 0x0000380C
		public void Play(int stateNameHash, int layer)
		{
			float negativeInfinity = float.NegativeInfinity;
			this.Play(stateNameHash, layer, negativeInfinity);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000562C File Offset: 0x0000382C
		public void Play(int stateNameHash)
		{
			float negativeInfinity = float.NegativeInfinity;
			int num = -1;
			this.Play(stateNameHash, num, negativeInfinity);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000564C File Offset: 0x0000384C
		public void SetTarget(AvatarTarget targetIndex, float targetNormalizedTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetTarget_Injected(intPtr, targetIndex, targetNormalizedTime);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005670 File Offset: 0x00003870
		public Vector3 targetPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_targetPosition_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00005698 File Offset: 0x00003898
		public Quaternion targetRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_targetRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000056C0 File Offset: 0x000038C0
		[Obsolete("Use mask and layers to control subset of transfroms in a skeleton.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsControlled(Transform transform)
		{
			return false;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000056D4 File Offset: 0x000038D4
		internal bool IsBoneTransform(Transform transform)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsBoneTransform_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform));
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000056FC File Offset: 0x000038FC
		public Transform avatarRoot
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(Animator.get_avatarRoot_Injected(intPtr));
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005724 File Offset: 0x00003924
		public Transform GetBoneTransform(HumanBodyBones humanBoneId)
		{
			bool flag = this.avatar == null;
			if (flag)
			{
				throw new InvalidOperationException("Avatar is null.");
			}
			bool flag2 = !this.avatar.isValid;
			if (flag2)
			{
				throw new InvalidOperationException("Avatar is not valid.");
			}
			bool flag3 = !this.avatar.isHuman;
			if (flag3)
			{
				throw new InvalidOperationException("Avatar is not of type humanoid.");
			}
			bool flag4 = humanBoneId < HumanBodyBones.Hips || humanBoneId >= HumanBodyBones.LastBone;
			if (flag4)
			{
				throw new IndexOutOfRangeException("humanBoneId must be between 0 and " + HumanBodyBones.LastBone.ToString());
			}
			return this.GetBoneTransformInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId));
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000057D0 File Offset: 0x000039D0
		[NativeMethod("GetBoneTransform")]
		internal Transform GetBoneTransformInternal(int humanBoneId)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Animator.GetBoneTransformInternal_Injected(intPtr, humanBoneId));
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x000057F8 File Offset: 0x000039F8
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000581C File Offset: 0x00003A1C
		public AnimatorCullingMode cullingMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_cullingMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_cullingMode_Injected(intPtr, value);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005840 File Offset: 0x00003A40
		public void StartPlayback()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StartPlayback_Injected(intPtr);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005864 File Offset: 0x00003A64
		public void StopPlayback()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StopPlayback_Injected(intPtr);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005888 File Offset: 0x00003A88
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x000058AC File Offset: 0x00003AAC
		public float playbackTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_playbackTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_playbackTime_Injected(intPtr, value);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000058D0 File Offset: 0x00003AD0
		public void StartRecording(int frameCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StartRecording_Injected(intPtr, frameCount);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000058F4 File Offset: 0x00003AF4
		public void StopRecording()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StopRecording_Injected(intPtr);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005918 File Offset: 0x00003B18
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00002059 File Offset: 0x00000259
		public float recorderStartTime
		{
			get
			{
				return this.GetRecorderStartTime();
			}
			set
			{
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00005930 File Offset: 0x00003B30
		private float GetRecorderStartTime()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetRecorderStartTime_Injected(intPtr);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00005954 File Offset: 0x00003B54
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00002059 File Offset: 0x00000259
		public float recorderStopTime
		{
			get
			{
				return this.GetRecorderStopTime();
			}
			set
			{
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000596C File Offset: 0x00003B6C
		private float GetRecorderStopTime()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetRecorderStopTime_Injected(intPtr);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00005990 File Offset: 0x00003B90
		public AnimatorRecorderMode recorderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_recorderMode_Injected(intPtr);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x000059B4 File Offset: 0x00003BB4
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x000059DC File Offset: 0x00003BDC
		public RuntimeAnimatorController runtimeAnimatorController
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RuntimeAnimatorController>(Animator.get_runtimeAnimatorController_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_runtimeAnimatorController_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RuntimeAnimatorController>(value));
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00005A04 File Offset: 0x00003C04
		public bool hasBoundPlayables
		{
			[NativeMethod("HasBoundPlayables")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_hasBoundPlayables_Injected(intPtr);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005A28 File Offset: 0x00003C28
		internal void ClearInternalControllerPlayable()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.ClearInternalControllerPlayable_Injected(intPtr);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005A4C File Offset: 0x00003C4C
		public bool HasState(int layerIndex, int stateID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.HasState_Injected(intPtr, layerIndex, stateID);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00005A70 File Offset: 0x00003C70
		[NativeMethod(Name = "ScriptingStringToCRC32", IsThreadSafe = true)]
		public unsafe static int StringToHash(string name)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Animator.StringToHash_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00005AC8 File Offset: 0x00003CC8
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00005AF0 File Offset: 0x00003CF0
		public Avatar avatar
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Avatar>(Animator.get_avatar_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_avatar_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Avatar>(value));
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005B18 File Offset: 0x00003D18
		internal string GetStats()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.GetStats_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00005B58 File Offset: 0x00003D58
		public PlayableGraph playableGraph
		{
			get
			{
				PlayableGraph playableGraph = default(PlayableGraph);
				this.GetCurrentGraph(ref playableGraph);
				return playableGraph;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005B7C File Offset: 0x00003D7C
		[FreeFunction(Name = "AnimatorBindings::GetCurrentGraph", HasExplicitThis = true)]
		private void GetCurrentGraph(ref PlayableGraph graph)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetCurrentGraph_Injected(intPtr, ref graph);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005BA0 File Offset: 0x00003DA0
		private void CheckIfInIKPass()
		{
			bool flag = this.logWarnings && !this.IsInIKPass();
			if (flag)
			{
				Debug.LogWarning("Setting and getting Body Position/Rotation, IK Goals, Lookat and BoneLocalRotation should only be done in OnAnimatorIK or OnStateIK");
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005BD4 File Offset: 0x00003DD4
		private bool IsInIKPass()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsInIKPass_Injected(intPtr);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005BF8 File Offset: 0x00003DF8
		[FreeFunction(Name = "AnimatorBindings::SetFloatString", HasExplicitThis = true)]
		private unsafe void SetFloatString(string name, float value)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetFloatString_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005C60 File Offset: 0x00003E60
		[FreeFunction(Name = "AnimatorBindings::SetFloatID", HasExplicitThis = true)]
		private void SetFloatID(int id, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetFloatID_Injected(intPtr, id, value);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005C84 File Offset: 0x00003E84
		[FreeFunction(Name = "AnimatorBindings::GetFloatString", HasExplicitThis = true)]
		private unsafe float GetFloatString(string name)
		{
			float floatString_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				floatString_Injected = Animator.GetFloatString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return floatString_Injected;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00005CEC File Offset: 0x00003EEC
		[FreeFunction(Name = "AnimatorBindings::GetFloatID", HasExplicitThis = true)]
		private float GetFloatID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetFloatID_Injected(intPtr, id);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005D10 File Offset: 0x00003F10
		[FreeFunction(Name = "AnimatorBindings::SetBoolString", HasExplicitThis = true)]
		private unsafe void SetBoolString(string name, bool value)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetBoolString_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005D78 File Offset: 0x00003F78
		[FreeFunction(Name = "AnimatorBindings::SetBoolID", HasExplicitThis = true)]
		private void SetBoolID(int id, bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetBoolID_Injected(intPtr, id, value);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005D9C File Offset: 0x00003F9C
		[FreeFunction(Name = "AnimatorBindings::GetBoolString", HasExplicitThis = true)]
		private unsafe bool GetBoolString(string name)
		{
			bool boolString_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				boolString_Injected = Animator.GetBoolString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return boolString_Injected;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005E04 File Offset: 0x00004004
		[FreeFunction(Name = "AnimatorBindings::GetBoolID", HasExplicitThis = true)]
		private bool GetBoolID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetBoolID_Injected(intPtr, id);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005E28 File Offset: 0x00004028
		[FreeFunction(Name = "AnimatorBindings::SetIntegerString", HasExplicitThis = true)]
		private unsafe void SetIntegerString(string name, int value)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetIntegerString_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00005E90 File Offset: 0x00004090
		[FreeFunction(Name = "AnimatorBindings::SetIntegerID", HasExplicitThis = true)]
		private void SetIntegerID(int id, int value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetIntegerID_Injected(intPtr, id, value);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00005EB4 File Offset: 0x000040B4
		[FreeFunction(Name = "AnimatorBindings::GetIntegerString", HasExplicitThis = true)]
		private unsafe int GetIntegerString(string name)
		{
			int integerString_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				integerString_Injected = Animator.GetIntegerString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return integerString_Injected;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005F1C File Offset: 0x0000411C
		[FreeFunction(Name = "AnimatorBindings::GetIntegerID", HasExplicitThis = true)]
		private int GetIntegerID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetIntegerID_Injected(intPtr, id);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005F40 File Offset: 0x00004140
		[FreeFunction(Name = "AnimatorBindings::SetTriggerString", HasExplicitThis = true)]
		private unsafe void SetTriggerString(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetTriggerString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005FA4 File Offset: 0x000041A4
		[FreeFunction(Name = "AnimatorBindings::SetTriggerID", HasExplicitThis = true)]
		private void SetTriggerID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetTriggerID_Injected(intPtr, id);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005FC8 File Offset: 0x000041C8
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerString", HasExplicitThis = true)]
		private unsafe void ResetTriggerString(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.ResetTriggerString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000602C File Offset: 0x0000422C
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerID", HasExplicitThis = true)]
		private void ResetTriggerID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.ResetTriggerID_Injected(intPtr, id);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006050 File Offset: 0x00004250
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveString", HasExplicitThis = true)]
		private unsafe bool IsParameterControlledByCurveString(string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Animator.IsParameterControlledByCurveString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000060B8 File Offset: 0x000042B8
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveID", HasExplicitThis = true)]
		private bool IsParameterControlledByCurveID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsParameterControlledByCurveID_Injected(intPtr, id);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000060DC File Offset: 0x000042DC
		[FreeFunction(Name = "AnimatorBindings::SetFloatStringDamp", HasExplicitThis = true)]
		private unsafe void SetFloatStringDamp(string name, float value, float dampTime, float deltaTime)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetFloatStringDamp_Injected(intPtr, ref managedSpanWrapper, value, dampTime, deltaTime);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006144 File Offset: 0x00004344
		[FreeFunction(Name = "AnimatorBindings::SetFloatIDDamp", HasExplicitThis = true)]
		private void SetFloatIDDamp(int id, float value, float dampTime, float deltaTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetFloatIDDamp_Injected(intPtr, id, value, dampTime, deltaTime);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000616C File Offset: 0x0000436C
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00006190 File Offset: 0x00004390
		public bool layersAffectMassCenter
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_layersAffectMassCenter_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_layersAffectMassCenter_Injected(intPtr, value);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000061B4 File Offset: 0x000043B4
		public float leftFeetBottomHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_leftFeetBottomHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000061D8 File Offset: 0x000043D8
		public float rightFeetBottomHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_rightFeetBottomHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000061FC File Offset: 0x000043FC
		[NativeConditional("UNITY_EDITOR")]
		internal bool supportsOnAnimatorMove
		{
			[NativeMethod("SupportsOnAnimatorMove")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_supportsOnAnimatorMove_Injected(intPtr);
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006220 File Offset: 0x00004420
		[NativeConditional("UNITY_EDITOR")]
		internal void OnUpdateModeChanged()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.OnUpdateModeChanged_Injected(intPtr);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006244 File Offset: 0x00004444
		[NativeConditional("UNITY_EDITOR")]
		internal void OnCullingModeChanged()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.OnCullingModeChanged_Injected(intPtr);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00006268 File Offset: 0x00004468
		[NativeConditional("UNITY_EDITOR")]
		internal void WriteDefaultPose()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.WriteDefaultPose_Injected(intPtr);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000628C File Offset: 0x0000448C
		[NativeMethod("UpdateWithDelta")]
		public void Update(float deltaTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.Update_Injected(intPtr, deltaTime);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000062AF File Offset: 0x000044AF
		public void Rebind()
		{
			this.Rebind(true);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000062BC File Offset: 0x000044BC
		private void Rebind(bool writeDefaultValues)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.Rebind_Injected(intPtr, writeDefaultValues);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000062E0 File Offset: 0x000044E0
		public void ApplyBuiltinRootMotion()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.ApplyBuiltinRootMotion_Injected(intPtr);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006302 File Offset: 0x00004502
		[NativeConditional("UNITY_EDITOR")]
		internal void EvaluateController()
		{
			this.EvaluateController(0f);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006314 File Offset: 0x00004514
		private void EvaluateController(float deltaTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.EvaluateController_Injected(intPtr, deltaTime);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006338 File Offset: 0x00004538
		[NativeConditional("UNITY_EDITOR")]
		internal string GetCurrentStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, true);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00006354 File Offset: 0x00004554
		[NativeConditional("UNITY_EDITOR")]
		internal string GetNextStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, false);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00006370 File Offset: 0x00004570
		[NativeConditional("UNITY_EDITOR")]
		private string GetAnimatorStateName(int layerIndex, bool current)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.GetAnimatorStateName_Injected(intPtr, layerIndex, current, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000063B4 File Offset: 0x000045B4
		internal string ResolveHash(int hash)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.ResolveHash_Injected(intPtr, hash, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000063F4 File Offset: 0x000045F4
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00006418 File Offset: 0x00004618
		public bool logWarnings
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_logWarnings_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_logWarnings_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000643C File Offset: 0x0000463C
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00006460 File Offset: 0x00004660
		public bool fireEvents
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_fireEvents_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_fireEvents_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006484 File Offset: 0x00004684
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000649C File Offset: 0x0000469C
		[Obsolete("keepAnimatorControllerStateOnDisable is deprecated, use keepAnimatorStateOnDisable instead. (UnityUpgradable) -> keepAnimatorStateOnDisable", false)]
		public bool keepAnimatorControllerStateOnDisable
		{
			get
			{
				return this.keepAnimatorStateOnDisable;
			}
			set
			{
				this.keepAnimatorStateOnDisable = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000064A8 File Offset: 0x000046A8
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000064CC File Offset: 0x000046CC
		public bool keepAnimatorStateOnDisable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_keepAnimatorStateOnDisable_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_keepAnimatorStateOnDisable_Injected(intPtr, value);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000064F0 File Offset: 0x000046F0
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00006514 File Offset: 0x00004714
		public bool writeDefaultValuesOnDisable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_writeDefaultValuesOnDisable_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_writeDefaultValuesOnDisable_Injected(intPtr, value);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006538 File Offset: 0x00004738
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(string name)
		{
			return Vector3.zero;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006550 File Offset: 0x00004750
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(string name, Vector3 value)
		{
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(int id, Vector3 value)
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00006568 File Offset: 0x00004768
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(string name)
		{
			return Quaternion.identity;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00006580 File Offset: 0x00004780
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(int id)
		{
			return Quaternion.identity;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(string name, Quaternion value)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00002059 File Offset: 0x00000259
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(int id, Quaternion value)
		{
		}

		// Token: 0x06000227 RID: 551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isOptimizable_Injected(IntPtr _unity_self);

		// Token: 0x06000228 RID: 552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isHuman_Injected(IntPtr _unity_self);

		// Token: 0x06000229 RID: 553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x0600022A RID: 554
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isRootPositionOrRotationControlledByCurves_Injected(IntPtr _unity_self);

		// Token: 0x0600022B RID: 555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_humanScale_Injected(IntPtr _unity_self);

		// Token: 0x0600022C RID: 556
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isInitialized_Injected(IntPtr _unity_self);

		// Token: 0x0600022D RID: 557
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_deltaPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600022E RID: 558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_deltaRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x0600022F RID: 559
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_velocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000230 RID: 560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000231 RID: 561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rootPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000232 RID: 562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rootPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000233 RID: 563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rootRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000234 RID: 564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rootRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x06000235 RID: 565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_applyRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000236 RID: 566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_applyRootMotion_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000237 RID: 567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_linearVelocityBlending_Injected(IntPtr _unity_self);

		// Token: 0x06000238 RID: 568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocityBlending_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000239 RID: 569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_animatePhysics_Injected(IntPtr _unity_self);

		// Token: 0x0600023A RID: 570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_animatePhysics_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600023B RID: 571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorUpdateMode get_updateMode_Injected(IntPtr _unity_self);

		// Token: 0x0600023C RID: 572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateMode_Injected(IntPtr _unity_self, AnimatorUpdateMode value);

		// Token: 0x0600023D RID: 573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasTransformHierarchy_Injected(IntPtr _unity_self);

		// Token: 0x0600023E RID: 574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowConstantClipSamplingOptimization_Injected(IntPtr _unity_self);

		// Token: 0x0600023F RID: 575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowConstantClipSamplingOptimization_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000240 RID: 576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_gravityWeight_Injected(IntPtr _unity_self);

		// Token: 0x06000241 RID: 577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bodyPositionInternal_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000242 RID: 578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bodyPositionInternal_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000243 RID: 579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bodyRotationInternal_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000244 RID: 580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bodyRotationInternal_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x06000245 RID: 581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGoalPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal, out Vector3 ret);

		// Token: 0x06000246 RID: 582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal, [In] ref Vector3 goalPosition);

		// Token: 0x06000247 RID: 583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGoalRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal, out Quaternion ret);

		// Token: 0x06000248 RID: 584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal, [In] ref Quaternion goalRotation);

		// Token: 0x06000249 RID: 585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGoalWeightPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal);

		// Token: 0x0600024A RID: 586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalWeightPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal, float value);

		// Token: 0x0600024B RID: 587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGoalWeightRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal);

		// Token: 0x0600024C RID: 588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalWeightRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal, float value);

		// Token: 0x0600024D RID: 589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHintPosition_Injected(IntPtr _unity_self, AvatarIKHint hint, out Vector3 ret);

		// Token: 0x0600024E RID: 590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHintPosition_Injected(IntPtr _unity_self, AvatarIKHint hint, [In] ref Vector3 hintPosition);

		// Token: 0x0600024F RID: 591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetHintWeightPosition_Injected(IntPtr _unity_self, AvatarIKHint hint);

		// Token: 0x06000250 RID: 592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHintWeightPosition_Injected(IntPtr _unity_self, AvatarIKHint hint, float value);

		// Token: 0x06000251 RID: 593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLookAtPositionInternal_Injected(IntPtr _unity_self, [In] ref Vector3 lookAtPosition);

		// Token: 0x06000252 RID: 594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLookAtWeightInternal_Injected(IntPtr _unity_self, float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight);

		// Token: 0x06000253 RID: 595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoneLocalRotationInternal_Injected(IntPtr _unity_self, int humanBoneId, [In] ref Quaternion rotation);

		// Token: 0x06000254 RID: 596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBehaviour_Injected(IntPtr _unity_self, Type type);

		// Token: 0x06000255 RID: 597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject[] InternalGetBehaviours_Injected(IntPtr _unity_self, Type type);

		// Token: 0x06000256 RID: 598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject[] InternalGetBehavioursByKey_Injected(IntPtr _unity_self, int fullPathHash, int layerIndex, Type type);

		// Token: 0x06000257 RID: 599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stabilizeFeet_Injected(IntPtr _unity_self);

		// Token: 0x06000258 RID: 600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stabilizeFeet_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000259 RID: 601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_layerCount_Injected(IntPtr _unity_self);

		// Token: 0x0600025A RID: 602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLayerName_Injected(IntPtr _unity_self, int layerIndex, out ManagedSpanWrapper ret);

		// Token: 0x0600025B RID: 603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerIndex_Injected(IntPtr _unity_self, ref ManagedSpanWrapper layerName);

		// Token: 0x0600025C RID: 604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetLayerWeight_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x0600025D RID: 605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerWeight_Injected(IntPtr _unity_self, int layerIndex, float weight);

		// Token: 0x0600025E RID: 606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorStateInfo_Injected(IntPtr _unity_self, int layerIndex, StateInfoIndex stateInfoIndex, out AnimatorStateInfo info);

		// Token: 0x0600025F RID: 607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorTransitionInfo_Injected(IntPtr _unity_self, int layerIndex, out AnimatorTransitionInfo info);

		// Token: 0x06000260 RID: 608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAnimatorClipInfoCount_Injected(IntPtr _unity_self, int layerIndex, bool current);

		// Token: 0x06000261 RID: 609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorClipInfo[] GetCurrentAnimatorClipInfo_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x06000262 RID: 610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorClipInfo[] GetNextAnimatorClipInfo_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x06000263 RID: 611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorClipInfoInternal_Injected(IntPtr _unity_self, int layerIndex, bool isCurrent, object clips);

		// Token: 0x06000264 RID: 612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInTransition_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x06000265 RID: 613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorControllerParameter[] get_parameters_Injected(IntPtr _unity_self);

		// Token: 0x06000266 RID: 614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_parameterCount_Injected(IntPtr _unity_self);

		// Token: 0x06000267 RID: 615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorControllerParameter GetParameterInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000268 RID: 616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_feetPivotActive_Injected(IntPtr _unity_self);

		// Token: 0x06000269 RID: 617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_feetPivotActive_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600026A RID: 618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_pivotWeight_Injected(IntPtr _unity_self);

		// Token: 0x0600026B RID: 619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pivotPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600026C RID: 620
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MatchTarget_Injected(IntPtr _unity_self, [In] ref Vector3 matchPosition, [In] ref Quaternion matchRotation, int targetBodyPart, [In] ref MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch);

		// Token: 0x0600026D RID: 621
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InterruptMatchTarget_Injected(IntPtr _unity_self, [DefaultValue("true")] bool completeMatch);

		// Token: 0x0600026E RID: 622
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isMatchingTarget_Injected(IntPtr _unity_self);

		// Token: 0x0600026F RID: 623
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_speed_Injected(IntPtr _unity_self);

		// Token: 0x06000270 RID: 624
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_speed_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000271 RID: 625
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CrossFadeInFixedTime_Injected(IntPtr _unity_self, int stateHashName, float fixedTransitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float fixedTimeOffset, [DefaultValue("0.0f")] float normalizedTransitionTime);

		// Token: 0x06000272 RID: 626
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteDefaultValues_Injected(IntPtr _unity_self);

		// Token: 0x06000273 RID: 627
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CrossFade_Injected(IntPtr _unity_self, int stateHashName, float normalizedTransitionDuration, [DefaultValue("-1")] int layer, [DefaultValue("0.0f")] float normalizedTimeOffset, [DefaultValue("0.0f")] float normalizedTransitionTime);

		// Token: 0x06000274 RID: 628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayInFixedTime_Injected(IntPtr _unity_self, int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float fixedTime);

		// Token: 0x06000275 RID: 629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self, int stateNameHash, [DefaultValue("-1")] int layer, [DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x06000276 RID: 630
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTarget_Injected(IntPtr _unity_self, AvatarTarget targetIndex, float targetNormalizedTime);

		// Token: 0x06000277 RID: 631
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000278 RID: 632
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000279 RID: 633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsBoneTransform_Injected(IntPtr _unity_self, IntPtr transform);

		// Token: 0x0600027A RID: 634
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_avatarRoot_Injected(IntPtr _unity_self);

		// Token: 0x0600027B RID: 635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBoneTransformInternal_Injected(IntPtr _unity_self, int humanBoneId);

		// Token: 0x0600027C RID: 636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorCullingMode get_cullingMode_Injected(IntPtr _unity_self);

		// Token: 0x0600027D RID: 637
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMode_Injected(IntPtr _unity_self, AnimatorCullingMode value);

		// Token: 0x0600027E RID: 638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartPlayback_Injected(IntPtr _unity_self);

		// Token: 0x0600027F RID: 639
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopPlayback_Injected(IntPtr _unity_self);

		// Token: 0x06000280 RID: 640
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_playbackTime_Injected(IntPtr _unity_self);

		// Token: 0x06000281 RID: 641
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playbackTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000282 RID: 642
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartRecording_Injected(IntPtr _unity_self, int frameCount);

		// Token: 0x06000283 RID: 643
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopRecording_Injected(IntPtr _unity_self);

		// Token: 0x06000284 RID: 644
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetRecorderStartTime_Injected(IntPtr _unity_self);

		// Token: 0x06000285 RID: 645
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetRecorderStopTime_Injected(IntPtr _unity_self);

		// Token: 0x06000286 RID: 646
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorRecorderMode get_recorderMode_Injected(IntPtr _unity_self);

		// Token: 0x06000287 RID: 647
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_runtimeAnimatorController_Injected(IntPtr _unity_self);

		// Token: 0x06000288 RID: 648
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_runtimeAnimatorController_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06000289 RID: 649
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasBoundPlayables_Injected(IntPtr _unity_self);

		// Token: 0x0600028A RID: 650
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearInternalControllerPlayable_Injected(IntPtr _unity_self);

		// Token: 0x0600028B RID: 651
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasState_Injected(IntPtr _unity_self, int layerIndex, int stateID);

		// Token: 0x0600028C RID: 652
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int StringToHash_Injected(ref ManagedSpanWrapper name);

		// Token: 0x0600028D RID: 653
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_avatar_Injected(IntPtr _unity_self);

		// Token: 0x0600028E RID: 654
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_avatar_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600028F RID: 655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStats_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000290 RID: 656
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentGraph_Injected(IntPtr _unity_self, ref PlayableGraph graph);

		// Token: 0x06000291 RID: 657
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInIKPass_Injected(IntPtr _unity_self);

		// Token: 0x06000292 RID: 658
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, float value);

		// Token: 0x06000293 RID: 659
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatID_Injected(IntPtr _unity_self, int id, float value);

		// Token: 0x06000294 RID: 660
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06000295 RID: 661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatID_Injected(IntPtr _unity_self, int id);

		// Token: 0x06000296 RID: 662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, bool value);

		// Token: 0x06000297 RID: 663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolID_Injected(IntPtr _unity_self, int id, bool value);

		// Token: 0x06000298 RID: 664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06000299 RID: 665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolID_Injected(IntPtr _unity_self, int id);

		// Token: 0x0600029A RID: 666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntegerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, int value);

		// Token: 0x0600029B RID: 667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntegerID_Injected(IntPtr _unity_self, int id, int value);

		// Token: 0x0600029C RID: 668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntegerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x0600029D RID: 669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntegerID_Injected(IntPtr _unity_self, int id);

		// Token: 0x0600029E RID: 670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTriggerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x0600029F RID: 671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTriggerID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060002A0 RID: 672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTriggerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060002A1 RID: 673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTriggerID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060002A2 RID: 674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsParameterControlledByCurveString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060002A3 RID: 675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsParameterControlledByCurveID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060002A4 RID: 676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatStringDamp_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, float value, float dampTime, float deltaTime);

		// Token: 0x060002A5 RID: 677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatIDDamp_Injected(IntPtr _unity_self, int id, float value, float dampTime, float deltaTime);

		// Token: 0x060002A6 RID: 678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_layersAffectMassCenter_Injected(IntPtr _unity_self);

		// Token: 0x060002A7 RID: 679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layersAffectMassCenter_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060002A8 RID: 680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_leftFeetBottomHeight_Injected(IntPtr _unity_self);

		// Token: 0x060002A9 RID: 681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rightFeetBottomHeight_Injected(IntPtr _unity_self);

		// Token: 0x060002AA RID: 682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_supportsOnAnimatorMove_Injected(IntPtr _unity_self);

		// Token: 0x060002AB RID: 683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnUpdateModeChanged_Injected(IntPtr _unity_self);

		// Token: 0x060002AC RID: 684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnCullingModeChanged_Injected(IntPtr _unity_self);

		// Token: 0x060002AD RID: 685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteDefaultPose_Injected(IntPtr _unity_self);

		// Token: 0x060002AE RID: 686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self, float deltaTime);

		// Token: 0x060002AF RID: 687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Rebind_Injected(IntPtr _unity_self, bool writeDefaultValues);

		// Token: 0x060002B0 RID: 688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyBuiltinRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x060002B1 RID: 689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EvaluateController_Injected(IntPtr _unity_self, float deltaTime);

		// Token: 0x060002B2 RID: 690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorStateName_Injected(IntPtr _unity_self, int layerIndex, bool current, out ManagedSpanWrapper ret);

		// Token: 0x060002B3 RID: 691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveHash_Injected(IntPtr _unity_self, int hash, out ManagedSpanWrapper ret);

		// Token: 0x060002B4 RID: 692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_logWarnings_Injected(IntPtr _unity_self);

		// Token: 0x060002B5 RID: 693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_logWarnings_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060002B6 RID: 694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_fireEvents_Injected(IntPtr _unity_self);

		// Token: 0x060002B7 RID: 695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fireEvents_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060002B8 RID: 696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_keepAnimatorStateOnDisable_Injected(IntPtr _unity_self);

		// Token: 0x060002B9 RID: 697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_keepAnimatorStateOnDisable_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060002BA RID: 698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_writeDefaultValuesOnDisable_Injected(IntPtr _unity_self);

		// Token: 0x060002BB RID: 699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_writeDefaultValuesOnDisable_Injected(IntPtr _unity_self, bool value);
	}
}
