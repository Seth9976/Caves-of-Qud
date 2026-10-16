using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007D RID: 125
	public abstract class ContextualMenuManager
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00016591 File Offset: 0x00014791
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x00016599 File Offset: 0x00014799
		internal bool displayMenuHandledOSX { get; set; }

		// Token: 0x060004B6 RID: 1206
		public abstract void DisplayMenuIfEventMatches(EventBase evt, IEventHandler eventHandler);

		// Token: 0x060004B7 RID: 1207 RVA: 0x000165A4 File Offset: 0x000147A4
		public void DisplayMenu(EventBase triggerEvent, IEventHandler target)
		{
			DropdownMenu dropdownMenu = new DropdownMenu();
			this.DisplayMenu(triggerEvent, target, dropdownMenu);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000165C4 File Offset: 0x000147C4
		internal void DisplayMenu(EventBase triggerEvent, IEventHandler target, DropdownMenu menu)
		{
			int num;
			int button;
			using (ContextualMenuPopulateEvent pooled = ContextualMenuPopulateEvent.GetPooled(triggerEvent, menu, target, this))
			{
				IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
				num = ((pointerEvent != null) ? pointerEvent.pointerId : PointerId.mousePointerId);
				button = pooled.button;
				if (target != null)
				{
					target.SendEvent(pooled);
				}
			}
			bool isOSXContextualMenuPlatform = UIElementsUtility.isOSXContextualMenuPlatform;
			if (isOSXContextualMenuPlatform)
			{
				this.displayMenuHandledOSX = true;
				bool flag = button >= 0;
				if (flag)
				{
					PointerDeviceState.ReleaseButton(num, button);
				}
			}
		}

		// Token: 0x060004B9 RID: 1209
		protected internal abstract void DoDisplayMenu(DropdownMenu menu, EventBase triggerEvent);
	}
}
