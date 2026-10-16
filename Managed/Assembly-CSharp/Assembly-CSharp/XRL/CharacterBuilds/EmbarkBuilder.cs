using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using LaundryBear.PlatformServices;
using Platform.IO;
using Qud.UI;
using UnityEngine;
using XRL.CharacterBuilds.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds
{
	/// <summary>
	/// The main Unity MonoBehaviour that controls the new Embark experience.  
	/// There can only ever be one of these because it's attached to the GameManager.
	/// </summary>
	// Token: 0x02000589 RID: 1417
	public class EmbarkBuilder : MonoBehaviour
	{
		/// <summary>
		///     Static helper to get at the game object.
		/// </summary>
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x0012AD8C File Offset: 0x00128F8C
		public new static GameObject gameObject
		{
			get
			{
				return GameManager.Instance.gameObject;
			}
		}

		/// <summary>
		///     Start the embark builder.
		/// </summary>
		/// <returns>A task that will resolve to the EmbarkInfo or null.</returns>
		/// <exception cref="T:System.InvalidOperationException">When called from a thread other than the game thread.</exception>
		// Token: 0x06003CDF RID: 15583 RVA: 0x0012AD98 File Offset: 0x00128F98
		public static async Task<EmbarkInfo> Begin()
		{
			EmbarkBuilder.finishedEvent = new TaskCompletionSource<EmbarkInfo>();
			if (Thread.CurrentThread == GameManager.Instance.uiQueue.threadContext)
			{
				throw new InvalidOperationException("EmbarkBuilder::Begin can only be called from the game thread.");
			}
			GameManager.Instance.CurrentGameView = "EmbarkBuilder";
			await The.UiContext;
			GameManager.Instance.SetActiveLayersForNavCategory("Chargen", true);
			EmbarkBuilder component = EmbarkBuilder.gameObject.GetComponent<EmbarkBuilder>();
			if (component != null)
			{
				component.Destroy();
			}
			GameManager.Instance.gameObject.AddComponent<EmbarkBuilder>().InitModules(false);
			object obj = null;
			int num = 0;
			EmbarkInfo embarkInfo;
			try
			{
				embarkInfo = await EmbarkBuilder.finishedEvent.Task.ConfigureAwait(false);
				num = 1;
			}
			catch (object obj)
			{
			}
			await The.UiContext;
			NavigationController.instance.activeContext = null;
			ControlManager.EnableLayer("Adventure");
			ControlManager.EnableLayer("AltAdventure");
			ControlManager.EnableLayer("Chargen");
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			EmbarkInfo embarkInfo2;
			if (num == 1)
			{
				embarkInfo2 = embarkInfo;
			}
			else
			{
				obj = null;
				embarkInfo = null;
			}
			return embarkInfo2;
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x0012ADD3 File Offset: 0x00128FD3
		public string generateCode()
		{
			return CodeCompressor.generateCode(this.enabledModules);
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x0012ADE0 File Offset: 0x00128FE0
		public object handleUIEvent(string id, object element = null)
		{
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in this.enabledModules)
			{
				element = abstractEmbarkBuilderModule.handleUIEvent(id, element);
			}
			return element;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x0012AE30 File Offset: 0x00129030
		public void ShowWindow<T>() where T : AbstractBuilderModuleWindowBase
		{
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.windowDescriptors.Find((EmbarkBuilderModuleWindowDescriptor desc) => desc.getWindow() is T);
			if (embarkBuilderModuleWindowDescriptor == null)
			{
				throw new Exception("Window of type T not found.");
			}
			this.ShowWindow(embarkBuilderModuleWindowDescriptor);
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x0012AE80 File Offset: 0x00129080
		public void ShowWindow(EmbarkBuilderModuleWindowDescriptor windowDescriptor)
		{
			if (windowDescriptor != null)
			{
				windowDescriptor.show();
			}
			bool? flag;
			if (windowDescriptor == null)
			{
				flag = null;
			}
			else
			{
				AbstractBuilderModuleWindowBase window = windowDescriptor.getWindow();
				flag = ((window != null) ? new bool?(window.UseOverlay()) : null);
			}
			bool? flag2 = flag;
			if (flag2.GetValueOrDefault())
			{
				this.GetOverlayWindow().BeforeShowWithWindow(this, windowDescriptor);
				this.GetOverlayWindow().Show();
			}
			else
			{
				this.GetOverlayWindow().Hide();
			}
			NavigationContext navigationContext;
			if (windowDescriptor == null)
			{
				navigationContext = null;
			}
			else
			{
				AbstractBuilderModuleWindowBase window2 = windowDescriptor.getWindow();
				navigationContext = ((window2 != null) ? window2.GetNavigationContext() : null);
			}
			NavigationContext navigationContext2 = navigationContext;
			if (navigationContext2 == null)
			{
				NavigationController.instance.activeContext = null;
				return;
			}
			navigationContext2.ActivateAndEnable();
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x0012AF21 File Offset: 0x00129121
		public void RefreshActiveWindow()
		{
			this.ShowWindow(this.activeWindow);
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x0012AF30 File Offset: 0x00129130
		public EmbarkBuilderModuleWindowDescriptor GetNextEnabledWindowDescriptor(bool editableOnly)
		{
			int i = this.windowDescriptors.IndexOf(this.activeWindow);
			for (i++; i < this.windowDescriptors.Count; i++)
			{
				if (this.windowDescriptors[i].enabled && TutorialManager.IsEditableChargenPanel(this.windowDescriptors[i].name) && (!editableOnly || this.windowDescriptors[i].module.shouldBeEditable()))
				{
					return this.windowDescriptors[i];
				}
			}
			return null;
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x0012AFBC File Offset: 0x001291BC
		public EmbarkBuilderModuleWindowDescriptor GetWindowDescriptorByViewID(string id)
		{
			int i = this.windowDescriptors.IndexOf(this.activeWindow);
			for (i++; i < this.windowDescriptors.Count; i++)
			{
				if (this.windowDescriptors[i].viewID == id)
				{
					return this.windowDescriptors[i];
				}
			}
			return null;
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x0012B01C File Offset: 0x0012921C
		public void advanceToSummary()
		{
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.activeWindow;
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor2;
			do
			{
				this.advance(false, false, false);
				if (this.activeWindow == embarkBuilderModuleWindowDescriptor)
				{
					break;
				}
				embarkBuilderModuleWindowDescriptor = this.activeWindow;
				embarkBuilderModuleWindowDescriptor2 = this.activeWindow;
			}
			while (((embarkBuilderModuleWindowDescriptor2 != null) ? embarkBuilderModuleWindowDescriptor2.viewID : null) != "Chargen/BuildSummary");
		}

		/// <summary>
		///     Checks the state of the current window for data errors and warnings, potentially showing popup messages.
		///     In the event of errors, it will show a dialog box, wait for dismissal and return false.
		///     In the event of warnings, it will ask the player to confirm advancing the screen, returning true or false.
		///     If no errors or warnings, returns true.
		/// </summary>
		/// <returns>If the current window is "ready" to advance</returns>
		// Token: 0x06003CE8 RID: 15592 RVA: 0x0012B068 File Offset: 0x00129268
		public async Task<bool> checkStateAsync()
		{
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.activeWindow;
			string text;
			if (embarkBuilderModuleWindowDescriptor == null)
			{
				text = null;
			}
			else
			{
				AbstractBuilderModuleWindowBase window = embarkBuilderModuleWindowDescriptor.getWindow();
				text = ((window != null) ? window.DataErrors() : null);
			}
			string text2 = text;
			bool flag;
			if (text2 != null)
			{
				await Popup.NewPopupMessageAsync(text2, PopupMessage.SingleButton, null, "{{r|Error!}}", null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null);
				flag = false;
			}
			else
			{
				EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor2 = this.activeWindow;
				string text3;
				if (embarkBuilderModuleWindowDescriptor2 == null)
				{
					text3 = null;
				}
				else
				{
					AbstractBuilderModuleWindowBase window2 = embarkBuilderModuleWindowDescriptor2.getWindow();
					text3 = ((window2 != null) ? window2.DataWarnings() : null);
				}
				string text4 = text3;
				if (text4 != null)
				{
					TaskAwaiter<QudMenuItem> taskAwaiter = Popup.NewPopupMessageAsync(text4 + "\n\nContinue anyway?", PopupMessage.YesNoButton, null, "{{W|Warning!}}", null, 0, null, null, null, true, true, false, default(CancellationToken), false, "", null, null, null).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<QudMenuItem> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<QudMenuItem>);
					}
					if (taskAwaiter.GetResult().command != "Yes")
					{
						return false;
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x0012B0AC File Offset: 0x001292AC
		public async void advance(bool force = false, bool editableOnly = false, bool deferLayout = false)
		{
			if (!force)
			{
				TaskAwaiter<bool> taskAwaiter = this.checkStateAsync().GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (!taskAwaiter.GetResult())
				{
					return;
				}
			}
			EmbarkBuilderModuleWindowDescriptor nextEnabledWindowDescriptor = this.GetNextEnabledWindowDescriptor(editableOnly);
			if (nextEnabledWindowDescriptor == null)
			{
				this.exitWithInfo();
			}
			else if (!deferLayout)
			{
				this.ShowWindow(nextEnabledWindowDescriptor);
				if (nextEnabledWindowDescriptor != null)
				{
					AbstractBuilderModuleWindowBase window = nextEnabledWindowDescriptor.window;
					if (window != null)
					{
						window.AfterShow(nextEnabledWindowDescriptor);
					}
				}
			}
			else
			{
				this.activeWindow = nextEnabledWindowDescriptor;
			}
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x0012B0FC File Offset: 0x001292FC
		public void advanceToViewId(string viewid)
		{
			EmbarkBuilderModuleWindowDescriptor windowDescriptorByViewID = this.GetWindowDescriptorByViewID(viewid);
			if (windowDescriptorByViewID == null)
			{
				this.exitWithInfo();
				return;
			}
			this.ShowWindow(windowDescriptorByViewID);
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x0012B124 File Offset: 0x00129324
		public void back()
		{
			int i = this.windowDescriptors.IndexOf(this.activeWindow);
			for (i--; i >= 0; i--)
			{
				if (this.windowDescriptors[i].enabled && this.windowDescriptors[i].module.shouldBeEditable() && TutorialManager.IsEditableChargenPanel(this.windowDescriptors[i].name))
				{
					this.ShowWindow(this.windowDescriptors[i]);
					return;
				}
			}
			this.exitWithoutInfo();
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x0012B1B0 File Offset: 0x001293B0
		public async Task SaveCharacterCode()
		{
			string path = DataManager.SavePath("lastcharacter.txt");
			TaskAwaiter<StorageResult> taskAwaiter = Blob.WriteAllTextAsync(DataManager.SavePath("lastcharacter.txt"), this.generateCode()).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<StorageResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<StorageResult>);
			}
			if (!taskAwaiter.GetResult().WasSuccessful())
			{
				MetricsManager.LogWarning("Could not save \"" + path + "\"");
			}
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x0012B1F4 File Offset: 0x001293F4
		public void exitWithInfo()
		{
			Debug.Log("exiting with info");
			try
			{
				this.SaveCharacterCode().Wait();
				if (Options.ModernUI)
				{
					WorldGenerationScreen.ShowWorldGenerationScreen(258);
				}
				this.ShowWindow(null);
				foreach (AbstractBuilderModuleWindowBase abstractBuilderModuleWindowBase in this.createdWindows)
				{
					abstractBuilderModuleWindowBase.DestroyWindow();
				}
				this.createdWindows.Clear();
				this.embarkInfo.modules.AddRange(this.modules.Where<AbstractEmbarkBuilderModule>((AbstractEmbarkBuilderModule m) => m.enabled));
				this.embarkInfo.modules.ForEach(delegate(AbstractEmbarkBuilderModule m)
				{
					AbstractEmbarkBuilderModuleData data = m.getData();
					if (data != null)
					{
						this.embarkInfo._data.Add(data);
					}
				});
				NavigationController.instance.activeContext = null;
			}
			finally
			{
				EmbarkBuilder.finishedEvent.TrySetResult(this.embarkInfo);
				this.Destroy();
			}
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x0012B304 File Offset: 0x00129504
		public void exitWithoutInfo()
		{
			try
			{
				this.ShowWindow(null);
				foreach (AbstractBuilderModuleWindowBase abstractBuilderModuleWindowBase in this.createdWindows)
				{
					abstractBuilderModuleWindowBase.DestroyWindow();
				}
				this.createdWindows.Clear();
				Debug.Log("exiting without info");
				this.embarkInfo = null;
				NavigationController.instance.activeContext = null;
			}
			finally
			{
				EmbarkBuilder.finishedEvent.TrySetResult(null);
				this.Destroy();
			}
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x0012B3A4 File Offset: 0x001295A4
		public void Update()
		{
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x0012B3A6 File Offset: 0x001295A6
		public EmbarkInfo info
		{
			get
			{
				return this.embarkInfo;
			}
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x0012B3AE File Offset: 0x001295AE
		public T GetModule<T>() where T : AbstractEmbarkBuilderModule
		{
			return this.modules.Find((AbstractEmbarkBuilderModule m) => m.GetType() == typeof(T)) as T;
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x0012B3E4 File Offset: 0x001295E4
		public IEnumerable<AbstractEmbarkBuilderModule> enabledModules
		{
			get
			{
				return this.modules.Where<AbstractEmbarkBuilderModule>((AbstractEmbarkBuilderModule m) => m.enabled);
			}
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x0012B410 File Offset: 0x00129610
		public EmbarkBuilderOverlayWindow GetOverlayWindow()
		{
			return UIManager.getWindow<EmbarkBuilderOverlayWindow>("Chargen/Overlay");
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x0012B41C File Offset: 0x0012961C
		public AbstractBuilderModuleWindowBase createWindow(string viewID, Type windowType)
		{
			Transform transform = GameObject.Find("UI Manager/Chargen").transform;
			AbstractBuilderModuleWindowBase abstractBuilderModuleWindowBase = UIManager.createWindow(viewID, windowType, transform) as AbstractBuilderModuleWindowBase;
			this.createdWindows.Add(abstractBuilderModuleWindowBase);
			return abstractBuilderModuleWindowBase;
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x0012B454 File Offset: 0x00129654
		public IEnumerable<UIBreadcrumb> GetBreadcrumbs()
		{
			foreach (EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor in this.windowDescriptors)
			{
				if (embarkBuilderModuleWindowDescriptor.enabled && embarkBuilderModuleWindowDescriptor.module.shouldBeEditable())
				{
					UIBreadcrumb breadcrumb = embarkBuilderModuleWindowDescriptor.window.GetBreadcrumb();
					if (breadcrumb != null)
					{
						yield return breadcrumb;
					}
				}
			}
			List<EmbarkBuilderModuleWindowDescriptor>.Enumerator enumerator = default(List<EmbarkBuilderModuleWindowDescriptor>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x0012B464 File Offset: 0x00129664
		public async void ShowFromCrumb(FrameworkDataElement choice)
		{
			bool isBeforeActive = true;
			foreach (EmbarkBuilderModuleWindowDescriptor desc in this.windowDescriptors)
			{
				if (desc == this.activeWindow)
				{
					isBeforeActive = false;
				}
				UIBreadcrumb breadcrumb = desc.window.GetBreadcrumb();
				if (((breadcrumb != null) ? breadcrumb.Id : null) == choice.Id)
				{
					if (desc == this.activeWindow)
					{
						return;
					}
					if (isBeforeActive)
					{
						this.ShowWindow(desc);
					}
					else
					{
						TaskAwaiter<bool> taskAwaiter = this.checkStateAsync().GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<bool> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<bool>);
						}
						if (taskAwaiter.GetResult())
						{
							this.advanceToViewId(desc.viewID);
						}
					}
				}
				desc = null;
			}
			List<EmbarkBuilderModuleWindowDescriptor>.Enumerator enumerator = default(List<EmbarkBuilderModuleWindowDescriptor>.Enumerator);
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x0012B4A4 File Offset: 0x001296A4
		public void fireBootEvent(string id)
		{
			this.modules.ForEach(delegate(AbstractEmbarkBuilderModule module)
			{
				if (module.enabled)
				{
					module.handleBootEvent(id, null, this.info, null);
				}
			});
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x0012B4DC File Offset: 0x001296DC
		public T fireBootEvent<T>(string id, T element)
		{
			List<AbstractEmbarkBuilderModule> list = this.modules;
			if (list != null)
			{
				list.ForEach(delegate(AbstractEmbarkBuilderModule module)
				{
					if (module != null && module.enabled)
					{
						element = (T)((object)((module != null) ? module.handleBootEvent(id, null, this.info, element) : null));
					}
				});
			}
			return element;
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x0012B528 File Offset: 0x00129728
		public bool IsEditableGameMode()
		{
			bool flag;
			try
			{
				flag = (bool)this.handleUIEvent(EmbarkBuilder.EventNames.EditableGameModeQuery, true);
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Error while processing EditableGameMode", ex);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x0012B570 File Offset: 0x00129770
		public void RegisterModuleEnabledCheck(Predicate<AbstractEmbarkBuilderModule> filter)
		{
			this.enabledChecks.Add(filter);
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x0012B580 File Offset: 0x00129780
		public bool ModuleEnabledCheck(AbstractEmbarkBuilderModule module)
		{
			using (List<Predicate<AbstractEmbarkBuilderModule>>.Enumerator enumerator = this.enabledChecks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current(module))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x0012B5DC File Offset: 0x001297DC
		private void InitModules(bool silent = false)
		{
			List<Predicate<AbstractEmbarkBuilderModule>> list = new List<Predicate<AbstractEmbarkBuilderModule>>();
			list.Add((AbstractEmbarkBuilderModule module) => module.shouldBeEnabled());
			this.enabledChecks = list;
			EmbarkBuilderConfiguration.Init();
			this.modules = new List<AbstractEmbarkBuilderModule>();
			this.windowDescriptors = new List<EmbarkBuilderModuleWindowDescriptor>();
			this.windows = new List<AbstractBuilderModuleWindowBase>();
			this.modules.AddRange(EmbarkBuilderConfiguration.activeModules);
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in this.modules)
			{
				abstractEmbarkBuilderModule.builder = this;
				if (this.ModuleEnabledCheck(abstractEmbarkBuilderModule))
				{
					abstractEmbarkBuilderModule.enable();
				}
				else
				{
					abstractEmbarkBuilderModule.disable();
				}
			}
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule2 in this.modules)
			{
				abstractEmbarkBuilderModule2.Init();
			}
			if (silent)
			{
				return;
			}
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule3 in this.modules)
			{
				abstractEmbarkBuilderModule3.assembleWindowDescriptors(this.windowDescriptors);
			}
			this.windowDescriptors.ForEach(delegate(EmbarkBuilderModuleWindowDescriptor descriptor)
			{
				AbstractBuilderModuleWindowBase window = descriptor.getWindow();
				descriptor.windowInit(window);
			});
			this.ShowWindow(this.windowDescriptors.First<EmbarkBuilderModuleWindowDescriptor>());
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x0012B770 File Offset: 0x00129970
		public void NotifyModuleChanges(AbstractEmbarkBuilderModule module, AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
			foreach (AbstractEmbarkBuilderModule abstractEmbarkBuilderModule in this.modules)
			{
				if (abstractEmbarkBuilderModule != module)
				{
					abstractEmbarkBuilderModule.handleModuleDataChange(module, oldValues, newValues);
				}
				bool flag = this.ModuleEnabledCheck(abstractEmbarkBuilderModule);
				if (flag != abstractEmbarkBuilderModule.enabled)
				{
					if (flag)
					{
						abstractEmbarkBuilderModule.enable();
					}
					else
					{
						abstractEmbarkBuilderModule.disable();
					}
				}
			}
			EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.activeWindow;
			bool? flag2;
			if (embarkBuilderModuleWindowDescriptor == null)
			{
				flag2 = null;
			}
			else
			{
				AbstractBuilderModuleWindowBase window = embarkBuilderModuleWindowDescriptor.getWindow();
				flag2 = ((window != null) ? new bool?(window.UseOverlay()) : null);
			}
			bool? flag3 = flag2;
			if (flag3.GetValueOrDefault() && !this.SkippingUIUpdates)
			{
				this.GetOverlayWindow().UpdateBreadcrumbs();
			}
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x0012B83C File Offset: 0x00129A3C
		public static EmbarkBuilder FromCode(string code, bool silent = false)
		{
			EmbarkBuilder embarkBuilder = new EmbarkBuilder();
			embarkBuilder.InitModulesFromCode(code, silent);
			return embarkBuilder;
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x0012B84B File Offset: 0x00129A4B
		public void InitModulesFromCode(string code, bool silent = false)
		{
			if (this.modules == null)
			{
				this.InitModules(silent);
			}
			CodeCompressor.loadCode(code, this.modules, silent);
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x0012B86C File Offset: 0x00129A6C
		public void ResetForwardViews()
		{
			foreach (EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor in this.windowDescriptors.SkipWhile<EmbarkBuilderModuleWindowDescriptor>((EmbarkBuilderModuleWindowDescriptor desc) => desc != this.activeWindow).Skip<EmbarkBuilderModuleWindowDescriptor>(1))
			{
				embarkBuilderModuleWindowDescriptor.getWindow().ResetSelection();
			}
		}

		/// <summary>
		///     State of the embark builder selection process.
		/// </summary>
		// Token: 0x04001E57 RID: 7767
		public static TaskCompletionSource<EmbarkInfo> finishedEvent = new TaskCompletionSource<EmbarkInfo>();

		// Token: 0x04001E58 RID: 7768
		protected EmbarkInfo embarkInfo = new EmbarkInfo();

		// Token: 0x04001E59 RID: 7769
		protected List<AbstractEmbarkBuilderModule> modules;

		// Token: 0x04001E5A RID: 7770
		protected List<EmbarkBuilderModuleWindowDescriptor> windowDescriptors;

		// Token: 0x04001E5B RID: 7771
		public EmbarkBuilderModuleWindowDescriptor activeWindow;

		// Token: 0x04001E5C RID: 7772
		protected List<AbstractBuilderModuleWindowBase> windows;

		// Token: 0x04001E5D RID: 7773
		public List<AbstractBuilderModuleWindowBase> createdWindows = new List<AbstractBuilderModuleWindowBase>();

		// Token: 0x04001E5E RID: 7774
		private List<Predicate<AbstractEmbarkBuilderModule>> enabledChecks;

		// Token: 0x04001E5F RID: 7775
		public bool SkippingUIUpdates;

		// Token: 0x02001982 RID: 6530
		public static class EventNames
		{
			// Token: 0x0400660B RID: 26123
			public static readonly string EditableGameModeQuery = "EditableGameModeQuery";
		}
	}
}
