using System;
using UnityEngine;

// Token: 0x020000E2 RID: 226
public class exUIScrollBar : exUIControl
{
	// Token: 0x0600086E RID: 2158 RVA: 0x00036440 File Offset: 0x00034640
	protected new void Awake()
	{
		base.Awake();
		Transform transform = base.transform.Find("__bar");
		if (transform)
		{
			this.bar = transform.GetComponent<exSprite>();
			if (this.bar)
			{
				this.bar.customSize = true;
				this.bar.anchor = Anchor.TopLeft;
			}
			exUIButton component = transform.GetComponent<exUIButton>();
			if (component)
			{
				component.grabMouseOrTouch = true;
				component.AddEventListener("onPressDown", delegate(exUIEvent _event)
				{
					if (this.dragging)
					{
						return;
					}
					exUIPointEvent exUIPointEvent = _event as exUIPointEvent;
					if (exUIPointEvent.isTouch || exUIPointEvent.GetMouseButton(0))
					{
						this.dragging = true;
						this.draggingID = exUIPointEvent.mainPoint.id;
						exUIMng.inst.SetFocus(this);
					}
				}, false);
				component.AddEventListener("onPressUp", delegate(exUIEvent _event)
				{
					exUIPointEvent exUIPointEvent2 = _event as exUIPointEvent;
					if ((exUIPointEvent2.isTouch || exUIPointEvent2.GetMouseButton(0)) && exUIPointEvent2.pointInfos[0].id == this.draggingID && this.dragging)
					{
						this.dragging = false;
						this.draggingID = -1;
					}
				}, false);
				component.AddEventListener("onHoverMove", delegate(exUIEvent _event)
				{
					if (this.scrollView)
					{
						exUIPointEvent exUIPointEvent3 = _event as exUIPointEvent;
						for (int i = 0; i < exUIPointEvent3.pointInfos.Length; i++)
						{
							exUIPointInfo exUIPointInfo = exUIPointEvent3.pointInfos[i];
							if (this.dragging && (exUIPointEvent3.isTouch || exUIPointEvent3.GetMouseButton(0)) && exUIPointInfo.id == this.draggingID)
							{
								Vector2 vector = exUIPointInfo.worldDelta;
								vector.y = -vector.y;
								this.scrollView.Scroll(vector / this.ratio);
							}
						}
					}
				}, false);
			}
		}
		this.background = base.GetComponent<exSprite>();
		if (this.background)
		{
			this.scrollStart = (transform ? transform.localPosition : Vector3.zero);
			if (this.background.spriteType == exSpriteType.Sliced)
			{
				if (this.direction == exUIScrollBar.Direction.Horizontal)
				{
					this.scrollStart.x = this.background.leftBorderSize;
				}
				else
				{
					this.scrollStart.y = this.background.topBorderSize;
				}
			}
		}
		if (this.scrollView)
		{
			this.scrollView.AddEventListener("onContentResized", delegate(exUIEvent _event)
			{
				this.UpdateScrollBarRatio();
				this.UpdateScrollBar();
			}, false);
			this.scrollView.AddEventListener("onScroll", delegate(exUIEvent _event)
			{
				if (this.direction == exUIScrollBar.Direction.Horizontal)
				{
					this.scrollOffset = this.scrollView.scrollOffset.x * this.ratio;
				}
				else
				{
					this.scrollOffset = this.scrollView.scrollOffset.y * this.ratio;
				}
				this.UpdateScrollBar();
				if (this.scrollView.showCondition == exUIScrollView.ShowCondition.WhenDragging)
				{
					base.activeSelf = true;
				}
			}, false);
			this.scrollView.AddEventListener("onScrollFinished", delegate(exUIEvent _event)
			{
				if (this.scrollView.showCondition == exUIScrollView.ShowCondition.WhenDragging)
				{
					this.cooldownTimer = this.cooldown;
					this.isCoolingDown = true;
				}
			}, false);
			this.UpdateScrollBarRatio();
			this.UpdateScrollBar();
			if (this.scrollView.showCondition == exUIScrollView.ShowCondition.WhenDragging)
			{
				exUIEffect exUIEffect = base.GetComponent<exUIEffect>();
				if (exUIEffect == null)
				{
					exUIEffect = base.gameObject.AddComponent<exUIEffect>();
					if (this.background != null)
					{
						exUIEffect.AddEffect_Color(this.background, EffectEventType.Deactive, exEase.Type.Linear, new Color(this.background.color.r, this.background.color.g, this.background.color.b, 0f), 0.5f);
					}
					if (this.bar != null)
					{
						exUIEffect.AddEffect_Color(this.bar, EffectEventType.Deactive, exEase.Type.Linear, new Color(this.bar.color.r, this.bar.color.g, this.bar.color.b, 0f), 0.5f);
					}
				}
				if (this.background)
				{
					Color color = this.background.color;
					color.a = 0f;
					this.background.color = color;
				}
				if (this.bar)
				{
					Color color2 = this.bar.color;
					color2.a = 0f;
					this.bar.color = color2;
				}
				this.active_ = false;
			}
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0003673B File Offset: 0x0003493B
	private void Update()
	{
		if (this.isCoolingDown)
		{
			this.cooldownTimer -= Time.deltaTime;
			if (this.cooldownTimer <= 0f)
			{
				this.isCoolingDown = false;
				base.activeSelf = false;
			}
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x00036774 File Offset: 0x00034974
	private void UpdateScrollBarRatio()
	{
		float num;
		float num2;
		if (this.direction == exUIScrollBar.Direction.Horizontal)
		{
			num = this.scrollView.contentSize.x;
			num2 = this.width;
			if (this.background != null && this.background.spriteType == exSpriteType.Sliced)
			{
				num2 = num2 - this.background.leftBorderSize - this.background.rightBorderSize;
			}
		}
		else
		{
			num = this.scrollView.contentSize.y;
			num2 = this.height;
			if (this.background != null && this.background.spriteType == exSpriteType.Sliced)
			{
				num2 = num2 - this.background.topBorderSize - this.background.bottomBorderSize;
			}
		}
		this.ratio = Mathf.Min(num2 / num, 1f);
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x00036848 File Offset: 0x00034A48
	private void UpdateScrollBar()
	{
		if (this.direction == exUIScrollBar.Direction.Horizontal)
		{
			float num = this.ratio * this.scrollView.width;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = this.width;
			float num5 = this.scrollOffset;
			this.bar.width = this.ratio * this.scrollView.width;
			if (this.bar.spriteType == exSpriteType.Sliced)
			{
				this.bar.width = this.bar.width + this.bar.leftBorderSize + this.bar.rightBorderSize;
				num2 = this.bar.leftBorderSize;
			}
			if (this.background != null && this.background.spriteType == exSpriteType.Sliced)
			{
				num4 = num4 - this.background.leftBorderSize - this.background.rightBorderSize;
			}
			float num6 = 0f;
			float num7 = num4 - num;
			if (num5 < num6)
			{
				num3 = num6 - num5;
				num5 = num6;
			}
			else if (num5 > num7)
			{
				num3 = num5 - num7;
				num5 = num7 + num3;
			}
			this.bar.width -= num3;
			this.bar.transform.localPosition = new Vector3(this.scrollStart.x + num5 - num2, -this.scrollStart.y, this.scrollStart.z);
			return;
		}
		float num8 = this.ratio * this.scrollView.height;
		float num9 = 0f;
		float num10 = 0f;
		float num11 = this.height;
		float num12 = this.scrollOffset;
		this.bar.height = num8;
		if (this.bar.spriteType == exSpriteType.Sliced)
		{
			this.bar.height = this.bar.height + this.bar.topBorderSize + this.bar.bottomBorderSize;
			num9 = this.bar.topBorderSize;
		}
		if (this.background != null && this.background.spriteType == exSpriteType.Sliced)
		{
			num11 = num11 - this.background.topBorderSize - this.background.bottomBorderSize;
		}
		float num13 = 0f;
		float num14 = num11 - num8;
		if (num12 < num13)
		{
			num10 = num13 - num12;
			num12 = num13;
		}
		else if (num12 > num14)
		{
			num10 = num12 - num14;
			num12 = num14 + num10;
		}
		this.bar.height -= num10;
		this.bar.transform.localPosition = new Vector3(this.scrollStart.x, -(this.scrollStart.y + num12 - num9), this.scrollStart.z);
	}

	// Token: 0x040005C3 RID: 1475
	public exUIScrollBar.Direction direction;

	// Token: 0x040005C4 RID: 1476
	public exUIScrollView scrollView;

	// Token: 0x040005C5 RID: 1477
	public float cooldown = 0.5f;

	// Token: 0x040005C6 RID: 1478
	protected exSprite bar;

	// Token: 0x040005C7 RID: 1479
	protected exSprite background;

	// Token: 0x040005C8 RID: 1480
	protected float ratio = 1f;

	// Token: 0x040005C9 RID: 1481
	protected float scrollOffset;

	// Token: 0x040005CA RID: 1482
	protected Vector3 scrollStart = Vector3.zero;

	// Token: 0x040005CB RID: 1483
	protected bool dragging;

	// Token: 0x040005CC RID: 1484
	protected int draggingID = -1;

	// Token: 0x040005CD RID: 1485
	protected float cooldownTimer;

	// Token: 0x040005CE RID: 1486
	protected bool isCoolingDown;

	// Token: 0x020015D3 RID: 5587
	public enum Direction
	{
		// Token: 0x040057E2 RID: 22498
		Vertical,
		// Token: 0x040057E3 RID: 22499
		Horizontal
	}
}
