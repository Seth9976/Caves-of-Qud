using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class TogglePanel : MonoBehaviour
{
	// Token: 0x06000B6B RID: 2923 RVA: 0x0004C902 File Offset: 0x0004AB02
	public void ToggleClick()
	{
		base.gameObject.SetActive(!base.gameObject.activeInHierarchy);
	}
}
