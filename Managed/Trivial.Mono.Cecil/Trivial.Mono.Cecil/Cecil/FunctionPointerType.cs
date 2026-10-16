using System;
using System.Text;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000040 RID: 64
	public sealed class FunctionPointerType : TypeSpecification, IMethodSignature, IMetadataTokenProvider
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00005E69 File Offset: 0x00004069
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x00005E76 File Offset: 0x00004076
		public bool HasThis
		{
			get
			{
				return this.function.HasThis;
			}
			set
			{
				this.function.HasThis = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005E84 File Offset: 0x00004084
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00005E91 File Offset: 0x00004091
		public bool ExplicitThis
		{
			get
			{
				return this.function.ExplicitThis;
			}
			set
			{
				this.function.ExplicitThis = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00005E9F File Offset: 0x0000409F
		// (set) Token: 0x060001FC RID: 508 RVA: 0x00005EAC File Offset: 0x000040AC
		public MethodCallingConvention CallingConvention
		{
			get
			{
				return this.function.CallingConvention;
			}
			set
			{
				this.function.CallingConvention = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00005EBA File Offset: 0x000040BA
		public bool HasParameters
		{
			get
			{
				return this.function.HasParameters;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00005EC7 File Offset: 0x000040C7
		public Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.function.Parameters;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00005ED4 File Offset: 0x000040D4
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00005EE6 File Offset: 0x000040E6
		public TypeReference ReturnType
		{
			get
			{
				return this.function.MethodReturnType.ReturnType;
			}
			set
			{
				this.function.MethodReturnType.ReturnType = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00005EF9 File Offset: 0x000040F9
		public MethodReturnType MethodReturnType
		{
			get
			{
				return this.function.MethodReturnType;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00005F06 File Offset: 0x00004106
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00002B65 File Offset: 0x00000D65
		public override string Name
		{
			get
			{
				return this.function.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00004111 File Offset: 0x00002311
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00002B65 File Offset: 0x00000D65
		public override string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00005F13 File Offset: 0x00004113
		public override ModuleDefinition Module
		{
			get
			{
				return this.ReturnType.Module;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005F20 File Offset: 0x00004120
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00002B65 File Offset: 0x00000D65
		public override IMetadataScope Scope
		{
			get
			{
				return this.function.ReturnType.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsFunctionPointer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00005F32 File Offset: 0x00004132
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.function.ContainsGenericParameter;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00005F40 File Offset: 0x00004140
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.function.Name);
				stringBuilder.Append(" ");
				stringBuilder.Append(this.function.ReturnType.FullName);
				stringBuilder.Append(" *");
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005FA1 File Offset: 0x000041A1
		public FunctionPointerType()
			: base(null)
		{
			this.function = new MethodReference();
			this.function.Name = "method";
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.FnPtr;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005FCD File Offset: 0x000041CD
		public override TypeDefinition Resolve()
		{
			return null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00002654 File Offset: 0x00000854
		public override TypeReference GetElementType()
		{
			return this;
		}

		// Token: 0x0400008A RID: 138
		private readonly MethodReference function;
	}
}
