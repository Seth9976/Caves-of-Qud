using System;
using System.Collections.Generic;
using System.Threading;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil.Cil
{
	// Token: 0x0200011D RID: 285
	public sealed class MethodBody
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00023627 File Offset: 0x00021827
		public MethodDefinition Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0002362F File Offset: 0x0002182F
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x00023637 File Offset: 0x00021837
		public int MaxStackSize
		{
			get
			{
				return this.max_stack_size;
			}
			set
			{
				this.max_stack_size = value;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00023640 File Offset: 0x00021840
		public int CodeSize
		{
			get
			{
				return this.code_size;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x00023648 File Offset: 0x00021848
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x00023650 File Offset: 0x00021850
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
			set
			{
				this.init_locals = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00023659 File Offset: 0x00021859
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x00023661 File Offset: 0x00021861
		public MetadataToken LocalVarToken
		{
			get
			{
				return this.local_var_token;
			}
			set
			{
				this.local_var_token = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002366C File Offset: 0x0002186C
		public Collection<Instruction> Instructions
		{
			get
			{
				Collection<Instruction> collection;
				if ((collection = this.instructions) == null)
				{
					collection = (this.instructions = new InstructionCollection(this.method));
				}
				return collection;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00023697 File Offset: 0x00021897
		public bool HasExceptionHandlers
		{
			get
			{
				return !this.exceptions.IsNullOrEmpty<ExceptionHandler>();
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x000236A8 File Offset: 0x000218A8
		public Collection<ExceptionHandler> ExceptionHandlers
		{
			get
			{
				Collection<ExceptionHandler> collection;
				if ((collection = this.exceptions) == null)
				{
					collection = (this.exceptions = new Collection<ExceptionHandler>());
				}
				return collection;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x000236CD File Offset: 0x000218CD
		public bool HasVariables
		{
			get
			{
				return !this.variables.IsNullOrEmpty<VariableDefinition>();
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x000236E0 File Offset: 0x000218E0
		public Collection<VariableDefinition> Variables
		{
			get
			{
				Collection<VariableDefinition> collection;
				if ((collection = this.variables) == null)
				{
					collection = (this.variables = new VariableDefinitionCollection());
				}
				return collection;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00023705 File Offset: 0x00021905
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0002370D File Offset: 0x0002190D
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00023718 File Offset: 0x00021918
		public ParameterDefinition ThisParameter
		{
			get
			{
				if (this.method == null || this.method.DeclaringType == null)
				{
					throw new NotSupportedException();
				}
				if (!this.method.HasThis)
				{
					return null;
				}
				if (this.this_parameter == null)
				{
					Interlocked.CompareExchange<ParameterDefinition>(ref this.this_parameter, MethodBody.CreateThisParameter(this.method), null);
				}
				return this.this_parameter;
			}
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00023778 File Offset: 0x00021978
		private static ParameterDefinition CreateThisParameter(MethodDefinition method)
		{
			TypeReference typeReference = method.DeclaringType;
			if (typeReference.HasGenericParameters)
			{
				GenericInstanceType genericInstanceType = new GenericInstanceType(typeReference);
				for (int i = 0; i < typeReference.GenericParameters.Count; i++)
				{
					genericInstanceType.GenericArguments.Add(typeReference.GenericParameters[i]);
				}
				typeReference = genericInstanceType;
			}
			if (typeReference.IsValueType || typeReference.IsPrimitive)
			{
				typeReference = new ByReferenceType(typeReference);
			}
			return new ParameterDefinition(typeReference, method);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000237E8 File Offset: 0x000219E8
		public MethodBody(MethodDefinition method)
		{
			this.method = method;
			this.instructionTokens = new Dictionary<Instruction, MetadataToken>();
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00023802 File Offset: 0x00021A02
		public ILProcessor GetILProcessor()
		{
			return new ILProcessor(this);
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002380A File Offset: 0x00021A0A
		public bool GetInstructionToken(Instruction instruction, out MetadataToken token)
		{
			return this.instructionTokens.TryGetValue(instruction, ref token);
		}

		// Token: 0x0400057D RID: 1405
		internal readonly MethodDefinition method;

		// Token: 0x0400057E RID: 1406
		internal ParameterDefinition this_parameter;

		// Token: 0x0400057F RID: 1407
		internal int max_stack_size;

		// Token: 0x04000580 RID: 1408
		internal int code_size;

		// Token: 0x04000581 RID: 1409
		internal bool init_locals;

		// Token: 0x04000582 RID: 1410
		internal MetadataToken local_var_token;

		// Token: 0x04000583 RID: 1411
		internal Collection<Instruction> instructions;

		// Token: 0x04000584 RID: 1412
		internal Collection<ExceptionHandler> exceptions;

		// Token: 0x04000585 RID: 1413
		internal Collection<VariableDefinition> variables;

		// Token: 0x04000586 RID: 1414
		private ScopeDebugInformation scope;

		// Token: 0x04000587 RID: 1415
		internal Dictionary<Instruction, MetadataToken> instructionTokens;
	}
}
