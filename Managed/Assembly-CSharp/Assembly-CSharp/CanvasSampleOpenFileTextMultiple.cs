using System;
using System.Collections;
using System.Collections.Generic;
using SFB;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000175 RID: 373
[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileTextMultiple : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E79 RID: 3705 RVA: 0x0005CC83 File Offset: 0x0005AE83
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000E7A RID: 3706 RVA: 0x0005CC85 File Offset: 0x0005AE85
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000E7B RID: 3707 RVA: 0x0005CCA4 File Offset: 0x0005AEA4
	private void OnClick()
	{
		string[] array = StandaloneFileBrowser.OpenFilePanel("Open File", "", "", true);
		if (array.Length != 0)
		{
			List<string> list = new List<string>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(new Uri(array[i]).AbsoluteUri);
			}
			base.StartCoroutine(this.OutputRoutine(list.ToArray()));
		}
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x0005CD08 File Offset: 0x0005AF08
	private IEnumerator OutputRoutine(string[] urlArr)
	{
		string outputText = "";
		int num;
		for (int i = 0; i < urlArr.Length; i = num + 1)
		{
			WWW loader = new WWW(urlArr[i]);
			yield return loader;
			outputText += loader.text;
			loader = null;
			num = i;
		}
		this.output.text = outputText;
		yield break;
	}

	// Token: 0x04000A0F RID: 2575
	public Text output;
}
