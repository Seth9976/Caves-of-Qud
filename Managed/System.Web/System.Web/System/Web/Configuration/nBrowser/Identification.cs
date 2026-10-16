using System;
using System.Text.RegularExpressions;

namespace System.Web.Configuration.nBrowser
{
	// Token: 0x020005F7 RID: 1527
	internal class Identification
	{
		// Token: 0x06004232 RID: 16946 RVA: 0x000AB730 File Offset: 0x000A9930
		public Identification(bool matchType, string matchGroup, string matchName, string matchPattern)
		{
			this.MatchType = matchType;
			this.MatchGroup = matchGroup;
			this.MatchName = matchName;
			this.MatchPattern = matchPattern;
			this.RegexPattern = new Regex(matchPattern);
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000AB795 File Offset: 0x000A9995
		public Match GetMatch(string Header)
		{
			return this.RegexPattern.Match((Header == null) ? string.Empty : Header);
		}

		// Token: 0x06004234 RID: 16948 RVA: 0x000AB7AD File Offset: 0x000A99AD
		public bool IsMatchSuccessful(Match m)
		{
			return this.MatchType == m.Success;
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x000AB7BD File Offset: 0x000A99BD
		public string Name
		{
			get
			{
				return this.MatchName;
			}
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x000AB7C5 File Offset: 0x000A99C5
		public string Group
		{
			get
			{
				return this.MatchGroup;
			}
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x000AB7CD File Offset: 0x000A99CD
		public string Pattern
		{
			get
			{
				return this.MatchPattern;
			}
		}

		// Token: 0x0400237E RID: 9086
		private bool MatchType = true;

		// Token: 0x0400237F RID: 9087
		private string MatchName = string.Empty;

		// Token: 0x04002380 RID: 9088
		private string MatchGroup = string.Empty;

		// Token: 0x04002381 RID: 9089
		private string MatchPattern = string.Empty;

		// Token: 0x04002382 RID: 9090
		private Regex RegexPattern;
	}
}
