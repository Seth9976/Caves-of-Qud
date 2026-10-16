using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an attribute that specifies that a property is required for model binding.</summary>
	// Token: 0x02000511 RID: 1297
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class BindRequiredAttribute : BindingBehaviorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindRequiredAttribute" /> class.</summary>
		// Token: 0x06003990 RID: 14736 RVA: 0x0009A2E2 File Offset: 0x000984E2
		public BindRequiredAttribute()
			: base(BindingBehavior.Required)
		{
		}
	}
}
