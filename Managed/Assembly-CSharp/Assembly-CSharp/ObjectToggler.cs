using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000097 RID: 151
public class ObjectToggler : MonoBehaviour
{
	// Token: 0x0600051E RID: 1310 RVA: 0x00021640 File Offset: 0x0001F840
	public void PoolReset()
	{
		this.button.interactable = false;
		this.button.interactable = true;
		this.toggled = true;
		base.transform.Find("Plus").GetComponent<Text>().text = (this.toggled ? "-" : "+");
		this.objects.Clear();
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x000216A8 File Offset: 0x0001F8A8
	public void Toggle()
	{
		this.toggled = !this.toggled;
		for (int i = 0; i < this.objects.Count; i++)
		{
			this.objects[i].SetActive(this.toggled);
		}
		base.transform.Find("Plus").GetComponent<Text>().text = (this.toggled ? "-" : "+");
	}

	// Token: 0x040003EE RID: 1006
	public List<GameObject> objects = new List<GameObject>();

	// Token: 0x040003EF RID: 1007
	public bool toggled = true;

	// Token: 0x040003F0 RID: 1008
	public Selectable button;
}
