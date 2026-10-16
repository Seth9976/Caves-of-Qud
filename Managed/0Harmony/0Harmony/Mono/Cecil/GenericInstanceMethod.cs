using System;
using System.Text;
using System.Threading;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000209 RID: 521
	internal sealed class GenericInstanceMethod : MethodSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00027A60 File Offset: 0x00025C60
		public bool HasGenericArguments
		{
			get
			{
				return !this.arguments.IsNullOrEmpty<TypeReference>();
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00027A70 File Offset: 0x00025C70
		public Collection<TypeReference> GenericArguments
		{
			get
			{
				if (this.arguments == null)
				{
					Interlocked.CompareExchange<Collection<TypeReference>>(ref this.arguments, new Collection<TypeReference>(), null);
				}
				return this.arguments;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsGenericInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00027A92 File Offset: 0x00025C92
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return base.ElementMethod;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00027A9A File Offset: 0x00025C9A
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return base.ElementMethod.DeclaringType;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000B2B RID: 2859 RVA: 0x00027AA7 File Offset: 0x00025CA7
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.ContainsGenericParameter() || base.ContainsGenericParameter;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00027ABC File Offset: 0x00025CBC
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				MethodReference elementMethod = base.ElementMethod;
				stringBuilder.Append(elementMethod.ReturnType.FullName).Append(" ").Append(elementMethod.DeclaringType.FullName)
					.Append("::")
					.Append(elementMethod.Name);
				this.GenericInstanceFullName(stringBuilder);
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00027B2B File Offset: 0x00025D2B
		public GenericInstanceMethod(MethodReference method)
			: base(method)
		{
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00027B34 File Offset: 0x00025D34
		internal GenericInstanceMethod(MethodReference method, int arity)
			: this(method)
		{
			this.arguments = new Collection<TypeReference>(arity);
		}

		// Token: 0x0400032D RID: 813
		private Collection<TypeReference> arguments;
	}
}
