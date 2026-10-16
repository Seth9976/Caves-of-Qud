using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000127 RID: 295
	[NullableContext(1)]
	[Nullable(0)]
	internal class NamespaceCache
	{
		// Token: 0x06000A49 RID: 2633 RVA: 0x0001DAF5 File Offset: 0x0001BCF5
		internal NamespaceCache(MetadataReader reader)
		{
			this._metadataReader = reader;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0001DB0F File Offset: 0x0001BD0F
		internal bool CacheIsRealized
		{
			get
			{
				return this._namespaceTable != null;
			}
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		internal string GetFullName(NamespaceDefinitionHandle handle)
		{
			NamespaceData namespaceData = this.GetNamespaceData(handle);
			return namespaceData.FullName;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0001DB37 File Offset: 0x0001BD37
		internal NamespaceData GetRootNamespace()
		{
			this.EnsureNamespaceTableIsPopulated();
			return this._rootNamespace;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0001DB48 File Offset: 0x0001BD48
		internal NamespaceData GetNamespaceData(NamespaceDefinitionHandle handle)
		{
			this.EnsureNamespaceTableIsPopulated();
			NamespaceData namespaceData;
			if (!this._namespaceTable.TryGetValue(handle, ref namespaceData))
			{
				Throw.InvalidHandle();
			}
			return namespaceData;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0001DB74 File Offset: 0x0001BD74
		private StringHandle GetSimpleName(NamespaceDefinitionHandle fullNamespaceHandle, int segmentIndex = 2147483647)
		{
			StringHandle fullName = fullNamespaceHandle.GetFullName();
			int num = fullNamespaceHandle.GetHeapOffset() - 1;
			for (int i = 0; i < segmentIndex; i++)
			{
				int num2 = this._metadataReader.StringHeap.IndexOfRaw(num + 1, '.');
				if (num2 == -1)
				{
					break;
				}
				num = num2;
			}
			int num3 = num + 1;
			return StringHandle.FromOffset(num3).WithDotTermination();
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
		private void PopulateNamespaceTable()
		{
			object namespaceTableAndListLock = this._namespaceTableAndListLock;
			lock (namespaceTableAndListLock)
			{
				if (this._namespaceTable == null)
				{
					Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> dictionary = new Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>();
					NamespaceDefinitionHandle namespaceDefinitionHandle = NamespaceDefinitionHandle.FromFullNameOffset(0);
					dictionary.Add(namespaceDefinitionHandle, new NamespaceCache.NamespaceDataBuilder(namespaceDefinitionHandle, namespaceDefinitionHandle.GetFullName(), string.Empty));
					this.PopulateTableWithTypeDefinitions(dictionary);
					this.PopulateTableWithExportedTypes(dictionary);
					Dictionary<string, NamespaceCache.NamespaceDataBuilder> dictionary2;
					this.MergeDuplicateNamespaces(dictionary, out dictionary2);
					List<NamespaceCache.NamespaceDataBuilder> list;
					this.ResolveParentChildRelationships(dictionary2, out list);
					Dictionary<NamespaceDefinitionHandle, NamespaceData> dictionary3 = new Dictionary<NamespaceDefinitionHandle, NamespaceData>();
					foreach (KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> keyValuePair in dictionary)
					{
						dictionary3.Add(keyValuePair.Key, keyValuePair.Value.Freeze());
					}
					if (list != null)
					{
						foreach (NamespaceCache.NamespaceDataBuilder namespaceDataBuilder in list)
						{
							dictionary3.Add(namespaceDataBuilder.Handle, namespaceDataBuilder.Freeze());
						}
					}
					this._rootNamespace = dictionary3[namespaceDefinitionHandle];
					this._namespaceTable = dictionary3;
				}
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		private void MergeDuplicateNamespaces(Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> table, out Dictionary<string, NamespaceCache.NamespaceDataBuilder> stringTable)
		{
			Dictionary<string, NamespaceCache.NamespaceDataBuilder> dictionary = new Dictionary<string, NamespaceCache.NamespaceDataBuilder>();
			List<KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>> list = null;
			foreach (KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> keyValuePair in table)
			{
				NamespaceCache.NamespaceDataBuilder value = keyValuePair.Value;
				NamespaceCache.NamespaceDataBuilder namespaceDataBuilder;
				if (dictionary.TryGetValue(value.FullName, ref namespaceDataBuilder))
				{
					value.MergeInto(namespaceDataBuilder);
					if (list == null)
					{
						list = new List<KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>>();
					}
					list.Add(new KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>(keyValuePair.Key, namespaceDataBuilder));
				}
				else
				{
					dictionary.Add(value.FullName, value);
				}
			}
			if (list != null)
			{
				foreach (KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> keyValuePair2 in list)
				{
					table[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			stringTable = dictionary;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0001DE40 File Offset: 0x0001C040
		private NamespaceCache.NamespaceDataBuilder SynthesizeNamespaceData(string fullName, NamespaceDefinitionHandle realChild)
		{
			int num = 0;
			for (int i = 0; i < fullName.Length; i++)
			{
				char c = fullName.get_Chars(i);
				if (c == '.')
				{
					num++;
				}
			}
			StringHandle simpleName = this.GetSimpleName(realChild, num);
			uint num2 = this._virtualNamespaceCounter + 1U;
			this._virtualNamespaceCounter = num2;
			NamespaceDefinitionHandle namespaceDefinitionHandle = NamespaceDefinitionHandle.FromVirtualIndex(num2);
			return new NamespaceCache.NamespaceDataBuilder(namespaceDefinitionHandle, simpleName, fullName);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001DEA5 File Offset: 0x0001C0A5
		private void LinkChildDataToParentData(NamespaceCache.NamespaceDataBuilder child, NamespaceCache.NamespaceDataBuilder parent)
		{
			child.Parent = parent.Handle;
			parent.Namespaces.Add(child.Handle);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		private void LinkChildToParentNamespace(Dictionary<string, NamespaceCache.NamespaceDataBuilder> existingNamespaces, NamespaceCache.NamespaceDataBuilder realChild, ref List<NamespaceCache.NamespaceDataBuilder> virtualNamespaces)
		{
			string text = realChild.FullName;
			NamespaceCache.NamespaceDataBuilder namespaceDataBuilder = realChild;
			NamespaceCache.NamespaceDataBuilder namespaceDataBuilder2;
			for (;;)
			{
				int num = text.LastIndexOf('.');
				string text2;
				if (num == -1)
				{
					if (text.Length == 0)
					{
						break;
					}
					text2 = string.Empty;
				}
				else
				{
					text2 = text.Substring(0, num);
				}
				if (existingNamespaces.TryGetValue(text2, ref namespaceDataBuilder2))
				{
					goto Block_3;
				}
				if (virtualNamespaces != null)
				{
					using (List<NamespaceCache.NamespaceDataBuilder>.Enumerator enumerator = virtualNamespaces.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NamespaceCache.NamespaceDataBuilder namespaceDataBuilder3 = enumerator.Current;
							if (namespaceDataBuilder3.FullName == text2)
							{
								this.LinkChildDataToParentData(namespaceDataBuilder, namespaceDataBuilder3);
								return;
							}
						}
						goto IL_0097;
					}
					goto IL_0090;
				}
				goto IL_0090;
				IL_0097:
				NamespaceCache.NamespaceDataBuilder namespaceDataBuilder4 = this.SynthesizeNamespaceData(text2, realChild.Handle);
				this.LinkChildDataToParentData(namespaceDataBuilder, namespaceDataBuilder4);
				virtualNamespaces.Add(namespaceDataBuilder4);
				text = namespaceDataBuilder4.FullName;
				namespaceDataBuilder = namespaceDataBuilder4;
				continue;
				IL_0090:
				virtualNamespaces = new List<NamespaceCache.NamespaceDataBuilder>();
				goto IL_0097;
			}
			return;
			Block_3:
			this.LinkChildDataToParentData(namespaceDataBuilder, namespaceDataBuilder2);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		private void ResolveParentChildRelationships(Dictionary<string, NamespaceCache.NamespaceDataBuilder> namespaces, out List<NamespaceCache.NamespaceDataBuilder> virtualNamespaces)
		{
			virtualNamespaces = null;
			foreach (KeyValuePair<string, NamespaceCache.NamespaceDataBuilder> keyValuePair in namespaces)
			{
				this.LinkChildToParentNamespace(namespaces, keyValuePair.Value, ref virtualNamespaces);
			}
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0001E008 File Offset: 0x0001C208
		private void PopulateTableWithTypeDefinitions(Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> table)
		{
			foreach (TypeDefinitionHandle typeDefinitionHandle in this._metadataReader.TypeDefinitions)
			{
				if (!this._metadataReader.GetTypeDefinition(typeDefinitionHandle).Attributes.IsNested())
				{
					NamespaceDefinitionHandle namespaceDefinition = this._metadataReader.TypeDefTable.GetNamespaceDefinition(typeDefinitionHandle);
					NamespaceCache.NamespaceDataBuilder namespaceDataBuilder;
					if (table.TryGetValue(namespaceDefinition, ref namespaceDataBuilder))
					{
						namespaceDataBuilder.TypeDefinitions.Add(typeDefinitionHandle);
					}
					else
					{
						StringHandle simpleName = this.GetSimpleName(namespaceDefinition, int.MaxValue);
						string @string = this._metadataReader.GetString(namespaceDefinition);
						table.Add(namespaceDefinition, new NamespaceCache.NamespaceDataBuilder(namespaceDefinition, simpleName, @string)
						{
							TypeDefinitions = { typeDefinitionHandle }
						});
					}
				}
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		private void PopulateTableWithExportedTypes(Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> table)
		{
			foreach (ExportedTypeHandle exportedTypeHandle in this._metadataReader.ExportedTypes)
			{
				ExportedType exportedType = this._metadataReader.GetExportedType(exportedTypeHandle);
				if (exportedType.Implementation.Kind != HandleKind.ExportedType)
				{
					NamespaceDefinitionHandle namespaceDefinition = exportedType.NamespaceDefinition;
					NamespaceCache.NamespaceDataBuilder namespaceDataBuilder;
					if (table.TryGetValue(namespaceDefinition, ref namespaceDataBuilder))
					{
						namespaceDataBuilder.ExportedTypes.Add(exportedTypeHandle);
					}
					else
					{
						StringHandle simpleName = this.GetSimpleName(namespaceDefinition, int.MaxValue);
						string @string = this._metadataReader.GetString(namespaceDefinition);
						table.Add(namespaceDefinition, new NamespaceCache.NamespaceDataBuilder(namespaceDefinition, simpleName, @string)
						{
							ExportedTypes = { exportedTypeHandle }
						});
					}
				}
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0001E1D4 File Offset: 0x0001C3D4
		private void EnsureNamespaceTableIsPopulated()
		{
			if (this._namespaceTable == null)
			{
				this.PopulateNamespaceTable();
			}
		}

		// Token: 0x04000733 RID: 1843
		private readonly MetadataReader _metadataReader;

		// Token: 0x04000734 RID: 1844
		private readonly object _namespaceTableAndListLock = new object();

		// Token: 0x04000735 RID: 1845
		private volatile Dictionary<NamespaceDefinitionHandle, NamespaceData> _namespaceTable;

		// Token: 0x04000736 RID: 1846
		private NamespaceData _rootNamespace;

		// Token: 0x04000737 RID: 1847
		private uint _virtualNamespaceCounter;

		// Token: 0x020001FF RID: 511
		private class NamespaceDataBuilder
		{
			// Token: 0x06000D3A RID: 3386 RVA: 0x00026ADB File Offset: 0x00024CDB
			public NamespaceDataBuilder(NamespaceDefinitionHandle handle, StringHandle name, string fullName)
			{
				this.Handle = handle;
				this.Name = name;
				this.FullName = fullName;
				this.Namespaces = ImmutableArray.CreateBuilder<NamespaceDefinitionHandle>();
				this.TypeDefinitions = ImmutableArray.CreateBuilder<TypeDefinitionHandle>();
				this.ExportedTypes = ImmutableArray.CreateBuilder<ExportedTypeHandle>();
			}

			// Token: 0x06000D3B RID: 3387 RVA: 0x00026B1C File Offset: 0x00024D1C
			public NamespaceData Freeze()
			{
				if (this._frozen == null)
				{
					ImmutableArray<NamespaceDefinitionHandle> immutableArray = this.Namespaces.ToImmutable();
					this.Namespaces = null;
					ImmutableArray<TypeDefinitionHandle> immutableArray2 = this.TypeDefinitions.ToImmutable();
					this.TypeDefinitions = null;
					ImmutableArray<ExportedTypeHandle> immutableArray3 = this.ExportedTypes.ToImmutable();
					this.ExportedTypes = null;
					this._frozen = new NamespaceData(this.Name, this.FullName, this.Parent, immutableArray, immutableArray2, immutableArray3);
				}
				return this._frozen;
			}

			// Token: 0x06000D3C RID: 3388 RVA: 0x00026B90 File Offset: 0x00024D90
			public void MergeInto(NamespaceCache.NamespaceDataBuilder other)
			{
				this.Parent = default(NamespaceDefinitionHandle);
				other.Namespaces.AddRange(this.Namespaces);
				other.TypeDefinitions.AddRange(this.TypeDefinitions);
				other.ExportedTypes.AddRange(this.ExportedTypes);
			}

			// Token: 0x04000BC2 RID: 3010
			public readonly NamespaceDefinitionHandle Handle;

			// Token: 0x04000BC3 RID: 3011
			public readonly StringHandle Name;

			// Token: 0x04000BC4 RID: 3012
			public readonly string FullName;

			// Token: 0x04000BC5 RID: 3013
			public NamespaceDefinitionHandle Parent;

			// Token: 0x04000BC6 RID: 3014
			public ImmutableArray<NamespaceDefinitionHandle>.Builder Namespaces;

			// Token: 0x04000BC7 RID: 3015
			public ImmutableArray<TypeDefinitionHandle>.Builder TypeDefinitions;

			// Token: 0x04000BC8 RID: 3016
			public ImmutableArray<ExportedTypeHandle>.Builder ExportedTypes;

			// Token: 0x04000BC9 RID: 3017
			private NamespaceData _frozen;
		}
	}
}
