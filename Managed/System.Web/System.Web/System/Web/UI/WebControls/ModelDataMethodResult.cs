using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Encapsulates the result of a data method operation.</summary>
	// Token: 0x020006B0 RID: 1712
	public class ModelDataMethodResult
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ModelDataMethodResult" /> class.</summary>
		/// <param name="returnValue">The return value of the data method.</param>
		/// <param name="outputParameters">A dictionary that contains the values of out and ref parameters.</param>
		// Token: 0x06004843 RID: 18499 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ModelDataMethodResult(object returnValue, OrderedDictionary outputParameters)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a read-only dictionary that contains the values of out and ref parameters.</summary>
		/// <returns>A dictionary that contains the values of out and ref parameters.</returns>
		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06004844 RID: 18500 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public OrderedDictionary OutputParameters
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the return value of the data method.</summary>
		/// <returns>The return value of the data method: an <see cref="T:System.Collections.IEnumerable" /> collection for a select operation, or an integer indicating the number of affected rows for update, delete, and insert operations.</returns>
		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06004845 RID: 18501 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public object ReturnValue
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
