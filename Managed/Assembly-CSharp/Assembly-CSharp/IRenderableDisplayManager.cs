using System;
using ConsoleLib.Console;
using UnityEngine;
using XRL;
using XRL.World;

// Token: 0x02000126 RID: 294
public class IRenderableDisplayManager : MonoBehaviour
{
	// Token: 0x06000B4A RID: 2890 RVA: 0x0004B348 File Offset: 0x00049548
	public void FromRenderable(IRenderable renderable)
	{
		RenderEvent renderEvent = renderable as RenderEvent;
		if (renderEvent != null && !string.IsNullOrEmpty(renderEvent.WantsBackdrop) && renderEvent.BackdropBleedthrough)
		{
			this.backdropManager.gameObject.SetActive(true);
			this.threeColorTile.gameObject.SetActive(false);
			this.backdropManager.SetBackdrop(renderEvent.WantsBackdrop);
			this.backdropManager.SetTile(renderEvent.x, renderEvent.y);
			return;
		}
		this.backdropManager.gameObject.SetActive(false);
		this.threeColorTile.gameObject.SetActive(true);
		this.threeColorTile.FromRenderable(renderable, true);
		if (this.threeColorTile.Background == The.Color.DarkBlack)
		{
			this.threeColorTile.Background = Color.clear;
		}
	}

	// Token: 0x06000B4B RID: 2891 RVA: 0x0004B41A File Offset: 0x0004961A
	private void Start()
	{
	}

	// Token: 0x06000B4C RID: 2892 RVA: 0x0004B41C File Offset: 0x0004961C
	private void Update()
	{
	}

	// Token: 0x040007BE RID: 1982
	public UIThreeColorProperties threeColorTile;

	// Token: 0x040007BF RID: 1983
	public BackdropWindowManager backdropManager;

	// Token: 0x040007C0 RID: 1984
	public Transform imposterRoot;
}
