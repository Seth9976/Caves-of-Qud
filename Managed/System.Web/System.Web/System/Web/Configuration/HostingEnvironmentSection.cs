using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that control the behavior of the application hosting environment. This class cannot be inherited.</summary>
	// Token: 0x020005A0 RID: 1440
	public sealed class HostingEnvironmentSection : ConfigurationSection
	{
		// Token: 0x06003D26 RID: 15654 RVA: 0x000A04EC File Offset: 0x0009E6EC
		static HostingEnvironmentSection()
		{
			HostingEnvironmentSection.properties.Add(HostingEnvironmentSection.idleTimeoutProp);
			HostingEnvironmentSection.properties.Add(HostingEnvironmentSection.shadowCopyBinAssembliesProp);
			HostingEnvironmentSection.properties.Add(HostingEnvironmentSection.shutdownTimeoutProp);
		}

		/// <summary>Gets or sets the amount of time, in minutes, before unloading an inactive application.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that is the specified number of minutes before unloading an inactive application. </returns>
		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x000A05B4 File Offset: 0x0009E7B4
		// (set) Token: 0x06003D28 RID: 15656 RVA: 0x000A05C6 File Offset: 0x0009E7C6
		[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
		public TimeSpan IdleTimeout
		{
			get
			{
				return (TimeSpan)base[HostingEnvironmentSection.idleTimeoutProp];
			}
			set
			{
				base[HostingEnvironmentSection.idleTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the assemblies of an application in Bin are shadow copied to the application's ASP.NET Temporary Files directory. </summary>
		/// <returns>true if the assemblies of an application in Bin are shadow copied to the application's ASP.NET Temporary Files directory; otherwise, false. The default is true.</returns>
		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x000A05D9 File Offset: 0x0009E7D9
		// (set) Token: 0x06003D2A RID: 15658 RVA: 0x000A05EB File Offset: 0x0009E7EB
		[ConfigurationProperty("shadowCopyBinAssemblies", DefaultValue = "True")]
		public bool ShadowCopyBinAssemblies
		{
			get
			{
				return (bool)base[HostingEnvironmentSection.shadowCopyBinAssembliesProp];
			}
			set
			{
				base[HostingEnvironmentSection.shadowCopyBinAssembliesProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time, in seconds, to gracefully shut down the application.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that is the specified number of seconds to gracefully shut down the application. The default is 30 seconds.</returns>
		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06003D2B RID: 15659 RVA: 0x000A05FE File Offset: 0x0009E7FE
		// (set) Token: 0x06003D2C RID: 15660 RVA: 0x000A0610 File Offset: 0x0009E810
		[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:30")]
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		public TimeSpan ShutdownTimeout
		{
			get
			{
				return (TimeSpan)base[HostingEnvironmentSection.shutdownTimeoutProp];
			}
			set
			{
				base[HostingEnvironmentSection.shutdownTimeoutProp] = value;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x000A0623 File Offset: 0x0009E823
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HostingEnvironmentSection.properties;
			}
		}

		/// <summary>Gets or sets a value that determines how ASP.NET caches URL metadata.</summary>
		/// <returns>A value that determines how ASP.NET caches URL metadata. The default value is 1 minute.</returns>
		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x000A062C File Offset: 0x0009E82C
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public TimeSpan UrlMetadataSlidingExpiration
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(TimeSpan);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002110 RID: 8464
		private static ConfigurationProperty idleTimeoutProp = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.TimeSpanMinutesOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002111 RID: 8465
		private static ConfigurationProperty shadowCopyBinAssembliesProp = new ConfigurationProperty("shadowCopyBinAssemblies", typeof(bool), true);

		// Token: 0x04002112 RID: 8466
		private static ConfigurationProperty shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002113 RID: 8467
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
