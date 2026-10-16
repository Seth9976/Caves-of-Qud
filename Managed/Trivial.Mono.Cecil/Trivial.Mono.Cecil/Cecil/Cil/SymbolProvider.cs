using System;
using System.IO;
using System.Reflection;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200012F RID: 303
	internal static class SymbolProvider
	{
		// Token: 0x06000B5B RID: 2907 RVA: 0x000259D8 File Offset: 0x00023BD8
		private static AssemblyName GetSymbolAssemblyName(SymbolKind kind)
		{
			if (kind == SymbolKind.PortablePdb)
			{
				throw new ArgumentException();
			}
			string symbolNamespace = SymbolProvider.GetSymbolNamespace(kind);
			AssemblyName name = typeof(SymbolProvider).Assembly().GetName();
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Name = name.Name + "." + symbolNamespace;
			assemblyName.Version = name.Version;
			assemblyName.SetPublicKeyToken(name.GetPublicKeyToken());
			return assemblyName;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00025A44 File Offset: 0x00023C44
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

		// Token: 0x06000B5D RID: 2909 RVA: 0x00025ABC File Offset: 0x00023CBC
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

		// Token: 0x06000B5E RID: 2910 RVA: 0x00025B12 File Offset: 0x00023D12
		private static string GetSymbolTypeName(SymbolKind kind, string name)
		{
			return string.Concat(new object[]
			{
				"Trivial.Mono.Cecil.",
				SymbolProvider.GetSymbolNamespace(kind),
				".",
				kind,
				name
			});
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00025B45 File Offset: 0x00023D45
		private static string GetSymbolNamespace(SymbolKind kind)
		{
			switch (kind)
			{
			case SymbolKind.NativePdb:
				return "Pdb";
			case SymbolKind.PortablePdb:
			case SymbolKind.EmbeddedPortablePdb:
				return "Cil";
			case SymbolKind.Mdb:
				return "Mdb";
			default:
				throw new ArgumentException();
			}
		}
	}
}
