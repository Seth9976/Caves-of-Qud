using System;
using UnityEngine;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001F6 RID: 502
	public class DialogTest : MonoBehaviour
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x0006B2B0 File Offset: 0x000694B0
		public void Update()
		{
			ProgressDialog progressDialog = DialogBase.getTopmost() as ProgressDialog;
			if (progressDialog && Input.GetKeyDown(KeyCode.Escape))
			{
				progressDialog.close();
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0006B2DF File Offset: 0x000694DF
		public void testSave()
		{
			DialogManager.getSaveFile("Save File Test", "*", delegate(string s)
			{
				DialogManager.notify("Save : " + s, this.Sprite);
			});
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0006B2FC File Offset: 0x000694FC
		public void testLoad()
		{
			DialogManager.getLoadFile("Load File Test", "*", delegate(string s)
			{
				DialogManager.notify("Load : " + s, this.Sprite);
			});
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0006B319 File Offset: 0x00069519
		public void testStringInput()
		{
			DialogManager.getString("String input:", "test string", delegate(string s)
			{
				DialogManager.notify("String Input : " + s, this.Sprite);
			}, InputType.String);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0006B337 File Offset: 0x00069537
		public void testLogin()
		{
			DialogManager.getLogin("Login Test", "username", "password", delegate(string u, string p)
			{
				DialogManager.notify("UserName : " + u, this.Sprite);
			});
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0006B359 File Offset: 0x00069559
		public void testNotification()
		{
			this._counter++;
			DialogManager.notify("Hello\n   World!!!!!\nCounter: " + this._counter.ToString(), this.Sprite);
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0006B389 File Offset: 0x00069589
		public void testProgress()
		{
			DialogManager.createProgressDialog().show("Testing progress dialog...\nPress Escape key to exit.", true);
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0006B39B File Offset: 0x0006959B
		public void testComplexDialog()
		{
			this.ComplexDialog.showDialog();
		}

		// Token: 0x04000BF4 RID: 3060
		public Sprite Sprite;

		// Token: 0x04000BF5 RID: 3061
		public ComplexDialogExample ComplexDialog;

		// Token: 0x04000BF6 RID: 3062
		private int _counter;
	}
}
