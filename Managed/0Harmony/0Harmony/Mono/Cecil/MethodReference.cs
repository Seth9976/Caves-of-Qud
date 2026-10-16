using System;
using System.Text;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200023B RID: 571
	public class MethodReference : MemberReference, IMethodSignature, IMetadataTokenProvider, IGenericParameterProvider, IGenericContext
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002B58C File Offset: 0x0002978C
		// (set) Token: 0x06000CFA RID: 3322 RVA: 0x0002B594 File Offset: 0x00029794
		public virtual bool HasThis
		{
			get
			{
				return this.has_this;
			}
			set
			{
				this.has_this = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0002B59D File Offset: 0x0002979D
		// (set) Token: 0x06000CFC RID: 3324 RVA: 0x0002B5A5 File Offset: 0x000297A5
		public virtual bool ExplicitThis
		{
			get
			{
				return this.explicit_this;
			}
			set
			{
				this.explicit_this = value;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002B5AE File Offset: 0x000297AE
		// (set) Token: 0x06000CFE RID: 3326 RVA: 0x0002B5B6 File Offset: 0x000297B6
		public virtual MethodCallingConvention CallingConvention
		{
			get
			{
				return this.calling_convention;
			}
			set
			{
				this.calling_convention = value;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002B5BF File Offset: 0x000297BF
		public virtual bool HasParameters
		{
			get
			{
				return !this.parameters.IsNullOrEmpty<ParameterDefinition>();
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0002B5CF File Offset: 0x000297CF
		public virtual Collection<ParameterDefinition> Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					Interlocked.CompareExchange<ParameterDefinitionCollection>(ref this.parameters, new ParameterDefinitionCollection(this), null);
				}
				return this.parameters;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0002B5F4 File Offset: 0x000297F4
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				TypeReference declaringType = this.DeclaringType;
				GenericInstanceType genericInstanceType = declaringType as GenericInstanceType;
				if (genericInstanceType != null)
				{
					return genericInstanceType.ElementType;
				}
				return declaringType;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00018380 File Offset: 0x00016580
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00018941 File Offset: 0x00016B41
		GenericParameterType IGenericParameterProvider.GenericParameterType
		{
			get
			{
				return GenericParameterType.Method;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002B61A File Offset: 0x0002981A
		public virtual bool HasGenericParameters
		{
			get
			{
				return !this.generic_parameters.IsNullOrEmpty<GenericParameter>();
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0002B62A File Offset: 0x0002982A
		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (this.generic_parameters == null)
				{
					Interlocked.CompareExchange<Collection<GenericParameter>>(ref this.generic_parameters, new GenericParameterCollection(this), null);
				}
				return this.generic_parameters;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0002B650 File Offset: 0x00029850
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0002B670 File Offset: 0x00029870
		public TypeReference ReturnType
		{
			get
			{
				MethodReturnType methodReturnType = this.MethodReturnType;
				if (methodReturnType == null)
				{
					return null;
				}
				return methodReturnType.ReturnType;
			}
			set
			{
				MethodReturnType methodReturnType = this.MethodReturnType;
				if (methodReturnType != null)
				{
					methodReturnType.ReturnType = value;
				}
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0002B68E File Offset: 0x0002988E
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0002B696 File Offset: 0x00029896
		public virtual MethodReturnType MethodReturnType
		{
			get
			{
				return this.return_type;
			}
			set
			{
				this.return_type = value;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0002B6A0 File Offset: 0x000298A0
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.ReturnType.FullName).Append(" ").Append(base.MemberFullName());
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00018318 File Offset: 0x00016518
		public virtual bool IsGenericInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0002B6E8 File Offset: 0x000298E8
		public override bool ContainsGenericParameter
		{
			get
			{
				if (this.ReturnType.ContainsGenericParameter || base.ContainsGenericParameter)
				{
					return true;
				}
				if (!this.HasParameters)
				{
					return false;
				}
				Collection<ParameterDefinition> collection = this.Parameters;
				for (int i = 0; i < collection.Count; i++)
				{
					if (collection[i].ParameterType.ContainsGenericParameter)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002B744 File Offset: 0x00029944
		internal MethodReference()
		{
			this.return_type = new MethodReturnType(this);
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002B768 File Offset: 0x00029968
		public MethodReference(string name, TypeReference returnType)
			: base(name)
		{
			Mixin.CheckType(returnType, Mixin.Argument.returnType);
			this.return_type = new MethodReturnType(this);
			this.return_type.ReturnType = returnType;
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002B7A1 File Offset: 0x000299A1
		public MethodReference(string name, TypeReference returnType, TypeReference declaringType)
			: this(name, returnType)
		{
			Mixin.CheckType(declaringType, Mixin.Argument.declaringType);
			this.DeclaringType = declaringType;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00018380 File Offset: 0x00016580
		public virtual MethodReference GetElementMethod()
		{
			return this;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002B7BA File Offset: 0x000299BA
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002B7C2 File Offset: 0x000299C2
		public new virtual MethodDefinition Resolve()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}

		// Token: 0x040003D1 RID: 977
		internal ParameterDefinitionCollection parameters;

		// Token: 0x040003D2 RID: 978
		private MethodReturnType return_type;

		// Token: 0x040003D3 RID: 979
		private bool has_this;

		// Token: 0x040003D4 RID: 980
		private bool explicit_this;

		// Token: 0x040003D5 RID: 981
		private MethodCallingConvention calling_convention;

		// Token: 0x040003D6 RID: 982
		internal Collection<GenericParameter> generic_parameters;
	}
}
