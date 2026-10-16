using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple default implementation of the <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> interface.</summary>
	// Token: 0x02000396 RID: 918
	public abstract class CustomTypeDescriptor : ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class.</summary>
		// Token: 0x06001E07 RID: 7687 RVA: 0x0000219B File Offset: 0x0000039B
		protected CustomTypeDescriptor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class using a parent custom type descriptor.</summary>
		/// <param name="parent">The parent custom type descriptor.</param>
		// Token: 0x06001E08 RID: 7688 RVA: 0x0006B39B File Offset: 0x0006959B
		protected CustomTypeDescriptor(ICustomTypeDescriptor parent)
		{
			this._parent = parent;
		}

		/// <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
		// Token: 0x06001E09 RID: 7689 RVA: 0x0006B3AA File Offset: 0x000695AA
		public virtual AttributeCollection GetAttributes()
		{
			if (this._parent != null)
			{
				return this._parent.GetAttributes();
			}
			return AttributeCollection.Empty;
		}

		/// <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is null.</returns>
		// Token: 0x06001E0A RID: 7690 RVA: 0x0006B3C5 File Offset: 0x000695C5
		public virtual string GetClassName()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetClassName();
		}

		/// <summary>Returns the name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is null.</returns>
		// Token: 0x06001E0B RID: 7691 RVA: 0x0006B3D8 File Offset: 0x000695D8
		public virtual string GetComponentName()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetComponentName();
		}

		/// <summary>Returns a type converter for the type represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
		// Token: 0x06001E0C RID: 7692 RVA: 0x0006B3EB File Offset: 0x000695EB
		public virtual TypeConverter GetConverter()
		{
			if (this._parent != null)
			{
				return this._parent.GetConverter();
			}
			return new TypeConverter();
		}

		/// <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x06001E0D RID: 7693 RVA: 0x0006B406 File Offset: 0x00069606
		public virtual EventDescriptor GetDefaultEvent()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetDefaultEvent();
		}

		/// <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x06001E0E RID: 7694 RVA: 0x0006B419 File Offset: 0x00069619
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetDefaultProperty();
		}

		/// <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
		/// <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is null.</returns>
		/// <param name="editorBaseType">The base type of the editor to retrieve.</param>
		// Token: 0x06001E0F RID: 7695 RVA: 0x0006B42C File Offset: 0x0006962C
		public virtual object GetEditor(Type editorBaseType)
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetEditor(editorBaseType);
		}

		/// <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		// Token: 0x06001E10 RID: 7696 RVA: 0x0006B440 File Offset: 0x00069640
		public virtual EventDescriptorCollection GetEvents()
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents();
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x06001E11 RID: 7697 RVA: 0x0006B45B File Offset: 0x0006965B
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents(attributes);
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		// Token: 0x06001E12 RID: 7698 RVA: 0x0006B477 File Offset: 0x00069677
		public virtual PropertyDescriptorCollection GetProperties()
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties();
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x06001E13 RID: 7699 RVA: 0x0006B492 File Offset: 0x00069692
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties(attributes);
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is null.</returns>
		/// <param name="pd">The property descriptor for which to retrieve the owning object.</param>
		// Token: 0x06001E14 RID: 7700 RVA: 0x0006B4AE File Offset: 0x000696AE
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			ICustomTypeDescriptor parent = this._parent;
			if (parent == null)
			{
				return null;
			}
			return parent.GetPropertyOwner(pd);
		}

		// Token: 0x04000F27 RID: 3879
		private readonly ICustomTypeDescriptor _parent;
	}
}
