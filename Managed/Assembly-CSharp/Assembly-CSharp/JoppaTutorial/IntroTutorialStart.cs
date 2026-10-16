using System;
using System.Collections.Generic;
using System.Linq;
using Qud.UI;
using UnityEngine;
using XRL;
using XRL.CharacterBuilds;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World.Parts;

namespace JoppaTutorial
{
	// Token: 0x020003F6 RID: 1014
	public class IntroTutorialStart : TutorialStep
	{
		// Token: 0x060025E4 RID: 9700 RVA: 0x000C331B File Offset: 0x000C151B
		public override bool AllowCommand(string id)
		{
			return true;
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x000C3320 File Offset: 0x000C1520
		public override bool AllowOnSelected(FrameworkDataElement element)
		{
			return element.Id == "Name" || element.Id == "Chargen:Back" || element.Id == "Chargen:Next" || element.Id == "Mutated Human" || element.Id == "Marsh Taur" || element.Id == "JoppaTutorial";
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x000C33A8 File Offset: 0x000C15A8
		public override void LateUpdate()
		{
			string text = null;
			int num = 64;
			string text2 = "nw";
			GameObject gameObject = ControlId.Get("Mutated Human", false);
			object obj;
			if (gameObject == null)
			{
				obj = null;
			}
			else
			{
				GameObject gameObject2 = gameObject.gameObject;
				obj = ((gameObject2 != null) ? gameObject2.transform : null);
			}
			RectTransform rectTransform = obj as RectTransform;
			if (rectTransform != null)
			{
				text = "Welcome to the Caves of Qud tutorial. We'll just be scratching the surface here, learning enough of the basics to help you get your footing.\n\nIn Caves of Qud, you play as a mutated human or true kin.\n\nFor the tutorial, we're picking mutated human.";
				if (Media.sizeClass <= Media.SizeClass.Small)
				{
					text2 = "ne";
				}
			}
			if (rectTransform == null)
			{
				GameObject gameObject3 = ControlId.Get("Marsh Taur", false);
				object obj2;
				if (gameObject3 == null)
				{
					obj2 = null;
				}
				else
				{
					GameObject gameObject4 = gameObject3.gameObject;
					obj2 = ((gameObject4 != null) ? gameObject4.transform : null);
				}
				rectTransform = obj2 as RectTransform;
				if (rectTransform != null)
				{
					text = "Character creation is a deep and sometimes long process. We included some preset builds to help you get started. After the tutorial, you can try another build, or make a character from scratch. (The recommended way! Once you get your footing.) \n\nFor now, pick the marsh taur.";
					if (Media.sizeClass <= Media.SizeClass.Small)
					{
						text2 = "ne";
					}
				}
			}
			if (rectTransform == null)
			{
				GameObject gameObject5 = ControlId.Get("JoppaTutorial", false);
				object obj3;
				if (gameObject5 == null)
				{
					obj3 = null;
				}
				else
				{
					GameObject gameObject6 = gameObject5.gameObject;
					obj3 = ((gameObject6 != null) ? gameObject6.transform : null);
				}
				rectTransform = obj3 as RectTransform;
				text = "<noframe>";
			}
			if (rectTransform == null)
			{
				GameObject gameObject7 = ControlId.Get("Chargen:Next", false);
				object obj4;
				if (gameObject7 == null)
				{
					obj4 = null;
				}
				else
				{
					GameObject gameObject8 = gameObject7.gameObject;
					obj4 = ((gameObject8 != null) ? gameObject8.transform : null);
				}
				rectTransform = obj4 as RectTransform;
				UIManager instance = UIManager.instance;
				if (((instance != null) ? instance.currentWindow : null) != null)
				{
					UIManager instance2 = UIManager.instance;
					string text3;
					if (instance2 == null)
					{
						text3 = null;
					}
					else
					{
						WindowBase currentWindow = instance2.currentWindow;
						text3 = ((currentWindow != null) ? currentWindow.name : null);
					}
					if (text3 == "Summary")
					{
						text = "Here is a summary of your attributes and mutations, which grant your character unique abilities.";
						num = 16;
						if (Media.sizeClass <= Media.SizeClass.Small)
						{
							text2 = "se";
						}
					}
					UIManager instance3 = UIManager.instance;
					string text4;
					if (instance3 == null)
					{
						text4 = null;
					}
					else
					{
						WindowBase currentWindow2 = instance3.currentWindow;
						text4 = ((currentWindow2 != null) ? currentWindow2.name : null);
					}
					if (text4 == "Customize")
					{
						text = "You can name your character or choose Next for a random name.";
						num = 16;
						if (Media.sizeClass <= Media.SizeClass.Small)
						{
							text2 = "se";
						}
					}
				}
				if (rectTransform == null)
				{
					GameObject gameObject9 = ControlId.Get("Chargen:Next:Small", false);
					object obj5;
					if (gameObject9 == null)
					{
						obj5 = null;
					}
					else
					{
						GameObject gameObject10 = gameObject9.gameObject;
						obj5 = ((gameObject10 != null) ? gameObject10.transform : null);
					}
					rectTransform = obj5 as RectTransform;
					text = "<noframe>";
				}
			}
			TutorialManager manager = this.manager;
			object obj6;
			if (rectTransform == null)
			{
				obj6 = null;
			}
			else
			{
				GameObject gameObject11 = rectTransform.gameObject;
				obj6 = ((gameObject11 != null) ? gameObject11.transform : null);
			}
			manager.Highlight(obj6 as RectTransform, text, text2, (float)num, (float)num, 0f, "big");
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000C35D0 File Offset: 0x000C17D0
		public override void OnBootGame(XRLGame game, EmbarkInfo info)
		{
			IEnumerable<string> preferenceOrder = ActivatedAbilities.PreferenceOrder;
			List<string> list = ((preferenceOrder != null) ? preferenceOrder.ToList<string>() : null) ?? new List<string>();
			if (list.Contains("CommandToggleRunning"))
			{
				list.Remove("CommandToggleRunning");
			}
			if (list.Contains("CommandFreezingRay"))
			{
				list.Remove("CommandFreezingRay");
			}
			if (list.Contains("CommandSurvivalCamp"))
			{
				list.Remove("CommandSurvivalCamp");
			}
			game.SetStringGameState("embarkIntroText", "On the 30th of Kisu Ux, you scuttle down a dark shaft at the edge of a sunken trade path and arrive at a caravanserai. It's powdered in salt and dust from across the ribbon of time.");
			game.SetStringGameState("JoppaWorldTutorial", "Yes");
			list.Insert(0, "CommandSurvivalCamp");
			list.Insert(0, "CommandFreezingRay");
			list.Insert(0, "CommandToggleRunning");
			ActivatedAbilities.PreferenceOrder = list;
			TutorialManager.AdvanceStep(new MoveToChest());
		}
	}
}
