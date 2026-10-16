using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200006E RID: 110
	internal class UIFakeStoreWindow : MonoBehaviour
	{
		// Token: 0x06000287 RID: 647 RVA: 0x00008454 File Offset: 0x00006654
		private void OnGUI()
		{
			Rect rect = this.CreateCenteredWindowRect();
			if (this.m_DoDropdown)
			{
				GUI.Window(1, rect, new GUI.WindowFunction(this.m_Dropdown.DoPopup), "Select Response");
				return;
			}
			GUI.Window(0, rect, new GUI.WindowFunction(this.DoMainGUI), "Fake Store");
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000084A8 File Offset: 0x000066A8
		private Rect CreateCenteredWindowRect()
		{
			float num = (float)Screen.width * 0.6f;
			float num2 = (float)Screen.height * 0.6f;
			float num3 = ((float)Screen.width - num) / 2f;
			float num4 = ((float)Screen.height - num2) / 2f;
			return new Rect(num3, num4, num, num2);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000084F4 File Offset: 0x000066F4
		private void DoMainGUI(int windowID)
		{
			this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box, Array.Empty<GUILayoutOption>());
			GUILayout.Label(this.m_QueryText, Array.Empty<GUILayoutOption>());
			if (this.m_DropdownEnabled && GUILayout.Button(this.m_LastSelectedOptionText, Array.Empty<GUILayoutOption>()))
			{
				this.DoDropDown();
			}
			if (GUILayout.Button(this.m_OkText, Array.Empty<GUILayoutOption>()))
			{
				this.OnOkClicked();
			}
			if (this.m_CancelEnabled && GUILayout.Button(this.m_CancelText, Array.Empty<GUILayoutOption>()))
			{
				this.OnCancelClicked();
			}
			GUILayout.EndScrollView();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000085A4 File Offset: 0x000067A4
		private void DoDropDown()
		{
			this.m_DoDropdown = true;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000085AD File Offset: 0x000067AD
		private void OnOkClicked()
		{
			Action onOk = this.m_OnOk;
			if (onOk == null)
			{
				return;
			}
			onOk();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000085BF File Offset: 0x000067BF
		private void OnCancelClicked()
		{
			Action onCancel = this.m_OnCancel;
			if (onCancel == null)
			{
				return;
			}
			onCancel();
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000085D1 File Offset: 0x000067D1
		internal void ConfigureMainDialogText(string queryText, string okText, string cancelText)
		{
			this.m_QueryText = queryText;
			this.m_OkText = okText;
			this.m_CancelText = cancelText;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x000085E8 File Offset: 0x000067E8
		internal void ConfigureDropdownOptions(List<string> options)
		{
			this.m_Dropdown.SetOptions(options);
			this.m_Dropdown.SetSelectionAction(new Action<int, string>(this.OnDropdown));
			if (options.Count > 0)
			{
				this.m_LastSelectedOptionText = options.First<string>();
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00008622 File Offset: 0x00006822
		private void OnDropdown(int index, string selectionText)
		{
			this.m_LastSelectedOptionText = selectionText;
			Action<int> onDropdown = this.m_OnDropdown;
			if (onDropdown != null)
			{
				onDropdown(index);
			}
			this.m_DoDropdown = false;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00008644 File Offset: 0x00006844
		internal void AssignCallbacks(Action onOk, Action onCancel, Action<int> onDropdown)
		{
			this.m_OnOk = onOk;
			this.m_CancelEnabled = onCancel != null;
			this.m_OnCancel = onCancel;
			this.m_DropdownEnabled = onDropdown != null;
			this.m_OnDropdown = onDropdown;
		}

		// Token: 0x04000123 RID: 291
		private string m_QueryText;

		// Token: 0x04000124 RID: 292
		private string m_OkText;

		// Token: 0x04000125 RID: 293
		private string m_CancelText;

		// Token: 0x04000126 RID: 294
		private string m_LastSelectedOptionText;

		// Token: 0x04000127 RID: 295
		private Action m_OnOk;

		// Token: 0x04000128 RID: 296
		private Action m_OnCancel;

		// Token: 0x04000129 RID: 297
		private Action<int> m_OnDropdown;

		// Token: 0x0400012A RID: 298
		private bool m_CancelEnabled;

		// Token: 0x0400012B RID: 299
		private bool m_DropdownEnabled;

		// Token: 0x0400012C RID: 300
		private bool m_DoDropdown;

		// Token: 0x0400012D RID: 301
		private readonly UIFakeStoreDropdown m_Dropdown = new UIFakeStoreDropdown();

		// Token: 0x0400012E RID: 302
		private Vector2 scrollPosition;

		// Token: 0x0400012F RID: 303
		private const float k_MenuScreenRatio = 0.6f;
	}
}
