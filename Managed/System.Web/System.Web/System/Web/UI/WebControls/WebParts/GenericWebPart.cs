using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Wraps server controls that are not <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls so that they can appear and behave as true <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls at run time.</summary>
	// Token: 0x020006D3 RID: 1747
	[ToolboxItem(false)]
	public class GenericWebPart : WebPart
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control by passing in a reference to a control that becomes the child control.</summary>
		/// <param name="control">A server control that becomes the child control of the <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control instance. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="control" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="control" /> is of type <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />.- or -<paramref name="control" /> has a null or empty <see cref="P:System.Web.UI.Control.ID" /> property.- or -<paramref name="control" /> has been output-cached.</exception>
		// Token: 0x06004A0E RID: 18958 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal GenericWebPart(Control control)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to the child control that is wrapped by a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control at run time.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Control" /> that refers to the wrapped child control.</returns>
		// Token: 0x170016EF RID: 5871
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Control ChildControl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the child control, to enable the control to be edited by custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <returns>An <see cref="T:System.Object" /> that consists of the child control of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control. </returns>
		// Token: 0x170016F0 RID: 5872
		// (get) Token: 0x06004A10 RID: 18960 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override object WebBrowsableObject
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls that can be used to edit the child control of a <see cref="T:System.Web.UI.WebControls.WebParts.GenericWebPart" /> control when it is in edit mode.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with a server control.</returns>
		// Token: 0x06004A11 RID: 18961 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public override EditorPartCollection CreateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
