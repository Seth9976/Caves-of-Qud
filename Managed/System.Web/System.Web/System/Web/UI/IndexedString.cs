using System;

namespace System.Web.UI
{
	/// <summary>Provides a utility string class that is used by the <see cref="T:System.Web.UI.ObjectStateFormatter" /> class to optimize object graph serialization. This class cannot be inherited.</summary>
	// Token: 0x02000188 RID: 392
	[Serializable]
	public sealed class IndexedString
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.IndexedString" /> class. </summary>
		/// <param name="s">The string.</param>
		/// <exception cref="T:System.ArgumentNullException">The string parameter passed to the constructor is null or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06000F6E RID: 3950 RVA: 0x00028A93 File Offset: 0x00026C93
		public IndexedString(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw new ArgumentNullException("s");
			}
			this._value = s;
		}

		/// <summary>Gets the string associated with the <see cref="T:System.Web.UI.IndexedString" /> object.</summary>
		/// <returns>An initialized string.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x00028AB5 File Offset: 0x00026CB5
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0400130D RID: 4877
		private string _value;
	}
}
