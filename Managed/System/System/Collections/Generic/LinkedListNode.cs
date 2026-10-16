using System;

namespace System.Collections.Generic
{
	/// <summary>Represents a node in a <see cref="T:System.Collections.Generic.LinkedList`1" />. This class cannot be inherited.</summary>
	/// <typeparam name="T">Specifies the element type of the linked list.</typeparam>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020004CF RID: 1231
	public sealed class LinkedListNode<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> class, containing the specified value.</summary>
		/// <param name="value">The value to contain in the <see cref="T:System.Collections.Generic.LinkedListNode`1" />.</param>
		// Token: 0x060027D6 RID: 10198 RVA: 0x00089C0B File Offset: 0x00087E0B
		public LinkedListNode(T value)
		{
			this.item = value;
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00089C1A File Offset: 0x00087E1A
		internal LinkedListNode(LinkedList<T> list, T value)
		{
			this.list = list;
			this.item = value;
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to.</summary>
		/// <returns>A reference to the <see cref="T:System.Collections.Generic.LinkedList`1" /> that the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> belongs to, or null if the <see cref="T:System.Collections.Generic.LinkedListNode`1" /> is not linked.</returns>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060027D8 RID: 10200 RVA: 0x00089C30 File Offset: 0x00087E30
		public LinkedList<T> List
		{
			get
			{
				return this.list;
			}
		}

		/// <summary>Gets the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the next node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the last element (<see cref="P:System.Collections.Generic.LinkedList`1.Last" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060027D9 RID: 10201 RVA: 0x00089C38 File Offset: 0x00087E38
		public LinkedListNode<T> Next
		{
			get
			{
				if (this.next != null && this.next != this.list.head)
				{
					return this.next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />.</summary>
		/// <returns>A reference to the previous node in the <see cref="T:System.Collections.Generic.LinkedList`1" />, or null if the current node is the first element (<see cref="P:System.Collections.Generic.LinkedList`1.First" />) of the <see cref="T:System.Collections.Generic.LinkedList`1" />.</returns>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060027DA RID: 10202 RVA: 0x00089C5D File Offset: 0x00087E5D
		public LinkedListNode<T> Previous
		{
			get
			{
				if (this.prev != null && this != this.list.head)
				{
					return this.prev;
				}
				return null;
			}
		}

		/// <summary>Gets the value contained in the node.</summary>
		/// <returns>The value contained in the node.</returns>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x00089C7D File Offset: 0x00087E7D
		// (set) Token: 0x060027DC RID: 10204 RVA: 0x00089C85 File Offset: 0x00087E85
		public T Value
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x00089C8E File Offset: 0x00087E8E
		internal void Invalidate()
		{
			this.list = null;
			this.next = null;
			this.prev = null;
		}

		// Token: 0x0400158F RID: 5519
		internal LinkedList<T> list;

		// Token: 0x04001590 RID: 5520
		internal LinkedListNode<T> next;

		// Token: 0x04001591 RID: 5521
		internal LinkedListNode<T> prev;

		// Token: 0x04001592 RID: 5522
		internal T item;
	}
}
