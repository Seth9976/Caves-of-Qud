using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000038 RID: 56
public class FlashImage : MonoBehaviour
{
	// Token: 0x060001F4 RID: 500 RVA: 0x0000A443 File Offset: 0x00008643
	private void Start()
	{
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0000A448 File Offset: 0x00008648
	private void Update()
	{
		float num = Time.timeSinceLevelLoad % 0.5f * 2f * 3.1415927f * 2f;
		if (this.i == null)
		{
			this.i = base.gameObject.GetComponent<Image>();
		}
		this.i.color = new Color(this.i.color.r, this.i.color.g, this.i.color.b, (Mathf.Sin(num) + 1f) / 2f);
	}

	// Token: 0x04000108 RID: 264
	private Image i;
}
