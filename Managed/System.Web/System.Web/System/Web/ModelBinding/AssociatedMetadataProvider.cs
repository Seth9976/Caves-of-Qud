using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an abstract class to implement a metadata provider.</summary>
	// Token: 0x020006F8 RID: 1784
	public abstract class AssociatedMetadataProvider : ModelMetadataProvider
	{
		/// <summary>When overridden in a derived class, initializes a new instance of the class that derives from the <see cref="T:System.Web.ModelBinding.AssociatedMetadataProvider" /> class.</summary>
		// Token: 0x06004B5D RID: 19293 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected AssociatedMetadataProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When overridden in a derived class, creates metadata for a model.</summary>
		/// <returns>Metadata for a model.</returns>
		/// <param name="attributes">The attributes. </param>
		/// <param name="containerType">The type of the container, or null if there is no container.</param>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="propertyName">The name of the property, or null if the model is not a property.</param>
		// Token: 0x06004B5E RID: 19294
		protected abstract ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName);

		/// <summary>Enables derived classes to filter the list of attributes.</summary>
		/// <returns>A list of attributes.</returns>
		/// <param name="containerType">The type of the container.</param>
		/// <param name="propertyDescriptor">The property descriptor.</param>
		/// <param name="attributes">The attributes.</param>
		// Token: 0x06004B5F RID: 19295 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		protected virtual IEnumerable<Attribute> FilterAttributes(Type containerType, PropertyDescriptor propertyDescriptor, IEnumerable<Attribute> attributes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>When overridden in a derived class, returns metadata for all properties.</summary>
		/// <returns>Metadata for all properties.</returns>
		/// <param name="container">The container.</param>
		/// <param name="containerType">The type of the container.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="containerType" /> parameter is null.</exception>
		// Token: 0x06004B60 RID: 19296 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>When overridden in a derived class, returns metadata for a property specified by a property descriptor object.</summary>
		/// <returns>Metadata for the specified property.</returns>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="containerType">The type of the container.</param>
		/// <param name="propertyDescriptor">The property descriptor.</param>
		// Token: 0x06004B61 RID: 19297 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, returns metadata for a property specified by a property name.</summary>
		/// <returns>Metadata for the specified property.</returns>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="containerType">The type of the container.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="containerType" /> parameter is null.-or-The <paramref name="propertyName" /> parameter is null or empty.-or-A property that has the name specified by <paramref name="propertyName" /> cannot be found in the <paramref name="containerType" /> type.</exception>
		// Token: 0x06004B62 RID: 19298 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, returns metadata for the model type.</summary>
		/// <returns>Metadata for the model type.</returns>
		/// <param name="modelAccessor">The model accessor.</param>
		/// <param name="modelType">The type of the model.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelType" /> parameter is null.</exception>
		// Token: 0x06004B63 RID: 19299 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When overridden in a derived class, returns a descriptor object for a specified type. </summary>
		/// <returns>The type descriptor object.</returns>
		/// <param name="type">The type.</param>
		// Token: 0x06004B64 RID: 19300 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
