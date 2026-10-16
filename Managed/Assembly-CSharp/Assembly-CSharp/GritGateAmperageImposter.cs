using System;
using UnityEngine;
using XRL.UI;

// Token: 0x020000A9 RID: 169
[ExecuteInEditMode]
public class GritGateAmperageImposter : MonoBehaviour
{
	// Token: 0x06000616 RID: 1558 RVA: 0x0002C706 File Offset: 0x0002A906
	private void Start()
	{
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0002C708 File Offset: 0x0002A908
	private void Update()
	{
		if (this.mesh == null)
		{
			this.mesh = base.GetComponent<TextMesh>();
		}
		if (this.mesh != null && this.mesh.text != GritGateAmperageImposter.display)
		{
			this.mesh.text = Sidebar.FormatToRTF(GritGateAmperageImposter.display, "FF");
		}
	}

	// Token: 0x04000468 RID: 1128
	public static string display = "Amps Reminaing: <updating...>";

	// Token: 0x04000469 RID: 1129
	private TextMesh mesh;
}
