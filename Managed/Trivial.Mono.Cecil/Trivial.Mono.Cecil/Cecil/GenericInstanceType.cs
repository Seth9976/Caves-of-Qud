using System;
using System.Text;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000042 RID: 66
	public sealed class GenericInstanceType : TypeSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000217 RID: 535 RVA: 0x000060A4 File Offset: 0x000042A4
		public bool HasGenericArguments
		{
			get
			{
				return !this.arguments.IsNullOrEmpty<TypeReference>();
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000218 RID: 536 RVA: 0x000060B4 File Offset: 0x000042B4
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000060D9 File Offset: 0x000042D9
		// (set) Token: 0x0600021A RID: 538 RVA: 0x000060E6 File Offset: 0x000042E6
		public override TypeReference DeclaringType
		{
			get
			{
				return base.ElementType.DeclaringType;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000060F0 File Offset: 0x000042F0
		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.FullName);
				this.GenericInstanceFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsGenericInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000611D File Offset: 0x0000431D
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.ContainsGenericParameter() || base.ContainsGenericParameter;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000612F File Offset: 0x0000432F
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return base.ElementType;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00006137 File Offset: 0x00004337
		public GenericInstanceType(TypeReference type)
			: base(type)
		{
			base.IsValueType = type.IsValueType;
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.GenericInst;
		}

		// Token: 0x0400008C RID: 140
		private Collection<TypeReference> arguments;
	}
}
