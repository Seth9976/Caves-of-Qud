using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000207 RID: 519
	[NativeHeader("Runtime/Export/Input/Cursor.bindings.h")]
	public class Cursor
	{
		// Token: 0x06001B49 RID: 6985 RVA: 0x00030B68 File Offset: 0x0002ED68
		private static void SetCursor(Texture2D texture, CursorMode cursorMode)
		{
			Cursor.SetCursor(texture, Vector2.zero, cursorMode);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x00030B78 File Offset: 0x0002ED78
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)
		{
			Cursor.SetCursor_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(texture), ref hotspot, cursorMode);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001B4B RID: 6987
		// (set) Token: 0x06001B4C RID: 6988
		public static extern bool visible
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001B4D RID: 6989
		// (set) Token: 0x06001B4E RID: 6990
		public static extern CursorLockMode lockState
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001B50 RID: 6992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetCursor_Injected(IntPtr texture, [In] ref Vector2 hotspot, CursorMode cursorMode);
	}
}
