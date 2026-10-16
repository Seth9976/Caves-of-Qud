using System;

namespace System.Web.ModelBinding
{
	/// <summary>Specifies the binder type for a model type.</summary>
	// Token: 0x02000516 RID: 1302
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class ExtensibleModelBinderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.ExtensibleModelBinderAttribute" /> class.</summary>
		/// <param name="binderType">The model binder type.</param>
		// Token: 0x060039A1 RID: 14753 RVA: 0x0009A563 File Offset: 0x00098763
		public ExtensibleModelBinderAttribute(Type binderType)
		{
			this.BinderType = binderType;
		}

		/// <summary>Gets the model binder type.</summary>
		/// <returns>The model binder type.</returns>
		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060039A2 RID: 14754 RVA: 0x0009A572 File Offset: 0x00098772
		// (set) Token: 0x060039A3 RID: 14755 RVA: 0x0009A57A File Offset: 0x0009877A
		public Type BinderType { get; private set; }

		/// <summary>Gets or sets a value that specifies whether the prefix check should be suppressed.</summary>
		/// <returns>true if the prefix check should be suppressed; otherwise, false.</returns>
		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x0009A583 File Offset: 0x00098783
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x0009A58B File Offset: 0x0009878B
		public bool SuppressPrefixCheck { get; set; }
	}
}
