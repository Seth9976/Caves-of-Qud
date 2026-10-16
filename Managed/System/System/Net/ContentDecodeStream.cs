using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000673 RID: 1651
	internal class ContentDecodeStream : WebReadStream
	{
		// Token: 0x060033F5 RID: 13301 RVA: 0x000B5144 File Offset: 0x000B3344
		public static ContentDecodeStream Create(WebOperation operation, Stream innerStream, ContentDecodeStream.Mode mode)
		{
			Stream stream;
			if (mode == ContentDecodeStream.Mode.GZip)
			{
				stream = new GZipStream(innerStream, CompressionMode.Decompress);
			}
			else
			{
				stream = new DeflateStream(innerStream, CompressionMode.Decompress);
			}
			return new ContentDecodeStream(operation, stream, innerStream);
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x060033F6 RID: 13302 RVA: 0x000B516E File Offset: 0x000B336E
		private Stream OriginalInnerStream { get; }

		// Token: 0x060033F7 RID: 13303 RVA: 0x000B5176 File Offset: 0x000B3376
		private ContentDecodeStream(WebOperation operation, Stream decodeStream, Stream originalInnerStream)
			: base(operation, decodeStream)
		{
			this.OriginalInnerStream = originalInnerStream;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x000B5187 File Offset: 0x000B3387
		protected override Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return base.InnerStream.ReadAsync(buffer, offset, size, cancellationToken);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x000B519C File Offset: 0x000B339C
		internal override Task FinishReading(CancellationToken cancellationToken)
		{
			WebReadStream webReadStream = this.OriginalInnerStream as WebReadStream;
			if (webReadStream != null)
			{
				return webReadStream.FinishReading(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x02000674 RID: 1652
		internal enum Mode
		{
			// Token: 0x04001E93 RID: 7827
			GZip,
			// Token: 0x04001E94 RID: 7828
			Deflate
		}
	}
}
