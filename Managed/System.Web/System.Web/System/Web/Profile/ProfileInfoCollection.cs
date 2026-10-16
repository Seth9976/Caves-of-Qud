using System;
using System.Collections;

namespace System.Web.Profile
{
	/// <summary>A collection of <see cref="T:System.Web.Profile.ProfileInfo" /> objects.</summary>
	// Token: 0x02000500 RID: 1280
	[Serializable]
	public sealed class ProfileInfoCollection : IEnumerable, ICollection
	{
		/// <summary>Creates a new, empty <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</summary>
		// Token: 0x060038F5 RID: 14581 RVA: 0x00097407 File Offset: 0x00095607
		public ProfileInfoCollection()
		{
			this._Hashtable = new Hashtable(10, StringComparer.CurrentCultureIgnoreCase);
			this._ArrayList = new ArrayList();
		}

		/// <summary>Adds the specified <see cref="T:System.Web.Profile.ProfileInfo" /> object to the collection.</summary>
		/// <param name="profileInfo">A <see cref="T:System.Web.Profile.ProfileInfo" /> object to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.Profile.ProfileInfo" /> object with the same <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> value as <paramref name="profileInfo" /> already exists in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="profileInfo" /> is null.-or-The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> property of <paramref name="profileInfo" /> is null.</exception>
		// Token: 0x060038F6 RID: 14582 RVA: 0x0009742C File Offset: 0x0009562C
		public void Add(ProfileInfo profileInfo)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			if (profileInfo == null || profileInfo.UserName == null)
			{
				throw new ArgumentNullException("profileInfo");
			}
			this._Hashtable.Add(profileInfo.UserName, this._CurPos);
			this._ArrayList.Add(profileInfo);
			this._CurPos++;
		}

		/// <summary>Removes the <see cref="T:System.Web.Profile.ProfileInfo" /> object with the specified user name from the collection.</summary>
		/// <param name="name">The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> of the <see cref="T:System.Web.Profile.ProfileInfo" /> object to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060038F7 RID: 14583 RVA: 0x00097494 File Offset: 0x00095694
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this._Hashtable[name];
			if (obj == null)
			{
				return;
			}
			this._Hashtable.Remove(name);
			this._ArrayList[(int)obj] = null;
			this._NumBlanks++;
		}

		/// <summary>Gets the <see cref="T:System.Web.Profile.ProfileInfo" /> object in the collection, referenced by the specified <see cref="P:System.Web.Profile.ProfileInfo.UserName" />.</summary>
		/// <returns>A <see cref="T:System.Web.Profile.ProfileInfo" /> object for the specified user name. If name is not found in the collection, null is returned.</returns>
		/// <param name="name">The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> of the <see cref="T:System.Web.Profile.ProfileInfo" /> object to retrieve from the collection.</param>
		// Token: 0x170011C7 RID: 4551
		public ProfileInfo this[string name]
		{
			get
			{
				object obj = this._Hashtable[name];
				if (obj == null)
				{
					return null;
				}
				return this._ArrayList[(int)obj] as ProfileInfo;
			}
		}

		/// <summary>Gets an enumerator that can iterate through the <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</returns>
		// Token: 0x060038F9 RID: 14585 RVA: 0x00097521 File Offset: 0x00095721
		public IEnumerator GetEnumerator()
		{
			this.DoCompact();
			return this._ArrayList.GetEnumerator();
		}

		/// <summary>Makes the contents of the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> read-only.</summary>
		// Token: 0x060038FA RID: 14586 RVA: 0x00097534 File Offset: 0x00095734
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
		}

		/// <summary>Removes all <see cref="T:System.Web.Profile.ProfileInfo" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x060038FB RID: 14587 RVA: 0x00097546 File Offset: 0x00095746
		public void Clear()
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			this._Hashtable.Clear();
			this._ArrayList.Clear();
			this._CurPos = 0;
			this._NumBlanks = 0;
		}

		/// <summary>Gets the number of <see cref="T:System.Web.Profile.ProfileInfo" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Web.Profile.ProfileInfo" /> objects in the collection.</returns>
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x0009757A File Offset: 0x0009577A
		public int Count
		{
			get
			{
				return this._Hashtable.Count;
			}
		}

		/// <summary>Gets a value indicating whether the profile info collection is thread safe.</summary>
		/// <returns>Always false, because thread-safe profile info collections are not supported.</returns>
		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the synchronization root.</summary>
		/// <returns>Always this (Me in Visual Basic), because synchronization of <see cref="T:System.Web.Profile.ProfileInfoCollection" /> objects is not supported.</returns>
		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x00002058 File Offset: 0x00000258
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> to a one-dimensional array.</summary>
		/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Profile.ProfileInfoCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in array at which copying begins.</param>
		// Token: 0x060038FF RID: 14591 RVA: 0x00097587 File Offset: 0x00095787
		public void CopyTo(Array array, int index)
		{
			this.DoCompact();
			this._ArrayList.CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Profile.ProfileInfo" />.</summary>
		/// <param name="array">A one-dimensional array of type <see cref="T:System.Web.Profile.ProfileInfo" /> that is the destination of the elements copied from the <see cref="T:System.Web.Profile.ProfileInfoCollection" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06003900 RID: 14592 RVA: 0x00097587 File Offset: 0x00095787
		public void CopyTo(ProfileInfo[] array, int index)
		{
			this.DoCompact();
			this._ArrayList.CopyTo(array, index);
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x0009759C File Offset: 0x0009579C
		private void DoCompact()
		{
			if (this._NumBlanks < 1)
			{
				return;
			}
			ArrayList arrayList = new ArrayList(this._CurPos - this._NumBlanks);
			int num = -1;
			for (int i = 0; i < this._CurPos; i++)
			{
				if (this._ArrayList[i] != null)
				{
					arrayList.Add(this._ArrayList[i]);
				}
				else if (num == -1)
				{
					num = i;
				}
			}
			this._NumBlanks = 0;
			this._ArrayList = arrayList;
			this._CurPos = this._ArrayList.Count;
			for (int j = num; j < this._CurPos; j++)
			{
				ProfileInfo profileInfo = this._ArrayList[j] as ProfileInfo;
				this._Hashtable[profileInfo.UserName] = j;
			}
		}

		// Token: 0x04001F12 RID: 7954
		private Hashtable _Hashtable;

		// Token: 0x04001F13 RID: 7955
		private ArrayList _ArrayList;

		// Token: 0x04001F14 RID: 7956
		private bool _ReadOnly;

		// Token: 0x04001F15 RID: 7957
		private int _CurPos;

		// Token: 0x04001F16 RID: 7958
		private int _NumBlanks;
	}
}
