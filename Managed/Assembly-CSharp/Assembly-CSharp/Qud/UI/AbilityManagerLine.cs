using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x02000429 RID: 1065
	public class AbilityManagerLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06002849 RID: 10313 RVA: 0x000D05A0 File Offset: 0x000CE7A0
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000D05A8 File Offset: 0x000CE7A8
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			AbilityManagerLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action> { 
				{
					InputAxisTypes.NavigationVAxis,
					new Action(this.HandleNavigationVAxis)
				} };
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>
				{
					{
						AbilityManagerLine.BIND_KEY.InputCommand,
						new Action(this.HandleRebind)
					},
					{
						AbilityManagerLine.UNBIND_KEY.InputCommand,
						new Action(this.HandleRemoveBind)
					}
				};
			}
			if (SingletonWindowBase<AbilityManagerScreen>.instance.sortMode == AbilityManagerScreen.SortMode.Custom)
			{
				this.context.menuOptionDescriptions = new List<MenuOption>
				{
					AbilityManagerLine.MOVE_UP,
					AbilityManagerLine.MOVE_DOWN,
					AbilityManagerLine.BIND_KEY
				};
			}
			else
			{
				this.context.menuOptionDescriptions = new List<MenuOption> { AbilityManagerLine.BIND_KEY };
			}
			if (!string.IsNullOrEmpty(this.context.data.hotkeyDescription))
			{
				if (this.context.menuOptionDescriptions.Count == 3)
				{
					this.context.menuOptionDescriptions.Add(AbilityManagerLine.UNBIND_KEY);
					return;
				}
			}
			else if (this.context.menuOptionDescriptions.Count == 4)
			{
				this.context.menuOptionDescriptions.RemoveAt(3);
			}
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x000D0700 File Offset: 0x000CE900
		public void HandleNavigationVAxis()
		{
			if (this.context.data.category != null)
			{
				this.screen.HandleSelectLeft(this.context.data);
			}
			else if (this.screen.sortMode == AbilityManagerScreen.SortMode.Custom)
			{
				this.screen.MoveItem(this.context.data, NavigationController.currentEvent.axisValue.GetValueOrDefault());
			}
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x000D0774 File Offset: 0x000CE974
		public async void HandleRebind()
		{
			if (!AbilityManagerLine.dragging)
			{
				NavigationController.currentEvent.Handle();
				if (this.context.data.ability != null)
				{
					NavigationController.currentEvent.Handle();
					await NavigationController.instance.SuspendContextWhile<bool>(async delegate
					{
						await AbilityManagerScreen.HandleRebindAsync(this.context.data.ability, null);
						return true;
					});
					SingletonWindowBase<AbilityManagerScreen>.instance.Refresh(true);
					GameManager.Instance.SetActiveLayersForNavCategory("Menu", true);
				}
			}
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x000D07AC File Offset: 0x000CE9AC
		public async void HandleRemoveBind()
		{
			if (!AbilityManagerLine.dragging)
			{
				NavigationController.currentEvent.Handle();
				if (this.context.data.hotkeyDescription != null)
				{
					TaskAwaiter<bool> taskAwaiter = AbilityManagerScreen.HandleRemoveBindAsync(this.context.data.ability).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<bool> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<bool>);
					}
					if (taskAwaiter.GetResult())
					{
						this.screen.Refresh(true);
					}
					GameManager.Instance.SetActiveLayersForNavCategory("Menu", true);
				}
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000D07E4 File Offset: 0x000CE9E4
		public void setData(FrameworkDataElement data)
		{
			AbilityManagerLineData abilityManagerLineData = data as AbilityManagerLineData;
			if (abilityManagerLineData != null)
			{
				this.context.data = abilityManagerLineData;
				if (abilityManagerLineData.category != null)
				{
					string text = (abilityManagerLineData.collapsed ? "-" : "+");
					this.text.SetText("[" + text + "] " + abilityManagerLineData.category);
					this.icon.gameObject.SetActive(false);
					return;
				}
				this.icon.gameObject.SetActive(true);
				this.icon.FromRenderable(abilityManagerLineData.ability.GetUITile(), true);
				this.SB.Clear();
				ActivatedAbilityEntry ability = abilityManagerLineData.ability;
				if (!ability.Enabled)
				{
					this.SB.Append(string.Concat(new string[]
					{
						"{{K|",
						abilityManagerLineData.quickKey.ToString(),
						") ",
						ability.DisplayName,
						ability.IsAttack ? " [attack]" : "",
						" [disabled]}}"
					}));
				}
				else if (ability.Cooldown <= 0)
				{
					if (ability.IsRealityDistortionBased && !abilityManagerLineData.realityIsWeak)
					{
						this.SB.Append(string.Concat(new string[]
						{
							"{{K|",
							abilityManagerLineData.quickKey.ToString(),
							") ",
							ability.DisplayName,
							ability.IsAttack ? " [attack]" : "",
							" [astrally tethered]}}"
						}));
					}
					else
					{
						this.SB.Append(abilityManagerLineData.quickKey.ToString() + ") " + ability.DisplayName + (ability.IsAttack ? " [{{W|attack}}]" : ""));
					}
				}
				else if (ability.IsRealityDistortionBased && !abilityManagerLineData.realityIsWeak)
				{
					this.SB.Append(string.Concat(new string[]
					{
						"{{K|",
						abilityManagerLineData.quickKey.ToString(),
						"}}) ",
						ability.DisplayName,
						" [{{C|",
						ability.CooldownRounds.ToString(),
						"}} turn cooldown, astrally tethered]"
					}));
				}
				else
				{
					this.SB.Append(string.Concat(new string[]
					{
						"{{K|",
						abilityManagerLineData.quickKey.ToString(),
						"}}) ",
						ability.DisplayName,
						" [{{C|",
						ability.CooldownRounds.ToString(),
						"}} turn cooldown]"
					}));
				}
				if (ability.Toggleable)
				{
					if (ability.ToggleState)
					{
						this.SB.Append(" {{K|[{{g|Toggled on}}]}}");
					}
					else
					{
						this.SB.Append(" {{K|[{{y|Toggled off}}]}}");
					}
				}
				if (!string.IsNullOrEmpty(abilityManagerLineData.hotkeyDescription))
				{
					this.SB.Append(" {{Y|<{{w|" + abilityManagerLineData.hotkeyDescription + "}}>}}");
				}
				this.text.SetText(this.SB.ToString());
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000D0B08 File Offset: 0x000CED08
		public void OnBeginDrag(PointerEventData eventData)
		{
			AbilityManagerLine.dragging = true;
			if (SingletonWindowBase<AbilityManagerScreen>.instance.sortMode != AbilityManagerScreen.SortMode.Custom)
			{
				eventData.pointerDrag = null;
				return;
			}
			CursorManager.instance.currentStyle = CursorManager.Style.ResizeNorthSouth;
			this.spacers.Clear();
			this.spacers.AddRange(base.transform.parent.GetComponentsInChildren<AbilityManagerSpacer>());
			this.HighlightClosestSpacer(eventData.position);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000D0B70 File Offset: 0x000CED70
		public int HighlightClosestSpacer(Vector2 screenPosition)
		{
			AbilityManagerSpacer abilityManagerSpacer = ((this.spacers.Count > 0) ? this.spacers[0] : null);
			float num = float.MaxValue;
			int num2 = -1;
			Vector3[] array = new Vector3[4];
			int num3 = 0;
			foreach (AbilityManagerSpacer abilityManagerSpacer2 in this.spacers)
			{
				(abilityManagerSpacer2.transform as RectTransform).GetWorldCorners(array);
				float magnitude = (new Vector2((array[0].x + array[2].x) / 2f, (array[0].y + array[2].y) / 2f) - screenPosition).magnitude;
				if (magnitude < num)
				{
					abilityManagerSpacer = abilityManagerSpacer2;
					num = magnitude;
					num2 = num3;
				}
				abilityManagerSpacer2.image.enabled = false;
				num3++;
			}
			if (abilityManagerSpacer != null)
			{
				abilityManagerSpacer.image.enabled = true;
			}
			return num2;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000D0C94 File Offset: 0x000CEE94
		public void OnDrag(PointerEventData eventData)
		{
			this.HighlightClosestSpacer(eventData.position);
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000D0CA4 File Offset: 0x000CEEA4
		public void OnEndDrag(PointerEventData eventData)
		{
			CursorManager.setStyle(CursorManager.Style.Pointer);
			if (!AbilityManagerLine.dragging)
			{
				return;
			}
			AbilityManagerLine.dragging = false;
			int num = this.HighlightClosestSpacer(eventData.position);
			this.spacers[num].image.enabled = false;
			int num2 = base.transform.GetSiblingIndex() / 2;
			if (num == num2 || num == num2 + 1)
			{
				return;
			}
			if (num < num2)
			{
				this.screen.MoveItem(this.context.data, num - num2);
				return;
			}
			this.screen.MoveItem(this.context.data, num - num2 - 1);
		}

		// Token: 0x0400140D RID: 5133
		private AbilityManagerLine.Context context = new AbilityManagerLine.Context();

		// Token: 0x0400140E RID: 5134
		public UITextSkin text;

		// Token: 0x0400140F RID: 5135
		public UIThreeColorProperties icon;

		// Token: 0x04001410 RID: 5136
		public AbilityManagerScreen screen;

		// Token: 0x04001411 RID: 5137
		public StringBuilder SB = new StringBuilder();

		// Token: 0x04001412 RID: 5138
		public static MenuOption MOVE_DOWN = new MenuOption
		{
			InputCommand = "V Positive",
			Description = "Move Down",
			disabled = true
		};

		// Token: 0x04001413 RID: 5139
		public static MenuOption MOVE_UP = new MenuOption
		{
			InputCommand = "V Negative",
			Description = "Move Up",
			disabled = true
		};

		// Token: 0x04001414 RID: 5140
		public static MenuOption BIND_KEY = new MenuOption
		{
			InputCommand = "CmdInsert",
			Description = "Bind Key",
			disabled = true
		};

		// Token: 0x04001415 RID: 5141
		public static MenuOption UNBIND_KEY = new MenuOption
		{
			InputCommand = "CmdDelete",
			Description = "Unbind Key",
			disabled = true
		};

		// Token: 0x04001416 RID: 5142
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x04001417 RID: 5143
		public static bool dragging = false;

		// Token: 0x020017E5 RID: 6117
		public class Context : NavigationContext
		{
			// Token: 0x04006041 RID: 24641
			public AbilityManagerLineData data;
		}
	}
}
