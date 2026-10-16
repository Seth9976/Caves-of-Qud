using System;

namespace System.Collections
{
	/// <summary>Enumerates the elements of a nongeneric dictionary.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000A15 RID: 2581
	public interface IDictionaryEnumerator : IEnumerator
	{
		/// <summary>Gets the key of the current dictionary entry.</summary>
		/// <returns>The key of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06005B91 RID: 23441
		object Key { get; }

		/// <summary>Gets the value of the current dictionary entry.</summary>
		/// <returns>The value of the current element of the enumeration.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06005B92 RID: 23442
		object Value { get; }

		/// <summary>Gets both the key and the value of the current dictionary entry.</summary>
		/// <returns>A <see cref="T:System.Collections.DictionaryEntry" /> containing both the key and the value of the current dictionary entry.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Collections.IDictionaryEnumerator" /> is positioned before the first entry of the dictionary or after the last entry. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06005B93 RID: 23443
		DictionaryEntry Entry { get; }
	}
}
