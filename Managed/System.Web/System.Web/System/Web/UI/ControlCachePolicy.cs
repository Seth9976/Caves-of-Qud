using System;
using System.Web.Caching;

namespace System.Web.UI
{
	/// <summary>Provides programmatic access to an ASP.NET user control's output cache settings.</summary>
	// Token: 0x020001B2 RID: 434
	public sealed class ControlCachePolicy
	{
		// Token: 0x06001193 RID: 4499 RVA: 0x0002EB1F File Offset: 0x0002CD1F
		internal ControlCachePolicy()
			: this(null)
		{
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0002EB28 File Offset: 0x0002CD28
		internal ControlCachePolicy(BasePartialCachingControl bpcc)
		{
			this.bpcc = bpcc;
		}

		/// <summary>Gets or sets a value indicating whether fragment caching is enabled for the user control.</summary>
		/// <returns>true if the user control's output is cached; otherwise, false.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Cached" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0002EB37 File Offset: 0x0002CD37
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x0002EB45 File Offset: 0x0002CD45
		public bool Cached
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.cached;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.cached = value;
			}
		}

		/// <summary>Gets or sets an instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class associated with the cached user control output.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.CacheDependency" /> associated with the control. The default is null.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Dependency" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0002EB54 File Offset: 0x0002CD54
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x0002EB67 File Offset: 0x0002CD67
		public CacheDependency Dependency
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.bpcc.Dependency;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.Dependency = value;
			}
		}

		/// <summary>Gets or sets the amount of time that cached items are to remain in the output cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that represents the amount of time a user control is to remain in the output cache. The default is <see cref="F:System.TimeSpan.Zero" />.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Duration" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x0002EB7B File Offset: 0x0002CD7B
		// (set) Token: 0x0600119A RID: 4506 RVA: 0x0002EB94 File Offset: 0x0002CD94
		public TimeSpan Duration
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return TimeSpan.FromMinutes((double)this.bpcc.Duration);
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.Duration = value.Minutes;
			}
		}

		/// <summary>Gets or sets the name of the output-cache provider that is associated with a control instance.</summary>
		/// <returns>The name of the provider.</returns>
		/// <exception cref="T:System.Configuration.Provider.ProviderException">The provider name was not found.</exception>
		/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.UI.ControlCachePolicy.ProviderName" /> property during or after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x0600119B RID: 4507 RVA: 0x0002EBAE File Offset: 0x0002CDAE
		// (set) Token: 0x0600119C RID: 4508 RVA: 0x0002EBC1 File Offset: 0x0002CDC1
		public string ProviderName
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.bpcc.ProviderName;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.ProviderName = value;
			}
		}

		/// <summary>Gets a value indicating whether the user control supports caching.</summary>
		/// <returns>true if the user control supports caching; otherwise, false.</returns>
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x0600119D RID: 4509 RVA: 0x0002EBD5 File Offset: 0x0002CDD5
		public bool SupportsCaching
		{
			get
			{
				return this.bpcc != null;
			}
		}

		/// <summary>Gets or sets a list of control identifiers to vary the cached output by.</summary>
		/// <returns>A semicolon-separated list of strings used to vary a user control's output cache. These strings represent the <see cref="P:System.Web.UI.Control.ID" /> property values of ASP.NET server controls declared in the user control.</returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.VaryByControl" /> property is set outside of the initialization and rendering stages of the control.</exception>
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x0002EBE0 File Offset: 0x0002CDE0
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x0002EBF3 File Offset: 0x0002CDF3
		public string VaryByControl
		{
			get
			{
				this.AssertBasePartialCachingControl();
				return this.bpcc.VaryByControls;
			}
			set
			{
				this.AssertBasePartialCachingControl();
				this.bpcc.VaryByControls = value;
			}
		}

		/// <summary>Gets or sets a list of GET or POST parameter names to vary the cached output by. </summary>
		/// <returns>A semicolon-separated list of strings used to vary the output cache. </returns>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x0002EC07 File Offset: 0x0002CE07
		public HttpCacheVaryByParams VaryByParams
		{
			get
			{
				this.AssertBasePartialCachingControl();
				throw new NotImplementedException();
			}
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.BasePartialCachingControl" /> control that wraps the user control to expire the cache entry at the specified date and time.</summary>
		/// <param name="expirationTime">A <see cref="T:System.DateTime" /> after which the cached entry expires.</param>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x060011A1 RID: 4513 RVA: 0x0002EC14 File Offset: 0x0002CE14
		public void SetExpires(DateTime expirationTime)
		{
			this.AssertBasePartialCachingControl();
			this.bpcc.ExpirationTime = expirationTime;
		}

		/// <summary>Instructs the <see cref="T:System.Web.UI.BasePartialCachingControl" /> control that wraps the user control to set the user control's cache entry to use sliding or absolute expiration. </summary>
		/// <param name="useSlidingExpiration">true to use sliding cache expiration instead of absolute expiration; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x060011A2 RID: 4514 RVA: 0x0002EC28 File Offset: 0x0002CE28
		public void SetSlidingExpiration(bool useSlidingExpiration)
		{
			this.AssertBasePartialCachingControl();
			this.bpcc.SlidingExpiration = useSlidingExpiration;
		}

		/// <summary>Sets a list of custom strings that the output cache will use to vary the user control.</summary>
		/// <param name="varyByCustom">The list of custom strings.</param>
		/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
		// Token: 0x060011A3 RID: 4515 RVA: 0x0002EC3C File Offset: 0x0002CE3C
		public void SetVaryByCustom(string varyByCustom)
		{
			this.AssertBasePartialCachingControl();
			this.bpcc.VaryByCustom = varyByCustom;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0002EC50 File Offset: 0x0002CE50
		private void AssertBasePartialCachingControl()
		{
			if (this.bpcc == null)
			{
				throw new HttpException("The user control is not associated with a 'BasePartialCachingControl' and is not cacheable.");
			}
		}

		// Token: 0x040013F7 RID: 5111
		private BasePartialCachingControl bpcc;

		// Token: 0x040013F8 RID: 5112
		private bool cached;
	}
}
