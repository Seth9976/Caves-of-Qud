using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides the base implementation for the <see cref="T:System.ComponentModel.INestedContainer" /> interface, which enables containers to have an owning component.</summary>
	// Token: 0x020003DE RID: 990
	public class NestedContainer : Container, INestedContainer, IContainer, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NestedContainer" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.</exception>
		// Token: 0x0600202E RID: 8238 RVA: 0x0006F73A File Offset: 0x0006D93A
		public NestedContainer(IComponent owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Owner = owner;
			this.Owner.Disposed += this.OnOwnerDisposed;
		}

		/// <summary>Gets the owning component for this nested container.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IComponent" /> that owns this nested container.</returns>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600202F RID: 8239 RVA: 0x0006F76E File Offset: 0x0006D96E
		public IComponent Owner { get; }

		/// <summary>Gets the name of the owning component.</summary>
		/// <returns>The name of the owning component.</returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x0006F778 File Offset: 0x0006D978
		protected virtual string OwnerName
		{
			get
			{
				string text = null;
				if (this.Owner != null && this.Owner.Site != null)
				{
					INestedSite nestedSite = this.Owner.Site as INestedSite;
					if (nestedSite != null)
					{
						text = nestedSite.FullName;
					}
					else
					{
						text = this.Owner.Site.Name;
					}
				}
				return text;
			}
		}

		/// <summary>Creates a site for the component within the container.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.ISite" />.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to create a site for.</param>
		/// <param name="name">The name to assign to <paramref name="component" />, or null to skip the name assignment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x06002031 RID: 8241 RVA: 0x0006F7CB File Offset: 0x0006D9CB
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new NestedContainer.Site(component, this, name);
		}

		/// <summary>Releases the resources used by the nested container.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06002032 RID: 8242 RVA: 0x0006F7E3 File Offset: 0x0006D9E3
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Owner.Disposed -= this.OnOwnerDisposed;
			}
			base.Dispose(disposing);
		}

		/// <summary>Gets the service object of the specified type, if it is available.</summary>
		/// <returns>An <see cref="T:System.Object" /> that implements the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="service">The <see cref="T:System.Type" /> of the service to retrieve.</param>
		// Token: 0x06002033 RID: 8243 RVA: 0x0006F806 File Offset: 0x0006DA06
		protected override object GetService(Type service)
		{
			if (service == typeof(INestedContainer))
			{
				return this;
			}
			return base.GetService(service);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x0006F823 File Offset: 0x0006DA23
		private void OnOwnerDisposed(object sender, EventArgs e)
		{
			base.Dispose();
		}

		// Token: 0x020003DF RID: 991
		private class Site : INestedSite, ISite, IServiceProvider
		{
			// Token: 0x06002035 RID: 8245 RVA: 0x0006F82B File Offset: 0x0006DA2B
			internal Site(IComponent component, NestedContainer container, string name)
			{
				this.Component = component;
				this.Container = container;
				this._name = name;
			}

			// Token: 0x17000693 RID: 1683
			// (get) Token: 0x06002036 RID: 8246 RVA: 0x0006F848 File Offset: 0x0006DA48
			public IComponent Component { get; }

			// Token: 0x17000694 RID: 1684
			// (get) Token: 0x06002037 RID: 8247 RVA: 0x0006F850 File Offset: 0x0006DA50
			public IContainer Container { get; }

			// Token: 0x06002038 RID: 8248 RVA: 0x0006F858 File Offset: 0x0006DA58
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return ((NestedContainer)this.Container).GetService(service);
				}
				return this;
			}

			// Token: 0x17000695 RID: 1685
			// (get) Token: 0x06002039 RID: 8249 RVA: 0x0006F880 File Offset: 0x0006DA80
			public bool DesignMode
			{
				get
				{
					IComponent owner = ((NestedContainer)this.Container).Owner;
					return owner != null && owner.Site != null && owner.Site.DesignMode;
				}
			}

			// Token: 0x17000696 RID: 1686
			// (get) Token: 0x0600203A RID: 8250 RVA: 0x0006F8B8 File Offset: 0x0006DAB8
			public string FullName
			{
				get
				{
					if (this._name != null)
					{
						string ownerName = ((NestedContainer)this.Container).OwnerName;
						string text = this._name;
						if (ownerName != null)
						{
							text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", ownerName, text);
						}
						return text;
					}
					return this._name;
				}
			}

			// Token: 0x17000697 RID: 1687
			// (get) Token: 0x0600203B RID: 8251 RVA: 0x0006F902 File Offset: 0x0006DB02
			// (set) Token: 0x0600203C RID: 8252 RVA: 0x0006F90A File Offset: 0x0006DB0A
			public string Name
			{
				get
				{
					return this._name;
				}
				set
				{
					if (value == null || this._name == null || !value.Equals(this._name))
					{
						((NestedContainer)this.Container).ValidateName(this.Component, value);
						this._name = value;
					}
				}
			}

			// Token: 0x04000FD7 RID: 4055
			private string _name;
		}
	}
}
