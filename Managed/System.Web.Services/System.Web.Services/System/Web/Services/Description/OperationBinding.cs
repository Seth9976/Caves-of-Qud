using System;
using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Provides specifications for protocols and data formats for the messages used in the action supported by the XML Web service. This class cannot be inherited.</summary>
	// Token: 0x020000F1 RID: 241
	[XmlFormatExtensionPoint("Extensions")]
	public sealed class OperationBinding : NamedItem
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0001C4EE File Offset: 0x0001A6EE
		internal void SetParent(Binding parent)
		{
			this.parent = parent;
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.Binding" /> of which the current <see cref="T:System.Web.Services.Description.OperationBinding" /> is a member.</summary>
		/// <returns>A binding of which the current <see cref="T:System.Web.Services.Description.OperationBinding" /> is a member.</returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001C4F7 File Offset: 0x0001A6F7
		public Binding Binding
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Gets the collection of extensibility elements specific to the current <see cref="T:System.Web.Services.Description.OperationBinding" />.</summary>
		/// <returns>A collection of extensibility elements.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001C4FF File Offset: 0x0001A6FF
		[XmlIgnore]
		public override ServiceDescriptionFormatExtensionCollection Extensions
		{
			get
			{
				if (this.extensions == null)
				{
					this.extensions = new ServiceDescriptionFormatExtensionCollection(this);
				}
				return this.extensions;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.InputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</summary>
		/// <returns>An <see cref="T:System.Web.Services.Description.InputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001C51B File Offset: 0x0001A71B
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001C523 File Offset: 0x0001A723
		[XmlElement("input")]
		public InputBinding Input
		{
			get
			{
				return this.input;
			}
			set
			{
				if (this.input != null)
				{
					this.input.SetParent(null);
				}
				this.input = value;
				if (this.input != null)
				{
					this.input.SetParent(this);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.OutputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</summary>
		/// <returns>An <see cref="T:System.Web.Services.Description.OutputBinding" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" />.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001C554 File Offset: 0x0001A754
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0001C55C File Offset: 0x0001A75C
		[XmlElement("output")]
		public OutputBinding Output
		{
			get
			{
				return this.output;
			}
			set
			{
				if (this.output != null)
				{
					this.output.SetParent(null);
				}
				this.output = value;
				if (this.output != null)
				{
					this.output.SetParent(this);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.Services.Description.FaultBindingCollection" /> associated with the <see cref="T:System.Web.Services.Description.OperationBinding" /> instance.</summary>
		/// <returns>A fault binding collection.</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001C58D File Offset: 0x0001A78D
		[XmlElement("fault")]
		public FaultBindingCollection Faults
		{
			get
			{
				if (this.faults == null)
				{
					this.faults = new FaultBindingCollection(this);
				}
				return this.faults;
			}
		}

		// Token: 0x040003F3 RID: 1011
		private ServiceDescriptionFormatExtensionCollection extensions;

		// Token: 0x040003F4 RID: 1012
		private FaultBindingCollection faults;

		// Token: 0x040003F5 RID: 1013
		private InputBinding input;

		// Token: 0x040003F6 RID: 1014
		private OutputBinding output;

		// Token: 0x040003F7 RID: 1015
		private Binding parent;
	}
}
