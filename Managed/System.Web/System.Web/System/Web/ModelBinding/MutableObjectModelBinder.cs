using System;
using System.Collections.Generic;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Maps a browser request to a mutable data object.</summary>
	// Token: 0x02000725 RID: 1829
	public class MutableObjectModelBinder : IModelBinder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.MutableObjectModelBinder" /> class.</summary>
		// Token: 0x06004C09 RID: 19465 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public MutableObjectModelBinder()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Binds the model by using the specified execution context and binding context.</summary>
		/// <returns>true in all cases.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <exception cref="T:System.InvalidOperationException">The model could not be bound because one or more required properties are missing. (The message specifies the first missing property.)</exception>
		// Token: 0x06004C0A RID: 19466 RVA: 0x000C8478 File Offset: 0x000C6678
		public virtual bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Returns a value that indicates whether a property can be updated.</summary>
		/// <returns>true if the property can be updated; otherwise, false.</returns>
		/// <param name="propertyMetadata">Metadata for the property to be evaluated.</param>
		// Token: 0x06004C0B RID: 19467 RVA: 0x000C8494 File Offset: 0x000C6694
		protected virtual bool CanUpdateProperty(ModelMetadata propertyMetadata)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Creates an instance of the model.</summary>
		/// <returns>The model object.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C0C RID: 19468 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual object CreateModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Creates a model instance if an instance does not yet exist in the binding context.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C0D RID: 19469 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void EnsureModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns metadata for properties of the model.</summary>
		/// <returns>Metadata for properties of the model.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004C0E RID: 19470 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		protected virtual IEnumerable<ModelMetadata> GetMetadataForProperties(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Sets the value of a specified property.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <param name="propertyMetadata">Metadata for the property to set.</param>
		/// <param name="complexModelResult">Validation information about the property.</param>
		// Token: 0x06004C0F RID: 19471 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void SetProperty(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, ModelMetadata propertyMetadata, ComplexModelResult complexModelResult)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
