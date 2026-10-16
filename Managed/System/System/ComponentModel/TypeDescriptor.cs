using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides information about the characteristics for a component, such as its attributes, properties, and events. This class cannot be inherited.</summary>
	// Token: 0x02000427 RID: 1063
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class TypeDescriptor
	{
		// Token: 0x0600229E RID: 8862 RVA: 0x0000219B File Offset: 0x0000039B
		private TypeDescriptor()
		{
		}

		/// <summary>Gets or sets the provider for the Component Object Model (COM) type information for the target component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComNativeDescriptorHandler" /> instance representing the COM type information provider.</returns>
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x00077D88 File Offset: 0x00075F88
		// (set) Token: 0x060022A0 RID: 8864 RVA: 0x00077DC8 File Offset: 0x00075FC8
		[Obsolete("This property has been deprecated.  Use a type description provider to supply type information for COM types instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IComNativeDescriptorHandler ComNativeDescriptorHandler
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(TypeDescriptor.ComObjectType);
				TypeDescriptor.ComNativeDescriptionProvider comNativeDescriptionProvider;
				do
				{
					comNativeDescriptionProvider = typeDescriptionNode.Provider as TypeDescriptor.ComNativeDescriptionProvider;
					typeDescriptionNode = typeDescriptionNode.Next;
				}
				while (typeDescriptionNode != null && comNativeDescriptionProvider == null);
				if (comNativeDescriptionProvider != null)
				{
					return comNativeDescriptionProvider.Handler;
				}
				return null;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(TypeDescriptor.ComObjectType);
				while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is TypeDescriptor.ComNativeDescriptionProvider))
				{
					typeDescriptionNode = typeDescriptionNode.Next;
				}
				if (typeDescriptionNode == null)
				{
					TypeDescriptor.AddProvider(new TypeDescriptor.ComNativeDescriptionProvider(value), TypeDescriptor.ComObjectType);
					return;
				}
				((TypeDescriptor.ComNativeDescriptionProvider)typeDescriptionNode.Provider).Handler = value;
			}
		}

		/// <summary>Gets the type of the Component Object Model (COM) object represented by the target component.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the COM object represented by this component, or null for non-COM objects.</returns>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x00077E1E File Offset: 0x0007601E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type ComObjectType
		{
			get
			{
				return typeof(TypeDescriptor.TypeDescriptorComObject);
			}
		}

		/// <summary>Gets a type that represents a type description provider for all interface types. </summary>
		/// <returns>A <see cref="T:System.Type" /> that represents a custom type description provider for all interface types. </returns>
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x00077E2A File Offset: 0x0007602A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type InterfaceType
		{
			get
			{
				return typeof(TypeDescriptor.TypeDescriptorInterface);
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x00077E36 File Offset: 0x00076036
		internal static int MetadataVersion
		{
			get
			{
				return TypeDescriptor._metadataVersion;
			}
		}

		/// <summary>Occurs when the cache for a component is cleared.</summary>
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060022A4 RID: 8868 RVA: 0x00077E40 File Offset: 0x00076040
		// (remove) Token: 0x060022A5 RID: 8869 RVA: 0x00077E74 File Offset: 0x00076074
		public static event RefreshEventHandler Refreshed;

		/// <summary>Adds class-level attributes to the target component type.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that was used to add the specified attributes.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects to add to the component's class.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters is null.</exception>
		// Token: 0x060022A6 RID: 8870 RVA: 0x00077EA7 File Offset: 0x000760A7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static TypeDescriptionProvider AddAttributes(Type type, params Attribute[] attributes)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			TypeDescriptor.AttributeProvider attributeProvider = new TypeDescriptor.AttributeProvider(TypeDescriptor.GetProvider(type), attributes);
			TypeDescriptor.AddProvider(attributeProvider, type);
			return attributeProvider;
		}

		/// <summary>Adds class-level attributes to the target component instance.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that was used to add the specified attributes.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects to add to the component's class.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters is null.</exception>
		// Token: 0x060022A7 RID: 8871 RVA: 0x00077EDE File Offset: 0x000760DE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static TypeDescriptionProvider AddAttributes(object instance, params Attribute[] attributes)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			TypeDescriptor.AttributeProvider attributeProvider = new TypeDescriptor.AttributeProvider(TypeDescriptor.GetProvider(instance), attributes);
			TypeDescriptor.AddProvider(attributeProvider, instance);
			return attributeProvider;
		}

		/// <summary>Adds an editor table for the given editor base type. </summary>
		/// <param name="editorBaseType">The editor base type to add the editor table for. If a table already exists for this type, this method will do nothing. </param>
		/// <param name="table">The <see cref="T:System.Collections.Hashtable" /> to add. </param>
		// Token: 0x060022A8 RID: 8872 RVA: 0x00077F0F File Offset: 0x0007610F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddEditorTable(Type editorBaseType, Hashtable table)
		{
			ReflectTypeDescriptionProvider.AddEditorTable(editorBaseType, table);
		}

		/// <summary>Adds a type description provider for a component class.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060022A9 RID: 8873 RVA: 0x00077F18 File Offset: 0x00076118
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void AddProvider(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(type, true);
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode2 = new TypeDescriptor.TypeDescriptionNode(provider);
				typeDescriptionNode2.Next = typeDescriptionNode;
				TypeDescriptor._providerTable[type] = typeDescriptionNode2;
				TypeDescriptor._providerTypeTable.Clear();
			}
			TypeDescriptor.Refresh(type);
		}

		/// <summary>Adds a type description provider for a single instance of a component.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060022AA RID: 8874 RVA: 0x00077FA8 File Offset: 0x000761A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void AddProvider(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			bool flag2;
			lock (providerTable)
			{
				flag2 = TypeDescriptor._providerTable.ContainsKey(instance);
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(instance, true);
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode2 = new TypeDescriptor.TypeDescriptionNode(provider);
				typeDescriptionNode2.Next = typeDescriptionNode;
				TypeDescriptor._providerTable.SetWeak(instance, typeDescriptionNode2);
				TypeDescriptor._providerTypeTable.Clear();
			}
			if (flag2)
			{
				TypeDescriptor.Refresh(instance, false);
			}
		}

		/// <summary>Adds a type description provider for a component class.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060022AB RID: 8875 RVA: 0x00078044 File Offset: 0x00076244
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddProviderTransparent(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TypeDescriptor.AddProvider(provider, type);
		}

		/// <summary>Adds a type description provider for a single instance of a component.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060022AC RID: 8876 RVA: 0x0007806F File Offset: 0x0007626F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddProviderTransparent(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			TypeDescriptor.AddProvider(provider, instance);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x00078094 File Offset: 0x00076294
		private static void CheckDefaultProvider(Type type)
		{
			object obj;
			if (TypeDescriptor._defaultProviders == null)
			{
				obj = TypeDescriptor._internalSyncObject;
				lock (obj)
				{
					if (TypeDescriptor._defaultProviders == null)
					{
						TypeDescriptor._defaultProviders = new Hashtable();
					}
				}
			}
			if (TypeDescriptor._defaultProviders.ContainsKey(type))
			{
				return;
			}
			obj = TypeDescriptor._internalSyncObject;
			lock (obj)
			{
				if (TypeDescriptor._defaultProviders.ContainsKey(type))
				{
					return;
				}
				TypeDescriptor._defaultProviders[type] = null;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeDescriptionProviderAttribute), false);
			bool flag2 = false;
			for (int i = customAttributes.Length - 1; i >= 0; i--)
			{
				Type type2 = Type.GetType(((TypeDescriptionProviderAttribute)customAttributes[i]).TypeName);
				if (type2 != null && typeof(TypeDescriptionProvider).IsAssignableFrom(type2))
				{
					TypeDescriptor.AddProvider((TypeDescriptionProvider)Activator.CreateInstance(type2), type);
					flag2 = true;
				}
			}
			if (!flag2)
			{
				Type baseType = type.BaseType;
				if (baseType != null && baseType != type)
				{
					TypeDescriptor.CheckDefaultProvider(baseType);
				}
			}
		}

		/// <summary>Creates a primary-secondary association between two objects.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" />.</param>
		/// <param name="secondary">The secondary <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="primary" /> is equal to <paramref name="secondary" />.</exception>
		// Token: 0x060022AE RID: 8878 RVA: 0x000781DC File Offset: 0x000763DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void CreateAssociation(object primary, object secondary)
		{
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			if (secondary == null)
			{
				throw new ArgumentNullException("secondary");
			}
			if (primary == secondary)
			{
				throw new ArgumentException(SR.GetString("Cannot create an association when the primary and secondary objects are the same."));
			}
			if (TypeDescriptor._associationTable == null)
			{
				object internalSyncObject = TypeDescriptor._internalSyncObject;
				lock (internalSyncObject)
				{
					if (TypeDescriptor._associationTable == null)
					{
						TypeDescriptor._associationTable = new WeakHashtable();
					}
				}
			}
			IList list = (IList)TypeDescriptor._associationTable[primary];
			if (list == null)
			{
				WeakHashtable associationTable = TypeDescriptor._associationTable;
				lock (associationTable)
				{
					list = (IList)TypeDescriptor._associationTable[primary];
					if (list == null)
					{
						list = new ArrayList(4);
						TypeDescriptor._associationTable.SetWeak(primary, list);
					}
					goto IL_0112;
				}
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				WeakReference weakReference = (WeakReference)list[i];
				if (weakReference.IsAlive && weakReference.Target == secondary)
				{
					throw new ArgumentException(SR.GetString("The primary and secondary objects are already associated with each other."));
				}
			}
			IL_0112:
			IList list2 = list;
			lock (list2)
			{
				list.Add(new WeakReference(secondary));
			}
		}

		/// <summary>Creates an instance of the designer associated with the specified component and of the specified type of designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> that is an instance of the designer for the component, or null if no designer can be found.</returns>
		/// <param name="component">An <see cref="T:System.ComponentModel.IComponent" /> that specifies the component to associate with the designer. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the type of designer to create. </param>
		// Token: 0x060022AF RID: 8879 RVA: 0x0007834C File Offset: 0x0007654C
		public static IDesigner CreateDesigner(IComponent component, Type designerBaseType)
		{
			Type type = null;
			IDesigner designer = null;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(component);
			for (int i = 0; i < attributes.Count; i++)
			{
				DesignerAttribute designerAttribute = attributes[i] as DesignerAttribute;
				if (designerAttribute != null)
				{
					Type type2 = Type.GetType(designerAttribute.DesignerBaseTypeName);
					if (type2 != null && type2 == designerBaseType)
					{
						ISite site = component.Site;
						bool flag = false;
						if (site != null)
						{
							ITypeResolutionService typeResolutionService = (ITypeResolutionService)site.GetService(typeof(ITypeResolutionService));
							if (typeResolutionService != null)
							{
								flag = true;
								type = typeResolutionService.GetType(designerAttribute.DesignerTypeName);
							}
						}
						if (!flag)
						{
							type = Type.GetType(designerAttribute.DesignerTypeName);
						}
						if (type != null)
						{
							break;
						}
					}
				}
			}
			if (type != null)
			{
				designer = (IDesigner)SecurityUtils.SecureCreateInstance(type, null, true);
			}
			return designer;
		}

		/// <summary>Creates a new event descriptor that is identical to an existing event descriptor by dynamically generating descriptor information from a specified event on a type.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that is bound to a type.</returns>
		/// <param name="componentType">The type of the component the event lives on. </param>
		/// <param name="name">The name of the event. </param>
		/// <param name="type">The type of the delegate that handles the event. </param>
		/// <param name="attributes">The attributes for this event. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060022B0 RID: 8880 RVA: 0x0007841E File Offset: 0x0007661E
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static EventDescriptor CreateEvent(Type componentType, string name, Type type, params Attribute[] attributes)
		{
			return new ReflectEventDescriptor(componentType, name, type, attributes);
		}

		/// <summary>Creates a new event descriptor that is identical to an existing event descriptor, when passed the existing <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.EventDescriptor" /> that has merged the specified metadata attributes with the existing metadata attributes.</returns>
		/// <param name="componentType">The type of the component for which to create the new event. </param>
		/// <param name="oldEventDescriptor">The existing event information. </param>
		/// <param name="attributes">The new attributes. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060022B1 RID: 8881 RVA: 0x00078429 File Offset: 0x00076629
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static EventDescriptor CreateEvent(Type componentType, EventDescriptor oldEventDescriptor, params Attribute[] attributes)
		{
			return new ReflectEventDescriptor(componentType, oldEventDescriptor, attributes);
		}

		/// <summary>Creates an object that can substitute for another data type. </summary>
		/// <returns>An instance of the substitute data type if an associated <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> is found; otherwise, null.</returns>
		/// <param name="provider">The service provider that provides a <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> service. This parameter can be null.</param>
		/// <param name="objectType">The <see cref="T:System.Type" /> of object to create.</param>
		/// <param name="argTypes">An optional array of parameter types to be passed to the object's constructor. This parameter can be null or an array of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor. If not null, the number of elements must be the same as <paramref name="argTypes" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is null, or <paramref name="args" /> is null when <paramref name="argTypes" /> is not null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="argTypes" /> and <paramref name="args" /> have different number of elements.</exception>
		// Token: 0x060022B2 RID: 8882 RVA: 0x00078434 File Offset: 0x00076634
		public static object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			if (argTypes != null)
			{
				if (args == null)
				{
					throw new ArgumentNullException("args");
				}
				if (argTypes.Length != args.Length)
				{
					throw new ArgumentException(SR.GetString("The number of elements in the Type and Object arrays must match."));
				}
			}
			object obj = null;
			if (provider != null)
			{
				TypeDescriptionProvider typeDescriptionProvider = provider.GetService(typeof(TypeDescriptionProvider)) as TypeDescriptionProvider;
				if (typeDescriptionProvider != null)
				{
					obj = typeDescriptionProvider.CreateInstance(provider, objectType, argTypes, args);
				}
			}
			if (obj == null)
			{
				obj = TypeDescriptor.NodeFor(objectType).CreateInstance(provider, objectType, argTypes, args);
			}
			return obj;
		}

		/// <summary>Creates and dynamically binds a property descriptor to a type, using the specified property name, type, and attribute array.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is bound to the specified type and that has the specified metadata attributes merged with the existing metadata attributes.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the component that the property is a member of. </param>
		/// <param name="name">The name of the property. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the property. </param>
		/// <param name="attributes">The new attributes for this property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060022B3 RID: 8883 RVA: 0x000784BB File Offset: 0x000766BB
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static PropertyDescriptor CreateProperty(Type componentType, string name, Type type, params Attribute[] attributes)
		{
			return new ReflectPropertyDescriptor(componentType, name, type, attributes);
		}

		/// <summary>Creates a new property descriptor from an existing property descriptor, using the specified existing <see cref="T:System.ComponentModel.PropertyDescriptor" /> and attribute array.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptor" /> that has the specified metadata attributes merged with the existing metadata attributes.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the component that the property is a member of. </param>
		/// <param name="oldPropertyDescriptor">The existing property descriptor. </param>
		/// <param name="attributes">The new attributes for this property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060022B4 RID: 8884 RVA: 0x000784C8 File Offset: 0x000766C8
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static PropertyDescriptor CreateProperty(Type componentType, PropertyDescriptor oldPropertyDescriptor, params Attribute[] attributes)
		{
			if (componentType == oldPropertyDescriptor.ComponentType && ((ExtenderProvidedPropertyAttribute)oldPropertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)]).ExtenderProperty is ReflectPropertyDescriptor)
			{
				return new ExtendedPropertyDescriptor(oldPropertyDescriptor, attributes);
			}
			return new ReflectPropertyDescriptor(componentType, oldPropertyDescriptor, attributes);
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(Type type, AttributeCollection attributes, AttributeCollection debugAttributes)
		{
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(AttributeCollection attributes, AttributeCollection debugAttributes)
		{
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(AttributeCollection attributes, Type type)
		{
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(AttributeCollection attributes, object instance, bool noCustomTypeDesc)
		{
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(TypeConverter converter, Type type)
		{
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(TypeConverter converter, object instance, bool noCustomTypeDesc)
		{
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(EventDescriptorCollection events, Type type, Attribute[] attributes)
		{
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(EventDescriptorCollection events, object instance, Attribute[] attributes, bool noCustomTypeDesc)
		{
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(PropertyDescriptorCollection properties, Type type, Attribute[] attributes)
		{
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		private static void DebugValidate(PropertyDescriptorCollection properties, object instance, Attribute[] attributes, bool noCustomTypeDesc)
		{
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x0007851C File Offset: 0x0007671C
		private static ArrayList FilterMembers(IList members, Attribute[] attributes)
		{
			ArrayList arrayList = null;
			int count = members.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag = false;
				for (int j = 0; j < attributes.Length; j++)
				{
					if (TypeDescriptor.ShouldHideMember((MemberDescriptor)members[i], attributes[j]))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList(count);
						for (int k = 0; k < i; k++)
						{
							arrayList.Add(members[k]);
						}
					}
				}
				else if (arrayList != null)
				{
					arrayList.Add(members[i]);
				}
			}
			return arrayList;
		}

		/// <summary>Returns an instance of the type associated with the specified primary object.</summary>
		/// <returns>An instance of the secondary type that has been associated with the primary object if an association exists; otherwise, <paramref name="primary" /> if no specified association exists.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="primary">The primary object of the association.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060022C0 RID: 8896 RVA: 0x000785B0 File Offset: 0x000767B0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static object GetAssociation(Type type, object primary)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			object obj = primary;
			if (!type.IsInstanceOfType(primary))
			{
				Hashtable associationTable = TypeDescriptor._associationTable;
				if (associationTable != null)
				{
					IList list = (IList)associationTable[primary];
					if (list != null)
					{
						IList list2 = list;
						lock (list2)
						{
							for (int i = list.Count - 1; i >= 0; i--)
							{
								object target = ((WeakReference)list[i]).Target;
								if (target == null)
								{
									list.RemoveAt(i);
								}
								else if (type.IsInstanceOfType(target))
								{
									obj = target;
								}
							}
						}
					}
				}
				if (obj == primary)
				{
					IComponent component = primary as IComponent;
					if (component != null)
					{
						ISite site = component.Site;
						if (site != null && site.DesignMode)
						{
							IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
							if (designerHost != null)
							{
								object designer = designerHost.GetDesigner(component);
								if (designer != null && type.IsInstanceOfType(designer))
								{
									obj = designer;
								}
							}
						}
					}
				}
			}
			return obj;
		}

		/// <summary>Returns a collection of attributes for the specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the type of the component. If the component is null, this method returns an empty collection.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component. </param>
		// Token: 0x060022C1 RID: 8897 RVA: 0x000786D8 File Offset: 0x000768D8
		public static AttributeCollection GetAttributes(Type componentType)
		{
			if (componentType == null)
			{
				return new AttributeCollection(null);
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetAttributes();
		}

		/// <summary>Returns the collection of attributes for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the component. If <paramref name="component" /> is null, this method returns an empty collection.</returns>
		/// <param name="component">The component for which you want to get attributes. </param>
		// Token: 0x060022C2 RID: 8898 RVA: 0x000786FA File Offset: 0x000768FA
		public static AttributeCollection GetAttributes(object component)
		{
			return TypeDescriptor.GetAttributes(component, false);
		}

		/// <summary>Returns a collection of attributes for the specified component and a Boolean indicating that a custom type descriptor has been created.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the component. If the component is null, this method returns an empty collection.</returns>
		/// <param name="component">The component for which you want to get attributes. </param>
		/// <param name="noCustomTypeDesc">true to use a baseline set of attributes from the custom type descriptor if <paramref name="component" /> is of type <see cref="T:System.ComponentModel.ICustomTypeDescriptor" />; otherwise, false.</param>
		// Token: 0x060022C3 RID: 8899 RVA: 0x00078704 File Offset: 0x00076904
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static AttributeCollection GetAttributes(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return new AttributeCollection(null);
			}
			ICollection collection = TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetAttributes();
			if (component is ICustomTypeDescriptor)
			{
				if (noCustomTypeDesc)
				{
					ICustomTypeDescriptor extendedDescriptor = TypeDescriptor.GetExtendedDescriptor(component);
					if (extendedDescriptor != null)
					{
						ICollection attributes = extendedDescriptor.GetAttributes();
						collection = TypeDescriptor.PipelineMerge(0, collection, attributes, component, null);
					}
				}
				else
				{
					collection = TypeDescriptor.PipelineFilter(0, collection, component, null);
				}
			}
			else
			{
				IDictionary cache = TypeDescriptor.GetCache(component);
				collection = TypeDescriptor.PipelineInitialize(0, collection, cache);
				ICustomTypeDescriptor extendedDescriptor2 = TypeDescriptor.GetExtendedDescriptor(component);
				if (extendedDescriptor2 != null)
				{
					ICollection attributes2 = extendedDescriptor2.GetAttributes();
					collection = TypeDescriptor.PipelineMerge(0, collection, attributes2, component, cache);
				}
				collection = TypeDescriptor.PipelineFilter(0, collection, component, cache);
			}
			AttributeCollection attributeCollection = collection as AttributeCollection;
			if (attributeCollection == null)
			{
				Attribute[] array = new Attribute[collection.Count];
				collection.CopyTo(array, 0);
				attributeCollection = new AttributeCollection(array);
			}
			return attributeCollection;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000787C5 File Offset: 0x000769C5
		internal static IDictionary GetCache(object instance)
		{
			return TypeDescriptor.NodeFor(instance).GetCache(instance);
		}

		/// <summary>Returns the name of the class for the specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x060022C5 RID: 8901 RVA: 0x000787D3 File Offset: 0x000769D3
		public static string GetClassName(object component)
		{
			return TypeDescriptor.GetClassName(component, false);
		}

		/// <summary>Returns the name of the class for the specified component using a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022C6 RID: 8902 RVA: 0x000787DC File Offset: 0x000769DC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static string GetClassName(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetClassName();
		}

		/// <summary>Returns the name of the class for the specified type.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component type.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="componentType" /> is null.</exception>
		// Token: 0x060022C7 RID: 8903 RVA: 0x000787EA File Offset: 0x000769EA
		public static string GetClassName(Type componentType)
		{
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetClassName();
		}

		/// <summary>Returns the name of the specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the specified component, or null if there is no component name.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022C8 RID: 8904 RVA: 0x000787FC File Offset: 0x000769FC
		public static string GetComponentName(object component)
		{
			return TypeDescriptor.GetComponentName(component, false);
		}

		/// <summary>Returns the name of the specified component using a custom type descriptor.</summary>
		/// <returns>The name of the class for the specified component, or null if there is no component name.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022C9 RID: 8905 RVA: 0x00078805 File Offset: 0x00076A05
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static string GetComponentName(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetComponentName();
		}

		/// <summary>Returns a type converter for the type of the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified component.</returns>
		/// <param name="component">A component to get the converter for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022CA RID: 8906 RVA: 0x00078813 File Offset: 0x00076A13
		public static TypeConverter GetConverter(object component)
		{
			return TypeDescriptor.GetConverter(component, false);
		}

		/// <summary>Returns a type converter for the type of the specified component with a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified component.</returns>
		/// <param name="component">A component to get the converter for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022CB RID: 8907 RVA: 0x0007881C File Offset: 0x00076A1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeConverter GetConverter(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetConverter();
		}

		/// <summary>Returns a type converter for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x060022CC RID: 8908 RVA: 0x0007882A File Offset: 0x00076A2A
		public static TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetDescriptor(type, "type").GetConverter();
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x0007883C File Offset: 0x00076A3C
		private static object ConvertFromInvariantString(Type type, string stringValue)
		{
			return TypeDescriptor.GetConverter(type).ConvertFromInvariantString(stringValue);
		}

		/// <summary>Returns the default event for the specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x060022CE RID: 8910 RVA: 0x0007884A File Offset: 0x00076A4A
		public static EventDescriptor GetDefaultEvent(Type componentType)
		{
			if (componentType == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetDefaultEvent();
		}

		/// <summary>Returns the default event for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="component">The component to get the event for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022CF RID: 8911 RVA: 0x00078867 File Offset: 0x00076A67
		public static EventDescriptor GetDefaultEvent(object component)
		{
			return TypeDescriptor.GetDefaultEvent(component, false);
		}

		/// <summary>Returns the default event for a component with a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="component">The component to get the event for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022D0 RID: 8912 RVA: 0x00078870 File Offset: 0x00076A70
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptor GetDefaultEvent(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetDefaultEvent();
		}

		/// <summary>Returns the default property for the specified type of component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="componentType">A <see cref="T:System.Type" /> that represents the class to get the property for. </param>
		// Token: 0x060022D1 RID: 8913 RVA: 0x00078883 File Offset: 0x00076A83
		public static PropertyDescriptor GetDefaultProperty(Type componentType)
		{
			if (componentType == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetDefaultProperty();
		}

		/// <summary>Returns the default property for the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="component">The component to get the default property for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022D2 RID: 8914 RVA: 0x000788A0 File Offset: 0x00076AA0
		public static PropertyDescriptor GetDefaultProperty(object component)
		{
			return TypeDescriptor.GetDefaultProperty(component, false);
		}

		/// <summary>Returns the default property for the specified component with a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="component">The component to get the default property for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022D3 RID: 8915 RVA: 0x000788A9 File Offset: 0x00076AA9
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static PropertyDescriptor GetDefaultProperty(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetDefaultProperty();
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x000788BC File Offset: 0x00076ABC
		internal static ICustomTypeDescriptor GetDescriptor(Type type, string typeName)
		{
			if (type == null)
			{
				throw new ArgumentNullException(typeName);
			}
			return TypeDescriptor.NodeFor(type).GetTypeDescriptor(type);
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000788DC File Offset: 0x00076ADC
		internal static ICustomTypeDescriptor GetDescriptor(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				throw new ArgumentException("component");
			}
			if (component is TypeDescriptor.IUnimplemented)
			{
				throw new NotSupportedException(SR.GetString("The object {0} is being remoted by a proxy that does not support interface discovery.  This type of remoted object is not supported.", new object[] { component.GetType().FullName }));
			}
			ICustomTypeDescriptor customTypeDescriptor = TypeDescriptor.NodeFor(component).GetTypeDescriptor(component);
			ICustomTypeDescriptor customTypeDescriptor2 = component as ICustomTypeDescriptor;
			if (!noCustomTypeDesc && customTypeDescriptor2 != null)
			{
				customTypeDescriptor = new TypeDescriptor.MergedTypeDescriptor(customTypeDescriptor2, customTypeDescriptor);
			}
			return customTypeDescriptor;
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00078946 File Offset: 0x00076B46
		internal static ICustomTypeDescriptor GetExtendedDescriptor(object component)
		{
			if (component == null)
			{
				throw new ArgumentException("component");
			}
			return TypeDescriptor.NodeFor(component).GetExtendedTypeDescriptor(component);
		}

		/// <summary>Gets an editor with the specified base type for the specified component.</summary>
		/// <returns>An instance of the editor that can be cast to the specified editor type, or null if no editor of the requested type can be found.</returns>
		/// <param name="component">The component to get the editor for. </param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you want to find. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="editorBaseType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022D7 RID: 8919 RVA: 0x00078962 File Offset: 0x00076B62
		public static object GetEditor(object component, Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(component, editorBaseType, false);
		}

		/// <summary>Returns an editor with the specified base type and with a custom type descriptor for the specified component.</summary>
		/// <returns>An instance of the editor that can be cast to the specified editor type, or null if no editor of the requested type can be found.</returns>
		/// <param name="component">The component to get the editor for. </param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you want to find. </param>
		/// <param name="noCustomTypeDesc">A flag indicating whether custom type description information should be considered.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="editorBaseType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022D8 RID: 8920 RVA: 0x0007896C File Offset: 0x00076B6C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static object GetEditor(object component, Type editorBaseType, bool noCustomTypeDesc)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetEditor(editorBaseType);
		}

		/// <summary>Returns an editor with the specified base type for the specified type.</summary>
		/// <returns>An instance of the editor object that can be cast to the given base type, or null if no editor of the requested type can be found.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you are trying to find. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="editorBaseType" /> is null. </exception>
		// Token: 0x060022D9 RID: 8921 RVA: 0x0007898F File Offset: 0x00076B8F
		public static object GetEditor(Type type, Type editorBaseType)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			return TypeDescriptor.GetDescriptor(type, "type").GetEditor(editorBaseType);
		}

		/// <summary>Returns the collection of events for a specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		// Token: 0x060022DA RID: 8922 RVA: 0x000789B6 File Offset: 0x00076BB6
		public static EventDescriptorCollection GetEvents(Type componentType)
		{
			if (componentType == null)
			{
				return new EventDescriptorCollection(null, true);
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetEvents();
		}

		/// <summary>Returns the collection of events for a specified type of component using a specified array of attributes as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that you can use as a filter. </param>
		// Token: 0x060022DB RID: 8923 RVA: 0x000789DC File Offset: 0x00076BDC
		public static EventDescriptorCollection GetEvents(Type componentType, Attribute[] attributes)
		{
			if (componentType == null)
			{
				return new EventDescriptorCollection(null, true);
			}
			EventDescriptorCollection eventDescriptorCollection = TypeDescriptor.GetDescriptor(componentType, "componentType").GetEvents(attributes);
			if (attributes != null && attributes.Length != 0)
			{
				ArrayList arrayList = TypeDescriptor.FilterMembers(eventDescriptorCollection, attributes);
				if (arrayList != null)
				{
					eventDescriptorCollection = new EventDescriptorCollection((EventDescriptor[])arrayList.ToArray(typeof(EventDescriptor)), true);
				}
			}
			return eventDescriptorCollection;
		}

		/// <summary>Returns the collection of events for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022DC RID: 8924 RVA: 0x00078A3B File Offset: 0x00076C3B
		public static EventDescriptorCollection GetEvents(object component)
		{
			return TypeDescriptor.GetEvents(component, null, false);
		}

		/// <summary>Returns the collection of events for a specified component with a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022DD RID: 8925 RVA: 0x00078A45 File Offset: 0x00076C45
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptorCollection GetEvents(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetEvents(component, null, noCustomTypeDesc);
		}

		/// <summary>Returns the collection of events for a specified component using a specified array of attributes as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that you can use as a filter. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022DE RID: 8926 RVA: 0x00078A4F File Offset: 0x00076C4F
		public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(component, attributes, false);
		}

		/// <summary>Returns the collection of events for a specified component using a specified array of attributes as a filter and using a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022DF RID: 8927 RVA: 0x00078A5C File Offset: 0x00076C5C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return new EventDescriptorCollection(null, true);
			}
			ICustomTypeDescriptor descriptor = TypeDescriptor.GetDescriptor(component, noCustomTypeDesc);
			ICollection collection;
			if (component is ICustomTypeDescriptor)
			{
				collection = descriptor.GetEvents(attributes);
				if (noCustomTypeDesc)
				{
					ICustomTypeDescriptor extendedDescriptor = TypeDescriptor.GetExtendedDescriptor(component);
					if (extendedDescriptor != null)
					{
						ICollection events = extendedDescriptor.GetEvents(attributes);
						collection = TypeDescriptor.PipelineMerge(2, collection, events, component, null);
					}
				}
				else
				{
					collection = TypeDescriptor.PipelineFilter(2, collection, component, null);
					collection = TypeDescriptor.PipelineAttributeFilter(2, collection, attributes, component, null);
				}
			}
			else
			{
				IDictionary cache = TypeDescriptor.GetCache(component);
				collection = descriptor.GetEvents(attributes);
				collection = TypeDescriptor.PipelineInitialize(2, collection, cache);
				ICustomTypeDescriptor extendedDescriptor2 = TypeDescriptor.GetExtendedDescriptor(component);
				if (extendedDescriptor2 != null)
				{
					ICollection events2 = extendedDescriptor2.GetEvents(attributes);
					collection = TypeDescriptor.PipelineMerge(2, collection, events2, component, cache);
				}
				collection = TypeDescriptor.PipelineFilter(2, collection, component, cache);
				collection = TypeDescriptor.PipelineAttributeFilter(2, collection, attributes, component, cache);
			}
			EventDescriptorCollection eventDescriptorCollection = collection as EventDescriptorCollection;
			if (eventDescriptorCollection == null)
			{
				EventDescriptor[] array = new EventDescriptor[collection.Count];
				collection.CopyTo(array, 0);
				eventDescriptorCollection = new EventDescriptorCollection(array, true);
			}
			return eventDescriptorCollection;
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00078B48 File Offset: 0x00076D48
		private static string GetExtenderCollisionSuffix(MemberDescriptor member)
		{
			string text = null;
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = member.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			if (extenderProvidedPropertyAttribute != null)
			{
				IExtenderProvider provider = extenderProvidedPropertyAttribute.Provider;
				if (provider != null)
				{
					string text2 = null;
					IComponent component = provider as IComponent;
					if (component != null && component.Site != null)
					{
						text2 = component.Site.Name;
					}
					if (text2 == null || text2.Length == 0)
					{
						text2 = (Interlocked.Increment(ref TypeDescriptor._collisionIndex) - 1).ToString(CultureInfo.InvariantCulture);
					}
					text = string.Format(CultureInfo.InvariantCulture, "_{0}", text2);
				}
			}
			return text;
		}

		/// <summary>Returns the fully qualified name of the component.</summary>
		/// <returns>The fully qualified name of the specified component, or null if the component has no name.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.Component" /> to find the name for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x060022E1 RID: 8929 RVA: 0x00078BDB File Offset: 0x00076DDB
		public static string GetFullComponentName(object component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return TypeDescriptor.GetProvider(component).GetFullComponentName(component);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00078BF7 File Offset: 0x00076DF7
		private static Type GetNodeForBaseType(Type searchType)
		{
			if (searchType.IsInterface)
			{
				return TypeDescriptor.InterfaceType;
			}
			if (searchType == TypeDescriptor.InterfaceType)
			{
				return null;
			}
			return searchType.BaseType;
		}

		/// <summary>Returns the collection of properties for a specified type of component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for a specified type of component.</returns>
		/// <param name="componentType">A <see cref="T:System.Type" /> that represents the component to get properties for.</param>
		// Token: 0x060022E3 RID: 8931 RVA: 0x00078C1C File Offset: 0x00076E1C
		public static PropertyDescriptorCollection GetProperties(Type componentType)
		{
			if (componentType == null)
			{
				return new PropertyDescriptorCollection(null, true);
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetProperties();
		}

		/// <summary>Returns the collection of properties for a specified type of component using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for this type of component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x060022E4 RID: 8932 RVA: 0x00078C40 File Offset: 0x00076E40
		public static PropertyDescriptorCollection GetProperties(Type componentType, Attribute[] attributes)
		{
			if (componentType == null)
			{
				return new PropertyDescriptorCollection(null, true);
			}
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetDescriptor(componentType, "componentType").GetProperties(attributes);
			if (attributes != null && attributes.Length != 0)
			{
				ArrayList arrayList = TypeDescriptor.FilterMembers(propertyDescriptorCollection, attributes);
				if (arrayList != null)
				{
					propertyDescriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor)), true);
				}
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Returns the collection of properties for a specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022E5 RID: 8933 RVA: 0x00078C9F File Offset: 0x00076E9F
		public static PropertyDescriptorCollection GetProperties(object component)
		{
			return TypeDescriptor.GetProperties(component, false);
		}

		/// <summary>Returns the collection of properties for a specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for a specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="noCustomTypeDesc">true to not consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022E6 RID: 8934 RVA: 0x00078CA8 File Offset: 0x00076EA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static PropertyDescriptorCollection GetProperties(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetPropertiesImpl(component, null, noCustomTypeDesc, true);
		}

		/// <summary>Returns the collection of properties for a specified component using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022E7 RID: 8935 RVA: 0x00078CB3 File Offset: 0x00076EB3
		public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(component, attributes, false);
		}

		/// <summary>Returns the collection of properties for a specified component using a specified array of attributes as a filter and using a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the events that match the specified attributes for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060022E8 RID: 8936 RVA: 0x00078CBD File Offset: 0x00076EBD
		public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetPropertiesImpl(component, attributes, noCustomTypeDesc, false);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00078CC8 File Offset: 0x00076EC8
		private static PropertyDescriptorCollection GetPropertiesImpl(object component, Attribute[] attributes, bool noCustomTypeDesc, bool noAttributes)
		{
			if (component == null)
			{
				return new PropertyDescriptorCollection(null, true);
			}
			ICustomTypeDescriptor descriptor = TypeDescriptor.GetDescriptor(component, noCustomTypeDesc);
			ICollection collection;
			if (component is ICustomTypeDescriptor)
			{
				collection = (noAttributes ? descriptor.GetProperties() : descriptor.GetProperties(attributes));
				if (noCustomTypeDesc)
				{
					ICustomTypeDescriptor extendedDescriptor = TypeDescriptor.GetExtendedDescriptor(component);
					if (extendedDescriptor != null)
					{
						ICollection collection2 = (noAttributes ? extendedDescriptor.GetProperties() : extendedDescriptor.GetProperties(attributes));
						collection = TypeDescriptor.PipelineMerge(1, collection, collection2, component, null);
					}
				}
				else
				{
					collection = TypeDescriptor.PipelineFilter(1, collection, component, null);
					collection = TypeDescriptor.PipelineAttributeFilter(1, collection, attributes, component, null);
				}
			}
			else
			{
				IDictionary cache = TypeDescriptor.GetCache(component);
				collection = (noAttributes ? descriptor.GetProperties() : descriptor.GetProperties(attributes));
				collection = TypeDescriptor.PipelineInitialize(1, collection, cache);
				ICustomTypeDescriptor extendedDescriptor2 = TypeDescriptor.GetExtendedDescriptor(component);
				if (extendedDescriptor2 != null)
				{
					ICollection collection3 = (noAttributes ? extendedDescriptor2.GetProperties() : extendedDescriptor2.GetProperties(attributes));
					collection = TypeDescriptor.PipelineMerge(1, collection, collection3, component, cache);
				}
				collection = TypeDescriptor.PipelineFilter(1, collection, component, cache);
				collection = TypeDescriptor.PipelineAttributeFilter(1, collection, attributes, component, cache);
			}
			PropertyDescriptorCollection propertyDescriptorCollection = collection as PropertyDescriptorCollection;
			if (propertyDescriptorCollection == null)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[collection.Count];
				collection.CopyTo(array, 0);
				propertyDescriptorCollection = new PropertyDescriptorCollection(array, true);
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Returns the type description provider for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> associated with the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060022EA RID: 8938 RVA: 0x00078DE4 File Offset: 0x00076FE4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider GetProvider(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return TypeDescriptor.NodeFor(type, true);
		}

		/// <summary>Returns the type description provider for the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> associated with the specified component.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x060022EB RID: 8939 RVA: 0x00078E01 File Offset: 0x00077001
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider GetProvider(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return TypeDescriptor.NodeFor(instance, true);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00078E18 File Offset: 0x00077018
		internal static TypeDescriptionProvider GetProviderRecursive(Type type)
		{
			return TypeDescriptor.NodeFor(type, false);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> that can be used to perform reflection, given a class type.</summary>
		/// <returns>A <see cref="T:System.Type" /> of the specified class.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060022ED RID: 8941 RVA: 0x00078E21 File Offset: 0x00077021
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type GetReflectionType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return TypeDescriptor.NodeFor(type).GetReflectionType(type);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> that can be used to perform reflection, given an object.</summary>
		/// <returns>A <see cref="T:System.Type" /> for the specified object.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x060022EE RID: 8942 RVA: 0x00078E43 File Offset: 0x00077043
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return TypeDescriptor.NodeFor(instance).GetReflectionType(instance);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x00078E18 File Offset: 0x00077018
		private static TypeDescriptor.TypeDescriptionNode NodeFor(Type type)
		{
			return TypeDescriptor.NodeFor(type, false);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x00078E60 File Offset: 0x00077060
		private static TypeDescriptor.TypeDescriptionNode NodeFor(Type type, bool createDelegator)
		{
			TypeDescriptor.CheckDefaultProvider(type);
			TypeDescriptor.TypeDescriptionNode typeDescriptionNode = null;
			Type type2 = type;
			while (typeDescriptionNode == null)
			{
				typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTypeTable[type2];
				if (typeDescriptionNode == null)
				{
					typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[type2];
				}
				if (typeDescriptionNode == null)
				{
					Type nodeForBaseType = TypeDescriptor.GetNodeForBaseType(type2);
					if (type2 == typeof(object) || nodeForBaseType == null)
					{
						WeakHashtable weakHashtable = TypeDescriptor._providerTable;
						lock (weakHashtable)
						{
							typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[type2];
							if (typeDescriptionNode == null)
							{
								typeDescriptionNode = new TypeDescriptor.TypeDescriptionNode(new ReflectTypeDescriptionProvider());
								TypeDescriptor._providerTable[type2] = typeDescriptionNode;
							}
							continue;
						}
					}
					if (createDelegator)
					{
						typeDescriptionNode = new TypeDescriptor.TypeDescriptionNode(new DelegatingTypeDescriptionProvider(nodeForBaseType));
						WeakHashtable weakHashtable = TypeDescriptor._providerTable;
						lock (weakHashtable)
						{
							TypeDescriptor._providerTypeTable[type2] = typeDescriptionNode;
							continue;
						}
					}
					type2 = nodeForBaseType;
				}
			}
			return typeDescriptionNode;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x00078F70 File Offset: 0x00077170
		private static TypeDescriptor.TypeDescriptionNode NodeFor(object instance)
		{
			return TypeDescriptor.NodeFor(instance, false);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00078F7C File Offset: 0x0007717C
		private static TypeDescriptor.TypeDescriptionNode NodeFor(object instance, bool createDelegator)
		{
			TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[instance];
			if (typeDescriptionNode == null)
			{
				Type type = instance.GetType();
				if (type.IsCOMObject)
				{
					type = TypeDescriptor.ComObjectType;
				}
				if (createDelegator)
				{
					typeDescriptionNode = new TypeDescriptor.TypeDescriptionNode(new DelegatingTypeDescriptionProvider(type));
				}
				else
				{
					typeDescriptionNode = TypeDescriptor.NodeFor(type);
				}
			}
			return typeDescriptionNode;
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00078FCC File Offset: 0x000771CC
		private static void NodeRemove(object key, TypeDescriptionProvider provider)
		{
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[key];
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode2 = typeDescriptionNode;
				while (typeDescriptionNode2 != null && typeDescriptionNode2.Provider != provider)
				{
					typeDescriptionNode2 = typeDescriptionNode2.Next;
				}
				if (typeDescriptionNode2 != null)
				{
					if (typeDescriptionNode2.Next != null)
					{
						typeDescriptionNode2.Provider = typeDescriptionNode2.Next.Provider;
						typeDescriptionNode2.Next = typeDescriptionNode2.Next.Next;
						if (typeDescriptionNode2 == typeDescriptionNode && typeDescriptionNode2.Provider is DelegatingTypeDescriptionProvider)
						{
							TypeDescriptor._providerTable.Remove(key);
						}
					}
					else if (typeDescriptionNode2 != typeDescriptionNode)
					{
						Type type = key as Type;
						if (type == null)
						{
							type = key.GetType();
						}
						typeDescriptionNode2.Provider = new DelegatingTypeDescriptionProvider(type.BaseType);
					}
					else
					{
						TypeDescriptor._providerTable.Remove(key);
					}
					TypeDescriptor._providerTypeTable.Clear();
				}
			}
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000790C4 File Offset: 0x000772C4
		private static ICollection PipelineAttributeFilter(int pipelineType, ICollection members, Attribute[] filter, object instance, IDictionary cache)
		{
			IList list = members as ArrayList;
			if (filter == null || filter.Length == 0)
			{
				return members;
			}
			if (cache != null && (list == null || list.IsReadOnly))
			{
				TypeDescriptor.AttributeFilterCacheItem attributeFilterCacheItem = cache[TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]] as TypeDescriptor.AttributeFilterCacheItem;
				if (attributeFilterCacheItem != null && attributeFilterCacheItem.IsValid(filter))
				{
					return attributeFilterCacheItem.FilteredMembers;
				}
			}
			if (list == null || list.IsReadOnly)
			{
				list = new ArrayList(members);
			}
			ArrayList arrayList = TypeDescriptor.FilterMembers(list, filter);
			if (arrayList != null)
			{
				list = arrayList;
			}
			if (cache != null)
			{
				ICollection collection;
				if (pipelineType != 1)
				{
					if (pipelineType != 2)
					{
						collection = null;
					}
					else
					{
						EventDescriptor[] array = new EventDescriptor[list.Count];
						list.CopyTo(array, 0);
						collection = new EventDescriptorCollection(array, true);
					}
				}
				else
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[list.Count];
					list.CopyTo(array2, 0);
					collection = new PropertyDescriptorCollection(array2, true);
				}
				TypeDescriptor.AttributeFilterCacheItem attributeFilterCacheItem2 = new TypeDescriptor.AttributeFilterCacheItem(filter, collection);
				cache[TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]] = attributeFilterCacheItem2;
			}
			return list;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000791B4 File Offset: 0x000773B4
		private static ICollection PipelineFilter(int pipelineType, ICollection members, object instance, IDictionary cache)
		{
			IComponent component = instance as IComponent;
			ITypeDescriptorFilterService typeDescriptorFilterService = null;
			if (component != null)
			{
				ISite site = component.Site;
				if (site != null)
				{
					typeDescriptorFilterService = site.GetService(typeof(ITypeDescriptorFilterService)) as ITypeDescriptorFilterService;
				}
			}
			IList list = members as ArrayList;
			if (typeDescriptorFilterService == null)
			{
				return members;
			}
			if (cache != null && (list == null || list.IsReadOnly))
			{
				TypeDescriptor.FilterCacheItem filterCacheItem = cache[TypeDescriptor._pipelineFilterKeys[pipelineType]] as TypeDescriptor.FilterCacheItem;
				if (filterCacheItem != null && filterCacheItem.IsValid(typeDescriptorFilterService))
				{
					return filterCacheItem.FilteredMembers;
				}
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary(members.Count);
			bool flag;
			if (pipelineType != 0)
			{
				if (pipelineType - 1 > 1)
				{
					flag = false;
				}
				else
				{
					foreach (object obj in members)
					{
						MemberDescriptor memberDescriptor = (MemberDescriptor)obj;
						string name = memberDescriptor.Name;
						if (orderedDictionary.Contains(name))
						{
							string text = TypeDescriptor.GetExtenderCollisionSuffix(memberDescriptor);
							if (text != null)
							{
								orderedDictionary[name + text] = memberDescriptor;
							}
							MemberDescriptor memberDescriptor2 = (MemberDescriptor)orderedDictionary[name];
							text = TypeDescriptor.GetExtenderCollisionSuffix(memberDescriptor2);
							if (text != null)
							{
								orderedDictionary.Remove(name);
								orderedDictionary[memberDescriptor2.Name + text] = memberDescriptor2;
							}
						}
						else
						{
							orderedDictionary[name] = memberDescriptor;
						}
					}
					if (pipelineType == 1)
					{
						flag = typeDescriptorFilterService.FilterProperties(component, orderedDictionary);
					}
					else
					{
						flag = typeDescriptorFilterService.FilterEvents(component, orderedDictionary);
					}
				}
			}
			else
			{
				foreach (object obj2 in members)
				{
					Attribute attribute = (Attribute)obj2;
					orderedDictionary[attribute.TypeId] = attribute;
				}
				flag = typeDescriptorFilterService.FilterAttributes(component, orderedDictionary);
			}
			if (list == null || list.IsReadOnly)
			{
				list = new ArrayList(orderedDictionary.Values);
			}
			else
			{
				list.Clear();
				foreach (object obj3 in orderedDictionary.Values)
				{
					list.Add(obj3);
				}
			}
			if (flag && cache != null)
			{
				ICollection collection;
				switch (pipelineType)
				{
				case 0:
				{
					Attribute[] array = new Attribute[list.Count];
					try
					{
						list.CopyTo(array, 0);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException(SR.GetString("Expected types in the collection to be of type {0}.", new object[] { typeof(Attribute).FullName }));
					}
					collection = new AttributeCollection(array);
					break;
				}
				case 1:
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[list.Count];
					try
					{
						list.CopyTo(array2, 0);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException(SR.GetString("Expected types in the collection to be of type {0}.", new object[] { typeof(PropertyDescriptor).FullName }));
					}
					collection = new PropertyDescriptorCollection(array2, true);
					break;
				}
				case 2:
				{
					EventDescriptor[] array3 = new EventDescriptor[list.Count];
					try
					{
						list.CopyTo(array3, 0);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException(SR.GetString("Expected types in the collection to be of type {0}.", new object[] { typeof(EventDescriptor).FullName }));
					}
					collection = new EventDescriptorCollection(array3, true);
					break;
				}
				default:
					collection = null;
					break;
				}
				TypeDescriptor.FilterCacheItem filterCacheItem2 = new TypeDescriptor.FilterCacheItem(typeDescriptorFilterService, collection);
				cache[TypeDescriptor._pipelineFilterKeys[pipelineType]] = filterCacheItem2;
				cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]);
			}
			return list;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00079578 File Offset: 0x00077778
		private static ICollection PipelineInitialize(int pipelineType, ICollection members, IDictionary cache)
		{
			if (cache != null)
			{
				bool flag = true;
				ICollection collection = cache[TypeDescriptor._pipelineInitializeKeys[pipelineType]] as ICollection;
				if (collection != null && collection.Count == members.Count)
				{
					IEnumerator enumerator = collection.GetEnumerator();
					IEnumerator enumerator2 = members.GetEnumerator();
					while (enumerator.MoveNext() && enumerator2.MoveNext())
					{
						if (enumerator.Current != enumerator2.Current)
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag)
				{
					cache.Remove(TypeDescriptor._pipelineMergeKeys[pipelineType]);
					cache.Remove(TypeDescriptor._pipelineFilterKeys[pipelineType]);
					cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]);
					cache[TypeDescriptor._pipelineInitializeKeys[pipelineType]] = members;
				}
			}
			return members;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x0007964C File Offset: 0x0007784C
		private static ICollection PipelineMerge(int pipelineType, ICollection primary, ICollection secondary, object instance, IDictionary cache)
		{
			if (secondary == null || secondary.Count == 0)
			{
				return primary;
			}
			if (cache != null)
			{
				ICollection collection = cache[TypeDescriptor._pipelineMergeKeys[pipelineType]] as ICollection;
				if (collection != null && collection.Count == primary.Count + secondary.Count)
				{
					IEnumerator enumerator = collection.GetEnumerator();
					IEnumerator enumerator2 = primary.GetEnumerator();
					bool flag = true;
					while (enumerator2.MoveNext() && enumerator.MoveNext())
					{
						if (enumerator2.Current != enumerator.Current)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						IEnumerator enumerator3 = secondary.GetEnumerator();
						while (enumerator3.MoveNext() && enumerator.MoveNext())
						{
							if (enumerator3.Current != enumerator.Current)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						return collection;
					}
				}
			}
			ArrayList arrayList = new ArrayList(primary.Count + secondary.Count);
			foreach (object obj in primary)
			{
				arrayList.Add(obj);
			}
			foreach (object obj2 in secondary)
			{
				arrayList.Add(obj2);
			}
			if (cache != null)
			{
				ICollection collection2;
				switch (pipelineType)
				{
				case 0:
				{
					Attribute[] array = new Attribute[arrayList.Count];
					arrayList.CopyTo(array, 0);
					collection2 = new AttributeCollection(array);
					break;
				}
				case 1:
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					collection2 = new PropertyDescriptorCollection(array2, true);
					break;
				}
				case 2:
				{
					EventDescriptor[] array3 = new EventDescriptor[arrayList.Count];
					arrayList.CopyTo(array3, 0);
					collection2 = new EventDescriptorCollection(array3, true);
					break;
				}
				default:
					collection2 = null;
					break;
				}
				cache[TypeDescriptor._pipelineMergeKeys[pipelineType]] = collection2;
				cache.Remove(TypeDescriptor._pipelineFilterKeys[pipelineType]);
				cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]);
			}
			return arrayList;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00079888 File Offset: 0x00077A88
		private static void RaiseRefresh(object component)
		{
			RefreshEventHandler refreshEventHandler = Volatile.Read<RefreshEventHandler>(ref TypeDescriptor.Refreshed);
			if (refreshEventHandler != null)
			{
				refreshEventHandler(new RefreshEventArgs(component));
			}
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000798B0 File Offset: 0x00077AB0
		private static void RaiseRefresh(Type type)
		{
			RefreshEventHandler refreshEventHandler = Volatile.Read<RefreshEventHandler>(ref TypeDescriptor.Refreshed);
			if (refreshEventHandler != null)
			{
				refreshEventHandler(new RefreshEventArgs(type));
			}
		}

		/// <summary>Clears the properties and events for the specified component from the cache.</summary>
		/// <param name="component">A component for which the properties or events have changed. </param>
		// Token: 0x060022FA RID: 8954 RVA: 0x000798D7 File Offset: 0x00077AD7
		public static void Refresh(object component)
		{
			TypeDescriptor.Refresh(component, true);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000798E0 File Offset: 0x00077AE0
		private static void Refresh(object component, bool refreshReflectionProvider)
		{
			if (component == null)
			{
				return;
			}
			bool flag = false;
			if (refreshReflectionProvider)
			{
				Type type = component.GetType();
				WeakHashtable providerTable = TypeDescriptor._providerTable;
				lock (providerTable)
				{
					foreach (object obj in TypeDescriptor._providerTable)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						Type type2 = dictionaryEntry.Key as Type;
						if ((type2 != null && type.IsAssignableFrom(type2)) || type2 == typeof(object))
						{
							TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)dictionaryEntry.Value;
							while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is ReflectTypeDescriptionProvider))
							{
								flag = true;
								typeDescriptionNode = typeDescriptionNode.Next;
							}
							if (typeDescriptionNode != null)
							{
								ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = (ReflectTypeDescriptionProvider)typeDescriptionNode.Provider;
								if (reflectTypeDescriptionProvider.IsPopulated(type))
								{
									flag = true;
									reflectTypeDescriptionProvider.Refresh(type);
								}
							}
						}
					}
				}
			}
			IDictionary cache = TypeDescriptor.GetCache(component);
			if (flag || cache != null)
			{
				if (cache != null)
				{
					for (int i = 0; i < TypeDescriptor._pipelineFilterKeys.Length; i++)
					{
						cache.Remove(TypeDescriptor._pipelineFilterKeys[i]);
						cache.Remove(TypeDescriptor._pipelineMergeKeys[i]);
						cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[i]);
					}
				}
				Interlocked.Increment(ref TypeDescriptor._metadataVersion);
				TypeDescriptor.RaiseRefresh(component);
			}
		}

		/// <summary>Clears the properties and events for the specified type of component from the cache.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		// Token: 0x060022FC RID: 8956 RVA: 0x00079A80 File Offset: 0x00077C80
		public static void Refresh(Type type)
		{
			if (type == null)
			{
				return;
			}
			bool flag = false;
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				foreach (object obj in TypeDescriptor._providerTable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					Type type2 = dictionaryEntry.Key as Type;
					if ((type2 != null && type.IsAssignableFrom(type2)) || type2 == typeof(object))
					{
						TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)dictionaryEntry.Value;
						while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is ReflectTypeDescriptionProvider))
						{
							flag = true;
							typeDescriptionNode = typeDescriptionNode.Next;
						}
						if (typeDescriptionNode != null)
						{
							ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = (ReflectTypeDescriptionProvider)typeDescriptionNode.Provider;
							if (reflectTypeDescriptionProvider.IsPopulated(type))
							{
								flag = true;
								reflectTypeDescriptionProvider.Refresh(type);
							}
						}
					}
				}
			}
			if (flag)
			{
				Interlocked.Increment(ref TypeDescriptor._metadataVersion);
				TypeDescriptor.RaiseRefresh(type);
			}
		}

		/// <summary>Clears the properties and events for the specified module from the cache.</summary>
		/// <param name="module">The <see cref="T:System.Reflection.Module" /> that represents the module to refresh. Each <see cref="T:System.Type" /> in this module will be refreshed. </param>
		// Token: 0x060022FD RID: 8957 RVA: 0x00079BAC File Offset: 0x00077DAC
		public static void Refresh(Module module)
		{
			if (module == null)
			{
				return;
			}
			Hashtable hashtable = null;
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				foreach (object obj in TypeDescriptor._providerTable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					Type type = dictionaryEntry.Key as Type;
					if ((type != null && type.Module.Equals(module)) || type == typeof(object))
					{
						TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)dictionaryEntry.Value;
						while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is ReflectTypeDescriptionProvider))
						{
							if (hashtable == null)
							{
								hashtable = new Hashtable();
							}
							hashtable[type] = type;
							typeDescriptionNode = typeDescriptionNode.Next;
						}
						if (typeDescriptionNode != null)
						{
							ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = (ReflectTypeDescriptionProvider)typeDescriptionNode.Provider;
							foreach (Type type2 in reflectTypeDescriptionProvider.GetPopulatedTypes(module))
							{
								reflectTypeDescriptionProvider.Refresh(type2);
								if (hashtable == null)
								{
									hashtable = new Hashtable();
								}
								hashtable[type2] = type2;
							}
						}
					}
				}
			}
			if (hashtable != null && TypeDescriptor.Refreshed != null)
			{
				foreach (object obj2 in hashtable.Keys)
				{
					TypeDescriptor.RaiseRefresh((Type)obj2);
				}
			}
		}

		/// <summary>Clears the properties and events for the specified assembly from the cache.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> that represents the assembly to refresh. Each <see cref="T:System.Type" /> in this assembly will be refreshed. </param>
		// Token: 0x060022FE RID: 8958 RVA: 0x00079D80 File Offset: 0x00077F80
		public static void Refresh(Assembly assembly)
		{
			if (assembly == null)
			{
				return;
			}
			Module[] modules = assembly.GetModules();
			for (int i = 0; i < modules.Length; i++)
			{
				TypeDescriptor.Refresh(modules[i]);
			}
		}

		/// <summary>Removes an association between two objects.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" />.</param>
		/// <param name="secondary">The secondary <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060022FF RID: 8959 RVA: 0x00079DB4 File Offset: 0x00077FB4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveAssociation(object primary, object secondary)
		{
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			if (secondary == null)
			{
				throw new ArgumentNullException("secondary");
			}
			Hashtable associationTable = TypeDescriptor._associationTable;
			if (associationTable != null)
			{
				IList list = (IList)associationTable[primary];
				if (list != null)
				{
					IList list2 = list;
					lock (list2)
					{
						for (int i = list.Count - 1; i >= 0; i--)
						{
							object target = ((WeakReference)list[i]).Target;
							if (target == null || target == secondary)
							{
								list.RemoveAt(i);
							}
						}
					}
				}
			}
		}

		/// <summary>Removes all associations for a primary object.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" /> in an association.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="primary" /> is null.</exception>
		// Token: 0x06002300 RID: 8960 RVA: 0x00079E5C File Offset: 0x0007805C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveAssociations(object primary)
		{
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			Hashtable associationTable = TypeDescriptor._associationTable;
			if (associationTable != null)
			{
				associationTable.Remove(primary);
			}
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified type.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06002301 RID: 8961 RVA: 0x00079E89 File Offset: 0x00078089
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveProvider(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TypeDescriptor.NodeRemove(type, provider);
			TypeDescriptor.RaiseRefresh(type);
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified object.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06002302 RID: 8962 RVA: 0x00079EBA File Offset: 0x000780BA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveProvider(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			TypeDescriptor.NodeRemove(instance, provider);
			TypeDescriptor.RaiseRefresh(instance);
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified type.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06002303 RID: 8963 RVA: 0x00079EE5 File Offset: 0x000780E5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveProviderTransparent(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TypeDescriptor.RemoveProvider(provider, type);
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified object.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x06002304 RID: 8964 RVA: 0x00079F10 File Offset: 0x00078110
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveProviderTransparent(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			TypeDescriptor.RemoveProvider(provider, instance);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00079F38 File Offset: 0x00078138
		private static bool ShouldHideMember(MemberDescriptor member, Attribute attribute)
		{
			if (member == null || attribute == null)
			{
				return true;
			}
			Attribute attribute2 = member.Attributes[attribute.GetType()];
			if (attribute2 == null)
			{
				return !attribute.IsDefaultAttribute();
			}
			return !attribute.Match(attribute2);
		}

		/// <summary>Sorts descriptors using the name of the descriptor.</summary>
		/// <param name="infos">An <see cref="T:System.Collections.IList" /> that contains the descriptors to sort. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="infos" /> is null.</exception>
		// Token: 0x06002306 RID: 8966 RVA: 0x00079F76 File Offset: 0x00078176
		public static void SortDescriptorArray(IList infos)
		{
			if (infos == null)
			{
				throw new ArgumentNullException("infos");
			}
			ArrayList.Adapter(infos).Sort(TypeDescriptor.MemberDescriptorComparer.Instance);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("DEBUG")]
		internal static void Trace(string message, params object[] args)
		{
		}

		// Token: 0x04001095 RID: 4245
		private static WeakHashtable _providerTable = new WeakHashtable();

		// Token: 0x04001096 RID: 4246
		private static Hashtable _providerTypeTable = new Hashtable();

		// Token: 0x04001097 RID: 4247
		private static volatile Hashtable _defaultProviders = new Hashtable();

		// Token: 0x04001098 RID: 4248
		private static volatile WeakHashtable _associationTable;

		// Token: 0x04001099 RID: 4249
		private static int _metadataVersion;

		// Token: 0x0400109A RID: 4250
		private static int _collisionIndex;

		// Token: 0x0400109B RID: 4251
		private static BooleanSwitch TraceDescriptor = new BooleanSwitch("TypeDescriptor", "Debug TypeDescriptor.");

		// Token: 0x0400109C RID: 4252
		private const int PIPELINE_ATTRIBUTES = 0;

		// Token: 0x0400109D RID: 4253
		private const int PIPELINE_PROPERTIES = 1;

		// Token: 0x0400109E RID: 4254
		private const int PIPELINE_EVENTS = 2;

		// Token: 0x0400109F RID: 4255
		private static readonly Guid[] _pipelineInitializeKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x040010A0 RID: 4256
		private static readonly Guid[] _pipelineMergeKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x040010A1 RID: 4257
		private static readonly Guid[] _pipelineFilterKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x040010A2 RID: 4258
		private static readonly Guid[] _pipelineAttributeFilterKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x040010A3 RID: 4259
		private static object _internalSyncObject = new object();

		// Token: 0x02000428 RID: 1064
		private sealed class AttributeProvider : TypeDescriptionProvider
		{
			// Token: 0x06002309 RID: 8969 RVA: 0x0007A09F File Offset: 0x0007829F
			internal AttributeProvider(TypeDescriptionProvider existingProvider, params Attribute[] attrs)
				: base(existingProvider)
			{
				this._attrs = attrs;
			}

			// Token: 0x0600230A RID: 8970 RVA: 0x0007A0AF File Offset: 0x000782AF
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				return new TypeDescriptor.AttributeProvider.AttributeTypeDescriptor(this._attrs, base.GetTypeDescriptor(objectType, instance));
			}

			// Token: 0x040010A5 RID: 4261
			private Attribute[] _attrs;

			// Token: 0x02000429 RID: 1065
			private class AttributeTypeDescriptor : CustomTypeDescriptor
			{
				// Token: 0x0600230B RID: 8971 RVA: 0x0007A0C4 File Offset: 0x000782C4
				internal AttributeTypeDescriptor(Attribute[] attrs, ICustomTypeDescriptor parent)
					: base(parent)
				{
					this._attributeArray = attrs;
				}

				// Token: 0x0600230C RID: 8972 RVA: 0x0007A0D4 File Offset: 0x000782D4
				public override AttributeCollection GetAttributes()
				{
					AttributeCollection attributes = base.GetAttributes();
					Attribute[] attributeArray = this._attributeArray;
					Attribute[] array = new Attribute[attributes.Count + attributeArray.Length];
					int count = attributes.Count;
					attributes.CopyTo(array, 0);
					for (int i = 0; i < attributeArray.Length; i++)
					{
						bool flag = false;
						for (int j = 0; j < attributes.Count; j++)
						{
							if (array[j].TypeId.Equals(attributeArray[i].TypeId))
							{
								flag = true;
								array[j] = attributeArray[i];
								break;
							}
						}
						if (!flag)
						{
							array[count++] = attributeArray[i];
						}
					}
					Attribute[] array2;
					if (count < array.Length)
					{
						array2 = new Attribute[count];
						Array.Copy(array, 0, array2, 0, count);
					}
					else
					{
						array2 = array;
					}
					return new AttributeCollection(array2);
				}

				// Token: 0x040010A6 RID: 4262
				private Attribute[] _attributeArray;
			}
		}

		// Token: 0x0200042A RID: 1066
		private sealed class ComNativeDescriptionProvider : TypeDescriptionProvider
		{
			// Token: 0x0600230D RID: 8973 RVA: 0x0007A196 File Offset: 0x00078396
			internal ComNativeDescriptionProvider(IComNativeDescriptorHandler handler)
			{
				this._handler = handler;
			}

			// Token: 0x17000727 RID: 1831
			// (get) Token: 0x0600230E RID: 8974 RVA: 0x0007A1A5 File Offset: 0x000783A5
			// (set) Token: 0x0600230F RID: 8975 RVA: 0x0007A1AD File Offset: 0x000783AD
			internal IComNativeDescriptorHandler Handler
			{
				get
				{
					return this._handler;
				}
				set
				{
					this._handler = value;
				}
			}

			// Token: 0x06002310 RID: 8976 RVA: 0x0007A1B6 File Offset: 0x000783B6
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				if (instance == null)
				{
					return null;
				}
				if (!objectType.IsInstanceOfType(instance))
				{
					throw new ArgumentException("instance");
				}
				return new TypeDescriptor.ComNativeDescriptionProvider.ComNativeTypeDescriptor(this._handler, instance);
			}

			// Token: 0x040010A7 RID: 4263
			private IComNativeDescriptorHandler _handler;

			// Token: 0x0200042B RID: 1067
			private sealed class ComNativeTypeDescriptor : ICustomTypeDescriptor
			{
				// Token: 0x06002311 RID: 8977 RVA: 0x0007A1F1 File Offset: 0x000783F1
				internal ComNativeTypeDescriptor(IComNativeDescriptorHandler handler, object instance)
				{
					this._handler = handler;
					this._instance = instance;
				}

				// Token: 0x06002312 RID: 8978 RVA: 0x0007A207 File Offset: 0x00078407
				AttributeCollection ICustomTypeDescriptor.GetAttributes()
				{
					return this._handler.GetAttributes(this._instance);
				}

				// Token: 0x06002313 RID: 8979 RVA: 0x0007A21A File Offset: 0x0007841A
				string ICustomTypeDescriptor.GetClassName()
				{
					return this._handler.GetClassName(this._instance);
				}

				// Token: 0x06002314 RID: 8980 RVA: 0x00002F6A File Offset: 0x0000116A
				string ICustomTypeDescriptor.GetComponentName()
				{
					return null;
				}

				// Token: 0x06002315 RID: 8981 RVA: 0x0007A22D File Offset: 0x0007842D
				TypeConverter ICustomTypeDescriptor.GetConverter()
				{
					return this._handler.GetConverter(this._instance);
				}

				// Token: 0x06002316 RID: 8982 RVA: 0x0007A240 File Offset: 0x00078440
				EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
				{
					return this._handler.GetDefaultEvent(this._instance);
				}

				// Token: 0x06002317 RID: 8983 RVA: 0x0007A253 File Offset: 0x00078453
				PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
				{
					return this._handler.GetDefaultProperty(this._instance);
				}

				// Token: 0x06002318 RID: 8984 RVA: 0x0007A266 File Offset: 0x00078466
				object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
				{
					return this._handler.GetEditor(this._instance, editorBaseType);
				}

				// Token: 0x06002319 RID: 8985 RVA: 0x0007A27A File Offset: 0x0007847A
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
				{
					return this._handler.GetEvents(this._instance);
				}

				// Token: 0x0600231A RID: 8986 RVA: 0x0007A28D File Offset: 0x0007848D
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
				{
					return this._handler.GetEvents(this._instance, attributes);
				}

				// Token: 0x0600231B RID: 8987 RVA: 0x0007A2A1 File Offset: 0x000784A1
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
				{
					return this._handler.GetProperties(this._instance, null);
				}

				// Token: 0x0600231C RID: 8988 RVA: 0x0007A2B5 File Offset: 0x000784B5
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
				{
					return this._handler.GetProperties(this._instance, attributes);
				}

				// Token: 0x0600231D RID: 8989 RVA: 0x0007A2C9 File Offset: 0x000784C9
				object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
				{
					return this._instance;
				}

				// Token: 0x040010A8 RID: 4264
				private IComNativeDescriptorHandler _handler;

				// Token: 0x040010A9 RID: 4265
				private object _instance;
			}
		}

		// Token: 0x0200042C RID: 1068
		private sealed class AttributeFilterCacheItem
		{
			// Token: 0x0600231E RID: 8990 RVA: 0x0007A2D1 File Offset: 0x000784D1
			internal AttributeFilterCacheItem(Attribute[] filter, ICollection filteredMembers)
			{
				this._filter = filter;
				this.FilteredMembers = filteredMembers;
			}

			// Token: 0x0600231F RID: 8991 RVA: 0x0007A2E8 File Offset: 0x000784E8
			internal bool IsValid(Attribute[] filter)
			{
				if (this._filter.Length != filter.Length)
				{
					return false;
				}
				for (int i = 0; i < filter.Length; i++)
				{
					if (this._filter[i] != filter[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040010AA RID: 4266
			private Attribute[] _filter;

			// Token: 0x040010AB RID: 4267
			internal ICollection FilteredMembers;
		}

		// Token: 0x0200042D RID: 1069
		private sealed class FilterCacheItem
		{
			// Token: 0x06002320 RID: 8992 RVA: 0x0007A322 File Offset: 0x00078522
			internal FilterCacheItem(ITypeDescriptorFilterService filterService, ICollection filteredMembers)
			{
				this._filterService = filterService;
				this.FilteredMembers = filteredMembers;
			}

			// Token: 0x06002321 RID: 8993 RVA: 0x0007A338 File Offset: 0x00078538
			internal bool IsValid(ITypeDescriptorFilterService filterService)
			{
				return this._filterService == filterService;
			}

			// Token: 0x040010AC RID: 4268
			private ITypeDescriptorFilterService _filterService;

			// Token: 0x040010AD RID: 4269
			internal ICollection FilteredMembers;
		}

		// Token: 0x0200042E RID: 1070
		private interface IUnimplemented
		{
		}

		// Token: 0x0200042F RID: 1071
		private sealed class MemberDescriptorComparer : IComparer
		{
			// Token: 0x06002322 RID: 8994 RVA: 0x0007A346 File Offset: 0x00078546
			public int Compare(object left, object right)
			{
				return string.Compare(((MemberDescriptor)left).Name, ((MemberDescriptor)right).Name, false, CultureInfo.InvariantCulture);
			}

			// Token: 0x040010AE RID: 4270
			public static readonly TypeDescriptor.MemberDescriptorComparer Instance = new TypeDescriptor.MemberDescriptorComparer();
		}

		// Token: 0x02000430 RID: 1072
		private sealed class MergedTypeDescriptor : ICustomTypeDescriptor
		{
			// Token: 0x06002325 RID: 8997 RVA: 0x0007A375 File Offset: 0x00078575
			internal MergedTypeDescriptor(ICustomTypeDescriptor primary, ICustomTypeDescriptor secondary)
			{
				this._primary = primary;
				this._secondary = secondary;
			}

			// Token: 0x06002326 RID: 8998 RVA: 0x0007A38C File Offset: 0x0007858C
			AttributeCollection ICustomTypeDescriptor.GetAttributes()
			{
				AttributeCollection attributeCollection = this._primary.GetAttributes();
				if (attributeCollection == null)
				{
					attributeCollection = this._secondary.GetAttributes();
				}
				return attributeCollection;
			}

			// Token: 0x06002327 RID: 8999 RVA: 0x0007A3B8 File Offset: 0x000785B8
			string ICustomTypeDescriptor.GetClassName()
			{
				string text = this._primary.GetClassName();
				if (text == null)
				{
					text = this._secondary.GetClassName();
				}
				return text;
			}

			// Token: 0x06002328 RID: 9000 RVA: 0x0007A3E4 File Offset: 0x000785E4
			string ICustomTypeDescriptor.GetComponentName()
			{
				string text = this._primary.GetComponentName();
				if (text == null)
				{
					text = this._secondary.GetComponentName();
				}
				return text;
			}

			// Token: 0x06002329 RID: 9001 RVA: 0x0007A410 File Offset: 0x00078610
			TypeConverter ICustomTypeDescriptor.GetConverter()
			{
				TypeConverter typeConverter = this._primary.GetConverter();
				if (typeConverter == null)
				{
					typeConverter = this._secondary.GetConverter();
				}
				return typeConverter;
			}

			// Token: 0x0600232A RID: 9002 RVA: 0x0007A43C File Offset: 0x0007863C
			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
			{
				EventDescriptor eventDescriptor = this._primary.GetDefaultEvent();
				if (eventDescriptor == null)
				{
					eventDescriptor = this._secondary.GetDefaultEvent();
				}
				return eventDescriptor;
			}

			// Token: 0x0600232B RID: 9003 RVA: 0x0007A468 File Offset: 0x00078668
			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
			{
				PropertyDescriptor propertyDescriptor = this._primary.GetDefaultProperty();
				if (propertyDescriptor == null)
				{
					propertyDescriptor = this._secondary.GetDefaultProperty();
				}
				return propertyDescriptor;
			}

			// Token: 0x0600232C RID: 9004 RVA: 0x0007A494 File Offset: 0x00078694
			object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
			{
				if (editorBaseType == null)
				{
					throw new ArgumentNullException("editorBaseType");
				}
				object obj = this._primary.GetEditor(editorBaseType);
				if (obj == null)
				{
					obj = this._secondary.GetEditor(editorBaseType);
				}
				return obj;
			}

			// Token: 0x0600232D RID: 9005 RVA: 0x0007A4D4 File Offset: 0x000786D4
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
			{
				EventDescriptorCollection eventDescriptorCollection = this._primary.GetEvents();
				if (eventDescriptorCollection == null)
				{
					eventDescriptorCollection = this._secondary.GetEvents();
				}
				return eventDescriptorCollection;
			}

			// Token: 0x0600232E RID: 9006 RVA: 0x0007A500 File Offset: 0x00078700
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
			{
				EventDescriptorCollection eventDescriptorCollection = this._primary.GetEvents(attributes);
				if (eventDescriptorCollection == null)
				{
					eventDescriptorCollection = this._secondary.GetEvents(attributes);
				}
				return eventDescriptorCollection;
			}

			// Token: 0x0600232F RID: 9007 RVA: 0x0007A52C File Offset: 0x0007872C
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
			{
				PropertyDescriptorCollection propertyDescriptorCollection = this._primary.GetProperties();
				if (propertyDescriptorCollection == null)
				{
					propertyDescriptorCollection = this._secondary.GetProperties();
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06002330 RID: 9008 RVA: 0x0007A558 File Offset: 0x00078758
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = this._primary.GetProperties(attributes);
				if (propertyDescriptorCollection == null)
				{
					propertyDescriptorCollection = this._secondary.GetProperties(attributes);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06002331 RID: 9009 RVA: 0x0007A584 File Offset: 0x00078784
			object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
			{
				object obj = this._primary.GetPropertyOwner(pd);
				if (obj == null)
				{
					obj = this._secondary.GetPropertyOwner(pd);
				}
				return obj;
			}

			// Token: 0x040010AF RID: 4271
			private ICustomTypeDescriptor _primary;

			// Token: 0x040010B0 RID: 4272
			private ICustomTypeDescriptor _secondary;
		}

		// Token: 0x02000431 RID: 1073
		private sealed class TypeDescriptionNode : TypeDescriptionProvider
		{
			// Token: 0x06002332 RID: 9010 RVA: 0x0007A5AF File Offset: 0x000787AF
			internal TypeDescriptionNode(TypeDescriptionProvider provider)
			{
				this.Provider = provider;
			}

			// Token: 0x06002333 RID: 9011 RVA: 0x0007A5C0 File Offset: 0x000787C0
			public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				if (argTypes != null)
				{
					if (args == null)
					{
						throw new ArgumentNullException("args");
					}
					if (argTypes.Length != args.Length)
					{
						throw new ArgumentException(SR.GetString("The number of elements in the Type and Object arrays must match."));
					}
				}
				return this.Provider.CreateInstance(provider, objectType, argTypes, args);
			}

			// Token: 0x06002334 RID: 9012 RVA: 0x0007A61C File Offset: 0x0007881C
			public override IDictionary GetCache(object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return this.Provider.GetCache(instance);
			}

			// Token: 0x06002335 RID: 9013 RVA: 0x0007A638 File Offset: 0x00078838
			public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return new TypeDescriptor.TypeDescriptionNode.DefaultExtendedTypeDescriptor(this, instance);
			}

			// Token: 0x06002336 RID: 9014 RVA: 0x0007A654 File Offset: 0x00078854
			protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return this.Provider.GetExtenderProviders(instance);
			}

			// Token: 0x06002337 RID: 9015 RVA: 0x0007A670 File Offset: 0x00078870
			public override string GetFullComponentName(object component)
			{
				if (component == null)
				{
					throw new ArgumentNullException("component");
				}
				return this.Provider.GetFullComponentName(component);
			}

			// Token: 0x06002338 RID: 9016 RVA: 0x0007A68C File Offset: 0x0007888C
			public override Type GetReflectionType(Type objectType, object instance)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				return this.Provider.GetReflectionType(objectType, instance);
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x0007A6AF File Offset: 0x000788AF
			public override Type GetRuntimeType(Type objectType)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				return this.Provider.GetRuntimeType(objectType);
			}

			// Token: 0x0600233A RID: 9018 RVA: 0x0007A6D1 File Offset: 0x000788D1
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				if (instance != null && !objectType.IsInstanceOfType(instance))
				{
					throw new ArgumentException("instance");
				}
				return new TypeDescriptor.TypeDescriptionNode.DefaultTypeDescriptor(this, objectType, instance);
			}

			// Token: 0x0600233B RID: 9019 RVA: 0x0007A70B File Offset: 0x0007890B
			public override bool IsSupportedType(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.Provider.IsSupportedType(type);
			}

			// Token: 0x040010B1 RID: 4273
			internal TypeDescriptor.TypeDescriptionNode Next;

			// Token: 0x040010B2 RID: 4274
			internal TypeDescriptionProvider Provider;

			// Token: 0x02000432 RID: 1074
			private struct DefaultExtendedTypeDescriptor : ICustomTypeDescriptor
			{
				// Token: 0x0600233C RID: 9020 RVA: 0x0007A72D File Offset: 0x0007892D
				internal DefaultExtendedTypeDescriptor(TypeDescriptor.TypeDescriptionNode node, object instance)
				{
					this._node = node;
					this._instance = instance;
				}

				// Token: 0x0600233D RID: 9021 RVA: 0x0007A740 File Offset: 0x00078940
				AttributeCollection ICustomTypeDescriptor.GetAttributes()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedAttributes(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					AttributeCollection attributes = extendedTypeDescriptor.GetAttributes();
					if (attributes == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetAttributes"
						}));
					}
					return attributes;
				}

				// Token: 0x0600233E RID: 9022 RVA: 0x0007A7F8 File Offset: 0x000789F8
				string ICustomTypeDescriptor.GetClassName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedClassName(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					string text = extendedTypeDescriptor.GetClassName();
					if (text == null)
					{
						text = this._instance.GetType().FullName;
					}
					return text;
				}

				// Token: 0x0600233F RID: 9023 RVA: 0x0007A88C File Offset: 0x00078A8C
				string ICustomTypeDescriptor.GetComponentName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedComponentName(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetComponentName();
				}

				// Token: 0x06002340 RID: 9024 RVA: 0x0007A908 File Offset: 0x00078B08
				TypeConverter ICustomTypeDescriptor.GetConverter()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedConverter(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					TypeConverter converter = extendedTypeDescriptor.GetConverter();
					if (converter == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetConverter"
						}));
					}
					return converter;
				}

				// Token: 0x06002341 RID: 9025 RVA: 0x0007A9C0 File Offset: 0x00078BC0
				EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedDefaultEvent(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetDefaultEvent();
				}

				// Token: 0x06002342 RID: 9026 RVA: 0x0007AA3C File Offset: 0x00078C3C
				PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedDefaultProperty(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetDefaultProperty();
				}

				// Token: 0x06002343 RID: 9027 RVA: 0x0007AAB8 File Offset: 0x00078CB8
				object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
				{
					if (editorBaseType == null)
					{
						throw new ArgumentNullException("editorBaseType");
					}
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedEditor(this._instance, editorBaseType);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetEditor(editorBaseType);
				}

				// Token: 0x06002344 RID: 9028 RVA: 0x0007AB4C File Offset: 0x00078D4C
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedEvents(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					EventDescriptorCollection events = extendedTypeDescriptor.GetEvents();
					if (events == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetEvents"
						}));
					}
					return events;
				}

				// Token: 0x06002345 RID: 9029 RVA: 0x0007AC04 File Offset: 0x00078E04
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedEvents(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					EventDescriptorCollection events = extendedTypeDescriptor.GetEvents(attributes);
					if (events == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetEvents"
						}));
					}
					return events;
				}

				// Token: 0x06002346 RID: 9030 RVA: 0x0007ACBC File Offset: 0x00078EBC
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedProperties(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					PropertyDescriptorCollection properties = extendedTypeDescriptor.GetProperties();
					if (properties == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetProperties"
						}));
					}
					return properties;
				}

				// Token: 0x06002347 RID: 9031 RVA: 0x0007AD74 File Offset: 0x00078F74
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedProperties(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					PropertyDescriptorCollection properties = extendedTypeDescriptor.GetProperties(attributes);
					if (properties == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetProperties"
						}));
					}
					return properties;
				}

				// Token: 0x06002348 RID: 9032 RVA: 0x0007AE2C File Offset: 0x0007902C
				object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedPropertyOwner(this._instance, pd);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					object obj = extendedTypeDescriptor.GetPropertyOwner(pd);
					if (obj == null)
					{
						obj = this._instance;
					}
					return obj;
				}

				// Token: 0x040010B3 RID: 4275
				private TypeDescriptor.TypeDescriptionNode _node;

				// Token: 0x040010B4 RID: 4276
				private object _instance;
			}

			// Token: 0x02000433 RID: 1075
			private struct DefaultTypeDescriptor : ICustomTypeDescriptor
			{
				// Token: 0x06002349 RID: 9033 RVA: 0x0007AEB6 File Offset: 0x000790B6
				internal DefaultTypeDescriptor(TypeDescriptor.TypeDescriptionNode node, Type objectType, object instance)
				{
					this._node = node;
					this._objectType = objectType;
					this._instance = instance;
				}

				// Token: 0x0600234A RID: 9034 RVA: 0x0007AED0 File Offset: 0x000790D0
				AttributeCollection ICustomTypeDescriptor.GetAttributes()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					AttributeCollection attributeCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						attributeCollection = reflectTypeDescriptionProvider.GetAttributes(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						attributeCollection = typeDescriptor.GetAttributes();
						if (attributeCollection == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetAttributes"
							}));
						}
					}
					return attributeCollection;
				}

				// Token: 0x0600234B RID: 9035 RVA: 0x0007AF94 File Offset: 0x00079194
				string ICustomTypeDescriptor.GetClassName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					string text;
					if (reflectTypeDescriptionProvider != null)
					{
						text = reflectTypeDescriptionProvider.GetClassName(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						text = typeDescriptor.GetClassName();
						if (text == null)
						{
							text = this._objectType.FullName;
						}
					}
					return text;
				}

				// Token: 0x0600234C RID: 9036 RVA: 0x0007B02C File Offset: 0x0007922C
				string ICustomTypeDescriptor.GetComponentName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					string text;
					if (reflectTypeDescriptionProvider != null)
					{
						text = reflectTypeDescriptionProvider.GetComponentName(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						text = typeDescriptor.GetComponentName();
					}
					return text;
				}

				// Token: 0x0600234D RID: 9037 RVA: 0x0007B0B8 File Offset: 0x000792B8
				TypeConverter ICustomTypeDescriptor.GetConverter()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					TypeConverter typeConverter;
					if (reflectTypeDescriptionProvider != null)
					{
						typeConverter = reflectTypeDescriptionProvider.GetConverter(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						typeConverter = typeDescriptor.GetConverter();
						if (typeConverter == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetConverter"
							}));
						}
					}
					return typeConverter;
				}

				// Token: 0x0600234E RID: 9038 RVA: 0x0007B180 File Offset: 0x00079380
				EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					EventDescriptor eventDescriptor;
					if (reflectTypeDescriptionProvider != null)
					{
						eventDescriptor = reflectTypeDescriptionProvider.GetDefaultEvent(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						eventDescriptor = typeDescriptor.GetDefaultEvent();
					}
					return eventDescriptor;
				}

				// Token: 0x0600234F RID: 9039 RVA: 0x0007B20C File Offset: 0x0007940C
				PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					PropertyDescriptor propertyDescriptor;
					if (reflectTypeDescriptionProvider != null)
					{
						propertyDescriptor = reflectTypeDescriptionProvider.GetDefaultProperty(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						propertyDescriptor = typeDescriptor.GetDefaultProperty();
					}
					return propertyDescriptor;
				}

				// Token: 0x06002350 RID: 9040 RVA: 0x0007B298 File Offset: 0x00079498
				object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
				{
					if (editorBaseType == null)
					{
						throw new ArgumentNullException("editorBaseType");
					}
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					object obj;
					if (reflectTypeDescriptionProvider != null)
					{
						obj = reflectTypeDescriptionProvider.GetEditor(this._objectType, this._instance, editorBaseType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						obj = typeDescriptor.GetEditor(editorBaseType);
					}
					return obj;
				}

				// Token: 0x06002351 RID: 9041 RVA: 0x0007B33C File Offset: 0x0007953C
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					EventDescriptorCollection eventDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						eventDescriptorCollection = reflectTypeDescriptionProvider.GetEvents(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						eventDescriptorCollection = typeDescriptor.GetEvents();
						if (eventDescriptorCollection == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetEvents"
							}));
						}
					}
					return eventDescriptorCollection;
				}

				// Token: 0x06002352 RID: 9042 RVA: 0x0007B400 File Offset: 0x00079600
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					EventDescriptorCollection eventDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						eventDescriptorCollection = reflectTypeDescriptionProvider.GetEvents(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						eventDescriptorCollection = typeDescriptor.GetEvents(attributes);
						if (eventDescriptorCollection == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetEvents"
							}));
						}
					}
					return eventDescriptorCollection;
				}

				// Token: 0x06002353 RID: 9043 RVA: 0x0007B4C4 File Offset: 0x000796C4
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					PropertyDescriptorCollection propertyDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						propertyDescriptorCollection = reflectTypeDescriptionProvider.GetProperties(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						propertyDescriptorCollection = typeDescriptor.GetProperties();
						if (propertyDescriptorCollection == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetProperties"
							}));
						}
					}
					return propertyDescriptorCollection;
				}

				// Token: 0x06002354 RID: 9044 RVA: 0x0007B588 File Offset: 0x00079788
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					PropertyDescriptorCollection propertyDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						propertyDescriptorCollection = reflectTypeDescriptionProvider.GetProperties(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						propertyDescriptorCollection = typeDescriptor.GetProperties(attributes);
						if (propertyDescriptorCollection == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetProperties"
							}));
						}
					}
					return propertyDescriptorCollection;
				}

				// Token: 0x06002355 RID: 9045 RVA: 0x0007B64C File Offset: 0x0007984C
				object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					object obj;
					if (reflectTypeDescriptionProvider != null)
					{
						obj = reflectTypeDescriptionProvider.GetPropertyOwner(this._objectType, this._instance, pd);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						obj = typeDescriptor.GetPropertyOwner(pd);
						if (obj == null)
						{
							obj = this._instance;
						}
					}
					return obj;
				}

				// Token: 0x040010B5 RID: 4277
				private TypeDescriptor.TypeDescriptionNode _node;

				// Token: 0x040010B6 RID: 4278
				private Type _objectType;

				// Token: 0x040010B7 RID: 4279
				private object _instance;
			}
		}

		// Token: 0x02000434 RID: 1076
		[TypeDescriptionProvider("System.Windows.Forms.ComponentModel.Com2Interop.ComNativeDescriptor, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		private sealed class TypeDescriptorComObject
		{
		}

		// Token: 0x02000435 RID: 1077
		private sealed class TypeDescriptorInterface
		{
		}
	}
}
