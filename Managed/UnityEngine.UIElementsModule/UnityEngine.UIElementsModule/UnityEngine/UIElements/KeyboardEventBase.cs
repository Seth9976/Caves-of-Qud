using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200024E RID: 590
	[EventCategory(EventCategory.Keyboard)]
	public abstract class KeyboardEventBase<T> : EventBase<T>, IKeyboardEvent where T : KeyboardEventBase<T>, new()
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0004A7F1 File Offset: 0x000489F1
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x0004A7F9 File Offset: 0x000489F9
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x0004A802 File Offset: 0x00048A02
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x0004A80A File Offset: 0x00048A0A
		public char character { get; protected set; }

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x0004A813 File Offset: 0x00048A13
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x0004A81B File Offset: 0x00048A1B
		public KeyCode keyCode { get; protected set; }

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x0004A824 File Offset: 0x00048A24
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0004A844 File Offset: 0x00048A44
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x0004A864 File Offset: 0x00048A64
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0004A884 File Offset: 0x00048A84
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x0004A8A4 File Offset: 0x00048AA4
		internal bool functionKey
		{
			get
			{
				return (this.modifiers & EventModifiers.FunctionKey) > EventModifiers.None;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0004A8C4 File Offset: 0x00048AC4
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

		// Token: 0x0600117F RID: 4479 RVA: 0x0004A8FD File Offset: 0x00048AFD
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0004A90E File Offset: 0x00048B0E
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			this.modifiers = EventModifiers.None;
			this.character = '\0';
			this.keyCode = KeyCode.None;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0004A934 File Offset: 0x00048B34
		public static T GetPooled(char c, KeyCode keyCode, EventModifiers modifiers)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.character = c;
			pooled.keyCode = keyCode;
			return pooled;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0004A974 File Offset: 0x00048B74
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.character = systemEvent.character;
				pooled.keyCode = systemEvent.keyCode;
			}
			return pooled;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0004782E File Offset: 0x00045A2E
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToFocusedElementOrPanelRoot(this, panel);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0004A9DA File Offset: 0x00048BDA
		protected KeyboardEventBase()
		{
			this.LocalInit();
		}
	}
}
