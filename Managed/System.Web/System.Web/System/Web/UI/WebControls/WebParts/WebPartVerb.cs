using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides an interactive user interface (UI) element that enables users to perform actions on a Web Parts page.</summary>
	// Token: 0x02000489 RID: 1161
	[TypeConverter("System.Web.UI.WebControls.WebParts.WebPartVerbConverter, System.Web")]
	public class WebPartVerb : IStateManager
	{
		/// <summary>Gets a string containing a unique ID for a verb.</summary>
		/// <returns>A string containing the ID for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" />.</returns>
		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x060034A3 RID: 13475 RVA: 0x00088902 File Offset: 0x00086B02
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates a client-side click event handler with the instance.</summary>
		/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
		/// <param name="clientClickHandler">A <see cref="T:System.String" /> that refers to the client-side handler for click events.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="clientClickHandler" /> parameter is null.</exception>
		// Token: 0x060034A4 RID: 13476 RVA: 0x0008890C File Offset: 0x00086B0C
		public WebPartVerb(string id, string clientClickHandler)
		{
			this.id = id;
			this.clientClickHandler = clientClickHandler;
			this.stateBag = new StateBag();
			this.stateBag.Add("clientClickHandler", clientClickHandler);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates a server-side click event handler with the instance.</summary>
		/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
		/// <param name="serverClickHandler">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles click events on the server.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="serverClickHandler" /> parameter is null.</exception>
		// Token: 0x060034A5 RID: 13477 RVA: 0x0008897C File Offset: 0x00086B7C
		public WebPartVerb(string id, WebPartEventHandler serverClickHandler)
		{
			this.id = id;
			this.serverClickHandler = serverClickHandler;
			this.stateBag = new StateBag();
			this.stateBag.Add("serverClickHandler", serverClickHandler);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class and associates both client and server-side click event handlers with the instance.</summary>
		/// <param name="id">A <see cref="T:System.String" /> that is the unique identifier for a verb.</param>
		/// <param name="serverClickHandler">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles click events on the server.</param>
		/// <param name="clientClickHandler">A <see cref="T:System.String" /> that refers to the client-side handler for click events.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="clientClickHandler" /> parameter is null.- or -The <paramref name="serverClickHandler" /> parameter is null.</exception>
		// Token: 0x060034A6 RID: 13478 RVA: 0x000889EC File Offset: 0x00086BEC
		public WebPartVerb(string id, WebPartEventHandler serverClickHandler, string clientClickHandler)
		{
			this.id = id;
			this.serverClickHandler = serverClickHandler;
			this.clientClickHandler = clientClickHandler;
			this.stateBag = new StateBag();
			this.stateBag.Add("serverClickHandler", serverClickHandler);
			this.stateBag.Add("clientClickHandler", clientClickHandler);
		}

		/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the view state to be restored.</param>
		// Token: 0x060034A7 RID: 13479 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		protected virtual void LoadViewState(object savedState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object's view-state changes that occurred since the page was last posted back to the server.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the state data to be saved.</returns>
		// Token: 0x060034A8 RID: 13480 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		protected virtual object SaveViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Tracks view-state changes to a verb so the changes can be stored in the verb's <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x060034A9 RID: 13481 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		protected virtual void TrackViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" /> method of the <see cref="T:System.Web.UI.IStateManager" /> interface by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.LoadViewState(System.Object)" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the view state to be restored. </param>
		// Token: 0x060034AA RID: 13482 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		void IStateManager.LoadViewState(object savedState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.SaveViewState" /> method by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.SaveViewState" /> method.</summary>
		/// <returns>Returns an <see cref="T:System.Object" /> containing the control's current view state. If no view state is associated with the control, this method returns null.</returns>
		// Token: 0x060034AB RID: 13483 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		object IStateManager.SaveViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IStateManager.TrackViewState" /> method by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="M:System.Web.UI.WebControls.WebParts.WebPartVerb.TrackViewState" /> method.</summary>
		// Token: 0x060034AC RID: 13484 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		void IStateManager.TrackViewState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Implements the <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" /> property by calling the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> class's own <see cref="P:System.Web.UI.WebControls.WebParts.WebPartVerb.IsTrackingViewState" /> property.</summary>
		/// <returns>true if view state is being tracked for a verb; otherwise, false.</returns>
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value indicating that some state associated with a custom verb is currently active or selected.</summary>
		/// <returns>true if a state associated with a custom verb is currently active; otherwise, false. The default is false.</returns>
		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x060034AE RID: 13486 RVA: 0x00088A72 File Offset: 0x00086C72
		// (set) Token: 0x060034AF RID: 13487 RVA: 0x00088A7A File Offset: 0x00086C7A
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[WebSysDescription("Denotes verb is checked or not.")]
		public virtual bool Checked
		{
			get
			{
				return this.isChecked;
			}
			set
			{
				this.isChecked = value;
			}
		}

		/// <summary>Gets the string containing the method name of the client-side event handler defined in the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> constructor.</summary>
		/// <returns>A string that contains the name of the method that handles client-side click events. The default is an empty string ("").</returns>
		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x060034B0 RID: 13488 RVA: 0x00088A83 File Offset: 0x00086C83
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ClientClickHandler
		{
			get
			{
				return this.clientClickHandler;
			}
		}

		/// <summary>Gets or sets a short description of the verb.</summary>
		/// <returns>A string containing a description of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" />. </returns>
		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x060034B1 RID: 13489 RVA: 0x00088A8B File Offset: 0x00086C8B
		// (set) Token: 0x060034B2 RID: 13490 RVA: 0x00088A93 File Offset: 0x00086C93
		[Localizable(true)]
		[WebSysDescription("Gives descriptive information about the verb")]
		[NotifyParentProperty(true)]
		public virtual string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a verb is enabled.</summary>
		/// <returns>true if the verb is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x00088A9C File Offset: 0x00086C9C
		// (set) Token: 0x060034B4 RID: 13492 RVA: 0x00088AA4 File Offset: 0x00086CA4
		[NotifyParentProperty(true)]
		[WebSysDescription("Determines whether verb is enabled.")]
		[DefaultValue(true)]
		public virtual bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets a string containing a URL to an image that represents a verb in the user interface (UI).</summary>
		/// <returns>A string that contains the URL to an image. The default is an empty string ("").</returns>
		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x060034B5 RID: 13493 RVA: 0x00088AAD File Offset: 0x00086CAD
		// (set) Token: 0x060034B6 RID: 13494 RVA: 0x00088AB5 File Offset: 0x00086CB5
		[WebSysDescription("Denotes URL of the image to be displayed for the verb")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design", "UITypeEditor, System.Drawing")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ImageUrl
		{
			get
			{
				return this.imageUrl;
			}
			set
			{
				this.imageUrl = value;
			}
		}

		/// <summary>Gets a value that indicates whether view state is currently being tracked for a verb.</summary>
		/// <returns>true if view state is being tracked; otherwise, false.</returns>
		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x00003A2B File Offset: 0x00001C2B
		protected virtual bool IsTrackingViewState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a reference to the method that handles server-side click events for the verb.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventHandler" /> that handles server-side click events.</returns>
		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x060034B8 RID: 13496 RVA: 0x00088ABE File Offset: 0x00086CBE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public WebPartEventHandler ServerClickHandler
		{
			get
			{
				return this.serverClickHandler;
			}
		}

		/// <summary>Gets or sets the text label for a verb that is displayed in the user interface (UI).</summary>
		/// <returns>A string containing the text label for a verb. The default is an empty string ("").</returns>
		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x00088AC6 File Offset: 0x00086CC6
		// (set) Token: 0x060034BA RID: 13498 RVA: 0x00088ACE File Offset: 0x00086CCE
		[WebSysDescription("Denotes text to be displayed for the verb")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a server control across multiple requests for the same page.</summary>
		/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the server control's view-state information.</returns>
		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x00088AD7 File Offset: 0x00086CD7
		protected StateBag ViewState
		{
			get
			{
				return this.stateBag;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a verb is visible to users.</summary>
		/// <returns>true if the verb is visible; otherwise, false. The default is true.</returns>
		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x00088ADF File Offset: 0x00086CDF
		// (set) Token: 0x060034BD RID: 13501 RVA: 0x00088AE7 File Offset: 0x00086CE7
		[DefaultValue(true)]
		[WebSysDescription("Denotes whether the verb is visible")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				this.visible = value;
			}
		}

		// Token: 0x04001D32 RID: 7474
		private string clientClickHandler;

		// Token: 0x04001D33 RID: 7475
		private WebPartEventHandler serverClickHandler;

		// Token: 0x04001D34 RID: 7476
		private StateBag stateBag;

		// Token: 0x04001D35 RID: 7477
		private bool isChecked;

		// Token: 0x04001D36 RID: 7478
		private string description = string.Empty;

		// Token: 0x04001D37 RID: 7479
		private bool enabled = true;

		// Token: 0x04001D38 RID: 7480
		private string imageUrl = string.Empty;

		// Token: 0x04001D39 RID: 7481
		private string text = string.Empty;

		// Token: 0x04001D3A RID: 7482
		private bool visible = true;

		// Token: 0x04001D3B RID: 7483
		private string id;
	}
}
