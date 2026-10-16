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
	// Token: 0x02000071 RID: 113
	internal sealed class MetadataReader : ByteBuffer
	{
		// Token: 0x06000378 RID: 888 RVA: 0x0000AC1C File Offset: 0x00008E1C
		public MetadataReader(ModuleDefinition module)
			: base(module.Image.TableHeap.data)
		{
			this.image = module.Image;
			this.module = module;
			this.metadata = module.MetadataSystem;
			this.code = new CodeReader(this);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000AC6A File Offset: 0x00008E6A
		public MetadataReader(Image image, ModuleDefinition module, MetadataReader metadata_reader)
			: base(image.TableHeap.data)
		{
			this.image = image;
			this.module = module;
			this.metadata = module.MetadataSystem;
			this.metadata_reader = metadata_reader;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000AC9E File Offset: 0x00008E9E
		private int GetCodedIndexSize(CodedIndex index)
		{
			return this.image.GetCodedIndexSize(index);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000ACAC File Offset: 0x00008EAC
		private uint ReadByIndexSize(int size)
		{
			if (size == 4)
			{
				return base.ReadUInt32();
			}
			return (uint)base.ReadUInt16();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000ACC0 File Offset: 0x00008EC0
		private byte[] ReadBlob()
		{
			BlobHeap blobHeap = this.image.BlobHeap;
			if (blobHeap == null)
			{
				this.position += 2;
				return Empty<byte>.Array;
			}
			return blobHeap.Read(this.ReadBlobIndex());
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000ACFC File Offset: 0x00008EFC
		private byte[] ReadBlob(uint signature)
		{
			BlobHeap blobHeap = this.image.BlobHeap;
			if (blobHeap == null)
			{
				return Empty<byte>.Array;
			}
			return blobHeap.Read(signature);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000AD25 File Offset: 0x00008F25
		private uint ReadBlobIndex()
		{
			BlobHeap blobHeap = this.image.BlobHeap;
			return this.ReadByIndexSize((blobHeap != null) ? blobHeap.IndexSize : 2);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000AD44 File Offset: 0x00008F44
		private void GetBlobView(uint signature, out byte[] blob, out int index, out int count)
		{
			BlobHeap blobHeap = this.image.BlobHeap;
			if (blobHeap == null)
			{
				blob = null;
				index = (count = 0);
				return;
			}
			blobHeap.GetView(signature, out blob, out index, out count);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000AD78 File Offset: 0x00008F78
		private string ReadString()
		{
			return this.image.StringHeap.Read(this.ReadByIndexSize(this.image.StringHeap.IndexSize));
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		private uint ReadStringIndex()
		{
			return this.ReadByIndexSize(this.image.StringHeap.IndexSize);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000ADB8 File Offset: 0x00008FB8
		private Guid ReadGuid()
		{
			return this.image.GuidHeap.Read(this.ReadByIndexSize(this.image.GuidHeap.IndexSize));
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		private uint ReadTableIndex(Table table)
		{
			return this.ReadByIndexSize(this.image.GetTableIndexSize(table));
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		private MetadataToken ReadMetadataToken(CodedIndex index)
		{
			return index.GetMetadataToken(this.ReadByIndexSize(this.GetCodedIndexSize(index)));
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000AE0C File Offset: 0x0000900C
		private int MoveTo(Table table)
		{
			TableInformation tableInformation = this.image.TableHeap[table];
			if (tableInformation.Length != 0U)
			{
				this.position = (int)tableInformation.Offset;
			}
			return (int)tableInformation.Length;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000AE48 File Offset: 0x00009048
		private bool MoveTo(Table table, uint row)
		{
			TableInformation tableInformation = this.image.TableHeap[table];
			uint length = tableInformation.Length;
			if (length == 0U || row > length)
			{
				return false;
			}
			this.position = (int)(tableInformation.Offset + tableInformation.RowSize * (row - 1U));
			return true;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000AE90 File Offset: 0x00009090
		public AssemblyNameDefinition ReadAssemblyNameDefinition()
		{
			if (this.MoveTo(Table.Assembly) == 0)
			{
				return null;
			}
			AssemblyNameDefinition assemblyNameDefinition = new AssemblyNameDefinition();
			assemblyNameDefinition.HashAlgorithm = (AssemblyHashAlgorithm)base.ReadUInt32();
			this.PopulateVersionAndFlags(assemblyNameDefinition);
			assemblyNameDefinition.PublicKey = this.ReadBlob();
			this.PopulateNameAndCulture(assemblyNameDefinition);
			return assemblyNameDefinition;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000AED6 File Offset: 0x000090D6
		public ModuleDefinition Populate(ModuleDefinition module)
		{
			if (this.MoveTo(Table.Module) == 0)
			{
				return module;
			}
			base.Advance(2);
			module.Name = this.ReadString();
			module.Mvid = this.ReadGuid();
			return module;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000AF04 File Offset: 0x00009104
		private void InitializeAssemblyReferences()
		{
			if (this.metadata.AssemblyReferences != null)
			{
				return;
			}
			int num = this.MoveTo(Table.AssemblyRef);
			AssemblyNameReference[] array = (this.metadata.AssemblyReferences = new AssemblyNameReference[num]);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				AssemblyNameReference assemblyNameReference = new AssemblyNameReference();
				assemblyNameReference.token = new MetadataToken(TokenType.AssemblyRef, num2 + 1U);
				this.PopulateVersionAndFlags(assemblyNameReference);
				byte[] array2 = this.ReadBlob();
				if (assemblyNameReference.HasPublicKey)
				{
					assemblyNameReference.PublicKey = array2;
				}
				else
				{
					assemblyNameReference.PublicKeyToken = array2;
				}
				this.PopulateNameAndCulture(assemblyNameReference);
				assemblyNameReference.Hash = this.ReadBlob();
				array[(int)num2] = assemblyNameReference;
				num2 += 1U;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000AFB0 File Offset: 0x000091B0
		public Collection<AssemblyNameReference> ReadAssemblyReferences()
		{
			this.InitializeAssemblyReferences();
			Collection<AssemblyNameReference> collection = new Collection<AssemblyNameReference>(this.metadata.AssemblyReferences);
			if (this.module.IsWindowsMetadata())
			{
				this.module.Projections.AddVirtualReferences(collection);
			}
			return collection;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public MethodDefinition ReadEntryPoint()
		{
			if (this.module.Image.EntryPointToken == 0U)
			{
				return null;
			}
			return this.GetMethodDefinition(new MetadataToken(this.module.Image.EntryPointToken).RID);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000B038 File Offset: 0x00009238
		public Collection<ModuleDefinition> ReadModules()
		{
			Collection<ModuleDefinition> collection = new Collection<ModuleDefinition>(1);
			collection.Add(this.module);
			int num = this.MoveTo(Table.File);
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				uint num3 = base.ReadUInt32();
				string text = this.ReadString();
				this.ReadBlobIndex();
				if (num3 == 0U)
				{
					ReaderParameters readerParameters = new ReaderParameters
					{
						ReadingMode = this.module.ReadingMode,
						SymbolReaderProvider = this.module.SymbolReaderProvider,
						AssemblyResolver = this.module.AssemblyResolver
					};
					collection.Add(ModuleDefinition.ReadModule(this.GetModuleFileName(text), readerParameters));
				}
				num2 += 1U;
			}
			return collection;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000B0D8 File Offset: 0x000092D8
		private string GetModuleFileName(string name)
		{
			if (this.module.FileName == null)
			{
				throw new NotSupportedException();
			}
			return Path.Combine(Path.GetDirectoryName(this.module.FileName), name);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000B104 File Offset: 0x00009304
		private void InitializeModuleReferences()
		{
			if (this.metadata.ModuleReferences == null)
			{
				int num = this.MoveTo(Table.ModuleRef);
				ModuleReference[] array = (this.metadata.ModuleReferences = new ModuleReference[num]);
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)num))
				{
					array[(int)num2] = new ModuleReference(this.ReadString())
					{
						token = new MetadataToken(TokenType.ModuleRef, num2 + 1U)
					};
					num2 += 1U;
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000B170 File Offset: 0x00009370
		public Collection<ModuleReference> ReadModuleReferences()
		{
			this.InitializeModuleReferences();
			return new Collection<ModuleReference>(this.metadata.ModuleReferences);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000B188 File Offset: 0x00009388
		public bool HasFileResource()
		{
			int num = this.MoveTo(Table.File);
			if (num == 0)
			{
				return false;
			}
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				if (this.ReadFileRecord(num2).Col1 == FileAttributes.ContainsNoMetaData)
				{
					return true;
				}
				num2 += 1U;
			}
			return false;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000B1C4 File Offset: 0x000093C4
		public Collection<Resource> ReadResources()
		{
			int num = this.MoveTo(Table.ManifestResource);
			Collection<Resource> collection = new Collection<Resource>(num);
			int i = 1;
			while (i <= num)
			{
				uint num2 = base.ReadUInt32();
				ManifestResourceAttributes manifestResourceAttributes = (ManifestResourceAttributes)base.ReadUInt32();
				string text = this.ReadString();
				MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.Implementation);
				Resource resource;
				if (metadataToken.RID == 0U)
				{
					resource = new EmbeddedResource(text, manifestResourceAttributes, num2, this);
					goto IL_00C6;
				}
				if (metadataToken.TokenType == TokenType.AssemblyRef)
				{
					resource = new AssemblyLinkedResource(text, manifestResourceAttributes)
					{
						Assembly = (AssemblyNameReference)this.GetTypeReferenceScope(metadataToken)
					};
					goto IL_00C6;
				}
				if (metadataToken.TokenType == TokenType.File)
				{
					Row<FileAttributes, string, uint> row = this.ReadFileRecord(metadataToken.RID);
					resource = new LinkedResource(text, manifestResourceAttributes)
					{
						File = row.Col2,
						hash = this.ReadBlob(row.Col3)
					};
					goto IL_00C6;
				}
				IL_00CE:
				i++;
				continue;
				IL_00C6:
				collection.Add(resource);
				goto IL_00CE;
			}
			return collection;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B2AC File Offset: 0x000094AC
		private Row<FileAttributes, string, uint> ReadFileRecord(uint rid)
		{
			int position = this.position;
			if (!this.MoveTo(Table.File, rid))
			{
				throw new ArgumentException();
			}
			Row<FileAttributes, string, uint> row = new Row<FileAttributes, string, uint>((FileAttributes)base.ReadUInt32(), this.ReadString(), this.ReadBlobIndex());
			this.position = position;
			return row;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public byte[] GetManagedResource(uint offset)
		{
			return this.image.GetReaderAt<uint, byte[]>(this.image.Resources.VirtualAddress, offset, delegate(uint o, BinaryStreamReader reader)
			{
				reader.Advance((int)o);
				return reader.ReadBytes(reader.ReadInt32());
			}) ?? Empty<byte>.Array;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B345 File Offset: 0x00009545
		private void PopulateVersionAndFlags(AssemblyNameReference name)
		{
			name.Version = new Version((int)base.ReadUInt16(), (int)base.ReadUInt16(), (int)base.ReadUInt16(), (int)base.ReadUInt16());
			name.Attributes = (AssemblyAttributes)base.ReadUInt32();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000B376 File Offset: 0x00009576
		private void PopulateNameAndCulture(AssemblyNameReference name)
		{
			name.Name = this.ReadString();
			name.Culture = this.ReadString();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B390 File Offset: 0x00009590
		public TypeDefinitionCollection ReadTypes()
		{
			this.InitializeTypeDefinitions();
			TypeDefinition[] types = this.metadata.Types;
			int num = types.Length - this.metadata.NestedTypes.Count;
			TypeDefinitionCollection typeDefinitionCollection = new TypeDefinitionCollection(this.module, num);
			foreach (TypeDefinition typeDefinition in types)
			{
				if (!MetadataReader.IsNested(typeDefinition.Attributes))
				{
					typeDefinitionCollection.Add(typeDefinition);
				}
			}
			if (this.image.HasTable(Table.MethodPtr) || this.image.HasTable(Table.FieldPtr))
			{
				this.CompleteTypes();
			}
			return typeDefinitionCollection;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000B424 File Offset: 0x00009624
		private void CompleteTypes()
		{
			TypeDefinition[] types = this.metadata.Types;
			foreach (TypeDefinition typeDefinition in types)
			{
				Mixin.Read(typeDefinition.Fields);
				Mixin.Read(typeDefinition.Methods);
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000B468 File Offset: 0x00009668
		private void InitializeTypeDefinitions()
		{
			if (this.metadata.Types != null)
			{
				return;
			}
			this.InitializeNestedTypes();
			this.InitializeFields();
			this.InitializeMethods();
			int num = this.MoveTo(Table.TypeDef);
			TypeDefinition[] array = (this.metadata.Types = new TypeDefinition[num]);
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				if (array[(int)num2] == null)
				{
					array[(int)num2] = this.ReadType(num2 + 1U);
				}
				num2 += 1U;
			}
			if (this.module.IsWindowsMetadata())
			{
				uint num3 = 0U;
				while ((ulong)num3 < (ulong)((long)num))
				{
					WindowsRuntimeProjections.Project(array[(int)num3]);
					num3 += 1U;
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000B4FC File Offset: 0x000096FC
		private static bool IsNested(TypeAttributes attributes)
		{
			TypeAttributes typeAttributes = attributes & TypeAttributes.VisibilityMask;
			return typeAttributes - TypeAttributes.NestedPublic <= 5U;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000B518 File Offset: 0x00009718
		public bool HasNestedTypes(TypeDefinition type)
		{
			this.InitializeNestedTypes();
			Collection<uint> collection;
			return this.metadata.TryGetNestedTypeMapping(type, out collection) && collection.Count > 0;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B548 File Offset: 0x00009748
		public Collection<TypeDefinition> ReadNestedTypes(TypeDefinition type)
		{
			this.InitializeNestedTypes();
			Collection<uint> collection;
			if (!this.metadata.TryGetNestedTypeMapping(type, out collection))
			{
				return new MemberDefinitionCollection<TypeDefinition>(type);
			}
			MemberDefinitionCollection<TypeDefinition> memberDefinitionCollection = new MemberDefinitionCollection<TypeDefinition>(type, collection.Count);
			for (int i = 0; i < collection.Count; i++)
			{
				TypeDefinition typeDefinition = this.GetTypeDefinition(collection[i]);
				if (typeDefinition != null)
				{
					memberDefinitionCollection.Add(typeDefinition);
				}
			}
			this.metadata.RemoveNestedTypeMapping(type);
			return memberDefinitionCollection;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B5B8 File Offset: 0x000097B8
		private void InitializeNestedTypes()
		{
			if (this.metadata.NestedTypes != null)
			{
				return;
			}
			int num = this.MoveTo(Table.NestedClass);
			this.metadata.NestedTypes = new Dictionary<uint, Collection<uint>>(num);
			this.metadata.ReverseNestedTypes = new Dictionary<uint, uint>(num);
			if (num != 0)
			{
				for (int i = 1; i <= num; i++)
				{
					uint num2 = this.ReadTableIndex(Table.TypeDef);
					uint num3 = this.ReadTableIndex(Table.TypeDef);
					this.AddNestedMapping(num3, num2);
				}
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000B625 File Offset: 0x00009825
		private void AddNestedMapping(uint declaring, uint nested)
		{
			this.metadata.SetNestedTypeMapping(declaring, MetadataReader.AddMapping<uint, uint>(this.metadata.NestedTypes, declaring, nested));
			this.metadata.SetReverseNestedTypeMapping(nested, declaring);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B654 File Offset: 0x00009854
		private static Collection<TValue> AddMapping<TKey, TValue>(Dictionary<TKey, Collection<TValue>> cache, TKey key, TValue value)
		{
			Collection<TValue> collection;
			if (!cache.TryGetValue(key, ref collection))
			{
				collection = new Collection<TValue>();
			}
			collection.Add(value);
			return collection;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B67C File Offset: 0x0000987C
		private TypeDefinition ReadType(uint rid)
		{
			if (!this.MoveTo(Table.TypeDef, rid))
			{
				return null;
			}
			TypeAttributes typeAttributes = (TypeAttributes)base.ReadUInt32();
			string text = this.ReadString();
			TypeDefinition typeDefinition = new TypeDefinition(this.ReadString(), text, typeAttributes);
			typeDefinition.token = new MetadataToken(TokenType.TypeDef, rid);
			typeDefinition.scope = this.module;
			typeDefinition.module = this.module;
			this.metadata.AddTypeDefinition(typeDefinition);
			this.context = typeDefinition;
			typeDefinition.BaseType = this.GetTypeDefOrRef(this.ReadMetadataToken(CodedIndex.TypeDefOrRef));
			typeDefinition.fields_range = this.ReadFieldsRange(rid);
			typeDefinition.methods_range = this.ReadMethodsRange(rid);
			if (MetadataReader.IsNested(typeAttributes))
			{
				typeDefinition.DeclaringType = this.GetNestedTypeDeclaringType(typeDefinition);
			}
			return typeDefinition;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000B730 File Offset: 0x00009930
		private TypeDefinition GetNestedTypeDeclaringType(TypeDefinition type)
		{
			uint num;
			if (!this.metadata.TryGetReverseNestedTypeMapping(type, out num))
			{
				return null;
			}
			this.metadata.RemoveReverseNestedTypeMapping(type);
			return this.GetTypeDefinition(num);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000B762 File Offset: 0x00009962
		private Range ReadFieldsRange(uint type_index)
		{
			return this.ReadListRange(type_index, Table.TypeDef, Table.Field);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000B76D File Offset: 0x0000996D
		private Range ReadMethodsRange(uint type_index)
		{
			return this.ReadListRange(type_index, Table.TypeDef, Table.Method);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000B778 File Offset: 0x00009978
		private Range ReadListRange(uint current_index, Table current, Table target)
		{
			Range range = default(Range);
			range.Start = this.ReadTableIndex(target);
			TableInformation tableInformation = this.image.TableHeap[current];
			uint num;
			if (current_index == tableInformation.Length)
			{
				num = this.image.TableHeap[target].Length + 1U;
			}
			else
			{
				int position = this.position;
				this.position += (int)((ulong)tableInformation.RowSize - (ulong)((long)this.image.GetTableIndexSize(target)));
				num = this.ReadTableIndex(target);
				this.position = position;
			}
			range.Length = num - range.Start;
			return range;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000B81C File Offset: 0x00009A1C
		public Row<short, int> ReadTypeLayout(TypeDefinition type)
		{
			this.InitializeTypeLayouts();
			uint rid = type.token.RID;
			Row<ushort, uint> row;
			if (!this.metadata.ClassLayouts.TryGetValue(rid, ref row))
			{
				return new Row<short, int>(-1, -1);
			}
			type.PackingSize = (short)row.Col1;
			type.ClassSize = (int)row.Col2;
			this.metadata.ClassLayouts.Remove(rid);
			return new Row<short, int>((short)row.Col1, (int)row.Col2);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000B898 File Offset: 0x00009A98
		private void InitializeTypeLayouts()
		{
			if (this.metadata.ClassLayouts == null)
			{
				int num = this.MoveTo(Table.ClassLayout);
				Dictionary<uint, Row<ushort, uint>> dictionary = (this.metadata.ClassLayouts = new Dictionary<uint, Row<ushort, uint>>(num));
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)num))
				{
					ushort num3 = base.ReadUInt16();
					uint num4 = base.ReadUInt32();
					uint num5 = this.ReadTableIndex(Table.TypeDef);
					dictionary.Add(num5, new Row<ushort, uint>(num3, num4));
					num2 += 1U;
				}
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000B908 File Offset: 0x00009B08
		public TypeReference GetTypeDefOrRef(MetadataToken token)
		{
			return (TypeReference)this.LookupToken(token);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000B918 File Offset: 0x00009B18
		public TypeDefinition GetTypeDefinition(uint rid)
		{
			this.InitializeTypeDefinitions();
			TypeDefinition typeDefinition = this.metadata.GetTypeDefinition(rid);
			if (typeDefinition != null)
			{
				return typeDefinition;
			}
			typeDefinition = this.ReadTypeDefinition(rid);
			if (this.module.IsWindowsMetadata())
			{
				WindowsRuntimeProjections.Project(typeDefinition);
			}
			return typeDefinition;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B959 File Offset: 0x00009B59
		private TypeDefinition ReadTypeDefinition(uint rid)
		{
			if (!this.MoveTo(Table.TypeDef, rid))
			{
				return null;
			}
			return this.ReadType(rid);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B96E File Offset: 0x00009B6E
		private void InitializeTypeReferences()
		{
			if (this.metadata.TypeReferences == null)
			{
				this.metadata.TypeReferences = new TypeReference[this.image.GetTableLength(Table.TypeRef)];
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000B99C File Offset: 0x00009B9C
		public TypeReference GetTypeReference(string scope, string full_name)
		{
			this.InitializeTypeReferences();
			int num = this.metadata.TypeReferences.Length;
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				TypeReference typeReference = this.GetTypeReference(num2);
				if (!(typeReference.FullName != full_name))
				{
					if (string.IsNullOrEmpty(scope))
					{
						return typeReference;
					}
					if (typeReference.Scope.Name == scope)
					{
						return typeReference;
					}
				}
				num2 += 1U;
			}
			return null;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000BA04 File Offset: 0x00009C04
		private TypeReference GetTypeReference(uint rid)
		{
			this.InitializeTypeReferences();
			TypeReference typeReference = this.metadata.GetTypeReference(rid);
			if (typeReference != null)
			{
				return typeReference;
			}
			return this.ReadTypeReference(rid);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000BA30 File Offset: 0x00009C30
		private TypeReference ReadTypeReference(uint rid)
		{
			if (!this.MoveTo(Table.TypeRef, rid))
			{
				return null;
			}
			TypeReference typeReference = null;
			MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.ResolutionScope);
			string text = this.ReadString();
			TypeReference typeReference2 = new TypeReference(this.ReadString(), text, this.module, null);
			typeReference2.token = new MetadataToken(TokenType.TypeRef, rid);
			this.metadata.AddTypeReference(typeReference2);
			IMetadataScope metadataScope2;
			if (metadataToken.TokenType == TokenType.TypeRef)
			{
				typeReference = this.GetTypeDefOrRef(metadataToken);
				object obj;
				if (typeReference == null)
				{
					IMetadataScope metadataScope = this.module;
					obj = metadataScope;
				}
				else
				{
					obj = typeReference.Scope;
				}
				metadataScope2 = (IMetadataScope)obj;
			}
			else
			{
				metadataScope2 = this.GetTypeReferenceScope(metadataToken);
			}
			typeReference2.scope = metadataScope2;
			typeReference2.DeclaringType = typeReference;
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference2);
			if (typeReference2.Module.IsWindowsMetadata())
			{
				WindowsRuntimeProjections.Project(typeReference2);
			}
			return typeReference2;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		private IMetadataScope GetTypeReferenceScope(MetadataToken scope)
		{
			if (scope.TokenType == TokenType.Module)
			{
				return this.module;
			}
			TokenType tokenType = scope.TokenType;
			IMetadataScope[] array2;
			if (tokenType != TokenType.ModuleRef)
			{
				if (tokenType != TokenType.AssemblyRef)
				{
					throw new NotSupportedException();
				}
				this.InitializeAssemblyReferences();
				IMetadataScope[] array = this.metadata.AssemblyReferences;
				array2 = array;
			}
			else
			{
				this.InitializeModuleReferences();
				IMetadataScope[] array = this.metadata.ModuleReferences;
				array2 = array;
			}
			uint num = scope.RID - 1U;
			if (num < 0U || (ulong)num >= (ulong)((long)array2.Length))
			{
				return null;
			}
			return array2[(int)num];
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000BB7C File Offset: 0x00009D7C
		public IEnumerable<TypeReference> GetTypeReferences()
		{
			this.InitializeTypeReferences();
			int tableLength = this.image.GetTableLength(Table.TypeRef);
			TypeReference[] array = new TypeReference[tableLength];
			uint num = 1U;
			while ((ulong)num <= (ulong)((long)tableLength))
			{
				array[(int)(num - 1U)] = this.GetTypeReference(num);
				num += 1U;
			}
			return array;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000BBC0 File Offset: 0x00009DC0
		private TypeReference GetTypeSpecification(uint rid)
		{
			if (!this.MoveTo(Table.TypeSpec, rid))
			{
				return null;
			}
			TypeReference typeReference = this.ReadSignature(this.ReadBlobIndex()).ReadTypeSignature();
			if (typeReference.token.RID == 0U)
			{
				typeReference.token = new MetadataToken(TokenType.TypeSpec, rid);
			}
			return typeReference;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000BC0B File Offset: 0x00009E0B
		private SignatureReader ReadSignature(uint signature)
		{
			return new SignatureReader(signature, this);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000BC14 File Offset: 0x00009E14
		public bool HasInterfaces(TypeDefinition type)
		{
			this.InitializeInterfaces();
			Collection<Row<uint, MetadataToken>> collection;
			return this.metadata.TryGetInterfaceMapping(type, out collection);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000BC38 File Offset: 0x00009E38
		public InterfaceImplementationCollection ReadInterfaces(TypeDefinition type)
		{
			this.InitializeInterfaces();
			Collection<Row<uint, MetadataToken>> collection;
			if (!this.metadata.TryGetInterfaceMapping(type, out collection))
			{
				return new InterfaceImplementationCollection(type);
			}
			InterfaceImplementationCollection interfaceImplementationCollection = new InterfaceImplementationCollection(type, collection.Count);
			this.context = type;
			for (int i = 0; i < collection.Count; i++)
			{
				interfaceImplementationCollection.Add(new InterfaceImplementation(this.GetTypeDefOrRef(collection[i].Col2), new MetadataToken(TokenType.InterfaceImpl, collection[i].Col1)));
			}
			this.metadata.RemoveInterfaceMapping(type);
			return interfaceImplementationCollection;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		private void InitializeInterfaces()
		{
			if (this.metadata.Interfaces == null)
			{
				int num = this.MoveTo(Table.InterfaceImpl);
				this.metadata.Interfaces = new Dictionary<uint, Collection<Row<uint, MetadataToken>>>(num);
				uint num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					uint num3 = this.ReadTableIndex(Table.TypeDef);
					MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.TypeDefOrRef);
					this.AddInterfaceMapping(num3, new Row<uint, MetadataToken>(num2, metadataToken));
					num2 += 1U;
				}
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000BD28 File Offset: 0x00009F28
		private void AddInterfaceMapping(uint type, Row<uint, MetadataToken> @interface)
		{
			this.metadata.SetInterfaceMapping(type, MetadataReader.AddMapping<uint, Row<uint, MetadataToken>>(this.metadata.Interfaces, type, @interface));
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000BD48 File Offset: 0x00009F48
		public Collection<FieldDefinition> ReadFields(TypeDefinition type)
		{
			Range fields_range = type.fields_range;
			if (fields_range.Length == 0U)
			{
				return new MemberDefinitionCollection<FieldDefinition>(type);
			}
			MemberDefinitionCollection<FieldDefinition> memberDefinitionCollection = new MemberDefinitionCollection<FieldDefinition>(type, (int)fields_range.Length);
			this.context = type;
			if (!this.MoveTo(Table.FieldPtr, fields_range.Start))
			{
				if (!this.MoveTo(Table.Field, fields_range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < fields_range.Length; num += 1U)
				{
					this.ReadField(fields_range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<FieldDefinition>(Table.FieldPtr, Table.Field, fields_range, memberDefinitionCollection, new Action<uint, Collection<FieldDefinition>>(this.ReadField));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		private void ReadField(uint field_rid, Collection<FieldDefinition> fields)
		{
			FieldAttributes fieldAttributes = (FieldAttributes)base.ReadUInt16();
			string text = this.ReadString();
			uint num = this.ReadBlobIndex();
			FieldDefinition fieldDefinition = new FieldDefinition(text, fieldAttributes, this.ReadFieldType(num));
			fieldDefinition.token = new MetadataToken(TokenType.Field, field_rid);
			this.metadata.AddFieldDefinition(fieldDefinition);
			if (!MetadataReader.IsDeleted(fieldDefinition))
			{
				fields.Add(fieldDefinition);
				if (this.module.IsWindowsMetadata())
				{
					WindowsRuntimeProjections.Project(fieldDefinition);
				}
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000BE48 File Offset: 0x0000A048
		private void InitializeFields()
		{
			if (this.metadata.Fields == null)
			{
				this.metadata.Fields = new FieldDefinition[this.image.GetTableLength(Table.Field)];
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000BE74 File Offset: 0x0000A074
		private TypeReference ReadFieldType(uint signature)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			if (signatureReader.ReadByte() != 6)
			{
				throw new NotSupportedException();
			}
			return signatureReader.ReadTypeSignature();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
		public int ReadFieldRVA(FieldDefinition field)
		{
			this.InitializeFieldRVAs();
			uint rid = field.token.RID;
			uint num;
			if (!this.metadata.FieldRVAs.TryGetValue(rid, ref num))
			{
				return 0;
			}
			int fieldTypeSize = MetadataReader.GetFieldTypeSize(field.FieldType);
			if (fieldTypeSize == 0 || num == 0U)
			{
				return 0;
			}
			this.metadata.FieldRVAs.Remove(rid);
			field.InitialValue = this.GetFieldInitializeValue(fieldTypeSize, num);
			return (int)num;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000BF0B File Offset: 0x0000A10B
		private byte[] GetFieldInitializeValue(int size, uint rva)
		{
			return this.image.GetReaderAt<int, byte[]>(rva, size, (int s, BinaryStreamReader reader) => reader.ReadBytes(s)) ?? Empty<byte>.Array;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000BF44 File Offset: 0x0000A144
		private static int GetFieldTypeSize(TypeReference type)
		{
			int num = 0;
			switch (type.etype)
			{
			case ElementType.Boolean:
			case ElementType.I1:
			case ElementType.U1:
				return 1;
			case ElementType.Char:
			case ElementType.I2:
			case ElementType.U2:
				return 2;
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.R4:
				return 4;
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R8:
				return 8;
			case ElementType.Ptr:
			case ElementType.FnPtr:
				return IntPtr.Size;
			case ElementType.CModReqD:
			case ElementType.CModOpt:
				return MetadataReader.GetFieldTypeSize(((IModifierType)type).ElementType);
			}
			TypeDefinition typeDefinition = type.Resolve();
			if (typeDefinition != null && typeDefinition.HasLayoutInfo)
			{
				num = typeDefinition.ClassSize;
			}
			return num;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000C024 File Offset: 0x0000A224
		private void InitializeFieldRVAs()
		{
			if (this.metadata.FieldRVAs == null)
			{
				int num = this.MoveTo(Table.FieldRVA);
				Dictionary<uint, uint> dictionary = (this.metadata.FieldRVAs = new Dictionary<uint, uint>(num));
				for (int i = 0; i < num; i++)
				{
					uint num2 = base.ReadUInt32();
					uint num3 = this.ReadTableIndex(Table.Field);
					dictionary.Add(num3, num2);
				}
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000C084 File Offset: 0x0000A284
		public int ReadFieldLayout(FieldDefinition field)
		{
			this.InitializeFieldLayouts();
			uint rid = field.token.RID;
			uint num;
			if (!this.metadata.FieldLayouts.TryGetValue(rid, ref num))
			{
				return -1;
			}
			this.metadata.FieldLayouts.Remove(rid);
			return (int)num;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000C0D0 File Offset: 0x0000A2D0
		private void InitializeFieldLayouts()
		{
			if (this.metadata.FieldLayouts == null)
			{
				int num = this.MoveTo(Table.FieldLayout);
				Dictionary<uint, uint> dictionary = (this.metadata.FieldLayouts = new Dictionary<uint, uint>(num));
				for (int i = 0; i < num; i++)
				{
					uint num2 = base.ReadUInt32();
					uint num3 = this.ReadTableIndex(Table.Field);
					dictionary.Add(num3, num2);
				}
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000C130 File Offset: 0x0000A330
		public bool HasEvents(TypeDefinition type)
		{
			this.InitializeEvents();
			Range range;
			return this.metadata.TryGetEventsRange(type, out range) && range.Length > 0U;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C160 File Offset: 0x0000A360
		public Collection<EventDefinition> ReadEvents(TypeDefinition type)
		{
			this.InitializeEvents();
			Range range;
			if (!this.metadata.TryGetEventsRange(type, out range))
			{
				return new MemberDefinitionCollection<EventDefinition>(type);
			}
			MemberDefinitionCollection<EventDefinition> memberDefinitionCollection = new MemberDefinitionCollection<EventDefinition>(type, (int)range.Length);
			this.metadata.RemoveEventsRange(type);
			if (range.Length == 0U)
			{
				return memberDefinitionCollection;
			}
			this.context = type;
			if (!this.MoveTo(Table.EventPtr, range.Start))
			{
				if (!this.MoveTo(Table.Event, range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < range.Length; num += 1U)
				{
					this.ReadEvent(range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<EventDefinition>(Table.EventPtr, Table.Event, range, memberDefinitionCollection, new Action<uint, Collection<EventDefinition>>(this.ReadEvent));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000C214 File Offset: 0x0000A414
		private void ReadEvent(uint event_rid, Collection<EventDefinition> events)
		{
			EventAttributes eventAttributes = (EventAttributes)base.ReadUInt16();
			string text = this.ReadString();
			TypeReference typeDefOrRef = this.GetTypeDefOrRef(this.ReadMetadataToken(CodedIndex.TypeDefOrRef));
			EventDefinition eventDefinition = new EventDefinition(text, eventAttributes, typeDefOrRef);
			eventDefinition.token = new MetadataToken(TokenType.Event, event_rid);
			if (!MetadataReader.IsDeleted(eventDefinition))
			{
				events.Add(eventDefinition);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C268 File Offset: 0x0000A468
		private void InitializeEvents()
		{
			if (this.metadata.Events == null)
			{
				int num = this.MoveTo(Table.EventMap);
				this.metadata.Events = new Dictionary<uint, Range>(num);
				uint num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					uint num3 = this.ReadTableIndex(Table.TypeDef);
					Range range = this.ReadEventsRange(num2);
					this.metadata.AddEventsRange(num3, range);
					num2 += 1U;
				}
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		private Range ReadEventsRange(uint rid)
		{
			return this.ReadListRange(rid, Table.EventMap, Table.Event);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		public bool HasProperties(TypeDefinition type)
		{
			this.InitializeProperties();
			Range range;
			return this.metadata.TryGetPropertiesRange(type, out range) && range.Length > 0U;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000C304 File Offset: 0x0000A504
		public Collection<PropertyDefinition> ReadProperties(TypeDefinition type)
		{
			this.InitializeProperties();
			Range range;
			if (!this.metadata.TryGetPropertiesRange(type, out range))
			{
				return new MemberDefinitionCollection<PropertyDefinition>(type);
			}
			this.metadata.RemovePropertiesRange(type);
			MemberDefinitionCollection<PropertyDefinition> memberDefinitionCollection = new MemberDefinitionCollection<PropertyDefinition>(type, (int)range.Length);
			if (range.Length == 0U)
			{
				return memberDefinitionCollection;
			}
			this.context = type;
			if (!this.MoveTo(Table.PropertyPtr, range.Start))
			{
				if (!this.MoveTo(Table.Property, range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < range.Length; num += 1U)
				{
					this.ReadProperty(range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<PropertyDefinition>(Table.PropertyPtr, Table.Property, range, memberDefinitionCollection, new Action<uint, Collection<PropertyDefinition>>(this.ReadProperty));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		private void ReadProperty(uint property_rid, Collection<PropertyDefinition> properties)
		{
			PropertyAttributes propertyAttributes = (PropertyAttributes)base.ReadUInt16();
			string text = this.ReadString();
			uint num = this.ReadBlobIndex();
			SignatureReader signatureReader = this.ReadSignature(num);
			byte b = signatureReader.ReadByte();
			if ((b & 8) == 0)
			{
				throw new NotSupportedException();
			}
			bool flag = (b & 32) > 0;
			signatureReader.ReadCompressedUInt32();
			PropertyDefinition propertyDefinition = new PropertyDefinition(text, propertyAttributes, signatureReader.ReadTypeSignature());
			propertyDefinition.HasThis = flag;
			propertyDefinition.token = new MetadataToken(TokenType.Property, property_rid);
			if (!MetadataReader.IsDeleted(propertyDefinition))
			{
				properties.Add(propertyDefinition);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C444 File Offset: 0x0000A644
		private void InitializeProperties()
		{
			if (this.metadata.Properties == null)
			{
				int num = this.MoveTo(Table.PropertyMap);
				this.metadata.Properties = new Dictionary<uint, Range>(num);
				uint num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					uint num3 = this.ReadTableIndex(Table.TypeDef);
					Range range = this.ReadPropertiesRange(num2);
					this.metadata.AddPropertiesRange(num3, range);
					num2 += 1U;
				}
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C4A3 File Offset: 0x0000A6A3
		private Range ReadPropertiesRange(uint rid)
		{
			return this.ReadListRange(rid, Table.PropertyMap, Table.Property);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
		private MethodSemanticsAttributes ReadMethodSemantics(MethodDefinition method)
		{
			this.InitializeMethodSemantics();
			Row<MethodSemanticsAttributes, MetadataToken> row;
			if (!this.metadata.Semantics.TryGetValue(method.token.RID, ref row))
			{
				return MethodSemanticsAttributes.None;
			}
			TypeDefinition declaringType = method.DeclaringType;
			MethodSemanticsAttributes col = row.Col1;
			if (col <= MethodSemanticsAttributes.AddOn)
			{
				switch (col)
				{
				case MethodSemanticsAttributes.Setter:
					MetadataReader.GetProperty(declaringType, row.Col2).set_method = method;
					goto IL_016B;
				case MethodSemanticsAttributes.Getter:
					MetadataReader.GetProperty(declaringType, row.Col2).get_method = method;
					goto IL_016B;
				case MethodSemanticsAttributes.Setter | MethodSemanticsAttributes.Getter:
					break;
				case MethodSemanticsAttributes.Other:
				{
					TokenType tokenType = row.Col2.TokenType;
					if (tokenType == TokenType.Event)
					{
						EventDefinition @event = MetadataReader.GetEvent(declaringType, row.Col2);
						if (@event.other_methods == null)
						{
							@event.other_methods = new Collection<MethodDefinition>();
						}
						@event.other_methods.Add(method);
						goto IL_016B;
					}
					if (tokenType != TokenType.Property)
					{
						throw new NotSupportedException();
					}
					PropertyDefinition property = MetadataReader.GetProperty(declaringType, row.Col2);
					if (property.other_methods == null)
					{
						property.other_methods = new Collection<MethodDefinition>();
					}
					property.other_methods.Add(method);
					goto IL_016B;
				}
				default:
					if (col == MethodSemanticsAttributes.AddOn)
					{
						MetadataReader.GetEvent(declaringType, row.Col2).add_method = method;
						goto IL_016B;
					}
					break;
				}
			}
			else
			{
				if (col == MethodSemanticsAttributes.RemoveOn)
				{
					MetadataReader.GetEvent(declaringType, row.Col2).remove_method = method;
					goto IL_016B;
				}
				if (col == MethodSemanticsAttributes.Fire)
				{
					MetadataReader.GetEvent(declaringType, row.Col2).invoke_method = method;
					goto IL_016B;
				}
			}
			throw new NotSupportedException();
			IL_016B:
			this.metadata.Semantics.Remove(method.token.RID);
			return row.Col1;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000C64A File Offset: 0x0000A84A
		private static EventDefinition GetEvent(TypeDefinition type, MetadataToken token)
		{
			if (token.TokenType != TokenType.Event)
			{
				throw new ArgumentException();
			}
			return MetadataReader.GetMember<EventDefinition>(type.Events, token);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C66C File Offset: 0x0000A86C
		private static PropertyDefinition GetProperty(TypeDefinition type, MetadataToken token)
		{
			if (token.TokenType != TokenType.Property)
			{
				throw new ArgumentException();
			}
			return MetadataReader.GetMember<PropertyDefinition>(type.Properties, token);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000C690 File Offset: 0x0000A890
		private static TMember GetMember<TMember>(Collection<TMember> members, MetadataToken token) where TMember : IMemberDefinition
		{
			for (int i = 0; i < members.Count; i++)
			{
				TMember tmember = members[i];
				if (tmember.MetadataToken == token)
				{
					return tmember;
				}
			}
			throw new ArgumentException();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
		private void InitializeMethodSemantics()
		{
			if (this.metadata.Semantics == null)
			{
				int num = this.MoveTo(Table.MethodSemantics);
				Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>> dictionary = (this.metadata.Semantics = new Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>>(0));
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)num))
				{
					MethodSemanticsAttributes methodSemanticsAttributes = (MethodSemanticsAttributes)base.ReadUInt16();
					uint num3 = this.ReadTableIndex(Table.Method);
					MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.HasSemantics);
					dictionary[num3] = new Row<MethodSemanticsAttributes, MetadataToken>(methodSemanticsAttributes, metadataToken);
					num2 += 1U;
				}
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000C745 File Offset: 0x0000A945
		public PropertyDefinition ReadMethods(PropertyDefinition property)
		{
			this.ReadAllSemantics(property.DeclaringType);
			return property;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000C754 File Offset: 0x0000A954
		public EventDefinition ReadMethods(EventDefinition @event)
		{
			this.ReadAllSemantics(@event.DeclaringType);
			return @event;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000C763 File Offset: 0x0000A963
		public MethodSemanticsAttributes ReadAllSemantics(MethodDefinition method)
		{
			this.ReadAllSemantics(method.DeclaringType);
			return method.SemanticsAttributes;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000C778 File Offset: 0x0000A978
		private void ReadAllSemantics(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (!methodDefinition.sem_attrs_ready)
				{
					methodDefinition.sem_attrs = this.ReadMethodSemantics(methodDefinition);
					methodDefinition.sem_attrs_ready = true;
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000C7C5 File Offset: 0x0000A9C5
		private Range ReadParametersRange(uint method_rid)
		{
			return this.ReadListRange(method_rid, Table.Method, Table.Param);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000C7D0 File Offset: 0x0000A9D0
		public Collection<MethodDefinition> ReadMethods(TypeDefinition type)
		{
			Range methods_range = type.methods_range;
			if (methods_range.Length == 0U)
			{
				return new MemberDefinitionCollection<MethodDefinition>(type);
			}
			MemberDefinitionCollection<MethodDefinition> memberDefinitionCollection = new MemberDefinitionCollection<MethodDefinition>(type, (int)methods_range.Length);
			if (!this.MoveTo(Table.MethodPtr, methods_range.Start))
			{
				if (!this.MoveTo(Table.Method, methods_range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0U; num < methods_range.Length; num += 1U)
				{
					this.ReadMethod(methods_range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				this.ReadPointers<MethodDefinition>(Table.MethodPtr, Table.Method, methods_range, memberDefinitionCollection, new Action<uint, Collection<MethodDefinition>>(this.ReadMethod));
			}
			return memberDefinitionCollection;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000C85C File Offset: 0x0000AA5C
		private void ReadPointers<TMember>(Table ptr, Table table, Range range, Collection<TMember> members, Action<uint, Collection<TMember>> reader) where TMember : IMemberDefinition
		{
			for (uint num = 0U; num < range.Length; num += 1U)
			{
				this.MoveTo(ptr, range.Start + num);
				uint num2 = this.ReadTableIndex(table);
				this.MoveTo(table, num2);
				reader.Invoke(num2, members);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000C8A5 File Offset: 0x0000AAA5
		private static bool IsDeleted(IMemberDefinition member)
		{
			return member.IsSpecialName && member.Name == "_Deleted";
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000C8C1 File Offset: 0x0000AAC1
		private void InitializeMethods()
		{
			if (this.metadata.Methods == null)
			{
				this.metadata.Methods = new MethodDefinition[this.image.GetTableLength(Table.Method)];
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		private void ReadMethod(uint method_rid, Collection<MethodDefinition> methods)
		{
			MethodDefinition methodDefinition = new MethodDefinition();
			methodDefinition.rva = base.ReadUInt32();
			methodDefinition.ImplAttributes = (MethodImplAttributes)base.ReadUInt16();
			methodDefinition.Attributes = (MethodAttributes)base.ReadUInt16();
			methodDefinition.Name = this.ReadString();
			methodDefinition.token = new MetadataToken(TokenType.Method, method_rid);
			if (!MetadataReader.IsDeleted(methodDefinition))
			{
				methods.Add(methodDefinition);
				uint num = this.ReadBlobIndex();
				Range range = this.ReadParametersRange(method_rid);
				this.context = methodDefinition;
				this.ReadMethodSignature(num, methodDefinition);
				this.metadata.AddMethodDefinition(methodDefinition);
				if (range.Length != 0U)
				{
					int position = this.position;
					this.ReadParameters(methodDefinition, range);
					this.position = position;
				}
				if (this.module.IsWindowsMetadata())
				{
					WindowsRuntimeProjections.Project(methodDefinition);
				}
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000C9AC File Offset: 0x0000ABAC
		private void ReadParameters(MethodDefinition method, Range param_range)
		{
			if (!this.MoveTo(Table.ParamPtr, param_range.Start))
			{
				if (this.MoveTo(Table.Param, param_range.Start))
				{
					for (uint num = 0U; num < param_range.Length; num += 1U)
					{
						this.ReadParameter(param_range.Start + num, method);
					}
					return;
				}
			}
			else
			{
				this.ReadParameterPointers(method, param_range);
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000CA00 File Offset: 0x0000AC00
		private void ReadParameterPointers(MethodDefinition method, Range range)
		{
			for (uint num = 0U; num < range.Length; num += 1U)
			{
				this.MoveTo(Table.ParamPtr, range.Start + num);
				uint num2 = this.ReadTableIndex(Table.Param);
				this.MoveTo(Table.Param, num2);
				this.ReadParameter(num2, method);
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000CA48 File Offset: 0x0000AC48
		private void ReadParameter(uint param_rid, MethodDefinition method)
		{
			ParameterAttributes parameterAttributes = (ParameterAttributes)base.ReadUInt16();
			ushort num = base.ReadUInt16();
			string text = this.ReadString();
			ParameterDefinition parameterDefinition = ((num == 0) ? method.MethodReturnType.Parameter : method.Parameters[(int)(num - 1)]);
			parameterDefinition.token = new MetadataToken(TokenType.Param, param_rid);
			parameterDefinition.Name = text;
			parameterDefinition.Attributes = parameterAttributes;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000CAA8 File Offset: 0x0000ACA8
		private void ReadMethodSignature(uint signature, IMethodSignature method)
		{
			this.ReadSignature(signature).ReadMethodSignature(method);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public PInvokeInfo ReadPInvokeInfo(MethodDefinition method)
		{
			this.InitializePInvokes();
			uint rid = method.token.RID;
			Row<PInvokeAttributes, uint, uint> row;
			if (!this.metadata.PInvokes.TryGetValue(rid, ref row))
			{
				return null;
			}
			this.metadata.PInvokes.Remove(rid);
			return new PInvokeInfo(row.Col1, this.image.StringHeap.Read(row.Col2), this.module.ModuleReferences[(int)(row.Col3 - 1U)]);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		private void InitializePInvokes()
		{
			if (this.metadata.PInvokes != null)
			{
				return;
			}
			int num = this.MoveTo(Table.ImplMap);
			Dictionary<uint, Row<PInvokeAttributes, uint, uint>> dictionary = (this.metadata.PInvokes = new Dictionary<uint, Row<PInvokeAttributes, uint, uint>>(num));
			for (int i = 1; i <= num; i++)
			{
				PInvokeAttributes pinvokeAttributes = (PInvokeAttributes)base.ReadUInt16();
				MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.MemberForwarded);
				uint num2 = this.ReadStringIndex();
				uint num3 = this.ReadTableIndex(Table.File);
				if (metadataToken.TokenType == TokenType.Method)
				{
					dictionary.Add(metadataToken.RID, new Row<PInvokeAttributes, uint, uint>(pinvokeAttributes, num2, num3));
				}
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		public bool HasGenericParameters(IGenericParameterProvider provider)
		{
			this.InitializeGenericParameters();
			Range[] array;
			return this.metadata.TryGetGenericParameterRanges(provider, out array) && MetadataReader.RangesSize(array) > 0;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public Collection<GenericParameter> ReadGenericParameters(IGenericParameterProvider provider)
		{
			this.InitializeGenericParameters();
			Range[] array;
			if (!this.metadata.TryGetGenericParameterRanges(provider, out array))
			{
				return new GenericParameterCollection(provider);
			}
			this.metadata.RemoveGenericParameterRange(provider);
			GenericParameterCollection genericParameterCollection = new GenericParameterCollection(provider, MetadataReader.RangesSize(array));
			for (int i = 0; i < array.Length; i++)
			{
				this.ReadGenericParametersRange(array[i], provider, genericParameterCollection);
			}
			return genericParameterCollection;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000CC60 File Offset: 0x0000AE60
		private void ReadGenericParametersRange(Range range, IGenericParameterProvider provider, GenericParameterCollection generic_parameters)
		{
			if (this.MoveTo(Table.GenericParam, range.Start))
			{
				for (uint num = 0U; num < range.Length; num += 1U)
				{
					base.ReadUInt16();
					GenericParameterAttributes genericParameterAttributes = (GenericParameterAttributes)base.ReadUInt16();
					this.ReadMetadataToken(CodedIndex.TypeOrMethodDef);
					generic_parameters.Add(new GenericParameter(this.ReadString(), provider)
					{
						token = new MetadataToken(TokenType.GenericParam, range.Start + num),
						Attributes = genericParameterAttributes
					});
				}
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000CCD8 File Offset: 0x0000AED8
		private void InitializeGenericParameters()
		{
			if (this.metadata.GenericParameters == null)
			{
				this.metadata.GenericParameters = this.InitializeRanges(Table.GenericParam, delegate
				{
					base.Advance(4);
					MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.TypeOrMethodDef);
					this.ReadStringIndex();
					return metadataToken;
				});
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000CD08 File Offset: 0x0000AF08
		private Dictionary<MetadataToken, Range[]> InitializeRanges(Table table, Func<MetadataToken> get_next)
		{
			int num = this.MoveTo(table);
			Dictionary<MetadataToken, Range[]> dictionary = new Dictionary<MetadataToken, Range[]>(num);
			if (num == 0)
			{
				return dictionary;
			}
			MetadataToken metadataToken = MetadataToken.Zero;
			Range range = new Range(1U, 0U);
			uint num2 = 1U;
			while ((ulong)num2 <= (ulong)((long)num))
			{
				MetadataToken metadataToken2 = get_next.Invoke();
				if (num2 == 1U)
				{
					metadataToken = metadataToken2;
					range.Length += 1U;
				}
				else if (metadataToken2 != metadataToken)
				{
					MetadataReader.AddRange(dictionary, metadataToken, range);
					range = new Range(num2, 1U);
					metadataToken = metadataToken2;
				}
				else
				{
					range.Length += 1U;
				}
				num2 += 1U;
			}
			MetadataReader.AddRange(dictionary, metadataToken, range);
			return dictionary;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		private static void AddRange(Dictionary<MetadataToken, Range[]> ranges, MetadataToken owner, Range range)
		{
			if (owner.RID != 0U)
			{
				Range[] array;
				if (!ranges.TryGetValue(owner, ref array))
				{
					ranges.Add(owner, new Range[] { range });
					return;
				}
				ranges[owner] = array.Add(range);
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public bool HasGenericConstraints(GenericParameter generic_parameter)
		{
			this.InitializeGenericConstraints();
			Collection<MetadataToken> collection;
			return this.metadata.TryGetGenericConstraintMapping(generic_parameter, out collection) && collection.Count > 0;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000CE18 File Offset: 0x0000B018
		public Collection<TypeReference> ReadGenericConstraints(GenericParameter generic_parameter)
		{
			this.InitializeGenericConstraints();
			Collection<MetadataToken> collection;
			if (!this.metadata.TryGetGenericConstraintMapping(generic_parameter, out collection))
			{
				return new Collection<TypeReference>();
			}
			Collection<TypeReference> collection2 = new Collection<TypeReference>(collection.Count);
			this.context = (IGenericContext)generic_parameter.Owner;
			for (int i = 0; i < collection.Count; i++)
			{
				collection2.Add(this.GetTypeDefOrRef(collection[i]));
			}
			this.metadata.RemoveGenericConstraintMapping(generic_parameter);
			return collection2;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000CE90 File Offset: 0x0000B090
		private void InitializeGenericConstraints()
		{
			if (this.metadata.GenericConstraints == null)
			{
				int num = this.MoveTo(Table.GenericParamConstraint);
				this.metadata.GenericConstraints = new Dictionary<uint, Collection<MetadataToken>>(num);
				for (int i = 1; i <= num; i++)
				{
					this.AddGenericConstraintMapping(this.ReadTableIndex(Table.GenericParam), this.ReadMetadataToken(CodedIndex.TypeDefOrRef));
				}
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000CEE5 File Offset: 0x0000B0E5
		private void AddGenericConstraintMapping(uint generic_parameter, MetadataToken constraint)
		{
			this.metadata.SetGenericConstraintMapping(generic_parameter, MetadataReader.AddMapping<uint, MetadataToken>(this.metadata.GenericConstraints, generic_parameter, constraint));
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000CF08 File Offset: 0x0000B108
		public bool HasOverrides(MethodDefinition method)
		{
			this.InitializeOverrides();
			Collection<MetadataToken> collection;
			return this.metadata.TryGetOverrideMapping(method, out collection) && collection.Count > 0;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000CF38 File Offset: 0x0000B138
		public Collection<MethodReference> ReadOverrides(MethodDefinition method)
		{
			this.InitializeOverrides();
			Collection<MetadataToken> collection;
			if (!this.metadata.TryGetOverrideMapping(method, out collection))
			{
				return new Collection<MethodReference>();
			}
			Collection<MethodReference> collection2 = new Collection<MethodReference>(collection.Count);
			this.context = method;
			for (int i = 0; i < collection.Count; i++)
			{
				collection2.Add((MethodReference)this.LookupToken(collection[i]));
			}
			this.metadata.RemoveOverrideMapping(method);
			return collection2;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		private void InitializeOverrides()
		{
			if (this.metadata.Overrides != null)
			{
				return;
			}
			int num = this.MoveTo(Table.MethodImpl);
			this.metadata.Overrides = new Dictionary<uint, Collection<MetadataToken>>(num);
			for (int i = 1; i <= num; i++)
			{
				this.ReadTableIndex(Table.TypeDef);
				MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.MethodDefOrRef);
				if (metadataToken.TokenType != TokenType.Method)
				{
					throw new NotSupportedException();
				}
				MetadataToken metadataToken2 = this.ReadMetadataToken(CodedIndex.MethodDefOrRef);
				this.AddOverrideMapping(metadataToken.RID, metadataToken2);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000D027 File Offset: 0x0000B227
		private void AddOverrideMapping(uint method_rid, MetadataToken @override)
		{
			this.metadata.SetOverrideMapping(method_rid, MetadataReader.AddMapping<uint, MetadataToken>(this.metadata.Overrides, method_rid, @override));
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000D047 File Offset: 0x0000B247
		public MethodBody ReadMethodBody(MethodDefinition method)
		{
			return this.code.ReadMethodBody(method);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000D055 File Offset: 0x0000B255
		public int ReadCodeSize(MethodDefinition method)
		{
			return this.code.ReadCodeSize(method);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000D064 File Offset: 0x0000B264
		public CallSite ReadCallSite(MetadataToken token)
		{
			if (!this.MoveTo(Table.StandAloneSig, token.RID))
			{
				return null;
			}
			uint num = this.ReadBlobIndex();
			CallSite callSite = new CallSite();
			this.ReadMethodSignature(num, callSite);
			callSite.MetadataToken = token;
			return callSite;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		public VariableDefinitionCollection ReadVariables(MetadataToken local_var_token)
		{
			if (!this.MoveTo(Table.StandAloneSig, local_var_token.RID))
			{
				return null;
			}
			SignatureReader signatureReader = this.ReadSignature(this.ReadBlobIndex());
			if (signatureReader.ReadByte() != 7)
			{
				throw new NotSupportedException();
			}
			uint num = signatureReader.ReadCompressedUInt32();
			if (num == 0U)
			{
				return null;
			}
			VariableDefinitionCollection variableDefinitionCollection = new VariableDefinitionCollection((int)num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				variableDefinitionCollection.Add(new VariableDefinition(signatureReader.ReadTypeSignature()));
				num2++;
			}
			return variableDefinitionCollection;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000D114 File Offset: 0x0000B314
		public IMetadataTokenProvider LookupToken(MetadataToken token)
		{
			uint rid = token.RID;
			if (rid == 0U)
			{
				return null;
			}
			if (this.metadata_reader != null)
			{
				return this.metadata_reader.LookupToken(token);
			}
			int position = this.position;
			IGenericContext genericContext = this.context;
			TokenType tokenType = token.TokenType;
			IMetadataTokenProvider metadataTokenProvider;
			if (tokenType <= TokenType.Field)
			{
				if (tokenType == TokenType.TypeRef)
				{
					metadataTokenProvider = this.GetTypeReference(rid);
					goto IL_00D8;
				}
				if (tokenType == TokenType.TypeDef)
				{
					metadataTokenProvider = this.GetTypeDefinition(rid);
					goto IL_00D8;
				}
				if (tokenType == TokenType.Field)
				{
					metadataTokenProvider = this.GetFieldDefinition(rid);
					goto IL_00D8;
				}
			}
			else if (tokenType <= TokenType.MemberRef)
			{
				if (tokenType == TokenType.Method)
				{
					metadataTokenProvider = this.GetMethodDefinition(rid);
					goto IL_00D8;
				}
				if (tokenType == TokenType.MemberRef)
				{
					metadataTokenProvider = this.GetMemberReference(rid);
					goto IL_00D8;
				}
			}
			else
			{
				if (tokenType == TokenType.TypeSpec)
				{
					metadataTokenProvider = this.GetTypeSpecification(rid);
					goto IL_00D8;
				}
				if (tokenType == TokenType.MethodSpec)
				{
					metadataTokenProvider = this.GetMethodSpecification(rid);
					goto IL_00D8;
				}
			}
			return null;
			IL_00D8:
			this.position = position;
			this.context = genericContext;
			return metadataTokenProvider;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000D208 File Offset: 0x0000B408
		public FieldDefinition GetFieldDefinition(uint rid)
		{
			this.InitializeTypeDefinitions();
			FieldDefinition fieldDefinition = this.metadata.GetFieldDefinition(rid);
			if (fieldDefinition != null)
			{
				return fieldDefinition;
			}
			return this.LookupField(rid);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000D234 File Offset: 0x0000B434
		private FieldDefinition LookupField(uint rid)
		{
			TypeDefinition fieldDeclaringType = this.metadata.GetFieldDeclaringType(rid);
			if (fieldDeclaringType == null)
			{
				return null;
			}
			Mixin.Read(fieldDeclaringType.Fields);
			return this.metadata.GetFieldDefinition(rid);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000D26C File Offset: 0x0000B46C
		public MethodDefinition GetMethodDefinition(uint rid)
		{
			this.InitializeTypeDefinitions();
			MethodDefinition methodDefinition = this.metadata.GetMethodDefinition(rid);
			if (methodDefinition != null)
			{
				return methodDefinition;
			}
			return this.LookupMethod(rid);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000D298 File Offset: 0x0000B498
		private MethodDefinition LookupMethod(uint rid)
		{
			TypeDefinition methodDeclaringType = this.metadata.GetMethodDeclaringType(rid);
			if (methodDeclaringType == null)
			{
				return null;
			}
			Mixin.Read(methodDeclaringType.Methods);
			return this.metadata.GetMethodDefinition(rid);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000D2D0 File Offset: 0x0000B4D0
		private MethodSpecification GetMethodSpecification(uint rid)
		{
			if (!this.MoveTo(Table.MethodSpec, rid))
			{
				return null;
			}
			MethodReference methodReference = (MethodReference)this.LookupToken(this.ReadMetadataToken(CodedIndex.MethodDefOrRef));
			uint num = this.ReadBlobIndex();
			MethodSpecification methodSpecification = this.ReadMethodSpecSignature(num, methodReference);
			methodSpecification.token = new MetadataToken(TokenType.MethodSpec, rid);
			return methodSpecification;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000D320 File Offset: 0x0000B520
		private MethodSpecification ReadMethodSpecSignature(uint signature, MethodReference method)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			if (signatureReader.ReadByte() != 10)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(method);
			signatureReader.ReadGenericInstanceSignature(method, genericInstanceMethod);
			return genericInstanceMethod;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000D358 File Offset: 0x0000B558
		private MemberReference GetMemberReference(uint rid)
		{
			this.InitializeMemberReferences();
			MemberReference memberReference = this.metadata.GetMemberReference(rid);
			if (memberReference != null)
			{
				return memberReference;
			}
			memberReference = this.ReadMemberReference(rid);
			if (memberReference != null && !memberReference.ContainsGenericParameter)
			{
				this.metadata.AddMemberReference(memberReference);
			}
			return memberReference;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		private MemberReference ReadMemberReference(uint rid)
		{
			if (!this.MoveTo(Table.MemberRef, rid))
			{
				return null;
			}
			MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.MemberRefParent);
			string text = this.ReadString();
			uint num = this.ReadBlobIndex();
			TokenType tokenType = metadataToken.TokenType;
			MemberReference memberReference;
			if (tokenType <= TokenType.TypeDef)
			{
				if (tokenType != TokenType.TypeRef && tokenType != TokenType.TypeDef)
				{
					goto IL_0073;
				}
			}
			else
			{
				if (tokenType == TokenType.Method)
				{
					memberReference = this.ReadMethodMemberReference(metadataToken, text, num);
					goto IL_0079;
				}
				if (tokenType != TokenType.TypeSpec)
				{
					goto IL_0073;
				}
			}
			memberReference = this.ReadTypeMemberReference(metadataToken, text, num);
			goto IL_0079;
			IL_0073:
			throw new NotSupportedException();
			IL_0079:
			memberReference.token = new MetadataToken(TokenType.MemberRef, rid);
			return memberReference;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000D438 File Offset: 0x0000B638
		private MemberReference ReadTypeMemberReference(MetadataToken type, string name, uint signature)
		{
			TypeReference typeDefOrRef = this.GetTypeDefOrRef(type);
			if (!typeDefOrRef.IsArray)
			{
				this.context = typeDefOrRef;
			}
			MemberReference memberReference = this.ReadMemberReferenceSignature(signature, typeDefOrRef);
			memberReference.Name = name;
			return memberReference;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000D470 File Offset: 0x0000B670
		private MemberReference ReadMemberReferenceSignature(uint signature, TypeReference declaring_type)
		{
			SignatureReader signatureReader = this.ReadSignature(signature);
			if (signatureReader.buffer[signatureReader.position] == 6)
			{
				signatureReader.position++;
				return new FieldReference
				{
					DeclaringType = declaring_type,
					FieldType = signatureReader.ReadTypeSignature()
				};
			}
			MethodReference methodReference = new MethodReference();
			methodReference.DeclaringType = declaring_type;
			signatureReader.ReadMethodSignature(methodReference);
			return methodReference;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000D4D4 File Offset: 0x0000B6D4
		private MemberReference ReadMethodMemberReference(MetadataToken token, string name, uint signature)
		{
			MemberReference memberReference = this.ReadMemberReferenceSignature(signature, ((MethodDefinition)(this.context = this.GetMethodDefinition(token.RID))).DeclaringType);
			memberReference.Name = name;
			return memberReference;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000D511 File Offset: 0x0000B711
		private void InitializeMemberReferences()
		{
			if (this.metadata.MemberReferences == null)
			{
				this.metadata.MemberReferences = new MemberReference[this.image.GetTableLength(Table.MemberRef)];
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000D540 File Offset: 0x0000B740
		public IEnumerable<MemberReference> GetMemberReferences()
		{
			this.InitializeMemberReferences();
			int tableLength = this.image.GetTableLength(Table.MemberRef);
			TypeSystem typeSystem = this.module.TypeSystem;
			MethodReference methodReference = new MethodReference(string.Empty, typeSystem.Void);
			methodReference.DeclaringType = new TypeReference(string.Empty, string.Empty, this.module, typeSystem.CoreLibrary);
			MemberReference[] array = new MemberReference[tableLength];
			uint num = 1U;
			while ((ulong)num <= (ulong)((long)tableLength))
			{
				this.context = methodReference;
				array[(int)(num - 1U)] = this.GetMemberReference(num);
				num += 1U;
			}
			return array;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		private void InitializeConstants()
		{
			if (this.metadata.Constants == null)
			{
				int num = this.MoveTo(Table.Constant);
				Dictionary<MetadataToken, Row<ElementType, uint>> dictionary = (this.metadata.Constants = new Dictionary<MetadataToken, Row<ElementType, uint>>(num));
				uint num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					ElementType elementType = (ElementType)base.ReadUInt16();
					MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.HasConstant);
					uint num3 = this.ReadBlobIndex();
					dictionary.Add(metadataToken, new Row<ElementType, uint>(elementType, num3));
					num2 += 1U;
				}
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000D641 File Offset: 0x0000B841
		public TypeReference ReadConstantSignature(MetadataToken token)
		{
			if (token.TokenType != TokenType.Signature)
			{
				throw new NotSupportedException();
			}
			if (!this.MoveTo(Table.StandAloneSig, token.RID))
			{
				return null;
			}
			return this.ReadFieldType(this.ReadBlobIndex());
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000D678 File Offset: 0x0000B878
		public object ReadConstant(IConstantProvider owner)
		{
			this.InitializeConstants();
			Row<ElementType, uint> row;
			if (!this.metadata.Constants.TryGetValue(owner.MetadataToken, ref row))
			{
				return Mixin.NoValue;
			}
			this.metadata.Constants.Remove(owner.MetadataToken);
			return this.ReadConstantValue(row.Col1, row.Col2);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		private object ReadConstantValue(ElementType etype, uint signature)
		{
			if (etype == ElementType.String)
			{
				return this.ReadConstantString(signature);
			}
			if (etype == ElementType.Class || etype == ElementType.Object)
			{
				return null;
			}
			return this.ReadConstantPrimitive(etype, signature);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		private string ReadConstantString(uint signature)
		{
			byte[] array;
			int num;
			int num2;
			this.GetBlobView(signature, out array, out num, out num2);
			if (num2 == 0)
			{
				return string.Empty;
			}
			if ((num2 & 1) == 1)
			{
				num2--;
			}
			return Encoding.Unicode.GetString(array, num, num2);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D732 File Offset: 0x0000B932
		private object ReadConstantPrimitive(ElementType type, uint signature)
		{
			return this.ReadSignature(signature).ReadConstantSignature(type);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D741 File Offset: 0x0000B941
		internal void InitializeCustomAttributes()
		{
			if (this.metadata.CustomAttributes == null)
			{
				this.metadata.CustomAttributes = this.InitializeRanges(Table.CustomAttribute, delegate
				{
					MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.HasCustomAttribute);
					this.ReadMetadataToken(CodedIndex.CustomAttributeType);
					this.ReadBlobIndex();
					return metadataToken;
				});
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D770 File Offset: 0x0000B970
		public bool HasCustomAttributes(ICustomAttributeProvider owner)
		{
			this.InitializeCustomAttributes();
			Range[] array;
			return this.metadata.TryGetCustomAttributeRanges(owner, out array) && MetadataReader.RangesSize(array) > 0;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public Collection<CustomAttribute> ReadCustomAttributes(ICustomAttributeProvider owner)
		{
			this.InitializeCustomAttributes();
			Range[] array;
			if (!this.metadata.TryGetCustomAttributeRanges(owner, out array))
			{
				return new Collection<CustomAttribute>();
			}
			Collection<CustomAttribute> collection = new Collection<CustomAttribute>(MetadataReader.RangesSize(array));
			for (int i = 0; i < array.Length; i++)
			{
				this.ReadCustomAttributeRange(array[i], collection);
			}
			this.metadata.RemoveCustomAttributeRange(owner);
			if (this.module.IsWindowsMetadata())
			{
				foreach (CustomAttribute customAttribute in collection)
				{
					WindowsRuntimeProjections.Project(owner, customAttribute);
				}
				return collection;
			}
			return collection;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000D850 File Offset: 0x0000BA50
		private void ReadCustomAttributeRange(Range range, Collection<CustomAttribute> custom_attributes)
		{
			if (this.MoveTo(Table.CustomAttribute, range.Start))
			{
				int num = 0;
				while ((long)num < (long)((ulong)range.Length))
				{
					this.ReadMetadataToken(CodedIndex.HasCustomAttribute);
					MethodReference methodReference = (MethodReference)this.LookupToken(this.ReadMetadataToken(CodedIndex.CustomAttributeType));
					uint num2 = this.ReadBlobIndex();
					custom_attributes.Add(new CustomAttribute(num2, methodReference));
					num++;
				}
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		private static int RangesSize(Range[] ranges)
		{
			uint num = 0U;
			for (int i = 0; i < ranges.Length; i++)
			{
				num += ranges[i].Length;
			}
			return (int)num;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000D8E0 File Offset: 0x0000BAE0
		public IEnumerable<CustomAttribute> GetCustomAttributes()
		{
			this.InitializeTypeDefinitions();
			uint length = this.image.TableHeap[Table.CustomAttribute].Length;
			Collection<CustomAttribute> collection = new Collection<CustomAttribute>((int)length);
			this.ReadCustomAttributeRange(new Range(1U, length), collection);
			return collection;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D921 File Offset: 0x0000BB21
		public byte[] ReadCustomAttributeBlob(uint signature)
		{
			return this.ReadBlob(signature);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000D92C File Offset: 0x0000BB2C
		public void ReadCustomAttributeSignature(CustomAttribute attribute)
		{
			SignatureReader signatureReader = this.ReadSignature(attribute.signature);
			if (!signatureReader.CanReadMore())
			{
				return;
			}
			if (signatureReader.ReadUInt16() != 1)
			{
				throw new InvalidOperationException();
			}
			MethodReference constructor = attribute.Constructor;
			if (constructor.HasParameters)
			{
				signatureReader.ReadCustomAttributeConstructorArguments(attribute, constructor.Parameters);
			}
			if (signatureReader.CanReadMore())
			{
				ushort num = signatureReader.ReadUInt16();
				if (num != 0)
				{
					signatureReader.ReadCustomAttributeNamedArguments(num, ref attribute.fields, ref attribute.properties);
				}
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		private void InitializeMarshalInfos()
		{
			if (this.metadata.FieldMarshals != null)
			{
				return;
			}
			int num = this.MoveTo(Table.FieldMarshal);
			Dictionary<MetadataToken, uint> dictionary = (this.metadata.FieldMarshals = new Dictionary<MetadataToken, uint>(num));
			for (int i = 0; i < num; i++)
			{
				MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.HasFieldMarshal);
				uint num2 = this.ReadBlobIndex();
				if (metadataToken.RID != 0U)
				{
					dictionary.Add(metadataToken, num2);
				}
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000DA09 File Offset: 0x0000BC09
		public bool HasMarshalInfo(IMarshalInfoProvider owner)
		{
			this.InitializeMarshalInfos();
			return this.metadata.FieldMarshals.ContainsKey(owner.MetadataToken);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000DA28 File Offset: 0x0000BC28
		public MarshalInfo ReadMarshalInfo(IMarshalInfoProvider owner)
		{
			this.InitializeMarshalInfos();
			uint num;
			if (!this.metadata.FieldMarshals.TryGetValue(owner.MetadataToken, ref num))
			{
				return null;
			}
			SignatureReader signatureReader = this.ReadSignature(num);
			this.metadata.FieldMarshals.Remove(owner.MetadataToken);
			return signatureReader.ReadMarshalInfo();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		private void InitializeSecurityDeclarations()
		{
			if (this.metadata.SecurityDeclarations == null)
			{
				this.metadata.SecurityDeclarations = this.InitializeRanges(Table.DeclSecurity, delegate
				{
					base.ReadUInt16();
					MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.HasDeclSecurity);
					this.ReadBlobIndex();
					return metadataToken;
				});
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		public bool HasSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			this.InitializeSecurityDeclarations();
			Range[] array;
			return this.metadata.TryGetSecurityDeclarationRanges(owner, out array) && MetadataReader.RangesSize(array) > 0;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000DADC File Offset: 0x0000BCDC
		public Collection<SecurityDeclaration> ReadSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			this.InitializeSecurityDeclarations();
			Range[] array;
			if (!this.metadata.TryGetSecurityDeclarationRanges(owner, out array))
			{
				return new Collection<SecurityDeclaration>();
			}
			Collection<SecurityDeclaration> collection = new Collection<SecurityDeclaration>(MetadataReader.RangesSize(array));
			for (int i = 0; i < array.Length; i++)
			{
				this.ReadSecurityDeclarationRange(array[i], collection);
			}
			this.metadata.RemoveSecurityDeclarationRange(owner);
			return collection;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		private void ReadSecurityDeclarationRange(Range range, Collection<SecurityDeclaration> security_declarations)
		{
			if (this.MoveTo(Table.DeclSecurity, range.Start))
			{
				int num = 0;
				while ((long)num < (long)((ulong)range.Length))
				{
					SecurityAction securityAction = (SecurityAction)base.ReadUInt16();
					this.ReadMetadataToken(CodedIndex.HasDeclSecurity);
					uint num2 = this.ReadBlobIndex();
					security_declarations.Add(new SecurityDeclaration(securityAction, num2, this.module));
					num++;
				}
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000D921 File Offset: 0x0000BB21
		public byte[] ReadSecurityDeclarationBlob(uint signature)
		{
			return this.ReadBlob(signature);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public void ReadSecurityDeclarationSignature(SecurityDeclaration declaration)
		{
			uint signature = declaration.signature;
			SignatureReader signatureReader = this.ReadSignature(signature);
			if (signatureReader.buffer[signatureReader.position] != 46)
			{
				this.ReadXmlSecurityDeclaration(signature, declaration);
				return;
			}
			signatureReader.position++;
			uint num = signatureReader.ReadCompressedUInt32();
			Collection<SecurityAttribute> collection = new Collection<SecurityAttribute>((int)num);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				collection.Add(signatureReader.ReadSecurityAttribute());
				num2++;
			}
			declaration.security_attributes = collection;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000DC10 File Offset: 0x0000BE10
		private void ReadXmlSecurityDeclaration(uint signature, SecurityDeclaration declaration)
		{
			declaration.security_attributes = new Collection<SecurityAttribute>(1)
			{
				new SecurityAttribute(this.module.TypeSystem.LookupType("System.Security.Permissions", "PermissionSetAttribute"))
				{
					properties = new Collection<CustomAttributeNamedArgument>(1),
					properties = 
					{
						new CustomAttributeNamedArgument("XML", new CustomAttributeArgument(this.module.TypeSystem.String, this.ReadUnicodeStringBlob(signature)))
					}
				}
			};
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000DC90 File Offset: 0x0000BE90
		public Collection<ExportedType> ReadExportedTypes()
		{
			int num = this.MoveTo(Table.ExportedType);
			if (num == 0)
			{
				return new Collection<ExportedType>();
			}
			Collection<ExportedType> collection = new Collection<ExportedType>(num);
			for (int i = 1; i <= num; i++)
			{
				TypeAttributes typeAttributes = (TypeAttributes)base.ReadUInt32();
				uint num2 = base.ReadUInt32();
				string text = this.ReadString();
				string text2 = this.ReadString();
				MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.Implementation);
				ExportedType exportedType = null;
				IMetadataScope metadataScope = null;
				TokenType tokenType = metadataToken.TokenType;
				if (tokenType != TokenType.AssemblyRef && tokenType != TokenType.File)
				{
					if (tokenType == TokenType.ExportedType)
					{
						exportedType = collection[(int)(metadataToken.RID - 1U)];
					}
				}
				else
				{
					metadataScope = this.GetExportedTypeScope(metadataToken);
				}
				ExportedType exportedType2 = new ExportedType(text2, text, this.module, metadataScope)
				{
					Attributes = typeAttributes,
					Identifier = (int)num2,
					DeclaringType = exportedType
				};
				exportedType2.token = new MetadataToken(TokenType.ExportedType, i);
				collection.Add(exportedType2);
			}
			return collection;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000DD80 File Offset: 0x0000BF80
		private IMetadataScope GetExportedTypeScope(MetadataToken token)
		{
			int position = this.position;
			TokenType tokenType = token.TokenType;
			IMetadataScope metadataScope;
			if (tokenType != TokenType.AssemblyRef)
			{
				if (tokenType != TokenType.File)
				{
					throw new NotSupportedException();
				}
				this.InitializeModuleReferences();
				metadataScope = this.GetModuleReferenceFromFile(token);
			}
			else
			{
				this.InitializeAssemblyReferences();
				metadataScope = this.metadata.GetAssemblyNameReference(token.RID);
			}
			this.position = position;
			return metadataScope;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		private ModuleReference GetModuleReferenceFromFile(MetadataToken token)
		{
			if (!this.MoveTo(Table.File, token.RID))
			{
				return null;
			}
			base.ReadUInt32();
			string text = this.ReadString();
			Collection<ModuleReference> moduleReferences = this.module.ModuleReferences;
			ModuleReference moduleReference;
			for (int i = 0; i < moduleReferences.Count; i++)
			{
				moduleReference = moduleReferences[i];
				if (moduleReference.Name == text)
				{
					return moduleReference;
				}
			}
			moduleReference = new ModuleReference(text);
			moduleReferences.Add(moduleReference);
			return moduleReference;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000DE5C File Offset: 0x0000C05C
		private void InitializeDocuments()
		{
			if (this.metadata.Documents == null)
			{
				int num = this.MoveTo(Table.Document);
				Document[] array = (this.metadata.Documents = new Document[num]);
				uint num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					uint num3 = this.ReadBlobIndex();
					Guid guid = this.ReadGuid();
					byte[] array2 = this.ReadBlob();
					Guid guid2 = this.ReadGuid();
					string text = this.ReadSignature(num3).ReadDocumentName();
					array[(int)(num2 - 1U)] = new Document(text)
					{
						HashAlgorithm = guid.ToHashAlgorithm(),
						Hash = array2,
						Language = guid2.ToLanguage(),
						token = new MetadataToken(TokenType.Document, num2)
					};
					num2 += 1U;
				}
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000DF14 File Offset: 0x0000C114
		public Collection<SequencePoint> ReadSequencePoints(MethodDefinition method)
		{
			this.InitializeDocuments();
			if (!this.MoveTo(Table.MethodDebugInformation, method.MetadataToken.RID))
			{
				return new Collection<SequencePoint>(0);
			}
			uint num = this.ReadTableIndex(Table.Document);
			uint num2 = this.ReadBlobIndex();
			if (num2 == 0U)
			{
				return new Collection<SequencePoint>(0);
			}
			Document document = this.GetDocument(num);
			return this.ReadSignature(num2).ReadSequencePoints(document);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000DF78 File Offset: 0x0000C178
		public Document GetDocument(uint rid)
		{
			Document document = this.metadata.GetDocument(rid);
			if (document == null)
			{
				return null;
			}
			document.custom_infos = this.GetCustomDebugInformation(document);
			return document;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
		private void InitializeLocalScopes()
		{
			if (this.metadata.LocalScopes == null)
			{
				this.InitializeMethods();
				int num = this.MoveTo(Table.LocalScope);
				this.metadata.LocalScopes = new Dictionary<uint, Collection<Row<uint, Range, Range, uint, uint, uint>>>();
				uint num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					uint num3 = this.ReadTableIndex(Table.Method);
					uint num4 = this.ReadTableIndex(Table.ImportScope);
					Range range = this.ReadListRange(num2, Table.LocalScope, Table.LocalVariable);
					Range range2 = this.ReadListRange(num2, Table.LocalScope, Table.LocalConstant);
					uint num5 = base.ReadUInt32();
					uint num6 = base.ReadUInt32();
					this.metadata.SetLocalScopes(num3, MetadataReader.AddMapping<uint, Row<uint, Range, Range, uint, uint, uint>>(this.metadata.LocalScopes, num3, new Row<uint, Range, Range, uint, uint, uint>(num4, range, range2, num5, num6, num2)));
					num2 += 1U;
				}
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000E05C File Offset: 0x0000C25C
		public ScopeDebugInformation ReadScope(MethodDefinition method)
		{
			this.InitializeLocalScopes();
			this.InitializeImportScopes();
			Collection<Row<uint, Range, Range, uint, uint, uint>> collection;
			if (!this.metadata.TryGetLocalScopes(method, out collection))
			{
				return null;
			}
			ScopeDebugInformation scopeDebugInformation = null;
			for (int i = 0; i < collection.Count; i++)
			{
				ScopeDebugInformation scopeDebugInformation2 = this.ReadLocalScope(collection[i]);
				if (i == 0)
				{
					scopeDebugInformation = scopeDebugInformation2;
				}
				else if (!MetadataReader.AddScope(scopeDebugInformation.scopes, scopeDebugInformation2))
				{
					scopeDebugInformation.Scopes.Add(scopeDebugInformation2);
				}
			}
			return scopeDebugInformation;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		private static bool AddScope(Collection<ScopeDebugInformation> scopes, ScopeDebugInformation scope)
		{
			if (scopes.IsNullOrEmpty<ScopeDebugInformation>())
			{
				return false;
			}
			foreach (ScopeDebugInformation scopeDebugInformation in scopes)
			{
				if (scopeDebugInformation.HasScopes && MetadataReader.AddScope(scopeDebugInformation.Scopes, scope))
				{
					return true;
				}
				if (scope.Start.Offset >= scopeDebugInformation.Start.Offset && scope.End.Offset <= scopeDebugInformation.End.Offset)
				{
					scopeDebugInformation.Scopes.Add(scope);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000E188 File Offset: 0x0000C388
		private ScopeDebugInformation ReadLocalScope(Row<uint, Range, Range, uint, uint, uint> record)
		{
			ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation
			{
				start = new InstructionOffset((int)record.Col4),
				end = new InstructionOffset((int)(record.Col4 + record.Col5)),
				token = new MetadataToken(TokenType.LocalScope, record.Col6)
			};
			if (record.Col1 != 0U)
			{
				scopeDebugInformation.import = this.metadata.GetImportScope(record.Col1);
			}
			if (record.Col2.Length != 0U)
			{
				scopeDebugInformation.variables = new Collection<VariableDebugInformation>((int)record.Col2.Length);
				for (uint num = 0U; num < record.Col2.Length; num += 1U)
				{
					scopeDebugInformation.variables.Add(this.ReadLocalVariable(record.Col2.Start + num));
				}
			}
			if (record.Col3.Length != 0U)
			{
				scopeDebugInformation.constants = new Collection<ConstantDebugInformation>((int)record.Col3.Length);
				for (uint num2 = 0U; num2 < record.Col3.Length; num2 += 1U)
				{
					scopeDebugInformation.constants.Add(this.ReadLocalConstant(record.Col3.Start + num2));
				}
			}
			return scopeDebugInformation;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000E2A8 File Offset: 0x0000C4A8
		private VariableDebugInformation ReadLocalVariable(uint rid)
		{
			if (!this.MoveTo(Table.LocalVariable, rid))
			{
				return null;
			}
			VariableAttributes variableAttributes = (VariableAttributes)base.ReadUInt16();
			ushort num = base.ReadUInt16();
			string text = this.ReadString();
			VariableDebugInformation variableDebugInformation = new VariableDebugInformation((int)num, text)
			{
				Attributes = variableAttributes,
				token = new MetadataToken(TokenType.LocalVariable, rid)
			};
			variableDebugInformation.custom_infos = this.GetCustomDebugInformation(variableDebugInformation);
			return variableDebugInformation;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000E308 File Offset: 0x0000C508
		private ConstantDebugInformation ReadLocalConstant(uint rid)
		{
			if (!this.MoveTo(Table.LocalConstant, rid))
			{
				return null;
			}
			string text = this.ReadString();
			SignatureReader signatureReader = this.ReadSignature(this.ReadBlobIndex());
			TypeReference typeReference = signatureReader.ReadTypeSignature();
			object obj;
			if (typeReference.etype == ElementType.String)
			{
				if (signatureReader.buffer[signatureReader.position] != 255)
				{
					byte[] array = signatureReader.ReadBytes((int)((ulong)signatureReader.sig_length - (ulong)((long)signatureReader.position - (long)((ulong)signatureReader.start))));
					obj = Encoding.Unicode.GetString(array, 0, array.Length);
				}
				else
				{
					obj = null;
				}
			}
			else if (!typeReference.IsTypeOf("System", "Decimal"))
			{
				obj = (typeReference.IsTypeOf("System", "DateTime") ? new DateTime(signatureReader.ReadInt64()) : ((typeReference.etype != ElementType.Object && typeReference.etype != ElementType.None && typeReference.etype != ElementType.Class) ? signatureReader.ReadConstantSignature(typeReference.etype) : null));
			}
			else
			{
				byte b = signatureReader.ReadByte();
				obj = new decimal(signatureReader.ReadInt32(), signatureReader.ReadInt32(), signatureReader.ReadInt32(), (b & 128) > 0, b & 127);
			}
			ConstantDebugInformation constantDebugInformation = new ConstantDebugInformation(text, typeReference, obj)
			{
				token = new MetadataToken(TokenType.LocalConstant, rid)
			};
			constantDebugInformation.custom_infos = this.GetCustomDebugInformation(constantDebugInformation);
			return constantDebugInformation;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000E460 File Offset: 0x0000C660
		private void InitializeImportScopes()
		{
			if (this.metadata.ImportScopes != null)
			{
				return;
			}
			int num = this.MoveTo(Table.ImportScope);
			this.metadata.ImportScopes = new ImportDebugInformation[num];
			for (int i = 1; i <= num; i++)
			{
				this.ReadTableIndex(Table.ImportScope);
				ImportDebugInformation importDebugInformation = new ImportDebugInformation();
				importDebugInformation.token = new MetadataToken(TokenType.ImportScope, i);
				SignatureReader signatureReader = this.ReadSignature(this.ReadBlobIndex());
				while (signatureReader.CanReadMore())
				{
					importDebugInformation.Targets.Add(this.ReadImportTarget(signatureReader));
				}
				this.metadata.ImportScopes[i - 1] = importDebugInformation;
			}
			this.MoveTo(Table.ImportScope);
			for (int j = 0; j < num; j++)
			{
				uint num2 = this.ReadTableIndex(Table.ImportScope);
				this.ReadBlobIndex();
				if (num2 != 0U)
				{
					this.metadata.ImportScopes[j].Parent = this.metadata.GetImportScope(num2);
				}
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000E548 File Offset: 0x0000C748
		public string ReadUTF8StringBlob(uint signature)
		{
			return this.ReadStringBlob(signature, Encoding.UTF8);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000E556 File Offset: 0x0000C756
		private string ReadUnicodeStringBlob(uint signature)
		{
			return this.ReadStringBlob(signature, Encoding.Unicode);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000E564 File Offset: 0x0000C764
		private string ReadStringBlob(uint signature, Encoding encoding)
		{
			byte[] array;
			int num;
			int num2;
			this.GetBlobView(signature, out array, out num, out num2);
			if (num2 == 0)
			{
				return string.Empty;
			}
			return encoding.GetString(array, num, num2);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000E590 File Offset: 0x0000C790
		private ImportTarget ReadImportTarget(SignatureReader signature)
		{
			AssemblyNameReference assemblyNameReference = null;
			string text = null;
			string text2 = null;
			TypeReference typeReference = null;
			ImportTargetKind importTargetKind = (ImportTargetKind)signature.ReadCompressedUInt32();
			switch (importTargetKind)
			{
			case ImportTargetKind.ImportNamespace:
				text = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.ImportNamespaceInAssembly:
				assemblyNameReference = this.metadata.GetAssemblyNameReference(signature.ReadCompressedUInt32());
				text = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.ImportType:
				typeReference = signature.ReadTypeToken();
				break;
			case ImportTargetKind.ImportXmlNamespaceWithAlias:
				text2 = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				text = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.ImportAlias:
				text2 = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineAssemblyAlias:
				text2 = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				assemblyNameReference = this.metadata.GetAssemblyNameReference(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineNamespaceAlias:
				text2 = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				text = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineNamespaceInAssemblyAlias:
				text2 = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				assemblyNameReference = this.metadata.GetAssemblyNameReference(signature.ReadCompressedUInt32());
				text = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineTypeAlias:
				text2 = this.ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				typeReference = signature.ReadTypeToken();
				break;
			}
			return new ImportTarget(importTargetKind)
			{
				alias = text2,
				type = typeReference,
				@namespace = text,
				reference = assemblyNameReference
			};
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		private void InitializeStateMachineMethods()
		{
			if (this.metadata.StateMachineMethods == null)
			{
				int num = this.MoveTo(Table.StateMachineMethod);
				this.metadata.StateMachineMethods = new Dictionary<uint, uint>(num);
				for (int i = 0; i < num; i++)
				{
					this.metadata.StateMachineMethods.Add(this.ReadTableIndex(Table.Method), this.ReadTableIndex(Table.Method));
				}
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0000E754 File Offset: 0x0000C954
		public MethodDefinition ReadStateMachineKickoffMethod(MethodDefinition method)
		{
			this.InitializeStateMachineMethods();
			uint num;
			if (!this.metadata.TryGetStateMachineKickOffMethod(method, out num))
			{
				return null;
			}
			return this.GetMethodDefinition(num);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0000E780 File Offset: 0x0000C980
		private void InitializeCustomDebugInformations()
		{
			if (this.metadata.CustomDebugInformations == null)
			{
				int num = this.MoveTo(Table.CustomDebugInformation);
				this.metadata.CustomDebugInformations = new Dictionary<MetadataToken, Row<Guid, uint, uint>[]>();
				uint num2 = 1U;
				while ((ulong)num2 <= (ulong)((long)num))
				{
					MetadataToken metadataToken = this.ReadMetadataToken(CodedIndex.HasCustomDebugInformation);
					Row<Guid, uint, uint> row = new Row<Guid, uint, uint>(this.ReadGuid(), this.ReadBlobIndex(), num2);
					Row<Guid, uint, uint>[] array;
					this.metadata.CustomDebugInformations.TryGetValue(metadataToken, ref array);
					this.metadata.CustomDebugInformations[metadataToken] = array.Add(row);
					num2 += 1U;
				}
			}
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000E80C File Offset: 0x0000CA0C
		public Collection<CustomDebugInformation> GetCustomDebugInformation(ICustomDebugInformationProvider provider)
		{
			this.InitializeCustomDebugInformations();
			Row<Guid, uint, uint>[] array;
			if (!this.metadata.CustomDebugInformations.TryGetValue(provider.MetadataToken, ref array))
			{
				return null;
			}
			Collection<CustomDebugInformation> collection = new Collection<CustomDebugInformation>(array.Length);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Col1 == StateMachineScopeDebugInformation.KindIdentifier)
				{
					SignatureReader signatureReader = this.ReadSignature(array[i].Col2);
					collection.Add(new StateMachineScopeDebugInformation(signatureReader.ReadInt32(), signatureReader.ReadInt32()));
				}
				else if (array[i].Col1 == AsyncMethodBodyDebugInformation.KindIdentifier)
				{
					SignatureReader signatureReader2 = this.ReadSignature(array[i].Col2);
					int num = signatureReader2.ReadInt32() - 1;
					Collection<InstructionOffset> collection2 = new Collection<InstructionOffset>();
					Collection<InstructionOffset> collection3 = new Collection<InstructionOffset>();
					uint num2 = 0U;
					while (signatureReader2.CanReadMore())
					{
						collection2.Add(new InstructionOffset(signatureReader2.ReadInt32()));
						collection3.Add(new InstructionOffset(signatureReader2.ReadInt32()));
						num2 = signatureReader2.ReadCompressedUInt32();
					}
					collection.Add(new AsyncMethodBodyDebugInformation(num)
					{
						yields = collection2,
						resumes = collection3,
						move_next = this.GetMethodDefinition(num2)
					});
				}
				else if (array[i].Col1 == EmbeddedSourceDebugInformation.KindIdentifier)
				{
					SignatureReader signatureReader3 = this.ReadSignature(array[i].Col2);
					int num3 = signatureReader3.ReadInt32();
					uint num4 = signatureReader3.sig_length - 4U;
					CustomDebugInformation customDebugInformation = null;
					if (num3 == 0)
					{
						customDebugInformation = new EmbeddedSourceDebugInformation(signatureReader3.ReadBytes((int)num4), false);
					}
					else if (num3 > 0)
					{
						MemoryStream memoryStream = new MemoryStream(signatureReader3.ReadBytes((int)num4));
						byte[] array2 = new byte[num3];
						MemoryStream memoryStream2 = new MemoryStream(array2);
						using (DeflateStream deflateStream = new DeflateStream(memoryStream, 0, true))
						{
							deflateStream.CopyTo(memoryStream2);
						}
						customDebugInformation = new EmbeddedSourceDebugInformation(array2, true);
					}
					else if (num3 < 0)
					{
						customDebugInformation = new BinaryCustomDebugInformation(array[i].Col1, this.ReadBlob(array[i].Col2));
					}
					collection.Add(customDebugInformation);
				}
				else if (array[i].Col1 == SourceLinkDebugInformation.KindIdentifier)
				{
					collection.Add(new SourceLinkDebugInformation(Encoding.UTF8.GetString(this.ReadBlob(array[i].Col2))));
				}
				else
				{
					collection.Add(new BinaryCustomDebugInformation(array[i].Col1, this.ReadBlob(array[i].Col2)));
				}
				collection[i].token = new MetadataToken(TokenType.CustomDebugInformation, array[i].Col3);
			}
			return collection;
		}

		// Token: 0x040000F2 RID: 242
		internal readonly Image image;

		// Token: 0x040000F3 RID: 243
		internal readonly ModuleDefinition module;

		// Token: 0x040000F4 RID: 244
		internal readonly MetadataSystem metadata;

		// Token: 0x040000F5 RID: 245
		internal CodeReader code;

		// Token: 0x040000F6 RID: 246
		internal IGenericContext context;

		// Token: 0x040000F7 RID: 247
		private readonly MetadataReader metadata_reader;
	}
}
