using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200016A RID: 362
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public struct RenderBuffer
	{
		// Token: 0x06000BC3 RID: 3011
		[FreeFunction(Name = "RenderBufferScripting::SetLoadAction", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetLoadAction(RenderBufferLoadAction action);

		// Token: 0x06000BC4 RID: 3012
		[FreeFunction(Name = "RenderBufferScripting::SetStoreAction", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void SetStoreAction(RenderBufferStoreAction action);

		// Token: 0x06000BC5 RID: 3013
		[FreeFunction(Name = "RenderBufferScripting::GetLoadAction", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern RenderBufferLoadAction GetLoadAction();

		// Token: 0x06000BC6 RID: 3014
		[FreeFunction(Name = "RenderBufferScripting::GetStoreAction", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern RenderBufferStoreAction GetStoreAction();

		// Token: 0x06000BC7 RID: 3015
		[FreeFunction(Name = "RenderBufferScripting::GetNativeRenderBufferPtr", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern IntPtr GetNativeRenderBufferPtr();

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00015AC8 File Offset: 0x00013CC8
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x00015AE0 File Offset: 0x00013CE0
		internal RenderBufferLoadAction loadAction
		{
			get
			{
				return this.GetLoadAction();
			}
			set
			{
				this.SetLoadAction(value);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00015AEC File Offset: 0x00013CEC
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00015B04 File Offset: 0x00013D04
		internal RenderBufferStoreAction storeAction
		{
			get
			{
				return this.GetStoreAction();
			}
			set
			{
				this.SetStoreAction(value);
			}
		}

		// Token: 0x040004A3 RID: 1187
		internal int m_RenderTextureInstanceID;

		// Token: 0x040004A4 RID: 1188
		internal IntPtr m_BufferPtr;
	}
}
