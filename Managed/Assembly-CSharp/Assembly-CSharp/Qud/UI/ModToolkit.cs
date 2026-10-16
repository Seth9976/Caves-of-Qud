using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using XRL;
using XRL.UI;
using XRL.World.Text;

namespace Qud.UI
{
	// Token: 0x02000499 RID: 1177
	[ExecuteAlways]
	[UIView("ModToolkit", false, false, false, null, null, false, 0, false, NavCategory = "Adventure", UICanvas = "ModToolkit", UICanvasHost = 1)]
	public class ModToolkit : SingletonWindowBase<ModToolkit>
	{
		// Token: 0x06002D23 RID: 11555 RVA: 0x000F3D28 File Offset: 0x000F1F28
		public override void Hide()
		{
			base.Hide();
			base.gameObject.SetActive(false);
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000F3D3C File Offset: 0x000F1F3C
		public override void Show()
		{
			base.gameObject.SetActive(true);
			base.Show();
			this.menuController.UpdateElements(true);
			this.menuController.Reselect(0);
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000F3D68 File Offset: 0x000F1F68
		public override void Init()
		{
			base.Init();
			if (this.menuController != null)
			{
				this.menuController.cancelHandlers.RemoveAllListeners();
				this.menuController.cancelHandlers.AddListener(new UnityAction(this.OnCancel));
				this.menuController.activateHandlers.RemoveAllListeners();
				this.menuController.activateHandlers.AddListener(new UnityAction<QudMenuItem>(this.OnActivate));
				this.menuController.isCurrentWindow = new Func<bool>(base.isCurrentWindow);
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000F3DF8 File Offset: 0x000F1FF8
		public void OnCancel()
		{
			this.menuclosed.TrySetResult(true);
			this.Hide();
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000F3E10 File Offset: 0x000F2010
		public async Task<bool> ShowMenuAsync()
		{
			this.menuclosed.TrySetCanceled();
			this.menuclosed = new TaskCompletionSource<bool>();
			await The.UiContext;
			UIManager.showWindow("ModToolkit", false);
			return await this.menuclosed.Task;
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000F3E54 File Offset: 0x000F2054
		public void OnActivate(QudMenuItem data)
		{
			if (data.command == "ModManager")
			{
				ControlManager.ConsumeCurrentInput();
				UIManager.pushWindow("ModManager", false);
			}
			if (data.command == "MapEditor")
			{
				ControlManager.ConsumeCurrentInput();
				GameManager.Instance.PushGameView("MapEditor", true);
				this.Hide();
			}
			if (data.command == "Workshop")
			{
				ControlManager.ConsumeCurrentInput();
				UIManager.getWindow("SteamWorkshopUploader").Show();
				this.Hide();
			}
			if (data.command == "Wiki")
			{
				ControlManager.ConsumeCurrentInput();
				Application.OpenURL("https://wiki.cavesofqud.com/Modding:Overview");
			}
			if (data.command == "History")
			{
				ControlManager.ConsumeCurrentInput();
				UIManager.getWindow("HistoryTest").Show();
				this.Hide();
			}
			if (data.command == "Waveform")
			{
				ControlManager.ConsumeCurrentInput();
				UIManager.getWindow("WaveformTest").Show();
				this.Hide();
			}
			if (data.command == "Blueprint")
			{
				ControlManager.ConsumeCurrentInput();
				UIManager.getWindow("BrowseBlueprintsView").Show();
				this.Hide();
			}
			if (data.command == "OpenSave")
			{
				ControlManager.ConsumeCurrentInput();
				this.OpenFolder();
			}
			if (data.command == "csproj")
			{
				ControlManager.ConsumeCurrentInput();
				this.WriteCSProj();
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000F3FBC File Offset: 0x000F21BC
		public void WriteCSProj()
		{
			string text = DataManager.SavePath("Mods.csproj");
			string text2 = DataManager.FilePath("Mods.csproj.template.txt");
			string fullPath = Path.GetFullPath(Path.Combine(Application.dataPath, "Managed") + "/");
			string text3 = File.ReadAllText(text2);
			text3 = text3.Replace("$MANAGED_PATH$", fullPath);
			text3 = text3.Replace("$AUTHORS$", Environment.UserName.Coalesce("Unknown"));
			using (TextBuilder textBuilder = TextBuilder.Get(64))
			{
				foreach (Assembly assembly in from x in AppDomain.CurrentDomain.GetAssemblies().Where<Assembly>(new Func<Assembly, bool>(ModManager.MainAssemblyPredicate))
					orderby x.GetName().Name
					select x)
				{
					string text4 = Path.Join("$(QudLibPath)", Path.GetRelativePath(fullPath, assembly.Location));
					textBuilder.Append("\n\t\t<Reference Include=\"" + text4 + "\" />");
				}
				textBuilder.Compound("\n\t", "");
				text3 = text3.Replace("$REFERENCES", textBuilder.ToString());
			}
			File.WriteAllText(text, text3);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000F4128 File Offset: 0x000F2328
		public void OpenFolder()
		{
			string text = DataManager.SavePath("").TrimEnd(new char[] { '\\', '/' });
			try
			{
				if (Process.Start("open", "\"" + text + "\"") != null)
				{
					return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				if (Process.Start("xdg-open", "\"" + text + "\"") != null)
				{
					return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				Process.Start("explorer.exe", "\"" + text.Replace("/", "\\") + "\"");
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04001946 RID: 6470
		public QudTextMenuController menuController;

		// Token: 0x04001947 RID: 6471
		private TaskCompletionSource<bool> menuclosed = new TaskCompletionSource<bool>();
	}
}
