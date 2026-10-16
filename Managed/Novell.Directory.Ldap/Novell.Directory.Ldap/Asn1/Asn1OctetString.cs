using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D9 RID: 217
	public class Asn1OctetString : Asn1Object
	{
		// Token: 0x06000554 RID: 1364 RVA: 0x000173F1 File Offset: 0x000155F1
		[CLSCompliant(false)]
		public Asn1OctetString(sbyte[] content)
			: base(Asn1OctetString.ID)
		{
			this.content = content;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00017408 File Offset: 0x00015608
		public Asn1OctetString(string content)
			: base(Asn1OctetString.ID)
		{
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(content));
				this.content = array;
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001745C File Offset: 0x0001565C
		[CLSCompliant(false)]
		public Asn1OctetString(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1OctetString.ID)
		{
			this.content = ((len > 0) ? ((sbyte[])dec.decodeOctetString(in_Renamed, len)) : new sbyte[0]);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00017488 File Offset: 0x00015688
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00017492 File Offset: 0x00015692
		[CLSCompliant(false)]
		public sbyte[] byteValue()
		{
			return this.content;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001749C File Offset: 0x0001569C
		public string stringValue()
		{
			string text = null;
			try
			{
				text = new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray(this.content)));
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
			return text;
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000174EC File Offset: 0x000156EC
		public override string ToString()
		{
			return base.ToString() + "OCTET STRING: " + this.stringValue();
		}

		// Token: 0x040004AF RID: 1199
		private sbyte[] content;

		// Token: 0x040004B0 RID: 1200
		public const int TAG = 4;

		// Token: 0x040004B1 RID: 1201
		protected internal static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 4);
	}
}
