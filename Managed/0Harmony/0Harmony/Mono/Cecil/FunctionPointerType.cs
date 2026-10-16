using System;
using System.Text;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000208 RID: 520
	internal sealed class FunctionPointerType : TypeSpecification, IMethodSignature, IMetadataTokenProvider
	{
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000278F9 File Offset: 0x00025AF9
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x00027906 File Offset: 0x00025B06
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

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00027914 File Offset: 0x00025B14
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x00027921 File Offset: 0x00025B21
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

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0002792F File Offset: 0x00025B2F
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0002793C File Offset: 0x00025B3C
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

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002794A File Offset: 0x00025B4A
		public bool HasParameters
		{
			get
			{
				return this.function.HasParameters;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x00027957 File Offset: 0x00025B57
		public Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.function.Parameters;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00027964 File Offset: 0x00025B64
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00027976 File Offset: 0x00025B76
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

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00027989 File Offset: 0x00025B89
		public MethodReturnType MethodReturnType
		{
			get
			{
				return this.function.MethodReturnType;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00027996 File Offset: 0x00025B96
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x0002640D File Offset: 0x0002460D
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000279A3 File Offset: 0x00025BA3
		public override ModuleDefinition Module
		{
			get
			{
				return this.ReturnType.Module;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x000279B0 File Offset: 0x00025BB0
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsFunctionPointer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x000279C2 File Offset: 0x00025BC2
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.function.ContainsGenericParameter;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x000279D0 File Offset: 0x00025BD0
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

		// Token: 0x06000B23 RID: 2851 RVA: 0x00027A31 File Offset: 0x00025C31
		public FunctionPointerType()
			: base(null)
		{
			this.function = new MethodReference();
			this.function.Name = "method";
			this.etype = Mono.Cecil.Metadata.ElementType.FnPtr;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00027A5D File Offset: 0x00025C5D
		public override TypeDefinition Resolve()
		{
			return null;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00018380 File Offset: 0x00016580
		public override TypeReference GetElementType()
		{
			return this;
		}

		// Token: 0x0400032C RID: 812
		private readonly MethodReference function;
	}
}
