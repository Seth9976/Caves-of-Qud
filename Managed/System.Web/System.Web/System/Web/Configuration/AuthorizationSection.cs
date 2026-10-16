using System;
using System.Configuration;
using System.Security.Principal;

namespace System.Web.Configuration
{
	/// <summary>Configures a Web application authorization. This class cannot be inherited.</summary>
	// Token: 0x02000581 RID: 1409
	public sealed class AuthorizationSection : ConfigurationSection
	{
		// Token: 0x06003B83 RID: 15235 RVA: 0x0009D485 File Offset: 0x0009B685
		static AuthorizationSection()
		{
			AuthorizationSection.properties.Add(AuthorizationSection.rulesProp);
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x0009D4C1 File Offset: 0x0009B6C1
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> of <see cref="T:System.Web.Configuration.AuthorizationRule" /> rules.</summary>
		/// <returns>Gets the <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> of <see cref="T:System.Web.Configuration.AuthorizationRule" /> rules defined by the <see cref="T:System.Web.Configuration.AuthorizationSection" />.</returns>
		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003B85 RID: 15237 RVA: 0x0009D4C9 File Offset: 0x0009B6C9
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthorizationRuleCollection Rules
		{
			get
			{
				return (AuthorizationRuleCollection)base[AuthorizationSection.rulesProp];
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003B86 RID: 15238 RVA: 0x0009D4DB File Offset: 0x0009B6DB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationSection.properties;
			}
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x0009D4E4 File Offset: 0x0009B6E4
		internal bool IsValidUser(IPrincipal user, string verb)
		{
			string text = ((user == null) ? string.Empty : user.Identity.Name);
			foreach (object obj in this.Rules)
			{
				AuthorizationRule authorizationRule = (AuthorizationRule)obj;
				if ((authorizationRule.Verbs.Count == 0 || authorizationRule.CheckVerb(verb)) && (authorizationRule.CheckUser(text) || (user != null && authorizationRule.CheckRole(user))))
				{
					return authorizationRule.Action == AuthorizationRuleAction.Allow;
				}
			}
			return true;
		}

		// Token: 0x0400208D RID: 8333
		private static ConfigurationProperty rulesProp = new ConfigurationProperty(string.Empty, typeof(AuthorizationRuleCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x0400208E RID: 8334
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
