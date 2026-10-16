using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000922 RID: 2338
	[StructLayout(LayoutKind.Sequential)]
	internal class EventOnTypeBuilderInst : EventInfo
	{
		// Token: 0x06004FFD RID: 20477 RVA: 0x000FA8A8 File Offset: 0x000F8AA8
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventBuilder evt)
		{
			this.instantiation = instantiation;
			this.event_builder = evt;
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x000FA8BE File Offset: 0x000F8ABE
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventInfo evt)
		{
			this.instantiation = instantiation;
			this.event_info = evt;
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004FFF RID: 20479 RVA: 0x000FA8D4 File Offset: 0x000F8AD4
		public override EventAttributes Attributes
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Attributes;
				}
				return this.event_builder.attrs;
			}
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x000FA8F8 File Offset: 0x000F8AF8
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.add_method : this.event_info.GetAddMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x000FA94C File Offset: 0x000F8B4C
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.raise_method : this.event_info.GetRaiseMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x000FA9A0 File Offset: 0x000F8BA0
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.remove_method : this.event_info.GetRemoveMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x000FA9F4 File Offset: 0x000F8BF4
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MethodInfo[] array;
			if (this.event_builder == null)
			{
				array = this.event_info.GetOtherMethods(nonPublic);
			}
			else
			{
				MethodInfo[] array2 = this.event_builder.other_methods;
				array = array2;
			}
			MethodInfo[] array3 = array;
			if (array3 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (MethodInfo methodInfo in array3)
			{
				if (nonPublic || methodInfo.IsPublic)
				{
					arrayList.Add(TypeBuilder.GetMethod(this.instantiation, methodInfo));
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array4, 0);
			return array4;
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06005004 RID: 20484 RVA: 0x000FAA84 File Offset: 0x000F8C84
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06005005 RID: 20485 RVA: 0x000FAA8C File Offset: 0x000F8C8C
		public override string Name
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Name;
				}
				return this.event_builder.name;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06005006 RID: 20486 RVA: 0x000FAA84 File Offset: 0x000F8C84
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x000472C8 File Offset: 0x000454C8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003158 RID: 12632
		private TypeBuilderInstantiation instantiation;

		// Token: 0x04003159 RID: 12633
		private EventBuilder event_builder;

		// Token: 0x0400315A RID: 12634
		private EventInfo event_info;
	}
}
