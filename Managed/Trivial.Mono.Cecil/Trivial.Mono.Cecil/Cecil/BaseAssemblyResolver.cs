using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000019 RID: 25
	public abstract class BaseAssemblyResolver : IAssemblyResolver, IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000BF RID: 191 RVA: 0x0000385C File Offset: 0x00001A5C
		// (remove) Token: 0x060000C0 RID: 192 RVA: 0x00003894 File Offset: 0x00001A94
		public event AssemblyResolveEventHandler ResolveFailure;

		// Token: 0x060000C1 RID: 193 RVA: 0x000038C9 File Offset: 0x00001AC9
		public void AddSearchDirectory(string directory)
		{
			this.directories.Add(directory);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000038D7 File Offset: 0x00001AD7
		public void RemoveSearchDirectory(string directory)
		{
			this.directories.Remove(directory);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000038E8 File Offset: 0x00001AE8
		public string[] GetSearchDirectories()
		{
			string[] array = new string[this.directories.size];
			Array.Copy(this.directories.items, array, array.Length);
			return array;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000391B File Offset: 0x00001B1B
		protected BaseAssemblyResolver()
		{
			this.directories = new Collection<string>(2) { ".", "bin" };
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003945 File Offset: 0x00001B45
		private AssemblyDefinition GetAssembly(string file, ReaderParameters parameters)
		{
			if (parameters.AssemblyResolver == null)
			{
				parameters.AssemblyResolver = this;
			}
			return ModuleDefinition.ReadModule(file, parameters).Assembly;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003962 File Offset: 0x00001B62
		public virtual AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			return this.Resolve(name, new ReaderParameters());
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003970 File Offset: 0x00001B70
		public virtual AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
		{
			Mixin.CheckName(name);
			Mixin.CheckParameters(parameters);
			AssemblyDefinition assemblyDefinition = this.SearchDirectory(name, this.directories, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			if (name.IsRetargetable)
			{
				name = new AssemblyNameReference(name.Name, Mixin.ZeroVersion)
				{
					PublicKeyToken = Empty<byte>.Array
				};
			}
			string directoryName = Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName);
			string[] array2;
			if (BaseAssemblyResolver.on_mono)
			{
				string[] array = new string[2];
				array[0] = directoryName;
				array2 = array;
				array[1] = Path.Combine(directoryName, "Facades");
			}
			else
			{
				(array2 = new string[1])[0] = directoryName;
			}
			string[] array3 = array2;
			if (BaseAssemblyResolver.IsZero(name.Version))
			{
				assemblyDefinition = this.SearchDirectory(name, array3, parameters);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			if (name.Name == "mscorlib")
			{
				assemblyDefinition = this.GetCorlib(name, parameters);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			assemblyDefinition = this.GetAssemblyInGac(name, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			assemblyDefinition = this.SearchDirectory(name, array3, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			if (this.ResolveFailure != null)
			{
				assemblyDefinition = this.ResolveFailure(this, name);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			throw new AssemblyResolutionException(name);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003A84 File Offset: 0x00001C84
		private AssemblyDefinition SearchDirectory(AssemblyNameReference name, IEnumerable<string> directories, ReaderParameters parameters)
		{
			string[] array2;
			if (name.IsWindowsRuntime)
			{
				string[] array = new string[2];
				array[0] = ".winmd";
				array2 = array;
				array[1] = ".dll";
			}
			else
			{
				string[] array3 = new string[2];
				array3[0] = ".exe";
				array2 = array3;
				array3[1] = ".dll";
			}
			string[] array4 = array2;
			foreach (string text in directories)
			{
				string[] array5 = array4;
				foreach (string text2 in array5)
				{
					string text3 = Path.Combine(text, name.Name + text2);
					if (File.Exists(text3))
					{
						try
						{
							return this.GetAssembly(text3, parameters);
						}
						catch (BadImageFormatException)
						{
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003B5C File Offset: 0x00001D5C
		private static bool IsZero(Version version)
		{
			return version.Major == 0 && version.Minor == 0 && version.Build == 0 && version.Revision == 0;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003B84 File Offset: 0x00001D84
		private AssemblyDefinition GetCorlib(AssemblyNameReference reference, ReaderParameters parameters)
		{
			Version version = reference.Version;
			if (typeof(object).Assembly.GetName().Version == version || BaseAssemblyResolver.IsZero(version))
			{
				return this.GetAssembly(typeof(object).Module.FullyQualifiedName, parameters);
			}
			string text = Directory.GetParent(Directory.GetParent(typeof(object).Module.FullyQualifiedName).FullName).FullName;
			if (!BaseAssemblyResolver.on_mono)
			{
				switch (version.Major)
				{
				case 1:
					text = ((version.MajorRevision != 3300) ? Path.Combine(text, "v1.0.5000.0") : Path.Combine(text, "v1.0.3705"));
					goto IL_0164;
				case 2:
					text = Path.Combine(text, "v2.0.50727");
					goto IL_0164;
				case 4:
					text = Path.Combine(text, "v4.0.30319");
					goto IL_0164;
				}
				throw new NotSupportedException("Version not supported: " + version);
			}
			if (version.Major == 1)
			{
				text = Path.Combine(text, "1.0");
			}
			else if (version.Major == 2)
			{
				text = ((version.MajorRevision != 5) ? Path.Combine(text, "2.0") : Path.Combine(text, "2.1"));
			}
			else
			{
				if (version.Major != 4)
				{
					throw new NotSupportedException("Version not supported: " + version);
				}
				text = Path.Combine(text, "4.0");
			}
			IL_0164:
			string text2 = Path.Combine(text, "mscorlib.dll");
			if (File.Exists(text2))
			{
				return this.GetAssembly(text2, parameters);
			}
			return null;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003D14 File Offset: 0x00001F14
		private static Collection<string> GetGacPaths()
		{
			if (BaseAssemblyResolver.on_mono)
			{
				return BaseAssemblyResolver.GetDefaultMonoGacPaths();
			}
			Collection<string> collection = new Collection<string>(2);
			string environmentVariable = Environment.GetEnvironmentVariable("WINDIR");
			if (environmentVariable == null)
			{
				return collection;
			}
			collection.Add(Path.Combine(environmentVariable, "assembly"));
			collection.Add(Path.Combine(environmentVariable, Path.Combine("Microsoft.NET", "assembly")));
			return collection;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003D74 File Offset: 0x00001F74
		private static Collection<string> GetDefaultMonoGacPaths()
		{
			Collection<string> collection = new Collection<string>(1);
			string currentMonoGac = BaseAssemblyResolver.GetCurrentMonoGac();
			if (currentMonoGac != null)
			{
				collection.Add(currentMonoGac);
			}
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_GAC_PREFIX");
			if (string.IsNullOrEmpty(environmentVariable))
			{
				return collection;
			}
			string[] array = environmentVariable.Split(new char[] { Path.PathSeparator });
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = Path.Combine(Path.Combine(Path.Combine(text, "lib"), "mono"), "gac");
					if (Directory.Exists(text2) && !collection.Contains(currentMonoGac))
					{
						collection.Add(text2);
					}
				}
			}
			return collection;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003E25 File Offset: 0x00002025
		private static string GetCurrentMonoGac()
		{
			return Path.Combine(Directory.GetParent(Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName)).FullName, "gac");
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003E54 File Offset: 0x00002054
		private AssemblyDefinition GetAssemblyInGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			if (reference.PublicKeyToken == null || reference.PublicKeyToken.Length == 0)
			{
				return null;
			}
			if (this.gac_paths == null)
			{
				this.gac_paths = BaseAssemblyResolver.GetGacPaths();
			}
			if (BaseAssemblyResolver.on_mono)
			{
				return this.GetAssemblyInMonoGac(reference, parameters);
			}
			return this.GetAssemblyInNetGac(reference, parameters);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003E94 File Offset: 0x00002094
		private AssemblyDefinition GetAssemblyInMonoGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			for (int i = 0; i < this.gac_paths.Count; i++)
			{
				string text = this.gac_paths[i];
				string assemblyFile = BaseAssemblyResolver.GetAssemblyFile(reference, string.Empty, text);
				if (File.Exists(assemblyFile))
				{
					return this.GetAssembly(assemblyFile, parameters);
				}
			}
			return null;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003EE4 File Offset: 0x000020E4
		private AssemblyDefinition GetAssemblyInNetGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			string[] array = new string[] { "GAC_MSIL", "GAC_32", "GAC_64", "GAC" };
			string[] array2 = new string[]
			{
				string.Empty,
				"v4.0_"
			};
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					string text = Path.Combine(this.gac_paths[i], array[j]);
					string assemblyFile = BaseAssemblyResolver.GetAssemblyFile(reference, array2[i], text);
					if (Directory.Exists(text) && File.Exists(assemblyFile))
					{
						return this.GetAssembly(assemblyFile, parameters);
					}
				}
			}
			return null;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003F8C File Offset: 0x0000218C
		private static string GetAssemblyFile(AssemblyNameReference reference, string prefix, string gac)
		{
			StringBuilder stringBuilder = new StringBuilder().Append(prefix).Append(reference.Version).Append("__");
			for (int i = 0; i < reference.PublicKeyToken.Length; i++)
			{
				stringBuilder.Append(reference.PublicKeyToken[i].ToString("x2"));
			}
			return Path.Combine(Path.Combine(Path.Combine(gac, reference.Name), stringBuilder.ToString()), reference.Name + ".dll");
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004016 File Offset: 0x00002216
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002921 File Offset: 0x00000B21
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0400002E RID: 46
		private static readonly bool on_mono = Type.GetType("Mono.Runtime") != null;

		// Token: 0x0400002F RID: 47
		private readonly Collection<string> directories;

		// Token: 0x04000030 RID: 48
		private Collection<string> gac_paths;
	}
}
