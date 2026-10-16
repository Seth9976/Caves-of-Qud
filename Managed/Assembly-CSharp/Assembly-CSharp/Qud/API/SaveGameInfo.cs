using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Platform.IO;
using XRL;
using XRL.Collections;

namespace Qud.API
{
	// Token: 0x0200040D RID: 1037
	[Serializable]
	public class SaveGameInfo
	{
		// Token: 0x0600275E RID: 10078 RVA: 0x000CC48C File Offset: 0x000CA68C
		public bool DifferentMods()
		{
			if (this.ModsEnabled == null)
			{
				this.ModsEnabled = new List<string>();
			}
			Rack<ModInfo> activeMods = ModManager.ActiveMods;
			return ((activeMods != null) ? activeMods.Count : 0) != this.ModsEnabled.Count || this.ModsEnabled.Union<string>(ModManager.GetRunningMods()).Count<string>() != this.ModsEnabled.Count;
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x000CC4F1 File Offset: 0x000CA6F1
		public void Delete()
		{
			DataManager.DeleteSaveDirectory(this.Directory);
		}

		// Token: 0x06002760 RID: 10080 RVA: 0x000CC500 File Offset: 0x000CA700
		public async Task<bool> TryRestoreModsAndLoadAsync()
		{
			await The.UiContext;
			TaskAwaiter<bool> taskAwaiter = The.Core.RestoreModsLoadedAsync(this.ModsEnabled).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			bool flag;
			if (taskAwaiter.GetResult())
			{
				Task loadGameTask = Task.Run(delegate
				{
					XRLGame.LoadGame(Path.Combine(new string[] { this.Directory, "Primary" }), true, true, null).Wait();
				});
				await loadGameTask;
				flag = loadGameTask.IsCompletedSuccessfully;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001371 RID: 4977
		public string ID;

		// Token: 0x04001372 RID: 4978
		public string Name;

		// Token: 0x04001373 RID: 4979
		public string Description;

		// Token: 0x04001374 RID: 4980
		public string SaveTime;

		// Token: 0x04001375 RID: 4981
		public string Info;

		// Token: 0x04001376 RID: 4982
		public string Version;

		// Token: 0x04001377 RID: 4983
		public SaveGameJSON json;

		// Token: 0x04001378 RID: 4984
		public string Directory;

		// Token: 0x04001379 RID: 4985
		public string Size;

		// Token: 0x0400137A RID: 4986
		public List<string> ModsEnabled;
	}
}
