using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Implements the default validator provider.</summary>
	// Token: 0x0200070C RID: 1804
	public class DataAnnotationsModelValidatorProvider : AssociatedValidatorProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.DataAnnotationsModelValidatorProvider" /> class.</summary>
		// Token: 0x06004BA8 RID: 19368 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public DataAnnotationsModelValidatorProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a value that indicates whether non-nullable value types are required.</summary>
		/// <returns>true if non-nullable value types are required; otherwise, false.</returns>
		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x000C82B8 File Offset: 0x000C64B8
		// (set) Token: 0x06004BAA RID: 19370 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static bool AddImplicitRequiredAttributeForValueTypes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a collection of validators for the model.</summary>
		/// <returns>The collection of validators.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attributes">The validation attributes.</param>
		// Token: 0x06004BAB RID: 19371 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context, IEnumerable<Attribute> attributes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Registers an adapter for client-side validation.</summary>
		/// <param name="attributeType">The type of the validation attribute.</param>
		/// <param name="adapterType">The type of the adapter.</param>
		// Token: 0x06004BAC RID: 19372 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterAdapter(Type attributeType, Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter factory for the validation provider.</summary>
		/// <param name="attributeType">The type of the attribute.</param>
		/// <param name="factory">The factory that will be used to create the validator object for the specified attribute.</param>
		// Token: 0x06004BAD RID: 19373 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterAdapterFactory(Type attributeType, DataAnnotationsModelValidationFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers the default adapter.</summary>
		/// <param name="adapterType">The type of the adapter.</param>
		// Token: 0x06004BAE RID: 19374 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterDefaultAdapter(Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers the default adapter factory.</summary>
		/// <param name="factory">The factory that will be used to create the validator object for the default adapter.</param>
		// Token: 0x06004BAF RID: 19375 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterDefaultAdapterFactory(DataAnnotationsModelValidationFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter for default object validation.</summary>
		/// <param name="adapterType">The type of the adapter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not derive from <see cref="T:System.Web.ModelBinding.ModelValidator" />.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not have a public constructor that accepts two parameters that are typed as <see cref="T:System.Web.ModelBinding.ModelMetadata" /> and <see cref="T:System.Web.ModelBinding.ModelBindingExecutionContext" />.</exception>
		// Token: 0x06004BB0 RID: 19376 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterDefaultValidatableObjectAdapter(Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter factory for the default object validation provider.</summary>
		/// <param name="factory">The factory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="factory" /> is null.</exception>
		// Token: 0x06004BB1 RID: 19377 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterDefaultValidatableObjectAdapterFactory(DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter for object validation.</summary>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="adapterType">The type of the adapter.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="adapterType" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The model type does not implement the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not implement <see cref="T:System.Web.ModelBinding.ModelValidator" />.</exception>
		/// <exception cref="T:System.ArgumentException">The adapter type does not have a public constructor that accepts two parameters that are typed <see cref="T:System.Web.ModelBinding.ModelMetadata" /> and <see cref="T:System.Web.ModelBinding.ModelBindingExecutionContext" />.</exception>
		// Token: 0x06004BB2 RID: 19378 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterValidatableObjectAdapter(Type modelType, Type adapterType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Registers an adapter factory for the object validation provider.</summary>
		/// <param name="modelType">The type of the model.</param>
		/// <param name="factory">The factory.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="factory" /> or <paramref name="type" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The model type does not implement the <see cref="T:System.ComponentModel.DataAnnotations.IValidatableObject" /> interface.</exception>
		// Token: 0x06004BB3 RID: 19379 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static void RegisterValidatableObjectAdapterFactory(Type modelType, DataAnnotationsValidatableObjectAdapterFactory factory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
