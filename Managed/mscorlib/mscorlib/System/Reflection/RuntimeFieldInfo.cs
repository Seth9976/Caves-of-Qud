using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020008F6 RID: 2294
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeFieldInfo : RtFieldInfo, ISerializable
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004D1C RID: 19740 RVA: 0x000F3CC5 File Offset: 0x000F1EC5
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x000F3A36 File Offset: 0x000F1C36
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x000F3A43 File Offset: 0x000F1C43
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x000F3CCD File Offset: 0x000F1ECD
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x000F3CDA File Offset: 0x000F1EDA
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x06004D21 RID: 19745
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern object UnsafeGetValue(object obj);

		// Token: 0x06004D22 RID: 19746 RVA: 0x000F3D04 File Offset: 0x000F1F04
		internal override void CheckConsistency(object target)
		{
			if ((this.Attributes & FieldAttributes.Static) == FieldAttributes.Static || this.DeclaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("Non-static field requires a target."));
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Field '{0}' defined on type '{1}' is not a field on the target object which is of type '{2}'."), this.Name, this.DeclaringType, target.GetType()));
		}

		// Token: 0x06004D23 RID: 19747 RVA: 0x000F3D6C File Offset: 0x000F1F6C
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal override void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			bool flag = false;
			RuntimeFieldHandle.SetValue(this, obj, value, null, this.Attributes, null, ref flag);
		}

		// Token: 0x06004D24 RID: 19748 RVA: 0x000F3D8D File Offset: 0x000F1F8D
		[DebuggerHidden]
		[DebuggerStepThrough]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x06004D25 RID: 19749 RVA: 0x000F3DC8 File Offset: 0x000F1FC8
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override object GetValueDirect(TypedReference obj)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), (RuntimeType)this.DeclaringType);
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x000F3E02 File Offset: 0x000F2002
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x000F3E0A File Offset: 0x000F200A
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.fhandle;
			}
		}

		// Token: 0x06004D28 RID: 19752
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type ResolveType();

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004D29 RID: 19753 RVA: 0x000F3E12 File Offset: 0x000F2012
		public override Type FieldType
		{
			get
			{
				if (this.type == null)
				{
					this.type = this.ResolveType();
				}
				return this.type;
			}
		}

		// Token: 0x06004D2A RID: 19754
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type GetParentType(bool declaring);

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004D2B RID: 19755 RVA: 0x000F3E34 File Offset: 0x000F2034
		public override Type ReflectedType
		{
			get
			{
				return this.GetParentType(false);
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004D2C RID: 19756 RVA: 0x000F3E3D File Offset: 0x000F203D
		public override Type DeclaringType
		{
			get
			{
				return this.GetParentType(true);
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004D2D RID: 19757 RVA: 0x000F3E46 File Offset: 0x000F2046
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x000F1969 File Offset: 0x000EFB69
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x000F1972 File Offset: 0x000EFB72
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D31 RID: 19761
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern int GetFieldOffset();

		// Token: 0x06004D32 RID: 19762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetValueInternal(object obj);

		// Token: 0x06004D33 RID: 19763 RVA: 0x000F3E50 File Offset: 0x000F2050
		public override object GetValue(object obj)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (!base.IsLiteral)
			{
				this.CheckGeneric();
			}
			return this.GetValueInternal(obj);
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x000F3EC2 File Offset: 0x000F20C2
		public override string ToString()
		{
			return string.Format("{0} {1}", this.FieldType, this.name);
		}

		// Token: 0x06004D35 RID: 19765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06004D36 RID: 19766 RVA: 0x000F3EDC File Offset: 0x000F20DC
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (base.IsLiteral)
			{
				throw new FieldAccessException("Cannot set a constant field");
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			this.CheckGeneric();
			if (val != null)
			{
				val = ((RuntimeType)this.FieldType).CheckValue(val, binder, culture, invokeAttr);
			}
			RuntimeFieldInfo.SetValueInternal(this, obj, val);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x000F3F80 File Offset: 0x000F2180
		internal RuntimeFieldInfo Clone(string newName)
		{
			return new RuntimeFieldInfo
			{
				name = newName,
				type = this.type,
				attrs = this.attrs,
				klass = this.klass,
				fhandle = this.fhandle
			};
		}

		// Token: 0x06004D38 RID: 19768
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern object GetRawConstantValue();

		// Token: 0x06004D39 RID: 19769 RVA: 0x000F3C9C File Offset: 0x000F1E9C
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004D3A RID: 19770 RVA: 0x000F3FBE File Offset: 0x000F21BE
		private void CheckGeneric()
		{
			if (this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.");
			}
		}

		// Token: 0x06004D3B RID: 19771
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x000F3FD8 File Offset: 0x000F21D8
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004D3D RID: 19773 RVA: 0x000F3FE3 File Offset: 0x000F21E3
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004D3E RID: 19774 RVA: 0x000F3FEE File Offset: 0x000F21EE
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x000F3FF9 File Offset: 0x000F21F9
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeFieldInfo>(other);
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004D40 RID: 19776 RVA: 0x000F4002 File Offset: 0x000F2202
		public override int MetadataToken
		{
			get
			{
				return RuntimeFieldInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D41 RID: 19777
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeFieldInfo monoField);

		// Token: 0x06004D42 RID: 19778
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetTypeModifiers(bool optional);

		// Token: 0x06004D43 RID: 19779 RVA: 0x000F400A File Offset: 0x000F220A
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.GetCustomModifiers(true);
		}

		// Token: 0x06004D44 RID: 19780 RVA: 0x000F4013 File Offset: 0x000F2213
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.GetCustomModifiers(false);
		}

		// Token: 0x06004D45 RID: 19781 RVA: 0x000F401C File Offset: 0x000F221C
		private Type[] GetCustomModifiers(bool optional)
		{
			return this.GetTypeModifiers(optional) ?? Type.EmptyTypes;
		}

		// Token: 0x04003050 RID: 12368
		internal IntPtr klass;

		// Token: 0x04003051 RID: 12369
		internal RuntimeFieldHandle fhandle;

		// Token: 0x04003052 RID: 12370
		private string name;

		// Token: 0x04003053 RID: 12371
		private Type type;

		// Token: 0x04003054 RID: 12372
		private FieldAttributes attrs;
	}
}
