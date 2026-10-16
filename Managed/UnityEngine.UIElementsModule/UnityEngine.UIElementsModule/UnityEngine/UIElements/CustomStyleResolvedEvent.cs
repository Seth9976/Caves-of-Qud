using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002A3 RID: 675
	[EventCategory(EventCategory.Style)]
	public class CustomStyleResolvedEvent : EventBase<CustomStyleResolvedEvent>
	{
		// Token: 0x06001374 RID: 4980 RVA: 0x0004EE78 File Offset: 0x0004D078
		static CustomStyleResolvedEvent()
		{
			EventBase<CustomStyleResolvedEvent>.SetCreateFunction(() => new CustomStyleResolvedEvent());
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001375 RID: 4981 RVA: 0x0004EE94 File Offset: 0x0004D094
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement elementTarget = base.elementTarget;
				return (elementTarget != null) ? elementTarget.customStyle : null;
			}
		}
	}
}
