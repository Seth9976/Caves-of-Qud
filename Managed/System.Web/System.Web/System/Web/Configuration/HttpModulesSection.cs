using System;
using System.Configuration;
using System.Reflection;
using System.Web.Security;

namespace System.Web.Configuration
{
	/// <summary>Configures an HTTP module for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005AC RID: 1452
	public sealed class HttpModulesSection : ConfigurationSection
	{
		// Token: 0x06003E1E RID: 15902 RVA: 0x000A284C File Offset: 0x000A0A4C
		static HttpModulesSection()
		{
			HttpModulesSection.properties.Add(HttpModulesSection.modulesProp);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.HttpModuleActionCollection" /> of <see cref="T:System.Web.Configuration.HttpModuleAction" /> modules contained by the <see cref="T:System.Web.Configuration.HttpModulesSection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.HttpModuleActionCollection" /> that contains the <see cref="T:System.Web.Configuration.HttpModuleAction" /> objects, or modules, defined by the <see cref="T:System.Web.Configuration.HttpModulesSection" />. </returns>
		// Token: 0x1700138B RID: 5003
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x000A2888 File Offset: 0x000A0A88
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public HttpModuleActionCollection Modules
		{
			get
			{
				return (HttpModuleActionCollection)base[HttpModulesSection.modulesProp];
			}
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06003E20 RID: 15904 RVA: 0x000A289A File Offset: 0x000A0A9A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpModulesSection.properties;
			}
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x000A28A4 File Offset: 0x000A0AA4
		internal HttpModuleCollection LoadModules(HttpApplication app)
		{
			HttpModuleCollection httpModuleCollection = new HttpModuleCollection();
			foreach (object obj in this.Modules)
			{
				HttpModuleAction httpModuleAction = (HttpModuleAction)obj;
				Type type = HttpApplication.LoadType(httpModuleAction.Type);
				if (!(type == null))
				{
					IHttpModule httpModule = (IHttpModule)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
					httpModule.Init(app);
					httpModuleCollection.AddModule(httpModuleAction.Name, httpModule);
				}
			}
			IHttpModule httpModule2 = new DefaultAuthenticationModule();
			httpModule2.Init(app);
			httpModuleCollection.AddModule("DefaultAuthentication", httpModule2);
			return httpModuleCollection;
		}

		// Token: 0x04002211 RID: 8721
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002212 RID: 8722
		private static ConfigurationProperty modulesProp = new ConfigurationProperty("", typeof(HttpModuleActionCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
