using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006A8 RID: 1704
	internal class MonoChunkStream : WebReadStream
	{
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000BF2A5 File Offset: 0x000BD4A5
		protected WebHeaderCollection Headers { get; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x000BF2AD File Offset: 0x000BD4AD
		protected MonoChunkParser Decoder { get; }

		// Token: 0x0600367E RID: 13950 RVA: 0x000BF2B5 File Offset: 0x000BD4B5
		public MonoChunkStream(WebOperation operation, Stream innerStream, WebHeaderCollection headers)
			: base(operation, innerStream)
		{
			this.Headers = headers;
			this.Decoder = new MonoChunkParser(headers);
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000BF2D4 File Offset: 0x000BD4D4
		protected override async Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int num;
			if (this.Decoder.DataAvailable)
			{
				num = this.Decoder.Read(buffer, offset, size);
			}
			else
			{
				int num2 = 0;
				byte[] moreBytes = null;
				while (num2 == 0 && this.Decoder.WantMore)
				{
					int num3 = this.Decoder.ChunkLeft;
					if (num3 <= 0)
					{
						num3 = 1024;
					}
					else if (num3 > 16384)
					{
						num3 = 16384;
					}
					if (moreBytes == null || moreBytes.Length < num3)
					{
						moreBytes = new byte[num3];
					}
					num2 = await base.InnerStream.ReadAsync(moreBytes, 0, num3, cancellationToken).ConfigureAwait(false);
					if (num2 <= 0)
					{
						return num2;
					}
					this.Decoder.Write(moreBytes, 0, num2);
					num2 = this.Decoder.Read(buffer, offset, size);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000BF338 File Offset: 0x000BD538
		internal override async Task FinishReading(CancellationToken cancellationToken)
		{
			await base.FinishReading(cancellationToken).ConfigureAwait(false);
			cancellationToken.ThrowIfCancellationRequested();
			if (this.Decoder.DataAvailable)
			{
				MonoChunkStream.ThrowExpectingChunkTrailer();
			}
			while (this.Decoder.WantMore)
			{
				byte[] buffer = new byte[256];
				int num = await base.InnerStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
				if (num <= 0)
				{
					MonoChunkStream.ThrowExpectingChunkTrailer();
				}
				this.Decoder.Write(buffer, 0, num);
				if (this.Decoder.Read(buffer, 0, 1) != 0)
				{
					MonoChunkStream.ThrowExpectingChunkTrailer();
				}
				buffer = null;
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000BF383 File Offset: 0x000BD583
		private static void ThrowExpectingChunkTrailer()
		{
			throw new WebException("Expecting chunk trailer.", null, WebExceptionStatus.ServerProtocolViolation, null);
		}
	}
}
