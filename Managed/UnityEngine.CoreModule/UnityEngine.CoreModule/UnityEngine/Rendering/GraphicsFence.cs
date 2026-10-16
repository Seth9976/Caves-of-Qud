using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x020004C6 RID: 1222
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/GPUFence.h")]
	public struct GraphicsFence
	{
		// Token: 0x06002DC8 RID: 11720 RVA: 0x00056E80 File Offset: 0x00055080
		internal static SynchronisationStageFlags TranslateSynchronizationStageToFlags(SynchronisationStage s)
		{
			return (s == SynchronisationStage.VertexProcessing) ? SynchronisationStageFlags.VertexProcessing : SynchronisationStageFlags.PixelProcessing;
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x00056E9C File Offset: 0x0005509C
		public bool passed
		{
			get
			{
				this.Validate();
				bool flag = !SystemInfo.supportsGraphicsFence;
				if (flag)
				{
					throw new NotSupportedException("Cannot determine if this GraphicsFence has passed as this platform has not implemented GraphicsFences.");
				}
				bool flag2 = this.m_FenceType == GraphicsFenceType.AsyncQueueSynchronisation && !SystemInfo.supportsAsyncCompute;
				if (flag2)
				{
					throw new NotSupportedException("Cannot determine if this AsyncQueueSynchronisation GraphicsFence has passed as this platform does not support async compute.");
				}
				bool flag3 = !this.IsFencePending();
				return flag3 || GraphicsFence.HasFencePassed_Internal(this.m_Ptr);
			}
		}

		// Token: 0x06002DCA RID: 11722
		[FreeFunction("GPUFenceInternals::HasFencePassed_Internal")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFencePassed_Internal(IntPtr fencePtr);

		// Token: 0x06002DCB RID: 11723 RVA: 0x00056F0C File Offset: 0x0005510C
		internal void InitPostAllocation()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			if (flag)
			{
				bool supportsGraphicsFence = SystemInfo.supportsGraphicsFence;
				if (supportsGraphicsFence)
				{
					throw new NullReferenceException("The internal fence ptr is null, this should not be possible for fences that have been correctly constructed using Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
				}
				this.m_Version = this.GetPlatformNotSupportedVersion();
			}
			else
			{
				this.m_Version = GraphicsFence.GetVersionNumber(this.m_Ptr);
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x00056F64 File Offset: 0x00055164
		internal bool IsFencePending()
		{
			bool flag = this.m_Ptr == IntPtr.Zero;
			return !flag && this.m_Version == GraphicsFence.GetVersionNumber(this.m_Ptr);
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x00056FA4 File Offset: 0x000551A4
		internal void Validate()
		{
			bool flag = this.m_Version == 0 || (SystemInfo.supportsGraphicsFence && this.m_Version == this.GetPlatformNotSupportedVersion());
			if (flag)
			{
				throw new InvalidOperationException("This GraphicsFence object has not been correctly constructed see Graphics.CreateGraphicsFence() or CommandBuffer.CreateGraphicsFence()");
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x00056FE4 File Offset: 0x000551E4
		private int GetPlatformNotSupportedVersion()
		{
			return -1;
		}

		// Token: 0x06002DCF RID: 11727
		[FreeFunction("GPUFenceInternals::GetVersionNumber")]
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVersionNumber(IntPtr fencePtr);

		// Token: 0x04000E9E RID: 3742
		internal IntPtr m_Ptr;

		// Token: 0x04000E9F RID: 3743
		internal int m_Version;

		// Token: 0x04000EA0 RID: 3744
		internal GraphicsFenceType m_FenceType;
	}
}
