using System;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002BD RID: 701
	public sealed class MethodBody
	{
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00039EE5 File Offset: 0x000380E5
		public MethodDefinition Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x00039EED File Offset: 0x000380ED
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x00039EF5 File Offset: 0x000380F5
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

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00039EFE File Offset: 0x000380FE
		public int CodeSize
		{
			get
			{
				return this.code_size;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00039F06 File Offset: 0x00038106
		// (set) Token: 0x060011F8 RID: 4600 RVA: 0x00039F0E File Offset: 0x0003810E
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

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00039F17 File Offset: 0x00038117
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x00039F1F File Offset: 0x0003811F
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

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x00039F28 File Offset: 0x00038128
		public Collection<Instruction> Instructions
		{
			get
			{
				if (this.instructions == null)
				{
					Interlocked.CompareExchange<Collection<Instruction>>(ref this.instructions, new InstructionCollection(this.method), null);
				}
				return this.instructions;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00039F50 File Offset: 0x00038150
		public bool HasExceptionHandlers
		{
			get
			{
				return !this.exceptions.IsNullOrEmpty<ExceptionHandler>();
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00039F60 File Offset: 0x00038160
		public Collection<ExceptionHandler> ExceptionHandlers
		{
			get
			{
				if (this.exceptions == null)
				{
					Interlocked.CompareExchange<Collection<ExceptionHandler>>(ref this.exceptions, new Collection<ExceptionHandler>(), null);
				}
				return this.exceptions;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x00039F82 File Offset: 0x00038182
		public bool HasVariables
		{
			get
			{
				return !this.variables.IsNullOrEmpty<VariableDefinition>();
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00039F92 File Offset: 0x00038192
		public Collection<VariableDefinition> Variables
		{
			get
			{
				if (this.variables == null)
				{
					Interlocked.CompareExchange<Collection<VariableDefinition>>(ref this.variables, new VariableDefinitionCollection(this.method), null);
				}
				return this.variables;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x00039FBC File Offset: 0x000381BC
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

		// Token: 0x06001201 RID: 4609 RVA: 0x0003A01C File Offset: 0x0003821C
		private static ParameterDefinition CreateThisParameter(MethodDefinition method)
		{
			TypeReference typeReference = method.DeclaringType;
			if (typeReference.HasGenericParameters)
			{
				GenericInstanceType genericInstanceType = new GenericInstanceType(typeReference, typeReference.GenericParameters.Count);
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

		// Token: 0x06001202 RID: 4610 RVA: 0x0003A097 File Offset: 0x00038297
		public MethodBody(MethodDefinition method)
		{
			this.method = method;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0003A0A6 File Offset: 0x000382A6
		public ILProcessor GetILProcessor()
		{
			return new ILProcessor(this);
		}

		// Token: 0x040007FC RID: 2044
		internal readonly MethodDefinition method;

		// Token: 0x040007FD RID: 2045
		internal ParameterDefinition this_parameter;

		// Token: 0x040007FE RID: 2046
		internal int max_stack_size;

		// Token: 0x040007FF RID: 2047
		internal int code_size;

		// Token: 0x04000800 RID: 2048
		internal bool init_locals;

		// Token: 0x04000801 RID: 2049
		internal MetadataToken local_var_token;

		// Token: 0x04000802 RID: 2050
		internal Collection<Instruction> instructions;

		// Token: 0x04000803 RID: 2051
		internal Collection<ExceptionHandler> exceptions;

		// Token: 0x04000804 RID: 2052
		internal Collection<VariableDefinition> variables;
	}
}
