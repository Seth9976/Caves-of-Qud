using System;
using System.Collections;
using System.Configuration;
using System.Web.Compilation;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.BuildProvider" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000585 RID: 1413
	[ConfigurationCollection(typeof(BuildProvider), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class BuildProviderCollection : ConfigurationElementCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BuildProviderCollection" /> class.</summary>
		// Token: 0x06003BB3 RID: 15283 RVA: 0x0009DA90 File Offset: 0x0009BC90
		public BuildProviderCollection()
			: base(CaseInsensitiveComparer.DefaultInvariant)
		{
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProvider" /> object at the specified index of the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</returns>
		/// <param name="index">An integer value specifying a particular <see cref="T:System.Web.Configuration.BuildProvider" /> object within the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</param>
		// Token: 0x17001264 RID: 4708
		public BuildProvider this[int index]
		{
			get
			{
				return (BuildProvider)base.BaseGet(index);
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

		/// <summary>Gets the <see cref="T:System.Web.Configuration.BuildProvider" /> collection element based on the specified name.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.BuildProvider" /> object to return from the collection.</param>
		// Token: 0x17001265 RID: 4709
		public BuildProvider this[string name]
		{
			get
			{
				string text;
				if (!string.IsNullOrEmpty(name))
				{
					text = name.ToLowerInvariant();
				}
				else
				{
					text = name;
				}
				return (BuildProvider)base.BaseGet(text);
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x0009DAD8 File Offset: 0x0009BCD8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return BuildProviderCollection.props;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.Configuration.BuildProvider" /> object to the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		/// <param name="buildProvider">A <see cref="T:System.Web.Configuration.BuildProvider" /> object.</param>
		// Token: 0x06003BB8 RID: 15288 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(BuildProvider buildProvider)
		{
			this.BaseAdd(buildProvider);
		}

		/// <summary>Clears all <see cref="T:System.Web.Configuration.BuildProvider" /> objects from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		// Token: 0x06003BB9 RID: 15289 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Removes a <see cref="T:System.Web.Configuration.BuildProvider" /> object from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		/// <param name="name">A string value specifying the <see cref="T:System.Web.Configuration.BuildProvider" /> reference.</param>
		// Token: 0x06003BBA RID: 15290 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.BuildProvider" /> object at the specified index from the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</summary>
		/// <param name="index">An integer value specifying the location of a specific <see cref="T:System.Web.Configuration.BuildProvider" /> object within the <see cref="T:System.Web.Configuration.BuildProviderCollection" />.</param>
		// Token: 0x06003BBB RID: 15291 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x0009DADF File Offset: 0x0009BCDF
		protected override ConfigurationElement CreateNewElement()
		{
			return new BuildProvider();
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x0009DAE6 File Offset: 0x0009BCE6
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((BuildProvider)element).Extension;
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x0009DAF3 File Offset: 0x0009BCF3
		internal Type GetProviderTypeForExtension(string extension)
		{
			return BuildProvider.GetProviderTypeForExtension(extension);
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x0009DAFB File Offset: 0x0009BCFB
		internal BuildProvider GetProviderInstanceForExtension(string extension)
		{
			return BuildProvider.GetProviderInstanceForExtension(extension);
		}

		// Token: 0x0400209C RID: 8348
		private static ConfigurationPropertyCollection props = new ConfigurationPropertyCollection();
	}
}
