using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000AF0 RID: 2800
	public static class SequenceReaderExtensions
	{
		// Token: 0x060063BE RID: 25534 RVA: 0x0014E1F4 File Offset: 0x0014C3F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryRead<[IsUnmanaged] T>(this SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			ReadOnlySpan<byte> unreadSpan = reader.UnreadSpan;
			if (unreadSpan.Length < sizeof(T))
			{
				return SequenceReaderExtensions.TryReadMultisegment<T>(ref reader, out value);
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(unreadSpan));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x0014E240 File Offset: 0x0014C440
		private unsafe static bool TryReadMultisegment<[IsUnmanaged] T>(ref SequenceReader<byte> reader, out T value) where T : struct, ValueType
		{
			T t = default(T);
			Span<byte> span = new Span<byte>((void*)(&t), sizeof(T));
			if (!reader.TryCopyTo(span))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(span));
			reader.Advance((long)sizeof(T));
			return true;
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x0014E296 File Offset: 0x0014C496
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead<short>(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0014E2AE File Offset: 0x0014C4AE
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead<short>(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x0014E2C6 File Offset: 0x0014C4C6
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out short value)
		{
			if ((ref reader).TryRead<short>(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x0014E2DD File Offset: 0x0014C4DD
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead<int>(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0014E2F5 File Offset: 0x0014C4F5
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead<int>(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x0014E30D File Offset: 0x0014C50D
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out int value)
		{
			if ((ref reader).TryRead<int>(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x0014E324 File Offset: 0x0014C524
		public static bool TryReadLittleEndian(this SequenceReader<byte> reader, out long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead<long>(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0014E33C File Offset: 0x0014C53C
		public static bool TryReadBigEndian(this SequenceReader<byte> reader, out long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return (ref reader).TryRead<long>(out value);
			}
			return SequenceReaderExtensions.TryReadReverseEndianness(ref reader, out value);
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x0014E354 File Offset: 0x0014C554
		private static bool TryReadReverseEndianness(ref SequenceReader<byte> reader, out long value)
		{
			if ((ref reader).TryRead<long>(out value))
			{
				value = BinaryPrimitives.ReverseEndianness(value);
				return true;
			}
			return false;
		}
	}
}
