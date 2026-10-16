using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000016 RID: 22
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Throw
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00002FC4 File Offset: 0x000011C4
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidCast()
		{
			throw new InvalidCastException();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002FCB File Offset: 0x000011CB
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidArgument(string message, string parameterName)
		{
			throw new ArgumentException(message, parameterName);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002FD4 File Offset: 0x000011D4
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidArgument_OffsetForVirtualHeapHandle()
		{
			throw new ArgumentException(SR.CantGetOffsetForVirtualHeapHandle, "handle");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002FE5 File Offset: 0x000011E5
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static Exception InvalidArgument_UnexpectedHandleKind(HandleKind kind)
		{
			throw new ArgumentException(SR.Format(SR.UnexpectedHandleKind, kind));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00002FFC File Offset: 0x000011FC
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static Exception InvalidArgument_Handle(string parameterName)
		{
			throw new ArgumentException(SR.InvalidHandle, parameterName);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003009 File Offset: 0x00001209
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void SignatureNotVarArg()
		{
			throw new InvalidOperationException(SR.SignatureNotVarArg);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003015 File Offset: 0x00001215
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ControlFlowBuilderNotAvailable()
		{
			throw new InvalidOperationException(SR.ControlFlowBuilderNotAvailable);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003021 File Offset: 0x00001221
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidOperationBuilderAlreadyLinked()
		{
			throw new InvalidOperationException(SR.BuilderAlreadyLinked);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000302D File Offset: 0x0000122D
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidOperation(string message)
		{
			throw new InvalidOperationException(message);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003035 File Offset: 0x00001235
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidOperation_LabelNotMarked(int id)
		{
			throw new InvalidOperationException(SR.Format(SR.LabelNotMarked, id));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000304C File Offset: 0x0000124C
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void LabelDoesntBelongToBuilder(string parameterName)
		{
			throw new ArgumentException(SR.LabelDoesntBelongToBuilder, parameterName);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003059 File Offset: 0x00001259
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void HeapHandleRequired()
		{
			throw new ArgumentException(SR.NotMetadataHeapHandle, "handle");
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000306A File Offset: 0x0000126A
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void EntityOrUserStringHandleRequired()
		{
			throw new ArgumentException(SR.NotMetadataTableOrUserStringHandle, "handle");
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000307B File Offset: 0x0000127B
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidToken()
		{
			throw new ArgumentException(SR.InvalidToken, "token");
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000308C File Offset: 0x0000128C
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ArgumentNull(string parameterName)
		{
			throw new ArgumentNullException(parameterName);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003094 File Offset: 0x00001294
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ArgumentEmptyString(string parameterName)
		{
			throw new ArgumentException(SR.ExpectedNonEmptyString, parameterName);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000030A1 File Offset: 0x000012A1
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ArgumentEmptyArray(string parameterName)
		{
			throw new ArgumentException(SR.ExpectedNonEmptyArray, parameterName);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000030AE File Offset: 0x000012AE
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ValueArgumentNull()
		{
			throw new ArgumentNullException("value");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000030BA File Offset: 0x000012BA
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void BuilderArgumentNull()
		{
			throw new ArgumentNullException("builder");
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000030C6 File Offset: 0x000012C6
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ArgumentOutOfRange(string parameterName)
		{
			throw new ArgumentOutOfRangeException(parameterName);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000030CE File Offset: 0x000012CE
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ArgumentOutOfRange(string parameterName, string message)
		{
			throw new ArgumentOutOfRangeException(parameterName, message);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000030D7 File Offset: 0x000012D7
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void BlobTooLarge(string parameterName)
		{
			throw new ArgumentOutOfRangeException(parameterName, SR.BlobTooLarge);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000030E4 File Offset: 0x000012E4
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void IndexOutOfRange()
		{
			throw new ArgumentOutOfRangeException("index");
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000030F0 File Offset: 0x000012F0
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void TableIndexOutOfRange()
		{
			throw new ArgumentOutOfRangeException("tableIndex");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000030FC File Offset: 0x000012FC
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ValueArgumentOutOfRange()
		{
			throw new ArgumentOutOfRangeException("value");
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003108 File Offset: 0x00001308
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void OutOfBounds()
		{
			throw new BadImageFormatException(SR.OutOfBoundsRead);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003114 File Offset: 0x00001314
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void WriteOutOfBounds()
		{
			throw new InvalidOperationException(SR.OutOfBoundsWrite);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003120 File Offset: 0x00001320
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidCodedIndex()
		{
			throw new BadImageFormatException(SR.InvalidCodedIndex);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000312C File Offset: 0x0000132C
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidHandle()
		{
			throw new BadImageFormatException(SR.InvalidHandle);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003138 File Offset: 0x00001338
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidCompressedInteger()
		{
			throw new BadImageFormatException(SR.InvalidCompressedInteger);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003144 File Offset: 0x00001344
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidSerializedString()
		{
			throw new BadImageFormatException(SR.InvalidSerializedString);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003150 File Offset: 0x00001350
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ImageTooSmall()
		{
			throw new BadImageFormatException(SR.ImageTooSmall);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000315C File Offset: 0x0000135C
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ImageTooSmallOrContainsInvalidOffsetOrCount()
		{
			throw new BadImageFormatException(SR.ImageTooSmallOrContainsInvalidOffsetOrCount);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003168 File Offset: 0x00001368
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ReferenceOverflow()
		{
			throw new BadImageFormatException(SR.RowIdOrHeapOffsetTooLarge);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003174 File Offset: 0x00001374
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void TableNotSorted(TableIndex tableIndex)
		{
			throw new BadImageFormatException(SR.Format(SR.MetadataTableNotSorted, tableIndex));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000318B File Offset: 0x0000138B
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidOperation_TableNotSorted(TableIndex tableIndex)
		{
			throw new InvalidOperationException(SR.Format(SR.MetadataTableNotSorted, tableIndex));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000031A2 File Offset: 0x000013A2
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void InvalidOperation_PEImageNotAvailable()
		{
			throw new InvalidOperationException(SR.PEImageNotAvailable);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000031AE File Offset: 0x000013AE
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void TooManySubnamespaces()
		{
			throw new BadImageFormatException(SR.TooManySubnamespaces);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000031BA File Offset: 0x000013BA
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void ValueOverflow()
		{
			throw new BadImageFormatException(SR.ValueTooLarge);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000031C6 File Offset: 0x000013C6
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void SequencePointValueOutOfRange()
		{
			throw new BadImageFormatException(SR.SequencePointValueOutOfRange);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000031D2 File Offset: 0x000013D2
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void HeapSizeLimitExceeded(HeapIndex heap)
		{
			throw new ImageFormatLimitationException(SR.Format(SR.HeapSizeLimitExceeded, heap));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000031E9 File Offset: 0x000013E9
		[DoesNotReturn]
		[MethodImpl(8)]
		internal static void PEReaderDisposed()
		{
			throw new ObjectDisposedException("PEReader");
		}
	}
}
