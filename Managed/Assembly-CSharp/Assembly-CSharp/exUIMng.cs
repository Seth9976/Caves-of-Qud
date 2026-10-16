using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000DF RID: 223
public class exUIMng : MonoBehaviour
{
	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06000834 RID: 2100 RVA: 0x00034A0F File Offset: 0x00032C0F
	public static exUIMng inst
	{
		get
		{
			if (exUIMng.inst_ == null)
			{
				exUIMng.inst_ = global::UnityEngine.Object.FindFirstObjectByType(typeof(exUIMng)) as exUIMng;
			}
			return exUIMng.inst_;
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x00034A3C File Offset: 0x00032C3C
	public static List<exUIControl> GetRoutine(exUIControl _ctrl)
	{
		List<exUIControl> list = new List<exUIControl>();
		exUIControl exUIControl = _ctrl.parent;
		while (exUIControl != null)
		{
			list.Add(exUIControl);
			exUIControl = exUIControl.parent;
		}
		return list;
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x00034A70 File Offset: 0x00032C70
	public static exUIControl FindRoot(exUIControl _ctrl)
	{
		exUIControl exUIControl = null;
		exUIControl exUIControl2 = _ctrl;
		while (exUIControl2 != null)
		{
			exUIControl = exUIControl2;
			exUIControl2 = exUIControl2.parent;
		}
		return exUIControl;
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x00034A96 File Offset: 0x00032C96
	public static exUIControl FindParent(exUIControl _ctrl)
	{
		return _ctrl.FindParentComponent<exUIControl>();
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x00034A9E File Offset: 0x00032C9E
	public static void FindAndAddChild(exUIControl _ctrl)
	{
		_ctrl.children.Clear();
		exUIMng.FindAndAddChildRecursively(_ctrl, _ctrl.transform);
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x00034AB8 File Offset: 0x00032CB8
	private static void FindAndAddChildRecursively(exUIControl _ctrl, Transform _trans)
	{
		foreach (object obj in _trans)
		{
			Transform transform = (Transform)obj;
			exUIControl component = transform.GetComponent<exUIControl>();
			if (component)
			{
				_ctrl.AddChild(component);
				exUIMng.FindAndAddChild(component);
			}
			else
			{
				exUIMng.FindAndAddChildRecursively(_ctrl, transform);
			}
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x00034B2C File Offset: 0x00032D2C
	private void Awake()
	{
		this.Init();
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x00034B34 File Offset: 0x00032D34
	private void Update()
	{
		this.HandleEvents();
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x00034B3C File Offset: 0x00032D3C
	private void OnGUI()
	{
		if (this.showDebugInfoInGameView)
		{
			this.ShowDebugInfo(new Rect(10f, 10f, 300f, 300f));
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x00034B65 File Offset: 0x00032D65
	public void AddControl(exUIControl _ctrl)
	{
		if (this.controls.IndexOf(_ctrl) == -1)
		{
			this.controls.Add(_ctrl);
			exUIMng.FindAndAddChild(_ctrl);
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x00034B88 File Offset: 0x00032D88
	public void SetFocus(exUIControl _ctrl)
	{
		if (this.focus != _ctrl)
		{
			exUIControl exUIControl = this.focus;
			if (this.focus != null)
			{
				exUIFocusEvent exUIFocusEvent = new exUIFocusEvent();
				exUIFocusEvent.relatedTarget = this.focus;
				this.focus.OnUnfocus(exUIFocusEvent);
			}
			this.focus = _ctrl;
			if (this.focus != null)
			{
				exUIFocusEvent exUIFocusEvent2 = new exUIFocusEvent();
				exUIFocusEvent2.relatedTarget = exUIControl;
				this.focus.OnFocus(exUIFocusEvent2);
			}
		}
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x00034C04 File Offset: 0x00032E04
	public void DispatchEvent(exUIControl _sender, string _name, List<exUIEventListener> _listeners, exUIEvent _event)
	{
		_event.target = _sender;
		if (_event.bubbles)
		{
			List<exUIControl> routine = exUIMng.GetRoutine(_sender);
			if (_listeners != null)
			{
				for (int i = 0; i < _listeners.Count; i++)
				{
					if (_listeners[i].capturePhase)
					{
						_event.eventPhase = exUIEventPhase.Capture;
						for (int j = routine.Count - 1; j >= 0; j--)
						{
							exUIControl exUIControl = routine[j];
							List<exUIEventListener> eventListeners = exUIControl.GetEventListeners(_name);
							_event.currentTarget = exUIControl;
							this.DoDispatchEvent(exUIControl, eventListeners, _event);
							if (_event.isPropagationStopped)
							{
								return;
							}
						}
					}
				}
			}
			if (_listeners != null)
			{
				_event.eventPhase = exUIEventPhase.Target;
				_event.currentTarget = _sender;
				this.DoDispatchEvent(_sender, _listeners, _event);
				if (_event.isPropagationStopped)
				{
					return;
				}
			}
			_event.eventPhase = exUIEventPhase.Bubble;
			for (int k = 0; k < routine.Count; k++)
			{
				exUIControl exUIControl2 = routine[k];
				List<exUIEventListener> eventListeners2 = exUIControl2.GetEventListeners(_name);
				_event.currentTarget = exUIControl2;
				this.DoDispatchEvent(exUIControl2, eventListeners2, _event);
				if (_event.isPropagationStopped)
				{
					return;
				}
			}
			return;
		}
		_event.eventPhase = exUIEventPhase.Target;
		_event.currentTarget = _sender;
		this.DoDispatchEvent(_sender, _listeners, _event);
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x00034D2C File Offset: 0x00032F2C
	public void DispatchEvent(exUIControl _sender, string _name, exUIEvent _event)
	{
		List<exUIEventListener> eventListeners = _sender.GetEventListeners(_name);
		this.DispatchEvent(_sender, _name, eventListeners, _event);
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x00034D4C File Offset: 0x00032F4C
	private void DoDispatchEvent(exUIControl _sender, List<exUIEventListener> _listeners, exUIEvent _event)
	{
		int i = 0;
		while (i < _listeners.Count)
		{
			exUIEventListener exUIEventListener = _listeners[i];
			if (_event.eventPhase == exUIEventPhase.Capture)
			{
				if (exUIEventListener.capturePhase)
				{
					goto IL_002F;
				}
			}
			else if (_event.eventPhase != exUIEventPhase.Bubble || !exUIEventListener.capturePhase)
			{
				goto IL_002F;
			}
			IL_003B:
			i++;
			continue;
			IL_002F:
			exUIEventListener.func(_event);
			goto IL_003B;
		}
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x00034DA4 File Offset: 0x00032FA4
	private void Init()
	{
		if (this.initialized)
		{
			return;
		}
		if (base.GetComponent<Camera>() == null)
		{
			Debug.LogError("The exUIMng should attach to a camera");
			return;
		}
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			this.hasMouse = false;
			this.hasTouch = true;
		}
		else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
		{
			this.hasMouse = true;
			this.hasTouch = false;
		}
		for (int i = 0; i < 10; i++)
		{
			this.touchPoints[i] = new exHotPoint();
			this.touchPoints[i].Reset();
			this.touchPoints[i].id = i;
		}
		for (int j = 0; j < 3; j++)
		{
			this.mousePoints[j] = new exHotPoint();
			this.mousePoints[j].Reset();
			this.mousePoints[j].id = j;
			this.mousePoints[j].isMouse = true;
		}
		foreach (exUIControl exUIControl in global::UnityEngine.Object.FindObjectsByType(typeof(exUIControl), FindObjectsSortMode.None) as exUIControl[])
		{
			if (exUIMng.FindParent(exUIControl) == null)
			{
				this.AddControl(exUIControl);
			}
		}
		this.initialized = true;
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x00034ED0 File Offset: 0x000330D0
	public void HoverOut(exUIControl _ctrl, int _id)
	{
		exHotPoint[] array;
		if (this.hasTouch || this.simulateMouseAsTouch)
		{
			array = this.touchPoints;
		}
		else
		{
			array = this.mousePoints;
		}
		foreach (exHotPoint exHotPoint in array)
		{
			if (exHotPoint.active && exHotPoint.id == _id)
			{
				exUIPointInfo exUIPointInfo = default(exUIPointInfo);
				exUIPointInfo.id = exHotPoint.id;
				exUIPointInfo.pos = exHotPoint.pos;
				exUIPointInfo.delta = exHotPoint.delta;
				exUIPointInfo.worldPos = exHotPoint.worldPos;
				exUIPointInfo.worldDelta = exHotPoint.worldDelta;
				exUIPointEvent exUIPointEvent = new exUIPointEvent();
				exUIPointEvent.isMouse = exHotPoint.isMouse;
				exUIPointEvent.pointInfos = new exUIPointInfo[] { exUIPointInfo };
				if (_ctrl != null)
				{
					_ctrl.OnHoverOut(exUIPointEvent);
				}
				exHotPoint.hover = null;
				if (_ctrl.parent != null)
				{
					exUIPointEvent.Reset();
					exHotPoint.hover = _ctrl.parent;
					_ctrl.parent.OnHoverIn(exUIPointEvent);
					if (exHotPoint.isTouch)
					{
						exUIPointEvent.Reset();
						exHotPoint.pressed = _ctrl.parent;
						_ctrl.parent.OnPressDown(exUIPointEvent);
					}
				}
			}
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x00035010 File Offset: 0x00033210
	private void HandleEvents()
	{
		for (int i = 0; i < this.touchPoints.Length; i++)
		{
			exHotPoint exHotPoint = this.touchPoints[i];
			exHotPoint.active = false;
			exHotPoint.pressDown = false;
			exHotPoint.pressUp = false;
		}
		for (int j = 0; j < this.mousePoints.Length; j++)
		{
			exHotPoint exHotPoint2 = this.mousePoints[j];
			exHotPoint2.active = false;
			exHotPoint2.pressDown = false;
			exHotPoint2.pressUp = false;
		}
		if (this.hasMouse)
		{
			this.HandleMouse();
		}
		if (this.hasTouch)
		{
			this.HandleTouches();
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x00035098 File Offset: 0x00033298
	private void HandleHotPoints(exHotPoint[] _hotPoints, bool _isMouse)
	{
		int num = (_isMouse ? 1 : _hotPoints.Length);
		for (int i = 0; i < num; i++)
		{
			exHotPoint exHotPoint = _hotPoints[i];
			if (exHotPoint.active && (!(exHotPoint.pressed != null) || !exHotPoint.pressed.grabMouseOrTouch))
			{
				exUIControl hover = exHotPoint.hover;
				exUIControl exUIControl = this.PickControl(exHotPoint.pos);
				exHotPoint.hover = exUIControl;
				if (hover != exUIControl)
				{
					exUIPointInfo exUIPointInfo = default(exUIPointInfo);
					exUIPointInfo.id = exHotPoint.id;
					exUIPointInfo.pos = exHotPoint.pos;
					exUIPointInfo.delta = exHotPoint.delta;
					exUIPointInfo.worldPos = exHotPoint.worldPos;
					exUIPointInfo.worldDelta = exHotPoint.worldDelta;
					exUIPointEvent exUIPointEvent = new exUIPointEvent();
					exUIPointEvent.isMouse = exHotPoint.isMouse;
					exUIPointEvent.pointInfos = new exUIPointInfo[] { exUIPointInfo };
					if (hover != null)
					{
						hover.OnHoverOut(exUIPointEvent);
					}
					if (exUIControl != null)
					{
						exUIPointEvent.Reset();
						exUIControl.OnHoverIn(exUIPointEvent);
						if (exHotPoint.isTouch)
						{
							exHotPoint.pressDown = true;
						}
					}
				}
			}
		}
		if (_isMouse)
		{
			for (int j = 1; j < _hotPoints.Length; j++)
			{
				_hotPoints[j].hover = _hotPoints[0].hover;
			}
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis != 0f && _hotPoints[0].hover != null)
			{
				exUIWheelEvent exUIWheelEvent = new exUIWheelEvent();
				exUIWheelEvent.delta = axis;
				_hotPoints[0].hover.OnEXMouseWheel(exUIWheelEvent);
			}
		}
		foreach (exHotPoint exHotPoint2 in _hotPoints)
		{
			if (exHotPoint2.active && exHotPoint2.pressDown)
			{
				exUIControl exUIControl2 = exHotPoint2.hover;
				if (exHotPoint2.pressed != null && exHotPoint2.pressed.grabMouseOrTouch)
				{
					exUIControl2 = exHotPoint2.pressed;
				}
				if (exUIControl2 != null)
				{
					exUIPointInfo exUIPointInfo2 = default(exUIPointInfo);
					exUIPointInfo2.id = exHotPoint2.id;
					exUIPointInfo2.pos = exHotPoint2.pos;
					exUIPointInfo2.delta = exHotPoint2.delta;
					exUIPointInfo2.worldPos = exHotPoint2.worldPos;
					exUIPointInfo2.worldDelta = exHotPoint2.worldDelta;
					exUIControl2.OnPressDown(new exUIPointEvent
					{
						isMouse = exHotPoint2.isMouse,
						pointInfos = new exUIPointInfo[] { exUIPointInfo2 }
					});
				}
				exHotPoint2.pressed = exUIControl2;
			}
		}
		Dictionary<exUIControl, List<exHotPoint>> dictionary = new Dictionary<exUIControl, List<exHotPoint>>();
		foreach (exHotPoint exHotPoint3 in _hotPoints)
		{
			if (exHotPoint3.active && exHotPoint3.delta != Vector2.zero)
			{
				exUIControl exUIControl3 = exHotPoint3.hover;
				if (exHotPoint3.pressed != null && exHotPoint3.pressed.grabMouseOrTouch)
				{
					exUIControl3 = exHotPoint3.pressed;
				}
				if (exUIControl3 != null)
				{
					List<exHotPoint> list;
					if (dictionary.ContainsKey(exUIControl3))
					{
						list = dictionary[exUIControl3];
					}
					else
					{
						list = new List<exHotPoint>();
						dictionary.Add(exUIControl3, list);
					}
					list.Add(exHotPoint3);
				}
			}
		}
		foreach (KeyValuePair<exUIControl, List<exHotPoint>> keyValuePair in dictionary)
		{
			exUIPointEvent exUIPointEvent2 = new exUIPointEvent();
			exUIPointEvent2.pointInfos = new exUIPointInfo[keyValuePair.Value.Count];
			for (int m = 0; m < keyValuePair.Value.Count; m++)
			{
				exHotPoint exHotPoint4 = keyValuePair.Value[m];
				exUIPointInfo exUIPointInfo3 = default(exUIPointInfo);
				exUIPointInfo3.id = exHotPoint4.id;
				exUIPointInfo3.pos = exHotPoint4.pos;
				exUIPointInfo3.delta = exHotPoint4.delta;
				exUIPointInfo3.worldPos = exHotPoint4.worldPos;
				exUIPointInfo3.worldDelta = exHotPoint4.worldDelta;
				exUIPointEvent2.pointInfos[m] = exUIPointInfo3;
				exUIPointEvent2.isMouse = exHotPoint4.isMouse;
			}
			keyValuePair.Key.OnHoverMove(exUIPointEvent2);
		}
		foreach (exHotPoint exHotPoint5 in _hotPoints)
		{
			if (exHotPoint5.active && exHotPoint5.pressUp)
			{
				exUIPointInfo exUIPointInfo4 = default(exUIPointInfo);
				exUIPointInfo4.id = exHotPoint5.id;
				exUIPointInfo4.pos = exHotPoint5.pos;
				exUIPointInfo4.delta = exHotPoint5.delta;
				exUIPointInfo4.worldPos = exHotPoint5.worldPos;
				exUIPointInfo4.worldDelta = exHotPoint5.worldDelta;
				exUIPointEvent exUIPointEvent3 = new exUIPointEvent();
				exUIPointEvent3.isMouse = exHotPoint5.isMouse;
				exUIPointEvent3.pointInfos = new exUIPointInfo[] { exUIPointInfo4 };
				exUIControl exUIControl4 = exHotPoint5.hover;
				if (exHotPoint5.pressed != null && exHotPoint5.pressed.grabMouseOrTouch)
				{
					exUIControl4 = exHotPoint5.pressed;
				}
				if (exUIControl4 != null)
				{
					exUIControl4.OnPressUp(exUIPointEvent3);
					if (exHotPoint5.isTouch)
					{
						exUIControl4.OnHoverOut(exUIPointEvent3);
					}
				}
				exHotPoint5.pressed = null;
			}
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x000355F4 File Offset: 0x000337F4
	private void HandleTouches()
	{
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			if (touch.fingerId < 10)
			{
				exHotPoint exHotPoint = this.touchPoints[touch.fingerId];
				exHotPoint.active = true;
				if (!exHotPoint.active)
				{
					exHotPoint.Reset();
				}
				else
				{
					exHotPoint.pos = touch.position;
					exHotPoint.delta = touch.deltaPosition;
					Vector3 vector = base.GetComponent<Camera>().ScreenToWorldPoint(touch.position - touch.deltaPosition);
					exHotPoint.worldPos = base.GetComponent<Camera>().ScreenToWorldPoint(touch.position);
					exHotPoint.worldDelta = exHotPoint.worldPos - vector;
					exHotPoint.pressDown = touch.phase == TouchPhase.Began;
					exHotPoint.pressUp = touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended;
				}
			}
		}
		this.HandleHotPoints(this.touchPoints, false);
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x000356FC File Offset: 0x000338FC
	private void HandleMouse()
	{
		if (this.simulateMouseAsTouch)
		{
			for (int i = 0; i < 3; i++)
			{
				exHotPoint exHotPoint = this.touchPoints[i];
				exHotPoint.id = i;
				exHotPoint.active = Input.GetMouseButtonDown(i) || Input.GetMouseButton(i) || Input.GetMouseButtonUp(i);
				if (!exHotPoint.active)
				{
					exHotPoint.Reset();
				}
				else
				{
					Vector2 pos = exHotPoint.pos;
					exHotPoint.pos = Input.mousePosition;
					if (Input.GetMouseButtonDown(i))
					{
						exHotPoint.delta = Vector2.zero;
					}
					else
					{
						exHotPoint.delta = exHotPoint.pos - pos;
					}
					Vector3 worldPos = exHotPoint.worldPos;
					exHotPoint.worldPos = base.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
					exHotPoint.worldDelta = exHotPoint.worldPos - worldPos;
					exHotPoint.pressDown = Input.GetMouseButtonDown(i);
					exHotPoint.pressUp = Input.GetMouseButtonUp(i);
				}
			}
			this.HandleHotPoints(this.touchPoints, false);
			return;
		}
		for (int j = 0; j < 3; j++)
		{
			exHotPoint exHotPoint2 = this.mousePoints[j];
			exHotPoint2.active = true;
			if (!exHotPoint2.active)
			{
				exHotPoint2.Reset();
			}
			else
			{
				Vector2 pos2 = exHotPoint2.pos;
				exHotPoint2.pos = Input.mousePosition;
				exHotPoint2.delta = exHotPoint2.pos - pos2;
				Vector3 worldPos2 = exHotPoint2.worldPos;
				exHotPoint2.worldPos = base.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
				exHotPoint2.worldDelta = exHotPoint2.worldPos - worldPos2;
				exHotPoint2.pressDown = Input.GetMouseButtonDown(j);
				exHotPoint2.pressUp = Input.GetMouseButtonUp(j);
			}
		}
		this.HandleHotPoints(this.mousePoints, true);
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x000358C8 File Offset: 0x00033AC8
	private exUIControl PickControl(Vector2 _screenPos)
	{
		Vector3 vector = base.GetComponent<Camera>().ScreenToWorldPoint(_screenPos);
		this.controls.Sort(exUIMng.controlSorterByPrioirty);
		for (int i = 0; i < this.controls.Count; i++)
		{
			exUIControl exUIControl = this.controls[i];
			exUIControl exUIControl2 = this.RecursivelyGetUIControl(exUIControl, vector);
			if (exUIControl2 != null)
			{
				return exUIControl2;
			}
		}
		Ray ray = base.GetComponent<Camera>().ScreenPointToRay(_screenPos);
		ray.origin = new Vector3(ray.origin.x, ray.origin.y, base.GetComponent<Camera>().transform.position.z);
		RaycastHit[] array = Physics.RaycastAll(ray);
		List<exUIControl> list = new List<exUIControl>();
		foreach (RaycastHit raycastHit in array)
		{
			exUIControl component = raycastHit.collider.gameObject.GetComponent<exUIControl>();
			if (component && component.gameObject.activeInHierarchy && component.activeInHierarchy)
			{
				list.Add(component);
			}
		}
		if (list.Count > 0)
		{
			list.Sort(exUIMng.controlSorterByLevel);
			return list[list.Count - 1];
		}
		return null;
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x00035A10 File Offset: 0x00033C10
	private exUIControl RecursivelyGetUIControl(exUIControl _ctrl, Vector2 _worldPos)
	{
		if (!_ctrl.gameObject.activeSelf || !_ctrl.activeSelf)
		{
			return null;
		}
		bool flag;
		if (_ctrl.useCollider)
		{
			flag = true;
		}
		else
		{
			Vector2 vector = new Vector2(_worldPos.x - _ctrl.transform.position.x, _worldPos.y - _ctrl.transform.position.y);
			flag = _ctrl.GetLocalAABoundingRect().Contains(vector);
		}
		if (flag)
		{
			for (int i = 0; i < _ctrl.children.Count; i++)
			{
				exUIControl exUIControl = _ctrl.children[i];
				exUIControl exUIControl2 = this.RecursivelyGetUIControl(exUIControl, _worldPos);
				if (exUIControl2 != null)
				{
					return exUIControl2;
				}
			}
			if (!_ctrl.useCollider)
			{
				return _ctrl;
			}
		}
		return null;
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x00035AD4 File Offset: 0x00033CD4
	public void ShowDebugInfo(Rect _pos)
	{
		GUILayout.BeginArea(new Rect(_pos.x, _pos.y, _pos.width, _pos.height), "Debug", GUI.skin.window);
		GUILayout.Label("Keyboard Focus: " + (this.focus ? this.focus.name : "None"), Array.Empty<GUILayoutOption>());
		if (this.hasTouch || this.simulateMouseAsTouch)
		{
			for (int i = 0; i < this.touchPoints.Length; i++)
			{
				exHotPoint exHotPoint = this.touchPoints[i];
				if (exHotPoint.active)
				{
					GUILayout.Label("Touch[" + i.ToString() + "]", Array.Empty<GUILayoutOption>());
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Space(15f);
					GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
					GUILayout.Label("pos: " + exHotPoint.pos.ToString(), Array.Empty<GUILayoutOption>());
					GUILayout.Label("hover: " + (exHotPoint.hover ? exHotPoint.hover.name : "None"), Array.Empty<GUILayoutOption>());
					GUILayout.Label("pressed: " + (exHotPoint.pressed ? exHotPoint.pressed.name : "None"), Array.Empty<GUILayoutOption>());
					GUILayout.EndVertical();
					GUILayout.EndHorizontal();
				}
			}
		}
		if (this.hasMouse && !this.simulateMouseAsTouch)
		{
			GUILayout.Label("Mouse", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Space(15f);
			GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
			GUILayout.Label("pos: " + this.mousePoints[0].pos.ToString(), Array.Empty<GUILayoutOption>());
			GUILayout.Label("hover: " + (this.mousePoints[0].hover ? this.mousePoints[0].hover.name : "None"), Array.Empty<GUILayoutOption>());
			GUILayout.Label("left-pressed: " + (this.mousePoints[0].pressed ? this.mousePoints[0].pressed.name : "None"), Array.Empty<GUILayoutOption>());
			GUILayout.Label("right-pressed: " + (this.mousePoints[1].pressed ? this.mousePoints[1].pressed.name : "None"), Array.Empty<GUILayoutOption>());
			GUILayout.Label("middle-pressed: " + (this.mousePoints[2].pressed ? this.mousePoints[2].pressed.name : "None"), Array.Empty<GUILayoutOption>());
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
		GUILayout.Label("Root Controls", Array.Empty<GUILayoutOption>());
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Space(15f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		for (int j = 0; j < this.controls.Count; j++)
		{
			GUILayout.Label("[" + j.ToString() + "] " + this.controls[j].name, Array.Empty<GUILayoutOption>());
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}

	// Token: 0x040005A7 RID: 1447
	protected static exUIMng inst_ = null;

	// Token: 0x040005A8 RID: 1448
	private static ControlSorterByPriority controlSorterByPrioirty = new ControlSorterByPriority();

	// Token: 0x040005A9 RID: 1449
	private static ControlSorterByLevel controlSorterByLevel = new ControlSorterByLevel();

	// Token: 0x040005AA RID: 1450
	public bool simulateMouseAsTouch;

	// Token: 0x040005AB RID: 1451
	public bool showDebugInfo;

	// Token: 0x040005AC RID: 1452
	public bool showDebugInfoInGameView;

	// Token: 0x040005AD RID: 1453
	private bool initialized;

	// Token: 0x040005AE RID: 1454
	private bool hasMouse;

	// Token: 0x040005AF RID: 1455
	private bool hasTouch;

	// Token: 0x040005B0 RID: 1456
	private List<exUIControl> controls = new List<exUIControl>();

	// Token: 0x040005B1 RID: 1457
	private exHotPoint[] mousePoints = new exHotPoint[3];

	// Token: 0x040005B2 RID: 1458
	private exHotPoint[] touchPoints = new exHotPoint[10];

	// Token: 0x040005B3 RID: 1459
	private exUIControl focus;
}
