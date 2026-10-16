using System;
using ConsoleLib.Console;
using Kobold;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class CombatJuiceEntryJump : CombatJuiceEntry
{
	// Token: 0x0600026A RID: 618 RVA: 0x0000C760 File Offset: 0x0000A960
	public CombatJuiceEntryJump(IRenderable Tile, Vector3 Start, Vector3 End, float Duration, float Arc = 0.5f, float Scale = 1f, bool Focus = false)
	{
		this.Tile = Tile;
		this.Start = Start;
		this.End = End;
		this.duration = Duration;
		this.Focus = Focus;
		this.Mod = new Vector3((Start.y - End.y) * -1f * Arc / 4f, Mathf.Abs(Start.x - End.x) * Arc, 0f);
		this.ScaleMod = 1f + Arc * Scale;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
	public override void start()
	{
		this.Target = SpriteManager.GetPooledSprite(this.Tile, true);
		this.Foreground = this.Target.color;
		this.Background = this.Target.backcolor;
		this.Detail = this.Target.detailcolor;
		Transform transform = this.Target.gameObject.transform;
		transform.SetParent(GameManager.Instance.TileRoot.transform);
		transform.position = this.Start + new Vector3(0f, 0f, -10f);
		this.Scale = transform.localScale;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x0000C8A4 File Offset: 0x0000AAA4
	public override void update()
	{
		this.Target.color = this.Foreground;
		this.Target.backcolor = this.Background;
		this.Target.detailcolor = this.Detail;
		float num = Mathf.Clamp(this.t / this.duration, 0f, 1f);
		float num2 = Mathf.Sin(num * 3.1415927f);
		this.Target.transform.localScale = Vector3.Lerp(this.Scale, this.Scale * this.ScaleMod, num2);
		this.Target.transform.position = Vector3.Lerp(this.Start, this.End, Easing.SineEaseOut(num)) + Vector3.Lerp(Vector3.zero, this.Mod, num2) + new Vector3(0f, 0f, -50f);
		if (this.Focus)
		{
			GameManager.Instance.TargetCameraLocation = this.Target.transform.position;
		}
	}

	// Token: 0x0600026D RID: 621 RVA: 0x0000C9B3 File Offset: 0x0000ABB3
	public override void finish()
	{
		SpriteManager.Return(this.Target);
		base.finish();
	}

	// Token: 0x0400014B RID: 331
	public ex3DSprite2 Target;

	// Token: 0x0400014C RID: 332
	public IRenderable Tile;

	// Token: 0x0400014D RID: 333
	public Vector3 Start;

	// Token: 0x0400014E RID: 334
	public Vector3 End;

	// Token: 0x0400014F RID: 335
	public Vector3 Mod;

	// Token: 0x04000150 RID: 336
	public Vector3 Scale;

	// Token: 0x04000151 RID: 337
	public float ScaleMod = 1f;

	// Token: 0x04000152 RID: 338
	public bool Focus;

	// Token: 0x04000153 RID: 339
	private Color Foreground;

	// Token: 0x04000154 RID: 340
	private Color Background;

	// Token: 0x04000155 RID: 341
	private Color Detail;
}
