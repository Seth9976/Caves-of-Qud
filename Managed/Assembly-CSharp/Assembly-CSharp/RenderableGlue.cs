using System;
using ConsoleLib.Console;
using ModelShark;
using UnityEngine;

// Token: 0x02000171 RID: 369
public class RenderableGlue : MonoBehaviour, Tooltip.SetupHelper
{
	// Token: 0x06000E66 RID: 3686 RVA: 0x0005C62C File Offset: 0x0005A82C
	public void BeforeShow(TooltipTrigger trigger, Tooltip tooltip)
	{
		object additionalData = trigger.AdditionalData;
		if (additionalData is DualImageRenderableGlue.DualImageRenderableGlueData)
		{
			DualImageRenderableGlue.DualImageRenderableGlueData dualImageRenderableGlueData = (DualImageRenderableGlue.DualImageRenderableGlueData)additionalData;
			this.image.FromRenderable(dualImageRenderableGlueData.imageOneRenderable);
			return;
		}
		this.image.FromRenderable(trigger.AdditionalData as IRenderable);
	}

	// Token: 0x04000A0B RID: 2571
	public IRenderableDisplayManager image;
}
