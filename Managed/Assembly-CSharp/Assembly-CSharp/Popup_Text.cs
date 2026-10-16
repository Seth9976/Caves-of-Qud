using System;
using QupKit;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

// Token: 0x020000A1 RID: 161
[UIView("Popup:Text", false, false, false, "Menu", "Popup:Text", false, 0, false)]
public class Popup_Text : BaseView
{
	// Token: 0x06000552 RID: 1362 RVA: 0x00022844 File Offset: 0x00020A44
	public override void Enter()
	{
		base.Enter();
		if (this.originalDialogSize == Vector2.zero)
		{
			this.originalDialogSize = base.GetChildComponent<RectTransform>("Controls").sizeDelta;
			this.originalTextSize = base.GetChildComponent<RectTransform>("Controls/Message").sizeDelta;
		}
		base.GetChildComponent<RectTransform>("Controls/Message").sizeDelta = this.originalTextSize;
		base.GetChildComponent<global::UnityEngine.UI.Text>("Controls/Message").text = Popup_Text.Text;
		Canvas.ForceUpdateCanvases();
		base.GetChildComponent<RectTransform>("Controls/Message").sizeDelta = new Vector2(base.GetChildComponent<global::UnityEngine.UI.Text>("Controls/Message").preferredWidth, base.GetChildComponent<global::UnityEngine.UI.Text>("Controls/Message").preferredHeight);
		Vector2 sizeDelta = base.GetChildComponent<RectTransform>("Controls/Message").sizeDelta;
		if (sizeDelta.x < 850f)
		{
			sizeDelta = new Vector2(850f, sizeDelta.y);
		}
		if (sizeDelta.y < this.originalTextSize.y)
		{
			sizeDelta = new Vector2(sizeDelta.x, this.originalTextSize.y);
		}
		base.GetChildComponent<RectTransform>("Controls").sizeDelta = this.originalDialogSize + (sizeDelta - this.originalTextSize);
	}

	// Token: 0x04000421 RID: 1057
	public new static string Text;

	// Token: 0x04000422 RID: 1058
	public Vector2 originalDialogSize = Vector2.zero;

	// Token: 0x04000423 RID: 1059
	public Vector2 originalTextSize = Vector2.zero;
}
