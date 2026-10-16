using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class exUIEffect : MonoBehaviour
{
	// Token: 0x0600080F RID: 2063 RVA: 0x00033AE8 File Offset: 0x00031CE8
	private void Awake()
	{
		exUIControl component = base.GetComponent<exUIControl>();
		if (component)
		{
			if (this.scaleInfos != null)
			{
				for (int i = 0; i < this.scaleInfos.Count; i++)
				{
					EffectInfo_Scale effectInfo_Scale = this.scaleInfos[i];
					this.AddState_Scale(component, new EffectState_Scale
					{
						info = effectInfo_Scale,
						func = effectInfo_Scale.GetCurveFunction()
					});
				}
			}
			if (this.offsetInfos != null)
			{
				for (int j = 0; j < this.offsetInfos.Count; j++)
				{
					EffectInfo_Offset effectInfo_Offset = this.offsetInfos[j];
					this.AddState_Offset(component, new EffectState_Offset
					{
						info = effectInfo_Offset,
						func = effectInfo_Offset.GetCurveFunction()
					});
				}
			}
			if (this.colorInfos != null)
			{
				for (int k = 0; k < this.colorInfos.Count; k++)
				{
					EffectInfo_Color effectInfo_Color = this.colorInfos[k];
					this.AddState_Color(component, new EffectState_Color
					{
						info = effectInfo_Color,
						func = effectInfo_Color.GetCurveFunction()
					});
				}
			}
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x00033C04 File Offset: 0x00031E04
	private void Update()
	{
		bool flag = true;
		for (int i = 0; i < this.states.Count; i++)
		{
			if (!this.states[i].Tick(Time.deltaTime))
			{
				flag = false;
			}
		}
		if (flag)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x00033C50 File Offset: 0x00031E50
	public void AddEffect_Scale(Transform _target, EffectEventType _type, exEase.Type _curveType, Vector3 _to, float _duration)
	{
		exUIControl component = base.GetComponent<exUIControl>();
		if (component)
		{
			EffectInfo_Scale effectInfo_Scale = new EffectInfo_Scale();
			effectInfo_Scale.duration = _duration;
			effectInfo_Scale.target = _target;
			effectInfo_Scale.normal = _target.localScale;
			effectInfo_Scale.curveType = _curveType;
			EffectInfo_Scale.PropInfo propInfo = new EffectInfo_Scale.PropInfo();
			propInfo.type = _type;
			propInfo.val = _to;
			effectInfo_Scale.propInfos.Add(propInfo);
			this.AddState_Scale(component, new EffectState_Scale
			{
				info = effectInfo_Scale,
				func = effectInfo_Scale.GetCurveFunction()
			});
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x00033CD8 File Offset: 0x00031ED8
	public void AddEffect_Color(exSpriteBase _target, EffectEventType _type, exEase.Type _curveType, Color _to, float _duration)
	{
		exUIControl component = base.GetComponent<exUIControl>();
		if (component)
		{
			EffectInfo_Color effectInfo_Color = new EffectInfo_Color();
			effectInfo_Color.duration = _duration;
			effectInfo_Color.target = _target;
			effectInfo_Color.normal = _target.color;
			effectInfo_Color.curveType = _curveType;
			EffectInfo_Color.PropInfo propInfo = new EffectInfo_Color.PropInfo();
			propInfo.type = _type;
			propInfo.val = _to;
			effectInfo_Color.propInfos.Add(propInfo);
			this.AddState_Color(component, new EffectState_Color
			{
				info = effectInfo_Color,
				func = effectInfo_Color.GetCurveFunction()
			});
		}
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x00033D60 File Offset: 0x00031F60
	public void AddEffect_Offset(exSpriteBase _target, EffectEventType _type, exEase.Type _curveType, Vector2 _to, float _duration)
	{
		exUIControl component = base.GetComponent<exUIControl>();
		if (component)
		{
			EffectInfo_Offset effectInfo_Offset = new EffectInfo_Offset();
			effectInfo_Offset.duration = _duration;
			effectInfo_Offset.target = _target;
			effectInfo_Offset.normal = _target.offset;
			effectInfo_Offset.curveType = _curveType;
			EffectInfo_Offset.PropInfo propInfo = new EffectInfo_Offset.PropInfo();
			propInfo.type = _type;
			propInfo.val = _to;
			effectInfo_Offset.propInfos.Add(propInfo);
			this.AddState_Offset(component, new EffectState_Offset
			{
				info = effectInfo_Offset,
				func = effectInfo_Offset.GetCurveFunction()
			});
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x00033DE8 File Offset: 0x00031FE8
	private void AddState_Scale(exUIControl _ctrl, EffectState_Scale _state)
	{
		Action<exUIEvent> <>9__1;
		Action<exUIEvent> <>9__3;
		Action<exUIEvent> <>9__4;
		Action<exUIEvent> <>9__6;
		Action<exUIEvent> <>9__8;
		for (int i = 0; i < _state.info.propInfos.Count; i++)
		{
			EffectInfo_Scale.PropInfo propInfo = _state.info.propInfos[i];
			switch (propInfo.type)
			{
			case EffectEventType.Deactive:
			{
				_ctrl.AddEventListener("onDeactive", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				exUIControl ctrl = _ctrl;
				string text = "onActive";
				Action<exUIEvent> action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.normal);
					});
				}
				ctrl.AddEventListener(text, action, false);
				break;
			}
			case EffectEventType.Press:
			{
				_ctrl.AddEventListener("onPressDown", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				exUIControl ctrl2 = _ctrl;
				string text2 = "onPressUp";
				Action<exUIEvent> action2;
				if ((action2 = <>9__3) == null)
				{
					action2 = (<>9__3 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.GetValue(EffectEventType.Hover));
					});
				}
				ctrl2.AddEventListener(text2, action2, false);
				exUIControl ctrl3 = _ctrl;
				string text3 = "onHoverOut";
				Action<exUIEvent> action3;
				if ((action3 = <>9__4) == null)
				{
					action3 = (<>9__4 = delegate(exUIEvent _event)
					{
						if (!_ctrl.grabMouseOrTouch)
						{
							this.enabled = true;
							_state.Begin(_state.info.normal);
						}
					});
				}
				ctrl3.AddEventListener(text3, action3, false);
				break;
			}
			case EffectEventType.Hover:
			{
				_ctrl.AddEventListener("onHoverIn", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				exUIControl ctrl4 = _ctrl;
				string text4 = "onHoverOut";
				Action<exUIEvent> action4;
				if ((action4 = <>9__6) == null)
				{
					action4 = (<>9__6 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.normal);
					});
				}
				ctrl4.AddEventListener(text4, action4, false);
				break;
			}
			case EffectEventType.Unchecked:
				if (_ctrl as exUIToggle != null)
				{
					_ctrl.AddEventListener("onUnchecked", delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(propInfo.val);
					}, false);
					exUIControl ctrl5 = _ctrl;
					string text5 = "onChecked";
					Action<exUIEvent> action5;
					if ((action5 = <>9__8) == null)
					{
						action5 = (<>9__8 = delegate(exUIEvent _event)
						{
							this.enabled = true;
							_state.Begin(_state.info.GetValue(EffectEventType.Hover));
						});
					}
					ctrl5.AddEventListener(text5, action5, false);
				}
				break;
			}
		}
		this.states.Add(_state);
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x000340AC File Offset: 0x000322AC
	private void AddState_Offset(exUIControl _ctrl, EffectState_Offset _state)
	{
		Action<exUIEvent> <>9__1;
		Action<exUIEvent> <>9__3;
		Action<exUIEvent> <>9__5;
		Action<exUIEvent> <>9__7;
		for (int i = 0; i < _state.info.propInfos.Count; i++)
		{
			EffectInfo_Offset.PropInfo propInfo = _state.info.propInfos[i];
			switch (propInfo.type)
			{
			case EffectEventType.Deactive:
			{
				_ctrl.AddEventListener("onDeactive", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				string text = "onActive";
				Action<exUIEvent> action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.normal);
					});
				}
				_ctrl.AddEventListener(text, action, false);
				break;
			}
			case EffectEventType.Press:
			{
				_ctrl.AddEventListener("onPressDown", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				string text2 = "onPressUp";
				Action<exUIEvent> action2;
				if ((action2 = <>9__3) == null)
				{
					action2 = (<>9__3 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.GetValue(EffectEventType.Hover));
					});
				}
				_ctrl.AddEventListener(text2, action2, false);
				break;
			}
			case EffectEventType.Hover:
			{
				_ctrl.AddEventListener("onHoverIn", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				string text3 = "onHoverOut";
				Action<exUIEvent> action3;
				if ((action3 = <>9__5) == null)
				{
					action3 = (<>9__5 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.normal);
					});
				}
				_ctrl.AddEventListener(text3, action3, false);
				break;
			}
			case EffectEventType.Unchecked:
				if (_ctrl as exUIToggle != null)
				{
					_ctrl.AddEventListener("onUnchecked", delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(propInfo.val);
					}, false);
					string text4 = "onChecked";
					Action<exUIEvent> action4;
					if ((action4 = <>9__7) == null)
					{
						action4 = (<>9__7 = delegate(exUIEvent _event)
						{
							this.enabled = true;
							_state.Begin(_state.info.GetValue(EffectEventType.Hover));
						});
					}
					_ctrl.AddEventListener(text4, action4, false);
				}
				break;
			}
		}
		this.states.Add(_state);
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x000342C4 File Offset: 0x000324C4
	private void AddState_Color(exUIControl _ctrl, EffectState_Color _state)
	{
		Action<exUIEvent> <>9__1;
		Action<exUIEvent> <>9__3;
		Action<exUIEvent> <>9__5;
		Action<exUIEvent> <>9__7;
		for (int i = 0; i < _state.info.propInfos.Count; i++)
		{
			EffectInfo_Color.PropInfo propInfo = _state.info.propInfos[i];
			switch (propInfo.type)
			{
			case EffectEventType.Deactive:
			{
				_ctrl.AddEventListener("onDeactive", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				string text = "onActive";
				Action<exUIEvent> action;
				if ((action = <>9__1) == null)
				{
					action = (<>9__1 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.normal);
					});
				}
				_ctrl.AddEventListener(text, action, false);
				break;
			}
			case EffectEventType.Press:
			{
				_ctrl.AddEventListener("onPressDown", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				string text2 = "onPressUp";
				Action<exUIEvent> action2;
				if ((action2 = <>9__3) == null)
				{
					action2 = (<>9__3 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.GetValue(EffectEventType.Hover));
					});
				}
				_ctrl.AddEventListener(text2, action2, false);
				break;
			}
			case EffectEventType.Hover:
			{
				_ctrl.AddEventListener("onHoverIn", delegate(exUIEvent _event)
				{
					this.enabled = true;
					_state.Begin(propInfo.val);
				}, false);
				string text3 = "onHoverOut";
				Action<exUIEvent> action3;
				if ((action3 = <>9__5) == null)
				{
					action3 = (<>9__5 = delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(_state.info.normal);
					});
				}
				_ctrl.AddEventListener(text3, action3, false);
				break;
			}
			case EffectEventType.Unchecked:
				if (_ctrl as exUIToggle != null)
				{
					_ctrl.AddEventListener("onUnchecked", delegate(exUIEvent _event)
					{
						this.enabled = true;
						_state.Begin(propInfo.val);
					}, false);
					string text4 = "onChecked";
					Action<exUIEvent> action4;
					if ((action4 = <>9__7) == null)
					{
						action4 = (<>9__7 = delegate(exUIEvent _event)
						{
							this.enabled = true;
							_state.Begin(_state.info.GetValue(EffectEventType.Hover));
						});
					}
					_ctrl.AddEventListener(text4, action4, false);
				}
				break;
			}
		}
		this.states.Add(_state);
	}

	// Token: 0x0400057E RID: 1406
	public List<EffectInfo_Scale> scaleInfos;

	// Token: 0x0400057F RID: 1407
	public List<EffectInfo_Offset> offsetInfos;

	// Token: 0x04000580 RID: 1408
	public List<EffectInfo_Color> colorInfos;

	// Token: 0x04000581 RID: 1409
	private List<EffectState_Base> states = new List<EffectState_Base>();
}
