using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C7 RID: 711
	internal class RuntimePanel : BaseRuntimePanel, IRuntimePanel, IPanel, IDisposable
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00051B8B File Offset: 0x0004FD8B
		public PanelSettings panelSettings
		{
			get
			{
				return this.m_PanelSettings;
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00051B94 File Offset: 0x0004FD94
		public static RuntimePanel Create(ScriptableObject ownerObject)
		{
			return new RuntimePanel(ownerObject);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00051BAC File Offset: 0x0004FDAC
		private RuntimePanel(ScriptableObject ownerObject)
			: base(ownerObject, RuntimePanel.s_EventDispatcher)
		{
			this.focusController = new FocusController(new NavigateFocusRing(this.visualTree));
			this.m_PanelSettings = ownerObject as PanelSettings;
			base.name = ((this.m_PanelSettings != null) ? this.m_PanelSettings.name : "RuntimePanel");
			this.visualTree.RegisterCallback<FocusEvent, RuntimePanel>(delegate(FocusEvent e, RuntimePanel p)
			{
				p.OnElementFocus(e);
			}, this, TrickleDown.TrickleDown);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00051C40 File Offset: 0x0004FE40
		internal override void Update()
		{
			bool flag = this.m_PanelSettings != null;
			if (flag)
			{
				this.m_PanelSettings.ApplyPanelSettings();
			}
			base.Update();
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00051C71 File Offset: 0x0004FE71
		private void OnElementFocus(FocusEvent evt)
		{
			UIElementsRuntimeUtility.defaultEventSystem.OnFocusEvent(this, evt);
		}

		// Token: 0x04000B1E RID: 2846
		internal static readonly EventDispatcher s_EventDispatcher = RuntimeEventDispatcher.Create();

		// Token: 0x04000B1F RID: 2847
		private readonly PanelSettings m_PanelSettings;
	}
}
