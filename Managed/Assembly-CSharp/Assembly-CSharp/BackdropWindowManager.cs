using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000125 RID: 293
public class BackdropWindowManager : MonoBehaviour
{
	// Token: 0x06000B45 RID: 2885 RVA: 0x0004B305 File Offset: 0x00049505
	public void SetBackdrop(string backdrop)
	{
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x0004B307 File Offset: 0x00049507
	public void SetTile(int x, int y)
	{
		this.backdropTransform.anchoredPosition = new Vector3((float)(-(float)x) * 3f * 16f, (float)y * 3f * 24f);
	}

	// Token: 0x06000B47 RID: 2887 RVA: 0x0004B33B File Offset: 0x0004953B
	private void Start()
	{
	}

	// Token: 0x06000B48 RID: 2888 RVA: 0x0004B33D File Offset: 0x0004953D
	private void Update()
	{
	}

	// Token: 0x040007BC RID: 1980
	public Image backdropImage;

	// Token: 0x040007BD RID: 1981
	public RectTransform backdropTransform;
}
