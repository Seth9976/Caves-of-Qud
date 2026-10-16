using System;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000176 RID: 374
[RequireComponent(typeof(Button))]
public class CanvasSampleSaveFileImage : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	// Token: 0x06000E7E RID: 3710 RVA: 0x0005CD28 File Offset: 0x0005AF28
	private void Awake()
	{
		int num = 100;
		int num2 = 100;
		Texture2D texture2D = new Texture2D(num, num2, TextureFormat.RGB24, false);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				texture2D.SetPixel(i, j, Color.red);
			}
		}
		texture2D.Apply();
		this._textureBytes = texture2D.EncodeToPNG();
		global::UnityEngine.Object.Destroy(texture2D);
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0005CD87 File Offset: 0x0005AF87
	public void OnPointerDown(PointerEventData eventData)
	{
	}

	// Token: 0x06000E80 RID: 3712 RVA: 0x0005CD89 File Offset: 0x0005AF89
	private void Start()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06000E81 RID: 3713 RVA: 0x0005CDA8 File Offset: 0x0005AFA8
	public void OnClick()
	{
		string text = StandaloneFileBrowser.SaveFilePanel("Title", "", "sample", "png");
		if (!string.IsNullOrEmpty(text))
		{
			File.WriteAllBytes(text, this._textureBytes);
		}
	}

	// Token: 0x04000A10 RID: 2576
	public Text output;

	// Token: 0x04000A11 RID: 2577
	private byte[] _textureBytes;
}
