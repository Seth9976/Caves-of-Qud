using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class exUIButton : exUIControl
{
	// Token: 0x060007D2 RID: 2002 RVA: 0x00032C00 File Offset: 0x00030E00
	public void OnClick(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onClick", this.onClick, _event);
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00032C19 File Offset: 0x00030E19
	public void OnButtonDown(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onButtonDown", this.onButtonDown, _event);
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00032C32 File Offset: 0x00030E32
	public void OnButtonUp(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onButtonUp", this.onButtonUp, _event);
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00032C4C File Offset: 0x00030E4C
	public override void CacheEventListeners()
	{
		base.CacheEventListeners();
		this.onClick = this.eventListenerTable["onClick"];
		this.onButtonDown = this.eventListenerTable["onButtonDown"];
		this.onButtonUp = this.eventListenerTable["onButtonUp"];
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00032CA4 File Offset: 0x00030EA4
	public override string[] GetEventNames()
	{
		string[] array = base.GetEventNames();
		string[] array2 = new string[array.Length + exUIButton.eventNames.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = array[i];
		}
		for (int j = 0; j < exUIButton.eventNames.Length; j++)
		{
			array2[j + array.Length] = exUIButton.eventNames[j];
		}
		return array2;
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00032D00 File Offset: 0x00030F00
	protected new void Awake()
	{
		base.Awake();
		base.AddEventListener("onPressDown", delegate(exUIEvent _event)
		{
			if (this.pressing)
			{
				return;
			}
			exUIPointEvent exUIPointEvent = _event as exUIPointEvent;
			if (exUIPointEvent.isTouch || exUIPointEvent.GetMouseButton(0))
			{
				this.pressing = true;
				this.pressDownAt = exUIPointEvent.mainPoint.pos;
				this.pressingID = exUIPointEvent.mainPoint.id;
				exUIMng.inst.SetFocus(this);
				this.OnButtonDown(new exUIEvent
				{
					bubbles = false
				});
				_event.StopPropagation();
			}
		}, false);
		base.AddEventListener("onPressUp", delegate(exUIEvent _event)
		{
			exUIPointEvent exUIPointEvent2 = _event as exUIPointEvent;
			if (exUIPointEvent2.isTouch || exUIPointEvent2.GetMouseButton(0))
			{
				this.OnButtonUp(new exUIEvent
				{
					bubbles = false
				});
				if (this.pressing)
				{
					this.pressing = false;
					this.OnClick(new exUIEvent
					{
						bubbles = false
					});
					_event.StopPropagation();
				}
			}
		}, false);
		base.AddEventListener("onHoverOut", delegate(exUIEvent _event)
		{
			if (this.pressing)
			{
				this.pressing = false;
				this.pressingID = -1;
				_event.StopPropagation();
			}
		}, false);
		base.AddEventListener("onHoverMove", delegate(exUIEvent _event)
		{
			if (this.pressing)
			{
				exUIPointEvent exUIPointEvent3 = _event as exUIPointEvent;
				for (int i = 0; i < exUIPointEvent3.pointInfos.Length; i++)
				{
					exUIPointInfo exUIPointInfo = exUIPointEvent3.pointInfos[i];
					if (exUIPointInfo.id == this.pressingID)
					{
						Vector2 vector = exUIPointEvent3.mainPoint.pos - this.pressDownAt;
						if (!this.allowDrag && vector.sqrMagnitude >= this.dragThreshold * this.dragThreshold)
						{
							exUIMng.inst.HoverOut(this, exUIPointInfo.id);
						}
						else
						{
							_event.StopPropagation();
						}
					}
				}
			}
		}, false);
	}

	// Token: 0x04000543 RID: 1347
	public new static string[] eventNames = new string[] { "onClick", "onButtonDown", "onButtonUp" };

	// Token: 0x04000544 RID: 1348
	private List<exUIEventListener> onClick;

	// Token: 0x04000545 RID: 1349
	private List<exUIEventListener> onButtonDown;

	// Token: 0x04000546 RID: 1350
	private List<exUIEventListener> onButtonUp;

	// Token: 0x04000547 RID: 1351
	public bool allowDrag = true;

	// Token: 0x04000548 RID: 1352
	public float dragThreshold = 40f;

	// Token: 0x04000549 RID: 1353
	private bool pressing;

	// Token: 0x0400054A RID: 1354
	private int pressingID = -1;

	// Token: 0x0400054B RID: 1355
	private Vector2 pressDownAt = Vector2.zero;
}
