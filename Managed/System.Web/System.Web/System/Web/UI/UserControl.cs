using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.Caching;
using System.Web.ModelBinding;
using System.Web.SessionState;
using Unity;

namespace System.Web.UI
{
	/// <summary>Represents an .ascx file, also known as a user control, requested from a server that hosts an ASP.NET Web application. The file must be called from a Web Forms page or a parser error will occur.</summary>
	// Token: 0x0200023C RID: 572
	[DesignerCategory("ASPXCodeBehind")]
	[ToolboxItem(false)]
	[Designer("System.Web.UI.Design.UserControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	[ParseChildren(true)]
	[Designer("Microsoft.VisualStudio.Web.WebForms.WebFormDesigner, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[ControlBuilder(typeof(UserControlControlBuilder))]
	[DefaultEvent("Load")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class UserControl : TemplateControl, IAttributeAccessor, IUserControlDesignerAccessor, INamingContainer, IFilterResolutionService, INonBindingContainer
	{
		/// <summary>Gets the <see cref="P:System.Web.HttpContext.Application" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpApplicationState" /> object for the current Web request.</returns>
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x0003DD30 File Offset: 0x0003BF30
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpApplicationState Application
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Application;
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0003DD4F File Offset: 0x0003BF4F
		private void EnsureAttributes()
		{
			if (this.attributes == null)
			{
				this.attrBag = new StateBag(true);
				if (base.IsTrackingViewState)
				{
					this.attrBag.TrackViewState();
				}
				this.attributes = new AttributeCollection(this.attrBag);
			}
		}

		/// <summary>Gets a collection of all attribute name and value pairs declared in the user control tag within the .aspx file.</summary>
		/// <returns>An <see cref="T:System.Web.UI.AttributeCollection" /> object that contains all the name and value pairs declared in the user control tag.</returns>
		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001780 RID: 6016 RVA: 0x0003DD89 File Offset: 0x0003BF89
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public AttributeCollection Attributes
		{
			get
			{
				this.EnsureAttributes();
				return this.attributes;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Caching.Cache" /> object that is associated with the application that contains the user control.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.Cache" /> object in which to store the user control's data.</returns>
		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0003DD98 File Offset: 0x0003BF98
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Cache Cache
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Cache;
			}
		}

		/// <summary>Gets a reference to a collection of caching parameters for this user control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCachePolicy" /> containing properties that define the caching parameters for this <see cref="T:System.Web.UI.UserControl" />.</returns>
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0003DDB8 File Offset: 0x0003BFB8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ControlCachePolicy CachePolicy
		{
			get
			{
				BasePartialCachingControl basePartialCachingControl = this.Parent as BasePartialCachingControl;
				if (basePartialCachingControl != null)
				{
					return basePartialCachingControl.CachePolicy;
				}
				if (this.cachePolicy == null)
				{
					this.cachePolicy = new ControlCachePolicy();
				}
				return this.cachePolicy;
			}
		}

		/// <summary>Gets a value indicating whether the user control is being loaded in response to a client postback, or if it is being loaded and accessed for the first time.</summary>
		/// <returns>true if the user control is being loaded in response to a client postback; otherwise, false.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x0003DDF4 File Offset: 0x0003BFF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsPostBack
		{
			get
			{
				Page page = this.Page;
				return page != null && page.IsPostBack;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpRequest" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpRequest" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0003DE14 File Offset: 0x0003C014
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpRequest Request
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpResponse" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpResponse" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0003DE34 File Offset: 0x0003C034
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public HttpResponse Response
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Response;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpServerUtility" /> object for the current Web request.</summary>
		/// <returns>The <see cref="T:System.Web.HttpServerUtility" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0003DE54 File Offset: 0x0003C054
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpServerUtility Server
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Server;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current Web request.</summary>
		/// <returns>An <see cref="T:System.Web.SessionState.HttpSessionState" /> object associated with the <see cref="T:System.Web.UI.Page" /> that contains the <see cref="T:System.Web.UI.UserControl" /> instance.</returns>
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x0003DE74 File Offset: 0x0003C074
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpSessionState Session
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Session;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</summary>
		/// <returns>The data from the <see cref="T:System.Web.TraceContext" /> object for the current Web request.</returns>
		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0003DE94 File Offset: 0x0003C094
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TraceContext Trace
		{
			get
			{
				Page page = this.Page;
				if (page == null)
				{
					return null;
				}
				return page.Trace;
			}
		}

		/// <summary>Performs any initialization steps on the user control that are required by RAD designers.</summary>
		// Token: 0x06001789 RID: 6025 RVA: 0x00034E57 File Offset: 0x00033057
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void DesignerInitialize()
		{
			this.InitRecursive(null);
		}

		/// <summary>Initializes the <see cref="T:System.Web.UI.UserControl" /> object that has been created declaratively. Since there are some differences between pages and user controls, this method makes sure that the user control is initialized properly.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> object that contains the user control. </param>
		// Token: 0x0600178A RID: 6026 RVA: 0x0003DEB3 File Offset: 0x0003C0B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeAsUserControl(Page page)
		{
			if (this.initialized)
			{
				return;
			}
			this.Page = page;
			this.InitializeAsUserControlInternal();
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0003DECB File Offset: 0x0003C0CB
		internal void InitializeAsUserControlInternal()
		{
			if (this.initialized)
			{
				return;
			}
			this.initialized = true;
			base.WireupAutomaticEvents();
			this.FrameworkInitialize();
		}

		/// <summary>Assigns a virtual file path, either absolute or relative, to a physical file path.</summary>
		/// <returns>The physical path to the file.</returns>
		/// <param name="virtualPath">The virtual file path to map. </param>
		// Token: 0x0600178C RID: 6028 RVA: 0x0003DEE9 File Offset: 0x0003C0E9
		public string MapPath(string virtualPath)
		{
			return this.Request.MapPath(virtualPath, this.TemplateSourceDirectory, true);
		}

		/// <summary>Restores the view-state information from a previous user control request that was saved by the <see cref="M:System.Web.UI.UserControl.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the user control state to be restored. </param>
		// Token: 0x0600178D RID: 6029 RVA: 0x0003DF00 File Offset: 0x0003C100
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair)savedState;
				base.LoadViewState(pair.First);
				if (pair.Second != null)
				{
					this.EnsureAttributes();
					this.attrBag.LoadViewState(pair.Second);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x0600178E RID: 6030 RVA: 0x0003DF42 File Offset: 0x0003C142
		protected internal override void OnInit(EventArgs e)
		{
			this.InitializeAsUserControl(this.Page);
			base.OnInit(e);
		}

		/// <summary>Saves any user control view-state changes that have occurred since the last page postback.</summary>
		/// <returns>Returns the user control's current view state. If there is no view state associated with the control, it returns null.</returns>
		// Token: 0x0600178F RID: 6031 RVA: 0x0003DF58 File Offset: 0x0003C158
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = null;
			if (this.attributes != null)
			{
				obj2 = this.attrBag.SaveViewState();
			}
			if (obj == null && obj2 == null)
			{
				return null;
			}
			return new Pair(obj, obj2);
		}

		/// <summary>Returns the value of the specified user control attribute.</summary>
		/// <returns>The value of the specified user control attribute.</returns>
		/// <param name="name">The name of the attribute to get the value of.</param>
		// Token: 0x06001790 RID: 6032 RVA: 0x0003DF91 File Offset: 0x0003C191
		string IAttributeAccessor.GetAttribute(string name)
		{
			if (this.attributes == null)
			{
				return null;
			}
			return this.attributes[name];
		}

		/// <summary>Sets the value of the specified user control attribute.</summary>
		/// <param name="name">The name of the attribute to set.</param>
		/// <param name="value">The value of the attribute to set.</param>
		// Token: 0x06001791 RID: 6033 RVA: 0x0003DFA9 File Offset: 0x0003C1A9
		void IAttributeAccessor.SetAttribute(string name, string value)
		{
			this.EnsureAttributes();
			this.Attributes[name] = value;
		}

		/// <summary>Gets or sets the text that appears between the opening and closing tags of a user control.</summary>
		/// <returns>The text that appears between the opening and closing tabs of a user control.</returns>
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0003DFC0 File Offset: 0x0003C1C0
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0003DFED File Offset: 0x0003C1ED
		string IUserControlDesignerAccessor.InnerText
		{
			get
			{
				string text = (string)this.ViewState["!DesignTimeInnerText"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["!DesignTimeInnerText"] = value;
			}
		}

		/// <summary>Gets or sets the full tag name of the user control.</summary>
		/// <returns>The full tag name of the user control.</returns>
		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0003E000 File Offset: 0x0003C200
		// (set) Token: 0x06001795 RID: 6037 RVA: 0x0003E02D File Offset: 0x0003C22D
		string IUserControlDesignerAccessor.TagName
		{
			get
			{
				string text = (string)this.ViewState["!DesignTimeTagName"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["!DesignTimeTagName"] = value;
			}
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		int IFilterResolutionService.CompareFilters(string filter1, string filter2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		bool IFilterResolutionService.EvaluateFilter(string filterName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Attempts to update the model instance by using the values from the data-bound control.</summary>
		/// <returns>true if the model instance was updated successfully; otherwise, false.</returns>
		/// <param name="model">The model instance to update.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		// Token: 0x06001798 RID: 6040 RVA: 0x0003E040 File Offset: 0x0003C240
		public virtual bool TryUpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Attempts to update the model instance using values from the value provider.</summary>
		/// <returns>true if the model instance was updated successfully; otherwise, false.</returns>
		/// <param name="model">The model instance to update.</param>
		/// <param name="valueProvider">A dictionary of values to use to update the model.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		// Token: 0x06001799 RID: 6041 RVA: 0x0003E05C File Offset: 0x0003C25C
		public virtual bool TryUpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Updates the model instance by using values from the data-bound control.</summary>
		/// <param name="model">The model instance to update.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		/// <exception cref="System.InvalidOperationException">The model instance was not updated successfully.</exception>
		// Token: 0x0600179A RID: 6042 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void UpdateModel<TModel>(TModel model)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Updates the specified model instance using values from the value provider of the user control.</summary>
		/// <param name="model">The model instance to update.</param>
		/// <param name="valueProvider">A dictionary of values to use to update the model.</param>
		/// <typeparam name="TModel">The type of the model object.</typeparam>
		/// <exception cref="System.InvalidOperationException">The model instance was not updated successfully.</exception>
		// Token: 0x0600179B RID: 6043 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void UpdateModel<TModel>(TModel model, IValueProvider valueProvider)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015F1 RID: 5617
		private ControlCachePolicy cachePolicy;

		// Token: 0x040015F2 RID: 5618
		private bool initialized;

		// Token: 0x040015F3 RID: 5619
		private AttributeCollection attributes;

		// Token: 0x040015F4 RID: 5620
		private StateBag attrBag;
	}
}
