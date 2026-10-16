using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002B8 RID: 696
	public sealed class Document : DebugInformation
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x0003942D File Offset: 0x0003762D
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x00039435 File Offset: 0x00037635
		public string Url
		{
			get
			{
				return this.url;
			}
			set
			{
				this.url = value;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0003943E File Offset: 0x0003763E
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x0003944B File Offset: 0x0003764B
		public DocumentType Type
		{
			get
			{
				return this.type.ToType();
			}
			set
			{
				this.type = value.ToGuid();
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00039459 File Offset: 0x00037659
		// (set) Token: 0x06001188 RID: 4488 RVA: 0x00039461 File Offset: 0x00037661
		public Guid TypeGuid
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0003946A File Offset: 0x0003766A
		// (set) Token: 0x0600118A RID: 4490 RVA: 0x00039477 File Offset: 0x00037677
		public DocumentHashAlgorithm HashAlgorithm
		{
			get
			{
				return this.hash_algorithm.ToHashAlgorithm();
			}
			set
			{
				this.hash_algorithm = value.ToGuid();
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x00039485 File Offset: 0x00037685
		// (set) Token: 0x0600118C RID: 4492 RVA: 0x0003948D File Offset: 0x0003768D
		public Guid HashAlgorithmGuid
		{
			get
			{
				return this.hash_algorithm;
			}
			set
			{
				this.hash_algorithm = value;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x00039496 File Offset: 0x00037696
		// (set) Token: 0x0600118E RID: 4494 RVA: 0x000394A3 File Offset: 0x000376A3
		public DocumentLanguage Language
		{
			get
			{
				return this.language.ToLanguage();
			}
			set
			{
				this.language = value.ToGuid();
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x000394B1 File Offset: 0x000376B1
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x000394B9 File Offset: 0x000376B9
		public Guid LanguageGuid
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x000394C2 File Offset: 0x000376C2
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x000394CF File Offset: 0x000376CF
		public DocumentLanguageVendor LanguageVendor
		{
			get
			{
				return this.language_vendor.ToVendor();
			}
			set
			{
				this.language_vendor = value.ToGuid();
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x000394DD File Offset: 0x000376DD
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x000394E5 File Offset: 0x000376E5
		public Guid LanguageVendorGuid
		{
			get
			{
				return this.language_vendor;
			}
			set
			{
				this.language_vendor = value;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000394EE File Offset: 0x000376EE
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x000394F6 File Offset: 0x000376F6
		public byte[] Hash
		{
			get
			{
				return this.hash;
			}
			set
			{
				this.hash = value;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x000394FF File Offset: 0x000376FF
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x00039507 File Offset: 0x00037707
		public byte[] EmbeddedSource
		{
			get
			{
				return this.embedded_source;
			}
			set
			{
				this.embedded_source = value;
			}
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00039510 File Offset: 0x00037710
		public Document(string url)
		{
			this.url = url;
			this.hash = Empty<byte>.Array;
			this.embedded_source = Empty<byte>.Array;
			this.token = new MetadataToken(TokenType.Document);
		}

		// Token: 0x040007E2 RID: 2018
		private string url;

		// Token: 0x040007E3 RID: 2019
		private Guid type;

		// Token: 0x040007E4 RID: 2020
		private Guid hash_algorithm;

		// Token: 0x040007E5 RID: 2021
		private Guid language;

		// Token: 0x040007E6 RID: 2022
		private Guid language_vendor;

		// Token: 0x040007E7 RID: 2023
		private byte[] hash;

		// Token: 0x040007E8 RID: 2024
		private byte[] embedded_source;
	}
}
