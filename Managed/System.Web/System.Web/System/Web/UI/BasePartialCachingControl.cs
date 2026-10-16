using System;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Caching;

namespace System.Web.UI
{
	/// <summary>Provides the base functionality for the <see cref="T:System.Web.UI.StaticPartialCachingControl" /> and <see cref="T:System.Web.UI.PartialCachingControl" /> classes.</summary>
	// Token: 0x0200019F RID: 415
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class BasePartialCachingControl : Control
	{
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00029944 File Offset: 0x00027B44
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0002994C File Offset: 0x00027B4C
		internal string CtrlID
		{
			get
			{
				return this.ctrl_id;
			}
			set
			{
				this.ctrl_id = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00029955 File Offset: 0x00027B55
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0002995D File Offset: 0x00027B5D
		internal string Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00029966 File Offset: 0x00027B66
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0002996E File Offset: 0x00027B6E
		internal int Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x00029977 File Offset: 0x00027B77
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0002997F File Offset: 0x00027B7F
		internal string VaryByParams
		{
			get
			{
				return this.varyby_params;
			}
			set
			{
				this.varyby_params = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00029988 File Offset: 0x00027B88
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x00029990 File Offset: 0x00027B90
		internal string VaryByControls
		{
			get
			{
				return this.varyby_controls;
			}
			set
			{
				this.varyby_controls = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00029999 File Offset: 0x00027B99
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x000299A1 File Offset: 0x00027BA1
		internal string VaryByCustom
		{
			get
			{
				return this.varyby_custom;
			}
			set
			{
				this.varyby_custom = value;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000299AA File Offset: 0x00027BAA
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x000299B2 File Offset: 0x00027BB2
		internal DateTime ExpirationTime
		{
			get
			{
				return this.expirationTime;
			}
			set
			{
				this.expirationTime = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x000299BB File Offset: 0x00027BBB
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x000299C3 File Offset: 0x00027BC3
		internal bool SlidingExpiration
		{
			get
			{
				return this.slidingExpiration;
			}
			set
			{
				this.slidingExpiration = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x000299CC File Offset: 0x00027BCC
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x000299D4 File Offset: 0x00027BD4
		internal string ProviderName { get; set; }

		// Token: 0x06000FE6 RID: 4070
		internal abstract Control CreateControl();

		/// <summary>Releases all resources used by the <see cref="T:System.Web.UI.BasePartialCachingControl" /> class. </summary>
		// Token: 0x06000FE7 RID: 4071 RVA: 0x000299DD File Offset: 0x00027BDD
		public override void Dispose()
		{
			if (this.dependency != null)
			{
				this.dependency.Dispose();
				this.dependency = null;
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000299FC File Offset: 0x00027BFC
		private void RetrieveCachedContents()
		{
			this.cacheKey = this.CreateKey();
			OutputCacheProvider provider = this.GetProvider();
			this.cachedData = provider.Get(this.cacheKey) as string;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00029A34 File Offset: 0x00027C34
		private OutputCacheProvider GetProvider()
		{
			string providerName = this.ProviderName;
			OutputCacheProvider outputCacheProvider;
			if (string.IsNullOrEmpty(providerName))
			{
				outputCacheProvider = OutputCache.DefaultProvider;
			}
			else
			{
				outputCacheProvider = OutputCache.GetProvider(providerName);
				if (outputCacheProvider == null)
				{
					outputCacheProvider = OutputCache.DefaultProvider;
				}
			}
			return outputCacheProvider;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00029A69 File Offset: 0x00027C69
		private void OnDependencyChanged(string key, object value, CacheItemRemovedReason reason)
		{
			Console.WriteLine("{0}.OnDependencyChanged (\"{0}\", {1}, {2})", new object[] { this, key, value, reason });
			this.GetProvider().Remove(key);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00029A9C File Offset: 0x00027C9C
		internal override void InitRecursive(Control namingContainer)
		{
			this.RetrieveCachedContents();
			if (this.cachedData == null)
			{
				this.control = this.CreateControl();
				this.Controls.Add(this.control);
			}
			else
			{
				this.control = null;
			}
			base.InitRecursive(namingContainer);
		}

		/// <summary>Outputs the user control's content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> output stream.</summary>
		/// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that writes the cached control to the page.</param>
		// Token: 0x06000FEC RID: 4076 RVA: 0x00029ADC File Offset: 0x00027CDC
		protected internal override void Render(HtmlTextWriter output)
		{
			if (this.cachedData != null)
			{
				output.Write(this.cachedData);
				return;
			}
			if (this.control == null)
			{
				base.Render(output);
				return;
			}
			HttpContext httpContext = HttpContext.Current;
			StringWriter stringWriter = new StringWriter();
			TextWriter textWriter = httpContext.Response.SetTextWriter(stringWriter);
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			string text;
			try
			{
				this.control.RenderControl(htmlTextWriter);
			}
			finally
			{
				text = stringWriter.ToString();
				httpContext.Response.SetTextWriter(textWriter);
				output.Write(text);
			}
			OutputCacheProvider provider = this.GetProvider();
			DateTime dateTime = DateTime.UtcNow.AddSeconds((double)this.duration);
			provider.Set(this.cacheKey, text, dateTime);
			httpContext.InternalCache.Insert(this.cacheKey, text, this.dependency, dateTime.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.ControlCachePolicy" /> object that is associated with the wrapped user control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlCachePolicy" /> that stores output caching-related properties of the wrapped user control.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00029BC0 File Offset: 0x00027DC0
		public ControlCachePolicy CachePolicy
		{
			get
			{
				if (this.cachePolicy == null)
				{
					this.cachePolicy = new ControlCachePolicy(this);
				}
				return this.cachePolicy;
			}
		}

		/// <summary>Gets or sets an instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class associated with the cached user control output.</summary>
		/// <returns>The <see cref="T:System.Web.Caching.CacheDependency" /> associated with the server control.</returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00029BDC File Offset: 0x00027DDC
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x00029BE4 File Offset: 0x00027DE4
		public CacheDependency Dependency
		{
			get
			{
				return this.dependency;
			}
			set
			{
				this.dependency = value;
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00029BF0 File Offset: 0x00027DF0
		private string CreateKey()
		{
			StringBuilder stringBuilder = new StringBuilder();
			HttpContext httpContext = HttpContext.Current;
			stringBuilder.Append("PartialCachingControl\n");
			stringBuilder.Append("GUID: " + this.guid + "\n");
			if (this.varyby_params != null && this.varyby_params.Length > 0)
			{
				string[] array = this.varyby_params.Split(';', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					string text = httpContext.Request.Params[array[i]];
					stringBuilder.Append("VP:");
					stringBuilder.Append(array[i]);
					stringBuilder.Append('=');
					stringBuilder.Append((text != null) ? text : "__null__");
					stringBuilder.Append('\n');
				}
			}
			if (this.varyby_controls != null && this.varyby_params.Length > 0)
			{
				string[] array2 = this.varyby_controls.Split(';', StringSplitOptions.None);
				for (int j = 0; j < array2.Length; j++)
				{
					string text2 = httpContext.Request.Params[array2[j]];
					stringBuilder.Append("VCN:");
					stringBuilder.Append(array2[j]);
					stringBuilder.Append('=');
					stringBuilder.Append((text2 != null) ? text2 : "__null__");
					stringBuilder.Append('\n');
				}
			}
			if (this.varyby_custom != null)
			{
				string varyByCustomString = httpContext.ApplicationInstance.GetVaryByCustomString(httpContext, this.varyby_custom);
				stringBuilder.Append("VC:");
				stringBuilder.Append(this.varyby_custom);
				stringBuilder.Append('=');
				stringBuilder.Append((varyByCustomString != null) ? varyByCustomString : "__null__");
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001348 RID: 4936
		private CacheDependency dependency;

		// Token: 0x04001349 RID: 4937
		private string ctrl_id;

		// Token: 0x0400134A RID: 4938
		private string guid;

		// Token: 0x0400134B RID: 4939
		private int duration;

		// Token: 0x0400134C RID: 4940
		private string varyby_params;

		// Token: 0x0400134D RID: 4941
		private string varyby_controls;

		// Token: 0x0400134E RID: 4942
		private string varyby_custom;

		// Token: 0x0400134F RID: 4943
		private DateTime expirationTime;

		// Token: 0x04001350 RID: 4944
		private bool slidingExpiration;

		// Token: 0x04001351 RID: 4945
		private Control control;

		// Token: 0x04001352 RID: 4946
		private ControlCachePolicy cachePolicy;

		// Token: 0x04001353 RID: 4947
		private string cacheKey;

		// Token: 0x04001354 RID: 4948
		private string cachedData;
	}
}
