using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
[ExecuteAlways]
public class VLS_Missle_VFX : MonoBehaviour
{
	// Token: 0x06000E5E RID: 3678 RVA: 0x0005C515 File Offset: 0x0005A715
	private void Start()
	{
		base.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.speed = 0f;
	}

	// Token: 0x06000E5F RID: 3679 RVA: 0x0005C544 File Offset: 0x0005A744
	private void Update()
	{
		base.transform.localPosition += new Vector3(0f, (500f + this.speed) * Time.deltaTime, 0f);
		this.speed += 500f * Time.deltaTime;
	}

	// Token: 0x04000A05 RID: 2565
	public float speed;
}
