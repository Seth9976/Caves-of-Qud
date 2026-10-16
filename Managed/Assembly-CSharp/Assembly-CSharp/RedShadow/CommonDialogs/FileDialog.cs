using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using XRL;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001F9 RID: 505
	public class FileDialog : DialogBase
	{
		// Token: 0x06001220 RID: 4640 RVA: 0x0006B794 File Offset: 0x00069994
		protected override void Awake()
		{
			base.Awake();
			this._titleText = base.transform.Find("Window/TitleBar/TitleText").gameObject.GetComponent<Text>();
			this._pathScrollRect = base.transform.Find("Window/PathPanelScroll").GetComponent<ScrollRect>();
			this._pathPanel = base.transform.Find("Window/PathPanelScroll/Viewport/PathPanel").gameObject;
			this._placesScrollRect = base.transform.Find("Window/CentralPanel/PlacesPanelScroll").GetComponent<ScrollRect>();
			this._placesPanel = base.transform.Find("Window/CentralPanel/PlacesPanelScroll/Viewport/PlacesPanel").gameObject;
			this._fileListContent = base.transform.Find("Window/CentralPanel/FileListScrollView/Viewport/FileListContent").gameObject;
			this._fileListScrollRect = base.transform.Find("Window/CentralPanel/FileListScrollView").GetComponent<ScrollRect>();
			this._previewPanel = base.transform.Find("Window/CentralPanel/PreviewPanel").gameObject;
			this._filenameInput = base.transform.Find("Window/FilenamePanel/FilenameInput/").gameObject.GetComponent<InputField>();
			this._actionButton = base.transform.Find("Window/FilenamePanel/ActionButton/").gameObject.GetComponent<Button>();
			this._actionText = base.transform.Find("Window/FilenamePanel/ActionButton/Text").gameObject.GetComponent<Text>();
			this._previewImage = base.transform.Find("Window/CentralPanel/PreviewPanel/PreviewParentPanel/RawImage").gameObject.GetComponent<RawImage>();
			this._modifiedDateText = base.transform.Find("Window/CentralPanel/PreviewPanel/DateText").gameObject.GetComponent<Text>();
			this._fileSizeText = base.transform.Find("Window/CentralPanel/PreviewPanel/SizeText").gameObject.GetComponent<Text>();
			this._dragPanel = base.transform.Find("Window/TitleBar").GetComponent<DragPanel>();
			this._resizePanel = base.transform.Find("Window/ResizeZone").GetComponent<ResizePanel>();
			this._extensionsDropDown = base.transform.Find("Window/FilenamePanel/FilenameInput/ExtensionDropDown").GetComponent<Dropdown>();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0006B998 File Offset: 0x00069B98
		public override void Update()
		{
			base.Update();
			if (base.isTop())
			{
				string text = Path.Combine(this._path, this._filenameInput.text);
				switch (this._mode)
				{
				case FileDialogMode.Save:
					this._actionButton.interactable = !string.IsNullOrEmpty(this._filenameInput.text);
					break;
				case FileDialogMode.Load:
					this._actionButton.interactable = File.Exists(text);
					break;
				case FileDialogMode.Path:
					this._actionButton.interactable = string.IsNullOrEmpty(this._filenameInput.text) || Directory.Exists(text);
					break;
				}
				if (this._selectedItem != null && Input.GetKeyDown(KeyCode.Return))
				{
					this.onDoubleClickFile(this._selectedItem);
				}
				if (Input.GetKeyDown(KeyCode.DownArrow))
				{
					int num = this._fileListItems.IndexOf(this._selectedItem);
					if (this._selectedItem == null || num == -1)
					{
						this.onSingleClickFile(this._fileListItems[0]);
					}
					else if (num < this._fileListItems.Count - 1)
					{
						this.onSingleClickFile(this._fileListItems[num + 1]);
					}
					DialogUtility.centerOnItem(this._fileListContent, this._selectedItem.GetComponent<RectTransform>());
				}
				if (Input.GetKeyDown(KeyCode.UpArrow))
				{
					int num2 = this._fileListItems.IndexOf(this._selectedItem);
					if (this._selectedItem == null || num2 == -1)
					{
						this.onSingleClickFile(this._fileListItems[0]);
					}
					else if (num2 > 0)
					{
						this.onSingleClickFile(this._fileListItems[num2 - 1]);
					}
					DialogUtility.centerOnItem(this._fileListContent, this._selectedItem.GetComponent<RectTransform>());
				}
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0006BB54 File Offset: 0x00069D54
		protected override void hide()
		{
			Vector2 position = this._dragPanel.getPosition();
			Vector2 size = this._resizePanel.getSize();
			PlayerPrefs.SetFloat("FileDialog_X", position.x);
			PlayerPrefs.SetFloat("FileDialog_Y", position.y);
			PlayerPrefs.SetFloat("FileDialog_W", size.x);
			PlayerPrefs.SetFloat("FileDialog_H", size.y);
			PlayerPrefs.Save();
			base.hide();
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0006BBC4 File Offset: 0x00069DC4
		protected override void show()
		{
			base.show();
			float @float = PlayerPrefs.GetFloat("FileDialog_X", -400f);
			float float2 = PlayerPrefs.GetFloat("FileDialog_Y", 275f);
			this._dragPanel.setPosition(new Vector2(@float, float2));
			float float3 = PlayerPrefs.GetFloat("FileDialog_W", 680f);
			float float4 = PlayerPrefs.GetFloat("FileDialog_H", 400f);
			this._resizePanel.setSize(new Vector2(float3, float4));
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0006BC3B File Offset: 0x00069E3B
		public override void cancel()
		{
			this.hide();
			this._callback(null);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0006BC50 File Offset: 0x00069E50
		public void show(FileDialogMode mode, string title, string extensions, Action<string> callback, string defaultFilename = null, string defaultPath = null)
		{
			if (mode == FileDialogMode.Load)
			{
				StandaloneFileBrowser.OpenFilePanelAsync(title, (defaultPath == null) ? Directory.GetCurrentDirectory() : defaultPath, extensions.Replace("*.", ""), false, delegate(string[] s)
				{
					if (s.Length != 0)
					{
						callback(s[0]);
						return;
					}
					callback(null);
				});
				return;
			}
			if (mode == FileDialogMode.Save)
			{
				StandaloneFileBrowser.SaveFilePanelAsync(title, (defaultPath == null) ? Directory.GetCurrentDirectory() : defaultPath, defaultFilename, extensions.Replace("*.", ""), delegate(string s)
				{
					callback(s);
				});
				return;
			}
			if (mode == FileDialogMode.Path)
			{
				StandaloneFileBrowser.OpenFolderPanelAsync(title, (defaultPath == null) ? Directory.GetCurrentDirectory() : defaultPath, false, delegate(string[] s)
				{
					if (s.Length != 0)
					{
						callback(s[0]);
						return;
					}
					callback(null);
				});
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0006BCFC File Offset: 0x00069EFC
		public static bool saveThumbnail(string filepath, Texture2D image)
		{
			bool flag;
			try
			{
				string text = Path.Combine(Path.GetDirectoryName(filepath), "thumbs");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
					File.SetAttributes(text, File.GetAttributes(text) | FileAttributes.Hidden);
				}
				string fileName = Path.GetFileName(filepath);
				File.WriteAllBytes(Path.Combine(text, fileName + ".png"), image.EncodeToPNG());
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0006BD74 File Offset: 0x00069F74
		public void onModsFolder()
		{
			this.setPath(DataManager.SavePath("Mods"));
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0006BD88 File Offset: 0x00069F88
		public void onAction()
		{
			string text = Path.Combine(this._path, this._filenameInput.text);
			if (this._mode == FileDialogMode.Save && File.Exists(text) && this.OverwriteWarn)
			{
				DialogManager.confirm("Overwrite file?", new Action<Buttons>(this.onOverwriteConfirm));
				return;
			}
			this.hide();
			this._callback(text);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0006BDF0 File Offset: 0x00069FF0
		public void setPath(string path)
		{
			if (path == "%%ROOT%%")
			{
				this._path = "%%ROOT%%";
			}
			else if (path == "%%PERSISTENT_DATA_PATH%%")
			{
				this._path = Application.persistentDataPath.Replace('/', Path.DirectorySeparatorChar);
			}
			else
			{
				this._path = Path.GetFullPath(path);
			}
			this.LastDirectory = path;
			PlayerPrefs.SetString("FileDialog_LastDirectory", this.LastDirectory);
			this._paths.Clear();
			foreach (object obj in this._pathPanel.transform)
			{
				global::UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
			if (this._path == "%%ROOT%%")
			{
				this.addPathButton("Drives", "%%ROOT%%", false);
			}
			else
			{
				string[] array = this._path.Split(new char[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
				string text = "";
				for (int i = 0; i < array.Length; i++)
				{
					string text2 = array[i];
					text = text + text2 + Path.DirectorySeparatorChar.ToString();
					text2 += Path.DirectorySeparatorChar.ToString();
					this.addPathButton(text2, text, i < array.Length - 1);
				}
			}
			base.StartCoroutine(this.repositionPathScroll_co());
			this.updateFileList();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0006BF70 File Offset: 0x0006A170
		public void updatePlaces()
		{
			foreach (object obj in this._placesPanel.transform)
			{
				global::UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
			foreach (Place place in this.Places)
			{
				this.addPlaceButton(place);
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0006C014 File Offset: 0x0006A214
		public void updateFileList()
		{
			this._previewImage.texture = null;
			this._selectedItem = null;
			this._fileListItems.Clear();
			foreach (object obj in this._fileListContent.transform)
			{
				global::UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
			if (this._path == "%%ROOT%%")
			{
				string[] logicalDrives = Environment.GetLogicalDrives();
				Array.Sort<string>(logicalDrives);
				foreach (string text in logicalDrives)
				{
					this.addFileButton(this.DriveIcon, text);
				}
			}
			else
			{
				if (this.ShowFolders || this._mode == FileDialogMode.Path)
				{
					string[] array2;
					try
					{
						string[] directories = Directory.GetDirectories(this._path);
						List<string> list = new List<string>();
						foreach (string text2 in directories)
						{
							bool flag = (File.GetAttributes(text2) & FileAttributes.Hidden) == FileAttributes.Hidden;
							if (this.ShowHidden || !flag)
							{
								list.Add(text2);
							}
						}
						array2 = list.ToArray();
					}
					catch (IOException)
					{
						Debug.LogWarning("Unable to access drive " + this._path);
						return;
					}
					catch (UnauthorizedAccessException)
					{
						Debug.LogWarning("Access denied to directory " + this._path);
						return;
					}
					if (this._paths.Count > 1)
					{
						this.addFileButton(this.ReturnIcon, "..");
					}
					Array.Sort<string>(array2);
					foreach (string text3 in array2)
					{
						this.addFileButton(this.FolderIcon, text3);
					}
				}
				if (this._mode != FileDialogMode.Path)
				{
					List<string> list2 = new List<string>();
					string text4 = this._extensionsDropDown.options[this._extensionsDropDown.value].text;
					if (text4 == "Known")
					{
						foreach (string text5 in this.Extensions.Split(';', StringSplitOptions.None))
						{
							list2.AddRange(Directory.GetFiles(this._path, text5));
						}
					}
					else if (text4 == "All")
					{
						list2.AddRange(Directory.GetFiles(this._path));
					}
					else
					{
						list2.AddRange(Directory.GetFiles(this._path, text4));
					}
					string[] array3 = list2.ToArray();
					for (int j = 0; j < array3.Length; j++)
					{
						array3[j] = Path.GetFileName(list2[j]);
					}
					Array.Sort<string>(array3);
					foreach (string text6 in array3)
					{
						bool flag2 = (File.GetAttributes(Path.Combine(this._path, text6)) & FileAttributes.Hidden) == FileAttributes.Hidden;
						if (this.ShowHidden || !flag2)
						{
							this.addFileButton(this.getSprite(text6), text6);
						}
					}
				}
			}
			base.StartCoroutine(this.repositionFileListScroll_co());
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0006C31C File Offset: 0x0006A51C
		public Sprite getSprite(string filename)
		{
			string text = Path.GetExtension(filename).ToLower();
			foreach (FileIcon fileIcon in this.Icons)
			{
				string[] array = fileIcon.Extension.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == text)
					{
						return fileIcon.Sprite;
					}
				}
			}
			return this.UnknownIcon;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0006C3C4 File Offset: 0x0006A5C4
		private void addPlaceButton(Place place)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.PlacePrefab);
			gameObject.transform.SetParent(this._placesPanel.transform, false);
			gameObject.transform.Find("Button/Image").GetComponent<Image>().sprite = place.Sprite;
			gameObject.transform.Find("Button/Text").GetComponent<Text>().text = place.Name;
			gameObject.transform.Find("Button").GetComponent<Button>().onClick.AddListener(delegate
			{
				this.onPlaceClick(place);
			});
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0006C47C File Offset: 0x0006A67C
		private void addPathButton(string text, string path, bool enabled)
		{
			this._paths.Add(path);
			PathButton pathButton = global::UnityEngine.Object.Instantiate<PathButton>(this.PathButtonPrefab);
			pathButton.transform.SetParent(this._pathPanel.transform, false);
			pathButton.Path = path;
			pathButton.transform.Find("Text").GetComponent<Text>().text = text;
			pathButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				this.setPath(path);
			});
			pathButton.GetComponent<Button>().interactable = enabled;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0006C51E File Offset: 0x0006A71E
		private IEnumerator repositionPathScroll_co()
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(this._pathPanel.GetComponent<RectTransform>());
			yield return new WaitForSeconds(0.05f);
			this._pathScrollRect.horizontalNormalizedPosition = 1f;
			yield break;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0006C530 File Offset: 0x0006A730
		private void addFileButton(Sprite icon, string text)
		{
			if (text.StartsWith(this._path))
			{
				text = text.Substring(this._path.Length);
			}
			text = text.TrimStart(Path.DirectorySeparatorChar);
			text = text.TrimEnd(Path.DirectorySeparatorChar);
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.ListItemPrefab);
			gameObject.transform.SetParent(this._fileListContent.transform, false);
			gameObject.transform.Find("Image").GetComponent<Image>().sprite = icon;
			if (this.ShowExtensions)
			{
				gameObject.transform.Find("Text").GetComponent<Text>().text = text;
			}
			else
			{
				gameObject.transform.Find("Text").GetComponent<Text>().text = Path.GetFileNameWithoutExtension(text);
			}
			FileListItem fileListItem = gameObject.GetComponent<FileListItem>();
			fileListItem.Filename = text;
			fileListItem.onDoubleClick.AddListener(delegate
			{
				this.onDoubleClickFile(fileListItem);
			});
			gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				this.onSingleClickFile(fileListItem);
			});
			if (this.ShowContextMenu && text != "..")
			{
				fileListItem.onRightClick.AddListener(delegate
				{
					this.onRightClickFile(fileListItem);
				});
			}
			this._fileListItems.Add(fileListItem);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0006C69A File Offset: 0x0006A89A
		private IEnumerator repositionFileListScroll_co()
		{
			yield return null;
			LayoutRebuilder.MarkLayoutForRebuild(this._fileListContent.GetComponent<RectTransform>());
			yield return new WaitForSeconds(0.05f);
			this._fileListScrollRect.normalizedPosition = new Vector2(0f, 1f);
			yield break;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0006C6A9 File Offset: 0x0006A8A9
		private void onPlaceClick(Place place)
		{
			if (string.IsNullOrEmpty(place.Path))
			{
				this.setPath(Environment.GetFolderPath(place.SpecialFolder));
				return;
			}
			this.setPath(place.Path);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0006C6D8 File Offset: 0x0006A8D8
		private void onSingleClickFile(FileListItem item)
		{
			this._selectedItem = item;
			item.setHighlighted(true);
			string fullFileName = this.getFullFileName(item);
			FileInfo fileInfo = new FileInfo(fullFileName);
			this._modifiedDateText.text = "";
			this._fileSizeText.text = "";
			this._previewImage.texture = null;
			this._previewImage.color = new Color(0f, 0f, 0f, 0f);
			if (!fileInfo.Exists)
			{
				return;
			}
			this._modifiedDateText.text = fileInfo.LastWriteTime.ToShortDateString() + " " + fileInfo.LastWriteTime.ToShortTimeString();
			if (Directory.Exists(fullFileName))
			{
				if (this._mode == FileDialogMode.Path)
				{
					this._filenameInput.text = item.Filename;
					return;
				}
			}
			else
			{
				this._fileSizeText.text = DialogUtility.prettyPrintSize(fileInfo.Length);
				this._filenameInput.text = item.Filename;
				if (this.ShowPreview)
				{
					base.StartCoroutine(this.loadPreview_co(fullFileName));
				}
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0006C7EC File Offset: 0x0006A9EC
		private void onDoubleClickFile(FileListItem item)
		{
			string fullFileName = this.getFullFileName(item);
			FileInfo fileInfo = new FileInfo(fullFileName);
			if (Directory.Exists(fullFileName))
			{
				this.setPath(fullFileName);
				if (this._mode == FileDialogMode.Path)
				{
					this._filenameInput.text = "";
					return;
				}
			}
			else if (fileInfo.Exists)
			{
				this.onAction();
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0006C840 File Offset: 0x0006AA40
		private void onRightClickFile(FileListItem item)
		{
			if (this._path == "%%ROOT%%")
			{
				return;
			}
			Menu menu = DialogManager.createMenu();
			menu.transform.SetParent(base.transform, false);
			menu.onClick.AddListener(new UnityAction<string>(this.onMenuItemClick));
			menu.addItem(this.NewFolderIcon, "New Folder");
			if (item != null)
			{
				this._selectedItem = item;
				item.setHighlighted(true);
				menu.addSeparator();
				menu.addItem(this.RenameIcon, "Rename");
				if (!Directory.Exists(this.getFullFileName(item)))
				{
					menu.addSeparator();
					menu.addItem(this.CopyIcon, "Copy");
					menu.addItem(this.PasteIcon, "Paste");
				}
				menu.addSeparator();
				menu.addItem(this.DeleteIcon, "Delete");
			}
			menu.show(Input.mousePosition);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0006C930 File Offset: 0x0006AB30
		private void onMenuItemClick(string text)
		{
			if (text == "New Folder")
			{
				DialogManager.getString("New Folder Name:", "", new Action<string>(this.onNewFolder), InputType.String);
				return;
			}
			if (text == "Rename")
			{
				DialogManager.getString(Directory.Exists(this.getFullFileName(this._selectedItem)) ? "New Folder Name:" : "New File Name:", this._selectedItem.Filename, new Action<string>(this.onRenameFile), InputType.String);
				return;
			}
			if (text == "Copy")
			{
				this._clipboardPath = this.getFullFileName(this._selectedItem);
				return;
			}
			if (text == "Paste" && !string.IsNullOrEmpty(this._clipboardPath))
			{
				string text2 = Path.Combine(this._path, Path.GetFileName(this._clipboardPath));
				int num = 0;
				while (File.Exists(text2))
				{
					num++;
					string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this._clipboardPath);
					string extension = Path.GetExtension(this._clipboardPath);
					string text3 = string.Format("{0} Copy({1}){2}", fileNameWithoutExtension, num, extension);
					text2 = Path.Combine(this._path, text3);
				}
				File.Copy(this._clipboardPath, text2);
				this.updateFileList();
				return;
			}
			if (text == "Delete")
			{
				DialogManager.confirm(Directory.Exists(this.getFullFileName(this._selectedItem)) ? "Delete Folder?" : "Delete file?", new Action<Buttons>(this.onConfirmDelete));
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0006CA9F File Offset: 0x0006AC9F
		public void onNewFolder(string name)
		{
			Directory.CreateDirectory(Path.Combine(this._path, name));
			this.updateFileList();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0006CABC File Offset: 0x0006ACBC
		public void onRenameFile(string name)
		{
			string fullFileName = this.getFullFileName(this._selectedItem);
			string text = Path.Combine(this._path, name);
			if (Directory.Exists(fullFileName))
			{
				Directory.Move(fullFileName, text);
			}
			else
			{
				File.Move(fullFileName, text);
			}
			this.updateFileList();
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0006CB01 File Offset: 0x0006AD01
		public void onFileListClick(BaseEventData eventData)
		{
			if (((PointerEventData)eventData).button == PointerEventData.InputButton.Right && this.ShowContextMenu)
			{
				this.onRightClickFile(null);
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0006CB20 File Offset: 0x0006AD20
		private void onConfirmDelete(Buttons result)
		{
			if (result == Buttons.Yes)
			{
				string fullFileName = this.getFullFileName(this._selectedItem);
				if (Directory.Exists(fullFileName))
				{
					Directory.Delete(fullFileName);
				}
				else
				{
					File.Delete(fullFileName);
				}
				this.updateFileList();
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0006CB5A File Offset: 0x0006AD5A
		private IEnumerator loadPreview_co(string filepath)
		{
			string text = Path.GetExtension(filepath).ToLower();
			bool flag = text == ".jpg" || text == ".jpeg" || text == ".png";
			this._previewImage.texture = null;
			string text2;
			if (flag)
			{
				text2 = filepath;
			}
			else
			{
				string text3 = Path.Combine(Path.GetDirectoryName(filepath), "thumbs");
				string fileName = Path.GetFileName(filepath);
				text2 = Path.Combine(text3, fileName + ".png");
			}
			if (File.Exists(text2))
			{
				using (UnityWebRequest texture = UnityWebRequestTexture.GetTexture("file://" + text2))
				{
					texture.SendWebRequest();
					this._previewImage.texture = DownloadHandlerTexture.GetContent(texture);
					this._previewImage.color = Color.white;
					DialogUtility.sizeToParent(this._previewImage, 2f);
					yield break;
				}
			}
			yield break;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0006CB70 File Offset: 0x0006AD70
		private void onOverwriteConfirm(Buttons result)
		{
			if (result == Buttons.Yes)
			{
				this.hide();
				this._callback(Path.Combine(this._path, this._filenameInput.text));
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
		private string getFullFileName(FileListItem item)
		{
			string text;
			if (this._path == "%%ROOT%%")
			{
				text = item.Filename + Path.DirectorySeparatorChar.ToString();
			}
			else
			{
				text = Path.Combine(this._path, item.Filename);
			}
			return text;
		}

		// Token: 0x04000BFB RID: 3067
		public string Extensions;

		// Token: 0x04000BFC RID: 3068
		public string DefaultFilename;

		// Token: 0x04000BFD RID: 3069
		public string DefaultDirectory;

		// Token: 0x04000BFE RID: 3070
		public string LastDirectory;

		// Token: 0x04000BFF RID: 3071
		public bool ShowPath = true;

		// Token: 0x04000C00 RID: 3072
		public bool ShowPlaces = true;

		// Token: 0x04000C01 RID: 3073
		public bool ShowPreview = true;

		// Token: 0x04000C02 RID: 3074
		public bool ShowFolders = true;

		// Token: 0x04000C03 RID: 3075
		public bool ShowHidden;

		// Token: 0x04000C04 RID: 3076
		public bool ShowInputField = true;

		// Token: 0x04000C05 RID: 3077
		public bool ShowExtensions = true;

		// Token: 0x04000C06 RID: 3078
		public bool ShowContextMenu = true;

		// Token: 0x04000C07 RID: 3079
		public bool OverwriteWarn = true;

		// Token: 0x04000C08 RID: 3080
		public GameObject ListItemPrefab;

		// Token: 0x04000C09 RID: 3081
		public PathButton PathButtonPrefab;

		// Token: 0x04000C0A RID: 3082
		public GameObject PlacePrefab;

		// Token: 0x04000C0B RID: 3083
		public Sprite UnknownIcon;

		// Token: 0x04000C0C RID: 3084
		public Sprite DriveIcon;

		// Token: 0x04000C0D RID: 3085
		public Sprite FolderIcon;

		// Token: 0x04000C0E RID: 3086
		public Sprite ReturnIcon;

		// Token: 0x04000C0F RID: 3087
		public Sprite NewFolderIcon;

		// Token: 0x04000C10 RID: 3088
		public Sprite RenameIcon;

		// Token: 0x04000C11 RID: 3089
		public Sprite CopyIcon;

		// Token: 0x04000C12 RID: 3090
		public Sprite PasteIcon;

		// Token: 0x04000C13 RID: 3091
		public Sprite DeleteIcon;

		// Token: 0x04000C14 RID: 3092
		public List<FileIcon> Icons = new List<FileIcon>();

		// Token: 0x04000C15 RID: 3093
		public List<Place> Places = new List<Place>();

		// Token: 0x04000C16 RID: 3094
		private Text _titleText;

		// Token: 0x04000C17 RID: 3095
		private ScrollRect _pathScrollRect;

		// Token: 0x04000C18 RID: 3096
		private GameObject _pathPanel;

		// Token: 0x04000C19 RID: 3097
		private ScrollRect _placesScrollRect;

		// Token: 0x04000C1A RID: 3098
		private GameObject _placesPanel;

		// Token: 0x04000C1B RID: 3099
		private ScrollRect _fileListScrollRect;

		// Token: 0x04000C1C RID: 3100
		private GameObject _fileListContent;

		// Token: 0x04000C1D RID: 3101
		private GameObject _previewPanel;

		// Token: 0x04000C1E RID: 3102
		private InputField _filenameInput;

		// Token: 0x04000C1F RID: 3103
		private Button _actionButton;

		// Token: 0x04000C20 RID: 3104
		private Text _actionText;

		// Token: 0x04000C21 RID: 3105
		private RawImage _previewImage;

		// Token: 0x04000C22 RID: 3106
		private Text _modifiedDateText;

		// Token: 0x04000C23 RID: 3107
		private Text _fileSizeText;

		// Token: 0x04000C24 RID: 3108
		private DragPanel _dragPanel;

		// Token: 0x04000C25 RID: 3109
		private ResizePanel _resizePanel;

		// Token: 0x04000C26 RID: 3110
		private Dropdown _extensionsDropDown;

		// Token: 0x04000C27 RID: 3111
		private Action<string> _callback;

		// Token: 0x04000C28 RID: 3112
		private FileDialogMode _mode;

		// Token: 0x04000C29 RID: 3113
		private string _path;

		// Token: 0x04000C2A RID: 3114
		private readonly List<string> _paths = new List<string>();

		// Token: 0x04000C2B RID: 3115
		private readonly List<FileListItem> _fileListItems = new List<FileListItem>();

		// Token: 0x04000C2C RID: 3116
		private FileListItem _selectedItem;

		// Token: 0x04000C2D RID: 3117
		private string _clipboardPath;

		// Token: 0x04000C2E RID: 3118
		private const string ROOT_PATH = "%%ROOT%%";

		// Token: 0x04000C2F RID: 3119
		private const string PERSISTENT_DATA_PATH = "%%PERSISTENT_DATA_PATH%%";

		// Token: 0x04000C30 RID: 3120
		private const string THUMBS_PATH = "thumbs";
	}
}
