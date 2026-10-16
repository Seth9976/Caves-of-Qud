using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState
{
	/// <summary>Defines the contract for the collection used by ASP.NET session state to manage session.</summary>
	// Token: 0x02000492 RID: 1170
	public interface ISessionStateItemCollection : ICollection, IEnumerable
	{
		/// <summary>Removes all values and keys from the session-state collection.</summary>
		// Token: 0x06003530 RID: 13616
		void Clear();

		/// <summary>Deletes an item from the collection.</summary>
		/// <param name="name">The name of the item to delete from the collection.</param>
		// Token: 0x06003531 RID: 13617
		void Remove(string name);

		/// <summary>Deletes an item at a specified index from the collection.</summary>
		/// <param name="index">The index of the item to remove from the collection.</param>
		// Token: 0x06003532 RID: 13618
		void RemoveAt(int index);

		/// <summary>Gets or sets a value indicating whether the collection has been marked as changed.</summary>
		/// <returns>true if the <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> contents have been changed; otherwise, false.</returns>
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06003533 RID: 13619
		// (set) Token: 0x06003534 RID: 13620
		bool Dirty { get; set; }

		/// <summary>Gets or sets a value in the collection by numerical index.</summary>
		/// <returns>The value in the collection stored at the specified index.</returns>
		/// <param name="index">The numerical index of the value in the collection.</param>
		// Token: 0x170010ED RID: 4333
		object this[int index] { get; set; }

		/// <summary>Gets or sets a value in the collection by name.</summary>
		/// <returns>The value in the collection with the specified name.</returns>
		/// <param name="name">The key name of the value in the collection.</param>
		// Token: 0x170010EE RID: 4334
		object this[string name] { get; set; }

		/// <summary>Gets a collection of the variable names for all values stored in the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the collection keys.</returns>
		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x06003539 RID: 13625
		NameObjectCollectionBase.KeysCollection Keys { get; }
	}
}
