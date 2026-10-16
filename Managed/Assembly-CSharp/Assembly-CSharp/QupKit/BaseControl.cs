using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QupKit
{
	// Token: 0x020003B3 RID: 947
	public class BaseControl
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000B26D2 File Offset: 0x000B08D2
		public EventSystem EventSystemManager
		{
			get
			{
				if (BaseControl._EventSystem == null)
				{
					BaseControl._EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
				}
				return BaseControl._EventSystem;
			}
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x000B26FA File Offset: 0x000B08FA
		public void Select()
		{
			this.rootObject.AddComponent<MakeSelected>();
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x000B2708 File Offset: 0x000B0908
		public void Select(GameObject go)
		{
			go.AddComponent<MakeSelected>();
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x000B2711 File Offset: 0x000B0911
		// (set) Token: 0x06002329 RID: 9001 RVA: 0x000B2728 File Offset: 0x000B0928
		public string Sprite
		{
			get
			{
				return this.rootObject.GetComponent<Image>().sprite.name;
			}
			set
			{
				this.rootObject.GetComponent<Image>().sprite = (Sprite)Resources.Load(value, typeof(Sprite));
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000B274F File Offset: 0x000B094F
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x000B2770 File Offset: 0x000B0970
		public string Label
		{
			get
			{
				return this.rootObject.transform.Find("Label").GetComponent<Text>().text;
			}
			set
			{
				this.rootObject.transform.Find("Label").GetComponent<Text>().text = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x000B2792 File Offset: 0x000B0992
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x000B27B3 File Offset: 0x000B09B3
		public string Text
		{
			get
			{
				return this.rootObject.transform.Find("Text").GetComponent<Text>().text;
			}
			set
			{
				this.rootObject.transform.Find("Text").GetComponent<Text>().text = value;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x000B27D5 File Offset: 0x000B09D5
		// (set) Token: 0x0600232F RID: 9007 RVA: 0x000B27FB File Offset: 0x000B09FB
		public string Icon
		{
			get
			{
				return this.rootObject.transform.Find("Icon").GetComponent<Image>().sprite.name;
			}
			set
			{
				this.rootObject.transform.Find("Icon").GetComponent<Image>().sprite = (Sprite)Resources.Load(value, typeof(Sprite));
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000B2831 File Offset: 0x000B0A31
		public GameObject FindChild(string path)
		{
			return this.rootObject.transform.Find(path).gameObject;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000B2849 File Offset: 0x000B0A49
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x000B2851 File Offset: 0x000B0A51
		public ControlLayout Layout
		{
			get
			{
				return this._Layout;
			}
			set
			{
				if (value.Parent == null)
				{
					this._Layout = value;
				}
				else
				{
					this._Layout = new ControlLayout(this._Layout);
				}
				this._Layout.Parent = this;
			}
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000B2884 File Offset: 0x000B0A84
		public virtual void MoveBy(Vector3 Offset, float Time = 0f)
		{
			LeanTween.finish(this.Layout);
			if (Time > 0f)
			{
				LeanTween.move(this.Layout, this.Layout.Offset + Offset, Time);
				return;
			}
			this.Layout.Offset += Offset;
			this.ApplyLayout();
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000B28E0 File Offset: 0x000B0AE0
		public virtual void MoveTo(Vector3 Offset, float Time = 0f)
		{
			LeanTween.finish(this.Layout);
			if (Time > 0f)
			{
				LeanTween.move(this.Layout, Offset, Time);
				return;
			}
			this.Layout.Offset = Offset;
			this.ApplyLayout();
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000B2916 File Offset: 0x000B0B16
		public virtual void OnCustomLayout()
		{
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000B2918 File Offset: 0x000B0B18
		public virtual void BeforeLayout()
		{
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000B291C File Offset: 0x000B0B1C
		public void ApplyLayout()
		{
			this.BeforeLayout();
			if (this.Layout.Anchor == ControlAnchor.Custom)
			{
				this.OnCustomLayout();
				return;
			}
			if (this.Layout.Anchor == ControlAnchor.Fill)
			{
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
				this.rootObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
				this.rootObject.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 0f);
			}
			if (this.Layout.Anchor >= ControlAnchor.TopLeft && this.Layout.Anchor <= ControlAnchor.BottomRight)
			{
				this.rootObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.Width, this.Height);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D = this.Layout.Offset;
			}
			switch (this.Layout.Anchor)
			{
			case ControlAnchor.TopLeft:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 1f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 1f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(this.Width / 2f + this.Layout.Margin.Left, -this.Height / 2f + this.Layout.Margin.Top, 0f);
				break;
			case ControlAnchor.Top:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 1f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0f + this.Layout.Margin.Left, -this.Height / 2f, 0f);
				break;
			case ControlAnchor.TopRight:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 1f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(-this.Width / 2f + this.Layout.Margin.Left, -this.Height / 2f + this.Layout.Margin.Top, 0f);
				break;
			case ControlAnchor.Left:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0.5f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0.5f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(this.Width / 2f + this.Layout.Margin.Left, 0f, 0f);
				break;
			case ControlAnchor.Middle:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0f, 0f, 0f);
				break;
			case ControlAnchor.Right:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0.5f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0.5f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(-this.Width / 2f - this.Layout.Margin.Right, 0f, 0f);
				break;
			case ControlAnchor.BottomLeft:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(0f, 0f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(this.Width / 2f + this.Layout.Margin.Left, this.Height / 2f - this.Layout.Margin.Bottom, 0f);
				break;
			case ControlAnchor.Bottom:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0f, this.Height / 2f - this.Layout.Margin.Bottom, 0f);
				break;
			case ControlAnchor.BottomRight:
				this.rootObject.GetComponent<RectTransform>().anchorMin = new Vector2(1f, 0f);
				this.rootObject.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 0f);
				this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(-this.Width / 2f - this.Layout.Margin.Right, this.Height / 2f - this.Layout.Margin.Bottom, 0f);
				break;
			}
			this.rootObject.GetComponent<RectTransform>().anchoredPosition3D += new Vector3(0f, 0f, this.Layout.Offset.z);
			for (int i = 0; i < this.Children.Count; i++)
			{
				this.Children[i].ApplyLayout();
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x000B300C File Offset: 0x000B120C
		public bool bWaiting
		{
			get
			{
				ScheduleComponent component = this.rootObject.GetComponent<ScheduleComponent>();
				return !(component == null) && component.Entries.Count > 0;
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000B303E File Offset: 0x000B123E
		public void After(float T, Action A)
		{
			if (this.rootObject.GetComponent<ScheduleComponent>() == null)
			{
				this.rootObject.AddComponent<ScheduleComponent>();
			}
			this.rootObject.GetComponent<ScheduleComponent>().After(T, A);
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x000B3071 File Offset: 0x000B1271
		// (set) Token: 0x0600233B RID: 9019 RVA: 0x000B3098 File Offset: 0x000B1298
		public RectTransform rectTransform
		{
			get
			{
				if (this._rectTransform == null)
				{
					this._rectTransform = this._rootObject.GetComponent<RectTransform>();
				}
				return this._rectTransform;
			}
			set
			{
				this._rectTransform = value;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x000B30A1 File Offset: 0x000B12A1
		// (set) Token: 0x0600233D RID: 9021 RVA: 0x000B30A9 File Offset: 0x000B12A9
		public GameObject rootObject
		{
			get
			{
				return this._rootObject;
			}
			set
			{
				this._rootObject = value;
				this.rectTransform = this._rootObject.GetComponent<RectTransform>();
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x000B30C4 File Offset: 0x000B12C4
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x000B3114 File Offset: 0x000B1314
		public float Width
		{
			get
			{
				float x;
				try
				{
					x = this.rectTransform.sizeDelta.x;
				}
				catch (Exception)
				{
					Debug.Log("no recttransform on " + this.rootObject.name);
					throw;
				}
				return x;
			}
			set
			{
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x000B3116 File Offset: 0x000B1316
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x000B3128 File Offset: 0x000B1328
		public float Height
		{
			get
			{
				return this.rectTransform.sizeDelta.y;
			}
			set
			{
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x000B3158 File Offset: 0x000B1358
		// (set) Token: 0x06002342 RID: 9026 RVA: 0x000B312C File Offset: 0x000B132C
		public Color MainColor
		{
			get
			{
				Image component = this.rootObject.GetComponent<Image>();
				if (component != null)
				{
					return component.color;
				}
				return new Color(1f, 0f, 1f, 1f);
			}
			set
			{
				Image component = this.rootObject.GetComponent<Image>();
				if (component != null)
				{
					component.color = value;
				}
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x000B319A File Offset: 0x000B139A
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x000B31B7 File Offset: 0x000B13B7
		public string Name
		{
			get
			{
				if (this.rootObject == null)
				{
					return null;
				}
				return this.rootObject.name;
			}
			set
			{
				if (this.rootObject == null)
				{
					return;
				}
				this.rootObject.name = value;
			}
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x000B31D4 File Offset: 0x000B13D4
		public T GetChildComponent<T>(string Child)
		{
			return this.rootObject.transform.Find(Child).GetComponent<T>();
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x000B31EC File Offset: 0x000B13EC
		public GameObject GetChildGameObject(string Name)
		{
			GameObject gameObject;
			try
			{
				gameObject = this.rootObject.transform.Find(Name).gameObject;
			}
			catch (Exception ex)
			{
				string text = "Exception getting control name ";
				string text2 = " : ";
				Exception ex2 = ex;
				Debug.LogError(text + Name + text2 + ((ex2 != null) ? ex2.ToString() : null));
				gameObject = null;
			}
			return gameObject;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x000B324C File Offset: 0x000B144C
		public BaseControl GetChild(string Name)
		{
			BaseControl baseControl;
			try
			{
				baseControl = this.ChildrenByName[Name];
			}
			catch (Exception ex)
			{
				string text = "Exception getting control name ";
				string text2 = " : ";
				Exception ex2 = ex;
				Debug.LogError(text + Name + text2 + ((ex2 != null) ? ex2.ToString() : null));
				baseControl = null;
			}
			return baseControl;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x000B32A0 File Offset: 0x000B14A0
		public BaseControl AddChild(BaseControl NewChild)
		{
			return this.AddChild(NewChild, new ControlLayout(ControlAnchor.Custom));
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x000B32B0 File Offset: 0x000B14B0
		public BaseControl AddChild(BaseControl NewChild, ControlLayout Layout)
		{
			NewChild.rootObject.transform.SetParent(this.rootObject.transform);
			NewChild.Hook = NewChild.rootObject.AddComponent<QupControlHook>();
			NewChild.Hook.Control = NewChild;
			NewChild.Layout = Layout;
			this.Children.Add(NewChild);
			this.ChildrenByName.Add(NewChild.Name, NewChild);
			this.ChildrenByControl.Add(NewChild.rootObject, NewChild);
			NewChild.AddOnClickHandler(delegate(BaseEventData D)
			{
				NewChild.OnClicked((PointerEventData)D);
			});
			NewChild.AddOnDragHandler(delegate(BaseEventData D)
			{
				NewChild.OnDrag((PointerEventData)D);
			});
			NewChild.Parent = this;
			return NewChild;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x000B33B0 File Offset: 0x000B15B0
		public virtual void AddPointerEnterHandler(Action<BaseEventData> A)
		{
			if (this.rootObject.GetComponent<EventTrigger>() == null)
			{
				this.rootObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback.AddListener(new UnityAction<BaseEventData>(A.Invoke));
			this.rootObject.GetComponent<EventTrigger>().triggers.Add(entry);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x000B3418 File Offset: 0x000B1618
		public static void AddEventHandler(GameObject rootObject, EventTriggerType Type, Action<BaseEventData> A)
		{
			if (rootObject.GetComponent<EventTrigger>() == null)
			{
				rootObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = Type;
			entry.callback.AddListener(new UnityAction<BaseEventData>(A.Invoke));
			rootObject.GetComponent<EventTrigger>().triggers.Add(entry);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x000B3470 File Offset: 0x000B1670
		public virtual void AddEventHandler(EventTriggerType Type, Action<BaseEventData> A)
		{
			if (this.rootObject.GetComponent<EventTrigger>() == null)
			{
				this.rootObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = Type;
			entry.callback.AddListener(new UnityAction<BaseEventData>(A.Invoke));
			this.rootObject.GetComponent<EventTrigger>().triggers.Add(entry);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x000B34D8 File Offset: 0x000B16D8
		public virtual void AddPointerExitHandler(Action<BaseEventData> A)
		{
			if (this.rootObject.GetComponent<EventTrigger>() == null)
			{
				this.rootObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerExit;
			entry.callback.AddListener(new UnityAction<BaseEventData>(A.Invoke));
			this.rootObject.GetComponent<EventTrigger>().triggers.Add(entry);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x000B3540 File Offset: 0x000B1740
		public virtual void AddOnClickHandler(Action<BaseEventData> A)
		{
			if (this.rootObject.GetComponent<EventTrigger>() == null)
			{
				this.rootObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(new UnityAction<BaseEventData>(A.Invoke));
			this.rootObject.GetComponent<EventTrigger>().triggers.Add(entry);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x000B35A8 File Offset: 0x000B17A8
		public virtual void AddOnDragHandler(Action<BaseEventData> A)
		{
			if (this.rootObject.GetComponent<EventTrigger>() == null)
			{
				this.rootObject.AddComponent<EventTrigger>();
			}
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener(new UnityAction<BaseEventData>(A.Invoke));
			this.rootObject.GetComponent<EventTrigger>().triggers.Add(entry);
		}

		// Token: 0x040011C7 RID: 4551
		public BaseControl Parent;

		// Token: 0x040011C8 RID: 4552
		private ControlLayout _Layout;

		// Token: 0x040011C9 RID: 4553
		private QupControlHook Hook;

		// Token: 0x040011CA RID: 4554
		private static EventSystem _EventSystem;

		// Token: 0x040011CB RID: 4555
		public RectTransform _rectTransform;

		// Token: 0x040011CC RID: 4556
		public GameObject _rootObject;

		// Token: 0x040011CD RID: 4557
		public Dictionary<GameObject, BaseControl> ChildrenByControl = new Dictionary<GameObject, BaseControl>();

		// Token: 0x040011CE RID: 4558
		public List<BaseControl> Children = new List<BaseControl>();

		// Token: 0x040011CF RID: 4559
		public Dictionary<string, BaseControl> ChildrenByName = new Dictionary<string, BaseControl>();

		// Token: 0x040011D0 RID: 4560
		public Action<PointerEventData> OnClicked = delegate(PointerEventData pointerEventData)
		{
		};

		// Token: 0x040011D1 RID: 4561
		public Action<PointerEventData> OnDrag = delegate(PointerEventData pointerEventData)
		{
		};
	}
}
