using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Describes a complex model, using a collection rather than individual properties as the data store.</summary>
	// Token: 0x020006FD RID: 1789
	public class ComplexModel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ComplexModel" /> class.</summary>
		/// <param name="modelMetadata">Metadata for the model.</param>
		/// <param name="propertyMetadata">Metadata for the properties of the model.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelMetadata" /> or <paramref name="propertyMetadata" /> parameter is null.</exception>
		// Token: 0x06004B6F RID: 19311 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ComplexModel(ModelMetadata modelMetadata, IEnumerable<ModelMetadata> propertyMetadata)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the metadata for the model.</summary>
		/// <returns>The metadata for the model.</returns>
		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06004B70 RID: 19312 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelMetadata ModelMetadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the metadata for the properties of the model.</summary>
		/// <returns>The metadata for the properties of the model.</returns>
		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x06004B71 RID: 19313 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public ReadOnlyCollection<ModelMetadata> PropertyMetadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a collection that contains entries that correspond to each property for which binding was attempted.</summary>
		/// <returns>Metadata for model properties for which model binding was attempted.</returns>
		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x06004B72 RID: 19314 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public IDictionary<ModelMetadata, ComplexModelResult> Results
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
