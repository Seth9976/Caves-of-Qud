using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E2 RID: 482
	[NativeHeader("Runtime/Graphics/Mesh/MeshRenderer.h")]
	public class MeshRenderer : Renderer
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x00002925 File Offset: 0x00000B25
		[RequiredByNativeCode]
		private void DontStripMeshRenderer()
		{
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x00023AD8 File Offset: 0x00021CD8
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x00023B00 File Offset: 0x00021D00
		public Mesh additionalVertexStreams
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshRenderer.get_additionalVertexStreams_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshRenderer.set_additionalVertexStreams_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00023B28 File Offset: 0x00021D28
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x00023B50 File Offset: 0x00021D50
		public Mesh enlightenVertexStream
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Mesh>(MeshRenderer.get_enlightenVertexStream_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				MeshRenderer.set_enlightenVertexStream_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Mesh>(value));
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00023B78 File Offset: 0x00021D78
		public int subMeshStartIndex
		{
			[NativeName("GetSubMeshStartIndex")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<MeshRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MeshRenderer.get_subMeshStartIndex_Injected(intPtr);
			}
		}

		// Token: 0x0600151D RID: 5405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_additionalVertexStreams_Injected(IntPtr _unity_self);

		// Token: 0x0600151E RID: 5406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_additionalVertexStreams_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x0600151F RID: 5407
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_enlightenVertexStream_Injected(IntPtr _unity_self);

		// Token: 0x06001520 RID: 5408
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_enlightenVertexStream_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x06001521 RID: 5409
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_subMeshStartIndex_Injected(IntPtr _unity_self);
	}
}
