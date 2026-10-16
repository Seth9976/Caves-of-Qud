using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002BE RID: 702
	internal sealed class VariableDefinitionCollection : Collection<VariableDefinition>
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x0003A0AE File Offset: 0x000382AE
		internal VariableDefinitionCollection(MethodDefinition method)
		{
			this.method = method;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003A0BD File Offset: 0x000382BD
		internal VariableDefinitionCollection(MethodDefinition method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0003A0CD File Offset: 0x000382CD
		protected override void OnAdd(VariableDefinition item, int index)
		{
			item.index = index;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0003A0D6 File Offset: 0x000382D6
		protected override void OnInsert(VariableDefinition item, int index)
		{
			item.index = index;
			this.UpdateVariableIndices(index, 1, null);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0003A0CD File Offset: 0x000382CD
		protected override void OnSet(VariableDefinition item, int index)
		{
			item.index = index;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0003A0E8 File Offset: 0x000382E8
		protected override void OnRemove(VariableDefinition item, int index)
		{
			this.UpdateVariableIndices(index + 1, -1, item);
			item.index = -1;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0003A0FC File Offset: 0x000382FC
		private void UpdateVariableIndices(int startIndex, int offset, VariableDefinition variableToRemove = null)
		{
			for (int i = startIndex; i < this.size; i++)
			{
				this.items[i].index = i + offset;
			}
			MethodDebugInformation methodDebugInformation = ((this.method == null) ? null : this.method.debug_info);
			if (methodDebugInformation == null || methodDebugInformation.Scope == null)
			{
				return;
			}
			foreach (ScopeDebugInformation scopeDebugInformation in methodDebugInformation.GetScopes())
			{
				if (scopeDebugInformation.HasVariables)
				{
					Collection<VariableDebugInformation> variables = scopeDebugInformation.Variables;
					int num = -1;
					for (int j = 0; j < variables.Count; j++)
					{
						VariableDebugInformation variableDebugInformation = variables[j];
						if (variableToRemove != null && ((variableDebugInformation.index.IsResolved && variableDebugInformation.index.ResolvedVariable == variableToRemove) || (!variableDebugInformation.index.IsResolved && variableDebugInformation.Index == variableToRemove.Index)))
						{
							num = j;
						}
						else if (!variableDebugInformation.index.IsResolved && variableDebugInformation.Index >= startIndex)
						{
							variableDebugInformation.index = new VariableIndex(variableDebugInformation.Index + offset);
						}
					}
					if (num >= 0)
					{
						variables.RemoveAt(num);
					}
				}
			}
		}

		// Token: 0x04000805 RID: 2053
		private readonly MethodDefinition method;
	}
}
