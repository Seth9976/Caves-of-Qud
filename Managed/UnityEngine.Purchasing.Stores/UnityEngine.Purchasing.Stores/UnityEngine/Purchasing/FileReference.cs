using System;
using System.IO;
using Uniject;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200008C RID: 140
	internal class FileReference
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000AA80 File Offset: 0x00008C80
		internal static FileReference CreateInstance(string filename, ILogger logger, IUtil util)
		{
			FileReference fileReference;
			try
			{
				string text = Path.Combine(util.persistentDataPath, "Unity");
				string text2 = Path.Combine(util.cloudProjectId, "IAP");
				string text3 = Path.Combine(text, text2);
				Directory.CreateDirectory(text3);
				fileReference = new FileReference(Path.Combine(text3, filename), logger);
			}
			catch
			{
				fileReference = null;
			}
			return fileReference;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		internal FileReference(string filePath, ILogger logger)
		{
			this.m_FilePath = filePath;
			this.m_Logger = logger;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		internal void Save(string payload)
		{
			try
			{
				File.WriteAllText(this.m_FilePath, payload);
			}
			catch (Exception ex)
			{
				this.m_Logger.LogError("Failed persisting content", ex);
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000AB38 File Offset: 0x00008D38
		internal string Load()
		{
			string text;
			try
			{
				text = File.ReadAllText(this.m_FilePath);
			}
			catch
			{
				text = null;
			}
			return text;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000AB6C File Offset: 0x00008D6C
		internal void Delete()
		{
			try
			{
				File.Delete(this.m_FilePath);
			}
			catch (Exception ex)
			{
				this.m_Logger.LogWarning("Failed deleting cached content", ex);
			}
		}

		// Token: 0x040001D4 RID: 468
		private readonly string m_FilePath;

		// Token: 0x040001D5 RID: 469
		private readonly ILogger m_Logger;
	}
}
