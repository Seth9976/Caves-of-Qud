using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200070B RID: 1803
	[Serializable]
	internal class EventDebuggerEventRecord
	{
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x000CBC71 File Offset: 0x000C9E71
		// (set) Token: 0x060033FB RID: 13307 RVA: 0x000CBC79 File Offset: 0x000C9E79
		public string eventBaseName { get; private set; }

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000CBC82 File Offset: 0x000C9E82
		// (set) Token: 0x060033FD RID: 13309 RVA: 0x000CBC8A File Offset: 0x000C9E8A
		public long eventTypeId { get; private set; }

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x060033FE RID: 13310 RVA: 0x000CBC93 File Offset: 0x000C9E93
		// (set) Token: 0x060033FF RID: 13311 RVA: 0x000CBC9B File Offset: 0x000C9E9B
		public ulong eventId { get; private set; }

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003400 RID: 13312 RVA: 0x000CBCA4 File Offset: 0x000C9EA4
		// (set) Token: 0x06003401 RID: 13313 RVA: 0x000CBCAC File Offset: 0x000C9EAC
		private ulong triggerEventId { get; set; }

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x000CBCB5 File Offset: 0x000C9EB5
		// (set) Token: 0x06003403 RID: 13315 RVA: 0x000CBCBD File Offset: 0x000C9EBD
		internal long timestamp { get; private set; }

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06003404 RID: 13316 RVA: 0x000CBCC6 File Offset: 0x000C9EC6
		// (set) Token: 0x06003405 RID: 13317 RVA: 0x000CBCCE File Offset: 0x000C9ECE
		public IEventHandler target { get; set; }

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06003406 RID: 13318 RVA: 0x000CBCD7 File Offset: 0x000C9ED7
		// (set) Token: 0x06003407 RID: 13319 RVA: 0x000CBCDF File Offset: 0x000C9EDF
		public bool hasUnderlyingPhysicalEvent { get; private set; }

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06003408 RID: 13320 RVA: 0x000CBCE8 File Offset: 0x000C9EE8
		// (set) Token: 0x06003409 RID: 13321 RVA: 0x000CBCF0 File Offset: 0x000C9EF0
		private bool isPropagationStopped { get; set; }

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x000CBCF9 File Offset: 0x000C9EF9
		// (set) Token: 0x0600340B RID: 13323 RVA: 0x000CBD01 File Offset: 0x000C9F01
		private bool isImmediatePropagationStopped { get; set; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000CBD0A File Offset: 0x000C9F0A
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x000CBD12 File Offset: 0x000C9F12
		public PropagationPhase propagationPhase { get; private set; }

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000CBD1B File Offset: 0x000C9F1B
		// (set) Token: 0x0600340F RID: 13327 RVA: 0x000CBD23 File Offset: 0x000C9F23
		private IEventHandler currentTarget { get; set; }

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000CBD2C File Offset: 0x000C9F2C
		// (set) Token: 0x06003411 RID: 13329 RVA: 0x000CBD34 File Offset: 0x000C9F34
		private bool dispatch { get; set; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000CBD3D File Offset: 0x000C9F3D
		// (set) Token: 0x06003413 RID: 13331 RVA: 0x000CBD45 File Offset: 0x000C9F45
		private Vector2 originalMousePosition { get; set; }

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000CBD4E File Offset: 0x000C9F4E
		// (set) Token: 0x06003415 RID: 13333 RVA: 0x000CBD56 File Offset: 0x000C9F56
		public EventModifiers modifiers { get; private set; }

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x000CBD5F File Offset: 0x000C9F5F
		// (set) Token: 0x06003417 RID: 13335 RVA: 0x000CBD67 File Offset: 0x000C9F67
		public Vector2 mousePosition { get; private set; }

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x000CBD70 File Offset: 0x000C9F70
		// (set) Token: 0x06003419 RID: 13337 RVA: 0x000CBD78 File Offset: 0x000C9F78
		public int clickCount { get; private set; }

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x000CBD81 File Offset: 0x000C9F81
		// (set) Token: 0x0600341B RID: 13339 RVA: 0x000CBD89 File Offset: 0x000C9F89
		public int button { get; private set; }

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x000CBD92 File Offset: 0x000C9F92
		// (set) Token: 0x0600341D RID: 13341 RVA: 0x000CBD9A File Offset: 0x000C9F9A
		public int pressedButtons { get; private set; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x000CBDA3 File Offset: 0x000C9FA3
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x000CBDAB File Offset: 0x000C9FAB
		public Vector3 delta { get; private set; }

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x000CBDB4 File Offset: 0x000C9FB4
		// (set) Token: 0x06003421 RID: 13345 RVA: 0x000CBDBC File Offset: 0x000C9FBC
		public char character { get; private set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000CBDC5 File Offset: 0x000C9FC5
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x000CBDCD File Offset: 0x000C9FCD
		public KeyCode keyCode { get; private set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000CBDD6 File Offset: 0x000C9FD6
		// (set) Token: 0x06003425 RID: 13349 RVA: 0x000CBDDE File Offset: 0x000C9FDE
		public string commandName { get; private set; }

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000CBDE7 File Offset: 0x000C9FE7
		// (set) Token: 0x06003427 RID: 13351 RVA: 0x000CBDEF File Offset: 0x000C9FEF
		public NavigationDeviceType deviceType { get; private set; }

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000CBDF8 File Offset: 0x000C9FF8
		// (set) Token: 0x06003429 RID: 13353 RVA: 0x000CBE00 File Offset: 0x000CA000
		public NavigationMoveEvent.Direction navigationDirection { get; private set; }

		// Token: 0x0600342A RID: 13354 RVA: 0x000CBE0C File Offset: 0x000CA00C
		private void Init(EventBase evt)
		{
			Type type = evt.GetType();
			this.eventBaseName = EventDebugger.GetTypeDisplayName(type);
			this.eventTypeId = evt.eventTypeId;
			this.eventId = evt.eventId;
			this.triggerEventId = evt.triggerEventId;
			this.timestamp = evt.timestamp;
			this.target = evt.target;
			this.isPropagationStopped = evt.isPropagationStopped;
			this.isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
			IMouseEvent mouseEvent = evt as IMouseEvent;
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			this.hasUnderlyingPhysicalEvent = mouseEvent != null && mouseEventInternal != null && mouseEventInternal.triggeredByOS;
			this.propagationPhase = evt.propagationPhase;
			this.originalMousePosition = evt.originalMousePosition;
			this.currentTarget = evt.currentTarget;
			this.dispatch = evt.dispatch;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.modifiers = mouseEvent.modifiers;
				this.mousePosition = mouseEvent.mousePosition;
				this.button = mouseEvent.button;
				this.pressedButtons = mouseEvent.pressedButtons;
				this.clickCount = mouseEvent.clickCount;
				WheelEvent wheelEvent = mouseEvent as WheelEvent;
				bool flag2 = wheelEvent != null;
				if (flag2)
				{
					this.delta = wheelEvent.delta;
				}
			}
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag3 = pointerEvent != null;
			if (flag3)
			{
				IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
				this.hasUnderlyingPhysicalEvent = pointerEvent != null && pointerEventInternal != null && pointerEventInternal.triggeredByOS;
				this.modifiers = pointerEvent.modifiers;
				this.mousePosition = pointerEvent.position;
				this.button = pointerEvent.button;
				this.pressedButtons = pointerEvent.pressedButtons;
				this.clickCount = pointerEvent.clickCount;
			}
			IKeyboardEvent keyboardEvent = evt as IKeyboardEvent;
			bool flag4 = keyboardEvent != null;
			if (flag4)
			{
				this.modifiers = keyboardEvent.modifiers;
				this.character = keyboardEvent.character;
				this.keyCode = keyboardEvent.keyCode;
			}
			ICommandEvent commandEvent = evt as ICommandEvent;
			bool flag5 = commandEvent != null;
			if (flag5)
			{
				this.commandName = commandEvent.commandName;
			}
			INavigationEvent navigationEvent = evt as INavigationEvent;
			bool flag6 = navigationEvent != null;
			if (flag6)
			{
				this.deviceType = navigationEvent.deviceType;
				NavigationMoveEvent navigationMoveEvent = evt as NavigationMoveEvent;
				bool flag7 = navigationMoveEvent != null;
				if (flag7)
				{
					this.navigationDirection = navigationMoveEvent.direction;
				}
			}
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000CC072 File Offset: 0x000CA272
		public EventDebuggerEventRecord(EventBase evt)
		{
			this.Init(evt);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000CC084 File Offset: 0x000CA284
		public string TimestampString()
		{
			long num = (long)((float)this.timestamp / 1000f * 10000000f);
			return new DateTime(num).ToString("HH:mm:ss.ffffff");
		}
	}
}
