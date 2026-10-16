using System;
using System.Collections.Generic;
using System.ComponentModel;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an abstract class for classes that implement a validation provider.</summary>
	// Token: 0x020006F9 RID: 1785
	public abstract class AssociatedValidatorProvider : ModelValidatorProvider
	{
		/// <summary>When implemented in a derived class, initializes a new instance of the <see cref="T:System.Web.ModelBinding.AssociatedValidatorProvider" /> class.</summary>
		// Token: 0x06004B65 RID: 19301 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected AssociatedValidatorProvider()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a type descriptor for the specified type.</summary>
		/// <returns>The type descriptor.</returns>
		/// <param name="type">The type.</param>
		// Token: 0x06004B66 RID: 19302 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the validators for the model using the specified metadata and execution context.</summary>
		/// <returns>The validators.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="metadata" /> or <paramref name="context" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The property name in the metadata does not refer to one of the properties of the container type.</exception>
		// Token: 0x06004B67 RID: 19303 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public sealed override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>When implemented in a derived type, gets the validators for the model, using the specified metadata, execution context, and attributes.</summary>
		/// <returns>The validators.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		/// <param name="attributes">The attributes.</param>
		// Token: 0x06004B68 RID: 19304
		protected abstract IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ModelBindingExecutionContext context, IEnumerable<Attribute> attributes);
	}
}
