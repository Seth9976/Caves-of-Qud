using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001E7 RID: 487
	[StaticAccessor("GetLODGroupManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Graphics/LOD/LODGroup.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODUtility.h")]
	[NativeHeader("Runtime/Graphics/LOD/LODGroupManager.h")]
	public class LODGroup : Component
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00023CCC File Offset: 0x00021ECC
		// (set) Token: 0x0600152F RID: 5423 RVA: 0x00023CF4 File Offset: 0x00021EF4
		public Vector3 localReferencePoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				LODGroup.get_localReferencePoint_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LODGroup.set_localReferencePoint_Injected(intPtr, ref value);
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x00023D18 File Offset: 0x00021F18
		// (set) Token: 0x06001531 RID: 5425 RVA: 0x00023D3C File Offset: 0x00021F3C
		public float size
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LODGroup.get_size_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LODGroup.set_size_Injected(intPtr, value);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00023D60 File Offset: 0x00021F60
		public int lodCount
		{
			[NativeMethod("GetLODCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LODGroup.get_lodCount_Injected(intPtr);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x00023D84 File Offset: 0x00021F84
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x00023DA8 File Offset: 0x00021FA8
		public bool lastLODBillboard
		{
			[NativeMethod("GetLastLODIsBillboard")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LODGroup.get_lastLODBillboard_Injected(intPtr);
			}
			[NativeMethod("SetLastLODIsBillboard")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LODGroup.set_lastLODBillboard_Injected(intPtr, value);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00023DCC File Offset: 0x00021FCC
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x00023DF0 File Offset: 0x00021FF0
		public LODFadeMode fadeMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LODGroup.get_fadeMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LODGroup.set_fadeMode_Injected(intPtr, value);
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00023E14 File Offset: 0x00022014
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x00023E38 File Offset: 0x00022038
		public bool animateCrossFading
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LODGroup.get_animateCrossFading_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LODGroup.set_animateCrossFading_Injected(intPtr, value);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00023E5C File Offset: 0x0002205C
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x00023E80 File Offset: 0x00022080
		public bool enabled
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return LODGroup.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LODGroup.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00023EA4 File Offset: 0x000220A4
		[FreeFunction("UpdateLODGroupBoundingBox", HasExplicitThis = true)]
		public void RecalculateBounds()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LODGroup.RecalculateBounds_Injected(intPtr);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x00023EC8 File Offset: 0x000220C8
		[FreeFunction("GetLODs_Binding", HasExplicitThis = true)]
		public LOD[] GetLODs()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return LODGroup.GetLODs_Injected(intPtr);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00023EEA File Offset: 0x000220EA
		[Obsolete("Use SetLODs instead.")]
		public void SetLODS(LOD[] lods)
		{
			this.SetLODs(lods);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00023EF8 File Offset: 0x000220F8
		[FreeFunction("SetLODs_Binding", HasExplicitThis = true)]
		public void SetLODs([Unmarshalled] LOD[] lods)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LODGroup.SetLODs_Injected(intPtr, lods);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00023F1C File Offset: 0x0002211C
		[FreeFunction("ForceLODLevel", HasExplicitThis = true)]
		public void ForceLOD(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			LODGroup.ForceLOD_Injected(intPtr, index);
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001540 RID: 5440
		// (set) Token: 0x06001541 RID: 5441
		[StaticAccessor("GetLODGroupManager()")]
		public static extern float crossFadeAnimationDuration
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00023F40 File Offset: 0x00022140
		internal Vector3 worldReferencePoint
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LODGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				LODGroup.get_worldReferencePoint_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x06001544 RID: 5444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_localReferencePoint_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06001545 RID: 5445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_localReferencePoint_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06001546 RID: 5446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_size_Injected(IntPtr _unity_self);

		// Token: 0x06001547 RID: 5447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, float value);

		// Token: 0x06001548 RID: 5448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_lodCount_Injected(IntPtr _unity_self);

		// Token: 0x06001549 RID: 5449
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_lastLODBillboard_Injected(IntPtr _unity_self);

		// Token: 0x0600154A RID: 5450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lastLODBillboard_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600154B RID: 5451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LODFadeMode get_fadeMode_Injected(IntPtr _unity_self);

		// Token: 0x0600154C RID: 5452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fadeMode_Injected(IntPtr _unity_self, LODFadeMode value);

		// Token: 0x0600154D RID: 5453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_animateCrossFading_Injected(IntPtr _unity_self);

		// Token: 0x0600154E RID: 5454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_animateCrossFading_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600154F RID: 5455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x06001550 RID: 5456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06001551 RID: 5457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateBounds_Injected(IntPtr _unity_self);

		// Token: 0x06001552 RID: 5458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LOD[] GetLODs_Injected(IntPtr _unity_self);

		// Token: 0x06001553 RID: 5459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLODs_Injected(IntPtr _unity_self, LOD[] lods);

		// Token: 0x06001554 RID: 5460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ForceLOD_Injected(IntPtr _unity_self, int index);

		// Token: 0x06001555 RID: 5461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_worldReferencePoint_Injected(IntPtr _unity_self, out Vector3 ret);
	}
}
