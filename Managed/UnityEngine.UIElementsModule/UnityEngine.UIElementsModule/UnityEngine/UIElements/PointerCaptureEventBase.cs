using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000216 RID: 534
	[EventCategory(EventCategory.Pointer)]
	public abstract class PointerCaptureEventBase<T> : EventBase<T>, IPointerCaptureEvent, IPointerCaptureEventInternal where T : PointerCaptureEventBase<T>, new()
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00047499 File Offset: 0x00045699
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000474A1 File Offset: 0x000456A1
		public IEventHandler relatedTarget { get; private set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000474AA File Offset: 0x000456AA
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000474B2 File Offset: 0x000456B2
		public int pointerId { get; private set; }

		// Token: 0x0600101E RID: 4126 RVA: 0x000474BB File Offset: 0x000456BB
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x000474CC File Offset: 0x000456CC
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.relatedTarget = null;
			this.pointerId = PointerId.invalidPointerId;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000474EC File Offset: 0x000456EC
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget, int pointerId)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.elementTarget = (VisualElement)target;
			pooled.relatedTarget = relatedTarget;
			pooled.pointerId = pointerId;
			return pooled;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00047531 File Offset: 0x00045731
		protected PointerCaptureEventBase()
		{
			this.LocalInit();
		}
	}
}
