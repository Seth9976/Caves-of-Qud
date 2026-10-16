using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000149 RID: 329
	internal interface ISavedState
	{
		// Token: 0x06001223 RID: 4643
		void StaticDisposeCurrentState();

		// Token: 0x06001224 RID: 4644
		void RestoreSavedState();
	}
}
