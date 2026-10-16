using System;
using System.ComponentModel.DataAnnotations;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for common metadata, for the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelMetadataProvider" /> class, and for the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidator" /> class for a data model.</summary>
	// Token: 0x02000708 RID: 1800
	public class DataAnnotationsModelMetadata : ModelMetadata
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelMetadata" /> class.</summary>
		/// <param name="provider">The provider object.</param>
		/// <param name="containerType">The type of the container, or null if there is no container.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="propertyName">The name of the property, or null if the model is not a property.</param>
		/// <param name="displayColumnAttribute">The display column attribute.</param>
		// Token: 0x06004B99 RID: 19353 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DataAnnotationsModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns text to display for the model when the model represents a complex object. </summary>
		/// <returns>The text to display for a complex object. If there is a <see cref="T:System.ComponentModel.DataAnnotations.DisplayColumnAttribute" /> attribute for the model, the default is the name of the display column. Otherwise the default is determined by calling the <see cref="M:System.Web.ModelBinding.ModelMetadata.GetSimpleDisplayText" /> method of the <see cref="T:System.Web.ModelBinding.ModelMetadata" /> base class.</returns>
		// Token: 0x06004B9A RID: 19354 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected override string GetSimpleDisplayText()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
