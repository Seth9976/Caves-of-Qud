using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Cecil.PE;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200006E RID: 110
	internal sealed class MetadataBuilder
	{
		// Token: 0x060002FD RID: 765 RVA: 0x00007890 File Offset: 0x00005A90
		public MetadataBuilder(ModuleDefinition module, string fq_name, uint timestamp, ISymbolWriterProvider symbol_writer_provider, ISymbolWriter symbol_writer)
		{
			this.module = module;
			this.text_map = this.CreateTextMap();
			this.fq_name = fq_name;
			this.timestamp = timestamp;
			this.symbol_writer_provider = symbol_writer_provider;
			if (symbol_writer == null && module.HasImage && module.Image.HasDebugTables())
			{
				symbol_writer = new PortablePdbWriter(this, module);
			}
			this.symbol_writer = symbol_writer;
			IMetadataSymbolWriter metadataSymbolWriter = symbol_writer as IMetadataSymbolWriter;
			if (metadataSymbolWriter != null)
			{
				this.portable_pdb = true;
				metadataSymbolWriter.SetMetadata(this);
			}
			this.code = new CodeWriter(this);
			this.data = new DataBuffer();
			this.resources = new ResourceBuffer();
			this.string_heap = new StringHeapBuffer();
			this.guid_heap = new GuidHeapBuffer();
			this.user_string_heap = new UserStringHeapBuffer();
			this.blob_heap = new BlobHeapBuffer();
			this.table_heap = new TableHeapBuffer(module, this);
			this.type_ref_table = this.GetTable<TypeRefTable>(Table.TypeRef);
			this.type_def_table = this.GetTable<TypeDefTable>(Table.TypeDef);
			this.field_table = this.GetTable<FieldTable>(Table.Field);
			this.method_table = this.GetTable<MethodTable>(Table.Method);
			this.param_table = this.GetTable<ParamTable>(Table.Param);
			this.iface_impl_table = this.GetTable<InterfaceImplTable>(Table.InterfaceImpl);
			this.member_ref_table = this.GetTable<MemberRefTable>(Table.MemberRef);
			this.constant_table = this.GetTable<ConstantTable>(Table.Constant);
			this.custom_attribute_table = this.GetTable<CustomAttributeTable>(Table.CustomAttribute);
			this.declsec_table = this.GetTable<DeclSecurityTable>(Table.DeclSecurity);
			this.standalone_sig_table = this.GetTable<StandAloneSigTable>(Table.StandAloneSig);
			this.event_map_table = this.GetTable<EventMapTable>(Table.EventMap);
			this.event_table = this.GetTable<EventTable>(Table.Event);
			this.property_map_table = this.GetTable<PropertyMapTable>(Table.PropertyMap);
			this.property_table = this.GetTable<PropertyTable>(Table.Property);
			this.typespec_table = this.GetTable<TypeSpecTable>(Table.TypeSpec);
			this.method_spec_table = this.GetTable<MethodSpecTable>(Table.MethodSpec);
			RowEqualityComparer rowEqualityComparer = new RowEqualityComparer();
			this.type_ref_map = new Dictionary<Row<uint, uint, uint>, MetadataToken>(rowEqualityComparer);
			this.type_spec_map = new Dictionary<uint, MetadataToken>();
			this.member_ref_map = new Dictionary<Row<uint, uint, uint>, MetadataToken>(rowEqualityComparer);
			this.method_spec_map = new Dictionary<Row<uint, uint>, MetadataToken>(rowEqualityComparer);
			this.generic_parameters = new Collection<GenericParameter>();
			if (this.portable_pdb)
			{
				this.document_table = this.GetTable<DocumentTable>(Table.Document);
				this.method_debug_information_table = this.GetTable<MethodDebugInformationTable>(Table.MethodDebugInformation);
				this.local_scope_table = this.GetTable<LocalScopeTable>(Table.LocalScope);
				this.local_variable_table = this.GetTable<LocalVariableTable>(Table.LocalVariable);
				this.local_constant_table = this.GetTable<LocalConstantTable>(Table.LocalConstant);
				this.import_scope_table = this.GetTable<ImportScopeTable>(Table.ImportScope);
				this.state_machine_method_table = this.GetTable<StateMachineMethodTable>(Table.StateMachineMethod);
				this.custom_debug_information_table = this.GetTable<CustomDebugInformationTable>(Table.CustomDebugInformation);
				this.document_map = new Dictionary<string, MetadataToken>(StringComparer.Ordinal);
				this.import_scope_map = new Dictionary<Row<uint, uint>, MetadataToken>(rowEqualityComparer);
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00007B64 File Offset: 0x00005D64
		public MetadataBuilder(ModuleDefinition module, PortablePdbWriterProvider writer_provider)
		{
			this.module = module;
			this.text_map = new TextMap();
			this.symbol_writer_provider = writer_provider;
			this.portable_pdb = true;
			this.string_heap = new StringHeapBuffer();
			this.guid_heap = new GuidHeapBuffer();
			this.user_string_heap = new UserStringHeapBuffer();
			this.blob_heap = new BlobHeapBuffer();
			this.table_heap = new TableHeapBuffer(module, this);
			this.pdb_heap = new PdbHeapBuffer();
			this.document_table = this.GetTable<DocumentTable>(Table.Document);
			this.method_debug_information_table = this.GetTable<MethodDebugInformationTable>(Table.MethodDebugInformation);
			this.local_scope_table = this.GetTable<LocalScopeTable>(Table.LocalScope);
			this.local_variable_table = this.GetTable<LocalVariableTable>(Table.LocalVariable);
			this.local_constant_table = this.GetTable<LocalConstantTable>(Table.LocalConstant);
			this.import_scope_table = this.GetTable<ImportScopeTable>(Table.ImportScope);
			this.state_machine_method_table = this.GetTable<StateMachineMethodTable>(Table.StateMachineMethod);
			this.custom_debug_information_table = this.GetTable<CustomDebugInformationTable>(Table.CustomDebugInformation);
			RowEqualityComparer rowEqualityComparer = new RowEqualityComparer();
			this.document_map = new Dictionary<string, MetadataToken>();
			this.import_scope_map = new Dictionary<Row<uint, uint>, MetadataToken>(rowEqualityComparer);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00007CA0 File Offset: 0x00005EA0
		private TextMap CreateTextMap()
		{
			TextMap textMap = new TextMap();
			textMap.AddMap(TextSegment.ImportAddressTable, (this.module.Architecture == TargetArchitecture.I386) ? 8 : 0);
			textMap.AddMap(TextSegment.CLIHeader, 72, 8);
			return textMap;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00007CDB File Offset: 0x00005EDB
		private TTable GetTable<TTable>(Table table) where TTable : MetadataTable, new()
		{
			return this.table_heap.GetTable<TTable>(table);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00007CE9 File Offset: 0x00005EE9
		private uint GetStringIndex(string @string)
		{
			if (string.IsNullOrEmpty(@string))
			{
				return 0U;
			}
			return this.string_heap.GetStringIndex(@string);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00007D01 File Offset: 0x00005F01
		private uint GetGuidIndex(Guid guid)
		{
			return this.guid_heap.GetGuidIndex(guid);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00007D0F File Offset: 0x00005F0F
		private uint GetBlobIndex(ByteBuffer blob)
		{
			if (blob.length == 0)
			{
				return 0U;
			}
			return this.blob_heap.GetBlobIndex(blob);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00007D27 File Offset: 0x00005F27
		private uint GetBlobIndex(byte[] blob)
		{
			if (blob.IsNullOrEmpty<byte>())
			{
				return 0U;
			}
			return this.GetBlobIndex(new ByteBuffer(blob));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00007D3F File Offset: 0x00005F3F
		public void BuildMetadata()
		{
			this.BuildModule();
			this.table_heap.string_offsets = this.string_heap.WriteStrings();
			this.table_heap.ComputeTableInformations();
			this.table_heap.WriteTableHeap();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00007D74 File Offset: 0x00005F74
		private void BuildModule()
		{
			ModuleTable table = this.GetTable<ModuleTable>(Table.Module);
			table.row.Col1 = this.GetStringIndex(this.module.Name);
			table.row.Col2 = this.GetGuidIndex(this.module.Mvid);
			AssemblyDefinition assembly = this.module.Assembly;
			if (assembly != null)
			{
				this.BuildAssembly();
			}
			if (this.module.HasAssemblyReferences)
			{
				this.AddAssemblyReferences();
			}
			if (this.module.HasModuleReferences)
			{
				this.AddModuleReferences();
			}
			if (this.module.HasResources)
			{
				this.AddResources();
			}
			if (this.module.HasExportedTypes)
			{
				this.AddExportedTypes();
			}
			this.BuildTypes();
			if (assembly != null)
			{
				if (assembly.HasCustomAttributes)
				{
					this.AddCustomAttributes(assembly);
				}
				if (assembly.HasSecurityDeclarations)
				{
					this.AddSecurityDeclarations(assembly);
				}
			}
			if (this.module.HasCustomAttributes)
			{
				this.AddCustomAttributes(this.module);
			}
			if (this.module.EntryPoint != null)
			{
				this.entry_point = this.LookupToken(this.module.EntryPoint);
			}
			IMetadataSymbolWriter metadataSymbolWriter = this.symbol_writer as IMetadataSymbolWriter;
			if (metadataSymbolWriter == null)
			{
				return;
			}
			metadataSymbolWriter.WriteModule();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00007E9C File Offset: 0x0000609C
		private void BuildAssembly()
		{
			AssemblyDefinition assembly = this.module.Assembly;
			AssemblyNameDefinition name = assembly.Name;
			this.GetTable<AssemblyTable>(Table.Assembly).row = new Row<AssemblyHashAlgorithm, ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint>(name.HashAlgorithm, (ushort)name.Version.Major, (ushort)name.Version.Minor, (ushort)name.Version.Build, (ushort)name.Version.Revision, name.Attributes, this.GetBlobIndex(name.PublicKey), this.GetStringIndex(name.Name), this.GetStringIndex(name.Culture));
			if (assembly.Modules.Count > 1)
			{
				this.BuildModules();
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007F44 File Offset: 0x00006144
		private void BuildModules()
		{
			Collection<ModuleDefinition> modules = this.module.Assembly.Modules;
			FileTable table = this.GetTable<FileTable>(Table.File);
			for (int i = 0; i < modules.Count; i++)
			{
				ModuleDefinition moduleDefinition = modules[i];
				if (!moduleDefinition.IsMain)
				{
					WriterParameters writerParameters = new WriterParameters
					{
						SymbolWriterProvider = this.symbol_writer_provider
					};
					string moduleFileName = this.GetModuleFileName(moduleDefinition.Name);
					moduleDefinition.Write(moduleFileName, writerParameters);
					byte[] array = CryptoService.ComputeHash(moduleFileName);
					table.AddRow(new Row<FileAttributes, uint, uint>(FileAttributes.ContainsMetaData, this.GetStringIndex(moduleDefinition.Name), this.GetBlobIndex(array)));
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00007FE1 File Offset: 0x000061E1
		private string GetModuleFileName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new NotSupportedException();
			}
			return Path.Combine(Path.GetDirectoryName(this.fq_name), name);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008004 File Offset: 0x00006204
		private void AddAssemblyReferences()
		{
			Collection<AssemblyNameReference> assemblyReferences = this.module.AssemblyReferences;
			AssemblyRefTable table = this.GetTable<AssemblyRefTable>(Table.AssemblyRef);
			if (this.module.IsWindowsMetadata())
			{
				this.module.Projections.RemoveVirtualReferences(assemblyReferences);
			}
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				byte[] array = (assemblyNameReference.PublicKey.IsNullOrEmpty<byte>() ? assemblyNameReference.PublicKeyToken : assemblyNameReference.PublicKey);
				Version version = assemblyNameReference.Version;
				int num = table.AddRow(new Row<ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint, uint>((ushort)version.Major, (ushort)version.Minor, (ushort)version.Build, (ushort)version.Revision, assemblyNameReference.Attributes, this.GetBlobIndex(array), this.GetStringIndex(assemblyNameReference.Name), this.GetStringIndex(assemblyNameReference.Culture), this.GetBlobIndex(assemblyNameReference.Hash)));
				assemblyNameReference.token = new MetadataToken(TokenType.AssemblyRef, num);
			}
			if (this.module.IsWindowsMetadata())
			{
				this.module.Projections.AddVirtualReferences(assemblyReferences);
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00008118 File Offset: 0x00006318
		private void AddModuleReferences()
		{
			Collection<ModuleReference> moduleReferences = this.module.ModuleReferences;
			ModuleRefTable table = this.GetTable<ModuleRefTable>(Table.ModuleRef);
			for (int i = 0; i < moduleReferences.Count; i++)
			{
				ModuleReference moduleReference = moduleReferences[i];
				moduleReference.token = new MetadataToken(TokenType.ModuleRef, table.AddRow(this.GetStringIndex(moduleReference.Name)));
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00008178 File Offset: 0x00006378
		private void AddResources()
		{
			Collection<Resource> collection = this.module.Resources;
			ManifestResourceTable table = this.GetTable<ManifestResourceTable>(Table.ManifestResource);
			for (int i = 0; i < collection.Count; i++)
			{
				Resource resource = collection[i];
				Row<uint, ManifestResourceAttributes, uint, uint> row = new Row<uint, ManifestResourceAttributes, uint, uint>(0U, resource.Attributes, this.GetStringIndex(resource.Name), 0U);
				switch (resource.ResourceType)
				{
				case ResourceType.Linked:
					row.Col4 = CodedIndex.Implementation.CompressMetadataToken(new MetadataToken(TokenType.File, this.AddLinkedResource((LinkedResource)resource)));
					break;
				case ResourceType.Embedded:
					row.Col1 = this.AddEmbeddedResource((EmbeddedResource)resource);
					break;
				case ResourceType.AssemblyLinked:
					row.Col4 = CodedIndex.Implementation.CompressMetadataToken(((AssemblyLinkedResource)resource).Assembly.MetadataToken);
					break;
				default:
					throw new NotSupportedException();
				}
				table.AddRow(row);
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000825C File Offset: 0x0000645C
		private uint AddLinkedResource(LinkedResource resource)
		{
			FileTable table = this.GetTable<FileTable>(Table.File);
			byte[] array = resource.Hash;
			if (array.IsNullOrEmpty<byte>())
			{
				array = CryptoService.ComputeHash(resource.File);
			}
			return (uint)table.AddRow(new Row<FileAttributes, uint, uint>(FileAttributes.ContainsNoMetaData, this.GetStringIndex(resource.File), this.GetBlobIndex(array)));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000082AC File Offset: 0x000064AC
		private uint AddEmbeddedResource(EmbeddedResource resource)
		{
			return this.resources.AddResource(resource.GetResourceData());
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000082C0 File Offset: 0x000064C0
		private void AddExportedTypes()
		{
			Collection<ExportedType> exportedTypes = this.module.ExportedTypes;
			ExportedTypeTable table = this.GetTable<ExportedTypeTable>(Table.ExportedType);
			for (int i = 0; i < exportedTypes.Count; i++)
			{
				ExportedType exportedType = exportedTypes[i];
				int num = table.AddRow(new Row<TypeAttributes, uint, uint, uint, uint>(exportedType.Attributes, (uint)exportedType.Identifier, this.GetStringIndex(exportedType.Name), this.GetStringIndex(exportedType.Namespace), MetadataBuilder.MakeCodedRID(this.GetExportedTypeScope(exportedType), CodedIndex.Implementation)));
				exportedType.token = new MetadataToken(TokenType.ExportedType, num);
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000834C File Offset: 0x0000654C
		private MetadataToken GetExportedTypeScope(ExportedType exported_type)
		{
			if (exported_type.DeclaringType != null)
			{
				return exported_type.DeclaringType.MetadataToken;
			}
			IMetadataScope scope = exported_type.Scope;
			TokenType tokenType = scope.MetadataToken.TokenType;
			if (tokenType != TokenType.ModuleRef)
			{
				if (tokenType == TokenType.AssemblyRef)
				{
					return scope.MetadataToken;
				}
			}
			else
			{
				FileTable table = this.GetTable<FileTable>(Table.File);
				for (int i = 0; i < table.length; i++)
				{
					if (table.rows[i].Col2 == this.GetStringIndex(scope.Name))
					{
						return new MetadataToken(TokenType.File, i + 1);
					}
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000083EC File Offset: 0x000065EC
		private void BuildTypes()
		{
			if (this.module.HasTypes)
			{
				this.AttachTokens();
				this.AddTypes();
				this.AddGenericParameters();
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00008410 File Offset: 0x00006610
		private void AttachTokens()
		{
			Collection<TypeDefinition> types = this.module.Types;
			for (int i = 0; i < types.Count; i++)
			{
				this.AttachTypeToken(types[i]);
			}
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008448 File Offset: 0x00006648
		private void AttachTypeToken(TypeDefinition type)
		{
			TypeDefinitionProjection typeDefinitionProjection = WindowsRuntimeProjections.RemoveProjection(type);
			TokenType tokenType = TokenType.TypeDef;
			uint num = this.type_rid;
			this.type_rid = num + 1U;
			type.token = new MetadataToken(tokenType, num);
			type.fields_range.Start = this.field_rid;
			type.methods_range.Start = this.method_rid;
			if (type.HasFields)
			{
				this.AttachFieldsToken(type);
			}
			if (type.HasMethods)
			{
				this.AttachMethodsToken(type);
			}
			if (type.HasNestedTypes)
			{
				this.AttachNestedTypesToken(type);
			}
			WindowsRuntimeProjections.ApplyProjection(type, typeDefinitionProjection);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000084D4 File Offset: 0x000066D4
		private void AttachNestedTypesToken(TypeDefinition type)
		{
			Collection<TypeDefinition> nestedTypes = type.NestedTypes;
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				this.AttachTypeToken(nestedTypes[i]);
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008508 File Offset: 0x00006708
		private void AttachFieldsToken(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			type.fields_range.Length = (uint)fields.Count;
			for (int i = 0; i < fields.Count; i++)
			{
				MemberReference memberReference = fields[i];
				TokenType tokenType = TokenType.Field;
				uint num = this.field_rid;
				this.field_rid = num + 1U;
				memberReference.token = new MetadataToken(tokenType, num);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008568 File Offset: 0x00006768
		private void AttachMethodsToken(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			type.methods_range.Length = (uint)methods.Count;
			for (int i = 0; i < methods.Count; i++)
			{
				MemberReference memberReference = methods[i];
				TokenType tokenType = TokenType.Method;
				uint num = this.method_rid;
				this.method_rid = num + 1U;
				memberReference.token = new MetadataToken(tokenType, num);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000085C5 File Offset: 0x000067C5
		private MetadataToken GetTypeToken(TypeReference type)
		{
			if (type == null)
			{
				return MetadataToken.Zero;
			}
			if (type.IsDefinition)
			{
				return type.token;
			}
			if (type.IsTypeSpecification())
			{
				return this.GetTypeSpecToken(type);
			}
			return this.GetTypeRefToken(type);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000085F8 File Offset: 0x000067F8
		private MetadataToken GetTypeSpecToken(TypeReference type)
		{
			uint blobIndex = this.GetBlobIndex(this.GetTypeSpecSignature(type));
			MetadataToken metadataToken;
			if (this.type_spec_map.TryGetValue(blobIndex, ref metadataToken))
			{
				return metadataToken;
			}
			return this.AddTypeSpecification(type, blobIndex);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008630 File Offset: 0x00006830
		private MetadataToken AddTypeSpecification(TypeReference type, uint row)
		{
			type.token = new MetadataToken(TokenType.TypeSpec, this.typespec_table.AddRow(row));
			MetadataToken token = type.token;
			this.type_spec_map.Add(row, token);
			return token;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00008670 File Offset: 0x00006870
		private MetadataToken GetTypeRefToken(TypeReference type)
		{
			TypeReferenceProjection typeReferenceProjection = WindowsRuntimeProjections.RemoveProjection(type);
			Row<uint, uint, uint> row = this.CreateTypeRefRow(type);
			MetadataToken metadataToken;
			if (!this.type_ref_map.TryGetValue(row, ref metadataToken))
			{
				metadataToken = this.AddTypeReference(type, row);
			}
			WindowsRuntimeProjections.ApplyProjection(type, typeReferenceProjection);
			return metadataToken;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000086AD File Offset: 0x000068AD
		private Row<uint, uint, uint> CreateTypeRefRow(TypeReference type)
		{
			return new Row<uint, uint, uint>(MetadataBuilder.MakeCodedRID(this.GetScopeToken(type), CodedIndex.ResolutionScope), this.GetStringIndex(type.Name), this.GetStringIndex(type.Namespace));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000086DA File Offset: 0x000068DA
		private MetadataToken GetScopeToken(TypeReference type)
		{
			if (type.IsNested)
			{
				return this.GetTypeRefToken(type.DeclaringType);
			}
			IMetadataScope scope = type.Scope;
			if (scope == null)
			{
				return MetadataToken.Zero;
			}
			return scope.MetadataToken;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008706 File Offset: 0x00006906
		private static uint MakeCodedRID(IMetadataTokenProvider provider, CodedIndex index)
		{
			return MetadataBuilder.MakeCodedRID(provider.MetadataToken, index);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008714 File Offset: 0x00006914
		private static uint MakeCodedRID(MetadataToken token, CodedIndex index)
		{
			return index.CompressMetadataToken(token);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008720 File Offset: 0x00006920
		private MetadataToken AddTypeReference(TypeReference type, Row<uint, uint, uint> row)
		{
			type.token = new MetadataToken(TokenType.TypeRef, this.type_ref_table.AddRow(row));
			MetadataToken token = type.token;
			this.type_ref_map.Add(row, token);
			return token;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008760 File Offset: 0x00006960
		private void AddTypes()
		{
			Collection<TypeDefinition> types = this.module.Types;
			for (int i = 0; i < types.Count; i++)
			{
				this.AddType(types[i]);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00008798 File Offset: 0x00006998
		private void AddType(TypeDefinition type)
		{
			TypeDefinitionProjection typeDefinitionProjection = WindowsRuntimeProjections.RemoveProjection(type);
			this.type_def_table.AddRow(new Row<TypeAttributes, uint, uint, uint, uint, uint>(type.Attributes, this.GetStringIndex(type.Name), this.GetStringIndex(type.Namespace), MetadataBuilder.MakeCodedRID(this.GetTypeToken(type.BaseType), CodedIndex.TypeDefOrRef), type.fields_range.Start, type.methods_range.Start));
			if (type.HasGenericParameters)
			{
				this.AddGenericParameters(type);
			}
			if (type.HasInterfaces)
			{
				this.AddInterfaces(type);
			}
			if (type.HasLayoutInfo)
			{
				this.AddLayoutInfo(type);
			}
			if (type.HasFields)
			{
				this.AddFields(type);
			}
			if (type.HasMethods)
			{
				this.AddMethods(type);
			}
			if (type.HasProperties)
			{
				this.AddProperties(type);
			}
			if (type.HasEvents)
			{
				this.AddEvents(type);
			}
			if (type.HasCustomAttributes)
			{
				this.AddCustomAttributes(type);
			}
			if (type.HasSecurityDeclarations)
			{
				this.AddSecurityDeclarations(type);
			}
			if (type.HasNestedTypes)
			{
				this.AddNestedTypes(type);
			}
			WindowsRuntimeProjections.ApplyProjection(type, typeDefinitionProjection);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000088A0 File Offset: 0x00006AA0
		private void AddGenericParameters(IGenericParameterProvider owner)
		{
			Collection<GenericParameter> genericParameters = owner.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				this.generic_parameters.Add(genericParameters[i]);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000088D8 File Offset: 0x00006AD8
		private void AddGenericParameters()
		{
			GenericParameter[] items = this.generic_parameters.items;
			int size = this.generic_parameters.size;
			Array.Sort<GenericParameter>(items, 0, size, new MetadataBuilder.GenericParameterComparer());
			GenericParamTable table = this.GetTable<GenericParamTable>(Table.GenericParam);
			GenericParamConstraintTable table2 = this.GetTable<GenericParamConstraintTable>(Table.GenericParamConstraint);
			for (int i = 0; i < size; i++)
			{
				GenericParameter genericParameter = items[i];
				int num = table.AddRow(new Row<ushort, GenericParameterAttributes, uint, uint>((ushort)genericParameter.Position, genericParameter.Attributes, MetadataBuilder.MakeCodedRID(genericParameter.Owner, CodedIndex.TypeOrMethodDef), this.GetStringIndex(genericParameter.Name)));
				genericParameter.token = new MetadataToken(TokenType.GenericParam, num);
				if (genericParameter.HasConstraints)
				{
					this.AddConstraints(genericParameter, table2);
				}
				if (genericParameter.HasCustomAttributes)
				{
					this.AddCustomAttributes(genericParameter);
				}
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000089A0 File Offset: 0x00006BA0
		private void AddConstraints(GenericParameter generic_parameter, GenericParamConstraintTable table)
		{
			Collection<TypeReference> constraints = generic_parameter.Constraints;
			uint rid = generic_parameter.token.RID;
			for (int i = 0; i < constraints.Count; i++)
			{
				table.AddRow(new Row<uint, uint>(rid, MetadataBuilder.MakeCodedRID(this.GetTypeToken(constraints[i]), CodedIndex.TypeDefOrRef)));
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000089F4 File Offset: 0x00006BF4
		private void AddInterfaces(TypeDefinition type)
		{
			Collection<InterfaceImplementation> interfaces = type.Interfaces;
			uint rid = type.token.RID;
			for (int i = 0; i < interfaces.Count; i++)
			{
				InterfaceImplementation interfaceImplementation = interfaces[i];
				int num = this.iface_impl_table.AddRow(new Row<uint, uint>(rid, MetadataBuilder.MakeCodedRID(this.GetTypeToken(interfaceImplementation.InterfaceType), CodedIndex.TypeDefOrRef)));
				interfaceImplementation.token = new MetadataToken(TokenType.InterfaceImpl, num);
				if (interfaceImplementation.HasCustomAttributes)
				{
					this.AddCustomAttributes(interfaceImplementation);
				}
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008A73 File Offset: 0x00006C73
		private void AddLayoutInfo(TypeDefinition type)
		{
			this.GetTable<ClassLayoutTable>(Table.ClassLayout).AddRow(new Row<ushort, uint, uint>((ushort)type.PackingSize, (uint)type.ClassSize, type.token.RID));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008AA0 File Offset: 0x00006CA0
		private void AddNestedTypes(TypeDefinition type)
		{
			Collection<TypeDefinition> nestedTypes = type.NestedTypes;
			NestedClassTable table = this.GetTable<NestedClassTable>(Table.NestedClass);
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				TypeDefinition typeDefinition = nestedTypes[i];
				this.AddType(typeDefinition);
				table.AddRow(new Row<uint, uint>(typeDefinition.token.RID, type.token.RID));
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008B00 File Offset: 0x00006D00
		private void AddFields(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				this.AddField(fields[i]);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008B34 File Offset: 0x00006D34
		private void AddField(FieldDefinition field)
		{
			FieldDefinitionProjection fieldDefinitionProjection = WindowsRuntimeProjections.RemoveProjection(field);
			this.field_table.AddRow(new Row<FieldAttributes, uint, uint>(field.Attributes, this.GetStringIndex(field.Name), this.GetBlobIndex(this.GetFieldSignature(field))));
			if (!field.InitialValue.IsNullOrEmpty<byte>())
			{
				this.AddFieldRVA(field);
			}
			if (field.HasLayoutInfo)
			{
				this.AddFieldLayout(field);
			}
			if (field.HasCustomAttributes)
			{
				this.AddCustomAttributes(field);
			}
			if (field.HasConstant)
			{
				this.AddConstant(field, field.FieldType);
			}
			if (field.HasMarshalInfo)
			{
				this.AddMarshalInfo(field);
			}
			WindowsRuntimeProjections.ApplyProjection(field, fieldDefinitionProjection);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008BD5 File Offset: 0x00006DD5
		private void AddFieldRVA(FieldDefinition field)
		{
			this.GetTable<FieldRVATable>(Table.FieldRVA).AddRow(new Row<uint, uint>(this.data.AddData(field.InitialValue), field.token.RID));
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00008C06 File Offset: 0x00006E06
		private void AddFieldLayout(FieldDefinition field)
		{
			this.GetTable<FieldLayoutTable>(Table.FieldLayout).AddRow(new Row<uint, uint>((uint)field.Offset, field.token.RID));
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008C2C File Offset: 0x00006E2C
		private void AddMethods(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				this.AddMethod(methods[i]);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00008C60 File Offset: 0x00006E60
		private void AddMethod(MethodDefinition method)
		{
			MethodDefinitionProjection methodDefinitionProjection = WindowsRuntimeProjections.RemoveProjection(method);
			this.method_table.AddRow(new Row<uint, MethodImplAttributes, MethodAttributes, uint, uint, uint>(method.HasBody ? this.code.WriteMethodBody(method) : 0U, method.ImplAttributes, method.Attributes, this.GetStringIndex(method.Name), this.GetBlobIndex(this.GetMethodSignature(method)), this.param_rid));
			this.AddParameters(method);
			if (method.HasGenericParameters)
			{
				this.AddGenericParameters(method);
			}
			if (method.IsPInvokeImpl)
			{
				this.AddPInvokeInfo(method);
			}
			if (method.HasCustomAttributes)
			{
				this.AddCustomAttributes(method);
			}
			if (method.HasSecurityDeclarations)
			{
				this.AddSecurityDeclarations(method);
			}
			if (method.HasOverrides)
			{
				this.AddOverrides(method);
			}
			WindowsRuntimeProjections.ApplyProjection(method, methodDefinitionProjection);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008D20 File Offset: 0x00006F20
		private void AddParameters(MethodDefinition method)
		{
			ParameterDefinition parameter = method.MethodReturnType.parameter;
			if (parameter != null && MetadataBuilder.RequiresParameterRow(parameter))
			{
				this.AddParameter(0, parameter, this.param_table);
			}
			if (!method.HasParameters)
			{
				return;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterDefinition parameterDefinition = parameters[i];
				if (MetadataBuilder.RequiresParameterRow(parameterDefinition))
				{
					this.AddParameter((ushort)(i + 1), parameterDefinition, this.param_table);
				}
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008D94 File Offset: 0x00006F94
		private void AddPInvokeInfo(MethodDefinition method)
		{
			PInvokeInfo pinvokeInfo = method.PInvokeInfo;
			if (pinvokeInfo != null)
			{
				this.GetTable<ImplMapTable>(Table.ImplMap).AddRow(new Row<PInvokeAttributes, uint, uint, uint>(pinvokeInfo.Attributes, MetadataBuilder.MakeCodedRID(method, CodedIndex.MemberForwarded), this.GetStringIndex(pinvokeInfo.EntryPoint), pinvokeInfo.Module.MetadataToken.RID));
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00008DEC File Offset: 0x00006FEC
		private void AddOverrides(MethodDefinition method)
		{
			Collection<MethodReference> overrides = method.Overrides;
			MethodImplTable table = this.GetTable<MethodImplTable>(Table.MethodImpl);
			for (int i = 0; i < overrides.Count; i++)
			{
				table.AddRow(new Row<uint, uint, uint>(method.DeclaringType.token.RID, MetadataBuilder.MakeCodedRID(method, CodedIndex.MethodDefOrRef), MetadataBuilder.MakeCodedRID(this.LookupToken(overrides[i]), CodedIndex.MethodDefOrRef)));
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00008E50 File Offset: 0x00007050
		private static bool RequiresParameterRow(ParameterDefinition parameter)
		{
			return !string.IsNullOrEmpty(parameter.Name) || parameter.Attributes != ParameterAttributes.None || parameter.HasMarshalInfo || parameter.HasConstant || parameter.HasCustomAttributes;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00008E80 File Offset: 0x00007080
		private void AddParameter(ushort sequence, ParameterDefinition parameter, ParamTable table)
		{
			table.AddRow(new Row<ParameterAttributes, ushort, uint>(parameter.Attributes, sequence, this.GetStringIndex(parameter.Name)));
			TokenType tokenType = TokenType.Param;
			uint num = this.param_rid;
			this.param_rid = num + 1U;
			parameter.token = new MetadataToken(tokenType, num);
			if (parameter.HasCustomAttributes)
			{
				this.AddCustomAttributes(parameter);
			}
			if (parameter.HasConstant)
			{
				this.AddConstant(parameter, parameter.ParameterType);
			}
			if (parameter.HasMarshalInfo)
			{
				this.AddMarshalInfo(parameter);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008F00 File Offset: 0x00007100
		private void AddMarshalInfo(IMarshalInfoProvider owner)
		{
			this.GetTable<FieldMarshalTable>(Table.FieldMarshal).AddRow(new Row<uint, uint>(MetadataBuilder.MakeCodedRID(owner, CodedIndex.HasFieldMarshal), this.GetBlobIndex(this.GetMarshalInfoSignature(owner))));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008F2C File Offset: 0x0000712C
		private void AddProperties(TypeDefinition type)
		{
			Collection<PropertyDefinition> properties = type.Properties;
			this.property_map_table.AddRow(new Row<uint, uint>(type.token.RID, this.property_rid));
			for (int i = 0; i < properties.Count; i++)
			{
				this.AddProperty(properties[i]);
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008F80 File Offset: 0x00007180
		private void AddProperty(PropertyDefinition property)
		{
			this.property_table.AddRow(new Row<PropertyAttributes, uint, uint>(property.Attributes, this.GetStringIndex(property.Name), this.GetBlobIndex(this.GetPropertySignature(property))));
			TokenType tokenType = TokenType.Property;
			uint num = this.property_rid;
			this.property_rid = num + 1U;
			property.token = new MetadataToken(tokenType, num);
			MethodDefinition methodDefinition = property.GetMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.Getter, property, methodDefinition);
			}
			methodDefinition = property.SetMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.Setter, property, methodDefinition);
			}
			if (property.HasOtherMethods)
			{
				this.AddOtherSemantic(property, property.OtherMethods);
			}
			if (property.HasCustomAttributes)
			{
				this.AddCustomAttributes(property);
			}
			if (property.HasConstant)
			{
				this.AddConstant(property, property.PropertyType);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00009040 File Offset: 0x00007240
		private void AddOtherSemantic(IMetadataTokenProvider owner, Collection<MethodDefinition> others)
		{
			for (int i = 0; i < others.Count; i++)
			{
				this.AddSemantic(MethodSemanticsAttributes.Other, owner, others[i]);
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009070 File Offset: 0x00007270
		private void AddEvents(TypeDefinition type)
		{
			Collection<EventDefinition> events = type.Events;
			this.event_map_table.AddRow(new Row<uint, uint>(type.token.RID, this.event_rid));
			for (int i = 0; i < events.Count; i++)
			{
				this.AddEvent(events[i]);
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000090C4 File Offset: 0x000072C4
		private void AddEvent(EventDefinition @event)
		{
			this.event_table.AddRow(new Row<EventAttributes, uint, uint>(@event.Attributes, this.GetStringIndex(@event.Name), MetadataBuilder.MakeCodedRID(this.GetTypeToken(@event.EventType), CodedIndex.TypeDefOrRef)));
			TokenType tokenType = TokenType.Event;
			uint num = this.event_rid;
			this.event_rid = num + 1U;
			@event.token = new MetadataToken(tokenType, num);
			MethodDefinition methodDefinition = @event.AddMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.AddOn, @event, methodDefinition);
			}
			methodDefinition = @event.InvokeMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.Fire, @event, methodDefinition);
			}
			methodDefinition = @event.RemoveMethod;
			if (methodDefinition != null)
			{
				this.AddSemantic(MethodSemanticsAttributes.RemoveOn, @event, methodDefinition);
			}
			if (@event.HasOtherMethods)
			{
				this.AddOtherSemantic(@event, @event.OtherMethods);
			}
			if (@event.HasCustomAttributes)
			{
				this.AddCustomAttributes(@event);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00009186 File Offset: 0x00007386
		private void AddSemantic(MethodSemanticsAttributes semantics, IMetadataTokenProvider provider, MethodDefinition method)
		{
			method.SemanticsAttributes = semantics;
			this.GetTable<MethodSemanticsTable>(Table.MethodSemantics).AddRow(new Row<MethodSemanticsAttributes, uint, uint>(semantics, method.token.RID, MetadataBuilder.MakeCodedRID(provider, CodedIndex.HasSemantics)));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000091B8 File Offset: 0x000073B8
		private void AddConstant(IConstantProvider owner, TypeReference type)
		{
			object constant = owner.Constant;
			ElementType constantType = MetadataBuilder.GetConstantType(type, constant);
			this.constant_table.AddRow(new Row<ElementType, uint, uint>(constantType, MetadataBuilder.MakeCodedRID(owner.MetadataToken, CodedIndex.HasConstant), this.GetBlobIndex(this.GetConstantSignature(constantType, constant))));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00009200 File Offset: 0x00007400
		private static ElementType GetConstantType(TypeReference constant_type, object constant)
		{
			if (constant == null)
			{
				return ElementType.Class;
			}
			ElementType etype = constant_type.etype;
			switch (etype)
			{
			case ElementType.None:
			{
				TypeDefinition typeDefinition = constant_type.CheckedResolve();
				if (typeDefinition.IsEnum)
				{
					return MetadataBuilder.GetConstantType(typeDefinition.GetEnumUnderlyingType(), constant);
				}
				return ElementType.Class;
			}
			case ElementType.Void:
			case ElementType.Ptr:
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case (ElementType)23:
			case (ElementType)26:
			case ElementType.FnPtr:
				return etype;
			case ElementType.Boolean:
			case ElementType.Char:
			case ElementType.I1:
			case ElementType.U1:
			case ElementType.I2:
			case ElementType.U2:
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R4:
			case ElementType.R8:
			case ElementType.I:
			case ElementType.U:
				return MetadataBuilder.GetConstantType(constant.GetType());
			case ElementType.String:
				return ElementType.String;
			case ElementType.ByRef:
			case ElementType.CModReqD:
			case ElementType.CModOpt:
				break;
			case ElementType.Var:
			case ElementType.Array:
			case ElementType.SzArray:
			case ElementType.MVar:
				return ElementType.Class;
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)constant_type;
				if (genericInstanceType.ElementType.IsTypeOf("System", "Nullable`1"))
				{
					return MetadataBuilder.GetConstantType(genericInstanceType.GenericArguments[0], constant);
				}
				return MetadataBuilder.GetConstantType(((TypeSpecification)constant_type).ElementType, constant);
			}
			case ElementType.Object:
				return MetadataBuilder.GetConstantType(constant.GetType());
			default:
				if (etype != ElementType.Sentinel)
				{
					return etype;
				}
				break;
			}
			return MetadataBuilder.GetConstantType(((TypeSpecification)constant_type).ElementType, constant);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00009344 File Offset: 0x00007544
		private static ElementType GetConstantType(Type type)
		{
			switch (type.GetTypeCode())
			{
			case 3:
				return ElementType.Boolean;
			case 4:
				return ElementType.Char;
			case 5:
				return ElementType.I1;
			case 6:
				return ElementType.U1;
			case 7:
				return ElementType.I2;
			case 8:
				return ElementType.U2;
			case 9:
				return ElementType.I4;
			case 10:
				return ElementType.U4;
			case 11:
				return ElementType.I8;
			case 12:
				return ElementType.U8;
			case 13:
				return ElementType.R4;
			case 14:
				return ElementType.R8;
			case 18:
				return ElementType.String;
			}
			throw new NotSupportedException(type.FullName);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000093D0 File Offset: 0x000075D0
		private void AddCustomAttributes(ICustomAttributeProvider owner)
		{
			Collection<CustomAttribute> customAttributes = owner.CustomAttributes;
			for (int i = 0; i < customAttributes.Count; i++)
			{
				CustomAttribute customAttribute = customAttributes[i];
				CustomAttributeValueProjection customAttributeValueProjection = WindowsRuntimeProjections.RemoveProjection(customAttribute);
				this.custom_attribute_table.AddRow(new Row<uint, uint, uint>(MetadataBuilder.MakeCodedRID(owner, CodedIndex.HasCustomAttribute), MetadataBuilder.MakeCodedRID(this.LookupToken(customAttribute.Constructor), CodedIndex.CustomAttributeType), this.GetBlobIndex(this.GetCustomAttributeSignature(customAttribute))));
				WindowsRuntimeProjections.ApplyProjection(customAttribute, customAttributeValueProjection);
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00009444 File Offset: 0x00007644
		private void AddSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			Collection<SecurityDeclaration> securityDeclarations = owner.SecurityDeclarations;
			for (int i = 0; i < securityDeclarations.Count; i++)
			{
				SecurityDeclaration securityDeclaration = securityDeclarations[i];
				this.declsec_table.AddRow(new Row<SecurityAction, uint, uint>(securityDeclaration.Action, MetadataBuilder.MakeCodedRID(owner, CodedIndex.HasDeclSecurity), this.GetBlobIndex(this.GetSecurityDeclarationSignature(securityDeclaration))));
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000949C File Offset: 0x0000769C
		private MetadataToken GetMemberRefToken(MemberReference member)
		{
			Row<uint, uint, uint> row = this.CreateMemberRefRow(member);
			MetadataToken metadataToken;
			if (!this.member_ref_map.TryGetValue(row, ref metadataToken))
			{
				return this.AddMemberReference(member, row);
			}
			return metadataToken;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000094CB File Offset: 0x000076CB
		private Row<uint, uint, uint> CreateMemberRefRow(MemberReference member)
		{
			return new Row<uint, uint, uint>(MetadataBuilder.MakeCodedRID(this.GetTypeToken(member.DeclaringType), CodedIndex.MemberRefParent), this.GetStringIndex(member.Name), this.GetBlobIndex(this.GetMemberRefSignature(member)));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009500 File Offset: 0x00007700
		private MetadataToken AddMemberReference(MemberReference member, Row<uint, uint, uint> row)
		{
			member.token = new MetadataToken(TokenType.MemberRef, this.member_ref_table.AddRow(row));
			MetadataToken token = member.token;
			this.member_ref_map.Add(row, token);
			return token;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009540 File Offset: 0x00007740
		private MetadataToken GetMethodSpecToken(MethodSpecification method_spec)
		{
			Row<uint, uint> row = this.CreateMethodSpecRow(method_spec);
			MetadataToken metadataToken;
			if (this.method_spec_map.TryGetValue(row, ref metadataToken))
			{
				return metadataToken;
			}
			this.AddMethodSpecification(method_spec, row);
			return method_spec.token;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009575 File Offset: 0x00007775
		private void AddMethodSpecification(MethodSpecification method_spec, Row<uint, uint> row)
		{
			method_spec.token = new MetadataToken(TokenType.MethodSpec, this.method_spec_table.AddRow(row));
			this.method_spec_map.Add(row, method_spec.token);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000095A5 File Offset: 0x000077A5
		private Row<uint, uint> CreateMethodSpecRow(MethodSpecification method_spec)
		{
			return new Row<uint, uint>(MetadataBuilder.MakeCodedRID(this.LookupToken(method_spec.ElementMethod), CodedIndex.MethodDefOrRef), this.GetBlobIndex(this.GetMethodSpecSignature(method_spec)));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000095CB File Offset: 0x000077CB
		private SignatureWriter CreateSignatureWriter()
		{
			return new SignatureWriter(this);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000095D4 File Offset: 0x000077D4
		private SignatureWriter GetMethodSpecSignature(MethodSpecification method_spec)
		{
			if (!method_spec.IsGenericInstance)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod genericInstanceMethod = (GenericInstanceMethod)method_spec;
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteByte(10);
			signatureWriter.WriteGenericInstanceSignature(genericInstanceMethod);
			return signatureWriter;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000960D File Offset: 0x0000780D
		public uint AddStandAloneSignature(uint signature)
		{
			return (uint)this.standalone_sig_table.AddRow(signature);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000961B File Offset: 0x0000781B
		public uint GetLocalVariableBlobIndex(Collection<VariableDefinition> variables)
		{
			return this.GetBlobIndex(this.GetVariablesSignature(variables));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000962A File Offset: 0x0000782A
		public uint GetCallSiteBlobIndex(CallSite call_site)
		{
			return this.GetBlobIndex(this.GetMethodSignature(call_site));
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009639 File Offset: 0x00007839
		public uint GetConstantTypeBlobIndex(TypeReference constant_type)
		{
			return this.GetBlobIndex(this.GetConstantTypeSignature(constant_type));
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009648 File Offset: 0x00007848
		private SignatureWriter GetVariablesSignature(Collection<VariableDefinition> variables)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteByte(7);
			signatureWriter.WriteCompressedUInt32((uint)variables.Count);
			for (int i = 0; i < variables.Count; i++)
			{
				signatureWriter.WriteTypeSignature(variables[i].VariableType);
			}
			return signatureWriter;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00009694 File Offset: 0x00007894
		private SignatureWriter GetConstantTypeSignature(TypeReference constant_type)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteByte(6);
			signatureWriter.WriteTypeSignature(constant_type);
			return signatureWriter;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000096B8 File Offset: 0x000078B8
		private SignatureWriter GetFieldSignature(FieldReference field)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteByte(6);
			signatureWriter.WriteTypeSignature(field.FieldType);
			return signatureWriter;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000096E0 File Offset: 0x000078E0
		private SignatureWriter GetMethodSignature(IMethodSignature method)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteMethodSignature(method);
			return signatureWriter;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000096FC File Offset: 0x000078FC
		private SignatureWriter GetMemberRefSignature(MemberReference member)
		{
			FieldReference fieldReference = member as FieldReference;
			if (fieldReference != null)
			{
				return this.GetFieldSignature(fieldReference);
			}
			MethodReference methodReference = member as MethodReference;
			if (methodReference != null)
			{
				return this.GetMethodSignature(methodReference);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00009734 File Offset: 0x00007934
		private SignatureWriter GetPropertySignature(PropertyDefinition property)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			byte b = 8;
			if (property.HasThis)
			{
				b |= 32;
			}
			uint num = 0U;
			Collection<ParameterDefinition> collection = null;
			if (property.HasParameters)
			{
				collection = property.Parameters;
				num = (uint)collection.Count;
			}
			signatureWriter.WriteByte(b);
			signatureWriter.WriteCompressedUInt32(num);
			signatureWriter.WriteTypeSignature(property.PropertyType);
			if (num == 0U)
			{
				return signatureWriter;
			}
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				signatureWriter.WriteTypeSignature(collection[num2].ParameterType);
				num2++;
			}
			return signatureWriter;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000097B8 File Offset: 0x000079B8
		private SignatureWriter GetTypeSpecSignature(TypeReference type)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteTypeSignature(type);
			return signatureWriter;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000097D4 File Offset: 0x000079D4
		private SignatureWriter GetConstantSignature(ElementType type, object value)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			if (type <= ElementType.String)
			{
				if (type != ElementType.None)
				{
					if (type != ElementType.String)
					{
						goto IL_003B;
					}
					signatureWriter.WriteConstantString((string)value);
					return signatureWriter;
				}
			}
			else if (type - ElementType.Class > 2 && type - ElementType.Object > 2)
			{
				goto IL_003B;
			}
			signatureWriter.WriteInt32(0);
			return signatureWriter;
			IL_003B:
			signatureWriter.WriteConstantPrimitive(value);
			return signatureWriter;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00009824 File Offset: 0x00007A24
		private SignatureWriter GetCustomAttributeSignature(CustomAttribute attribute)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			if (!attribute.resolved)
			{
				signatureWriter.WriteBytes(attribute.GetBlob());
				return signatureWriter;
			}
			signatureWriter.WriteUInt16(1);
			signatureWriter.WriteCustomAttributeConstructorArguments(attribute);
			signatureWriter.WriteCustomAttributeNamedArguments(attribute);
			return signatureWriter;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009864 File Offset: 0x00007A64
		private SignatureWriter GetSecurityDeclarationSignature(SecurityDeclaration declaration)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			if (!declaration.resolved)
			{
				signatureWriter.WriteBytes(declaration.GetBlob());
			}
			else if (this.module.Runtime < TargetRuntime.Net_2_0)
			{
				signatureWriter.WriteXmlSecurityDeclaration(declaration);
			}
			else
			{
				signatureWriter.WriteSecurityDeclaration(declaration);
			}
			return signatureWriter;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000098B0 File Offset: 0x00007AB0
		private SignatureWriter GetMarshalInfoSignature(IMarshalInfoProvider owner)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteMarshalInfo(owner.MarshalInfo);
			return signatureWriter;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000098D1 File Offset: 0x00007AD1
		private static Exception CreateForeignMemberException(MemberReference member)
		{
			return new ArgumentException(string.Format("Member '{0}' is declared in another module and needs to be imported", member));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000098E4 File Offset: 0x00007AE4
		public MetadataToken LookupToken(IMetadataTokenProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException();
			}
			if (this.metadata_builder != null)
			{
				return this.metadata_builder.LookupToken(provider);
			}
			MemberReference memberReference = provider as MemberReference;
			if (memberReference == null || memberReference.Module != this.module)
			{
				throw MetadataBuilder.CreateForeignMemberException(memberReference);
			}
			MetadataToken metadataToken = provider.MetadataToken;
			TokenType tokenType = metadataToken.TokenType;
			if (tokenType <= TokenType.MemberRef)
			{
				if (tokenType <= TokenType.TypeDef)
				{
					if (tokenType == TokenType.TypeRef)
					{
						goto IL_00BE;
					}
					if (tokenType != TokenType.TypeDef)
					{
						goto IL_00E0;
					}
				}
				else if (tokenType != TokenType.Field && tokenType != TokenType.Method)
				{
					if (tokenType != TokenType.MemberRef)
					{
						goto IL_00E0;
					}
					return this.GetMemberRefToken(memberReference);
				}
			}
			else if (tokenType <= TokenType.Property)
			{
				if (tokenType != TokenType.Event && tokenType != TokenType.Property)
				{
					goto IL_00E0;
				}
			}
			else
			{
				if (tokenType == TokenType.TypeSpec || tokenType == TokenType.GenericParam)
				{
					goto IL_00BE;
				}
				if (tokenType != TokenType.MethodSpec)
				{
					goto IL_00E0;
				}
				return this.GetMethodSpecToken((MethodSpecification)provider);
			}
			return metadataToken;
			IL_00BE:
			return this.GetTypeToken((TypeReference)provider);
			IL_00E0:
			throw new NotSupportedException();
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000099D8 File Offset: 0x00007BD8
		public void AddMethodDebugInformation(MethodDebugInformation method_info)
		{
			if (method_info.HasSequencePoints)
			{
				this.AddSequencePoints(method_info);
			}
			if (method_info.Scope != null)
			{
				this.AddLocalScope(method_info, method_info.Scope);
			}
			if (method_info.StateMachineKickOffMethod != null)
			{
				this.AddStateMachineMethod(method_info);
			}
			this.AddCustomDebugInformations(method_info.Method);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00009A24 File Offset: 0x00007C24
		private void AddStateMachineMethod(MethodDebugInformation method_info)
		{
			this.state_machine_method_table.AddRow(new Row<uint, uint>(method_info.Method.MetadataToken.RID, method_info.StateMachineKickOffMethod.MetadataToken.RID));
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00009A68 File Offset: 0x00007C68
		private void AddLocalScope(MethodDebugInformation method_info, ScopeDebugInformation scope)
		{
			int num = this.local_scope_table.AddRow(new Row<uint, uint, uint, uint, uint, uint>(method_info.Method.MetadataToken.RID, (scope.import != null) ? this.AddImportScope(scope.import) : 0U, this.local_variable_rid, this.local_constant_rid, (uint)scope.Start.Offset, (uint)((scope.End.IsEndOfMethod ? method_info.code_size : scope.End.Offset) - scope.Start.Offset)));
			scope.token = new MetadataToken(TokenType.LocalScope, num);
			this.AddCustomDebugInformations(scope);
			if (scope.HasVariables)
			{
				this.AddLocalVariables(scope);
			}
			if (scope.HasConstants)
			{
				this.AddLocalConstants(scope);
			}
			for (int i = 0; i < scope.Scopes.Count; i++)
			{
				this.AddLocalScope(method_info, scope.Scopes[i]);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00009B60 File Offset: 0x00007D60
		private void AddLocalVariables(ScopeDebugInformation scope)
		{
			for (int i = 0; i < scope.Variables.Count; i++)
			{
				VariableDebugInformation variableDebugInformation = scope.Variables[i];
				this.local_variable_table.AddRow(new Row<VariableAttributes, ushort, uint>(variableDebugInformation.Attributes, (ushort)variableDebugInformation.Index, this.GetStringIndex(variableDebugInformation.Name)));
				variableDebugInformation.token = new MetadataToken(TokenType.LocalVariable, this.local_variable_rid);
				this.local_variable_rid += 1U;
				this.AddCustomDebugInformations(variableDebugInformation);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009BE8 File Offset: 0x00007DE8
		private void AddLocalConstants(ScopeDebugInformation scope)
		{
			for (int i = 0; i < scope.Constants.Count; i++)
			{
				ConstantDebugInformation constantDebugInformation = scope.Constants[i];
				this.local_constant_table.AddRow(new Row<uint, uint>(this.GetStringIndex(constantDebugInformation.Name), this.GetBlobIndex(this.GetConstantSignature(constantDebugInformation))));
				constantDebugInformation.token = new MetadataToken(TokenType.LocalConstant, this.local_constant_rid);
				this.local_constant_rid += 1U;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009C68 File Offset: 0x00007E68
		private SignatureWriter GetConstantSignature(ConstantDebugInformation constant)
		{
			TypeReference constantType = constant.ConstantType;
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteTypeSignature(constantType);
			if (constantType.IsTypeOf("System", "Decimal"))
			{
				int[] bits = decimal.GetBits((decimal)constant.Value);
				uint num = (uint)bits[0];
				uint num2 = (uint)bits[1];
				uint num3 = (uint)bits[2];
				byte b = (byte)(bits[3] >> 16);
				bool flag = ((long)bits[3] & (long)((ulong)int.MinValue)) != 0L;
				signatureWriter.WriteByte(b | (flag ? 128 : 0));
				signatureWriter.WriteUInt32(num);
				signatureWriter.WriteUInt32(num2);
				signatureWriter.WriteUInt32(num3);
				return signatureWriter;
			}
			if (constantType.IsTypeOf("System", "DateTime"))
			{
				signatureWriter.WriteInt64(((DateTime)constant.Value).Ticks);
				return signatureWriter;
			}
			signatureWriter.WriteBytes(this.GetConstantSignature(constantType.etype, constant.Value));
			return signatureWriter;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009D4C File Offset: 0x00007F4C
		public void AddCustomDebugInformations(ICustomDebugInformationProvider provider)
		{
			if (!provider.HasCustomDebugInformations)
			{
				return;
			}
			Collection<CustomDebugInformation> customDebugInformations = provider.CustomDebugInformations;
			int i = 0;
			while (i < customDebugInformations.Count)
			{
				CustomDebugInformation customDebugInformation = customDebugInformations[i];
				switch (customDebugInformation.Kind)
				{
				case CustomDebugInformationKind.Binary:
				{
					BinaryCustomDebugInformation binaryCustomDebugInformation = (BinaryCustomDebugInformation)customDebugInformation;
					this.AddCustomDebugInformation(provider, binaryCustomDebugInformation, this.GetBlobIndex(binaryCustomDebugInformation.Data));
					break;
				}
				case CustomDebugInformationKind.StateMachineScope:
					this.AddStateMachineScopeDebugInformation(provider, (StateMachineScopeDebugInformation)customDebugInformation);
					break;
				case CustomDebugInformationKind.DynamicVariable:
				case CustomDebugInformationKind.DefaultNamespace:
					goto IL_00A6;
				case CustomDebugInformationKind.AsyncMethodBody:
					this.AddAsyncMethodBodyDebugInformation(provider, (AsyncMethodBodyDebugInformation)customDebugInformation);
					break;
				case CustomDebugInformationKind.EmbeddedSource:
					this.AddEmbeddedSourceDebugInformation(provider, (EmbeddedSourceDebugInformation)customDebugInformation);
					break;
				case CustomDebugInformationKind.SourceLink:
					this.AddSourceLinkDebugInformation(provider, (SourceLinkDebugInformation)customDebugInformation);
					break;
				default:
					goto IL_00A6;
				}
				i++;
				continue;
				IL_00A6:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009E18 File Offset: 0x00008018
		private void AddStateMachineScopeDebugInformation(ICustomDebugInformationProvider provider, StateMachineScopeDebugInformation state_machine_scope)
		{
			MethodDebugInformation debugInformation = ((MethodDefinition)provider).DebugInformation;
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteUInt32((uint)state_machine_scope.Start.Offset);
			int num = (state_machine_scope.End.IsEndOfMethod ? debugInformation.code_size : state_machine_scope.End.Offset);
			signatureWriter.WriteUInt32((uint)(num - state_machine_scope.Start.Offset));
			this.AddCustomDebugInformation(provider, state_machine_scope, signatureWriter);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009E94 File Offset: 0x00008094
		private void AddAsyncMethodBodyDebugInformation(ICustomDebugInformationProvider provider, AsyncMethodBodyDebugInformation async_method)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteUInt32((uint)(async_method.catch_handler.Offset + 1));
			for (int i = 0; i < async_method.yields.Count; i++)
			{
				signatureWriter.WriteUInt32((uint)async_method.yields[i].Offset);
				signatureWriter.WriteUInt32((uint)async_method.resumes[i].Offset);
				signatureWriter.WriteCompressedUInt32(async_method.move_next.MetadataToken.RID);
			}
			this.AddCustomDebugInformation(provider, async_method, signatureWriter);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009F28 File Offset: 0x00008128
		private void AddEmbeddedSourceDebugInformation(ICustomDebugInformationProvider provider, EmbeddedSourceDebugInformation embedded_source)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			byte[] array = embedded_source.content ?? Empty<byte>.Array;
			if (embedded_source.compress)
			{
				signatureWriter.WriteInt32(array.Length);
				MemoryStream memoryStream = new MemoryStream(array);
				MemoryStream memoryStream2 = new MemoryStream();
				using (DeflateStream deflateStream = new DeflateStream(memoryStream2, 1, true))
				{
					memoryStream.CopyTo(deflateStream);
				}
				signatureWriter.WriteBytes(memoryStream2.ToArray());
			}
			else
			{
				signatureWriter.WriteInt32(0);
				signatureWriter.WriteBytes(array);
			}
			this.AddCustomDebugInformation(provider, embedded_source, signatureWriter);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00009FC0 File Offset: 0x000081C0
		private void AddSourceLinkDebugInformation(ICustomDebugInformationProvider provider, SourceLinkDebugInformation source_link)
		{
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteBytes(Encoding.UTF8.GetBytes(source_link.content));
			this.AddCustomDebugInformation(provider, source_link, signatureWriter);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00009FF3 File Offset: 0x000081F3
		private void AddCustomDebugInformation(ICustomDebugInformationProvider provider, CustomDebugInformation custom_info, SignatureWriter signature)
		{
			this.AddCustomDebugInformation(provider, custom_info, this.GetBlobIndex(signature));
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000A004 File Offset: 0x00008204
		private void AddCustomDebugInformation(ICustomDebugInformationProvider provider, CustomDebugInformation custom_info, uint blob_index)
		{
			int num = this.custom_debug_information_table.AddRow(new Row<uint, uint, uint>(MetadataBuilder.MakeCodedRID(provider.MetadataToken, CodedIndex.HasCustomDebugInformation), this.GetGuidIndex(custom_info.Identifier), blob_index));
			custom_info.token = new MetadataToken(TokenType.CustomDebugInformation, num);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000A050 File Offset: 0x00008250
		private uint AddImportScope(ImportDebugInformation import)
		{
			uint num = 0U;
			if (import.Parent != null)
			{
				num = this.AddImportScope(import.Parent);
			}
			uint num2 = 0U;
			if (import.HasTargets)
			{
				SignatureWriter signatureWriter = this.CreateSignatureWriter();
				for (int i = 0; i < import.Targets.Count; i++)
				{
					this.AddImportTarget(import.Targets[i], signatureWriter);
				}
				num2 = this.GetBlobIndex(signatureWriter);
			}
			Row<uint, uint> row = new Row<uint, uint>(num, num2);
			MetadataToken metadataToken;
			if (this.import_scope_map.TryGetValue(row, ref metadataToken))
			{
				return metadataToken.RID;
			}
			metadataToken = new MetadataToken(TokenType.ImportScope, this.import_scope_table.AddRow(row));
			this.import_scope_map.Add(row, metadataToken);
			return metadataToken.RID;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000A10C File Offset: 0x0000830C
		private void AddImportTarget(ImportTarget target, SignatureWriter signature)
		{
			signature.WriteCompressedUInt32((uint)target.kind);
			switch (target.kind)
			{
			case ImportTargetKind.ImportNamespace:
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.@namespace));
				return;
			case ImportTargetKind.ImportNamespaceInAssembly:
				signature.WriteCompressedUInt32(target.reference.MetadataToken.RID);
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.@namespace));
				return;
			case ImportTargetKind.ImportType:
				signature.WriteTypeToken(target.type);
				return;
			case ImportTargetKind.ImportXmlNamespaceWithAlias:
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.@namespace));
				return;
			case ImportTargetKind.ImportAlias:
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.alias));
				return;
			case ImportTargetKind.DefineAssemblyAlias:
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(target.reference.MetadataToken.RID);
				return;
			case ImportTargetKind.DefineNamespaceAlias:
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.@namespace));
				return;
			case ImportTargetKind.DefineNamespaceInAssemblyAlias:
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(target.reference.MetadataToken.RID);
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.@namespace));
				return;
			case ImportTargetKind.DefineTypeAlias:
				signature.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(target.alias));
				signature.WriteTypeToken(target.type);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000A28A File Offset: 0x0000848A
		private uint GetUTF8StringBlobIndex(string s)
		{
			return this.GetBlobIndex(Encoding.UTF8.GetBytes(s));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000A2A0 File Offset: 0x000084A0
		public MetadataToken GetDocumentToken(Document document)
		{
			MetadataToken metadataToken;
			if (this.document_map.TryGetValue(document.Url, ref metadataToken))
			{
				return metadataToken;
			}
			metadataToken = (document.token = new MetadataToken(TokenType.Document, this.document_table.AddRow(new Row<uint, uint, uint, uint>(this.GetBlobIndex(this.GetDocumentNameSignature(document)), this.GetGuidIndex(document.HashAlgorithm.ToGuid()), this.GetBlobIndex(document.Hash), this.GetGuidIndex(document.Language.ToGuid())))));
			this.AddCustomDebugInformations(document);
			this.document_map.Add(document.Url, metadataToken);
			return metadataToken;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000A340 File Offset: 0x00008540
		private SignatureWriter GetDocumentNameSignature(Document document)
		{
			string url = document.Url;
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			char c;
			if (!MetadataBuilder.TryGetDocumentNameSeparator(url, out c))
			{
				signatureWriter.WriteByte(0);
				signatureWriter.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(url));
				return signatureWriter;
			}
			signatureWriter.WriteByte((byte)c);
			string[] array = url.Split(new char[] { c });
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == string.Empty)
				{
					signatureWriter.WriteCompressedUInt32(0U);
				}
				else
				{
					signatureWriter.WriteCompressedUInt32(this.GetUTF8StringBlobIndex(array[i]));
				}
			}
			return signatureWriter;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000A3D0 File Offset: 0x000085D0
		private static bool TryGetDocumentNameSeparator(string path, out char separator)
		{
			separator = '\0';
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < path.Length; i++)
			{
				if (path.get_Chars(i) == '/')
				{
					num++;
				}
				else if (path.get_Chars(i) == '\\')
				{
					num2++;
				}
			}
			if (num == 0 && num2 == 0)
			{
				return false;
			}
			if (num >= num2)
			{
				separator = '/';
				return true;
			}
			separator = '\\';
			return true;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000A438 File Offset: 0x00008638
		private void AddSequencePoints(MethodDebugInformation info)
		{
			uint rid = info.Method.MetadataToken.RID;
			Document document;
			if (info.TryGetUniqueDocument(out document))
			{
				this.method_debug_information_table.rows[(int)(rid - 1U)].Col1 = this.GetDocumentToken(document).RID;
			}
			SignatureWriter signatureWriter = this.CreateSignatureWriter();
			signatureWriter.WriteSequencePoints(info);
			this.method_debug_information_table.rows[(int)(rid - 1U)].Col2 = this.GetBlobIndex(signatureWriter);
		}

		// Token: 0x040000B3 RID: 179
		internal readonly ModuleDefinition module;

		// Token: 0x040000B4 RID: 180
		internal readonly ISymbolWriterProvider symbol_writer_provider;

		// Token: 0x040000B5 RID: 181
		internal readonly ISymbolWriter symbol_writer;

		// Token: 0x040000B6 RID: 182
		internal readonly TextMap text_map;

		// Token: 0x040000B7 RID: 183
		internal readonly string fq_name;

		// Token: 0x040000B8 RID: 184
		internal readonly uint timestamp;

		// Token: 0x040000B9 RID: 185
		private readonly Dictionary<Row<uint, uint, uint>, MetadataToken> type_ref_map;

		// Token: 0x040000BA RID: 186
		private readonly Dictionary<uint, MetadataToken> type_spec_map;

		// Token: 0x040000BB RID: 187
		private readonly Dictionary<Row<uint, uint, uint>, MetadataToken> member_ref_map;

		// Token: 0x040000BC RID: 188
		private readonly Dictionary<Row<uint, uint>, MetadataToken> method_spec_map;

		// Token: 0x040000BD RID: 189
		private readonly Collection<GenericParameter> generic_parameters;

		// Token: 0x040000BE RID: 190
		internal readonly CodeWriter code;

		// Token: 0x040000BF RID: 191
		internal readonly DataBuffer data;

		// Token: 0x040000C0 RID: 192
		internal readonly ResourceBuffer resources;

		// Token: 0x040000C1 RID: 193
		internal readonly StringHeapBuffer string_heap;

		// Token: 0x040000C2 RID: 194
		internal readonly GuidHeapBuffer guid_heap;

		// Token: 0x040000C3 RID: 195
		internal readonly UserStringHeapBuffer user_string_heap;

		// Token: 0x040000C4 RID: 196
		internal readonly BlobHeapBuffer blob_heap;

		// Token: 0x040000C5 RID: 197
		internal readonly TableHeapBuffer table_heap;

		// Token: 0x040000C6 RID: 198
		internal readonly PdbHeapBuffer pdb_heap;

		// Token: 0x040000C7 RID: 199
		internal MetadataToken entry_point;

		// Token: 0x040000C8 RID: 200
		internal uint type_rid = 1U;

		// Token: 0x040000C9 RID: 201
		internal uint field_rid = 1U;

		// Token: 0x040000CA RID: 202
		internal uint method_rid = 1U;

		// Token: 0x040000CB RID: 203
		internal uint param_rid = 1U;

		// Token: 0x040000CC RID: 204
		internal uint property_rid = 1U;

		// Token: 0x040000CD RID: 205
		internal uint event_rid = 1U;

		// Token: 0x040000CE RID: 206
		internal uint local_variable_rid = 1U;

		// Token: 0x040000CF RID: 207
		internal uint local_constant_rid = 1U;

		// Token: 0x040000D0 RID: 208
		private readonly TypeRefTable type_ref_table;

		// Token: 0x040000D1 RID: 209
		private readonly TypeDefTable type_def_table;

		// Token: 0x040000D2 RID: 210
		private readonly FieldTable field_table;

		// Token: 0x040000D3 RID: 211
		private readonly MethodTable method_table;

		// Token: 0x040000D4 RID: 212
		private readonly ParamTable param_table;

		// Token: 0x040000D5 RID: 213
		private readonly InterfaceImplTable iface_impl_table;

		// Token: 0x040000D6 RID: 214
		private readonly MemberRefTable member_ref_table;

		// Token: 0x040000D7 RID: 215
		private readonly ConstantTable constant_table;

		// Token: 0x040000D8 RID: 216
		private readonly CustomAttributeTable custom_attribute_table;

		// Token: 0x040000D9 RID: 217
		private readonly DeclSecurityTable declsec_table;

		// Token: 0x040000DA RID: 218
		private readonly StandAloneSigTable standalone_sig_table;

		// Token: 0x040000DB RID: 219
		private readonly EventMapTable event_map_table;

		// Token: 0x040000DC RID: 220
		private readonly EventTable event_table;

		// Token: 0x040000DD RID: 221
		private readonly PropertyMapTable property_map_table;

		// Token: 0x040000DE RID: 222
		private readonly PropertyTable property_table;

		// Token: 0x040000DF RID: 223
		private readonly TypeSpecTable typespec_table;

		// Token: 0x040000E0 RID: 224
		private readonly MethodSpecTable method_spec_table;

		// Token: 0x040000E1 RID: 225
		private readonly bool portable_pdb;

		// Token: 0x040000E2 RID: 226
		internal MetadataBuilder metadata_builder;

		// Token: 0x040000E3 RID: 227
		private readonly DocumentTable document_table;

		// Token: 0x040000E4 RID: 228
		private readonly MethodDebugInformationTable method_debug_information_table;

		// Token: 0x040000E5 RID: 229
		private readonly LocalScopeTable local_scope_table;

		// Token: 0x040000E6 RID: 230
		private readonly LocalVariableTable local_variable_table;

		// Token: 0x040000E7 RID: 231
		private readonly LocalConstantTable local_constant_table;

		// Token: 0x040000E8 RID: 232
		private readonly ImportScopeTable import_scope_table;

		// Token: 0x040000E9 RID: 233
		private readonly StateMachineMethodTable state_machine_method_table;

		// Token: 0x040000EA RID: 234
		private readonly CustomDebugInformationTable custom_debug_information_table;

		// Token: 0x040000EB RID: 235
		private readonly Dictionary<Row<uint, uint>, MetadataToken> import_scope_map;

		// Token: 0x040000EC RID: 236
		private readonly Dictionary<string, MetadataToken> document_map;

		// Token: 0x0200013E RID: 318
		private sealed class GenericParameterComparer : IComparer<GenericParameter>
		{
			// Token: 0x06000B94 RID: 2964 RVA: 0x00025F60 File Offset: 0x00024160
			public int Compare(GenericParameter a, GenericParameter b)
			{
				uint num = MetadataBuilder.MakeCodedRID(a.Owner, CodedIndex.TypeOrMethodDef);
				uint num2 = MetadataBuilder.MakeCodedRID(b.Owner, CodedIndex.TypeOrMethodDef);
				if (num == num2)
				{
					int position = a.Position;
					int position2 = b.Position;
					if (position == position2)
					{
						return 0;
					}
					if (position <= position2)
					{
						return -1;
					}
					return 1;
				}
				else
				{
					if (num <= num2)
					{
						return -1;
					}
					return 1;
				}
			}
		}
	}
}
