using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Qud.UI;
using QupKit;
using SFB;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.UI;

// Token: 0x020000A3 RID: 163
[UIView("SteamWorkshopUploaderView", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "SteamWorkshopUploader", UICanvasHost = 1)]
public class SteamWorkshopUploaderView : SingletonWindowBase<SteamWorkshopUploaderView>
{
	// Token: 0x06000561 RID: 1377 RVA: 0x00022B24 File Offset: 0x00020D24
	public static void OpenInWinFileBrowser(string path)
	{
		bool flag = false;
		string text = path.Replace("/", "\\");
		if (Directory.Exists(text))
		{
			flag = true;
		}
		try
		{
			Process.Start("explorer.exe", (flag ? "/root," : "/select,") + text);
		}
		catch (Win32Exception ex)
		{
			ex.HelpLink = "";
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000562 RID: 1378 RVA: 0x00022B90 File Offset: 0x00020D90
	private GameObject rootObject
	{
		get
		{
			return base.gameObject;
		}
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00022B98 File Offset: 0x00020D98
	public void ShowProgress(string Text)
	{
		this.rootObject.transform.Find("ProgressPanel").gameObject.SetActive(true);
		this.rootObject.transform.Find("ProgressPanel/ProgressLabel").GetComponent<global::UnityEngine.UI.Text>().text = Text;
		this.rootObject.transform.Find("ProgressPanel/Button").gameObject.SetActive(false);
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00022C08 File Offset: 0x00020E08
	public void SetProgress(string Text, float Progress)
	{
		this.rootObject.transform.Find("ProgressPanel/ProgressLabel").GetComponent<global::UnityEngine.UI.Text>().text = Text;
		if (Progress >= 100f)
		{
			this.rootObject.transform.Find("ProgressPanel/Button").gameObject.SetActive(true);
		}
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00022C5D File Offset: 0x00020E5D
	public void ClearProgress()
	{
		this.rootObject.transform.Find("ProgressPanel").gameObject.SetActive(false);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00022C80 File Offset: 0x00020E80
	public void SetModInfo(ModInfo info)
	{
		this.currentMod = info;
		if (info.WorkshopInfo == null)
		{
			this.rootObject.transform.Find("CreatePanel").gameObject.SetActive(true);
			this.rootObject.transform.Find("DetailsPanel").gameObject.SetActive(false);
		}
		else
		{
			this.rootObject.transform.Find("CreatePanel").gameObject.SetActive(false);
			this.rootObject.transform.Find("DetailsPanel").gameObject.SetActive(true);
			if (this.currentMod.WorkshopInfo.Title != null)
			{
				this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/TitleField/Field").GetComponent<TMP_InputField>().text = this.currentMod.WorkshopInfo.Title;
			}
			if (this.currentMod.WorkshopInfo.Description != null)
			{
				this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/DescriptionField/Field").GetComponent<TMP_InputField>().text = this.currentMod.WorkshopInfo.Description;
			}
			if (this.currentMod.WorkshopInfo.Tags != null)
			{
				this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/TagsField/Field").GetComponent<TMP_InputField>().text = string.Join(',', this.currentMod.WorkshopInfo.Tags);
			}
			if (!string.IsNullOrEmpty(this.currentMod.WorkshopInfo.Visibility))
			{
				this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/Visibility").GetComponent<Dropdown>().value = Convert.ToInt32(this.currentMod.WorkshopInfo.Visibility);
			}
		}
		this.rootObject.transform.Find("SelectedModLabel").GetComponent<UITextSkin>().SetText("Managing - " + info.ID);
		this.UpdatePreview();
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00022E6C File Offset: 0x0002106C
	public override void Show()
	{
		base.Show();
		EventSystem.current.SetSelectedGameObject(this.rootObject.transform.Find("Mod Scroller").gameObject);
		this.modScrollerController = GameObject.Find("ModScrollerController").GetComponent<ModScrollerController>();
		this.modScrollerController.Refresh();
		if (this.m_itemCreated == null)
		{
			this.m_itemCreated = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate(this.OnItemCreated));
			this.m_itemSubmitted = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate(this.OnItemSubmitted));
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00022EFC File Offset: 0x000210FC
	public void Popup(string text)
	{
		this.rootObject.transform.Find("PopupPanel").gameObject.SetActive(true);
		this.rootObject.transform.Find("PopupPanel/Panel/PopupLabel").GetComponent<global::UnityEngine.UI.Text>().text = text;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00022F4C File Offset: 0x0002114C
	private void OnItemSubmitted(SubmitItemUpdateResult_t callback, bool ioFailure)
	{
		if (ioFailure)
		{
			this.Popup("Error: I/O Failure! :(");
			return;
		}
		if (callback.m_eResult == EResult.k_EResultOK)
		{
			this.Popup("SUCCESS! Item submitted!");
			this.ClearProgress();
			return;
		}
		this.Popup("Unknown result: " + callback.m_eResult.ToString());
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00022FA8 File Offset: 0x000211A8
	private void OnItemCreated(CreateItemResult_t callback, bool ioFailure)
	{
		this.ClearProgress();
		if (ioFailure)
		{
			this.Popup("Error: I/O Failure!");
			return;
		}
		EResult eResult = callback.m_eResult;
		if (eResult == EResult.k_EResultTimeout)
		{
			this.SetProgress("Upload failed", 100f);
			this.Popup("Error: Timeout");
			return;
		}
		if (eResult == EResult.k_EResultNotLoggedOn)
		{
			this.SetProgress("Upload failed", 100f);
			this.Popup("Error: You're not logged into Steam!");
			return;
		}
		if (eResult == EResult.k_EResultInsufficientPrivilege)
		{
			this.SetProgress("Upload failed", 100f);
			this.Popup("Error: Unfortunately, you're banned by the community from uploading to the workshop! Bummer.");
			return;
		}
		if (callback.m_bUserNeedsToAcceptWorkshopLegalAgreement)
		{
			Application.OpenURL("https://steamcommunity.com/workshop/workshoplegalagreement/");
		}
		if (callback.m_eResult == EResult.k_EResultOK)
		{
			this.SetProgress("Upload complete!", 100f);
			this.Popup("Item creation successful! Published Item ID: " + callback.m_nPublishedFileId.ToString());
			global::UnityEngine.Debug.Log("Item created: Id: " + callback.m_nPublishedFileId.ToString());
			this.currentMod.InitializeWorkshopInfo(callback.m_nPublishedFileId.m_PublishedFileId);
			this.SetModInfo(this.currentMod);
			if (this._tempPath != null)
			{
				if (this._tempPath.StartsWith(Path.GetTempPath()) && this._tempPath != Path.GetTempPath())
				{
					Directory.Delete(this._tempPath, true);
				}
				this._tempPath = null;
			}
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00023108 File Offset: 0x00021308
	public void Update()
	{
		if (base.canvas.enabled && ControlManager.isCommandDown("Cancel", true, false, false))
		{
			this.OnCommand("Back");
		}
		if (this.UpdateAction != null)
		{
			this.UpdateAction();
			this.UpdateAction = null;
		}
		if (this.bFilter && !PickFileView.IsShowing())
		{
			this.bFilter = false;
			return;
		}
		if (this.currentHandle != UGCUpdateHandle_t.Invalid)
		{
			ulong num;
			ulong num2;
			EItemUpdateStatus itemUpdateProgress = SteamUGC.GetItemUpdateProgress(this.currentHandle, out num, out num2);
			float num3 = num / num2;
			switch (itemUpdateProgress)
			{
			case EItemUpdateStatus.k_EItemUpdateStatusInvalid:
				this.SetProgress("Item invalid ... dunno why! :(", 100f);
				return;
			case EItemUpdateStatus.k_EItemUpdateStatusPreparingConfig:
				this.SetProgress("Preparing configuration...", num3);
				return;
			case EItemUpdateStatus.k_EItemUpdateStatusPreparingContent:
				this.SetProgress("Preparing content...", num3);
				break;
			case EItemUpdateStatus.k_EItemUpdateStatusUploadingContent:
				this.SetProgress("Uploading content...", num3);
				return;
			case EItemUpdateStatus.k_EItemUpdateStatusUploadingPreviewFile:
				this.SetProgress("Uploading preview image...", num3);
				return;
			case EItemUpdateStatus.k_EItemUpdateStatusCommittingChanges:
				this.SetProgress("Committing changes...", num3);
				return;
			default:
				return;
			}
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0002320C File Offset: 0x0002140C
	public void SubmitCurrentMod()
	{
		try
		{
			this.ShowProgress("Submitting update... Please wait.");
			int value = this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/Visibility").GetComponent<Dropdown>().value;
			this.currentMod.WorkshopInfo.Title = this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/TitleField/Field").GetComponent<TMP_InputField>().text;
			this.currentMod.WorkshopInfo.Description = this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/DescriptionField/Field").GetComponent<TMP_InputField>().text;
			SteamWorkshopInfo workshopInfo = this.currentMod.WorkshopInfo;
			string text = this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/TagsField/Field").GetComponent<TMP_InputField>().text;
			workshopInfo.Tags = ((text != null) ? text.Split(',', StringSplitOptions.None) : null);
			this.currentMod.WorkshopInfo.Visibility = value.ToString();
			this.currentMod.SaveWorkshopInfo();
			ulong workshopId = this.currentMod.WorkshopInfo.WorkshopId;
			PublishedFileId_t publishedFileId_t = new PublishedFileId_t(workshopId);
			UGCUpdateHandle_t ugcupdateHandle_t = SteamUGC.StartItemUpdate(new AppId_t(333640U), publishedFileId_t);
			SteamUGC.SetItemTitle(ugcupdateHandle_t, this.currentMod.WorkshopInfo.Title);
			SteamUGC.SetItemDescription(ugcupdateHandle_t, this.currentMod.WorkshopInfo.Description);
			SteamUGC.RemoveItemKeyValueTags(ugcupdateHandle_t, "manifest_id");
			SteamUGC.AddItemKeyValueTag(ugcupdateHandle_t, "manifest_id", this.currentMod.ID);
			SteamUGC.RemoveItemKeyValueTags(ugcupdateHandle_t, "manifest_version");
			SteamUGC.AddItemKeyValueTag(ugcupdateHandle_t, "manifest_version", this.currentMod.Manifest.Version.ToString());
			SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate);
			if (value == 0)
			{
				SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPrivate);
			}
			if (value == 1)
			{
				SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityFriendsOnly);
			}
			if (value == 2)
			{
				SteamUGC.SetItemVisibility(ugcupdateHandle_t, ERemoteStoragePublishedFileVisibility.k_ERemoteStoragePublishedFileVisibilityPublic);
			}
			if (this.uploadHiddenFiles)
			{
				SteamUGC.SetItemContent(ugcupdateHandle_t, this.currentMod.Path);
			}
			else
			{
				this.ShowProgress("Copying files for deployment...");
				string path = this.currentMod.Path;
				string text2 = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
				Directory.CreateDirectory(text2);
				this._tempPath = text2;
				foreach (string text3 in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
				{
					if (!new FileInfo(text3).Attributes.HasFlag(FileAttributes.Hidden) && !new FileInfo(text3).Attributes.HasFlag(FileAttributes.ReparsePoint))
					{
						File.Copy(text3, text3.Replace(path, text2), true);
					}
				}
				List<string> list = new List<string>();
				string[] array = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
				for (int i = 0; i < array.Length; i++)
				{
					string dirPath = array[i];
					if (!new DirectoryInfo(dirPath).Attributes.HasFlag(FileAttributes.Hidden) && !new DirectoryInfo(dirPath).Attributes.HasFlag(FileAttributes.ReparsePoint) && !list.Any<string>((string hd) => dirPath.StartsWith(hd)))
					{
						Directory.CreateDirectory(dirPath.Replace(path, text2));
						foreach (string text4 in Directory.GetFiles(dirPath, "*.*", SearchOption.TopDirectoryOnly))
						{
							if (!new FileInfo(text4).Attributes.HasFlag(FileAttributes.Hidden) && !new FileInfo(text4).Attributes.HasFlag(FileAttributes.ReparsePoint))
							{
								File.Copy(text4, text4.Replace(path, text2), true);
							}
						}
					}
					else
					{
						list.Add(dirPath);
					}
				}
				SteamUGC.SetItemContent(ugcupdateHandle_t, text2);
			}
			if (!string.IsNullOrEmpty(this.currentMod.WorkshopInfo.ImagePath))
			{
				SteamUGC.SetItemPreview(ugcupdateHandle_t, Path.Combine(this.currentMod.Path, this.currentMod.WorkshopInfo.ImagePath));
			}
			SteamUGC.SetItemTags(ugcupdateHandle_t, this.currentMod.WorkshopInfo.Tags, false);
			this.currentHandle = ugcupdateHandle_t;
			SteamAPICall_t steamAPICall_t = SteamUGC.SubmitItemUpdate(ugcupdateHandle_t, this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/ChangelistPanel/ChangelistField/Field").GetComponent<TMP_InputField>().text);
			this.m_itemSubmitted.Set(steamAPICall_t, null);
		}
		catch (Exception ex)
		{
			this.ClearProgress();
			this.Popup(ex.ToString());
		}
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000236D4 File Offset: 0x000218D4
	public void UpdatePreview()
	{
		if (this.currentMod.WorkshopInfo != null && !string.IsNullOrEmpty(this.currentMod.WorkshopInfo.ImagePath))
		{
			byte[] array = File.ReadAllBytes(Path.Combine(this.currentMod.Path, "preview.png"));
			Texture2D texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(array);
			this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/WorkshopImage").GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), new Vector2(0.5f, 0.5f));
			return;
		}
		this.rootObject.transform.Find("DetailsPanel/Scroll View/Viewport/Content/WorkshopImage").GetComponent<Image>().sprite = null;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x000237A8 File Offset: 0x000219A8
	public void SetImage(string Path)
	{
		try
		{
			if (Path != null)
			{
				File.Copy(Path, Path.Combine(this.currentMod.Path, "preview.png"), true);
				this.currentMod.WorkshopInfo.ImagePath = "preview.png";
			}
			this.UpdatePreview();
			this.currentMod.SaveWorkshopInfo();
		}
		catch (Exception ex)
		{
			this.Popup(ex.ToString());
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600056F RID: 1391 RVA: 0x0002381C File Offset: 0x00021A1C
	// (set) Token: 0x06000570 RID: 1392 RVA: 0x00023829 File Offset: 0x00021A29
	public bool uploadHiddenFiles
	{
		get
		{
			return this.uploadHiddenFilesToggle.isOn;
		}
		set
		{
			this.uploadHiddenFilesToggle.isOn = value;
		}
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00023838 File Offset: 0x00021A38
	public void OnCommand(string Command)
	{
		if (PickFileView.IsShowing())
		{
			this.bFilter = true;
			return;
		}
		if (Command == "SelectImage")
		{
			if (this.currentMod != null && this.currentMod.WorkshopInfo != null)
			{
				StandaloneFileBrowser.OpenFilePanelAsync("Select Workshop Image", "", "png", false, delegate(string[] s)
				{
					this.rootObject.transform.Find("BrowsePanel").gameObject.SetActive(false);
					if (s.Length != 0)
					{
						this.SetImage(s[0]);
						return;
					}
					this.SetImage(null);
				});
			}
			return;
		}
		if (Command == "UploadContent")
		{
			if (this.currentMod != null && this.currentMod.WorkshopInfo != null)
			{
				this.SubmitCurrentMod();
			}
			return;
		}
		if (Command == "CreateWorkshopItem")
		{
			if (this.currentMod != null && this.currentMod.WorkshopInfo == null)
			{
				try
				{
					this.ShowProgress("Requesting a new Steam Workshop item... Please wait.");
					SteamAPICall_t steamAPICall_t = SteamUGC.CreateItem(new AppId_t(333640U), EWorkshopFileType.k_EWorkshopFileTypeFirst);
					this.m_itemCreated.Set(steamAPICall_t, null);
				}
				catch
				{
					this.ClearProgress();
				}
			}
			return;
		}
		if (Command == "Back")
		{
			UIManager.getWindow("SteamWorkshopUploader").Hide();
			UIManager.showWindow("MainMenu", false);
		}
		if (Command == "RefreshMods")
		{
			this.modScrollerController.Refresh();
		}
		if (Command == "BrowseMods")
		{
			if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Mods")))
			{
				SteamWorkshopUploaderView.OpenInWinFileBrowser(Path.Combine(Application.persistentDataPath, "Mods"));
				return;
			}
			SteamWorkshopUploaderView.OpenInWinFileBrowser(Application.persistentDataPath);
		}
	}

	// Token: 0x0400042B RID: 1067
	private ModScrollerController modScrollerController;

	// Token: 0x0400042C RID: 1068
	private ModInfo currentMod;

	// Token: 0x0400042D RID: 1069
	private Action UpdateAction;

	// Token: 0x0400042E RID: 1070
	protected CallResult<CreateItemResult_t> m_itemCreated;

	// Token: 0x0400042F RID: 1071
	protected CallResult<SubmitItemUpdateResult_t> m_itemSubmitted;

	// Token: 0x04000430 RID: 1072
	private UGCUpdateHandle_t currentHandle = UGCUpdateHandle_t.Invalid;

	// Token: 0x04000431 RID: 1073
	private string _tempPath;

	// Token: 0x04000432 RID: 1074
	public Toggle uploadHiddenFilesToggle;

	// Token: 0x04000433 RID: 1075
	public bool bFilter;
}
