using System;
using System.IO;
using System.Reflection;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002F1 RID: 753
	internal static class SymbolProvider
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x0003D584 File Offset: 0x0003B784
		private static AssemblyName GetSymbolAssemblyName(SymbolKind kind)
		{
			if (kind == SymbolKind.PortablePdb)
			{
				throw new ArgumentException();
			}
			string symbolNamespace = SymbolProvider.GetSymbolNamespace(kind);
			AssemblyName name = typeof(SymbolProvider).Assembly.GetName();
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = name.Name + "." + symbolNamespace;
			assemblyName.Version = name.Version;
			assemblyName.CultureInfo = name.CultureInfo;
			assemblyName.SetPublicKeyToken(name.GetPublicKeyToken());
			return assemblyName;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0003D5F8 File Offset: 0x0003B7F8
		private static Type GetSymbolType(SymbolKind kind, string fullname)
		{
			Type type = Type.GetType(fullname);
			if (type != null)
			{
				return type;
			}
			AssemblyName symbolAssemblyName = SymbolProvider.GetSymbolAssemblyName(kind);
			type = Type.GetType(fullname + ", " + symbolAssemblyName.FullName);
			if (type != null)
			{
				return type;
			}
			try
			{
				Assembly assembly = Assembly.Load(symbolAssemblyName);
				if (assembly != null)
				{
					return assembly.GetType(fullname);
				}
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
			}
			return null;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0003D684 File Offset: 0x0003B884
		public static ISymbolReaderProvider GetReaderProvider(SymbolKind kind)
		{
			if (kind == SymbolKind.PortablePdb)
			{
				return new PortablePdbReaderProvider();
			}
			if (kind == SymbolKind.EmbeddedPortablePdb)
			{
				return new EmbeddedPortablePdbReaderProvider();
			}
			string symbolTypeName = SymbolProvider.GetSymbolTypeName(kind, "ReaderProvider");
			Type symbolType = SymbolProvider.GetSymbolType(kind, symbolTypeName);
			if (symbolType == null)
			{
				throw new TypeLoadException("Could not find symbol provider type " + symbolTypeName);
			}
			return (ISymbolReaderProvider)Activator.CreateInstance(symbolType);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0003D6DC File Offset: 0x0003B8DC
		private static string GetSymbolTypeName(SymbolKind kind, string name)
		{
			return string.Concat(new string[]
			{
				"Mono.Cecil.",
				SymbolProvider.GetSymbolNamespace(kind),
				".",
				kind.ToString(),
				name
			});
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003D716 File Offset: 0x0003B916
		private static string GetSymbolNamespace(SymbolKind kind)
		{
			if (kind == SymbolKind.PortablePdb || kind == SymbolKind.EmbeddedPortablePdb)
			{
				return "Cil";
			}
			if (kind == SymbolKind.NativePdb)
			{
				return "Pdb";
			}
			if (kind == SymbolKind.Mdb)
			{
				return "Mdb";
			}
			throw new ArgumentException();
		}
	}
}
