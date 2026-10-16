using System;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007D5 RID: 2005
	internal class EncodedStreamFactory
	{
		// Token: 0x06004045 RID: 16453 RVA: 0x000DB8B2 File Offset: 0x000D9AB2
		internal IEncodableStream GetEncoder(TransferEncoding encoding, Stream stream)
		{
			if (encoding == TransferEncoding.Base64)
			{
				return new Base64Stream(stream, new Base64WriteStateInfo());
			}
			if (encoding == TransferEncoding.QuotedPrintable)
			{
				return new QuotedPrintableStream(stream, true);
			}
			if (encoding == TransferEncoding.SevenBit || encoding == TransferEncoding.EightBit)
			{
				return new EightBitStream(stream);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06004046 RID: 16454 RVA: 0x000DB8E4 File Offset: 0x000D9AE4
		internal IEncodableStream GetEncoderForHeader(Encoding encoding, bool useBase64Encoding, int headerTextLength)
		{
			byte[] array = this.CreateHeader(encoding, useBase64Encoding);
			byte[] array2 = this.CreateFooter();
			if (useBase64Encoding)
			{
				return new Base64Stream((Base64WriteStateInfo)new Base64WriteStateInfo(1024, array, array2, 70, headerTextLength));
			}
			return new QEncodedStream(new WriteStateInfoBase(1024, array, array2, 70, headerTextLength));
		}

		// Token: 0x06004047 RID: 16455 RVA: 0x000DB932 File Offset: 0x000D9B32
		protected byte[] CreateHeader(Encoding encoding, bool useBase64Encoding)
		{
			return Encoding.ASCII.GetBytes("=?" + encoding.HeaderName + "?" + (useBase64Encoding ? "B?" : "Q?"));
		}

		// Token: 0x06004048 RID: 16456 RVA: 0x000DB962 File Offset: 0x000D9B62
		protected byte[] CreateFooter()
		{
			return new byte[] { 63, 61 };
		}

		// Token: 0x04002696 RID: 9878
		internal const int DefaultMaxLineLength = 70;

		// Token: 0x04002697 RID: 9879
		private const int InitialBufferSize = 1024;
	}
}
