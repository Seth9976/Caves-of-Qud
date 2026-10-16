using System;
using System.Linq;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides an extension method for objects that implement the <see cref="T:System.Linq.IQueryable`1" /> interface.</summary>
	// Token: 0x02000798 RID: 1944
	public static class QueryExtensions
	{
		/// <summary>Specifies a sort order.</summary>
		/// <returns>The sorted version of the <paramref name="source" /> object.</returns>
		/// <param name="source">The object to sort.</param>
		/// <param name="sortExpression">The sort expression.</param>
		/// <typeparam name="T">The generic type of the object.</typeparam>
		// Token: 0x06004E71 RID: 20081 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string sortExpression)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}
