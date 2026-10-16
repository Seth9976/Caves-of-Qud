using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/Animation/Avatar.h")]
	[UsedByNativeCode]
	public class Avatar : Object
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x00006C0A File Offset: 0x00004E0A
		private Avatar()
		{
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00006C14 File Offset: 0x00004E14
		public bool isValid
		{
			[NativeMethod("IsValid")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Avatar.get_isValid_Injected(intPtr);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00006C38 File Offset: 0x00004E38
		public bool isHuman
		{
			[NativeMethod("IsHuman")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Avatar.get_isHuman_Injected(intPtr);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00006C5C File Offset: 0x00004E5C
		public HumanDescription humanDescription
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HumanDescription humanDescription;
				Avatar.get_humanDescription_Injected(intPtr, out humanDescription);
				return humanDescription;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00006C84 File Offset: 0x00004E84
		internal void SetMuscleMinMax(int muscleId, float min, float max)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Avatar.SetMuscleMinMax_Injected(intPtr, muscleId, min, max);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00006CAC File Offset: 0x00004EAC
		internal void SetParameter(int parameterId, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Avatar.SetParameter_Injected(intPtr, parameterId, value);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00006CD0 File Offset: 0x00004ED0
		internal float GetAxisLength(int humanId)
		{
			return this.Internal_GetAxisLength(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00006CF0 File Offset: 0x00004EF0
		internal Quaternion GetPreRotation(int humanId)
		{
			return this.Internal_GetPreRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00006D10 File Offset: 0x00004F10
		internal Quaternion GetPostRotation(int humanId)
		{
			return this.Internal_GetPostRotation(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00006D30 File Offset: 0x00004F30
		internal Quaternion GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			return this.Internal_GetZYPostQ(HumanTrait.GetBoneIndexFromMono(humanId), parentQ, q);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00006D50 File Offset: 0x00004F50
		internal Quaternion GetZYRoll(int humanId, Vector3 uvw)
		{
			return this.Internal_GetZYRoll(HumanTrait.GetBoneIndexFromMono(humanId), uvw);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00006D70 File Offset: 0x00004F70
		internal Vector3 GetLimitSign(int humanId)
		{
			return this.Internal_GetLimitSign(HumanTrait.GetBoneIndexFromMono(humanId));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00006D90 File Offset: 0x00004F90
		[NativeMethod("GetAxisLength")]
		internal float Internal_GetAxisLength(int humanId)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Avatar.Internal_GetAxisLength_Injected(intPtr, humanId);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00006DB4 File Offset: 0x00004FB4
		[NativeMethod("GetPreRotation")]
		internal Quaternion Internal_GetPreRotation(int humanId)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Quaternion quaternion;
			Avatar.Internal_GetPreRotation_Injected(intPtr, humanId, out quaternion);
			return quaternion;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00006DDC File Offset: 0x00004FDC
		[NativeMethod("GetPostRotation")]
		internal Quaternion Internal_GetPostRotation(int humanId)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Quaternion quaternion;
			Avatar.Internal_GetPostRotation_Injected(intPtr, humanId, out quaternion);
			return quaternion;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00006E04 File Offset: 0x00005004
		[NativeMethod("GetZYPostQ")]
		internal Quaternion Internal_GetZYPostQ(int humanId, Quaternion parentQ, Quaternion q)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Quaternion quaternion;
			Avatar.Internal_GetZYPostQ_Injected(intPtr, humanId, ref parentQ, ref q, out quaternion);
			return quaternion;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00006E30 File Offset: 0x00005030
		[NativeMethod("GetZYRoll")]
		internal Quaternion Internal_GetZYRoll(int humanId, Vector3 uvw)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Quaternion quaternion;
			Avatar.Internal_GetZYRoll_Injected(intPtr, humanId, ref uvw, out quaternion);
			return quaternion;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00006E58 File Offset: 0x00005058
		[NativeMethod("GetLimitSign")]
		internal Vector3 Internal_GetLimitSign(int humanId)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Avatar>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Avatar.Internal_GetLimitSign_Injected(intPtr, humanId, out vector);
			return vector;
		}

		// Token: 0x06000308 RID: 776
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isValid_Injected(IntPtr _unity_self);

		// Token: 0x06000309 RID: 777
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isHuman_Injected(IntPtr _unity_self);

		// Token: 0x0600030A RID: 778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_humanDescription_Injected(IntPtr _unity_self, out HumanDescription ret);

		// Token: 0x0600030B RID: 779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMuscleMinMax_Injected(IntPtr _unity_self, int muscleId, float min, float max);

		// Token: 0x0600030C RID: 780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetParameter_Injected(IntPtr _unity_self, int parameterId, float value);

		// Token: 0x0600030D RID: 781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetAxisLength_Injected(IntPtr _unity_self, int humanId);

		// Token: 0x0600030E RID: 782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetPreRotation_Injected(IntPtr _unity_self, int humanId, out Quaternion ret);

		// Token: 0x0600030F RID: 783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetPostRotation_Injected(IntPtr _unity_self, int humanId, out Quaternion ret);

		// Token: 0x06000310 RID: 784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetZYPostQ_Injected(IntPtr _unity_self, int humanId, [In] ref Quaternion parentQ, [In] ref Quaternion q, out Quaternion ret);

		// Token: 0x06000311 RID: 785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetZYRoll_Injected(IntPtr _unity_self, int humanId, [In] ref Vector3 uvw, out Quaternion ret);

		// Token: 0x06000312 RID: 786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetLimitSign_Injected(IntPtr _unity_self, int humanId, out Vector3 ret);
	}
}
