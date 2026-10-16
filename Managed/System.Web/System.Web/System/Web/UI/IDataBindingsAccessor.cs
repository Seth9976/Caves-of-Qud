using System;

namespace System.Web.UI
{
	/// <summary>Allows access to the collection of data-binding expressions on a control at design time.</summary>
	// Token: 0x02000169 RID: 361
	public interface IDataBindingsAccessor
	{
		/// <summary>Gets a collection of all data bindings on the control. This property is read-only.</summary>
		/// <returns>The collection of data bindings.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000F23 RID: 3875
		DataBindingCollection DataBindings { get; }

		/// <summary>Gets a value indicating whether the control contains any data-binding logic.</summary>
		/// <returns>true if the control contains data binding logic.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000F24 RID: 3876
		bool HasDataBindings { get; }
	}
}
