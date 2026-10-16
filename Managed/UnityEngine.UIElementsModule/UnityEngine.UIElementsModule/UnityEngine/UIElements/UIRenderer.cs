using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F4 RID: 756
	[NativeType(Header = "Modules/UIElements/Core/Native/Renderer/UIRenderer.h")]
	public sealed class UIRenderer : Renderer
	{
		// Token: 0x06001584 RID: 5508 RVA: 0x00055974 File Offset: 0x00053B74
		internal void SetNativeData(int safeFrameIndex, int cmdListIndex, Material mat)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<UIRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			UIRenderer.SetNativeData_Injected(intPtr, safeFrameIndex, cmdListIndex, Object.MarshalledUnityObject.Marshal<Material>(mat));
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000559A0 File Offset: 0x00053BA0
		[RequiredByNativeCode]
		private static void OnRenderNodeExecute(UIRenderer renderer, int safeFrameIndex, int cmdListIndex)
		{
			bool flag = renderer.skipRendering;
			if (!flag)
			{
				List<CommandList>[] array = renderer.commandLists;
				List<CommandList> list = ((array != null) ? array[safeFrameIndex] : null);
				bool flag2 = list != null && cmdListIndex < list.Count;
				if (flag2)
				{
					CommandList commandList = list[cmdListIndex];
					if (commandList != null)
					{
						commandList.Execute();
					}
				}
			}
		}

		// Token: 0x06001587 RID: 5511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNativeData_Injected(IntPtr _unity_self, int safeFrameIndex, int cmdListIndex, IntPtr mat);

		// Token: 0x04000B9D RID: 2973
		internal volatile List<CommandList>[] commandLists;

		// Token: 0x04000B9E RID: 2974
		internal volatile bool skipRendering;
	}
}
