using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000593 RID: 1427
	[HelpURL("UIE-VisualTree-landing")]
	[Serializable]
	public class VisualTreeAsset : ScriptableObject
	{
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x0009A154 File Offset: 0x00098354
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x0009A16C File Offset: 0x0009836C
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x0009A178 File Offset: 0x00098378
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x0009A190 File Offset: 0x00098390
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool importerWithUpdatedUrls
		{
			get
			{
				return this.m_HasUpdatedUrls;
			}
			set
			{
				this.m_HasUpdatedUrls = value;
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x0009A19C File Offset: 0x0009839C
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x0009A1B4 File Offset: 0x000983B4
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0009A1C0 File Offset: 0x000983C0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int GetNextChildSerialNumber()
		{
			List<VisualElementAsset> visualElementAssets = this.m_VisualElementAssets;
			int num = ((visualElementAssets != null) ? visualElementAssets.Count : 0);
			int num2 = num;
			List<TemplateAsset> templateAssets = this.m_TemplateAssets;
			num = num2 + ((templateAssets != null) ? templateAssets.Count : 0);
			bool flag = this.m_UxmlObjectEntries != null;
			if (flag)
			{
				num += this.m_UxmlObjectEntries.Count;
				foreach (VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry in this.m_UxmlObjectEntries)
				{
					bool flag2 = uxmlObjectEntry.uxmlObjectAssets != null;
					if (flag2)
					{
						num += uxmlObjectEntry.uxmlObjectAssets.Count;
					}
				}
			}
			return num;
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x0009A27C File Offset: 0x0009847C
		internal List<VisualTreeAsset.UsingEntry> usings
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_Usings;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x0009A284 File Offset: 0x00098484
		public IEnumerable<VisualTreeAsset> templateDependencies
		{
			get
			{
				bool flag = this.m_Usings.Count == 0;
				if (flag)
				{
					yield break;
				}
				HashSet<VisualTreeAsset> sent = new HashSet<VisualTreeAsset>();
				foreach (VisualTreeAsset.UsingEntry entry in this.m_Usings)
				{
					bool flag2 = entry.asset != null && !sent.Contains(entry.asset);
					if (flag2)
					{
						sent.Add(entry.asset);
						yield return entry.asset;
					}
					else
					{
						bool flag3 = !string.IsNullOrEmpty(entry.path);
						if (flag3)
						{
							VisualTreeAsset vta = Panel.LoadResource(entry.path, typeof(VisualTreeAsset), 1f) as VisualTreeAsset;
							bool flag4 = vta != null && !sent.Contains(entry.asset);
							if (flag4)
							{
								sent.Add(entry.asset);
								yield return vta;
							}
							vta = null;
						}
					}
					entry = default(VisualTreeAsset.UsingEntry);
				}
				List<VisualTreeAsset.UsingEntry>.Enumerator enumerator = default(List<VisualTreeAsset.UsingEntry>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x0009A2A4 File Offset: 0x000984A4
		public IEnumerable<StyleSheet> stylesheets
		{
			get
			{
				HashSet<StyleSheet> sent = new HashSet<StyleSheet>();
				foreach (VisualElementAsset vea in this.m_VisualElementAssets)
				{
					bool hasStylesheets = vea.hasStylesheets;
					if (hasStylesheets)
					{
						foreach (StyleSheet stylesheet in vea.stylesheets)
						{
							bool flag = !sent.Contains(stylesheet);
							if (flag)
							{
								sent.Add(stylesheet);
								yield return stylesheet;
							}
							stylesheet = null;
						}
						List<StyleSheet>.Enumerator enumerator2 = default(List<StyleSheet>.Enumerator);
					}
					bool hasStylesheetPaths = vea.hasStylesheetPaths;
					if (hasStylesheetPaths)
					{
						foreach (string stylesheetPath in vea.stylesheetPaths)
						{
							StyleSheet stylesheet2 = Panel.LoadResource(stylesheetPath, typeof(StyleSheet), 1f) as StyleSheet;
							bool flag2 = stylesheet2 != null && !sent.Contains(stylesheet2);
							if (flag2)
							{
								sent.Add(stylesheet2);
								yield return stylesheet2;
							}
							stylesheet2 = null;
							stylesheetPath = null;
						}
						List<string>.Enumerator enumerator3 = default(List<string>.Enumerator);
					}
					vea = null;
				}
				List<VisualElementAsset>.Enumerator enumerator = default(List<VisualElementAsset>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x0009A2C3 File Offset: 0x000984C3
		internal List<VisualElementAsset> visualElementAssets
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_VisualElementAssets;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x0009A2CB File Offset: 0x000984CB
		internal List<TemplateAsset> templateAssets
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_TemplateAssets;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x0009A2D3 File Offset: 0x000984D3
		internal List<VisualTreeAsset.UxmlObjectEntry> uxmlObjectEntries
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_UxmlObjectEntries;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x0009A2DB File Offset: 0x000984DB
		internal List<int> uxmlObjectIds
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_UxmlObjectIds;
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0009A2E4 File Offset: 0x000984E4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void RemoveElementAndDependencies(VisualElementAsset asset)
		{
			bool flag = asset == null;
			if (!flag)
			{
				this.m_VisualElementAssets.Remove(asset);
				this.RemoveUxmlObjectEntryDependencies(asset.id);
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0009A318 File Offset: 0x00098518
		internal void RegisterUxmlObject(UxmlObjectAsset uxmlObjectAsset)
		{
			VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry = this.GetUxmlObjectEntry(uxmlObjectAsset.parentId);
			bool flag = uxmlObjectEntry.uxmlObjectAssets != null;
			if (flag)
			{
				uxmlObjectEntry.uxmlObjectAssets.Add(uxmlObjectAsset);
			}
			else
			{
				this.m_UxmlObjectEntries.Add(new VisualTreeAsset.UxmlObjectEntry(uxmlObjectAsset.parentId, new List<UxmlObjectAsset> { uxmlObjectAsset }));
				this.m_UxmlObjectIds.Add(uxmlObjectAsset.id);
			}
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0009A38C File Offset: 0x0009858C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal UxmlObjectAsset AddUxmlObject(UxmlAsset parent, string fieldUxmlName, string fullTypeName, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
		{
			VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry = this.GetUxmlObjectEntry(parent.id);
			bool flag = uxmlObjectEntry.uxmlObjectAssets == null;
			if (flag)
			{
				uxmlObjectEntry = new VisualTreeAsset.UxmlObjectEntry(parent.id, new List<UxmlObjectAsset>());
				this.m_UxmlObjectEntries.Add(uxmlObjectEntry);
			}
			bool flag2 = string.IsNullOrEmpty(fieldUxmlName);
			UxmlObjectAsset uxmlObjectAsset2;
			if (flag2)
			{
				UxmlObjectAsset uxmlObjectAsset = new UxmlObjectAsset(fullTypeName, false, xmlNamespace);
				uxmlObjectAsset.parentId = parent.id;
				uxmlObjectAsset.id = this.GetNextUxmlAssetId(parent.id);
				this.m_UxmlObjectIds.Add(uxmlObjectAsset.id);
				uxmlObjectEntry.uxmlObjectAssets.Add(uxmlObjectAsset);
				uxmlObjectAsset2 = uxmlObjectAsset;
			}
			else
			{
				UxmlObjectAsset uxmlObjectAsset3 = uxmlObjectEntry.GetField(fieldUxmlName);
				bool flag3 = uxmlObjectAsset3 == null;
				if (flag3)
				{
					uxmlObjectAsset3 = new UxmlObjectAsset(fieldUxmlName, true, xmlNamespace);
					uxmlObjectEntry.uxmlObjectAssets.Add(uxmlObjectAsset3);
					uxmlObjectAsset3.parentId = parent.id;
					uxmlObjectAsset3.id = this.GetNextUxmlAssetId(parent.id);
					this.m_UxmlObjectIds.Add(uxmlObjectAsset3.id);
				}
				uxmlObjectAsset2 = this.AddUxmlObject(uxmlObjectAsset3, null, fullTypeName, xmlNamespace);
			}
			return uxmlObjectAsset2;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0009A4A4 File Offset: 0x000986A4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int GetNextUxmlAssetId(int parentId)
		{
			int hashCode = Guid.NewGuid().GetHashCode();
			return (this.GetNextChildSerialNumber() + 585386304) * -1521134295 + parentId + hashCode;
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0009A4E0 File Offset: 0x000986E0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void RemoveUxmlObject(int id, bool onlyIfIsField = false)
		{
			for (int i = 0; i < this.m_UxmlObjectEntries.Count; i++)
			{
				VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry = this.m_UxmlObjectEntries[i];
				int j = 0;
				while (j < uxmlObjectEntry.uxmlObjectAssets.Count)
				{
					UxmlObjectAsset uxmlObjectAsset = uxmlObjectEntry.uxmlObjectAssets[j];
					bool flag = uxmlObjectAsset.id == id;
					if (flag)
					{
						bool flag2 = onlyIfIsField && !uxmlObjectAsset.isField;
						if (flag2)
						{
							return;
						}
						uxmlObjectEntry.uxmlObjectAssets.RemoveAt(j);
						this.RemoveUxmlObjectEntryDependencies(uxmlObjectAsset.id);
						bool flag3 = uxmlObjectEntry.uxmlObjectAssets.Count == 0;
						if (flag3)
						{
							int num = this.m_UxmlObjectEntries.IndexOf(uxmlObjectEntry);
							this.m_UxmlObjectEntries.RemoveAt(num);
							this.m_UxmlObjectIds.RemoveAt(num);
							this.RemoveUxmlObject(uxmlObjectEntry.parentId, true);
						}
						return;
					}
					else
					{
						j++;
					}
				}
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0009A5E8 File Offset: 0x000987E8
		private void RemoveUxmlObjectEntryDependencies(int parentId)
		{
			bool flag = this.m_UxmlObjectEntries.Count == 0;
			if (!flag)
			{
				List<VisualTreeAsset.UxmlObjectEntry> list = CollectionPool<List<VisualTreeAsset.UxmlObjectEntry>, VisualTreeAsset.UxmlObjectEntry>.Get();
				foreach (VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry in this.m_UxmlObjectEntries)
				{
					bool flag2 = parentId == uxmlObjectEntry.parentId;
					if (flag2)
					{
						list.Add(uxmlObjectEntry);
					}
				}
				foreach (VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry2 in list)
				{
					int num = this.m_UxmlObjectEntries.IndexOf(uxmlObjectEntry2);
					this.m_UxmlObjectEntries.RemoveAt(num);
					this.m_UxmlObjectIds.RemoveAt(num);
					foreach (UxmlObjectAsset uxmlObjectAsset in uxmlObjectEntry2.uxmlObjectAssets)
					{
						this.RemoveUxmlObjectEntryDependencies(uxmlObjectAsset.id);
					}
				}
				CollectionPool<List<VisualTreeAsset.UxmlObjectEntry>, VisualTreeAsset.UxmlObjectEntry>.Release(list);
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0009A730 File Offset: 0x00098930
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void CollectUxmlObjectAssets(UxmlAsset parent, string fieldName, List<UxmlObjectAsset> foundEntries)
		{
			bool flag = parent == null;
			if (!flag)
			{
				foreach (VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry in this.m_UxmlObjectEntries)
				{
					bool flag2 = uxmlObjectEntry.parentId == parent.id;
					if (flag2)
					{
						bool flag3 = !string.IsNullOrEmpty(fieldName);
						if (flag3)
						{
							UxmlObjectAsset field = uxmlObjectEntry.GetField(fieldName);
							bool flag4 = field != null;
							if (flag4)
							{
								this.CollectUxmlObjectAssets(field, null, foundEntries);
							}
						}
						else
						{
							foreach (UxmlObjectAsset uxmlObjectAsset in uxmlObjectEntry.uxmlObjectAssets)
							{
								bool flag5 = !uxmlObjectAsset.isField;
								if (flag5)
								{
									foundEntries.Add(uxmlObjectAsset);
								}
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0009A840 File Offset: 0x00098A40
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SetUxmlObjectAssets(UxmlAsset parent, string fieldName, List<UxmlObjectAsset> entries)
		{
			foreach (VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry in this.m_UxmlObjectEntries)
			{
				bool flag = uxmlObjectEntry.parentId == parent.id;
				if (flag)
				{
					bool flag2 = !string.IsNullOrEmpty(fieldName);
					if (flag2)
					{
						UxmlObjectAsset field = uxmlObjectEntry.GetField(fieldName);
						bool flag3 = field != null;
						if (flag3)
						{
							this.SetUxmlObjectAssets(field, null, entries);
						}
					}
					else
					{
						for (int i = uxmlObjectEntry.uxmlObjectAssets.Count - 1; i >= 0; i--)
						{
							bool flag4 = !uxmlObjectEntry.uxmlObjectAssets[i].isField;
							if (flag4)
							{
								uxmlObjectEntry.uxmlObjectAssets.RemoveAt(i);
							}
						}
						uxmlObjectEntry.uxmlObjectAssets.AddRange(entries);
						bool flag5 = uxmlObjectEntry.uxmlObjectAssets.Count == 0;
						if (flag5)
						{
							int num = this.m_UxmlObjectEntries.IndexOf(uxmlObjectEntry);
							this.m_UxmlObjectEntries.RemoveAt(num);
							this.m_UxmlObjectIds.RemoveAt(num);
							this.RemoveUxmlObject(uxmlObjectEntry.parentId, true);
						}
					}
					break;
				}
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0009A9A0 File Offset: 0x00098BA0
		internal List<T> GetUxmlObjects<T>(IUxmlAttributes asset, CreationContext cc) where T : new()
		{
			UxmlAsset uxmlAsset = asset as UxmlAsset;
			bool flag = uxmlAsset != null;
			if (flag)
			{
				VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry = this.GetUxmlObjectEntry(uxmlAsset.id);
				bool flag2 = uxmlObjectEntry.uxmlObjectAssets != null;
				if (flag2)
				{
					List<T> list = null;
					foreach (UxmlObjectAsset uxmlObjectAsset in uxmlObjectEntry.uxmlObjectAssets)
					{
						IBaseUxmlObjectFactory uxmlObjectFactory = this.GetUxmlObjectFactory(uxmlObjectAsset);
						IUxmlObjectFactory<T> uxmlObjectFactory2 = uxmlObjectFactory as IUxmlObjectFactory<T>;
						bool flag3 = uxmlObjectFactory2 == null;
						if (!flag3)
						{
							T t = uxmlObjectFactory2.CreateObject(uxmlObjectAsset, cc);
							bool flag4 = list == null;
							if (flag4)
							{
								list = new List<T> { t };
							}
							else
							{
								list.Add(t);
							}
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x0009AA8C File Offset: 0x00098C8C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool AssetEntryExists(string path, Type type)
		{
			foreach (VisualTreeAsset.AssetEntry assetEntry in this.m_AssetEntries)
			{
				bool flag = assetEntry.path == path && assetEntry.type == type;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x0009AB0C File Offset: 0x00098D0C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void RegisterAssetEntry(string path, Type type, Object asset)
		{
			this.m_AssetEntries.Add(new VisualTreeAsset.AssetEntry(path, type, asset));
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x0009AB23 File Offset: 0x00098D23
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void TransferAssetEntries(VisualTreeAsset otherVta)
		{
			this.m_AssetEntries.Clear();
			this.m_AssetEntries.AddRange(otherVta.m_AssetEntries);
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x0009AB44 File Offset: 0x00098D44
		internal T GetAsset<T>(string path) where T : Object
		{
			return this.GetAsset(path, typeof(T)) as T;
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0009AB64 File Offset: 0x00098D64
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Object GetAsset(string path, Type type)
		{
			foreach (VisualTreeAsset.AssetEntry assetEntry in this.m_AssetEntries)
			{
				bool flag = assetEntry.path == path && type.IsAssignableFrom(assetEntry.type);
				if (flag)
				{
					return assetEntry.asset;
				}
			}
			return null;
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0009ABE8 File Offset: 0x00098DE8
		internal Type GetAssetType(string path)
		{
			foreach (VisualTreeAsset.AssetEntry assetEntry in this.m_AssetEntries)
			{
				bool flag = assetEntry.path == path;
				if (flag)
				{
					return assetEntry.type;
				}
			}
			return null;
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x0009AC5C File Offset: 0x00098E5C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualTreeAsset.UxmlObjectEntry GetUxmlObjectEntry(int id)
		{
			bool flag = this.m_UxmlObjectEntries != null;
			if (flag)
			{
				foreach (VisualTreeAsset.UxmlObjectEntry uxmlObjectEntry in this.m_UxmlObjectEntries)
				{
					bool flag2 = uxmlObjectEntry.parentId == id;
					if (flag2)
					{
						return uxmlObjectEntry;
					}
				}
			}
			return default(VisualTreeAsset.UxmlObjectEntry);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x0009ACE0 File Offset: 0x00098EE0
		internal IBaseUxmlObjectFactory GetUxmlObjectFactory(UxmlObjectAsset uxmlObjectAsset)
		{
			List<IBaseUxmlObjectFactory> list;
			bool flag = !UxmlObjectFactoryRegistry.factories.TryGetValue(uxmlObjectAsset.fullTypeName, out list);
			IBaseUxmlObjectFactory baseUxmlObjectFactory;
			if (flag)
			{
				Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[] { uxmlObjectAsset.fullTypeName });
				baseUxmlObjectFactory = null;
			}
			else
			{
				IBaseUxmlObjectFactory baseUxmlObjectFactory2 = null;
				CreationContext creationContext = new CreationContext(this);
				foreach (IBaseUxmlObjectFactory baseUxmlObjectFactory3 in list)
				{
					bool flag2 = baseUxmlObjectFactory3.AcceptsAttributeBag(uxmlObjectAsset, creationContext);
					if (flag2)
					{
						baseUxmlObjectFactory2 = baseUxmlObjectFactory3;
						break;
					}
				}
				bool flag3 = baseUxmlObjectFactory2 == null;
				if (flag3)
				{
					Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[] { uxmlObjectAsset.fullTypeName });
					baseUxmlObjectFactory = null;
				}
				else
				{
					baseUxmlObjectFactory = baseUxmlObjectFactory2;
				}
			}
			return baseUxmlObjectFactory;
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x0009ADB8 File Offset: 0x00098FB8
		internal List<VisualTreeAsset.SlotDefinition> slots
		{
			get
			{
				return this.m_Slots;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x0009ADC0 File Offset: 0x00098FC0
		// (set) Token: 0x06002AEB RID: 10987 RVA: 0x0009ADD8 File Offset: 0x00098FD8
		internal int contentContainerId
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_ContentContainerId;
			}
			set
			{
				this.m_ContentContainerId = value;
			}
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x0009ADE4 File Offset: 0x00098FE4
		public TemplateContainer Instantiate()
		{
			TemplateContainer templateContainer = new TemplateContainer(base.name, this);
			try
			{
				CreationContext creationContext = new CreationContext(VisualTreeAsset.s_TemporarySlotInsertionPoints, null, null, null, null, VisualTreeAsset.s_VeaIdsPath, null);
				this.CloneTree(templateContainer, creationContext);
			}
			finally
			{
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
				VisualTreeAsset.s_VeaIdsPath.Clear();
			}
			return templateContainer;
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x0009AE50 File Offset: 0x00099050
		public TemplateContainer Instantiate(string bindingPath)
		{
			TemplateContainer templateContainer = this.Instantiate();
			templateContainer.bindingPath = bindingPath;
			return templateContainer;
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x0009AE74 File Offset: 0x00099074
		public TemplateContainer CloneTree()
		{
			return this.Instantiate();
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x0009AE8C File Offset: 0x0009908C
		public TemplateContainer CloneTree(string bindingPath)
		{
			return this.Instantiate(bindingPath);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x0009AEA8 File Offset: 0x000990A8
		public void CloneTree(VisualElement target)
		{
			int num;
			int num2;
			this.CloneTree(target, out num, out num2);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0009AEC4 File Offset: 0x000990C4
		public void CloneTree(VisualElement target, out int firstElementIndex, out int elementAddedCount)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			firstElementIndex = target.childCount;
			try
			{
				CreationContext creationContext = new CreationContext(VisualTreeAsset.s_TemporarySlotInsertionPoints, null, null, null, null, VisualTreeAsset.s_VeaIdsPath, null);
				this.CloneTree(target, creationContext);
			}
			finally
			{
				elementAddedCount = target.childCount - firstElementIndex;
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
				VisualTreeAsset.s_VeaIdsPath.Clear();
			}
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x0009AF44 File Offset: 0x00099144
		internal void CloneTree(VisualElement target, CreationContext cc)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = (this.visualElementAssets == null || this.visualElementAssets.Count <= 0) && (this.templateAssets == null || this.templateAssets.Count <= 0);
			if (!flag2)
			{
				Dictionary<int, List<VisualElementAsset>> dictionary = new Dictionary<int, List<VisualElementAsset>>();
				int num = ((this.visualElementAssets == null) ? 0 : this.visualElementAssets.Count);
				int num2 = ((this.templateAssets == null) ? 0 : this.templateAssets.Count);
				for (int i = 0; i < num + num2; i++)
				{
					VisualElementAsset visualElementAsset = ((i < num) ? this.visualElementAssets[i] : this.templateAssets[i - num]);
					List<VisualElementAsset> list;
					bool flag3 = !dictionary.TryGetValue(visualElementAsset.parentId, out list);
					if (flag3)
					{
						list = new List<VisualElementAsset>();
						dictionary[visualElementAsset.parentId] = list;
					}
					list.Add(visualElementAsset);
				}
				List<VisualElementAsset> list2;
				dictionary.TryGetValue(0, out list2);
				bool flag4 = list2 == null || list2.Count == 0;
				if (!flag4)
				{
					Debug.Assert(list2.Count == 1);
					VisualElementAsset visualElementAsset2 = list2[0];
					VisualTreeAsset.AssignClassListFromAssetToElement(visualElementAsset2, target);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(visualElementAsset2, target);
					list2.Clear();
					dictionary.TryGetValue(visualElementAsset2.id, out list2);
					bool flag5 = list2 == null || list2.Count == 0;
					if (!flag5)
					{
						list2.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						foreach (VisualElementAsset visualElementAsset3 in list2)
						{
							Assert.IsNotNull<VisualElementAsset>(visualElementAsset3);
							bool flag6 = false;
							bool flag7 = visualElementAsset3 is TemplateAsset;
							if (flag7)
							{
								cc.veaIdsPath.Add(visualElementAsset3.id);
								flag6 = true;
							}
							CreationContext creationContext = new CreationContext(cc.slotInsertionPoints, cc.attributeOverrides, cc.serializedDataOverrides, this, target, cc.veaIdsPath, null);
							VisualElement visualElement = this.CloneSetupRecursively(visualElementAsset3, dictionary, creationContext);
							bool flag8 = flag6;
							if (flag8)
							{
								cc.veaIdsPath.Remove(visualElementAsset3.id);
							}
							bool flag9 = visualElement == null;
							if (!flag9)
							{
								visualElement.visualTreeAssetSource = this;
								target.hierarchy.Add(visualElement);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x0009B1DC File Offset: 0x000993DC
		private VisualElement CloneSetupRecursively(VisualElementAsset root, Dictionary<int, List<VisualElementAsset>> idToChildren, CreationContext context)
		{
			bool skipClone = root.skipClone;
			VisualElement visualElement;
			if (skipClone)
			{
				visualElement = null;
			}
			else
			{
				VisualElement visualElement2 = VisualTreeAsset.Create(root, context);
				bool flag = visualElement2 == null;
				if (flag)
				{
					visualElement = null;
				}
				else
				{
					bool flag2 = root.id == context.visualTreeAsset.contentContainerId;
					if (flag2)
					{
						TemplateContainer templateContainer = context.target as TemplateContainer;
						bool flag3 = templateContainer != null;
						if (flag3)
						{
							templateContainer.SetContentContainer(visualElement2);
						}
						else
						{
							Debug.LogError("Trying to clone a VisualTreeAsset with a custom content container into a element which is not a template container");
						}
					}
					string text;
					bool flag4 = context.slotInsertionPoints != null && this.TryGetSlotInsertionPoint(root.id, out text);
					if (flag4)
					{
						context.slotInsertionPoints.Add(text, visualElement2);
					}
					bool flag5 = root.ruleIndex != -1;
					if (flag5)
					{
						bool flag6 = this.inlineSheet == null;
						if (flag6)
						{
							Debug.LogWarning("VisualElementAsset has a RuleIndex but no inlineStyleSheet");
						}
						else
						{
							StyleRule styleRule = this.inlineSheet.rules[root.ruleIndex];
							visualElement2.SetInlineRule(this.inlineSheet, styleRule);
						}
					}
					TemplateAsset templateAsset = root as TemplateAsset;
					List<VisualElementAsset> list;
					bool flag7 = idToChildren.TryGetValue(root.id, out list);
					if (flag7)
					{
						list.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						using (List<VisualElementAsset>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								VisualElementAsset childVea = enumerator.Current;
								bool flag8 = false;
								bool flag9 = childVea is TemplateAsset;
								if (flag9)
								{
									context.veaIdsPath.Add(childVea.id);
									flag8 = true;
								}
								VisualElement visualElement3 = this.CloneSetupRecursively(childVea, idToChildren, context);
								bool flag10 = flag8;
								if (flag10)
								{
									context.veaIdsPath.Remove(childVea.id);
								}
								bool flag11 = visualElement3 == null;
								if (!flag11)
								{
									bool flag12 = templateAsset == null;
									if (flag12)
									{
										visualElement2.Add(visualElement3);
									}
									else
									{
										int num = ((templateAsset.slotUsages == null) ? (-1) : templateAsset.slotUsages.FindIndex((VisualTreeAsset.SlotUsageEntry u) => u.assetId == childVea.id));
										bool flag13 = num != -1;
										if (flag13)
										{
											string slotName = templateAsset.slotUsages[num].slotName;
											Assert.IsFalse(string.IsNullOrEmpty(slotName), "a lost name should not be null or empty, this probably points to an importer or serialization bug");
											VisualElement visualElement4;
											bool flag14 = context.slotInsertionPoints == null || !context.slotInsertionPoints.TryGetValue(slotName, out visualElement4);
											if (flag14)
											{
												Debug.LogErrorFormat("Slot '{0}' was not found. Existing slots: {1}", new object[]
												{
													slotName,
													(context.slotInsertionPoints == null) ? string.Empty : string.Join(", ", context.slotInsertionPoints.Keys.ToArray<string>())
												});
												visualElement2.Add(visualElement3);
											}
											else
											{
												visualElement4.Add(visualElement3);
											}
										}
										else
										{
											visualElement2.Add(visualElement3);
										}
									}
								}
							}
						}
					}
					bool flag15 = templateAsset != null && context.slotInsertionPoints != null;
					if (flag15)
					{
						context.slotInsertionPoints.Clear();
					}
					visualElement = visualElement2;
				}
			}
			return visualElement;
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x0009B524 File Offset: 0x00099724
		internal static int CompareForOrder(VisualElementAsset a, VisualElementAsset b)
		{
			return a.orderInDocument.CompareTo(b.orderInDocument);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x0009B548 File Offset: 0x00099748
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool TryGetSlotInsertionPoint(int insertionPointId, out string slotName)
		{
			for (int i = 0; i < this.m_Slots.Count; i++)
			{
				VisualTreeAsset.SlotDefinition slotDefinition = this.m_Slots[i];
				bool flag = slotDefinition.insertionPointId == insertionPointId;
				if (flag)
				{
					slotName = slotDefinition.name;
					return true;
				}
			}
			slotName = null;
			return false;
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0009B5A4 File Offset: 0x000997A4
		internal bool TryGetUsingEntry(string templateName, out VisualTreeAsset.UsingEntry entry)
		{
			entry = default(VisualTreeAsset.UsingEntry);
			bool flag = this.m_Usings.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int num = this.m_Usings.BinarySearch(new VisualTreeAsset.UsingEntry(templateName, string.Empty), VisualTreeAsset.UsingEntry.comparer);
				bool flag3 = num < 0;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					entry = this.m_Usings[num];
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0009B60E File Offset: 0x0009980E
		private void RemoveUsingEntry(VisualTreeAsset.UsingEntry entry)
		{
			this.m_Usings.Remove(entry);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0009B620 File Offset: 0x00099820
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualTreeAsset ResolveTemplate(string templateName)
		{
			VisualTreeAsset.UsingEntry usingEntry;
			bool flag = !this.TryGetUsingEntry(templateName, out usingEntry);
			VisualTreeAsset visualTreeAsset;
			if (flag)
			{
				visualTreeAsset = null;
			}
			else
			{
				bool flag2 = usingEntry.asset;
				if (flag2)
				{
					visualTreeAsset = usingEntry.asset;
				}
				else
				{
					string path = usingEntry.path;
					visualTreeAsset = Panel.LoadResource(path, typeof(VisualTreeAsset), 1f) as VisualTreeAsset;
				}
			}
			return visualTreeAsset;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x0009B684 File Offset: 0x00099884
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static VisualElement Create(VisualElementAsset asset, CreationContext ctx)
		{
			VisualTreeAsset.<>c__DisplayClass82_0 CS$<>8__locals1;
			CS$<>8__locals1.asset = asset;
			bool flag = CS$<>8__locals1.asset.serializedData != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = CS$<>8__locals1.asset.Instantiate(ctx);
			}
			else
			{
				List<IUxmlFactory> list;
				bool flag2 = !VisualElementFactoryRegistry.TryGetValue(CS$<>8__locals1.asset.fullTypeName, out list);
				if (flag2)
				{
					bool flag3 = CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEngine.Experimental.UIElements.") || CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEditor.Experimental.UIElements.");
					if (flag3)
					{
						string text = CS$<>8__locals1.asset.fullTypeName.Replace(".Experimental.UIElements", ".UIElements");
						bool flag4 = !VisualElementFactoryRegistry.TryGetValue(text, out list);
						if (flag4)
						{
							return VisualTreeAsset.<Create>g__CreateError|82_0(ref CS$<>8__locals1);
						}
					}
					else
					{
						bool flag5 = CS$<>8__locals1.asset.fullTypeName == "UXML";
						if (!flag5)
						{
							return VisualTreeAsset.<Create>g__CreateError|82_0(ref CS$<>8__locals1);
						}
						VisualElementFactoryRegistry.TryGetValue(typeof(UxmlRootElementFactory).Namespace + "." + CS$<>8__locals1.asset.fullTypeName, out list);
					}
				}
				IUxmlFactory uxmlFactory = null;
				foreach (IUxmlFactory uxmlFactory2 in list)
				{
					bool flag6 = uxmlFactory2.AcceptsAttributeBag(CS$<>8__locals1.asset, ctx);
					if (flag6)
					{
						uxmlFactory = uxmlFactory2;
						break;
					}
				}
				bool flag7 = uxmlFactory == null;
				if (flag7)
				{
					Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[] { CS$<>8__locals1.asset.fullTypeName });
					visualElement = new Label(string.Format("Type with no factory: '{0}'", CS$<>8__locals1.asset.fullTypeName));
				}
				else
				{
					VisualElement visualElement2 = uxmlFactory.Create(CS$<>8__locals1.asset, ctx);
					bool flag8 = visualElement2 != null;
					if (flag8)
					{
						VisualTreeAsset.AssignClassListFromAssetToElement(CS$<>8__locals1.asset, visualElement2);
						VisualTreeAsset.AssignStyleSheetFromAssetToElement(CS$<>8__locals1.asset, visualElement2);
					}
					visualElement = visualElement2;
				}
			}
			return visualElement;
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x0009B894 File Offset: 0x00099A94
		private static void AssignClassListFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool flag = asset.classes != null;
			if (flag)
			{
				for (int i = 0; i < asset.classes.Length; i++)
				{
					element.AddToClassList(asset.classes[i]);
				}
			}
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0009B8DC File Offset: 0x00099ADC
		private static void AssignStyleSheetFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool hasStylesheetPaths = asset.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int i = 0; i < asset.stylesheetPaths.Count; i++)
				{
					element.AddStyleSheetPath(asset.stylesheetPaths[i]);
				}
			}
			bool hasStylesheets = asset.hasStylesheets;
			if (hasStylesheets)
			{
				for (int j = 0; j < asset.stylesheets.Count; j++)
				{
					bool flag = asset.stylesheets[j] != null;
					if (flag)
					{
						element.styleSheets.Add(asset.stylesheets[j]);
					}
				}
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0009B98C File Offset: 0x00099B8C
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0009B9A4 File Offset: 0x00099BA4
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0009BA3C File Offset: 0x00099C3C
		[CompilerGenerated]
		internal static VisualElement <Create>g__CreateError|82_0(ref VisualTreeAsset.<>c__DisplayClass82_0 A_0)
		{
			Debug.LogErrorFormat(VisualTreeAsset.NoRegisteredFactoryErrorMessage, new object[] { A_0.asset.fullTypeName });
			return new Label(string.Format("Unknown type: '{0}'", A_0.asset.fullTypeName));
		}

		// Token: 0x04001322 RID: 4898
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static string LinkedVEAInTemplatePropertyName = "--unity-linked-vea-in-template";

		// Token: 0x04001323 RID: 4899
		internal static string NoRegisteredFactoryErrorMessage = "Element '{0}' is missing a UxmlElementAttribute and has no registered factory method. Please ensure that you have the correct namespace imported.";

		// Token: 0x04001324 RID: 4900
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x04001325 RID: 4901
		[SerializeField]
		private bool m_HasUpdatedUrls;

		// Token: 0x04001326 RID: 4902
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x04001327 RID: 4903
		private static readonly Dictionary<string, VisualElement> s_TemporarySlotInsertionPoints = new Dictionary<string, VisualElement>();

		// Token: 0x04001328 RID: 4904
		private static readonly List<int> s_VeaIdsPath = new List<int>();

		// Token: 0x04001329 RID: 4905
		[SerializeField]
		private List<VisualTreeAsset.UsingEntry> m_Usings = new List<VisualTreeAsset.UsingEntry>();

		// Token: 0x0400132A RID: 4906
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleSheet inlineSheet;

		// Token: 0x0400132B RID: 4907
		[SerializeField]
		internal List<VisualElementAsset> m_VisualElementAssets = new List<VisualElementAsset>();

		// Token: 0x0400132C RID: 4908
		[SerializeField]
		internal List<TemplateAsset> m_TemplateAssets = new List<TemplateAsset>();

		// Token: 0x0400132D RID: 4909
		[SerializeField]
		private List<VisualTreeAsset.UxmlObjectEntry> m_UxmlObjectEntries = new List<VisualTreeAsset.UxmlObjectEntry>();

		// Token: 0x0400132E RID: 4910
		[SerializeField]
		private List<int> m_UxmlObjectIds = new List<int>();

		// Token: 0x0400132F RID: 4911
		[SerializeField]
		private List<VisualTreeAsset.AssetEntry> m_AssetEntries = new List<VisualTreeAsset.AssetEntry>();

		// Token: 0x04001330 RID: 4912
		[SerializeField]
		private List<VisualTreeAsset.SlotDefinition> m_Slots = new List<VisualTreeAsset.SlotDefinition>();

		// Token: 0x04001331 RID: 4913
		[SerializeField]
		private int m_ContentContainerId;

		// Token: 0x04001332 RID: 4914
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x02000594 RID: 1428
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct UsingEntry
		{
			// Token: 0x06002B01 RID: 11009 RVA: 0x0009BA87 File Offset: 0x00099C87
			public UsingEntry(string alias, string path)
			{
				this.alias = alias;
				this.path = path;
				this.asset = null;
			}

			// Token: 0x06002B02 RID: 11010 RVA: 0x0009BA9F File Offset: 0x00099C9F
			public UsingEntry(string alias, VisualTreeAsset asset)
			{
				this.alias = alias;
				this.path = null;
				this.asset = asset;
			}

			// Token: 0x04001333 RID: 4915
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal static readonly IComparer<VisualTreeAsset.UsingEntry> comparer = new VisualTreeAsset.UsingEntryComparer();

			// Token: 0x04001334 RID: 4916
			[SerializeField]
			public string alias;

			// Token: 0x04001335 RID: 4917
			[SerializeField]
			public string path;

			// Token: 0x04001336 RID: 4918
			[SerializeField]
			public VisualTreeAsset asset;
		}

		// Token: 0x02000595 RID: 1429
		private class UsingEntryComparer : IComparer<VisualTreeAsset.UsingEntry>
		{
			// Token: 0x06002B04 RID: 11012 RVA: 0x0009BAC4 File Offset: 0x00099CC4
			public int Compare(VisualTreeAsset.UsingEntry x, VisualTreeAsset.UsingEntry y)
			{
				return string.CompareOrdinal(x.alias, y.alias);
			}
		}

		// Token: 0x02000596 RID: 1430
		[Serializable]
		internal struct SlotDefinition
		{
			// Token: 0x04001337 RID: 4919
			[SerializeField]
			public string name;

			// Token: 0x04001338 RID: 4920
			[SerializeField]
			public int insertionPointId;
		}

		// Token: 0x02000597 RID: 1431
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct SlotUsageEntry
		{
			// Token: 0x06002B06 RID: 11014 RVA: 0x0009BAE7 File Offset: 0x00099CE7
			public SlotUsageEntry(string slotName, int assetId)
			{
				this.slotName = slotName;
				this.assetId = assetId;
			}

			// Token: 0x04001339 RID: 4921
			[SerializeField]
			public string slotName;

			// Token: 0x0400133A RID: 4922
			[SerializeField]
			public int assetId;
		}

		// Token: 0x02000598 RID: 1432
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct UxmlObjectEntry
		{
			// Token: 0x06002B07 RID: 11015 RVA: 0x0009BAF8 File Offset: 0x00099CF8
			public UxmlObjectEntry(int parentId, List<UxmlObjectAsset> uxmlObjectAssets)
			{
				this.parentId = parentId;
				this.uxmlObjectAssets = uxmlObjectAssets;
			}

			// Token: 0x06002B08 RID: 11016 RVA: 0x0009BB0C File Offset: 0x00099D0C
			public UxmlObjectAsset GetField(string fieldName)
			{
				foreach (UxmlObjectAsset uxmlObjectAsset in this.uxmlObjectAssets)
				{
					bool flag = uxmlObjectAsset.isField && uxmlObjectAsset.fullTypeName == fieldName;
					if (flag)
					{
						return uxmlObjectAsset;
					}
				}
				return null;
			}

			// Token: 0x06002B09 RID: 11017 RVA: 0x0009BB84 File Offset: 0x00099D84
			public override string ToString()
			{
				string text = "UxmlObjectEntry parent:{0} ({1})";
				object obj = this.parentId;
				List<UxmlObjectAsset> list = this.uxmlObjectAssets;
				return string.Format(text, obj, (list != null) ? new int?(list.Count) : null);
			}

			// Token: 0x0400133B RID: 4923
			[SerializeField]
			public int parentId;

			// Token: 0x0400133C RID: 4924
			[SerializeField]
			public List<UxmlObjectAsset> uxmlObjectAssets;
		}

		// Token: 0x02000599 RID: 1433
		[Serializable]
		private struct AssetEntry
		{
			// Token: 0x17000ABD RID: 2749
			// (get) Token: 0x06002B0A RID: 11018 RVA: 0x0009BBCC File Offset: 0x00099DCC
			public Type type
			{
				get
				{
					Type type;
					if ((type = this.m_CachedType) == null)
					{
						type = (this.m_CachedType = Type.GetType(this.m_TypeFullName));
					}
					return type;
				}
			}

			// Token: 0x17000ABE RID: 2750
			// (get) Token: 0x06002B0B RID: 11019 RVA: 0x0009BBF7 File Offset: 0x00099DF7
			public string path
			{
				get
				{
					return this.m_Path;
				}
			}

			// Token: 0x17000ABF RID: 2751
			// (get) Token: 0x06002B0C RID: 11020 RVA: 0x0009BC00 File Offset: 0x00099E00
			public Object asset
			{
				get
				{
					bool isSet = this.m_AssetReference.isSet;
					Object @object;
					if (isSet)
					{
						@object = this.m_AssetReference.asset;
					}
					else
					{
						@object = null;
					}
					return @object;
				}
			}

			// Token: 0x06002B0D RID: 11021 RVA: 0x0009BC31 File Offset: 0x00099E31
			public AssetEntry(string path, Type type, Object asset)
			{
				this.m_Path = path;
				this.m_TypeFullName = type.AssemblyQualifiedName;
				this.m_CachedType = type;
				this.m_AssetReference = asset;
				this.m_InstanceID = ((asset != null) ? asset.GetInstanceID() : 0);
			}

			// Token: 0x0400133D RID: 4925
			[SerializeField]
			private string m_Path;

			// Token: 0x0400133E RID: 4926
			[SerializeField]
			private string m_TypeFullName;

			// Token: 0x0400133F RID: 4927
			[SerializeField]
			private LazyLoadReference<Object> m_AssetReference;

			// Token: 0x04001340 RID: 4928
			[SerializeField]
			private int m_InstanceID;

			// Token: 0x04001341 RID: 4929
			private Type m_CachedType;
		}
	}
}
