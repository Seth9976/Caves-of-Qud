using System;
using UnityEngine;

// Token: 0x02000166 RID: 358
public class FloatText : MonoBehaviour
{
	// Token: 0x06000E24 RID: 3620 RVA: 0x0005B4ED File Offset: 0x000596ED
	private void Start()
	{
		this.StartTime = (double)Time.fixedTime;
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0005B4FC File Offset: 0x000596FC
	private void Update()
	{
		if ((double)Time.fixedTime - this.StartTime > this.Duration)
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		base.gameObject.transform.position += new Vector3(0f, this.Speed * Time.deltaTime, 0f);
	}

	// Token: 0x040009D8 RID: 2520
	private float Speed = 35f;

	// Token: 0x040009D9 RID: 2521
	public double Duration = 2.0;

	// Token: 0x040009DA RID: 2522
	private double StartTime;
}
