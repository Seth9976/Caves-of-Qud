using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class exUIControl : exPlane
{
	// Token: 0x060007DE RID: 2014 RVA: 0x00032F5B File Offset: 0x0003115B
	public void OnFocus(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onFocus", this.onFocus, _event);
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00032F74 File Offset: 0x00031174
	public void OnUnfocus(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onUnfocus", this.onUnfocus, _event);
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00032F8D File Offset: 0x0003118D
	public void OnActive(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onActive", this.onActive, _event);
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00032FA6 File Offset: 0x000311A6
	public void OnDeactive(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onDeactive", this.onDeactive, _event);
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00032FBF File Offset: 0x000311BF
	public void OnHoverIn(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onHoverIn", this.onHoverIn, _event);
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00032FD8 File Offset: 0x000311D8
	public void OnHoverOut(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onHoverOut", this.onHoverOut, _event);
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00032FF1 File Offset: 0x000311F1
	public void OnHoverMove(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onHoverMove", this.onHoverMove, _event);
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x0003300A File Offset: 0x0003120A
	public void OnPressDown(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onPressDown", this.onPressDown, _event);
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00033023 File Offset: 0x00031223
	public void OnPressUp(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onPressUp", this.onPressUp, _event);
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0003303C File Offset: 0x0003123C
	public void OnEXMouseWheel(exUIEvent _event)
	{
		exUIMng.inst.DispatchEvent(this, "onMouseWheel", this.onMouseWheel, _event);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00033058 File Offset: 0x00031258
	public virtual void CacheEventListeners()
	{
		this.onFocus = this.eventListenerTable["onFocus"];
		this.onUnfocus = this.eventListenerTable["onUnfocus"];
		this.onActive = this.eventListenerTable["onActive"];
		this.onDeactive = this.eventListenerTable["onDeactive"];
		this.onHoverIn = this.eventListenerTable["onHoverIn"];
		this.onHoverOut = this.eventListenerTable["onHoverOut"];
		this.onHoverMove = this.eventListenerTable["onHoverMove"];
		this.onPressDown = this.eventListenerTable["onPressDown"];
		this.onPressUp = this.eventListenerTable["onPressUp"];
		this.onMouseWheel = this.eventListenerTable["onMouseWheel"];
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00033144 File Offset: 0x00031344
	public virtual string[] GetEventNames()
	{
		string[] array = new string[exUIControl.eventNames.Length];
		for (int i = 0; i < exUIControl.eventNames.Length; i++)
		{
			array[i] = exUIControl.eventNames[i];
		}
		return array;
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0003317C File Offset: 0x0003137C
	private static void AddEventListeners(exUIControl _ctrl, string _eventName, List<exUIControl.SlotInfo> _slots)
	{
		foreach (exUIControl.SlotInfo slotInfo in _slots)
		{
			bool flag = false;
			if (!(slotInfo.receiver == null))
			{
				foreach (MonoBehaviour monoBehaviour in slotInfo.receiver.GetComponents<MonoBehaviour>())
				{
					if (!(monoBehaviour is exUIControl))
					{
						MethodInfo method = monoBehaviour.GetType().GetMethod(slotInfo.method, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { typeof(exUIEvent) }, null);
						if (method != null)
						{
							Action<exUIEvent> action = (Action<exUIEvent>)Delegate.CreateDelegate(typeof(Action<exUIEvent>), monoBehaviour, method);
							_ctrl.AddEventListener(_eventName, action, slotInfo.capturePhase);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					Debug.LogWarning("Can not find method " + slotInfo.method + " in " + slotInfo.receiver.name);
				}
			}
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x060007EB RID: 2027 RVA: 0x00033294 File Offset: 0x00031494
	// (set) Token: 0x060007EC RID: 2028 RVA: 0x000332D0 File Offset: 0x000314D0
	public bool activeInHierarchy
	{
		get
		{
			if (!this.active_)
			{
				return false;
			}
			exUIControl exUIControl = this.parent;
			while (exUIControl != null)
			{
				if (!exUIControl.active_)
				{
					return false;
				}
				exUIControl = exUIControl.parent;
			}
			return true;
		}
		set
		{
			if (this.active_ != value)
			{
				this.active_ = value;
				exUIEvent exUIEvent = new exUIEvent();
				exUIEvent.bubbles = false;
				if (this.active_)
				{
					this.OnActive(exUIEvent);
				}
				else
				{
					this.OnDeactive(exUIEvent);
				}
				for (int i = 0; i < this.children.Count; i++)
				{
					this.children[i].activeInHierarchy = value;
				}
			}
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x0003333A File Offset: 0x0003153A
	// (set) Token: 0x060007EE RID: 2030 RVA: 0x00033344 File Offset: 0x00031544
	public bool activeSelf
	{
		get
		{
			return this.active_;
		}
		set
		{
			if (this.active_ != value)
			{
				this.active_ = value;
				exUIEvent exUIEvent = new exUIEvent();
				exUIEvent.bubbles = false;
				if (this.active_)
				{
					this.OnActive(exUIEvent);
					return;
				}
				this.OnDeactive(exUIEvent);
			}
		}
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x00033388 File Offset: 0x00031588
	protected void Awake()
	{
		foreach (string text in this.GetEventNames())
		{
			if (!this.eventListenerTable.ContainsKey(text))
			{
				this.eventListenerTable.Add(text, new List<exUIEventListener>());
			}
		}
		for (int j = 0; j < this.events.Count; j++)
		{
			exUIControl.EventTrigger eventTrigger = this.events[j];
			exUIControl.AddEventListeners(this, eventTrigger.name, eventTrigger.slots);
		}
		this.CacheEventListeners();
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0003340B File Offset: 0x0003160B
	private void OnDestroy()
	{
		if (this.parent != null)
		{
			this.parent.RemoveChild(this);
		}
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00033428 File Offset: 0x00031628
	public void AddEventListener(string _name, Action<exUIEvent> _func, bool _capturePhase = false)
	{
		List<exUIEventListener> list = null;
		if (this.eventListenerTable.ContainsKey(_name))
		{
			list = this.eventListenerTable[_name];
			for (int i = 0; i < list.Count; i++)
			{
				exUIEventListener exUIEventListener = list[i];
				if (exUIEventListener.func == _func && exUIEventListener.capturePhase == _capturePhase)
				{
					return;
				}
			}
		}
		if (list == null)
		{
			list = new List<exUIEventListener>();
			this.eventListenerTable.Add(_name, list);
		}
		list.Add(new exUIEventListener
		{
			func = _func,
			capturePhase = _capturePhase
		});
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x000334B4 File Offset: 0x000316B4
	public void RemoveEventListener(string _name, Action<exUIEvent> _func, bool _capturePhase = false)
	{
		if (this.eventListenerTable.ContainsKey(_name))
		{
			List<exUIEventListener> list = this.eventListenerTable[_name];
			for (int i = 0; i < list.Count; i++)
			{
				exUIEventListener exUIEventListener = list[i];
				if (exUIEventListener.func == _func && exUIEventListener.capturePhase == _capturePhase)
				{
					list.RemoveAt(i);
					return;
				}
			}
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00033518 File Offset: 0x00031718
	public void DispatchEvent(string _name, exUIEvent _event)
	{
		if (this.eventListenerTable.ContainsKey(_name))
		{
			List<exUIEventListener> list = this.eventListenerTable[_name];
			exUIMng.inst.DispatchEvent(this, _name, list, _event);
		}
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x0003354E File Offset: 0x0003174E
	public List<exUIEventListener> GetEventListeners(string _name)
	{
		if (this.eventListenerTable.ContainsKey(_name))
		{
			return this.eventListenerTable[_name];
		}
		return null;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x0003356C File Offset: 0x0003176C
	public bool IsSelfOrAncestorOf(exUIControl _ctrl)
	{
		if (_ctrl == null)
		{
			return false;
		}
		if (_ctrl == this)
		{
			return true;
		}
		exUIControl exUIControl = _ctrl.parent;
		while (exUIControl != null)
		{
			if (exUIControl == this)
			{
				return true;
			}
			exUIControl = exUIControl.parent;
		}
		return false;
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x000335B4 File Offset: 0x000317B4
	public void AddChild(exUIControl _ctrl)
	{
		if (_ctrl == null)
		{
			return;
		}
		if (_ctrl.parent == this)
		{
			return;
		}
		if (_ctrl.IsSelfOrAncestorOf(this))
		{
			return;
		}
		exUIControl exUIControl = _ctrl.parent;
		if (exUIControl != null)
		{
			exUIControl.RemoveChild(_ctrl);
		}
		this.children.Add(_ctrl);
		_ctrl.parent = this;
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00033610 File Offset: 0x00031810
	public void RemoveChild(exUIControl _ctrl)
	{
		if (_ctrl == null)
		{
			return;
		}
		int num = this.children.IndexOf(_ctrl);
		if (num != -1)
		{
			this.children.RemoveAt(num);
			_ctrl.parent = null;
		}
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0003364B File Offset: 0x0003184B
	public void Internal_SetActive(bool _active)
	{
		this.active_ = _active;
	}

	// Token: 0x0400054C RID: 1356
	public static string[] eventNames = new string[] { "onFocus", "onUnfocus", "onActive", "onDeactive", "onHoverIn", "onHoverOut", "onHoverMove", "onPressDown", "onPressUp", "onMouseWheel" };

	// Token: 0x0400054D RID: 1357
	private List<exUIEventListener> onFocus;

	// Token: 0x0400054E RID: 1358
	private List<exUIEventListener> onUnfocus;

	// Token: 0x0400054F RID: 1359
	private List<exUIEventListener> onActive;

	// Token: 0x04000550 RID: 1360
	private List<exUIEventListener> onDeactive;

	// Token: 0x04000551 RID: 1361
	private List<exUIEventListener> onHoverIn;

	// Token: 0x04000552 RID: 1362
	private List<exUIEventListener> onHoverOut;

	// Token: 0x04000553 RID: 1363
	private List<exUIEventListener> onHoverMove;

	// Token: 0x04000554 RID: 1364
	private List<exUIEventListener> onPressDown;

	// Token: 0x04000555 RID: 1365
	private List<exUIEventListener> onPressUp;

	// Token: 0x04000556 RID: 1366
	private List<exUIEventListener> onMouseWheel;

	// Token: 0x04000557 RID: 1367
	[NonSerialized]
	public exUIControl parent;

	// Token: 0x04000558 RID: 1368
	[NonSerialized]
	public List<exUIControl> children = new List<exUIControl>();

	// Token: 0x04000559 RID: 1369
	[SerializeField]
	protected bool active_ = true;

	// Token: 0x0400055A RID: 1370
	public int priority;

	// Token: 0x0400055B RID: 1371
	public bool useCollider;

	// Token: 0x0400055C RID: 1372
	public bool grabMouseOrTouch;

	// Token: 0x0400055D RID: 1373
	public List<exUIControl.EventTrigger> events = new List<exUIControl.EventTrigger>();

	// Token: 0x0400055E RID: 1374
	protected Dictionary<string, List<exUIEventListener>> eventListenerTable = new Dictionary<string, List<exUIEventListener>>();

	// Token: 0x0400055F RID: 1375
	protected List<exUIEventListener>[] cachedEventListenerTable;

	// Token: 0x020015C5 RID: 5573
	[Serializable]
	public class SlotInfo
	{
		// Token: 0x040057B5 RID: 22453
		public GameObject receiver;

		// Token: 0x040057B6 RID: 22454
		public string method = "";

		// Token: 0x040057B7 RID: 22455
		public bool capturePhase;
	}

	// Token: 0x020015C6 RID: 5574
	[Serializable]
	public class EventTrigger
	{
		// Token: 0x0600D60A RID: 54794 RVA: 0x004F7659 File Offset: 0x004F5859
		public EventTrigger(string _name)
		{
			this.name = _name;
			this.slots = new List<exUIControl.SlotInfo>();
		}

		// Token: 0x040057B8 RID: 22456
		public string name;

		// Token: 0x040057B9 RID: 22457
		public List<exUIControl.SlotInfo> slots;
	}
}
