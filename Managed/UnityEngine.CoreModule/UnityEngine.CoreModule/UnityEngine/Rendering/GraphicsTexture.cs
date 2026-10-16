using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004CD RID: 1229
	[NativeHeader("Runtime/Export/Graphics/GraphicsTexture.bindings.h")]
	[NativeType("Runtime/Graphics/Texture/GraphicsTexture.h")]
	[UsedByNativeCode]
	public class GraphicsTexture : IDisposable
	{
		// Token: 0x06002E26 RID: 11814 RVA: 0x0005785F File Offset: 0x00055A5F
		private GraphicsTexture(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x00057870 File Offset: 0x00055A70
		~GraphicsTexture()
		{
			this.Dispose(false);
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x000578A4 File Offset: 0x00055AA4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x000578B8 File Offset: 0x00055AB8
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ReleaseBuffer();
			}
			else
			{
				this.ReleaseBufferOnMain();
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x000578E6 File Offset: 0x00055AE6
		public GraphicsTexture(GraphicsTextureDescriptor desc)
		{
			this.m_Ptr = GraphicsTexture.InitBuffer(desc);
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x000578FC File Offset: 0x00055AFC
		internal void UploadData(IntPtr data, int size)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new ObjectDisposedException("GraphicsTexture");
			}
			bool flag2 = data == IntPtr.Zero || size == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to GraphicsTexture.UploadData");
			}
			else
			{
				this.UploadBuffer(data, (ulong)((long)size));
			}
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x0005795C File Offset: 0x00055B5C
		internal void UploadData(byte[] data)
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				throw new ObjectDisposedException("GraphicsTexture");
			}
			bool flag2 = data == null || data.Length == 0;
			if (flag2)
			{
				Debug.LogError("No texture data provided to GraphicsTexture.UploadData");
			}
			else
			{
				this.UploadBuffer_Array(data);
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x000579B0 File Offset: 0x00055BB0
		public GraphicsTextureDescriptor descriptor
		{
			[FreeFunction("GraphicsTexture_Bindings::GetDescriptor", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
			get
			{
				IntPtr intPtr = GraphicsTexture.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GraphicsTextureDescriptor graphicsTextureDescriptor;
				GraphicsTexture.get_descriptor_Injected(intPtr, out graphicsTextureDescriptor);
				return graphicsTextureDescriptor;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000579D8 File Offset: 0x00055BD8
		public GraphicsTextureState state
		{
			[FreeFunction("GraphicsTexture_Bindings::GetState", HasExplicitThis = true, IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = GraphicsTexture.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsTexture.get_state_Injected(intPtr);
			}
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000579FC File Offset: 0x00055BFC
		[FreeFunction("GraphicsTexture_Bindings::GetActive")]
		private static GraphicsTexture GetActive()
		{
			IntPtr active_Injected = GraphicsTexture.GetActive_Injected();
			return (active_Injected == 0) ? null : GraphicsTexture.BindingsMarshaller.ConvertToManaged(active_Injected);
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x00057A1C File Offset: 0x00055C1C
		[FreeFunction("RenderTextureScripting::SetActive")]
		private static void SetActive(GraphicsTexture target)
		{
			GraphicsTexture.SetActive_Injected((target == null) ? ((IntPtr)0) : GraphicsTexture.BindingsMarshaller.ConvertToNative(target));
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06002E31 RID: 11825 RVA: 0x00057A40 File Offset: 0x00055C40
		// (set) Token: 0x06002E32 RID: 11826 RVA: 0x00057A57 File Offset: 0x00055C57
		public static GraphicsTexture active
		{
			get
			{
				return GraphicsTexture.GetActive();
			}
			set
			{
				GraphicsTexture.SetActive(value);
			}
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x00057A64 File Offset: 0x00055C64
		[FreeFunction("GraphicsTexture_Bindings::InitBuffer", ThrowsException = true)]
		private static IntPtr InitBuffer(GraphicsTextureDescriptor desc)
		{
			return GraphicsTexture.InitBuffer_Injected(ref desc);
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x00057A78 File Offset: 0x00055C78
		[FreeFunction("GraphicsTexture_Bindings::ReleaseBuffer", HasExplicitThis = true, IsThreadSafe = true)]
		private void ReleaseBuffer()
		{
			IntPtr intPtr = GraphicsTexture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsTexture.ReleaseBuffer_Injected(intPtr);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x00057A9C File Offset: 0x00055C9C
		[FreeFunction("GraphicsTexture_Bindings::ReleaseBufferOnMain", HasExplicitThis = true, IsThreadSafe = true)]
		private void ReleaseBufferOnMain()
		{
			IntPtr intPtr = GraphicsTexture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsTexture.ReleaseBufferOnMain_Injected(intPtr);
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x00057AC0 File Offset: 0x00055CC0
		[FreeFunction("GraphicsTexture_Bindings::UploadBuffer", HasExplicitThis = true, ThrowsException = true)]
		private bool UploadBuffer(IntPtr data, ulong size)
		{
			IntPtr intPtr = GraphicsTexture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsTexture.UploadBuffer_Injected(intPtr, data, size);
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x00057AE4 File Offset: 0x00055CE4
		[FreeFunction("GraphicsTexture_Bindings::UploadBuffer", HasExplicitThis = true, ThrowsException = true)]
		private unsafe bool UploadBuffer_Array(byte[] data)
		{
			IntPtr intPtr = GraphicsTexture.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<byte> span = new Span<byte>(data);
			bool flag;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = GraphicsTexture.UploadBuffer_Array_Injected(intPtr, ref managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x06002E38 RID: 11832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_descriptor_Injected(IntPtr _unity_self, out GraphicsTextureDescriptor ret);

		// Token: 0x06002E39 RID: 11833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsTextureState get_state_Injected(IntPtr _unity_self);

		// Token: 0x06002E3A RID: 11834
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetActive_Injected();

		// Token: 0x06002E3B RID: 11835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetActive_Injected(IntPtr target);

		// Token: 0x06002E3C RID: 11836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitBuffer_Injected([In] ref GraphicsTextureDescriptor desc);

		// Token: 0x06002E3D RID: 11837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseBuffer_Injected(IntPtr _unity_self);

		// Token: 0x06002E3E RID: 11838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseBufferOnMain_Injected(IntPtr _unity_self);

		// Token: 0x06002E3F RID: 11839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UploadBuffer_Injected(IntPtr _unity_self, IntPtr data, ulong size);

		// Token: 0x06002E40 RID: 11840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UploadBuffer_Array_Injected(IntPtr _unity_self, ref ManagedSpanWrapper data);

		// Token: 0x04000ECA RID: 3786
		internal IntPtr m_Ptr;

		// Token: 0x020004CE RID: 1230
		internal static class BindingsMarshaller
		{
			// Token: 0x06002E41 RID: 11841 RVA: 0x00057B2C File Offset: 0x00055D2C
			public static IntPtr ConvertToNative(GraphicsTexture graphicsTexture)
			{
				return graphicsTexture.m_Ptr;
			}

			// Token: 0x06002E42 RID: 11842 RVA: 0x00057B34 File Offset: 0x00055D34
			public static GraphicsTexture ConvertToManaged(IntPtr ptr)
			{
				return new GraphicsTexture(ptr);
			}
		}
	}
}
