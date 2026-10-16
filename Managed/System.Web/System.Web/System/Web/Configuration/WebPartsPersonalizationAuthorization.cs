using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the authorization section contained in the webParts section of the configuration. This class cannot be inherited.</summary>
	// Token: 0x020005F1 RID: 1521
	public sealed class WebPartsPersonalizationAuthorization : ConfigurationElement
	{
		// Token: 0x0600420A RID: 16906 RVA: 0x000AAB34 File Offset: 0x000A8D34
		static WebPartsPersonalizationAuthorization()
		{
			WebPartsPersonalizationAuthorization.properties.Add(WebPartsPersonalizationAuthorization.Prop);
		}

		/// <summary>Gets a collection of rules used for personalization authorization related to Web Parts.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> object.</returns>
		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x000AAB70 File Offset: 0x000A8D70
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthorizationRuleCollection Rules
		{
			get
			{
				return (AuthorizationRuleCollection)base[WebPartsPersonalizationAuthorization.Prop];
			}
		}

		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x0600420C RID: 16908 RVA: 0x000AAB82 File Offset: 0x000A8D82
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsPersonalizationAuthorization.properties;
			}
		}

		// Token: 0x0400236A RID: 9066
		private static ConfigurationProperty Prop = new ConfigurationProperty("", typeof(AuthorizationRuleCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x0400236B RID: 9067
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
