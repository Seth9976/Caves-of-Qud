using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeCatchClause" /> objects.</summary>
	// Token: 0x02000300 RID: 768
	[Serializable]
	public class CodeCatchClauseCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class.</summary>
		// Token: 0x0600187A RID: 6266 RVA: 0x0004E33C File Offset: 0x0004C53C
		public CodeCatchClauseCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600187B RID: 6267 RVA: 0x0005F565 File Offset: 0x0005D765
		public CodeCatchClauseCollection(CodeCatchClauseCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are null.</exception>
		// Token: 0x0600187C RID: 6268 RVA: 0x0005F574 File Offset: 0x0005D774
		public CodeCatchClauseCollection(CodeCatchClause[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeCatchClause" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClause" /> object at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x170004CD RID: 1229
		public CodeCatchClause this[int index]
		{
			get
			{
				return (CodeCatchClause)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to add. </param>
		// Token: 0x0600187F RID: 6271 RVA: 0x00051026 File Offset: 0x0004F226
		public int Add(CodeCatchClause value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeCatchClause" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeCatchClause" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001880 RID: 6272 RVA: 0x0005F598 File Offset: 0x0005D798
		public void AddRange(CodeCatchClause[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001881 RID: 6273 RVA: 0x0005F5CC File Offset: 0x0005D7CC
		public void AddRange(CodeCatchClauseCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to locate in the collection. </param>
		// Token: 0x06001882 RID: 6274 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public bool Contains(CodeCatchClause value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06001883 RID: 6275 RVA: 0x000510CE File Offset: 0x0004F2CE
		public void CopyTo(CodeCatchClause[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object in the collection, if it exists in the collection.</summary>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to locate in the collection. </param>
		// Token: 0x06001884 RID: 6276 RVA: 0x000510DD File Offset: 0x0004F2DD
		public int IndexOf(CodeCatchClause value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to insert. </param>
		// Token: 0x06001885 RID: 6277 RVA: 0x000510EB File Offset: 0x0004F2EB
		public void Insert(int index, CodeCatchClause value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeCatchClause" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeCatchClause" /> object to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06001886 RID: 6278 RVA: 0x0005113D File Offset: 0x0004F33D
		public void Remove(CodeCatchClause value)
		{
			base.List.Remove(value);
		}
	}
}
