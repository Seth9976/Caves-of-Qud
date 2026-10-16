using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200041F RID: 1055
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060021FD RID: 8701 RVA: 0x000743BC File Offset: 0x000725BC
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			try
			{
				if (type == null)
				{
					throw new ArgumentException(SR.GetString("Invalid type for the {0} property.", new object[] { name }));
				}
				if (componentClass == null)
				{
					throw new ArgumentException(SR.GetString("Null is not a valid value for {0}.", new object[] { "componentClass" }));
				}
				this.type = type;
				this.componentClass = componentClass;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00074440 File Offset: 0x00072640
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, PropertyInfo propInfo, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.propInfo = propInfo;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			if (getMethod != null && propInfo != null && setMethod == null)
			{
				this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetOnDemand] = true;
				return;
			}
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000744BD File Offset: 0x000726BD
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Type receiverType, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.receiverType = receiverType;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000744FC File Offset: 0x000726FC
		public ReflectPropertyDescriptor(Type componentClass, PropertyDescriptor oldReflectPropertyDescriptor, Attribute[] attributes)
			: base(oldReflectPropertyDescriptor, attributes)
		{
			this.componentClass = componentClass;
			this.type = oldReflectPropertyDescriptor.PropertyType;
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("Null is not a valid value for {0}.", new object[] { "componentClass" }));
			}
			ReflectPropertyDescriptor reflectPropertyDescriptor = oldReflectPropertyDescriptor as ReflectPropertyDescriptor;
			if (reflectPropertyDescriptor != null)
			{
				if (reflectPropertyDescriptor.ComponentType == componentClass)
				{
					this.propInfo = reflectPropertyDescriptor.propInfo;
					this.getMethod = reflectPropertyDescriptor.getMethod;
					this.setMethod = reflectPropertyDescriptor.setMethod;
					this.shouldSerializeMethod = reflectPropertyDescriptor.shouldSerializeMethod;
					this.resetMethod = reflectPropertyDescriptor.resetMethod;
					this.defaultValue = reflectPropertyDescriptor.defaultValue;
					this.ambientValue = reflectPropertyDescriptor.ambientValue;
					this.state = reflectPropertyDescriptor.state;
				}
				if (attributes != null)
				{
					foreach (Attribute attribute in attributes)
					{
						DefaultValueAttribute defaultValueAttribute = attribute as DefaultValueAttribute;
						if (defaultValueAttribute != null)
						{
							this.defaultValue = defaultValueAttribute.Value;
							if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
							{
								this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
							}
							this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
						}
						else
						{
							AmbientValueAttribute ambientValueAttribute = attribute as AmbientValueAttribute;
							if (ambientValueAttribute != null)
							{
								this.ambientValue = ambientValueAttribute.Value;
								this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x00074688 File Offset: 0x00072888
		private object AmbientValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitAmbientValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(AmbientValueAttribute)];
					if (attribute != null)
					{
						this.ambientValue = ((AmbientValueAttribute)attribute).Value;
					}
					else
					{
						this.ambientValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.ambientValue;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x000746F8 File Offset: 0x000728F8
		private EventDescriptor ChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitChangedQueried] = true;
					this.realChangedEvent = TypeDescriptor.GetEvents(this.ComponentType)[string.Format(CultureInfo.InvariantCulture, "{0}Changed", this.Name)];
				}
				return this.realChangedEvent;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x0007475C File Offset: 0x0007295C
		// (set) Token: 0x06002204 RID: 8708 RVA: 0x000747C8 File Offset: 0x000729C8
		private EventDescriptor IPropChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitIPropChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
					if (typeof(INotifyPropertyChanged).IsAssignableFrom(this.ComponentType))
					{
						this.realIPropChangedEvent = TypeDescriptor.GetEvents(typeof(INotifyPropertyChanged))["PropertyChanged"];
					}
				}
				return this.realIPropChangedEvent;
			}
			set
			{
				this.realIPropChangedEvent = value;
				this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x000747E2 File Offset: 0x000729E2
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000747EC File Offset: 0x000729EC
		private object DefaultValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitDefaultValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(DefaultValueAttribute)];
					if (attribute != null)
					{
						this.defaultValue = ((DefaultValueAttribute)attribute).Value;
						if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
						{
							this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
						}
					}
					else
					{
						this.defaultValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.defaultValue;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x000748A8 File Offset: 0x00072AA8
		private MethodInfo GetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitGetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitGetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.getMethod = this.propInfo.GetGetMethod(true);
						}
						if (this.getMethod == null)
						{
							throw new InvalidOperationException(SR.GetString("Accessor methods for the {0} property are missing.", new object[] { this.componentClass.FullName + "." + this.Name }));
						}
					}
					else
					{
						this.getMethod = MemberDescriptor.FindMethod(this.componentClass, "Get" + this.Name, new Type[] { this.receiverType }, this.type);
						if (this.getMethod == null)
						{
							throw new ArgumentException(SR.GetString("Accessor methods for the {0} property are missing.", new object[] { this.Name }));
						}
					}
				}
				return this.getMethod;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x000749F9 File Offset: 0x00072BF9
		private bool IsExtender
		{
			get
			{
				return this.receiverType != null;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x00074A07 File Offset: 0x00072C07
		public override bool IsReadOnly
		{
			get
			{
				return this.SetMethodValue == null || ((ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)]).IsReadOnly;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x0600220A RID: 8714 RVA: 0x00074A38 File Offset: 0x00072C38
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x00074A40 File Offset: 0x00072C40
		private MethodInfo ResetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitResetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitResetQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					this.resetMethod = MemberDescriptor.FindMethod(this.componentClass, "Reset" + this.Name, array, typeof(void), false);
				}
				return this.resetMethod;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x00074ACC File Offset: 0x00072CCC
		private MethodInfo SetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried] && this.state[ReflectPropertyDescriptor.BitSetOnDemand])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					string name = this.propInfo.Name;
					if (this.setMethod == null)
					{
						Type type = this.ComponentType.BaseType;
						while (type != null && type != typeof(object) && !(type == null))
						{
							PropertyInfo property = type.GetProperty(name, bindingFlags, null, this.PropertyType, new Type[0], null);
							if (property != null)
							{
								this.setMethod = property.GetSetMethod();
								if (this.setMethod != null)
								{
									break;
								}
							}
							type = type.BaseType;
						}
					}
				}
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags2 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags2, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.setMethod = this.propInfo.GetSetMethod(true);
						}
					}
					else
					{
						this.setMethod = MemberDescriptor.FindMethod(this.componentClass, "Set" + this.Name, new Type[] { this.receiverType, this.type }, typeof(void));
					}
				}
				return this.setMethod;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x00074C84 File Offset: 0x00072E84
		private MethodInfo ShouldSerializeMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried])
				{
					this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					this.shouldSerializeMethod = MemberDescriptor.FindMethod(this.componentClass, "ShouldSerialize" + this.Name, array, typeof(bool), false);
				}
				return this.shouldSerializeMethod;
			}
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00074D10 File Offset: 0x00072F10
		public override void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.AddEventHandler(component, handler);
				return;
			}
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.AddEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
			base.AddValueChanged(component, handler);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00074D88 File Offset: 0x00072F88
		internal bool ExtenderCanResetValue(IExtenderProvider provider, object component)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.ExtenderGetValue(provider, component), this.defaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				MethodInfo shouldSerializeMethodValue = this.ShouldSerializeMethodValue;
				if (shouldSerializeMethodValue != null)
				{
					try
					{
						provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
						return (bool)shouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00074E1C File Offset: 0x0007301C
		internal Type ExtenderGetReceiverType()
		{
			return this.receiverType;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00074E24 File Offset: 0x00073024
		internal Type ExtenderGetType(IExtenderProvider provider)
		{
			return this.PropertyType;
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00074E2C File Offset: 0x0007302C
		internal object ExtenderGetValue(IExtenderProvider provider, object component)
		{
			if (provider != null)
			{
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				return this.GetMethodValue.Invoke(provider, new object[] { component });
			}
			return null;
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00074E60 File Offset: 0x00073060
		internal void ExtenderResetValue(IExtenderProvider provider, object component, PropertyDescriptor notifyDesc)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.DefaultValue, notifyDesc);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.AmbientValue, notifyDesc);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.ResetMethodValue != null)
				{
					this.ResetMethodValue.Invoke(provider, new object[] { component });
					if (componentChangeService != null)
					{
						object obj2 = this.ExtenderGetValue(provider, component);
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, obj2);
					}
				}
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00074F64 File Offset: 0x00073164
		internal void ExtenderSetValue(IExtenderProvider provider, object component, object value, PropertyDescriptor notifyDesc)
		{
			if (provider != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.SetMethodValue != null)
				{
					this.SetMethodValue.Invoke(provider, new object[] { component, value });
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, value);
					}
				}
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0007501C File Offset: 0x0007321C
		internal bool ExtenderShouldSerializeValue(IExtenderProvider provider, object component)
		{
			provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.ExtenderGetValue(provider, component));
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000750F8 File Offset: 0x000732F8
		public override bool CanResetValue(object component)
		{
			if (this.IsExtender || this.IsReadOnly)
			{
				return false;
			}
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.GetValue(component), this.DefaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					component = this.GetInvocationTarget(this.componentClass, component);
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return this.AmbientValue != ReflectPropertyDescriptor.noValue && this.ShouldSerializeValue(component);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000751A8 File Offset: 0x000733A8
		protected override void FillAttributes(IList attributes)
		{
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute attribute = (Attribute)obj;
				attributes.Add(attribute);
			}
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Type type = this.componentClass;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = this.componentClass;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo memberInfo;
					if (this.IsExtender)
					{
						memberInfo = type.GetMethod("Get" + this.Name, bindingFlags, null, new Type[] { this.receiverType }, null);
					}
					else
					{
						memberInfo = type.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
					}
					if (memberInfo != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(memberInfo);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						Attribute[] array4 = array3;
						for (int j = 0; j < array4.Length; j++)
						{
							AttributeProviderAttribute attributeProviderAttribute = array4[j] as AttributeProviderAttribute;
							if (attributeProviderAttribute != null)
							{
								Type type2 = Type.GetType(attributeProviderAttribute.TypeName);
								if (type2 != null)
								{
									Attribute[] array5 = null;
									if (!string.IsNullOrEmpty(attributeProviderAttribute.PropertyName))
									{
										MemberInfo[] member = type2.GetMember(attributeProviderAttribute.PropertyName);
										if (member.Length != 0 && member[0] != null)
										{
											array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(member[0]);
										}
									}
									else
									{
										array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type2);
									}
									if (array5 != null)
									{
										foreach (Attribute attribute2 in array5)
										{
											attributes.Add(attribute2);
										}
									}
								}
							}
						}
					}
				}
				foreach (Attribute[] array7 in array)
				{
					if (array7 != null)
					{
						foreach (Attribute attribute3 in array7)
						{
							attributes.Add(attribute3);
						}
					}
				}
			}
			base.FillAttributes(attributes);
			if (this.SetMethodValue == null)
			{
				attributes.Add(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00075440 File Offset: 0x00073640
		public override object GetValue(object component)
		{
			if (this.IsExtender)
			{
				return null;
			}
			if (component != null)
			{
				component = this.GetInvocationTarget(this.componentClass, component);
				try
				{
					return SecurityUtils.MethodInfoInvoke(this.GetMethodValue, component, null);
				}
				catch (Exception innerException)
				{
					string text = null;
					IComponent component2 = component as IComponent;
					if (component2 != null)
					{
						ISite site = component2.Site;
						if (site != null && site.Name != null)
						{
							text = site.Name;
						}
					}
					if (text == null)
					{
						text = component.GetType().FullName;
					}
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					string text2 = innerException.Message;
					if (text2 == null)
					{
						text2 = innerException.GetType().Name;
					}
					throw new TargetInvocationException(SR.GetString("Property accessor '{0}' on object '{1}' threw the following exception:'{2}'", new object[] { this.Name, text, text2 }), innerException);
				}
			}
			return null;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x0007551C File Offset: 0x0007371C
		internal void OnINotifyPropertyChanged(object component, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.PropertyName) || string.Compare(e.PropertyName, this.Name, true, CultureInfo.InvariantCulture) == 0)
			{
				this.OnValueChanged(component, e);
			}
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0007554C File Offset: 0x0007374C
		protected override void OnValueChanged(object component, EventArgs e)
		{
			if (this.state[ReflectPropertyDescriptor.BitChangedQueried] && this.realChangedEvent == null)
			{
				base.OnValueChanged(component, e);
			}
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00075570 File Offset: 0x00073770
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.RemoveEventHandler(component, handler);
				return;
			}
			base.RemoveValueChanged(component, handler);
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.RemoveEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x000755E8 File Offset: 0x000737E8
		public override void ResetValue(object component)
		{
			object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.DefaultValue);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.AmbientValue);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
				}
				if (this.ResetMethodValue != null)
				{
					SecurityUtils.MethodInfoInvoke(this.ResetMethodValue, invocationTarget, null);
					if (componentChangeService != null)
					{
						object obj2 = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						componentChangeService.OnComponentChanged(component, this, obj, obj2);
					}
				}
			}
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000756E4 File Offset: 0x000738E4
		public override void SetValue(object component, object value)
		{
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
				if (!this.IsReadOnly)
				{
					if (site != null)
					{
						componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					}
					if (componentChangeService != null)
					{
						obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
					}
					try
					{
						SecurityUtils.MethodInfoInvoke(this.SetMethodValue, invocationTarget, new object[] { value });
						this.OnValueChanged(invocationTarget, EventArgs.Empty);
					}
					catch (Exception ex2)
					{
						value = obj;
						if (ex2 is TargetInvocationException && ex2.InnerException != null)
						{
							throw ex2.InnerException;
						}
						throw ex2;
					}
					finally
					{
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(component, this, obj, value);
						}
					}
				}
			}
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x000757E0 File Offset: 0x000739E0
		public override bool ShouldSerializeValue(object component)
		{
			component = this.GetInvocationTarget(this.componentClass, component);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.GetValue(component));
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x000758A4 File Offset: 0x00073AA4
		public override bool SupportsChangeEvents
		{
			get
			{
				return this.IPropChangedEventValue != null || this.ChangedEventValue != null;
			}
		}

		// Token: 0x0400105B RID: 4187
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x0400105C RID: 4188
		private static readonly object noValue = new object();

		// Token: 0x0400105D RID: 4189
		private static TraceSwitch PropDescCreateSwitch = new TraceSwitch("PropDescCreate", "ReflectPropertyDescriptor: Dump errors when creating property info");

		// Token: 0x0400105E RID: 4190
		private static TraceSwitch PropDescUsageSwitch = new TraceSwitch("PropDescUsage", "ReflectPropertyDescriptor: Debug propertydescriptor usage");

		// Token: 0x0400105F RID: 4191
		private static readonly int BitDefaultValueQueried = BitVector32.CreateMask();

		// Token: 0x04001060 RID: 4192
		private static readonly int BitGetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitDefaultValueQueried);

		// Token: 0x04001061 RID: 4193
		private static readonly int BitSetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitGetQueried);

		// Token: 0x04001062 RID: 4194
		private static readonly int BitShouldSerializeQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitSetQueried);

		// Token: 0x04001063 RID: 4195
		private static readonly int BitResetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitShouldSerializeQueried);

		// Token: 0x04001064 RID: 4196
		private static readonly int BitChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitResetQueried);

		// Token: 0x04001065 RID: 4197
		private static readonly int BitIPropChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitChangedQueried);

		// Token: 0x04001066 RID: 4198
		private static readonly int BitReadOnlyChecked = BitVector32.CreateMask(ReflectPropertyDescriptor.BitIPropChangedQueried);

		// Token: 0x04001067 RID: 4199
		private static readonly int BitAmbientValueQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitReadOnlyChecked);

		// Token: 0x04001068 RID: 4200
		private static readonly int BitSetOnDemand = BitVector32.CreateMask(ReflectPropertyDescriptor.BitAmbientValueQueried);

		// Token: 0x04001069 RID: 4201
		private BitVector32 state;

		// Token: 0x0400106A RID: 4202
		private Type componentClass;

		// Token: 0x0400106B RID: 4203
		private Type type;

		// Token: 0x0400106C RID: 4204
		private object defaultValue;

		// Token: 0x0400106D RID: 4205
		private object ambientValue;

		// Token: 0x0400106E RID: 4206
		private PropertyInfo propInfo;

		// Token: 0x0400106F RID: 4207
		private MethodInfo getMethod;

		// Token: 0x04001070 RID: 4208
		private MethodInfo setMethod;

		// Token: 0x04001071 RID: 4209
		private MethodInfo shouldSerializeMethod;

		// Token: 0x04001072 RID: 4210
		private MethodInfo resetMethod;

		// Token: 0x04001073 RID: 4211
		private EventDescriptor realChangedEvent;

		// Token: 0x04001074 RID: 4212
		private EventDescriptor realIPropChangedEvent;

		// Token: 0x04001075 RID: 4213
		private Type receiverType;
	}
}
