using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
	// Token: 0x0200044F RID: 1103
	public class DesignerVerbCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class.</summary>
		// Token: 0x060023CF RID: 9167 RVA: 0x0004E33C File Offset: 0x0004C53C
		public DesignerVerbCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> class using the specified array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array that indicates the verbs to contain within the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060023D0 RID: 9168 RVA: 0x000813A1 File Offset: 0x0007F5A1
		public DesignerVerbCollection(DesignerVerb[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at each valid index in the collection.</returns>
		/// <param name="index">The index at which to get or set the <see cref="T:System.ComponentModel.Design.DesignerVerb" />. </param>
		// Token: 0x17000741 RID: 1857
		public DesignerVerb this[int index]
		{
			get
			{
				return (DesignerVerb)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to the collection.</summary>
		/// <returns>The index in the collection at which the verb was added.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to add to the collection. </param>
		// Token: 0x060023D3 RID: 9171 RVA: 0x00051026 File Offset: 0x0004F226
		public int Add(DesignerVerb value)
		{
			return base.List.Add(value);
		}

		/// <summary>Adds the specified set of designer verbs to the collection.</summary>
		/// <param name="value">An array of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060023D4 RID: 9172 RVA: 0x000813C4 File Offset: 0x0007F5C4
		public void AddRange(DesignerVerb[] value)
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

		/// <summary>Adds the specified collection of designer verbs to the collection.</summary>
		/// <param name="value">A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060023D5 RID: 9173 RVA: 0x000813F8 File Offset: 0x0007F5F8
		public void AddRange(DesignerVerbCollection value)
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

		/// <summary>Inserts the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> at the specified index.</summary>
		/// <param name="index">The index in the collection at which to insert the verb. </param>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to insert in the collection. </param>
		// Token: 0x060023D6 RID: 9174 RVA: 0x000510EB File Offset: 0x0004F2EB
		public void Insert(int index, DesignerVerb value)
		{
			base.List.Insert(index, value);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" />.</summary>
		/// <returns>The index of the specified object if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> whose index to get in the collection. </param>
		// Token: 0x060023D7 RID: 9175 RVA: 0x000510DD File Offset: 0x0004F2DD
		public int IndexOf(DesignerVerb value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Gets a value indicating whether the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> exists in the collection.</summary>
		/// <returns>true if the specified object exists in the collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to search for in the collection. </param>
		// Token: 0x060023D8 RID: 9176 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public bool Contains(DesignerVerb value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.Design.DesignerVerb" /> to remove from the collection. </param>
		// Token: 0x060023D9 RID: 9177 RVA: 0x0005113D File Offset: 0x0004F33D
		public void Remove(DesignerVerb value)
		{
			base.List.Remove(value);
		}

		/// <summary>Copies the collection members to the specified <see cref="T:System.ComponentModel.Design.DesignerVerb" /> array beginning at the specified destination index.</summary>
		/// <param name="array">The array to copy collection members to. </param>
		/// <param name="index">The destination index to begin copying to. </param>
		// Token: 0x060023DA RID: 9178 RVA: 0x000510CE File Offset: 0x0004F2CE
		public void CopyTo(DesignerVerb[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Raises the Set event.</summary>
		/// <param name="index">The index at which to set the item. </param>
		/// <param name="oldValue">The old object. </param>
		/// <param name="newValue">The new object. </param>
		// Token: 0x060023DB RID: 9179 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnSet(int index, object oldValue, object newValue)
		{
		}

		/// <summary>Raises the Insert event.</summary>
		/// <param name="index">The index at which to insert an item. </param>
		/// <param name="value">The object to insert. </param>
		// Token: 0x060023DC RID: 9180 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnInsert(int index, object value)
		{
		}

		/// <summary>Raises the Clear event.</summary>
		// Token: 0x060023DD RID: 9181 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnClear()
		{
		}

		/// <summary>Raises the Remove event.</summary>
		/// <param name="index">The index at which to remove the item. </param>
		/// <param name="value">The object to remove. </param>
		// Token: 0x060023DE RID: 9182 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnRemove(int index, object value)
		{
		}

		/// <summary>Raises the Validate event.</summary>
		/// <param name="value">The object to validate. </param>
		// Token: 0x060023DF RID: 9183 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void OnValidate(object value)
		{
		}
	}
}
