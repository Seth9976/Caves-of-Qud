using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D3 RID: 211
	public class Asn1Identifier : ICloneable
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00016F13 File Offset: 0x00015113
		public virtual int Asn1Class
		{
			get
			{
				return this.tagClass;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00016F1B File Offset: 0x0001511B
		public virtual bool Constructed
		{
			get
			{
				return this.constructed;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00016F23 File Offset: 0x00015123
		public virtual int Tag
		{
			get
			{
				return this.tag;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00016F2B File Offset: 0x0001512B
		public virtual int EncodedLength
		{
			get
			{
				return this.encodedLength;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00016F33 File Offset: 0x00015133
		[CLSCompliant(false)]
		public virtual bool Universal
		{
			get
			{
				return this.tagClass == 0;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00016F3E File Offset: 0x0001513E
		[CLSCompliant(false)]
		public virtual bool Application
		{
			get
			{
				return this.tagClass == 1;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x00016F49 File Offset: 0x00015149
		[CLSCompliant(false)]
		public virtual bool Context
		{
			get
			{
				return this.tagClass == 2;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00016F54 File Offset: 0x00015154
		[CLSCompliant(false)]
		public virtual bool Private
		{
			get
			{
				return this.tagClass == 3;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00016F5F File Offset: 0x0001515F
		public Asn1Identifier(int tagClass, bool constructed, int tag)
		{
			this.tagClass = tagClass;
			this.constructed = constructed;
			this.tag = tag;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00016F7C File Offset: 0x0001517C
		public Asn1Identifier(Stream in_Renamed)
		{
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = (num & 32) != 0;
			this.tag = num & 31;
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00016FEA File Offset: 0x000151EA
		public Asn1Identifier()
		{
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00016FF4 File Offset: 0x000151F4
		public void reset(Stream in_Renamed)
		{
			this.encodedLength = 0;
			int num = in_Renamed.ReadByte();
			this.encodedLength++;
			if (num < 0)
			{
				throw new EndOfStreamException("BERDecoder: decode: EOF in Identifier");
			}
			this.tagClass = num >> 6;
			this.constructed = (num & 32) != 0;
			this.tag = num & 31;
			if (this.tag == 31)
			{
				this.tag = this.decodeTagNumber(in_Renamed);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00017064 File Offset: 0x00015264
		private int decodeTagNumber(Stream in_Renamed)
		{
			int num = 0;
			for (;;)
			{
				int num2 = in_Renamed.ReadByte();
				this.encodedLength++;
				if (num2 < 0)
				{
					break;
				}
				num = (num << 7) + (num2 & 127);
				if ((num2 & 128) == 0)
				{
					return num;
				}
			}
			throw new EndOfStreamException("BERDecoder: decode: EOF in tag number");
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x000170AC File Offset: 0x000152AC
		public object Clone()
		{
			object obj;
			try
			{
				obj = base.MemberwiseClone();
			}
			catch (Exception)
			{
				throw new SystemException("Internal error, cannot create clone");
			}
			return obj;
		}

		// Token: 0x0400049F RID: 1183
		public const int UNIVERSAL = 0;

		// Token: 0x040004A0 RID: 1184
		public const int APPLICATION = 1;

		// Token: 0x040004A1 RID: 1185
		public const int CONTEXT = 2;

		// Token: 0x040004A2 RID: 1186
		public const int PRIVATE = 3;

		// Token: 0x040004A3 RID: 1187
		private int tagClass;

		// Token: 0x040004A4 RID: 1188
		private bool constructed;

		// Token: 0x040004A5 RID: 1189
		private int tag;

		// Token: 0x040004A6 RID: 1190
		private int encodedLength;
	}
}
