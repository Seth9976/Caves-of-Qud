using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Security;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the appropriate content template for a given user, based on the user's authentication status and role membership.</summary>
	// Token: 0x020003C7 RID: 967
	[Bindable(true)]
	[DefaultEvent("ViewChanged")]
	[DefaultProperty("CurrentView")]
	[Designer("System.Web.UI.Design.WebControls.LoginViewDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[Themeable(true)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LoginView : Control, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		// Token: 0x0600288F RID: 10383 RVA: 0x0006800B File Offset: 0x0006620B
		public LoginView()
		{
			this.theming = true;
		}

		/// <summary>Gets or sets the template to display to users who are not logged in to the Web site.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> to display.</returns>
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x0006801A File Offset: 0x0006621A
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x00068022 File Offset: 0x00066222
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
		public virtual ITemplate AnonymousTemplate
		{
			get
			{
				return this.anonymousTemplate;
			}
			set
			{
				this.anonymousTemplate = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.ControlCollection" /> object that contains the child controls for the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		/// <returns>The collection of child controls for the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</returns>
		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x0004505A File Offset: 0x0004325A
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		/// <summary>Gets or sets a value indicating whether themes can be applied to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. </summary>
		/// <returns>true to use themes; otherwise, false. The default is true.</returns>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x0006802B File Offset: 0x0006622B
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x00068033 File Offset: 0x00066233
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return this.theming;
			}
			set
			{
				this.theming = value;
			}
		}

		/// <summary>Gets or sets the template to display to Web site users who are logged in to the Web site but are not members of one of the role groups specified in the <see cref="P:System.Web.UI.WebControls.LoginView.RoleGroups" /> property.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ITemplate" /> to display.</returns>
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x0006803C File Offset: 0x0006623C
		// (set) Token: 0x06002896 RID: 10390 RVA: 0x00068044 File Offset: 0x00066244
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(LoginView))]
		[DefaultValue(null)]
		public virtual ITemplate LoggedInTemplate
		{
			get
			{
				return this.loggedInTemplate;
			}
			set
			{
				this.loggedInTemplate = value;
			}
		}

		/// <summary>Gets a collection of role groups that associate content templates with particular roles.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" /> object that contains the defined role-group templates.</returns>
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x0006804D File Offset: 0x0006624D
		[Filterable(false)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Themeable(false)]
		public RoleGroupCollection RoleGroups
		{
			get
			{
				if (this.coll == null)
				{
					this.coll = new RoleGroupCollection();
				}
				return this.coll;
			}
		}

		/// <summary>Gets or sets the skin to apply to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		/// <returns>The name of the skin to apply to the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The skin specified in the <see cref="P:System.Web.UI.WebControls.LoginView.SkinID" /> property does not exist in the theme. </exception>
		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002898 RID: 10392 RVA: 0x0003003F File Offset: 0x0002E23F
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x00030047 File Offset: 0x0002E247
		[Browsable(true)]
		public override string SkinID
		{
			get
			{
				return base.SkinID;
			}
			set
			{
				base.SkinID = value;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x0600289A RID: 10394 RVA: 0x00068068 File Offset: 0x00066268
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x00068070 File Offset: 0x00066270
		private bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
			set
			{
				if (value == this.isAuthenticated)
				{
					return;
				}
				this.isAuthenticated = value;
				this.OnViewChanging(EventArgs.Empty);
				base.ChildControlsCreated = false;
				this.OnViewChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000680A0 File Offset: 0x000662A0
		private ITemplate GetTemplateFromRoleGroup(RoleGroup rg, IPrincipal user)
		{
			if (user == null)
			{
				return null;
			}
			foreach (string text in rg.Roles)
			{
				if (user.IsInRole(text))
				{
					return rg.ContentTemplate;
				}
			}
			return null;
		}

		/// <summary>Creates the child controls that make up the <see cref="T:System.Web.UI.WebControls.LoginView" /> control.</summary>
		// Token: 0x0600289D RID: 10397 RVA: 0x000680DC File Offset: 0x000662DC
		protected internal override void CreateChildControls()
		{
			this.Controls.Clear();
			Control control = new Control();
			ITemplate template = null;
			if (this.Page != null && this.Page.Request.IsAuthenticated)
			{
				this.isAuthenticated = true;
				HttpContext httpContext = HttpContext.Current;
				IPrincipal principal = ((httpContext != null) ? httpContext.User : null);
				RoleGroupCollection roleGroups;
				if (Roles.Enabled && (roleGroups = this.RoleGroups) != null && roleGroups.Count > 0)
				{
					foreach (object obj in roleGroups)
					{
						RoleGroup roleGroup = (RoleGroup)obj;
						template = this.GetTemplateFromRoleGroup(roleGroup, principal);
						if (template != null)
						{
							break;
						}
					}
				}
				if (template == null)
				{
					template = this.LoggedInTemplate;
				}
			}
			else
			{
				this.isAuthenticated = false;
				template = this.AnonymousTemplate;
			}
			if (template != null)
			{
				template.InstantiateIn(control);
			}
			this.Controls.Add(control);
		}

		/// <summary>Binds a data source to <see cref="T:System.Web.UI.WebControls.LoginView" /> and all its child controls.</summary>
		// Token: 0x0600289E RID: 10398 RVA: 0x000681DC File Offset: 0x000663DC
		public override void DataBind()
		{
			EventArgs empty = EventArgs.Empty;
			this.OnDataBinding(empty);
			this.EnsureChildControls();
			this.DataBindChildren();
		}

		/// <summary>Sets input focus to a control.</summary>
		/// <exception cref="T:System.NotSupportedException">You call the <see cref="M:System.Web.UI.WebControls.LoginView.Focus" /> method.</exception>
		// Token: 0x0600289F RID: 10399 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>This method implements <see cref="M:System.Web.UI.Control.LoadControlState(System.Object)" />.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
		// Token: 0x060028A0 RID: 10400 RVA: 0x00068202 File Offset: 0x00066402
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			this.isAuthenticated = (bool)savedState;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060028A1 RID: 10401 RVA: 0x00068214 File Offset: 0x00066414
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				this.Page.RegisterRequiresControlState(this);
			}
		}

		/// <summary>Determines which role-group template to display, based on the roles of the logged-in user.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060028A2 RID: 10402 RVA: 0x00068231 File Offset: 0x00066431
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.Page != null)
			{
				this.IsAuthenticated = this.Page.Request.IsAuthenticated;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginView.ViewChanged" /> event after the <see cref="T:System.Web.UI.WebControls.LoginView" /> control switches views.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060028A3 RID: 10403 RVA: 0x00068258 File Offset: 0x00066458
		protected virtual void OnViewChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginView.viewChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.LoginView.ViewChanging" /> event before the <see cref="T:System.Web.UI.WebControls.LoginView" /> control switches views.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060028A4 RID: 10404 RVA: 0x00068288 File Offset: 0x00066488
		protected virtual void OnViewChanging(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LoginView.viewChangingEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the Web server control content to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> used to render the server control content on the client's browser.</param>
		// Token: 0x060028A5 RID: 10405 RVA: 0x000682B6 File Offset: 0x000664B6
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.EnsureChildControls();
			base.Render(writer);
		}

		/// <summary>This method implements <see cref="M:System.Web.UI.Control.SaveControlState" />.</summary>
		// Token: 0x060028A6 RID: 10406 RVA: 0x000682C5 File Offset: 0x000664C5
		protected internal override object SaveControlState()
		{
			if (this.isAuthenticated)
			{
				return this.isAuthenticated;
			}
			return null;
		}

		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> object containing the state of the <see cref="T:System.Web.UI.WebControls.LoginView" /> control. </param>
		// Token: 0x060028A7 RID: 10407 RVA: 0x0004FA84 File Offset: 0x0004DC84
		[global::System.MonoTODO("for design-time usage - no more details available")]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override void SetDesignModeState(IDictionary data)
		{
			base.SetDesignModeState(data);
		}

		/// <summary>Occurs after the view is changed.</summary>
		// Token: 0x140000AE RID: 174
		// (add) Token: 0x060028A8 RID: 10408 RVA: 0x000682DC File Offset: 0x000664DC
		// (remove) Token: 0x060028A9 RID: 10409 RVA: 0x000682EF File Offset: 0x000664EF
		public event EventHandler ViewChanged
		{
			add
			{
				base.Events.AddHandler(LoginView.viewChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginView.viewChangedEvent, value);
			}
		}

		/// <summary>Occurs before the view is changed.</summary>
		// Token: 0x140000AF RID: 175
		// (add) Token: 0x060028AA RID: 10410 RVA: 0x00068302 File Offset: 0x00066502
		// (remove) Token: 0x060028AB RID: 10411 RVA: 0x00068315 File Offset: 0x00066515
		public event EventHandler ViewChanging
		{
			add
			{
				base.Events.AddHandler(LoginView.viewChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LoginView.viewChangingEvent, value);
			}
		}

		// Token: 0x04001A86 RID: 6790
		private static readonly object viewChangedEvent = new object();

		// Token: 0x04001A87 RID: 6791
		private static readonly object viewChangingEvent = new object();

		// Token: 0x04001A88 RID: 6792
		private ITemplate anonymousTemplate;

		// Token: 0x04001A89 RID: 6793
		private ITemplate loggedInTemplate;

		// Token: 0x04001A8A RID: 6794
		private bool isAuthenticated;

		// Token: 0x04001A8B RID: 6795
		private bool theming;

		// Token: 0x04001A8C RID: 6796
		private RoleGroupCollection coll;
	}
}
