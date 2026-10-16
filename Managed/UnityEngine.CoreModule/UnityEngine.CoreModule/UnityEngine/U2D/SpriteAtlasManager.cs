using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.U2D
{
	// Token: 0x020005B9 RID: 1465
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlasManager.h")]
	[StaticAccessor("GetSpriteAtlasManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/2D/SpriteAtlas/SpriteAtlas.h")]
	public class SpriteAtlasManager
	{
		// Token: 0x14000041 RID: 65
		// (add) Token: 0x0600382D RID: 14381 RVA: 0x0006E620 File Offset: 0x0006C820
		// (remove) Token: 0x0600382E RID: 14382 RVA: 0x0006E654 File Offset: 0x0006C854
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<string, Action<SpriteAtlas>> atlasRequested;

		// Token: 0x0600382F RID: 14383 RVA: 0x0006E688 File Offset: 0x0006C888
		[RequiredByNativeCode]
		private static bool RequestAtlas(string tag)
		{
			bool flag = SpriteAtlasManager.atlasRequested != null;
			bool flag2;
			if (flag)
			{
				SpriteAtlasManager.atlasRequested(tag, new Action<SpriteAtlas>(SpriteAtlasManager.Register));
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06003830 RID: 14384 RVA: 0x0006E6C4 File Offset: 0x0006C8C4
		// (remove) Token: 0x06003831 RID: 14385 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<SpriteAtlas> atlasRegistered;

		// Token: 0x06003832 RID: 14386 RVA: 0x0006E72B File Offset: 0x0006C92B
		[RequiredByNativeCode]
		private static void PostRegisteredAtlas(SpriteAtlas spriteAtlas)
		{
			Action<SpriteAtlas> action = SpriteAtlasManager.atlasRegistered;
			if (action != null)
			{
				action(spriteAtlas);
			}
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x0006E740 File Offset: 0x0006C940
		internal static void Register(SpriteAtlas spriteAtlas)
		{
			SpriteAtlasManager.Register_Injected(Object.MarshalledUnityObject.Marshal<SpriteAtlas>(spriteAtlas));
		}

		// Token: 0x06003835 RID: 14389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Register_Injected(IntPtr spriteAtlas);
	}
}
