using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001EB RID: 491
	internal static class DragAndDropUtility
	{
		// Token: 0x06000F2B RID: 3883 RVA: 0x000432F8 File Offset: 0x000414F8
		internal static IDragAndDrop GetDragAndDrop(IPanel panel)
		{
			bool flag = panel.contextType == ContextType.Player;
			IDragAndDrop dragAndDrop2;
			if (flag)
			{
				IDragAndDrop dragAndDrop;
				if ((dragAndDrop = DragAndDropUtility.s_DragAndDropPlayMode) == null)
				{
					dragAndDrop = (DragAndDropUtility.s_DragAndDropPlayMode = new DefaultDragAndDropClient());
				}
				dragAndDrop2 = dragAndDrop;
			}
			else
			{
				IDragAndDrop dragAndDrop3;
				if ((dragAndDrop3 = DragAndDropUtility.s_DragAndDropEditor) == null)
				{
					IDragAndDrop dragAndDrop5;
					if (DragAndDropUtility.s_MakeDragAndDropClientFunc == null)
					{
						IDragAndDrop dragAndDrop4 = new DefaultDragAndDropClient();
						dragAndDrop5 = dragAndDrop4;
					}
					else
					{
						dragAndDrop5 = DragAndDropUtility.s_MakeDragAndDropClientFunc();
					}
					dragAndDrop3 = (DragAndDropUtility.s_DragAndDropEditor = dragAndDrop5);
				}
				dragAndDrop2 = dragAndDrop3;
			}
			return dragAndDrop2;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00043358 File Offset: 0x00041558
		internal static void RegisterMakeClientFunc(Func<IDragAndDrop> makeClient)
		{
			DragAndDropUtility.s_MakeDragAndDropClientFunc = makeClient;
			DragAndDropUtility.s_DragAndDropEditor = null;
		}

		// Token: 0x04000945 RID: 2373
		private static Func<IDragAndDrop> s_MakeDragAndDropClientFunc;

		// Token: 0x04000946 RID: 2374
		private static IDragAndDrop s_DragAndDropEditor;

		// Token: 0x04000947 RID: 2375
		private static IDragAndDrop s_DragAndDropPlayMode;
	}
}
