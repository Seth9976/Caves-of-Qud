using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000275 RID: 629
	[EventCategory(EventCategory.Navigation)]
	public abstract class NavigationEventBase<T> : EventBase<T>, INavigationEvent where T : NavigationEventBase<T>, new()
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x0004C3A4 File Offset: 0x0004A5A4
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x0004C3AC File Offset: 0x0004A5AC
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0004C3B8 File Offset: 0x0004A5B8
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0004C3D8 File Offset: 0x0004A5D8
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0004C3F8 File Offset: 0x0004A5F8
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0004C418 File Offset: 0x0004A618
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0004C438 File Offset: 0x0004A638
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

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x0004C471 File Offset: 0x0004A671
		NavigationDeviceType INavigationEvent.deviceType
		{
			get
			{
				return this.deviceType;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x0004C479 File Offset: 0x0004A679
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x0004C481 File Offset: 0x0004A681
		internal NavigationDeviceType deviceType { get; private set; }

		// Token: 0x0600125E RID: 4702 RVA: 0x0004C48A File Offset: 0x0004A68A
		protected NavigationEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0004C49B File Offset: 0x0004A69B
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0004C4AC File Offset: 0x0004A6AC
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			this.modifiers = EventModifiers.None;
			this.deviceType = NavigationDeviceType.Unknown;
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x0004C4C8 File Offset: 0x0004A6C8
		public static T GetPooled(EventModifiers modifiers = EventModifiers.None)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.deviceType = NavigationDeviceType.Unknown;
			return pooled;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004C4FC File Offset: 0x0004A6FC
		internal static T GetPooled(NavigationDeviceType deviceType, EventModifiers modifiers = EventModifiers.None)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.deviceType = deviceType;
			return pooled;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x0004782E File Offset: 0x00045A2E
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToFocusedElementOrPanelRoot(this, panel);
		}
	}
}
