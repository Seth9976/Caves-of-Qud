using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200060E RID: 1550
	[VisibleToOtherModules(new string[] { "Unity.UIElements" })]
	[NativeHeader("Modules/UIElements/Core/Native/Renderer/UIRendererUtility.h")]
	internal class Utility
	{
		// Token: 0x06002DD4 RID: 11732 RVA: 0x000A28CC File Offset: 0x000A0ACC
		public static void SetVectorArray<T>(MaterialPropertyBlock props, int name, NativeSlice<T> vector4s) where T : struct
		{
			int num = vector4s.Length * vector4s.Stride / 16;
			Utility.SetVectorArray(props, name, new IntPtr(vector4s.GetUnsafePtr<T>()), num);
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002DD5 RID: 11733 RVA: 0x000A2904 File Offset: 0x000A0B04
		// (remove) Token: 0x06002DD6 RID: 11734 RVA: 0x000A2938 File Offset: 0x000A0B38
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<bool> GraphicsResourcesRecreate;

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002DD7 RID: 11735 RVA: 0x000A296C File Offset: 0x000A0B6C
		// (remove) Token: 0x06002DD8 RID: 11736 RVA: 0x000A29A0 File Offset: 0x000A0BA0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action EngineUpdate;

		// Token: 0x14000056 RID: 86
		// (add) Token: 0x06002DD9 RID: 11737 RVA: 0x000A29D4 File Offset: 0x000A0BD4
		// (remove) Token: 0x06002DDA RID: 11738 RVA: 0x000A2A08 File Offset: 0x000A0C08
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action FlushPendingResources;

		// Token: 0x06002DDB RID: 11739 RVA: 0x000A2A3B File Offset: 0x000A0C3B
		[RequiredByNativeCode]
		internal static void RaiseGraphicsResourcesRecreate(bool recreate)
		{
			Action<bool> graphicsResourcesRecreate = Utility.GraphicsResourcesRecreate;
			if (graphicsResourcesRecreate != null)
			{
				graphicsResourcesRecreate(recreate);
			}
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000A2A50 File Offset: 0x000A0C50
		[RequiredByNativeCode]
		internal static void RaiseEngineUpdate()
		{
			bool flag = Utility.EngineUpdate != null;
			if (flag)
			{
				Utility.EngineUpdate();
			}
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x000A2A77 File Offset: 0x000A0C77
		[RequiredByNativeCode]
		internal static void RaiseFlushPendingResources()
		{
			Action flushPendingResources = Utility.FlushPendingResources;
			if (flushPendingResources != null)
			{
				flushPendingResources();
			}
		}

		// Token: 0x06002DDE RID: 11742
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr AllocateBuffer(int elementCount, int elementStride, bool vertexBuffer);

		// Token: 0x06002DDF RID: 11743
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FreeBuffer(IntPtr buffer);

		// Token: 0x06002DE0 RID: 11744
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateBufferRanges(IntPtr buffer, IntPtr ranges, int rangeCount, int writeRangeStart, int writeRangeEnd);

		// Token: 0x06002DE1 RID: 11745 RVA: 0x000A2A8C File Offset: 0x000A0C8C
		[ThreadSafe]
		private static void SetVectorArray(MaterialPropertyBlock props, int name, IntPtr vector4s, int count)
		{
			Utility.SetVectorArray_Injected((props == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(props), name, vector4s, count);
		}

		// Token: 0x06002DE2 RID: 11746 RVA: 0x000A2AB0 File Offset: 0x000A0CB0
		[ThreadSafe]
		public unsafe static IntPtr GetVertexDeclaration(VertexAttributeDescriptor[] vertexAttributes)
		{
			Span<VertexAttributeDescriptor> span = new Span<VertexAttributeDescriptor>(vertexAttributes);
			IntPtr vertexDeclaration_Injected;
			fixed (VertexAttributeDescriptor* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				vertexDeclaration_Injected = Utility.GetVertexDeclaration_Injected(ref managedSpanWrapper);
			}
			return vertexDeclaration_Injected;
		}

		// Token: 0x06002DE3 RID: 11747
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void DrawRanges(IntPtr ib, IntPtr* vertexStreams, int streamCount, IntPtr ranges, int rangeCount, IntPtr vertexDecl);

		// Token: 0x06002DE4 RID: 11748 RVA: 0x000A2AE8 File Offset: 0x000A0CE8
		[ThreadSafe]
		public static void SetPropertyBlock(MaterialPropertyBlock props)
		{
			Utility.SetPropertyBlock_Injected((props == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(props));
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x000A2B0C File Offset: 0x000A0D0C
		[ThreadSafe]
		public static void SetScissorRect(RectInt scissorRect)
		{
			Utility.SetScissorRect_Injected(ref scissorRect);
		}

		// Token: 0x06002DE6 RID: 11750
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableScissor();

		// Token: 0x06002DE7 RID: 11751
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsScissorEnabled();

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000A2B20 File Offset: 0x000A0D20
		[ThreadSafe]
		public static IntPtr CreateStencilState(StencilState stencilState)
		{
			return Utility.CreateStencilState_Injected(ref stencilState);
		}

		// Token: 0x06002DE9 RID: 11753
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetStencilState(IntPtr stencilState, int stencilRef);

		// Token: 0x06002DEA RID: 11754
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool HasMappedBufferRange();

		// Token: 0x06002DEB RID: 11755
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint InsertCPUFence();

		// Token: 0x06002DEC RID: 11756
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool CPUFencePassed(uint fence);

		// Token: 0x06002DED RID: 11757
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WaitForCPUFencePassed(uint fence);

		// Token: 0x06002DEE RID: 11758
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SyncRenderThread();

		// Token: 0x06002DEF RID: 11759 RVA: 0x000A2B34 File Offset: 0x000A0D34
		[ThreadSafe]
		public static RectInt GetActiveViewport()
		{
			RectInt rectInt;
			Utility.GetActiveViewport_Injected(out rectInt);
			return rectInt;
		}

		// Token: 0x06002DF0 RID: 11760
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainBegin();

		// Token: 0x06002DF1 RID: 11761
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ProfileDrawChainEnd();

		// Token: 0x06002DF2 RID: 11762
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void NotifyOfUIREvents(bool subscribe);

		// Token: 0x06002DF3 RID: 11763 RVA: 0x000A2B4C File Offset: 0x000A0D4C
		[ThreadSafe]
		public static Matrix4x4 GetUnityProjectionMatrix()
		{
			Matrix4x4 matrix4x;
			Utility.GetUnityProjectionMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x000A2B64 File Offset: 0x000A0D64
		[ThreadSafe]
		public static Matrix4x4 GetDeviceProjectionMatrix()
		{
			Matrix4x4 matrix4x;
			Utility.GetDeviceProjectionMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06002DF5 RID: 11765
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool DebugIsMainThread();

		// Token: 0x06002DF8 RID: 11768
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArray_Injected(IntPtr props, int name, IntPtr vector4s, int count);

		// Token: 0x06002DF9 RID: 11769
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetVertexDeclaration_Injected(ref ManagedSpanWrapper vertexAttributes);

		// Token: 0x06002DFA RID: 11770
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPropertyBlock_Injected(IntPtr props);

		// Token: 0x06002DFB RID: 11771
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetScissorRect_Injected([In] ref RectInt scissorRect);

		// Token: 0x06002DFC RID: 11772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateStencilState_Injected([In] ref StencilState stencilState);

		// Token: 0x06002DFD RID: 11773
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveViewport_Injected(out RectInt ret);

		// Token: 0x06002DFE RID: 11774
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetUnityProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06002DFF RID: 11775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDeviceProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x040014CA RID: 5322
		private static ProfilerMarker s_MarkerRaiseEngineUpdate = new ProfilerMarker("UIR.RaiseEngineUpdate");

		// Token: 0x0200060F RID: 1551
		internal enum GPUBufferType
		{
			// Token: 0x040014CC RID: 5324
			Vertex,
			// Token: 0x040014CD RID: 5325
			Index
		}

		// Token: 0x02000610 RID: 1552
		public class GPUBuffer<T> : IDisposable where T : struct
		{
			// Token: 0x06002E00 RID: 11776 RVA: 0x000A2B8A File Offset: 0x000A0D8A
			public GPUBuffer(int elementCount, Utility.GPUBufferType type)
			{
				this.elemCount = elementCount;
				this.elemStride = UnsafeUtility.SizeOf<T>();
				this.buffer = Utility.AllocateBuffer(elementCount, this.elemStride, type == Utility.GPUBufferType.Vertex);
			}

			// Token: 0x06002E01 RID: 11777 RVA: 0x000A2BBC File Offset: 0x000A0DBC
			public void Dispose()
			{
				Utility.FreeBuffer(this.buffer);
			}

			// Token: 0x06002E02 RID: 11778 RVA: 0x000A2BCB File Offset: 0x000A0DCB
			public void UpdateRanges(NativeSlice<GfxUpdateBufferRange> ranges, int rangesMin, int rangesMax)
			{
				Utility.UpdateBufferRanges(this.buffer, new IntPtr(ranges.GetUnsafePtr<GfxUpdateBufferRange>()), ranges.Length, rangesMin, rangesMax);
			}

			// Token: 0x17000B38 RID: 2872
			// (get) Token: 0x06002E03 RID: 11779 RVA: 0x000A2BF0 File Offset: 0x000A0DF0
			public int ElementStride
			{
				get
				{
					return this.elemStride;
				}
			}

			// Token: 0x17000B39 RID: 2873
			// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000A2C08 File Offset: 0x000A0E08
			public int Count
			{
				get
				{
					return this.elemCount;
				}
			}

			// Token: 0x17000B3A RID: 2874
			// (get) Token: 0x06002E05 RID: 11781 RVA: 0x000A2C20 File Offset: 0x000A0E20
			internal IntPtr BufferPointer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x040014CE RID: 5326
			private IntPtr buffer;

			// Token: 0x040014CF RID: 5327
			private int elemCount;

			// Token: 0x040014D0 RID: 5328
			private int elemStride;
		}
	}
}
