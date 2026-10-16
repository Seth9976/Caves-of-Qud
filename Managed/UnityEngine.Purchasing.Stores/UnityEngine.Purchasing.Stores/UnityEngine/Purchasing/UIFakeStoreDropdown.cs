using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200006D RID: 109
	internal class UIFakeStoreDropdown
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000837C File Offset: 0x0000657C
		public void DoPopup(int windowID)
		{
			if (this.m_Options != null)
			{
				this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box, Array.Empty<GUILayoutOption>());
				int count = this.m_Options.Count;
				for (int i = 0; i < count; i++)
				{
					if (GUILayout.Button(this.m_Options[i], Array.Empty<GUILayoutOption>()))
					{
						this.OnOptionSelected(i);
					}
				}
				GUILayout.EndScrollView();
				return;
			}
			GUILayout.Label("Error! No Dropdown options set!", Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008413 File Offset: 0x00006613
		private void OnOptionSelected(int optionIndex)
		{
			Action<int, string> onDropdown = this.m_OnDropdown;
			if (onDropdown == null)
			{
				return;
			}
			onDropdown(optionIndex, this.m_Options[optionIndex]);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008432 File Offset: 0x00006632
		internal void SetOptions(List<string> options)
		{
			this.m_Options = new List<string>(options);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008440 File Offset: 0x00006640
		internal void SetSelectionAction(Action<int, string> onDropdown)
		{
			this.m_OnDropdown = onDropdown;
		}

		// Token: 0x04000120 RID: 288
		private List<string> m_Options;

		// Token: 0x04000121 RID: 289
		private Action<int, string> m_OnDropdown;

		// Token: 0x04000122 RID: 290
		private Vector2 scrollPosition;
	}
}
