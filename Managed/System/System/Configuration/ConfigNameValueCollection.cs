using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x020001A1 RID: 417
	internal class ConfigNameValueCollection : NameValueCollection
	{
		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002EDD4 File Offset: 0x0002CFD4
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002EDDC File Offset: 0x0002CFDC
		public ConfigNameValueCollection(ConfigNameValueCollection col)
			: base(col.Count, col)
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002EDEB File Offset: 0x0002CFEB
		public ConfigNameValueCollection(IHashCodeProvider hashProvider, IComparer comparer)
			: base(hashProvider, comparer)
		{
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002EDF5 File Offset: 0x0002CFF5
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0002EDFE File Offset: 0x0002CFFE
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002EE06 File Offset: 0x0002D006
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x04000754 RID: 1876
		private bool modified;
	}
}
