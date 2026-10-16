using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace System.Drawing.Design
{
	/// <summary>Provides a <see cref="T:System.Drawing.Design.UITypeEditor" /> that can perform default file searching for cursor (.cur) files.</summary>
	// Token: 0x02000012 RID: 18
	public class CursorEditor : UITypeEditor
	{
		/// <summary>Edits the given object value using the editor style provided by the <see cref="Overload:System.Drawing.Design.CursorEditor.GetEditStyle" /> method.</summary>
		/// <returns>The new value of the object. If the value of the object has not changed, this should return the same object it was passed.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		/// <param name="provider">A service provider object through which editing services may be obtained. </param>
		/// <param name="value">An instance of the value being edited. </param>
		// Token: 0x0600002E RID: 46 RVA: 0x00003028 File Offset: 0x00001228
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider == null)
			{
				return value;
			}
			IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (windowsFormsEditorService == null)
			{
				return value;
			}
			CursorEditor.CursorUI cursorUI = new CursorEditor.CursorUI(this, windowsFormsEditorService, value);
			windowsFormsEditorService.DropDownControl(cursorUI);
			return cursorUI.Value;
		}

		/// <summary>Retrieves the editing style of the <see cref="Overload:System.Drawing.Design.CursorEditor.EditValue" /> method. </summary>
		/// <returns>An enum value indicating the provided editing style. If the method is not supported, this will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
		/// <param name="context">A type descriptor context that can be used to provide additional context information. </param>
		// Token: 0x0600002F RID: 47 RVA: 0x00002454 File Offset: 0x00000654
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002457 File Offset: 0x00000657
		public override bool IsDropDownResizable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x02000013 RID: 19
		private class CursorUI : ListBox
		{
			// Token: 0x06000031 RID: 49 RVA: 0x0000306C File Offset: 0x0000126C
			public CursorUI(UITypeEditor host, IWindowsFormsEditorService service, object value)
			{
				this.service = service;
				this.value = value;
				foreach (object obj in TypeDescriptor.GetConverter(typeof(Cursor)).GetStandardValues())
				{
					base.Items.Add(obj);
				}
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000030E8 File Offset: 0x000012E8
			protected override void OnClick(EventArgs e)
			{
				base.OnClick(e);
				this.value = base.SelectedItem;
				this.service.CloseDropDown();
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000033 RID: 51 RVA: 0x00003108 File Offset: 0x00001308
			public object Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x04000036 RID: 54
			private object value;

			// Token: 0x04000037 RID: 55
			private IWindowsFormsEditorService service;
		}
	}
}
