using System;

namespace SFB
{
	// Token: 0x0200018B RID: 395
	public class StandaloneFileBrowser
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x0005FA5C File Offset: 0x0005DC5C
		public static string[] OpenFilePanel(string title, string directory, string extension, bool multiselect)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			return StandaloneFileBrowser.OpenFilePanel(title, directory, array2, multiselect);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0005FA9F File Offset: 0x0005DC9F
		public static string[] OpenFilePanel(string title, string directory, ExtensionFilter[] extensions, bool multiselect)
		{
			return StandaloneFileBrowser._platformWrapper.OpenFilePanel(title, directory, extensions, multiselect);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0005FAB0 File Offset: 0x0005DCB0
		public static void OpenFilePanelAsync(string title, string directory, string extension, bool multiselect, Action<string[]> cb)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			StandaloneFileBrowser.OpenFilePanelAsync(title, directory, array2, multiselect, cb);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0005FAF5 File Offset: 0x0005DCF5
		public static void OpenFilePanelAsync(string title, string directory, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
		{
			StandaloneFileBrowser._platformWrapper.OpenFilePanelAsync(title, directory, extensions, multiselect, cb);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0005FB07 File Offset: 0x0005DD07
		public static string[] OpenFolderPanel(string title, string directory, bool multiselect)
		{
			return StandaloneFileBrowser._platformWrapper.OpenFolderPanel(title, directory, multiselect);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0005FB16 File Offset: 0x0005DD16
		public static void OpenFolderPanelAsync(string title, string directory, bool multiselect, Action<string[]> cb)
		{
			StandaloneFileBrowser._platformWrapper.OpenFolderPanelAsync(title, directory, multiselect, cb);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0005FB28 File Offset: 0x0005DD28
		public static string SaveFilePanel(string title, string directory, string defaultName, string extension)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			return StandaloneFileBrowser.SaveFilePanel(title, directory, defaultName, array2);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0005FB6B File Offset: 0x0005DD6B
		public static string SaveFilePanel(string title, string directory, string defaultName, ExtensionFilter[] extensions)
		{
			return StandaloneFileBrowser._platformWrapper.SaveFilePanel(title, directory, defaultName, extensions);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0005FB7C File Offset: 0x0005DD7C
		public static void SaveFilePanelAsync(string title, string directory, string defaultName, string extension, Action<string> cb)
		{
			ExtensionFilter[] array;
			if (!string.IsNullOrEmpty(extension))
			{
				(array = new ExtensionFilter[1])[0] = new ExtensionFilter("", new string[] { extension });
			}
			else
			{
				array = null;
			}
			ExtensionFilter[] array2 = array;
			StandaloneFileBrowser.SaveFilePanelAsync(title, directory, defaultName, array2, cb);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0005FBC1 File Offset: 0x0005DDC1
		public static void SaveFilePanelAsync(string title, string directory, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
		{
			StandaloneFileBrowser._platformWrapper.SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
		}

		// Token: 0x04000A6D RID: 2669
		private static IStandaloneFileBrowser _platformWrapper = new StandaloneFileBrowserWindows();
	}
}
