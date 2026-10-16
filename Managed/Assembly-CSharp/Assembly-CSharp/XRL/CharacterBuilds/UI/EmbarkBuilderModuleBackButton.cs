using System;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.UI
{
	// Token: 0x020005C0 RID: 1472
	[RequireComponent(typeof(FrameworkContext))]
	[RequireComponent(typeof(FrameworkClickToAccept))]
	[RequireComponent(typeof(FrameworkHoverable))]
	public class EmbarkBuilderModuleBackButton : MonoBehaviour
	{
		// Token: 0x06003EBD RID: 16061 RVA: 0x00134110 File Offset: 0x00132310
		public void Awake()
		{
			if (this.navigationContext == null)
			{
				this.navigationContext = new NavigationContext();
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x00134128 File Offset: 0x00132328
		// (set) Token: 0x06003EBF RID: 16063 RVA: 0x00134154 File Offset: 0x00132354
		public NavigationContext navigationContext
		{
			get
			{
				NavigationContext navigationContext;
				if ((navigationContext = this._navigationContext) == null)
				{
					navigationContext = (this._navigationContext = base.GetComponent<FrameworkContext>().context);
				}
				return navigationContext;
			}
			set
			{
				base.GetComponent<FrameworkContext>().context = value;
				this._navigationContext = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x00134178 File Offset: 0x00132378
		public Color currentColor
		{
			get
			{
				EmbarkBuilderModuleBackButton.State? state = this.lastState;
				EmbarkBuilderModuleBackButton.State state2 = EmbarkBuilderModuleBackButton.State.Disabled;
				if ((state.GetValueOrDefault() == state2) & (state != null))
				{
					return this.disabledColor;
				}
				state = this.lastState;
				state2 = EmbarkBuilderModuleBackButton.State.Active;
				if ((state.GetValueOrDefault() == state2) & (state != null))
				{
					return this.activeColor;
				}
				return this.defaultColor;
			}
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x001341D3 File Offset: 0x001323D3
		public EmbarkBuilderModuleBackButton.State GetState()
		{
			if (this.navigationContext.disabled)
			{
				return EmbarkBuilderModuleBackButton.State.Disabled;
			}
			if (this.navigationContext.IsActive(true))
			{
				return EmbarkBuilderModuleBackButton.State.Active;
			}
			return EmbarkBuilderModuleBackButton.State.Default;
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x001341F5 File Offset: 0x001323F5
		public void ForceUpdate()
		{
			this.lastState = null;
			this.Update();
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x0013420C File Offset: 0x0013240C
		public void Update()
		{
			EmbarkBuilderModuleBackButton.State? state = this.lastState;
			EmbarkBuilderModuleBackButton.State state2 = this.GetState();
			if (!((state.GetValueOrDefault() == state2) & (state != null)))
			{
				this.lastState = new EmbarkBuilderModuleBackButton.State?(this.GetState());
				this.ButtonImage.color = this.currentColor;
				this.TextSkin.text = this.menuOption.getMenuText();
				this.TextSkin.color = this.currentColor;
				this.TextSkin.StripFormatting = this.GetState() == EmbarkBuilderModuleBackButton.State.Disabled;
				this.TextSkin.Apply();
			}
		}

		// Token: 0x04001EEB RID: 7915
		public Image ButtonImage;

		// Token: 0x04001EEC RID: 7916
		public UITextSkin TextSkin;

		// Token: 0x04001EED RID: 7917
		public MenuOption menuOption = EmbarkBuilderOverlayWindow.BackMenuOption;

		// Token: 0x04001EEE RID: 7918
		public Color disabledColor;

		// Token: 0x04001EEF RID: 7919
		public Color activeColor;

		// Token: 0x04001EF0 RID: 7920
		public Color defaultColor;

		// Token: 0x04001EF1 RID: 7921
		private NavigationContext _navigationContext;

		// Token: 0x04001EF2 RID: 7922
		private EmbarkBuilderModuleBackButton.State? lastState;

		// Token: 0x020019D3 RID: 6611
		public enum State
		{
			// Token: 0x04006743 RID: 26435
			Default,
			// Token: 0x04006744 RID: 26436
			Active,
			// Token: 0x04006745 RID: 26437
			Disabled
		}
	}
}
