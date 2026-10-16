using System;
using System.Collections.Generic;
using ConsoleLib.Console;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, the item's effective pull force is increased
	/// by a percentage equal to ((power load - 100) / 10), i.e. 30% for
	/// the standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001117 RID: 4375
	[Serializable]
	public class GrapplingGun : IPoweredPart
	{
		// Token: 0x0600AD3E RID: 44350 RVA: 0x003F9152 File Offset: 0x003F7352
		public GrapplingGun()
		{
			this.ChargeUse = 20;
			this.WorksOnSelf = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600AD3F RID: 44351 RVA: 0x003F9184 File Offset: 0x003F7384
		public override bool SameAs(IPart p)
		{
			GrapplingGun grapplingGun = p as GrapplingGun;
			return grapplingGun.Force == this.Force && grapplingGun.MaxPenetrations == this.MaxPenetrations && !(grapplingGun.Color != this.Color) && base.SameAs(p);
		}

		// Token: 0x0600AD40 RID: 44352 RVA: 0x003F91D4 File Offset: 0x003F73D4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNamePenetrationColorEvent>.ID || ID == PooledEvent<MissilePenetrateEvent>.ID;
		}

		// Token: 0x0600AD41 RID: 44353 RVA: 0x003F91F2 File Offset: 0x003F73F2
		public override bool HandleEvent(GetDisplayNamePenetrationColorEvent E)
		{
			if (this.MaxPenetrations <= 1)
			{
				E.Color = "K";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD42 RID: 44354 RVA: 0x003F9210 File Offset: 0x003F7410
		public override bool HandleEvent(MissilePenetrateEvent E)
		{
			if (E.Penetrations <= 0)
			{
				string text = ", but";
				GameObject projectile = E.Projectile;
				E.OutcomeMessageFragment = text + (((projectile != null) ? projectile.GetVerb("manage", true, false, false, true, false) : null) ?? "manages") + " to latch on anyway";
			}
			else if (this.MaxPenetrations > 0 && E.Penetrations > this.MaxPenetrations)
			{
				E.Penetrations = this.MaxPenetrations;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD43 RID: 44355 RVA: 0x003F928C File Offset: 0x003F748C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LauncherProjectileHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD44 RID: 44356 RVA: 0x003F92A1 File Offset: 0x003F74A1
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LauncherProjectileHit")
			{
				this.PerformGrapple(E.GetGameObjectParameter("Attacker"), E.GetGameObjectParameter("Defender"));
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD45 RID: 44357 RVA: 0x003F92DC File Offset: 0x003F74DC
		public bool PerformGrapple(GameObject Actor, GameObject Target)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			int num3 = this.MyPowerLoadLevel();
			int num4 = this.Force;
			int num5 = IComponent<GameObject>.PowerLoadBonus(num3, 100, 10);
			if (num5 != 0)
			{
				num4 = num4 * (100 + num5) / 100;
			}
			Actor.PlayWorldSound("Sounds/Missile/Special/sfx_missile_grapplingGun_pull", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			int num6 = 0;
			int num7 = 0;
			while (GameObject.Validate(ref Actor) && GameObject.Validate(ref Target) && Actor != Target && !Target.InSameOrAdjacentCellTo(Actor) && ++num6 < 100)
			{
				The.Core.RenderBaseToBuffer(scrapBuffer);
				List<Tuple<Cell, char>> lineTo = Actor.GetLineTo(Target, true, false);
				string text = null;
				int i = 1;
				int num8 = lineTo.Count - 2;
				while (i <= num8)
				{
					int x = lineTo[i].Item1.X;
					int y = lineTo[i].Item1.Y;
					int x2 = lineTo[i - 1].Item1.X;
					int y2 = lineTo[i - 1].Item1.Y;
					int x3 = lineTo[i + 1].Item1.X;
					int y3 = lineTo[i + 1].Item1.Y;
					if (y == y3 && y == y2)
					{
						text = "-";
					}
					else if (x == x3 && x == x2)
					{
						text = "|";
					}
					else if ((x == x3 && x != x2 && y != y3 && y == x2) || (x != x3 && x == x2 && y == y3 && y != x2))
					{
						text = null;
					}
					else if (y3 > y2)
					{
						text = ((x3 > x2) ? "\\" : "/");
					}
					else if (y3 < y2)
					{
						text = ((x3 > x2) ? "/" : "\\");
					}
					if (!text.IsNullOrEmpty())
					{
						if (!this.Color.IsNullOrEmpty())
						{
							text = string.Concat(new string[] { "{{", this.Color, "|", text, "}}" });
						}
						scrapBuffer.WriteAt(x, y, text, true);
					}
					i++;
				}
				scrapBuffer.Draw();
				The.ParticleManager.Frame();
				int kineticResistance = Actor.GetKineticResistance();
				int num9 = (Target.CanBeInvoluntarilyMoved() ? Target.GetKineticResistance() : int.MaxValue);
				int num10 = kineticResistance - num;
				int num11 = num9 - num2;
				if (num10 > num4 && num11 > num4)
				{
					break;
				}
				bool flag3 = true;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				bool flag11 = false;
				bool flag12 = false;
				int num12 = 1;
				int? num13 = new int?(num3);
				if (!base.IsReady(flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, num12, null, false, 0L, num13))
				{
					break;
				}
				int num14 = Math.Min(Math.Min(num10, num11), num4);
				num = Math.Min(num + num14, num4);
				num2 = Math.Min(num2 + num14, num4);
				int num15 = num - kineticResistance;
				int num16 = num2 - num9;
				Cell currentCell = Actor.CurrentCell;
				Cell currentCell2 = Target.CurrentCell;
				if (num15 > num16)
				{
					string generalDirectionFromCell = currentCell.GetGeneralDirectionFromCell(currentCell2);
					Cell cellFromDirection = currentCell.GetCellFromDirection(generalDirectionFromCell, true);
					if (cellFromDirection == null || cellFromDirection == currentCell)
					{
						break;
					}
					int num17 = currentCell.DistanceTo(currentCell2);
					GameObject gameObject = Actor;
					string text2 = generalDirectionFromCell;
					bool flag13 = true;
					bool flag14 = false;
					bool flag15 = true;
					bool flag16 = false;
					bool flag17 = false;
					bool flag18 = false;
					GameObject parentObject = this.ParentObject;
					num13 = new int?(0);
					if (!gameObject.Move(text2, flag13, flag14, flag15, flag16, flag17, flag18, parentObject, Actor, true, num13, "Grapple", null, true, false, null, null, 0))
					{
						break;
					}
					flag = true;
					num -= kineticResistance;
					if (Actor.DistanceTo(currentCell2) >= num17 && ++num7 >= 3)
					{
						break;
					}
				}
				else
				{
					string generalDirectionFromCell2 = currentCell2.GetGeneralDirectionFromCell(currentCell);
					Cell cellFromDirection2 = currentCell2.GetCellFromDirection(generalDirectionFromCell2, true);
					if (cellFromDirection2 == null || cellFromDirection2 == currentCell2)
					{
						break;
					}
					int num18 = currentCell.DistanceTo(currentCell2);
					GameObject gameObject2 = Target;
					string text3 = generalDirectionFromCell2;
					bool flag19 = true;
					bool flag20 = false;
					bool flag21 = true;
					bool flag22 = false;
					bool flag23 = false;
					bool flag24 = false;
					GameObject parentObject2 = this.ParentObject;
					num13 = new int?(0);
					if (!gameObject2.Move(text3, flag19, flag20, flag21, flag22, flag23, flag24, parentObject2, Actor, true, num13, "Grapple", null, true, false, null, null, 0))
					{
						break;
					}
					flag2 = true;
					num2 -= num9;
					if (currentCell.DistanceTo(Target) >= num18 && ++num7 >= 3)
					{
						break;
					}
				}
				if (currentCell.DistanceTo(Actor.CurrentCell) > 1 || currentCell2.DistanceTo(Target.CurrentCell) > 1)
				{
					break;
				}
			}
			if (flag && GameObject.Validate(ref Actor))
			{
				Actor.Gravitate();
			}
			if (flag2 && GameObject.Validate(ref Target))
			{
				Target.Gravitate();
			}
			return flag || flag2;
		}

		// Token: 0x04004783 RID: 18307
		public int Force = 4500;

		// Token: 0x04004784 RID: 18308
		public int MaxPenetrations = 1;

		// Token: 0x04004785 RID: 18309
		public string Color;
	}
}
