using System;

namespace System.ComponentModel
{
	/// <summary>Provides support for rolling back the changes</summary>
	// Token: 0x02000401 RID: 1025
	public interface IRevertibleChangeTracking : IChangeTracking
	{
		/// <summary>Resets the object’s state to unchanged by rejecting the modifications.</summary>
		// Token: 0x06002146 RID: 8518
		void RejectChanges();
	}
}
