using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Declares the base type of the container control of a property that returns an <see cref="T:System.Web.UI.ITemplate" /> interface and is marked with the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> attribute. The control with the <see cref="T:System.Web.UI.ITemplate" /> property must implement the <see cref="T:System.Web.UI.INamingContainer" /> interface. This class cannot be inherited.</summary>
	// Token: 0x0200022B RID: 555
	[AttributeUsage(AttributeTargets.Property)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TemplateContainerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> class using the specified container type and the <see cref="P:System.Web.UI.TemplateContainerAttribute.BindingDirection" /> property.</summary>
		/// <param name="containerType">The <see cref="T:System.Type" /> for the container control.</param>
		/// <param name="bindingDirection">The <see cref="P:System.Web.UI.TemplateContainerAttribute.BindingDirection" /> for the container control.</param>
		// Token: 0x060016C1 RID: 5825 RVA: 0x0003AFFA File Offset: 0x000391FA
		public TemplateContainerAttribute(Type containerType, BindingDirection bindingDirection)
		{
			this.containerType = containerType;
			this.direction = bindingDirection;
		}

		/// <summary>Gets the binding direction of the container control.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.BindingDirection" /> indicating the container control's binding direction. The default is <see cref="F:System.ComponentModel.BindingDirection.OneWay" />.</returns>
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x060016C2 RID: 5826 RVA: 0x0003B010 File Offset: 0x00039210
		public BindingDirection BindingDirection
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateContainerAttribute" /> class using the specified container type.</summary>
		/// <param name="containerType">The <see cref="T:System.Type" /> for the container control. </param>
		// Token: 0x060016C3 RID: 5827 RVA: 0x0003B018 File Offset: 0x00039218
		public TemplateContainerAttribute(Type containerType)
		{
			this.containerType = containerType;
		}

		/// <summary>Gets the container control type.</summary>
		/// <returns>The container control <see cref="T:System.Type" />.</returns>
		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060016C4 RID: 5828 RVA: 0x0003B027 File Offset: 0x00039227
		public Type ContainerType
		{
			get
			{
				return this.containerType;
			}
		}

		// Token: 0x04001587 RID: 5511
		private Type containerType;

		// Token: 0x04001588 RID: 5512
		private BindingDirection direction;
	}
}
