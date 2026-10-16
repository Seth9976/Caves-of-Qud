using System;
using System.CodeDom;

namespace System.Web.Services.Description
{
	// Token: 0x020000D6 RID: 214
	internal abstract class MimeImporter
	{
		// Token: 0x0600056E RID: 1390
		internal abstract MimeParameterCollection ImportParameters();

		// Token: 0x0600056F RID: 1391
		internal abstract MimeReturn ImportReturn();

		// Token: 0x06000570 RID: 1392 RVA: 0x00002B13 File Offset: 0x00000D13
		internal virtual void GenerateCode(MimeReturn[] importedReturns, MimeParameterCollection[] importedParameters)
		{
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00002B13 File Offset: 0x00000D13
		internal virtual void AddClassMetadata(CodeTypeDeclaration codeClass)
		{
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00018CB4 File Offset: 0x00016EB4
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x00018CBC File Offset: 0x00016EBC
		internal HttpProtocolImporter ImportContext
		{
			get
			{
				return this.protocol;
			}
			set
			{
				this.protocol = value;
			}
		}

		// Token: 0x04000394 RID: 916
		private HttpProtocolImporter protocol;
	}
}
