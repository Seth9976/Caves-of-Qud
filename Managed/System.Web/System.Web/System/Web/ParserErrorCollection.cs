using System;
using System.Collections;

namespace System.Web
{
	/// <summary>Manages a set of parser errors detected during parsing. This class cannot be inherited.</summary>
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public sealed class ParserErrorCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserErrorCollection" /> class.</summary>
		// Token: 0x06000ADF RID: 2783 RVA: 0x0001C3DA File Offset: 0x0001A5DA
		public ParserErrorCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ParserErrorCollection" /> class.</summary>
		/// <param name="value">An array of type <see cref="T:System.Web.ParserError" /> that specifies the errors to add to the collection.</param>
		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001C3E2 File Offset: 0x0001A5E2
		public ParserErrorCollection(ParserError[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.ParserError" /> object at the specified index within the collection.</summary>
		/// <returns>The <see cref="T:System.Web.ParserError" /> at the specified index within the collection.</returns>
		/// <param name="index">The index within the collection of the <see cref="T:System.Web.ParserError" /> object to get or set.</param>
		// Token: 0x170003E2 RID: 994
		public ParserError this[int index]
		{
			get
			{
				return (ParserError)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds a value to the collection.</summary>
		/// <returns>The index of the value within the collection; otherwise, -1 if the value is already in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Web.ParserError" /> value to add to the collection.</param>
		// Token: 0x06000AE3 RID: 2787 RVA: 0x0001C418 File Offset: 0x0001A618
		public int Add(ParserError value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the objects in an existing <see cref="T:System.Web.ParserErrorCollection" /> to the collection. </summary>
		/// <param name="value">A <see cref="T:System.Web.ParserErrorCollection" /> containing the <see cref="T:System.Web.ParserError" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.ParserError" /> value is null.</exception>
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0001C426 File Offset: 0x0001A626
		public void AddRange(ParserErrorCollection value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Adds an array of <see cref="T:System.Web.ParserError" /> objects to the collection.</summary>
		/// <param name="value">An array of type <see cref="T:System.Web.ParserError" /> that specifies the values to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0001C426 File Offset: 0x0001A626
		public void AddRange(ParserError[] value)
		{
			base.InnerList.AddRange(value);
		}

		/// <summary>Determines whether the <see cref="T:System.Web.ParserError" /> object is located in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Web.ParserError" /> is in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Web.ParserError" /> to locate in the collection.</param>
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0001C434 File Offset: 0x0001A634
		public bool Contains(ParserError value)
		{
			return base.InnerList.Contains(value);
		}

		/// <summary>Copies the <see cref="T:System.Web.ParserError" /> objects in the collection to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.ParserError" /> to which the parser errors in the collection are copied.</param>
		/// <param name="index">The first index within the array to which the <see cref="T:System.Web.ParserError" /> is copied.</param>
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0001C442 File Offset: 0x0001A642
		public void CopyTo(ParserError[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Web.ParserError" /> object in the collection.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Web.ParserError" /> objects within the collection; otherwise, 1 if the <see cref="T:System.Web.ParserError" /> is not in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Web.ParserError" /> to locate in the collection.</param>
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0001C451 File Offset: 0x0001A651
		public int IndexOf(ParserError value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.ParserError" /> object into the collection at the specified index.</summary>
		/// <param name="index">The index within the collection at which to insert the <see cref="T:System.Web.ParserError" />.</param>
		/// <param name="value">The <see cref="T:System.Web.ParserError" /> object to insert into the collection.</param>
		// Token: 0x06000AE9 RID: 2793 RVA: 0x0001C45F File Offset: 0x0001A65F
		public void Insert(int index, ParserError value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.ParserError" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Web.ParserError" /> to remove from the collection.</param>
		// Token: 0x06000AEA RID: 2794 RVA: 0x0001C46E File Offset: 0x0001A66E
		public void Remove(ParserError value)
		{
			base.InnerList.Remove(value);
		}
	}
}
