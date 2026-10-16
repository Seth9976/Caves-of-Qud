using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class FileBrowser
{
	// Token: 0x0600053D RID: 1341 RVA: 0x00021E8E File Offset: 0x0002008E
	public FileBrowser(Rect screenRect, string name, FileBrowser.FinishedCallback callback)
	{
		this.m_name = name;
		this.m_screenRect = screenRect;
		this.m_browserType = FileBrowserType.File;
		this.m_callback = callback;
		this.SetNewDirectory(Directory.GetCurrentDirectory());
		this.SwitchDirectoryNow();
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x0600053E RID: 1342 RVA: 0x00021EC3 File Offset: 0x000200C3
	// (set) Token: 0x0600053F RID: 1343 RVA: 0x00021ECB File Offset: 0x000200CB
	public string CurrentDirectory
	{
		get
		{
			return this.m_currentDirectory;
		}
		set
		{
			Debug.Log("Setdir->" + value);
			this.SetNewDirectory(value);
			this.SwitchDirectoryNow();
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06000540 RID: 1344 RVA: 0x00021EEA File Offset: 0x000200EA
	// (set) Token: 0x06000541 RID: 1345 RVA: 0x00021EF2 File Offset: 0x000200F2
	public string SelectionPattern
	{
		get
		{
			return this.m_filePattern;
		}
		set
		{
			this.m_filePattern = value;
			this.ReadDirectoryContents();
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000542 RID: 1346 RVA: 0x00021F01 File Offset: 0x00020101
	// (set) Token: 0x06000543 RID: 1347 RVA: 0x00021F09 File Offset: 0x00020109
	public Texture2D DirectoryImage
	{
		get
		{
			return this.m_directoryImage;
		}
		set
		{
			this.m_directoryImage = value;
			this.BuildContent();
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000544 RID: 1348 RVA: 0x00021F18 File Offset: 0x00020118
	// (set) Token: 0x06000545 RID: 1349 RVA: 0x00021F20 File Offset: 0x00020120
	public Texture2D FileImage
	{
		get
		{
			return this.m_fileImage;
		}
		set
		{
			this.m_fileImage = value;
			this.BuildContent();
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000546 RID: 1350 RVA: 0x00021F2F File Offset: 0x0002012F
	// (set) Token: 0x06000547 RID: 1351 RVA: 0x00021F37 File Offset: 0x00020137
	public FileBrowserType BrowserType
	{
		get
		{
			return this.m_browserType;
		}
		set
		{
			this.m_browserType = value;
			this.ReadDirectoryContents();
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000548 RID: 1352 RVA: 0x00021F48 File Offset: 0x00020148
	protected GUIStyle CentredText
	{
		get
		{
			if (this.m_centredText == null)
			{
				this.m_centredText = new GUIStyle(GUI.skin.label);
				this.m_centredText.alignment = TextAnchor.MiddleLeft;
				this.m_centredText.fixedHeight = GUI.skin.button.fixedHeight;
			}
			return this.m_centredText;
		}
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00021F9E File Offset: 0x0002019E
	protected void SetNewDirectory(string directory)
	{
		this.m_newDirectory = directory;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00021FA8 File Offset: 0x000201A8
	protected void SwitchDirectoryNow()
	{
		if (this.m_newDirectory == null || this.m_currentDirectory == this.m_newDirectory)
		{
			return;
		}
		this.m_currentDirectory = this.m_newDirectory;
		this.m_scrollPosition = Vector2.zero;
		this.m_selectedDirectory = (this.m_selectedNonMatchingDirectory = (this.m_selectedFile = -1));
		this.ReadDirectoryContents();
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00022008 File Offset: 0x00020208
	protected void ReadDirectoryContents()
	{
		if (this.m_currentDirectory == "/")
		{
			this.m_currentDirectoryParts = new string[] { "" };
			this.m_currentDirectoryMatches = false;
		}
		else
		{
			this.m_currentDirectoryParts = this.m_currentDirectory.Split(Path.DirectorySeparatorChar, StringSplitOptions.None);
			if (this.SelectionPattern != null)
			{
				bool directoryName = Path.GetDirectoryName(this.m_currentDirectory) != null;
				string[] array = new string[0];
				if (directoryName)
				{
					array = Directory.GetDirectories(Path.GetDirectoryName(this.m_currentDirectory), this.SelectionPattern);
				}
				this.m_currentDirectoryMatches = Array.IndexOf<string>(array, this.m_currentDirectory) >= 0;
			}
			else
			{
				this.m_currentDirectoryMatches = false;
			}
		}
		if (this.BrowserType == FileBrowserType.File || this.SelectionPattern == null)
		{
			this.m_directories = Directory.GetDirectories(this.m_currentDirectory);
			this.m_nonMatchingDirectories = new string[0];
		}
		else
		{
			this.m_directories = Directory.GetDirectories(this.m_currentDirectory, this.SelectionPattern);
			List<string> list = new List<string>();
			foreach (string text in Directory.GetDirectories(this.m_currentDirectory))
			{
				if (Array.IndexOf<string>(this.m_directories, text) < 0)
				{
					list.Add(text);
				}
			}
			this.m_nonMatchingDirectories = list.ToArray();
			for (int j = 0; j < this.m_nonMatchingDirectories.Length; j++)
			{
				int num = this.m_nonMatchingDirectories[j].LastIndexOf(Path.DirectorySeparatorChar);
				this.m_nonMatchingDirectories[j] = this.m_nonMatchingDirectories[j].Substring(num + 1);
			}
			Array.Sort<string>(this.m_nonMatchingDirectories);
		}
		for (int k = 0; k < this.m_directories.Length; k++)
		{
			this.m_directories[k] = this.m_directories[k].Substring(this.m_directories[k].LastIndexOf(Path.DirectorySeparatorChar) + 1);
		}
		if (this.BrowserType == FileBrowserType.Directory || this.SelectionPattern == null)
		{
			this.m_files = Directory.GetFiles(this.m_currentDirectory);
			this.m_nonMatchingFiles = new string[0];
		}
		else
		{
			this.m_files = Directory.GetFiles(this.m_currentDirectory, this.SelectionPattern);
			List<string> list2 = new List<string>();
			foreach (string text2 in Directory.GetFiles(this.m_currentDirectory))
			{
				if (Array.IndexOf<string>(this.m_files, text2) < 0)
				{
					list2.Add(text2);
				}
			}
			this.m_nonMatchingFiles = list2.ToArray();
			for (int l = 0; l < this.m_nonMatchingFiles.Length; l++)
			{
				this.m_nonMatchingFiles[l] = Path.GetFileName(this.m_nonMatchingFiles[l]);
			}
			Array.Sort<string>(this.m_nonMatchingFiles);
		}
		for (int m = 0; m < this.m_files.Length; m++)
		{
			this.m_files[m] = Path.GetFileName(this.m_files[m]);
		}
		Array.Sort<string>(this.m_files);
		this.BuildContent();
		this.m_newDirectory = null;
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x000222EC File Offset: 0x000204EC
	protected void BuildContent()
	{
		this.m_directoriesWithImages = new GUIContent[this.m_directories.Length];
		for (int i = 0; i < this.m_directoriesWithImages.Length; i++)
		{
			this.m_directoriesWithImages[i] = new GUIContent(this.m_directories[i], this.DirectoryImage);
		}
		this.m_nonMatchingDirectoriesWithImages = new GUIContent[this.m_nonMatchingDirectories.Length];
		for (int j = 0; j < this.m_nonMatchingDirectoriesWithImages.Length; j++)
		{
			this.m_nonMatchingDirectoriesWithImages[j] = new GUIContent(this.m_nonMatchingDirectories[j], this.DirectoryImage);
		}
		this.m_filesWithImages = new GUIContent[this.m_files.Length];
		for (int k = 0; k < this.m_filesWithImages.Length; k++)
		{
			this.m_filesWithImages[k] = new GUIContent(this.m_files[k], this.FileImage);
		}
		this.m_nonMatchingFilesWithImages = new GUIContent[this.m_nonMatchingFiles.Length];
		for (int l = 0; l < this.m_nonMatchingFilesWithImages.Length; l++)
		{
			this.m_nonMatchingFilesWithImages[l] = new GUIContent(this.m_nonMatchingFiles[l], this.FileImage);
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00022400 File Offset: 0x00020600
	public void OnGUI()
	{
		if (this.skin == null)
		{
			this.skin = Resources.Load("GUISKIN/QudSkin") as GUISkin;
			GUI.skin = this.skin;
		}
		GUILayout.BeginArea(this.m_screenRect, this.m_name, GUI.skin.window);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		for (int i = 0; i < this.m_currentDirectoryParts.Length; i++)
		{
			if (i == this.m_currentDirectoryParts.Length - 1)
			{
				GUILayout.Label(this.m_currentDirectoryParts[i], this.CentredText, Array.Empty<GUILayoutOption>());
			}
			else if (GUILayout.Button(this.m_currentDirectoryParts[i], Array.Empty<GUILayoutOption>()))
			{
				string text = this.m_currentDirectory;
				for (int j = this.m_currentDirectoryParts.Length - 1; j > i; j--)
				{
					text = Path.GetDirectoryName(text);
				}
				this.SetNewDirectory(text);
			}
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		this.m_scrollPosition = GUILayout.BeginScrollView(this.m_scrollPosition, false, true, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.box, Array.Empty<GUILayoutOption>());
		this.m_selectedDirectory = GUILayoutx.SelectionList(this.m_selectedDirectory, this.m_directoriesWithImages, new GUILayoutx.DoubleClickCallback(this.DirectoryDoubleClickCallback));
		if (this.m_selectedDirectory > -1)
		{
			this.m_selectedFile = (this.m_selectedNonMatchingDirectory = -1);
		}
		this.m_selectedNonMatchingDirectory = GUILayoutx.SelectionList(this.m_selectedNonMatchingDirectory, this.m_nonMatchingDirectoriesWithImages, new GUILayoutx.DoubleClickCallback(this.NonMatchingDirectoryDoubleClickCallback));
		if (this.m_selectedNonMatchingDirectory > -1)
		{
			this.m_selectedDirectory = (this.m_selectedFile = -1);
		}
		GUI.enabled = this.BrowserType == FileBrowserType.File;
		this.m_selectedFile = GUILayoutx.SelectionList(this.m_selectedFile, this.m_filesWithImages, new GUILayoutx.DoubleClickCallback(this.FileDoubleClickCallback));
		GUI.enabled = true;
		if (this.m_selectedFile > -1)
		{
			this.m_selectedDirectory = (this.m_selectedNonMatchingDirectory = -1);
		}
		GUI.enabled = false;
		GUILayoutx.SelectionList(-1, this.m_nonMatchingFilesWithImages);
		GUI.enabled = true;
		GUILayout.EndScrollView();
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		string text2 = "Select";
		if (FileBrowser.Mode == FileBrowser.PickFileModes.Save)
		{
			text2 = "Save";
			FileBrowser.SaveFileName = GUILayout.TextArea(FileBrowser.SaveFileName, new GUILayoutOption[] { GUILayout.Width(500f) });
		}
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("Cancel", new GUILayoutOption[] { GUILayout.Width(100f) }))
		{
			this.m_callback(null);
		}
		if (FileBrowser.Mode == FileBrowser.PickFileModes.Save)
		{
			GUI.enabled = !string.IsNullOrEmpty(FileBrowser.SaveFileName);
		}
		else if (this.BrowserType == FileBrowserType.File)
		{
			GUI.enabled = this.m_selectedFile > -1;
		}
		else if (this.SelectionPattern == null)
		{
			GUI.enabled = this.m_selectedDirectory > -1;
		}
		else
		{
			GUI.enabled = this.m_selectedDirectory > -1 || (this.m_currentDirectoryMatches && this.m_selectedNonMatchingDirectory == -1 && this.m_selectedFile == -1);
		}
		if (GUILayout.Button(text2, new GUILayoutOption[] { GUILayout.Width(100f) }))
		{
			if (FileBrowser.Mode == FileBrowser.PickFileModes.Save && FileBrowser.SaveFileName != null)
			{
				this.m_callback(Path.Combine(this.m_currentDirectory, FileBrowser.SaveFileName));
			}
			else if (this.BrowserType == FileBrowserType.File)
			{
				this.m_callback(Path.Combine(this.m_currentDirectory, this.m_files[this.m_selectedFile]));
			}
			else if (this.m_selectedDirectory > -1)
			{
				this.m_callback(Path.Combine(this.m_currentDirectory, this.m_directories[this.m_selectedDirectory]));
			}
			else
			{
				this.m_callback(this.m_currentDirectory);
			}
		}
		GUI.enabled = true;
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
		if (Event.current.type == EventType.Repaint)
		{
			this.SwitchDirectoryNow();
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x000227C7 File Offset: 0x000209C7
	protected void FileDoubleClickCallback(int i)
	{
		if (this.BrowserType == FileBrowserType.File)
		{
			this.m_callback(Path.Combine(this.m_currentDirectory, this.m_files[i]));
		}
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x000227EF File Offset: 0x000209EF
	protected void DirectoryDoubleClickCallback(int i)
	{
		this.SetNewDirectory(Path.Combine(this.m_currentDirectory, this.m_directories[i]));
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x0002280A File Offset: 0x00020A0A
	protected void NonMatchingDirectoryDoubleClickCallback(int i)
	{
		this.SetNewDirectory(Path.Combine(this.m_currentDirectory, this.m_nonMatchingDirectories[i]));
	}

	// Token: 0x04000405 RID: 1029
	public static string Pattern = "*.*";

	// Token: 0x04000406 RID: 1030
	public static string SaveFileName = "";

	// Token: 0x04000407 RID: 1031
	public static FileBrowser.PickFileModes Mode = FileBrowser.PickFileModes.Select;

	// Token: 0x04000408 RID: 1032
	protected FileBrowserType m_browserType;

	// Token: 0x04000409 RID: 1033
	public FileBrowser.FinishedCallback m_callback;

	// Token: 0x0400040A RID: 1034
	protected GUIStyle m_centredText;

	// Token: 0x0400040B RID: 1035
	protected string m_currentDirectory;

	// Token: 0x0400040C RID: 1036
	protected bool m_currentDirectoryMatches;

	// Token: 0x0400040D RID: 1037
	protected string[] m_currentDirectoryParts;

	// Token: 0x0400040E RID: 1038
	protected string[] m_directories;

	// Token: 0x0400040F RID: 1039
	protected GUIContent[] m_directoriesWithImages;

	// Token: 0x04000410 RID: 1040
	protected Texture2D m_directoryImage;

	// Token: 0x04000411 RID: 1041
	protected Texture2D m_fileImage;

	// Token: 0x04000412 RID: 1042
	protected string m_filePattern;

	// Token: 0x04000413 RID: 1043
	protected string[] m_files;

	// Token: 0x04000414 RID: 1044
	protected GUIContent[] m_filesWithImages;

	// Token: 0x04000415 RID: 1045
	protected string m_name;

	// Token: 0x04000416 RID: 1046
	protected string m_newDirectory;

	// Token: 0x04000417 RID: 1047
	protected string[] m_nonMatchingDirectories;

	// Token: 0x04000418 RID: 1048
	protected GUIContent[] m_nonMatchingDirectoriesWithImages;

	// Token: 0x04000419 RID: 1049
	protected string[] m_nonMatchingFiles;

	// Token: 0x0400041A RID: 1050
	protected GUIContent[] m_nonMatchingFilesWithImages;

	// Token: 0x0400041B RID: 1051
	protected Rect m_screenRect;

	// Token: 0x0400041C RID: 1052
	protected Vector2 m_scrollPosition;

	// Token: 0x0400041D RID: 1053
	protected int m_selectedDirectory;

	// Token: 0x0400041E RID: 1054
	protected int m_selectedFile;

	// Token: 0x0400041F RID: 1055
	protected int m_selectedNonMatchingDirectory;

	// Token: 0x04000420 RID: 1056
	public GUISkin skin;

	// Token: 0x020015A8 RID: 5544
	// (Invoke) Token: 0x0600D5E5 RID: 54757
	public delegate void FinishedCallback(string path);

	// Token: 0x020015A9 RID: 5545
	public enum PickFileModes
	{
		// Token: 0x04005724 RID: 22308
		Select,
		// Token: 0x04005725 RID: 22309
		Save
	}
}
