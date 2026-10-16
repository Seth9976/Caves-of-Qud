using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.AuthorizationRule" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000580 RID: 1408
	[ConfigurationCollection(typeof(AuthorizationRule), AddItemName = "allow,deny", CollectionType = ConfigurationElementCollectionType.BasicMapAlternate)]
	public sealed class AuthorizationRuleCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to the collection.</summary>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to add to the collection. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object already exists in the collection, or the collection is read-only.</exception>
		// Token: 0x06003B72 RID: 15218 RVA: 0x0009CB99 File Offset: 0x0009AD99
		public void Add(AuthorizationRule rule)
		{
			base.BaseAdd(rule, false);
		}

		/// <summary>Removes all <see cref="T:System.Web.Configuration.AuthorizationRule" /> objects from the collection.</summary>
		// Token: 0x06003B73 RID: 15219 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x0009D3C8 File Offset: 0x0009B5C8
		protected override ConfigurationElement CreateNewElement(string elementName)
		{
			return new AuthorizationRule((elementName == "allow") ? AuthorizationRuleAction.Allow : AuthorizationRuleAction.Deny);
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x0009D3E0 File Offset: 0x0009B5E0
		protected override ConfigurationElement CreateNewElement()
		{
			return new AuthorizationRule(AuthorizationRuleAction.Allow);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</returns>
		/// <param name="index">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> index. </param>
		// Token: 0x06003B76 RID: 15222 RVA: 0x0009D3E8 File Offset: 0x0009B5E8
		public AuthorizationRule Get(int index)
		{
			return (AuthorizationRule)base.BaseGet(index);
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x0009D3F8 File Offset: 0x0009B5F8
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AuthorizationRule)element).Action.ToString();
		}

		/// <summary>Gets the collection index of the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object.</returns>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object whose index is returned.</param>
		// Token: 0x06003B78 RID: 15224 RVA: 0x0009D41E File Offset: 0x0009B61E
		public int IndexOf(AuthorizationRule rule)
		{
			return base.BaseIndexOf(rule);
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x0009D427 File Offset: 0x0009B627
		protected override bool IsElementName(string elementname)
		{
			return elementname == "allow" || elementname == "deny";
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object from the collection.</summary>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" />  object to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The passed <see cref="T:System.Web.Configuration.AuthorizationRule" /> object does not exist in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003B7A RID: 15226 RVA: 0x0009D444 File Offset: 0x0009B644
		public void Remove(AuthorizationRule rule)
		{
			base.BaseRemove(rule.Action.ToString());
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.AuthorizationRule" /> object from the collection at the specified index.</summary>
		/// <param name="index">The index location of the <see cref="T:System.Web.Configuration.AuthorizationRule" /> to remove.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">There is no <see cref="T:System.Web.Configuration.AuthorizationRule" /> object with the specified index in the collection, the element has already been removed, or the collection is read-only.</exception>
		// Token: 0x06003B7B RID: 15227 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to the collection at the specified index.</summary>
		/// <param name="index">The index location at which to add the specified <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> object. </param>
		/// <param name="rule">The <see cref="T:System.Web.Configuration.AuthorizationRule" /> object to be added.</param>
		// Token: 0x06003B7C RID: 15228 RVA: 0x0009CBDF File Offset: 0x0009ADDF
		public void Set(int index, AuthorizationRule rule)
		{
			if (base.BaseGet(index) != null)
			{
				base.BaseRemoveAt(index);
			}
			this.BaseAdd(index, rule);
		}

		/// <summary>Gets the type of this <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" />.</summary>
		/// <returns>A value from the <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> enumeration representing the type of this collection.</returns>
		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06003B7D RID: 15229 RVA: 0x0003392E File Offset: 0x00031B2E
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMapAlternate;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06003B7E RID: 15230 RVA: 0x0000EE63 File Offset: 0x0000D063
		protected override string ElementName
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets an item in this collection.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationRule" /> at the specified index.</returns>
		/// <param name="index">
		///   <see cref="T:System.Web.Configuration.AuthorizationRule" /> collection index</param>
		// Token: 0x17001251 RID: 4689
		public AuthorizationRule this[int index]
		{
			get
			{
				return this.Get(index);
			}
			set
			{
				this.Set(index, value);
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06003B81 RID: 15233 RVA: 0x0009D47E File Offset: 0x0009B67E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationRuleCollection.properties;
			}
		}

		// Token: 0x0400208C RID: 8332
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
