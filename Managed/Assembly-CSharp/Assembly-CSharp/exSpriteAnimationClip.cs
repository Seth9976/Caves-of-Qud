using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AD RID: 173
public class exSpriteAnimationClip : ScriptableObject
{
	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000628 RID: 1576 RVA: 0x0002CB5F File Offset: 0x0002AD5F
	// (set) Token: 0x06000629 RID: 1577 RVA: 0x0002CB67 File Offset: 0x0002AD67
	public float frameRate
	{
		get
		{
			return this.frameRate_;
		}
		set
		{
			if (value != this.frameRate_)
			{
				this.frameRate_ = (float)Mathf.RoundToInt(Mathf.Max(value, 1f));
			}
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0002CB8C File Offset: 0x0002AD8C
	public int GetTotalFrames()
	{
		int num = 0;
		for (int i = 0; i < this.frameInfos.Count; i++)
		{
			num += this.frameInfos[i].frames;
		}
		return num;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
	public int[] GetFrameInfoFrames()
	{
		if (this.frameInfoFrames == null)
		{
			this.frameInfoFrames = new int[this.frameInfos.Count];
			int num = 0;
			for (int i = 0; i < this.frameInfos.Count; i++)
			{
				num += this.frameInfos[i].frames;
				this.frameInfoFrames[i] = num;
			}
		}
		return this.frameInfoFrames;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0002CC2E File Offset: 0x0002AE2E
	public float GetLength()
	{
		return (float)this.GetTotalFrames() / this.frameRate_;
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0002CC3E File Offset: 0x0002AE3E
	public void AddFrame(exTextureInfo _info, int _frames = 1)
	{
		this.InsertFrameInfo(this.frameInfos.Count, new exSpriteAnimationClip.FrameInfo(_info, _frames));
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0002CC58 File Offset: 0x0002AE58
	public void AddFrameAt(int _idx, exTextureInfo _info, int _frames = 1)
	{
		this.InsertFrameInfo(_idx, new exSpriteAnimationClip.FrameInfo(_info, _frames));
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0002CC68 File Offset: 0x0002AE68
	public void RemoveFrame(exSpriteAnimationClip.FrameInfo _frameInfo)
	{
		this.frameInfos.Remove(_frameInfo);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0002CC77 File Offset: 0x0002AE77
	public void InsertFrameInfo(int _idx, exSpriteAnimationClip.FrameInfo _frameInfo)
	{
		this.frameInfos.Insert(_idx, _frameInfo);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0002CC88 File Offset: 0x0002AE88
	public void AddEmptyEvent(int _frame)
	{
		this.AddEvent(new exSpriteAnimationClip.EventInfo
		{
			frame = _frame
		});
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0002CCAC File Offset: 0x0002AEAC
	public void AddEvent(exSpriteAnimationClip.EventInfo _eventInfo)
	{
		if (this.eventInfos.Count == 0)
		{
			this.eventInfos.Insert(0, _eventInfo);
			return;
		}
		if (this.eventInfos.Count != 1)
		{
			bool flag = false;
			exSpriteAnimationClip.EventInfo eventInfo = this.eventInfos[0];
			for (int i = 1; i < this.eventInfos.Count; i++)
			{
				exSpriteAnimationClip.EventInfo eventInfo2 = this.eventInfos[i];
				if (_eventInfo.frame >= eventInfo.frame && _eventInfo.frame < eventInfo2.frame)
				{
					this.eventInfos.Insert(i, _eventInfo);
					flag = true;
					break;
				}
				eventInfo = eventInfo2;
			}
			if (!flag)
			{
				this.eventInfos.Insert(this.eventInfos.Count, _eventInfo);
			}
			return;
		}
		if (_eventInfo.frame >= this.eventInfos[0].frame)
		{
			this.eventInfos.Insert(1, _eventInfo);
			return;
		}
		this.eventInfos.Insert(0, _eventInfo);
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0002CD92 File Offset: 0x0002AF92
	public void RemoveEvent(exSpriteAnimationClip.EventInfo _eventInfo)
	{
		this.eventInfos.Remove(_eventInfo);
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0002CDA4 File Offset: 0x0002AFA4
	public Dictionary<int, List<exSpriteAnimationClip.EventInfo>> GetFrameToEventDict()
	{
		if (this.frameToEventDict == null)
		{
			this.frameToEventDict = new Dictionary<int, List<exSpriteAnimationClip.EventInfo>>();
			int num = -1;
			List<exSpriteAnimationClip.EventInfo> list = null;
			for (int i = 0; i < this.eventInfos.Count; i++)
			{
				exSpriteAnimationClip.EventInfo eventInfo = this.eventInfos[i];
				if (eventInfo.frame != num)
				{
					if (list != null)
					{
						this.frameToEventDict.Add(num, list);
					}
					list = new List<exSpriteAnimationClip.EventInfo>();
					num = eventInfo.frame;
				}
				list.Add(eventInfo);
			}
			if (list != null)
			{
				this.frameToEventDict.Add(num, list);
			}
		}
		return this.frameToEventDict;
	}

	// Token: 0x0400048F RID: 1167
	public WrapMode wrapMode = WrapMode.Once;

	// Token: 0x04000490 RID: 1168
	public exSpriteAnimationClip.StopAction stopAction;

	// Token: 0x04000491 RID: 1169
	[SerializeField]
	protected float frameRate_ = 60f;

	// Token: 0x04000492 RID: 1170
	public List<exSpriteAnimationClip.FrameInfo> frameInfos = new List<exSpriteAnimationClip.FrameInfo>();

	// Token: 0x04000493 RID: 1171
	public List<exSpriteAnimationClip.EventInfo> eventInfos = new List<exSpriteAnimationClip.EventInfo>();

	// Token: 0x04000494 RID: 1172
	[NonSerialized]
	private int[] frameInfoFrames;

	// Token: 0x04000495 RID: 1173
	[NonSerialized]
	private Dictionary<int, List<exSpriteAnimationClip.EventInfo>> frameToEventDict;

	// Token: 0x04000496 RID: 1174
	public float speed = 1f;

	// Token: 0x020015BC RID: 5564
	public enum StopAction
	{
		// Token: 0x04005793 RID: 22419
		DoNothing,
		// Token: 0x04005794 RID: 22420
		DefaultSprite,
		// Token: 0x04005795 RID: 22421
		Hide,
		// Token: 0x04005796 RID: 22422
		Destroy
	}

	// Token: 0x020015BD RID: 5565
	[Serializable]
	public class FrameInfo
	{
		// Token: 0x0600D5F5 RID: 54773 RVA: 0x004F7512 File Offset: 0x004F5712
		public FrameInfo(exTextureInfo _textureInfo, int _frames)
		{
			this.textureInfo = _textureInfo;
			this.frames = _frames;
		}

		// Token: 0x04005797 RID: 22423
		public exTextureInfo textureInfo;

		// Token: 0x04005798 RID: 22424
		public int frames = 1;
	}

	// Token: 0x020015BE RID: 5566
	[Serializable]
	public class EventInfo
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x0600D5F6 RID: 54774 RVA: 0x004F752F File Offset: 0x004F572F
		// (set) Token: 0x0600D5F7 RID: 54775 RVA: 0x004F753A File Offset: 0x004F573A
		public bool boolParam
		{
			get
			{
				return this.intParam != 0;
			}
			set
			{
				this.intParam = (value ? 1 : 0);
			}
		}

		// Token: 0x04005799 RID: 22425
		public int frame;

		// Token: 0x0400579A RID: 22426
		public string methodName = "";

		// Token: 0x0400579B RID: 22427
		public exSpriteAnimationClip.EventInfo.ParamType paramType;

		// Token: 0x0400579C RID: 22428
		public string stringParam = "";

		// Token: 0x0400579D RID: 22429
		public float floatParam;

		// Token: 0x0400579E RID: 22430
		public int intParam = -1;

		// Token: 0x0400579F RID: 22431
		public global::UnityEngine.Object objectParam;

		// Token: 0x040057A0 RID: 22432
		public SendMessageOptions msgOptions;

		// Token: 0x02001E70 RID: 7792
		public class SearchComparer : IComparer<exSpriteAnimationClip.EventInfo>
		{
			// Token: 0x0600F737 RID: 63287 RVA: 0x0055BA40 File Offset: 0x00559C40
			public static int BinarySearch(List<exSpriteAnimationClip.EventInfo> _list, int _frame)
			{
				exSpriteAnimationClip.EventInfo.SearchComparer.frame = _frame;
				if (exSpriteAnimationClip.EventInfo.SearchComparer.instance_ == null)
				{
					exSpriteAnimationClip.EventInfo.SearchComparer.instance_ = new exSpriteAnimationClip.EventInfo.SearchComparer();
				}
				return _list.BinarySearch(null, exSpriteAnimationClip.EventInfo.SearchComparer.instance_);
			}

			// Token: 0x0600F738 RID: 63288 RVA: 0x0055BA68 File Offset: 0x00559C68
			public int Compare(exSpriteAnimationClip.EventInfo _x, exSpriteAnimationClip.EventInfo _y)
			{
				if (_x == null && _y == null)
				{
					return 0;
				}
				if (_x != null)
				{
					if (_x.frame > exSpriteAnimationClip.EventInfo.SearchComparer.frame)
					{
						return 1;
					}
					if (_x.frame < exSpriteAnimationClip.EventInfo.SearchComparer.frame)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (exSpriteAnimationClip.EventInfo.SearchComparer.frame > _y.frame)
					{
						return 1;
					}
					if (exSpriteAnimationClip.EventInfo.SearchComparer.frame < _y.frame)
					{
						return -1;
					}
					return 0;
				}
			}

			// Token: 0x04007640 RID: 30272
			private static exSpriteAnimationClip.EventInfo.SearchComparer instance_;

			// Token: 0x04007641 RID: 30273
			private static int frame;
		}

		// Token: 0x02001E71 RID: 7793
		public enum ParamType
		{
			// Token: 0x04007643 RID: 30275
			None,
			// Token: 0x04007644 RID: 30276
			String,
			// Token: 0x04007645 RID: 30277
			Float,
			// Token: 0x04007646 RID: 30278
			Int,
			// Token: 0x04007647 RID: 30279
			Bool,
			// Token: 0x04007648 RID: 30280
			Object
		}
	}
}
