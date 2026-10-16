using System;
using System.Configuration.Provider;

namespace System.Web.Caching
{
	/// <summary>Represents the collection of output-cache providers that are configured for a Web site.</summary>
	// Token: 0x02000673 RID: 1651
	public sealed class OutputCacheProviderCollection : ProviderCollection
	{
		/// <summary>Returns a reference to the named output-cache provider in the collection.</summary>
		/// <returns>A provider from the collection. </returns>
		/// <param name="name">The name of a provider in the collection.</param>
		// Token: 0x170015F1 RID: 5617
		public OutputCacheProvider this[string name]
		{
			get
			{
				return (OutputCacheProvider)base[name];
			}
		}

		/// <summary>Inserts a provider into the collection of output-cache providers.</summary>
		/// <param name="provider">An output cache provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="provider" /> is not of type <see cref="T:System.Web.Caching.OutputCacheProvider" />.</exception>
		// Token: 0x060046A0 RID: 18080 RVA: 0x000C4220 File Offset: 0x000C2420
		public override void Add(ProviderBase provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!(provider is OutputCacheProvider))
			{
				throw new ArgumentException(global::SR.GetString("Provider must implement the class '{0}'.", new object[] { typeof(OutputCacheProvider).Name }), "provider");
			}
			base.Add(provider);
		}

		/// <summary>Copies the output-cache providers to a compatible one-dimensional array at the specified index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection The array must have zero-based indexing.</param>
		/// <param name="index">The point in the array where the copying starts.</param>
		// Token: 0x060046A1 RID: 18081 RVA: 0x000C4278 File Offset: 0x000C2478
		public void CopyTo(OutputCacheProvider[] array, int index)
		{
			base.CopyTo(array, index);
		}
	}
}
