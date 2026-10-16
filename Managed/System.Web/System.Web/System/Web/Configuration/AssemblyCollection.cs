using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.AssemblyInfo" /> objects. This class cannot be inherited.</summary>
	// Token: 0x0200057C RID: 1404
	[ConfigurationCollection(typeof(AssemblyInfo), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class AssemblyCollection : ConfigurationElementCollection
	{
		/// <summary>Adds an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object to the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		/// <param name="assemblyInformation">A string value specifying the assembly reference.</param>
		// Token: 0x06003B42 RID: 15170 RVA: 0x0009CB99 File Offset: 0x0009AD99
		public void Add(AssemblyInfo assemblyInformation)
		{
			base.BaseAdd(assemblyInformation, false);
		}

		/// <summary>Clears all the <see cref="T:System.Web.Configuration.AssemblyInfo" /> objects from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		// Token: 0x06003B43 RID: 15171 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x06003B44 RID: 15172 RVA: 0x0009CBAB File Offset: 0x0009ADAB
		protected override ConfigurationElement CreateNewElement()
		{
			return new AssemblyInfo();
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x0009CBB2 File Offset: 0x0009ADB2
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((AssemblyInfo)element).Assembly;
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.AssemblyInfo" /> object from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		/// <param name="key">A string value specifying the assembly reference.</param>
		// Token: 0x06003B46 RID: 15174 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string key)
		{
			base.BaseRemove(key);
		}

		/// <summary>Removes an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object from the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</summary>
		/// <param name="index">An integer value specifying an <see cref="T:System.Web.Configuration.AssemblyInfo" /> object within the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</param>
		// Token: 0x06003B47 RID: 15175 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.AssemblyInfo" /> at the specified index in the <see cref="T:System.Web.Configuration.AssemblyCollection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.AssemblyInfo" /> object.</returns>
		/// <param name="index">An integer value specifying a specific <see cref="T:System.Web.Configuration.AssemblyInfo" /> object within the <see cref="T:System.Web.Configuration.AssemblyCollection" /> collection.</param>
		// Token: 0x17001241 RID: 4673
		public AssemblyInfo this[int index]
		{
			get
			{
				return (AssemblyInfo)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				this.BaseAdd(index, value);
			}
		}

		/// <summary>Gets the item identified by the specified assembly name.</summary>
		/// <returns>The item identified by the specified assembly name.</returns>
		/// <param name="assemblyName">The name identifying the assembly to retrieve.</param>
		// Token: 0x17001242 RID: 4674
		public AssemblyInfo this[string assemblyName]
		{
			get
			{
				return (AssemblyInfo)base.BaseGet(assemblyName);
			}
		}

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x0009CC07 File Offset: 0x0009AE07
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssemblyCollection.properties;
			}
		}

		// Token: 0x0400207F RID: 8319
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
