using System;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Threading;

namespace System.Security.Cryptography
{
	// Token: 0x020002B3 RID: 691
	internal class DerSequenceReader
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x000569E9 File Offset: 0x00054BE9
		// (set) Token: 0x0600158F RID: 5519 RVA: 0x000569F1 File Offset: 0x00054BF1
		internal int ContentLength { get; private set; }

		// Token: 0x06001590 RID: 5520 RVA: 0x000569FA File Offset: 0x00054BFA
		private DerSequenceReader(bool startAtPayload, byte[] data, int offset, int length)
		{
			this._data = data;
			this._position = offset;
			this._end = offset + length;
			this.ContentLength = length;
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00056A22 File Offset: 0x00054C22
		internal DerSequenceReader(byte[] data)
			: this(data, 0, data.Length)
		{
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00056A2F File Offset: 0x00054C2F
		internal DerSequenceReader(byte[] data, int offset, int length)
			: this(DerSequenceReader.DerTag.Sequence, data, offset, length)
		{
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00056A3C File Offset: 0x00054C3C
		private DerSequenceReader(DerSequenceReader.DerTag tagToEat, byte[] data, int offset, int length)
		{
			if (offset < 0 || length < 2 || length > data.Length - offset)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			this._data = data;
			this._end = offset + length;
			this._position = offset;
			this.EatTag(tagToEat);
			int num = this.EatLength();
			this.ContentLength = num;
			this._end = this._position + num;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00056AA7 File Offset: 0x00054CA7
		internal static DerSequenceReader CreateForPayload(byte[] payload)
		{
			return new DerSequenceReader(true, payload, 0, payload.Length);
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00056AB4 File Offset: 0x00054CB4
		internal bool HasData
		{
			get
			{
				return this._position < this._end;
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00056AC4 File Offset: 0x00054CC4
		internal byte PeekTag()
		{
			if (!this.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = this._data[this._position];
			if ((b & 31) == 31)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return b;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00056AF9 File Offset: 0x00054CF9
		internal bool HasTag(DerSequenceReader.DerTag expectedTag)
		{
			return this.HasTag((byte)expectedTag);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00056B02 File Offset: 0x00054D02
		internal bool HasTag(byte expectedTag)
		{
			return this.HasData && this._data[this._position] == expectedTag;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00056B20 File Offset: 0x00054D20
		internal void SkipValue()
		{
			this.EatTag((DerSequenceReader.DerTag)this.PeekTag());
			int num = this.EatLength();
			this._position += num;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00056B50 File Offset: 0x00054D50
		internal void ValidateAndSkipDerValue()
		{
			byte b = this.PeekTag();
			if ((b & 192) == 0)
			{
				if (b == 0 || b == 15)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				bool flag = false;
				int num = (int)(b & 31);
				if (num <= 11)
				{
					if (num != 8 && num != 11)
					{
						goto IL_004E;
					}
				}
				else if (num - 16 > 1 && num != 29)
				{
					goto IL_004E;
				}
				flag = true;
				IL_004E:
				bool flag2 = (b & 32) == 32;
				if (flag != flag2)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
			}
			this.EatTag((DerSequenceReader.DerTag)b);
			int num2 = this.EatLength();
			if (num2 > 0 && (b & 32) == 32)
			{
				DerSequenceReader derSequenceReader = new DerSequenceReader(true, this._data, this._position, this._end - this._position);
				while (derSequenceReader.HasData)
				{
					derSequenceReader.ValidateAndSkipDerValue();
				}
			}
			this._position += num2;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00056C20 File Offset: 0x00054E20
		internal byte[] ReadNextEncodedValue()
		{
			this.PeekTag();
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			byte[] array = new byte[num3];
			Buffer.BlockCopy(this._data, this._position, array, 0, num3);
			this._position += num3;
			return array;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00056C80 File Offset: 0x00054E80
		internal bool ReadBoolean()
		{
			this.EatTag(DerSequenceReader.DerTag.Boolean);
			int num = this.EatLength();
			if (num != 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			bool flag = this._data[this._position] > 0;
			this._position += num;
			return flag;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00056CC8 File Offset: 0x00054EC8
		internal int ReadInteger()
		{
			byte[] array = this.ReadIntegerBytes();
			Array.Reverse<byte>(array);
			return (int)new BigInteger(array);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00056CED File Offset: 0x00054EED
		internal byte[] ReadIntegerBytes()
		{
			this.EatTag(DerSequenceReader.DerTag.Integer);
			return this.ReadContentAsBytes();
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00056CFC File Offset: 0x00054EFC
		internal byte[] ReadBitString()
		{
			this.EatTag(DerSequenceReader.DerTag.BitString);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (this._data[this._position] > 7)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			num--;
			this._position++;
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00056D7D File Offset: 0x00054F7D
		internal byte[] ReadOctetString()
		{
			this.EatTag(DerSequenceReader.DerTag.OctetString);
			return this.ReadContentAsBytes();
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00056D8C File Offset: 0x00054F8C
		internal string ReadOidAsString()
		{
			this.EatTag(DerSequenceReader.DerTag.ObjectIdentifier);
			int num = this.EatLength();
			if (num < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			StringBuilder stringBuilder = new StringBuilder(num * 4);
			byte b = this._data[this._position];
			byte b2 = b / 40;
			byte b3 = b % 40;
			stringBuilder.Append(b2);
			stringBuilder.Append('.');
			stringBuilder.Append(b3);
			bool flag = true;
			BigInteger bigInteger = new BigInteger(0);
			for (int i = 1; i < num; i++)
			{
				byte b4 = this._data[this._position + i];
				byte b5 = b4 & 127;
				if (flag)
				{
					stringBuilder.Append('.');
					flag = false;
				}
				bigInteger <<= 7;
				bigInteger += b5;
				if (b4 == b5)
				{
					stringBuilder.Append(bigInteger);
					bigInteger = 0;
					flag = true;
				}
			}
			this._position += num;
			return stringBuilder.ToString();
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00056E7D File Offset: 0x0005507D
		internal Oid ReadOid()
		{
			return new Oid(this.ReadOidAsString());
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00056E8C File Offset: 0x0005508C
		internal string ReadUtf8String()
		{
			this.EatTag(DerSequenceReader.DerTag.UTF8String);
			int num = this.EatLength();
			string @string = Encoding.UTF8.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00056ED4 File Offset: 0x000550D4
		private DerSequenceReader ReadCollectionWithTag(DerSequenceReader.DerTag expected)
		{
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position + 1, this._end, out num2);
			int num3 = 1 + num2 + num;
			DerSequenceReader derSequenceReader = new DerSequenceReader(expected, this._data, this._position, num3);
			this._position += num3;
			return derSequenceReader;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00056F36 File Offset: 0x00055136
		internal DerSequenceReader ReadSequence()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Sequence);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00056F40 File Offset: 0x00055140
		internal DerSequenceReader ReadSet()
		{
			return this.ReadCollectionWithTag(DerSequenceReader.DerTag.Set);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00056F4C File Offset: 0x0005514C
		internal string ReadPrintableString()
		{
			this.EatTag(DerSequenceReader.DerTag.PrintableString);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00056F94 File Offset: 0x00055194
		internal string ReadIA5String()
		{
			this.EatTag(DerSequenceReader.DerTag.IA5String);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00056FDC File Offset: 0x000551DC
		internal string ReadT61String()
		{
			this.EatTag(DerSequenceReader.DerTag.T61String);
			int num = this.EatLength();
			Encoding encoding = LazyInitializer.EnsureInitialized<Encoding>(ref DerSequenceReader.s_utf8EncodingWithExceptionFallback, () => new UTF8Encoding(false, true));
			Encoding encoding2 = LazyInitializer.EnsureInitialized<Encoding>(ref DerSequenceReader.s_latin1Encoding, () => Encoding.GetEncoding("iso-8859-1"));
			string text;
			try
			{
				text = encoding.GetString(this._data, this._position, num);
			}
			catch (DecoderFallbackException)
			{
				text = encoding2.GetString(this._data, this._position, num);
			}
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(text);
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x000570A0 File Offset: 0x000552A0
		internal DateTime ReadX509Date()
		{
			DerSequenceReader.DerTag derTag = (DerSequenceReader.DerTag)this.PeekTag();
			if (derTag == DerSequenceReader.DerTag.UTCTime)
			{
				return this.ReadUtcTime();
			}
			if (derTag != DerSequenceReader.DerTag.GeneralizedTime)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return this.ReadGeneralizedTime();
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x000570D8 File Offset: 0x000552D8
		internal DateTime ReadUtcTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.UTCTime, "yyMMddHHmmss'Z'");
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000570E7 File Offset: 0x000552E7
		internal DateTime ReadGeneralizedTime()
		{
			return this.ReadTime(DerSequenceReader.DerTag.GeneralizedTime, "yyyyMMddHHmmss'Z'");
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000570F8 File Offset: 0x000552F8
		internal string ReadBMPString()
		{
			this.EatTag(DerSequenceReader.DerTag.BMPString);
			int num = this.EatLength();
			string @string = Encoding.BigEndianUnicode.GetString(this._data, this._position, num);
			this._position += num;
			return DerSequenceReader.TrimTrailingNulls(@string);
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00057140 File Offset: 0x00055340
		private static string TrimTrailingNulls(string value)
		{
			if (value != null && value.Length > 0)
			{
				int num = value.Length;
				while (num > 0 && value[num - 1] == '\0')
				{
					num--;
				}
				if (num != value.Length)
				{
					return value.Substring(0, num);
				}
			}
			return value;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00057188 File Offset: 0x00055388
		private DateTime ReadTime(DerSequenceReader.DerTag timeTag, string formatString)
		{
			this.EatTag(timeTag);
			int num = this.EatLength();
			string @string = Encoding.ASCII.GetString(this._data, this._position, num);
			this._position += num;
			DateTimeFormatInfo dateTimeFormatInfo = LazyInitializer.EnsureInitialized<DateTimeFormatInfo>(ref DerSequenceReader.s_validityDateTimeFormatInfo, delegate
			{
				DateTimeFormatInfo dateTimeFormatInfo2 = (DateTimeFormatInfo)CultureInfo.InvariantCulture.DateTimeFormat.Clone();
				dateTimeFormatInfo2.Calendar.TwoDigitYearMax = 2049;
				return dateTimeFormatInfo2;
			});
			DateTime dateTime;
			if (!DateTime.TryParseExact(@string, formatString, dateTimeFormatInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out dateTime))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return dateTime;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0005720C File Offset: 0x0005540C
		private byte[] ReadContentAsBytes()
		{
			int num = this.EatLength();
			byte[] array = new byte[num];
			Buffer.BlockCopy(this._data, this._position, array, 0, num);
			this._position += num;
			return array;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0005724A File Offset: 0x0005544A
		private void EatTag(DerSequenceReader.DerTag expected)
		{
			if (!this.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			DerSequenceReader.CheckTag(expected, this._data, this._position);
			this._position++;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00057280 File Offset: 0x00055480
		private static void CheckTag(DerSequenceReader.DerTag expected, byte[] data, int position)
		{
			if (position >= data.Length)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = data[position];
			byte b2 = b & 31;
			if (b2 == 31)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if ((b & 128) != 0)
			{
				return;
			}
			if ((byte)(expected & (DerSequenceReader.DerTag)31) != b2)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000572D4 File Offset: 0x000554D4
		private int EatLength()
		{
			int num2;
			int num = DerSequenceReader.ScanContentLength(this._data, this._position, this._end, out num2);
			this._position += num2;
			return num;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00057308 File Offset: 0x00055508
		private static int ScanContentLength(byte[] data, int offset, int end, out int bytesConsumed)
		{
			if (offset >= end)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			byte b = data[offset];
			if (b < 128)
			{
				bytesConsumed = 1;
				if ((int)b > end - offset - bytesConsumed)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (int)b;
			}
			else
			{
				int num = (int)(b & 127);
				if (num > 4)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				bytesConsumed = 1 + num;
				if (bytesConsumed > end - offset)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (bytesConsumed == 1)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				int num2 = offset + bytesConsumed;
				int num3 = 0;
				for (int i = offset + 1; i < num2; i++)
				{
					num3 <<= 8;
					num3 |= (int)data[i];
				}
				if (num3 < 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (num3 > end - offset - bytesConsumed)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return num3;
			}
		}

		// Token: 0x04000C2D RID: 3117
		internal const byte ContextSpecificTagFlag = 128;

		// Token: 0x04000C2E RID: 3118
		internal const byte ConstructedFlag = 32;

		// Token: 0x04000C2F RID: 3119
		internal const byte ContextSpecificConstructedTag0 = 160;

		// Token: 0x04000C30 RID: 3120
		internal const byte ContextSpecificConstructedTag1 = 161;

		// Token: 0x04000C31 RID: 3121
		internal const byte ContextSpecificConstructedTag2 = 162;

		// Token: 0x04000C32 RID: 3122
		internal const byte ContextSpecificConstructedTag3 = 163;

		// Token: 0x04000C33 RID: 3123
		internal const byte ConstructedSequence = 48;

		// Token: 0x04000C34 RID: 3124
		internal const byte TagClassMask = 192;

		// Token: 0x04000C35 RID: 3125
		internal const byte TagNumberMask = 31;

		// Token: 0x04000C36 RID: 3126
		internal static DateTimeFormatInfo s_validityDateTimeFormatInfo;

		// Token: 0x04000C37 RID: 3127
		private static Encoding s_utf8EncodingWithExceptionFallback;

		// Token: 0x04000C38 RID: 3128
		private static Encoding s_latin1Encoding;

		// Token: 0x04000C39 RID: 3129
		private readonly byte[] _data;

		// Token: 0x04000C3A RID: 3130
		private readonly int _end;

		// Token: 0x04000C3B RID: 3131
		private int _position;

		// Token: 0x020002B4 RID: 692
		internal enum DerTag : byte
		{
			// Token: 0x04000C3E RID: 3134
			Boolean = 1,
			// Token: 0x04000C3F RID: 3135
			Integer,
			// Token: 0x04000C40 RID: 3136
			BitString,
			// Token: 0x04000C41 RID: 3137
			OctetString,
			// Token: 0x04000C42 RID: 3138
			Null,
			// Token: 0x04000C43 RID: 3139
			ObjectIdentifier,
			// Token: 0x04000C44 RID: 3140
			UTF8String = 12,
			// Token: 0x04000C45 RID: 3141
			Sequence = 16,
			// Token: 0x04000C46 RID: 3142
			Set,
			// Token: 0x04000C47 RID: 3143
			PrintableString = 19,
			// Token: 0x04000C48 RID: 3144
			T61String,
			// Token: 0x04000C49 RID: 3145
			IA5String = 22,
			// Token: 0x04000C4A RID: 3146
			UTCTime,
			// Token: 0x04000C4B RID: 3147
			GeneralizedTime,
			// Token: 0x04000C4C RID: 3148
			BMPString = 30
		}
	}
}
