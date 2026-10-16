using System;
using System.Text;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200001B RID: 27
	public sealed class CallSite : IMethodSignature, IMetadataTokenProvider
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004074 File Offset: 0x00002274
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004081 File Offset: 0x00002281
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

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000408F File Offset: 0x0000228F
		// (set) Token: 0x060000DE RID: 222 RVA: 0x0000409C File Offset: 0x0000229C
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000040AA File Offset: 0x000022AA
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000040B7 File Offset: 0x000022B7
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000040C5 File Offset: 0x000022C5
		public bool HasParameters
		{
			get
			{
				return this.signature.HasParameters;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000040D2 File Offset: 0x000022D2
		public Collection<ParameterDefinition> Parameters
		{
			get
			{
				return this.signature.Parameters;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000040DF File Offset: 0x000022DF
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x000040F1 File Offset: 0x000022F1
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004104 File Offset: 0x00002304
		public MethodReturnType MethodReturnType
		{
			get
			{
				return this.signature.MethodReturnType;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004111 File Offset: 0x00002311
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004111 File Offset: 0x00002311
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002B65 File Offset: 0x00000D65
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004118 File Offset: 0x00002318
		public ModuleDefinition Module
		{
			get
			{
				return this.ReturnType.Module;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004125 File Offset: 0x00002325
		public IMetadataScope Scope
		{
			get
			{
				return this.signature.ReturnType.Scope;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004137 File Offset: 0x00002337
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00004144 File Offset: 0x00002344
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

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004154 File Offset: 0x00002354
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

		// Token: 0x060000EF RID: 239 RVA: 0x00004186 File Offset: 0x00002386
		internal CallSite()
		{
			this.signature = new MethodReference();
			this.signature.token = new MetadataToken(TokenType.Signature, 0);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000041AF File Offset: 0x000023AF
		public CallSite(TypeReference returnType)
			: this()
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			this.signature.ReturnType = returnType;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000041D1 File Offset: 0x000023D1
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x04000032 RID: 50
		private readonly MethodReference signature;
	}
}
