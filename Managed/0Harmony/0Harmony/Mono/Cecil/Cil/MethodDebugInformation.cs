using System;
using System.Collections.Generic;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002E9 RID: 745
	public sealed class MethodDebugInformation : DebugInformation
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0003CEAB File Offset: 0x0003B0AB
		public MethodDefinition Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x0003CEB3 File Offset: 0x0003B0B3
		public bool HasSequencePoints
		{
			get
			{
				return !this.sequence_points.IsNullOrEmpty<SequencePoint>();
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0003CEC3 File Offset: 0x0003B0C3
		public Collection<SequencePoint> SequencePoints
		{
			get
			{
				if (this.sequence_points == null)
				{
					Interlocked.CompareExchange<Collection<SequencePoint>>(ref this.sequence_points, new Collection<SequencePoint>(), null);
				}
				return this.sequence_points;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0003CEE5 File Offset: 0x0003B0E5
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x0003CEED File Offset: 0x0003B0ED
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

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0003CEF6 File Offset: 0x0003B0F6
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x0003CEFE File Offset: 0x0003B0FE
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

		// Token: 0x060012ED RID: 4845 RVA: 0x0003CF08 File Offset: 0x0003B108
		internal MethodDebugInformation(MethodDefinition method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.method = method;
			this.token = new MetadataToken(TokenType.MethodDebugInformation, method.MetadataToken.RID);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0003CF50 File Offset: 0x0003B150
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

		// Token: 0x060012EF RID: 4847 RVA: 0x0003CFA4 File Offset: 0x0003B1A4
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
				if (!dictionary2.ContainsKey(this.sequence_points[i].Offset))
				{
					dictionary2.Add(this.sequence_points[i].Offset, this.sequence_points[i]);
				}
			}
			Collection<Instruction> instructions = this.method.Body.Instructions;
			for (int j = 0; j < instructions.Count; j++)
			{
				SequencePoint sequencePoint;
				if (dictionary2.TryGetValue(instructions[j].Offset, out sequencePoint))
				{
					dictionary.Add(instructions[j], sequencePoint);
				}
			}
			return dictionary;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003D07F File Offset: 0x0003B27F
		public IEnumerable<ScopeDebugInformation> GetScopes()
		{
			if (this.scope == null)
			{
				return Empty<ScopeDebugInformation>.Array;
			}
			return MethodDebugInformation.GetScopes(new ScopeDebugInformation[] { this.scope });
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0003D0A3 File Offset: 0x0003B2A3
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
					scope = null;
				}
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0003D0B4 File Offset: 0x0003B2B4
		public bool TryGetName(VariableDefinition variable, out string name)
		{
			name = null;
			bool flag = false;
			string text = "";
			using (IEnumerator<ScopeDebugInformation> enumerator = this.GetScopes().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string text2;
					if (enumerator.Current.TryGetName(variable, out text2))
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
			}
			name = text;
			return flag;
		}

		// Token: 0x0400099D RID: 2461
		internal MethodDefinition method;

		// Token: 0x0400099E RID: 2462
		internal Collection<SequencePoint> sequence_points;

		// Token: 0x0400099F RID: 2463
		internal ScopeDebugInformation scope;

		// Token: 0x040009A0 RID: 2464
		internal MethodDefinition kickoff_method;

		// Token: 0x040009A1 RID: 2465
		internal int code_size;

		// Token: 0x040009A2 RID: 2466
		internal MetadataToken local_var_token;
	}
}
