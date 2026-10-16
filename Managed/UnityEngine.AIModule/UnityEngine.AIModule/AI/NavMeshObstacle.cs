using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/AI/Components/NavMeshObstacle.bindings.h")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshObstacle.html")]
	[MovedFrom("UnityEngine")]
	public sealed class NavMeshObstacle : Behaviour
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000356C File Offset: 0x0000176C
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00003590 File Offset: 0x00001790
		public float height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshObstacle.get_height_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_height_Injected(intPtr, value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000035B4 File Offset: 0x000017B4
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000035D8 File Offset: 0x000017D8
		public float radius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshObstacle.get_radius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000035FC File Offset: 0x000017FC
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00003624 File Offset: 0x00001824
		public Vector3 velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshObstacle.get_velocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_velocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003648 File Offset: 0x00001848
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000366C File Offset: 0x0000186C
		public bool carving
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshObstacle.get_carving_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_carving_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003690 File Offset: 0x00001890
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000036B4 File Offset: 0x000018B4
		public bool carveOnlyStationary
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshObstacle.get_carveOnlyStationary_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_carveOnlyStationary_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000036D8 File Offset: 0x000018D8
		// (set) Token: 0x060000FD RID: 253 RVA: 0x000036FC File Offset: 0x000018FC
		[NativeProperty("MoveThreshold")]
		public float carvingMoveThreshold
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshObstacle.get_carvingMoveThreshold_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_carvingMoveThreshold_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003720 File Offset: 0x00001920
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00003744 File Offset: 0x00001944
		[NativeProperty("TimeToStationary")]
		public float carvingTimeToStationary
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshObstacle.get_carvingTimeToStationary_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_carvingTimeToStationary_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003768 File Offset: 0x00001968
		// (set) Token: 0x06000101 RID: 257 RVA: 0x0000378C File Offset: 0x0000198C
		public NavMeshObstacleShape shape
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshObstacle.get_shape_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_shape_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000037B0 File Offset: 0x000019B0
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000037D8 File Offset: 0x000019D8
		public Vector3 center
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshObstacle.get_center_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_center_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000037FC File Offset: 0x000019FC
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00003824 File Offset: 0x00001A24
		public Vector3 size
		{
			[FreeFunction("NavMeshObstacleScriptBindings::GetSize", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshObstacle.get_size_Injected(intPtr, out vector);
				return vector;
			}
			[FreeFunction("NavMeshObstacleScriptBindings::SetSize", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshObstacle.set_size_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00003848 File Offset: 0x00001A48
		[FreeFunction("NavMeshObstacleScriptBindings::FitExtents", HasExplicitThis = true)]
		internal void FitExtents()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshObstacle>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshObstacle.FitExtents_Injected(intPtr);
		}

		// Token: 0x06000108 RID: 264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_height_Injected(IntPtr _unity_self);

		// Token: 0x06000109 RID: 265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_height_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600010A RID: 266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_radius_Injected(IntPtr _unity_self);

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_velocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_velocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_carving_Injected(IntPtr _unity_self);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_carving_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_carveOnlyStationary_Injected(IntPtr _unity_self);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_carveOnlyStationary_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_carvingMoveThreshold_Injected(IntPtr _unity_self);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_carvingMoveThreshold_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_carvingTimeToStationary_Injected(IntPtr _unity_self);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_carvingTimeToStationary_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshObstacleShape get_shape_Injected(IntPtr _unity_self);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_shape_Injected(IntPtr _unity_self, NavMeshObstacleShape value);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_center_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_center_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600011A RID: 282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_size_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600011B RID: 283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_size_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FitExtents_Injected(IntPtr _unity_self);
	}
}
