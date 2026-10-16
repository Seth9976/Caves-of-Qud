using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020005D5 RID: 1493
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class VisualTreeStyleUpdaterTraversal : HierarchyTraversal
	{
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x0009EE76 File Offset: 0x0009D076
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x0009EE7E File Offset: 0x0009D07E
		private float currentPixelsPerPoint { get; set; } = 1f;

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x0009EE87 File Offset: 0x0009D087
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x0009EE8F File Offset: 0x0009D08F
		private BaseVisualElementPanel currentPanel { get; set; }

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x0009EE98 File Offset: 0x0009D098
		public StyleMatchingContext styleMatchingContext
		{
			get
			{
				return this.m_StyleMatchingContext;
			}
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x0009EEA0 File Offset: 0x0009D0A0
		public void PrepareTraversal(BaseVisualElementPanel panel, float pixelsPerPoint)
		{
			this.currentPanel = panel;
			this.currentPixelsPerPoint = pixelsPerPoint;
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x0009EEB4 File Offset: 0x0009D0B4
		public void AddChangedElement(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_UpdateList.Add(ve);
			bool flag = (versionChangeType & VersionChangeType.StyleSheet) == VersionChangeType.StyleSheet;
			if (flag)
			{
				this.PropagateToChildren(ve);
			}
			this.PropagateToParents(ve);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x0009EEEB File Offset: 0x0009D0EB
		public void Clear()
		{
			this.m_UpdateList.Clear();
			this.m_ParentList.Clear();
			this.m_TempMatchResults.Clear();
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x0009EF14 File Offset: 0x0009D114
		private void PropagateToChildren(VisualElement ve)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = this.m_UpdateList.Add(visualElement);
				bool flag2 = flag;
				if (flag2)
				{
					this.PropagateToChildren(visualElement);
				}
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0009EF74 File Offset: 0x0009D174
		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement visualElement = ve.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(visualElement);
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x0009EFC2 File Offset: 0x0009D1C2
		private static void OnProcessMatchResult(VisualElement current, MatchResultInfo info)
		{
			current.triggerPseudoMask |= info.triggerPseudoMask;
			current.dependencyPseudoMask |= info.dependencyPseudoMask;
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x0009EFEC File Offset: 0x0009D1EC
		public override void TraverseRecursive(VisualElement element, int depth)
		{
			bool flag = this.ShouldSkipElement(element);
			if (!flag)
			{
				bool flag2 = this.m_UpdateList.Contains(element);
				bool flag3 = flag2;
				if (flag3)
				{
					element.triggerPseudoMask = (PseudoStates)0;
					element.dependencyPseudoMask = (PseudoStates)0;
				}
				int styleSheetCount = this.m_StyleMatchingContext.styleSheetCount;
				bool flag4 = element.styleSheetList != null;
				if (flag4)
				{
					for (int i = 0; i < element.styleSheetList.Count; i++)
					{
						StyleSheet styleSheet = element.styleSheetList[i];
						bool flag5 = styleSheet.flattenedRecursiveImports != null;
						if (flag5)
						{
							for (int j = 0; j < styleSheet.flattenedRecursiveImports.Count; j++)
							{
								this.m_StyleMatchingContext.AddStyleSheet(styleSheet.flattenedRecursiveImports[j]);
							}
						}
						this.m_StyleMatchingContext.AddStyleSheet(styleSheet);
					}
				}
				StyleVariableContext variableContext = this.m_StyleMatchingContext.variableContext;
				int customPropertiesCount = element.computedStyle.customPropertiesCount;
				bool flag6 = flag2;
				if (flag6)
				{
					this.m_StyleMatchingContext.currentElement = element;
					StyleSelectorHelper.FindMatches(this.m_StyleMatchingContext, this.m_TempMatchResults, styleSheetCount - 1);
					ComputedStyle computedStyle = this.ProcessMatchedRules(element, this.m_TempMatchResults);
					computedStyle.Acquire();
					bool hasInlineStyle = element.hasInlineStyle;
					if (hasInlineStyle)
					{
						element.inlineStyleAccess.ApplyInlineStyles(ref computedStyle);
					}
					ComputedTransitionUtils.UpdateComputedTransitions(ref computedStyle);
					bool flag7 = element.hasRunningAnimations && !ComputedTransitionUtils.SameTransitionProperty(element.computedStyle, ref computedStyle);
					if (flag7)
					{
						this.CancelAnimationsWithNoTransitionProperty(element, ref computedStyle);
					}
					bool flag8 = computedStyle.hasTransition && element.styleInitialized;
					if (flag8)
					{
						this.ProcessTransitions(element, element.computedStyle, ref computedStyle);
						element.SetComputedStyle(ref computedStyle);
						this.ForceUpdateTransitions(element);
					}
					else
					{
						element.SetComputedStyle(ref computedStyle);
					}
					computedStyle.Release();
					element.styleInitialized = true;
					element.inheritedStylesHash = element.computedStyle.inheritedData.GetHashCode();
					this.m_StyleMatchingContext.currentElement = null;
					this.m_TempMatchResults.Clear();
				}
				else
				{
					this.m_StyleMatchingContext.variableContext = element.variableContext;
				}
				bool flag9 = flag2 && (customPropertiesCount > 0 || element.computedStyle.customPropertiesCount > 0) && element.HasSelfEventInterests(EventBase<CustomStyleResolvedEvent>.EventCategory);
				if (flag9)
				{
					using (CustomStyleResolvedEvent pooled = EventBase<CustomStyleResolvedEvent>.GetPooled())
					{
						pooled.elementTarget = element;
						EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(pooled, this.currentPanel, element);
					}
				}
				this.m_StyleMatchingContext.ancestorFilter.PushElement(element);
				base.Recurse(element, depth);
				this.m_StyleMatchingContext.ancestorFilter.PopElement();
				this.m_StyleMatchingContext.variableContext = variableContext;
				bool flag10 = this.m_StyleMatchingContext.styleSheetCount > styleSheetCount;
				if (flag10)
				{
					this.m_StyleMatchingContext.RemoveStyleSheetRange(styleSheetCount, this.m_StyleMatchingContext.styleSheetCount - styleSheetCount);
				}
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0009F304 File Offset: 0x0009D504
		private void ProcessTransitions(VisualElement element, ref ComputedStyle oldStyle, ref ComputedStyle newStyle)
		{
			for (int i = newStyle.computedTransitions.Length - 1; i >= 0; i--)
			{
				ComputedTransitionProperty computedTransitionProperty = newStyle.computedTransitions[i];
				bool flag = element.hasInlineStyle && element.inlineStyleAccess.IsValueSet(computedTransitionProperty.id);
				if (!flag)
				{
					ComputedStyle.StartAnimation(element, computedTransitionProperty.id, ref oldStyle, ref newStyle, computedTransitionProperty.durationMs, computedTransitionProperty.delayMs, computedTransitionProperty.easingCurve);
				}
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x0009F380 File Offset: 0x0009D580
		private void ForceUpdateTransitions(VisualElement element)
		{
			element.styleAnimation.GetAllAnimations(this.m_AnimatedProperties);
			bool flag = this.m_AnimatedProperties.Count > 0;
			if (flag)
			{
				foreach (StylePropertyId stylePropertyId in this.m_AnimatedProperties)
				{
					element.styleAnimation.UpdateAnimation(stylePropertyId);
				}
				this.m_AnimatedProperties.Clear();
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x0009F410 File Offset: 0x0009D610
		internal void CancelAnimationsWithNoTransitionProperty(VisualElement element, ref ComputedStyle newStyle)
		{
			element.styleAnimation.GetAllAnimations(this.m_AnimatedProperties);
			foreach (StylePropertyId stylePropertyId in this.m_AnimatedProperties)
			{
				bool flag = !(ref newStyle).HasTransitionProperty(stylePropertyId);
				if (flag)
				{
					element.styleAnimation.CancelAnimation(stylePropertyId);
				}
			}
			this.m_AnimatedProperties.Clear();
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0009F49C File Offset: 0x0009D69C
		protected bool ShouldSkipElement(VisualElement element)
		{
			return !this.m_ParentList.Contains(element) && !this.m_UpdateList.Contains(element);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x0009F4D0 File Offset: 0x0009D6D0
		private ComputedStyle ProcessMatchedRules(VisualElement element, List<SelectorMatchRecord> matchingSelectors)
		{
			matchingSelectors.Sort((SelectorMatchRecord a, SelectorMatchRecord b) => SelectorMatchRecord.Compare(a, b));
			long num = (long)element.fullTypeName.GetHashCode();
			num = (num * 397L) ^ (long)this.currentPixelsPerPoint.GetHashCode();
			int variableHash = this.m_StyleMatchingContext.variableContext.GetVariableHash();
			int num2 = 0;
			foreach (SelectorMatchRecord selectorMatchRecord in matchingSelectors)
			{
				num2 += selectorMatchRecord.complexSelector.rule.customPropertiesCount;
			}
			bool flag = num2 > 0;
			if (flag)
			{
				this.m_ProcessVarContext.AddInitialRange(this.m_StyleMatchingContext.variableContext);
			}
			foreach (SelectorMatchRecord selectorMatchRecord2 in matchingSelectors)
			{
				StyleSheet sheet = selectorMatchRecord2.sheet;
				int ruleIndex = selectorMatchRecord2.complexSelector.ruleIndex;
				int specificity = selectorMatchRecord2.complexSelector.specificity;
				num = (num * 397L) ^ (long)sheet.contentHash;
				num = (num * 397L) ^ (long)ruleIndex;
				num = (num * 397L) ^ (long)specificity;
				StyleRule rule = selectorMatchRecord2.complexSelector.rule;
				bool flag2 = rule.customPropertiesCount > 0;
				if (flag2)
				{
					this.ProcessMatchedVariables(selectorMatchRecord2.sheet, rule);
				}
			}
			VisualElement parent = element.hierarchy.parent;
			int num3 = ((parent != null) ? parent.inheritedStylesHash : 0);
			num = (num * 397L) ^ (long)num3;
			int num4 = variableHash;
			bool flag3 = num2 > 0;
			if (flag3)
			{
				num4 = this.m_ProcessVarContext.GetVariableHash();
			}
			num = (num * 397L) ^ (long)num4;
			bool flag4 = variableHash != num4;
			if (flag4)
			{
				StyleVariableContext styleVariableContext;
				bool flag5 = !StyleCache.TryGetValue(num4, out styleVariableContext);
				if (flag5)
				{
					styleVariableContext = new StyleVariableContext(this.m_ProcessVarContext);
					StyleCache.SetValue(num4, styleVariableContext);
				}
				this.m_StyleMatchingContext.variableContext = styleVariableContext;
			}
			element.variableContext = this.m_StyleMatchingContext.variableContext;
			this.m_ProcessVarContext.Clear();
			ComputedStyle computedStyle;
			bool flag6 = !StyleCache.TryGetValue(num, out computedStyle);
			if (flag6)
			{
				ref ComputedStyle ptr;
				if (parent != null)
				{
					ref ComputedStyle computedStyle2 = ref parent.computedStyle;
					ptr = parent.computedStyle;
				}
				else
				{
					ptr = InitialStyle.Get();
				}
				ref ComputedStyle ptr2 = ref ptr;
				computedStyle = ComputedStyle.Create(ref ptr2);
				computedStyle.matchingRulesHash = num;
				float scaledPixelsPerPoint = element.scaledPixelsPerPoint;
				foreach (SelectorMatchRecord selectorMatchRecord3 in matchingSelectors)
				{
					this.m_StylePropertyReader.SetContext(selectorMatchRecord3.sheet, selectorMatchRecord3.complexSelector, this.m_StyleMatchingContext.variableContext, scaledPixelsPerPoint);
					computedStyle.ApplyProperties(this.m_StylePropertyReader, ref ptr2);
				}
				computedStyle.FinalizeApply(ref ptr2);
				StyleCache.SetValue(num, ref computedStyle);
			}
			return computedStyle;
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0009F80C File Offset: 0x0009DA0C
		private void ProcessMatchedVariables(StyleSheet sheet, StyleRule rule)
		{
			foreach (StyleProperty styleProperty in rule.properties)
			{
				bool isCustomProperty = styleProperty.isCustomProperty;
				if (isCustomProperty)
				{
					StyleVariable styleVariable = new StyleVariable(styleProperty.name, sheet, styleProperty.values);
					this.m_ProcessVarContext.Add(styleVariable);
				}
			}
		}

		// Token: 0x04001424 RID: 5156
		private StyleVariableContext m_ProcessVarContext = new StyleVariableContext();

		// Token: 0x04001425 RID: 5157
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		// Token: 0x04001426 RID: 5158
		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		// Token: 0x04001427 RID: 5159
		private List<SelectorMatchRecord> m_TempMatchResults = new List<SelectorMatchRecord>();

		// Token: 0x04001429 RID: 5161
		private StyleMatchingContext m_StyleMatchingContext = new StyleMatchingContext(new Action<VisualElement, MatchResultInfo>(VisualTreeStyleUpdaterTraversal.OnProcessMatchResult));

		// Token: 0x0400142A RID: 5162
		private StylePropertyReader m_StylePropertyReader = new StylePropertyReader();

		// Token: 0x0400142C RID: 5164
		private readonly List<StylePropertyId> m_AnimatedProperties = new List<StylePropertyId>();
	}
}
