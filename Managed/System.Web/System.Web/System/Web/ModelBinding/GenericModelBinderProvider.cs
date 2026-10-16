using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Gets a model binder for a generic type.</summary>
	// Token: 0x02000717 RID: 1815
	public sealed class GenericModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.GenericModelBinderProvider" /> class by using the specified model type and model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelType" /> or <paramref name="modelBinderFactory" /> is null.</exception>
		// Token: 0x06004BD3 RID: 19411 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public GenericModelBinderProvider(Type modelType, Func<Type[], IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.GenericModelBinderProvider" /> class by using the specified model type and model binder type.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderType">The model binder type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelType" /> or <paramref name="modelBinderType" /> is null.</exception>
		// Token: 0x06004BD4 RID: 19412 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public GenericModelBinderProvider(Type modelType, Type modelBinderType)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.GenericModelBinderProvider" /> by using the specified model type and model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelType" /> or <paramref name="modelBinder" /> is null.</exception>
		// Token: 0x06004BD5 RID: 19413 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public GenericModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x06004BD6 RID: 19414 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ModelType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the prefix check should be suppressed.</summary>
		/// <returns>true if the prefix check should be suppressed; otherwise, false.</returns>
		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06004BD7 RID: 19415 RVA: 0x000C837C File Offset: 0x000C657C
		// (set) Token: 0x06004BD8 RID: 19416 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool SuppressPrefixCheck
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a model binder</summary>
		/// <returns>A model binder, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004BD9 RID: 19417 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
