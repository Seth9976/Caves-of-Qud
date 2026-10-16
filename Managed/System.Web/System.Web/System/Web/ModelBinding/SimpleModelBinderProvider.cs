using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a model binder for a simple type.</summary>
	// Token: 0x02000731 RID: 1841
	public sealed class SimpleModelBinderProvider : ModelBinderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SimpleModelBinderProvider" /> class by using the specified model type and the model binder factory.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinderFactory">The model binder factory.</param>
		// Token: 0x06004C31 RID: 19505 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SimpleModelBinderProvider(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.SimpleModelBinderProvider" /> class by using the specified model type and the model binder.</summary>
		/// <param name="modelType">The model type.</param>
		/// <param name="modelBinder">The model binder.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelType" /> or <paramref name="modelBinder" /> parameter is null.</exception>
		// Token: 0x06004C32 RID: 19506 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public SimpleModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
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
		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x000C84CC File Offset: 0x000C66CC
		// (set) Token: 0x06004C35 RID: 19509 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		/// <summary>Returns a model binder by using the specified execution context and binding context.</summary>
		/// <returns>The model binder, or null if the attempt to get a model binder is unsuccessful.</returns>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="bindingContext">The binding context.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="modelBindingExecutionContext" /> or <paramref name="bindingContext" /> is null.</exception>
		// Token: 0x06004C36 RID: 19510 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
