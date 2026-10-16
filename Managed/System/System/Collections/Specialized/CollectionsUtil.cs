using System;

namespace System.Collections.Specialized
{
	/// <summary>Creates collections that ignore the case in strings.</summary>
	// Token: 0x020004A5 RID: 1189
	public class CollectionsUtil
	{
		/// <summary>Creates a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the default initial capacity.</summary>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the default initial capacity.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002614 RID: 9748 RVA: 0x000857C8 File Offset: 0x000839C8
		public static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>Creates a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the specified initial capacity.</summary>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the specified initial capacity.</returns>
		/// <param name="capacity">The approximate number of entries that the <see cref="T:System.Collections.Hashtable" /> can initially contain. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero. </exception>
		// Token: 0x06002615 RID: 9749 RVA: 0x000857D4 File Offset: 0x000839D4
		public static Hashtable CreateCaseInsensitiveHashtable(int capacity)
		{
			return new Hashtable(capacity, StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>Copies the entries from the specified dictionary to a new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class with the same initial capacity as the number of entries copied.</summary>
		/// <returns>A new case-insensitive instance of the <see cref="T:System.Collections.Hashtable" /> class containing the entries from the specified <see cref="T:System.Collections.IDictionary" />.</returns>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> to copy to a new case-insensitive <see cref="T:System.Collections.Hashtable" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x06002616 RID: 9750 RVA: 0x000857E1 File Offset: 0x000839E1
		public static Hashtable CreateCaseInsensitiveHashtable(IDictionary d)
		{
			return new Hashtable(d, StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Collections.SortedList" /> class that ignores the case of strings.</summary>
		/// <returns>A new instance of the <see cref="T:System.Collections.SortedList" /> class that ignores the case of strings.</returns>
		// Token: 0x06002617 RID: 9751 RVA: 0x000857EE File Offset: 0x000839EE
		public static SortedList CreateCaseInsensitiveSortedList()
		{
			return new SortedList(CaseInsensitiveComparer.Default);
		}
	}
}
