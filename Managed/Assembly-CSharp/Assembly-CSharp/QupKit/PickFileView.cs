using System;
using System.IO;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003A1 RID: 929
	public class PickFileView : BaseView
	{
		// Token: 0x060022E5 RID: 8933 RVA: 0x000B1A0E File Offset: 0x000AFC0E
		public static bool IsShowing()
		{
			return PickFileView._showing;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000B1A18 File Offset: 0x000AFC18
		public static void Show(Action<string> _After = null, FileBrowser.PickFileModes _Mode = FileBrowser.PickFileModes.Select, string pattern = "*.rpm")
		{
			PickFileView.PreviousView = LegacyViewManager.Instance.ActiveView;
			LegacyViewManager.Instance.ActiveView = LegacyViewManager.Instance.Views["PickFile"];
			LegacyViewManager.Instance.Views["PickFile"].Enter();
			PickFileView.After = _After;
			FileBrowser.SaveFileName = "";
			FileBrowser.Mode = _Mode;
			FileBrowser.Pattern = pattern;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000B1A87 File Offset: 0x000AFC87
		public override void OnGUI()
		{
			this.m_fileBrowser.OnGUI();
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000B1A94 File Offset: 0x000AFC94
		public void Selected(string path)
		{
			PickFileView.PreviousView = null;
			this.m_fileBrowser = null;
			if (PickFileView.After != null)
			{
				PickFileView.After(path);
				PickFileView.After = null;
			}
			PickFileView._showing = false;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000B1AC4 File Offset: 0x000AFCC4
		public override void Enter()
		{
			PickFileView._showing = true;
			base.Enter();
			this.m_fileBrowser = new FileBrowser(new Rect(100f, 100f, 1000f, 500f), "Choose " + FileBrowser.Pattern + " File", new FileBrowser.FinishedCallback(this.Selected));
			this.m_fileBrowser.SelectionPattern = FileBrowser.Pattern;
			this.m_fileBrowser.CurrentDirectory = Path.Combine(Application.streamingAssetsPath, "Base").Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000B1B63 File Offset: 0x000AFD63
		protected void OnGUIMain()
		{
			this.m_fileBrowser.OnGUI();
		}

		// Token: 0x04001196 RID: 4502
		private static BaseView PreviousView;

		// Token: 0x04001197 RID: 4503
		private new static Action<string> After;

		// Token: 0x04001198 RID: 4504
		protected Texture2D m_directoryImage;

		// Token: 0x04001199 RID: 4505
		protected Texture2D m_fileImage;

		// Token: 0x0400119A RID: 4506
		protected FileBrowser m_fileBrowser;

		// Token: 0x0400119B RID: 4507
		protected string m_textPath;

		// Token: 0x0400119C RID: 4508
		public static bool _showing;
	}
}
