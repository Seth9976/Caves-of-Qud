using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000747 RID: 1863
	internal class ManagedErrorInfo : IErrorInfo
	{
		// Token: 0x0600414A RID: 16714 RVA: 0x000E1FFF File Offset: 0x000E01FF
		public ManagedErrorInfo(Exception e)
		{
			this.m_Exception = e;
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600414B RID: 16715 RVA: 0x000E200E File Offset: 0x000E020E
		public Exception Exception
		{
			get
			{
				return this.m_Exception;
			}
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x000E2016 File Offset: 0x000E0216
		public int GetGUID(out Guid guid)
		{
			guid = Guid.Empty;
			return 0;
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x000E2024 File Offset: 0x000E0224
		public int GetSource(out string source)
		{
			source = this.m_Exception.Source;
			return 0;
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x000E2034 File Offset: 0x000E0234
		public int GetDescription(out string description)
		{
			description = this.m_Exception.Message;
			return 0;
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x000E2044 File Offset: 0x000E0244
		public int GetHelpFile(out string helpFile)
		{
			helpFile = this.m_Exception.HelpLink;
			return 0;
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x000E2054 File Offset: 0x000E0254
		public int GetHelpContext(out uint helpContext)
		{
			helpContext = 0U;
			return 0;
		}

		// Token: 0x04002BCB RID: 11211
		private Exception m_Exception;
	}
}
