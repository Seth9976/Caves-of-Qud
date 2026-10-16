using System;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Manages the path context for the current Web application. This class cannot be inherited.</summary>
	// Token: 0x020005EE RID: 1518
	public sealed class WebContext
	{
		// Token: 0x060041F7 RID: 16887 RVA: 0x000AA94D File Offset: 0x000A8B4D
		public WebContext(WebApplicationLevel pathLevel, string site, string applicationPath, string path, string locationSubPath)
		{
			this.pathLevel = pathLevel;
			this.site = site;
			this.applicationPath = applicationPath;
			this.path = path;
			this.locationSubPath = locationSubPath;
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object that represents the path level of the current Web application.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object.</returns>
		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x060041F8 RID: 16888 RVA: 0x000AA97A File Offset: 0x000A8B7A
		public WebApplicationLevel ApplicationLevel
		{
			get
			{
				return this.pathLevel;
			}
		}

		/// <summary>Gets the application path of the current Web application.</summary>
		/// <returns>The application path of the current Web application.</returns>
		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x060041F9 RID: 16889 RVA: 0x000AA982 File Offset: 0x000A8B82
		public string ApplicationPath
		{
			get
			{
				return this.applicationPath;
			}
		}

		/// <summary>Gets the location subpath of the Web application.</summary>
		/// <returns>The location subpath of the current Web application.</returns>
		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x060041FA RID: 16890 RVA: 0x000AA98A File Offset: 0x000A8B8A
		public string LocationSubPath
		{
			get
			{
				return this.locationSubPath;
			}
		}

		/// <summary>Gets the current virtual path of the Web application.</summary>
		/// <returns>The current virtual path of the Web application.</returns>
		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x060041FB RID: 16891 RVA: 0x000AA992 File Offset: 0x000A8B92
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		/// <summary>Gets the name of the current Web application.</summary>
		/// <returns>The name of the current Web application.</returns>
		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x060041FC RID: 16892 RVA: 0x000AA99A File Offset: 0x000A8B9A
		public string Site
		{
			get
			{
				return this.site;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.WebContext" /> class.</summary>
		/// <param name="pathLevel">A <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object.</param>
		/// <param name="site">The name of the Web site.</param>
		/// <param name="applicationPath">The virtual path of the root level of the current Web application.</param>
		/// <param name="path">The virtual path of the Web.config file that the current configuration object represents.</param>
		/// <param name="locationSubPath">The path value of the location element that is currently being edited.</param>
		/// <param name="appConfigPath">The current Web application's configuration path.</param>
		// Token: 0x060041FD RID: 16893 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebContext(WebApplicationLevel pathLevel, string site, string applicationPath, string path, string locationSubPath, string appConfigPath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400235F RID: 9055
		private WebApplicationLevel pathLevel;

		// Token: 0x04002360 RID: 9056
		private string site;

		// Token: 0x04002361 RID: 9057
		private string applicationPath;

		// Token: 0x04002362 RID: 9058
		private string path;

		// Token: 0x04002363 RID: 9059
		private string locationSubPath;
	}
}
