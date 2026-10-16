using System;
using UnityEngine;
using XRL.UI;

// Token: 0x0200007A RID: 122
public class ScaleResponsiveElement : MonoBehaviour
{
	// Token: 0x0600048D RID: 1165 RVA: 0x0001DEEA File Offset: 0x0001C0EA
	private float GetTargetScale()
	{
		if (Media.sizeClass <= Media.SizeClass.Small)
		{
			return this.SmallScale;
		}
		if (Media.sizeClass >= Media.SizeClass.Large)
		{
			return this.LargeScale;
		}
		return this.MediumScale;
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0001DF14 File Offset: 0x0001C114
	private void Check()
	{
		if (this.lastClass == Media.sizeClass)
		{
			return;
		}
		this.lastClass = Media.sizeClass;
		float targetScale = this.GetTargetScale();
		this.target.transform.localScale = new Vector3(this.ControlX ? targetScale : this.target.transform.localScale.x, this.ControlY ? targetScale : this.target.transform.localScale.y, this.ControlZ ? targetScale : this.target.transform.localScale.z);
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0001DFB6 File Offset: 0x0001C1B6
	private void Awake()
	{
		this.Check();
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001DFBE File Offset: 0x0001C1BE
	private void Update()
	{
		this.Check();
	}

	// Token: 0x04000374 RID: 884
	public Transform target;

	// Token: 0x04000375 RID: 885
	public float SmallScale;

	// Token: 0x04000376 RID: 886
	public float MediumScale;

	// Token: 0x04000377 RID: 887
	public float LargeScale;

	// Token: 0x04000378 RID: 888
	public bool ControlX;

	// Token: 0x04000379 RID: 889
	public bool ControlY;

	// Token: 0x0400037A RID: 890
	public bool ControlZ;

	// Token: 0x0400037B RID: 891
	private Media.SizeClass lastClass = Media.SizeClass.Unset;
}
