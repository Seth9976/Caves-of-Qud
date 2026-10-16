using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200050F RID: 1295
	public static class UIToolkitInputConfiguration
	{
		// Token: 0x0600284C RID: 10316 RVA: 0x0009368F File Offset: 0x0009188F
		public static void SetRuntimeInputBackend(UIToolkitInputBackendOption backend)
		{
			UIElementsRuntimeUtility.defaultEventSystem.useInputForUI = backend != UIToolkitInputBackendOption.LegacyBackend;
		}
	}
}
