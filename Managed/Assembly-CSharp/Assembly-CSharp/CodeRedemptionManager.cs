using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Ionic.Zip;
using Qud.UI;
using UnityEngine;
using XRL;
using XRL.UI;

// Token: 0x02000040 RID: 64
public static class CodeRedemptionManager
{
	// Token: 0x0600022B RID: 555 RVA: 0x0000B034 File Offset: 0x00009234
	public static async void redeemNoProgress(string code)
	{
		await SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingTextAsync("hm");
		await Task.Yield();
		if (string.IsNullOrEmpty(code) || Uri.EscapeUriString(code) != code)
		{
			await Popup.ShowAsync("That code is invalid.", true, true, true, true, false);
		}
		else
		{
			code = code.ToUpper();
			if (code[0] != 'P')
			{
				await Popup.ShowAsync("That code is invalid.", true, true, true, true, false);
			}
			else
			{
				await SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingTextAsync("Redeeming code...");
				await Task.Yield();
				if (code[0] == 'P')
				{
					code = code.Substring(1);
					WebClient webClient = new WebClient();
					string text = DataManager.SavePath("Temp");
					Directory.CreateDirectory(text);
					string fileName = code + ".zip";
					string downloadPath = Path.Combine(text, fileName).Replace('\\', '/');
					if (File.Exists(downloadPath))
					{
						Debug.Log("Deleting existing file " + downloadPath + "...");
						File.Delete(downloadPath);
					}
					await SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingTextAsync("Downloading pet...");
					await Task.Yield();
					int num = 0;
					try
					{
						webClient.DownloadFile("http://s3.us-east-2.amazonaws.com/cavesofqud/pets/" + fileName, downloadPath);
					}
					catch (Exception obj)
					{
						num = 1;
					}
					if (num == 1)
					{
						object obj;
						await Popup.ShowAsync("Error downloading pet: " + ((Exception)obj).ToString(), true, true, true, true, true);
					}
					else
					{
						string modPath = DataManager.SavePath("Mods");
						Directory.CreateDirectory(modPath);
						await SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingTextAsync("Installing pet...");
						await Task.Yield();
						using (ZipFile zipFile = ZipFile.Read(downloadPath))
						{
							foreach (ZipEntry zipEntry in zipFile)
							{
								zipEntry.Extract(modPath, ExtractExistingFileAction.OverwriteSilently);
							}
						}
						await SingletonWindowBase<LoadingStatusWindow>.instance.SetLoadingTextAsync("Reloading configuration...");
						await Task.Yield();
						await Popup.ShowAsync("Your new pet is ready to love.", true, true, true, true, true);
						GameManager.Restart();
						webClient = null;
						fileName = null;
						downloadPath = null;
						modPath = null;
					}
				}
			}
		}
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000B06B File Offset: 0x0000926B
	public static void redeem(string code)
	{
		CodeRedemptionManager.<>c__DisplayClass1_0 CS$<>8__locals1 = new CodeRedemptionManager.<>c__DisplayClass1_0();
		CS$<>8__locals1.code = code;
		bool result = Popup.ShowProgressAsync<bool>(delegate(Progress p)
		{
			CodeRedemptionManager.<>c__DisplayClass1_0.<<redeem>b__0>d <<redeem>b__0>d;
			<<redeem>b__0>d.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<<redeem>b__0>d.<>4__this = CS$<>8__locals1;
			<<redeem>b__0>d.p = p;
			<<redeem>b__0>d.<>1__state = -1;
			<<redeem>b__0>d.<>t__builder.Start<CodeRedemptionManager.<>c__DisplayClass1_0.<<redeem>b__0>d>(ref <<redeem>b__0>d);
			return <<redeem>b__0>d.<>t__builder.Task;
		}).Result;
	}
}
