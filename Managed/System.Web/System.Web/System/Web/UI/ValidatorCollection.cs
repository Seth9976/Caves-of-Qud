using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Exposes an array of <see cref="T:System.Web.UI.IValidator" /> references. This class cannot be inherited.</summary>
	// Token: 0x02000241 RID: 577
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ValidatorCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ValidatorCollection" /> class.</summary>
		// Token: 0x060017B4 RID: 6068 RVA: 0x0003E30D File Offset: 0x0003C50D
		public ValidatorCollection()
		{
			this._validators = new ArrayList();
		}

		/// <summary>Gets the number of references in the collection.</summary>
		/// <returns>The number of validation controls in the page's <see cref="T:System.Web.UI.ValidatorCollection" />.</returns>
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0003E320 File Offset: 0x0003C520
		public int Count
		{
			get
			{
				return this._validators.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.ValidatorCollection" /> collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0003E32D File Offset: 0x0003C52D
		public bool IsReadOnly
		{
			get
			{
				return this._validators.IsReadOnly;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Web.UI.ValidatorCollection" /> collection is synchronized.</summary>
		/// <returns>true if the collection is synchronized; otherwise, false.</returns>
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0003E33A File Offset: 0x0003C53A
		public bool IsSynchronized
		{
			get
			{
				return this._validators.IsSynchronized;
			}
		}

		/// <summary>Gets the validation server control at the specified index location in the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
		/// <returns>The value of the specified validator.</returns>
		/// <param name="index">The index of the validator to return. </param>
		// Token: 0x1700079F RID: 1951
		public IValidator this[int index]
		{
			get
			{
				return (IValidator)this._validators[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Object" /> to synchronize the collection with.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Adds the specified validation server control to the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
		/// <param name="validator">The validation server control to add. </param>
		// Token: 0x060017BA RID: 6074 RVA: 0x0003E35A File Offset: 0x0003C55A
		public void Add(IValidator validator)
		{
			this._validators.Add(validator);
		}

		/// <summary>Determines whether the specified validation server control is contained within the page's <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
		/// <returns>true if the validation server control is in the collection; otherwise, false.</returns>
		/// <param name="validator">The validation server control to check for. </param>
		// Token: 0x060017BB RID: 6075 RVA: 0x0003E369 File Offset: 0x0003C569
		public bool Contains(IValidator validator)
		{
			return this._validators.Contains(validator);
		}

		/// <summary>Copies the validator collection to the specified array, beginning at the specified location.</summary>
		/// <param name="array">The collection to which the validation server control is added. </param>
		/// <param name="index">The index where the validation server control is copied. </param>
		// Token: 0x060017BC RID: 6076 RVA: 0x0003E377 File Offset: 0x0003C577
		public void CopyTo(Array array, int index)
		{
			this._validators.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> instance for the <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> for the collection.</returns>
		// Token: 0x060017BD RID: 6077 RVA: 0x0003E386 File Offset: 0x0003C586
		public IEnumerator GetEnumerator()
		{
			return this._validators.GetEnumerator();
		}

		/// <summary>Removes the specified validation server control from the page's <see cref="T:System.Web.UI.ValidatorCollection" /> collection.</summary>
		/// <param name="validator">The validation server control to remove from the collection. </param>
		// Token: 0x060017BE RID: 6078 RVA: 0x0003E393 File Offset: 0x0003C593
		public void Remove(IValidator validator)
		{
			this._validators.Remove(validator);
		}

		// Token: 0x040015F8 RID: 5624
		private ArrayList _validators;
	}
}
