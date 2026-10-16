using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an abstract base class for a custom metadata provider.</summary>
	// Token: 0x020006F0 RID: 1776
	public abstract class ModelMetadataProvider
	{
		/// <summary>When overridden in a derived class, initializes a new instance of the object that derives from the <see cref="T:System.Web.ModelBinding.ModelMetadataProvider" /> class.</summary>
		// Token: 0x06004B33 RID: 19251 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ModelMetadataProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Web.ModelBinding.ModelMetadata" /> object for all properties of a model.</summary>
		/// <returns>A collection of <see cref="T:System.Web.ModelBinding.ModelMetadata" /> objects.</returns>
		/// <param name="container">The container object.</param>
		/// <param name="containerType">The type of the container object.</param>
		// Token: 0x06004B34 RID: 19252
		public abstract IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType);

		/// <summary>Returns metadata for the specified property.</summary>
		/// <returns>Metadata for the specified property.</returns>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="containerType">The type of the container.</param>
		/// <param name="propertyName">The name of the property.</param>
		// Token: 0x06004B35 RID: 19253
		public abstract ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName);

		/// <summary>Returns metadata for the specified model type.</summary>
		/// <returns>Metadata for the specified model type.</returns>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		// Token: 0x06004B36 RID: 19254
		public abstract ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType);
	}
}
