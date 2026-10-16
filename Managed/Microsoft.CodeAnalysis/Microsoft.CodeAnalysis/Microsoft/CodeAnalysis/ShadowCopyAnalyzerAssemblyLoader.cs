using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200016A RID: 362
	internal sealed class ShadowCopyAnalyzerAssemblyLoader : DefaultAnalyzerAssemblyLoader
	{
		// Token: 0x06000E88 RID: 3720 RVA: 0x0002C2DC File Offset: 0x0002A4DC
		public ShadowCopyAnalyzerAssemblyLoader(string baseDirectory = null)
		{
			if (baseDirectory != null)
			{
				this._baseDirectory = baseDirectory;
			}
			else
			{
				this._baseDirectory = Path.Combine(Path.GetTempPath(), "CodeAnalysis", "AnalyzerShadowCopies");
			}
			this._shadowCopyDirectoryAndMutex = new Lazy<ValueTuple<string, Mutex>>(() => this.CreateUniqueDirectoryForProcess(), 2);
			this.DeleteLeftoverDirectoriesTask = Task.Run(new Action(this.DeleteLeftoverDirectories));
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0002C344 File Offset: 0x0002A544
		private void DeleteLeftoverDirectories()
		{
			if (!Directory.Exists(this._baseDirectory))
			{
				return;
			}
			IEnumerable<string> enumerable;
			try
			{
				enumerable = Directory.EnumerateDirectories(this._baseDirectory);
			}
			catch (DirectoryNotFoundException)
			{
				return;
			}
			foreach (string text in enumerable)
			{
				string text2 = Path.GetFileName(text).ToLowerInvariant();
				Mutex mutex = null;
				try
				{
					if (!Mutex.TryOpenExisting(text2, ref mutex))
					{
						ShadowCopyAnalyzerAssemblyLoader.ClearReadOnlyFlagOnFiles(text);
						Directory.Delete(text, true);
					}
				}
				catch
				{
				}
				finally
				{
					if (mutex != null)
					{
						mutex.Dispose();
					}
				}
			}
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0002C400 File Offset: 0x0002A600
		protected override Assembly LoadImpl(string fullPath)
		{
			string text = this.CreateUniqueDirectoryForAssembly();
			string text2 = ShadowCopyAnalyzerAssemblyLoader.CopyFileAndResources(fullPath, text);
			return base.LoadImpl(text2);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0002C424 File Offset: 0x0002A624
		private static string CopyFileAndResources(string fullPath, string assemblyDirectory)
		{
			string fileName = Path.GetFileName(fullPath);
			string text = Path.Combine(assemblyDirectory, fileName);
			ShadowCopyAnalyzerAssemblyLoader.CopyFile(fullPath, text);
			string directoryName = Path.GetDirectoryName(fullPath);
			string text2 = Path.GetFileNameWithoutExtension(fileName) + ".resources";
			string text3 = text2 + ".dll";
			foreach (string text4 in Directory.EnumerateDirectories(directoryName))
			{
				string fileName2 = Path.GetFileName(text4);
				string text5 = Path.Combine(text4, text3);
				if (File.Exists(text5))
				{
					string text6 = Path.Combine(assemblyDirectory, fileName2, text3);
					ShadowCopyAnalyzerAssemblyLoader.CopyFile(text5, text6);
				}
				text5 = Path.Combine(text4, text2, text3);
				if (File.Exists(text5))
				{
					string text7 = Path.Combine(assemblyDirectory, fileName2, text2, text3);
					ShadowCopyAnalyzerAssemblyLoader.CopyFile(text5, text7);
				}
			}
			return text;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x0002C4FC File Offset: 0x0002A6FC
		private static void CopyFile(string originalPath, string shadowCopyPath)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(shadowCopyPath));
			File.Copy(originalPath, shadowCopyPath);
			ShadowCopyAnalyzerAssemblyLoader.ClearReadOnlyFlagOnFile(new FileInfo(shadowCopyPath));
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x0002C51C File Offset: 0x0002A71C
		private static void ClearReadOnlyFlagOnFiles(string directoryPath)
		{
			foreach (FileInfo fileInfo in new DirectoryInfo(directoryPath).EnumerateFiles("*", 1))
			{
				ShadowCopyAnalyzerAssemblyLoader.ClearReadOnlyFlagOnFile(fileInfo);
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x0002C574 File Offset: 0x0002A774
		private static void ClearReadOnlyFlagOnFile(FileInfo fileInfo)
		{
			try
			{
				if (fileInfo.IsReadOnly)
				{
					fileInfo.IsReadOnly = false;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0002C5A8 File Offset: 0x0002A7A8
		private string CreateUniqueDirectoryForAssembly()
		{
			int num = Interlocked.Increment(ref this._assemblyDirectoryId);
			string text = Path.Combine(this._shadowCopyDirectoryAndMutex.Value.Item1, num.ToString());
			Directory.CreateDirectory(text);
			return text;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0002C5E4 File Offset: 0x0002A7E4
		[return: TupleElementNames(new string[] { "directory", "mutex" })]
		private ValueTuple<string, Mutex> CreateUniqueDirectoryForProcess()
		{
			string text = Guid.NewGuid().ToString("N").ToLowerInvariant();
			string text2 = Path.Combine(this._baseDirectory, text);
			Mutex mutex = new Mutex(false, text);
			Directory.CreateDirectory(text2);
			return new ValueTuple<string, Mutex>(text2, mutex);
		}

		// Token: 0x0400041F RID: 1055
		private readonly string _baseDirectory;

		// Token: 0x04000420 RID: 1056
		internal readonly Task DeleteLeftoverDirectoriesTask;

		// Token: 0x04000421 RID: 1057
		[TupleElementNames(new string[] { "directory", null })]
		private readonly Lazy<ValueTuple<string, Mutex>> _shadowCopyDirectoryAndMutex;

		// Token: 0x04000422 RID: 1058
		private int _assemblyDirectoryId;
	}
}
