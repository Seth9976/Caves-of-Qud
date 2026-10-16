using System;
using System.Collections.Generic;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200011E RID: 286
	public sealed class MethodDebugInformation : DebugInformation
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00023819 File Offset: 0x00021A19
		public MethodDefinition Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00023821 File Offset: 0x00021A21
		public bool HasSequencePoints
		{
			get
			{
				return !this.sequence_points.IsNullOrEmpty<SequencePoint>();
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00023834 File Offset: 0x00021A34
		public Collection<SequencePoint> SequencePoints
		{
			get
			{
				Collection<SequencePoint> collection;
				if ((collection = this.sequence_points) == null)
				{
					collection = (this.sequence_points = new Collection<SequencePoint>());
				}
				return collection;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00023859 File Offset: 0x00021A59
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00023861 File Offset: 0x00021A61
		public ScopeDebugInformation Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0002386A File Offset: 0x00021A6A
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00023872 File Offset: 0x00021A72
		public MethodDefinition StateMachineKickOffMethod
		{
			get
			{
				return this.kickoff_method;
			}
			set
			{
				this.kickoff_method = value;
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002387C File Offset: 0x00021A7C
		internal MethodDebugInformation(MethodDefinition method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.method = method;
			this.token = new MetadataToken(TokenType.MethodDebugInformation, method.MetadataToken.RID);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000238C4 File Offset: 0x00021AC4
		public SequencePoint GetSequencePoint(Instruction instruction)
		{
			if (!this.HasSequencePoints)
			{
				return null;
			}
			for (int i = 0; i < this.sequence_points.Count; i++)
			{
				if (this.sequence_points[i].Offset == instruction.Offset)
				{
					return this.sequence_points[i];
				}
			}
			return null;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x00023918 File Offset: 0x00021B18
		public IDictionary<Instruction, SequencePoint> GetSequencePointMapping()
		{
			Dictionary<Instruction, SequencePoint> dictionary = new Dictionary<Instruction, SequencePoint>();
			if (!this.HasSequencePoints || !this.method.HasBody)
			{
				return dictionary;
			}
			Dictionary<int, SequencePoint> dictionary2 = new Dictionary<int, SequencePoint>(this.sequence_points.Count);
			for (int i = 0; i < this.sequence_points.Count; i++)
			{
				dictionary2.Add(this.sequence_points[i].Offset, this.sequence_points[i]);
			}
			Collection<Instruction> instructions = this.method.Body.Instructions;
			for (int j = 0; j < instructions.Count; j++)
			{
				SequencePoint sequencePoint;
				if (dictionary2.TryGetValue(instructions[j].Offset, ref sequencePoint))
				{
					dictionary.Add(instructions[j], sequencePoint);
				}
			}
			return dictionary;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000239DA File Offset: 0x00021BDA
		public IEnumerable<ScopeDebugInformation> GetScopes()
		{
			if (this.scope == null)
			{
				return Empty<ScopeDebugInformation>.Array;
			}
			return MethodDebugInformation.GetScopes(new ScopeDebugInformation[] { this.scope });
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000239FE File Offset: 0x00021BFE
		private static IEnumerable<ScopeDebugInformation> GetScopes(IList<ScopeDebugInformation> scopes)
		{
			int num;
			for (int i = 0; i < scopes.Count; i = num + 1)
			{
				ScopeDebugInformation scope = scopes[i];
				yield return scope;
				if (scope.HasScopes)
				{
					foreach (ScopeDebugInformation scopeDebugInformation in MethodDebugInformation.GetScopes(scope.Scopes))
					{
						yield return scopeDebugInformation;
					}
					IEnumerator<ScopeDebugInformation> enumerator = null;
				}
				scope = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x00023A10 File Offset: 0x00021C10
		public bool TryGetName(VariableDefinition variable, out string name)
		{
			name = null;
			bool flag = false;
			string text = "";
			foreach (ScopeDebugInformation scopeDebugInformation in this.GetScopes())
			{
				string text2;
				if (scopeDebugInformation.TryGetName(variable, out text2))
				{
					if (!flag)
					{
						flag = true;
						text = text2;
					}
					else if (text != text2)
					{
						return false;
					}
				}
			}
			name = text;
			return flag;
		}

		// Token: 0x04000588 RID: 1416
		internal MethodDefinition method;

		// Token: 0x04000589 RID: 1417
		internal Collection<SequencePoint> sequence_points;

		// Token: 0x0400058A RID: 1418
		internal ScopeDebugInformation scope;

		// Token: 0x0400058B RID: 1419
		internal MethodDefinition kickoff_method;

		// Token: 0x0400058C RID: 1420
		internal int code_size;

		// Token: 0x0400058D RID: 1421
		internal MetadataToken local_var_token;
	}
}
