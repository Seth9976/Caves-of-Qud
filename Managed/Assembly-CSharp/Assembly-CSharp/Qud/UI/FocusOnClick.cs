using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Qud.UI
{
	// Token: 0x0200041B RID: 1051
	public class FocusOnClick : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x060027F6 RID: 10230 RVA: 0x000CEA6A File Offset: 0x000CCC6A
		public void OnPointerClick(PointerEventData eventData)
		{
			this.objectToFocus.Select();
			if (this.objectToFocus.gameObject.GetComponent<InputField>() != null)
			{
				this.objectToFocus.gameObject.GetComponent<InputField>().ActivateInputField();
			}
		}

		// Token: 0x040013BC RID: 5052
		public Selectable objectToFocus;
	}
}
