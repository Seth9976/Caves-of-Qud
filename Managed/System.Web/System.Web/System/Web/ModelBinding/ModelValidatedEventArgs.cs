using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides data for the <see cref="E:System.Web.ModelBinding.ModelValidationNode.Validated" /> event.</summary>
	// Token: 0x020006F4 RID: 1780
	public sealed class ModelValidatedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatedEventArgs" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="parentNode">The parent model validation node.</param>
		// Token: 0x06004B52 RID: 19282 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelValidatedEventArgs(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the execution context.</summary>
		/// <returns>The execution context.</returns>
		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06004B53 RID: 19283 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelBindingExecutionContext ModelBindingExecutionContext
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the parent model-validation node.</summary>
		/// <returns>The model-validation node.</returns>
		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06004B54 RID: 19284 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelValidationNode ParentNode
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
