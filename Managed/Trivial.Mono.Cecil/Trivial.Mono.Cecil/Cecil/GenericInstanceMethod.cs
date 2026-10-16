using System;
using System.Text;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000041 RID: 65
	public sealed class GenericInstanceMethod : MethodSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00005FD0 File Offset: 0x000041D0
		public bool HasGenericArguments
		{
			get
			{
				return !this.arguments.IsNullOrEmpty<TypeReference>();
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00005FE0 File Offset: 0x000041E0
		public Collection<TypeReference> GenericArguments
		{
			get
			{
				Collection<TypeReference> collection;
				if ((collection = this.arguments) == null)
				{
					collection = (this.arguments = new Collection<TypeReference>());
				}
				return collection;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsGenericInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00006005 File Offset: 0x00004205
		IGenericParameterProvider IGenericContext.Method
		{
			get
			{
				return base.ElementMethod;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000600D File Offset: 0x0000420D
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return base.ElementMethod.DeclaringType;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000214 RID: 532 RVA: 0x0000601A File Offset: 0x0000421A
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.ContainsGenericParameter() || base.ContainsGenericParameter;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000602C File Offset: 0x0000422C
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

		// Token: 0x06000216 RID: 534 RVA: 0x0000609B File Offset: 0x0000429B
		public GenericInstanceMethod(MethodReference method)
			: base(method)
		{
		}

		// Token: 0x0400008B RID: 139
		private Collection<TypeReference> arguments;
	}
}
