using System;
using System.Text;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001F6 RID: 502
	public sealed class CallSite : IMethodSignature, IMetadataTokenProvider
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00026370 File Offset: 0x00024570
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0002637D File Offset: 0x0002457D
		public bool HasThis
		{
			get
			{
				return this.signature.HasThis;
			}
			set
			{
				this.signature.HasThis = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002638B File Offset: 0x0002458B
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00026398 File Offset: 0x00024598
		public bool ExplicitThis
		{
			get
			{
				return this.signature.ExplicitThis;
			}
			set
			{
				this.signature.ExplicitThis = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x000263A6 File Offset: 0x000245A6
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x000263B3 File Offset: 0x000245B3
		public MethodCallingConvention CallingConvention
		{
			get
			{
				return this.signature.CallingConvention;
			}
			set
			{
				this.signature.CallingConvention = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x000263C1 File Offset: 0x000245C1
		public bool HasParameters
		{
			get
			{
				return this.signature.HasParameters;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000263CE File Offset: 0x000245CE
		public Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.signature.Parameters;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000263DB File Offset: 0x000245DB
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x000263ED File Offset: 0x000245ED
		public TypeReference ReturnType
		{
			get
			{
				return this.signature.MethodReturnType.ReturnType;
			}
			set
			{
				this.signature.MethodReturnType.ReturnType = value;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00026400 File Offset: 0x00024600
		public MethodReturnType MethodReturnType
		{
			get
			{
				return this.signature.MethodReturnType;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0002640D File Offset: 0x0002460D
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x000189AE File Offset: 0x00016BAE
		public string Name
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

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002640D File Offset: 0x0002460D
		// (set) Token: 0x06000A2D RID: 2605 RVA: 0x000189AE File Offset: 0x00016BAE
		public string Namespace
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

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00026414 File Offset: 0x00024614
		public ModuleDefinition Module
		{
			get
			{
				return this.ReturnType.Module;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00026421 File Offset: 0x00024621
		public IMetadataScope Scope
		{
			get
			{
				return this.signature.ReturnType.Scope;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00026433 File Offset: 0x00024633
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x00026440 File Offset: 0x00024640
		public MetadataToken MetadataToken
		{
			get
			{
				return this.signature.token;
			}
			set
			{
				this.signature.token = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00026450 File Offset: 0x00024650
		public string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.ReturnType.FullName);
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00026482 File Offset: 0x00024682
		internal CallSite()
		{
			this.signature = new MethodReference();
			this.signature.token = new MetadataToken(TokenType.Signature, 0);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x000264AB File Offset: 0x000246AB
		public CallSite(TypeReference returnType)
			: this()
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			this.signature.ReturnType = returnType;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000264CD File Offset: 0x000246CD
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x040002E2 RID: 738
		private readonly MethodReference signature;
	}
}
