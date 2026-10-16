using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001F4 RID: 500
	internal class MatchSparse : Match
	{
		// Token: 0x06000D3C RID: 3388 RVA: 0x0003623E File Offset: 0x0003443E
		internal MatchSparse(Regex regex, Hashtable caps, int capcount, string text, int begpos, int len, int startpos)
			: base(regex, capcount, text, begpos, len, startpos)
		{
			this._caps = caps;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00036257 File Offset: 0x00034457
		public override GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, this._caps);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x04000817 RID: 2071
		internal new readonly Hashtable _caps;
	}
}
