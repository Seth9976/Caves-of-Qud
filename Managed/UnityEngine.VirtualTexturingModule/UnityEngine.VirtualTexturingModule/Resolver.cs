using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTextureResolver.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class Resolver : IDisposable
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000021E4 File Offset: 0x000003E4
		public Resolver()
		{
			bool flag = !UnityEngine.Rendering.VirtualTexturing.System.enabled;
			if (flag)
			{
				throw new InvalidOperationException("Virtual texturing is not enabled in the player settings.");
			}
			this.m_Ptr = Resolver.InitNative();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000222C File Offset: 0x0000042C
		~Resolver()
		{
			this.Dispose(false);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002260 File Offset: 0x00000460
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002274 File Offset: 0x00000474
		protected virtual void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Flush_Internal();
				Resolver.ReleaseNative(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000020 RID: 32
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InitNative();

		// Token: 0x06000021 RID: 33
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseNative(IntPtr ptr);

		// Token: 0x06000022 RID: 34 RVA: 0x000022B8 File Offset: 0x000004B8
		private void Flush_Internal()
		{
			IntPtr intPtr = Resolver.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Resolver.Flush_Internal_Injected(intPtr);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022DC File Offset: 0x000004DC
		private void Init_Internal(int width, int height)
		{
			IntPtr intPtr = Resolver.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Resolver.Init_Internal_Injected(intPtr, width, height);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002300 File Offset: 0x00000500
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002308 File Offset: 0x00000508
		public int CurrentWidth { get; private set; } = 0;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002311 File Offset: 0x00000511
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002319 File Offset: 0x00000519
		public int CurrentHeight { get; private set; } = 0;

		// Token: 0x06000028 RID: 40 RVA: 0x00002324 File Offset: 0x00000524
		public void UpdateSize(int width, int height)
		{
			bool flag = this.CurrentWidth != width || this.CurrentHeight != height;
			if (flag)
			{
				bool flag2 = width <= 0 || height <= 0;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Zero sized dimensions are invalid (width: {0}, height: {1}.", width, height));
				}
				this.CurrentWidth = width;
				this.CurrentHeight = height;
				this.Flush_Internal();
				this.Init_Internal(this.CurrentWidth, this.CurrentHeight);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023AC File Offset: 0x000005AC
		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt)
		{
			this.Process(cmd, rt, 0, this.CurrentWidth, 0, this.CurrentHeight, 0, 0);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023D4 File Offset: 0x000005D4
		public void Process(CommandBuffer cmd, RenderTargetIdentifier rt, int x, int width, int y, int height, int mip, int slice)
		{
			bool flag = cmd == null;
			if (flag)
			{
				throw new ArgumentNullException("cmd");
			}
			cmd.ProcessVTFeedback(rt, this.m_Ptr, slice, x, width, y, height, mip);
		}

		// Token: 0x0600002B RID: 43
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Flush_Internal_Injected(IntPtr _unity_self);

		// Token: 0x0600002C RID: 44
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Init_Internal_Injected(IntPtr _unity_self, int width, int height);

		// Token: 0x04000009 RID: 9
		internal IntPtr m_Ptr;

		// Token: 0x02000008 RID: 8
		internal static class BindingsMarshaller
		{
			// Token: 0x0600002D RID: 45 RVA: 0x0000240F File Offset: 0x0000060F
			public static IntPtr ConvertToNative(Resolver resolver)
			{
				return resolver.m_Ptr;
			}
		}
	}
}
