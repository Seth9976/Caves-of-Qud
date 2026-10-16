using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000686 RID: 1670
	internal class FixedSizeReadStream : WebReadStream
	{
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000B79D3 File Offset: 0x000B5BD3
		public long ContentLength { get; }

		// Token: 0x06003496 RID: 13462 RVA: 0x000B79DB File Offset: 0x000B5BDB
		public FixedSizeReadStream(WebOperation operation, Stream innerStream, long contentLength)
			: base(operation, innerStream)
		{
			this.ContentLength = contentLength;
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000B79EC File Offset: 0x000B5BEC
		protected override async Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long num = this.ContentLength - this.position;
			int num2;
			if (num == 0L)
			{
				num2 = 0;
			}
			else
			{
				int num3 = (int)Math.Min(num, (long)size);
				int num4 = await base.InnerStream.ReadAsync(buffer, offset, num3, cancellationToken).ConfigureAwait(false);
				if (num4 <= 0)
				{
					num2 = num4;
				}
				else
				{
					this.position += (long)num4;
					num2 = num4;
				}
			}
			return num2;
		}

		// Token: 0x04001EC3 RID: 7875
		private long position;
	}
}
