using System;
using ConsoleLib.Console;
using ModelShark;
using UnityEngine;
using XRL.UI;

// Token: 0x02000076 RID: 118
public class DualImageRenderableGlue : MonoBehaviour, Tooltip.SetupHelper
{
	// Token: 0x06000479 RID: 1145 RVA: 0x0001DC1C File Offset: 0x0001BE1C
	public void BeforeShow(TooltipTrigger trigger, Tooltip tooltip)
	{
		object additionalData = trigger.AdditionalData;
		if (additionalData is DualImageRenderableGlue.DualImageRenderableGlueData)
		{
			DualImageRenderableGlue.DualImageRenderableGlueData dualImageRenderableGlueData = (DualImageRenderableGlue.DualImageRenderableGlueData)additionalData;
			this.image.FromRenderable(dualImageRenderableGlueData.imageOneRenderable);
			this.image2.FromRenderable(dualImageRenderableGlueData.imageTwoRenderable);
		}
		if (Media.sizeClass < Media.SizeClass.Medium)
		{
			base.transform.localScale = new Vector3(0.75f, 0.75f, 1f);
			return;
		}
		base.transform.localScale = new Vector3(1f, 1f, 1f);
	}

	// Token: 0x04000360 RID: 864
	public IRenderableDisplayManager image;

	// Token: 0x04000361 RID: 865
	public IRenderableDisplayManager image2;

	// Token: 0x0200159F RID: 5535
	public struct DualImageRenderableGlueData
	{
		// Token: 0x0600D5C8 RID: 54728 RVA: 0x004F5C7E File Offset: 0x004F3E7E
		public DualImageRenderableGlueData(IRenderable one, IRenderable two)
		{
			this.imageOneRenderable = one;
			this.imageTwoRenderable = two;
		}

		// Token: 0x040056FA RID: 22266
		public IRenderable imageOneRenderable;

		// Token: 0x040056FB RID: 22267
		public IRenderable imageTwoRenderable;
	}
}
