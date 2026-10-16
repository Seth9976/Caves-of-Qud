using System;
using UnityEngine;
using XRL.UI;

// Token: 0x02000081 RID: 129
public class MapScrollerPinItem : MonoBehaviour
{
	// Token: 0x060004B5 RID: 1205 RVA: 0x0001FB6D File Offset: 0x0001DD6D
	public void SetData(MapScrollerController.MapPinData data)
	{
		this.titleText.SetText(data.title);
		this.detailsText.SetText(data.details);
	}

	// Token: 0x040003A9 RID: 937
	public UITextSkin titleText;

	// Token: 0x040003AA RID: 938
	public UITextSkin detailsText;
}
