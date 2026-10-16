using System;
using UnityEngine;

namespace Qud.UI
{
	// Token: 0x02000421 RID: 1057
	public abstract class QudBaseMenuController : MonoBehaviour
	{
		// Token: 0x0600280E RID: 10254 RVA: 0x000CF1C0 File Offset: 0x000CD3C0
		public virtual void SetSelected(ControlledSelectable item, QudBaseMenuController.MenuEventPipeline pipeline = QudBaseMenuController.MenuEventPipeline.Pointer)
		{
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x000CF1C2 File Offset: 0x000CD3C2
		public virtual void Activate(ControlledSelectable item, QudBaseMenuController.MenuEventPipeline pipeline = QudBaseMenuController.MenuEventPipeline.Pointer)
		{
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000CF1C4 File Offset: 0x000CD3C4
		public virtual void SetSelectedInput(IControlledInputField item, QudBaseMenuController.MenuEventPipeline pipeline = QudBaseMenuController.MenuEventPipeline.Pointer)
		{
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000CF1C6 File Offset: 0x000CD3C6
		public virtual void ActivateInput(IControlledInputField item, QudBaseMenuController.MenuEventPipeline pipeline = QudBaseMenuController.MenuEventPipeline.Pointer)
		{
		}

		// Token: 0x040013DF RID: 5087
		public QudBaseMenuController.MenuEventPipeline lastEventSource = QudBaseMenuController.MenuEventPipeline.Pointer;

		// Token: 0x020017E0 RID: 6112
		public enum MenuEventPipeline
		{
			// Token: 0x04006028 RID: 24616
			KeyboardOrJoystick,
			// Token: 0x04006029 RID: 24617
			Pointer
		}
	}
}
