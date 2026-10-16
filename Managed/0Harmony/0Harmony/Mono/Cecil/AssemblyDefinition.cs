using System;
using System.IO;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001B2 RID: 434
	public sealed class AssemblyDefinition : ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider, IDisposable
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0001ABA3 File Offset: 0x00018DA3
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x0001ABAB File Offset: 0x00018DAB
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0001ABB4 File Offset: 0x00018DB4
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x0001ABCF File Offset: 0x00018DCF
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x00018659 File Offset: 0x00016859
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0001ABDC File Offset: 0x00018DDC
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
				Interlocked.CompareExchange<Collection<ModuleDefinition>>(ref this.modules, new Collection<ModuleDefinition>(1) { this.main_module }, null);
				return this.modules;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0001AC5C File Offset: 0x00018E5C
		public ModuleDefinition MainModule
		{
			get
			{
				return this.main_module;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0001AC64 File Offset: 0x00018E64
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0001AC71 File Offset: 0x00018E71
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

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x0001AC7F File Offset: 0x00018E7F
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001ACA4 File Offset: 0x00018EA4
		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				return this.custom_attributes ?? this.GetCustomAttributes(ref this.custom_attributes, this.main_module);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001ACC2 File Offset: 0x00018EC2
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001ACE7 File Offset: 0x00018EE7
		public Collection<SecurityDeclaration> SecurityDeclarations
		{
			get
			{
				return this.security_declarations ?? this.GetSecurityDeclarations(ref this.security_declarations, this.main_module);
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00002AED File Offset: 0x00000CED
		internal AssemblyDefinition()
		{
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001AD08 File Offset: 0x00018F08
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

		// Token: 0x060007BD RID: 1981 RVA: 0x0001AD4D File Offset: 0x00018F4D
		public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleKind kind)
		{
			return AssemblyDefinition.CreateAssembly(assemblyName, moduleName, new ModuleParameters
			{
				Kind = kind
			});
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001AD64 File Offset: 0x00018F64
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

		// Token: 0x060007BF RID: 1983 RVA: 0x0001ADBA File Offset: 0x00018FBA
		public static AssemblyDefinition ReadAssembly(string fileName)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(fileName));
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001ADC7 File Offset: 0x00018FC7
		public static AssemblyDefinition ReadAssembly(string fileName, ReaderParameters parameters)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(fileName, parameters));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001ADD5 File Offset: 0x00018FD5
		public static AssemblyDefinition ReadAssembly(Stream stream)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(stream));
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001ADE2 File Offset: 0x00018FE2
		public static AssemblyDefinition ReadAssembly(Stream stream, ReaderParameters parameters)
		{
			return AssemblyDefinition.ReadAssembly(ModuleDefinition.ReadModule(stream, parameters));
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		private static AssemblyDefinition ReadAssembly(ModuleDefinition module)
		{
			AssemblyDefinition assembly = module.Assembly;
			if (assembly == null)
			{
				throw new ArgumentException();
			}
			return assembly;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001AE01 File Offset: 0x00019001
		public void Write(string fileName)
		{
			this.Write(fileName, new WriterParameters());
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001AE0F File Offset: 0x0001900F
		public void Write(string fileName, WriterParameters parameters)
		{
			this.main_module.Write(fileName, parameters);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001AE1E File Offset: 0x0001901E
		public void Write()
		{
			this.main_module.Write();
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001AE2B File Offset: 0x0001902B
		public void Write(WriterParameters parameters)
		{
			this.main_module.Write(parameters);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001AE39 File Offset: 0x00019039
		public void Write(Stream stream)
		{
			this.Write(stream, new WriterParameters());
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001AE47 File Offset: 0x00019047
		public void Write(Stream stream, WriterParameters parameters)
		{
			this.main_module.Write(stream, parameters);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001AE56 File Offset: 0x00019056
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x0400026D RID: 621
		private AssemblyNameDefinition name;

		// Token: 0x0400026E RID: 622
		internal ModuleDefinition main_module;

		// Token: 0x0400026F RID: 623
		private Collection<ModuleDefinition> modules;

		// Token: 0x04000270 RID: 624
		private Collection<CustomAttribute> custom_attributes;

		// Token: 0x04000271 RID: 625
		private Collection<SecurityDeclaration> security_declarations;
	}
}
