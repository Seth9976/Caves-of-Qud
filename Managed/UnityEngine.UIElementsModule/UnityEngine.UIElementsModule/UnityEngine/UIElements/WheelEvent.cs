using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000261 RID: 609
	public class WheelEvent : MouseEventBase<WheelEvent>
	{
		// Token: 0x060011FE RID: 4606 RVA: 0x0004B76C File Offset: 0x0004996C
		static WheelEvent()
		{
			EventBase<WheelEvent>.SetCreateFunction(() => new WheelEvent());
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0004B785 File Offset: 0x00049985
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x0004B78D File Offset: 0x0004998D
		public Vector3 delta { get; private set; }

		// Token: 0x06001201 RID: 4609 RVA: 0x0004B798 File Offset: 0x00049998
		public new static WheelEvent GetPooled(Event systemEvent)
		{
			WheelEvent pooled = MouseEventBase<WheelEvent>.GetPooled(systemEvent);
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.delta = systemEvent.delta;
			}
			return pooled;
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0004B7D0 File Offset: 0x000499D0
		internal static WheelEvent GetPooled(Vector3 delta, Vector3 mousePosition, EventModifiers modifiers = EventModifiers.None)
		{
			WheelEvent pooled = EventBase<WheelEvent>.GetPooled();
			pooled.delta = delta;
			pooled.mousePosition = mousePosition;
			pooled.modifiers = modifiers;
			return pooled;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0004B808 File Offset: 0x00049A08
		internal static WheelEvent GetPooled(Vector3 delta, IPointerEvent pointerEvent)
		{
			WheelEvent pooled = MouseEventBase<WheelEvent>.GetPooled(pointerEvent);
			pooled.delta = delta;
			return pooled;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0004B82A File Offset: 0x00049A2A
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004B83B File Offset: 0x00049A3B
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			this.delta = Vector3.zero;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0004B852 File Offset: 0x00049A52
		public WheelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0004B863 File Offset: 0x00049A63
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToElementUnderPointerOrPanelRoot(this, panel, PointerId.mousePointerId, base.mousePosition);
		}

		// Token: 0x04000A49 RID: 2633
		public const float scrollDeltaPerTick = 3f;
	}
}
