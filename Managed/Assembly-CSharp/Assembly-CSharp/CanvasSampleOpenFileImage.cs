using System;
using System.Collections;
using SFB;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000173 RID: 371
[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E6F RID: 3695 RVA: 0x0005CB84 File Offset: 0x0005AD84
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x0005CB86 File Offset: 0x0005AD86
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x0005CBA4 File Offset: 0x0005ADA4
	private void OnClick()
	{
		string[] array = StandaloneFileBrowser.OpenFilePanel("Title", "", ".png", false);
		if (array.Length != 0)
		{
			base.StartCoroutine(this.OutputRoutine(new Uri(array[0]).AbsoluteUri));
		}
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x0005CBE5 File Offset: 0x0005ADE5
	private IEnumerator OutputRoutine(string url)
	{
		WWW loader = new WWW(url);
		yield return loader;
		this.output.texture = loader.texture;
		yield break;
	}

	// Token: 0x04000A0D RID: 2573
	public RawImage output;
}
