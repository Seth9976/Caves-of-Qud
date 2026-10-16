using System;
using System.Text;
using System.Threading;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200020A RID: 522
	internal sealed class GenericInstanceType : TypeSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00027B49 File Offset: 0x00025D49
		public bool HasGenericArguments
		{
			get
			{
				return !this.arguments.IsNullOrEmpty<TypeReference>();
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x00027B59 File Offset: 0x00025D59
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

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x00027B7B File Offset: 0x00025D7B
		// (set) Token: 0x06000B32 RID: 2866 RVA: 0x00003B32 File Offset: 0x00001D32
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

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00027B88 File Offset: 0x00025D88
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

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsGenericInstance
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00027BB5 File Offset: 0x00025DB5
		public override bool ContainsGenericParameter
		{
			get
			{
				return this.ContainsGenericParameter() || base.ContainsGenericParameter;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00027BC7 File Offset: 0x00025DC7
		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				return base.ElementType;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00027BCF File Offset: 0x00025DCF
		public GenericInstanceType(TypeReference type)
			: base(type)
		{
			base.IsValueType = type.IsValueType;
			this.etype = Mono.Cecil.Metadata.ElementType.GenericInst;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00027BEC File Offset: 0x00025DEC
		internal GenericInstanceType(TypeReference type, int arity)
			: this(type)
		{
			this.arguments = new Collection<TypeReference>(arity);
		}

		// Token: 0x0400032E RID: 814
		private Collection<TypeReference> arguments;
	}
}
