using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002E1 RID: 737
	[NullableContext(1)]
	[Nullable(0)]
	internal static class EncodedStringText
	{
		// Token: 0x06001F83 RID: 8067 RVA: 0x00060EA0 File Offset: 0x0005F0A0
		internal static Encoding CreateFallbackEncoding()
		{
			Encoding encoding;
			try
			{
				if (CodePagesEncodingProvider.Instance != null)
				{
					Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
				}
				encoding = Encoding.GetEncoding(0) ?? Encoding.GetEncoding(1252);
			}
			catch (NotSupportedException)
			{
				encoding = Encoding.GetEncoding("Latin1");
			}
			return encoding;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00060EF4 File Offset: 0x0005F0F4
		internal static SourceText Create(Stream stream, [Nullable(2)] Encoding defaultEncoding = null, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1, bool canBeEmbedded = false)
		{
			return EncodedStringText.Create(stream, EncodedStringText.s_fallbackEncoding, defaultEncoding, checksumAlgorithm, canBeEmbedded);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00060F04 File Offset: 0x0005F104
		internal static SourceText Create(Stream stream, Lazy<Encoding> getEncoding, [Nullable(2)] Encoding defaultEncoding = null, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1, bool canBeEmbedded = false)
		{
			bool flag = defaultEncoding == null;
			if (flag)
			{
				try
				{
					return EncodedStringText.Decode(stream, EncodedStringText.s_utf8Encoding, checksumAlgorithm, false, canBeEmbedded);
				}
				catch (DecoderFallbackException)
				{
				}
			}
			SourceText sourceText;
			try
			{
				sourceText = EncodedStringText.Decode(stream, defaultEncoding ?? getEncoding.Value, checksumAlgorithm, flag, canBeEmbedded);
			}
			catch (DecoderFallbackException ex)
			{
				throw new InvalidDataException(ex.Message);
			}
			return sourceText;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x00060F70 File Offset: 0x0005F170
		private static SourceText Decode(Stream data, Encoding encoding, SourceHashAlgorithm checksumAlgorithm, bool throwIfBinaryDetected = false, bool canBeEmbedded = false)
		{
			if (data.CanSeek)
			{
				data.Seek(0L, 0);
				ArraySegment<byte> arraySegment;
				if (encoding.GetMaxCharCountOrThrowIfHuge(data) < 40960 && EncodedStringText.TryGetBytesFromStream(data, out arraySegment) && arraySegment.Offset == 0 && arraySegment.Array != null)
				{
					return SourceText.From(arraySegment.Array, (int)data.Length, encoding, checksumAlgorithm, throwIfBinaryDetected, canBeEmbedded);
				}
			}
			return SourceText.From(data, encoding, checksumAlgorithm, throwIfBinaryDetected, canBeEmbedded);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00060FE0 File Offset: 0x0005F1E0
		[NullableContext(0)]
		internal static bool TryGetBytesFromStream([Nullable(1)] Stream data, out ArraySegment<byte> bytes)
		{
			MemoryStream memoryStream = data as MemoryStream;
			if (memoryStream != null)
			{
				return memoryStream.TryGetBuffer(ref bytes);
			}
			FileStream fileStream = data as FileStream;
			if (fileStream != null)
			{
				return EncodedStringText.TryGetBytesFromFileStream(fileStream, out bytes);
			}
			bytes = new ArraySegment<byte>(Array.Empty<byte>());
			return false;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00061024 File Offset: 0x0005F224
		[NullableContext(0)]
		private static bool TryGetBytesFromFileStream([Nullable(1)] FileStream stream, out ArraySegment<byte> bytes)
		{
			int num = (int)stream.Length;
			if (num == 0)
			{
				bytes = new ArraySegment<byte>(Array.Empty<byte>());
				return true;
			}
			byte[] array = new byte[num];
			bool flag = stream.TryReadAll(array, 0, num) == num;
			bytes = (flag ? new ArraySegment<byte>(array) : new ArraySegment<byte>(Array.Empty<byte>()));
			return flag;
		}

		// Token: 0x04000DE0 RID: 3552
		private const int LargeObjectHeapLimitInChars = 40960;

		// Token: 0x04000DE1 RID: 3553
		private static readonly Encoding s_utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04000DE2 RID: 3554
		private static readonly Lazy<Encoding> s_fallbackEncoding = new Lazy<Encoding>(new Func<Encoding>(EncodedStringText.CreateFallbackEncoding));

		// Token: 0x02000678 RID: 1656
		[Nullable(0)]
		internal static class TestAccessor
		{
			// Token: 0x06003928 RID: 14632 RVA: 0x0009EE52 File Offset: 0x0009D052
			internal static SourceText Create(Stream stream, Lazy<Encoding> getEncoding, Encoding defaultEncoding, SourceHashAlgorithm checksumAlgorithm, bool canBeEmbedded)
			{
				return EncodedStringText.Create(stream, getEncoding, defaultEncoding, checksumAlgorithm, canBeEmbedded);
			}

			// Token: 0x06003929 RID: 14633 RVA: 0x0009EE5F File Offset: 0x0009D05F
			internal static SourceText Decode(Stream data, Encoding encoding, SourceHashAlgorithm checksumAlgorithm, bool throwIfBinaryDetected, bool canBeEmbedded)
			{
				return EncodedStringText.Decode(data, encoding, checksumAlgorithm, throwIfBinaryDetected, canBeEmbedded);
			}
		}
	}
}
