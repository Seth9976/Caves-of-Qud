using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000240 RID: 576
	[EventCategory(EventCategory.Focus)]
	public abstract class FocusEventBase<T> : EventBase<T>, IFocusEvent where T : FocusEventBase<T>, new()
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0004A45C File Offset: 0x0004865C
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x0004A464 File Offset: 0x00048664
		public Focusable relatedTarget { get; private set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0004A46D File Offset: 0x0004866D
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x0004A475 File Offset: 0x00048675
		public FocusChangeDirection direction { get; private set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004A47E File Offset: 0x0004867E
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x0004A486 File Offset: 0x00048686
		private protected FocusController focusController { protected get; private set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0004A48F File Offset: 0x0004868F
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x0004A497 File Offset: 0x00048697
		internal bool IsFocusDelegated { get; private set; }

		// Token: 0x06001137 RID: 4407 RVA: 0x0004A4A0 File Offset: 0x000486A0
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0004A4B1 File Offset: 0x000486B1
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.TricklesDown;
			this.relatedTarget = null;
			this.direction = FocusChangeDirection.unspecified;
			this.focusController = null;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0004A4D8 File Offset: 0x000486D8
		public static T GetPooled(IEventHandler target, Focusable relatedTarget, FocusChangeDirection direction, FocusController focusController, bool bIsFocusDelegated = false)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.elementTarget = (VisualElement)target;
			pooled.relatedTarget = relatedTarget;
			pooled.direction = direction;
			pooled.focusController = focusController;
			pooled.IsFocusDelegated = bIsFocusDelegated;
			return pooled;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0004A538 File Offset: 0x00048738
		protected FocusEventBase()
		{
			this.LocalInit();
		}
	}
}
