using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[NativeType(Header = "Modules/Grid/Public/Grid.h")]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	public sealed class Grid : GridLayout
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Vector3 GetCellCenterLocal(Vector3Int position)
		{
			return base.CellToLocalInterpolated(position + base.GetLayoutCellCenter());
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207C File Offset: 0x0000027C
		public Vector3 GetCellCenterWorld(Vector3Int position)
		{
			return base.LocalToWorld(base.CellToLocalInterpolated(position + base.GetLayoutCellCenter()));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020AC File Offset: 0x000002AC
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public new Vector3 cellSize
		{
			[FreeFunction("GridBindings::GetCellSize", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Grid.get_cellSize_Injected(intPtr, out vector);
				return vector;
			}
			[FreeFunction("GridBindings::SetCellSize", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Grid.set_cellSize_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020F8 File Offset: 0x000002F8
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002120 File Offset: 0x00000320
		public new Vector3 cellGap
		{
			[FreeFunction("GridBindings::GetCellGap", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Grid.get_cellGap_Injected(intPtr, out vector);
				return vector;
			}
			[FreeFunction("GridBindings::SetCellGap", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Grid.set_cellGap_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002168 File Offset: 0x00000368
		public new GridLayout.CellLayout cellLayout
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Grid.get_cellLayout_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Grid.set_cellLayout_Injected(intPtr, value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000218C File Offset: 0x0000038C
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000021B0 File Offset: 0x000003B0
		public new GridLayout.CellSwizzle cellSwizzle
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Grid.get_cellSwizzle_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Grid>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Grid.set_cellSwizzle_Injected(intPtr, value);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021D4 File Offset: 0x000003D4
		[FreeFunction("GridBindings::CellSwizzle")]
		public static Vector3 Swizzle(GridLayout.CellSwizzle swizzle, Vector3 position)
		{
			Vector3 vector;
			Grid.Swizzle_Injected(swizzle, ref position, out vector);
			return vector;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		[FreeFunction("GridBindings::InverseCellSwizzle")]
		public static Vector3 InverseSwizzle(GridLayout.CellSwizzle swizzle, Vector3 position)
		{
			Vector3 vector;
			Grid.InverseSwizzle_Injected(swizzle, ref position, out vector);
			return vector;
		}

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cellSize_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cellSize_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_cellGap_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000011 RID: 17
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cellGap_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000012 RID: 18
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GridLayout.CellLayout get_cellLayout_Injected(IntPtr _unity_self);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cellLayout_Injected(IntPtr _unity_self, GridLayout.CellLayout value);

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GridLayout.CellSwizzle get_cellSwizzle_Injected(IntPtr _unity_self);

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cellSwizzle_Injected(IntPtr _unity_self, GridLayout.CellSwizzle value);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Swizzle_Injected(GridLayout.CellSwizzle swizzle, [In] ref Vector3 position, out Vector3 ret);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InverseSwizzle_Injected(GridLayout.CellSwizzle swizzle, [In] ref Vector3 position, out Vector3 ret);
	}
}
