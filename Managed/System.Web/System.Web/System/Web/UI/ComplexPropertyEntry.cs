using System;
using Unity;

namespace System.Web.UI
{
	/// <summary>Serves as the property entry for read/write and read-only properties such as templates.</summary>
	// Token: 0x020001AF RID: 431
	public class ComplexPropertyEntry : BuilderPropertyEntry
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x0002B87F File Offset: 0x00029A7F
		internal ComplexPropertyEntry(bool isCollectionItem, bool readOnly)
		{
			this.IsCollectionItem = isCollectionItem;
			this.ReadOnly = readOnly;
		}

		/// <summary>Gets a value indicating whether the property is a collection object.</summary>
		/// <returns>true if the property entry represents an item that contains a collection of values; otherwise, false.</returns>
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x0002B895 File Offset: 0x00029A95
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x0002B89D File Offset: 0x00029A9D
		public bool IsCollectionItem { get; private set; }

		/// <summary>Gets or sets a value indicating whether the item represented in the property entry contains a method for setting its value.</summary>
		/// <returns>true if the item represented by the property entry does not contain a set method; otherwise, false.</returns>
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x0002B8A6 File Offset: 0x00029AA6
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x0002B8AE File Offset: 0x00029AAE
		public bool ReadOnly { get; set; }

		// Token: 0x0600108A RID: 4234 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal ComplexPropertyEntry()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
