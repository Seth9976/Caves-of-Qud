using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class SimpleTextMeshTweener : MonoBehaviour
{
	// Token: 0x060002C8 RID: 712 RVA: 0x00010469 File Offset: 0x0000E669
	public void doTweens(float t)
	{
		if (this.mesh != null)
		{
			base.gameObject.transform.position = Vector3.Lerp(this.start, this.end, t);
		}
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0001049C File Offset: 0x0000E69C
	public void init(Vector3 start, Vector3 end, Color startColor, Color endColor, float duration, string poolName = null)
	{
		this.start = start;
		this.end = end;
		this.startColor = startColor;
		this.endColor = endColor;
		this.duration = duration;
		this.poolName = poolName;
		this.mesh = base.gameObject.GetComponent<TextMesh>();
		this.time = 0f;
		this.doTweens(0f);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x000104FD File Offset: 0x0000E6FD
	public void Finish()
	{
		if (this.poolName != null)
		{
			CombatJuice.pool(this.poolName, base.gameObject, true);
			return;
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060002CB RID: 715 RVA: 0x00010525 File Offset: 0x0000E725
	private void Update()
	{
		this.time += Time.deltaTime;
		this.doTweens(this.time / this.duration);
		if (this.time > this.duration)
		{
			this.Finish();
		}
	}

	// Token: 0x0400020E RID: 526
	public string poolName;

	// Token: 0x0400020F RID: 527
	public float time;

	// Token: 0x04000210 RID: 528
	public float duration;

	// Token: 0x04000211 RID: 529
	public Vector3 start;

	// Token: 0x04000212 RID: 530
	public Vector3 end;

	// Token: 0x04000213 RID: 531
	public Color startColor;

	// Token: 0x04000214 RID: 532
	public Color endColor;

	// Token: 0x04000215 RID: 533
	public TextMesh mesh;

	// Token: 0x04000216 RID: 534
	public SimpleTextOutline outline;
}
