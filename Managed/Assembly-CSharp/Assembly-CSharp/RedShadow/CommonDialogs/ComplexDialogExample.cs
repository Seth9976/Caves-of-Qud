using System;
using UnityEngine;
using UnityEngine.Events;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001EA RID: 490
	public class ComplexDialogExample : DialogBase
	{
		// Token: 0x0600118E RID: 4494 RVA: 0x000699E4 File Offset: 0x00067BE4
		public override void Start()
		{
			this.setupMenuBar();
			this.setupToolBar();
			if (this.TitleBar != null)
			{
				this.TitleBar.onClose.AddListener(new UnityAction(this.cancel));
				this.TitleBar.setTitleText("Text Editor");
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00069A39 File Offset: 0x00067C39
		public void showDialog()
		{
			base.show();
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00069A44 File Offset: 0x00067C44
		private void setupMenuBar()
		{
			Menu menu = DialogManager.createMenu();
			menu.addItem(this.FileNewIcon, "New").setHotKeyText("Ctrl+N").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu.addItem(this.FileOpenIcon, "Open..").setHotKeyText("Ctrl+O").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu.addItem(this.FileSaveIcon, "Save").setHotKeyText("Ctrl+S").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu.addItem(this.FileSaveAsIcon, "Save As...").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu.addSeparator();
			menu.addItem(this.FileExitIcon, "Exit").setHotKeyText("Alt+F4").setCallback(new UnityAction(this.cancel));
			this.MenuBar.addMenu("File", menu);
			Menu menu2 = DialogManager.createMenu();
			menu2.addItem(this.EditUndoIcon, "Undo").setHotKeyText("Ctrl+Z").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu2.addItem(this.EditRedoIcon, "Redo").setHotKeyText("Ctrl+Y").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu2.addSeparator();
			menu2.addItem(this.EditCutIcon, "Cut").setHotKeyText("Ctrl+X").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu2.addItem(this.EditCopyIcon, "Copy").setHotKeyText("Ctrl+C").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu2.addItem(this.EditPasteIcon, "Paste").setHotKeyText("Ctrl+V").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu2.addItem(this.EditDeleteIcon, "Delete").setHotKeyText("Del").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu2.addSeparator();
			menu2.addItem(this.EditSelectAllIcon, "Select All").setHotKeyText("Ctrl+A").setCallback(new UnityAction(this.exampleMenuItemHandler));
			menu2.addSeparator();
			menu2.addItem(this.EditSettingsIcon, "Settings").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.MenuBar.addMenu("Edit", menu2);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00069CD0 File Offset: 0x00067ED0
		private void setupToolBar()
		{
			this.ToolBar.addButton(this.FileNewIcon, "New").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addButton(this.FileOpenIcon, "Open").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addButton(this.FileSaveIcon, "Save").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addButton(this.FileSaveAsIcon, "Save As").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addSeparator();
			this.ToolBar.addButton(this.EditUndoIcon, "Undo").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addButton(this.EditRedoIcon, "Redo").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addSeparator();
			this.ToolBar.addButton(this.EditCutIcon, "Cut").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addButton(this.EditCopyIcon, "Copy").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addButton(this.EditPasteIcon, "Paste").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addButton(this.EditDeleteIcon, "Delete").setCallback(new UnityAction(this.exampleMenuItemHandler));
			this.ToolBar.addSeparator();
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00069E91 File Offset: 0x00068091
		private void exampleMenuItemHandler()
		{
		}

		// Token: 0x04000BA2 RID: 2978
		public TitleBar TitleBar;

		// Token: 0x04000BA3 RID: 2979
		public MenuBar MenuBar;

		// Token: 0x04000BA4 RID: 2980
		public ToolBar ToolBar;

		// Token: 0x04000BA5 RID: 2981
		public StatusBar StatusBar;

		// Token: 0x04000BA6 RID: 2982
		public Sprite FileNewIcon;

		// Token: 0x04000BA7 RID: 2983
		public Sprite FileOpenIcon;

		// Token: 0x04000BA8 RID: 2984
		public Sprite FileSaveIcon;

		// Token: 0x04000BA9 RID: 2985
		public Sprite FileSaveAsIcon;

		// Token: 0x04000BAA RID: 2986
		public Sprite FileExitIcon;

		// Token: 0x04000BAB RID: 2987
		public Sprite EditUndoIcon;

		// Token: 0x04000BAC RID: 2988
		public Sprite EditRedoIcon;

		// Token: 0x04000BAD RID: 2989
		public Sprite EditCopyIcon;

		// Token: 0x04000BAE RID: 2990
		public Sprite EditCutIcon;

		// Token: 0x04000BAF RID: 2991
		public Sprite EditPasteIcon;

		// Token: 0x04000BB0 RID: 2992
		public Sprite EditDeleteIcon;

		// Token: 0x04000BB1 RID: 2993
		public Sprite EditSelectAllIcon;

		// Token: 0x04000BB2 RID: 2994
		public Sprite EditSettingsIcon;
	}
}
