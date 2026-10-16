using System;
using System.Text;

namespace System.Net
{
	// Token: 0x02000587 RID: 1415
	internal class ResponseDescription
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002DC7 RID: 11719 RVA: 0x0009CCF2 File Offset: 0x0009AEF2
		internal bool PositiveIntermediate
		{
			get
			{
				return this.Status >= 100 && this.Status <= 199;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x0009CD10 File Offset: 0x0009AF10
		internal bool PositiveCompletion
		{
			get
			{
				return this.Status >= 200 && this.Status <= 299;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002DC9 RID: 11721 RVA: 0x0009CD31 File Offset: 0x0009AF31
		internal bool TransientFailure
		{
			get
			{
				return this.Status >= 400 && this.Status <= 499;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x0009CD52 File Offset: 0x0009AF52
		internal bool PermanentFailure
		{
			get
			{
				return this.Status >= 500 && this.Status <= 599;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002DCB RID: 11723 RVA: 0x0009CD73 File Offset: 0x0009AF73
		internal bool InvalidStatusCode
		{
			get
			{
				return this.Status < 100 || this.Status > 599;
			}
		}

		// Token: 0x04001939 RID: 6457
		internal const int NoStatus = -1;

		// Token: 0x0400193A RID: 6458
		internal bool Multiline;

		// Token: 0x0400193B RID: 6459
		internal int Status = -1;

		// Token: 0x0400193C RID: 6460
		internal string StatusDescription;

		// Token: 0x0400193D RID: 6461
		internal StringBuilder StatusBuffer = new StringBuilder();

		// Token: 0x0400193E RID: 6462
		internal string StatusCodeString;
	}
}
