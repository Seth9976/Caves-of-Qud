using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020003E9 RID: 1001
	internal sealed class ReflectEventDescriptor : EventDescriptor
	{
		// Token: 0x060020C5 RID: 8389 RVA: 0x00070FF8 File Offset: 0x0006F1F8
		public ReflectEventDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.Format("Null is not a valid value for {0}.", "componentClass"));
			}
			if (type == null || !typeof(Delegate).IsAssignableFrom(type))
			{
				throw new ArgumentException(SR.Format("Invalid type for the {0} event.", name));
			}
			this._componentClass = componentClass;
			this._type = type;
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x00071066 File Offset: 0x0006F266
		public ReflectEventDescriptor(Type componentClass, EventInfo eventInfo)
			: base(eventInfo.Name, Array.Empty<Attribute>())
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.Format("Null is not a valid value for {0}.", "componentClass"));
			}
			this._componentClass = componentClass;
			this._realEvent = eventInfo;
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000710A8 File Offset: 0x0006F2A8
		public ReflectEventDescriptor(Type componentType, EventDescriptor oldReflectEventDescriptor, Attribute[] attributes)
			: base(oldReflectEventDescriptor, attributes)
		{
			this._componentClass = componentType;
			this._type = oldReflectEventDescriptor.EventType;
			ReflectEventDescriptor reflectEventDescriptor = oldReflectEventDescriptor as ReflectEventDescriptor;
			if (reflectEventDescriptor != null)
			{
				this._addMethod = reflectEventDescriptor._addMethod;
				this._removeMethod = reflectEventDescriptor._removeMethod;
				this._filledMethods = true;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x000710F9 File Offset: 0x0006F2F9
		public override Type ComponentType
		{
			get
			{
				return this._componentClass;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x00071101 File Offset: 0x0006F301
		public override Type EventType
		{
			get
			{
				this.FillMethods();
				return this._type;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x0007110F File Offset: 0x0006F30F
		public override bool IsMulticast
		{
			get
			{
				return typeof(MulticastDelegate).IsAssignableFrom(this.EventType);
			}
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x00071128 File Offset: 0x0006F328
		public override void AddEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					if (this.EventType != value.GetType())
					{
						throw new ArgumentException(SR.Format("Invalid event handler for the {0} event.", this.Name));
					}
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Combine(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase addMethod = this._addMethod;
					object[] array = new Delegate[] { value };
					addMethod.Invoke(component, array);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0007123C File Offset: 0x0006F43C
		protected override void FillAttributes(IList attributes)
		{
			this.FillMethods();
			if (this._realEvent != null)
			{
				this.FillEventInfoAttribute(this._realEvent, attributes);
			}
			else
			{
				this.FillSingleMethodAttribute(this._removeMethod, attributes);
				this.FillSingleMethodAttribute(this._addMethod, attributes);
			}
			base.FillAttributes(attributes);
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00071290 File Offset: 0x0006F490
		private void FillEventInfoAttribute(EventInfo realEventInfo, IList attributes)
		{
			string name = realEventInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realEventInfo.ReflectedType;
			int num = 0;
			while (type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realEventInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != typeof(object))
				{
					MemberInfo @event = type.GetEvent(name, bindingFlags);
					if (@event != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(@event);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0007136C File Offset: 0x0006F56C
		private void FillMethods()
		{
			if (this._filledMethods)
			{
				return;
			}
			if (this._realEvent != null)
			{
				this._addMethod = this._realEvent.GetAddMethod();
				this._removeMethod = this._realEvent.GetRemoveMethod();
				EventInfo eventInfo = null;
				if (this._addMethod == null || this._removeMethod == null)
				{
					Type baseType = this._componentClass.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
						EventInfo @event = baseType.GetEvent(this._realEvent.Name, bindingFlags);
						if (@event.GetAddMethod() != null)
						{
							eventInfo = @event;
							break;
						}
					}
				}
				if (eventInfo != null)
				{
					this._addMethod = eventInfo.GetAddMethod();
					this._removeMethod = eventInfo.GetRemoveMethod();
					this._type = eventInfo.EventHandlerType;
				}
				else
				{
					this._type = this._realEvent.EventHandlerType;
				}
			}
			else
			{
				this._realEvent = this._componentClass.GetEvent(this.Name);
				if (this._realEvent != null)
				{
					this.FillMethods();
					return;
				}
				Type[] array = new Type[] { this._type };
				this._addMethod = MemberDescriptor.FindMethod(this._componentClass, "AddOn" + this.Name, array, typeof(void));
				this._removeMethod = MemberDescriptor.FindMethod(this._componentClass, "RemoveOn" + this.Name, array, typeof(void));
				if (this._addMethod == null || this._removeMethod == null)
				{
					throw new ArgumentException(SR.Format("Accessor methods for the {0} event are missing.", this.Name));
				}
			}
			this._filledMethods = true;
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0007153C File Offset: 0x0006F73C
		private void FillSingleMethodAttribute(MethodInfo realMethodInfo, IList attributes)
		{
			string name = realMethodInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realMethodInfo.ReflectedType;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realMethodInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo method = type.GetMethod(name, bindingFlags);
					if (method != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(method);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0007162C File Offset: 0x0006F82C
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
					componentChangeService.OnComponentChanging(component, this);
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Remove(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					MethodBase removeMethod = this._removeMethod;
					object[] array = new Delegate[] { value };
					removeMethod.Invoke(component, array);
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x04000FFC RID: 4092
		private Type _type;

		// Token: 0x04000FFD RID: 4093
		private readonly Type _componentClass;

		// Token: 0x04000FFE RID: 4094
		private MethodInfo _addMethod;

		// Token: 0x04000FFF RID: 4095
		private MethodInfo _removeMethod;

		// Token: 0x04001000 RID: 4096
		private EventInfo _realEvent;

		// Token: 0x04001001 RID: 4097
		private bool _filledMethods;
	}
}
