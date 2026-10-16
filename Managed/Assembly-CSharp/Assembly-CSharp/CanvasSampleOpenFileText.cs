using System;
using System.Collections;
using SFB;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000174 RID: 372
[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileText : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E74 RID: 3700 RVA: 0x0005CC03 File Offset: 0x0005AE03
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x0005CC05 File Offset: 0x0005AE05
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x0005CC24 File Offset: 0x0005AE24
	private void OnClick()
	{
		string[] array = StandaloneFileBrowser.OpenFilePanel("Title", "", "txt", false);
		if (array.Length != 0)
		{
			base.StartCoroutine(this.OutputRoutine(new Uri(array[0]).AbsoluteUri));
		}
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x0005CC65 File Offset: 0x0005AE65
	private IEnumerator OutputRoutine(string url)
	{
		WWW loader = new WWW(url);
		yield return loader;
		this.output.text = loader.text;
		yield break;
	}

	// Token: 0x04000A0E RID: 2574
	public Text output;
}
