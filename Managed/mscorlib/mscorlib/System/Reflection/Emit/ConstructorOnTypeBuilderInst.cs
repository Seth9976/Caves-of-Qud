using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000916 RID: 2326
	[StructLayout(LayoutKind.Sequential)]
	internal class ConstructorOnTypeBuilderInst : ConstructorInfo
	{
		// Token: 0x06004F21 RID: 20257 RVA: 0x000F8616 File Offset: 0x000F6816
		public ConstructorOnTypeBuilderInst(TypeBuilderInstantiation instantiation, ConstructorInfo cb)
		{
			this.instantiation = instantiation;
			this.cb = cb;
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004F22 RID: 20258 RVA: 0x000F862C File Offset: 0x000F682C
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x000F8634 File Offset: 0x000F6834
		public override string Name
		{
			get
			{
				return this.cb.Name;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004F24 RID: 20260 RVA: 0x000F862C File Offset: 0x000F682C
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x000F8641 File Offset: 0x000F6841
		public override Module Module
		{
			get
			{
				return this.cb.Module;
			}
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x000F864E File Offset: 0x000F684E
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.cb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x000F865D File Offset: 0x000F685D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.cb.GetCustomAttributes(inherit);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x000F866B File Offset: 0x000F686B
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.cb.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x000F867A File Offset: 0x000F687A
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.cb.GetMethodImplementationFlags();
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x000F8687 File Offset: 0x000F6887
		public override ParameterInfo[] GetParameters()
		{
			if (!this.instantiation.IsCreated)
			{
				throw new NotSupportedException();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x000F86A4 File Offset: 0x000F68A4
		internal override ParameterInfo[] GetParametersInternal()
		{
			ParameterInfo[] array;
			if (this.cb is ConstructorBuilder)
			{
				ConstructorBuilder constructorBuilder = (ConstructorBuilder)this.cb;
				array = new ParameterInfo[constructorBuilder.parameters.Length];
				for (int i = 0; i < constructorBuilder.parameters.Length; i++)
				{
					Type type = this.instantiation.InflateType(constructorBuilder.parameters[i]);
					ParameterInfo[] array2 = array;
					int num = i;
					ParameterBuilder[] pinfo = constructorBuilder.pinfo;
					array2[num] = RuntimeParameterInfo.New((pinfo != null) ? pinfo[i] : null, type, this, i + 1);
				}
			}
			else
			{
				ParameterInfo[] parameters = this.cb.GetParameters();
				array = new ParameterInfo[parameters.Length];
				for (int j = 0; j < parameters.Length; j++)
				{
					Type type2 = this.instantiation.InflateType(parameters[j].ParameterType);
					array[j] = RuntimeParameterInfo.New(parameters[j], type2, this, j + 1);
				}
			}
			return array;
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x000F8774 File Offset: 0x000F6974
		internal override Type[] GetParameterTypes()
		{
			if (this.cb is ConstructorBuilder)
			{
				return (this.cb as ConstructorBuilder).parameters;
			}
			ParameterInfo[] parameters = this.cb.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			return array;
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x000F87CE File Offset: 0x000F69CE
		internal ConstructorInfo RuntimeResolve()
		{
			return this.instantiation.InternalResolve().GetConstructor(this.cb);
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x000F87E6 File Offset: 0x000F69E6
		public override int MetadataToken
		{
			get
			{
				return base.MetadataToken;
			}
		}

		// Token: 0x06004F2F RID: 20271 RVA: 0x000F87EE File Offset: 0x000F69EE
		internal override int GetParametersCount()
		{
			return this.cb.GetParametersCount();
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x000F87FB File Offset: 0x000F69FB
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.cb.Invoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06004F31 RID: 20273 RVA: 0x000F880F File Offset: 0x000F6A0F
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.cb.MethodHandle;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004F32 RID: 20274 RVA: 0x000F881C File Offset: 0x000F6A1C
		public override MethodAttributes Attributes
		{
			get
			{
				return this.cb.Attributes;
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004F33 RID: 20275 RVA: 0x000F8829 File Offset: 0x000F6A29
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.cb.CallingConvention;
			}
		}

		// Token: 0x06004F34 RID: 20276 RVA: 0x000F8836 File Offset: 0x000F6A36
		public override Type[] GetGenericArguments()
		{
			return this.cb.GetGenericArguments();
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004F35 RID: 20277 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004F36 RID: 20278 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004F37 RID: 20279 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public override bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004F38 RID: 20280 RVA: 0x00084B95 File Offset: 0x00082D95
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04003127 RID: 12583
		internal TypeBuilderInstantiation instantiation;

		// Token: 0x04003128 RID: 12584
		internal ConstructorInfo cb;
	}
}
