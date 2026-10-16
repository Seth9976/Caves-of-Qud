using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides an attribute that specifies that model binding should exclude a property.</summary>
	// Token: 0x02000510 RID: 1296
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class BindNeverAttribute : BindingBehaviorAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindNeverAttribute" /> class.</summary>
		// Token: 0x0600398F RID: 14735 RVA: 0x0009A2D9 File Offset: 0x000984D9
		public BindNeverAttribute()
			: base(BindingBehavior.Never)
		{
		}
	}
}
