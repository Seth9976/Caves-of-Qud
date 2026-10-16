using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013BB RID: 5051
	[Serializable]
	public class Tinkering_LayMine : BaseSkill
	{
		// Token: 0x0600C2F0 RID: 49904 RVA: 0x00472B5F File Offset: 0x00470D5F
		public static void Init()
		{
		}

		// Token: 0x0600C2F1 RID: 49905 RVA: 0x00472B61 File Offset: 0x00470D61
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C2F2 RID: 49906 RVA: 0x00472B77 File Offset: 0x00470D77
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2F3 RID: 49907 RVA: 0x00472B9A File Offset: 0x00470D9A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C2F4 RID: 49908 RVA: 0x00472B9D File Offset: 0x00470D9D
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandLayMine");
			Registrar.Register("CommandLayMineTimed");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C2F5 RID: 49909 RVA: 0x00472BBD File Offset: 0x00470DBD
		private bool Explosives(GameObject obj)
		{
			return obj != this.ParentObject && (obj.HasPart<Tinkering_Layable>() || obj.HasPart<Tinkering_Mine>());
		}

		// Token: 0x0600C2F6 RID: 49910 RVA: 0x00472BE0 File Offset: 0x00470DE0
		public static GameObject CreateBomb(GameObject obj = null, GameObject Actor = null, int Countdown = -1)
		{
			if (obj == null)
			{
				obj = GameObject.Create(PopulationManager.RollOneFrom("DynamicObjectsTable:Grenades", null, null).Blueprint);
			}
			Tinkering_Mine tinkering_Mine = obj.GetPart<Tinkering_Mine>();
			GameObject gameObject;
			if (tinkering_Mine == null)
			{
				gameObject = GameObject.Create("MineShell");
				tinkering_Mine = gameObject.GetPart<Tinkering_Mine>();
				obj = obj.RemoveOne();
				obj.RemoveFromContext(null);
				tinkering_Mine.SetExplosive(obj);
			}
			else
			{
				gameObject = obj;
				obj = tinkering_Mine.Explosive;
			}
			Tinkering_Layable part = obj.GetPart<Tinkering_Layable>();
			if (part == null)
			{
				throw new Exception("no Tinkering_Layable part in " + obj.DebugName);
			}
			Render render = gameObject.Render;
			Render render2 = obj.Render;
			TinkerItem part2 = gameObject.GetPart<TinkerItem>();
			TinkerItem part3 = obj.GetPart<TinkerItem>();
			string text = ((Countdown > 0) ? "bomb" : "mine");
			tinkering_Mine.Message = part.DetonationMessage;
			tinkering_Mine.Timer = Countdown;
			tinkering_Mine.Arm(Actor);
			if (render != null && render2 != null)
			{
				string text2 = render2.DisplayName;
				if (text2.Contains("grenade"))
				{
					text2 = text2.Replace("grenade", text);
				}
				else
				{
					text2 = text2 + " " + text;
				}
				render.DisplayName = text2;
			}
			Examiner part4 = obj.GetPart<Examiner>();
			if (part4 != null)
			{
				gameObject.RequirePart<Examiner>(false).CopyFrom(part4);
			}
			if (part2 != null && part3 != null)
			{
				part2.SubstituteBlueprint = (part3.SubstituteBlueprint.IsNullOrEmpty() ? obj.Blueprint : part3.SubstituteBlueprint);
				part2.CanBuild = part3.CanBuild;
				part2.CanDisassemble = part3.CanDisassemble;
			}
			return gameObject;
		}

		// Token: 0x0600C2F7 RID: 49911 RVA: 0x00472D5C File Offset: 0x00470F5C
		public static GameObject CreateBomb(string Blueprint, GameObject Actor = null, int Countdown = -1)
		{
			return Tinkering_LayMine.CreateBomb(GameObject.CreateUnmodified(Blueprint), Actor, Countdown);
		}

		// Token: 0x0600C2F8 RID: 49912 RVA: 0x00472D6B File Offset: 0x00470F6B
		public static GameObject CreateMine(GameObject obj = null, GameObject Actor = null)
		{
			return Tinkering_LayMine.CreateBomb(obj, Actor, -1);
		}

		// Token: 0x0600C2F9 RID: 49913 RVA: 0x00472D75 File Offset: 0x00470F75
		public static GameObject CreateMine(string Blueprint, GameObject Actor = null)
		{
			return Tinkering_LayMine.CreateMine(GameObject.CreateUnmodified(Blueprint), Actor);
		}

		// Token: 0x0600C2FA RID: 49914 RVA: 0x00472D84 File Offset: 0x00470F84
		private bool AttemptLayMine(bool Timed = false)
		{
			if (this.ParentObject.OnWorldMap())
			{
				if (this.ParentObject.IsPlayer())
				{
					Popup.ShowFail("You cannot do that on the world map.", true, true, true);
				}
				return false;
			}
			if (!this.ParentObject.CheckFrozen(false, true, false, null))
			{
				return false;
			}
			Inventory inventory = this.ParentObject.Inventory;
			string text = (Timed ? "Select an explosive to set as a bomb:\n\n" : "Select an explosive to lay as a mine:\n\n");
			List<GameObject> objects = inventory.GetObjects(new Predicate<GameObject>(this.Explosives));
			if (objects.Count == 0)
			{
				if (this.ParentObject.IsPlayer())
				{
					Popup.ShowFail("You have no explosives to deploy!", true, true, true);
				}
				return false;
			}
			GameObject gameObject = (this.ParentObject.IsPlayer() ? Popup.PickGameObject(text, objects, true, false, true, null, 0, false) : objects.GetRandomElement<GameObject>(null));
			if (gameObject == null)
			{
				return false;
			}
			Cell cell;
			if (this.ParentObject.IsPlayer())
			{
				string text2 = XRL.UI.PickDirection.ShowPicker("Lay Mine");
				if (text2 == null)
				{
					return false;
				}
				cell = this.ParentObject.CurrentCell.GetCellFromDirection(text2, true);
			}
			else
			{
				cell = this.ParentObject.CurrentCell.GetEmptyAdjacentCells().GetRandomElement<Cell>(null);
			}
			if (cell == null || !cell.IsEmpty())
			{
				if (this.ParentObject.IsPlayer())
				{
					Popup.ShowFail("You can't deploy there!", true, true, true);
				}
				return false;
			}
			int num = -1;
			if (Timed)
			{
				if (this.ParentObject.IsPlayer())
				{
					int? num2 = Popup.AskNumber("For how many rounds would you like to set the timer? (max=10)", "Sounds/UI/ui_notification", "", 1, 0, 10);
					if (num2 != null)
					{
						int? num3 = num2;
						int num4 = 0;
						if (!((num3.GetValueOrDefault() == num4) & (num3 != null)))
						{
							try
							{
								num = Convert.ToInt32(num2);
								if (num > 10)
								{
									num = 10;
								}
								else if (num < 0)
								{
									return false;
								}
								num++;
								goto IL_01C3;
							}
							catch
							{
								return false;
							}
							goto IL_01B9;
						}
					}
					return false;
				}
				IL_01B9:
				num = Stat.Random(6, 11);
			}
			IL_01C3:
			GameObject gameObject2;
			if (Timed)
			{
				gameObject2 = Tinkering_LayMine.CreateBomb(gameObject, this.ParentObject, num);
			}
			else
			{
				gameObject2 = Tinkering_LayMine.CreateMine(gameObject, this.ParentObject);
			}
			if (Timed)
			{
				cell.PlayWorldSound("Sounds/Interact/sfx_interact_mine_setBomb", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			else
			{
				cell.PlayWorldSound("Sounds/Interact/sfx_interact_mine_lay", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (gameObject2.Understood())
			{
				base.DidXToY(Timed ? "set" : "lay", gameObject2, null, null, null, null, this.ParentObject, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
			}
			else
			{
				base.DidX("drop", "something", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			}
			cell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.ParentObject.UseEnergy(1000, "Skill Tinkering " + (Timed ? "Bomb" : "Mine") + " Deploy", null, null, false);
			return true;
		}

		// Token: 0x0600C2FB RID: 49915 RVA: 0x00473094 File Offset: 0x00471294
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandLayMine")
			{
				if (this.AttemptLayMine(false))
				{
					E.RequestInterfaceExit();
				}
			}
			else if (E.ID == "CommandLayMineTimed" && this.AttemptLayMine(true))
			{
				E.RequestInterfaceExit();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C2FC RID: 49916 RVA: 0x004730EC File Offset: 0x004712EC
		public override bool AddSkill(GameObject GO)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Lay Mine", "CommandLayMine", "Tinkering", null, "é", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.TimedActivatedAbilityID = base.AddMyActivatedAbility("Set Bomb", "CommandLayMineTimed", "Tinkering", null, "ë", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.AddSkill(GO);
		}

		// Token: 0x0600C2FD RID: 49917 RVA: 0x00473168 File Offset: 0x00471368
		public override bool RemoveSkill(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.TimedActivatedAbilityID, null);
			return base.RemoveSkill(GO);
		}

		// Token: 0x04004DAC RID: 19884
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04004DAD RID: 19885
		public Guid TimedActivatedAbilityID = Guid.Empty;
	}
}
