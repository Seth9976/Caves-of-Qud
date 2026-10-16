using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Uniject;
using UnityEngine.EventSystems;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200006C RID: 108
	internal class UIFakeStore : FakeStore
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00007EC9 File Offset: 0x000060C9
		public UIFakeStore()
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00007ED1 File Offset: 0x000060D1
		public UIFakeStore(IUtil util)
		{
			this.m_Util = util;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00007EE0 File Offset: 0x000060E0
		protected override bool StartUI<T>(object model, FakeStore.DialogType dialogType, Action<bool, T> callback)
		{
			List<string> list = new List<string> { "Success" };
			foreach (object obj in Enum.GetValues(typeof(T)))
			{
				T t = (T)((object)obj);
				list.Add(t.ToString());
			}
			Action<bool, int> action = delegate(bool result, int codeValue)
			{
				T t2 = (T)((object)codeValue);
				callback(result, t2);
			};
			string text = null;
			string text2 = null;
			if (dialogType == FakeStore.DialogType.Purchase)
			{
				text = this.CreatePurchaseQuestion((ProductDefinition)model);
				if (this.UIMode == FakeStoreUIMode.DeveloperUser)
				{
					text2 = "OK";
				}
				else
				{
					text2 = "Buy";
				}
			}
			else if (dialogType == FakeStore.DialogType.RetrieveProducts)
			{
				text = this.CreateRetrieveProductsQuestion((ReadOnlyCollection<ProductDefinition>)model);
				text2 = "OK";
			}
			else
			{
				Debug.LogError("Unrecognized DialogType " + dialogType.ToString());
			}
			string text3 = "Cancel";
			return this.StartUI(text, text2, text3, list, action);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008000 File Offset: 0x00006200
		private bool StartUI(string queryText, string okayButtonText, string cancelButtonText, List<string> options, Action<bool, int> callback)
		{
			if (this.IsShowingDialog())
			{
				return false;
			}
			DialogRequest dialogRequest = new DialogRequest
			{
				QueryText = queryText,
				OkayButtonText = okayButtonText,
				CancelButtonText = cancelButtonText,
				Options = options,
				Callback = callback
			};
			this.m_CurrentDialog = dialogRequest;
			this.InstantiateDialog();
			return true;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00008050 File Offset: 0x00006250
		private void InstantiateDialog()
		{
			if (this.m_CurrentDialog != null)
			{
				UIFakeStoreWindow orCreateFakeStoreWindow = this.GetOrCreateFakeStoreWindow();
				this.AddLifeCycleNotifierAndSetDestroyCallback(orCreateFakeStoreWindow.gameObject);
				this.EnsureEventSystemCreated(orCreateFakeStoreWindow.transform);
				this.ConfigureDialogWindow(orCreateFakeStoreWindow);
				return;
			}
			Debug.LogError(((this != null) ? this.ToString() : null) + " requires m_CurrentDialog. Not showing dialog.");
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000080A8 File Offset: 0x000062A8
		private UIFakeStoreWindow GetOrCreateFakeStoreWindow()
		{
			if (this.m_UIFakeStoreWindowObject == null)
			{
				this.m_UIFakeStoreWindowObject = new GameObject("UIFakeStoreWindow", new Type[] { typeof(Transform) });
				this.m_UIFakeStoreWindowObject.AddComponent<UIFakeStoreWindow>();
			}
			return this.m_UIFakeStoreWindowObject.GetComponent<UIFakeStoreWindow>();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000080FD File Offset: 0x000062FD
		private void AddLifeCycleNotifierAndSetDestroyCallback(GameObject gameObject)
		{
			gameObject.AddComponent<LifecycleNotifier>().OnDestroyCallback = delegate
			{
				this.m_CurrentDialog = null;
			};
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00008116 File Offset: 0x00006316
		private void EnsureEventSystemCreated(Transform rootTransform)
		{
			if (Object.FindObjectOfType<EventSystem>() == null)
			{
				this.CreateEventSystem(rootTransform);
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000812C File Offset: 0x0000632C
		private void ConfigureDialogWindow(UIFakeStoreWindow dialogWindow)
		{
			bool flag = this.UIMode != FakeStoreUIMode.DeveloperUser;
			bool flag2 = this.UIMode != FakeStoreUIMode.StandardUser;
			dialogWindow.ConfigureMainDialogText(this.m_CurrentDialog.QueryText, this.m_CurrentDialog.OkayButtonText, this.m_CurrentDialog.CancelButtonText);
			if (flag2)
			{
				dialogWindow.ConfigureDropdownOptions(this.m_CurrentDialog.Options);
			}
			this.ConfigureDialogWindowCallbacks(dialogWindow, flag, flag2);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008198 File Offset: 0x00006398
		private void ConfigureDialogWindowCallbacks(UIFakeStoreWindow dialogWindow, bool assignCancelCallback, bool assignDropDownCallback)
		{
			Action action = null;
			Action<int> action2 = null;
			if (assignCancelCallback)
			{
				action = new Action(this.CancelButtonClicked);
			}
			if (assignDropDownCallback)
			{
				action2 = new Action<int>(this.DropdownValueChanged);
			}
			dialogWindow.AssignCallbacks(new Action(this.OkayButtonClicked), action, action2);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000081E0 File Offset: 0x000063E0
		private void CreateEventSystem(Transform rootTransform)
		{
			this.m_EventSystem = new GameObject("EventSystem", new Type[] { typeof(EventSystem) });
			this.m_EventSystem.AddComponent<StandaloneInputModule>();
			this.m_EventSystem.transform.parent = rootTransform;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000822D File Offset: 0x0000642D
		private string CreatePurchaseQuestion(ProductDefinition definition)
		{
			return "Do you want to Purchase " + definition.id + "?\n\n[Environment: FakeStore]";
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008244 File Offset: 0x00006444
		private string CreateRetrieveProductsQuestion(ReadOnlyCollection<ProductDefinition> definitions)
		{
			string text = "Do you want to initialize purchasing for products {";
			text += string.Join(", ", (from pid in definitions.Take<ProductDefinition>(2)
				select pid.id).ToArray<string>());
			if (definitions.Count > 2)
			{
				text += ", ...";
			}
			return text + "}?\n\n[Environment: FakeStore]";
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000082BC File Offset: 0x000064BC
		private void OkayButtonClicked()
		{
			bool flag = false;
			if (this.m_LastSelectedDropdownIndex == 0 || this.UIMode != FakeStoreUIMode.DeveloperUser)
			{
				flag = true;
			}
			int num = Math.Max(0, this.m_LastSelectedDropdownIndex - 1);
			this.m_CurrentDialog.Callback(flag, num);
			this.CloseDialog();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008308 File Offset: 0x00006508
		private void CancelButtonClicked()
		{
			int num = Math.Max(0, this.m_LastSelectedDropdownIndex - 1);
			this.m_CurrentDialog.Callback(false, num);
			this.CloseDialog();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000833C File Offset: 0x0000653C
		private void DropdownValueChanged(int selectedItem)
		{
			this.m_LastSelectedDropdownIndex = selectedItem;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008345 File Offset: 0x00006545
		private void CloseDialog()
		{
			this.m_CurrentDialog = null;
			if (this.m_UIFakeStoreWindowObject != null)
			{
				Object.Destroy(this.m_UIFakeStoreWindowObject);
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008367 File Offset: 0x00006567
		public bool IsShowingDialog()
		{
			return this.m_CurrentDialog != null;
		}

		// Token: 0x04000118 RID: 280
		private const string EnvironmentDescriptionPostfix = "\n\n[Environment: FakeStore]";

		// Token: 0x04000119 RID: 281
		private const string SuccessString = "Success";

		// Token: 0x0400011A RID: 282
		private const int RetrieveProductsDescriptionCount = 2;

		// Token: 0x0400011B RID: 283
		private DialogRequest m_CurrentDialog;

		// Token: 0x0400011C RID: 284
		private int m_LastSelectedDropdownIndex;

		// Token: 0x0400011D RID: 285
		private GameObject m_UIFakeStoreWindowObject;

		// Token: 0x0400011E RID: 286
		private GameObject m_EventSystem;

		// Token: 0x0400011F RID: 287
		private readonly IUtil m_Util;
	}
}
