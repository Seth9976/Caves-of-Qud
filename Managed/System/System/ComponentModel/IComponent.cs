using System;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
	/// <summary>Provides functionality required by all components. </summary>
	// Token: 0x02000419 RID: 1049
	[Designer("System.Windows.Forms.Design.ComponentDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[TypeConverter(typeof(ComponentConverter))]
	[ComVisible(true)]
	[RootDesignerSerializer("System.ComponentModel.Design.Serialization.RootCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true)]
	[Designer("System.ComponentModel.Design.ComponentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IDesigner))]
	public interface IComponent : IDisposable
	{
		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.ComponentModel.IComponent" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> object associated with the component; or null, if the component does not have a site.</returns>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060021D1 RID: 8657
		// (set) Token: 0x060021D2 RID: 8658
		ISite Site { get; set; }

		/// <summary>Represents the method that handles the <see cref="E:System.ComponentModel.IComponent.Disposed" /> event of a component.</summary>
		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060021D3 RID: 8659
		// (remove) Token: 0x060021D4 RID: 8660
		event EventHandler Disposed;
	}
}
