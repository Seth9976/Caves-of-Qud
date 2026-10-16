using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.Properties;
using UnityEngine.Bindings;
using UnityEngine.Pool;
using UnityEngine.UIElements.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000044 RID: 68
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class DataBindingUtility
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000A830 File Offset: 0x00008A30
		public static void GetBoundElements(IPanel panel, List<VisualElement> boundElements)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel != null;
			if (flag)
			{
				boundElements.AddRange(baseVisualElementPanel.dataBindingManager.GetUnorderedBoundElements());
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A860 File Offset: 0x00008A60
		public static void GetBindingsForElement(VisualElement element, List<BindingInfo> result)
		{
			HashSet<PropertyPath> hashSet;
			using (CollectionPool<HashSet<PropertyPath>, PropertyPath>.Get(out hashSet))
			{
				foreach (ValueTuple<Binding, BindingId> valueTuple in DataBindingManager.GetBindingRequests(element))
				{
					bool flag = hashSet.Add(in valueTuple.Item2) && valueTuple.Item1 != null;
					if (flag)
					{
						PropertyPath propertyPath = in valueTuple.Item2;
						result.Add(BindingInfo.FromRequest(element, in propertyPath, valueTuple.Item1));
					}
				}
				bool flag2 = element.elementPanel == null;
				if (!flag2)
				{
					List<DataBindingManager.BindingData> bindingData = element.elementPanel.dataBindingManager.GetBindingData(element);
					foreach (DataBindingManager.BindingData bindingData2 in bindingData)
					{
						bool flag3 = hashSet.Add(in bindingData2.target.bindingId);
						if (flag3)
						{
							result.Add(BindingInfo.FromBindingData(in bindingData2));
						}
					}
				}
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public static bool TryGetBinding(VisualElement element, in BindingId bindingId, out BindingInfo bindingInfo)
		{
			Binding binding;
			bool flag = DataBindingManager.TryGetBindingRequest(element, in bindingId, out binding);
			bool flag3;
			if (flag)
			{
				bool flag2 = binding == null;
				if (flag2)
				{
					bindingInfo = default(BindingInfo);
					flag3 = false;
				}
				else
				{
					PropertyPath propertyPath = in bindingId;
					bindingInfo = BindingInfo.FromRequest(element, in propertyPath, binding);
					flag3 = true;
				}
			}
			else
			{
				bool flag4 = element.elementPanel != null;
				if (flag4)
				{
					DataBindingManager.BindingData bindingData;
					bool flag5 = element.elementPanel.dataBindingManager.TryGetBindingData(element, in bindingId, out bindingData);
					if (flag5)
					{
						bindingInfo = BindingInfo.FromBindingData(in bindingData);
						return true;
					}
				}
				bindingInfo = default(BindingInfo);
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AA3C File Offset: 0x00008C3C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static bool TryGetDataSourceOrDataSourceTypeFromHierarchy(VisualElement element, out object dataSourceObject, out Type dataSourceType, out PropertyPath fullPath)
		{
			VisualElement visualElement = element;
			dataSourceObject = null;
			dataSourceType = null;
			fullPath = default(PropertyPath);
			while (visualElement != null)
			{
				PropertyPath propertyPath = visualElement.dataSourcePath;
				bool flag = !propertyPath.IsEmpty;
				if (flag)
				{
					bool isEmpty = fullPath.IsEmpty;
					if (isEmpty)
					{
						fullPath = visualElement.dataSourcePath;
					}
					else
					{
						propertyPath = visualElement.dataSourcePath;
						fullPath = PropertyPath.Combine(in propertyPath, in fullPath);
					}
				}
				dataSourceObject = visualElement.dataSource;
				bool flag2 = visualElement.dataSource != null;
				if (flag2)
				{
					return true;
				}
				visualElement = visualElement.hierarchy.parent;
			}
			return !fullPath.IsEmpty;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public static bool TryGetRelativeDataSourceFromHierarchy(VisualElement element, out object dataSource)
		{
			DataSourceContext hierarchicalDataSourceContext = element.GetHierarchicalDataSourceContext();
			dataSource = hierarchicalDataSourceContext.dataSource;
			PropertyPath propertyPath = hierarchicalDataSourceContext.dataSourcePath;
			bool isEmpty = propertyPath.IsEmpty;
			bool flag;
			if (isEmpty)
			{
				flag = dataSource != null;
			}
			else
			{
				bool flag2 = hierarchicalDataSourceContext.dataSource == null;
				if (flag2)
				{
					flag = false;
				}
				else
				{
					propertyPath = hierarchicalDataSourceContext.dataSourcePath;
					object obj;
					bool flag3 = !PropertyContainer.TryGetValue<object, object>(ref dataSource, in propertyPath, out obj);
					if (flag3)
					{
						flag = true;
					}
					else
					{
						dataSource = obj;
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AB64 File Offset: 0x00008D64
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static string ReplaceAllIndicesInPath(string path, string newText)
		{
			return path.Contains('[') ? DataBindingUtility.s_ReplaceIndices.Replace(path, "[" + newText + "]") : path;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		public static bool TryGetLastUIBindingResult(in BindingId bindingId, VisualElement element, out BindingResult result)
		{
			result = default(BindingResult);
			DataBindingManager.BindingData activeBindingData = DataBindingUtility.GetActiveBindingData(in bindingId, element);
			bool flag = ((activeBindingData != null) ? activeBindingData.binding : null) == null || element.elementPanel == null;
			return !flag && element.elementPanel.dataBindingManager.TryGetLastUIBindingResult(activeBindingData, out result);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		public static bool TryGetLastSourceBindingResult(in BindingId bindingId, VisualElement element, out BindingResult result)
		{
			result = default(BindingResult);
			DataBindingManager.BindingData activeBindingData = DataBindingUtility.GetActiveBindingData(in bindingId, element);
			bool flag = ((activeBindingData != null) ? activeBindingData.binding : null) == null;
			return !flag && element.elementPanel.dataBindingManager.TryGetLastSourceBindingResult(activeBindingData, out result);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AC44 File Offset: 0x00008E44
		public static void GetMatchingConverterGroups(Type sourceType, Type destinationType, List<string> result)
		{
			List<ConverterGroup> list;
			using (CollectionPool<List<ConverterGroup>, ConverterGroup>.Get(out list))
			{
				ConverterGroups.GetAllConverterGroups(list);
				foreach (ConverterGroup converterGroup in list)
				{
					Delegate @delegate;
					bool flag = converterGroup.registry.TryGetConverter(sourceType, destinationType, out @delegate);
					if (flag)
					{
						result.Add(converterGroup.id);
					}
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		public static void GetMatchingConverterGroupsFromType(Type sourceType, List<string> result)
		{
			List<ConverterGroup> list;
			using (CollectionPool<List<ConverterGroup>, ConverterGroup>.Get(out list))
			{
				List<Type> list2;
				using (CollectionPool<List<Type>, Type>.Get(out list2))
				{
					ConverterGroups.GetAllConverterGroups(list);
					foreach (ConverterGroup converterGroup in list)
					{
						converterGroup.registry.GetAllTypesConvertingFromType(sourceType, list2);
						bool flag = list2.Count > 0;
						if (flag)
						{
							result.Add(converterGroup.id);
						}
						list2.Clear();
					}
				}
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000ADC4 File Offset: 0x00008FC4
		public static void GetMatchingConverterGroupsToType(Type destinationType, List<string> result)
		{
			List<ConverterGroup> list;
			using (CollectionPool<List<ConverterGroup>, ConverterGroup>.Get(out list))
			{
				List<Type> list2;
				using (CollectionPool<List<Type>, Type>.Get(out list2))
				{
					ConverterGroups.GetAllConverterGroups(list);
					foreach (ConverterGroup converterGroup in list)
					{
						converterGroup.registry.GetAllTypesConvertingToType(destinationType, list2);
						bool flag = list2.Count > 0;
						if (flag)
						{
							result.Add(converterGroup.id);
						}
						list2.Clear();
					}
				}
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000AEA0 File Offset: 0x000090A0
		public static void GetAllConversionsFromSourceToUI(DataBinding binding, Type destinationType, List<Type> result)
		{
			result.Add(destinationType);
			if (binding != null)
			{
				binding.sourceToUiConverters.registry.GetAllTypesConvertingToType(destinationType, result);
			}
			ConverterGroups.globalConverters.registry.GetAllTypesConvertingToType(destinationType, result);
			ConverterGroups.primitivesConverters.registry.GetAllTypesConvertingToType(destinationType, result);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000AEFC File Offset: 0x000090FC
		public static void GetAllConversionsFromUIToSource(DataBinding binding, Type sourceType, List<Type> result)
		{
			result.Add(sourceType);
			if (binding != null)
			{
				binding.uiToSourceConverters.registry.GetAllTypesConvertingToType(sourceType, result);
			}
			ConverterGroups.globalConverters.registry.GetAllTypesConvertingToType(sourceType, result);
			ConverterGroups.primitivesConverters.registry.GetAllTypesConvertingToType(sourceType, result);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000AF58 File Offset: 0x00009158
		public static BindingTypeResult IsPathValid(object dataSource, string path)
		{
			return DataBindingUtility.IsPathValid(dataSource, (dataSource != null) ? dataSource.GetType() : null, path);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000AF80 File Offset: 0x00009180
		public static BindingTypeResult IsPathValid(Type type, string path)
		{
			return DataBindingUtility.IsPathValid(null, type, path);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000AF9C File Offset: 0x0000919C
		private static BindingTypeResult IsPathValid(object dataSource, Type type, string path)
		{
			bool flag = type == null;
			BindingTypeResult bindingTypeResult;
			if (flag)
			{
				VisitReturnCode visitReturnCode = VisitReturnCode.NullContainer;
				int num = 0;
				PropertyPath propertyPath = default(PropertyPath);
				bindingTypeResult = new BindingTypeResult(visitReturnCode, num, in propertyPath);
			}
			else
			{
				TypePathVisitor typePathVisitor = DataBindingUtility.k_TypeVisitors.Get();
				IPropertyBag propertyBag = PropertyBag.GetPropertyBag(type);
				BindingTypeResult bindingTypeResult2;
				try
				{
					typePathVisitor.Path = new PropertyPath(path);
					bool flag2 = propertyBag == null;
					if (flag2)
					{
						typePathVisitor.ReturnCode = VisitReturnCode.MissingPropertyBag;
					}
					bool flag3 = dataSource == null;
					if (flag3)
					{
						if (propertyBag != null)
						{
							propertyBag.Accept(typePathVisitor);
						}
					}
					else if (propertyBag != null)
					{
						propertyBag.Accept(typePathVisitor, ref dataSource);
					}
					bool flag4 = typePathVisitor.ReturnCode == VisitReturnCode.Ok;
					if (flag4)
					{
						Type resolvedType = typePathVisitor.resolvedType;
						PropertyPath propertyPath = typePathVisitor.Path;
						bindingTypeResult2 = new BindingTypeResult(resolvedType, in propertyPath);
					}
					else
					{
						PropertyPath propertyPath = typePathVisitor.Path;
						PropertyPath propertyPath2 = PropertyPath.SubPath(in propertyPath, 0, typePathVisitor.PathIndex);
						bindingTypeResult2 = new BindingTypeResult(typePathVisitor.ReturnCode, typePathVisitor.PathIndex, in propertyPath2);
					}
				}
				finally
				{
					DataBindingUtility.k_TypeVisitors.Release(typePathVisitor);
				}
				bindingTypeResult = bindingTypeResult2;
			}
			return bindingTypeResult;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B0AC File Offset: 0x000092AC
		public static void GetPropertyPaths(object dataSource, int depth, List<PropertyPathInfo> listResult)
		{
			DataBindingUtility.GetPropertyPaths(dataSource, (dataSource != null) ? dataSource.GetType() : null, depth, listResult);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public static void GetPropertyPaths(Type type, int depth, List<PropertyPathInfo> listResult)
		{
			DataBindingUtility.GetPropertyPaths(null, type, depth, listResult);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000B0D4 File Offset: 0x000092D4
		private static void GetPropertyPaths(object dataSource, Type type, int depth, List<PropertyPathInfo> resultList)
		{
			bool flag = type == null;
			if (!flag)
			{
				IPropertyBag propertyBag = PropertyBag.GetPropertyBag(type);
				bool flag2 = propertyBag == null;
				if (!flag2)
				{
					AutoCompletePathVisitor autoCompletePathVisitor = DataBindingUtility.k_AutoCompleteVisitors.Get();
					try
					{
						autoCompletePathVisitor.propertyPathList = resultList;
						autoCompletePathVisitor.maxDepth = depth;
						bool flag3 = dataSource == null;
						if (flag3)
						{
							propertyBag.Accept(autoCompletePathVisitor);
						}
						else
						{
							propertyBag.Accept(autoCompletePathVisitor, ref dataSource);
						}
					}
					finally
					{
						DataBindingUtility.k_AutoCompleteVisitors.Release(autoCompletePathVisitor);
					}
				}
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B15C File Offset: 0x0000935C
		private static DataBindingManager.BindingData GetActiveBindingData(in BindingId bindingId, VisualElement element)
		{
			DataBindingManager.BindingData bindingData;
			bool flag = element.elementPanel != null && element.elementPanel.dataBindingManager.TryGetBindingData(element, in bindingId, out bindingData);
			DataBindingManager.BindingData bindingData2;
			if (flag)
			{
				bindingData2 = bindingData;
			}
			else
			{
				bindingData2 = null;
			}
			return bindingData2;
		}

		// Token: 0x04000162 RID: 354
		private static readonly ObjectPool<TypePathVisitor> k_TypeVisitors = new ObjectPool<TypePathVisitor>(() => new TypePathVisitor(), delegate(TypePathVisitor v)
		{
			v.Reset();
		}, null, null, true, 1, 10000);

		// Token: 0x04000163 RID: 355
		private static readonly ObjectPool<AutoCompletePathVisitor> k_AutoCompleteVisitors = new ObjectPool<AutoCompletePathVisitor>(() => new AutoCompletePathVisitor(), delegate(AutoCompletePathVisitor v)
		{
			v.Reset();
		}, null, null, true, 1, 10000);

		// Token: 0x04000164 RID: 356
		private static readonly Regex s_ReplaceIndices = new Regex("\\[[0-9]+\\]", RegexOptions.Compiled);
	}
}
