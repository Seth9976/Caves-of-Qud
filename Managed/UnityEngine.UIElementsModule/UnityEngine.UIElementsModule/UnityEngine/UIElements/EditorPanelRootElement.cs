using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000313 RID: 787
	internal class EditorPanelRootElement : PanelRootElement
	{
		// Token: 0x060016A6 RID: 5798 RVA: 0x00057A60 File Offset: 0x00055C60
		public EditorPanelRootElement()
		{
			base.pickingMode = PickingMode.Position;
			base.RegisterCallback<ExecuteCommandEvent>(new EventCallback<ExecuteCommandEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<ValidateCommandEvent>(new EventCallback<ValidateCommandEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseEnterWindowEvent>(new EventCallback<MouseEnterWindowEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<MouseLeaveWindowEvent>(new EventCallback<MouseLeaveWindowEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
			base.RegisterCallback<IMGUIEvent>(new EventCallback<IMGUIEvent>(this.OnEventCompletedAtAnyTarget), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00057AE4 File Offset: 0x00055CE4
		private void OnEventCompletedAtAnyTarget(EventBase evt)
		{
			bool propagateToIMGUI = evt.propagateToIMGUI;
			if (propagateToIMGUI)
			{
				EventDispatchUtilities.PropagateToRemainingIMGUIContainers(evt, this);
				evt.propagateToIMGUI = false;
			}
		}
	}
}
