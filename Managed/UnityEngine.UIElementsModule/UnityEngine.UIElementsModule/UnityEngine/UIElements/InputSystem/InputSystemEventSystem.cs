using System;

namespace UnityEngine.UIElements.InputSystem
{
	// Token: 0x0200060A RID: 1546
	[AddComponentMenu("UI Toolkit/Input System Event System (UI Toolkit)")]
	public class InputSystemEventSystem : MonoBehaviour
	{
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x000A288D File Offset: 0x000A0A8D
		// (set) Token: 0x06002DCC RID: 11724 RVA: 0x000A2895 File Offset: 0x000A0A95
		public bool isAppFocused { get; private set; } = true;

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x000A289E File Offset: 0x000A0A9E
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x000A28A6 File Offset: 0x000A0AA6
		[Obsolete("EventSystem no longer supports input override for legacy input. Install Input System package for full input binding functionality.")]
		public InputWrapper inputOverride { get; set; }

		// Token: 0x06002DCF RID: 11727 RVA: 0x000A28AF File Offset: 0x000A0AAF
		protected InputSystemEventSystem()
		{
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000A28C0 File Offset: 0x000A0AC0
		private void OnApplicationFocus(bool hasFocus)
		{
			this.isAppFocused = hasFocus;
		}
	}
}
