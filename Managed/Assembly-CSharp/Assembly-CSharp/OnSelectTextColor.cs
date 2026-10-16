using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000098 RID: 152
public class OnSelectTextColor : MonoBehaviour, ISelectHandler, IEventSystemHandler, IDeselectHandler
{
	// Token: 0x06000521 RID: 1313 RVA: 0x00021739 File Offset: 0x0001F939
	void ISelectHandler.OnSelect(BaseEventData eventData)
	{
		this.textToChange.color = this.onSelectColor;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0002174C File Offset: 0x0001F94C
	public void OnDeselect(BaseEventData eventData)
	{
		this.textToChange.color = this.onDeselectColor;
	}

	// Token: 0x040003F1 RID: 1009
	public Text textToChange;

	// Token: 0x040003F2 RID: 1010
	public Color onSelectColor;

	// Token: 0x040003F3 RID: 1011
	public Color onDeselectColor;
}
