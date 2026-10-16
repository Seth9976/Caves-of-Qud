using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements
{
	// Token: 0x020004DD RID: 1245
	internal class StyleVariableResolver
	{
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00089C3D File Offset: 0x00087E3D
		private StyleSheet currentSheet
		{
			get
			{
				return this.m_CurrentContext.sheet;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x00089C4A File Offset: 0x00087E4A
		private StyleValueHandle[] currentHandles
		{
			get
			{
				return this.m_CurrentContext.handles;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00089C57 File Offset: 0x00087E57
		public List<StylePropertyValue> resolvedValues
		{
			get
			{
				return this.m_ResolvedValues;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x00089C5F File Offset: 0x00087E5F
		// (set) Token: 0x060025FF RID: 9727 RVA: 0x00089C67 File Offset: 0x00087E67
		public StyleVariableContext variableContext { get; set; }

		// Token: 0x06002600 RID: 9728 RVA: 0x00089C70 File Offset: 0x00087E70
		public void Init(StyleProperty property, StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_ResolvedValues.Clear();
			this.m_ContextStack.Clear();
			this.m_Property = property;
			this.PushContext(sheet, handles);
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00089C9C File Offset: 0x00087E9C
		private void PushContext(StyleSheet sheet, StyleValueHandle[] handles)
		{
			this.m_CurrentContext = new StyleVariableResolver.ResolveContext
			{
				sheet = sheet,
				handles = handles
			};
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x00089CDB File Offset: 0x00087EDB
		private void PopContext()
		{
			this.m_ContextStack.Pop();
			this.m_CurrentContext = this.m_ContextStack.Peek();
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x00089CFC File Offset: 0x00087EFC
		public void AddValue(StyleValueHandle handle)
		{
			this.m_ResolvedValues.Add(new StylePropertyValue
			{
				sheet = this.currentSheet,
				handle = handle
			});
		}

		// Token: 0x06002604 RID: 9732 RVA: 0x00089D34 File Offset: 0x00087F34
		public bool ResolveVarFunction(ref int index)
		{
			this.m_ResolvedVarStack.Clear();
			int num;
			string text;
			StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out num, out text);
			StyleVariableResolver.Result result = this.ResolveVarFunction(ref index, num, text);
			return result == StyleVariableResolver.Result.Valid;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00089D78 File Offset: 0x00087F78
		private StyleVariableResolver.Result ResolveVarFunction(ref int index, int argc, string varName)
		{
			StyleVariableResolver.Result result = this.ResolveVariable(varName);
			bool flag = argc > 1;
			if (flag)
			{
				StyleValueHandle[] currentHandles = this.currentHandles;
				int num = index + 1;
				index = num;
				StyleValueHandle styleValueHandle = currentHandles[num];
				Debug.Assert(styleValueHandle.valueType == StyleValueType.CommaSeparator, string.Format("Unexpected value type {0} in var() fallback; expected CommaSeparator.", styleValueHandle.valueType));
				bool flag2 = styleValueHandle.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
				if (flag2)
				{
					index++;
					result = this.ResolveFallback(ref index, result == StyleVariableResolver.Result.NotFound);
				}
			}
			return result;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00089E14 File Offset: 0x00088014
		public bool ValidateResolvedValues()
		{
			bool isCustomProperty = this.m_Property.isCustomProperty;
			bool flag;
			if (isCustomProperty)
			{
				flag = true;
			}
			else
			{
				string text;
				bool flag2 = !StylePropertyCache.TryGetSyntax(this.m_Property.name, out text);
				if (flag2)
				{
					Debug.LogAssertion("Unknown style property " + this.m_Property.name);
					flag = false;
				}
				else
				{
					Expression expression = StyleVariableResolver.s_SyntaxParser.Parse(text);
					flag = this.m_Matcher.Match(expression, this.m_ResolvedValues).success;
				}
			}
			return flag;
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00089EA0 File Offset: 0x000880A0
		private StyleVariableResolver.Result ResolveVariable(string variableName)
		{
			StyleVariable styleVariable;
			bool flag = !this.variableContext.TryFindVariable(variableName, out styleVariable);
			StyleVariableResolver.Result result;
			if (flag)
			{
				result = StyleVariableResolver.Result.NotFound;
			}
			else
			{
				bool flag2 = this.m_ResolvedVarStack.Contains(styleVariable.name);
				if (flag2)
				{
					result = StyleVariableResolver.Result.NotFound;
				}
				else
				{
					this.m_ResolvedVarStack.Push(styleVariable.name);
					StyleVariableResolver.Result result2 = StyleVariableResolver.Result.Valid;
					int num = 0;
					while (num < styleVariable.handles.Length && result2 == StyleVariableResolver.Result.Valid)
					{
						bool flag3 = this.m_ResolvedValues.Count + 1 > 100;
						if (flag3)
						{
							return StyleVariableResolver.Result.Invalid;
						}
						StyleValueHandle styleValueHandle = styleVariable.handles[num];
						bool flag4 = styleValueHandle.IsVarFunction();
						if (flag4)
						{
							this.PushContext(styleVariable.sheet, styleVariable.handles);
							int num2;
							string text;
							StyleVariableResolver.ParseVarFunction(styleVariable.sheet, styleVariable.handles, ref num, out num2, out text);
							result2 = this.ResolveVarFunction(ref num, num2, text);
							this.PopContext();
						}
						else
						{
							this.m_ResolvedValues.Add(new StylePropertyValue
							{
								sheet = styleVariable.sheet,
								handle = styleValueHandle
							});
						}
						num++;
					}
					this.m_ResolvedVarStack.Pop();
					result = result2;
				}
			}
			return result;
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00089FE8 File Offset: 0x000881E8
		private StyleVariableResolver.Result ResolveFallback(ref int index, bool appendValues)
		{
			StyleVariableResolver.Result result = StyleVariableResolver.Result.Valid;
			while (index < this.currentHandles.Length && result == StyleVariableResolver.Result.Valid)
			{
				StyleValueHandle styleValueHandle = this.currentHandles[index];
				bool flag = styleValueHandle.IsVarFunction();
				if (flag)
				{
					int num;
					string text;
					StyleVariableResolver.ParseVarFunction(this.currentSheet, this.currentHandles, ref index, out num, out text);
					if (appendValues)
					{
						StyleVariableResolver.Result result2 = this.ResolveVarFunction(ref index, num, text);
						bool flag2 = result2 > StyleVariableResolver.Result.Valid;
						if (flag2)
						{
							result = result2;
						}
					}
					else
					{
						bool flag3 = num > 1;
						if (flag3)
						{
							StyleValueHandle[] currentHandles = this.currentHandles;
							int num2 = index + 1;
							index = num2;
							StyleValueHandle styleValueHandle2 = currentHandles[num2];
							Debug.Assert(styleValueHandle2.valueType == StyleValueType.CommaSeparator);
							bool flag4 = styleValueHandle2.valueType == StyleValueType.CommaSeparator && index + 1 < this.currentHandles.Length;
							if (flag4)
							{
								index++;
								this.ResolveFallback(ref index, false);
							}
						}
					}
				}
				else if (appendValues)
				{
					this.m_ResolvedValues.Add(new StylePropertyValue
					{
						sheet = this.currentSheet,
						handle = styleValueHandle
					});
				}
				index++;
			}
			return result;
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0008A124 File Offset: 0x00088324
		private static void ParseVarFunction(StyleSheet sheet, StyleValueHandle[] handles, ref int index, out int argCount, out string variableName)
		{
			int num = index + 1;
			index = num;
			argCount = (int)sheet.ReadFloat(handles[num]);
			num = index + 1;
			index = num;
			variableName = sheet.ReadVariable(handles[num]);
		}

		// Token: 0x0400111F RID: 4383
		internal const int kMaxResolves = 100;

		// Token: 0x04001120 RID: 4384
		private static StyleSyntaxParser s_SyntaxParser = new StyleSyntaxParser();

		// Token: 0x04001121 RID: 4385
		private StylePropertyValueMatcher m_Matcher = new StylePropertyValueMatcher();

		// Token: 0x04001122 RID: 4386
		private List<StylePropertyValue> m_ResolvedValues = new List<StylePropertyValue>();

		// Token: 0x04001123 RID: 4387
		private Stack<string> m_ResolvedVarStack = new Stack<string>();

		// Token: 0x04001124 RID: 4388
		private StyleProperty m_Property;

		// Token: 0x04001125 RID: 4389
		private Stack<StyleVariableResolver.ResolveContext> m_ContextStack = new Stack<StyleVariableResolver.ResolveContext>();

		// Token: 0x04001126 RID: 4390
		private StyleVariableResolver.ResolveContext m_CurrentContext;

		// Token: 0x020004DE RID: 1246
		private enum Result
		{
			// Token: 0x04001129 RID: 4393
			Valid,
			// Token: 0x0400112A RID: 4394
			Invalid,
			// Token: 0x0400112B RID: 4395
			NotFound
		}

		// Token: 0x020004DF RID: 1247
		private struct ResolveContext
		{
			// Token: 0x0400112C RID: 4396
			public StyleSheet sheet;

			// Token: 0x0400112D RID: 4397
			public StyleValueHandle[] handles;
		}
	}
}
