using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a collection of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000429 RID: 1065
	public sealed class TreeNodeBindingCollection : StateManagedCollection
	{
		// Token: 0x06003091 RID: 12433 RVA: 0x00062525 File Offset: 0x00060725
		internal TreeNodeBindingCollection()
		{
		}

		/// <summary>Appends the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object to the end of the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <returns>The zero-based index of the location of the added <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to append. </param>
		// Token: 0x06003092 RID: 12434 RVA: 0x000624BA File Offset: 0x000606BA
		public int Add(TreeNodeBinding binding)
		{
			return ((IList)this).Add(binding);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object is in the collection.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> is in the collection; otherwise, false.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to find.</param>
		// Token: 0x06003093 RID: 12435 RVA: 0x00052B1E File Offset: 0x00050D1E
		public bool Contains(TreeNodeBinding binding)
		{
			return ((IList)this).Contains(binding);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="bindingArray">A zero-based array of <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> objects that receives the copied items from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</param>
		/// <param name="index">The position in <paramref name="bindingArray" /> at which to start receiving the copied content.</param>
		// Token: 0x06003094 RID: 12436 RVA: 0x00052B27 File Offset: 0x00050D27
		public void CopyTo(TreeNodeBinding[] bindingArray, int index)
		{
			((ICollection)this).CopyTo(bindingArray, index);
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0007E02F File Offset: 0x0007C22F
		protected override object CreateKnownType(int index)
		{
			return new TreeNodeBinding();
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0007E036 File Offset: 0x0007C236
		protected override Type[] GetKnownTypes()
		{
			return TreeNodeBindingCollection.types;
		}

		/// <summary>Determines the index of the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object in the collection.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="binding" /> within the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />, if found; otherwise, -1.</returns>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to locate.</param>
		// Token: 0x06003097 RID: 12439 RVA: 0x00052B31 File Offset: 0x00050D31
		public int IndexOf(TreeNodeBinding binding)
		{
			return ((IList)this).IndexOf(binding);
		}

		/// <summary>Inserts the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object into the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object at the specified index location.</summary>
		/// <param name="index">The zero-based index location at which to insert the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" />. </param>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to insert. </param>
		// Token: 0x06003098 RID: 12440 RVA: 0x00052B3A File Offset: 0x00050D3A
		public void Insert(int index, TreeNodeBinding binding)
		{
			((IList)this).Insert(index, binding);
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to remove. </param>
		// Token: 0x06003099 RID: 12441 RVA: 0x00052B44 File Offset: 0x00050D44
		public void Remove(TreeNodeBinding binding)
		{
			((IList)this).Remove(binding);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object at the specified index location from the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <param name="index">The zero-based index location of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to remove. </param>
		// Token: 0x0600309A RID: 12442 RVA: 0x00052B4D File Offset: 0x00050D4D
		public void RemoveAt(int index)
		{
			((IList)this).RemoveAt(index);
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> object at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" /> object.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> at the specified index in the <see cref="T:System.Web.UI.WebControls.TreeNodeBindingCollection" />.</returns>
		/// <param name="i">The zero-based index of the <see cref="T:System.Web.UI.WebControls.TreeNodeBinding" /> to retrieve. </param>
		// Token: 0x17000F85 RID: 3973
		public TreeNodeBinding this[int i]
		{
			get
			{
				return (TreeNodeBinding)((IList)this)[i];
			}
			set
			{
				((IList)this)[i] = value;
			}
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x0007E04B File Offset: 0x0007C24B
		protected override void SetDirtyObject(object o)
		{
			((TreeNodeBinding)o).SetDirty();
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0006BDFA File Offset: 0x00069FFA
		protected override void OnClear()
		{
			base.OnClear();
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0006BE02 File Offset: 0x0006A002
		protected override void OnRemoveComplete(int index, object value)
		{
			base.OnRemoveComplete(index, value);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x00052BDE File Offset: 0x00050DDE
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
		}

		// Token: 0x04001C11 RID: 7185
		private static Type[] types = new Type[] { typeof(TreeNodeBinding) };
	}
}
