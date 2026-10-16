using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the performance counter element in the System.Net configuration file that determines whether networking performance counters are enabled. This class cannot be inherited.</summary>
	// Token: 0x02000774 RID: 1908
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		// Token: 0x06003C08 RID: 15368 RVA: 0x000CD83D File Offset: 0x000CBA3D
		static PerformanceCountersElement()
		{
			PerformanceCountersElement.properties.Add(PerformanceCountersElement.enabledProp);
		}

		/// <summary>Gets or sets whether performance counters are enabled.</summary>
		/// <returns>true if performance counters are enabled; otherwise, false.</returns>
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x06003C09 RID: 15369 RVA: 0x000CD877 File Offset: 0x000CBA77
		// (set) Token: 0x06003C0A RID: 15370 RVA: 0x000CD889 File Offset: 0x000CBA89
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[PerformanceCountersElement.enabledProp];
			}
			set
			{
				base[PerformanceCountersElement.enabledProp] = value;
			}
		}

		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06003C0B RID: 15371 RVA: 0x000CD89C File Offset: 0x000CBA9C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerformanceCountersElement.properties;
			}
		}

		// Token: 0x040023BF RID: 9151
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x040023C0 RID: 9152
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
