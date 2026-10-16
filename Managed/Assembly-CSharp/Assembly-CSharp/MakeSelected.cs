using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000096 RID: 150
public class MakeSelected : MonoBehaviour
{
	// Token: 0x1700008B RID: 139
	// (get) Token: 0x0600051B RID: 1307 RVA: 0x000215DB File Offset: 0x0001F7DB
	public EventSystem EventSystemManager
	{
		get
		{
			if (MakeSelected._EventSystem == null)
			{
				MakeSelected._EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
			}
			return MakeSelected._EventSystem;
		}
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00021603 File Offset: 0x0001F803
	public void Update()
	{
		this.EventSystemManager.firstSelectedGameObject = base.gameObject;
		if (base.GetComponent<Button>() != null)
		{
			base.GetComponent<Button>().Select();
		}
		global::UnityEngine.Object.Destroy(this);
	}

	// Token: 0x040003ED RID: 1005
	private static EventSystem _EventSystem;
}
