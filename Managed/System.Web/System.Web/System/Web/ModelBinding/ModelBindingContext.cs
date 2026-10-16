using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides the context in which a model binder functions.</summary>
	// Token: 0x020006EC RID: 1772
	public class ModelBindingContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBindingContext" /> class.</summary>
		// Token: 0x06004AE1 RID: 19169 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelBindingContext()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelBindingContext" /> class by using an existing instance of the class.</summary>
		/// <param name="bindingContext">The binding context.</param>
		// Token: 0x06004AE2 RID: 19170 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelBindingContext(ModelBindingContext bindingContext)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the model.</summary>
		/// <returns>The model.</returns>
		/// <exception cref="T:System.InvalidOperationException">Model metadata must be set before accessing this property.</exception>
		// Token: 0x1700172F RID: 5935
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AE4 RID: 19172 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public object Model
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a collection of model-binder providers.</summary>
		/// <returns>The collection of model-binder providers. If the collection does not exist yet, a new collection of model-binder providers is created and returned.</returns>
		// Token: 0x17001730 RID: 5936
		// (get) Token: 0x06004AE5 RID: 19173 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AE6 RID: 19174 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelBinderProviderCollection ModelBinderProviders
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets metadata for the model.</summary>
		/// <returns>Metadata for the model.</returns>
		// Token: 0x17001731 RID: 5937
		// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AE8 RID: 19176 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelMetadata ModelMetadata
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the model.</summary>
		/// <returns>The name of the model.</returns>
		// Token: 0x17001732 RID: 5938
		// (get) Token: 0x06004AE9 RID: 19177 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AEA RID: 19178 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public string ModelName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the model state.</summary>
		/// <returns>The model state, or a new instance of <see cref="T:System.Web.ModelBinding.ModelStateDictionary" /> if the model state does not exist yet.</returns>
		// Token: 0x17001733 RID: 5939
		// (get) Token: 0x06004AEB RID: 19179 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AEC RID: 19180 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelStateDictionary ModelState
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the type of the model.</summary>
		/// <returns>The type of the model.</returns>
		/// <exception cref="T:System.InvalidOperationException">Model metadata must be set before accessing this property.</exception>
		// Token: 0x17001734 RID: 5940
		// (get) Token: 0x06004AED RID: 19181 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Type ModelType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets metadata for properties of the model.</summary>
		/// <returns>Metadata for properties of the model.</returns>
		// Token: 0x17001735 RID: 5941
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public IDictionary<string, ModelMetadata> PropertyMetadata
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets or sets a value that indicates whether client input is validated.</summary>
		/// <returns>true if client input is validated; otherwise, false.</returns>
		// Token: 0x17001736 RID: 5942
		// (get) Token: 0x06004AEF RID: 19183 RVA: 0x000C8050 File Offset: 0x000C6250
		// (set) Token: 0x06004AF0 RID: 19184 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool ValidateRequest
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

		/// <summary>Gets or sets the validation node.</summary>
		/// <returns>The validation node, or a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidationNode" /> class if the validation node does not exist yet.</returns>
		// Token: 0x17001737 RID: 5943
		// (get) Token: 0x06004AF1 RID: 19185 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AF2 RID: 19186 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelValidationNode ValidationNode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the value provider.</summary>
		/// <returns>The value provider.</returns>
		// Token: 0x17001738 RID: 5944
		// (get) Token: 0x06004AF3 RID: 19187 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004AF4 RID: 19188 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public IValueProvider ValueProvider
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
