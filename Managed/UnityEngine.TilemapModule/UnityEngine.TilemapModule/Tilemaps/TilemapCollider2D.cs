using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000019 RID: 25
	[NativeType(Header = "Modules/Tilemap/Public/TilemapCollider2D.h")]
	[RequireComponent(typeof(Tilemap))]
	public sealed class TilemapCollider2D : Collider2D
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00004064 File Offset: 0x00002264
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00004088 File Offset: 0x00002288
		public bool useDelaunayMesh
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapCollider2D.get_useDelaunayMesh_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapCollider2D.set_useDelaunayMesh_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000040AC File Offset: 0x000022AC
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000040D0 File Offset: 0x000022D0
		public uint maximumTileChangeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapCollider2D.get_maximumTileChangeCount_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapCollider2D.set_maximumTileChangeCount_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000040F4 File Offset: 0x000022F4
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00004118 File Offset: 0x00002318
		public float extrusionFactor
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapCollider2D.get_extrusionFactor_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				TilemapCollider2D.set_extrusionFactor_Injected(intPtr, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000413C File Offset: 0x0000233C
		public bool hasTilemapChanges
		{
			[NativeMethod("HasTilemapChanges")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return TilemapCollider2D.get_hasTilemapChanges_Injected(intPtr);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004160 File Offset: 0x00002360
		[NativeMethod(Name = "ProcessTileChangeQueue")]
		public void ProcessTilemapChanges()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<TilemapCollider2D>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			TilemapCollider2D.ProcessTilemapChanges_Injected(intPtr);
		}

		// Token: 0x0600014F RID: 335
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useDelaunayMesh_Injected(IntPtr _unity_self);

		// Token: 0x06000150 RID: 336
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useDelaunayMesh_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000151 RID: 337
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_maximumTileChangeCount_Injected(IntPtr _unity_self);

		// Token: 0x06000152 RID: 338
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maximumTileChangeCount_Injected(IntPtr _unity_self, uint value);

		// Token: 0x06000153 RID: 339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_extrusionFactor_Injected(IntPtr _unity_self);

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_extrusionFactor_Injected(IntPtr _unity_self, float value);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasTilemapChanges_Injected(IntPtr _unity_self);

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ProcessTilemapChanges_Injected(IntPtr _unity_self);
	}
}
