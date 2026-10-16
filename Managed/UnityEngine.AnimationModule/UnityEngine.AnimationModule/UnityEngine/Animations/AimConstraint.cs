using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000050 RID: 80
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/AimConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class AimConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x00007FFA File Offset: 0x000061FA
		private AimConstraint()
		{
			AimConstraint.Internal_Create(this);
		}

		// Token: 0x060003C2 RID: 962
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] AimConstraint self);

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000800C File Offset: 0x0000620C
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00008030 File Offset: 0x00006230
		public float weight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AimConstraint.get_weight_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_weight_Injected(intPtr, value);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x00008054 File Offset: 0x00006254
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00008078 File Offset: 0x00006278
		public bool constraintActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AimConstraint.get_constraintActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_constraintActive_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000809C File Offset: 0x0000629C
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x000080C0 File Offset: 0x000062C0
		public bool locked
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AimConstraint.get_locked_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_locked_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x000080E4 File Offset: 0x000062E4
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000810C File Offset: 0x0000630C
		public Vector3 rotationAtRest
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				AimConstraint.get_rotationAtRest_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_rotationAtRest_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00008130 File Offset: 0x00006330
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00008158 File Offset: 0x00006358
		public Vector3 rotationOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				AimConstraint.get_rotationOffset_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_rotationOffset_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000817C File Offset: 0x0000637C
		// (set) Token: 0x060003CE RID: 974 RVA: 0x000081A0 File Offset: 0x000063A0
		public Axis rotationAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AimConstraint.get_rotationAxis_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_rotationAxis_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000081C4 File Offset: 0x000063C4
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x000081EC File Offset: 0x000063EC
		public Vector3 aimVector
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				AimConstraint.get_aimVector_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_aimVector_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00008210 File Offset: 0x00006410
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00008238 File Offset: 0x00006438
		public Vector3 upVector
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				AimConstraint.get_upVector_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_upVector_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000825C File Offset: 0x0000645C
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00008284 File Offset: 0x00006484
		public Vector3 worldUpVector
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				AimConstraint.get_worldUpVector_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_worldUpVector_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x000082A8 File Offset: 0x000064A8
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x000082D0 File Offset: 0x000064D0
		public Transform worldUpObject
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(AimConstraint.get_worldUpObject_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_worldUpObject_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(value));
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000082F8 File Offset: 0x000064F8
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000831C File Offset: 0x0000651C
		public AimConstraint.WorldUpType worldUpType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AimConstraint.get_worldUpType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				AimConstraint.set_worldUpType_Injected(intPtr, value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00008340 File Offset: 0x00006540
		public int sourceCount
		{
			get
			{
				return AimConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00008358 File Offset: 0x00006558
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		private static int GetSourceCountInternal([NotNull] AimConstraint self)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			return AimConstraint.GetSourceCountInternal_Injected(intPtr);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00008390 File Offset: 0x00006590
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		public void GetSources([NotNull] List<ConstraintSource> sources)
		{
			if (sources == null)
			{
				ThrowHelper.ThrowArgumentNullException(sources, "sources");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AimConstraint.GetSources_Injected(intPtr, sources);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000083C4 File Offset: 0x000065C4
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			AimConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000083F0 File Offset: 0x000065F0
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		private static void SetSourcesInternal([NotNull] AimConstraint self, List<ConstraintSource> sources)
		{
			if (self == null)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(self);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(self, "self");
			}
			AimConstraint.SetSourcesInternal_Injected(intPtr, sources);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00008428 File Offset: 0x00006628
		public int AddSource(ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AimConstraint.AddSource_Injected(intPtr, ref source);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000844C File Offset: 0x0000664C
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00008460 File Offset: 0x00006660
		[NativeName("RemoveSource")]
		private void RemoveSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AimConstraint.RemoveSourceInternal_Injected(intPtr, index);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00008484 File Offset: 0x00006684
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000084A8 File Offset: 0x000066A8
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ConstraintSource constraintSource;
			AimConstraint.GetSourceInternal_Injected(intPtr, index, out constraintSource);
			return constraintSource;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000084CE File Offset: 0x000066CE
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000084E4 File Offset: 0x000066E4
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AimConstraint>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AimConstraint.SetSourceInternal_Injected(intPtr, index, ref source);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000850C File Offset: 0x0000670C
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The AimConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060003E6 RID: 998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_weight_Injected(IntPtr _unity_self);

		// Token: 0x060003E7 RID: 999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_weight_Injected(IntPtr _unity_self, float value);

		// Token: 0x060003E8 RID: 1000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_constraintActive_Injected(IntPtr _unity_self);

		// Token: 0x060003E9 RID: 1001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_constraintActive_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003EA RID: 1002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_locked_Injected(IntPtr _unity_self);

		// Token: 0x060003EB RID: 1003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_locked_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060003EC RID: 1004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationAtRest_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003ED RID: 1005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAtRest_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060003EE RID: 1006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rotationOffset_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003EF RID: 1007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationOffset_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060003F0 RID: 1008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Axis get_rotationAxis_Injected(IntPtr _unity_self);

		// Token: 0x060003F1 RID: 1009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rotationAxis_Injected(IntPtr _unity_self, Axis value);

		// Token: 0x060003F2 RID: 1010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_aimVector_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003F3 RID: 1011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_aimVector_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060003F4 RID: 1012
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_upVector_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003F5 RID: 1013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_upVector_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060003F6 RID: 1014
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldUpVector_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060003F7 RID: 1015
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldUpVector_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060003F8 RID: 1016
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_worldUpObject_Injected(IntPtr _unity_self);

		// Token: 0x060003F9 RID: 1017
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldUpObject_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060003FA RID: 1018
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AimConstraint.WorldUpType get_worldUpType_Injected(IntPtr _unity_self);

		// Token: 0x060003FB RID: 1019
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_worldUpType_Injected(IntPtr _unity_self, AimConstraint.WorldUpType value);

		// Token: 0x060003FC RID: 1020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal_Injected(IntPtr self);

		// Token: 0x060003FD RID: 1021
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSources_Injected(IntPtr _unity_self, List<ConstraintSource> sources);

		// Token: 0x060003FE RID: 1022
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal_Injected(IntPtr self, List<ConstraintSource> sources);

		// Token: 0x060003FF RID: 1023
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddSource_Injected(IntPtr _unity_self, [In] ref ConstraintSource source);

		// Token: 0x06000400 RID: 1024
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveSourceInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000401 RID: 1025
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourceInternal_Injected(IntPtr _unity_self, int index, out ConstraintSource ret);

		// Token: 0x06000402 RID: 1026
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourceInternal_Injected(IntPtr _unity_self, int index, [In] ref ConstraintSource source);

		// Token: 0x02000051 RID: 81
		public enum WorldUpType
		{
			// Token: 0x04000152 RID: 338
			SceneUp,
			// Token: 0x04000153 RID: 339
			ObjectUp,
			// Token: 0x04000154 RID: 340
			ObjectRotationUp,
			// Token: 0x04000155 RID: 341
			Vector,
			// Token: 0x04000156 RID: 342
			None
		}
	}
}
