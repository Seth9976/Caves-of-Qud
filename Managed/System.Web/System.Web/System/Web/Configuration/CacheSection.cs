using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the global cache settings for an ASP.NET application. This class cannot be inherited.</summary>
	// Token: 0x02000586 RID: 1414
	public sealed class CacheSection : ConfigurationSection
	{
		// Token: 0x06003BC0 RID: 15296 RVA: 0x0009DB04 File Offset: 0x0009BD04
		static CacheSection()
		{
			CacheSection.properties.Add(CacheSection.disableExpirationProp);
			CacheSection.properties.Add(CacheSection.disableMemoryCollectionProp);
			CacheSection.properties.Add(CacheSection.percentagePhysicalMemoryUsedLimitProp);
			CacheSection.properties.Add(CacheSection.privateBytesLimitProp);
			CacheSection.properties.Add(CacheSection.privateBytesPollTimeProp);
		}

		/// <summary>Gets or sets a value indicating whether the cache expiration is disabled.</summary>
		/// <returns>true if the cache expiration is disabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x0009DC4F File Offset: 0x0009BE4F
		// (set) Token: 0x06003BC2 RID: 15298 RVA: 0x0009DC61 File Offset: 0x0009BE61
		[ConfigurationProperty("disableExpiration", DefaultValue = "False")]
		public bool DisableExpiration
		{
			get
			{
				return (bool)base[CacheSection.disableExpirationProp];
			}
			set
			{
				base[CacheSection.disableExpirationProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cache memory collection is disabled.</summary>
		/// <returns>true if the cache memory collection is disabled; otherwise, false. The default is false.</returns>
		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x0009DC74 File Offset: 0x0009BE74
		// (set) Token: 0x06003BC4 RID: 15300 RVA: 0x0009DC86 File Offset: 0x0009BE86
		[ConfigurationProperty("disableMemoryCollection", DefaultValue = "False")]
		public bool DisableMemoryCollection
		{
			get
			{
				return (bool)base[CacheSection.disableMemoryCollectionProp];
			}
			set
			{
				base[CacheSection.disableMemoryCollectionProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum percentage of virtual memory usage.</summary>
		/// <returns>The maximum percentage of virtual memory usage. The default value is 90%.</returns>
		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x0009DC99 File Offset: 0x0009BE99
		// (set) Token: 0x06003BC6 RID: 15302 RVA: 0x0009DCAB File Offset: 0x0009BEAB
		[IntegerValidator(MinValue = 0, MaxValue = 100)]
		[ConfigurationProperty("percentagePhysicalMemoryUsedLimit", DefaultValue = "0")]
		public int PercentagePhysicalMemoryUsedLimit
		{
			get
			{
				return (int)base[CacheSection.percentagePhysicalMemoryUsedLimitProp];
			}
			set
			{
				base[CacheSection.percentagePhysicalMemoryUsedLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum size of the working-process private space.</summary>
		/// <returns>The maximum number, in bytes, of the private space allocated to the working process. The default value is 0.</returns>
		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003BC7 RID: 15303 RVA: 0x0009DCBE File Offset: 0x0009BEBE
		// (set) Token: 0x06003BC8 RID: 15304 RVA: 0x0009DCD0 File Offset: 0x0009BED0
		[LongValidator(MinValue = 0L, MaxValue = 9223372036854775807L)]
		[ConfigurationProperty("privateBytesLimit", DefaultValue = "0")]
		public long PrivateBytesLimit
		{
			get
			{
				return (long)base[CacheSection.privateBytesLimitProp];
			}
			set
			{
				base[CacheSection.privateBytesLimitProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the time interval between polling for the worker-process memory usage.</summary>
		/// <returns>The time interval between polling for the worker process memory usage. The default value is 2 minutes.</returns>
		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003BC9 RID: 15305 RVA: 0x0009DCE3 File Offset: 0x0009BEE3
		// (set) Token: 0x06003BCA RID: 15306 RVA: 0x0009DCF5 File Offset: 0x0009BEF5
		[TypeConverter(typeof(InfiniteTimeSpanConverter))]
		[ConfigurationProperty("privateBytesPollTime", DefaultValue = "00:02:00")]
		public TimeSpan PrivateBytesPollTime
		{
			get
			{
				return (TimeSpan)base[CacheSection.privateBytesPollTimeProp];
			}
			set
			{
				base[CacheSection.privateBytesPollTimeProp] = value;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003BCB RID: 15307 RVA: 0x0009DD08 File Offset: 0x0009BF08
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CacheSection.properties;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003BCD RID: 15309 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06003BCE RID: 15310 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string DefaultProvider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003BCF RID: 15311 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ProviderSettingsCollection Providers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x0400209D RID: 8349
		private static ConfigurationProperty disableExpirationProp = new ConfigurationProperty("disableExpiration", typeof(bool), false);

		// Token: 0x0400209E RID: 8350
		private static ConfigurationProperty disableMemoryCollectionProp = new ConfigurationProperty("disableMemoryCollection", typeof(bool), false);

		// Token: 0x0400209F RID: 8351
		private static ConfigurationProperty percentagePhysicalMemoryUsedLimitProp = new ConfigurationProperty("percentagePhysicalMemoryUsedLimit", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020A0 RID: 8352
		private static ConfigurationProperty privateBytesLimitProp = new ConfigurationProperty("privateBytesLimit", typeof(long), 0L, TypeDescriptor.GetConverter(typeof(long)), new LongValidator(0L, long.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040020A1 RID: 8353
		private static ConfigurationProperty privateBytesPollTimeProp = new ConfigurationProperty("privateBytesPollTime", typeof(TimeSpan), TimeSpan.FromMinutes(2.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020A2 RID: 8354
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
