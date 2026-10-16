using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200023F RID: 575
	internal abstract class MethodSpecification : MethodReference
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0002BCBD File Offset: 0x00029EBD
		public MethodReference ElementMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0002BCC5 File Offset: 0x00029EC5
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x000189AE File Offset: 0x00016BAE
		public override string Name
		{
			get
			{
				return this.method.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0002BCD2 File Offset: 0x00029ED2
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000189AE File Offset: 0x00016BAE
		public override MethodCallingConvention CallingConvention
		{
			get
			{
				return this.method.CallingConvention;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x0002BCDF File Offset: 0x00029EDF
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x000189AE File Offset: 0x00016BAE
		public override bool HasThis
		{
			get
			{
				return this.method.HasThis;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0002BCEC File Offset: 0x00029EEC
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x000189AE File Offset: 0x00016BAE
		public override bool ExplicitThis
		{
			get
			{
				return this.method.ExplicitThis;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x0002BCF9 File Offset: 0x00029EF9
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x000189AE File Offset: 0x00016BAE
		public override MethodReturnType MethodReturnType
		{
			get
			{
				return this.method.MethodReturnType;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x0002BD06 File Offset: 0x00029F06
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x000189AE File Offset: 0x00016BAE
		public override TypeReference DeclaringType
		{
			get
			{
				return this.method.DeclaringType;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x0002BD13 File Offset: 0x00029F13
		public override ModuleDefinition Module
		{
			get
			{
				return this.method.Module;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0002BD20 File Offset: 0x00029F20
		public override bool HasParameters
		{
			get
			{
				return this.method.HasParameters;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0002BD2D File Offset: 0x00029F2D
		public override Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.method.Parameters;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0002BD3A File Offset: 0x00029F3A
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.method.ContainsGenericParameter;
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002BD47 File Offset: 0x00029F47
		internal MethodSpecification(MethodReference method)
		{
			Mixin.CheckMethod(method);
			this.method = method;
			this.token = new MetadataToken(TokenType.MethodSpec);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002BD6C File Offset: 0x00029F6C
		public sealed override MethodReference GetElementMethod()
		{
			return this.method.GetElementMethod();
		}

		// Token: 0x040003E4 RID: 996
		private readonly MethodReference method;
	}
}
