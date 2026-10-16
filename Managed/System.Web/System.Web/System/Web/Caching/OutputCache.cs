using System;
using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.Caching
{
	/// <summary>Provides programmatic access to the output-cache providers that are specified in the configuration file for a Web site. </summary>
	// Token: 0x0200068C RID: 1676
	public static class OutputCache
	{
		/// <summary>Gets the name of the default provider that is configured for the output cache.</summary>
		/// <returns>The name of the default provider.</returns>
		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06004753 RID: 18259 RVA: 0x000C682A File Offset: 0x000C4A2A
		public static string DefaultProviderName
		{
			get
			{
				OutputCache.Init();
				if (string.IsNullOrEmpty(OutputCache.defaultProviderName))
				{
					return "AspNetInternalProvider";
				}
				return OutputCache.defaultProviderName;
			}
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06004754 RID: 18260 RVA: 0x000C6848 File Offset: 0x000C4A48
		internal static OutputCacheProvider DefaultProvider
		{
			get
			{
				if (OutputCache.defaultProvider == null)
				{
					object obj = OutputCache.defaultProviderInitLock;
					lock (obj)
					{
						if (OutputCache.defaultProvider == null)
						{
							OutputCache.defaultProvider = new InMemoryOutputCacheProvider();
						}
					}
				}
				return OutputCache.defaultProvider;
			}
		}

		/// <summary>Gets a collection of the output-cache providers that are specified in the configuration file for a Web site. </summary>
		/// <returns>The collection of configured providers.</returns>
		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06004755 RID: 18261 RVA: 0x000C68A0 File Offset: 0x000C4AA0
		public static OutputCacheProviderCollection Providers
		{
			get
			{
				OutputCache.Init();
				return OutputCache.providers;
			}
		}

		/// <summary>Deserializes a binary object into output-cache data.</summary>
		/// <returns>An object that contains the deserialized data.</returns>
		/// <param name="stream">The data to deserialize.</param>
		/// <exception cref="T:System.ArgumentException">The deserialized object that is returned by the method is not a valid output-cache type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06004756 RID: 18262 RVA: 0x000C68AC File Offset: 0x000C4AAC
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public static object Deserialize(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			object obj = new BinaryFormatter().Deserialize(stream);
			if (obj == null || OutputCache.IsInvalidType(obj))
			{
				throw new ArgumentException("The provided parameter is not of a supported type for serialization and/or deserialization.");
			}
			return obj;
		}

		/// <summary>Serializes output-cache data into binary data.</summary>
		/// <param name="stream">The object to contain the serialized binary data.</param>
		/// <param name="data">The output-cache data to serialize.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data" /> is not one of the specified output-cache types. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="data" /> is null or <paramref name="stream" /> is null. </exception>
		// Token: 0x06004757 RID: 18263 RVA: 0x000C68EA File Offset: 0x000C4AEA
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public static void Serialize(Stream stream, object data)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (data == null || OutputCache.IsInvalidType(data))
			{
				throw new ArgumentException("The provided parameter is not of a supported type for serialization and/or deserialization.");
			}
			new BinaryFormatter().Serialize(stream, data);
		}

		// Token: 0x06004758 RID: 18264 RVA: 0x000C691C File Offset: 0x000C4B1C
		internal static OutputCacheProvider GetProvider(string providerName)
		{
			if (string.IsNullOrEmpty(providerName))
			{
				return null;
			}
			if (string.Compare(providerName, "AspNetInternalProvider", StringComparison.Ordinal) == 0)
			{
				return OutputCache.DefaultProvider;
			}
			OutputCacheProviderCollection outputCacheProviderCollection = OutputCache.Providers;
			if (outputCacheProviderCollection == null)
			{
				return null;
			}
			return outputCacheProviderCollection[providerName];
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x000C6959 File Offset: 0x000C4B59
		private static bool IsInvalidType(object data)
		{
			return !(data is MemoryResponseElement) && !(data is FileResponseElement) && !(data is SubstitutionResponseElement);
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x000C697C File Offset: 0x000C4B7C
		private static void Init()
		{
			if (OutputCache.initialized)
			{
				return;
			}
			object obj = OutputCache.initLock;
			lock (obj)
			{
				if (!OutputCache.initialized)
				{
					OutputCacheSection outputCacheSection = WebConfigurationManager.GetWebApplicationSection("system.web/caching/outputCache") as OutputCacheSection;
					ProviderSettingsCollection providerSettingsCollection = outputCacheSection.Providers;
					OutputCache.defaultProviderName = outputCacheSection.DefaultProviderName;
					if (providerSettingsCollection != null && providerSettingsCollection.Count > 0)
					{
						OutputCacheProviderCollection outputCacheProviderCollection = new OutputCacheProviderCollection();
						foreach (object obj2 in providerSettingsCollection)
						{
							ProviderSettings providerSettings = (ProviderSettings)obj2;
							outputCacheProviderCollection.Add(OutputCache.LoadProvider(providerSettings));
						}
						outputCacheProviderCollection.SetReadOnly();
						OutputCache.providers = outputCacheProviderCollection;
					}
					OutputCache.initialized = true;
				}
			}
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x000C6A60 File Offset: 0x000C4C60
		private static OutputCacheProvider LoadProvider(ProviderSettings ps)
		{
			Type type = HttpApplication.LoadType(ps.Type, false);
			if (type == null)
			{
				throw new ConfigurationErrorsException(string.Format("Could not load type '{0}'.", ps.Type));
			}
			OutputCacheProvider outputCacheProvider = Activator.CreateInstance(type) as OutputCacheProvider;
			outputCacheProvider.Initialize(ps.Name, ps.Parameters);
			return outputCacheProvider;
		}

		// Token: 0x0600475C RID: 18268 RVA: 0x000C6AB4 File Offset: 0x000C4CB4
		internal static void RemoveFromProvider(string key, string providerName)
		{
			if (providerName == null)
			{
				return;
			}
			OutputCacheProviderCollection outputCacheProviderCollection = OutputCache.Providers;
			OutputCacheProvider outputCacheProvider;
			if (outputCacheProviderCollection == null || outputCacheProviderCollection.Count == 0)
			{
				outputCacheProvider = null;
			}
			else
			{
				outputCacheProvider = outputCacheProviderCollection[providerName];
			}
			if (outputCacheProvider == null)
			{
				throw new ProviderException("Provider '" + providerName + "' was not found.");
			}
			outputCacheProvider.Remove(key);
		}

		// Token: 0x040025B2 RID: 9650
		internal const string DEFAULT_PROVIDER_NAME = "AspNetInternalProvider";

		// Token: 0x040025B3 RID: 9651
		private static readonly object initLock = new object();

		// Token: 0x040025B4 RID: 9652
		private static readonly object defaultProviderInitLock = new object();

		// Token: 0x040025B5 RID: 9653
		private static bool initialized;

		// Token: 0x040025B6 RID: 9654
		private static string defaultProviderName;

		// Token: 0x040025B7 RID: 9655
		private static OutputCacheProviderCollection providers;

		// Token: 0x040025B8 RID: 9656
		private static OutputCacheProvider defaultProvider;
	}
}
