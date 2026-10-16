using System;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004B RID: 75
	[AddComponentMenu("UI Toolkit/Panel Event Handler (UI Toolkit)")]
	public class PanelEventHandler : UIBehaviour, IPointerMoveHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler, ISubmitHandler, ICancelHandler, IMoveHandler, IScrollHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler, IPointerEnterHandler, IRuntimePanelComponent, IPointerClickHandler
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000175A9 File Offset: 0x000157A9
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000175B4 File Offset: 0x000157B4
		public IPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)value;
				if (this.m_Panel != baseRuntimePanel)
				{
					this.UnregisterCallbacks();
					this.m_Panel = baseRuntimePanel;
					this.RegisterCallbacks();
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000175E4 File Offset: 0x000157E4
		private GameObject selectableGameObject
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.selectableGameObject;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000175F7 File Offset: 0x000157F7
		private EventSystem eventSystem
		{
			get
			{
				return UIElementsRuntimeUtility.activeEventSystem as EventSystem;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00017603 File Offset: 0x00015803
		private bool isCurrentFocusedPanel
		{
			get
			{
				return this.m_Panel != null && this.eventSystem != null && this.eventSystem.currentSelectedGameObject == this.selectableGameObject;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00017633 File Offset: 0x00015833
		private Focusable currentFocusedElement
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.focusController.GetLeafFocusedElement();
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001764B File Offset: 0x0001584B
		protected override void OnEnable()
		{
			base.OnEnable();
			this.RegisterCallbacks();
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00017659 File Offset: 0x00015859
		protected override void OnDisable()
		{
			base.OnDisable();
			this.UnregisterCallbacks();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017668 File Offset: 0x00015868
		private void RegisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed += this.OnPanelDestroyed;
				this.m_Panel.visualTree.RegisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.RegisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000176D0 File Offset: 0x000158D0
		private void UnregisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed -= this.OnPanelDestroyed;
				this.m_Panel.visualTree.UnregisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.UnregisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017736 File Offset: 0x00015936
		private void OnPanelDestroyed()
		{
			this.panel = null;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001773F File Offset: 0x0001593F
		private void OnElementFocus(FocusEvent e)
		{
			if (!this.m_Selecting && this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017768 File Offset: 0x00015968
		private void OnElementBlur(BlurEvent e)
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001776C File Offset: 0x0001596C
		public void OnSelect(BaseEventData eventData)
		{
			this.m_Selecting = true;
			try
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel != null)
				{
					panel.Focus();
				}
			}
			finally
			{
				this.m_Selecting = false;
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000177AC File Offset: 0x000159AC
		public void OnDeselect(BaseEventData eventData)
		{
			BaseRuntimePanel panel = this.m_Panel;
			if (panel == null)
			{
				return;
			}
			panel.Blur();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000177C0 File Offset: 0x000159C0
		public void OnPointerMove(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			using (PointerMoveEvent pooled = PointerEventBase<PointerMoveEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00017818 File Offset: 0x00015A18
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Up))
			{
				return;
			}
			using (PointerUpEvent pooled = PointerEventBase<PointerUpEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
				if (pooled.pressedButtons == 0)
				{
					PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pooled.pointerId, null);
				}
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00017884 File Offset: 0x00015A84
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Down))
			{
				return;
			}
			if (this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
			using (PointerDownEvent pooled = PointerEventBase<PointerDownEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
				PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pooled.pointerId, this.m_Panel);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001790C File Offset: 0x00015B0C
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			if (eventData.pointerCurrentRaycast.gameObject == base.gameObject && eventData.pointerPressRaycast.gameObject != base.gameObject && this.m_PointerEvent.pointerId != PointerId.mousePointerId)
			{
				using (PointerCancelEvent pooled = PointerEventBase<PointerCancelEvent>.GetPooled(this.m_PointerEvent))
				{
					this.SendEvent(pooled, eventData);
					if (pooled.pressedButtons == 0)
					{
						PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pooled.pointerId, null);
					}
				}
			}
			this.m_Panel.PointerLeavesPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000179E4 File Offset: 0x00015BE4
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			this.m_Panel.PointerEntersPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017A30 File Offset: 0x00015C30
		public void OnPointerClick(PointerEventData eventData)
		{
			this.m_LastClickTime = Time.unscaledTime;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017A40 File Offset: 0x00015C40
		public void OnSubmit(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			Focusable focusable = this.currentFocusedElement ?? this.m_Panel.visualTree;
			this.ProcessImguiEvents(focusable);
			using (NavigationSubmitEvent pooled = NavigationEventBase<NavigationSubmitEvent>.GetPooled(PanelEventHandler.s_Modifiers))
			{
				pooled.target = focusable;
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00017AAC File Offset: 0x00015CAC
		public void OnCancel(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			Focusable focusable = this.currentFocusedElement ?? this.m_Panel.visualTree;
			this.ProcessImguiEvents(focusable);
			using (NavigationCancelEvent pooled = NavigationEventBase<NavigationCancelEvent>.GetPooled(PanelEventHandler.s_Modifiers))
			{
				pooled.target = focusable;
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00017B18 File Offset: 0x00015D18
		public void OnMove(AxisEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			Focusable focusable = this.currentFocusedElement ?? this.m_Panel.visualTree;
			this.ProcessImguiEvents(focusable);
			using (NavigationMoveEvent pooled = NavigationMoveEvent.GetPooled(eventData.moveVector, PanelEventHandler.s_Modifiers))
			{
				pooled.target = focusable;
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00017B88 File Offset: 0x00015D88
		public void OnScroll(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			Vector2 scrollDelta = eventData.scrollDelta;
			Vector2 vector = this.eventSystem.currentInputModule.ConvertPointerEventScrollDeltaToTicks(scrollDelta) * 3f;
			vector.y = -vector.y;
			using (WheelEvent pooled = WheelEvent.GetPooled(vector, this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017C18 File Offset: 0x00015E18
		private void SendEvent(EventBase e, BaseEventData sourceEventData)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
			if (e.isPropagationStopped)
			{
				sourceEventData.Use();
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00017C35 File Offset: 0x00015E35
		private void SendEvent(EventBase e, Event sourceEvent)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00017C44 File Offset: 0x00015E44
		internal void Update()
		{
			if (this.isCurrentFocusedPanel)
			{
				this.ProcessImguiEvents(this.currentFocusedElement ?? this.m_Panel.visualTree);
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00017C69 File Offset: 0x00015E69
		private void LateUpdate()
		{
			this.ProcessImguiEvents(null);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00017C74 File Offset: 0x00015E74
		private void ProcessImguiEvents(Focusable target)
		{
			bool flag = true;
			while (Event.PopEvent(this.m_Event))
			{
				if (this.m_Event.type != EventType.Ignore && this.m_Event.type != EventType.Repaint && this.m_Event.type != EventType.Layout)
				{
					PanelEventHandler.s_Modifiers = (flag ? this.m_Event.modifiers : (PanelEventHandler.s_Modifiers | this.m_Event.modifiers));
					flag = false;
					if (target != null)
					{
						this.ProcessKeyboardEvent(this.m_Event, target);
						if (this.eventSystem.sendNavigationEvents)
						{
							this.ProcessTabEvent(this.m_Event, target);
						}
					}
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00017D12 File Offset: 0x00015F12
		private void ProcessKeyboardEvent(Event e, Focusable target)
		{
			if (e.type == EventType.KeyUp)
			{
				this.SendKeyUpEvent(e, target);
				return;
			}
			if (e.type == EventType.KeyDown)
			{
				this.SendKeyDownEvent(e, target);
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00017D37 File Offset: 0x00015F37
		private void ProcessTabEvent(Event e, Focusable target)
		{
			if (e.ShouldSendNavigationMoveEventRuntime())
			{
				this.SendTabEvent(e, e.shift ? NavigationMoveEvent.Direction.Previous : NavigationMoveEvent.Direction.Next, target);
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00017D58 File Offset: 0x00015F58
		private void SendTabEvent(Event e, NavigationMoveEvent.Direction direction, Focusable target)
		{
			using (NavigationMoveEvent pooled = NavigationMoveEvent.GetPooled(direction, PanelEventHandler.s_Modifiers))
			{
				pooled.target = target;
				this.SendEvent(pooled, e);
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00017D9C File Offset: 0x00015F9C
		private void SendKeyUpEvent(Event e, Focusable target)
		{
			using (KeyUpEvent keyUpEvent = (KeyUpEvent)UIElementsRuntimeUtility.CreateEvent(e))
			{
				keyUpEvent.target = target;
				this.SendEvent(keyUpEvent, e);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00017DE0 File Offset: 0x00015FE0
		private void SendKeyDownEvent(Event e, Focusable target)
		{
			using (KeyDownEvent keyDownEvent = (KeyDownEvent)UIElementsRuntimeUtility.CreateEvent(e))
			{
				keyDownEvent.target = target;
				this.SendEvent(keyDownEvent, e);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017E24 File Offset: 0x00016024
		private bool ReadPointerData(PanelEventHandler.PointerEvent pe, PointerEventData eventData, PanelEventHandler.PointerEventType eventType = PanelEventHandler.PointerEventType.Default)
		{
			if (this.eventSystem == null || this.eventSystem.currentInputModule == null)
			{
				return false;
			}
			pe.Read(this, eventData, eventType);
			Vector2 vector;
			Vector2 vector2;
			this.m_Panel.ScreenToPanel(pe.position, pe.deltaPosition, out vector, out vector2, true);
			pe.SetPosition(vector, vector2);
			return true;
		}

		// Token: 0x040001A5 RID: 421
		private BaseRuntimePanel m_Panel;

		// Token: 0x040001A6 RID: 422
		private readonly PanelEventHandler.PointerEvent m_PointerEvent = new PanelEventHandler.PointerEvent();

		// Token: 0x040001A7 RID: 423
		private float m_LastClickTime;

		// Token: 0x040001A8 RID: 424
		private bool m_Selecting;

		// Token: 0x040001A9 RID: 425
		private Event m_Event = new Event();

		// Token: 0x040001AA RID: 426
		private static EventModifiers s_Modifiers;

		// Token: 0x020000BD RID: 189
		private enum PointerEventType
		{
			// Token: 0x0400032F RID: 815
			Default,
			// Token: 0x04000330 RID: 816
			Down,
			// Token: 0x04000331 RID: 817
			Up
		}

		// Token: 0x020000BE RID: 190
		private class PointerEvent : IPointerEvent
		{
			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001C856 File Offset: 0x0001AA56
			// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001C85E File Offset: 0x0001AA5E
			public int pointerId { get; private set; }

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001C867 File Offset: 0x0001AA67
			// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001C86F File Offset: 0x0001AA6F
			public string pointerType { get; private set; }

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001C878 File Offset: 0x0001AA78
			// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001C880 File Offset: 0x0001AA80
			public bool isPrimary { get; private set; }

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001C889 File Offset: 0x0001AA89
			// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001C891 File Offset: 0x0001AA91
			public int button { get; private set; }

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001C89A File Offset: 0x0001AA9A
			// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001C8A2 File Offset: 0x0001AAA2
			public int pressedButtons { get; private set; }

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001C8AB File Offset: 0x0001AAAB
			// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001C8B3 File Offset: 0x0001AAB3
			public Vector3 position { get; private set; }

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001C8BC File Offset: 0x0001AABC
			// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001C8C4 File Offset: 0x0001AAC4
			public Vector3 localPosition { get; private set; }

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001C8CD File Offset: 0x0001AACD
			// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001C8D5 File Offset: 0x0001AAD5
			public Vector3 deltaPosition { get; private set; }

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001C8DE File Offset: 0x0001AADE
			// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001C8E6 File Offset: 0x0001AAE6
			public float deltaTime { get; private set; }

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001C8EF File Offset: 0x0001AAEF
			// (set) Token: 0x06000739 RID: 1849 RVA: 0x0001C8F7 File Offset: 0x0001AAF7
			public int clickCount { get; private set; }

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001C900 File Offset: 0x0001AB00
			// (set) Token: 0x0600073B RID: 1851 RVA: 0x0001C908 File Offset: 0x0001AB08
			public float pressure { get; private set; }

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001C911 File Offset: 0x0001AB11
			// (set) Token: 0x0600073D RID: 1853 RVA: 0x0001C919 File Offset: 0x0001AB19
			public float tangentialPressure { get; private set; }

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x0600073E RID: 1854 RVA: 0x0001C922 File Offset: 0x0001AB22
			// (set) Token: 0x0600073F RID: 1855 RVA: 0x0001C92A File Offset: 0x0001AB2A
			public float altitudeAngle { get; private set; }

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001C933 File Offset: 0x0001AB33
			// (set) Token: 0x06000741 RID: 1857 RVA: 0x0001C93B File Offset: 0x0001AB3B
			public float azimuthAngle { get; private set; }

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001C944 File Offset: 0x0001AB44
			// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001C94C File Offset: 0x0001AB4C
			public float twist { get; private set; }

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001C955 File Offset: 0x0001AB55
			// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001C95D File Offset: 0x0001AB5D
			public Vector2 tilt { get; private set; }

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001C966 File Offset: 0x0001AB66
			// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001C96E File Offset: 0x0001AB6E
			public PenStatus penStatus { get; private set; }

			// Token: 0x170001F1 RID: 497
			// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001C977 File Offset: 0x0001AB77
			// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001C97F File Offset: 0x0001AB7F
			public Vector2 radius { get; private set; }

			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001C988 File Offset: 0x0001AB88
			// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001C990 File Offset: 0x0001AB90
			public Vector2 radiusVariance { get; private set; }

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001C999 File Offset: 0x0001AB99
			// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001C9A1 File Offset: 0x0001ABA1
			public EventModifiers modifiers { get; private set; }

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001C9AA File Offset: 0x0001ABAA
			public bool shiftKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
				}
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x0600074F RID: 1871 RVA: 0x0001C9B7 File Offset: 0x0001ABB7
			public bool ctrlKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
				}
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001C9C4 File Offset: 0x0001ABC4
			public bool commandKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000751 RID: 1873 RVA: 0x0001C9D1 File Offset: 0x0001ABD1
			public bool altKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001C9DE File Offset: 0x0001ABDE
			public bool actionKey
			{
				get
				{
					if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer)
					{
						return this.ctrlKey;
					}
					return this.commandKey;
				}
			}

			// Token: 0x06000753 RID: 1875 RVA: 0x0001C9FC File Offset: 0x0001ABFC
			public void Read(PanelEventHandler self, PointerEventData eventData, PanelEventHandler.PointerEventType eventType)
			{
				this.pointerId = self.eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
				this.pointerType = (PanelEventHandler.PointerEvent.<Read>g__InRange|90_0(this.pointerId, PointerId.touchPointerIdBase, PointerId.touchPointerCount) ? PointerType.touch : (PanelEventHandler.PointerEvent.<Read>g__InRange|90_0(this.pointerId, PointerId.penPointerIdBase, PointerId.penPointerCount) ? PointerType.pen : PointerType.mouse));
				this.isPrimary = this.pointerId == PointerId.mousePointerId || this.pointerId == PointerId.touchPointerIdBase || this.pointerId == PointerId.penPointerIdBase;
				int num = Screen.height;
				Vector3 relativeMousePositionForRaycast = MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
				int num2 = (int)relativeMousePositionForRaycast.z;
				if (num2 > 0 && num2 < Display.displays.Length)
				{
					num = Display.displays[num2].systemHeight;
				}
				Vector2 delta = eventData.delta;
				relativeMousePositionForRaycast.y = (float)num - relativeMousePositionForRaycast.y;
				delta.y = -delta.y;
				this.localPosition = (this.position = relativeMousePositionForRaycast);
				this.deltaPosition = delta;
				this.deltaTime = 0f;
				this.pressure = eventData.pressure;
				this.tangentialPressure = eventData.tangentialPressure;
				this.altitudeAngle = eventData.altitudeAngle;
				this.azimuthAngle = eventData.azimuthAngle;
				this.twist = eventData.twist;
				this.tilt = eventData.tilt;
				this.penStatus = eventData.penStatus;
				this.radius = eventData.radius;
				this.radiusVariance = eventData.radiusVariance;
				this.modifiers = PanelEventHandler.s_Modifiers;
				if (eventType == PanelEventHandler.PointerEventType.Default)
				{
					this.button = -1;
					this.clickCount = 0;
				}
				else
				{
					this.button = Mathf.Max(0, (int)eventData.button);
					this.clickCount = eventData.clickCount;
					if (eventType == PanelEventHandler.PointerEventType.Down)
					{
						if (Time.unscaledTime > self.m_LastClickTime + (float)ClickDetector.s_DoubleClickTime * 0.001f)
						{
							this.clickCount = 0;
						}
						int clickCount = this.clickCount;
						this.clickCount = clickCount + 1;
						PointerDeviceState.PressButton(this.pointerId, this.button);
					}
					else if (eventType == PanelEventHandler.PointerEventType.Up)
					{
						PointerDeviceState.ReleaseButton(this.pointerId, this.button);
					}
					this.clickCount = Mathf.Max(1, this.clickCount);
				}
				this.pressedButtons = PointerDeviceState.GetPressedButtons(this.pointerId);
			}

			// Token: 0x06000754 RID: 1876 RVA: 0x0001CC44 File Offset: 0x0001AE44
			public void SetPosition(Vector3 positionOverride, Vector3 deltaOverride)
			{
				this.position = positionOverride;
				this.localPosition = positionOverride;
				this.deltaPosition = deltaOverride;
			}

			// Token: 0x06000756 RID: 1878 RVA: 0x0001CC70 File Offset: 0x0001AE70
			[CompilerGenerated]
			internal static bool <Read>g__InRange|90_0(int i, int start, int count)
			{
				return i >= start && i < start + count;
			}
		}
	}
}
