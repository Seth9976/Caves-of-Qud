using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Runtime.CompilerServices;
using Unity;

namespace System.ComponentModel.Design
{
	/// <summary>Presents a user interface for designing components.</summary>
	// Token: 0x02000104 RID: 260
	public class DesignSurface : IServiceProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000775 RID: 1909 RVA: 0x0000C501 File Offset: 0x0000A701
		public DesignSurface()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <param name="rootComponentType">The type of root component to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rootComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000776 RID: 1910 RVA: 0x0000C50A File Offset: 0x0000A70A
		public DesignSurface(Type rootComponentType)
			: this(null, rootComponentType)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <param name="parentProvider">The parent service provider, or null if there is no parent used to resolve services.</param>
		/// <param name="rootComponentType">The type of root component to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rootComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000777 RID: 1911 RVA: 0x0000C514 File Offset: 0x0000A714
		public DesignSurface(IServiceProvider parentProvider, Type rootComponentType)
			: this(parentProvider)
		{
			if (rootComponentType == null)
			{
				throw new ArgumentNullException("rootComponentType");
			}
			this.BeginLoad(rootComponentType);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurface" /> class.</summary>
		/// <param name="parentProvider">The parent service provider, or null if there is no parent used to resolve services.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x06000778 RID: 1912 RVA: 0x0000C538 File Offset: 0x0000A738
		public DesignSurface(IServiceProvider parentProvider)
		{
			this._serviceContainer = new DesignSurfaceServiceContainer(parentProvider);
			this._serviceContainer.AddNonReplaceableService(typeof(IServiceContainer), this._serviceContainer);
			this._designerHost = new DesignerHost(this._serviceContainer);
			this._designerHost.DesignerLoaderHostLoaded += this.OnDesignerHost_Loaded;
			this._designerHost.DesignerLoaderHostLoading += this.OnDesignerHost_Loading;
			this._designerHost.DesignerLoaderHostUnloading += this.OnDesignerHost_Unloading;
			this._designerHost.DesignerLoaderHostUnloaded += this.OnDesignerHost_Unloaded;
			this._designerHost.Activated += this.OnDesignerHost_Activated;
			this._serviceContainer.AddNonReplaceableService(typeof(IComponentChangeService), this._designerHost);
			this._serviceContainer.AddNonReplaceableService(typeof(IDesignerHost), this._designerHost);
			this._serviceContainer.AddNonReplaceableService(typeof(IContainer), this._designerHost);
			this._serviceContainer.AddService(typeof(ITypeDescriptorFilterService), new TypeDescriptorFilterService(this._serviceContainer));
			ExtenderService extenderService = new ExtenderService();
			this._serviceContainer.AddService(typeof(IExtenderProviderService), extenderService);
			this._serviceContainer.AddService(typeof(IExtenderListService), extenderService);
			this._serviceContainer.AddService(typeof(DesignSurface), this);
			SelectionService selectionService = new SelectionService(this._serviceContainer);
			this._serviceContainer.AddService(typeof(ISelectionService), selectionService);
		}

		/// <summary>Gets the service container.</summary>
		/// <returns>The service container that provides all services to designers contained within the design surface.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0000C6D1 File Offset: 0x0000A8D1
		protected ServiceContainer ServiceContainer
		{
			get
			{
				if (this._designerHost == null)
				{
					throw new ObjectDisposedException("DesignSurface");
				}
				return this._serviceContainer;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.IContainer" /> implementation within the design surface.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IContainer" /> implementation within the design surface.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0000C6EC File Offset: 0x0000A8EC
		public IContainer ComponentContainer
		{
			get
			{
				if (this._designerHost == null)
				{
					throw new ObjectDisposedException("DesignSurface");
				}
				return this._designerHost.Container;
			}
		}

		/// <summary>Gets a value indicating whether the design surface is currently loaded.</summary>
		/// <returns>true if the design surface is currently loaded; otherwise, false.</returns>
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x0000C70C File Offset: 0x0000A90C
		public bool IsLoaded
		{
			get
			{
				return this._isLoaded;
			}
		}

		/// <summary>Returns a collection of loading errors or a void collection.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> of loading errors.</returns>
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x0000C714 File Offset: 0x0000A914
		public ICollection LoadErrors
		{
			get
			{
				if (this._loadErrors == null)
				{
					this._loadErrors = new object[0];
				}
				return this._loadErrors;
			}
		}

		/// <summary>Gets the view for the root designer.</summary>
		/// <returns>The view for the root designer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The design surface is not loading, the designer loader has not yet created a root designer, or the design surface finished the load, but failed. More information may available in the <see cref="P:System.Exception.InnerException" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The designer loaded, but it does not offer a view compatible with this design surface.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x0000C730 File Offset: 0x0000A930
		public object View
		{
			get
			{
				if (this._designerHost == null)
				{
					throw new ObjectDisposedException("DesignSurface");
				}
				if (this._designerHost.RootComponent == null || this.LoadErrors.Count > 0)
				{
					throw new InvalidOperationException("The DesignSurface isn't loaded.");
				}
				IRootDesigner rootDesigner = this._designerHost.GetDesigner(this._designerHost.RootComponent) as IRootDesigner;
				if (rootDesigner == null)
				{
					throw new InvalidOperationException("The DesignSurface isn't loaded.");
				}
				ViewTechnology[] supportedTechnologies = rootDesigner.SupportedTechnologies;
				for (int i = 0; i < supportedTechnologies.Length; i++)
				{
					try
					{
						return rootDesigner.GetView(supportedTechnologies[i]);
					}
					catch
					{
					}
				}
				throw new NotSupportedException("No supported View Technology found.");
			}
		}

		/// <summary>Occurs when the design surface is disposed.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600077E RID: 1918 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		// (remove) Token: 0x0600077F RID: 1919 RVA: 0x0000C81C File Offset: 0x0000AA1C
		public event EventHandler Disposed;

		/// <summary>Occurs when a call is made to the <see cref="M:System.ComponentModel.Design.DesignSurface.Flush" /> method of <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000780 RID: 1920 RVA: 0x0000C854 File Offset: 0x0000AA54
		// (remove) Token: 0x06000781 RID: 1921 RVA: 0x0000C88C File Offset: 0x0000AA8C
		public event EventHandler Flushed;

		/// <summary>Occurs when the designer load has completed.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000782 RID: 1922 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		// (remove) Token: 0x06000783 RID: 1923 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		public event LoadedEventHandler Loaded;

		/// <summary>Occurs when the designer is about to be loaded.</summary>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000784 RID: 1924 RVA: 0x0000C934 File Offset: 0x0000AB34
		// (remove) Token: 0x06000785 RID: 1925 RVA: 0x0000C96C File Offset: 0x0000AB6C
		public event EventHandler Loading;

		/// <summary>Occurs when a designer has finished unloading.</summary>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000786 RID: 1926 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		// (remove) Token: 0x06000787 RID: 1927 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		public event EventHandler Unloaded;

		/// <summary>Occurs when a designer is about to unload.</summary>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000788 RID: 1928 RVA: 0x0000CA14 File Offset: 0x0000AC14
		// (remove) Token: 0x06000789 RID: 1929 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public event EventHandler Unloading;

		/// <summary>Occurs when the <see cref="M:System.ComponentModel.Design.IDesignerHost.Activate" /> method has been called on <see cref="T:System.ComponentModel.Design.IDesignerHost" />.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600078A RID: 1930 RVA: 0x0000CA84 File Offset: 0x0000AC84
		// (remove) Token: 0x0600078B RID: 1931 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public event EventHandler ViewActivated;

		/// <summary>Begins the loading process.</summary>
		/// <param name="rootComponentType">The type of component to create in design mode.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="rootComponentType" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600078C RID: 1932 RVA: 0x0000CAF1 File Offset: 0x0000ACF1
		public void BeginLoad(Type rootComponentType)
		{
			if (rootComponentType == null)
			{
				throw new ArgumentNullException("rootComponentType");
			}
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			this.BeginLoad(new DesignSurface.DefaultDesignerLoader(rootComponentType));
		}

		/// <summary>Begins the loading process with the given designer loader.</summary>
		/// <param name="loader">The designer loader to use for loading the designer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="loader" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600078D RID: 1933 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public void BeginLoad(DesignerLoader loader)
		{
			if (loader == null)
			{
				throw new ArgumentNullException("loader");
			}
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			if (!this._isLoaded)
			{
				this._loadErrors = null;
				this._designerLoader = loader;
				this.OnLoading(EventArgs.Empty);
				this._designerLoader.BeginLoad(this._designerHost);
			}
		}

		/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
		// Token: 0x0600078E RID: 1934 RVA: 0x0000CB88 File Offset: 0x0000AD88
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the resources used by the <see cref="T:System.ComponentModel.Design.DesignSurface" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600078F RID: 1935 RVA: 0x0000CB94 File Offset: 0x0000AD94
		protected virtual void Dispose(bool disposing)
		{
			if (this._designerLoader != null)
			{
				this._designerLoader.Dispose();
				this._designerLoader = null;
			}
			if (this._designerHost != null)
			{
				this._designerHost.Dispose();
				this._designerHost.DesignerLoaderHostLoaded -= this.OnDesignerHost_Loaded;
				this._designerHost.DesignerLoaderHostLoading -= this.OnDesignerHost_Loading;
				this._designerHost.DesignerLoaderHostUnloading -= this.OnDesignerHost_Unloading;
				this._designerHost.DesignerLoaderHostUnloaded -= this.OnDesignerHost_Unloaded;
				this._designerHost.Activated -= this.OnDesignerHost_Activated;
				this._designerHost = null;
			}
			if (this._serviceContainer != null)
			{
				this._serviceContainer.Dispose();
				this._serviceContainer = null;
			}
			if (this.Disposed != null)
			{
				this.Disposed(this, EventArgs.Empty);
			}
		}

		/// <summary>Serializes changes to the design surface.</summary>
		// Token: 0x06000790 RID: 1936 RVA: 0x0000CC7E File Offset: 0x0000AE7E
		public void Flush()
		{
			if (this._designerLoader != null)
			{
				this._designerLoader.Flush();
			}
			if (this.Flushed != null)
			{
				this.Flushed(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0000CCAC File Offset: 0x0000AEAC
		private void OnDesignerHost_Loaded(object sender, LoadedEventArgs e)
		{
			this.OnLoaded(e);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0000CCB5 File Offset: 0x0000AEB5
		private void OnDesignerHost_Loading(object sender, EventArgs e)
		{
			this.OnLoading(EventArgs.Empty);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0000CCC2 File Offset: 0x0000AEC2
		private void OnDesignerHost_Unloading(object sender, EventArgs e)
		{
			this.OnUnloading(EventArgs.Empty);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000CCCF File Offset: 0x0000AECF
		private void OnDesignerHost_Unloaded(object sender, EventArgs e)
		{
			this.OnUnloaded(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Loaded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Design.LoadedEventArgs" /> that contains the event data.</param>
		// Token: 0x06000795 RID: 1941 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		protected virtual void OnLoaded(LoadedEventArgs e)
		{
			this._loadErrors = e.Errors;
			this._isLoaded = e.HasSucceeded;
			if (this.Loaded != null)
			{
				this.Loaded(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Loading" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000796 RID: 1942 RVA: 0x0000CD0B File Offset: 0x0000AF0B
		protected virtual void OnLoading(EventArgs e)
		{
			if (this.Loading != null)
			{
				this.Loading(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Unloaded" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000797 RID: 1943 RVA: 0x0000CD22 File Offset: 0x0000AF22
		protected virtual void OnUnloaded(EventArgs e)
		{
			if (this.Unloaded != null)
			{
				this.Unloaded(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.Unloading" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000798 RID: 1944 RVA: 0x0000CD39 File Offset: 0x0000AF39
		protected virtual void OnUnloading(EventArgs e)
		{
			if (this.Unloading != null)
			{
				this.Unloading(this, e);
			}
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000CD50 File Offset: 0x0000AF50
		internal void OnDesignerHost_Activated(object sender, EventArgs args)
		{
			this.OnViewActivate(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Design.DesignSurface.ViewActivated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600079A RID: 1946 RVA: 0x0000CD5D File Offset: 0x0000AF5D
		protected virtual void OnViewActivate(EventArgs e)
		{
			if (this.ViewActivated != null)
			{
				this.ViewActivated(this, e);
			}
		}

		/// <summary>Creates an instance of a component.</summary>
		/// <returns>The newly created component.</returns>
		/// <param name="componentType">The type of component to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="componentType" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079B RID: 1947 RVA: 0x0000CD74 File Offset: 0x0000AF74
		[Obsolete("CreateComponent has been replaced by CreateInstance")]
		protected internal virtual IComponent CreateComponent(Type componentType)
		{
			return this.CreateInstance(componentType) as IComponent;
		}

		/// <summary>Creates an instance of the given type.</summary>
		/// <returns>The newly created object.</returns>
		/// <param name="type">The type to create.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079C RID: 1948 RVA: 0x0000CD82 File Offset: 0x0000AF82
		protected internal virtual object CreateInstance(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._designerHost.CreateComponent(type);
		}

		/// <summary>Creates a designer when a component is added to the container.</summary>
		/// <returns>An instance of the requested designer, or null if no matching designer could be found.</returns>
		/// <param name="component">The component for which the designer should be created.</param>
		/// <param name="rootDesigner">true to create a root designer; false to create a normal designer.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079D RID: 1949 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		protected internal virtual IDesigner CreateDesigner(IComponent component, bool rootDesigner)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignerSurface");
			}
			return this._designerHost.CreateDesigner(component, rootDesigner);
		}

		/// <summary>Creates a container suitable for nesting controls or components.</summary>
		/// <returns>The nested container.</returns>
		/// <param name="owningComponent">The component that manages the nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owningComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079E RID: 1950 RVA: 0x0000CDD4 File Offset: 0x0000AFD4
		public INestedContainer CreateNestedContainer(IComponent owningComponent)
		{
			return this.CreateNestedContainer(owningComponent, null);
		}

		/// <summary>Creates a container suitable for nesting controls or components.</summary>
		/// <returns>The nested container.</returns>
		/// <param name="owningComponent">The component that manages the nested container.</param>
		/// <param name="containerName">An additional name for the nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owningComponent" /> is null.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Design.IDesignerHost" />  attached to the <see cref="T:System.ComponentModel.Design.DesignSurface" />  has been disposed.</exception>
		// Token: 0x0600079F RID: 1951 RVA: 0x0000CDDE File Offset: 0x0000AFDE
		public INestedContainer CreateNestedContainer(IComponent owningComponent, string containerName)
		{
			if (this._designerHost == null)
			{
				throw new ObjectDisposedException("DesignSurface");
			}
			return new DesignModeNestedContainer(owningComponent, containerName);
		}

		/// <summary>Gets a service from the service container.</summary>
		/// <returns>An object that implements, or is a derived class of, <paramref name="serviceType" />, or null if the service does not exist in the service container.</returns>
		/// <param name="serviceType">The type of service to retrieve. </param>
		// Token: 0x060007A0 RID: 1952 RVA: 0x0000CDFA File Offset: 0x0000AFFA
		public object GetService(Type serviceType)
		{
			if (typeof(IServiceContainer) == serviceType)
			{
				return this._serviceContainer;
			}
			return this._serviceContainer.GetService(serviceType);
		}

		/// <summary>Gets a value indicating whether the Design-time Error List is loading. </summary>
		/// <returns>true if the Design-time Error List is loading; otherwise, false. </returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000CE24 File Offset: 0x0000B024
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x00009525 File Offset: 0x00007725
		public bool DtelLoading
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04000192 RID: 402
		private DesignerHost _designerHost;

		// Token: 0x04000193 RID: 403
		private DesignSurfaceServiceContainer _serviceContainer;

		// Token: 0x04000194 RID: 404
		private ICollection _loadErrors;

		// Token: 0x04000195 RID: 405
		private bool _isLoaded;

		// Token: 0x04000196 RID: 406
		private DesignerLoader _designerLoader;

		// Token: 0x02000105 RID: 261
		internal class DefaultDesignerLoader : DesignerLoader
		{
			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000CE3F File Offset: 0x0000B03F
			public override bool Loading
			{
				get
				{
					return this._loading;
				}
			}

			// Token: 0x060007A4 RID: 1956 RVA: 0x0000CE47 File Offset: 0x0000B047
			public DefaultDesignerLoader(Type componentType)
			{
				if (componentType == null)
				{
					throw new ArgumentNullException("componentType");
				}
				this._componentType = componentType;
			}

			// Token: 0x060007A5 RID: 1957 RVA: 0x0000CE6A File Offset: 0x0000B06A
			public override void BeginLoad(IDesignerLoaderHost loaderHost)
			{
				this._loading = true;
				loaderHost.CreateComponent(this._componentType);
				loaderHost.EndLoad(this._componentType.FullName, true, null);
				this._loading = false;
			}

			// Token: 0x060007A6 RID: 1958 RVA: 0x0000CE9A File Offset: 0x0000B09A
			public override void Dispose()
			{
				this._componentType = null;
			}

			// Token: 0x0400019E RID: 414
			private Type _componentType;

			// Token: 0x0400019F RID: 415
			private bool _loading;
		}
	}
}
