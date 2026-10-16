using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C0 RID: 192
[Serializable]
public class exSpriteAnimationState
{
	// Token: 0x0600079E RID: 1950 RVA: 0x00031CB5 File Offset: 0x0002FEB5
	public exSpriteAnimationState(exSpriteAnimationClip _animClip)
		: this(_animClip.name, _animClip)
	{
	}

	// Token: 0x0600079F RID: 1951 RVA: 0x00031CC4 File Offset: 0x0002FEC4
	public exSpriteAnimationState(string _name, exSpriteAnimationClip _animClip)
	{
		this.name = _name;
		this.clip = _animClip;
		this.wrapMode = this.clip.wrapMode;
		this.stopAction = this.clip.stopAction;
		this.speed = this.clip.speed;
		this.frameInfoFrames = this.clip.GetFrameInfoFrames();
		if (this.frameInfoFrames.Length != 0)
		{
			this.totalFrames = this.frameInfoFrames[this.frameInfoFrames.Length - 1];
		}
		else
		{
			this.totalFrames = 0;
		}
		this.length = (float)this.totalFrames / this.clip.frameRate;
		if (this.clip.eventInfos.Count >= 9)
		{
			this.frameToEventDict = this.clip.GetFrameToEventDict();
		}
	}

	// Token: 0x060007A0 RID: 1952 RVA: 0x00031DAC File Offset: 0x0002FFAC
	public int GetCurrentIndex()
	{
		if (this.totalFrames > 1)
		{
			this.frame = (int)(this.time * this.clip.frameRate);
			if (this.frame < 0)
			{
				this.frame = -this.frame;
			}
			int num;
			if (this.wrapMode != WrapMode.PingPong)
			{
				num = exMath.Wrap(this.frame, this.totalFrames - 1, this.wrapMode);
			}
			else
			{
				num = this.frame;
				int num2 = num / this.totalFrames;
				num %= this.totalFrames;
				if ((num2 & 1) == 1)
				{
					num = this.totalFrames - 1 - num;
				}
			}
			if (this.cachedIndex - 1 >= 0 && num >= this.frameInfoFrames[this.cachedIndex - 1] && num < this.frameInfoFrames[this.cachedIndex])
			{
				return this.cachedIndex;
			}
			int num3 = Array.BinarySearch<int>(this.frameInfoFrames, num + 1);
			if (num3 < 0)
			{
				num3 = ~num3;
			}
			this.cachedIndex = num3;
			return num3;
		}
		else
		{
			if (this.totalFrames == 1)
			{
				return 0;
			}
			return -1;
		}
	}

	// Token: 0x060007A1 RID: 1953 RVA: 0x00031EA0 File Offset: 0x000300A0
	public void TriggerEvents(Component _target, int _start, float _end)
	{
		if (this.clip.eventInfos.Count == 0)
		{
			return;
		}
		int num = _start;
		while ((float)num <= _end)
		{
			if (this.totalFrames == 0)
			{
				this.TriggerEvents(_target, 0, false);
			}
			else
			{
				bool flag = false;
				int num3;
				if (this.wrapMode == WrapMode.PingPong)
				{
					int num2 = num / this.totalFrames;
					num3 = num % this.totalFrames;
					flag = (num2 & 1) == 1;
					if (flag)
					{
						num3 = this.totalFrames - num3;
					}
				}
				else if (this.wrapMode == WrapMode.Loop)
				{
					num3 = exMath.Wrap(num, this.totalFrames - 1, this.wrapMode);
					if (num > 0 && num3 == 0)
					{
						this.TriggerEvents(_target, this.totalFrames, false);
					}
				}
				else
				{
					num3 = num;
				}
				this.TriggerEvents(_target, num3, flag);
			}
			num++;
		}
	}

	// Token: 0x060007A2 RID: 1954 RVA: 0x00031F5C File Offset: 0x0003015C
	private void TriggerEvents(Component _target, int _wrappedIndex, bool _reversed)
	{
		if (this.clip.eventInfos.Count == 0)
		{
			return;
		}
		List<exSpriteAnimationClip.EventInfo> eventInfos;
		if (this.frameToEventDict == null)
		{
			eventInfos = this.clip.eventInfos;
		}
		else if (!this.frameToEventDict.TryGetValue(_wrappedIndex, out eventInfos))
		{
			return;
		}
		if (_reversed)
		{
			for (int i = eventInfos.Count - 1; i >= 0; i--)
			{
				if (eventInfos[i].frame == _wrappedIndex)
				{
					this.Trigger(_target, eventInfos[i]);
				}
			}
			return;
		}
		for (int j = 0; j < eventInfos.Count; j++)
		{
			if (eventInfos[j].frame == _wrappedIndex)
			{
				this.Trigger(_target, eventInfos[j]);
			}
		}
	}

	// Token: 0x060007A3 RID: 1955 RVA: 0x00032008 File Offset: 0x00030208
	public void Trigger(Component _target, exSpriteAnimationClip.EventInfo _event)
	{
		if (_event.methodName == "")
		{
			return;
		}
		switch (_event.paramType)
		{
		case exSpriteAnimationClip.EventInfo.ParamType.None:
			_target.SendMessage(_event.methodName, _event.msgOptions);
			return;
		case exSpriteAnimationClip.EventInfo.ParamType.String:
			_target.SendMessage(_event.methodName, _event.stringParam, _event.msgOptions);
			return;
		case exSpriteAnimationClip.EventInfo.ParamType.Float:
			_target.SendMessage(_event.methodName, _event.floatParam, _event.msgOptions);
			return;
		case exSpriteAnimationClip.EventInfo.ParamType.Int:
			_target.SendMessage(_event.methodName, _event.intParam, _event.msgOptions);
			return;
		case exSpriteAnimationClip.EventInfo.ParamType.Bool:
			_target.SendMessage(_event.methodName, _event.boolParam, _event.msgOptions);
			return;
		case exSpriteAnimationClip.EventInfo.ParamType.Object:
			_target.SendMessage(_event.methodName, _event.objectParam, _event.msgOptions);
			return;
		default:
			return;
		}
	}

	// Token: 0x04000529 RID: 1321
	[NonSerialized]
	public exSpriteAnimationClip clip;

	// Token: 0x0400052A RID: 1322
	[NonSerialized]
	public string name;

	// Token: 0x0400052B RID: 1323
	[NonSerialized]
	public WrapMode wrapMode;

	// Token: 0x0400052C RID: 1324
	[NonSerialized]
	public exSpriteAnimationClip.StopAction stopAction;

	// Token: 0x0400052D RID: 1325
	[NonSerialized]
	public float length;

	// Token: 0x0400052E RID: 1326
	[NonSerialized]
	public int totalFrames;

	// Token: 0x0400052F RID: 1327
	[NonSerialized]
	public float speed = 1f;

	// Token: 0x04000530 RID: 1328
	[NonSerialized]
	public float time;

	// Token: 0x04000531 RID: 1329
	[NonSerialized]
	public int frame = -1;

	// Token: 0x04000532 RID: 1330
	[NonSerialized]
	private int[] frameInfoFrames;

	// Token: 0x04000533 RID: 1331
	[NonSerialized]
	private int cachedIndex = -1;

	// Token: 0x04000534 RID: 1332
	[NonSerialized]
	private Dictionary<int, List<exSpriteAnimationClip.EventInfo>> frameToEventDict;
}
