using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002D6 RID: 726
	public sealed class ScopeDebugInformation : DebugInformation
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600127B RID: 4731 RVA: 0x0003C696 File Offset: 0x0003A896
		// (set) Token: 0x0600127C RID: 4732 RVA: 0x0003C69E File Offset: 0x0003A89E
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

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x0003C6A7 File Offset: 0x0003A8A7
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x0003C6AF File Offset: 0x0003A8AF
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

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0003C6B8 File Offset: 0x0003A8B8
		// (set) Token: 0x06001280 RID: 4736 RVA: 0x0003C6C0 File Offset: 0x0003A8C0
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

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x0003C6C9 File Offset: 0x0003A8C9
		public bool HasScopes
		{
			get
			{
				return !this.scopes.IsNullOrEmpty<ScopeDebugInformation>();
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0003C6D9 File Offset: 0x0003A8D9
		public Collection<ScopeDebugInformation> Scopes
		{
			get
			{
				if (this.scopes == null)
				{
					Interlocked.CompareExchange<Collection<ScopeDebugInformation>>(ref this.scopes, new Collection<ScopeDebugInformation>(), null);
				}
				return this.scopes;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x0003C6FB File Offset: 0x0003A8FB
		public bool HasVariables
		{
			get
			{
				return !this.variables.IsNullOrEmpty<VariableDebugInformation>();
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x0003C70B File Offset: 0x0003A90B
		public Collection<VariableDebugInformation> Variables
		{
			get
			{
				if (this.variables == null)
				{
					Interlocked.CompareExchange<Collection<VariableDebugInformation>>(ref this.variables, new Collection<VariableDebugInformation>(), null);
				}
				return this.variables;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0003C72D File Offset: 0x0003A92D
		public bool HasConstants
		{
			get
			{
				return !this.constants.IsNullOrEmpty<ConstantDebugInformation>();
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x0003C73D File Offset: 0x0003A93D
		public Collection<ConstantDebugInformation> Constants
		{
			get
			{
				if (this.constants == null)
				{
					Interlocked.CompareExchange<Collection<ConstantDebugInformation>>(ref this.constants, new Collection<ConstantDebugInformation>(), null);
				}
				return this.constants;
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0003C75F File Offset: 0x0003A95F
		internal ScopeDebugInformation()
		{
			this.token = new MetadataToken(TokenType.LocalScope);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0003C777 File Offset: 0x0003A977
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

		// Token: 0x06001289 RID: 4745 RVA: 0x0003C7A8 File Offset: 0x0003A9A8
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

		// Token: 0x0400095C RID: 2396
		internal InstructionOffset start;

		// Token: 0x0400095D RID: 2397
		internal InstructionOffset end;

		// Token: 0x0400095E RID: 2398
		internal ImportDebugInformation import;

		// Token: 0x0400095F RID: 2399
		internal Collection<ScopeDebugInformation> scopes;

		// Token: 0x04000960 RID: 2400
		internal Collection<VariableDebugInformation> variables;

		// Token: 0x04000961 RID: 2401
		internal Collection<ConstantDebugInformation> constants;
	}
}
