using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeNamespace" /> objects.</summary>
	// Token: 0x02000320 RID: 800
	[Serializable]
	public class CodeNamespaceCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> class.</summary>
		// Token: 0x0600195F RID: 6495 RVA: 0x0004E33C File Offset: 0x0004C53C
		public CodeNamespaceCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> class that contains the elements of the specified source collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespaceCollection" /> with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001960 RID: 6496 RVA: 0x0006063D File Offset: 0x0005E83D
		public CodeNamespaceCollection(CodeNamespaceCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> class that contains the specified array of <see cref="T:System.CodeDom.CodeNamespace" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeNamespace" /> objects with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are null.</exception>
		// Token: 0x06001961 RID: 6497 RVA: 0x0006064C File Offset: 0x0005E84C
		public CodeNamespaceCollection(CodeNamespace[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespace" /> at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000514 RID: 1300
		public CodeNamespace this[int index]
		{
			get
			{
				return (CodeNamespace)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeNamespace" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to add. </param>
		// Token: 0x06001964 RID: 6500 RVA: 0x00051026 File Offset: 0x0004F226
		public int Add(CodeNamespace value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeNamespace" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeNamespace" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001965 RID: 6501 RVA: 0x00060670 File Offset: 0x0005E870
		public void AddRange(CodeNamespace[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Adds the contents of the specified <see cref="T:System.CodeDom.CodeNamespaceCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001966 RID: 6502 RVA: 0x000606A4 File Offset: 0x0005E8A4
		public void AddRange(CodeNamespaceCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int count = value.Count;
			for (int i = 0; i < count; i++)
			{
				this.Add(value[i]);
			}
		}

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeNamespace" /> object.</summary>
		/// <returns>true if the <see cref="T:System.CodeDom.CodeNamespace" /> is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to search for in the collection. </param>
		// Token: 0x06001967 RID: 6503 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public bool Contains(CodeNamespace value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeNamespaceCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06001968 RID: 6504 RVA: 0x000510CE File Offset: 0x0004F2CE
		public void CopyTo(CodeNamespace[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeNamespace" /> object in the <see cref="T:System.CodeDom.CodeNamespaceCollection" />, if it exists in the collection.</summary>
		/// <returns>The index of the specified <see cref="T:System.CodeDom.CodeNamespace" />, if it is found, in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to locate. </param>
		// Token: 0x06001969 RID: 6505 RVA: 0x000510DD File Offset: 0x0004F2DD
		public int IndexOf(CodeNamespace value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeNamespace" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the new item should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to insert. </param>
		// Token: 0x0600196A RID: 6506 RVA: 0x000510EB File Offset: 0x0004F2EB
		public void Insert(int index, CodeNamespace value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeNamespace" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeNamespace" /> to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x0600196B RID: 6507 RVA: 0x0005113D File Offset: 0x0004F33D
		public void Remove(CodeNamespace value)
		{
			base.List.Remove(value);
		}
	}
}
