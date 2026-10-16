using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.ModelBinding
{
	/// <summary>Provides data for the <see cref="E:System.Web.ModelBinding.ModelValidationNode.Validating" /> event.</summary>
	// Token: 0x020006F5 RID: 1781
	public sealed class ModelValidatingEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ModelValidatingEventArgs" /> class.</summary>
		/// <param name="modelBindingExecutionContext">The execution context.</param>
		/// <param name="parentNode">The parent model validation node.</param>
		// Token: 0x06004B55 RID: 19285 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelValidatingEventArgs(ModelBindingExecutionContext modelBindingExecutionContext, ModelValidationNode parentNode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the execution context.</summary>
		/// <returns>The execution context.</returns>
		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public ModelBindingExecutionContext ModelBindingExecutionContext
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the parent model validation node.</summary>
		/// <returns>The parent model validation node.</returns>
		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06004B57 RID: 19287 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
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
