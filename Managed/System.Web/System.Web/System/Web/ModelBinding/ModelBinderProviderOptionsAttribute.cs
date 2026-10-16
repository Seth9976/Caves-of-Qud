using System;

namespace System.Web.ModelBinding
{
	/// <summary>Represents an attribute that specifies options for a model-binder provider.</summary>
	// Token: 0x0200051B RID: 1307
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ModelBinderProviderOptionsAttribute : Attribute
	{
		/// <summary>Gets or sets a value that specifies whether a model binder provider should appear at the beginning of the list of model-binder providers.</summary>
		/// <returns>true if the model binder provider should go at the beginning of the list; otherwise, false.</returns>
		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x0009A594 File Offset: 0x00098794
		// (set) Token: 0x060039AD RID: 14765 RVA: 0x0009A59C File Offset: 0x0009879C
		public bool FrontOfList { get; set; }
	}
}
