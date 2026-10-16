using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x020003A0 RID: 928
	internal sealed class DelegatingTypeDescriptionProvider : TypeDescriptionProvider
	{
		// Token: 0x06001E4A RID: 7754 RVA: 0x0006BA77 File Offset: 0x00069C77
		internal DelegatingTypeDescriptionProvider(Type type)
		{
			this._type = type;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x0006BA86 File Offset: 0x00069C86
		internal TypeDescriptionProvider Provider
		{
			get
			{
				return TypeDescriptor.GetProviderRecursive(this._type);
			}
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0006BA93 File Offset: 0x00069C93
		public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			return this.Provider.CreateInstance(provider, objectType, argTypes, args);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0006BAA5 File Offset: 0x00069CA5
		public override IDictionary GetCache(object instance)
		{
			return this.Provider.GetCache(instance);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0006BAB3 File Offset: 0x00069CB3
		public override string GetFullComponentName(object component)
		{
			return this.Provider.GetFullComponentName(component);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0006BAC1 File Offset: 0x00069CC1
		public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			return this.Provider.GetExtendedTypeDescriptor(instance);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0006BACF File Offset: 0x00069CCF
		protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
		{
			return this.Provider.GetExtenderProviders(instance);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0006BADD File Offset: 0x00069CDD
		public override Type GetReflectionType(Type objectType, object instance)
		{
			return this.Provider.GetReflectionType(objectType, instance);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0006BAEC File Offset: 0x00069CEC
		public override Type GetRuntimeType(Type objectType)
		{
			return this.Provider.GetRuntimeType(objectType);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0006BAFA File Offset: 0x00069CFA
		public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			return this.Provider.GetTypeDescriptor(objectType, instance);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0006BB09 File Offset: 0x00069D09
		public override bool IsSupportedType(Type type)
		{
			return this.Provider.IsSupportedType(type);
		}

		// Token: 0x04000F3E RID: 3902
		private readonly Type _type;
	}
}
