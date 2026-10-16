using System;
using System.Globalization;
using System.Net;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002BE RID: 702
	internal sealed class GeneralNameEncoder
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x00057F1A File Offset: 0x0005611A
		internal byte[][] EncodeEmailAddress(string emailAddress)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(emailAddress.ToCharArray());
			array[0][0] = 129;
			return array;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00057F31 File Offset: 0x00056131
		internal byte[][] EncodeDnsName(string dnsName)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(GeneralNameEncoder.s_idnMapping.GetAscii(dnsName).ToCharArray());
			array[0][0] = 130;
			return array;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00057F52 File Offset: 0x00056152
		internal byte[][] EncodeUri(Uri uri)
		{
			byte[][] array = DerEncoder.SegmentedEncodeIA5String(uri.AbsoluteUri.ToCharArray());
			array[0][0] = 134;
			return array;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00057F6E File Offset: 0x0005616E
		internal byte[][] EncodeIpAddress(IPAddress address)
		{
			byte[][] array = DerEncoder.SegmentedEncodeOctetString(address.GetAddressBytes());
			array[0][0] = 135;
			return array;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00057F88 File Offset: 0x00056188
		internal byte[][] EncodeUserPrincipalName(string upn)
		{
			byte[][] array = DerEncoder.SegmentedEncodeUtf8String(upn.ToCharArray());
			byte[][] array2 = DerEncoder.ConstructSegmentedSequence(new byte[][][] { array });
			array2[0][0] = 160;
			byte[][] array3 = DerEncoder.ConstructSegmentedSequence(new byte[][][]
			{
				DerEncoder.SegmentedEncodeOid("1.3.6.1.4.1.311.20.2.3"),
				array2
			});
			array3[0][0] = 160;
			return array3;
		}

		// Token: 0x04000C6F RID: 3183
		private static readonly IdnMapping s_idnMapping = new IdnMapping();

		// Token: 0x020002BF RID: 703
		private enum GeneralNameTag : byte
		{
			// Token: 0x04000C71 RID: 3185
			OtherName = 160,
			// Token: 0x04000C72 RID: 3186
			Rfc822Name = 129,
			// Token: 0x04000C73 RID: 3187
			DnsName,
			// Token: 0x04000C74 RID: 3188
			X400Address,
			// Token: 0x04000C75 RID: 3189
			DirectoryName,
			// Token: 0x04000C76 RID: 3190
			EdiPartyName,
			// Token: 0x04000C77 RID: 3191
			Uri,
			// Token: 0x04000C78 RID: 3192
			IpAddress,
			// Token: 0x04000C79 RID: 3193
			RegisteredId
		}
	}
}
