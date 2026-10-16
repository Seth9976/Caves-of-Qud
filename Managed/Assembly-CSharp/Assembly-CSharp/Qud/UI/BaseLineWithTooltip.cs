using System;
using ConsoleLib.Console;
using ModelShark;
using QupKit;
using UnityEngine;
using UnityEngine.EventSystems;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace Qud.UI
{
	// Token: 0x02000464 RID: 1124
	public class BaseLineWithTooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x06002A98 RID: 10904 RVA: 0x000E06E8 File Offset: 0x000DE8E8
		public Vector2 ScreenToRectPos(Vector2 screen_pos, Canvas canvas, RectTransform rectTransform)
		{
			if (canvas.renderMode != RenderMode.ScreenSpaceOverlay && canvas.worldCamera != null)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screen_pos, canvas.worldCamera, out vector);
				return vector;
			}
			Vector2 vector2 = screen_pos - new Vector2(rectTransform.position.x, rectTransform.position.y);
			vector2 = new Vector2(vector2.x / rectTransform.lossyScale.x, vector2.y / rectTransform.lossyScale.y);
			return vector2;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000E076C File Offset: 0x000DE96C
		public virtual void Update()
		{
			if (!TutorialManager.AllowTooltipUpdate())
			{
				return;
			}
			if (this.tooltipGo != null)
			{
				if (this.tooltipContextActive)
				{
					if (!this.ShiftShowing && (Input.GetKey(global::UnityEngine.KeyCode.LeftAlt) || Input.GetKey(global::UnityEngine.KeyCode.RightAlt)))
					{
						this.ShiftShowing = true;
						this.StartTooltip(this.tooltipGo, this.tooltipCompareGo, false, base.gameObject.transform as RectTransform);
						return;
					}
					if (ControlManager.GetButton("Highlight", true) && !this.ShiftShowing)
					{
						this.GamepadAltShowing = true;
						this.StartTooltip(this.tooltipGo, this.tooltipCompareGo, false, base.gameObject.transform as RectTransform);
						return;
					}
				}
				if (this.ShiftShowing && !Input.GetKey(global::UnityEngine.KeyCode.LeftAlt) && !Input.GetKey(global::UnityEngine.KeyCode.RightAlt))
				{
					this.ShiftShowing = false;
					TooltipTrigger tooltipTrigger = this.tooltip;
					if (tooltipTrigger != null)
					{
						tooltipTrigger.ForceHideTooltip();
					}
					this.tooltip = null;
				}
				else if (this.GamepadAltShowing && !ControlManager.GetButton("Highlight", true))
				{
					this.GamepadAltShowing = false;
					TooltipTrigger tooltipTrigger2 = this.tooltip;
					if (tooltipTrigger2 != null)
					{
						tooltipTrigger2.ForceHideTooltip();
					}
					this.tooltip = null;
				}
				else if (!this.tooltipContextActive)
				{
					this.Inside = false;
					this.GamepadAltShowing = false;
					this.ShiftShowing = false;
				}
			}
			if (!this.Inside && !this.ShiftShowing && !this.GamepadAltShowing && this.tooltip != null)
			{
				this.tooltip.ForceHideTooltip();
				this.tooltip = null;
			}
			if (this.Inside && this.tooltip == null && this.tooltipGo != null && !this.ShiftShowing && !this.GamepadAltShowing)
			{
				this.TooltipTimer += Time.deltaTime;
				if (this.TooltipTimer >= BaseLineWithTooltip.TOOLTIP_DELAY)
				{
					this.StartTooltip(this.tooltipGo, this.tooltipCompareGo, true, null);
				}
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000E094F File Offset: 0x000DEB4F
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Inside = true;
			this.TooltipTimer = 0f;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000E0963 File Offset: 0x000DEB63
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Inside = false;
			TooltipTrigger tooltipTrigger = this.tooltip;
			if (tooltipTrigger != null)
			{
				tooltipTrigger.ForceHideTooltip();
			}
			this.tooltip = null;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x000E0984 File Offset: 0x000DEB84
		public void StartTooltip(global::XRL.World.GameObject go, global::XRL.World.GameObject compareGo, bool mousePosition = true, RectTransform targetRect = null)
		{
			Look.TooltipInformation tooltipInformation = Look.GenerateTooltipInformation(go);
			Look.TooltipInformation tooltipInformation2 = default(Look.TooltipInformation);
			if (compareGo != null && compareGo.IsValid() && compareGo.HasPart<Description>())
			{
				tooltipInformation2 = Look.GenerateTooltipInformation(compareGo);
			}
			string text = "looker";
			if (this.tooltip != null)
			{
				return;
			}
			this.tooltip = ((text == "looker") ? GameManager.Instance.lookerTooltip : GameManager.Instance.tileTooltip);
			if (compareGo != null)
			{
				this.tooltip = GameManager.Instance.compareLookerTooltip;
			}
			foreach (ParameterizedTextField parameterizedTextField in this.tooltip.parameterizedTextFields)
			{
				ParameterizedTextField parameterizedTextField2;
				string name = (parameterizedTextField2 = parameterizedTextField).name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				string text2;
				if (num <= 1619459020U)
				{
					if (num <= 765268398U)
					{
						if (num != 596592551U)
						{
							if (num != 765268398U)
							{
								goto IL_0235;
							}
							if (!(name == "WoundLevel"))
							{
								goto IL_0235;
							}
							text2 = tooltipInformation.WoundLevel;
						}
						else
						{
							if (!(name == "LongDescription"))
							{
								goto IL_0235;
							}
							text2 = tooltipInformation.LongDescription.Trim();
						}
					}
					else if (num != 1023647740U)
					{
						if (num != 1619459020U)
						{
							goto IL_0235;
						}
						if (!(name == "DisplayName2"))
						{
							goto IL_0235;
						}
						text2 = tooltipInformation2.DisplayName;
					}
					else
					{
						if (!(name == "ConText"))
						{
							goto IL_0235;
						}
						text2 = tooltipInformation.SubHeader;
					}
				}
				else if (num <= 2408427407U)
				{
					if (num != 1782757524U)
					{
						if (num != 2408427407U)
						{
							goto IL_0235;
						}
						if (!(name == "LongDescription2"))
						{
							goto IL_0235;
						}
						text2 = tooltipInformation2.LongDescription.Trim();
					}
					else
					{
						if (!(name == "WoundLevel2"))
						{
							goto IL_0235;
						}
						text2 = tooltipInformation2.WoundLevel;
					}
				}
				else if (num != 3669266762U)
				{
					if (num != 4176258230U)
					{
						goto IL_0235;
					}
					if (!(name == "DisplayName"))
					{
						goto IL_0235;
					}
					text2 = tooltipInformation.DisplayName;
				}
				else
				{
					if (!(name == "ConText2"))
					{
						goto IL_0235;
					}
					text2 = tooltipInformation2.SubHeader;
				}
				IL_023C:
				parameterizedTextField2.value = RTF.FormatToRTF(Markup.Color("y", text2), "FF", 60, false) ?? "";
				continue;
				IL_0235:
				text2 = "";
				goto IL_023C;
			}
			this.tooltip.AdditionalData = new DualImageRenderableGlue.DualImageRenderableGlueData(tooltipInformation.IconRenderable, tooltipInformation2.IconRenderable);
			if (!this.tooltip.transform.parent.gameObject.activeInHierarchy)
			{
				this.tooltip.transform.parent.gameObject.SetActive(true);
			}
			if (mousePosition)
			{
				this.tooltip.ShowManually(true, Input.mousePosition, true, false);
			}
			else
			{
				targetRect.GetWorldCorners(this.corners);
				this.tooltip.ShowManually(true, this.corners[0], true, false);
			}
			TutorialManager.StartingLineTooltip(go, compareGo);
			if (go != null)
			{
				Action <>9__1;
				this.tooltip.onHideAction = delegate
				{
					ThreadTaskQueue gameQueue = GameManager.Instance.gameQueue;
					string text3 = "LookedAt" + go.GetHashCode().ToString();
					Action action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate
						{
							go.FireEvent(global::XRL.World.Event.New("AfterLookedAt", "Looker", The.Player));
							global::XRL.World.GameObject player = The.Player;
							if (player == null)
							{
								return;
							}
							player.FireEvent(global::XRL.World.Event.New("LookedAt", "Object", go));
						});
					}
					gameQueue.queueSingletonTask(text3, action, false);
				};
			}
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x000E0CF4 File Offset: 0x000DEEF4
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Inside = false;
		}

		// Token: 0x04001691 RID: 5777
		public bool tooltipContextActive;

		// Token: 0x04001692 RID: 5778
		public global::XRL.World.GameObject tooltipGo;

		// Token: 0x04001693 RID: 5779
		public global::XRL.World.GameObject tooltipCompareGo;

		// Token: 0x04001694 RID: 5780
		public TooltipTrigger tooltip;

		// Token: 0x04001695 RID: 5781
		public bool Inside;

		// Token: 0x04001696 RID: 5782
		public float TooltipTimer;

		// Token: 0x04001697 RID: 5783
		public bool ShiftShowing;

		// Token: 0x04001698 RID: 5784
		public static float TOOLTIP_DELAY = 2f;

		// Token: 0x04001699 RID: 5785
		private bool GamepadAltShowing;

		// Token: 0x0400169A RID: 5786
		private Vector3[] corners = new Vector3[4];
	}
}
