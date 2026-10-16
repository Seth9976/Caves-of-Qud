using System;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000177 RID: 375
[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileText : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E83 RID: 3715 RVA: 0x0005CDEB File Offset: 0x0005AFEB
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x0005CDED File Offset: 0x0005AFED
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0005CE0C File Offset: 0x0005B00C
	public void OnClick()
	{
		string text = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "txt");
		if (!string.IsNullOrEmpty(text))
		{
			File.WriteAllText(text, this._data);
		}
	}

	// Token: 0x04000A12 RID: 2578
	public Text output;

	// Token: 0x04000A13 RID: 2579
	private string _data = "Example text created by StandaloneFileBrowser";
}
