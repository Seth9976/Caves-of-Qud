using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace HarmonyLib
{
	// Token: 0x02000194 RID: 404
	public static class FileLog
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00016914 File Offset: 0x00014B14
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x0001691B File Offset: 0x00014B1B
		public static StreamWriter LogWriter { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00016924 File Offset: 0x00014B24
		public static string LogPath
		{
			get
			{
				object obj = FileLog.fileLock;
				string logPath;
				lock (obj)
				{
					if (!FileLog._logPathInited)
					{
						FileLog._logPathInited = true;
						if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("HARMONY_NO_LOG")))
						{
							return null;
						}
						FileLog._logPath = Environment.GetEnvironmentVariable("HARMONY_LOG_FILE");
						if (string.IsNullOrEmpty(FileLog._logPath))
						{
							string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
							Directory.CreateDirectory(folderPath);
							FileLog._logPath = Path.Combine(folderPath, "harmony.log.txt");
						}
					}
					logPath = FileLog._logPath;
				}
				return logPath;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000169C0 File Offset: 0x00014BC0
		private static string IndentString()
		{
			return new string(FileLog.indentChar, FileLog.indentLevel);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000169D4 File Offset: 0x00014BD4
		public static void ChangeIndent(int delta)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.indentLevel = Math.Max(0, FileLog.indentLevel + delta);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00016A20 File Offset: 0x00014C20
		public static void LogBuffered(string str)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.buffer.Add(FileLog.IndentString() + str);
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00016A70 File Offset: 0x00014C70
		public static void LogBuffered(List<string> strings)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.buffer.AddRange(strings);
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00016AB4 File Offset: 0x00014CB4
		public static List<string> GetBuffer(bool clear)
		{
			object obj = FileLog.fileLock;
			List<string> list2;
			lock (obj)
			{
				List<string> list = FileLog.buffer;
				if (clear)
				{
					FileLog.buffer = new List<string>();
				}
				list2 = list;
			}
			return list2;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00016B04 File Offset: 0x00014D04
		public static void SetBuffer(List<string> buffer)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				FileLog.buffer = buffer;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00016B44 File Offset: 0x00014D44
		public static void FlushBuffer()
		{
			if (FileLog.LogWriter != null)
			{
				foreach (string text in FileLog.buffer)
				{
					FileLog.LogWriter.WriteLine(text);
				}
				FileLog.buffer.Clear();
				return;
			}
			if (FileLog.LogPath == null)
			{
				return;
			}
			object obj = FileLog.fileLock;
			lock (obj)
			{
				if (FileLog.buffer.Count > 0)
				{
					using (StreamWriter streamWriter = File.AppendText(FileLog.LogPath))
					{
						foreach (string text2 in FileLog.buffer)
						{
							streamWriter.WriteLine(text2);
						}
						FileLog.buffer.Clear();
					}
				}
			}
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00016C60 File Offset: 0x00014E60
		public static void Log(string str)
		{
			if (FileLog.LogWriter != null)
			{
				FileLog.LogWriter.WriteLine(FileLog.IndentString() + str);
				return;
			}
			if (FileLog.LogPath == null)
			{
				return;
			}
			object obj = FileLog.fileLock;
			lock (obj)
			{
				using (StreamWriter streamWriter = File.AppendText(FileLog.LogPath))
				{
					streamWriter.WriteLine(FileLog.IndentString() + str);
				}
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00016CF0 File Offset: 0x00014EF0
		public static void Debug(string str)
		{
			if (Harmony.DEBUG)
			{
				FileLog.Log(str);
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00016D00 File Offset: 0x00014F00
		public static void Reset()
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				File.Delete(string.Format("{0}{1}harmony.log.txt", Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Path.DirectorySeparatorChar));
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00016D58 File Offset: 0x00014F58
		public unsafe static void LogBytes(long ptr, int len)
		{
			object obj = FileLog.fileLock;
			lock (obj)
			{
				byte* ptr2 = ptr;
				string text = "";
				for (int i = 1; i <= len; i++)
				{
					if (text.Length == 0)
					{
						text = "#  ";
					}
					text += string.Format("{0:X2} ", *ptr2);
					if (i > 1 || len == 1)
					{
						if (i % 8 == 0 || i == len)
						{
							FileLog.Log(text);
							text = "";
						}
						else if (i % 4 == 0)
						{
							text += " ";
						}
					}
					ptr2++;
				}
				byte[] array = new byte[len];
				Marshal.Copy((IntPtr)ptr, array, 0, len);
				byte[] array2 = MD5.Create().ComputeHash(array);
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < array2.Length; j++)
				{
					stringBuilder.Append(array2[j].ToString("X2"));
				}
				FileLog.Log(string.Format("HASH: {0}", stringBuilder));
			}
		}

		// Token: 0x0400020E RID: 526
		private static readonly object fileLock = new object();

		// Token: 0x0400020F RID: 527
		private static bool _logPathInited;

		// Token: 0x04000210 RID: 528
		private static string _logPath;

		// Token: 0x04000212 RID: 530
		public static char indentChar = '\t';

		// Token: 0x04000213 RID: 531
		public static int indentLevel = 0;

		// Token: 0x04000214 RID: 532
		private static List<string> buffer = new List<string>();
	}
}
