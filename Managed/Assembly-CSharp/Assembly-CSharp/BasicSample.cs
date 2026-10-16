using System;
using SFB;
using UnityEngine;

// Token: 0x02000172 RID: 370
public class BasicSample : MonoBehaviour
{
	// Token: 0x06000E68 RID: 3688 RVA: 0x0005C680 File Offset: 0x0005A880
	private void OnGUI()
	{
		Vector3 vector = new Vector3((float)Screen.width / 800f, (float)Screen.height / 600f, 1f);
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, vector);
		GUILayout.Space(20f);
		GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
		GUILayout.Space(20f);
		GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
		if (GUILayout.Button("Open File", Array.Empty<GUILayoutOption>()))
		{
			this.WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Open File Async", Array.Empty<GUILayoutOption>()))
		{
			StandaloneFileBrowser.OpenFilePanelAsync("Open File", "", "", false, delegate(string[] paths)
			{
				this.WriteResult(paths);
			});
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Open File Multiple", Array.Empty<GUILayoutOption>()))
		{
			this.WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", true));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Open File Extension", Array.Empty<GUILayoutOption>()))
		{
			this.WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "txt", true));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Open File Directory", Array.Empty<GUILayoutOption>()))
		{
			this.WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", Application.dataPath, "", true));
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Open File Filter", Array.Empty<GUILayoutOption>()))
		{
			ExtensionFilter[] array = new ExtensionFilter[]
			{
				new ExtensionFilter("Image Files", new string[] { "png", "jpg", "jpeg" }),
				new ExtensionFilter("Sound Files", new string[] { "mp3", "wav" }),
				new ExtensionFilter("All Files", new string[] { "*" })
			};
			this.WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", array, true));
		}
		GUILayout.Space(15f);
		if (GUILayout.Button("Open Folder", Array.Empty<GUILayoutOption>()))
		{
			string[] array2 = StandaloneFileBrowser.OpenFolderPanel("Select Folder", "", true);
			this.WriteResult(array2);
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Open Folder Async", Array.Empty<GUILayoutOption>()))
		{
			StandaloneFileBrowser.OpenFolderPanelAsync("Select Folder", "", true, delegate(string[] paths)
			{
				this.WriteResult(paths);
			});
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Open Folder Directory", Array.Empty<GUILayoutOption>()))
		{
			string[] array3 = StandaloneFileBrowser.OpenFolderPanel("Select Folder", Application.dataPath, true);
			this.WriteResult(array3);
		}
		GUILayout.Space(15f);
		if (GUILayout.Button("Save File", Array.Empty<GUILayoutOption>()))
		{
			this._path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "", "");
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Save File Async", Array.Empty<GUILayoutOption>()))
		{
			StandaloneFileBrowser.SaveFilePanelAsync("Save File", "", "", "", delegate(string path)
			{
				this.WriteResult(path);
			});
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Save File Default Name", Array.Empty<GUILayoutOption>()))
		{
			this._path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", "");
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Save File Default Name Ext", Array.Empty<GUILayoutOption>()))
		{
			this._path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", "dat");
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Save File Directory", Array.Empty<GUILayoutOption>()))
		{
			this._path = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "", "");
		}
		GUILayout.Space(5f);
		if (GUILayout.Button("Save File Filter", Array.Empty<GUILayoutOption>()))
		{
			ExtensionFilter[] array4 = new ExtensionFilter[]
			{
				new ExtensionFilter("Binary", new string[] { "bin" }),
				new ExtensionFilter("Text", new string[] { "txt" })
			};
			this._path = StandaloneFileBrowser.SaveFilePanel("Save File", "", "MySaveFile", array4);
		}
		GUILayout.EndVertical();
		GUILayout.Space(20f);
		GUILayout.Label(this._path, Array.Empty<GUILayoutOption>());
		GUILayout.EndHorizontal();
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x0005CB10 File Offset: 0x0005AD10
	public void WriteResult(string[] paths)
	{
		if (paths.Length == 0)
		{
			return;
		}
		this._path = "";
		foreach (string text in paths)
		{
			this._path = this._path + text + "\n";
		}
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x0005CB58 File Offset: 0x0005AD58
	public void WriteResult(string path)
	{
		this._path = path;
	}

	// Token: 0x04000A0C RID: 2572
	private string _path;
}
