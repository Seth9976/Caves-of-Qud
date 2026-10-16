using System;
using System.IO;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200000F RID: 15
	public sealed class AssemblyDefinition : ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider, IDisposable
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002E06 File Offset: 0x00001006
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002E0E File Offset: 0x0000100E
		public AssemblyNameDefinition Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002E17 File Offset: 0x00001017
		public string FullName
		{
			get
			{
				if (this.name == null)
				{
					return string.Empty;
				}
				return this.name.FullName;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E32 File Offset: 0x00001032
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002921 File Offset: 0x00000B21
		public MetadataToken MetadataToken
		{
			get
			{
				return new MetadataToken(TokenType.Assembly, 1);
			}
			set
			{
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E40 File Offset: 0x00001040
		public Collection<ModuleDefinition> Modules
		{
			get
			{
				if (this.modules != null)
				{
					return this.modules;
				}
				if (this.main_module.HasImage)
				{
					return this.main_module.Read<AssemblyDefinition, Collection<ModuleDefinition>>(ref this.modules, this, (AssemblyDefinition _, MetadataReader reader) => reader.ReadModules());
				}
				Collection<ModuleDefinition> collection = new Collection<ModuleDefinition>(1);
				collection.Add(this.main_module);
				Collection<ModuleDefinition> collection2 = collection;
				this.modules = collection;
				return collection2;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002EB6 File Offset: 0x000010B6
		public ModuleDefinition MainModule
		{
			get
			{
				return this.main_module;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002ECB File Offset: 0x000010CB
		public MethodDefinition EntryPoint
		{
			get
			{
				return this.main_module.EntryPoint;
			}
			set
			{
				this.main_module.EntryPoint = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002ED9 File Offset: 0x000010D9
		public bool HasCustomAttributes
		{
			get
			{
				if (this.custom_attributes != null)
				{
					return this.custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this.main_module);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002EFE File Offset: 0x000010FE
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.main_module);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002F1C File Offset: 0x0000111C
		public bool HasSecurityDeclarations
		{
			get
			{
				if (this.security_declarations != null)
				{
					return this.security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(this.main_module);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002F41 File Offset: 0x00001141
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.main_module);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F5F File Offset: 0x0000115F
		internal AssemblyDefinition()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F68 File Offset: 0x00001168
		public void Dispose()
		{
			if (this.modules == null)
			{
				this.main_module.Dispose();
				return;
			}
			Collection<ModuleDefinition> collection = this.Modules;
			for (int i = 0; i < collection.Count; i++)
			{
				collection[i].Dispose();
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002FAD File Offset: 0x000011AD
		public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleKind kind)
		{
			return AssemblyDefinition.CreateAssembly(assemblyName, moduleName, new ModuleParameters
			{
				Kind = kind
			});
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002FC4 File Offset: 0x000011C4
		public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleParameters parameters)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (moduleName == null)
			{
				throw new ArgumentNullException("moduleName");
			}
			Mixin.CheckParameters(parameters);
			if (parameters.Kind == ModuleKind.NetModule)
			{
				throw new ArgumentException("kind");
			}
			AssemblyDefinition assembly = ModuleDefinition.CreateModule(moduleName, parameters).Assembly;
			assembly.Name = assemblyName;
			return assembly;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000301C File Offset: 0x0000121C
		public static AssemblyDefinition ReadAssembly(string fileName)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(fileName));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003029 File Offset: 0x00001229
		public static AssemblyDefinition ReadAssembly(string fileName, ReaderParameters parameters)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(fileName, parameters));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003037 File Offset: 0x00001237
		public static AssemblyDefinition ReadAssembly(Stream stream)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(stream));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003044 File Offset: 0x00001244
		public static AssemblyDefinition ReadAssembly(Stream stream, ReaderParameters parameters)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(stream, parameters));
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003054 File Offset: 0x00001254
		private static AssemblyDefinition ReadAssembly(ModuleDefinition module)
		{
			AssemblyDefinition assembly = module.Assembly;
			if (assembly == null)
			{
				throw new ArgumentException();
			}
			return assembly;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003072 File Offset: 0x00001272
		public void Write(string fileName)
		{
			this.Write(fileName, new WriterParameters());
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003080 File Offset: 0x00001280
		public void Write(string fileName, WriterParameters parameters)
		{
			this.main_module.Write(fileName, parameters);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000308F File Offset: 0x0000128F
		public void Write()
		{
			this.main_module.Write();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000309C File Offset: 0x0000129C
		public void Write(WriterParameters parameters)
		{
			this.main_module.Write(parameters);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000030AA File Offset: 0x000012AA
		public void Write(Stream stream)
		{
			this.Write(stream, new WriterParameters());
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030B8 File Offset: 0x000012B8
		public void Write(Stream stream, WriterParameters parameters)
		{
			this.main_module.Write(stream, parameters);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000030C7 File Offset: 0x000012C7
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000018 RID: 24
		private AssemblyNameDefinition name;

		// Token: 0x04000019 RID: 25
		internal ModuleDefinition main_module;

		// Token: 0x0400001A RID: 26
		private Collection<ModuleDefinition> modules;

		// Token: 0x0400001B RID: 27
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x0400001C RID: 28
		private Collection<SecurityDeclaration> security_declarations;
	}
}
