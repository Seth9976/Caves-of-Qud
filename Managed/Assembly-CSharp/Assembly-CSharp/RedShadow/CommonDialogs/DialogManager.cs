using System;
using System.Collections.Generic;
using UnityEngine;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001F5 RID: 501
	public class DialogManager : MonoBehaviour
	{
		// Token: 0x060011F3 RID: 4595 RVA: 0x0006AEFA File Offset: 0x000690FA
		protected void Awake()
		{
			DialogManager.Instance = this;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0006AF02 File Offset: 0x00069102
		public static FileDialog createFileDialog()
		{
			FileDialog fileDialog = global::UnityEngine.Object.Instantiate<FileDialog>(DialogManager.Instance.FileDialogPrefab);
			fileDialog.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return fileDialog;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x0006AF2E File Offset: 0x0006912E
		public static MessageDialog createMessageDialog()
		{
			MessageDialog messageDialog = global::UnityEngine.Object.Instantiate<MessageDialog>(DialogManager.Instance.MessageDialogPrefab);
			messageDialog.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return messageDialog;
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x0006AF5A File Offset: 0x0006915A
		public static NotificationMessage createNotificationMessage()
		{
			NotificationMessage notificationMessage = global::UnityEngine.Object.Instantiate<NotificationMessage>(DialogManager.Instance.NotificationMessagePrefab);
			notificationMessage.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return notificationMessage;
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x0006AF86 File Offset: 0x00069186
		public static ProgressDialog createProgressDialog()
		{
			ProgressDialog progressDialog = global::UnityEngine.Object.Instantiate<ProgressDialog>(DialogManager.Instance.ProgressDialogPrefab);
			progressDialog.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return progressDialog;
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0006AFB2 File Offset: 0x000691B2
		public static InputDialog createInputDialog()
		{
			InputDialog inputDialog = global::UnityEngine.Object.Instantiate<InputDialog>(DialogManager.Instance.InputDialogPrefab);
			inputDialog.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return inputDialog;
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x0006AFDE File Offset: 0x000691DE
		public static LoginDialog createLoginDialog()
		{
			LoginDialog loginDialog = global::UnityEngine.Object.Instantiate<LoginDialog>(DialogManager.Instance.LoginDialogPrefab);
			loginDialog.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return loginDialog;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0006B00A File Offset: 0x0006920A
		public static LoginDialog createPairDialog()
		{
			LoginDialog loginDialog = global::UnityEngine.Object.Instantiate<LoginDialog>(DialogManager.Instance.PairDialogPrefab);
			loginDialog.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return loginDialog;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0006B036 File Offset: 0x00069236
		public static Menu createMenu()
		{
			Menu menu = global::UnityEngine.Object.Instantiate<Menu>(DialogManager.Instance.MenuPrefab);
			menu.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return menu;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0006B062 File Offset: 0x00069262
		public static MenuBar createMenuBar()
		{
			MenuBar menuBar = global::UnityEngine.Object.Instantiate<MenuBar>(DialogManager.Instance.MenuBarPrefab);
			menuBar.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return menuBar;
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0006B08E File Offset: 0x0006928E
		public static ToolBar createToolBar()
		{
			ToolBar toolBar = global::UnityEngine.Object.Instantiate<ToolBar>(DialogManager.Instance.ToolBarPrefab);
			toolBar.transform.SetParent(DialogManager.Instance.gameObject.transform, false);
			return toolBar;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0006B0BA File Offset: 0x000692BA
		public static void getSaveFile(string text, string extensions, Action<string> callback)
		{
			DialogManager.createFileDialog().show(FileDialogMode.Save, text, extensions, callback, null, null);
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0006B0CC File Offset: 0x000692CC
		public static void getLoadFile(string text, string extensions, Action<string> callback)
		{
			DialogManager.createFileDialog().show(FileDialogMode.Load, text, extensions, callback, null, null);
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0006B0DE File Offset: 0x000692DE
		public static void selectPath(string text, string extensions, Action<string> callback)
		{
			DialogManager.createFileDialog().show(FileDialogMode.Path, text, extensions, callback, null, null);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0006B0F0 File Offset: 0x000692F0
		public static void error(string text)
		{
			MessageDialog messageDialog = DialogManager.createMessageDialog();
			messageDialog.show(text, delegate(Buttons r)
			{
			}, messageDialog.ErrorSprite, Buttons.Ok);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0006B130 File Offset: 0x00069330
		public static void warning(string text)
		{
			MessageDialog messageDialog = DialogManager.createMessageDialog();
			messageDialog.show(text, delegate(Buttons r)
			{
			}, messageDialog.WarningSprite, Buttons.Ok);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0006B170 File Offset: 0x00069370
		public static void info(string text)
		{
			MessageDialog messageDialog = DialogManager.createMessageDialog();
			messageDialog.show(text, delegate(Buttons r)
			{
			}, messageDialog.InfoSprite, Buttons.Ok);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0006B1B0 File Offset: 0x000693B0
		public static void confirm(string text, Action<Buttons> callback)
		{
			MessageDialog messageDialog = DialogManager.createMessageDialog();
			messageDialog.show(text, callback, messageDialog.QuestionSprite, Buttons.Yes | Buttons.No);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0006B1D3 File Offset: 0x000693D3
		public static void notify(string text, Sprite icon = null)
		{
			DialogManager.createNotificationMessage().show(text, icon);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0006B1E1 File Offset: 0x000693E1
		public static void getString(string text, string defaultText, Action<string> callback, InputType type = InputType.String)
		{
			DialogManager.createInputDialog().getString(text, defaultText, callback, type);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0006B1F1 File Offset: 0x000693F1
		public static void getFloat(string text, float defaultValue, float minValue, float maxValue, Action<float> callback)
		{
			DialogManager.createInputDialog().getFloat(text, defaultValue, minValue, maxValue, callback);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0006B203 File Offset: 0x00069403
		public static void getInt(string text, int defaultValue, int minValue, int maxValue, Action<int> callback)
		{
			DialogManager.createInputDialog().getInt(text, defaultValue, minValue, maxValue, callback);
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0006B215 File Offset: 0x00069415
		public static void getChoice(string text, int defaultChoice, List<string> choices, Action<int> callback)
		{
			DialogManager.createInputDialog().getChoice(text, defaultChoice, choices, callback);
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0006B225 File Offset: 0x00069425
		public static void getLogin(string text, string defaultUserName, string defaultPassword, Action<string, string> callback)
		{
			DialogManager.createLoginDialog().getLogin(text, defaultUserName, defaultPassword, callback);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0006B235 File Offset: 0x00069435
		public static void getPair(string text, string defaultName, string defaultValue, Action<string, string> callback)
		{
			DialogManager.createPairDialog().getLogin(text, defaultName, defaultValue, callback);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0006B248 File Offset: 0x00069448
		public static void clear()
		{
			foreach (object obj in DialogManager.Instance.transform)
			{
				global::UnityEngine.Object.Destroy(((Transform)obj).gameObject);
			}
		}

		// Token: 0x04000BE9 RID: 3049
		public static DialogManager Instance;

		// Token: 0x04000BEA RID: 3050
		public FileDialog FileDialogPrefab;

		// Token: 0x04000BEB RID: 3051
		public MessageDialog MessageDialogPrefab;

		// Token: 0x04000BEC RID: 3052
		public NotificationMessage NotificationMessagePrefab;

		// Token: 0x04000BED RID: 3053
		public ProgressDialog ProgressDialogPrefab;

		// Token: 0x04000BEE RID: 3054
		public InputDialog InputDialogPrefab;

		// Token: 0x04000BEF RID: 3055
		public LoginDialog LoginDialogPrefab;

		// Token: 0x04000BF0 RID: 3056
		public LoginDialog PairDialogPrefab;

		// Token: 0x04000BF1 RID: 3057
		public Menu MenuPrefab;

		// Token: 0x04000BF2 RID: 3058
		public MenuBar MenuBarPrefab;

		// Token: 0x04000BF3 RID: 3059
		public ToolBar ToolBarPrefab;
	}
}
