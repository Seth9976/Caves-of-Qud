using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Implements the default model metadata provider.</summary>
	// Token: 0x02000709 RID: 1801
	public class DataAnnotationsModelMetadataProvider : AssociatedMetadataProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelMetadataProvider" /> class.</summary>
		// Token: 0x06004B9B RID: 19355 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DataAnnotationsModelMetadataProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates metadata for a specified model.</summary>
		/// <returns>Metadata for a model.</returns>
		/// <param name="attributes">The attributes.</param>
		/// <param name="containerType">The type of the container, or null if there is no container.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="propertyName">The name of the property, or null if the model is not a property.</param>
		// Token: 0x06004B9C RID: 19356 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
