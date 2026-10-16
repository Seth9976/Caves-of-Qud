using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Represents a collection of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020005D6 RID: 1494
	[ConfigurationCollection(typeof(SqlCacheDependencyDatabase), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class SqlCacheDependencyDatabaseCollection : ConfigurationElementCollection
	{
		/// <summary>Adds a <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to add to the collection.</param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object already exists in the collection or the collection is read only.</exception>
		// Token: 0x0600409C RID: 16540 RVA: 0x0009D8D3 File Offset: 0x0009BAD3
		public void Add(SqlCacheDependencyDatabase name)
		{
			this.BaseAdd(name);
		}

		/// <summary>Removes all the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects from the collection.</summary>
		// Token: 0x0600409D RID: 16541 RVA: 0x0009CBA3 File Offset: 0x0009ADA3
		public void Clear()
		{
			base.BaseClear();
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element to retrieve.</param>
		// Token: 0x0600409E RID: 16542 RVA: 0x000A8030 File Offset: 0x000A6230
		public SqlCacheDependencyDatabase Get(string name)
		{
			return (SqlCacheDependencyDatabase)base.BaseGet(name);
		}

		/// <summary>Returns the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element at the specified index.</returns>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> element to retrieve.</param>
		// Token: 0x0600409F RID: 16543 RVA: 0x000A803E File Offset: 0x000A623E
		public SqlCacheDependencyDatabase Get(int index)
		{
			return (SqlCacheDependencyDatabase)base.BaseGet(index);
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x000A804C File Offset: 0x000A624C
		protected override ConfigurationElement CreateNewElement()
		{
			return new SqlCacheDependencyDatabase();
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x000A8053 File Offset: 0x000A6253
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SqlCacheDependencyDatabase)element).Name;
		}

		/// <summary>Returns the key for the element located at the specified index in the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" />.</summary>
		/// <returns>The key at the specified index.</returns>
		/// <param name="index">The index of the key to return.</param>
		// Token: 0x060040A2 RID: 16546 RVA: 0x000A8060 File Offset: 0x000A6260
		public string GetKey(int index)
		{
			SqlCacheDependencyDatabase sqlCacheDependencyDatabase = this.Get(index);
			if (sqlCacheDependencyDatabase == null)
			{
				return null;
			}
			return sqlCacheDependencyDatabase.Name;
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to remove from the collection.</param>
		// Token: 0x060040A3 RID: 16547 RVA: 0x0009CBBF File Offset: 0x0009ADBF
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object to remove from the collection.</param>
		// Token: 0x060040A4 RID: 16548 RVA: 0x0009CBC8 File Offset: 0x0009ADC8
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}

		/// <summary>Resets a specified <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object that exists within the collection. </summary>
		/// <param name="user">The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> element to reset. </param>
		/// <exception cref="T:System.Configuration.ConfigurationException">The <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> is read-only or already exists.</exception>
		// Token: 0x060040A5 RID: 16549 RVA: 0x000A8080 File Offset: 0x000A6280
		public void Set(SqlCacheDependencyDatabase user)
		{
			SqlCacheDependencyDatabase sqlCacheDependencyDatabase = this.Get(user.Name);
			if (sqlCacheDependencyDatabase == null)
			{
				this.Add(user);
				return;
			}
			int num = base.BaseIndexOf(sqlCacheDependencyDatabase);
			this.RemoveAt(num);
			this.BaseAdd(num, user);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> keys.</summary>
		/// <returns>The string array containing the collection keys.</returns>
		// Token: 0x17001488 RID: 5256
		// (get) Token: 0x060040A6 RID: 16550 RVA: 0x000A80BC File Offset: 0x000A62BC
		public string[] AllKeys
		{
			get
			{
				string[] array = new string[base.Count];
				for (int i = 0; i < base.Count; i++)
				{
					array[i] = this[i].Name;
				}
				return array;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object at the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object.</param>
		// Token: 0x17001489 RID: 5257
		public SqlCacheDependencyDatabase this[int index]
		{
			get
			{
				return (SqlCacheDependencyDatabase)base.BaseGet(index);
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

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object with the specified name.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object with the specified name.</returns>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> object.</param>
		// Token: 0x1700148A RID: 5258
		public SqlCacheDependencyDatabase this[string name]
		{
			get
			{
				return (SqlCacheDependencyDatabase)base.BaseGet(name);
			}
		}
	}
}
