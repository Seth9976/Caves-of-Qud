using System;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200008F RID: 143
	internal abstract class ModuleReader
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x00013E95 File Offset: 0x00012095
		protected ModuleReader(Image image, ReadingMode mode)
		{
			this.module = new ModuleDefinition(image);
			this.module.ReadingMode = mode;
		}

		// Token: 0x0600062E RID: 1582
		protected abstract void ReadModule();

		// Token: 0x0600062F RID: 1583
		public abstract void ReadSymbols(ModuleDefinition module);

		// Token: 0x06000630 RID: 1584 RVA: 0x00013EB5 File Offset: 0x000120B5
		protected void ReadModuleManifest(MetadataReader reader)
		{
			reader.Populate(this.module);
			this.ReadAssembly(reader);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00013ECC File Offset: 0x000120CC
		private void ReadAssembly(MetadataReader reader)
		{
			AssemblyNameDefinition assemblyNameDefinition = reader.ReadAssemblyNameDefinition();
			if (assemblyNameDefinition == null)
			{
				this.module.kind = ModuleKind.NetModule;
				return;
			}
			AssemblyDefinition assemblyDefinition = new AssemblyDefinition();
			assemblyDefinition.Name = assemblyNameDefinition;
			this.module.assembly = assemblyDefinition;
			assemblyDefinition.main_module = this.module;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00013F18 File Offset: 0x00012118
		public static ModuleDefinition CreateModule(Image image, ReaderParameters parameters)
		{
			ModuleReader moduleReader = ModuleReader.CreateModuleReader(image, parameters.ReadingMode);
			ModuleDefinition moduleDefinition = moduleReader.module;
			if (parameters.assembly_resolver != null)
			{
				moduleDefinition.assembly_resolver = Disposable.NotOwned<IAssemblyResolver>(parameters.assembly_resolver);
			}
			if (parameters.metadata_resolver != null)
			{
				moduleDefinition.metadata_resolver = parameters.metadata_resolver;
			}
			if (parameters.metadata_importer_provider != null)
			{
				moduleDefinition.metadata_importer = parameters.metadata_importer_provider.GetMetadataImporter(moduleDefinition);
			}
			if (parameters.reflection_importer_provider != null)
			{
				moduleDefinition.reflection_importer = parameters.reflection_importer_provider.GetReflectionImporter(moduleDefinition);
			}
			ModuleReader.GetMetadataKind(moduleDefinition, parameters);
			moduleReader.ReadModule();
			ModuleReader.ReadSymbols(moduleDefinition, parameters);
			moduleReader.ReadSymbols(moduleDefinition);
			if (parameters.ReadingMode == ReadingMode.Immediate)
			{
				moduleDefinition.MetadataSystem.Clear();
			}
			return moduleDefinition;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00013FCC File Offset: 0x000121CC
		private static void ReadSymbols(ModuleDefinition module, ReaderParameters parameters)
		{
			ISymbolReaderProvider symbolReaderProvider = parameters.SymbolReaderProvider;
			if (symbolReaderProvider == null && parameters.ReadSymbols)
			{
				symbolReaderProvider = new DefaultSymbolReaderProvider();
			}
			if (symbolReaderProvider != null)
			{
				module.SymbolReaderProvider = symbolReaderProvider;
				ISymbolReader symbolReader = ((parameters.SymbolStream != null) ? symbolReaderProvider.GetSymbolReader(module, parameters.SymbolStream) : symbolReaderProvider.GetSymbolReader(module, module.FileName));
				if (symbolReader != null)
				{
					module.ReadSymbols(symbolReader);
				}
			}
			if (module.Image.HasDebugTables())
			{
				module.ReadSymbols(new PortablePdbReader(module.Image, module));
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001404C File Offset: 0x0001224C
		private static void GetMetadataKind(ModuleDefinition module, ReaderParameters parameters)
		{
			if (!parameters.ApplyWindowsRuntimeProjections)
			{
				module.MetadataKind = MetadataKind.Ecma335;
				return;
			}
			string runtimeVersion = module.RuntimeVersion;
			if (!runtimeVersion.Contains("WindowsRuntime"))
			{
				module.MetadataKind = MetadataKind.Ecma335;
				return;
			}
			if (runtimeVersion.Contains("CLR"))
			{
				module.MetadataKind = MetadataKind.ManagedWindowsMetadata;
				return;
			}
			module.MetadataKind = MetadataKind.WindowsMetadata;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000140A1 File Offset: 0x000122A1
		private static ModuleReader CreateModuleReader(Image image, ReadingMode mode)
		{
			if (mode == ReadingMode.Immediate)
			{
				return new ImmediateModuleReader(image);
			}
			if (mode != ReadingMode.Deferred)
			{
				throw new ArgumentException();
			}
			return new DeferredModuleReader(image);
		}

		// Token: 0x040001DC RID: 476
		protected readonly ModuleDefinition module;
	}
}
