using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeType(Header = "Modules/Grid/Public/Grid.h")]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[RequireComponent(typeof(Transform))]
	public class GridLayout : Behaviour
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002210 File Offset: 0x00000410
		public Vector3 cellSize
		{
			[FreeFunction("GridLayoutBindings::GetCellSize", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				GridLayout.get_cellSize_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002238 File Offset: 0x00000438
		public Vector3 cellGap
		{
			[FreeFunction("GridLayoutBindings::GetCellGap", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				GridLayout.get_cellGap_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002260 File Offset: 0x00000460
		public GridLayout.CellLayout cellLayout
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GridLayout.get_cellLayout_Injected(intPtr);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002284 File Offset: 0x00000484
		public GridLayout.CellSwizzle cellSwizzle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GridLayout.get_cellSwizzle_Injected(intPtr);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022A8 File Offset: 0x000004A8
		[FreeFunction("GridLayoutBindings::GetBoundsLocal", HasExplicitThis = true)]
		public Bounds GetBoundsLocal(Vector3Int cellPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Bounds bounds;
			GridLayout.GetBoundsLocal_Injected(intPtr, ref cellPosition, out bounds);
			return bounds;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022D0 File Offset: 0x000004D0
		public Bounds GetBoundsLocal(Vector3 origin, Vector3 size)
		{
			return this.GetBoundsLocalOriginSize(origin, size);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022EC File Offset: 0x000004EC
		[FreeFunction("GridLayoutBindings::GetBoundsLocalOriginSize", HasExplicitThis = true)]
		private Bounds GetBoundsLocalOriginSize(Vector3 origin, Vector3 size)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Bounds bounds;
			GridLayout.GetBoundsLocalOriginSize_Injected(intPtr, ref origin, ref size, out bounds);
			return bounds;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002318 File Offset: 0x00000518
		[FreeFunction("GridLayoutBindings::CellToLocal", HasExplicitThis = true)]
		public Vector3 CellToLocal(Vector3Int cellPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			GridLayout.CellToLocal_Injected(intPtr, ref cellPosition, out vector);
			return vector;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002340 File Offset: 0x00000540
		[FreeFunction("GridLayoutBindings::LocalToCell", HasExplicitThis = true)]
		public Vector3Int LocalToCell(Vector3 localPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3Int vector3Int;
			GridLayout.LocalToCell_Injected(intPtr, ref localPosition, out vector3Int);
			return vector3Int;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002368 File Offset: 0x00000568
		[FreeFunction("GridLayoutBindings::CellToLocalInterpolated", HasExplicitThis = true)]
		public Vector3 CellToLocalInterpolated(Vector3 cellPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			GridLayout.CellToLocalInterpolated_Injected(intPtr, ref cellPosition, out vector);
			return vector;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002390 File Offset: 0x00000590
		[FreeFunction("GridLayoutBindings::LocalToCellInterpolated", HasExplicitThis = true)]
		public Vector3 LocalToCellInterpolated(Vector3 localPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			GridLayout.LocalToCellInterpolated_Injected(intPtr, ref localPosition, out vector);
			return vector;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023B8 File Offset: 0x000005B8
		[FreeFunction("GridLayoutBindings::CellToWorld", HasExplicitThis = true)]
		public Vector3 CellToWorld(Vector3Int cellPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			GridLayout.CellToWorld_Injected(intPtr, ref cellPosition, out vector);
			return vector;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023E0 File Offset: 0x000005E0
		[FreeFunction("GridLayoutBindings::WorldToCell", HasExplicitThis = true)]
		public Vector3Int WorldToCell(Vector3 worldPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3Int vector3Int;
			GridLayout.WorldToCell_Injected(intPtr, ref worldPosition, out vector3Int);
			return vector3Int;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002408 File Offset: 0x00000608
		[FreeFunction("GridLayoutBindings::LocalToWorld", HasExplicitThis = true)]
		public Vector3 LocalToWorld(Vector3 localPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			GridLayout.LocalToWorld_Injected(intPtr, ref localPosition, out vector);
			return vector;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002430 File Offset: 0x00000630
		[FreeFunction("GridLayoutBindings::WorldToLocal", HasExplicitThis = true)]
		public Vector3 WorldToLocal(Vector3 worldPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			GridLayout.WorldToLocal_Injected(intPtr, ref worldPosition, out vector);
			return vector;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002458 File Offset: 0x00000658
		[FreeFunction("GridLayoutBindings::GetLayoutCellCenter", HasExplicitThis = true)]
		public Vector3 GetLayoutCellCenter()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GridLayout>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			GridLayout.GetLayoutCellCenter_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000247D File Offset: 0x0000067D
		[RequiredByNativeCode]
		private void DoNothing()
		{
		}

		// Token: 0x0600002A RID: 42
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cellSize_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cellGap_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GridLayout.CellLayout get_cellLayout_Injected(IntPtr _unity_self);

		// Token: 0x0600002D RID: 45
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GridLayout.CellSwizzle get_cellSwizzle_Injected(IntPtr _unity_self);

		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBoundsLocal_Injected(IntPtr _unity_self, [In] ref Vector3Int cellPosition, out Bounds ret);

		// Token: 0x0600002F RID: 47
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBoundsLocalOriginSize_Injected(IntPtr _unity_self, [In] ref Vector3 origin, [In] ref Vector3 size, out Bounds ret);

		// Token: 0x06000030 RID: 48
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CellToLocal_Injected(IntPtr _unity_self, [In] ref Vector3Int cellPosition, out Vector3 ret);

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LocalToCell_Injected(IntPtr _unity_self, [In] ref Vector3 localPosition, out Vector3Int ret);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CellToLocalInterpolated_Injected(IntPtr _unity_self, [In] ref Vector3 cellPosition, out Vector3 ret);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LocalToCellInterpolated_Injected(IntPtr _unity_self, [In] ref Vector3 localPosition, out Vector3 ret);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CellToWorld_Injected(IntPtr _unity_self, [In] ref Vector3Int cellPosition, out Vector3 ret);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WorldToCell_Injected(IntPtr _unity_self, [In] ref Vector3 worldPosition, out Vector3Int ret);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void LocalToWorld_Injected(IntPtr _unity_self, [In] ref Vector3 localPosition, out Vector3 ret);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WorldToLocal_Injected(IntPtr _unity_self, [In] ref Vector3 worldPosition, out Vector3 ret);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLayoutCellCenter_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x02000004 RID: 4
		public enum CellLayout
		{
			// Token: 0x04000002 RID: 2
			Rectangle,
			// Token: 0x04000003 RID: 3
			Hexagon,
			// Token: 0x04000004 RID: 4
			Isometric,
			// Token: 0x04000005 RID: 5
			IsometricZAsY
		}

		// Token: 0x02000005 RID: 5
		public enum CellSwizzle
		{
			// Token: 0x04000007 RID: 7
			XYZ,
			// Token: 0x04000008 RID: 8
			XZY,
			// Token: 0x04000009 RID: 9
			YXZ,
			// Token: 0x0400000A RID: 10
			YZX,
			// Token: 0x0400000B RID: 11
			ZXY,
			// Token: 0x0400000C RID: 12
			ZYX
		}
	}
}
