using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace MonoMod
{
	// Token: 0x0200042C RID: 1068
	internal static class MMDbgLog
	{
		// Token: 0x06001679 RID: 5753 RVA: 0x00048950 File Offset: 0x00046B50
		static MMDbgLog()
		{
			bool flag3;
			if (!(Environment.GetEnvironmentVariable("MONOMOD_DBGLOG") == "1"))
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONOMOD_DBGLOG");
				bool? flag;
				if (environmentVariable == null)
				{
					flag = null;
				}
				else
				{
					string text = environmentVariable.ToLower(CultureInfo.InvariantCulture);
					flag = ((text != null) ? new bool?(text.Contains(MMDbgLog.Tag.ToLower(CultureInfo.InvariantCulture), StringComparison.Ordinal)) : null);
				}
				bool? flag2 = flag;
				flag3 = flag2.GetValueOrDefault();
			}
			else
			{
				flag3 = true;
			}
			if (flag3)
			{
				MMDbgLog.Start();
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000489EF File Offset: 0x00046BEF
		public static void WaitForDebugger()
		{
			if (!MMDbgLog.Debugging)
			{
				MMDbgLog.Debugging = true;
				Debugger.Launch();
				Thread.Sleep(6000);
				Debugger.Break();
			}
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00048A14 File Offset: 0x00046C14
		public static void Start()
		{
			if (MMDbgLog.Writer != null)
			{
				return;
			}
			string text = Environment.GetEnvironmentVariable("MONOMOD_DBGLOG_PATH");
			if (text == "-")
			{
				MMDbgLog.Writer = Console.Out;
				return;
			}
			if (string.IsNullOrEmpty(text))
			{
				text = "mmdbglog.txt";
			}
			text = Path.GetFullPath(Path.GetFileNameWithoutExtension(text) + "-" + MMDbgLog.Tag + Path.GetExtension(text));
			try
			{
				if (File.Exists(text))
				{
					File.Delete(text);
				}
			}
			catch
			{
			}
			try
			{
				string directoryName = Path.GetDirectoryName(text);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				MMDbgLog.Writer = new StreamWriter(new FileStream(text, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read | FileShare.Write | FileShare.Delete), Encoding.UTF8);
			}
			catch
			{
			}
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00048AE0 File Offset: 0x00046CE0
		public static void Log(string str)
		{
			TextWriter writer = MMDbgLog.Writer;
			if (writer == null)
			{
				return;
			}
			writer.WriteLine(str);
			writer.Flush();
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00048B04 File Offset: 0x00046D04
		public static T Log<T>(string str, T value)
		{
			TextWriter writer = MMDbgLog.Writer;
			if (writer == null)
			{
				return value;
			}
			writer.WriteLine(string.Format(CultureInfo.InvariantCulture, str, new object[] { value }));
			writer.Flush();
			return value;
		}

		// Token: 0x04000FAE RID: 4014
		public static readonly string Tag = typeof(MMDbgLog).Assembly.GetName().Name;

		// Token: 0x04000FAF RID: 4015
		public static TextWriter Writer;

		// Token: 0x04000FB0 RID: 4016
		public static bool Debugging;
	}
}
