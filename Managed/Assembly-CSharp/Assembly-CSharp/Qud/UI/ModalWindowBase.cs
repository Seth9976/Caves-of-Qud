using System;
using UnityEngine;

namespace Qud.UI
{
	// Token: 0x02000410 RID: 1040
	public class ModalWindowBase<T> : WindowBase where T : WindowBase, new()
	{
		// Token: 0x06002770 RID: 10096 RVA: 0x000CCAB8 File Offset: 0x000CACB8
		public static void ShowModal()
		{
			ModalWindowBase<T>.instance.Show();
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x000CCAC9 File Offset: 0x000CACC9
		public override void Init()
		{
			ModalWindowBase<T>.instance = this as T;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x000CCADB File Offset: 0x000CACDB
		public override void Hide()
		{
			base.canvas.enabled = false;
			base.raycaster.enabled = false;
			if (this != ModalWindowBase<T>.instance)
			{
				global::UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x000CCB14 File Offset: 0x000CAD14
		public override void Show()
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(base.gameObject);
			gameObject.SetActive(true);
			gameObject.GetComponent<ModalWindowBase<T>>().canvas.enabled = true;
			gameObject.GetComponent<ModalWindowBase<T>>().raycaster.enabled = true;
			gameObject.transform.SetParent(base.gameObject.transform.parent, false);
			gameObject.transform.SetAsLastSibling();
			RectTransform component = base.GetComponent<RectTransform>();
			RectTransform component2 = gameObject.GetComponent<RectTransform>();
			component2.anchorMin = component.anchorMin;
			component2.anchorMax = component.anchorMax;
			component2.anchoredPosition = component.anchoredPosition;
			component2.sizeDelta = component.sizeDelta;
		}

		// Token: 0x04001380 RID: 4992
		public static T instance;
	}
}
