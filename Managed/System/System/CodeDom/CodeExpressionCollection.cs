using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeExpression" /> objects.</summary>
	// Token: 0x02000311 RID: 785
	[Serializable]
	public class CodeExpressionCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionCollection" /> class.</summary>
		// Token: 0x060018E7 RID: 6375 RVA: 0x0004E33C File Offset: 0x0004C53C
		public CodeExpressionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpressionCollection" /> with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060018E8 RID: 6376 RVA: 0x0005FB99 File Offset: 0x0005DD99
		public CodeExpressionCollection(CodeExpressionCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeExpression" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects with which to initialize the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">One or more objects in the array are null.</exception>
		// Token: 0x060018E9 RID: 6377 RVA: 0x0005FBA8 File Offset: 0x0005DDA8
		public CodeExpressionCollection(CodeExpression[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeExpression" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> object at each valid index.</returns>
		/// <param name="index">The zero-based index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x170004EA RID: 1258
		public CodeExpression this[int index]
		{
			get
			{
				return (CodeExpression)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeExpression" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to add. </param>
		// Token: 0x060018EC RID: 6380 RVA: 0x00051026 File Offset: 0x0004F226
		public int Add(CodeExpression value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeExpression" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060018ED RID: 6381 RVA: 0x0005FBCC File Offset: 0x0005DDCC
		public void AddRange(CodeExpression[] value)
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

		/// <summary>Copies the contents of another <see cref="T:System.CodeDom.CodeExpressionCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060018EE RID: 6382 RVA: 0x0005FC00 File Offset: 0x0005DE00
		public void AddRange(CodeExpressionCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeExpression" /> object.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to locate in the collection. </param>
		// Token: 0x060018EF RID: 6383 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public bool Contains(CodeExpression value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance beginning at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeExpressionCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x060018F0 RID: 6384 RVA: 0x000510CE File Offset: 0x0004F2CE
		public void CopyTo(CodeExpression[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeExpression" /> object in the collection, if it exists in the collection.</summary>
		/// <returns>The index of the specified object, if found, in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to locate in the collection. </param>
		// Token: 0x060018F1 RID: 6385 RVA: 0x000510DD File Offset: 0x0004F2DD
		public int IndexOf(CodeExpression value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeExpression" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to insert. </param>
		// Token: 0x060018F2 RID: 6386 RVA: 0x000510EB File Offset: 0x0004F2EB
		public void Insert(int index, CodeExpression value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeExpression" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeExpression" /> object to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x060018F3 RID: 6387 RVA: 0x0005113D File Offset: 0x0004F33D
		public void Remove(CodeExpression value)
		{
			base.List.Remove(value);
		}
	}
}
