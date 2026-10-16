using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Supports the page parser in building a template and the child controls it contains.</summary>
	// Token: 0x02000229 RID: 553
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TemplateBuilder : ControlBuilder, ITemplate
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateBuilder" /> class.</summary>
		// Token: 0x060016B2 RID: 5810 RVA: 0x000287AA File Offset: 0x000269AA
		public TemplateBuilder()
		{
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0003AEA8 File Offset: 0x000390A8
		internal TemplateBuilder(ICustomAttributeProvider prov)
		{
			object[] array = prov.GetCustomAttributes(typeof(TemplateContainerAttribute), true);
			if (array.Length != 0)
			{
				this.containerAttribute = (TemplateContainerAttribute)array[0];
			}
			array = prov.GetCustomAttributes(typeof(TemplateInstanceAttribute), true);
			if (array.Length != 0)
			{
				this.instanceAttribute = (TemplateInstanceAttribute)array[0];
			}
		}

		/// <summary>Gets or sets the text between the opening and closing tags of the template.</summary>
		/// <returns>The text that appears between the opening and closing tags of the template.</returns>
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x0003AF03 File Offset: 0x00039103
		// (set) Token: 0x060016B5 RID: 5813 RVA: 0x0003AF0B File Offset: 0x0003910B
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x0003AF14 File Offset: 0x00039114
		internal Type ContainerType
		{
			get
			{
				if (this.containerAttribute == null)
				{
					return null;
				}
				return this.containerAttribute.ContainerType;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x0003AF2C File Offset: 0x0003912C
		internal TemplateInstance? TemplateInstance
		{
			get
			{
				if (this.instanceAttribute == null)
				{
					return null;
				}
				return new TemplateInstance?(this.instanceAttribute.Instances);
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x0003AF5B File Offset: 0x0003915B
		internal BindingDirection BindingDirection
		{
			get
			{
				if (this.containerAttribute == null)
				{
					return BindingDirection.TwoWay;
				}
				return this.containerAttribute.BindingDirection;
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x0003AF72 File Offset: 0x00039172
		internal void RegisterBoundProperty(Type controlType, string controlProperty, string controlId, string fieldName)
		{
			if (this.bindings == null)
			{
				this.bindings = new List<TemplateBinding>();
			}
			this.bindings.Add(new TemplateBinding(controlType, controlProperty, controlId, fieldName));
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x0003AF9C File Offset: 0x0003919C
		internal ICollection Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		/// <summary>Used during design time to build the template and its child controls. </summary>
		/// <returns>A reference to the instance of the <see cref="T:System.Web.UI.TemplateBuilder" /> class.</returns>
		// Token: 0x060016BB RID: 5819 RVA: 0x0003AFA4 File Offset: 0x000391A4
		public override object BuildObject()
		{
			return base.BuildObject();
		}

		/// <summary>Initializes the template builder when a Web request is made.</summary>
		/// <param name="parser">The <see cref="T:System.Web.UI.TemplateParser" /> responsible for parsing the control. </param>
		/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> responsible for building the control. </param>
		/// <param name="type">The <see cref="T:System.Type" /> assigned to the control that the builder will create. </param>
		/// <param name="tagName">The name of the tag to build. This allows the builder to support multiple tag types. </param>
		/// <param name="ID">The <see cref="P:System.Web.UI.ControlBuilder.ID" /> assigned to the control. </param>
		/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> that holds all the specified tag attributes. </param>
		// Token: 0x060016BC RID: 5820 RVA: 0x0003AFAC File Offset: 0x000391AC
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string ID, IDictionary attribs)
		{
			if (parser != null)
			{
				base.FileName = parser.InputFile;
			}
			base.Init(parser, parentBuilder, type, tagName, ID, attribs);
		}

		/// <summary>Defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to in design time.</summary>
		/// <param name="container">The <see cref="T:System.Web.UI.Control" /> to contain the instances of controls from the inline template.</param>
		// Token: 0x060016BD RID: 5821 RVA: 0x0003AFCC File Offset: 0x000391CC
		public virtual void InstantiateIn(Control container)
		{
			this.CreateChildren(container);
		}

		/// <summary>Determines if the control builder needs to get its inner text.</summary>
		/// <returns>true if the control builder needs to get its inner text. The default is false.</returns>
		// Token: 0x060016BE RID: 5822 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool NeedsTagInnerText()
		{
			return false;
		}

		/// <summary>Saves the inner text of the template tag.</summary>
		/// <param name="text">The inner text of the template.</param>
		// Token: 0x060016BF RID: 5823 RVA: 0x0003AF0B File Offset: 0x0003910B
		public override void SetTagInnerText(string text)
		{
			this.text = text;
		}

		// Token: 0x0400157F RID: 5503
		private string text;

		// Token: 0x04001580 RID: 5504
		private TemplateContainerAttribute containerAttribute;

		// Token: 0x04001581 RID: 5505
		private TemplateInstanceAttribute instanceAttribute;

		// Token: 0x04001582 RID: 5506
		private List<TemplateBinding> bindings;
	}
}
