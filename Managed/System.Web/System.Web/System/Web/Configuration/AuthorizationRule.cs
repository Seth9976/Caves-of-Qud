using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Security.Principal;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> class allows you to programmatically access and modify the authorization section of a configuration file. This class cannot be inherited.</summary>
	// Token: 0x0200057F RID: 1407
	public sealed class AuthorizationRule : ConfigurationElement
	{
		// Token: 0x06003B5B RID: 15195 RVA: 0x0009CDC0 File Offset: 0x0009AFC0
		static AuthorizationRule()
		{
			AuthorizationRule.properties.Add(AuthorizationRule.rolesProp);
			AuthorizationRule.properties.Add(AuthorizationRule.usersProp);
			AuthorizationRule.properties.Add(AuthorizationRule.verbsProp);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Web.Configuration.AuthorizationRule" /> class using the passed object. </summary>
		/// <param name="action">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to use to initialize the new instance.</param>
		// Token: 0x06003B5C RID: 15196 RVA: 0x0009CE74 File Offset: 0x0009B074
		public AuthorizationRule(AuthorizationRuleAction action)
		{
			this.action = action;
			base[AuthorizationRule.rolesProp] = new CommaDelimitedStringCollection();
			base[AuthorizationRule.usersProp] = new CommaDelimitedStringCollection();
			base[AuthorizationRule.verbsProp] = new CommaDelimitedStringCollection();
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if the objects are equal; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x06003B5D RID: 15197 RVA: 0x0009CEC8 File Offset: 0x0009B0C8
		public override bool Equals(object obj)
		{
			AuthorizationRule authorizationRule = obj as AuthorizationRule;
			if (authorizationRule == null)
			{
				return false;
			}
			if (this.action != authorizationRule.Action)
			{
				return false;
			}
			if (this.Roles.Count != authorizationRule.Roles.Count || this.Users.Count != authorizationRule.Users.Count || this.Verbs.Count != authorizationRule.Verbs.Count)
			{
				return false;
			}
			for (int i = 0; i < this.Roles.Count; i++)
			{
				if (this.Roles[i] != authorizationRule.Roles[i])
				{
					return false;
				}
			}
			for (int i = 0; i < this.Users.Count; i++)
			{
				if (this.Users[i] != authorizationRule.Users[i])
				{
					return false;
				}
			}
			for (int i = 0; i < this.Verbs.Count; i++)
			{
				if (this.Verbs[i] != authorizationRule.Verbs[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Serves as a hash function for this object.</summary>
		/// <returns>An integer representing the hash code for the current object.</returns>
		// Token: 0x06003B5E RID: 15198 RVA: 0x0009CFE4 File Offset: 0x0009B1E4
		public override int GetHashCode()
		{
			int num = (int)this.action;
			for (int i = 0; i < this.Roles.Count; i++)
			{
				num += this.Roles[i].GetHashCode();
			}
			for (int i = 0; i < this.Users.Count; i++)
			{
				num += this.Users[i].GetHashCode();
			}
			for (int i = 0; i < this.Verbs.Count; i++)
			{
				num += this.Verbs[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x0009D077 File Offset: 0x0009B277
		[global::System.MonoTODO("Not implemented")]
		protected internal override bool IsModified()
		{
			return ((CommaDelimitedStringCollection)this.Roles).IsModified || ((CommaDelimitedStringCollection)this.Users).IsModified || ((CommaDelimitedStringCollection)this.Verbs).IsModified;
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x0009D0B2 File Offset: 0x0009B2B2
		private void VerifyData()
		{
			if (this.Roles.Count == 0 && this.Users.Count == 0)
			{
				throw new ConfigurationErrorsException("You must supply either a list of users or roles when creating an AuthorizationRule");
			}
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x0009D0D9 File Offset: 0x0009B2D9
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
			this.VerifyData();
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x0009D0E7 File Offset: 0x0009B2E7
		protected override void PreSerialize(XmlWriter writer)
		{
			base.PreSerialize(writer);
			this.VerifyData();
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x0009D0F8 File Offset: 0x0009B2F8
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			AuthorizationRule authorizationRule = (AuthorizationRule)parentElement;
			this.Action = authorizationRule.Action;
			base.Reset(parentElement);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x0009D11F File Offset: 0x0009B31F
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x0009D128 File Offset: 0x0009B328
		protected internal override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
		{
			if (this.saveMode != ConfigurationSaveMode.Full && !this.IsModified())
			{
				return true;
			}
			this.PreSerialize(writer);
			writer.WriteStartElement((this.action == AuthorizationRuleAction.Allow) ? "allow" : "deny");
			if (this.Roles.Count > 0)
			{
				writer.WriteAttributeString("roles", this.Roles.ToString());
			}
			if (this.Users.Count > 0)
			{
				writer.WriteAttributeString("users", this.Users.ToString());
			}
			if (this.Verbs.Count > 0)
			{
				writer.WriteAttributeString("verbs", this.Verbs.ToString());
			}
			writer.WriteEndElement();
			return true;
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x0009D1DD File Offset: 0x0009B3DD
		protected internal override void SetReadOnly()
		{
			base.SetReadOnly();
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x0009D1E8 File Offset: 0x0009B3E8
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
			this.saveMode = saveMode;
			AuthorizationRule authorizationRule = sourceElement as AuthorizationRule;
			if (authorizationRule != null)
			{
				this.action = authorizationRule.Action;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.AuthorizationRule" /> action.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.AuthorizationRuleAction" /> values.</returns>
		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x0009D21B File Offset: 0x0009B41B
		// (set) Token: 0x06003B69 RID: 15209 RVA: 0x0009D223 File Offset: 0x0009B423
		public AuthorizationRuleAction Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
			}
		}

		/// <summary>Gets the roles associated with the resource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the roles whose authorization must be verified.</returns>
		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x0009D22C File Offset: 0x0009B42C
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		[ConfigurationProperty("roles")]
		public StringCollection Roles
		{
			get
			{
				return (StringCollection)base[AuthorizationRule.rolesProp];
			}
		}

		/// <summary>Gets the users associated with the resource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the users whose authorization must be verified.</returns>
		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x0009D23E File Offset: 0x0009B43E
		[ConfigurationProperty("users")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Users
		{
			get
			{
				return (StringCollection)base[AuthorizationRule.usersProp];
			}
		}

		/// <summary>Gets the verbs associated with the resource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> collection containing the verbs whose authorization must be verified. </returns>
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x0009D250 File Offset: 0x0009B450
		[ConfigurationProperty("verbs")]
		[TypeConverter(typeof(CommaDelimitedStringCollectionConverter))]
		public StringCollection Verbs
		{
			get
			{
				return (StringCollection)base[AuthorizationRule.verbsProp];
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x0009D262 File Offset: 0x0009B462
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationRule.properties;
			}
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x0009D26C File Offset: 0x0009B46C
		internal bool CheckVerb(string verb)
		{
			using (StringEnumerator enumerator = this.Verbs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Compare(enumerator.Current, verb, true, Helpers.InvariantCulture) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x0009D2D0 File Offset: 0x0009B4D0
		internal bool CheckUser(string user)
		{
			foreach (string text in this.Users)
			{
				if (string.Compare(text, user, true, Helpers.InvariantCulture) == 0 || text == "*" || (text == "?" && user == ""))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x0009D35C File Offset: 0x0009B55C
		internal bool CheckRole(IPrincipal user)
		{
			foreach (string text in this.Roles)
			{
				if (user.IsInRole(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002086 RID: 8326
		private static ConfigurationProperty rolesProp = new ConfigurationProperty("roles", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002087 RID: 8327
		private static ConfigurationProperty usersProp = new ConfigurationProperty("users", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002088 RID: 8328
		private static ConfigurationProperty verbsProp = new ConfigurationProperty("verbs", typeof(StringCollection), null, PropertyHelper.CommaDelimitedStringCollectionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002089 RID: 8329
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400208A RID: 8330
		private AuthorizationRuleAction action;

		// Token: 0x0400208B RID: 8331
		private ConfigurationSaveMode saveMode = ConfigurationSaveMode.Full;
	}
}
