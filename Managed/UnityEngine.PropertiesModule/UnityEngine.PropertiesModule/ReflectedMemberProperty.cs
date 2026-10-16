using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Unity.Properties
{
	// Token: 0x02000030 RID: 48
	public class ReflectedMemberProperty<TContainer, TValue> : Property<TContainer, TValue>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004DBF File Offset: 0x00002FBF
		public override string Name { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004DC7 File Offset: 0x00002FC7
		public override bool IsReadOnly { get; }

		// Token: 0x060000EA RID: 234 RVA: 0x00004DCF File Offset: 0x00002FCF
		public ReflectedMemberProperty(FieldInfo info, string name)
			: this(new FieldMember(info), name)
		{
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004DE5 File Offset: 0x00002FE5
		public ReflectedMemberProperty(PropertyInfo info, string name)
			: this(new PropertyMember(info), name)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004DFC File Offset: 0x00002FFC
		internal ReflectedMemberProperty(IMemberInfo info, string name)
		{
			this.Name = name;
			this.m_Info = info;
			this.m_IsStructContainerType = TypeTraits<TContainer>.IsValueType;
			base.AddAttributes(info.GetCustomAttributes());
			bool flag = this.m_Info.IsReadOnly;
			bool flag2 = base.HasAttribute<CreatePropertyAttribute>();
			if (flag2)
			{
				CreatePropertyAttribute attribute = base.GetAttribute<CreatePropertyAttribute>();
				flag |= attribute.ReadOnly;
			}
			this.IsReadOnly = flag;
			IMemberInfo memberInfo = this.m_Info;
			FieldMember fieldMember;
			bool flag3;
			if (memberInfo is FieldMember)
			{
				fieldMember = (FieldMember)memberInfo;
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			bool flag4 = flag3;
			if (flag4)
			{
				FieldInfo fieldInfo = fieldMember.m_FieldInfo;
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, fieldInfo.FieldType, new Type[] { this.m_IsStructContainerType ? fieldInfo.ReflectedType.MakeByRefType() : fieldInfo.ReflectedType }, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilgenerator.Emit(OpCodes.Ret);
				bool isStructContainerType = this.m_IsStructContainerType;
				if (isStructContainerType)
				{
					this.m_GetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction));
				}
				else
				{
					this.m_GetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction));
				}
				bool flag5 = !flag;
				if (flag5)
				{
					dynamicMethod = new DynamicMethod(string.Empty, typeof(void), new Type[]
					{
						this.m_IsStructContainerType ? fieldInfo.ReflectedType.MakeByRefType() : fieldInfo.ReflectedType,
						fieldInfo.FieldType
					}, true);
					ilgenerator = dynamicMethod.GetILGenerator();
					ilgenerator.Emit(OpCodes.Ldarg_0);
					ilgenerator.Emit(OpCodes.Ldarg_1);
					ilgenerator.Emit(OpCodes.Stfld, fieldInfo);
					ilgenerator.Emit(OpCodes.Ret);
					bool isStructContainerType2 = this.m_IsStructContainerType;
					if (isStructContainerType2)
					{
						this.m_SetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction));
					}
					else
					{
						this.m_SetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction));
					}
				}
			}
			else
			{
				memberInfo = this.m_Info;
				PropertyMember propertyMember;
				bool flag6;
				if (memberInfo is PropertyMember)
				{
					propertyMember = (PropertyMember)memberInfo;
					flag6 = true;
				}
				else
				{
					flag6 = false;
				}
				bool flag7 = flag6;
				if (flag7)
				{
					bool isStructContainerType3 = this.m_IsStructContainerType;
					if (isStructContainerType3)
					{
						MethodInfo getMethod = propertyMember.m_PropertyInfo.GetGetMethod(true);
						this.m_GetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction), getMethod);
						bool flag8 = !flag;
						if (flag8)
						{
							MethodInfo setMethod = propertyMember.m_PropertyInfo.GetSetMethod(true);
							this.m_SetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction), setMethod);
						}
					}
					else
					{
						MethodInfo getMethod2 = propertyMember.m_PropertyInfo.GetGetMethod(true);
						this.m_GetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction), getMethod2);
						bool flag9 = !flag;
						if (flag9)
						{
							MethodInfo setMethod2 = propertyMember.m_PropertyInfo.GetSetMethod(true);
							this.m_SetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction), setMethod2);
						}
					}
				}
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00005128 File Offset: 0x00003328
		public override TValue GetValue(ref TContainer container)
		{
			bool isStructContainerType = this.m_IsStructContainerType;
			TValue tvalue;
			if (isStructContainerType)
			{
				tvalue = ((this.m_GetStructValueAction == null) ? ((TValue)((object)this.m_Info.GetValue(container))) : this.m_GetStructValueAction(ref container));
			}
			else
			{
				tvalue = ((this.m_GetClassValueAction == null) ? ((TValue)((object)this.m_Info.GetValue(container))) : this.m_GetClassValueAction(container));
			}
			return tvalue;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000051B0 File Offset: 0x000033B0
		public override void SetValue(ref TContainer container, TValue value)
		{
			bool isReadOnly = this.IsReadOnly;
			if (isReadOnly)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}
			bool isStructContainerType = this.m_IsStructContainerType;
			if (isStructContainerType)
			{
				bool flag = this.m_SetStructValueAction == null;
				if (flag)
				{
					object obj = container;
					this.m_Info.SetValue(obj, value);
					container = (TContainer)((object)obj);
				}
				else
				{
					this.m_SetStructValueAction(ref container, value);
				}
			}
			else
			{
				bool flag2 = this.m_SetClassValueAction == null;
				if (flag2)
				{
					this.m_Info.SetValue(container, value);
				}
				else
				{
					this.m_SetClassValueAction(container, value);
				}
			}
		}

		// Token: 0x04000051 RID: 81
		private readonly IMemberInfo m_Info;

		// Token: 0x04000052 RID: 82
		private readonly bool m_IsStructContainerType;

		// Token: 0x04000053 RID: 83
		private ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction m_GetStructValueAction;

		// Token: 0x04000054 RID: 84
		private ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction m_SetStructValueAction;

		// Token: 0x04000055 RID: 85
		private ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction m_GetClassValueAction;

		// Token: 0x04000056 RID: 86
		private ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction m_SetClassValueAction;

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x060000F0 RID: 240
		private delegate TValue GetStructValueAction(ref TContainer container);

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x060000F4 RID: 244
		private delegate void SetStructValueAction(ref TContainer container, TValue value);

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x060000F8 RID: 248
		private delegate TValue GetClassValueAction(TContainer container);

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x060000FC RID: 252
		private delegate void SetClassValueAction(TContainer container, TValue value);
	}
}
