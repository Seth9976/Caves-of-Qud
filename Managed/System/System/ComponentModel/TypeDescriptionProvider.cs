using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides supplemental metadata to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020003F6 RID: 1014
	public abstract class TypeDescriptionProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class.</summary>
		// Token: 0x0600210A RID: 8458 RVA: 0x0000219B File Offset: 0x0000039B
		protected TypeDescriptionProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class using a parent type description provider.</summary>
		/// <param name="parent">The parent type description provider.</param>
		// Token: 0x0600210B RID: 8459 RVA: 0x00071C17 File Offset: 0x0006FE17
		protected TypeDescriptionProvider(TypeDescriptionProvider parent)
		{
			this._parent = parent;
		}

		/// <summary>Creates an object that can substitute for another data type.</summary>
		/// <returns>The substitute <see cref="T:System.Object" />.</returns>
		/// <param name="provider">An optional service provider.</param>
		/// <param name="objectType">The type of object to create. This parameter is never null.</param>
		/// <param name="argTypes">An optional array of types that represent the parameter types to be passed to the object's constructor. This array can be null or of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor.</param>
		// Token: 0x0600210C RID: 8460 RVA: 0x00071C26 File Offset: 0x0006FE26
		public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (this._parent != null)
			{
				return this._parent.CreateInstance(provider, objectType, argTypes, args);
			}
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			return Activator.CreateInstance(objectType, args);
		}

		/// <summary>Gets a per-object cache, accessed as an <see cref="T:System.Collections.IDictionary" /> of key/value pairs.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> if the provided object supports caching; otherwise, null.</returns>
		/// <param name="instance">The object for which to get the cache.</param>
		// Token: 0x0600210D RID: 8461 RVA: 0x00071C5D File Offset: 0x0006FE5D
		public virtual IDictionary GetCache(object instance)
		{
			TypeDescriptionProvider parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetCache(instance);
		}

		/// <summary>Gets an extended custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide extended metadata for the object.</returns>
		/// <param name="instance">The object for which to get the extended type descriptor.</param>
		// Token: 0x0600210E RID: 8462 RVA: 0x00071C74 File Offset: 0x0006FE74
		public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtendedTypeDescriptor(instance);
			}
			TypeDescriptionProvider.EmptyCustomTypeDescriptor emptyCustomTypeDescriptor;
			if ((emptyCustomTypeDescriptor = this._emptyDescriptor) == null)
			{
				emptyCustomTypeDescriptor = (this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor());
			}
			return emptyCustomTypeDescriptor;
		}

		/// <summary>Gets the extender providers for the specified object.</summary>
		/// <returns>An array of extender providers for <paramref name="instance" />.</returns>
		/// <param name="instance">The object to get extender providers for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x0600210F RID: 8463 RVA: 0x00071CAE File Offset: 0x0006FEAE
		protected internal virtual IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtenderProviders(instance);
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return Array.Empty<IExtenderProvider>();
		}

		/// <summary>Gets the name of the specified component, or null if the component has no name.</summary>
		/// <returns>The name of the specified component.</returns>
		/// <param name="component">The specified component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x06002110 RID: 8464 RVA: 0x00071CD8 File Offset: 0x0006FED8
		public virtual string GetFullComponentName(object component)
		{
			if (this._parent != null)
			{
				return this._parent.GetFullComponentName(component);
			}
			return this.GetTypeDescriptor(component).GetComponentName();
		}

		/// <summary>Performs normal reflection against a type.</summary>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is null.</exception>
		// Token: 0x06002111 RID: 8465 RVA: 0x00071CFB File Offset: 0x0006FEFB
		public Type GetReflectionType(Type objectType)
		{
			return this.GetReflectionType(objectType, null);
		}

		/// <summary>Performs normal reflection against the given object.</summary>
		/// <returns>The type of reflection for this <paramref name="instance" />.</returns>
		/// <param name="instance">An instance of the type (should not be null).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06002112 RID: 8466 RVA: 0x00071D05 File Offset: 0x0006FF05
		public Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetReflectionType(instance.GetType(), instance);
		}

		/// <summary>Performs normal reflection against the given object with the given type.</summary>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <param name="instance">An instance of the type. Can be null.</param>
		// Token: 0x06002113 RID: 8467 RVA: 0x00071D22 File Offset: 0x0006FF22
		public virtual Type GetReflectionType(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetReflectionType(objectType, instance);
			}
			return objectType;
		}

		/// <summary>Converts a reflection type into a runtime type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the runtime equivalent of <paramref name="reflectionType" />.</returns>
		/// <param name="reflectionType">The type to convert to its runtime equivalent.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionType" /> is null.</exception>
		// Token: 0x06002114 RID: 8468 RVA: 0x00071D3C File Offset: 0x0006FF3C
		public virtual Type GetRuntimeType(Type reflectionType)
		{
			if (this._parent != null)
			{
				return this._parent.GetRuntimeType(reflectionType);
			}
			if (reflectionType == null)
			{
				throw new ArgumentNullException("reflectionType");
			}
			if (reflectionType.GetType().Assembly == typeof(object).Assembly)
			{
				return reflectionType;
			}
			return reflectionType.UnderlyingSystemType;
		}

		/// <summary>Gets a custom type descriptor for the given type.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		// Token: 0x06002115 RID: 8469 RVA: 0x00071D9B File Offset: 0x0006FF9B
		public ICustomTypeDescriptor GetTypeDescriptor(Type objectType)
		{
			return this.GetTypeDescriptor(objectType, null);
		}

		/// <summary>Gets a custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06002116 RID: 8470 RVA: 0x00071DA5 File Offset: 0x0006FFA5
		public ICustomTypeDescriptor GetTypeDescriptor(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetTypeDescriptor(instance.GetType(), instance);
		}

		/// <summary>Gets a custom type descriptor for the given type and object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		// Token: 0x06002117 RID: 8471 RVA: 0x00071DC4 File Offset: 0x0006FFC4
		public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetTypeDescriptor(objectType, instance);
			}
			TypeDescriptionProvider.EmptyCustomTypeDescriptor emptyCustomTypeDescriptor;
			if ((emptyCustomTypeDescriptor = this._emptyDescriptor) == null)
			{
				emptyCustomTypeDescriptor = (this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor());
			}
			return emptyCustomTypeDescriptor;
		}

		/// <summary>Gets a value that indicates whether the specified type is compatible with the type description and its chain of type description providers. </summary>
		/// <returns>true if <paramref name="type" /> is compatible with the type description and its chain of type description providers; otherwise, false. </returns>
		/// <param name="type">The type to test for compatibility.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06002118 RID: 8472 RVA: 0x00071DFF File Offset: 0x0006FFFF
		public virtual bool IsSupportedType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._parent == null || this._parent.IsSupportedType(type);
		}

		// Token: 0x04001015 RID: 4117
		private readonly TypeDescriptionProvider _parent;

		// Token: 0x04001016 RID: 4118
		private TypeDescriptionProvider.EmptyCustomTypeDescriptor _emptyDescriptor;

		// Token: 0x020003F7 RID: 1015
		private sealed class EmptyCustomTypeDescriptor : CustomTypeDescriptor
		{
		}
	}
}
