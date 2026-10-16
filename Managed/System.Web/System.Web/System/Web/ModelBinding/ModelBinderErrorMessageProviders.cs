using System;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for model-binder error message providers.</summary>
	// Token: 0x0200071F RID: 1823
	public static class ModelBinderErrorMessageProviders
	{
		/// <summary>Returns the current provider for type-conversion error messages.</summary>
		/// <returns>The error message provider.</returns>
		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x06004BFB RID: 19451 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004BFC RID: 19452 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static ModelBinderErrorMessageProvider TypeConversionErrorMessageProvider
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

		/// <summary>Returns the current provider for value-required error messages.</summary>
		/// <returns>The error message provider.</returns>
		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x06004BFD RID: 19453 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004BFE RID: 19454 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static ModelBinderErrorMessageProvider ValueRequiredErrorMessageProvider
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
