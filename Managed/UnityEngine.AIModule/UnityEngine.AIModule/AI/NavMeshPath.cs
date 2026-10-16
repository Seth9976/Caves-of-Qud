using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200001A RID: 26
	[NativeHeader("Modules/AI/NavMeshPath.bindings.h")]
	[MovedFrom("UnityEngine")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class NavMeshPath
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00004528 File Offset: 0x00002728
		public NavMeshPath()
		{
			this.m_Ptr = NavMeshPath.InitializeNavMeshPath();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00004540 File Offset: 0x00002740
		~NavMeshPath()
		{
			NavMeshPath.DestroyNavMeshPath(this.m_Ptr);
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x060001E2 RID: 482
		[FreeFunction("NavMeshPathScriptBindings::InitializeNavMeshPath")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitializeNavMeshPath();

		// Token: 0x060001E3 RID: 483
		[FreeFunction("NavMeshPathScriptBindings::DestroyNavMeshPath", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyNavMeshPath(IntPtr ptr);

		// Token: 0x060001E4 RID: 484 RVA: 0x00004580 File Offset: 0x00002780
		[FreeFunction("NavMeshPathScriptBindings::GetCornersNonAlloc", HasExplicitThis = true)]
		public unsafe int GetCornersNonAlloc([Out] Vector3[] results)
		{
			int cornersNonAlloc_Injected;
			try
			{
				IntPtr intPtr = NavMeshPath.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (results != null)
				{
					fixed (Vector3[] array = results)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				cornersNonAlloc_Injected = NavMeshPath.GetCornersNonAlloc_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				Vector3[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector3>(ref array);
			}
			return cornersNonAlloc_Injected;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000045E0 File Offset: 0x000027E0
		[FreeFunction("NavMeshPathScriptBindings::CalculateCornersInternal", HasExplicitThis = true)]
		private Vector3[] CalculateCornersInternal()
		{
			Vector3[] array2;
			try
			{
				IntPtr intPtr = NavMeshPath.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				NavMeshPath.CalculateCornersInternal_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector3[] array;
				blittableArrayWrapper.Unmarshal<Vector3>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00004624 File Offset: 0x00002824
		[FreeFunction("NavMeshPathScriptBindings::ClearCornersInternal", HasExplicitThis = true)]
		private void ClearCornersInternal()
		{
			IntPtr intPtr = NavMeshPath.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshPath.ClearCornersInternal_Injected(intPtr);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00004646 File Offset: 0x00002846
		public void ClearCorners()
		{
			this.ClearCornersInternal();
			this.m_Corners = null;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00004658 File Offset: 0x00002858
		private void CalculateCorners()
		{
			bool flag = this.m_Corners == null;
			if (flag)
			{
				this.m_Corners = this.CalculateCornersInternal();
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00004680 File Offset: 0x00002880
		public Vector3[] corners
		{
			get
			{
				this.CalculateCorners();
				return this.m_Corners;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000046A0 File Offset: 0x000028A0
		public NavMeshPathStatus status
		{
			get
			{
				IntPtr intPtr = NavMeshPath.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshPath.get_status_Injected(intPtr);
			}
		}

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCornersNonAlloc_Injected(IntPtr _unity_self, out BlittableArrayWrapper results);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CalculateCornersInternal_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearCornersInternal_Injected(IntPtr _unity_self);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshPathStatus get_status_Injected(IntPtr _unity_self);

		// Token: 0x04000043 RID: 67
		internal IntPtr m_Ptr;

		// Token: 0x04000044 RID: 68
		internal Vector3[] m_Corners;

		// Token: 0x0200001B RID: 27
		internal static class BindingsMarshaller
		{
			// Token: 0x060001EF RID: 495 RVA: 0x000046C2 File Offset: 0x000028C2
			public static IntPtr ConvertToNative(NavMeshPath navMeshPath)
			{
				return navMeshPath.m_Ptr;
			}
		}
	}
}
