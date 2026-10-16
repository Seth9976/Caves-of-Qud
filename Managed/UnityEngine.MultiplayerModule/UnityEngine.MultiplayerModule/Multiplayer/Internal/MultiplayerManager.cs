using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Multiplayer.Internal
{
	// Token: 0x02000003 RID: 3
	[StaticAccessor("GetMultiplayerManager()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/Multiplayer/MultiplayerManager.h")]
	internal static class MultiplayerManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2
		public static extern MultiplayerRoleFlags activeMultiplayerRoleMask
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		public static MultiplayerRoleFlags GetMultiplayerRoleMaskForGameObject(GameObject gameObject)
		{
			return MultiplayerManager.GetMultiplayerRoleMaskForGameObject_Injected(Object.MarshalledUnityObject.Marshal<GameObject>(gameObject));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public static MultiplayerRoleFlags GetMultiplayerRoleMaskForComponent(Component component)
		{
			return MultiplayerManager.GetMultiplayerRoleMaskForComponent_Injected(Object.MarshalledUnityObject.Marshal<Component>(component));
		}

		// Token: 0x06000005 RID: 5
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MultiplayerRoleFlags GetMultiplayerRoleMaskForGameObject_Injected(IntPtr gameObject);

		// Token: 0x06000006 RID: 6
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MultiplayerRoleFlags GetMultiplayerRoleMaskForComponent_Injected(IntPtr component);
	}
}
