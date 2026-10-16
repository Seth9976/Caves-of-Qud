using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class SimpleSpriteAnimator : MonoBehaviour, CombatJuice.ICombatJuiceAnimator
{
	// Token: 0x060002C2 RID: 706 RVA: 0x000102BB File Offset: 0x0000E4BB
	public void Finish()
	{
		if (this.after != null)
		{
			this.after();
			this.after = null;
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x000102D8 File Offset: 0x0000E4D8
	public void Play(bool loop = false, Action after = null, string name = null, string objectId = null)
	{
		this.setsize = this.sprites.Length / this.sets;
		this.after = after;
		this.loop = loop;
		this.playingset = global::UnityEngine.Random.Range(0, this.sets);
		this.frame = 0;
		this.deltaTime = 0f;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0001032C File Offset: 0x0000E52C
	private void Start()
	{
		this.spr = base.GetComponent<SpriteRenderer>();
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0001033C File Offset: 0x0000E53C
	private void Update()
	{
		this.deltaTime += Time.deltaTime;
		while (this.frameSeconds > 0f && this.deltaTime >= this.frameSeconds)
		{
			this.deltaTime -= this.frameSeconds;
			this.frame++;
			if (this.loop)
			{
				this.frame %= this.sprites.Length;
			}
			else if (this.frame >= this.setsize)
			{
				this.frame = this.sprites.Length - 1;
				base.gameObject.SendMessage("OnSimpleAnimationComplete", SendMessageOptions.DontRequireReceiver);
				if (this.after != null)
				{
					this.after();
					this.after = null;
					return;
				}
			}
		}
		this.spr.sprite = this.sprites[this.frame + this.playingset * this.setsize];
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0001042E File Offset: 0x0000E62E
	public void Stop()
	{
		this.loop = false;
		this.frame = this.setsize;
		this.deltaTime = this.frameSeconds;
	}

	// Token: 0x04000204 RID: 516
	public bool loop;

	// Token: 0x04000205 RID: 517
	public float frameSeconds = 1f;

	// Token: 0x04000206 RID: 518
	public int sets = 1;

	// Token: 0x04000207 RID: 519
	private int setsize;

	// Token: 0x04000208 RID: 520
	private int playingset;

	// Token: 0x04000209 RID: 521
	private SpriteRenderer spr;

	// Token: 0x0400020A RID: 522
	public Sprite[] sprites;

	// Token: 0x0400020B RID: 523
	private int frame;

	// Token: 0x0400020C RID: 524
	private float deltaTime;

	// Token: 0x0400020D RID: 525
	private Action after;
}
