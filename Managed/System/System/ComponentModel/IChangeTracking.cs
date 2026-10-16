using System;

namespace System.ComponentModel
{
	/// <summary>Defines the mechanism for querying the object for changes and resetting of the changed status.</summary>
	// Token: 0x020003FF RID: 1023
	public interface IChangeTracking
	{
		/// <summary>Gets the object's changed status.</summary>
		/// <returns>true if the object’s content has changed since the last call to <see cref="M:System.ComponentModel.IChangeTracking.AcceptChanges" />; otherwise, false.</returns>
		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06002141 RID: 8513
		bool IsChanged { get; }

		/// <summary>Resets the object’s state to unchanged by accepting the modifications.</summary>
		// Token: 0x06002142 RID: 8514
		void AcceptChanges();
	}
}
