using System;

namespace System.Web.UI
{
	/// <summary>Defines the properties a class must implement to support collections of expressions.</summary>
	// Token: 0x0200016E RID: 366
	public interface IExpressionsAccessor
	{
		/// <summary>Gets a value indicating whether the instance of the class that implements this interface has any properties bound by an expression.</summary>
		/// <returns>true if the control has properties set through expressions; otherwise, false. </returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000F30 RID: 3888
		bool HasExpressions { get; }

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> containing <see cref="T:System.Web.UI.ExpressionBinding" /> objects that represent the properties and expressions for a control.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000F31 RID: 3889
		ExpressionBindingCollection Expressions { get; }
	}
}
