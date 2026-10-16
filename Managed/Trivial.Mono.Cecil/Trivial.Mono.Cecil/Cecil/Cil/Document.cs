using System;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x020000FF RID: 255
	public sealed class Document : DebugInformation
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00022665 File Offset: 0x00020865
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0002266D File Offset: 0x0002086D
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

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00022676 File Offset: 0x00020876
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x0002267E File Offset: 0x0002087E
		public DocumentType Type
		{
			get
			{
				return (DocumentType)this.type;
			}
			set
			{
				this.type = (byte)value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00022688 File Offset: 0x00020888
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00022690 File Offset: 0x00020890
		public DocumentHashAlgorithm HashAlgorithm
		{
			get
			{
				return (DocumentHashAlgorithm)this.hash_algorithm;
			}
			set
			{
				this.hash_algorithm = (byte)value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x0002269A File Offset: 0x0002089A
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x000226A2 File Offset: 0x000208A2
		public DocumentLanguage Language
		{
			get
			{
				return (DocumentLanguage)this.language;
			}
			set
			{
				this.language = (byte)value;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x000226AC File Offset: 0x000208AC
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x000226B4 File Offset: 0x000208B4
		public DocumentLanguageVendor LanguageVendor
		{
			get
			{
				return (DocumentLanguageVendor)this.language_vendor;
			}
			set
			{
				this.language_vendor = (byte)value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x000226BE File Offset: 0x000208BE
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x000226C6 File Offset: 0x000208C6
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

		// Token: 0x06000A2F RID: 2607 RVA: 0x000226CF File Offset: 0x000208CF
		public Document(string url)
		{
			this.url = url;
			this.hash = Empty<byte>.Array;
			this.token = new MetadataToken(TokenType.Document);
		}

		// Token: 0x04000517 RID: 1303
		private string url;

		// Token: 0x04000518 RID: 1304
		private byte type;

		// Token: 0x04000519 RID: 1305
		private byte hash_algorithm;

		// Token: 0x0400051A RID: 1306
		private byte language;

		// Token: 0x0400051B RID: 1307
		private byte language_vendor;

		// Token: 0x0400051C RID: 1308
		private byte[] hash;
	}
}
