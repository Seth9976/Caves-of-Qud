using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002C8 RID: 712
	public class FocusPanel : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		// Token: 0x060018A2 RID: 6306 RVA: 0x0007F968 File Offset: 0x0007DB68
		public void OnPointerDown(PointerEventData data)
		{
			this.panel.SetAsLastSibling();
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x0007F975 File Offset: 0x0007DB75
		private void Awake()
		{
			this.panel = base.GetComponent<RectTransform>();
		}

		// Token: 0x04000E9B RID: 3739
		private RectTransform panel;
	}
}
