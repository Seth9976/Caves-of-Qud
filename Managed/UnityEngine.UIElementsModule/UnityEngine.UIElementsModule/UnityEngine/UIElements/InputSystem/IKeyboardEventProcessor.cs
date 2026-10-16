using System;

namespace UnityEngine.UIElements.InputSystem
{
	// Token: 0x0200060B RID: 1547
	internal interface IKeyboardEventProcessor
	{
		// Token: 0x06002DD1 RID: 11729
		void OnEnable();

		// Token: 0x06002DD2 RID: 11730
		void OnDisable();

		// Token: 0x06002DD3 RID: 11731
		void ProcessKeyboardEvents(InputSystemEventSystem eventSystem);
	}
}
