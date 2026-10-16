using System;
using UnityEngine.InputForUI;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028C RID: 652
	[EventCategory(EventCategory.Pointer)]
	public abstract class PointerEventBase<T> : EventBase<T>, IPointerEvent, IPointerEventInternal, IPointerOrMouseEvent where T : PointerEventBase<T>, new()
	{
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0004CD0E File Offset: 0x0004AF0E
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x0004CD16 File Offset: 0x0004AF16
		public int pointerId { get; protected set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004CD1F File Offset: 0x0004AF1F
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x0004CD27 File Offset: 0x0004AF27
		public string pointerType { get; protected set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0004CD30 File Offset: 0x0004AF30
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0004CD38 File Offset: 0x0004AF38
		public bool isPrimary { get; protected set; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0004CD41 File Offset: 0x0004AF41
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x0004CD49 File Offset: 0x0004AF49
		public int button { get; protected set; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004CD52 File Offset: 0x0004AF52
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x0004CD5A File Offset: 0x0004AF5A
		public int pressedButtons { get; protected set; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0004CD63 File Offset: 0x0004AF63
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0004CD6B File Offset: 0x0004AF6B
		public Vector3 position { get; protected set; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0004CD74 File Offset: 0x0004AF74
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0004CD7C File Offset: 0x0004AF7C
		public Vector3 localPosition { get; protected set; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0004CD85 File Offset: 0x0004AF85
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0004CD8D File Offset: 0x0004AF8D
		public Vector3 deltaPosition { get; protected set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0004CD96 File Offset: 0x0004AF96
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0004CD9E File Offset: 0x0004AF9E
		public float deltaTime { get; protected set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0004CDA7 File Offset: 0x0004AFA7
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x0004CDAF File Offset: 0x0004AFAF
		public int clickCount { get; protected set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0004CDB8 File Offset: 0x0004AFB8
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x0004CDC0 File Offset: 0x0004AFC0
		public float pressure { get; protected set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0004CDC9 File Offset: 0x0004AFC9
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x0004CDD1 File Offset: 0x0004AFD1
		public float tangentialPressure { get; protected set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0004CDDC File Offset: 0x0004AFDC
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x0004CE18 File Offset: 0x0004B018
		public float altitudeAngle
		{
			get
			{
				bool altitudeNeedsConversion = this.m_AltitudeNeedsConversion;
				if (altitudeNeedsConversion)
				{
					this.m_AltitudeAngle = PointerEventBase<T>.TiltToAltitude(this.tilt);
					this.m_AltitudeNeedsConversion = false;
				}
				return this.m_AltitudeAngle;
			}
			protected set
			{
				this.m_AltitudeNeedsConversion = true;
				this.m_AltitudeAngle = value;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0004CE2C File Offset: 0x0004B02C
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x0004CE68 File Offset: 0x0004B068
		public float azimuthAngle
		{
			get
			{
				bool azimuthNeedsConversion = this.m_AzimuthNeedsConversion;
				if (azimuthNeedsConversion)
				{
					this.m_AzimuthAngle = PointerEventBase<T>.TiltToAzimuth(this.tilt);
					this.m_AzimuthNeedsConversion = false;
				}
				return this.m_AzimuthAngle;
			}
			protected set
			{
				this.m_AzimuthNeedsConversion = true;
				this.m_AzimuthAngle = value;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0004CE79 File Offset: 0x0004B079
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0004CE81 File Offset: 0x0004B081
		public float twist { get; protected set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0004CE8C File Offset: 0x0004B08C
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x0004CEF3 File Offset: 0x0004B0F3
		public Vector2 tilt
		{
			get
			{
				bool flag = Application.platform != RuntimePlatform.WindowsEditor && Application.platform != RuntimePlatform.WindowsPlayer && this.pointerType == PointerType.touch && this.m_TiltNeeded;
				if (flag)
				{
					this.m_Tilt = PointerEventBase<T>.AzimuthAndAlitutudeToTilt(this.m_AltitudeAngle, this.m_AzimuthAngle);
					this.m_TiltNeeded = false;
				}
				return this.m_Tilt;
			}
			protected set
			{
				this.m_TiltNeeded = true;
				this.m_Tilt = value;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x0004CF04 File Offset: 0x0004B104
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x0004CF0C File Offset: 0x0004B10C
		public PenStatus penStatus { get; protected set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x0004CF15 File Offset: 0x0004B115
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x0004CF1D File Offset: 0x0004B11D
		public Vector2 radius { get; protected set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x0004CF26 File Offset: 0x0004B126
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x0004CF2E File Offset: 0x0004B12E
		public Vector2 radiusVariance { get; protected set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0004CF37 File Offset: 0x0004B137
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x0004CF3F File Offset: 0x0004B13F
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x0004CF48 File Offset: 0x0004B148
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x0004CF68 File Offset: 0x0004B168
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0004CF88 File Offset: 0x0004B188
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0004CFC8 File Offset: 0x0004B1C8
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x0004D001 File Offset: 0x0004B201
		// (set) Token: 0x060012FB RID: 4859 RVA: 0x0004D009 File Offset: 0x0004B209
		bool IPointerEventInternal.triggeredByOS { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0004D012 File Offset: 0x0004B212
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x0004D01A File Offset: 0x0004B21A
		IMouseEvent IPointerEventInternal.compatibilityMouseEvent { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x0004D023 File Offset: 0x0004B223
		// (set) Token: 0x060012FF RID: 4863 RVA: 0x0004D02B File Offset: 0x0004B22B
		int IPointerEventInternal.displayIndex { get; set; }

		// Token: 0x06001300 RID: 4864 RVA: 0x0004D034 File Offset: 0x0004B234
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004D048 File Offset: 0x0004B248
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.pointerId = 0;
			this.pointerType = PointerType.unknown;
			this.isPrimary = false;
			this.button = -1;
			this.pressedButtons = 0;
			this.position = Vector3.zero;
			this.localPosition = Vector3.zero;
			this.deltaPosition = Vector3.zero;
			this.deltaTime = 0f;
			this.clickCount = 0;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.tilt = new Vector2(0f, 0f);
			this.twist = 0f;
			this.penStatus = PenStatus.None;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
			this.modifiers = EventModifiers.None;
			((IPointerEventInternal)this).triggeredByOS = false;
			bool flag = ((IPointerEventInternal)this).compatibilityMouseEvent != null;
			if (flag)
			{
				((IDisposable)((IPointerEventInternal)this).compatibilityMouseEvent).Dispose();
				((IPointerEventInternal)this).compatibilityMouseEvent = null;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0004D16C File Offset: 0x0004B36C
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x0004D184 File Offset: 0x0004B384
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement visualElement = this.currentTarget as VisualElement;
				bool flag = visualElement != null;
				if (flag)
				{
					this.localPosition = visualElement.WorldToLocal(this.position);
				}
				else
				{
					this.localPosition = this.position;
				}
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0004D1E0 File Offset: 0x0004B3E0
		private static bool IsMouse(Event systemEvent)
		{
			EventType rawType = systemEvent.rawType;
			return rawType == EventType.MouseMove || rawType == EventType.MouseDown || rawType == EventType.MouseUp || rawType == EventType.MouseDrag || rawType == EventType.ContextClick || rawType == EventType.MouseEnterWindow || rawType == EventType.MouseLeaveWindow;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0004D21C File Offset: 0x0004B41C
		private static bool IsTouch(Event systemEvent)
		{
			EventType rawType = systemEvent.rawType;
			return rawType == EventType.TouchMove || rawType == EventType.TouchDown || rawType == EventType.TouchUp || rawType == EventType.TouchStationary || rawType == EventType.TouchEnter || rawType == EventType.TouchLeave;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0004D258 File Offset: 0x0004B458
		private static float TiltToAzimuth(Vector2 tilt)
		{
			float num = 0f;
			bool flag = tilt.x != 0f;
			if (flag)
			{
				num = 1.5707964f - Mathf.Atan2(-Mathf.Cos(tilt.x) * Mathf.Sin(tilt.y), Mathf.Cos(tilt.y) * Mathf.Sin(tilt.x));
				bool flag2 = num < 0f;
				if (flag2)
				{
					num += 6.2831855f;
				}
				bool flag3 = num >= 1.5707964f;
				if (flag3)
				{
					num -= 1.5707964f;
				}
				else
				{
					num += 4.712389f;
				}
			}
			return num;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0004D2FC File Offset: 0x0004B4FC
		private static Vector2 AzimuthAndAlitutudeToTilt(float altitude, float azimuth)
		{
			return new Vector2(0f, 0f)
			{
				x = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Cos(altitude) / Mathf.Sin(azimuth)),
				y = Mathf.Atan(Mathf.Cos(azimuth) * Mathf.Sin(altitude) / Mathf.Sin(azimuth))
			};
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004D360 File Offset: 0x0004B560
		private static float TiltToAltitude(Vector2 tilt)
		{
			return 1.5707964f - Mathf.Acos(Mathf.Cos(tilt.x) * Mathf.Cos(tilt.y));
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0004D394 File Offset: 0x0004B594
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = !PointerEventBase<T>.IsMouse(systemEvent) && !PointerEventBase<T>.IsTouch(systemEvent) && systemEvent.rawType != EventType.DragUpdated;
			if (flag)
			{
				Debug.Assert(false, string.Concat(new string[]
				{
					"Unexpected event type: ",
					systemEvent.rawType.ToString(),
					" (",
					systemEvent.type.ToString(),
					")"
				}));
			}
			PointerType pointerType = systemEvent.pointerType;
			PointerType pointerType2 = pointerType;
			if (pointerType2 != PointerType.Touch)
			{
				if (pointerType2 != PointerType.Pen)
				{
					pooled.pointerType = PointerType.mouse;
					pooled.pointerId = PointerId.mousePointerId;
				}
				else
				{
					pooled.pointerType = PointerType.pen;
					pooled.pointerId = PointerId.penPointerIdBase;
					bool flag2 = systemEvent.penStatus == PenStatus.Barrel;
					if (flag2)
					{
						PointerDeviceState.PressButton(pooled.pointerId, 1);
					}
					else
					{
						PointerDeviceState.ReleaseButton(pooled.pointerId, 1);
					}
					bool flag3 = systemEvent.penStatus == PenStatus.Eraser;
					if (flag3)
					{
						PointerDeviceState.PressButton(pooled.pointerId, 5);
					}
					else
					{
						PointerDeviceState.ReleaseButton(pooled.pointerId, 5);
					}
				}
			}
			else
			{
				pooled.pointerType = PointerType.touch;
				pooled.pointerId = PointerId.touchPointerIdBase;
			}
			pooled.isPrimary = true;
			pooled.altitudeAngle = 0f;
			pooled.azimuthAngle = 0f;
			pooled.radius = Vector2.zero;
			pooled.radiusVariance = Vector2.zero;
			pooled.imguiEvent = systemEvent;
			bool flag4 = systemEvent.rawType == EventType.MouseDown || systemEvent.rawType == EventType.TouchDown;
			if (flag4)
			{
				PointerDeviceState.PressButton(pooled.pointerId, systemEvent.button);
				pooled.button = systemEvent.button;
			}
			else
			{
				bool flag5 = systemEvent.rawType == EventType.MouseUp || systemEvent.rawType == EventType.TouchUp;
				if (flag5)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, systemEvent.button);
					pooled.button = systemEvent.button;
				}
				else
				{
					bool flag6 = systemEvent.rawType == EventType.MouseMove || systemEvent.rawType == EventType.TouchMove;
					if (flag6)
					{
						pooled.button = -1;
					}
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = systemEvent.mousePosition;
			pooled.localPosition = systemEvent.mousePosition;
			pooled.deltaPosition = systemEvent.delta;
			pooled.clickCount = systemEvent.clickCount;
			pooled.modifiers = systemEvent.modifiers;
			pooled.tilt = systemEvent.tilt;
			pooled.penStatus = systemEvent.penStatus;
			pooled.twist = systemEvent.twist;
			PointerType pointerType3 = systemEvent.pointerType;
			PointerType pointerType4 = pointerType3;
			if (pointerType4 != PointerType.Touch)
			{
				if (pointerType4 != PointerType.Pen)
				{
					pooled.pressure = ((pooled.pressedButtons == 0) ? 0f : 0.5f);
				}
				else
				{
					pooled.pressure = systemEvent.pressure;
				}
			}
			else
			{
				pooled.pressure = systemEvent.pressure;
			}
			pooled.tangentialPressure = 0f;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0004D77C File Offset: 0x0004B97C
		internal static T GetPooled(EventType eventType, Vector3 mousePosition, Vector2 delta, int button, int clickCount, EventModifiers modifiers, int displayIndex)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.pointerId = PointerId.mousePointerId;
			pooled.pointerType = PointerType.mouse;
			pooled.isPrimary = true;
			pooled.displayIndex = displayIndex;
			bool flag = eventType == EventType.MouseDown;
			if (flag)
			{
				PointerDeviceState.PressButton(pooled.pointerId, button);
				pooled.button = button;
			}
			else
			{
				bool flag2 = eventType == EventType.MouseUp;
				if (flag2)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, button);
					pooled.button = button;
				}
				else
				{
					pooled.button = -1;
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = mousePosition;
			pooled.localPosition = mousePosition;
			pooled.deltaPosition = delta;
			pooled.clickCount = clickCount;
			pooled.modifiers = modifiers;
			pooled.pressure = ((pooled.pressedButtons == 0) ? 0f : 0.5f);
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0004D8CD File Offset: 0x0004BACD
		public static T GetPooled(Touch touch, EventModifiers modifiers = EventModifiers.None)
		{
			return PointerEventBase<T>.GetPooled(touch, modifiers, 0);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004D8D8 File Offset: 0x0004BAD8
		internal static T GetPooled(Touch touch, EventModifiers modifiers, int displayIndex)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.pointerId = touch.fingerId + PointerId.touchPointerIdBase;
			pooled.pointerType = PointerType.touch;
			pooled.displayIndex = displayIndex;
			bool flag = false;
			for (int i = PointerId.touchPointerIdBase; i < PointerId.touchPointerIdBase + PointerId.touchPointerCount; i++)
			{
				bool flag2 = i != pooled.pointerId && PointerDeviceState.GetPressedButtons(i) != 0;
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			pooled.isPrimary = !flag;
			bool flag3 = touch.phase == TouchPhase.Began;
			if (flag3)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 0);
				pooled.button = 0;
			}
			else
			{
				bool flag4 = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
				if (flag4)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, 0);
					pooled.button = 0;
				}
				else
				{
					pooled.button = -1;
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = touch.position;
			pooled.localPosition = touch.position;
			pooled.deltaPosition = touch.deltaPosition;
			pooled.deltaTime = touch.deltaTime;
			pooled.clickCount = touch.tapCount;
			pooled.pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > 1E-30f) ? (touch.pressure / touch.maximumPossiblePressure) : 1f);
			pooled.tangentialPressure = 0f;
			pooled.altitudeAngle = touch.altitudeAngle;
			pooled.azimuthAngle = touch.azimuthAngle;
			pooled.twist = 0f;
			pooled.tilt = new Vector2(0f, 0f);
			pooled.penStatus = PenStatus.None;
			pooled.radius = new Vector2(touch.radius, touch.radius);
			pooled.radiusVariance = new Vector2(touch.radiusVariance, touch.radiusVariance);
			pooled.modifiers = modifiers;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004DB8A File Offset: 0x0004BD8A
		public static T GetPooled(PenData pen, EventModifiers modifiers = EventModifiers.None)
		{
			return PointerEventBase<T>.GetPooled(pen, modifiers, 0);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0004DB94 File Offset: 0x0004BD94
		internal static T GetPooled(PenData pen, EventModifiers modifiers, int displayIndex)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.pointerId = PointerId.penPointerIdBase;
			pooled.pointerType = PointerType.pen;
			pooled.displayIndex = displayIndex;
			pooled.isPrimary = true;
			bool flag = pen.contactType == PenEventType.PenDown;
			if (flag)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 0);
				pooled.button = 0;
			}
			else
			{
				bool flag2 = pen.contactType == PenEventType.PenUp;
				if (flag2)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, 0);
					pooled.button = 0;
				}
				else
				{
					pooled.button = -1;
				}
			}
			bool flag3 = pen.penStatus == PenStatus.Barrel;
			if (flag3)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 1);
			}
			else
			{
				PointerDeviceState.ReleaseButton(pooled.pointerId, 1);
			}
			bool flag4 = pen.penStatus == PenStatus.Eraser;
			if (flag4)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 5);
			}
			else
			{
				PointerDeviceState.ReleaseButton(pooled.pointerId, 5);
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = pen.position;
			pooled.localPosition = pen.position;
			pooled.deltaPosition = pen.deltaPos;
			pooled.clickCount = 0;
			pooled.pressure = pen.pressure;
			pooled.tangentialPressure = 0f;
			pooled.twist = pen.twist;
			pooled.tilt = pen.tilt;
			pooled.penStatus = pen.penStatus;
			pooled.radius = Vector2.zero;
			pooled.radiusVariance = Vector2.zero;
			pooled.modifiers = modifiers;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0004DDC8 File Offset: 0x0004BFC8
		internal static T GetPooled(PointerEvent pointerEvent, Vector2 position, Vector2 deltaPosition, int pointerId, float deltaTime)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.position = position;
			pooled.localPosition = position;
			pooled.deltaPosition = deltaPosition;
			pooled.pointerId = pointerId;
			pooled.deltaTime = deltaTime;
			pooled.displayIndex = pointerEvent.displayIndex;
			pooled.isPrimary = pointerEvent.isPrimaryPointer;
			pooled.button = -1;
			bool flag = pointerEvent.eventSource == EventSource.Mouse;
			if (flag)
			{
				pooled.pointerType = PointerType.mouse;
				Debug.Assert(pointerEvent.isPrimaryPointer, "PointerEvent from Mouse source is expected to be a primary pointer.");
				Debug.Assert(pointerId == PointerId.mousePointerId, "PointerEvent from Mouse source is expected to have mouse pointer id.");
				bool flag2 = pointerEvent.button == PointerEvent.Button.Primary;
				if (flag2)
				{
					pooled.button = 0;
				}
				else
				{
					bool flag3 = pointerEvent.button == PointerEvent.Button.PenEraserInTouch;
					if (flag3)
					{
						pooled.button = 1;
					}
					else
					{
						bool flag4 = pointerEvent.button == PointerEvent.Button.PenBarrelButton;
						if (flag4)
						{
							pooled.button = 2;
						}
					}
				}
			}
			else
			{
				bool flag5 = pointerEvent.eventSource == EventSource.Touch;
				if (flag5)
				{
					pooled.pointerType = PointerType.touch;
					Debug.Assert(pooled.pointerId >= PointerId.touchPointerIdBase && pooled.pointerId < PointerId.touchPointerIdBase + PointerId.touchPointerCount, "PointerEvent from Touch source is expected to have touch-based pointer id.");
					bool flag6 = pointerEvent.button == PointerEvent.Button.Primary;
					if (flag6)
					{
						pooled.button = 0;
					}
				}
				else
				{
					bool flag7 = pointerEvent.eventSource == EventSource.Pen;
					if (!flag7)
					{
						throw new ArgumentOutOfRangeException("pointerEvent", "Unsupported EventSource for pointer event");
					}
					pooled.pointerType = PointerType.pen;
					Debug.Assert(pooled.pointerId >= PointerId.penPointerIdBase && pooled.pointerId < PointerId.penPointerIdBase + PointerId.penPointerCount, "PointerEvent from Pen source is expected to have pen-based pointer id.");
					bool flag8 = pointerEvent.button == PointerEvent.Button.Primary;
					if (flag8)
					{
						pooled.button = 0;
					}
					else
					{
						bool flag9 = pointerEvent.button == PointerEvent.Button.PenBarrelButton;
						if (flag9)
						{
							pooled.button = 1;
						}
						else
						{
							bool flag10 = pointerEvent.button == PointerEvent.Button.PenEraserInTouch;
							if (flag10)
							{
								pooled.button = 5;
							}
						}
					}
				}
			}
			bool flag11 = pointerEvent.type == PointerEvent.Type.ButtonPressed;
			if (flag11)
			{
				Debug.Assert(pooled.button != -1, "PointerEvent of type ButtonPressed is expected to have button != -1.");
				PointerDeviceState.PressButton(pooled.pointerId, pooled.button);
			}
			else
			{
				bool flag12 = pointerEvent.type == PointerEvent.Type.ButtonReleased;
				if (flag12)
				{
					Debug.Assert(pooled.button != -1, "PointerEvent of type ButtonReleased is expected to have button != -1.");
					PointerDeviceState.ReleaseButton(pooled.pointerId, pooled.button);
				}
				else
				{
					bool flag13 = pointerEvent.type != PointerEvent.Type.TouchCanceled;
					if (flag13)
					{
						Debug.Assert(pooled.button == -1, "PointerEvent of type other than ButtonPressed, ButtonReleased, or TouchCanceled is expected to have button set to none.");
					}
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			bool flag14 = pointerEvent.eventSource == EventSource.Pen;
			if (flag14)
			{
				pooled.penStatus = PenStatus.None;
				bool flag15 = (pooled.pressedButtons & 1) != 0;
				if (flag15)
				{
					ref T ptr = ref pooled;
					ptr.penStatus |= PenStatus.Contact;
				}
				bool flag16 = (pooled.pressedButtons & 2) != 0;
				if (flag16)
				{
					ref T ptr = ref pooled;
					ptr.penStatus |= PenStatus.Barrel;
				}
				bool flag17 = (pooled.pressedButtons & 32) != 0;
				if (flag17)
				{
					ref T ptr = ref pooled;
					ptr.penStatus |= PenStatus.Eraser;
				}
				bool isInverted = pointerEvent.isInverted;
				if (isInverted)
				{
					ref T ptr = ref pooled;
					ptr.penStatus |= PenStatus.Inverted;
				}
			}
			pooled.clickCount = pointerEvent.clickCount;
			pooled.pressure = pointerEvent.pressure;
			pooled.altitudeAngle = pointerEvent.altitude;
			pooled.azimuthAngle = pointerEvent.azimuth;
			pooled.twist = pointerEvent.twist;
			pooled.tilt = pointerEvent.tilt;
			EventModifiers eventModifiers = EventModifiers.None;
			bool isShiftPressed = pointerEvent.eventModifiers.isShiftPressed;
			if (isShiftPressed)
			{
				eventModifiers |= EventModifiers.Shift;
			}
			bool isCtrlPressed = pointerEvent.eventModifiers.isCtrlPressed;
			if (isCtrlPressed)
			{
				eventModifiers |= EventModifiers.Control;
			}
			bool isAltPressed = pointerEvent.eventModifiers.isAltPressed;
			if (isAltPressed)
			{
				eventModifiers |= EventModifiers.Alt;
			}
			bool isMetaPressed = pointerEvent.eventModifiers.isMetaPressed;
			if (isMetaPressed)
			{
				eventModifiers |= EventModifiers.Command;
			}
			pooled.modifiers = eventModifiers;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004E324 File Offset: 0x0004C524
		internal static T GetPooled(IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = PointerEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.position = position;
				pooled.localPosition = position;
				pooled.pointerId = pointerId;
				pooled.pointerType = PointerType.GetPointerType(pointerId);
				t = pooled;
			}
			return t;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004E394 File Offset: 0x0004C594
		public static T GetPooled(IPointerEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.pointerId = triggerEvent.pointerId;
				pooled.pointerType = triggerEvent.pointerType;
				pooled.isPrimary = triggerEvent.isPrimary;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.position = triggerEvent.position;
				pooled.localPosition = triggerEvent.localPosition;
				pooled.deltaPosition = triggerEvent.deltaPosition;
				pooled.deltaTime = triggerEvent.deltaTime;
				pooled.clickCount = triggerEvent.clickCount;
				pooled.pressure = triggerEvent.pressure;
				pooled.tangentialPressure = triggerEvent.tangentialPressure;
				pooled.altitudeAngle = triggerEvent.altitudeAngle;
				pooled.azimuthAngle = triggerEvent.azimuthAngle;
				pooled.twist = triggerEvent.twist;
				pooled.tilt = triggerEvent.tilt;
				pooled.penStatus = triggerEvent.penStatus;
				pooled.radius = triggerEvent.radius;
				pooled.radiusVariance = triggerEvent.radiusVariance;
				pooled.modifiers = triggerEvent.modifiers;
				IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
				bool flag2 = pointerEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS |= pointerEventInternal.triggeredByOS;
				}
			}
			return pooled;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004E550 File Offset: 0x0004C750
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(this.pointerId, this.position, panel, panel.contextType);
			}
			EventBase eventBase = (EventBase)((IPointerEventInternal)this).compatibilityMouseEvent;
			if (eventBase != null)
			{
				eventBase.PreDispatch(panel);
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004E5A8 File Offset: 0x0004C7A8
		protected internal override void PostDispatch(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				panel.ProcessPointerCapture(i);
			}
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			EventBase eventBase = (EventBase)((IPointerEventInternal)this).compatibilityMouseEvent;
			if (eventBase != null)
			{
				eventBase.PostDispatch(panel);
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004E612 File Offset: 0x0004C812
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToCapturingElementOrElementUnderPointer(this, panel, this.pointerId, this.position);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004E630 File Offset: 0x0004C830
		protected PointerEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x04000A82 RID: 2690
		private const float k_DefaultButtonPressure = 0.5f;

		// Token: 0x04000A83 RID: 2691
		private bool m_AltitudeNeedsConversion = true;

		// Token: 0x04000A84 RID: 2692
		private bool m_AzimuthNeedsConversion = true;

		// Token: 0x04000A85 RID: 2693
		private float m_AltitudeAngle = 0f;

		// Token: 0x04000A86 RID: 2694
		private float m_AzimuthAngle = 0f;

		// Token: 0x04000A87 RID: 2695
		private bool m_TiltNeeded = true;

		// Token: 0x04000A88 RID: 2696
		private Vector2 m_Tilt = new Vector2(0f, 0f);
	}
}
