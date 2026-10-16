using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020005A1 RID: 1441
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class VisualTreeAssetUtilities
	{
		// Token: 0x06002B41 RID: 11073 RVA: 0x0009C5F4 File Offset: 0x0009A7F4
		public static UxmlAsset GetParentAsset(this VisualTreeAsset vta, UxmlAsset asset)
		{
			int parentId = asset.parentId;
			int num = vta.visualElementAssets.FindIndex((VisualElementAsset ua) => ua.id == parentId);
			bool flag = num >= 0;
			UxmlAsset uxmlAsset;
			if (flag)
			{
				uxmlAsset = vta.visualElementAssets[num];
			}
			else
			{
				num = vta.templateAssets.FindIndex((TemplateAsset ta) => ta.id == parentId);
				bool flag2 = num >= 0;
				if (flag2)
				{
					uxmlAsset = vta.templateAssets[num];
				}
				else
				{
					Predicate<UxmlObjectAsset> <>9__2;
					for (int i = 0; i < vta.uxmlObjectEntries.Count; i++)
					{
						List<UxmlObjectAsset> uxmlObjectAssets = vta.uxmlObjectEntries[i].uxmlObjectAssets;
						List<UxmlObjectAsset> list = uxmlObjectAssets;
						Predicate<UxmlObjectAsset> predicate;
						if ((predicate = <>9__2) == null)
						{
							predicate = (<>9__2 = (UxmlObjectAsset ua) => ua.id == parentId);
						}
						num = list.FindIndex(predicate);
						bool flag3 = num >= 0;
						if (flag3)
						{
							return uxmlObjectAssets[num];
						}
					}
					uxmlAsset = null;
				}
			}
			return uxmlAsset;
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0009C6FC File Offset: 0x0009A8FC
		public static IEnumerable<string> EnumerateEnclosingNamespaces(string fullTypeName)
		{
			int startIndex = fullTypeName.Length - 1;
			for (;;)
			{
				int lastDot = fullTypeName.LastIndexOf(".", startIndex, StringComparison.Ordinal);
				bool flag = lastDot >= 0;
				if (!flag)
				{
					break;
				}
				yield return fullTypeName.Substring(0, lastDot);
				startIndex = lastDot - 1;
			}
			yield break;
			yield break;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x0009C70C File Offset: 0x0009A90C
		public static UxmlNamespaceDefinition FindUxmlNamespaceDefinitionFromPrefix(this VisualTreeAsset vta, UxmlAsset asset, string prefix)
		{
			for (UxmlAsset uxmlAsset = asset; uxmlAsset != null; uxmlAsset = vta.GetParentAsset(uxmlAsset))
			{
				for (int i = 0; i < uxmlAsset.namespaceDefinitions.Count; i++)
				{
					UxmlNamespaceDefinition uxmlNamespaceDefinition = uxmlAsset.namespaceDefinitions[i];
					bool flag = string.Compare(uxmlNamespaceDefinition.prefix, prefix, StringComparison.Ordinal) == 0;
					if (flag)
					{
						return uxmlNamespaceDefinition;
					}
				}
			}
			return UxmlNamespaceDefinition.Empty;
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x0009C784 File Offset: 0x0009A984
		public static UxmlNamespaceDefinition FindUxmlNamespaceDefinitionForTypeName(this VisualTreeAsset vta, UxmlAsset asset, string fullTypeName)
		{
			List<UxmlNamespaceDefinition> list;
			UxmlNamespaceDefinition uxmlNamespaceDefinition;
			using (CollectionPool<List<UxmlNamespaceDefinition>, UxmlNamespaceDefinition>.Get(out list))
			{
				for (UxmlAsset uxmlAsset = asset; uxmlAsset != null; uxmlAsset = vta.GetParentAsset(uxmlAsset))
				{
					list.AddRange(uxmlAsset.namespaceDefinitions);
				}
				bool flag = list.Count == 0;
				if (flag)
				{
					uxmlNamespaceDefinition = UxmlNamespaceDefinition.Empty;
				}
				else
				{
					foreach (string text in VisualTreeAssetUtilities.EnumerateEnclosingNamespaces(fullTypeName))
					{
						for (int i = 0; i < list.Count; i++)
						{
							bool flag2 = list[i].resolvedNamespace.Equals(text, StringComparison.Ordinal);
							if (flag2)
							{
								return list[i];
							}
						}
					}
					uxmlNamespaceDefinition = UxmlNamespaceDefinition.Empty;
				}
			}
			return uxmlNamespaceDefinition;
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x0009C888 File Offset: 0x0009AA88
		public static void GatherUxmlNamespaceDefinitions(this VisualTreeAsset vta, UxmlAsset asset, List<UxmlNamespaceDefinition> definitions)
		{
			for (UxmlAsset uxmlAsset = asset; uxmlAsset != null; uxmlAsset = vta.GetParentAsset(uxmlAsset))
			{
				definitions.InsertRange(0, uxmlAsset.namespaceDefinitions);
			}
		}
	}
}
