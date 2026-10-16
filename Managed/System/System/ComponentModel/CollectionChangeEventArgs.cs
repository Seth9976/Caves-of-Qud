using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.Data.DataColumnCollection.CollectionChanged" /> event.</summary>
	// Token: 0x0200038E RID: 910
	public class CollectionChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> class.</summary>
		/// <param name="action">One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values that specifies how the collection changed. </param>
		/// <param name="element">An <see cref="T:System.Object" /> that specifies the instance of the collection where the change occurred. </param>
		// Token: 0x06001DE1 RID: 7649 RVA: 0x00069A10 File Offset: 0x00067C10
		public CollectionChangeEventArgs(CollectionChangeAction action, object element)
		{
			this.Action = action;
			this.Element = element;
		}

		/// <summary>Gets an action that specifies how the collection changed.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.CollectionChangeAction" /> values.</returns>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x00069A26 File Offset: 0x00067C26
		public virtual CollectionChangeAction Action { get; }

		/// <summary>Gets the instance of the collection with the change.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the instance of the collection with the change, or null if you refresh the collection.</returns>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00069A2E File Offset: 0x00067C2E
		public virtual object Element { get; }
	}
}
