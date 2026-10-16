using System;
using System.Collections;

namespace System.CodeDom
{
	/// <summary>Represents a collection of <see cref="T:System.CodeDom.CodeTypeParameter" /> objects.</summary>
	// Token: 0x0200033D RID: 829
	[Serializable]
	public class CodeTypeParameterCollection : CollectionBase
	{
		/// <summary>Initializes a new, empty instance of the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> class. </summary>
		// Token: 0x06001A42 RID: 6722 RVA: 0x0004E33C File Offset: 0x0004C53C
		public CodeTypeParameterCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> class containing the elements of the specified source collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> with which to initialize the collection.</param>
		// Token: 0x06001A43 RID: 6723 RVA: 0x0006161A File Offset: 0x0005F81A
		public CodeTypeParameterCollection(CodeTypeParameterCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> class containing the specified array of <see cref="T:System.CodeDom.CodeTypeParameter" /> objects. </summary>
		/// <param name="value">An array of <see cref="T:System.CodeDom.CodeTypeParameter" /> objects with which to initialize the collection.</param>
		// Token: 0x06001A44 RID: 6724 RVA: 0x00061629 File Offset: 0x0005F829
		public CodeTypeParameterCollection(CodeTypeParameter[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.CodeDom.CodeTypeParameter" /> object at the specified index in the collection.</summary>
		/// <returns>The <see cref="T:System.CodeDom.CodeTypeParameter" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the collection object to access.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000553 RID: 1363
		public CodeTypeParameter this[int index]
		{
			get
			{
				return (CodeTypeParameter)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object to the collection.</summary>
		/// <returns>The zero-based index at which the new element was inserted.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> to add.</param>
		// Token: 0x06001A47 RID: 6727 RVA: 0x00051026 File Offset: 0x0004F226
		public int Add(CodeTypeParameter value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object to the collection using the specified data type name.</summary>
		/// <param name="value">The name of a data type for which to add the <see cref="T:System.CodeDom.CodeTypeParameter" /> object to the collection.</param>
		// Token: 0x06001A48 RID: 6728 RVA: 0x0006164B File Offset: 0x0005F84B
		public void Add(string value)
		{
			this.Add(new CodeTypeParameter(value));
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> array to the end of the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.CodeDom.CodeTypeParameter" /> containing the objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001A49 RID: 6729 RVA: 0x0006165C File Offset: 0x0005F85C
		public void AddRange(CodeTypeParameter[] value)
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

		/// <summary>Copies the elements of the specified <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> to the end of the collection.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> containing the <see cref="T:System.CodeDom.CodeTypeParameter" /> objects to add to the collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06001A4A RID: 6730 RVA: 0x00061690 File Offset: 0x0005F890
		public void AddRange(CodeTypeParameterCollection value)
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

		/// <summary>Determines whether the collection contains the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object.</summary>
		/// <returns>true if the <see cref="T:System.CodeDom.CodeTypeParameter" /> object is contained in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to search for in the collection.</param>
		// Token: 0x06001A4B RID: 6731 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public bool Contains(CodeTypeParameter value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Copies the items in the collection to the specified one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection. </param>
		/// <param name="index">The index of the array at which to begin inserting. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.-or- The number of elements in the <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> is greater than the available space between the index of the target array specified by <paramref name="index" /> and the end of the target array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the target array's lowest index. </exception>
		// Token: 0x06001A4C RID: 6732 RVA: 0x000510CE File Offset: 0x0004F2CE
		public void CopyTo(CodeTypeParameter[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object, if it exists in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object in the collection if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to locate in the collection.</param>
		// Token: 0x06001A4D RID: 6733 RVA: 0x000510DD File Offset: 0x0004F2DD
		public int IndexOf(CodeTypeParameter value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object into the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the item. </param>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to insert. </param>
		// Token: 0x06001A4E RID: 6734 RVA: 0x000510EB File Offset: 0x0004F2EB
		public void Insert(int index, CodeTypeParameter value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.CodeDom.CodeTypeParameter" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.CodeDom.CodeTypeParameter" /> object to remove from the collection.</param>
		/// <exception cref="T:System.ArgumentException">The specified object is not found in the collection. </exception>
		// Token: 0x06001A4F RID: 6735 RVA: 0x0005113D File Offset: 0x0004F33D
		public void Remove(CodeTypeParameter value)
		{
			base.List.Remove(value);
		}
	}
}
