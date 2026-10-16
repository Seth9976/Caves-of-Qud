using System;
using UnityEngine;

// Token: 0x02000168 RID: 360
public class OrthoCamera : MonoBehaviour
{
	// Token: 0x06000E40 RID: 3648 RVA: 0x0005BFC1 File Offset: 0x0005A1C1
	protected void Awake()
	{
		base.gameObject.GetComponent<Camera>().transparencySortMode = TransparencySortMode.Orthographic;
	}
}
