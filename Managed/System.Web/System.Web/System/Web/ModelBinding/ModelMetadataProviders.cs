using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for the current <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> instance.</summary>
	// Token: 0x02000722 RID: 1826
	public static class ModelMetadataProviders
	{
		/// <summary>Gets or sets the current <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> instance.</summary>
		/// <returns>The current <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> instance. The default value is a new instance of the <see cref="T:System.Web.ModelBinding.EmptyModelMetadataProvider" /> class.</returns>
		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004C02 RID: 19458 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static ModelMetadataProvider Current
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
	}
}
