using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200015E RID: 350
	[NativeHeader("Runtime/Graphics/CustomRenderTextureManager.h")]
	public static class CustomRenderTextureManager
	{
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000B42 RID: 2882 RVA: 0x00014D2C File Offset: 0x00012F2C
		// (remove) Token: 0x06000B43 RID: 2883 RVA: 0x00014D60 File Offset: 0x00012F60
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<CustomRenderTexture> textureLoaded;

		// Token: 0x06000B44 RID: 2884 RVA: 0x00014D93 File Offset: 0x00012F93
		[RequiredByNativeCode]
		private static void InvokeOnTextureLoaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureLoaded;
			if (action != null)
			{
				action(source);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000B45 RID: 2885 RVA: 0x00014DA8 File Offset: 0x00012FA8
		// (remove) Token: 0x06000B46 RID: 2886 RVA: 0x00014DDC File Offset: 0x00012FDC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<CustomRenderTexture> textureUnloaded;

		// Token: 0x06000B47 RID: 2887 RVA: 0x00014E0F File Offset: 0x0001300F
		[RequiredByNativeCode]
		private static void InvokeOnTextureUnloaded_Internal(CustomRenderTexture source)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.textureUnloaded;
			if (action != null)
			{
				action(source);
			}
		}

		// Token: 0x06000B48 RID: 2888
		[FreeFunction(Name = "CustomRenderTextureManagerScripting::GetAllCustomRenderTextures", HasExplicitThis = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetAllCustomRenderTextures(List<CustomRenderTexture> currentCustomRenderTextures);

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000B49 RID: 2889 RVA: 0x00014E24 File Offset: 0x00013024
		// (remove) Token: 0x06000B4A RID: 2890 RVA: 0x00014E58 File Offset: 0x00013058
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<CustomRenderTexture, int> updateTriggered;

		// Token: 0x06000B4B RID: 2891 RVA: 0x00014E8B File Offset: 0x0001308B
		internal static void InvokeTriggerUpdate(CustomRenderTexture crt, int updateCount)
		{
			Action<CustomRenderTexture, int> action = CustomRenderTextureManager.updateTriggered;
			if (action != null)
			{
				action(crt, updateCount);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000B4C RID: 2892 RVA: 0x00014EA0 File Offset: 0x000130A0
		// (remove) Token: 0x06000B4D RID: 2893 RVA: 0x00014ED4 File Offset: 0x000130D4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<CustomRenderTexture> initializeTriggered;

		// Token: 0x06000B4E RID: 2894 RVA: 0x00014F07 File Offset: 0x00013107
		internal static void InvokeTriggerInitialize(CustomRenderTexture crt)
		{
			Action<CustomRenderTexture> action = CustomRenderTextureManager.initializeTriggered;
			if (action != null)
			{
				action(crt);
			}
		}
	}
}
