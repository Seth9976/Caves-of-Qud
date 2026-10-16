using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000086 RID: 134
	public abstract class MethodSpecification : MethodReference
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x00010D91 File Offset: 0x0000EF91
		public MethodReference ElementMethod
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x00010D99 File Offset: 0x0000EF99
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00010DA6 File Offset: 0x0000EFA6
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00010DB3 File Offset: 0x0000EFB3
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x00010DCD File Offset: 0x0000EFCD
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x00010DDA File Offset: 0x0000EFDA
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x00010DE7 File Offset: 0x0000EFE7
		public override ModuleDefinition Module
		{
			get
			{
				return this.method.Module;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00010DF4 File Offset: 0x0000EFF4
		public override bool HasParameters
		{
			get
			{
				return this.method.HasParameters;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x00010E01 File Offset: 0x0000F001
		public override Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.method.Parameters;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00010E0E File Offset: 0x0000F00E
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.method.ContainsGenericParameter;
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00010E1B File Offset: 0x0000F01B
		internal MethodSpecification(MethodReference method)
		{
			Mixin.CheckMethod(method);
			this.method = method;
			this.token = new MetadataToken(TokenType.MethodSpec);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00010E40 File Offset: 0x0000F040
		public sealed override MethodReference GetElementMethod()
		{
			return this.method.GetElementMethod();
		}

		// Token: 0x04000196 RID: 406
		private readonly MethodReference method;
	}
}
