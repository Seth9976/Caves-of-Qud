using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B5 RID: 693
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public abstract class BaseFieldMouseDragger
	{
		// Token: 0x060013B8 RID: 5048 RVA: 0x0004F2D9 File Offset: 0x0004D4D9
		public void SetDragZone(VisualElement dragElement)
		{
			this.SetDragZone(dragElement, new Rect(0f, 0f, -1f, -1f));
		}

		// Token: 0x060013B9 RID: 5049
		public abstract void SetDragZone(VisualElement dragElement, Rect hotZone);
	}
}
