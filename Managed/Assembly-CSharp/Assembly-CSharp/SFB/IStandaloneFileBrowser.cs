using System;

namespace SFB
{
	// Token: 0x02000189 RID: 393
	public interface IStandaloneFileBrowser
	{
		// Token: 0x06000F04 RID: 3844
		string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect);

		// Token: 0x06000F05 RID: 3845
		string[] OpenFolderPanel(string title, string directory, bool multiselect);

		// Token: 0x06000F06 RID: 3846
		string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions);

		// Token: 0x06000F07 RID: 3847
		void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb);

		// Token: 0x06000F08 RID: 3848
		void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb);

		// Token: 0x06000F09 RID: 3849
		void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb);
	}
}
