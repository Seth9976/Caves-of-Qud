using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x02000129 RID: 297
	public sealed class ScopeDebugInformation : DebugInformation
	{
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x000256A2 File Offset: 0x000238A2
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x000256AA File Offset: 0x000238AA
		public InstructionOffset Start
		{
			get
			{
				return this.start;
			}
			set
			{
				this.start = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x000256B3 File Offset: 0x000238B3
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x000256BB File Offset: 0x000238BB
		public InstructionOffset End
		{
			get
			{
				return this.end;
			}
			set
			{
				this.end = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x000256C4 File Offset: 0x000238C4
		// (set) Token: 0x06000B36 RID: 2870 RVA: 0x000256CC File Offset: 0x000238CC
		public ImportDebugInformation Import
		{
			get
			{
				return this.import;
			}
			set
			{
				this.import = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x000256D5 File Offset: 0x000238D5
		public bool HasScopes
		{
			get
			{
				return !this.scopes.IsNullOrEmpty<ScopeDebugInformation>();
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x000256E8 File Offset: 0x000238E8
		public Collection<ScopeDebugInformation> Scopes
		{
			get
			{
				Collection<ScopeDebugInformation> collection;
				if ((collection = this.scopes) == null)
				{
					collection = (this.scopes = new Collection<ScopeDebugInformation>());
				}
				return collection;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0002570D File Offset: 0x0002390D
		public bool HasVariables
		{
			get
			{
				return !this.variables.IsNullOrEmpty<VariableDebugInformation>();
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x00025720 File Offset: 0x00023920
		public Collection<VariableDebugInformation> Variables
		{
			get
			{
				Collection<VariableDebugInformation> collection;
				if ((collection = this.variables) == null)
				{
					collection = (this.variables = new Collection<VariableDebugInformation>());
				}
				return collection;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00025745 File Offset: 0x00023945
		public bool HasConstants
		{
			get
			{
				return !this.constants.IsNullOrEmpty<ConstantDebugInformation>();
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00025758 File Offset: 0x00023958
		public Collection<ConstantDebugInformation> Constants
		{
			get
			{
				Collection<ConstantDebugInformation> collection;
				if ((collection = this.constants) == null)
				{
					collection = (this.constants = new Collection<ConstantDebugInformation>());
				}
				return collection;
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002577D File Offset: 0x0002397D
		internal ScopeDebugInformation()
		{
			this.token = new MetadataToken(TokenType.LocalScope);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00025795 File Offset: 0x00023995
		public ScopeDebugInformation(Instruction start, Instruction end)
			: this()
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.start = new InstructionOffset(start);
			if (end != null)
			{
				this.end = new InstructionOffset(end);
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000257C8 File Offset: 0x000239C8
		public bool TryGetName(VariableDefinition variable, out string name)
		{
			name = null;
			if (this.variables == null || this.variables.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < this.variables.Count; i++)
			{
				if (this.variables[i].Index == variable.Index)
				{
					name = this.variables[i].Name;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400069F RID: 1695
		internal InstructionOffset start;

		// Token: 0x040006A0 RID: 1696
		internal InstructionOffset end;

		// Token: 0x040006A1 RID: 1697
		internal ImportDebugInformation import;

		// Token: 0x040006A2 RID: 1698
		internal Collection<ScopeDebugInformation> scopes;

		// Token: 0x040006A3 RID: 1699
		internal Collection<VariableDebugInformation> variables;

		// Token: 0x040006A4 RID: 1700
		internal Collection<ConstantDebugInformation> constants;
	}
}
