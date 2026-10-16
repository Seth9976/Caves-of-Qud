using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the SQL cache dependencies databases for an ASP.NET application. This class cannot be inherited. </summary>
	// Token: 0x020005D5 RID: 1493
	public sealed class SqlCacheDependencyDatabase : ConfigurationElement
	{
		// Token: 0x0600408F RID: 16527 RVA: 0x000A7EA0 File Offset: 0x000A60A0
		static SqlCacheDependencyDatabase()
		{
			SqlCacheDependencyDatabase.properties.Add(SqlCacheDependencyDatabase.connectionStringNameProp);
			SqlCacheDependencyDatabase.properties.Add(SqlCacheDependencyDatabase.nameProp);
			SqlCacheDependencyDatabase.properties.Add(SqlCacheDependencyDatabase.pollTimeProp);
			SqlCacheDependencyDatabase.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencyDatabase), new ValidatorCallback(SqlCacheDependencyDatabase.ValidateElement)));
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal SqlCacheDependencyDatabase()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> class with the passed parameters.</summary>
		/// <param name="name">A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</param>
		/// <param name="connectionStringName">A string that specifies the name of the connection string in the connectionStrings section to use to connect to this database.</param>
		// Token: 0x06004091 RID: 16529 RVA: 0x000A7F8A File Offset: 0x000A618A
		public SqlCacheDependencyDatabase(string name, string connectionStringName)
		{
			this.Name = name;
			this.ConnectionStringName = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> class. </summary>
		/// <param name="name">A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</param>
		/// <param name="connectionStringName">A string that specifies the name of the connection string in the connectionStrings section to use to connect to this database.</param>
		/// <param name="pollTime">The database polling time, in milliseconds. </param>
		// Token: 0x06004092 RID: 16530 RVA: 0x000A7FA0 File Offset: 0x000A61A0
		public SqlCacheDependencyDatabase(string name, string connectionStringName, int pollTime)
		{
			this.Name = name;
			this.ConnectionStringName = name;
			this.PollTime = pollTime;
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x17001483 RID: 5251
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x000A7FBD File Offset: 0x000A61BD
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencyDatabase.elementProperty;
			}
		}

		/// <summary>Gets or sets the connection name for the database.</summary>
		/// <returns>A string that specifies the name of a database connection string within the connectionStrings configuration section.</returns>
		// Token: 0x17001484 RID: 5252
		// (get) Token: 0x06004095 RID: 16533 RVA: 0x000A7FC4 File Offset: 0x000A61C4
		// (set) Token: 0x06004096 RID: 16534 RVA: 0x000A7FD6 File Offset: 0x000A61D6
		[ConfigurationProperty("connectionStringName", Options = ConfigurationPropertyOptions.IsRequired)]
		[StringValidator(MinLength = 1)]
		public string ConnectionStringName
		{
			get
			{
				return (string)base[SqlCacheDependencyDatabase.connectionStringNameProp];
			}
			set
			{
				base[SqlCacheDependencyDatabase.connectionStringNameProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the database. </summary>
		/// <returns>A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</returns>
		// Token: 0x17001485 RID: 5253
		// (get) Token: 0x06004097 RID: 16535 RVA: 0x000A7FE4 File Offset: 0x000A61E4
		// (set) Token: 0x06004098 RID: 16536 RVA: 0x000A7FF6 File Offset: 0x000A61F6
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[SqlCacheDependencyDatabase.nameProp];
			}
			set
			{
				base[SqlCacheDependencyDatabase.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the frequency with which the <see cref="T:System.Web.Caching.SqlCacheDependency" /> polls the database table for changes.</summary>
		/// <returns>The database polling time, in milliseconds. </returns>
		// Token: 0x17001486 RID: 5254
		// (get) Token: 0x06004099 RID: 16537 RVA: 0x000A8004 File Offset: 0x000A6204
		// (set) Token: 0x0600409A RID: 16538 RVA: 0x000A8016 File Offset: 0x000A6216
		[ConfigurationProperty("pollTime", DefaultValue = "60000")]
		public int PollTime
		{
			get
			{
				return (int)base[SqlCacheDependencyDatabase.pollTimeProp];
			}
			set
			{
				base[SqlCacheDependencyDatabase.pollTimeProp] = value;
			}
		}

		// Token: 0x17001487 RID: 5255
		// (get) Token: 0x0600409B RID: 16539 RVA: 0x000A8029 File Offset: 0x000A6229
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencyDatabase.properties;
			}
		}

		// Token: 0x0400230B RID: 8971
		private static ConfigurationProperty connectionStringNameProp = new ConfigurationProperty("connectionStringName", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400230C RID: 8972
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400230D RID: 8973
		private static ConfigurationProperty pollTimeProp = new ConfigurationProperty("pollTime", typeof(int), 60000);

		// Token: 0x0400230E RID: 8974
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400230F RID: 8975
		private static ConfigurationElementProperty elementProperty;
	}
}
