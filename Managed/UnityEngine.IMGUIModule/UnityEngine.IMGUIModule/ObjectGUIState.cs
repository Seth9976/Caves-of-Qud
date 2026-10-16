using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003E RID: 62
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	internal class ObjectGUIState : IDisposable
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0001148B File Offset: 0x0000F68B
		public ObjectGUIState()
		{
			this.m_Ptr = ObjectGUIState.Internal_Create();
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000114B4 File Offset: 0x0000F6B4
		~ObjectGUIState()
		{
			this.Destroy();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000114E4 File Offset: 0x0000F6E4
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ObjectGUIState.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000497 RID: 1175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x06000498 RID: 1176
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x0400013B RID: 315
		internal IntPtr m_Ptr;

		// Token: 0x0200003F RID: 63
		internal static class BindingsMarshaller
		{
			// Token: 0x06000499 RID: 1177 RVA: 0x0001151F File Offset: 0x0000F71F
			public static IntPtr ConvertToNative(ObjectGUIState objectGUIState)
			{
				return objectGUIState.m_Ptr;
			}
		}
	}
}
