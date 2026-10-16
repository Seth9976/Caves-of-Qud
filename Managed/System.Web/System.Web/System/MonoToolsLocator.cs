using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x02000015 RID: 21
	internal static class MonoToolsLocator
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002C54 File Offset: 0x00000E54
		static MonoToolsLocator()
		{
			string directoryName = Path.GetDirectoryName((string)typeof(Environment).GetProperty("GacPath", BindingFlags.Static | BindingFlags.NonPublic).GetGetMethod(true).Invoke(null, null));
			if (Path.DirectorySeparatorChar == '\\')
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				global::System.MonoToolsLocator.GetModuleFileName(IntPtr.Zero, stringBuilder, stringBuilder.Capacity);
				string text = stringBuilder.ToString();
				string fileName = Path.GetFileName(text);
				if (fileName.StartsWith("mono") && fileName.EndsWith(".exe"))
				{
					global::System.MonoToolsLocator.Mono = text;
				}
				if (!File.Exists(global::System.MonoToolsLocator.Mono))
				{
					global::System.MonoToolsLocator.Mono = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(directoryName)), "bin\\mono.exe");
				}
				if (!File.Exists(global::System.MonoToolsLocator.Mono))
				{
					global::System.MonoToolsLocator.Mono = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(directoryName))), "mono\\mini\\mono.exe");
				}
				global::System.MonoToolsLocator.McsCSharpCompiler = Path.Combine(directoryName, "4.5", "mcs.exe");
				if (!File.Exists(global::System.MonoToolsLocator.McsCSharpCompiler))
				{
					global::System.MonoToolsLocator.McsCSharpCompiler = Path.Combine(Path.GetDirectoryName(directoryName), "lib", "net_4_x", "mcs.exe");
				}
				global::System.MonoToolsLocator.VBCompiler = Path.Combine(directoryName, "4.5\\vbnc.exe");
				global::System.MonoToolsLocator.AssemblyLinker = Path.Combine(directoryName, "4.5\\al.exe");
				if (!File.Exists(global::System.MonoToolsLocator.AssemblyLinker))
				{
					global::System.MonoToolsLocator.AssemblyLinker = Path.Combine(Path.GetDirectoryName(directoryName), "lib\\net_4_x\\al.exe");
					return;
				}
			}
			else
			{
				global::System.MonoToolsLocator.Mono = Path.Combine(directoryName, "bin", "mono");
				if (!File.Exists(global::System.MonoToolsLocator.Mono))
				{
					global::System.MonoToolsLocator.Mono = "mono";
				}
				string localPath = new Uri(typeof(object).Assembly.CodeBase).LocalPath;
				global::System.MonoToolsLocator.McsCSharpCompiler = Path.GetFullPath(Path.Combine(new string[] { localPath, "..", "..", "..", "..", "bin", "mcs" }));
				if (!File.Exists(global::System.MonoToolsLocator.McsCSharpCompiler))
				{
					global::System.MonoToolsLocator.McsCSharpCompiler = "mcs";
				}
				global::System.MonoToolsLocator.VBCompiler = Path.GetFullPath(Path.Combine(new string[] { localPath, "..", "..", "..", "..", "bin", "vbnc" }));
				if (!File.Exists(global::System.MonoToolsLocator.VBCompiler))
				{
					global::System.MonoToolsLocator.VBCompiler = "vbnc";
				}
				global::System.MonoToolsLocator.AssemblyLinker = Path.GetFullPath(Path.Combine(new string[] { localPath, "..", "..", "..", "..", "bin", "al" }));
				if (!File.Exists(global::System.MonoToolsLocator.AssemblyLinker))
				{
					global::System.MonoToolsLocator.AssemblyLinker = "al";
				}
			}
		}

		// Token: 0x0600003E RID: 62
		[DllImport("kernel32.dll")]
		private static extern uint GetModuleFileName([In] IntPtr hModule, [Out] StringBuilder lpFilename, [In] int nSize);

		// Token: 0x04000D44 RID: 3396
		public static readonly string Mono;

		// Token: 0x04000D45 RID: 3397
		public static readonly string McsCSharpCompiler;

		// Token: 0x04000D46 RID: 3398
		public static readonly string VBCompiler;

		// Token: 0x04000D47 RID: 3399
		public static readonly string AssemblyLinker;
	}
}
