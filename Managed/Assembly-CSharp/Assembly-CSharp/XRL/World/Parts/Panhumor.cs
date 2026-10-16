using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E85 RID: 3717
	[Serializable]
	public class Panhumor : IPart
	{
		// Token: 0x060095D5 RID: 38357 RVA: 0x00363034 File Offset: 0x00361234
		public override bool SameAs(IPart p)
		{
			ElementalJelly elementalJelly = p as ElementalJelly;
			return elementalJelly.Level == this.Level && elementalJelly.MoveBoost == this.MoveBoost && !(elementalJelly.Form != this.Form) && !(elementalJelly.Blueprint != this.Blueprint) && base.SameAs(p);
		}

		// Token: 0x060095D6 RID: 38358 RVA: 0x00363099 File Offset: 0x00361299
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			Registrar.Register("ObjectCreated");
			Registrar.Register("BeforeDeathRemoval");
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x060095D7 RID: 38359 RVA: 0x003630D0 File Offset: 0x003612D0
		public void NewForm()
		{
			string form = this.Form;
			int num = Stat.Random(1, 4);
			if (num == 1)
			{
				this.Form = "Fire";
			}
			else if (num == 2)
			{
				this.Form = "Ice";
			}
			else if (num == 3)
			{
				this.Form = "Acid";
			}
			else if (num == 4)
			{
				this.Form = "Shocking";
			}
			if (this.Form == form)
			{
				return;
			}
			if (this.MoveBoost != 0)
			{
				this.ParentObject.Statistics["MoveSpeed"].Penalty -= this.MoveBoost;
				this.MoveBoost = 0;
			}
			this.MoveBoost = 5 * this.Level;
			this.ParentObject.Statistics["MoveSpeed"].Penalty += this.MoveBoost;
			List<string> list = new List<string> { "Fire", "Ice", "Acid", "Shocking" };
			list.ShuffleInPlace<string>(null);
			this.Form = list[0];
			this.CheckPodInit(ref this.Pod1);
			this.SetupPod(this.Pod1);
			this.Form = list[1];
			this.CheckPodInit(ref this.Pod2);
			this.SetupPod(this.Pod2);
			this.Form = list[2];
			this.CheckPodInit(ref this.Pod3);
			this.SetupPod(this.Pod3);
			this.Form = list[3];
			this.CheckPodInit(ref this.Pod4);
			this.SetupPod(this.Pod4);
		}

		// Token: 0x060095D8 RID: 38360 RVA: 0x00363276 File Offset: 0x00361476
		private void CheckPodInit(ref GameObject obj)
		{
			if (obj == null || obj.IsInvalid())
			{
				obj = GameObject.Create(this.Blueprint);
				obj.SetStringProperty("NeverStack", "1", false);
				this.ParentObject.ReceiveObject(obj, false, null);
			}
		}

		// Token: 0x060095D9 RID: 38361 RVA: 0x003632B4 File Offset: 0x003614B4
		private void SetupPod(GameObject obj)
		{
			obj.RemovePart<TemperatureOnHit>();
			obj.RemovePart<DischargeOnHit>();
			ElementalDamage part = obj.GetPart<ElementalDamage>();
			if (this.Form == "Fire")
			{
				obj.Render.DisplayName = "{{R|flaming pseudopod}}";
				if (part != null)
				{
					part.Attributes = "Fire";
				}
				obj.AddPart<TemperatureOnHit>(new TemperatureOnHit(this.Level.ToString() + "d20+" + (this.Level * 50).ToString(), 400), true, false);
				return;
			}
			if (this.Form == "Ice")
			{
				obj.Render.DisplayName = "{{C|hoary pseudopod}}";
				if (part != null)
				{
					part.Attributes = "Cold";
				}
				obj.AddPart<TemperatureOnHit>(new TemperatureOnHit("-" + this.Level.ToString() + "d20-" + (this.Level * 20).ToString(), 400), true, false);
				return;
			}
			if (this.Form == "Acid")
			{
				obj.Render.DisplayName = "{{G|acidic pseudopod}}";
				if (part != null)
				{
					part.Attributes = "Acid";
					return;
				}
			}
			else if (this.Form == "Shocking")
			{
				obj.Render.DisplayName = "{{W|sparking pseudopod}}";
				if (part != null)
				{
					part.Attributes = "Shock";
				}
				obj.AddPart<DischargeOnHit>(new DischargeOnHit("3d3", "1d6"), true, false);
			}
		}

		// Token: 0x060095DA RID: 38362 RVA: 0x00363428 File Offset: 0x00361628
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "BeginTakeAction"))
			{
				if (E.ID == "EnteredCell")
				{
					this.ParentObject.CurrentCell.AddObject("SaltyAcidPool", null, null, null, null);
				}
				else if (E.ID == "ObjectCreated")
				{
					this.NewForm();
				}
				else if (E.ID == "BeforeDeathRemoval")
				{
					if (this.ParentObject.Physics.CurrentCell == null || this.ParentObject.Physics.CurrentCell.ParentZone == null || !this.ParentObject.Physics.CurrentCell.ParentZone.IsActive())
					{
						return true;
					}
					bool flag = true;
					if (this.Form == "Shocking" || flag)
					{
						this.ParentObject.Discharge(this.ParentObject.CurrentCell.GetRandomLocalAdjacentCell() ?? this.ParentObject.CurrentCell, 20, 0, this.Level.ToString() + "d20", null, this.ParentObject, this.ParentObject, null, null, null, null, null, 0, false, false, false);
					}
					if (this.Form == "Fire" || flag)
					{
						base.DidX("explode", null, "!", null, null, null, IComponent<GameObject>.ThePlayer, false, false, null, null, false, false, false, false, false, null);
						Physics physics = this.ParentObject.Physics;
						if (physics != null && physics.CurrentCell != null)
						{
							List<Cell> adjacentCells = physics.CurrentCell.GetAdjacentCells(true);
							adjacentCells.Add(physics.CurrentCell);
							int num = 1000 * this.Level;
							TextConsole.LoadScrapBuffers();
							TextConsole textConsole = Look._TextConsole;
							ScreenBuffer scrapBuffer = TextConsole.ScrapBuffer;
							XRLCore.Core.RenderMapToBuffer(scrapBuffer);
							GameObject gameObjectParameter = E.GetGameObjectParameter("Owner");
							int phase = this.ParentObject.GetPhase();
							foreach (Cell cell in adjacentCells)
							{
								cell.TemperatureChange(num, gameObjectParameter, false, false, false, false, phase);
								if (num > 0)
								{
									scrapBuffer.Goto(cell.X, cell.Y);
									int num2 = Stat.Random(1, 3);
									if (num2 == 1)
									{
										scrapBuffer.Write("&R*", true, false, false, null, -1);
									}
									else if (num2 == 2)
									{
										scrapBuffer.Write("&W*", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write("&r*", true, false, false, null, -1);
									}
								}
								else
								{
									scrapBuffer.Goto(cell.X, cell.Y);
									int num3 = Stat.Random(1, 3);
									if (num3 == 1)
									{
										scrapBuffer.Write("&C*", true, false, false, null, -1);
									}
									else if (num3 == 2)
									{
										scrapBuffer.Write("&Y*", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer.Write("&c*", true, false, false, null, -1);
									}
								}
								if (cell.GetObjectCount() > 0)
								{
									GameObject objectInCell = cell.GetObjectInCell(0);
									if (num > 0)
									{
										for (int i = 0; i < 5; i++)
										{
											objectInCell.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int j = 0; j < 5; j++)
										{
											objectInCell.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int k = 0; k < 5; k++)
										{
											objectInCell.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
									}
									else
									{
										for (int l = 0; l < 5; l++)
										{
											objectInCell.ParticleText("&Y" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int m = 0; m < 5; m++)
										{
											objectInCell.ParticleText("&C" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int n = 0; n < 5; n++)
										{
											objectInCell.ParticleText("&c" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
									}
								}
							}
							textConsole.DrawBuffer(scrapBuffer, null, false);
						}
					}
					if (this.Form == "Ice" || flag)
					{
						base.DidX("explode", null, "!", null, null, null, IComponent<GameObject>.ThePlayer, false, false, null, null, false, false, false, false, false, null);
						Physics physics2 = this.ParentObject.Physics;
						if (physics2 != null && physics2.CurrentCell != null)
						{
							List<Cell> adjacentCells2 = physics2.CurrentCell.GetAdjacentCells(true);
							adjacentCells2.Add(physics2.CurrentCell);
							int num4 = -1000 * this.Level;
							TextConsole.LoadScrapBuffers();
							TextConsole textConsole2 = Look._TextConsole;
							ScreenBuffer scrapBuffer2 = TextConsole.ScrapBuffer;
							XRLCore.Core.RenderMapToBuffer(scrapBuffer2);
							GameObject gameObjectParameter2 = E.GetGameObjectParameter("Owner");
							int phase2 = this.ParentObject.GetPhase();
							foreach (Cell cell2 in adjacentCells2)
							{
								cell2.TemperatureChange(num4, gameObjectParameter2, false, false, false, false, phase2);
								if (num4 > 0)
								{
									scrapBuffer2.Goto(cell2.X, cell2.Y);
									int num5 = Stat.Random(1, 3);
									if (num5 == 1)
									{
										scrapBuffer2.Write("&R*", true, false, false, null, -1);
									}
									else if (num5 == 2)
									{
										scrapBuffer2.Write("&W*", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer2.Write("&r*", true, false, false, null, -1);
									}
								}
								else
								{
									scrapBuffer2.Goto(cell2.X, cell2.Y);
									int num6 = Stat.Random(1, 3);
									if (num6 == 1)
									{
										scrapBuffer2.Write("&C*", true, false, false, null, -1);
									}
									else if (num6 == 2)
									{
										scrapBuffer2.Write("&Y*", true, false, false, null, -1);
									}
									else
									{
										scrapBuffer2.Write("&c*", true, false, false, null, -1);
									}
								}
								if (cell2.GetObjectCount() > 0)
								{
									GameObject objectInCell2 = cell2.GetObjectInCell(0);
									if (num4 > 0)
									{
										for (int num7 = 0; num7 < 5; num7++)
										{
											objectInCell2.ParticleText("&r" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int num8 = 0; num8 < 5; num8++)
										{
											objectInCell2.ParticleText("&R" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int num9 = 0; num9 < 5; num9++)
										{
											objectInCell2.ParticleText("&W" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
									}
									else
									{
										for (int num10 = 0; num10 < 5; num10++)
										{
											objectInCell2.ParticleText("&Y" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int num11 = 0; num11 < 5; num11++)
										{
											objectInCell2.ParticleText("&C" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
										for (int num12 = 0; num12 < 5; num12++)
										{
											objectInCell2.ParticleText("&c" + ((char)(219 + Stat.Random(0, 4))).ToString(), 4.9f, 5);
										}
									}
								}
							}
							textConsole2.DrawBuffer(scrapBuffer2, null, false);
						}
					}
					if ((this.Form == "Acid" || flag) && this.ParentObject.Physics.CurrentCell != null)
					{
						List<Cell> adjacentCells3 = this.ParentObject.Physics.CurrentCell.GetAdjacentCells(true);
						adjacentCells3.Add(this.ParentObject.Physics.CurrentCell);
						foreach (Cell cell3 in adjacentCells3)
						{
							if (!cell3.IsOccluding() && Stat.Random(0, 100) <= 75)
							{
								cell3.AddObject(GameObjectFactory.Factory.CreateObject("SaltyAcidPool"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DF1 RID: 15857
		public int Level = 1;

		// Token: 0x04003DF2 RID: 15858
		public int MoveBoost;

		// Token: 0x04003DF3 RID: 15859
		public string Form;

		// Token: 0x04003DF4 RID: 15860
		public string Blueprint;

		// Token: 0x04003DF5 RID: 15861
		public GameObject Pod1;

		// Token: 0x04003DF6 RID: 15862
		public GameObject Pod2;

		// Token: 0x04003DF7 RID: 15863
		public GameObject Pod3;

		// Token: 0x04003DF8 RID: 15864
		public GameObject Pod4;
	}
}
