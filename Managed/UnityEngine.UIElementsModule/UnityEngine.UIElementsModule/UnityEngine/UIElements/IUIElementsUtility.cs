using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200050B RID: 1291
	internal interface IUIElementsUtility
	{
		// Token: 0x06002818 RID: 10264
		bool TakeCapture();

		// Token: 0x06002819 RID: 10265
		bool ReleaseCapture();

		// Token: 0x0600281A RID: 10266
		bool ProcessEvent(int instanceID, IntPtr nativeEventPtr, ref bool eventHandled);

		// Token: 0x0600281B RID: 10267
		bool CleanupRoots();

		// Token: 0x0600281C RID: 10268
		bool EndContainerGUIFromException(Exception exception);

		// Token: 0x0600281D RID: 10269
		bool MakeCurrentIMGUIContainerDirty();

		// Token: 0x0600281E RID: 10270
		void UpdateSchedulers();

		// Token: 0x0600281F RID: 10271
		void RequestRepaintForPanels(Action<ScriptableObject> repaintCallback);
	}
}
