using System;
using System.IO;
using System.Linq;
using AiUnity.Common.IO;
using UnityEngine;

namespace AiUnity.NLog.Core.Common
{
	// Token: 0x020002B8 RID: 696
	public class NLogConfigFile : UnityFileInfo<NLogConfigFile>
	{
		// Token: 0x06001860 RID: 6240 RVA: 0x0007EDF4 File Offset: 0x0007CFF4
		public NLogConfigFile()
		{
			string text = PlayerPrefs.GetString("AiUnityNLogConfigFullFileName");
			if (string.IsNullOrEmpty(text))
			{
				if (Application.isEditor)
				{
					string text2 = (from s in Directory.GetFiles(Application.dataPath, "NLogger.cs", SearchOption.AllDirectories)
						select s.Replace('\\', '/')).FirstOrDefault<string>((string s) => s.Contains("/NLog/Core/"));
					string text3 = (string.IsNullOrEmpty(text2) ? Application.dataPath : text2.Substring(0, text2.IndexOf("/NLog/Core/"))) + "/UserData/NLog/Resources";
					Directory.CreateDirectory(text3);
					text = text3 + "/NLog.xml";
				}
				else
				{
					text = Application.dataPath + "/NLog.xml";
				}
			}
			base.FileInfo = new FileInfo(text);
		}

		// Token: 0x06001861 RID: 6241 RVA: 0x0007EEDB File Offset: 0x0007D0DB
		public void SetConfigFileName(string configFullFileName)
		{
			PlayerPrefs.SetString("AiUnityNLogConfigFullFileName", configFullFileName);
			base.FileInfo = new FileInfo(configFullFileName);
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0007EEF4 File Offset: 0x0007D0F4
		public string GetConfigText()
		{
			TextAsset textAsset = Resources.Load<TextAsset>(base.NameWithoutExtension);
			if (textAsset != null)
			{
				return textAsset.text;
			}
			if (base.FileInfo.Exists)
			{
				return File.ReadAllText(base.FileInfo.FullName);
			}
			return null;
		}
	}
}
