using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.CodeDom
{
	/// <summary>Provides a common base class for most Code Document Object Model (CodeDOM) objects.</summary>
	// Token: 0x020002EE RID: 750
	[Serializable]
	public class CodeObject
	{
		/// <summary>Gets the user-definable data for the current object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing user data for the current object.</returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0005E704 File Offset: 0x0005C904
		public IDictionary UserData
		{
			get
			{
				IDictionary dictionary;
				if ((dictionary = this._userData) == null)
				{
					dictionary = (this._userData = new ListDictionary());
				}
				return dictionary;
			}
		}

		// Token: 0x04000D5D RID: 3421
		private IDictionary _userData;
	}
}
