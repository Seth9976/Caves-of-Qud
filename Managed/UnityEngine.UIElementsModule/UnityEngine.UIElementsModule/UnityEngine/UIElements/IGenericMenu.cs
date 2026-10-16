using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FA RID: 250
	internal interface IGenericMenu
	{
		// Token: 0x0600081B RID: 2075
		void AddItem(string itemName, bool isChecked, Action action);

		// Token: 0x0600081C RID: 2076
		void AddItem(string itemName, bool isChecked, Action<object> action, object data);

		// Token: 0x0600081D RID: 2077
		void AddDisabledItem(string itemName, bool isChecked);

		// Token: 0x0600081E RID: 2078
		void AddSeparator(string path);

		// Token: 0x0600081F RID: 2079
		void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false);
	}
}
