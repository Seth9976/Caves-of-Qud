using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x0200011F RID: 287
	internal sealed class DesignerHost : Container, IDesignerLoaderHost, IDesignerHost, IServiceContainer, IServiceProvider, IComponentChangeService
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		public DesignerHost(IServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			this._serviceProvider = serviceProvider;
			this._serviceContainer = serviceProvider.GetService(typeof(IServiceContainer)) as IServiceContainer;
			this._designers = new Hashtable();
			this._transactions = new Stack();
			this._loading = true;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		public override void Add(IComponent component, string name)
		{
			this.AddPreProcess(component, name);
			base.Add(component, name);
			this.AddPostProcess(component, name);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0000DD56 File Offset: 0x0000BF56
		internal void AddPreProcess(IComponent component, string name)
		{
			if (this.ComponentAdding != null)
			{
				this.ComponentAdding(this, new ComponentEventArgs(component));
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0000DD74 File Offset: 0x0000BF74
		internal void AddPostProcess(IComponent component, string name)
		{
			IDesigner designer;
			if (this._rootComponent == null)
			{
				this._rootComponent = component;
				designer = this.CreateDesigner(component, true);
			}
			else
			{
				designer = this.CreateDesigner(component, false);
			}
			if (designer != null)
			{
				this._designers[component] = designer;
				designer.Initialize(component);
			}
			else
			{
				IUIService iuiservice = this.GetService(typeof(IUIService)) as IUIService;
				if (iuiservice != null)
				{
					iuiservice.ShowError("Unable to load a designer for component type '" + component.GetType().Name + "'");
				}
				this.DestroyComponent(component);
			}
			if (component == this._rootComponent)
			{
				this.Activate();
			}
			if (component is IExtenderProvider)
			{
				IExtenderProviderService extenderProviderService = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
				if (extenderProviderService != null)
				{
					extenderProviderService.AddExtenderProvider((IExtenderProvider)component);
				}
			}
			if (this.ComponentAdded != null)
			{
				this.ComponentAdded(this, new ComponentEventArgs(component));
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000DE53 File Offset: 0x0000C053
		public override void Remove(IComponent component)
		{
			DesignerTransaction designerTransaction = this.CreateTransaction("Remove " + component.Site.Name);
			this.RemovePreProcess(component);
			base.Remove(component);
			this.RemovePostProcess(component);
			designerTransaction.Commit();
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000DE8C File Offset: 0x0000C08C
		internal void RemovePreProcess(IComponent component)
		{
			if (!this._unloading && this.ComponentRemoving != null)
			{
				this.ComponentRemoving(this, new ComponentEventArgs(component));
			}
			IDesigner designer = this._designers[component] as IDesigner;
			if (designer != null)
			{
				designer.Dispose();
			}
			this._designers.Remove(component);
			if (component == this._rootComponent)
			{
				this._rootComponent = null;
			}
			if (component is IExtenderProvider)
			{
				IExtenderProviderService extenderProviderService = this.GetService(typeof(IExtenderProviderService)) as IExtenderProviderService;
				if (extenderProviderService != null)
				{
					extenderProviderService.RemoveExtenderProvider((IExtenderProvider)component);
				}
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000DF1F File Offset: 0x0000C11F
		internal void RemovePostProcess(IComponent component)
		{
			if (!this._unloading && this.ComponentRemoved != null)
			{
				this.ComponentRemoved(this, new ComponentEventArgs(component));
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0000DF44 File Offset: 0x0000C144
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (name == null)
			{
				INameCreationService nameCreationService = this.GetService(typeof(INameCreationService)) as INameCreationService;
				if (nameCreationService != null)
				{
					name = nameCreationService.CreateName(this, component.GetType());
				}
			}
			return new DesignModeSite(component, name, this, this);
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0000DF85 File Offset: 0x0000C185
		public IContainer Container
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x0000DF88 File Offset: 0x0000C188
		public bool InTransaction
		{
			get
			{
				return this._transactions != null && this._transactions.Count > 0;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0000DFA3 File Offset: 0x0000C1A3
		public bool Loading
		{
			get
			{
				return this._loading;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0000DFAB File Offset: 0x0000C1AB
		public IComponent RootComponent
		{
			get
			{
				return this._rootComponent;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0000DFB3 File Offset: 0x0000C1B3
		public string RootComponentClassName
		{
			get
			{
				if (this._rootComponent != null)
				{
					return this._rootComponent.GetType().AssemblyQualifiedName;
				}
				return null;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0000DFCF File Offset: 0x0000C1CF
		public string TransactionDescription
		{
			get
			{
				if (this._transactions != null && this._transactions.Count > 0)
				{
					return ((DesignerHost.DesignerHostTransaction)this._transactions.Peek()).Description;
				}
				return null;
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0000E000 File Offset: 0x0000C200
		public void Activate()
		{
			ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
			if (selectionService != null)
			{
				selectionService.SetSelectedComponents(new IComponent[] { this._rootComponent });
			}
			if (this.Activated != null)
			{
				this.Activated(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0000E054 File Offset: 0x0000C254
		public IComponent CreateComponent(Type componentClass)
		{
			return this.CreateComponent(componentClass, null);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0000E060 File Offset: 0x0000C260
		public IComponent CreateComponent(Type componentClass, string name)
		{
			if (componentClass == null)
			{
				throw new ArgumentNullException("componentClass");
			}
			if (!typeof(IComponent).IsAssignableFrom(componentClass))
			{
				throw new ArgumentException("componentClass");
			}
			IComponent component = this.CreateInstance(componentClass) as IComponent;
			this.Add(component, name);
			return component;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		internal object CreateInstance(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		internal IDesigner CreateDesigner(IComponent component, bool rootDesigner)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (rootDesigner)
			{
				return this.CreateDesigner(component, typeof(IRootDesigner));
			}
			return this.CreateDesigner(component, typeof(IDesigner));
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0000E110 File Offset: 0x0000C310
		private IDesigner CreateDesigner(IComponent component, Type designerBaseType)
		{
			IDesigner designer = null;
			foreach (object obj in TypeDescriptor.GetAttributes(component))
			{
				DesignerAttribute designerAttribute = ((Attribute)obj) as DesignerAttribute;
				if (designerAttribute != null && (designerBaseType.FullName == designerAttribute.DesignerBaseTypeName || designerBaseType.AssemblyQualifiedName == designerAttribute.DesignerBaseTypeName))
				{
					Type type = Type.GetType(designerAttribute.DesignerTypeName);
					if (type == null && designerBaseType == typeof(IRootDesigner))
					{
						type = typeof(DocumentDesigner);
					}
					if (type != null)
					{
						designer = (IDesigner)Activator.CreateInstance(type);
						break;
					}
					break;
				}
			}
			if (designer == null)
			{
				Type type2 = component.GetType().BaseType;
				do
				{
					foreach (object obj2 in TypeDescriptor.GetAttributes(type2))
					{
						DesignerAttribute designerAttribute2 = ((Attribute)obj2) as DesignerAttribute;
						if (designerAttribute2 != null && (designerBaseType.FullName == designerAttribute2.DesignerBaseTypeName || designerBaseType.AssemblyQualifiedName == designerAttribute2.DesignerBaseTypeName))
						{
							Type type3 = Type.GetType(designerAttribute2.DesignerTypeName);
							if (type3 != null)
							{
								designer = (IDesigner)Activator.CreateInstance(type3);
								break;
							}
							break;
						}
					}
					type2 = type2.BaseType;
				}
				while (designer == null && type2 != null);
			}
			return designer;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		public void DestroyComponent(IComponent component)
		{
			if (component.Site != null && component.Site.Container == this)
			{
				this.Remove(component);
				component.Dispose();
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000E2D5 File Offset: 0x0000C4D5
		public IDesigner GetDesigner(IComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return this._designers[component] as IDesigner;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000E2F6 File Offset: 0x0000C4F6
		public DesignerTransaction CreateTransaction()
		{
			return this.CreateTransaction(null);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000E300 File Offset: 0x0000C500
		public DesignerTransaction CreateTransaction(string description)
		{
			if (this.TransactionOpening != null)
			{
				this.TransactionOpening(this, EventArgs.Empty);
			}
			DesignerHost.DesignerHostTransaction designerHostTransaction = new DesignerHost.DesignerHostTransaction(this, description);
			this._transactions.Push(designerHostTransaction);
			if (this.TransactionOpened != null)
			{
				this.TransactionOpened(this, EventArgs.Empty);
			}
			return designerHostTransaction;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000E354 File Offset: 0x0000C554
		public Type GetType(string typeName)
		{
			ITypeResolutionService typeResolutionService = this.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService;
			Type type;
			if (typeResolutionService != null)
			{
				type = typeResolutionService.GetType(typeName);
			}
			else
			{
				type = Type.GetType(typeName);
			}
			return type;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0000E38C File Offset: 0x0000C58C
		protected override void Dispose(bool disposing)
		{
			this.Unload();
			base.Dispose(disposing);
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x0600086B RID: 2155 RVA: 0x0000E39C File Offset: 0x0000C59C
		// (remove) Token: 0x0600086C RID: 2156 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		public event EventHandler Activated;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600086D RID: 2157 RVA: 0x0000E40C File Offset: 0x0000C60C
		// (remove) Token: 0x0600086E RID: 2158 RVA: 0x0000E444 File Offset: 0x0000C644
		public event EventHandler Deactivated;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600086F RID: 2159 RVA: 0x0000E47C File Offset: 0x0000C67C
		// (remove) Token: 0x06000870 RID: 2160 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		public event EventHandler LoadComplete;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000871 RID: 2161 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		// (remove) Token: 0x06000872 RID: 2162 RVA: 0x0000E524 File Offset: 0x0000C724
		public event DesignerTransactionCloseEventHandler TransactionClosed;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000873 RID: 2163 RVA: 0x0000E55C File Offset: 0x0000C75C
		// (remove) Token: 0x06000874 RID: 2164 RVA: 0x0000E594 File Offset: 0x0000C794
		public event DesignerTransactionCloseEventHandler TransactionClosing;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000875 RID: 2165 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		// (remove) Token: 0x06000876 RID: 2166 RVA: 0x0000E604 File Offset: 0x0000C804
		public event EventHandler TransactionOpened;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000877 RID: 2167 RVA: 0x0000E63C File Offset: 0x0000C83C
		// (remove) Token: 0x06000878 RID: 2168 RVA: 0x0000E674 File Offset: 0x0000C874
		public event EventHandler TransactionOpening;

		// Token: 0x06000879 RID: 2169 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		private void OnTransactionClosing(DesignerHost.DesignerHostTransaction raiser, DesignerHost.TransactionAction action)
		{
			bool flag = false;
			bool flag2 = false;
			if (this._transactions.Peek() != raiser)
			{
				throw new InvalidOperationException("Current transaction differs from the one a commit was requested for.");
			}
			if (this._transactions.Count == 1)
			{
				flag2 = true;
			}
			if (action == DesignerHost.TransactionAction.Commit)
			{
				flag = true;
			}
			if (this.TransactionClosing != null)
			{
				this.TransactionClosing(this, new DesignerTransactionCloseEventArgs(flag, flag2));
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000E708 File Offset: 0x0000C908
		private void OnTransactionClosed(DesignerHost.DesignerHostTransaction raiser, DesignerHost.TransactionAction action)
		{
			bool flag = false;
			bool flag2 = false;
			if (this._transactions.Peek() != raiser)
			{
				throw new InvalidOperationException("Current transaction differs from the one a commit was requested for.");
			}
			if (this._transactions.Count == 1)
			{
				flag2 = true;
			}
			if (action == DesignerHost.TransactionAction.Commit)
			{
				flag = true;
			}
			this._transactions.Pop();
			if (this.TransactionClosed != null)
			{
				this.TransactionClosed(this, new DesignerTransactionCloseEventArgs(flag, flag2));
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600087B RID: 2171 RVA: 0x0000E770 File Offset: 0x0000C970
		// (remove) Token: 0x0600087C RID: 2172 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		internal event LoadedEventHandler DesignerLoaderHostLoaded;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x0600087D RID: 2173 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		// (remove) Token: 0x0600087E RID: 2174 RVA: 0x0000E818 File Offset: 0x0000CA18
		internal event EventHandler DesignerLoaderHostLoading;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600087F RID: 2175 RVA: 0x0000E850 File Offset: 0x0000CA50
		// (remove) Token: 0x06000880 RID: 2176 RVA: 0x0000E888 File Offset: 0x0000CA88
		internal event EventHandler DesignerLoaderHostUnloading;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000881 RID: 2177 RVA: 0x0000E8C0 File Offset: 0x0000CAC0
		// (remove) Token: 0x06000882 RID: 2178 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		internal event EventHandler DesignerLoaderHostUnloaded;

		// Token: 0x06000883 RID: 2179 RVA: 0x0000E92D File Offset: 0x0000CB2D
		public void EndLoad(string rootClassName, bool successful, ICollection errorCollection)
		{
			if (this.DesignerLoaderHostLoaded != null)
			{
				this.DesignerLoaderHostLoaded(this, new LoadedEventArgs(successful, errorCollection));
			}
			if (this.LoadComplete != null)
			{
				this.LoadComplete(this, EventArgs.Empty);
			}
			this._loading = false;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0000E96A File Offset: 0x0000CB6A
		public void Reload()
		{
			this._loading = true;
			this.Unload();
			if (this.DesignerLoaderHostLoading != null)
			{
				this.DesignerLoaderHostLoading(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0000E994 File Offset: 0x0000CB94
		private void Unload()
		{
			this._unloading = true;
			if (this.DesignerLoaderHostUnloading != null)
			{
				this.DesignerLoaderHostUnloading(this, EventArgs.Empty);
			}
			IComponent[] array = new IComponent[this.Components.Count];
			this.Components.CopyTo(array, 0);
			foreach (IComponent component in array)
			{
				this.Remove(component);
			}
			this._transactions.Clear();
			if (this.DesignerLoaderHostUnloaded != null)
			{
				this.DesignerLoaderHostUnloaded(this, EventArgs.Empty);
			}
			this._unloading = false;
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06000886 RID: 2182 RVA: 0x0000EA28 File Offset: 0x0000CC28
		// (remove) Token: 0x06000887 RID: 2183 RVA: 0x0000EA60 File Offset: 0x0000CC60
		public event ComponentEventHandler ComponentAdded;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000888 RID: 2184 RVA: 0x0000EA98 File Offset: 0x0000CC98
		// (remove) Token: 0x06000889 RID: 2185 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		public event ComponentEventHandler ComponentAdding;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600088A RID: 2186 RVA: 0x0000EB08 File Offset: 0x0000CD08
		// (remove) Token: 0x0600088B RID: 2187 RVA: 0x0000EB40 File Offset: 0x0000CD40
		public event ComponentChangedEventHandler ComponentChanged;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600088C RID: 2188 RVA: 0x0000EB78 File Offset: 0x0000CD78
		// (remove) Token: 0x0600088D RID: 2189 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		public event ComponentChangingEventHandler ComponentChanging;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600088E RID: 2190 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		// (remove) Token: 0x0600088F RID: 2191 RVA: 0x0000EC20 File Offset: 0x0000CE20
		public event ComponentEventHandler ComponentRemoved;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x06000890 RID: 2192 RVA: 0x0000EC58 File Offset: 0x0000CE58
		// (remove) Token: 0x06000891 RID: 2193 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public event ComponentEventHandler ComponentRemoving;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000892 RID: 2194 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		// (remove) Token: 0x06000893 RID: 2195 RVA: 0x0000ED00 File Offset: 0x0000CF00
		public event ComponentRenameEventHandler ComponentRename;

		// Token: 0x06000894 RID: 2196 RVA: 0x0000ED35 File Offset: 0x0000CF35
		public void OnComponentChanged(object component, MemberDescriptor member, object oldValue, object newValue)
		{
			if (this.ComponentChanged != null)
			{
				this.ComponentChanged(this, new ComponentChangedEventArgs(component, member, oldValue, newValue));
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0000ED55 File Offset: 0x0000CF55
		public void OnComponentChanging(object component, MemberDescriptor member)
		{
			if (this.ComponentChanging != null)
			{
				this.ComponentChanging(this, new ComponentChangingEventArgs(component, member));
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0000ED72 File Offset: 0x0000CF72
		internal void OnComponentRename(object component, string oldName, string newName)
		{
			if (this.ComponentRename != null)
			{
				this.ComponentRename(this, new ComponentRenameEventArgs(component, oldName, newName));
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public void AddService(Type serviceType, object serviceInstance)
		{
			this._serviceContainer.AddService(serviceType, serviceInstance);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		public void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			this._serviceContainer.AddService(serviceType, serviceInstance, promote);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0000EDAF File Offset: 0x0000CFAF
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this._serviceContainer.AddService(serviceType, callback);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0000EDBE File Offset: 0x0000CFBE
		public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			this._serviceContainer.AddService(serviceType, callback, promote);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0000EDCE File Offset: 0x0000CFCE
		public void RemoveService(Type serviceType)
		{
			this._serviceContainer.RemoveService(serviceType);
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public void RemoveService(Type serviceType, bool promote)
		{
			this._serviceContainer.RemoveService(serviceType, promote);
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0000EDEB File Offset: 0x0000CFEB
		public new object GetService(Type serviceType)
		{
			if (this._serviceProvider != null)
			{
				return this._serviceProvider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x040001D0 RID: 464
		private IServiceProvider _serviceProvider;

		// Token: 0x040001D1 RID: 465
		private Hashtable _designers;

		// Token: 0x040001D2 RID: 466
		private Stack _transactions;

		// Token: 0x040001D3 RID: 467
		private IServiceContainer _serviceContainer;

		// Token: 0x040001D4 RID: 468
		private bool _loading;

		// Token: 0x040001D5 RID: 469
		private bool _unloading;

		// Token: 0x040001D6 RID: 470
		private IComponent _rootComponent;

		// Token: 0x02000120 RID: 288
		private enum TransactionAction
		{
			// Token: 0x040001EA RID: 490
			Commit,
			// Token: 0x040001EB RID: 491
			Cancel
		}

		// Token: 0x02000121 RID: 289
		private sealed class DesignerHostTransaction : DesignerTransaction
		{
			// Token: 0x0600089E RID: 2206 RVA: 0x0000EE03 File Offset: 0x0000D003
			public DesignerHostTransaction(DesignerHost host, string description)
				: base(description)
			{
				this._designerHost = host;
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x0000EE13 File Offset: 0x0000D013
			protected override void OnCancel()
			{
				this._designerHost.OnTransactionClosing(this, DesignerHost.TransactionAction.Cancel);
				this._designerHost.OnTransactionClosed(this, DesignerHost.TransactionAction.Cancel);
			}

			// Token: 0x060008A0 RID: 2208 RVA: 0x0000EE2F File Offset: 0x0000D02F
			protected override void OnCommit()
			{
				this._designerHost.OnTransactionClosing(this, DesignerHost.TransactionAction.Commit);
				this._designerHost.OnTransactionClosed(this, DesignerHost.TransactionAction.Commit);
			}

			// Token: 0x040001EC RID: 492
			private DesignerHost _designerHost;
		}
	}
}
