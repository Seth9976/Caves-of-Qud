using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200015D RID: 349
	[NativeHeader("Runtime/Graphics/Billboard/BillboardRenderer.h")]
	public sealed class BillboardRenderer : Renderer
	{
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00014CDC File Offset: 0x00012EDC
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x00014D04 File Offset: 0x00012F04
		public BillboardAsset billboard
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<BillboardAsset>(BillboardRenderer.get_billboard_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<BillboardRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BillboardRenderer.set_billboard_Injected(intPtr, Object.MarshalledUnityObject.Marshal<BillboardAsset>(value));
			}
		}

		// Token: 0x06000B40 RID: 2880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_billboard_Injected(IntPtr _unity_self);

		// Token: 0x06000B41 RID: 2881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_billboard_Injected(IntPtr _unity_self, IntPtr value);
	}
}
