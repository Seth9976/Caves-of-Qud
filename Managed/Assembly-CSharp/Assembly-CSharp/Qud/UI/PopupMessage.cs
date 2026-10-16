using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ConsoleLib.Console;
using Genkit;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x020004A3 RID: 1187
	[UIView("DynamicPopupMessage", false, false, false, "Conversation", null, false, 0, false, IgnoreForceFullscreen = true, OverlayMode = 1)]
	[UIView("PopupMessage", false, false, false, "Conversation", "PopupMessage", false, 0, false, IgnoreForceFullscreen = true, UICanvasHost = 1, OverlayMode = 1)]
	public class PopupMessage : WindowBase
	{
		// Token: 0x06002D75 RID: 11637 RVA: 0x000F6E47 File Offset: 0x000F5047
		public override void Init()
		{
			this.controller.isCurrentWindow = new Func<bool>(base.isCurrentWindow);
			base.Init();
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x000F6E68 File Offset: 0x000F5068
		public static List<QudMenuItem> CancelButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._CancelButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} Cancel",
							command = "Cancel",
							hotkey = "Cancel"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} Cancel",
						command = "Cancel",
						hotkey = "Cancel"
					}
				};
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000F6F44 File Offset: 0x000F5144
		public static List<QudMenuItem> CopyButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._CopyButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{y|Copy}}",
							command = "Copy",
							hotkey = "char:c"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Cancel}}",
							command = "Cancel",
							hotkey = "Cancel"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{y|Copy}}",
						command = "Copy",
						hotkey = "char:c"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					}
				};
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x000F708C File Offset: 0x000F528C
		public static List<QudMenuItem> SingleButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._SingleButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{W|press " + ControlManager.getCommandInputDescription("Accept", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}}",
							command = "Accept",
							hotkey = "Accept,Cancel"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|press [" + ControlManager.getCommandInputDescription("Accept", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}}",
						command = "Accept",
						hotkey = "Accept,Cancel"
					}
				};
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x000F7168 File Offset: 0x000F5368
		public static List<QudMenuItem> YesNoButton
		{
			get
			{
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return PopupMessage._YesNoButtonJoystick;
				}
				return PopupMessage._YesNoButton;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000F7180 File Offset: 0x000F5380
		public static List<QudMenuItem> YesNoCancelButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._YesNoCancelButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputFormatted("Accept", true, false) + "}} {{y|Yes}}",
							command = "Yes",
							hotkey = "Y,Accept"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputFormatted("V Negative", true, false) + "}} {{y|No}}",
							command = "No",
							hotkey = "N,V Negative"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + " Cancel}}",
							command = "Cancel",
							hotkey = "Cancel"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputFormatted("Accept", true, false) + "]}} {{y|Yes}}",
						command = "Yes",
						hotkey = "Y,Accept"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputFormatted("V Negative", true, false) + "]}} {{y|No}}",
						command = "No",
						hotkey = "N,V Negative"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} Cancel",
						command = "Cancel",
						hotkey = "Cancel"
					}
				};
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x000F7384 File Offset: 0x000F5584
		public static List<QudMenuItem> AcceptCancelButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._AcceptCancelButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Accept", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Accept}}",
							command = "keep",
							hotkey = "Accept"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Cancel}}",
							command = "Cancel",
							hotkey = "Cancel"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Accept", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Accept}}",
						command = "keep",
						hotkey = "Accept"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					}
				};
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x000F7504 File Offset: 0x000F5704
		public static List<QudMenuItem> AcceptCancelButtonWithoutHotkey
		{
			get
			{
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad || CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{y|Accept}}",
							command = "keep"
						},
						new QudMenuItem
						{
							text = "{{y|Cancel}}",
							command = "Cancel"
						}
					};
				}
				return PopupMessage._AcceptCancelButton;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x000F7580 File Offset: 0x000F5780
		public static List<QudMenuItem> AcceptCancelColorButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._AcceptCancelColorButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Submit", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Submit}}",
							command = "keep",
							hotkey = "Submit"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Cancel}}",
							command = "Cancel",
							hotkey = "Cancel"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Help", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Color}}",
							command = "Color",
							hotkey = "CmdHelp"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Submit", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Submit}}",
						command = "keep",
						hotkey = "Submit"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Help", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Color}}",
						command = "Color",
						hotkey = "CmdHelp"
					}
				};
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x000F779C File Offset: 0x000F599C
		public static List<QudMenuItem> SubmitCancelButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._SubmitCancelButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Submit", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Submit}}",
							command = "keep",
							hotkey = "Submit"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Cancel}}",
							command = "Cancel",
							hotkey = "Cancel"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Submit", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Submit}}",
						command = "keep",
						hotkey = "Submit"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					}
				};
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x000F791C File Offset: 0x000F5B1C
		public static List<QudMenuItem> SubmitCancelHoldButton
		{
			get
			{
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|" + ControlManager.getCommandInputDescription("Submit", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Submit}}",
						command = "keep",
						hotkey = "Submit"
					},
					new QudMenuItem
					{
						text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					},
					new QudMenuItem
					{
						text = "{{W|" + ControlManager.getCommandInputDescription("CmdOptions", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Hold to Accept}}",
						command = "CmdOptions",
						hotkey = "CmdOptions"
					}
				};
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x000F7A1C File Offset: 0x000F5C1C
		public static List<QudMenuItem> CancelHoldButton
		{
			get
			{
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					},
					new QudMenuItem
					{
						text = "{{W|" + ControlManager.getCommandInputDescription("CmdOptions", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Hold to Accept}}",
						command = "CmdOptions",
						hotkey = "CmdOptions"
					}
				};
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x000F7ACC File Offset: 0x000F5CCC
		public static List<QudMenuItem> SubmitCancelColorButton
		{
			get
			{
				if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad && !CapabilityManager.HasCapability(new CapabilityManager.CapabilityType?(CapabilityManager.CapabilityType.IsDesktop)))
				{
					return PopupMessage._AcceptCancelColorButton;
				}
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					return new List<QudMenuItem>
					{
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Submit", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Submit}}",
							command = "keep",
							hotkey = "Submit"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Cancel}}",
							command = "Cancel",
							hotkey = "Cancel"
						},
						new QudMenuItem
						{
							text = "{{W|" + ControlManager.getCommandInputDescription("CmdSystem1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "}} {{y|Color}}",
							command = "Color",
							hotkey = "CmdHelp"
						}
					};
				}
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Submit", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Submit}}",
						command = "keep",
						hotkey = "Submit"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					},
					new QudMenuItem
					{
						text = "{{W|[" + ControlManager.getCommandInputDescription("CmdSystem1", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Color}}",
						command = "Color",
						hotkey = "CmdHelp"
					}
				};
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x000F7CE8 File Offset: 0x000F5EE8
		public static List<QudMenuItem> AcceptCancelTradeButton
		{
			get
			{
				return new List<QudMenuItem>
				{
					new QudMenuItem
					{
						text = "{{[W|" + ControlManager.getCommandInputDescription("CmdStartTrade", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Trade}}",
						command = "trade",
						hotkey = "CmdStartTrade"
					},
					new QudMenuItem
					{
						text = "{{[W|" + ControlManager.getCommandInputDescription("Accept", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Accept}}",
						command = "keep",
						hotkey = "Accept"
					},
					new QudMenuItem
					{
						text = "{{[W|" + ControlManager.getCommandInputDescription("Cancel", Options.ModernUI, false, ControlManager.InputDeviceType.Unknown, false) + "]}} {{y|Cancel}}",
						command = "Cancel",
						hotkey = "Cancel"
					}
				};
			}
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000F7DE8 File Offset: 0x000F5FE8
		public void ShowPopup(string message, List<QudMenuItem> buttons, Action<QudMenuItem> commandCallback = null, List<QudMenuItem> items = null, Action<QudMenuItem> selectedItemCallback = null, string title = null, bool includeInput = false, string inputDefault = null, int DefaultSelected = 0, Action onHide = null, IRenderable contextRender = null, string contextTitle = null, IRenderable afterRender = null, bool showContextFrame = true, bool pushView = true, CancellationToken cancelToken = default(CancellationToken), bool askingNumber = false, string RestrictChars = "", string WantsSpecificPrompt = null, Location2D PopupLocation = null, string PopupID = null)
		{
			this.HideNextFrame = 0;
			if (this.onHide != null)
			{
				try
				{
					MetricsManager.LogError("ShowPopup::OnHide wasn't called! Calling it now");
					this.onHide();
					this.onHide = null;
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("PopupMessage::ShowPopup onHide sanity check", ex, "game_exception");
				}
			}
			if (PopupLocation == null)
			{
				this.centringLayoutGroup.enabled = true;
				this.popupPositionTransform.pivot = new Vector2(0.5f, 0.5f);
			}
			else
			{
				this.centringLayoutGroup.enabled = false;
				Vector3 vector;
				if (PopupLocation == PopupMessage.LOCATION_AT_MOUSE_CURSOR)
				{
					vector = Input.mousePosition;
				}
				else
				{
					vector = GameManager.Instance.getScreenTileCenter(PopupLocation.X, PopupLocation.Y, 0);
				}
				this.popupPositionTransform.pivot = new Vector2(0f, 0f);
				if (vector.x < (float)(Screen.width / 2))
				{
					if (vector.y < (float)(Screen.height / 2))
					{
						this.popupPositionTransform.pivot = new Vector2(0f, 0f);
					}
					else
					{
						this.popupPositionTransform.pivot = new Vector2(0f, 1f);
					}
				}
				else if (vector.y < (float)(Screen.height / 2))
				{
					this.popupPositionTransform.pivot = new Vector2(1f, 0f);
				}
				else
				{
					this.popupPositionTransform.pivot = new Vector2(1f, 1f);
				}
				this.popupPositionTransform.position = new Vector3(vector.x, vector.y, 0f);
				Transform parent = this.popupPositionTransform.parent;
				this.ClampPopupPosition();
			}
			this.EnforceSuspensionContext();
			this.PopupID = PopupID;
			this.cancellationToken = cancelToken;
			this.onHide = onHide;
			base.canvasGroup.alpha = 0f;
			base.transform.SetAsLastSibling();
			base.gameObject.SetActive(true);
			this.Message.SetText("{{y|" + message + "}}");
			this.Message.GetComponent<LayoutElement>().minWidth = 0f;
			this.commandCallback = commandCallback;
			this.selectCallback = selectedItemCallback;
			this.controller.menuData = items ?? new List<QudMenuItem>();
			this.controller.bottomContextOptions = buttons;
			this.WantsSpecificPrompt = WantsSpecificPrompt;
			this.RestrictChars = RestrictChars;
			if (this.RestrictChars == "0123456789")
			{
				this.inputBox.contentType = TMP_InputField.ContentType.IntegerNumber;
			}
			else if (this.RestrictChars == "0123456789-")
			{
				this.inputBox.contentType = TMP_InputField.ContentType.IntegerNumber;
			}
			else
			{
				this.inputBox.contentType = TMP_InputField.ContentType.Standard;
			}
			if (string.IsNullOrEmpty(title))
			{
				this.TitleContainer.SetActive(false);
				this.Title.SetText("");
			}
			else
			{
				this.TitleContainer.SetActive(true);
				this.Title.SetText("{{W|" + title + "}}");
			}
			if (includeInput)
			{
				this.inputBox.gameObject.SetActive(true);
				if (!this.controller.inputFields.Contains(this.inputBox))
				{
					this.controller.inputFields.Add(this.inputBox);
				}
				this.inputBox.text = inputDefault ?? "";
				this.inputBox.onSubmit.RemoveAllListeners();
				this.inputBox.onSubmit.AddListener(new UnityAction<string>(this.OnInputSubmit));
				CapabilityManager.SuggestOnscreenKeyboard();
			}
			else
			{
				this.inputBox.gameObject.SetActive(false);
				if (this.controller.inputFields.Contains(this.inputBox))
				{
					this.controller.inputFields.Remove(this.inputBox);
				}
			}
			if (contextTitle != null || contextRender != null || afterRender != null)
			{
				this.contextContainer.SetActive(true);
				if (contextRender != null)
				{
					this.contextImage.gameObject.SetActive(true);
					this.contextImage.FromRenderable(contextRender);
				}
				else
				{
					this.contextImage.gameObject.SetActive(false);
				}
				if (afterRender != null)
				{
					this.afterImage.gameObject.SetActive(true);
					this.afterImage.FromRenderable(afterRender, true);
					if (this.afterImage.Background == The.Color.DarkBlack)
					{
						this.afterImage.Background = Color.clear;
					}
				}
				else
				{
					this.afterImage.gameObject.SetActive(false);
				}
				if (!string.IsNullOrEmpty(contextTitle))
				{
					this.contextText.gameObject.SetActive(true);
					this.contextText.SetText(contextTitle);
				}
				else
				{
					this.contextText.gameObject.SetActive(false);
				}
			}
			else
			{
				this.contextContainer.SetActive(false);
			}
			this.contextFrame.SetActive(showContextFrame);
			this.controller.UpdateElements(true);
			this.controller.UpdateButtonLayout();
			QudMenuBottomContext qudMenuBottomContext = this.bottomContextController;
			if (qudMenuBottomContext != null)
			{
				qudMenuBottomContext.RefreshButtons();
			}
			this.Show();
			this.controller.Reselect(DefaultSelected);
			this.controller.Update();
			this.SizeContentToWidth();
			PopupMessage.lastPopupID = PopupID;
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000F8320 File Offset: 0x000F6520
		public void SizeContentToWidth()
		{
			LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform as RectTransform);
			int num = 10;
			while (this.controller.GetComponent<ContentSizeWithMax>().MaximumPreferredSize.y != base.rectTransform.rect.height - 100f || this.controller.GetComponent<ContentSizeWithMax>().MaximumPreferredSize.x != 840f)
			{
				this.controller.GetComponent<ContentSizeWithMax>().MaximumPreferredSize = new Vector2(840f, base.rectTransform.rect.height - 100f);
				Canvas.ForceUpdateCanvases();
				num--;
				if (num == 0)
				{
					break;
				}
			}
			if (this.Message.text != "{{y|}}")
			{
				float num2 = this.controller.GetComponent<RectTransform>().rect.width - 80f;
				if (this.Message.preferredWidth < num2)
				{
					this.Message.GetComponent<LayoutElement>().minWidth = num2;
				}
				LayoutRebuilder.ForceRebuildLayoutImmediate(base.transform as RectTransform);
			}
			base.canvasGroup.alpha = 1f;
			this.ClampPopupPosition();
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000F844C File Offset: 0x000F664C
		public void ClampPopupPosition()
		{
			RectTransform rectTransform = this.popupPositionTransform;
			RectTransform rectTransform2 = this.popupPositionTransform.parent as RectTransform;
			if (rectTransform.pivot.y == 1f)
			{
				float num = -rectTransform.anchoredPosition.y + rectTransform.rect.height;
				if (num > rectTransform2.rect.height)
				{
					rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + (num - rectTransform2.rect.height));
					return;
				}
			}
			else if (rectTransform.pivot.y == 0f)
			{
				float num2 = -rectTransform.anchoredPosition.y - rectTransform.rect.height;
				if (num2 < 0f)
				{
					rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + num2);
				}
			}
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000F8544 File Offset: 0x000F6744
		public void OnActivateCommand(QudMenuItem command)
		{
			if (!TutorialManager.AllowSelectedPopupCommand(this.PopupID, command))
			{
				return;
			}
			if (command.command == "CmdOptions")
			{
				return;
			}
			if (this.commandCallback != null)
			{
				this.commandCallback(command);
				this.commandCallback = null;
				this.Hide();
			}
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000F8594 File Offset: 0x000F6794
		public void OnSelect(QudMenuItem command)
		{
			if (!TutorialManager.AllowSelectedPopupCommand(this.PopupID, command))
			{
				return;
			}
			if (this.selectCallback != null)
			{
				this.selectCallback(command);
				this.selectCallback = null;
				this.Hide();
			}
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000F85C8 File Offset: 0x000F67C8
		public void OnInputSubmit(string value)
		{
			foreach (QudMenuItem qudMenuItem in this.controller.bottomContextOptions)
			{
				if (qudMenuItem.hotkey == "Submit" || qudMenuItem.hotkey == "Accept" || qudMenuItem.hotkey == "CmdOptions")
				{
					ControlManager.WaitForKeyup("Submit");
					ControlManager.WaitForKeyup("Accept");
					this.OnSelect(qudMenuItem);
					break;
				}
			}
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000F866C File Offset: 0x000F686C
		public void EnforceSuspensionContext()
		{
			if (NavigationController.instance.activeContext != null && NavigationController.instance.activeContext != NavigationController.instance.suspensionContext)
			{
				NavigationController.instance.activeContext = NavigationController.instance.suspensionContext;
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000F86A4 File Offset: 0x000F68A4
		public void Update()
		{
			if (this.HideNextFrame > 0)
			{
				this.HideNextFrame--;
				if (this.HideNextFrame <= 0)
				{
					this.HideNextFrame = 0;
					base.canvasGroup.alpha = 0f;
					if (base.canvas.enabled)
					{
						base.canvas.enabled = false;
					}
					base.gameObject.SetActive(false);
				}
				return;
			}
			if (base.isActiveAndEnabled && this.cancellationToken != default(CancellationToken) && this.cancellationToken.IsCancellationRequested)
			{
				this.commandCallback(default(QudMenuItem));
				this.selectCallback = null;
				this.Hide();
				return;
			}
			if (!string.IsNullOrEmpty(this.RestrictChars))
			{
				for (int i = 0; i < this.inputBox.text.Length; i++)
				{
					if (!this.RestrictChars.Contains(this.inputBox.text[i]))
					{
						this.inputBox.text = this.inputBox.text.Replace(string.Format("{0}", this.inputBox.text[i]), "");
					}
				}
			}
			if ((this.inputBox.contentType == TMP_InputField.ContentType.IntegerNumber || this.inputBox.contentType == TMP_InputField.ContentType.DecimalNumber) && Input.GetKeyDown(global::UnityEngine.KeyCode.Space))
			{
				this.OnInputSubmit(this.inputBox.text);
				return;
			}
			if (this.WantsSpecificPrompt != null && base.isActiveAndEnabled)
			{
				if (!ControlManager.isCommandPressed("CmdOptions"))
				{
					if (this.holdTime > 0f)
					{
						this.inputBox.text = "";
					}
					this.holdTime = 0f;
					if (this.bottomContextController.buttons.Count > 0)
					{
						this.bottomContextController.buttons.Last<SelectableTextMenuItem>().progressBar.gameObject.SetActive(false);
					}
				}
				else
				{
					this.holdTime += Time.deltaTime;
					if (this.holdTime >= 2f)
					{
						this.inputBox.text = this.WantsSpecificPrompt;
						this.controller.bottomContextOptions.Find((QudMenuItem item) => item.hotkey == "CmdOptions").text = this.WantsSpecificPrompt;
						this.OnInputSubmit(this.WantsSpecificPrompt);
						return;
					}
					SelectableTextMenuItem selectableTextMenuItem = this.bottomContextController.buttons.Last<SelectableTextMenuItem>();
					RectTransform component = this.bottomContextController.buttons.Last<SelectableTextMenuItem>().GetComponent<RectTransform>();
					selectableTextMenuItem.progressBar.gameObject.SetActive(true);
					selectableTextMenuItem.progressBar.sizeDelta = new Vector2(component.rect.width * (this.holdTime / 2f), component.rect.height);
					this.inputBox.text = this.WantsSpecificPrompt.Substring(0, (int)Math.Min((float)this.WantsSpecificPrompt.Length, (float)this.WantsSpecificPrompt.Length * (this.holdTime / 2f) + 1f));
				}
			}
			this.EnforceSuspensionContext();
			if (this.cancellationToken == default(CancellationToken) && this.controller.bottomContextOptions.Count == 0 && this.controller.menuData.Count == 0 && Input.anyKeyDown)
			{
				this.commandCallback(default(QudMenuItem));
				this.selectCallback = null;
				this.Hide();
			}
			foreach (QudMenuItem qudMenuItem in this.controller.menuData)
			{
				if (qudMenuItem.hotkey != null && qudMenuItem.hotkey.StartsWith("char:"))
				{
					char c = ((qudMenuItem.hotkey.Length > 5) ? qudMenuItem.hotkey[5] : '\0');
					if (c != '\0' && c != ' ' && (!this.inputBox.IsSelected() || c != '\b') && ControlManager.isCharDown(c))
					{
						if (this.commandCallback != null)
						{
							if (TutorialManager.AllowSelectedPopupCommand(this.PopupID, qudMenuItem))
							{
								this.commandCallback(qudMenuItem);
								this.commandCallback = null;
								this.Hide();
							}
						}
						return;
					}
				}
			}
			this.ClampPopupPosition();
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000F8B28 File Offset: 0x000F6D28
		public void BackgroundClicked()
		{
			QudMenuItem qudMenuItem = this.bottomContextController.items.Where<QudMenuItem>((QudMenuItem i) => i.hotkey == "Cancel" || i.hotkey == "Accept,Cancel").FirstOrDefault<QudMenuItem>();
			if (qudMenuItem.hotkey == "Cancel" || qudMenuItem.hotkey == "Accept,Cancel")
			{
				this.OnActivateCommand(qudMenuItem);
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000F8B95 File Offset: 0x000F6D95
		public override void Show()
		{
			this.HideNextFrame = 0;
			this.EnforceSuspensionContext();
			base.Show();
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000F8BAC File Offset: 0x000F6DAC
		public override void Hide()
		{
			this.HideNextFrame = 2;
			if (this.wasPushed)
			{
				this.wasPushed = false;
			}
			if (base.raycaster != null && base.raycaster.enabled)
			{
				base.raycaster.enabled = false;
			}
			if (base.canvasGroup != null && base.canvasGroup.interactable)
			{
				base.canvasGroup.interactable = false;
			}
			if (this.onHide != null)
			{
				Action action = this.onHide;
				this.onHide = null;
				action();
			}
		}

		// Token: 0x040019AD RID: 6573
		public UITextSkin Message;

		// Token: 0x040019AE RID: 6574
		public Action<QudMenuItem> commandCallback;

		// Token: 0x040019AF RID: 6575
		public Action<QudMenuItem> selectCallback;

		// Token: 0x040019B0 RID: 6576
		public QudTextMenuController controller;

		// Token: 0x040019B1 RID: 6577
		public ControlledTMPInputField inputBox;

		// Token: 0x040019B2 RID: 6578
		public IRenderableDisplayManager contextImage;

		// Token: 0x040019B3 RID: 6579
		public UIThreeColorProperties afterImage;

		// Token: 0x040019B4 RID: 6580
		public UITextSkin contextText;

		// Token: 0x040019B5 RID: 6581
		public GameObject contextFrame;

		// Token: 0x040019B6 RID: 6582
		public GameObject contextContainer;

		// Token: 0x040019B7 RID: 6583
		public GameObject TitleContainer;

		// Token: 0x040019B8 RID: 6584
		public UITextSkin Title;

		// Token: 0x040019B9 RID: 6585
		public static List<QudMenuItem> AnyKey = new List<QudMenuItem>();

		// Token: 0x040019BA RID: 6586
		public static List<QudMenuItem> _CancelButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Cancel}}",
				command = "Cancel",
				hotkey = "Cancel"
			}
		};

		// Token: 0x040019BB RID: 6587
		public static List<QudMenuItem> _CopyButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Copy}}",
				command = "Copy",
				hotkey = "char:c"
			},
			new QudMenuItem
			{
				text = "{{y|Cancel}}",
				command = "Cancel",
				hotkey = "Cancel"
			}
		};

		// Token: 0x040019BC RID: 6588
		public static readonly QudMenuItem LookButton = new QudMenuItem
		{
			text = "{{y|Look}}",
			command = "Look",
			hotkey = "char:l"
		};

		// Token: 0x040019BD RID: 6589
		public static List<QudMenuItem> _SingleButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Continue}}",
				command = "Accept",
				hotkey = "Accept,Cancel"
			}
		};

		// Token: 0x040019BE RID: 6590
		public static List<QudMenuItem> _YesNoButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Yes}}",
				command = "Yes",
				hotkey = "Y,Accept"
			},
			new QudMenuItem
			{
				text = "{{y|No}}",
				command = "No",
				hotkey = "N,Cancel"
			}
		};

		// Token: 0x040019BF RID: 6591
		public static List<QudMenuItem> _YesNoButtonJoystick = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Yes}}",
				command = "Yes",
				hotkey = "Y,Accept"
			},
			new QudMenuItem
			{
				text = "{{y|No}}",
				command = "No",
				hotkey = "N,Cancel"
			}
		};

		// Token: 0x040019C0 RID: 6592
		public static List<QudMenuItem> _YesNoCancelButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Yes}}",
				command = "Yes",
				hotkey = "Y,Accept"
			},
			new QudMenuItem
			{
				text = "{{y|No}}",
				command = "No",
				hotkey = "N,V Negative"
			},
			new QudMenuItem
			{
				text = "{{y|Cancel}}",
				command = "Cancel",
				hotkey = "Cancel"
			}
		};

		// Token: 0x040019C1 RID: 6593
		public static List<QudMenuItem> _AcceptCancelButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Accept}}",
				command = "keep",
				hotkey = "Accept"
			},
			new QudMenuItem
			{
				text = "{{y|Cancel}}",
				command = "Cancel",
				hotkey = "Cancel"
			}
		};

		// Token: 0x040019C2 RID: 6594
		public static List<QudMenuItem> _AcceptCancelColorButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Submit}}",
				command = "keep",
				hotkey = "Submit"
			},
			new QudMenuItem
			{
				text = "{{y|Cancel}}",
				command = "Cancel",
				hotkey = "Cancel"
			},
			new QudMenuItem
			{
				text = "{{y|Color}}",
				command = "Color",
				hotkey = "CmdHelp"
			}
		};

		// Token: 0x040019C3 RID: 6595
		public static List<QudMenuItem> _SubmitCancelButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Submit}}",
				command = "keep",
				hotkey = "Submit"
			},
			new QudMenuItem
			{
				text = "{{y|Cancel}}",
				command = "Cancel",
				hotkey = "Cancel"
			}
		};

		// Token: 0x040019C4 RID: 6596
		public static List<QudMenuItem> _SubmitCancelColorButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Submit}}",
				command = "keep",
				hotkey = "Submit"
			},
			new QudMenuItem
			{
				text = "{{y|Cancel}}",
				command = "Cancel",
				hotkey = "Cancel"
			},
			new QudMenuItem
			{
				text = "{{y|Color}}",
				command = "Color",
				hotkey = "CmdHelp"
			}
		};

		// Token: 0x040019C5 RID: 6597
		public static List<QudMenuItem> AcceptButton = new List<QudMenuItem>
		{
			new QudMenuItem
			{
				text = "{{y|Accept}}",
				command = "keep",
				hotkey = "Accept"
			}
		};

		// Token: 0x040019C6 RID: 6598
		public QudMenuBottomContext bottomContextController;

		// Token: 0x040019C7 RID: 6599
		public Action onHide;

		// Token: 0x040019C8 RID: 6600
		public CancellationToken cancellationToken;

		// Token: 0x040019C9 RID: 6601
		public bool wasPushed;

		// Token: 0x040019CA RID: 6602
		public string WantsSpecificPrompt;

		// Token: 0x040019CB RID: 6603
		public string RestrictChars;

		// Token: 0x040019CC RID: 6604
		public VerticalLayoutGroup centringLayoutGroup;

		// Token: 0x040019CD RID: 6605
		public RectTransform popupPositionTransform;

		// Token: 0x040019CE RID: 6606
		public static Location2D LOCATION_AT_MOUSE_CURSOR = new Location2D(-9999, -9999);

		// Token: 0x040019CF RID: 6607
		public string PopupID;

		// Token: 0x040019D0 RID: 6608
		public static string lastPopupID;

		// Token: 0x040019D1 RID: 6609
		public float holdTime;

		// Token: 0x040019D2 RID: 6610
		private int HideNextFrame;
	}
}
