using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a container for model validation information.</summary>
	// Token: 0x020006F3 RID: 1779
	public sealed class ModelValidationNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> class, using the model metadata and state key.</summary>
		/// <param name="modelMetadata">The model metadata.</param>
		/// <param name="modelStateKey">The model state key.</param>
		// Token: 0x06004B42 RID: 19266 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> class, using the model metadata, the model state key, and child model-validation nodes.</summary>
		/// <param name="modelMetadata">The model metadata.</param>
		/// <param name="modelStateKey">The model state key.</param>
		/// <param name="childNodes">The model child nodes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelMetadata" /> or <paramref name="modelStateKey" /> parameter is null.</exception>
		// Token: 0x06004B43 RID: 19267 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelValidationNode(ModelMetadata modelMetadata, string modelStateKey, IEnumerable<ModelValidationNode> childNodes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the child nodes.</summary>
		/// <returns>The child nodes.</returns>
		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06004B44 RID: 19268 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public ICollection<ModelValidationNode> ChildNodes
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the model metadata.</summary>
		/// <returns>The model metadata.</returns>
		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06004B45 RID: 19269 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelMetadata ModelMetadata
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the model state key.</summary>
		/// <returns>The model state key.</returns>
		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string ModelStateKey
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether validation should be suppressed.</summary>
		/// <returns>true if validation should be suppressed; otherwise, false.</returns>
		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x06004B47 RID: 19271 RVA: 0x000C81A0 File Offset: 0x000C63A0
		// (set) Token: 0x06004B48 RID: 19272 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool SuppressValidation
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

		/// <summary>Gets or sets a value that indicates whether all properties of the model should be validated.</summary>
		/// <returns>true if all properties of the model should be validated, or false if validation should be skipped.</returns>
		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x06004B49 RID: 19273 RVA: 0x000C81BC File Offset: 0x000C63BC
		// (set) Token: 0x06004B4A RID: 19274 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool ValidateAllProperties
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

		/// <summary>Occurs when the model has been validated.</summary>
		// Token: 0x1400012D RID: 301
		// (add) Token: 0x06004B4B RID: 19275 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x06004B4C RID: 19276 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public event EventHandler<ModelValidatedEventArgs> Validated
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Occurs when the model is being validated.</summary>
		// Token: 0x1400012E RID: 302
		// (add) Token: 0x06004B4D RID: 19277 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x06004B4E RID: 19278 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public event EventHandler<ModelValidatingEventArgs> Validating
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Combines the current <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> instance with a specified <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> instance.</summary>
		/// <param name="otherNode">The model validation node to combine with the current instance.</param>
		// Token: 0x06004B4F RID: 19279 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CombineWith(ModelValidationNode otherNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Validates the model using the specified execution context.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="modelBindingExecutionContext" /> parameter is null.</exception>
		// Token: 0x06004B50 RID: 19280 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Validate(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Validates the model using the specified execution context and parent node.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="parentNode">The parent node.</param>
		// Token: 0x06004B51 RID: 19281 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Validate(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
