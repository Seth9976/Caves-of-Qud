using System;
using System.Collections;

namespace System.Web
{
	// Token: 0x02000044 RID: 68
	internal class HttpRawResponse
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000725D File Offset: 0x0000545D
		internal HttpRawResponse(int statusCode, string statusDescription, ArrayList headers, ArrayList buffers, bool hasSubstBlocks)
		{
			this._statusCode = statusCode;
			this._statusDescr = statusDescription;
			this._headers = headers;
			this._buffers = buffers;
			this._hasSubstBlocks = hasSubstBlocks;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000728A File Offset: 0x0000548A
		internal int StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00007292 File Offset: 0x00005492
		internal string StatusDescription
		{
			get
			{
				return this._statusDescr;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000729A File Offset: 0x0000549A
		internal ArrayList Headers
		{
			get
			{
				return this._headers;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000072A2 File Offset: 0x000054A2
		internal ArrayList Buffers
		{
			get
			{
				return this._buffers;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000072AA File Offset: 0x000054AA
		internal bool HasSubstBlocks
		{
			get
			{
				return this._hasSubstBlocks;
			}
		}

		// Token: 0x04000DA2 RID: 3490
		private int _statusCode;

		// Token: 0x04000DA3 RID: 3491
		private string _statusDescr;

		// Token: 0x04000DA4 RID: 3492
		private ArrayList _headers;

		// Token: 0x04000DA5 RID: 3493
		private ArrayList _buffers;

		// Token: 0x04000DA6 RID: 3494
		private bool _hasSubstBlocks;
	}
}
