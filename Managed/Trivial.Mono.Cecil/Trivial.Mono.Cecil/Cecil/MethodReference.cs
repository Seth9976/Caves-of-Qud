using System;
using System.Text;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000081 RID: 129
	public class MethodReference : MemberReference, IMethodSignature, IMetadataTokenProvider, IGenericParameterProvider, IGenericContext
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000106F6 File Offset: 0x0000E8F6
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x000106FE File Offset: 0x0000E8FE
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

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00010707 File Offset: 0x0000E907
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0001070F File Offset: 0x0000E90F
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

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x00010718 File Offset: 0x0000E918
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00010720 File Offset: 0x0000E920
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

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00010729 File Offset: 0x0000E929
		public virtual bool HasParameters
		{
			get
			{
				return !this.parameters.IsNullOrEmpty<ParameterDefinition>();
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x00010739 File Offset: 0x0000E939
		public virtual Collection<ParameterDefinition> Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ParameterDefinitionCollection(this);
				}
				return this.parameters;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00010758 File Offset: 0x0000E958
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

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00002654 File Offset: 0x00000854
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00002AF8 File Offset: 0x00000CF8
		GenericParameterType IGenericParameterProvider.GenericParameterType
		{
			get
			{
				return GenericParameterType.Method;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0001077E File Offset: 0x0000E97E
		public virtual bool HasGenericParameters
		{
			get
			{
				return !this.generic_parameters.IsNullOrEmpty<GenericParameter>();
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00010790 File Offset: 0x0000E990
		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (this.generic_parameters != null)
				{
					return this.generic_parameters;
				}
				return this.generic_parameters = new GenericParameterCollection(this);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000107BB File Offset: 0x0000E9BB
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x000107D0 File Offset: 0x0000E9D0
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

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x000107EE File Offset: 0x0000E9EE
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x000107F6 File Offset: 0x0000E9F6
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

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00010800 File Offset: 0x0000EA00
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

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x000025EF File Offset: 0x000007EF
		public virtual bool IsGenericInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00010848 File Offset: 0x0000EA48
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

		// Token: 0x06000515 RID: 1301 RVA: 0x000108A4 File Offset: 0x0000EAA4
		internal MethodReference()
		{
			this.return_type = new MethodReturnType(this);
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000108C8 File Offset: 0x0000EAC8
		public MethodReference(string name, TypeReference returnType)
			: base(name)
		{
			Mixin.CheckType(returnType, Mixin.Argument.returnType);
			this.return_type = new MethodReturnType(this);
			this.return_type.ReturnType = returnType;
			this.token = new MetadataToken(TokenType.MemberRef);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00010901 File Offset: 0x0000EB01
		public MethodReference(string name, TypeReference returnType, TypeReference declaringType)
			: this(name, returnType)
		{
			Mixin.CheckType(declaringType, Mixin.Argument.declaringType);
			this.DeclaringType = declaringType;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00002654 File Offset: 0x00000854
		public virtual MethodReference GetElementMethod()
		{
			return this;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001091A File Offset: 0x0000EB1A
		protected override IMemberDefinition ResolveDefinition()
		{
			return this.Resolve();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00010924 File Offset: 0x0000EB24
		public new virtual MethodDefinition Resolve()
		{
			ModuleDefinition module = this.Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}

		// Token: 0x04000185 RID: 389
		internal ParameterDefinitionCollection parameters;

		// Token: 0x04000186 RID: 390
		private MethodReturnType return_type;

		// Token: 0x04000187 RID: 391
		private bool has_this;

		// Token: 0x04000188 RID: 392
		private bool explicit_this;

		// Token: 0x04000189 RID: 393
		private MethodCallingConvention calling_convention;

		// Token: 0x0400018A RID: 394
		internal Collection<GenericParameter> generic_parameters;
	}
}
