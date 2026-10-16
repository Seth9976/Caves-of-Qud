using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the SQL cache dependencies for an ASP.NET application. This class cannot be inherited. </summary>
	// Token: 0x020005D7 RID: 1495
	public sealed class SqlCacheDependencySection : ConfigurationSection
	{
		// Token: 0x060040AB RID: 16555 RVA: 0x000A80F8 File Offset: 0x000A62F8
		static SqlCacheDependencySection()
		{
			SqlCacheDependencySection.properties.Add(SqlCacheDependencySection.databasesProp);
			SqlCacheDependencySection.properties.Add(SqlCacheDependencySection.enabledProp);
			SqlCacheDependencySection.properties.Add(SqlCacheDependencySection.pollTimeProp);
			SqlCacheDependencySection.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencySection), new ValidatorCallback(SqlCacheDependencySection.ValidateElement)));
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x00003946 File Offset: 0x00001B46
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060040AD RID: 16557 RVA: 0x000A81C0 File Offset: 0x000A63C0
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return SqlCacheDependencySection.elementProperty;
			}
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x0009D4C1 File Offset: 0x0009B6C1
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects stored within the <see cref="T:System.Web.Configuration.SqlCacheDependencySection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects</returns>
		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060040AF RID: 16559 RVA: 0x000A81C7 File Offset: 0x000A63C7
		[ConfigurationProperty("databases")]
		public SqlCacheDependencyDatabaseCollection Databases
		{
			get
			{
				return (SqlCacheDependencyDatabaseCollection)base[SqlCacheDependencySection.databasesProp];
			}
		}

		/// <summary>Gets or sets a value indicating whether the database table should be monitored for changes.</summary>
		/// <returns>true if SQL cache monitoring is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060040B0 RID: 16560 RVA: 0x000A81D9 File Offset: 0x000A63D9
		// (set) Token: 0x060040B1 RID: 16561 RVA: 0x000A81EB File Offset: 0x000A63EB
		[ConfigurationProperty("enabled", DefaultValue = "True")]
		public bool Enabled
		{
			get
			{
				return (bool)base[SqlCacheDependencySection.enabledProp];
			}
			set
			{
				base[SqlCacheDependencySection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets the frequency with which the <see cref="T:System.Web.Caching.SqlCacheDependency" /> polls the database table for changes.</summary>
		/// <returns>The SQL cache dependency polling time, in milliseconds. The default is 500.</returns>
		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000A81FE File Offset: 0x000A63FE
		// (set) Token: 0x060040B3 RID: 16563 RVA: 0x000A8210 File Offset: 0x000A6410
		[ConfigurationProperty("pollTime", DefaultValue = "60000")]
		public int PollTime
		{
			get
			{
				return (int)base[SqlCacheDependencySection.pollTimeProp];
			}
			set
			{
				base[SqlCacheDependencySection.pollTimeProp] = value;
			}
		}

		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x000A8223 File Offset: 0x000A6423
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SqlCacheDependencySection.properties;
			}
		}

		// Token: 0x04002310 RID: 8976
		private static ConfigurationProperty databasesProp = new ConfigurationProperty("databases", typeof(SqlCacheDependencyDatabaseCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002311 RID: 8977
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);

		// Token: 0x04002312 RID: 8978
		private static ConfigurationProperty pollTimeProp = new ConfigurationProperty("pollTime", typeof(int), 60000);

		// Token: 0x04002313 RID: 8979
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002314 RID: 8980
		private static ConfigurationElementProperty elementProperty;
	}
}
