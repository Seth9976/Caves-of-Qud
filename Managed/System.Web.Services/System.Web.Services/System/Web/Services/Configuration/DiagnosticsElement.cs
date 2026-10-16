using System;
using System.Configuration;

namespace System.Web.Services.Configuration
{
	/// <summary>Represents the &lt;diagnostics&gt; element in the Web.config configuration file.</summary>
	// Token: 0x0200013D RID: 317
	public sealed class DiagnosticsElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.DiagnosticsElement" /> class. </summary>
		// Token: 0x060009B1 RID: 2481 RVA: 0x00043564 File Offset: 0x00041764
		public DiagnosticsElement()
		{
			this.properties.Add(this.suppressReturningExceptions);
		}

		/// <summary>Gets or sets a value that indicates whether the service returns exceptions.</summary>
		/// <returns>true if the service returns exceptions; otherwise, false. The default is false.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000435B3 File Offset: 0x000417B3
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x000435C6 File Offset: 0x000417C6
		[ConfigurationProperty("suppressReturningExceptions", DefaultValue = false)]
		public bool SuppressReturningExceptions
		{
			get
			{
				return (bool)base[this.suppressReturningExceptions];
			}
			set
			{
				base[this.suppressReturningExceptions] = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x000435DA File Offset: 0x000417DA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04000598 RID: 1432
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000599 RID: 1433
		private readonly ConfigurationProperty suppressReturningExceptions = new ConfigurationProperty("suppressReturningExceptions", typeof(bool), false);
	}
}
