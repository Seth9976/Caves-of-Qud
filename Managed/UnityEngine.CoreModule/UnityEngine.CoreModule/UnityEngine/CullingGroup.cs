using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200012C RID: 300
	[NativeHeader("Runtime/Export/Camera/CullingGroup.bindings.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class CullingGroup : IDisposable
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x0000E30F File Offset: 0x0000C50F
		public CullingGroup()
		{
			this.m_Ptr = CullingGroup.Init(this);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0000E32C File Offset: 0x0000C52C
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					this.FinalizerFailure();
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0000E374 File Offset: 0x0000C574
		[FreeFunction("CullingGroup_Bindings::Dispose", HasExplicitThis = true)]
		private void DisposeInternal()
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.DisposeInternal_Injected(intPtr);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0000E396 File Offset: 0x0000C596
		public void Dispose()
		{
			this.DisposeInternal();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public CullingGroup.StateChanged onStateChanged
		{
			get
			{
				return this.m_OnStateChanged;
			}
			set
			{
				this.m_OnStateChanged = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public bool enabled
		{
			get
			{
				IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return CullingGroup.get_enabled_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CullingGroup.set_enabled_Injected(intPtr, value);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0000E418 File Offset: 0x0000C618
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0000E440 File Offset: 0x0000C640
		public Camera targetCamera
		{
			get
			{
				IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Camera>(CullingGroup.get_targetCamera_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				CullingGroup.set_targetCamera_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Camera>(value));
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0000E468 File Offset: 0x0000C668
		public void SetBoundingSpheres([Unmarshalled] BoundingSphere[] array)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.SetBoundingSpheres_Injected(intPtr, array);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000E48C File Offset: 0x0000C68C
		public void SetBoundingSphereCount(int count)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.SetBoundingSphereCount_Injected(intPtr, count);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000E4B0 File Offset: 0x0000C6B0
		public void EraseSwapBack(int index)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.EraseSwapBack_Injected(intPtr, index);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0000E4D3 File Offset: 0x0000C6D3
		public static void EraseSwapBack<T>(int index, T[] myArray, ref int size)
		{
			size--;
			myArray[index] = myArray[size];
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public int QueryIndices(bool visible, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, -1, CullingQueryOptions.IgnoreDistance, result, firstIndex);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0000E50C File Offset: 0x0000C70C
		public int QueryIndices(int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(false, distanceIndex, CullingQueryOptions.IgnoreVisibility, result, firstIndex);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0000E52C File Offset: 0x0000C72C
		public int QueryIndices(bool visible, int distanceIndex, int[] result, int firstIndex)
		{
			return this.QueryIndices(visible, distanceIndex, CullingQueryOptions.Normal, result, firstIndex);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0000E54C File Offset: 0x0000C74C
		[FreeFunction("CullingGroup_Bindings::QueryIndices", HasExplicitThis = true)]
		[NativeThrows]
		private unsafe int QueryIndices(bool visible, int distanceIndex, CullingQueryOptions options, int[] result, int firstIndex)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = new Span<int>(result);
			int num;
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = CullingGroup.QueryIndices_Injected(intPtr, visible, distanceIndex, options, ref managedSpanWrapper, firstIndex);
			}
			return num;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0000E59C File Offset: 0x0000C79C
		[FreeFunction("CullingGroup_Bindings::IsVisible", HasExplicitThis = true)]
		[NativeThrows]
		public bool IsVisible(int index)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CullingGroup.IsVisible_Injected(intPtr, index);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
		[FreeFunction("CullingGroup_Bindings::GetDistance", HasExplicitThis = true)]
		[NativeThrows]
		public int GetDistance(int index)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return CullingGroup.GetDistance_Injected(intPtr, index);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
		[FreeFunction("CullingGroup_Bindings::SetBoundingDistances", HasExplicitThis = true)]
		public unsafe void SetBoundingDistances(float[] distances)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(distances);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				CullingGroup.SetBoundingDistances_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0000E62C File Offset: 0x0000C82C
		[FreeFunction("CullingGroup_Bindings::SetDistanceReferencePoint", HasExplicitThis = true)]
		private void SetDistanceReferencePoint_InternalVector3(Vector3 point)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.SetDistanceReferencePoint_InternalVector3_Injected(intPtr, ref point);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0000E650 File Offset: 0x0000C850
		[NativeMethod("SetDistanceReferenceTransform")]
		private void SetDistanceReferencePoint_InternalTransform(Transform transform)
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.SetDistanceReferencePoint_InternalTransform_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform));
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000E678 File Offset: 0x0000C878
		public void SetDistanceReferencePoint(Vector3 point)
		{
			this.SetDistanceReferencePoint_InternalVector3(point);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000E683 File Offset: 0x0000C883
		public void SetDistanceReferencePoint(Transform transform)
		{
			this.SetDistanceReferencePoint_InternalTransform(transform);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000E690 File Offset: 0x0000C890
		[SecuritySafeCritical]
		[RequiredByNativeCode]
		private unsafe static void SendEvents(CullingGroup cullingGroup, IntPtr eventsPtr, int count)
		{
			CullingGroupEvent* ptr = (CullingGroupEvent*)eventsPtr.ToPointer();
			bool flag = cullingGroup.m_OnStateChanged == null;
			if (!flag)
			{
				for (int i = 0; i < count; i++)
				{
					cullingGroup.m_OnStateChanged(ptr[i]);
				}
			}
		}

		// Token: 0x06000858 RID: 2136
		[FreeFunction("CullingGroup_Bindings::Init")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init(object scripting);

		// Token: 0x06000859 RID: 2137 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		[FreeFunction("CullingGroup_Bindings::FinalizerFailure", HasExplicitThis = true, IsThreadSafe = true)]
		private void FinalizerFailure()
		{
			IntPtr intPtr = CullingGroup.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CullingGroup.FinalizerFailure_Injected(intPtr);
		}

		// Token: 0x0600085A RID: 2138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisposeInternal_Injected(IntPtr _unity_self);

		// Token: 0x0600085B RID: 2139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_enabled_Injected(IntPtr _unity_self);

		// Token: 0x0600085C RID: 2140
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enabled_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600085D RID: 2141
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_targetCamera_Injected(IntPtr _unity_self);

		// Token: 0x0600085E RID: 2142
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_targetCamera_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600085F RID: 2143
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoundingSpheres_Injected(IntPtr _unity_self, BoundingSphere[] array);

		// Token: 0x06000860 RID: 2144
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoundingSphereCount_Injected(IntPtr _unity_self, int count);

		// Token: 0x06000861 RID: 2145
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EraseSwapBack_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000862 RID: 2146
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int QueryIndices_Injected(IntPtr _unity_self, bool visible, int distanceIndex, CullingQueryOptions options, ref ManagedSpanWrapper result, int firstIndex);

		// Token: 0x06000863 RID: 2147
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsVisible_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000864 RID: 2148
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDistance_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000865 RID: 2149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoundingDistances_Injected(IntPtr _unity_self, ref ManagedSpanWrapper distances);

		// Token: 0x06000866 RID: 2150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDistanceReferencePoint_InternalVector3_Injected(IntPtr _unity_self, [In] ref Vector3 point);

		// Token: 0x06000867 RID: 2151
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDistanceReferencePoint_InternalTransform_Injected(IntPtr _unity_self, IntPtr transform);

		// Token: 0x06000868 RID: 2152
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FinalizerFailure_Injected(IntPtr _unity_self);

		// Token: 0x04000409 RID: 1033
		internal IntPtr m_Ptr;

		// Token: 0x0400040A RID: 1034
		private CullingGroup.StateChanged m_OnStateChanged = null;

		// Token: 0x0200012D RID: 301
		// (Invoke) Token: 0x0600086A RID: 2154
		public delegate void StateChanged(CullingGroupEvent sphere);

		// Token: 0x0200012E RID: 302
		internal static class BindingsMarshaller
		{
			// Token: 0x0600086D RID: 2157 RVA: 0x0000E706 File Offset: 0x0000C906
			public static IntPtr ConvertToNative(CullingGroup cullingGroup)
			{
				return cullingGroup.m_Ptr;
			}
		}
	}
}
