using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class instructions : MonoBehaviour
{
	// Token: 0x06000BBC RID: 3004 RVA: 0x0004EA7E File Offset: 0x0004CC7E
	public void OnBackButtonPressed()
	{
		global::UnityEngine.Object.Instantiate(Resources.Load("Main Menu"));
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
	}
}
