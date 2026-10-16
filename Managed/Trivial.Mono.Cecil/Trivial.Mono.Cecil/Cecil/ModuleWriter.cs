using System;
using System.IO;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000093 RID: 147
	internal static class ModuleWriter
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00014184 File Offset: 0x00012384
		public static void WriteModule(ModuleDefinition module, Disposable<Stream> stream, WriterParameters parameters)
		{
			using (stream)
			{
				ModuleWriter.Write(module, stream, parameters);
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000141BC File Offset: 0x000123BC
		private static void Write(ModuleDefinition module, Disposable<Stream> stream, WriterParameters parameters)
		{
			if ((module.Attributes & ModuleAttributes.ILOnly) == (ModuleAttributes)0)
			{
				throw new NotSupportedException("Writing mixed-mode assemblies is not supported");
			}
			if (module.HasImage && module.ReadingMode == ReadingMode.Deferred)
			{
				ImmediateModuleReader immediateModuleReader = new ImmediateModuleReader(module.Image);
				immediateModuleReader.ReadModule(module, false);
				immediateModuleReader.ReadSymbols(module);
			}
			module.MetadataSystem.Clear();
			if (module.symbol_reader != null)
			{
				module.symbol_reader.Dispose();
			}
			AssemblyNameDefinition assemblyNameDefinition = ((module.assembly != null) ? module.assembly.Name : null);
			string fileName = stream.value.GetFileName();
			uint num = parameters.Timestamp ?? module.timestamp;
			ISymbolWriterProvider symbolWriterProvider = parameters.SymbolWriterProvider;
			if (symbolWriterProvider == null && parameters.WriteSymbols)
			{
				symbolWriterProvider = new DefaultSymbolWriterProvider();
			}
			if (parameters.StrongNameKeyPair != null && assemblyNameDefinition != null)
			{
				assemblyNameDefinition.PublicKey = parameters.StrongNameKeyPair.PublicKey;
				module.Attributes |= ModuleAttributes.StrongNameSigned;
			}
			using (ISymbolWriter symbolWriter = ModuleWriter.GetSymbolWriter(module, fileName, symbolWriterProvider, parameters))
			{
				MetadataBuilder metadataBuilder = new MetadataBuilder(module, fileName, num, symbolWriterProvider, symbolWriter);
				ModuleWriter.BuildMetadata(module, metadataBuilder);
				ImageWriter imageWriter = ImageWriter.CreateWriter(module, metadataBuilder, stream);
				stream.value.SetLength(0L);
				imageWriter.WriteImage();
				if (parameters.StrongNameKeyPair != null)
				{
					CryptoService.StrongName(stream.value, imageWriter, parameters.StrongNameKeyPair);
				}
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001432C File Offset: 0x0001252C
		private static void BuildMetadata(ModuleDefinition module, MetadataBuilder metadata)
		{
			if (!module.HasImage)
			{
				metadata.BuildMetadata();
				return;
			}
			module.Read<MetadataBuilder, MetadataBuilder>(metadata, delegate(MetadataBuilder builder, MetadataReader _)
			{
				builder.BuildMetadata();
				return builder;
			});
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00014364 File Offset: 0x00012564
		private static ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fq_name, ISymbolWriterProvider symbol_writer_provider, WriterParameters parameters)
		{
			if (symbol_writer_provider == null)
			{
				return null;
			}
			if (parameters.SymbolStream != null)
			{
				return symbol_writer_provider.GetSymbolWriter(module, parameters.SymbolStream);
			}
			return symbol_writer_provider.GetSymbolWriter(module, fq_name);
		}
	}
}
