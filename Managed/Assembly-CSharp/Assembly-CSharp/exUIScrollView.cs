using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E3 RID: 227
public class exUIScrollView : exUIControl
{
	// Token: 0x06000879 RID: 2169 RVA: 0x00036D00 File Offset: 0x00034F00
	public void OnScroll(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onScroll", this.onScroll, _event);
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x00036D19 File Offset: 0x00034F19
	public void OnScrollFinished(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onScrollFinished", this.onScrollFinished, _event);
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x00036D32 File Offset: 0x00034F32
	public void OnContentResized(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onContentResized", this.onContentResized, _event);
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00036D4C File Offset: 0x00034F4C
	public override void CacheEventListeners()
	{
		base.CacheEventListeners();
		this.onScroll = this.eventListenerTable["onScroll"];
		this.onScrollFinished = this.eventListenerTable["onScrollFinished"];
		this.onContentResized = this.eventListenerTable["onContentResized"];
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x00036DA4 File Offset: 0x00034FA4
	public override string[] GetEventNames()
	{
		string[] array = base.GetEventNames();
		string[] array2 = new string[array.Length + exUIScrollView.eventNames.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		for (int j = 0; j < exUIScrollView.eventNames.Length; j++)
		{
			array2[j + array.Length] = exUIScrollView.eventNames[j];
		}
		return array2;
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x0600087E RID: 2174 RVA: 0x00036DFE File Offset: 0x00034FFE
	// (set) Token: 0x0600087F RID: 2175 RVA: 0x00036E08 File Offset: 0x00035008
	public Vector2 contentSize
	{
		get
		{
			return this.contentSize_;
		}
		set
		{
			if (this.contentSize_ != value)
			{
				this.contentSize_ = value;
				this.OnContentResized(new exUIEvent
				{
					bubbles = false
				});
			}
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000880 RID: 2176 RVA: 0x00036E3E File Offset: 0x0003503E
	public Vector2 scrollOffset
	{
		get
		{
			return this.scrollOffset_;
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x00036E48 File Offset: 0x00035048
	protected new void Awake()
	{
		base.Awake();
		this.grabMouseOrTouch = true;
		if (this.contentAnchor != null)
		{
			this.originalAnchorPos = this.contentAnchor.localPosition;
		}
		base.AddEventListener("onPressDown", delegate(exUIEvent _event)
		{
			if (this.dragging)
			{
				_event.StopPropagation();
				return;
			}
			this.StartDrag(_event);
		}, false);
		base.AddEventListener("onPressUp", delegate(exUIEvent _event)
		{
			exUIPointEvent exUIPointEvent = _event as exUIPointEvent;
			if (this.draggable && (exUIPointEvent.isTouch || exUIPointEvent.GetMouseButton(0)) && exUIPointEvent.pointInfos[0].id == this.draggingID)
			{
				if (this.dragging)
				{
					this.dragging = false;
					this.draggingID = -1;
					this.StartScroll();
				}
				_event.StopPropagation();
			}
		}, false);
		base.AddEventListener("onHoverIn", delegate(exUIEvent _event)
		{
			if (this.dragging)
			{
				_event.StopPropagation();
				return;
			}
			this.StartDrag(_event);
		}, false);
		base.AddEventListener("onHoverMove", delegate(exUIEvent _event)
		{
			if (!this.dragging)
			{
				this.StartDrag(_event);
				return;
			}
			float num = ((this.horizontalContentDir == exUIScrollView.ContentDirection.LeftToRight) ? 0f : (-(this.contentSize_.x - this.width)));
			float num2 = ((this.verticalContentDir == exUIScrollView.ContentDirection.TopToBottom) ? 0f : (-(this.contentSize_.y - this.height)));
			Rect rect = new Rect(this.scrollOffset_.x, this.scrollOffset_.y, this.width, this.height);
			Rect rect2 = new Rect(num, num2, Mathf.Max(this.contentSize_.x, this.width), Mathf.Max(this.contentSize_.y, this.height));
			Vector2 constrainOffset = exGeometryUtility.GetConstrainOffset(rect, rect2);
			exUIPointEvent exUIPointEvent2 = _event as exUIPointEvent;
			for (int i = 0; i < exUIPointEvent2.pointInfos.Length; i++)
			{
				exUIPointInfo exUIPointInfo = exUIPointEvent2.pointInfos[i];
				if (this.draggable && (exUIPointEvent2.isTouch || exUIPointEvent2.GetMouseButton(0)) && exUIPointInfo.id == this.draggingID)
				{
					Vector2 vector = exUIPointInfo.worldDelta;
					vector.x = -vector.x;
					if (Mathf.Abs(constrainOffset.x) > 0.001f)
					{
						vector.x *= 0.5f;
					}
					if (Mathf.Abs(constrainOffset.y) > 0.001f)
					{
						vector.y *= 0.5f;
					}
					this.velocity = Vector2.Lerp(this.velocity, this.velocity + vector / Time.deltaTime * this.scrollSpeed, 0.67f);
					if (Mathf.Sign(this.velocity.x) != Mathf.Sign(vector.x))
					{
						this.velocity.x = 0f;
					}
					if (Mathf.Sign(this.velocity.y) != Mathf.Sign(vector.y))
					{
						this.velocity.y = 0f;
					}
					_event.StopPropagation();
					this.Scroll(vector);
					return;
				}
			}
		}, false);
		base.AddEventListener("onMouseWheel", delegate(exUIEvent _event)
		{
			float num3 = ((this.horizontalContentDir == exUIScrollView.ContentDirection.LeftToRight) ? 0f : (-(this.contentSize_.x - this.width)));
			float num4 = ((this.verticalContentDir == exUIScrollView.ContentDirection.TopToBottom) ? 0f : (-(this.contentSize_.y - this.height)));
			exUIWheelEvent exUIWheelEvent = _event as exUIWheelEvent;
			Vector2 vector2 = new Vector2(0f, -exUIWheelEvent.delta * 100f);
			if (Mathf.Abs(exGeometryUtility.GetConstrainOffset(new Rect(this.scrollOffset_.x, this.scrollOffset_.y, this.width, this.height), new Rect(num3, num4, this.contentSize_.x, this.contentSize_.y)).y) > 0.001f)
			{
				vector2.y *= 0.5f;
			}
			this.velocity = Vector2.Lerp(this.velocity, this.velocity + vector2 / Time.deltaTime * this.scrollSpeed, 0.67f);
			if (Mathf.Sign(this.velocity.x) != Mathf.Sign(vector2.x))
			{
				this.velocity.x = 0f;
			}
			if (Mathf.Sign(this.velocity.y) != Mathf.Sign(vector2.y))
			{
				this.velocity.y = 0f;
			}
			_event.StopPropagation();
			this.Scroll(vector2);
			this.StartScroll();
		}, false);
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x00036EFC File Offset: 0x000350FC
	private void LateUpdate()
	{
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = Vector2.zero;
		bool flag = this.damping || this.spring;
		float num = ((this.horizontalContentDir == exUIScrollView.ContentDirection.LeftToRight) ? 0f : (-(this.contentSize_.x - this.width)));
		float num2 = ((this.verticalContentDir == exUIScrollView.ContentDirection.TopToBottom) ? 0f : (-(this.contentSize_.y - this.height)));
		Rect rect = new Rect(this.scrollOffset_.x, this.scrollOffset_.y, this.width, this.height);
		Rect rect2 = new Rect(num, num2, Mathf.Max(this.contentSize_.x, this.width), Mathf.Max(this.contentSize_.y, this.height));
		if (this.damping || this.spring)
		{
			vector = exGeometryUtility.GetConstrainOffset(rect, rect2);
		}
		this.velocity.x = this.velocity.x * 0.9f;
		this.velocity.y = this.velocity.y * 0.9f;
		if (this.damping)
		{
			if (Mathf.Abs(vector.x) > 0.001f)
			{
				if (this.dragEffect != exUIScrollView.DragEffect.MomentumAndSpring)
				{
					this.velocity.x = 0f;
				}
				else
				{
					this.spring = true;
				}
			}
			if (Mathf.Abs(vector.y) > 0.001f)
			{
				if (this.dragEffect != exUIScrollView.DragEffect.MomentumAndSpring)
				{
					this.velocity.y = 0f;
				}
				else
				{
					this.spring = true;
				}
			}
			if (this.velocity.sqrMagnitude < 1f)
			{
				this.damping = false;
				this.velocity = Vector2.zero;
			}
			else
			{
				vector2 = this.velocity * Time.deltaTime;
			}
		}
		if (this.spring)
		{
			Vector2 vector3 = this.contentAnchor.localPosition;
			Vector2 vector4 = exMath.SpringLerp(vector3, vector3 - vector, 15f, Time.deltaTime) - vector3;
			if (vector4.sqrMagnitude < 0.001f)
			{
				vector2 = -vector;
				this.spring = false;
			}
			else
			{
				vector2 += vector4;
			}
		}
		if (flag)
		{
			this.Scroll(vector2);
			this.contentAnchor.localPosition = this.scrollDest;
			if (this.damping || this.spring)
			{
				this.OnScrollFinished(new exUIEvent
				{
					bubbles = false
				});
				return;
			}
		}
		else
		{
			this.contentAnchor.localPosition = Vector3.Lerp(this.contentAnchor.localPosition, this.scrollDest, 0.6f);
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x00037188 File Offset: 0x00035388
	private void StartDrag(exUIEvent _event)
	{
		exUIPointEvent exUIPointEvent = _event as exUIPointEvent;
		if (this.draggable && (exUIPointEvent.isTouch || exUIPointEvent.GetMouseButton(0)))
		{
			this.dragging = true;
			this.draggingID = exUIPointEvent.mainPoint.id;
			this.damping = false;
			this.spring = false;
			this.velocity = Vector2.zero;
			exUIMng.inst.SetFocus(this);
			_event.StopPropagation();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x000371F8 File Offset: 0x000353F8
	public void StartScroll()
	{
		if (this.dragEffect != exUIScrollView.DragEffect.None)
		{
			this.damping = true;
			float num = ((this.horizontalContentDir == exUIScrollView.ContentDirection.LeftToRight) ? 0f : (-(this.contentSize_.x - this.width)));
			float num2 = ((this.verticalContentDir == exUIScrollView.ContentDirection.TopToBottom) ? 0f : (-(this.contentSize_.y - this.height)));
			Vector2 constrainOffset = exGeometryUtility.GetConstrainOffset(new Rect(this.scrollOffset_.x, this.scrollOffset_.y, this.width, this.height), new Rect(num, num2, this.contentSize_.x, this.contentSize_.y));
			if (Mathf.Abs(constrainOffset.x) > 0.001f && this.dragEffect == exUIScrollView.DragEffect.MomentumAndSpring)
			{
				this.velocity.x = this.velocity.x * 0.5f;
			}
			if (Mathf.Abs(constrainOffset.y) > 0.001f && this.dragEffect == exUIScrollView.DragEffect.MomentumAndSpring)
			{
				this.velocity.y = this.velocity.y * 0.5f;
				return;
			}
		}
		else
		{
			this.velocity = Vector2.zero;
			this.damping = false;
			this.OnScrollFinished(new exUIEvent
			{
				bubbles = false
			});
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0003732C File Offset: 0x0003552C
	public void Scroll(Vector2 _delta)
	{
		if (!this.allowHorizontalScroll)
		{
			_delta.x = 0f;
		}
		if (!this.allowVerticalScroll)
		{
			_delta.y = 0f;
		}
		this.scrollOffset_ += _delta;
		if (this.dragEffect != exUIScrollView.DragEffect.MomentumAndSpring)
		{
			this.scrollOffset_.x = Mathf.Clamp(this.scrollOffset_.x, 0f, this.contentSize_.x - this.width);
			this.scrollOffset_.y = Mathf.Clamp(this.scrollOffset_.y, 0f, this.contentSize_.y - this.height);
		}
		if (this.contentAnchor != null)
		{
			this.scrollDest = new Vector3(this.originalAnchorPos.x - this.scrollOffset_.x, this.originalAnchorPos.y + this.scrollOffset_.y, this.originalAnchorPos.z);
		}
		this.OnScroll(new exUIEvent
		{
			bubbles = false
		});
	}

	// Token: 0x040005CF RID: 1487
	public new static string[] eventNames = new string[] { "onScroll", "onScrollFinished", "onContentResized" };

	// Token: 0x040005D0 RID: 1488
	private List<exUIEventListener> onScroll;

	// Token: 0x040005D1 RID: 1489
	private List<exUIEventListener> onScrollFinished;

	// Token: 0x040005D2 RID: 1490
	private List<exUIEventListener> onContentResized;

	// Token: 0x040005D3 RID: 1491
	[SerializeField]
	protected Vector2 contentSize_ = Vector2.zero;

	// Token: 0x040005D4 RID: 1492
	public bool draggable = true;

	// Token: 0x040005D5 RID: 1493
	public exUIScrollView.DragEffect dragEffect = exUIScrollView.DragEffect.MomentumAndSpring;

	// Token: 0x040005D6 RID: 1494
	public exUIScrollView.ShowCondition showCondition;

	// Token: 0x040005D7 RID: 1495
	public bool allowHorizontalScroll = true;

	// Token: 0x040005D8 RID: 1496
	public exUIScrollView.ContentDirection horizontalContentDir = exUIScrollView.ContentDirection.LeftToRight;

	// Token: 0x040005D9 RID: 1497
	public bool allowVerticalScroll = true;

	// Token: 0x040005DA RID: 1498
	public exUIScrollView.ContentDirection verticalContentDir;

	// Token: 0x040005DB RID: 1499
	public Transform contentAnchor;

	// Token: 0x040005DC RID: 1500
	public float scrollSpeed = 0.5f;

	// Token: 0x040005DD RID: 1501
	private Vector2 scrollOffset_ = Vector2.zero;

	// Token: 0x040005DE RID: 1502
	private bool dragging;

	// Token: 0x040005DF RID: 1503
	private int draggingID = -1;

	// Token: 0x040005E0 RID: 1504
	private Vector3 originalAnchorPos = Vector3.zero;

	// Token: 0x040005E1 RID: 1505
	private bool damping;

	// Token: 0x040005E2 RID: 1506
	private Vector2 velocity = Vector2.zero;

	// Token: 0x040005E3 RID: 1507
	private bool spring;

	// Token: 0x040005E4 RID: 1508
	private Vector3 scrollDest = Vector3.zero;

	// Token: 0x020015D4 RID: 5588
	public enum DragEffect
	{
		// Token: 0x040057E5 RID: 22501
		None,
		// Token: 0x040057E6 RID: 22502
		Momentum,
		// Token: 0x040057E7 RID: 22503
		MomentumAndSpring
	}

	// Token: 0x020015D5 RID: 5589
	public enum ShowCondition
	{
		// Token: 0x040057E9 RID: 22505
		Always,
		// Token: 0x040057EA RID: 22506
		OnlyIfNeeded,
		// Token: 0x040057EB RID: 22507
		WhenDragging
	}

	// Token: 0x020015D6 RID: 5590
	public enum ContentDirection
	{
		// Token: 0x040057ED RID: 22509
		TopToBottom,
		// Token: 0x040057EE RID: 22510
		BottomToTop,
		// Token: 0x040057EF RID: 22511
		LeftToRight,
		// Token: 0x040057F0 RID: 22512
		RightToLeft
	}
}
