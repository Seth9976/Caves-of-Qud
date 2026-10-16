using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021C RID: 540
	public abstract class MouseCaptureEventBase<T> : PointerCaptureEventBase<T>, IMouseCaptureEvent where T : MouseCaptureEventBase<T>, new()
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x000475E6 File Offset: 0x000457E6
		public new IEventHandler relatedTarget
		{
			get
			{
				return base.relatedTarget;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x000475F0 File Offset: 0x000457F0
		public static T GetPooled(IEventHandler target, IEventHandler relatedTarget)
		{
			return PointerCaptureEventBase<T>.GetPooled(target, relatedTarget, 0);
		}
	}
}
