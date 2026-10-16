using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event.</summary>
	// Token: 0x020003D0 RID: 976
	public class ListChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the index of the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added, changed, or removed.</param>
		// Token: 0x06001F7B RID: 8059 RVA: 0x0006D53E File Offset: 0x0006B73E
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex)
			: this(listChangedType, newIndex, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change, the index of the affected item, and a <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the affected item.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The index of the item that was added or changed.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> describing the item.</param>
		// Token: 0x06001F7C RID: 8060 RVA: 0x0006D549 File Offset: 0x0006B749
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, PropertyDescriptor propDesc)
			: this(listChangedType, newIndex)
		{
			this.PropertyDescriptor = propDesc;
			this.OldIndex = newIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="propDesc">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, removed, or changed.</param>
		// Token: 0x06001F7D RID: 8061 RVA: 0x0006D561 File Offset: 0x0006B761
		public ListChangedEventArgs(ListChangedType listChangedType, PropertyDescriptor propDesc)
		{
			this.ListChangedType = listChangedType;
			this.PropertyDescriptor = propDesc;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListChangedEventArgs" /> class given the type of change and the old and new index of the item that was moved.</summary>
		/// <param name="listChangedType">A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</param>
		/// <param name="newIndex">The new index of the item that was moved.</param>
		/// <param name="oldIndex">The old index of the item that was moved.</param>
		// Token: 0x06001F7E RID: 8062 RVA: 0x0006D577 File Offset: 0x0006B777
		public ListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex)
		{
			this.ListChangedType = listChangedType;
			this.NewIndex = newIndex;
			this.OldIndex = oldIndex;
		}

		/// <summary>Gets the type of change.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ListChangedType" /> value indicating the type of change.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x0006D594 File Offset: 0x0006B794
		public ListChangedType ListChangedType { get; }

		/// <summary>Gets the index of the item affected by the change.</summary>
		/// <returns>The index of the affected by the change.</returns>
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x0006D59C File Offset: 0x0006B79C
		public int NewIndex { get; }

		/// <summary>Gets the old index of an item that has been moved.</summary>
		/// <returns>The old index of the moved item.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001F81 RID: 8065 RVA: 0x0006D5A4 File Offset: 0x0006B7A4
		public int OldIndex { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added, changed, or deleted.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> affected by the change.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001F82 RID: 8066 RVA: 0x0006D5AC File Offset: 0x0006B7AC
		public PropertyDescriptor PropertyDescriptor { get; }
	}
}
