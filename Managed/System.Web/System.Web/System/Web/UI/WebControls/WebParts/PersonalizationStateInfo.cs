using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>When overridden in a derived class, implements a representation of information about a set of Web Parts data for a page.</summary>
	// Token: 0x020007B0 RID: 1968
	[Serializable]
	public abstract class PersonalizationStateInfo
	{
		// Token: 0x06004F81 RID: 20353 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal PersonalizationStateInfo()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the date and time that the personalization state was last updated.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> indicating when the personalization state was last updated.</returns>
		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x06004F82 RID: 20354 RVA: 0x000C8F14 File Offset: 0x000C7114
		public DateTime LastUpdatedDate
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(DateTime);
			}
		}

		/// <summary>Gets the path to the page associated with the personalization state information.</summary>
		/// <returns>The path of the page associated with the personalization state information.</returns>
		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string Path
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the size of the personalization state information stored in the underlying data store.</summary>
		/// <returns>The size, in bytes, of the personalization state information.</returns>
		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x06004F84 RID: 20356 RVA: 0x000C8F30 File Offset: 0x000C7130
		public int Size
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}
	}
}
