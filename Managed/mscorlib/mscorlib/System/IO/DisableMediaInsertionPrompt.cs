using System;

namespace System.IO
{
	// Token: 0x02000B00 RID: 2816
	internal struct DisableMediaInsertionPrompt : IDisposable
	{
		// Token: 0x06006495 RID: 25749 RVA: 0x00155C6C File Offset: 0x00153E6C
		public static DisableMediaInsertionPrompt Create()
		{
			DisableMediaInsertionPrompt disableMediaInsertionPrompt = default(DisableMediaInsertionPrompt);
			disableMediaInsertionPrompt._disableSuccess = Interop.Kernel32.SetThreadErrorMode(1U, out disableMediaInsertionPrompt._oldMode);
			return disableMediaInsertionPrompt;
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00155C98 File Offset: 0x00153E98
		public void Dispose()
		{
			if (this._disableSuccess)
			{
				uint num;
				Interop.Kernel32.SetThreadErrorMode(this._oldMode, out num);
			}
		}

		// Token: 0x04003B2A RID: 15146
		private bool _disableSuccess;

		// Token: 0x04003B2B RID: 15147
		private uint _oldMode;
	}
}
