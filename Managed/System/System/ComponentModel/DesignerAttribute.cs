using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the class used to implement design-time services for a component.</summary>
	// Token: 0x02000415 RID: 1045
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the name of the type that provides design-time services.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		// Token: 0x060021B2 RID: 8626 RVA: 0x00073318 File Offset: 0x00071518
		public DesignerAttribute(string designerTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the type that provides design-time services.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to. </param>
		// Token: 0x060021B3 RID: 8627 RVA: 0x00073348 File Offset: 0x00071548
		public DesignerAttribute(Type designerType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the designer type and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseTypeName">The fully qualified name of the base class to associate with the designer class. </param>
		// Token: 0x060021B4 RID: 8628 RVA: 0x00073371 File Offset: 0x00071571
		public DesignerAttribute(string designerTypeName, string designerBaseTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class, using the name of the designer class and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerTypeName" />. </param>
		// Token: 0x060021B5 RID: 8629 RVA: 0x00073393 File Offset: 0x00071593
		public DesignerAttribute(string designerTypeName, Type designerBaseType)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the types of the designer and designer base class.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerType" />. </param>
		// Token: 0x060021B6 RID: 8630 RVA: 0x000733BA File Offset: 0x000715BA
		public DesignerAttribute(Type designerType, Type designerBaseType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base type of this designer.</summary>
		/// <returns>The name of the base type of this designer.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x000733DA File Offset: 0x000715DA
		public string DesignerBaseTypeName
		{
			get
			{
				return this.designerBaseTypeName;
			}
		}

		/// <summary>Gets the name of the designer type associated with this designer attribute.</summary>
		/// <returns>The name of the designer type associated with this designer attribute.</returns>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x000733E2 File Offset: 0x000715E2
		public string DesignerTypeName
		{
			get
			{
				return this.designerTypeName;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000733EC File Offset: 0x000715EC
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.designerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignerAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060021BA RID: 8634 RVA: 0x0007343C File Offset: 0x0007163C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerAttribute designerAttribute = obj as DesignerAttribute;
			return designerAttribute != null && designerAttribute.designerBaseTypeName == this.designerBaseTypeName && designerAttribute.designerTypeName == this.designerTypeName;
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0007347F File Offset: 0x0007167F
		public override int GetHashCode()
		{
			return this.designerTypeName.GetHashCode() ^ this.designerBaseTypeName.GetHashCode();
		}

		// Token: 0x04001044 RID: 4164
		private readonly string designerTypeName;

		// Token: 0x04001045 RID: 4165
		private readonly string designerBaseTypeName;

		// Token: 0x04001046 RID: 4166
		private string typeId;
	}
}
