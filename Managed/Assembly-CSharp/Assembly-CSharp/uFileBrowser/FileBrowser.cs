using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace uFileBrowser
{
	// Token: 0x02000186 RID: 390
	public class FileBrowser : MonoBehaviour
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0005EE76 File Offset: 0x0005D076
		public string SelectedPath
		{
			get
			{
				if (this.selected > -1)
				{
					return this.fileButtons[this.selected].fullPath;
				}
				return null;
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0005EE9C File Offset: 0x0005D09C
		private void Awake()
		{
			this.slash = Path.DirectorySeparatorChar.ToString();
			if (Application.platform == RuntimePlatform.Android)
			{
				if (string.IsNullOrEmpty(this.defaultPath))
				{
					this.defaultPath = "/storage";
					return;
				}
			}
			else
			{
				if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
				{
					this.drives = new List<string>(Directory.GetLogicalDrives());
					return;
				}
				if ((Application.platform == RuntimePlatform.LinuxPlayer || Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer) && string.IsNullOrEmpty(this.defaultPath))
				{
					this.defaultPath = "/home/";
				}
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0005EF2F File Offset: 0x0005D12F
		public void Show(FileBrowserCallback callback)
		{
			this.GotoDirectory(this.defaultPath);
			this.UpdateUI();
			this.callback = callback;
			if (this.overlay)
			{
				this.overlay.SetActive(true);
			}
			this.window.SetActive(true);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0005EF70 File Offset: 0x0005D170
		public void Hide()
		{
			if (this.selected > -1)
			{
				this.fileButtons[this.selected].Unselect();
			}
			this.currentPath = "";
			this.selected = -1;
			this.search = "";
			if (this.overlay)
			{
				this.overlay.SetActive(false);
			}
			this.window.SetActive(false);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0005EFE0 File Offset: 0x0005D1E0
		public void UpdateUI()
		{
			if (this.cancelButton)
			{
				this.cancelButton.gameObject.SetActive(this.canCancel);
			}
			this.currentPathField.text = this.currentPath;
			this.searchField.text = this.search;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0005F034 File Offset: 0x0005D234
		public void OnFileClick(int i)
		{
			if (i >= this.fileButtons.Count)
			{
				Debug.LogError("uFileBrowser: Button index is bigger than array, something went wrong.");
				return;
			}
			if (!this.fileButtons[i].isDir)
			{
				this.SelectFile(i);
				return;
			}
			if (!this.selectDirectory)
			{
				this.GotoDirectory(this.fileButtons[i].fullPath);
				return;
			}
			this.SelectFile(i);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0005F09C File Offset: 0x0005D29C
		public void OnDirectoryClick(int i)
		{
			if (i >= this.dirButtons.Count)
			{
				Debug.LogError("uFileBrowser: Button index is bigger than array, something went wrong.");
				return;
			}
			this.GotoDirectory(this.dirButtons[i].fullPath);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0005F0D0 File Offset: 0x0005D2D0
		private void GotoDirectory(string path)
		{
			if (path == this.currentPath && path != string.Empty)
			{
				return;
			}
			if (string.IsNullOrEmpty(path))
			{
				if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
				{
					this.currentPath = "";
				}
				else if (Application.platform == RuntimePlatform.Android)
				{
					this.currentPath = "/storage";
				}
				else
				{
					this.currentPath = "/home/";
				}
			}
			else
			{
				if (!Directory.Exists(path))
				{
					Debug.LogError("uFileBrowser: Directory doesn't exist:\n" + path);
					return;
				}
				this.currentPath = path;
			}
			if (this.currentPathField)
			{
				this.currentPathField.text = this.currentPath;
			}
			this.selected = -1;
			this.UpdateFileList();
			this.UpdateDirectoryList();
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0005F194 File Offset: 0x0005D394
		private void SelectFile(int i)
		{
			if (i >= this.fileButtons.Count)
			{
				Debug.LogError("uFileBrowser: Selection index bigger than array.");
				return;
			}
			if (i == this.selected && this.selectDirectory && this.fileButtons[i].isDir)
			{
				this.GotoDirectory(this.fileButtons[i].fullPath);
				return;
			}
			if (!this.fileButtons[i].isDir && this.selectDirectory)
			{
				return;
			}
			if (this.selected != -1)
			{
				this.fileButtons[this.selected].Unselect();
			}
			this.selected = i;
			this.fileButtons[i].Select();
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0005F24C File Offset: 0x0005D44C
		private void UpdateFileList()
		{
			if (this.fileButtons == null)
			{
				this.fileButtons = new List<FileButton>();
			}
			else
			{
				for (int i = 0; i < this.fileButtons.Count; i++)
				{
					global::UnityEngine.Object.Destroy(this.fileButtons[i].gameObject);
				}
				this.fileButtons.Clear();
			}
			if ((Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) && string.IsNullOrEmpty(this.currentPath))
			{
				for (int j = 0; j < this.drives.Count; j++)
				{
					this.CreateFileButton(this.drives[j], this.drives[j], true, j);
				}
				return;
			}
			List<string> list = new List<string>();
			if ((this.selectDirectory && this.showFiles) || !this.selectDirectory)
			{
				try
				{
					list = new List<string>(Directory.GetFiles(this.currentPath));
				}
				catch (Exception ex)
				{
					Debug.LogError("uFileBrowser: " + ex.Message);
				}
				this.FilterFormat(list);
			}
			List<string> list2 = new List<string>();
			try
			{
				list2 = new List<string>(Directory.GetDirectories(this.currentPath));
			}
			catch (Exception ex2)
			{
				Debug.LogError("uFileBrowser: " + ex2.Message);
			}
			for (int k = 0; k < list2.Count; k++)
			{
				string text = list2[k].Substring(list2[k].LastIndexOf(this.slash) + 1);
				this.CreateFileButton(text, list2[k], true, this.fileButtons.Count);
			}
			for (int l = 0; l < list.Count; l++)
			{
				string text2 = list[l].Substring(list[l].LastIndexOf(this.slash) + 1);
				this.CreateFileButton(text2, list[l], false, this.fileButtons.Count);
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0005F448 File Offset: 0x0005D648
		private void UpdateDirectoryList()
		{
			if (!this.directoryButtonPrefab)
			{
				return;
			}
			if (this.dirButtons == null)
			{
				this.dirButtons = new List<DirectoryButton>();
			}
			else
			{
				for (int i = 0; i < this.dirButtons.Count; i++)
				{
					global::UnityEngine.Object.Destroy(this.dirButtons[i].gameObject);
				}
				this.dirButtons.Clear();
			}
			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
			{
				this.CreateDirectoryButton("My Computer", "", 0);
			}
			else
			{
				this.CreateDirectoryButton(this.slash, this.slash, 0);
			}
			if (!string.IsNullOrEmpty(this.currentPath))
			{
				string[] array = this.currentPath.Split(this.slash[0], StringSplitOptions.None);
				for (int j = 0; j < array.Length; j++)
				{
					if (!string.IsNullOrEmpty(array[j]))
					{
						string text = this.currentPath.Substring(0, this.currentPath.LastIndexOf(array[j]));
						this.CreateDirectoryButton(array[j] + this.slash, text + array[j] + this.slash, this.dirButtons.Count);
					}
				}
			}
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0005F56C File Offset: 0x0005D76C
		private void FilterFormat(List<string> files)
		{
			if (string.IsNullOrEmpty(this.fileFormat))
			{
				return;
			}
			string[] array = this.fileFormat.Split('|', StringSplitOptions.None);
			for (int i = 0; i < files.Count; i++)
			{
				bool flag = true;
				string text = "";
				if (files[i].Contains("."))
				{
					text = files[i].Substring(files[i].LastIndexOf('.') + 1).ToLowerInvariant();
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (text == array[j].Trim().ToLowerInvariant())
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					files.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0005F624 File Offset: 0x0005D824
		private void FilterList()
		{
			if (string.IsNullOrEmpty(this.search))
			{
				for (int i = 0; i < this.fileButtons.Count; i++)
				{
					this.fileButtons[i].gameObject.SetActive(true);
				}
				return;
			}
			for (int j = 0; j < this.fileButtons.Count; j++)
			{
				if (!this.fileButtons[j].text.ToLowerInvariant().Contains(this.search))
				{
					this.fileButtons[j].gameObject.SetActive(false);
				}
				else
				{
					this.fileButtons[j].gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0005F6D8 File Offset: 0x0005D8D8
		private void CreateFileButton(string text, string path, bool dir, int i)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.fileButtonPrefab, Vector3.zero, Quaternion.identity);
			gameObject.GetComponent<RectTransform>().SetParent(this.fileContent, false);
			FileButton component = gameObject.GetComponent<FileButton>();
			component.Set(this, text, path, dir, i);
			this.fileButtons.Add(component);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0005F72C File Offset: 0x0005D92C
		private void CreateDirectoryButton(string text, string path, int i)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.directoryButtonPrefab, Vector3.zero, Quaternion.identity);
			gameObject.GetComponent<RectTransform>().SetParent(this.dirContent, false);
			DirectoryButton component = gameObject.GetComponent<DirectoryButton>();
			component.Set(this, text, path, i);
			this.dirButtons.Add(component);
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0005F77C File Offset: 0x0005D97C
		public void PathFieldEndEdit()
		{
			if (Directory.Exists(this.currentPathField.text))
			{
				this.GotoDirectory(this.currentPathField.text);
				return;
			}
			this.currentPathField.text = this.currentPath;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0005F7B3 File Offset: 0x0005D9B3
		public void SearchChanged()
		{
			if (!this.searchField)
			{
				return;
			}
			this.search = this.searchField.text.Trim();
			this.FilterList();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0005F7DF File Offset: 0x0005D9DF
		public void SearchCancelClick()
		{
			this.search = "";
			this.searchField.text = "";
			this.FilterList();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0005F804 File Offset: 0x0005DA04
		public void SelectButtonClicked()
		{
			if (this.selected > -1 && ((this.fileButtons[this.selected].isDir && this.selectDirectory) || (!this.fileButtons[this.selected].isDir && !this.selectDirectory)))
			{
				this.callback(this.fileButtons[this.selected].fullPath);
				this.Hide();
			}
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0005F881 File Offset: 0x0005DA81
		public void CancelButtonClicked()
		{
			if (!this.canCancel)
			{
				return;
			}
			if (this.callback != null)
			{
				this.callback("");
			}
			this.Hide();
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0005F8AC File Offset: 0x0005DAAC
		public Sprite GetFileIcon(string path)
		{
			if (path.Contains("."))
			{
				string text = path.Substring(path.LastIndexOf('.') + 1);
				for (int i = 0; i < this.fileIcons.Count; i++)
				{
					if (this.fileIcons[i].extension == text)
					{
						return this.fileIcons[i].icon;
					}
				}
				return this.defaultIcon;
			}
			return this.defaultIcon;
		}

		// Token: 0x04000A45 RID: 2629
		public string defaultPath = "";

		// Token: 0x04000A46 RID: 2630
		public bool selectDirectory;

		// Token: 0x04000A47 RID: 2631
		public bool showFiles;

		// Token: 0x04000A48 RID: 2632
		public bool canCancel = true;

		// Token: 0x04000A49 RID: 2633
		public string fileFormat = "";

		// Token: 0x04000A4A RID: 2634
		public GameObject overlay;

		// Token: 0x04000A4B RID: 2635
		public GameObject window;

		// Token: 0x04000A4C RID: 2636
		public GameObject fileButtonPrefab;

		// Token: 0x04000A4D RID: 2637
		public GameObject directoryButtonPrefab;

		// Token: 0x04000A4E RID: 2638
		public RectTransform fileContent;

		// Token: 0x04000A4F RID: 2639
		public RectTransform dirContent;

		// Token: 0x04000A50 RID: 2640
		public InputField currentPathField;

		// Token: 0x04000A51 RID: 2641
		public InputField searchField;

		// Token: 0x04000A52 RID: 2642
		public Button searchCancelButton;

		// Token: 0x04000A53 RID: 2643
		public Button cancelButton;

		// Token: 0x04000A54 RID: 2644
		public Sprite folderIcon;

		// Token: 0x04000A55 RID: 2645
		public Sprite defaultIcon;

		// Token: 0x04000A56 RID: 2646
		public List<FileIcon> fileIcons = new List<FileIcon>();

		// Token: 0x04000A57 RID: 2647
		[SerializeField]
		[HideInInspector]
		private string currentPath;

		// Token: 0x04000A58 RID: 2648
		[SerializeField]
		[HideInInspector]
		private string search;

		// Token: 0x04000A59 RID: 2649
		[SerializeField]
		[HideInInspector]
		private string slash;

		// Token: 0x04000A5A RID: 2650
		[SerializeField]
		[HideInInspector]
		private List<string> drives;

		// Token: 0x04000A5B RID: 2651
		private List<FileButton> fileButtons;

		// Token: 0x04000A5C RID: 2652
		private List<DirectoryButton> dirButtons;

		// Token: 0x04000A5D RID: 2653
		private int selected = -1;

		// Token: 0x04000A5E RID: 2654
		private FileBrowserCallback callback;
	}
}
