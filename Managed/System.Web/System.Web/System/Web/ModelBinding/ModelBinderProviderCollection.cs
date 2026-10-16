using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for a collection of model binder providers.</summary>
	// Token: 0x020006ED RID: 1773
	public sealed class ModelBinderProviderCollection : Collection<ModelBinderProvider>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBinderProviderCollection" /> class.</summary>
		// Token: 0x06004AF5 RID: 19189 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelBinderProviderCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBinderProviderCollection" /> class by using the specified list of model binder providers.</summary>
		/// <param name="list">The list of model binder providers.</param>
		// Token: 0x06004AF6 RID: 19190 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelBinderProviderCollection(IList<ModelBinderProvider> list)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns the appropriate model binder for the specified execution context and binding context.</summary>
		/// <returns>The model binder.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> or the <paramref name="bindingContext" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">No model binder or model binder provider exists for the specified model type.</exception>
		// Token: 0x06004AF7 RID: 19191 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void InsertItem(int index, ModelBinderProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder to the collection for a generic type by using the specified model type and model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		// Token: 0x06004AF9 RID: 19193 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RegisterBinderForGenericType(Type modelType, Func<Type[], IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a generic type by using the specified model type and model binder type.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderType">The model binder type.</param>
		// Token: 0x06004AFA RID: 19194 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RegisterBinderForGenericType(Type modelType, Type modelBinderType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a generic type by using the specified model type and model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		// Token: 0x06004AFB RID: 19195 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RegisterBinderForGenericType(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a non-generic type by using the specified model type and model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		// Token: 0x06004AFC RID: 19196 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RegisterBinderForType(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Adds a model binder provider to the collection for a non-generic type by using the specified model type and model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		// Token: 0x06004AFD RID: 19197 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void RegisterBinderForType(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void SetItem(int index, ModelBinderProvider item)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
