using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides navigation and a user interface (UI) to collect related data across multiple steps.</summary>
	// Token: 0x0200043A RID: 1082
	[ToolboxData("<{0}:Wizard runat=\"server\"> <WizardSteps> <asp:WizardStep title=\"Step 1\" runat=\"server\"></asp:WizardStep> <asp:WizardStep title=\"Step 2\" runat=\"server\"></asp:WizardStep> </WizardSteps> </{0}:Wizard>")]
	[Designer("System.Web.UI.Design.WebControls.WizardDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Bindable(false)]
	[DefaultEvent("FinishButtonClick")]
	public class Wizard : CompositeControl
	{
		/// <summary>Occurs when the user switches to a new step in the control.</summary>
		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x06003207 RID: 12807 RVA: 0x00083B24 File Offset: 0x00081D24
		// (remove) Token: 0x06003208 RID: 12808 RVA: 0x00083B37 File Offset: 0x00081D37
		public event EventHandler ActiveStepChanged
		{
			add
			{
				base.Events.AddHandler(Wizard.ActiveStepChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.ActiveStepChangedEvent, value);
			}
		}

		/// <summary>Occurs when the Cancel button is clicked.</summary>
		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06003209 RID: 12809 RVA: 0x00083B4A File Offset: 0x00081D4A
		// (remove) Token: 0x0600320A RID: 12810 RVA: 0x00083B5D File Offset: 0x00081D5D
		public event EventHandler CancelButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.CancelButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.CancelButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the Finish button is clicked.</summary>
		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x0600320B RID: 12811 RVA: 0x00083B70 File Offset: 0x00081D70
		// (remove) Token: 0x0600320C RID: 12812 RVA: 0x00083B83 File Offset: 0x00081D83
		public event WizardNavigationEventHandler FinishButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.FinishButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.FinishButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the Next button is clicked.</summary>
		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x0600320D RID: 12813 RVA: 0x00083B96 File Offset: 0x00081D96
		// (remove) Token: 0x0600320E RID: 12814 RVA: 0x00083BA9 File Offset: 0x00081DA9
		public event WizardNavigationEventHandler NextButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.NextButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.NextButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when the Previous button is clicked.</summary>
		// Token: 0x140000FA RID: 250
		// (add) Token: 0x0600320F RID: 12815 RVA: 0x00083BBC File Offset: 0x00081DBC
		// (remove) Token: 0x06003210 RID: 12816 RVA: 0x00083BCF File Offset: 0x00081DCF
		public event WizardNavigationEventHandler PreviousButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.PreviousButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.PreviousButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when a button in the sidebar area is clicked.</summary>
		// Token: 0x140000FB RID: 251
		// (add) Token: 0x06003211 RID: 12817 RVA: 0x00083BE2 File Offset: 0x00081DE2
		// (remove) Token: 0x06003212 RID: 12818 RVA: 0x00083BF5 File Offset: 0x00081DF5
		public event WizardNavigationEventHandler SideBarButtonClick
		{
			add
			{
				base.Events.AddHandler(Wizard.SideBarButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Wizard.SideBarButtonClickEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.ActiveStepChanged" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003213 RID: 12819 RVA: 0x00083C08 File Offset: 0x00081E08
		protected virtual void OnActiveStepChanged(object source, EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Wizard.ActiveStepChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(source, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.CancelButtonClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> containing the event data.</param>
		// Token: 0x06003214 RID: 12820 RVA: 0x00083C40 File Offset: 0x00081E40
		protected virtual void OnCancelButtonClick(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[Wizard.CancelButtonClickEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.FinishButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing the event data.</param>
		// Token: 0x06003215 RID: 12821 RVA: 0x00083C78 File Offset: 0x00081E78
		protected virtual void OnFinishButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.FinishButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.NextButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing the event data.</param>
		// Token: 0x06003216 RID: 12822 RVA: 0x00083CB0 File Offset: 0x00081EB0
		protected virtual void OnNextButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.NextButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.PreviousButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing event data.</param>
		// Token: 0x06003217 RID: 12823 RVA: 0x00083CE8 File Offset: 0x00081EE8
		protected virtual void OnPreviousButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.PreviousButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Wizard.SideBarButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WizardNavigationEventArgs" /> containing event data.</param>
		// Token: 0x06003218 RID: 12824 RVA: 0x00083D20 File Offset: 0x00081F20
		protected virtual void OnSideBarButtonClick(WizardNavigationEventArgs e)
		{
			if (base.Events != null)
			{
				WizardNavigationEventHandler wizardNavigationEventHandler = (WizardNavigationEventHandler)base.Events[Wizard.SideBarButtonClickEvent];
				if (wizardNavigationEventHandler != null)
				{
					wizardNavigationEventHandler(this, e);
				}
			}
		}

		/// <summary>Gets the step in the <see cref="P:System.Web.UI.WebControls.Wizard.WizardSteps" /> collection that is currently displayed to the user.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that is currently displayed to the user.</returns>
		/// <exception cref="T:System.InvalidOperationException">The corresponding <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStepIndex" /> is less than -1 or greater than the number of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects in the <see cref="T:System.Web.UI.WebControls.Wizard" />.</exception>
		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x00083D58 File Offset: 0x00081F58
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WizardStepBase ActiveStep
		{
			get
			{
				int num = this.ActiveStepIndex;
				if (num < -1 || num >= this.WizardSteps.Count)
				{
					throw new InvalidOperationException("ActiveStepIndex has an invalid value.");
				}
				if (num == -1)
				{
					return null;
				}
				return this.WizardSteps[num];
			}
		}

		/// <summary>Gets or sets the index of the current <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that is currently displayed in the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is higher than the number of wizard steps defined in the <see cref="P:System.Web.UI.WebControls.Wizard.WizardSteps" /> collection.</exception>
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x0600321A RID: 12826 RVA: 0x00083D9B File Offset: 0x00081F9B
		// (set) Token: 0x0600321B RID: 12827 RVA: 0x00083DA4 File Offset: 0x00081FA4
		[Themeable(false)]
		[DefaultValue(-1)]
		public virtual int ActiveStepIndex
		{
			get
			{
				return this.activeStepIndex;
			}
			set
			{
				if (value < -1 || (value > this.WizardSteps.Count && (this.inited || this.WizardSteps.Count > 0)))
				{
					throw new ArgumentOutOfRangeException("The ActiveStepIndex must be less than WizardSteps.Count and at least -1");
				}
				if (this.inited && !this.AllowNavigationToStep(value))
				{
					return;
				}
				if (this.activeStepIndex != value)
				{
					this.activeStepIndex = value;
					if (this.inited)
					{
						this.multiView.ActiveViewIndex = value;
						if (this.stepDatalist != null)
						{
							this.stepDatalist.SelectedIndex = value;
							this.stepDatalist.DataBind();
						}
						this.OnActiveStepChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the URL of the image displayed for the Cancel button.</summary>
		/// <returns>The URL of the image displayed for the Cancel button on the <see cref="T:System.Web.UI.WebControls.Wizard" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x0600321C RID: 12828 RVA: 0x00083E48 File Offset: 0x00082048
		// (set) Token: 0x0600321D RID: 12829 RVA: 0x000497B1 File Offset: 0x000479B1
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string CancelButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["CancelButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["CancelButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a collection of style properties that define the appearance of the Cancel button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x0600321E RID: 12830 RVA: 0x00083E75 File Offset: 0x00082075
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public Style CancelButtonStyle
		{
			get
			{
				if (this.cancelButtonStyle == null)
				{
					this.cancelButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.cancelButtonStyle).TrackViewState();
					}
				}
				return this.cancelButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Cancel button.</summary>
		/// <returns>The text caption displayed for Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Cancel". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x0600321F RID: 12831 RVA: 0x00083EA4 File Offset: 0x000820A4
		// (set) Token: 0x06003220 RID: 12832 RVA: 0x00049809 File Offset: 0x00047A09
		[Localizable(true)]
		public virtual string CancelButtonText
		{
			get
			{
				object obj = this.ViewState["CancelButtonText"];
				if (obj == null)
				{
					return "Cancel";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["CancelButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Cancel button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06003221 RID: 12833 RVA: 0x00083ED4 File Offset: 0x000820D4
		// (set) Token: 0x06003222 RID: 12834 RVA: 0x00049847 File Offset: 0x00047A47
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType CancelButtonType
		{
			get
			{
				object obj = this.ViewState["CancelButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["CancelButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL that the user is directed to when they click the Cancel button.</summary>
		/// <returns>The URL that the user is redirected to when they click Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06003223 RID: 12835 RVA: 0x00083F00 File Offset: 0x00082100
		// (set) Token: 0x06003224 RID: 12836 RVA: 0x00049876 File Offset: 0x00047A76
		[Themeable(false)]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string CancelDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["CancelDestinationPageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["CancelDestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of the cell and the cell border.</summary>
		/// <returns>The amount of space, in pixels, between the contents of a cell and the cell border. The default is 0.</returns>
		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06003225 RID: 12837 RVA: 0x00083F2D File Offset: 0x0008212D
		// (set) Token: 0x06003226 RID: 12838 RVA: 0x00057BF7 File Offset: 0x00055DF7
		[DefaultValue(0)]
		public virtual int CellPadding
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellPadding;
				}
				return 0;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space, in pixels, between cells. The default is 0.</returns>
		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x00057C0A File Offset: 0x00055E0A
		// (set) Token: 0x06003228 RID: 12840 RVA: 0x00057C26 File Offset: 0x00055E26
		[DefaultValue(0)]
		public virtual int CellSpacing
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellSpacing;
				}
				return 0;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether to display a Cancel button.</summary>
		/// <returns>true to display Cancel on the <see cref="T:System.Web.UI.WebControls.Wizard" />; otherwise, false. The default is false.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x00083F4C File Offset: 0x0008214C
		// (set) Token: 0x0600322A RID: 12842 RVA: 0x00083F75 File Offset: 0x00082175
		[DefaultValue(false)]
		[Themeable(false)]
		public virtual bool DisplayCancelButton
		{
			get
			{
				object obj = this.ViewState["DisplayCancelButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["DisplayCancelButton"] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value indicating whether to display the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>true to display the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />; otherwise, false. The default is true.This property cannot be set by themes or style sheet themes. For more information, see <see cref="T:System.Web.UI.ThemeableAttribute" /> and ASP.NET Themes and Skins.</returns>
		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x0600322B RID: 12843 RVA: 0x00083F90 File Offset: 0x00082190
		// (set) Token: 0x0600322C RID: 12844 RVA: 0x00083FB9 File Offset: 0x000821B9
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool DisplaySideBar
		{
			get
			{
				object obj = this.ViewState["DisplaySideBar"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["DisplaySideBar"] = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Finish button.</summary>
		/// <returns>The URL of the image displayed for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x0600322D RID: 12845 RVA: 0x00083FD8 File Offset: 0x000821D8
		// (set) Token: 0x0600322E RID: 12846 RVA: 0x00084005 File Offset: 0x00082205
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string FinishCompleteButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishCompleteButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Finish button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x00084018 File Offset: 0x00082218
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style FinishCompleteButtonStyle
		{
			get
			{
				if (this.finishCompleteButtonStyle == null)
				{
					this.finishCompleteButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.finishCompleteButtonStyle).TrackViewState();
					}
				}
				return this.finishCompleteButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Finish button.</summary>
		/// <returns>The text caption displayed for Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Finish". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06003230 RID: 12848 RVA: 0x00084048 File Offset: 0x00082248
		// (set) Token: 0x06003231 RID: 12849 RVA: 0x00084075 File Offset: 0x00082275
		[Localizable(true)]
		public virtual string FinishCompleteButtonText
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonText"];
				if (obj == null)
				{
					return "Finish";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishCompleteButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Finish button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x00084088 File Offset: 0x00082288
		// (set) Token: 0x06003233 RID: 12851 RVA: 0x000840B1 File Offset: 0x000822B1
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType FinishCompleteButtonType
		{
			get
			{
				object obj = this.ViewState["FinishCompleteButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["FinishCompleteButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL that the user is redirected to when they click the Finish button.</summary>
		/// <returns>The URL that the user is redirected to when they click Finish on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06003234 RID: 12852 RVA: 0x000840CC File Offset: 0x000822CC
		// (set) Token: 0x06003235 RID: 12853 RVA: 0x000840F9 File Offset: 0x000822F9
		[UrlProperty]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[Themeable(false)]
		public virtual string FinishDestinationPageUrl
		{
			get
			{
				object obj = this.ViewState["FinishDestinationPageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishDestinationPageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the template that is used to display the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> that defines the content for the navigation area for the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06003236 RID: 12854 RVA: 0x0008410C File Offset: 0x0008230C
		// (set) Token: 0x06003237 RID: 12855 RVA: 0x00084114 File Offset: 0x00082314
		[DefaultValue(null)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate FinishNavigationTemplate
		{
			get
			{
				return this.finishNavigationTemplate;
			}
			set
			{
				this.finishNavigationTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>The URL of the image displayed for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x06003238 RID: 12856 RVA: 0x00084124 File Offset: 0x00082324
		// (set) Token: 0x06003239 RID: 12857 RVA: 0x00084151 File Offset: 0x00082351
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string FinishPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishPreviousButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FF6 RID: 4086
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x00084164 File Offset: 0x00082364
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style FinishPreviousButtonStyle
		{
			get
			{
				if (this.finishPreviousButtonStyle == null)
				{
					this.finishPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.finishPreviousButtonStyle).TrackViewState();
					}
				}
				return this.finishPreviousButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>The text caption displayed for Previous on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Previous". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x00084194 File Offset: 0x00082394
		// (set) Token: 0x0600323C RID: 12860 RVA: 0x000841C1 File Offset: 0x000823C1
		[Localizable(true)]
		public virtual string FinishPreviousButtonText
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonText"];
				if (obj == null)
				{
					return "Previous";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["FinishPreviousButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000841D4 File Offset: 0x000823D4
		// (set) Token: 0x0600323E RID: 12862 RVA: 0x000841FD File Offset: 0x000823FD
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType FinishPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["FinishPreviousButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["FinishPreviousButtonType"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the header area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FF9 RID: 4089
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x00084215 File Offset: 0x00082415
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.headerStyle).TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the template that is used to display the header area on the control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17000FFA RID: 4090
		// (get) Token: 0x06003240 RID: 12864 RVA: 0x00084243 File Offset: 0x00082443
		// (set) Token: 0x06003241 RID: 12865 RVA: 0x0008424B File Offset: 0x0008244B
		[DefaultValue(null)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the header area on the control.</summary>
		/// <returns>The text caption displayed for the header area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17000FFB RID: 4091
		// (get) Token: 0x06003242 RID: 12866 RVA: 0x0008425C File Offset: 0x0008245C
		// (set) Token: 0x06003243 RID: 12867 RVA: 0x00082A0D File Offset: 0x00080C0D
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		/// <summary>Gets or sets the custom content of the root container in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An object that contains the custom content for the root container in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000FFC RID: 4092
		// (get) Token: 0x06003244 RID: 12868 RVA: 0x00084289 File Offset: 0x00082489
		// (set) Token: 0x06003245 RID: 12869 RVA: 0x00084291 File Offset: 0x00082491
		[DefaultValue(null)]
		[TemplateContainer(typeof(Wizard))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate LayoutTemplate { get; set; }

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the buttons in the navigation area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the buttons in the navigation area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FFD RID: 4093
		// (get) Token: 0x06003246 RID: 12870 RVA: 0x0008429A File Offset: 0x0008249A
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style NavigationButtonStyle
		{
			get
			{
				if (this.navigationButtonStyle == null)
				{
					this.navigationButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.navigationButtonStyle).TrackViewState();
					}
				}
				return this.navigationButtonStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the navigation area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the navigation area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FFE RID: 4094
		// (get) Token: 0x06003247 RID: 12871 RVA: 0x000842C8 File Offset: 0x000824C8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle NavigationStyle
		{
			get
			{
				if (this.navigationStyle == null)
				{
					this.navigationStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.navigationStyle).TrackViewState();
					}
				}
				return this.navigationStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the sidebar area on the control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17000FFF RID: 4095
		// (get) Token: 0x06003248 RID: 12872 RVA: 0x000842F6 File Offset: 0x000824F6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public TableItemStyle SideBarStyle
		{
			get
			{
				if (this.sideBarStyle == null)
				{
					this.sideBarStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sideBarStyle).TrackViewState();
					}
				}
				return this.sideBarStyle;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the buttons on the sidebar.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the buttons on the sidebar of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06003249 RID: 12873 RVA: 0x00084324 File Offset: 0x00082524
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		public Style SideBarButtonStyle
		{
			get
			{
				if (this.sideBarButtonStyle == null)
				{
					this.sideBarButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sideBarButtonStyle).TrackViewState();
					}
				}
				return this.sideBarButtonStyle;
			}
		}

		/// <summary>Gets or sets the template that is used to display the sidebar area on the control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the sidebar area on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x0600324A RID: 12874 RVA: 0x00084352 File Offset: 0x00082552
		// (set) Token: 0x0600324B RID: 12875 RVA: 0x0008435A File Offset: 0x0008255A
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		public virtual ITemplate SideBarTemplate
		{
			get
			{
				return this.sideBarTemplate;
			}
			set
			{
				this.sideBarTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets a value that is used to render alternate text that notifies screen readers to skip the content in the sidebar area.</summary>
		/// <returns>A string that the <see cref="T:System.Web.UI.WebControls.Wizard" /> renders as alternate text with an invisible image, as a hint to screen readers. The default is "Skip Navigation Links". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17001002 RID: 4098
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x0008436C File Offset: 0x0008256C
		// (set) Token: 0x0600324D RID: 12877 RVA: 0x00069335 File Offset: 0x00067535
		[Localizable(true)]
		public virtual string SkipLinkText
		{
			get
			{
				object obj = this.ViewState["SkipLinkText"];
				if (obj == null)
				{
					return "Skip Navigation Links.";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["SkipLinkText"] = value;
			}
		}

		/// <summary>Gets or sets the template that is used to display the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the navigation area on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> for the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is null.</returns>
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x0600324E RID: 12878 RVA: 0x00084399 File Offset: 0x00082599
		// (set) Token: 0x0600324F RID: 12879 RVA: 0x000843A1 File Offset: 0x000825A1
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		[DefaultValue(null)]
		public virtual ITemplate StartNavigationTemplate
		{
			get
			{
				return this.startNavigationTemplate;
			}
			set
			{
				this.startNavigationTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>The URL of the image displayed for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is an empty string ("").</returns>
		// Token: 0x17001004 RID: 4100
		// (get) Token: 0x06003250 RID: 12880 RVA: 0x000843B0 File Offset: 0x000825B0
		// (set) Token: 0x06003251 RID: 12881 RVA: 0x000843DD File Offset: 0x000825DD
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string StartNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StartNextButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StartNextButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001005 RID: 4101
		// (get) Token: 0x06003252 RID: 12882 RVA: 0x000843F0 File Offset: 0x000825F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Style StartNextButtonStyle
		{
			get
			{
				if (this.startNextButtonStyle == null)
				{
					this.startNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.startNextButtonStyle).TrackViewState();
					}
				}
				return this.startNextButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>The text caption displayed for Next on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> of the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Next". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06003253 RID: 12883 RVA: 0x00084420 File Offset: 0x00082620
		// (set) Token: 0x06003254 RID: 12884 RVA: 0x0008444D File Offset: 0x0008264D
		[Localizable(true)]
		public virtual string StartNextButtonText
		{
			get
			{
				object obj = this.ViewState["StartNextButtonText"];
				if (obj == null)
				{
					return "Next";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StartNextButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06003255 RID: 12885 RVA: 0x00084460 File Offset: 0x00082660
		// (set) Token: 0x06003256 RID: 12886 RVA: 0x00084489 File Offset: 0x00082689
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType StartNextButtonType
		{
			get
			{
				object obj = this.ViewState["StartNextButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["StartNextButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the template that is used to display the navigation area on any <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects other than the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" />, the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" />, or <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" /> step.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the template for displaying the navigation area on any <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived objects of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control other than the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" />, <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" />, or <see cref="F:System.Web.UI.WebControls.WizardStepType.Complete" />. The default is null.</returns>
		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06003257 RID: 12887 RVA: 0x000844A1 File Offset: 0x000826A1
		// (set) Token: 0x06003258 RID: 12888 RVA: 0x000844A9 File Offset: 0x000826A9
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(Wizard), BindingDirection.OneWay)]
		public virtual ITemplate StepNavigationTemplate
		{
			get
			{
				return this.stepNavigationTemplate;
			}
			set
			{
				this.stepNavigationTemplate = value;
				this.UpdateViews();
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Next button.</summary>
		/// <returns>The URL of the image displayed for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001009 RID: 4105
		// (get) Token: 0x06003259 RID: 12889 RVA: 0x000844B8 File Offset: 0x000826B8
		// (set) Token: 0x0600325A RID: 12890 RVA: 0x000844E5 File Offset: 0x000826E5
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[DefaultValue("")]
		public virtual string StepNextButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepNextButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepNextButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Next button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000844F8 File Offset: 0x000826F8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public Style StepNextButtonStyle
		{
			get
			{
				if (this.stepNextButtonStyle == null)
				{
					this.stepNextButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.stepNextButtonStyle).TrackViewState();
					}
				}
				return this.stepNextButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Next button.</summary>
		/// <returns>The text caption displayed for Next on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Next". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x00084528 File Offset: 0x00082728
		// (set) Token: 0x0600325D RID: 12893 RVA: 0x00084555 File Offset: 0x00082755
		[Localizable(true)]
		public virtual string StepNextButtonText
		{
			get
			{
				object obj = this.ViewState["StepNextButtonText"];
				if (obj == null)
				{
					return "Next";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepNextButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Next button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x00084568 File Offset: 0x00082768
		// (set) Token: 0x0600325F RID: 12895 RVA: 0x00084591 File Offset: 0x00082791
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType StepNextButtonType
		{
			get
			{
				object obj = this.ViewState["StepNextButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["StepNextButtonType"] = value;
			}
		}

		/// <summary>Gets or sets the URL of the image that is displayed for the Previous button.</summary>
		/// <returns>The URL of the image displayed for Previous on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06003260 RID: 12896 RVA: 0x000845AC File Offset: 0x000827AC
		// (set) Token: 0x06003261 RID: 12897 RVA: 0x000845D9 File Offset: 0x000827D9
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string StepPreviousButtonImageUrl
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepPreviousButtonImageUrl"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the Previous button.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for Previous on a <see cref="F:System.Web.UI.WebControls.WizardStepType.Step" /> for the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000845EC File Offset: 0x000827EC
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Style StepPreviousButtonStyle
		{
			get
			{
				if (this.stepPreviousButtonStyle == null)
				{
					this.stepPreviousButtonStyle = new Style();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.stepPreviousButtonStyle).TrackViewState();
					}
				}
				return this.stepPreviousButtonStyle;
			}
		}

		/// <summary>Gets or sets the text caption that is displayed for the Previous button.</summary>
		/// <returns>The text caption displayed for Previous on the <see cref="T:System.Web.UI.WebControls.Wizard" />. The default is "Previous". The default text for the control is localized based on the current locale for the server.</returns>
		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06003263 RID: 12899 RVA: 0x0008461C File Offset: 0x0008281C
		// (set) Token: 0x06003264 RID: 12900 RVA: 0x00084649 File Offset: 0x00082849
		[Localizable(true)]
		public virtual string StepPreviousButtonText
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonText"];
				if (obj == null)
				{
					return "Previous";
				}
				return (string)obj;
			}
			set
			{
				this.ViewState["StepPreviousButtonText"] = value;
			}
		}

		/// <summary>Gets or sets the type of button that is rendered as the Previous button.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see cref="F:System.Web.UI.WebControls.ButtonType.Button" />.</returns>
		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x0008465C File Offset: 0x0008285C
		// (set) Token: 0x06003266 RID: 12902 RVA: 0x00084685 File Offset: 0x00082885
		[DefaultValue(ButtonType.Button)]
		public virtual ButtonType StepPreviousButtonType
		{
			get
			{
				object obj = this.ViewState["StepPreviousButtonType"];
				if (obj == null)
				{
					return ButtonType.Button;
				}
				return (ButtonType)obj;
			}
			set
			{
				this.ViewState["StepPreviousButtonType"] = value;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.Style" /> object that defines the settings for the <see cref="T:System.Web.UI.WebControls.WizardStep" /> objects.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.Style" /> that defines the style settings for the <see cref="T:System.Web.UI.WebControls.WizardStep" /> objects on the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x0008469D File Offset: 0x0008289D
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle StepStyle
		{
			get
			{
				if (this.stepStyle == null)
				{
					this.stepStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.stepStyle).TrackViewState();
					}
				}
				return this.stepStyle;
			}
		}

		/// <summary>Gets a collection containing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that are defined for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> representing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the <see cref="T:System.Web.UI.WebControls.Wizard" />.</returns>
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000846CB File Offset: 0x000828CB
		[Editor("System.Web.UI.Design.WebControls.WizardStepCollectionEditor,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Themeable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual WizardStepCollection WizardSteps
		{
			get
			{
				if (this.steps == null)
				{
					this.steps = new WizardStepCollection(this);
				}
				return this.steps;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</returns>
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x0004A624 File Offset: 0x00048824
		protected new virtual HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x000846E7 File Offset: 0x000828E7
		internal virtual ITemplate SideBarItemTemplate
		{
			get
			{
				return new Wizard.SideBarButtonTemplate(this);
			}
		}

		/// <summary>Returns a collection of <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that have been accessed.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that have been accessed.</returns>
		// Token: 0x0600326B RID: 12907 RVA: 0x000846EF File Offset: 0x000828EF
		public ICollection GetHistory()
		{
			if (this.history == null)
			{
				this.history = new ArrayList();
			}
			return this.history;
		}

		/// <summary>Sets the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object as the value for the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" /> property of the <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object to set as the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStepIndex" /> of the associated <see cref="T:System.Web.UI.WebControls.WizardStepBase" />-derived object passed in is equal to -1.</exception>
		// Token: 0x0600326C RID: 12908 RVA: 0x0008470C File Offset: 0x0008290C
		public void MoveTo(WizardStepBase wizardStep)
		{
			if (wizardStep == null)
			{
				throw new ArgumentNullException("wizardStep");
			}
			int num = this.WizardSteps.IndexOf(wizardStep);
			if (num == -1)
			{
				throw new ArgumentException("The provided wizard step does not belong to this wizard.");
			}
			this.ActiveStepIndex = num;
		}

		/// <summary>Returns the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> value for the specified <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WizardStepType" /> values.</returns>
		/// <param name="wizardStep">The <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> for which the associated <see cref="T:System.Web.UI.WebControls.WizardStepType" />  is returned.</param>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> for which the associated <see cref="T:System.Web.UI.WebControls.WizardStepType" />  is returned.</param>
		// Token: 0x0600326D RID: 12909 RVA: 0x0008474C File Offset: 0x0008294C
		public WizardStepType GetStepType(WizardStepBase wizardStep, int index)
		{
			if (wizardStep.StepType != WizardStepType.Auto)
			{
				return wizardStep.StepType;
			}
			if (index == this.WizardSteps.Count - 1 || (this.WizardSteps.Count > 1 && this.WizardSteps[this.WizardSteps.Count - 1].StepType == WizardStepType.Complete && index == this.WizardSteps.Count - 2))
			{
				return WizardStepType.Finish;
			}
			if (index == 0)
			{
				return WizardStepType.Start;
			}
			return WizardStepType.Step;
		}

		/// <summary>Uses a Boolean value to determine whether the <see cref="P:System.Web.UI.WebControls.Wizard.ActiveStep" /> property can be set to the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object that corresponds to the index that is passed in.</summary>
		/// <returns>false if the index passed in refers to a <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> that has already been accessed and its <see cref="P:System.Web.UI.WebControls.WizardStepBase.AllowReturn" /> property is set to false; otherwise, true.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> object being checked.</param>
		// Token: 0x0600326E RID: 12910 RVA: 0x000847C0 File Offset: 0x000829C0
		protected virtual bool AllowNavigationToStep(int index)
		{
			return index < 0 || index >= this.WizardSteps.Count || this.history == null || !this.history.Contains(index) || this.WizardSteps[index].AllowReturn;
		}

		/// <summary>Raises the Init event.</summary>
		/// <param name="e">The raised event.</param>
		// Token: 0x0600326F RID: 12911 RVA: 0x0008480D File Offset: 0x00082A0D
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			base.OnInit(e);
			if (this.ActiveStepIndex == -1)
			{
				this.ActiveStepIndex = 0;
			}
			this.EnsureChildControls();
			this.inited = true;
		}

		/// <summary>Creates control collection.</summary>
		// Token: 0x06003270 RID: 12912 RVA: 0x0008483F File Offset: 0x00082A3F
		protected override ControlCollection CreateControlCollection()
		{
			ControlCollection controlCollection = new ControlCollection(this);
			controlCollection.SetReadonly(true);
			return controlCollection;
		}

		/// <summary>Creates child controls.</summary>
		// Token: 0x06003271 RID: 12913 RVA: 0x0008484E File Offset: 0x00082A4E
		protected internal override void CreateChildControls()
		{
			this.CreateControlHierarchy();
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x00084856 File Offset: 0x00082A56
		private InvalidOperationException MakeLayoutException(string phName, string phID, string condition = null)
		{
			return new InvalidOperationException(string.Format("A {0} placeholder must be specified on Wizard '{1}'{2}. Specify a placeholder by setting a control's ID property to \"{3}\". The placeholder control must also specify runat=\"server\"", new object[] { phName, this.ID, condition, phID }));
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x00084884 File Offset: 0x00082A84
		private void CreateControlHierarchy_LayoutTemplate(ITemplate layoutTemplate)
		{
			WizardLayoutContainer wizardLayoutContainer = new WizardLayoutContainer();
			ControlCollection controls = this.Controls;
			controls.SetReadonly(false);
			controls.Add(wizardLayoutContainer);
			controls.SetReadonly(true);
			layoutTemplate.InstantiateIn(wizardLayoutContainer);
			WizardStepCollection wizardSteps = this.WizardSteps;
			bool flag = wizardSteps != null && wizardSteps.Count > 0;
			Control control;
			Control control2;
			if (this.DisplaySideBar)
			{
				control = wizardLayoutContainer.FindControl(Wizard.SideBarPlaceholderId);
				if (control == null)
				{
					throw this.MakeLayoutException("sidebar", Wizard.SideBarPlaceholderId, " when DisplaySideBar is set to true");
				}
				control2 = new Control();
				this.CreateSideBar(control2);
				this.ReplacePlaceHolder(wizardLayoutContainer, control, control2);
			}
			ITemplate template = this.HeaderTemplate;
			if (template != null)
			{
				control = wizardLayoutContainer.FindControl(Wizard.HeaderPlaceholderId);
				if (control == null)
				{
					throw this.MakeLayoutException("header", Wizard.HeaderPlaceholderId, " when HeaderTemplate is set");
				}
				control2 = new Control();
				template.InstantiateIn(control2);
				this.ReplacePlaceHolder(wizardLayoutContainer, control, control2);
			}
			control = wizardLayoutContainer.FindControl(Wizard.WizardStepPlaceholderId);
			if (control == null)
			{
				throw this.MakeLayoutException("step", Wizard.WizardStepPlaceholderId, null);
			}
			this.customNavigation = null;
			this.multiView = new MultiView();
			foreach (object obj in wizardSteps)
			{
				View view = (View)obj;
				if (view is TemplatedWizardStep)
				{
					this.InstantiateTemplateStep((TemplatedWizardStep)view);
				}
				this.multiView.Views.Add(view);
			}
			this.multiView.ActiveViewIndex = this.ActiveStepIndex;
			this.ReplacePlaceHolder(wizardLayoutContainer, control, this.multiView);
			control = wizardLayoutContainer.FindControl(Wizard.NavigationPlaceholderId);
			if (control == null)
			{
				throw this.MakeLayoutException("navigation", Wizard.NavigationPlaceholderId, null);
			}
			Table table = new Table();
			table.CellSpacing = 5;
			table.CellPadding = 5;
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			control2 = new Control();
			this.CreateButtonBar(control2);
			tableRow.Cells.Add(tableCell);
			table.Rows.Add(tableRow);
			this.ReplacePlaceHolder(wizardLayoutContainer, control, control2);
			wizardLayoutContainer.Visible = flag;
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x00084AAC File Offset: 0x00082CAC
		private void ReplacePlaceHolder(WebControl container, Control placeHolder, Control replacement)
		{
			ControlCollection controls = container.Controls;
			int num = controls.IndexOf(placeHolder);
			controls.Remove(placeHolder);
			controls.AddAt(num, replacement);
		}

		/// <summary>Creates the hierarchy of child controls that make up the control.</summary>
		/// <exception cref="T:System.InvalidOperationException">The sidebar template does not contain a <see cref="T:System.Web.UI.WebControls.DataList" /> control.</exception>
		// Token: 0x06003275 RID: 12917 RVA: 0x00084AD8 File Offset: 0x00082CD8
		protected virtual void CreateControlHierarchy()
		{
			ITemplate layoutTemplate = this.LayoutTemplate;
			if (layoutTemplate != null)
			{
				this.CreateControlHierarchy_LayoutTemplate(layoutTemplate);
				return;
			}
			this.styles.Clear();
			this.wizardTable = new ContainedTable(this);
			Table table = this.wizardTable;
			if (this.DisplaySideBar)
			{
				table = new Table();
				table.CellPadding = 0;
				table.CellSpacing = 0;
				table.Height = new Unit("100%");
				table.Width = new Unit("100%");
				TableRow tableRow = new TableRow();
				Wizard.TableCellNamingContainer tableCellNamingContainer = new Wizard.TableCellNamingContainer(this.SkipLinkText, this.ClientID);
				tableCellNamingContainer.ID = "SideBarContainer";
				tableCellNamingContainer.ControlStyle.Height = Unit.Percentage(100.0);
				this.CreateSideBar(tableCellNamingContainer);
				tableRow.Cells.Add(tableCellNamingContainer);
				TableCell tableCell = new TableCell();
				tableCell.Controls.Add(table);
				tableCell.Height = new Unit("100%");
				tableRow.Cells.Add(tableCell);
				this.wizardTable.Rows.Add(tableRow);
			}
			this.AddHeaderRow(table);
			TableRow tableRow2 = new TableRow();
			TableCell tableCell2 = new TableCell();
			this.customNavigation = null;
			this.multiView = new MultiView();
			foreach (object obj in this.WizardSteps)
			{
				View view = (View)obj;
				if (view is TemplatedWizardStep)
				{
					this.InstantiateTemplateStep((TemplatedWizardStep)view);
				}
				this.multiView.Views.Add(view);
			}
			this.multiView.ActiveViewIndex = this.ActiveStepIndex;
			this.RegisterApplyStyle(tableCell2, this.StepStyle);
			tableCell2.Controls.Add(this.multiView);
			tableRow2.Cells.Add(tableCell2);
			tableRow2.Height = new Unit("100%");
			table.Rows.Add(tableRow2);
			TableRow tableRow3 = new TableRow();
			this._navigationCell = new TableCell();
			this._navigationCell.HorizontalAlign = HorizontalAlign.Right;
			this.RegisterApplyStyle(this._navigationCell, this.NavigationStyle);
			this.CreateButtonBar(this._navigationCell);
			tableRow3.Cells.Add(this._navigationCell);
			table.Rows.Add(tableRow3);
			this.Controls.SetReadonly(false);
			this.Controls.Add(this.wizardTable);
			this.Controls.SetReadonly(true);
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x00084D6C File Offset: 0x00082F6C
		internal virtual void InstantiateTemplateStep(TemplatedWizardStep step)
		{
			Wizard.BaseWizardContainer baseWizardContainer = new Wizard.BaseWizardContainer();
			if (step.ContentTemplate != null)
			{
				step.ContentTemplate.InstantiateIn(baseWizardContainer.InnerCell);
			}
			step.ContentTemplateContainer = baseWizardContainer;
			step.Controls.Clear();
			step.Controls.Add(baseWizardContainer);
			Wizard.BaseWizardNavigationContainer baseWizardNavigationContainer = new Wizard.BaseWizardNavigationContainer();
			if (step.CustomNavigationTemplate != null)
			{
				step.CustomNavigationTemplate.InstantiateIn(baseWizardNavigationContainer);
				this.RegisterCustomNavigation(step, baseWizardNavigationContainer);
			}
			step.CustomNavigationTemplateContainer = baseWizardNavigationContainer;
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x00084DDF File Offset: 0x00082FDF
		internal void RegisterCustomNavigation(TemplatedWizardStep step, Wizard.BaseWizardNavigationContainer customNavigationTemplateContainer)
		{
			if (this.customNavigation == null)
			{
				this.customNavigation = new Hashtable();
			}
			this.customNavigation[step] = customNavigationTemplateContainer;
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x00084E04 File Offset: 0x00083004
		private void CreateButtonBar(Control container)
		{
			if (this.customNavigation != null && this.customNavigation.Values.Count > 0)
			{
				int num = 0;
				foreach (object obj in this.customNavigation.Values)
				{
					Control control = (Control)obj;
					control.ID = "CustomNavigationTemplateContainerID" + num++.ToString();
					container.Controls.Add(control);
				}
			}
			this._startNavContainer = new Wizard.StartNavigationContainer(this);
			this._startNavContainer.ID = "StartNavigationTemplateContainerID";
			if (this.startNavigationTemplate != null)
			{
				this.startNavigationTemplate.InstantiateIn(this._startNavContainer);
			}
			else
			{
				TableRow tableRow;
				Wizard.AddNavButtonsTable(this._startNavContainer, out tableRow);
				this.AddButtonCell(tableRow, this.CreateButtonSet(Wizard.StartNextButtonIDShort, Wizard.MoveNextCommandName));
				this.AddButtonCell(tableRow, this.CreateButtonSet(Wizard.CancelButtonIDShort, Wizard.CancelCommandName, false));
				this._startNavContainer.ConfirmDefaultTemplate();
			}
			container.Controls.Add(this._startNavContainer);
			this._stepNavContainer = new Wizard.StepNavigationContainer(this);
			this._stepNavContainer.ID = "StepNavigationTemplateContainerID";
			if (this.stepNavigationTemplate != null)
			{
				this.stepNavigationTemplate.InstantiateIn(this._stepNavContainer);
			}
			else
			{
				TableRow tableRow2;
				Wizard.AddNavButtonsTable(this._stepNavContainer, out tableRow2);
				this.AddButtonCell(tableRow2, this.CreateButtonSet(Wizard.StepPreviousButtonIDShort, Wizard.MovePreviousCommandName, false));
				this.AddButtonCell(tableRow2, this.CreateButtonSet(Wizard.StepNextButtonIDShort, Wizard.MoveNextCommandName));
				this.AddButtonCell(tableRow2, this.CreateButtonSet(Wizard.CancelButtonIDShort, Wizard.CancelCommandName, false));
				this._stepNavContainer.ConfirmDefaultTemplate();
			}
			container.Controls.Add(this._stepNavContainer);
			this._finishNavContainer = new Wizard.FinishNavigationContainer(this);
			this._finishNavContainer.ID = "FinishNavigationTemplateContainerID";
			if (this.finishNavigationTemplate != null)
			{
				this.finishNavigationTemplate.InstantiateIn(this._finishNavContainer);
			}
			else
			{
				TableRow tableRow3;
				Wizard.AddNavButtonsTable(this._finishNavContainer, out tableRow3);
				this.AddButtonCell(tableRow3, this.CreateButtonSet(Wizard.FinishPreviousButtonIDShort, Wizard.MovePreviousCommandName, false));
				this.AddButtonCell(tableRow3, this.CreateButtonSet(Wizard.FinishButtonIDShort, Wizard.MoveCompleteCommandName));
				this.AddButtonCell(tableRow3, this.CreateButtonSet(Wizard.CancelButtonIDShort, Wizard.CancelCommandName, false));
				this._finishNavContainer.ConfirmDefaultTemplate();
			}
			container.Controls.Add(this._finishNavContainer);
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x0008508C File Offset: 0x0008328C
		private static void AddNavButtonsTable(Wizard.BaseWizardNavigationContainer container, out TableRow row)
		{
			Table table = new Table();
			table.CellPadding = 5;
			table.CellSpacing = 5;
			row = new TableRow();
			table.Rows.Add(row);
			container.Controls.Add(table);
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x000850CE File Offset: 0x000832CE
		private Control[] CreateButtonSet(string id, string command)
		{
			return this.CreateButtonSet(id, command, true, null);
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x000850DA File Offset: 0x000832DA
		private Control[] CreateButtonSet(string id, string command, bool causesValidation)
		{
			return this.CreateButtonSet(id, command, causesValidation, null);
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x000850E8 File Offset: 0x000832E8
		internal Control[] CreateButtonSet(string id, string command, bool causesValidation, string validationGroup)
		{
			return new Control[]
			{
				this.CreateButton(id + ButtonType.Button.ToString(), command, ButtonType.Button, causesValidation, validationGroup),
				this.CreateButton(id + ButtonType.Image.ToString(), command, ButtonType.Image, causesValidation, validationGroup),
				this.CreateButton(id + ButtonType.Link.ToString(), command, ButtonType.Link, causesValidation, validationGroup)
			};
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x00085164 File Offset: 0x00083364
		private Control CreateButton(string id, string command, ButtonType type, bool causesValidation, string validationGroup)
		{
			WebControl webControl;
			switch (type)
			{
			case ButtonType.Button:
				webControl = this.CreateStandardButton();
				break;
			case ButtonType.Image:
				webControl = this.CreateImageButton(null);
				break;
			case ButtonType.Link:
				webControl = this.CreateLinkButton();
				break;
			default:
				throw new ArgumentOutOfRangeException("type");
			}
			webControl.ID = id;
			webControl.EnableTheming = false;
			((IButtonControl)webControl).CommandName = command;
			((IButtonControl)webControl).CausesValidation = causesValidation;
			if (!string.IsNullOrEmpty(validationGroup))
			{
				((IButtonControl)webControl).ValidationGroup = validationGroup;
			}
			this.RegisterApplyStyle(webControl, this.NavigationButtonStyle);
			return webControl;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x000851F7 File Offset: 0x000833F7
		private WebControl CreateStandardButton()
		{
			return new Button();
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x000851FE File Offset: 0x000833FE
		private WebControl CreateImageButton(string imageUrl)
		{
			return new ImageButton
			{
				ImageUrl = imageUrl
			};
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x0008520C File Offset: 0x0008340C
		private WebControl CreateLinkButton()
		{
			return new LinkButton();
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x00085214 File Offset: 0x00083414
		private void AddButtonCell(TableRow row, params Control[] controls)
		{
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = HorizontalAlign.Right;
			for (int i = 0; i < controls.Length; i++)
			{
				tableCell.Controls.Add(controls[i]);
			}
			row.Cells.Add(tableCell);
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x00085258 File Offset: 0x00083458
		private void CreateSideBar(Control container)
		{
			WebControl webControl = container as WebControl;
			if (webControl != null)
			{
				this.RegisterApplyStyle(webControl, this.SideBarStyle);
			}
			if (this.sideBarTemplate != null)
			{
				this.sideBarTemplate.InstantiateIn(container);
				this.stepDatalist = container.FindControl(Wizard.DataListID) as DataList;
				if (this.stepDatalist == null)
				{
					throw new InvalidOperationException("The side bar template must contain a DataList control with id '" + Wizard.DataListID + "'.");
				}
				this.stepDatalist.ItemDataBound += this.StepDatalistItemDataBound;
			}
			else
			{
				this.stepDatalist = new DataList();
				this.stepDatalist.ID = Wizard.DataListID;
				this.stepDatalist.SelectedItemStyle.Font.Bold = true;
				this.stepDatalist.ItemTemplate = this.SideBarItemTemplate;
				container.Controls.Add(this.stepDatalist);
			}
			this.stepDatalist.ItemCommand += this.StepDatalistItemCommand;
			this.stepDatalist.CellSpacing = 0;
			this.stepDatalist.DataSource = this.WizardSteps;
			this.stepDatalist.SelectedIndex = this.ActiveStepIndex;
			this.stepDatalist.DataBind();
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x00085384 File Offset: 0x00083584
		private void StepDatalistItemCommand(object sender, DataListCommandEventArgs e)
		{
			WizardNavigationEventArgs e2 = new WizardNavigationEventArgs(this.ActiveStepIndex, Convert.ToInt32(e.CommandArgument));
			this.OnSideBarButtonClick(e2);
			if (!e2.Cancel)
			{
				this.ActiveStepIndex = e2.NextStepIndex;
			}
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x000853C4 File Offset: 0x000835C4
		private void StepDatalistItemDataBound(object sender, DataListItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.SelectedItem)
			{
				IButtonControl buttonControl = (IButtonControl)e.Item.FindControl(Wizard.SideBarButtonID);
				if (buttonControl == null)
				{
					throw new InvalidOperationException("SideBarList control must contain an IButtonControl with ID " + Wizard.SideBarButtonID + " in every item template, this maybe include ItemTemplate, EditItemTemplate, SelectedItemTemplate or AlternatingItemTemplate if they exist.");
				}
				WizardStepBase wizardStepBase = (WizardStepBase)e.Item.DataItem;
				if (buttonControl is Button)
				{
					((Button)buttonControl).UseSubmitBehavior = false;
				}
				buttonControl.CommandName = Wizard.MoveToCommandName;
				buttonControl.CommandArgument = this.WizardSteps.IndexOf(wizardStepBase).ToString();
				buttonControl.Text = wizardStepBase.Name;
				if (wizardStepBase.StepType == WizardStepType.Complete && buttonControl is WebControl)
				{
					((WebControl)buttonControl).Enabled = false;
				}
			}
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x000854A4 File Offset: 0x000836A4
		private void AddHeaderRow(Table table)
		{
			TableRow tableRow = new TableRow();
			this._headerCell = new Wizard.WizardHeaderCell();
			this._headerCell.ID = "HeaderContainer";
			this.RegisterApplyStyle(this._headerCell, this.HeaderStyle);
			if (this.headerTemplate != null)
			{
				this.headerTemplate.InstantiateIn(this._headerCell);
				this._headerCell.ConfirmInitState();
			}
			tableRow.Cells.Add(this._headerCell);
			table.Rows.Add(tableRow);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x00085527 File Offset: 0x00083727
		internal void RegisterApplyStyle(WebControl control, Style style)
		{
			this.styles.Add(new object[] { control, style });
		}

		/// <summary>Creates control style.</summary>
		// Token: 0x06003287 RID: 12935 RVA: 0x00085543 File Offset: 0x00083743
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellPadding = 0,
				CellSpacing = 0
			};
		}

		/// <summary>Gets the design mode state.</summary>
		// Token: 0x06003288 RID: 12936 RVA: 0x00003A2B File Offset: 0x00001C2B
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Restores control state information.</summary>
		/// <param name="state">The control state to be restored.</param>
		// Token: 0x06003289 RID: 12937 RVA: 0x00085558 File Offset: 0x00083758
		protected internal override void LoadControlState(object state)
		{
			if (state == null)
			{
				return;
			}
			object[] array = (object[])state;
			base.LoadControlState(array[0]);
			this.activeStepIndex = (int)array[1];
			this.history = (ArrayList)array[2];
		}

		/// <summary>Save the control state.</summary>
		/// <returns>The control state.</returns>
		// Token: 0x0600328A RID: 12938 RVA: 0x00085598 File Offset: 0x00083798
		protected internal override object SaveControlState()
		{
			if (this.GetHistory().Count == 0 || (int)this.history[0] != this.ActiveStepIndex)
			{
				this.history.Insert(0, this.ActiveStepIndex);
			}
			object obj = base.SaveControlState();
			return new object[] { obj, this.activeStepIndex, this.history };
		}

		/// <summary>Loads view-state information.</summary>
		/// <param name="savedState">The control state to be restored.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="savedState" /> is not a valid <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> value.</exception>
		// Token: 0x0600328B RID: 12939 RVA: 0x0008560C File Offset: 0x0008380C
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.StepStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.SideBarStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.NavigationStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.SideBarButtonStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.CancelButtonStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.FinishCompleteButtonStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.FinishPreviousButtonStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.StartNextButtonStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.StepNextButtonStyle).LoadViewState(array[10]);
			}
			if (array[11] != null)
			{
				((IStateManager)this.StepPreviousButtonStyle).LoadViewState(array[11]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.NavigationButtonStyle).LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				base.ControlStyle.LoadViewState(array[13]);
			}
		}

		/// <summary>Saves the view state.</summary>
		/// <returns>The view state.</returns>
		// Token: 0x0600328C RID: 12940 RVA: 0x00085738 File Offset: 0x00083938
		protected override object SaveViewState()
		{
			object[] array = new object[14];
			array[0] = base.SaveViewState();
			if (this.stepStyle != null)
			{
				array[1] = ((IStateManager)this.stepStyle).SaveViewState();
			}
			if (this.sideBarStyle != null)
			{
				array[2] = ((IStateManager)this.sideBarStyle).SaveViewState();
			}
			if (this.headerStyle != null)
			{
				array[3] = ((IStateManager)this.headerStyle).SaveViewState();
			}
			if (this.navigationStyle != null)
			{
				array[4] = ((IStateManager)this.navigationStyle).SaveViewState();
			}
			if (this.sideBarButtonStyle != null)
			{
				array[5] = ((IStateManager)this.sideBarButtonStyle).SaveViewState();
			}
			if (this.cancelButtonStyle != null)
			{
				array[6] = ((IStateManager)this.cancelButtonStyle).SaveViewState();
			}
			if (this.finishCompleteButtonStyle != null)
			{
				array[7] = ((IStateManager)this.finishCompleteButtonStyle).SaveViewState();
			}
			if (this.finishPreviousButtonStyle != null)
			{
				array[8] = ((IStateManager)this.finishPreviousButtonStyle).SaveViewState();
			}
			if (this.startNextButtonStyle != null)
			{
				array[9] = ((IStateManager)this.startNextButtonStyle).SaveViewState();
			}
			if (this.stepNextButtonStyle != null)
			{
				array[10] = ((IStateManager)this.stepNextButtonStyle).SaveViewState();
			}
			if (this.stepPreviousButtonStyle != null)
			{
				array[11] = ((IStateManager)this.stepPreviousButtonStyle).SaveViewState();
			}
			if (this.navigationButtonStyle != null)
			{
				array[12] = ((IStateManager)this.navigationButtonStyle).SaveViewState();
			}
			if (base.ControlStyleCreated)
			{
				array[13] = base.ControlStyle.SaveViewState();
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Tracks view state.</summary>
		// Token: 0x0600328D RID: 12941 RVA: 0x00085890 File Offset: 0x00083A90
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.stepStyle != null)
			{
				((IStateManager)this.stepStyle).TrackViewState();
			}
			if (this.sideBarStyle != null)
			{
				((IStateManager)this.sideBarStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.navigationStyle != null)
			{
				((IStateManager)this.navigationStyle).TrackViewState();
			}
			if (this.sideBarButtonStyle != null)
			{
				((IStateManager)this.sideBarButtonStyle).TrackViewState();
			}
			if (this.cancelButtonStyle != null)
			{
				((IStateManager)this.cancelButtonStyle).TrackViewState();
			}
			if (this.finishCompleteButtonStyle != null)
			{
				((IStateManager)this.finishCompleteButtonStyle).TrackViewState();
			}
			if (this.finishPreviousButtonStyle != null)
			{
				((IStateManager)this.finishPreviousButtonStyle).TrackViewState();
			}
			if (this.startNextButtonStyle != null)
			{
				((IStateManager)this.startNextButtonStyle).TrackViewState();
			}
			if (this.stepNextButtonStyle != null)
			{
				((IStateManager)this.stepNextButtonStyle).TrackViewState();
			}
			if (this.stepPreviousButtonStyle != null)
			{
				((IStateManager)this.stepPreviousButtonStyle).TrackViewState();
			}
			if (this.navigationButtonStyle != null)
			{
				((IStateManager)this.navigationButtonStyle).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				base.ControlStyle.TrackViewState();
			}
		}

		/// <summary>Registers a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventHandler" /> class for the specified <see cref="T:System.Web.UI.WebControls.IButtonControl" /> object.</summary>
		/// <param name="button">The <see cref="T:System.Web.UI.WebControls.IButtonControl" /> for which the new instance of <see cref="T:System.Web.UI.WebControls.CommandEventHandler" /> is registered.</param>
		// Token: 0x0600328E RID: 12942 RVA: 0x0008599A File Offset: 0x00083B9A
		protected internal void RegisterCommandEvents(IButtonControl button)
		{
			button.Command += this.ProcessCommand;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000859B0 File Offset: 0x00083BB0
		private void ProcessCommand(object sender, CommandEventArgs args)
		{
			Control control = sender as Control;
			if (control != null)
			{
				string id = control.ID;
				if (id == "CancelButton")
				{
					this.ProcessEvent("Cancel", null);
					return;
				}
				if (id == "FinishButton")
				{
					this.ProcessEvent("MoveComplete", null);
					return;
				}
				if (id == "StepPreviousButton" || id == "FinishPreviousButton")
				{
					this.ProcessEvent("MovePrevious", null);
					return;
				}
				if (id == "StartNextButton" || id == "StepNextButton")
				{
					this.ProcessEvent("MoveNext", null);
					return;
				}
			}
			this.ProcessEvent(args.CommandName, args.CommandArgument as string);
		}

		/// <summary>Determines whether the event for the server control is passed up the page’s user interface server control hierarchy.</summary>
		/// <returns>true if the event for the server control is passed up the page’s user interface server control hierarchy; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">Contains event data.</param>
		// Token: 0x06003290 RID: 12944 RVA: 0x00085A6C File Offset: 0x00083C6C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs e2 = e as CommandEventArgs;
			if (e2 != null)
			{
				this.ProcessEvent(e2.CommandName, e2.CommandArgument as string);
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x00085AA4 File Offset: 0x00083CA4
		private void ProcessEvent(string commandName, string commandArg)
		{
			if (!(commandName == "Cancel"))
			{
				if (!(commandName == "MoveComplete"))
				{
					if (!(commandName == "MoveNext"))
					{
						if (!(commandName == "MovePrevious"))
						{
							if (!(commandName == "Move"))
							{
								return;
							}
							int num = int.Parse(commandArg);
							this.ActiveStepIndex = num;
						}
						else if (this.ActiveStepIndex > 0)
						{
							WizardNavigationEventArgs e = new WizardNavigationEventArgs(this.ActiveStepIndex, this.ActiveStepIndex - 1);
							int num2 = this.ActiveStepIndex;
							this.OnPreviousButtonClick(e);
							if (!e.Cancel)
							{
								if (num2 == this.activeStepIndex)
								{
									int num3 = this.ActiveStepIndex;
									this.ActiveStepIndex = num3 - 1;
								}
								if (this.history != null && this.activeStepIndex < num2)
								{
									this.history.Remove(num2);
									return;
								}
							}
						}
					}
					else if (this.ActiveStepIndex < this.WizardSteps.Count - 1)
					{
						WizardNavigationEventArgs e2 = new WizardNavigationEventArgs(this.ActiveStepIndex, this.ActiveStepIndex + 1);
						int num4 = this.ActiveStepIndex;
						this.OnNextButtonClick(e2);
						if (!e2.Cancel && num4 == this.activeStepIndex)
						{
							int num3 = this.ActiveStepIndex;
							this.ActiveStepIndex = num3 + 1;
							return;
						}
					}
				}
				else
				{
					int num5 = -1;
					for (int i = 0; i < this.WizardSteps.Count; i++)
					{
						if (this.WizardSteps[i].StepType == WizardStepType.Complete)
						{
							num5 = i;
							break;
						}
					}
					if (num5 == -1 && this.ActiveStepIndex == this.WizardSteps.Count - 1)
					{
						num5 = this.ActiveStepIndex;
					}
					WizardNavigationEventArgs e3 = new WizardNavigationEventArgs(this.ActiveStepIndex, num5);
					this.OnFinishButtonClick(e3);
					if (this.FinishDestinationPageUrl.Length > 0)
					{
						this.Context.Response.Redirect(this.FinishDestinationPageUrl);
						return;
					}
					if (num5 != -1 && !e3.Cancel)
					{
						this.ActiveStepIndex = num5;
						return;
					}
				}
				return;
			}
			if (this.CancelDestinationPageUrl.Length > 0)
			{
				this.Context.Response.Redirect(this.CancelDestinationPageUrl);
				return;
			}
			this.OnCancelButtonClick(EventArgs.Empty);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x00076EE5 File Offset: 0x000750E5
		internal void UpdateViews()
		{
			base.ChildControlsCreated = false;
		}

		/// <summary>Renders the control to the specified writer.</summary>
		/// <param name="writer">The HTML writer.</param>
		// Token: 0x06003293 RID: 12947 RVA: 0x00085CBF File Offset: 0x00083EBF
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			if (this.LayoutTemplate == null)
			{
				this.wizardTable.Render(writer);
				return;
			}
			this.RenderChildren(writer);
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00085CE4 File Offset: 0x00083EE4
		private void PrepareControlHierarchy()
		{
			if (this.LayoutTemplate == null)
			{
				if (!this._headerCell.Initialized)
				{
					if (string.IsNullOrEmpty(this.HeaderText))
					{
						this._headerCell.Parent.Visible = false;
					}
					else
					{
						this._headerCell.Text = this.HeaderText;
					}
				}
				if (this.ActiveStep.StepType == WizardStepType.Complete)
				{
					this._headerCell.Parent.Visible = false;
				}
			}
			else
			{
				WizardStepCollection wizardSteps = this.WizardSteps;
				if (wizardSteps == null || wizardSteps.Count == 0)
				{
					return;
				}
			}
			if (this.stepDatalist != null)
			{
				this.stepDatalist.SelectedIndex = this.ActiveStepIndex;
				this.stepDatalist.DataBind();
				if (this.ActiveStep.StepType == WizardStepType.Complete)
				{
					this.stepDatalist.NamingContainer.Visible = false;
				}
			}
			TemplatedWizardStep templatedWizardStep = this.ActiveStep as TemplatedWizardStep;
			if (templatedWizardStep != null)
			{
				Wizard.BaseWizardContainer baseWizardContainer = templatedWizardStep.ContentTemplateContainer as Wizard.BaseWizardContainer;
				if (baseWizardContainer != null)
				{
					baseWizardContainer.PrepareControlHierarchy();
				}
			}
			if (this.customNavigation != null)
			{
				foreach (object obj in this.customNavigation.Values)
				{
					((Control)obj).Visible = false;
				}
			}
			this._startNavContainer.Visible = false;
			this._stepNavContainer.Visible = false;
			this._finishNavContainer.Visible = false;
			Wizard.BaseWizardNavigationContainer currentNavContainer = this.GetCurrentNavContainer();
			if (currentNavContainer == null)
			{
				if (this._navigationCell != null)
				{
					this._navigationCell.Parent.Visible = false;
				}
			}
			else
			{
				currentNavContainer.Visible = true;
				currentNavContainer.PrepareControlHierarchy();
				if (this._navigationCell != null && !currentNavContainer.Visible)
				{
					this._navigationCell.Parent.Visible = false;
				}
			}
			foreach (object obj2 in this.styles)
			{
				object[] array = (object[])obj2;
				((WebControl)array[0]).ApplyStyle((Style)array[1]);
			}
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x00085F08 File Offset: 0x00084108
		private Wizard.BaseWizardNavigationContainer GetCurrentNavContainer()
		{
			if (this.customNavigation != null && this.customNavigation[this.ActiveStep] != null)
			{
				return (Wizard.BaseWizardNavigationContainer)this.customNavigation[this.ActiveStep];
			}
			switch (this.GetStepType(this.ActiveStep, this.ActiveStepIndex))
			{
			case WizardStepType.Finish:
				return this._finishNavContainer;
			case WizardStepType.Start:
				return this._startNavContainer;
			case WizardStepType.Step:
				return this._stepNavContainer;
			default:
				return null;
			}
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x00085FA0 File Offset: 0x000841A0
		// Note: this type is marked as 'beforefieldinit'.
		static Wizard()
		{
			Wizard.ActiveStepChangedEvent = new object();
			Wizard.CancelButtonClickEvent = new object();
			Wizard.FinishButtonClickEvent = new object();
			Wizard.NextButtonClickEvent = new object();
			Wizard.PreviousButtonClickEvent = new object();
			Wizard.SideBarButtonClickEvent = new object();
		}

		/// <summary>Retrieves the command name for the Cancel button. This field is static and read-only.</summary>
		// Token: 0x04001C66 RID: 7270
		public static readonly string CancelCommandName = "Cancel";

		/// <summary>Retrieves the command name that is associated with the Finish button. This field is static and read-only.</summary>
		// Token: 0x04001C67 RID: 7271
		public static readonly string MoveCompleteCommandName = "MoveComplete";

		/// <summary>Retrieves the command name that is associated with the Next button. This field is static and read-only.</summary>
		// Token: 0x04001C68 RID: 7272
		public static readonly string MoveNextCommandName = "MoveNext";

		/// <summary>Retrieves the command name that is associated with the Previous button. This field is static and read-only. </summary>
		// Token: 0x04001C69 RID: 7273
		public static readonly string MovePreviousCommandName = "MovePrevious";

		/// <summary>Retrieves the command name that is associated with each of the sidebar buttons. This field is static and read-only. </summary>
		// Token: 0x04001C6A RID: 7274
		public static readonly string MoveToCommandName = "Move";

		/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.HeaderTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C6B RID: 7275
		public static readonly string HeaderPlaceholderId = "headerPlaceholder";

		/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.StartNavigationTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C6C RID: 7276
		public static readonly string NavigationPlaceholderId = "navigationPlaceholder";

		/// <summary>Gets the ID of the <see cref="P:System.Web.UI.WebControls.Wizard.SideBarTemplate" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C6D RID: 7277
		public static readonly string SideBarPlaceholderId = "sideBarPlaceholder";

		/// <summary>Gets the ID of the <see cref="T:System.Web.UI.WebControls.WizardStep" /> placeholder in a <see cref="T:System.Web.UI.WebControls.Wizard" /> control.</summary>
		// Token: 0x04001C6E RID: 7278
		public static readonly string WizardStepPlaceholderId = "wizardStepPlaceholder";

		/// <summary>Retrieves the identifier for the sidebar <see cref="T:System.Web.UI.WebControls.DataList" /> collection. This field is static and read-only.</summary>
		// Token: 0x04001C6F RID: 7279
		protected static readonly string DataListID = "SideBarList";

		// Token: 0x04001C70 RID: 7280
		private static readonly string CancelButtonIDShort = "Cancel";

		/// <summary>Specifies the identifier for the Cancel button. This field is static and read-only.</summary>
		// Token: 0x04001C71 RID: 7281
		protected static readonly string CancelButtonID = Wizard.CancelButtonIDShort + "Button";

		// Token: 0x04001C72 RID: 7282
		private static readonly string CustomFinishButtonIDShort = "CustomFinish";

		/// <summary>Retrieves the identifier for a custom Finish button. This field is static and read-only.</summary>
		// Token: 0x04001C73 RID: 7283
		protected static readonly string CustomFinishButtonID = Wizard.CustomFinishButtonIDShort + "Button";

		// Token: 0x04001C74 RID: 7284
		private static readonly string CustomNextButtonIDShort = "CustomNext";

		/// <summary>Retrieves the identifier for a custom Next button. This field is static and read-only.</summary>
		// Token: 0x04001C75 RID: 7285
		protected static readonly string CustomNextButtonID = Wizard.CustomNextButtonIDShort + "Button";

		// Token: 0x04001C76 RID: 7286
		private static readonly string CustomPreviousButtonIDShort = "CustomPrevious";

		/// <summary>Retrieves the identifier for a custom Previous button. This field is static and read-only.</summary>
		// Token: 0x04001C77 RID: 7287
		protected static readonly string CustomPreviousButtonID = Wizard.CustomPreviousButtonIDShort + "Button";

		// Token: 0x04001C78 RID: 7288
		private static readonly string FinishButtonIDShort = "Finish";

		/// <summary>Retrieves the identifier for the Finish button. This field is static and read-only.</summary>
		// Token: 0x04001C79 RID: 7289
		protected static readonly string FinishButtonID = Wizard.FinishButtonIDShort + "Button";

		// Token: 0x04001C7A RID: 7290
		private static readonly string FinishPreviousButtonIDShort = "FinishPrevious";

		/// <summary>Retrieves the identifier for the Previous button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Finish" /> step. This field is static and read-only.</summary>
		// Token: 0x04001C7B RID: 7291
		protected static readonly string FinishPreviousButtonID = Wizard.FinishPreviousButtonIDShort + "Button";

		// Token: 0x04001C7C RID: 7292
		private static readonly string SideBarButtonIDShort = "SideBar";

		/// <summary>Retrieves the identifier that is associated with each of the sidebar buttons. This field is static and read-only. </summary>
		// Token: 0x04001C7D RID: 7293
		protected static readonly string SideBarButtonID = Wizard.SideBarButtonIDShort + "Button";

		// Token: 0x04001C7E RID: 7294
		private static readonly string StartNextButtonIDShort = "StartNext";

		/// <summary>Retrieves the identifier that is associated with the Next button on the <see cref="F:System.Web.UI.WebControls.WizardStepType.Start" /> step. This field is static and read-only. </summary>
		// Token: 0x04001C7F RID: 7295
		protected static readonly string StartNextButtonID = Wizard.StartNextButtonIDShort + "Button";

		// Token: 0x04001C80 RID: 7296
		private static readonly string StepNextButtonIDShort = "StepNext";

		/// <summary>Retrieves the identifier that is associated with the Next button. This field is static and read-only. </summary>
		// Token: 0x04001C81 RID: 7297
		protected static readonly string StepNextButtonID = Wizard.StepNextButtonIDShort + "Button";

		// Token: 0x04001C82 RID: 7298
		private static readonly string StepPreviousButtonIDShort = "StepPrevious";

		/// <summary>Retrieves the identifier that is associated with the Previous button. This field is static and read-only. </summary>
		// Token: 0x04001C83 RID: 7299
		protected static readonly string StepPreviousButtonID = Wizard.StepPreviousButtonIDShort + "Button";

		// Token: 0x04001C84 RID: 7300
		private WizardStepCollection steps;

		// Token: 0x04001C85 RID: 7301
		private TableItemStyle stepStyle;

		// Token: 0x04001C86 RID: 7302
		private TableItemStyle sideBarStyle;

		// Token: 0x04001C87 RID: 7303
		private TableItemStyle headerStyle;

		// Token: 0x04001C88 RID: 7304
		private TableItemStyle navigationStyle;

		// Token: 0x04001C89 RID: 7305
		private Style sideBarButtonStyle;

		// Token: 0x04001C8A RID: 7306
		private Style cancelButtonStyle;

		// Token: 0x04001C8B RID: 7307
		private Style finishCompleteButtonStyle;

		// Token: 0x04001C8C RID: 7308
		private Style finishPreviousButtonStyle;

		// Token: 0x04001C8D RID: 7309
		private Style startNextButtonStyle;

		// Token: 0x04001C8E RID: 7310
		private Style stepNextButtonStyle;

		// Token: 0x04001C8F RID: 7311
		private Style stepPreviousButtonStyle;

		// Token: 0x04001C90 RID: 7312
		private Style navigationButtonStyle;

		// Token: 0x04001C91 RID: 7313
		private ITemplate finishNavigationTemplate;

		// Token: 0x04001C92 RID: 7314
		private ITemplate startNavigationTemplate;

		// Token: 0x04001C93 RID: 7315
		private ITemplate stepNavigationTemplate;

		// Token: 0x04001C94 RID: 7316
		private ITemplate headerTemplate;

		// Token: 0x04001C95 RID: 7317
		private ITemplate sideBarTemplate;

		// Token: 0x04001C96 RID: 7318
		private int activeStepIndex = -1;

		// Token: 0x04001C97 RID: 7319
		private bool inited;

		// Token: 0x04001C98 RID: 7320
		private ArrayList history;

		// Token: 0x04001C99 RID: 7321
		private Table wizardTable;

		// Token: 0x04001C9A RID: 7322
		private Wizard.WizardHeaderCell _headerCell;

		// Token: 0x04001C9B RID: 7323
		private TableCell _navigationCell;

		// Token: 0x04001C9C RID: 7324
		private Wizard.StartNavigationContainer _startNavContainer;

		// Token: 0x04001C9D RID: 7325
		private Wizard.StepNavigationContainer _stepNavContainer;

		// Token: 0x04001C9E RID: 7326
		private Wizard.FinishNavigationContainer _finishNavContainer;

		// Token: 0x04001C9F RID: 7327
		private MultiView multiView;

		// Token: 0x04001CA0 RID: 7328
		private DataList stepDatalist;

		// Token: 0x04001CA1 RID: 7329
		private ArrayList styles = new ArrayList();

		// Token: 0x04001CA2 RID: 7330
		private Hashtable customNavigation;

		// Token: 0x0200043B RID: 1083
		private sealed class TableCellNamingContainer : TableCell, INamingContainer, INonBindingContainer
		{
			// Token: 0x06003298 RID: 12952 RVA: 0x0008617C File Offset: 0x0008437C
			protected internal override void RenderChildren(HtmlTextWriter writer)
			{
				if (this.haveSkipLink)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "#" + this.clientId + "_SkipLink");
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.AddAttribute(HtmlTextWriterAttribute.Alt, this.skipLinkText);
					writer.AddAttribute(HtmlTextWriterAttribute.Height, "0");
					writer.AddAttribute(HtmlTextWriterAttribute.Width, "0");
					Page page = this.Page;
					ClientScriptManager clientScriptManager;
					if (page != null)
					{
						clientScriptManager = page.ClientScript;
					}
					else
					{
						clientScriptManager = new ClientScriptManager(null);
					}
					writer.AddAttribute(HtmlTextWriterAttribute.Src, clientScriptManager.GetWebResourceUrl(typeof(SiteMapPath), "transparent.gif"));
					writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "0px");
					writer.RenderBeginTag(HtmlTextWriterTag.Img);
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				base.RenderChildren(writer);
				if (this.haveSkipLink)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Id, "SkipLink");
					writer.RenderBeginTag(HtmlTextWriterTag.A);
					writer.RenderEndTag();
				}
			}

			// Token: 0x06003299 RID: 12953 RVA: 0x0008625F File Offset: 0x0008445F
			public TableCellNamingContainer(string skipLinkText, string clientId)
			{
				this.skipLinkText = skipLinkText;
				this.clientId = clientId;
				this.haveSkipLink = !string.IsNullOrEmpty(skipLinkText);
			}

			// Token: 0x04001CAA RID: 7338
			private string skipLinkText;

			// Token: 0x04001CAB RID: 7339
			private string clientId;

			// Token: 0x04001CAC RID: 7340
			private bool haveSkipLink;
		}

		// Token: 0x0200043C RID: 1084
		private sealed class SideBarButtonTemplate : ITemplate
		{
			// Token: 0x0600329A RID: 12954 RVA: 0x00086284 File Offset: 0x00084484
			public SideBarButtonTemplate(Wizard wizard)
			{
				this.wizard = wizard;
			}

			// Token: 0x0600329B RID: 12955 RVA: 0x00086294 File Offset: 0x00084494
			public void InstantiateIn(Control control)
			{
				LinkButton linkButton = new LinkButton();
				this.wizard.RegisterApplyStyle(linkButton, this.wizard.SideBarButtonStyle);
				control.Controls.Add(linkButton);
				control.DataBinding += this.Bound;
			}

			// Token: 0x0600329C RID: 12956 RVA: 0x000862DC File Offset: 0x000844DC
			private void Bound(object s, EventArgs args)
			{
				WizardStepBase wizardStepBase = DataBinder.GetDataItem(s) as WizardStepBase;
				if (wizardStepBase != null)
				{
					LinkButton linkButton = (LinkButton)((DataListItem)s).Controls[0];
					linkButton.ID = Wizard.SideBarButtonID;
					linkButton.CommandName = Wizard.MoveToCommandName;
					linkButton.CommandArgument = this.wizard.WizardSteps.IndexOf(wizardStepBase).ToString();
					linkButton.Text = wizardStepBase.Name;
					if (wizardStepBase.StepType == WizardStepType.Complete)
					{
						linkButton.Enabled = false;
					}
				}
			}

			// Token: 0x04001CAD RID: 7341
			private Wizard wizard;
		}

		// Token: 0x0200043D RID: 1085
		private class WizardHeaderCell : TableCell, INamingContainer, INonBindingContainer
		{
			// Token: 0x17001015 RID: 4117
			// (get) Token: 0x0600329D RID: 12957 RVA: 0x00086360 File Offset: 0x00084560
			public bool Initialized
			{
				get
				{
					return this._initialized;
				}
			}

			// Token: 0x0600329F RID: 12959 RVA: 0x00086370 File Offset: 0x00084570
			public void ConfirmInitState()
			{
				this._initialized = true;
			}

			// Token: 0x04001CAE RID: 7342
			private bool _initialized;
		}

		// Token: 0x0200043E RID: 1086
		internal abstract class DefaultNavigationContainer : Wizard.BaseWizardNavigationContainer
		{
			// Token: 0x17001016 RID: 4118
			// (get) Token: 0x060032A0 RID: 12960 RVA: 0x00086379 File Offset: 0x00084579
			protected Wizard Wizard
			{
				get
				{
					return this._wizard;
				}
			}

			// Token: 0x060032A1 RID: 12961 RVA: 0x00086381 File Offset: 0x00084581
			protected DefaultNavigationContainer(Wizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x060032A2 RID: 12962 RVA: 0x00086390 File Offset: 0x00084590
			public sealed override void PrepareControlHierarchy()
			{
				if (this._isDefault)
				{
					this.UpdateState();
				}
			}

			// Token: 0x060032A3 RID: 12963
			protected abstract void UpdateState();

			// Token: 0x060032A4 RID: 12964 RVA: 0x000863A0 File Offset: 0x000845A0
			public void ConfirmDefaultTemplate()
			{
				this._isDefault = true;
			}

			// Token: 0x060032A5 RID: 12965 RVA: 0x000863AC File Offset: 0x000845AC
			protected void UpdateNavButtonState(string id, string text, string image, Style style)
			{
				WebControl webControl = (WebControl)this.FindControl(id);
				foreach (object obj in webControl.Parent.Controls)
				{
					Control control = (Control)obj;
					control.Visible = webControl == control;
				}
				((IButtonControl)webControl).Text = text;
				ImageButton imageButton = webControl as ImageButton;
				if (imageButton != null)
				{
					imageButton.ImageUrl = image;
				}
				webControl.ApplyStyle(style);
			}

			// Token: 0x04001CAF RID: 7343
			private bool _isDefault;

			// Token: 0x04001CB0 RID: 7344
			private Wizard _wizard;
		}

		// Token: 0x0200043F RID: 1087
		private sealed class StartNavigationContainer : Wizard.DefaultNavigationContainer
		{
			// Token: 0x060032A6 RID: 12966 RVA: 0x00086444 File Offset: 0x00084644
			public StartNavigationContainer(Wizard wizard)
				: base(wizard)
			{
			}

			// Token: 0x060032A7 RID: 12967 RVA: 0x00086450 File Offset: 0x00084650
			protected override void UpdateState()
			{
				bool flag = false;
				if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex + 1))
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.StartNextButtonIDShort + base.Wizard.StartNextButtonType.ToString(), base.Wizard.StartNextButtonText, base.Wizard.StartNextButtonImageUrl, base.Wizard.StartNextButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[0].Visible = false;
				}
				if (base.Wizard.DisplayCancelButton)
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.CancelButtonIDShort + base.Wizard.CancelButtonType.ToString(), base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[1].Visible = false;
				}
				this.Visible = flag;
			}
		}

		// Token: 0x02000440 RID: 1088
		private sealed class StepNavigationContainer : Wizard.DefaultNavigationContainer
		{
			// Token: 0x060032A8 RID: 12968 RVA: 0x00086444 File Offset: 0x00084644
			public StepNavigationContainer(Wizard wizard)
				: base(wizard)
			{
			}

			// Token: 0x060032A9 RID: 12969 RVA: 0x00086584 File Offset: 0x00084784
			protected override void UpdateState()
			{
				bool flag = false;
				if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex - 1))
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.StepPreviousButtonIDShort + base.Wizard.StepPreviousButtonType.ToString(), base.Wizard.StepPreviousButtonText, base.Wizard.StepPreviousButtonImageUrl, base.Wizard.StepPreviousButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[0].Visible = false;
				}
				if (base.Wizard.AllowNavigationToStep(base.Wizard.ActiveStepIndex + 1))
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.StepNextButtonIDShort + base.Wizard.StepNextButtonType.ToString(), base.Wizard.StepNextButtonText, base.Wizard.StepNextButtonImageUrl, base.Wizard.StepNextButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[1].Visible = false;
				}
				if (base.Wizard.DisplayCancelButton)
				{
					flag = true;
					base.UpdateNavButtonState(Wizard.CancelButtonIDShort + base.Wizard.CancelButtonType.ToString(), base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[2].Visible = false;
				}
				this.Visible = flag;
			}
		}

		// Token: 0x02000441 RID: 1089
		private sealed class FinishNavigationContainer : Wizard.DefaultNavigationContainer
		{
			// Token: 0x060032AA RID: 12970 RVA: 0x00086444 File Offset: 0x00084644
			public FinishNavigationContainer(Wizard wizard)
				: base(wizard)
			{
			}

			// Token: 0x060032AB RID: 12971 RVA: 0x0008674C File Offset: 0x0008494C
			protected override void UpdateState()
			{
				int num = base.Wizard.ActiveStepIndex - 1;
				if (num >= 0 && base.Wizard.AllowNavigationToStep(num))
				{
					base.UpdateNavButtonState(Wizard.FinishPreviousButtonIDShort + base.Wizard.FinishPreviousButtonType.ToString(), base.Wizard.FinishPreviousButtonText, base.Wizard.FinishPreviousButtonImageUrl, base.Wizard.FinishPreviousButtonStyle);
				}
				else
				{
					((Table)this.Controls[0]).Rows[0].Cells[0].Visible = false;
				}
				base.UpdateNavButtonState(Wizard.FinishButtonIDShort + base.Wizard.FinishCompleteButtonType.ToString(), base.Wizard.FinishCompleteButtonText, base.Wizard.FinishCompleteButtonImageUrl, base.Wizard.FinishCompleteButtonStyle);
				if (base.Wizard.DisplayCancelButton)
				{
					base.UpdateNavButtonState(Wizard.CancelButtonIDShort + base.Wizard.CancelButtonType.ToString(), base.Wizard.CancelButtonText, base.Wizard.CancelButtonImageUrl, base.Wizard.CancelButtonStyle);
					return;
				}
				((Table)this.Controls[0]).Rows[0].Cells[2].Visible = false;
			}
		}

		// Token: 0x02000442 RID: 1090
		internal class BaseWizardContainer : Table, INamingContainer, INonBindingContainer
		{
			// Token: 0x17001017 RID: 4119
			// (get) Token: 0x060032AC RID: 12972 RVA: 0x000868C1 File Offset: 0x00084AC1
			public TableCell InnerCell
			{
				get
				{
					return this.Rows[0].Cells[0];
				}
			}

			// Token: 0x060032AD RID: 12973 RVA: 0x000868DA File Offset: 0x00084ADA
			internal BaseWizardContainer()
			{
				this.InitTable();
			}

			// Token: 0x060032AE RID: 12974 RVA: 0x000868E8 File Offset: 0x00084AE8
			private void InitTable()
			{
				TableRow tableRow = new TableRow();
				TableCell tableCell = new TableCell();
				tableCell.ControlStyle.Width = Unit.Percentage(100.0);
				tableCell.ControlStyle.Height = Unit.Percentage(100.0);
				tableRow.Cells.Add(tableCell);
				base.ControlStyle.Width = Unit.Percentage(100.0);
				base.ControlStyle.Height = Unit.Percentage(100.0);
				this.CellPadding = 0;
				this.CellSpacing = 0;
				this.Rows.Add(tableRow);
			}

			// Token: 0x060032AF RID: 12975 RVA: 0x00003946 File Offset: 0x00001B46
			public virtual void PrepareControlHierarchy()
			{
			}
		}

		// Token: 0x02000443 RID: 1091
		internal class BaseWizardNavigationContainer : Control, INamingContainer, INonBindingContainer
		{
			// Token: 0x060032B0 RID: 12976 RVA: 0x0002993C File Offset: 0x00027B3C
			internal BaseWizardNavigationContainer()
			{
			}

			// Token: 0x060032B1 RID: 12977 RVA: 0x00003946 File Offset: 0x00001B46
			public virtual void PrepareControlHierarchy()
			{
			}
		}

		// Token: 0x02000444 RID: 1092
		internal abstract class DefaultContentContainer : Wizard.BaseWizardContainer
		{
			// Token: 0x17001018 RID: 4120
			// (get) Token: 0x060032B2 RID: 12978 RVA: 0x0008698D File Offset: 0x00084B8D
			protected bool IsDefaultTemplate
			{
				get
				{
					return this._isDefault;
				}
			}

			// Token: 0x17001019 RID: 4121
			// (get) Token: 0x060032B3 RID: 12979 RVA: 0x00086995 File Offset: 0x00084B95
			protected Wizard Wizard
			{
				get
				{
					return this._wizard;
				}
			}

			// Token: 0x060032B4 RID: 12980 RVA: 0x0008699D File Offset: 0x00084B9D
			protected DefaultContentContainer(Wizard wizard)
			{
				this._wizard = wizard;
			}

			// Token: 0x060032B5 RID: 12981 RVA: 0x000869AC File Offset: 0x00084BAC
			public sealed override void PrepareControlHierarchy()
			{
				if (this._isDefault)
				{
					this.UpdateState();
				}
			}

			// Token: 0x060032B6 RID: 12982
			protected abstract void UpdateState();

			// Token: 0x060032B7 RID: 12983 RVA: 0x000869BC File Offset: 0x00084BBC
			public void ConfirmDefaultTemplate()
			{
				this._isDefault = true;
			}

			// Token: 0x04001CB1 RID: 7345
			private bool _isDefault;

			// Token: 0x04001CB2 RID: 7346
			private Wizard _wizard;
		}
	}
}
