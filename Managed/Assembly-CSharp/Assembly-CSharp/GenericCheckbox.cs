using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;

// Token: 0x02000077 RID: 119
[RequireComponent(typeof(Toggle))]
public class GenericCheckbox : MonoBehaviour
{
	// Token: 0x0600047B RID: 1147 RVA: 0x0001DCB1 File Offset: 0x0001BEB1
	private void Start()
	{
		this.toggle = base.GetComponent<Toggle>();
		this.OnToggleValueChanged(this.toggle.isOn);
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x0001DCD0 File Offset: 0x0001BED0
	public void OnToggleValueChanged(bool value)
	{
		this.checkboxText.SetText(value ? "[■] " : "[ ] ");
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x0001DCED File Offset: 0x0001BEED
	private void Update()
	{
	}

	// Token: 0x04000362 RID: 866
	public Toggle toggle;

	// Token: 0x04000363 RID: 867
	public UITextSkin checkboxText;

	// Token: 0x04000364 RID: 868
	protected const string EMPTY_CHECK = "[ ] ";

	// Token: 0x04000365 RID: 869
	protected const string CHECKED = "[■] ";

	// Token: 0x04000366 RID: 870
	protected const string CHECKED_ACTIVE = "[{{W|■}}] ";
}
