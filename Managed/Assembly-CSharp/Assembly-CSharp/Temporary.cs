using System;
using UnityEngine;

// Token: 0x0200016B RID: 363
public class Temporary : MonoBehaviour
{
	// Token: 0x06000E49 RID: 3657 RVA: 0x0005C164 File Offset: 0x0005A364
	public Temporary()
	{
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x0005C191 File Offset: 0x0005A391
	public Temporary(float _Duration)
	{
		this.Duration = _Duration * 1000f;
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x0005C1CB File Offset: 0x0005A3CB
	private void Start()
	{
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x0005C1D0 File Offset: 0x0005A3D0
	private void Update()
	{
		this.Duration -= Time.deltaTime;
		if (this.Duration < 0f && !this.Destroyed)
		{
			this.Destroyed = true;
			if (this.BeforeDestroy != null)
			{
				this.BeforeDestroy();
			}
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		base.transform.position += this.Delta * Time.deltaTime;
	}

	// Token: 0x040009FB RID: 2555
	[NonSerialized]
	private bool Destroyed;

	// Token: 0x040009FC RID: 2556
	public float Duration = 1025f;

	// Token: 0x040009FD RID: 2557
	public Vector3 Delta = new Vector3(0f, 0f, 0f);

	// Token: 0x040009FE RID: 2558
	public Action BeforeDestroy;
}
