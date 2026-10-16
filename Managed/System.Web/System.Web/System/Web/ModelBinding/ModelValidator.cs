using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a base class for implementing validation logic.</summary>
	// Token: 0x020006F1 RID: 1777
	public abstract class ModelValidator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidator" /> class.</summary>
		/// <param name="metadata">The metadata.</param>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="metadata" /> or <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004B37 RID: 19255 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ModelValidator(ModelMetadata metadata, ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When implemented in a derived class, gets a value that indicates whether the model is required.</summary>
		/// <returns>true if the model is required; otherwise false. The default is false.</returns>
		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x06004B38 RID: 19256 RVA: 0x000C8184 File Offset: 0x000C6384
		public virtual bool IsRequired
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>When implemented in a derived class, gets the metadata for the model validator.</summary>
		/// <returns>The metadata.</returns>
		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal ModelMetadata Metadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>When implemented in a derived class, gets the execution context.</summary>
		/// <returns>The execution context.</returns>
		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected internal ModelBindingExecutionContext ModelBindingExecutionContext
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a composite model validator for the model.</summary>
		/// <returns>The composite model validator for the model.</returns>
		/// <param name="metadata">The metadata.</param>
		/// <param name="context">The execution context.</param>
		// Token: 0x06004B3B RID: 19259 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public static ModelValidator GetModelValidator(ModelMetadata metadata, ModelBindingExecutionContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>When implemented in a derived class, validates the object.</summary>
		/// <returns>A collection of validation results.</returns>
		/// <param name="container">The container.</param>
		// Token: 0x06004B3C RID: 19260
		public abstract IEnumerable<ModelValidationResult> Validate(object container);
	}
}
