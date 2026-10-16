using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Encapsulates zero or more components.</summary>
	// Token: 0x02000412 RID: 1042
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Container : IContainer, IDisposable
	{
		// Token: 0x06002199 RID: 8601 RVA: 0x00072AC8 File Offset: 0x00070CC8
		~Container()
		{
			this.Dispose(false);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" />. The component is unnamed.</summary>
		/// <param name="component">The component to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600219A RID: 8602 RVA: 0x00072AF8 File Offset: 0x00070CF8
		public virtual void Add(IComponent component)
		{
			this.Add(component, null);
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Component" /> to the <see cref="T:System.ComponentModel.Container" /> and assigns it a name.</summary>
		/// <param name="component">The component to add. </param>
		/// <param name="name">The unique, case-insensitive name to assign to the component.-or- null, which leaves the component unnamed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x0600219B RID: 8603 RVA: 0x00072B04 File Offset: 0x00070D04
		public virtual void Add(IComponent component, string name)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site == null || site.Container != this)
					{
						if (this.sites == null)
						{
							this.sites = new ISite[4];
						}
						else
						{
							this.ValidateName(component, name);
							if (this.sites.Length == this.siteCount)
							{
								ISite[] array = new ISite[this.siteCount * 2];
								Array.Copy(this.sites, 0, array, 0, this.siteCount);
								this.sites = array;
							}
						}
						if (site != null)
						{
							site.Container.Remove(component);
						}
						ISite site2 = this.CreateSite(component, name);
						ISite[] array2 = this.sites;
						int num = this.siteCount;
						this.siteCount = num + 1;
						array2[num] = site2;
						component.Site = site2;
						this.components = null;
					}
				}
			}
		}

		/// <summary>Creates a site <see cref="T:System.ComponentModel.ISite" /> for the given <see cref="T:System.ComponentModel.IComponent" /> and assigns the given name to the site.</summary>
		/// <returns>The newly created site.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for. </param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment. </param>
		// Token: 0x0600219C RID: 8604 RVA: 0x00072BFC File Offset: 0x00070DFC
		protected virtual ISite CreateSite(IComponent component, string name)
		{
			return new Container.Site(component, this, name);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Container" />.</summary>
		// Token: 0x0600219D RID: 8605 RVA: 0x00072C06 File Offset: 0x00070E06
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Container" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600219E RID: 8606 RVA: 0x00072C18 File Offset: 0x00070E18
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				object obj = this.syncObj;
				lock (obj)
				{
					while (this.siteCount > 0)
					{
						ISite[] array = this.sites;
						int num = this.siteCount - 1;
						this.siteCount = num;
						object obj2 = array[num];
						((ISite)obj2).Component.Site = null;
						((ISite)obj2).Component.Dispose();
					}
					this.sites = null;
					this.components = null;
				}
			}
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> implementing the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve. </param>
		// Token: 0x0600219F RID: 8607 RVA: 0x00072C9C File Offset: 0x00070E9C
		protected virtual object GetService(Type service)
		{
			if (!(service == typeof(IContainer)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Gets all the components in the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <returns>A collection that contains the components in the <see cref="T:System.ComponentModel.Container" />.</returns>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060021A0 RID: 8608 RVA: 0x00072CB4 File Offset: 0x00070EB4
		public virtual ComponentCollection Components
		{
			get
			{
				object obj = this.syncObj;
				ComponentCollection componentCollection2;
				lock (obj)
				{
					if (this.components == null)
					{
						IComponent[] array = new IComponent[this.siteCount];
						for (int i = 0; i < this.siteCount; i++)
						{
							array[i] = this.sites[i].Component;
						}
						this.components = new ComponentCollection(array);
						if (this.filter == null && this.checkedFilter)
						{
							this.checkedFilter = false;
						}
					}
					if (!this.checkedFilter)
					{
						this.filter = this.GetService(typeof(ContainerFilterService)) as ContainerFilterService;
						this.checkedFilter = true;
					}
					if (this.filter != null)
					{
						ComponentCollection componentCollection = this.filter.FilterComponents(this.components);
						if (componentCollection != null)
						{
							this.components = componentCollection;
						}
					}
					componentCollection2 = this.components;
				}
				return componentCollection2;
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" />.</summary>
		/// <param name="component">The component to remove. </param>
		// Token: 0x060021A1 RID: 8609 RVA: 0x00072DA4 File Offset: 0x00070FA4
		public virtual void Remove(IComponent component)
		{
			this.Remove(component, false);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00072DB0 File Offset: 0x00070FB0
		private void Remove(IComponent component, bool preserveSite)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null && site.Container == this)
					{
						if (!preserveSite)
						{
							component.Site = null;
						}
						for (int i = 0; i < this.siteCount; i++)
						{
							if (this.sites[i] == site)
							{
								this.siteCount--;
								Array.Copy(this.sites, i + 1, this.sites, i, this.siteCount - i);
								this.sites[this.siteCount] = null;
								this.components = null;
								break;
							}
						}
					}
				}
			}
		}

		/// <summary>Removes a component from the <see cref="T:System.ComponentModel.Container" /> without setting <see cref="P:System.ComponentModel.IComponent.Site" /> to null.</summary>
		/// <param name="component">The component to remove.</param>
		// Token: 0x060021A3 RID: 8611 RVA: 0x00072E70 File Offset: 0x00071070
		protected void RemoveWithoutUnsiting(IComponent component)
		{
			this.Remove(component, true);
		}

		/// <summary>Determines whether the component name is unique for this container.</summary>
		/// <param name="component">The named component.</param>
		/// <param name="name">The component name to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not unique.</exception>
		// Token: 0x060021A4 RID: 8612 RVA: 0x00072E7C File Offset: 0x0007107C
		protected virtual void ValidateName(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (name != null)
			{
				for (int i = 0; i < Math.Min(this.siteCount, this.sites.Length); i++)
				{
					ISite site = this.sites[i];
					if (site != null && site.Name != null && string.Equals(site.Name, name, StringComparison.OrdinalIgnoreCase) && site.Component != component && ((InheritanceAttribute)TypeDescriptor.GetAttributes(site.Component)[typeof(InheritanceAttribute)]).InheritanceLevel != InheritanceLevel.InheritedReadOnly)
					{
						throw new ArgumentException(SR.GetString("Duplicate component name '{0}'.  Component names must be unique and case-insensitive.", new object[] { name }));
					}
				}
			}
		}

		// Token: 0x0400103B RID: 4155
		private ISite[] sites;

		// Token: 0x0400103C RID: 4156
		private int siteCount;

		// Token: 0x0400103D RID: 4157
		private ComponentCollection components;

		// Token: 0x0400103E RID: 4158
		private ContainerFilterService filter;

		// Token: 0x0400103F RID: 4159
		private bool checkedFilter;

		// Token: 0x04001040 RID: 4160
		private object syncObj = new object();

		// Token: 0x02000413 RID: 1043
		private class Site : ISite, IServiceProvider
		{
			// Token: 0x060021A6 RID: 8614 RVA: 0x00072F3E File Offset: 0x0007113E
			internal Site(IComponent component, Container container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x170006EF RID: 1775
			// (get) Token: 0x060021A7 RID: 8615 RVA: 0x00072F5B File Offset: 0x0007115B
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x170006F0 RID: 1776
			// (get) Token: 0x060021A8 RID: 8616 RVA: 0x00072F63 File Offset: 0x00071163
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x060021A9 RID: 8617 RVA: 0x00072F6B File Offset: 0x0007116B
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x170006F1 RID: 1777
			// (get) Token: 0x060021AA RID: 8618 RVA: 0x00003062 File Offset: 0x00001262
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170006F2 RID: 1778
			// (get) Token: 0x060021AB RID: 8619 RVA: 0x00072F8D File Offset: 0x0007118D
			// (set) Token: 0x060021AC RID: 8620 RVA: 0x00072F95 File Offset: 0x00071195
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (value == null || this.name == null || !value.Equals(this.name))
					{
						this.container.ValidateName(this.component, value);
						this.name = value;
					}
				}
			}

			// Token: 0x04001041 RID: 4161
			private IComponent component;

			// Token: 0x04001042 RID: 4162
			private Container container;

			// Token: 0x04001043 RID: 4163
			private string name;
		}
	}
}
