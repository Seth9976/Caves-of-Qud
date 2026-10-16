using System;
using System.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000124 RID: 292
	internal static class PdbGuidMapping
	{
		// Token: 0x06000B06 RID: 2822 RVA: 0x00024E30 File Offset: 0x00023030
		static PdbGuidMapping()
		{
			PdbGuidMapping.AddMapping(DocumentLanguage.C, new Guid("63a08714-fc37-11d2-904c-00c04fa302a1"));
			PdbGuidMapping.AddMapping(DocumentLanguage.Cpp, new Guid("3a12d0b7-c26c-11d0-b442-00a0244a1dd2"));
			PdbGuidMapping.AddMapping(DocumentLanguage.CSharp, new Guid("3f5162f8-07c6-11d3-9053-00c04fa302a1"));
			PdbGuidMapping.AddMapping(DocumentLanguage.Basic, new Guid("3a12d0b8-c26c-11d0-b442-00a0244a1dd2"));
			PdbGuidMapping.AddMapping(DocumentLanguage.Java, new Guid("3a12d0b4-c26c-11d0-b442-00a0244a1dd2"));
			PdbGuidMapping.AddMapping(DocumentLanguage.Cobol, new Guid("af046cd1-d0e1-11d2-977c-00a0c9b4d50c"));
			PdbGuidMapping.AddMapping(DocumentLanguage.Pascal, new Guid("af046cd2-d0e1-11d2-977c-00a0c9b4d50c"));
			PdbGuidMapping.AddMapping(DocumentLanguage.Cil, new Guid("af046cd3-d0e1-11d2-977c-00a0c9b4d50c"));
			PdbGuidMapping.AddMapping(DocumentLanguage.JScript, new Guid("3a12d0b6-c26c-11d0-b442-00a0244a1dd2"));
			PdbGuidMapping.AddMapping(DocumentLanguage.Smc, new Guid("0d9b9f7b-6611-11d3-bd2a-0000f80849bd"));
			PdbGuidMapping.AddMapping(DocumentLanguage.MCpp, new Guid("4b35fde8-07c6-11d3-9053-00c04fa302a1"));
			PdbGuidMapping.AddMapping(DocumentLanguage.FSharp, new Guid("ab4f38c9-b6e6-43ba-be3b-58080b2ccce3"));
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00024F60 File Offset: 0x00023160
		private static void AddMapping(DocumentLanguage language, Guid guid)
		{
			PdbGuidMapping.guid_language.Add(guid, language);
			PdbGuidMapping.language_guid.Add(language, guid);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00024F7A File Offset: 0x0002317A
		public static DocumentType ToType(this Guid guid)
		{
			if (guid == PdbGuidMapping.type_text)
			{
				return DocumentType.Text;
			}
			return DocumentType.Other;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00024F8C File Offset: 0x0002318C
		public static Guid ToGuid(this DocumentType type)
		{
			if (type == DocumentType.Text)
			{
				return PdbGuidMapping.type_text;
			}
			return default(Guid);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00024FAC File Offset: 0x000231AC
		public static DocumentHashAlgorithm ToHashAlgorithm(this Guid guid)
		{
			if (guid == PdbGuidMapping.hash_md5)
			{
				return DocumentHashAlgorithm.MD5;
			}
			if (guid == PdbGuidMapping.hash_sha1)
			{
				return DocumentHashAlgorithm.SHA1;
			}
			if (guid == PdbGuidMapping.hash_sha256)
			{
				return DocumentHashAlgorithm.SHA256;
			}
			return DocumentHashAlgorithm.None;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00024FDC File Offset: 0x000231DC
		public static Guid ToGuid(this DocumentHashAlgorithm hash_algo)
		{
			if (hash_algo == DocumentHashAlgorithm.MD5)
			{
				return PdbGuidMapping.hash_md5;
			}
			if (hash_algo != DocumentHashAlgorithm.SHA1)
			{
				return default(Guid);
			}
			return PdbGuidMapping.hash_sha1;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00025008 File Offset: 0x00023208
		public static DocumentLanguage ToLanguage(this Guid guid)
		{
			DocumentLanguage documentLanguage;
			if (!PdbGuidMapping.guid_language.TryGetValue(guid, ref documentLanguage))
			{
				return DocumentLanguage.Other;
			}
			return documentLanguage;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00025028 File Offset: 0x00023228
		public static Guid ToGuid(this DocumentLanguage language)
		{
			Guid guid;
			if (!PdbGuidMapping.language_guid.TryGetValue(language, ref guid))
			{
				return default(Guid);
			}
			return guid;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0002504F File Offset: 0x0002324F
		public static DocumentLanguageVendor ToVendor(this Guid guid)
		{
			if (guid == PdbGuidMapping.vendor_ms)
			{
				return DocumentLanguageVendor.Microsoft;
			}
			return DocumentLanguageVendor.Other;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00025064 File Offset: 0x00023264
		public static Guid ToGuid(this DocumentLanguageVendor vendor)
		{
			if (vendor == DocumentLanguageVendor.Microsoft)
			{
				return PdbGuidMapping.vendor_ms;
			}
			return default(Guid);
		}

		// Token: 0x04000690 RID: 1680
		private static readonly Dictionary<Guid, DocumentLanguage> guid_language = new Dictionary<Guid, DocumentLanguage>();

		// Token: 0x04000691 RID: 1681
		private static readonly Dictionary<DocumentLanguage, Guid> language_guid = new Dictionary<DocumentLanguage, Guid>();

		// Token: 0x04000692 RID: 1682
		private static readonly Guid type_text = new Guid("5a869d0b-6611-11d3-bd2a-0000f80849bd");

		// Token: 0x04000693 RID: 1683
		private static readonly Guid hash_md5 = new Guid("406ea660-64cf-4c82-b6f0-42d48172a799");

		// Token: 0x04000694 RID: 1684
		private static readonly Guid hash_sha1 = new Guid("ff1816ec-aa5e-4d10-87f7-6f4963833460");

		// Token: 0x04000695 RID: 1685
		private static readonly Guid hash_sha256 = new Guid("8829d00f-11b8-4213-878b-770e8597ac16");

		// Token: 0x04000696 RID: 1686
		private static readonly Guid vendor_ms = new Guid("994b45c4-e6e9-11d2-903f-00c04fa302a1");
	}
}
