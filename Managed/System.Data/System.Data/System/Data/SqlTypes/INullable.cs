using System;

namespace System.Data.SqlTypes
{
	/// <summary>All the <see cref="N:System.Data.SqlTypes" /> objects and structures implement the INullable interface. </summary>
	// Token: 0x02000304 RID: 772
	public interface INullable
	{
		/// <summary>Indicates whether a structure is null. This property is read-only.</summary>
		/// <returns>
		///   <see cref="T:System.Data.SqlTypes.SqlBoolean" />true if the value of this object is null. Otherwise, false.</returns>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002242 RID: 8770
		bool IsNull { get; }
	}
}
