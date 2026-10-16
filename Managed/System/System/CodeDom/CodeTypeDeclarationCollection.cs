using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects.</summary>
	// Token: 0x02000337 RID: 823
	[Serializable]
	public class CodeTypeDeclarationCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class.</summary>
		// Token: 0x06001A09 RID: 6665 RVA: 0x0004E33C File Offset: 0x0004C53C
		public CodeTypeDeclarationCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class that contains the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object with which to initialize the collection. </param>
		// Token: 0x06001A0A RID: 6666 RVA: 0x0006125D File Offset: 0x0005F45D
		public CodeTypeDeclarationCollection(CodeTypeDeclarationCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> class that contains the specified array of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects.</summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeDeclaration" /> objects with which to initialize the collection. </param>
		// Token: 0x06001A0B RID: 6667 RVA: 0x0006126C File Offset: 0x0005F46C
		public CodeTypeDeclarationCollection(CodeTypeDeclaration[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object at the specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclaration" /> at each valid index.</returns>
		/// <param name="index">The index of the collection to access. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000543 RID: 1347
		public CodeTypeDeclaration this[int index]
		{
			get
			{
				return (CodeTypeDeclaration)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to the collection.</summary>
		/// <returns>The index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to add. </param>
		// Token: 0x06001A0E RID: 6670 RVA: 0x00051026 File Offset: 0x0004F226
		public int Add(CodeTypeDeclaration value)
		{
			return base.List.Add(value);
		}

		/// <summary>Copies the elements of the specified array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeDeclaration" /> that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001A0F RID: 6671 RVA: 0x00061290 File Offset: 0x0005F490
		public void AddRange(CodeTypeDeclaration[] value)
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

		/// <summary>Adds the contents of another <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object that contains the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001A10 RID: 6672 RVA: 0x000612C4 File Offset: 0x0005F4C4
		public void AddRange(CodeTypeDeclarationCollection value)
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

		/// <summary>Gets a value that indicates whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object.</summary>
		/// <returns>true if the collection contains the specified object; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to search for in the collection. </param>
		// Token: 0x06001A11 RID: 6673 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public bool Contains(CodeTypeDeclaration value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the elements in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> object to a one-dimensional <see cref="T:System.Array" /> instance, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">The destination array is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> is greater than the available space between the index of the target array specified by the <paramref name="index" /> parameter and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than the target array's minimum index. </exception>
		// Token: 0x06001A12 RID: 6674 RVA: 0x000510CE File Offset: 0x0004F2CE
		public void CopyTo(CodeTypeDeclaration[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object in the <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" />, if it exists in the collection.</summary>
		/// <returns>The index of the specified object, if it is found, in the collection; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> to locate in the collection. </param>
		// Token: 0x06001A13 RID: 6675 RVA: 0x000510DD File Offset: 0x0004F2DD
		public int IndexOf(CodeTypeDeclaration value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index where the specified object should be inserted. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object to insert. </param>
		// Token: 0x06001A14 RID: 6676 RVA: 0x000510EB File Offset: 0x0004F2EB
		public void Insert(int index, CodeTypeDeclaration value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeTypeDeclaration" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeDeclaration" /> to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06001A15 RID: 6677 RVA: 0x0005113D File Offset: 0x0004F33D
		public void Remove(CodeTypeDeclaration value)
		{
			base.List.Remove(value);
		}
	}
}
