using System;
using System.Text;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000DD RID: 221
	public class ResultState
	{
		// Token: 0x06000706 RID: 1798 RVA: 0x000110BD File Offset: 0x0000F2BD
		public ResultState(TestStatus status)
			: this(status, string.Empty, FailureSite.Test)
		{
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000110CC File Offset: 0x0000F2CC
		public ResultState(TestStatus status, string label)
			: this(status, label, FailureSite.Test)
		{
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000110D7 File Offset: 0x0000F2D7
		public ResultState(TestStatus status, FailureSite site)
			: this(status, string.Empty, site)
		{
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000110E6 File Offset: 0x0000F2E6
		public ResultState(TestStatus status, string label, FailureSite site)
		{
			this.Status = status;
			this.Label = ((label == null) ? string.Empty : label);
			this.Site = site;
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001110D File Offset: 0x0000F30D
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x00011115 File Offset: 0x0000F315
		public TestStatus Status { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001111E File Offset: 0x0000F31E
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00011126 File Offset: 0x0000F326
		public string Label { get; private set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001112F File Offset: 0x0000F32F
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00011137 File Offset: 0x0000F337
		public FailureSite Site { get; private set; }

		// Token: 0x06000710 RID: 1808 RVA: 0x00011140 File Offset: 0x0000F340
		public ResultState WithSite(FailureSite site)
		{
			return new ResultState(this.Status, this.Label, site);
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00011154 File Offset: 0x0000F354
		public override bool Equals(object obj)
		{
			ResultState resultState = obj as ResultState;
			return resultState != null && (this.Status.Equals(resultState.Status) && this.Label.Equals(resultState.Label)) && this.Site.Equals(resultState.Site);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000111C2 File Offset: 0x0000F3C2
		public override int GetHashCode()
		{
			return (int)(((int)this.Status << (int)(((FailureSite)8 + (int)this.Site) & (FailureSite)31)) ^ (TestStatus)this.Label.GetHashCode());
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000111E4 File Offset: 0x0000F3E4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.Status.ToString());
			if (this.Label != null && this.Label.Length > 0)
			{
				stringBuilder.AppendFormat(":{0}", this.Label);
			}
			if (this.Site != FailureSite.Test)
			{
				stringBuilder.AppendFormat("({0})", this.Site.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400016B RID: 363
		public static readonly ResultState Inconclusive = new ResultState(TestStatus.Inconclusive);

		// Token: 0x0400016C RID: 364
		public static readonly ResultState Skipped = new ResultState(TestStatus.Skipped);

		// Token: 0x0400016D RID: 365
		public static readonly ResultState Ignored = new ResultState(TestStatus.Skipped, "Ignored");

		// Token: 0x0400016E RID: 366
		public static readonly ResultState Explicit = new ResultState(TestStatus.Skipped, "Explicit");

		// Token: 0x0400016F RID: 367
		public static readonly ResultState Success = new ResultState(TestStatus.Passed);

		// Token: 0x04000170 RID: 368
		public static readonly ResultState Failure = new ResultState(TestStatus.Failed);

		// Token: 0x04000171 RID: 369
		public static readonly ResultState Error = new ResultState(TestStatus.Failed, "Error");

		// Token: 0x04000172 RID: 370
		public static readonly ResultState Cancelled = new ResultState(TestStatus.Failed, "Cancelled");

		// Token: 0x04000173 RID: 371
		public static readonly ResultState NotRunnable = new ResultState(TestStatus.Failed, "Invalid");

		// Token: 0x04000174 RID: 372
		public static readonly ResultState ChildFailure = ResultState.Failure.WithSite(FailureSite.Child);

		// Token: 0x04000175 RID: 373
		public static readonly ResultState SetUpFailure = ResultState.Failure.WithSite(FailureSite.SetUp);

		// Token: 0x04000176 RID: 374
		public static readonly ResultState SetUpError = ResultState.Error.WithSite(FailureSite.SetUp);

		// Token: 0x04000177 RID: 375
		public static readonly ResultState TearDownError = ResultState.Error.WithSite(FailureSite.TearDown);
	}
}
