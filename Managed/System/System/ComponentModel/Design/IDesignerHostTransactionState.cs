using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies methods for the designer host to report on the state of transactions.</summary>
	// Token: 0x02000460 RID: 1120
	public interface IDesignerHostTransactionState
	{
		/// <summary>Gets a value indicating whether the designer host is closing a transaction. </summary>
		/// <returns>true if the designer is closing a transaction; otherwise, false. </returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002447 RID: 9287
		bool IsClosingTransaction { get; }
	}
}
