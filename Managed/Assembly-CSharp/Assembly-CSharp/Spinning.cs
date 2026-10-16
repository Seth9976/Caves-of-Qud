using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
[ExecuteInEditMode]
public class Spinning : MonoBehaviour
{
	// Token: 0x06000B5B RID: 2907 RVA: 0x0004BD6D File Offset: 0x00049F6D
	private void Start()
	{
	}

	// Token: 0x06000B5C RID: 2908 RVA: 0x0004BD6F File Offset: 0x00049F6F
	private void Update()
	{
		base.gameObject.transform.Rotate(0f, 0f, this.speed * Time.deltaTime * 1f, Space.Self);
	}

	// Token: 0x040007DF RID: 2015
	public float speed = 10f;
}
