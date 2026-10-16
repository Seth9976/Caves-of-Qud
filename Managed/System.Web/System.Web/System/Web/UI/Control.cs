using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Routing;
using System.Web.UI.Adapters;
using System.Web.UI.WebControls;
using System.Web.Util;
using Unity;

namespace System.Web.UI
{
	/// <summary>Defines the properties, methods, and events that are shared by all ASP.NET server controls.</summary>
	// Token: 0x020001B0 RID: 432
	[DesignerCategory("Code")]
	[Themeable(false)]
	[DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.ControlCodeDomSerializer, Microsoft.VisualStudio.Web, Version=0.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Bindable(true)]
	[ToolboxItem("System.Web.UI.Design.WebControlToolboxItem, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Require)]
	[DefaultProperty("ID")]
	[Designer("System.Web.UI.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Control : IComponent, IDisposable, IParserAccessor, IDataBindingsAccessor, IUrlResolutionService, IControlBuilderAccessor, IControlDesignerAccessor, IExpressionsAccessor
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x0002B8B8 File Offset: 0x00029AB8
		static Control()
		{
			Control.DataBindingEvent = new object();
			Control.DisposedEvent = new object();
			Control.InitEvent = new object();
			Control.LoadEvent = new object();
			Control.PreRenderEvent = new object();
			Control.UnloadEvent = new object();
			Control.defaultNameArray = new string[100];
			for (int i = 0; i < 100; i++)
			{
				Control.defaultNameArray[i] = "ctl" + i.ToString("D2");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Control" /> class.</summary>
		// Token: 0x0600108C RID: 4236 RVA: 0x0002B937 File Offset: 0x00029B37
		public Control()
		{
			this.stateMask = 55;
			if (this is INamingContainer)
			{
				this.stateMask |= 64;
			}
			this.viewStateMode = ViewStateMode.Inherit;
		}

		/// <summary>Gets the browser-specific adapter for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> for this control. If the target browser does not require an adapter, returns null.</returns>
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x0002B977 File Offset: 0x00029B77
		protected internal ControlAdapter Adapter
		{
			get
			{
				if (!this.did_adapter_lookup)
				{
					this.adapter = this.ResolveAdapter();
					if (this.adapter != null)
					{
						this.adapter.control = this;
					}
					this.did_adapter_lookup = true;
				}
				return this.adapter;
			}
		}

		/// <summary>Gets or sets the application-relative virtual directory of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> object that contains this control.</summary>
		/// <returns>The application-relative virtual directory of the page or user control that contains this control.</returns>
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x0002BA0C File Offset: 0x00029C0C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AppRelativeTemplateSourceDirectory
		{
			get
			{
				if (this._appRelativeTemplateSourceDirectory != null)
				{
					return this._appRelativeTemplateSourceDirectory;
				}
				string text = null;
				TemplateControl templateControl = this.TemplateControl;
				if (templateControl != null)
				{
					string appRelativeVirtualPath = templateControl.AppRelativeVirtualPath;
					if (!string.IsNullOrEmpty(appRelativeVirtualPath))
					{
						text = VirtualPathUtility.GetDirectory(appRelativeVirtualPath, false);
					}
				}
				this._appRelativeTemplateSourceDirectory = ((text != null) ? text : VirtualPathUtility.ToAppRelative(this.TemplateSourceDirectory));
				return this._appRelativeTemplateSourceDirectory;
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			set
			{
				this._appRelativeTemplateSourceDirectory = value;
				this._templateSourceDirectory = null;
			}
		}

		/// <summary>Gets the control that contains this control's data binding.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> that contains this control's data binding.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0002BA1C File Offset: 0x00029C1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Control BindingContainer
		{
			get
			{
				Control control = this.NamingContainer;
				if ((control != null && control is INonBindingContainer) || (this.stateMask & 16) == 0)
				{
					control = control.BindingContainer;
				}
				return control;
			}
		}

		/// <summary>Gets the control ID for HTML markup that is generated by ASP.NET.</summary>
		/// <returns>The control ID for HTML markup that is generated by ASP.NET.</returns>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x0002BA4E File Offset: 0x00029C4E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("An Identification of the control that is rendered.")]
		public virtual string ClientID
		{
			get
			{
				if (this.clientID != null)
				{
					return this.clientID;
				}
				this.clientID = this.GetClientID();
				this.stateMask |= 1024;
				return this.clientID;
			}
		}

		/// <summary>Gets a value that specifies the ASP.NET version that rendered HTML will be compatible with.</summary>
		/// <returns>The ASP.NET version that rendered HTML will be compatible with.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x0002BA84 File Offset: 0x00029C84
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x0002BACD File Offset: 0x00029CCD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[Browsable(false)]
		public virtual Version RenderingCompatibility
		{
			get
			{
				if (this.renderingCompatibility == null)
				{
					PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
					this.renderingCompatibility = ((pagesSection != null) ? pagesSection.ControlRenderingCompatibilityVersion : new Version(4, 0));
				}
				return this.renderingCompatibility;
			}
			set
			{
				this.renderingCompatibility = value;
				this.renderingCompatibilityOld = null;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0002BAE2 File Offset: 0x00029CE2
		internal bool RenderingCompatibilityLessThan40
		{
			get
			{
				if (this.renderingCompatibilityOld == null)
				{
					this.renderingCompatibilityOld = new bool?(this.RenderingCompatibility < new Version(4, 0));
				}
				return this.renderingCompatibilityOld.Value;
			}
		}

		/// <summary>Gets a reference to the naming container if the naming container implements <see cref="T:System.Web.UI.IDataItemContainer" />.</summary>
		/// <returns>The naming container. In a hierarchy of naming containers that implement <see cref="T:System.Web.UI.IDataItemContainer" />, this property returns the naming container at the top of the hierarchy, or null if the current <see cref="T:System.Web.UI.Control" /> object is not in a naming container that implements <see cref="T:System.Web.UI.IDataItemContainer" />.</returns>
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0002BB1C File Offset: 0x00029D1C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Control DataItemContainer
		{
			get
			{
				Control namingContainer = this.NamingContainer;
				if (namingContainer == null)
				{
					return null;
				}
				if (namingContainer is IDataItemContainer)
				{
					return namingContainer;
				}
				return namingContainer.DataItemContainer;
			}
		}

		/// <summary>Gets a reference to the naming container if the naming container implements <see cref="T:System.Web.UI.IDataKeysControl" />.</summary>
		/// <returns>The naming container. In a hierarchy of naming containers that implement <see cref="T:System.Web.UI.IDataKeysControl" />, the property returns the naming container at the top of the hierarchy, or null if the current <see cref="T:System.Web.UI.Control" /> object is not in a naming container that implements <see cref="T:System.Web.UI.IDataKeysControl" />.</returns>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0002BB48 File Offset: 0x00029D48
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[Bindable(false)]
		public Control DataKeysContainer
		{
			get
			{
				Control namingContainer = this.NamingContainer;
				if (namingContainer == null)
				{
					return null;
				}
				if (namingContainer is IDataKeysControl)
				{
					return namingContainer;
				}
				return namingContainer.DataKeysContainer;
			}
		}

		/// <summary>Gets or sets the algorithm that is used to generate the value of the <see cref="P:System.Web.UI.Control.ClientID" /> property.</summary>
		/// <returns>A value that indicates how the <see cref="P:System.Web.UI.Control.ClientID" /> property is generated. The default is <see cref="F:System.Web.UI.ClientIDMode.Inherit" />.</returns>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0002BB71 File Offset: 0x00029D71
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x0002BB8D File Offset: 0x00029D8D
		[DefaultValue(ClientIDMode.Inherit)]
		[Themeable(false)]
		public virtual ClientIDMode ClientIDMode
		{
			get
			{
				if (this.clientIDMode == null)
				{
					return ClientIDMode.Inherit;
				}
				return this.clientIDMode.Value;
			}
			set
			{
				if (this.clientIDMode == null || this.clientIDMode.Value != value)
				{
					this.ClearCachedClientID();
					this.ClearEffectiveClientIDMode();
					this.clientIDMode = new ClientIDMode?(value);
				}
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0002BBC4 File Offset: 0x00029DC4
		internal ClientIDMode EffectiveClientIDMode
		{
			get
			{
				if (this.effectiveClientIDMode != null)
				{
					return this.effectiveClientIDMode.Value;
				}
				ClientIDMode clientIDMode = this.ClientIDMode;
				if (clientIDMode != ClientIDMode.Inherit)
				{
					this.effectiveClientIDMode = new ClientIDMode?(clientIDMode);
					return clientIDMode;
				}
				Control namingContainer = this.NamingContainer;
				if (namingContainer != null)
				{
					this.effectiveClientIDMode = new ClientIDMode?(namingContainer.EffectiveClientIDMode);
					return this.effectiveClientIDMode.Value;
				}
				PagesSection pagesSection = WebConfigurationManager.GetSection("system.web/pages") as PagesSection;
				this.effectiveClientIDMode = new ClientIDMode?(pagesSection.ClientIDMode);
				return this.effectiveClientIDMode.Value;
			}
		}

		/// <summary>Sets the cached <see cref="P:System.Web.UI.Control.ClientID" /> value to null.</summary>
		// Token: 0x0600109A RID: 4250 RVA: 0x0002BC58 File Offset: 0x00029E58
		protected void ClearCachedClientID()
		{
			this.clientID = null;
			if (!this.HasControls())
			{
				return;
			}
			for (int i = 0; i < this._controls.Count; i++)
			{
				this._controls[i].ClearCachedClientID();
			}
		}

		/// <summary>Sets the <see cref="P:System.Web.UI.Control.ClientIDMode" /> property of the current control instance and of any child controls to <see cref="F:System.Web.UI.ClientIDMode.Inherit" />.</summary>
		// Token: 0x0600109B RID: 4251 RVA: 0x0002BC9C File Offset: 0x00029E9C
		protected void ClearEffectiveClientIDMode()
		{
			this.effectiveClientIDMode = null;
			if (!this.HasControls())
			{
				return;
			}
			for (int i = 0; i < this._controls.Count; i++)
			{
				this._controls[i].ClearEffectiveClientIDMode();
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0002BCE8 File Offset: 0x00029EE8
		private string GetClientID()
		{
			switch (this.EffectiveClientIDMode)
			{
			case ClientIDMode.AutoID:
				return this.UniqueID2ClientID(this.UniqueID);
			case ClientIDMode.Predictable:
				this.EnsureID();
				return this.GeneratePredictableClientID();
			case ClientIDMode.Static:
				this.EnsureID();
				return this.ID;
			default:
				throw new InvalidOperationException("Unsupported ClientIDMode value.");
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0002BD44 File Offset: 0x00029F44
		private string GeneratePredictableClientID()
		{
			string text = this.ID;
			bool flag = !string.IsNullOrEmpty(text);
			char clientIDSeparator = this.ClientIDSeparator;
			StringBuilder stringBuilder = new StringBuilder();
			Control namingContainer = this.NamingContainer;
			if (this is INamingContainer && !flag)
			{
				if (namingContainer != null)
				{
					this.EnsureIDInternal();
				}
				text = this._userId;
			}
			if (namingContainer != null && namingContainer != this.Page)
			{
				if (!string.IsNullOrEmpty(namingContainer.ID))
				{
					stringBuilder.Append(namingContainer.GetClientID());
					stringBuilder.Append(clientIDSeparator);
				}
				else
				{
					stringBuilder.Append(namingContainer.GeneratePredictableClientID());
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(clientIDSeparator);
					}
				}
			}
			if (!flag)
			{
				if (this is INamingContainer || !this.AutoID)
				{
					stringBuilder.Append(text);
				}
				else
				{
					int length = stringBuilder.Length;
					if (length > 0 && stringBuilder[length - 1] == clientIDSeparator)
					{
						stringBuilder.Length = length - 1;
					}
				}
				return stringBuilder.ToString();
			}
			stringBuilder.Append(text);
			IDataItemContainer dataItemContainer = this.DataItemContainer as IDataItemContainer;
			if (dataItemContainer == null)
			{
				return stringBuilder.ToString();
			}
			IDataKeysControl dataKeysControl = this.DataKeysContainer as IDataKeysControl;
			this.GetDataBoundControlFieldValue(stringBuilder, clientIDSeparator, dataItemContainer, dataKeysControl);
			return stringBuilder.ToString();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0002BE70 File Offset: 0x0002A070
		private void GetDataBoundControlFieldValue(StringBuilder sb, char separator, IDataItemContainer dataItemContainer, IDataKeysControl dataKeysContainer)
		{
			if (dataItemContainer is IDataBoundItemControl)
			{
				return;
			}
			int displayIndex = dataItemContainer.DisplayIndex;
			if (dataKeysContainer == null)
			{
				if (displayIndex >= 0)
				{
					sb.Append(separator);
					sb.Append(displayIndex);
				}
				return;
			}
			string[] clientIDRowSuffix = dataKeysContainer.ClientIDRowSuffix;
			DataKeyArray clientIDRowSuffixDataKeys = dataKeysContainer.ClientIDRowSuffixDataKeys;
			if (clientIDRowSuffixDataKeys == null || clientIDRowSuffix == null || clientIDRowSuffix.Length == 0)
			{
				sb.Append(separator);
				sb.Append(displayIndex);
				return;
			}
			DataKey dataKey = clientIDRowSuffixDataKeys[displayIndex];
			foreach (string text in clientIDRowSuffix)
			{
				sb.Append(separator);
				object obj = ((dataKey != null) ? dataKey[text] : null);
				if (obj != null)
				{
					sb.Append(obj.ToString());
				}
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0002BF20 File Offset: 0x0002A120
		internal string UniqueID2ClientID(string uniqueId)
		{
			if (string.IsNullOrEmpty(uniqueId))
			{
				return null;
			}
			return uniqueId.Replace(this.IdSeparator, this.ClientIDSeparator);
		}

		/// <summary>Gets a character value representing the separator character used in the <see cref="P:System.Web.UI.Control.ClientID" /> property.</summary>
		/// <returns>Always returns the underscore character (_).</returns>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0002BF3E File Offset: 0x0002A13E
		protected char ClientIDSeparator
		{
			get
			{
				return '_';
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that represents the child controls for a specified server control in the UI hierarchy.</summary>
		/// <returns>The collection of child controls for the specified server control.</returns>
		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0002BF42 File Offset: 0x0002A142
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("The child controls of this control.")]
		public virtual ControlCollection Controls
		{
			get
			{
				if (this._controls == null)
				{
					this._controls = this.CreateControlCollection();
				}
				return this._controls;
			}
		}

		/// <summary>Gets a value indicating whether a control is being used on a design surface.</summary>
		/// <returns>true if the control is being used in a designer; otherwise, false.</returns>
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00008A4D File Offset: 0x00006C4D
		[global::System.MonoTODO("revisit once we have a real design strategy")]
		protected internal bool DesignMode
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the server control persists its view state, and the view state of any child controls it contains, to the requesting client.</summary>
		/// <returns>true if the server control maintains its view state; otherwise false. The default is true.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0002BF5E File Offset: 0x0002A15E
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x0002BF6B File Offset: 0x0002A16B
		[DefaultValue(true)]
		[Themeable(false)]
		[WebSysDescription("An Identification of the control that is rendered.")]
		[WebCategory("Behavior")]
		public virtual bool EnableViewState
		{
			get
			{
				return (this.stateMask & 1) != 0;
			}
			set
			{
				this.SetMask(1, value);
			}
		}

		/// <summary>Gets or sets the programmatic identifier assigned to the server control.</summary>
		/// <returns>The programmatic identifier assigned to the control.</returns>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x0002BF75 File Offset: 0x0002A175
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x0002BF8D File Offset: 0x0002A18D
		[Filterable(false)]
		[Themeable(false)]
		[WebSysDescription("The name of the control that is rendered.")]
		[ParenthesizePropertyName(true)]
		[MergableProperty(false)]
		public virtual string ID
		{
			get
			{
				if ((this.stateMask & 1024) == 0)
				{
					return null;
				}
				return this._userId;
			}
			set
			{
				if (value != null && value.Length == 0)
				{
					value = null;
				}
				this.stateMask |= 1024;
				this._userId = value;
				this.NullifyUniqueID();
			}
		}

		/// <summary>Gets a value indicating whether controls contained within this control have control state.</summary>
		/// <returns>true if children of this control do not use control state; otherwise, false.</returns>
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0002BFBC File Offset: 0x0002A1BC
		protected internal bool IsChildControlStateCleared
		{
			get
			{
				return this._isChildControlStateCleared;
			}
		}

		/// <summary>Gets a value indicating whether the control participates in loading its view state by <see cref="P:System.Web.UI.Control.ID" /> instead of index. </summary>
		/// <returns>true if the control loads its view state by <see cref="P:System.Web.UI.Control.ID" />; otherwise, false. The default value is false.</returns>
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		protected bool LoadViewStateByID
		{
			get
			{
				if (this.loadViewStateByID == null)
				{
					this.loadViewStateByID = new bool?(this.IsLoadViewStateByID());
				}
				return this.loadViewStateByID.Value;
			}
		}

		/// <summary>Gets a value indicating whether view state is enabled for this control.</summary>
		/// <returns>true if view state is enabled for the control; otherwise, false.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0002BFF0 File Offset: 0x0002A1F0
		protected internal bool IsViewStateEnabled
		{
			get
			{
				for (Control control = this; control != null; control = control.Parent)
				{
					if (!control.EnableViewState)
					{
						return false;
					}
					ViewStateMode viewStateMode = control.ViewStateMode;
					if (viewStateMode != ViewStateMode.Inherit)
					{
						return viewStateMode == ViewStateMode.Enabled;
					}
				}
				return true;
			}
		}

		/// <summary>Gets the character used to separate control identifiers.</summary>
		/// <returns>The separator character. The default is "$".</returns>
		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x0002C025 File Offset: 0x0002A225
		protected char IdSeparator
		{
			get
			{
				return '$';
			}
		}

		/// <summary>Gets a reference to the server control's naming container, which creates a unique namespace for differentiating between server controls with the same <see cref="P:System.Web.UI.Control.ID" /> property value.</summary>
		/// <returns>The server control's naming container.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x0002C02C File Offset: 0x0002A22C
		[WebSysDescription("The container that this control is part of. The control's name has to be unique within the container.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public virtual Control NamingContainer
		{
			get
			{
				if (this._namingContainer == null && this._parent != null)
				{
					if ((this._parent.stateMask & 64) == 0)
					{
						this._namingContainer = this._parent.NamingContainer;
					}
					else
					{
						this._namingContainer = this._parent;
					}
				}
				return this._namingContainer;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.Page" /> instance that contains the server control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Page" /> instance that contains the server control.</returns>
		/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0002C080 File Offset: 0x0002A280
		// (set) Token: 0x060010AD RID: 4269 RVA: 0x0002C0CF File Offset: 0x0002A2CF
		[WebSysDescription("The webpage that this control resides on.")]
		[Bindable(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Page Page
		{
			get
			{
				if (this._page == null)
				{
					if (this.NamingContainer != null)
					{
						this._page = this.NamingContainer.Page;
					}
					else if (this.Parent != null)
					{
						this._page = this.Parent.Page;
					}
				}
				return this._page;
			}
			set
			{
				this._page = value;
			}
		}

		/// <summary>Gets a reference to the server control's parent control in the page control hierarchy.</summary>
		/// <returns>A reference to the server control's parent control.</returns>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0002C0D8 File Offset: 0x0002A2D8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("The parent control of this control.")]
		[Bindable(false)]
		[Browsable(false)]
		public virtual Control Parent
		{
			get
			{
				return this._parent;
			}
		}

		/// <summary>Gets information about the container that hosts the current control when rendered on a design surface.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> that contains information about the container that the control is hosted in.</returns>
		/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
		// (set) Token: 0x060010B0 RID: 4272 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		[WebSysDescription("The site this control is part of.")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ISite Site
		{
			get
			{
				return this._site;
			}
			set
			{
				this._site = value;
			}
		}

		/// <summary>Gets or sets a reference to the template that contains this control. </summary>
		/// <returns>The <see cref="T:System.Web.UI.TemplateControl" /> instance that contains this control. </returns>
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060010B1 RID: 4273 RVA: 0x0002C0F1 File Offset: 0x0002A2F1
		// (set) Token: 0x060010B2 RID: 4274 RVA: 0x0002C0F9 File Offset: 0x0002A2F9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public TemplateControl TemplateControl
		{
			get
			{
				return this.TemplateControlInternal;
			}
			[EditorBrowsable(EditorBrowsableState.Never)]
			set
			{
				this._templateControl = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0002C102 File Offset: 0x0002A302
		internal virtual TemplateControl TemplateControlInternal
		{
			get
			{
				if (this._templateControl != null)
				{
					return this._templateControl;
				}
				if (this._parent != null)
				{
					return this._parent.TemplateControl;
				}
				return null;
			}
		}

		/// <summary>Gets the virtual directory of the <see cref="T:System.Web.UI.Page" /> or <see cref="T:System.Web.UI.UserControl" /> that contains the current server control.</summary>
		/// <returns>The virtual directory of the page or user control that contains the server control.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x0002C128 File Offset: 0x0002A328
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("A virtual directory containing the parent of the control.")]
		[Browsable(false)]
		public virtual string TemplateSourceDirectory
		{
			get
			{
				if (this._templateSourceDirectory == null)
				{
					TemplateControl templateControl = this.TemplateControl;
					if (templateControl == null)
					{
						HttpContext context = this.Context;
						if (context != null)
						{
							this._templateSourceDirectory = VirtualPathUtility.GetDirectory(context.Request.CurrentExecutionFilePath);
						}
					}
					else if (templateControl != this)
					{
						this._templateSourceDirectory = templateControl.TemplateSourceDirectory;
					}
					if (this._templateSourceDirectory == null && this is TemplateControl)
					{
						string appRelativeVirtualPath = ((TemplateControl)this).AppRelativeVirtualPath;
						if (appRelativeVirtualPath != null)
						{
							string directory = VirtualPathUtility.GetDirectory(VirtualPathUtility.ToAbsolute(appRelativeVirtualPath));
							int num = directory.Length;
							if (num <= 1)
							{
								return directory;
							}
							if (directory[--num] == '/')
							{
								this._templateSourceDirectory = directory.Substring(0, num);
							}
						}
						else
						{
							this._templateSourceDirectory = string.Empty;
						}
					}
					if (this._templateSourceDirectory == null)
					{
						this._templateSourceDirectory = string.Empty;
					}
				}
				return this._templateSourceDirectory;
			}
		}

		/// <summary>Gets the unique, hierarchically qualified identifier for the server control.</summary>
		/// <returns>The fully qualified identifier for the server control.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x0002C1FC File Offset: 0x0002A3FC
		[WebSysDescription("The unique ID of the control.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string UniqueID
		{
			get
			{
				if (this.uniqueID != null)
				{
					return this.uniqueID;
				}
				Control namingContainer = this.NamingContainer;
				if (namingContainer == null)
				{
					return this._userId;
				}
				this.EnsureIDInternal();
				string text = namingContainer.UniqueID;
				if (namingContainer == this.Page || text == null)
				{
					this.uniqueID = this._userId;
					return this.uniqueID;
				}
				this.uniqueID = text + this.IdSeparator.ToString() + this._userId;
				return this.uniqueID;
			}
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0002C27B File Offset: 0x0002A47B
		private void SetMask(int m, bool val)
		{
			if (val)
			{
				this.stateMask |= m;
				return;
			}
			this.stateMask &= ~m;
		}

		/// <summary>Gets or sets a value that indicates whether a server control is rendered as UI on the page.</summary>
		/// <returns>true if the control is visible on the page; otherwise false.</returns>
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0002C29E File Offset: 0x0002A49E
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x0002C2C1 File Offset: 0x0002A4C1
		[WebCategory("Behavior")]
		[Bindable(true)]
		[DefaultValue(true)]
		[WebSysDescription("Visiblity state of the control.")]
		public virtual bool Visible
		{
			get
			{
				return (this.stateMask & 2) != 0 && (this._parent == null || this._parent.Visible);
			}
			set
			{
				if (((value && (this.stateMask & 2) == 0) || (!value && (this.stateMask & 2) != 0)) && this.IsTrackingViewState)
				{
					this.stateMask |= 128;
				}
				this.SetMask(2, value);
			}
		}

		/// <summary>Gets a value that indicates whether the server control's child controls have been created.</summary>
		/// <returns>true if child controls have been created; otherwise, false.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0002C2FF File Offset: 0x0002A4FF
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x0002C310 File Offset: 0x0002A510
		protected bool ChildControlsCreated
		{
			get
			{
				return (this.stateMask & 512) != 0;
			}
			set
			{
				if (!value && (this.stateMask & 512) != 0)
				{
					ControlCollection controls = this.Controls;
					if (controls != null)
					{
						controls.Clear();
					}
				}
				this.SetMask(512, value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object associated with the server control for the current Web request.</summary>
		/// <returns>The specified <see cref="T:System.Web.HttpContext" /> object associated with the current request.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0002C34C File Offset: 0x0002A54C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal virtual HttpContext Context
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.Context;
				}
				return HttpContext.Current;
			}
		}

		/// <summary>Gets a list of event handler delegates for the control. This property is read-only.</summary>
		/// <returns>The list of event handler delegates.</returns>
		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0002C36F File Offset: 0x0002A56F
		protected EventHandlerList Events
		{
			get
			{
				if (this._events == null)
				{
					this._events = new EventHandlerList();
				}
				return this._events;
			}
		}

		/// <summary>Gets a value indicating whether the current server control's child controls have any saved view-state settings.</summary>
		/// <returns>true if any child controls have saved view state information; otherwise, false.</returns>
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x0002C38A File Offset: 0x0002A58A
		protected bool HasChildViewState
		{
			get
			{
				return this.pendingVS != null && this.pendingVS.Count > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the server control is saving changes to its view state.</summary>
		/// <returns>true if the control is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0002C3A4 File Offset: 0x0002A5A4
		protected bool IsTrackingViewState
		{
			get
			{
				return (this.stateMask & 256) != 0;
			}
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a server control across multiple requests for the same page.</summary>
		/// <returns>An instance of the <see cref="T:System.Web.UI.StateBag" /> class that contains the server control's view-state information.</returns>
		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0002C3B5 File Offset: 0x0002A5B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("ViewState")]
		protected virtual StateBag ViewState
		{
			get
			{
				if (this._viewState == null)
				{
					this._viewState = new StateBag(this.ViewStateIgnoresCase);
				}
				if (this.IsTrackingViewState)
				{
					this._viewState.TrackViewState();
				}
				return this._viewState;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.StateBag" /> object is case-insensitive.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.StateBag" /> instance is case-insensitive; otherwise, false. The default is false.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x00008A4D File Offset: 0x00006C4D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected virtual bool ViewStateIgnoresCase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0002C3E9 File Offset: 0x0002A5E9
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0002C3F7 File Offset: 0x0002A5F7
		internal bool AutoEventWireup
		{
			get
			{
				return (this.stateMask & 32) != 0;
			}
			set
			{
				this.SetMask(32, value);
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0002C402 File Offset: 0x0002A602
		internal void SetBindingContainer(bool isBC)
		{
			this.SetMask(16, isBC);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0002C40D File Offset: 0x0002A60D
		internal void ResetChildNames()
		{
			this.ResetChildNames(-1);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0002C416 File Offset: 0x0002A616
		internal void ResetChildNames(int value)
		{
			if (value < 0)
			{
				this.defaultNumberID = 0;
				return;
			}
			this.defaultNumberID = value;
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0002C42B File Offset: 0x0002A62B
		internal int GetDefaultNumberID()
		{
			return this.defaultNumberID;
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0002C434 File Offset: 0x0002A634
		private string GetDefaultName()
		{
			string text2;
			if (this.defaultNumberID > 99)
			{
				string text = "ctl";
				int num = this.defaultNumberID;
				this.defaultNumberID = num + 1;
				text2 = text + num.ToString();
			}
			else
			{
				string[] array = Control.defaultNameArray;
				int num = this.defaultNumberID;
				this.defaultNumberID = num + 1;
				text2 = array[num];
			}
			return text2;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0002C488 File Offset: 0x0002A688
		private void NullifyUniqueID()
		{
			this.uniqueID = null;
			this.ClearCachedClientID();
			if (!this.HasControls())
			{
				return;
			}
			for (int i = 0; i < this._controls.Count; i++)
			{
				this._controls[i].NullifyUniqueID();
			}
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0002C4D4 File Offset: 0x0002A6D4
		private bool IsLoadViewStateByID()
		{
			if (Control.loadViewStateByIDCache == null)
			{
				Control.loadViewStateByIDCache = new Dictionary<Type, bool>();
			}
			Type type = base.GetType();
			bool flag;
			if (Control.loadViewStateByIDCache.TryGetValue(type, out flag))
			{
				return flag;
			}
			AttributeCollection attributes = TypeDescriptor.GetAttributes(type);
			flag = false;
			if (attributes != null)
			{
				using (IEnumerator enumerator = attributes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((Attribute)enumerator.Current) is ViewStateModeByIdAttribute)
						{
							flag = true;
							break;
						}
					}
				}
			}
			Control.loadViewStateByIDCache.Add(type, flag);
			return flag;
		}

		/// <summary>Called after a child control is added to the <see cref="P:System.Web.UI.Control.Controls" /> collection of the <see cref="T:System.Web.UI.Control" /> object.</summary>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that has been added. </param>
		/// <param name="index">The index of the control in the <see cref="P:System.Web.UI.Control.Controls" /> collection. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="control" /> is a <see cref="T:System.Web.UI.WebControls.Substitution" />  control.</exception>
		// Token: 0x060010CA RID: 4298 RVA: 0x0002C570 File Offset: 0x0002A770
		protected internal virtual void AddedControl(Control control, int index)
		{
			this.ResetControlsCache();
			if (control._parent != null)
			{
				control._parent.Controls.Remove(control);
			}
			control._parent = this;
			Control control2 = (((this.stateMask & 64) != 0) ? this : this.NamingContainer);
			if ((this.stateMask & 6144) != 0)
			{
				control.InitRecursive(control2);
				control.SetMask(262144, false);
				if ((this.stateMask & 24576) != 0 && this.pendingVS != null)
				{
					bool flag = this.LoadViewStateByID;
					string text;
					object obj;
					if (flag)
					{
						control.EnsureID();
						text = control.ID;
						obj = this.pendingVS[text];
					}
					else
					{
						text = null;
						obj = this.pendingVS[index];
					}
					if (obj != null)
					{
						if (flag)
						{
							this.pendingVS.Remove(text);
						}
						else
						{
							this.pendingVS.Remove(index);
						}
						if (this.pendingVS.Count == 0)
						{
							this.pendingVS = null;
						}
						control.LoadViewStateRecursive(obj);
					}
				}
				if ((this.stateMask & 16384) != 0)
				{
					control.LoadRecursive();
				}
				if ((this.stateMask & 32768) != 0)
				{
					control.PreRenderRecursiveInternal();
				}
				return;
			}
			control.SetNamingContainer(control2);
			control.SetMask(262144, false);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0002C6AA File Offset: 0x0002A8AA
		private void SetNamingContainer(Control nc)
		{
			if (nc != null)
			{
				this._namingContainer = nc;
				if (this.AutoID)
				{
					this.EnsureIDInternal();
				}
			}
		}

		/// <summary>Notifies the server control that an element, either XML or HTML, was parsed, and adds the element to the server control's <see cref="T:System.Web.UI.ControlCollection" /> object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		// Token: 0x060010CC RID: 4300 RVA: 0x0002C6C4 File Offset: 0x0002A8C4
		protected virtual void AddParsedSubObject(object obj)
		{
			Control control = obj as Control;
			if (control != null)
			{
				this.Controls.Add(control);
			}
		}

		/// <summary>Applies the style properties defined in the page style sheet to the control.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> containing the control.</param>
		/// <exception cref="T:System.InvalidOperationException">The style sheet is already applied.</exception>
		// Token: 0x060010CD RID: 4301 RVA: 0x0002C6E8 File Offset: 0x0002A8E8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual void ApplyStyleSheetSkin(Page page)
		{
			if (page == null)
			{
				return;
			}
			if (!this.EnableTheming)
			{
				return;
			}
			if (page.StyleSheetPageTheme != null)
			{
				ControlSkin controlSkin = page.StyleSheetPageTheme.GetControlSkin(base.GetType(), this.SkinID);
				if (controlSkin != null)
				{
					controlSkin.ApplySkin(this);
				}
			}
		}

		/// <summary>Gathers information about the server control and delivers it to the <see cref="P:System.Web.UI.Page.Trace" /> property to be displayed when tracing is enabled for the page.</summary>
		/// <param name="parentId">The identifier of the control's parent. </param>
		/// <param name="calcViewState">A Boolean that indicates whether the view-state size is calculated. </param>
		// Token: 0x060010CE RID: 4302 RVA: 0x00003946 File Offset: 0x00001B46
		[global::System.MonoTODO]
		protected void BuildProfileTree(string parentId, bool calcViewState)
		{
		}

		/// <summary>Deletes the control-state information for the server control's child controls. </summary>
		// Token: 0x060010CF RID: 4303 RVA: 0x0002C72C File Offset: 0x0002A92C
		protected void ClearChildControlState()
		{
			this._isChildControlStateCleared = true;
		}

		/// <summary>Deletes the view-state and control-state information for all the server control's child controls.</summary>
		// Token: 0x060010D0 RID: 4304 RVA: 0x0002C735 File Offset: 0x0002A935
		protected void ClearChildState()
		{
			this.ClearChildViewState();
			this.ClearChildControlState();
		}

		/// <summary>Deletes the view-state information for all the server control's child controls.</summary>
		// Token: 0x060010D1 RID: 4305 RVA: 0x0002C743 File Offset: 0x0002A943
		protected void ClearChildViewState()
		{
			this.pendingVS = null;
		}

		/// <summary>Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.</summary>
		// Token: 0x060010D2 RID: 4306 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void CreateChildControls()
		{
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
		// Token: 0x060010D3 RID: 4307 RVA: 0x0002C74C File Offset: 0x0002A94C
		protected virtual ControlCollection CreateControlCollection()
		{
			return new ControlCollection(this);
		}

		/// <summary>Determines whether the server control contains child controls. If it does not, it creates child controls.</summary>
		// Token: 0x060010D4 RID: 4308 RVA: 0x0002C754 File Offset: 0x0002A954
		protected virtual void EnsureChildControls()
		{
			if (!this.ChildControlsCreated && (this.stateMask & 8) == 0)
			{
				this.stateMask |= 8;
				if (this.Adapter != null)
				{
					this.Adapter.CreateChildControls();
				}
				else
				{
					this.CreateChildControls();
				}
				this.ChildControlsCreated = true;
				this.stateMask &= -9;
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0002C7B2 File Offset: 0x0002A9B2
		private void EnsureIDInternal()
		{
			if (this._userId != null)
			{
				return;
			}
			this._userId = this.NamingContainer.GetDefaultName();
			this.SetMask(131072, true);
		}

		/// <summary>Creates an identifier for controls that do not have an identifier assigned.</summary>
		// Token: 0x060010D6 RID: 4310 RVA: 0x0002C7DA File Offset: 0x0002A9DA
		protected void EnsureID()
		{
			if (this.NamingContainer == null)
			{
				return;
			}
			this.EnsureIDInternal();
			this.SetMask(1024, true);
		}

		/// <summary>Returns a value indicating whether events are registered for the control or any child controls.</summary>
		/// <returns>true if events are registered; otherwise, false.</returns>
		// Token: 0x060010D7 RID: 4311 RVA: 0x0002C7F7 File Offset: 0x0002A9F7
		protected bool HasEvents()
		{
			return this._events != null;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0002C802 File Offset: 0x0002AA02
		private void ResetControlsCache()
		{
			this._controlsCache = null;
			if ((this.stateMask & 64) == 0 && this.Parent != null)
			{
				this.Parent.ResetControlsCache();
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0002C82C File Offset: 0x0002AA2C
		private Hashtable InitControlsCache()
		{
			if (this._controlsCache != null)
			{
				return this._controlsCache;
			}
			if ((this.stateMask & 64) != 0 || this.Parent == null)
			{
				this._controlsCache = new Hashtable(StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				this._controlsCache = this.Parent.InitControlsCache();
			}
			return this._controlsCache;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0002C884 File Offset: 0x0002AA84
		private void EnsureControlsCache()
		{
			if (this._controlsCache != null)
			{
				return;
			}
			this.InitControlsCache();
			this.FillControlCache(this._controls);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0002C8A4 File Offset: 0x0002AAA4
		private void FillControlCache(ControlCollection controls)
		{
			if (controls == null || controls.Count == 0)
			{
				return;
			}
			foreach (object obj in controls)
			{
				Control control = (Control)obj;
				try
				{
					if (control._userId != null)
					{
						this._controlsCache.Add(control._userId, control);
					}
				}
				catch (ArgumentException)
				{
					throw new HttpException("Multiple controls with the same ID '" + control._userId + "' were found. FindControl requires that controls have unique IDs. ");
				}
				if ((control.stateMask & 64) == 0 && control.HasControls())
				{
					this.FillControlCache(control.Controls);
				}
			}
		}

		/// <summary>Determines if the server control holds only literal content.</summary>
		/// <returns>true if the server control contains solely literal content; otherwise false.</returns>
		// Token: 0x060010DC RID: 4316 RVA: 0x0002C960 File Offset: 0x0002AB60
		protected bool IsLiteralContent()
		{
			return this._controls != null && this._controls.Count == 1 && this._controls[0] is LiteralControl;
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> parameter.</summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found. </param>
		// Token: 0x060010DD RID: 4317 RVA: 0x0002C98E File Offset: 0x0002AB8E
		[WebSysDescription("")]
		public virtual Control FindControl(string id)
		{
			return this.FindControl(id, 0);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0002C998 File Offset: 0x0002AB98
		private Control LookForControlByName(string id)
		{
			this.EnsureControlsCache();
			return (Control)this._controlsCache[id];
		}

		/// <summary>Searches the current naming container for a server control with the specified <paramref name="id" /> and an integer, specified in the <paramref name="pathOffset" /> parameter, which aids in the search. You should not override this version of the <see cref="Overload:System.Web.UI.Control.FindControl" /> method.</summary>
		/// <returns>The specified control, or null if the specified control does not exist.</returns>
		/// <param name="id">The identifier for the control to be found. </param>
		/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
		// Token: 0x060010DF RID: 4319 RVA: 0x0002C9B4 File Offset: 0x0002ABB4
		protected virtual Control FindControl(string id, int pathOffset)
		{
			this.EnsureChildControls();
			if ((this.stateMask & 64) == 0)
			{
				Control control = this.NamingContainer;
				if (control == null)
				{
					return null;
				}
				return control.FindControl(id, pathOffset);
			}
			else
			{
				if (!this.HasControls())
				{
					return null;
				}
				int num = id.IndexOf(this.IdSeparator, pathOffset);
				if (num == -1)
				{
					Control control2 = this.LookForControlByName((pathOffset > 0) ? id.Substring(pathOffset) : id);
					if (control2 != null)
					{
						return control2;
					}
					if (pathOffset == 0)
					{
						Control control = this.NamingContainer;
						if (control != null)
						{
							control2 = control.FindControl(id);
							if (control2 != null)
							{
								return control2;
							}
						}
					}
					return null;
				}
				else
				{
					string text = id.Substring(pathOffset, num - pathOffset);
					Control control = this.LookForControlByName(text);
					if (control == null)
					{
						return null;
					}
					return control.FindControl(id, num + 1);
				}
			}
		}

		/// <summary>Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
		// Token: 0x060010E0 RID: 4320 RVA: 0x0002CA60 File Offset: 0x0002AC60
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				this.ViewState.LoadViewState(savedState);
				object obj = this.ViewState["Visible"];
				if (obj != null)
				{
					this.SetMask(2, (bool)obj);
					this.stateMask |= 128;
				}
			}
		}

		/// <summary>Retrieves the physical path that a virtual path, either absolute or relative, maps to.</summary>
		/// <returns>The physical path to the requested file.</returns>
		/// <param name="virtualPath">A relative or root relative URL. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="virtualPath" /> is null or an empty string ("").</exception>
		// Token: 0x060010E1 RID: 4321 RVA: 0x0002CAB0 File Offset: 0x0002ACB0
		protected string MapPathSecure(string virtualPath)
		{
			string text = UrlUtils.Combine(this.TemplateSourceDirectory, virtualPath);
			return this.Context.Request.MapPath(text);
		}

		/// <summary>Determines whether the event for the server control is passed up the page's UI server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="args">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060010E2 RID: 4322 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool OnBubbleEvent(object source, EventArgs args)
		{
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.DataBinding" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060010E3 RID: 4323 RVA: 0x0002CADC File Offset: 0x0002ACDC
		protected virtual void OnDataBinding(EventArgs e)
		{
			if ((this.event_mask & 1) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.DataBindingEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060010E4 RID: 4324 RVA: 0x0002CB14 File Offset: 0x0002AD14
		protected internal virtual void OnInit(EventArgs e)
		{
			if ((this.event_mask & 4) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.InitEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Load" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060010E5 RID: 4325 RVA: 0x0002CB4C File Offset: 0x0002AD4C
		protected internal virtual void OnLoad(EventArgs e)
		{
			if ((this.event_mask & 8) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.LoadEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060010E6 RID: 4326 RVA: 0x0002CB84 File Offset: 0x0002AD84
		protected internal virtual void OnPreRender(EventArgs e)
		{
			if ((this.event_mask & 16) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.PreRenderEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data. </param>
		// Token: 0x060010E7 RID: 4327 RVA: 0x0002CBC0 File Offset: 0x0002ADC0
		protected internal virtual void OnUnload(EventArgs e)
		{
			if ((this.event_mask & 32) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.UnloadEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.IO.Stream" /> used to read a file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that references the desired file.</returns>
		/// <param name="path">The path to the desired file.</param>
		/// <exception cref="T:System.Web.HttpException">Access to the specified file was denied.</exception>
		// Token: 0x060010E8 RID: 4328 RVA: 0x0002CBFC File Offset: 0x0002ADFC
		protected internal Stream OpenFile(string path)
		{
			Stream stream;
			try
			{
				stream = File.OpenRead(this.Context.Server.MapPath(path));
			}
			catch (UnauthorizedAccessException)
			{
				throw new HttpException("Access to the specified file was denied.");
			}
			return stream;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0002CC40 File Offset: 0x0002AE40
		internal string GetPhysicalFilePath(string virtualPath)
		{
			Page page = this.Page;
			if (VirtualPathUtility.IsAbsolute(virtualPath))
			{
				if (page == null)
				{
					return this.Context.Server.MapPath(virtualPath);
				}
				return page.MapPath(virtualPath);
			}
			else
			{
				MasterPage masterPage = null;
				for (Control control = this.Parent; control != null; control = control.Parent)
				{
					if (control is MasterPage)
					{
						masterPage = control as MasterPage;
						break;
					}
				}
				string text;
				if (masterPage != null)
				{
					text = VirtualPathUtility.Combine(masterPage.TemplateSourceDirectory + "/", virtualPath);
				}
				else
				{
					text = VirtualPathUtility.Combine(this.TemplateSourceDirectory + "/", virtualPath);
				}
				if (page == null)
				{
					return this.Context.Server.MapPath(text);
				}
				return page.MapPath(text);
			}
		}

		/// <summary>Assigns any sources of the event and its information to the control's parent.</summary>
		/// <param name="source">The source of the event. </param>
		/// <param name="args">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x060010EA RID: 4330 RVA: 0x0002CCF0 File Offset: 0x0002AEF0
		protected void RaiseBubbleEvent(object source, EventArgs args)
		{
			Control control = this.Parent;
			while (control != null && !control.OnBubbleEvent(source, args))
			{
				control = control.Parent;
			}
		}

		/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content. </param>
		// Token: 0x060010EB RID: 4331 RVA: 0x0002CD1A File Offset: 0x0002AF1A
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}

		/// <summary>Outputs the content of a server control's children to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the rendered content. </param>
		// Token: 0x060010EC RID: 4332 RVA: 0x0002CD24 File Offset: 0x0002AF24
		protected internal virtual void RenderChildren(HtmlTextWriter writer)
		{
			if (this._renderMethodDelegate != null)
			{
				this._renderMethodDelegate(writer, this);
				return;
			}
			if (this._controls == null)
			{
				return;
			}
			int count = this._controls.Count;
			for (int i = 0; i < count; i++)
			{
				Control control = this._controls[i];
				if (control != null)
				{
					ControlAdapter controlAdapter = control.Adapter;
					if (controlAdapter != null)
					{
						control.RenderControl(writer, controlAdapter);
					}
					else
					{
						control.RenderControl(writer);
					}
				}
			}
		}

		/// <summary>Gets the control adapter responsible for rendering the specified control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> that will render the control.</returns>
		// Token: 0x060010ED RID: 4333 RVA: 0x0002CD94 File Offset: 0x0002AF94
		protected virtual ControlAdapter ResolveAdapter()
		{
			HttpContext context = this.Context;
			if (context == null)
			{
				return null;
			}
			if (!context.Request.BrowserMightHaveAdapters)
			{
				return null;
			}
			IDictionary adapters = context.Request.Browser.Adapters;
			Type type = base.GetType();
			Type type2 = (Type)adapters[type];
			while (type2 == null && type != typeof(Control))
			{
				type = type.BaseType;
				type2 = (Type)adapters[type];
			}
			ControlAdapter controlAdapter = null;
			if (type2 != null)
			{
				controlAdapter = (ControlAdapter)Activator.CreateInstance(type2);
			}
			return controlAdapter;
		}

		/// <summary>Saves any server control view-state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns null.</returns>
		// Token: 0x060010EE RID: 4334 RVA: 0x0002CE30 File Offset: 0x0002B030
		protected virtual object SaveViewState()
		{
			if ((this.stateMask & 128) != 0)
			{
				this.ViewState["Visible"] = (this.stateMask & 2) != 0;
			}
			else if (this._viewState == null)
			{
				return null;
			}
			return this._viewState.SaveViewState();
		}

		/// <summary>Causes tracking of view-state changes to the server control so they can be stored in the server control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x060010EF RID: 4335 RVA: 0x0002CE82 File Offset: 0x0002B082
		protected virtual void TrackViewState()
		{
			if (this._viewState != null)
			{
				this._viewState.TrackViewState();
			}
			this.stateMask |= 256;
		}

		/// <summary>Enables a server control to perform final clean up before it is released from memory.</summary>
		// Token: 0x060010F0 RID: 4336 RVA: 0x0002CEAC File Offset: 0x0002B0AC
		public virtual void Dispose()
		{
			if ((this.event_mask & 2) != 0)
			{
				EventHandler eventHandler = (EventHandler)this._events[Control.DisposedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the server control binds to a data source.</summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060010F1 RID: 4337 RVA: 0x0002CEE8 File Offset: 0x0002B0E8
		// (remove) Token: 0x060010F2 RID: 4338 RVA: 0x0002CF09 File Offset: 0x0002B109
		[WebSysDescription("Raised when the contols databound properties are evaluated.")]
		[WebCategory("FIXME")]
		public event EventHandler DataBinding
		{
			add
			{
				this.event_mask |= 1;
				this.Events.AddHandler(Control.DataBindingEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.DataBindingEvent, value);
			}
		}

		/// <summary>Occurs when a server control is released from memory, which is the last stage of the server control lifecycle when an ASP.NET page is requested.</summary>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060010F3 RID: 4339 RVA: 0x0002CF1C File Offset: 0x0002B11C
		// (remove) Token: 0x060010F4 RID: 4340 RVA: 0x0002CF3D File Offset: 0x0002B13D
		[WebSysDescription("Raised when the contol is disposed.")]
		public event EventHandler Disposed
		{
			add
			{
				this.event_mask |= 2;
				this.Events.AddHandler(Control.DisposedEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.DisposedEvent, value);
			}
		}

		/// <summary>Occurs when the server control is initialized, which is the first step in its lifecycle.</summary>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060010F5 RID: 4341 RVA: 0x0002CF50 File Offset: 0x0002B150
		// (remove) Token: 0x060010F6 RID: 4342 RVA: 0x0002CF71 File Offset: 0x0002B171
		[WebSysDescription("Raised when the page containing the control is initialized.")]
		public event EventHandler Init
		{
			add
			{
				this.event_mask |= 4;
				this.Events.AddHandler(Control.InitEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.InitEvent, value);
			}
		}

		/// <summary>Occurs when the server control is loaded into the <see cref="T:System.Web.UI.Page" /> object.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060010F7 RID: 4343 RVA: 0x0002CF84 File Offset: 0x0002B184
		// (remove) Token: 0x060010F8 RID: 4344 RVA: 0x0002CFA5 File Offset: 0x0002B1A5
		[WebSysDescription("Raised after the page containing the control has been loaded.")]
		public event EventHandler Load
		{
			add
			{
				this.event_mask |= 8;
				this.Events.AddHandler(Control.LoadEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.LoadEvent, value);
			}
		}

		/// <summary>Occurs after the <see cref="T:System.Web.UI.Control" /> object is loaded but prior to rendering.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060010F9 RID: 4345 RVA: 0x0002CFB8 File Offset: 0x0002B1B8
		// (remove) Token: 0x060010FA RID: 4346 RVA: 0x0002CFDA File Offset: 0x0002B1DA
		[WebSysDescription("Raised before the page containing the control is rendered.")]
		public event EventHandler PreRender
		{
			add
			{
				this.event_mask |= 16;
				this.Events.AddHandler(Control.PreRenderEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.PreRenderEvent, value);
			}
		}

		/// <summary>Occurs when the server control is unloaded from memory.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060010FB RID: 4347 RVA: 0x0002CFED File Offset: 0x0002B1ED
		// (remove) Token: 0x060010FC RID: 4348 RVA: 0x0002D00F File Offset: 0x0002B20F
		[WebSysDescription("Raised when the page containing the control is unloaded.")]
		public event EventHandler Unload
		{
			add
			{
				this.event_mask |= 32;
				this.Events.AddHandler(Control.UnloadEvent, value);
			}
			remove
			{
				this.Events.RemoveHandler(Control.UnloadEvent, value);
			}
		}

		/// <summary>Binds a data source to the invoked server control and all its child controls.</summary>
		// Token: 0x060010FD RID: 4349 RVA: 0x0002D022 File Offset: 0x0002B222
		public virtual void DataBind()
		{
			this.DataBind(true);
		}

		/// <summary>Binds a data source to the server control's child controls.</summary>
		// Token: 0x060010FE RID: 4350 RVA: 0x0002D02C File Offset: 0x0002B22C
		protected virtual void DataBindChildren()
		{
			if (!this.HasControls())
			{
				return;
			}
			int num = ((this._controls != null) ? this._controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this._controls[i].DataBind();
			}
		}

		/// <summary>Determines if the server control contains any child controls.</summary>
		/// <returns>true if the control contains other controls; otherwise, false.</returns>
		// Token: 0x060010FF RID: 4351 RVA: 0x0002D076 File Offset: 0x0002B276
		public virtual bool HasControls()
		{
			return this._controls != null && this._controls.Count > 0;
		}

		/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object and stores tracing information about the control if tracing is enabled.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content. </param>
		// Token: 0x06001100 RID: 4352 RVA: 0x0002D090 File Offset: 0x0002B290
		public virtual void RenderControl(HtmlTextWriter writer)
		{
			if (this.adapter != null)
			{
				this.RenderControl(writer, this.adapter);
				return;
			}
			if ((this.stateMask & 2) != 0)
			{
				HttpContext context = this.Context;
				TraceContext traceContext = ((context != null) ? context.Trace : null);
				int num = 0;
				if (traceContext != null && traceContext.IsEnabled)
				{
					num = context.Response.GetOutputByteCount();
				}
				this.Render(writer);
				if (traceContext != null && traceContext.IsEnabled)
				{
					int num2 = context.Response.GetOutputByteCount() - num;
					traceContext.SaveSize(this, (num2 >= 0) ? num2 : 0);
				}
			}
		}

		/// <summary>Outputs server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object using a provided <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the control content.</param>
		/// <param name="adapter">The <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> that defines the rendering.</param>
		// Token: 0x06001101 RID: 4353 RVA: 0x0002D119 File Offset: 0x0002B319
		protected void RenderControl(HtmlTextWriter writer, ControlAdapter adapter)
		{
			if ((this.stateMask & 2) != 0)
			{
				adapter.BeginRender(writer);
				adapter.Render(writer);
				adapter.EndRender(writer);
			}
		}

		/// <summary>Converts a URL into one that is usable on the requesting client.</summary>
		/// <returns>The converted URL.</returns>
		/// <param name="relativeUrl">The URL associated with the <see cref="P:System.Web.UI.Control.TemplateSourceDirectory" /> property. </param>
		/// <exception cref="T:System.ArgumentNullException">Occurs if the <paramref name="relativeUrl" /> parameter contains null. </exception>
		// Token: 0x06001102 RID: 4354 RVA: 0x0002D13C File Offset: 0x0002B33C
		public string ResolveUrl(string relativeUrl)
		{
			if (relativeUrl == null)
			{
				throw new ArgumentNullException("relativeUrl");
			}
			if (relativeUrl == string.Empty)
			{
				return relativeUrl;
			}
			if (VirtualPathUtility.IsAbsolute(relativeUrl))
			{
				return relativeUrl;
			}
			if (relativeUrl[0] == '#')
			{
				return relativeUrl;
			}
			string appRelativeTemplateSourceDirectory = this.AppRelativeTemplateSourceDirectory;
			HttpContext context = this.Context;
			HttpResponse httpResponse = ((context != null) ? context.Response : null);
			if (appRelativeTemplateSourceDirectory == null || appRelativeTemplateSourceDirectory.Length == 0 || httpResponse == null || relativeUrl.IndexOf(':') >= 0)
			{
				return relativeUrl;
			}
			if (!VirtualPathUtility.IsAppRelative(relativeUrl))
			{
				relativeUrl = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(appRelativeTemplateSourceDirectory), relativeUrl);
			}
			return httpResponse.ApplyAppPathModifier(relativeUrl);
		}

		/// <summary>Gets a URL that can be used by the browser.</summary>
		/// <returns>A fully qualified URL to the specified resource suitable for use on the browser.</returns>
		/// <param name="relativeUrl">A URL relative to the current page.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="relativeUrl" /> is null.</exception>
		// Token: 0x06001103 RID: 4355 RVA: 0x0002D1D0 File Offset: 0x0002B3D0
		public string ResolveClientUrl(string relativeUrl)
		{
			if (relativeUrl == null)
			{
				throw new ArgumentNullException("relativeUrl");
			}
			if (relativeUrl.Length == 0)
			{
				return string.Empty;
			}
			if (VirtualPathUtility.IsAbsolute(relativeUrl) || relativeUrl.IndexOf(':') >= 0)
			{
				return relativeUrl;
			}
			HttpContext context = this.Context;
			HttpRequest httpRequest = ((context != null) ? context.Request : null);
			if (httpRequest == null)
			{
				return relativeUrl;
			}
			string templateSourceDirectory = this.TemplateSourceDirectory;
			if (templateSourceDirectory == null || templateSourceDirectory.Length == 0)
			{
				return relativeUrl;
			}
			string text = httpRequest.ClientFilePath;
			if (text.Length > 1 && text[text.Length - 1] != '/')
			{
				text = VirtualPathUtility.GetDirectory(text, false);
			}
			if (VirtualPathUtility.IsAppRelative(relativeUrl))
			{
				return VirtualPathUtility.MakeRelative(text, relativeUrl);
			}
			string text2 = VirtualPathUtility.AppendTrailingSlash(templateSourceDirectory);
			if (text.Length == text2.Length && string.CompareOrdinal(text, text2) == 0)
			{
				return relativeUrl;
			}
			relativeUrl = VirtualPathUtility.Combine(text2, relativeUrl);
			return VirtualPathUtility.MakeRelative(text, relativeUrl);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0002D2AC File Offset: 0x0002B4AC
		internal bool HasRenderMethodDelegate()
		{
			return this._renderMethodDelegate != null;
		}

		/// <summary>Assigns an event handler delegate to render the server control and its content into its parent control.</summary>
		/// <param name="renderMethod">The information necessary to pass to the delegate so that it can render the server control. </param>
		// Token: 0x06001105 RID: 4357 RVA: 0x0002D2B7 File Offset: 0x0002B4B7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void SetRenderMethodDelegate(RenderMethod renderMethod)
		{
			this._renderMethodDelegate = renderMethod;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0002D2C0 File Offset: 0x0002B4C0
		internal void LoadRecursive()
		{
			if ((this.stateMask & 16384) == 0)
			{
				if (this.Adapter != null)
				{
					this.Adapter.OnLoad(EventArgs.Empty);
				}
				else
				{
					this.OnLoad(EventArgs.Empty);
				}
			}
			int num = ((this._controls != null) ? this._controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this._controls[i].LoadRecursive();
			}
			this.stateMask |= 16384;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0002D348 File Offset: 0x0002B548
		internal void UnloadRecursive(bool dispose)
		{
			int num = ((this._controls != null) ? this._controls.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this._controls[i].UnloadRecursive(dispose);
			}
			ControlAdapter controlAdapter = this.Adapter;
			if (controlAdapter != null)
			{
				controlAdapter.OnUnload(EventArgs.Empty);
			}
			else
			{
				this.OnUnload(EventArgs.Empty);
			}
			if (dispose)
			{
				this.Dispose();
			}
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0002D3B8 File Offset: 0x0002B5B8
		internal void PreRenderRecursiveInternal()
		{
			if (this.Visible)
			{
				this.SetMask(2, true);
				this.EnsureChildControls();
				if (this.Adapter != null)
				{
					this.Adapter.OnPreRender(EventArgs.Empty);
				}
				else
				{
					this.OnPreRender(EventArgs.Empty);
				}
				if (!this.HasControls())
				{
					return;
				}
				int num = ((this._controls != null) ? this._controls.Count : 0);
				for (int i = 0; i < num; i++)
				{
					this._controls[i].PreRenderRecursiveInternal();
				}
			}
			else
			{
				this.SetMask(2, false);
			}
			this.stateMask |= 32768;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0002D45C File Offset: 0x0002B65C
		internal virtual void InitRecursive(Control namingContainer)
		{
			this.SetNamingContainer(namingContainer);
			if (this.HasControls())
			{
				if ((this.stateMask & 64) != 0)
				{
					namingContainer = this;
				}
				int num = ((this._controls != null) ? this._controls.Count : 0);
				for (int i = 0; i < num; i++)
				{
					this._controls[i].InitRecursive(namingContainer);
				}
			}
			if ((this.stateMask & 262144) == 0 && (this.stateMask & 2048) != 2048)
			{
				this.stateMask |= 4096;
				this.ApplyTheme();
				ControlAdapter controlAdapter = this.Adapter;
				if (controlAdapter != null)
				{
					controlAdapter.OnInit(EventArgs.Empty);
				}
				else
				{
					this.OnInit(EventArgs.Empty);
				}
				this.TrackViewState();
				this.stateMask |= 2048;
				this.stateMask &= -4097;
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0002D540 File Offset: 0x0002B740
		internal object SaveViewStateRecursive()
		{
			TraceContext traceContext = ((this.Context != null && this.Context.Trace.IsEnabled) ? this.Context.Trace : null);
			ArrayList arrayList = null;
			bool flag = this.LoadViewStateByID;
			if (this.HasControls())
			{
				int num = ((this._controls != null) ? this._controls.Count : 0);
				for (int i = 0; i < num; i++)
				{
					Control control = this._controls[i];
					object obj = control.SaveViewStateRecursive();
					if (obj != null)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						if (flag)
						{
							control.EnsureID();
							arrayList.Add(new Pair(control.ID, obj));
						}
						else
						{
							arrayList.Add(new Pair(i, obj));
						}
					}
				}
			}
			object obj2 = null;
			if (this.Adapter != null)
			{
				obj2 = this.Adapter.SaveAdapterViewState();
			}
			object obj3 = null;
			if (this.IsViewStateEnabled)
			{
				obj3 = this.SaveViewState();
			}
			if (obj3 == null && arrayList == null)
			{
				if (traceContext != null)
				{
					traceContext.SaveViewState(this, null);
				}
				return null;
			}
			if (traceContext != null)
			{
				traceContext.SaveViewState(this, obj3);
			}
			obj3 = new object[] { obj3, obj2 };
			return new Pair(obj3, arrayList);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0002D670 File Offset: 0x0002B870
		internal void LoadViewStateRecursive(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			Pair pair = (Pair)savedState;
			object[] array = (object[])pair.First;
			if (this.Adapter != null)
			{
				this.Adapter.LoadAdapterViewState(array[1]);
			}
			this.LoadViewState(array[0]);
			ArrayList arrayList = pair.Second as ArrayList;
			if (arrayList == null)
			{
				return;
			}
			int count = arrayList.Count;
			bool flag = this.LoadViewStateByID;
			for (int i = 0; i < count; i++)
			{
				Pair pair2 = arrayList[i] as Pair;
				if (pair2 != null)
				{
					if (flag)
					{
						string text = (string)pair2.First;
						bool flag2 = false;
						foreach (object obj in this.Controls)
						{
							Control control = (Control)obj;
							control.EnsureID();
							if (control.ID == text)
							{
								flag2 = true;
								control.LoadViewStateRecursive(pair2.Second);
								break;
							}
						}
						if (!flag2)
						{
							if (this.pendingVS == null)
							{
								this.pendingVS = new Hashtable();
							}
							this.pendingVS[text] = pair2.Second;
						}
					}
					else
					{
						int num = (int)pair2.First;
						if (num < this.Controls.Count)
						{
							this.Controls[num].LoadViewStateRecursive(pair2.Second);
						}
						else
						{
							if (this.pendingVS == null)
							{
								this.pendingVS = new Hashtable();
							}
							this.pendingVS[num] = pair2.Second;
						}
					}
				}
			}
			this.stateMask |= 8192;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x0002D830 File Offset: 0x0002BA30
		internal void ApplyTheme()
		{
			Page page = this.Page;
			if (page != null && page.PageTheme != null && this.EnableTheming)
			{
				ControlSkin controlSkin = page.PageTheme.GetControlSkin(base.GetType(), this.SkinID);
				if (controlSkin != null)
				{
					controlSkin.ApplySkin(this);
				}
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0002D879 File Offset: 0x0002BA79
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x0002D886 File Offset: 0x0002BA86
		internal bool AutoID
		{
			get
			{
				return (this.stateMask & 4) != 0;
			}
			set
			{
				if (!value && (this.stateMask & 64) != 0)
				{
					return;
				}
				this.SetMask(4, value);
			}
		}

		/// <summary>Called after a child control is removed from the <see cref="P:System.Web.UI.Control.Controls" /> collection of the <see cref="T:System.Web.UI.Control" /> object.</summary>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> that has been removed. </param>
		/// <exception cref="T:System.InvalidOperationException">The control is a <see cref="T:System.Web.UI.WebControls.Substitution" /> control.</exception>
		// Token: 0x0600110F RID: 4367 RVA: 0x0002D8A0 File Offset: 0x0002BAA0
		protected internal virtual void RemovedControl(Control control)
		{
			control.UnloadRecursive(false);
			control._parent = null;
			control._page = null;
			control._namingContainer = null;
			if ((control.stateMask & 131072) != 0)
			{
				control._userId = null;
				control.SetMask(1024, false);
			}
			control.NullifyUniqueID();
			control.SetMask(262144, true);
			this.ResetControlsCache();
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is true. </returns>
		/// <exception cref="T:System.InvalidOperationException">The Page_PreInit event has already occurred.- or -The control has already been added to the Controls collection.</exception>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x0002D902 File Offset: 0x0002BB02
		// (set) Token: 0x06001111 RID: 4369 RVA: 0x0002D92E File Offset: 0x0002BB2E
		[Browsable(false)]
		[DefaultValue(true)]
		[Themeable(false)]
		public virtual bool EnableTheming
		{
			get
			{
				if ((this.stateMask & 65536) != 0)
				{
					return this._enableTheming;
				}
				return this._parent == null || this._parent.EnableTheming;
			}
			set
			{
				this.SetMask(65536, true);
				this._enableTheming = value;
			}
		}

		/// <summary>Gets or sets the skin to apply to the control.</summary>
		/// <returns>The name of the skin to apply to the control. The default is <see cref="F:System.String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The style sheet has already been applied.- or -The Page_PreInit event has already occurred.- or -The control was already added to the Controls collection.</exception>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0002D943 File Offset: 0x0002BB43
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x0002D94B File Offset: 0x0002BB4B
		[Filterable(false)]
		[Browsable(false)]
		[DefaultValue("")]
		public virtual string SkinID
		{
			get
			{
				return this.skinId;
			}
			set
			{
				this.skinId = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IControlBuilderAccessor.ControlBuilder" />. </summary>
		/// <returns>The <see cref="T:System.Web.UI.ControlBuilder" /> that built the control; otherwise, null if no builder was used.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00003A2B File Offset: 0x00001C2B
		ControlBuilder IControlBuilderAccessor.ControlBuilder
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.GetDesignModeState" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> of the control state.</returns>
		// Token: 0x06001115 RID: 4373 RVA: 0x00003A2B File Offset: 0x00001C2B
		IDictionary IControlDesignerAccessor.GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.SetDesignModeState(System.Collections.IDictionary)" />. </summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
		// Token: 0x06001116 RID: 4374 RVA: 0x0002D954 File Offset: 0x0002BB54
		void IControlDesignerAccessor.SetDesignModeState(IDictionary designData)
		{
			this.SetDesignModeState(designData);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IControlDesignerAccessor.SetOwnerControl(System.Web.UI.Control)" />. </summary>
		/// <param name="owner">The owner of the control. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="owner" /> is set to the current control.</exception>
		// Token: 0x06001117 RID: 4375 RVA: 0x00003A2B File Offset: 0x00001C2B
		void IControlDesignerAccessor.SetOwnerControl(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IControlDesignerAccessor.UserData" />. </summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing information about the control.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00003A2B File Offset: 0x00001C2B
		IDictionary IControlDesignerAccessor.UserData
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IExpressionsAccessor.Expressions" />. </summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> containing <see cref="T:System.Web.UI.ExpressionBinding" /> objects that represent the properties and expressions for a control.</returns>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0002D95D File Offset: 0x0002BB5D
		ExpressionBindingCollection IExpressionsAccessor.Expressions
		{
			get
			{
				if (this.expressionBindings == null)
				{
					this.expressionBindings = new ExpressionBindingCollection();
				}
				return this.expressionBindings;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IExpressionsAccessor.HasExpressions" />. </summary>
		/// <returns>true if the control has properties set through expressions; otherwise, false.</returns>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0002D978 File Offset: 0x0002BB78
		bool IExpressionsAccessor.HasExpressions
		{
			get
			{
				return this.expressionBindings != null && this.expressionBindings.Count > 0;
			}
		}

		/// <summary>Sets input focus to a control.</summary>
		// Token: 0x0600111B RID: 4379 RVA: 0x0002D992 File Offset: 0x0002BB92
		public virtual void Focus()
		{
			this.Page.SetFocus(this);
		}

		/// <summary>Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState" /> method.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored. </param>
		// Token: 0x0600111C RID: 4380 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void LoadControlState(object savedState)
		{
		}

		/// <summary>Saves any server control state changes that have occurred since the time the page was posted back to the server.</summary>
		/// <returns>Returns the server control's current state. If there is no state associated with the control, this method returns null.</returns>
		// Token: 0x0600111D RID: 4381 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected internal virtual object SaveControlState()
		{
			return null;
		}

		/// <summary>Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding" /> event. </summary>
		/// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding" /> event is raised; otherwise, false.</param>
		// Token: 0x0600111E RID: 4382 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		protected virtual void DataBind(bool raiseOnDataBinding)
		{
			bool flag = false;
			if ((this.stateMask & 64) != 0 && this.Page != null)
			{
				object dataItem = DataBinder.GetDataItem(this, out flag);
				if (flag)
				{
					this.Page.PushDataItemContext(dataItem);
				}
			}
			try
			{
				if (raiseOnDataBinding)
				{
					this.OnDataBinding(EventArgs.Empty);
				}
				this.DataBindChildren();
			}
			finally
			{
				if (flag)
				{
					this.Page.PopDataItemContext();
				}
			}
		}

		/// <summary>Gets design-time data for a control.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control.</returns>
		// Token: 0x0600111F RID: 4383 RVA: 0x00003A2B File Offset: 0x00001C2B
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected virtual IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets design-time data for a control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IDictionary" /> containing the design-time data for the control. </param>
		// Token: 0x06001120 RID: 4384 RVA: 0x00003A2B File Offset: 0x00001C2B
		protected virtual void SetDesignModeState(IDictionary data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0002DA10 File Offset: 0x0002BC10
		internal bool IsInited
		{
			get
			{
				return (this.stateMask & 2048) != 0;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0002DA21 File Offset: 0x0002BC21
		internal bool IsLoaded
		{
			get
			{
				return (this.stateMask & 16384) != 0;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0002DA32 File Offset: 0x0002BC32
		internal bool IsPrerendered
		{
			get
			{
				return (this.stateMask & 32768) != 0;
			}
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0002DA43 File Offset: 0x0002BC43
		private bool CheckForValidationSupport()
		{
			return base.GetType().GetCustomAttributes(typeof(SupportsEventValidationAttribute), false).Length != 0;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0002DA60 File Offset: 0x0002BC60
		internal void ValidateEvent(string uniqueId, string argument)
		{
			Page page = this.Page;
			if (page != null && this.CheckForValidationSupport())
			{
				page.ClientScript.ValidateEvent(uniqueId, argument);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IParserAccessor.AddParsedSubObject(System.Object)" />. </summary>
		/// <param name="obj">The object to add.</param>
		// Token: 0x06001126 RID: 4390 RVA: 0x0002DA8C File Offset: 0x0002BC8C
		void IParserAccessor.AddParsedSubObject(object obj)
		{
			this.AddParsedSubObject(obj);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataBindingsAccessor.DataBindings" />. </summary>
		/// <returns>The collection of data bindings.</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0002DA95 File Offset: 0x0002BC95
		DataBindingCollection IDataBindingsAccessor.DataBindings
		{
			get
			{
				if (this.dataBindings == null)
				{
					this.dataBindings = new DataBindingCollection();
				}
				return this.dataBindings;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataBindingsAccessor.HasDataBindings" />. </summary>
		/// <returns>true if the control contains data-binding logic; otherwise, false.</returns>
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0002DAB0 File Offset: 0x0002BCB0
		bool IDataBindingsAccessor.HasDataBindings
		{
			get
			{
				return this.dataBindings != null && this.dataBindings.Count > 0;
			}
		}

		/// <summary>Gets or sets the view-state mode of this control.</summary>
		/// <returns>The view-state mode of this control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">An attempt was made to set this property to a value that is not in the <see cref="T:System.Web.UI.ViewStateMode" /> enumeration.</exception>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0002DACB File Offset: 0x0002BCCB
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x0002DAD3 File Offset: 0x0002BCD3
		[DefaultValue(ViewStateMode.Inherit)]
		[Themeable(false)]
		public virtual ViewStateMode ViewStateMode
		{
			get
			{
				return this.viewStateMode;
			}
			set
			{
				if (value < ViewStateMode.Inherit || value > ViewStateMode.Disabled)
				{
					throw new ArgumentOutOfRangeException("An attempt was made to set this property to a value that is not in the ViewStateMode enumeration.");
				}
				this.viewStateMode = value;
			}
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters.</summary>
		/// <returns>The URL that corresponds to the specified route parameters.</returns>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x0600112B RID: 4395 RVA: 0x0002DAEF File Offset: 0x0002BCEF
		public string GetRouteUrl(object routeParameters)
		{
			return this.GetRouteUrl(null, new RouteValueDictionary(routeParameters));
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters.</summary>
		/// <returns>The URL that corresponds to the specified route parameters.</returns>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x0600112C RID: 4396 RVA: 0x0002DAFE File Offset: 0x0002BCFE
		public string GetRouteUrl(RouteValueDictionary routeParameters)
		{
			return this.GetRouteUrl(null, routeParameters);
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters and a route name.</summary>
		/// <returns>The URL that corresponds to the specified route parameters and route name.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x0600112D RID: 4397 RVA: 0x0002DB08 File Offset: 0x0002BD08
		public string GetRouteUrl(string routeName, object routeParameters)
		{
			return this.GetRouteUrl(routeName, new RouteValueDictionary(routeParameters));
		}

		/// <summary>Gets the URL that corresponds to a set of route parameters and a route name.</summary>
		/// <returns>The URL that corresponds to the specified route parameters and route name.</returns>
		/// <param name="routeName">The name of the route.</param>
		/// <param name="routeParameters">The route parameters.</param>
		// Token: 0x0600112E RID: 4398 RVA: 0x0002DB18 File Offset: 0x0002BD18
		public string GetRouteUrl(string routeName, RouteValueDictionary routeParameters)
		{
			HttpContext httpContext = this.Context ?? HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest == null)
			{
				return null;
			}
			VirtualPathData virtualPath = RouteTable.Routes.GetVirtualPath(httpRequest.RequestContext, routeName, routeParameters);
			if (virtualPath == null)
			{
				return null;
			}
			return virtualPath.VirtualPath;
		}

		/// <summary>Returns the prefixed portion of the <see cref="P:System.Web.UI.Control.UniqueID" /> property of the specified control.</summary>
		/// <returns>The prefixed portion of the <see cref="P:System.Web.UI.Control.UniqueID" /> property of the specified control.</returns>
		/// <param name="control">A control that is within a naming container.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.Control.NamingContainer" /> property of <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		// Token: 0x0600112F RID: 4399 RVA: 0x0002DB68 File Offset: 0x0002BD68
		public string GetUniqueIDRelativeTo(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Control control2 = this;
			Control namingContainer = control.NamingContainer;
			if (namingContainer != null)
			{
				while (control2 != null && control2 != namingContainer)
				{
					control2 = control2.Parent;
				}
			}
			if (control2 != namingContainer)
			{
				throw new InvalidOperationException(string.Format("This control is not a descendant of the NamingContainer of '{0}'", control.UniqueID));
			}
			int num = control.UniqueID.LastIndexOf(this.IdSeparator);
			if (num < 0)
			{
				return this.UniqueID;
			}
			return this.UniqueID.Substring(num + 1);
		}

		/// <summary>Gets or sets a value that indicates whether the control checks client input from the browser for potentially dangerous values.</summary>
		/// <returns>A value that determines whether the control checks client input. Values can include <see cref="F:System.Web.UI.ValidateRequestMode.Disabled" />, <see cref="F:System.Web.UI.ValidateRequestMode.Enabled" />, and <see cref="F:System.Web.UI.ValidateRequestMode.Inherit" />. The default is <see cref="F:System.Web.UI.ValidateRequestMode.Inherit" />, which means that the control gets the value from its parent.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0002DBE4 File Offset: 0x0002BDE4
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Begins design-time tracing of rendering data.</summary>
		/// <param name="writer">The object that writes trace data.</param>
		/// <param name="traceObject">The trace object.</param>
		// Token: 0x06001132 RID: 4402 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void BeginRenderTracing(TextWriter writer, object traceObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Ends design-time tracing of rendering data.</summary>
		/// <param name="writer">The object that writes trace data.</param>
		/// <param name="traceObject">The trace object.</param>
		// Token: 0x06001133 RID: 4403 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected void EndRenderTracing(TextWriter writer, object traceObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets trace data for design-time tracing of rendering data, using the trace data key and the trace data value.</summary>
		/// <param name="traceDataKey">The trace data key.</param>
		/// <param name="traceDataValue">The trace data value.</param>
		// Token: 0x06001134 RID: 4404 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetTraceData(object traceDataKey, object traceDataValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sets trace data for design-time tracing of rendering data, using the traced object, the trace data key, and the trace data value.</summary>
		/// <param name="tracedObject">The traced object.</param>
		/// <param name="traceDataKey">The trace data key.</param>
		/// <param name="traceDataValue">The trace data value.</param>
		// Token: 0x06001135 RID: 4405 RVA: 0x0000B3D8 File Offset: 0x000095D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400139B RID: 5019
		internal static string[] defaultNameArray;

		// Token: 0x0400139C RID: 5020
		private int event_mask;

		// Token: 0x0400139D RID: 5021
		private const int databinding_mask = 1;

		// Token: 0x0400139E RID: 5022
		private const int disposed_mask = 2;

		// Token: 0x0400139F RID: 5023
		private const int init_mask = 4;

		// Token: 0x040013A0 RID: 5024
		private const int load_mask = 8;

		// Token: 0x040013A1 RID: 5025
		private const int prerender_mask = 16;

		// Token: 0x040013A2 RID: 5026
		private const int unload_mask = 32;

		// Token: 0x040013A3 RID: 5027
		[ThreadStatic]
		private static Dictionary<Type, bool> loadViewStateByIDCache;

		// Token: 0x040013A4 RID: 5028
		private bool? loadViewStateByID;

		// Token: 0x040013A5 RID: 5029
		private string uniqueID;

		// Token: 0x040013A6 RID: 5030
		private string clientID;

		// Token: 0x040013A7 RID: 5031
		private string _userId;

		// Token: 0x040013A8 RID: 5032
		private ControlCollection _controls;

		// Token: 0x040013A9 RID: 5033
		private Control _namingContainer;

		// Token: 0x040013AA RID: 5034
		private Page _page;

		// Token: 0x040013AB RID: 5035
		private Control _parent;

		// Token: 0x040013AC RID: 5036
		private ISite _site;

		// Token: 0x040013AD RID: 5037
		private StateBag _viewState;

		// Token: 0x040013AE RID: 5038
		private EventHandlerList _events;

		// Token: 0x040013AF RID: 5039
		private RenderMethod _renderMethodDelegate;

		// Token: 0x040013B0 RID: 5040
		private Hashtable _controlsCache;

		// Token: 0x040013B1 RID: 5041
		private int defaultNumberID;

		// Token: 0x040013B2 RID: 5042
		private DataBindingCollection dataBindings;

		// Token: 0x040013B3 RID: 5043
		private Hashtable pendingVS;

		// Token: 0x040013B4 RID: 5044
		private TemplateControl _templateControl;

		// Token: 0x040013B5 RID: 5045
		private bool _isChildControlStateCleared;

		// Token: 0x040013B6 RID: 5046
		private string _templateSourceDirectory;

		// Token: 0x040013B7 RID: 5047
		private ViewStateMode viewStateMode;

		// Token: 0x040013B8 RID: 5048
		private ClientIDMode? clientIDMode;

		// Token: 0x040013B9 RID: 5049
		private ClientIDMode? effectiveClientIDMode;

		// Token: 0x040013BA RID: 5050
		private Version renderingCompatibility;

		// Token: 0x040013BB RID: 5051
		private bool? renderingCompatibilityOld;

		// Token: 0x040013BC RID: 5052
		private int stateMask;

		// Token: 0x040013BD RID: 5053
		private const int ENABLE_VIEWSTATE = 1;

		// Token: 0x040013BE RID: 5054
		private const int VISIBLE = 2;

		// Token: 0x040013BF RID: 5055
		private const int AUTOID = 4;

		// Token: 0x040013C0 RID: 5056
		private const int CREATING_CONTROLS = 8;

		// Token: 0x040013C1 RID: 5057
		private const int BINDING_CONTAINER = 16;

		// Token: 0x040013C2 RID: 5058
		private const int AUTO_EVENT_WIREUP = 32;

		// Token: 0x040013C3 RID: 5059
		private const int IS_NAMING_CONTAINER = 64;

		// Token: 0x040013C4 RID: 5060
		private const int VISIBLE_CHANGED = 128;

		// Token: 0x040013C5 RID: 5061
		private const int TRACK_VIEWSTATE = 256;

		// Token: 0x040013C6 RID: 5062
		private const int CHILD_CONTROLS_CREATED = 512;

		// Token: 0x040013C7 RID: 5063
		private const int ID_SET = 1024;

		// Token: 0x040013C8 RID: 5064
		private const int INITED = 2048;

		// Token: 0x040013C9 RID: 5065
		private const int INITING = 4096;

		// Token: 0x040013CA RID: 5066
		private const int VIEWSTATE_LOADED = 8192;

		// Token: 0x040013CB RID: 5067
		private const int LOADED = 16384;

		// Token: 0x040013CC RID: 5068
		private const int PRERENDERED = 32768;

		// Token: 0x040013CD RID: 5069
		private const int ENABLE_THEMING = 65536;

		// Token: 0x040013CE RID: 5070
		private const int AUTOID_SET = 131072;

		// Token: 0x040013CF RID: 5071
		private const int REMOVED = 262144;

		// Token: 0x040013D0 RID: 5072
		private ControlAdapter adapter;

		// Token: 0x040013D1 RID: 5073
		private bool did_adapter_lookup;

		// Token: 0x040013D2 RID: 5074
		private string _appRelativeTemplateSourceDirectory;

		// Token: 0x040013D3 RID: 5075
		internal ControlSkin controlSkin;

		// Token: 0x040013D4 RID: 5076
		private string skinId = string.Empty;

		// Token: 0x040013D5 RID: 5077
		private bool _enableTheming = true;

		// Token: 0x040013D6 RID: 5078
		private ExpressionBindingCollection expressionBindings;
	}
}
