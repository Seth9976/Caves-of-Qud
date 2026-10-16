using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200141A RID: 5146
	[Serializable]
	public abstract class IDelayedLineMutation : BaseMutation
	{
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600C809 RID: 51209 RVA: 0x0048CFF2 File Offset: 0x0048B1F2
		// (set) Token: 0x0600C80A RID: 51210 RVA: 0x0048CFFA File Offset: 0x0048B1FA
		public virtual int Delay { get; set; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600C80B RID: 51211 RVA: 0x0048D003 File Offset: 0x0048B203
		// (set) Token: 0x0600C80C RID: 51212 RVA: 0x0048D00B File Offset: 0x0048B20B
		public virtual Cell Target { get; set; }

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600C80D RID: 51213
		public abstract GameObject Projectile { get; }

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x0600C80E RID: 51214
		public abstract string Command { get; }

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600C80F RID: 51215 RVA: 0x0048D014 File Offset: 0x0048B214
		public virtual string Label
		{
			get
			{
				return base.GetDisplayName(true);
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600C810 RID: 51216 RVA: 0x0048D01D File Offset: 0x0048B21D
		public virtual int PaintDuration
		{
			get
			{
				return 500;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600C811 RID: 51217 RVA: 0x0048D024 File Offset: 0x0048B224
		public virtual bool CanRefract
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600C812 RID: 51218 RVA: 0x0048D027 File Offset: 0x0048B227
		public bool IsGazeBased
		{
			get
			{
				GameObject projectile = this.Projectile;
				TreatAsSolid treatAsSolid = ((projectile != null) ? projectile.GetPart<TreatAsSolid>() : null);
				return treatAsSolid != null && treatAsSolid.GazeBased;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600C813 RID: 51219 RVA: 0x0048D048 File Offset: 0x0048B248
		public List<Cell> TargetLine
		{
			get
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				bool isGazeBased = this.IsGazeBased;
				if (this._TargetLine == null)
				{
					if (currentCell == null || this.Target == null)
					{
						return null;
					}
					if (isGazeBased && currentCell.IsBlackedOut())
					{
						return null;
					}
					List<Point> list = Zone.Line(currentCell.X, currentCell.Y, this.Target.X, this.Target.Y, false);
					if (list.Count <= 1)
					{
						List<Cell> list2 = new List<Cell>();
						list2.Add(currentCell);
						List<Cell> list3 = list2;
						this._TargetLine = list2;
						return list3;
					}
					list.RemoveAt(0);
					this._TargetLine = new List<Cell>(this.GetRange(base.Level) + 1) { currentCell };
					int num = 0;
					int num2 = currentCell.X;
					int num3 = currentCell.Y;
					int range = this.GetRange(base.Level);
					while (this._TargetLine.Count <= range)
					{
						int num4 = num % list.Count;
						num2 += list[num4].X - ((num4 > 0) ? list[num4 - 1].X : currentCell.X);
						num3 += list[num4].Y - ((num4 > 0) ? list[num4 - 1].Y : currentCell.Y);
						Cell cell = currentCell.ParentZone.GetCell(num2, num3);
						if (cell == null || !this.ParentObject.HasLOSTo(cell, true, false, false, true, null) || (isGazeBased && cell.IsBlackedOut()))
						{
							break;
						}
						if (cell != this._TargetLine.Last<Cell>())
						{
							this._TargetLine.Add(cell);
						}
						num++;
					}
				}
				else if (this._TargetLine.Count == 0 || this._TargetLine[0] != currentCell)
				{
					this.Delay = 0;
					this.Target = null;
					this._TargetLine = null;
				}
				else if (isGazeBased)
				{
					int i = 0;
					int count = this._TargetLine.Count;
					while (i < count)
					{
						if (this._TargetLine[i].IsBlackedOut())
						{
							do
							{
								this._TargetLine.RemoveAt(i);
								if (this._TargetLine.Count <= i)
								{
									break;
								}
							}
							while (this._TargetLine[i] != null);
							break;
						}
						i++;
					}
				}
				return this._TargetLine;
			}
		}

		// Token: 0x0600C814 RID: 51220
		public abstract int GetRange(int Level);

		// Token: 0x0600C815 RID: 51221
		public abstract int GetDelay(int Level);

		// Token: 0x0600C816 RID: 51222
		public abstract int GetCooldown(int Level);

		// Token: 0x0600C817 RID: 51223
		public abstract void FireLine(List<Cell> Path);

		// Token: 0x0600C818 RID: 51224 RVA: 0x0048D2A0 File Offset: 0x0048B4A0
		public void Refract(List<Cell> Path)
		{
			Event @event = null;
			for (int i = 1; i < Path.Count; i++)
			{
				Cell cell = Path[i];
				if (cell.HasObjectWithRegisteredEvent("RefractLight"))
				{
					if (@event == null)
					{
						Cell cell2 = Path[0];
						Cell cell3 = Path[Path.Count - 1];
						@event = Event.New("RefractLight", 0, 0, 0);
						@event.SetParameter("Projectile", this.Projectile);
						@event.SetParameter("Attacker", this.ParentObject);
						@event.SetParameter("Angle", (float)Math.Atan2((double)(cell3.X - cell2.X), (double)(cell3.Y - cell2.Y)).toDegrees());
						@event.SetParameter("Sound", "sfx_light_refract");
					}
					@event.SetParameter("Cell", cell);
					@event.SetParameter("Direction", Stat.Random(0, 359));
					@event.SetParameter("Verb", null);
					@event.SetParameter("By", null);
					if (!cell.FireEvent(@event))
					{
						GameObject gameObjectParameter = @event.GetGameObjectParameter("By");
						if (GameObject.Validate(ref gameObjectParameter))
						{
							Path.RemoveRange(i, Path.Count - i);
							base.PlayWorldSound(@event.GetStringParameter("Sound", null), 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
							IComponent<GameObject>.XDidYToZ(gameObjectParameter, @event.GetStringParameter("Verb", null) ?? "refract", this.Projectile, "!", null, null, null, gameObjectParameter, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
							int num = @event.GetIntParameter("Direction", 0).normalizeDegrees();
							float num2 = (float)cell.X;
							float num3 = (float)cell.Y;
							float num4 = Mathf.Sin((float)num * 0.017453292f);
							float num5 = Mathf.Cos((float)num * 0.017453292f);
							Cell cell4 = cell;
							do
							{
								num2 += num4;
								num3 += num5;
								Cell cell5 = cell.ParentZone.GetCell((int)num2, (int)num3);
								if (cell5 == null)
								{
									break;
								}
								if (cell5 != cell4)
								{
									Path.Add(cell4 = cell5);
									if (cell5.GetCombatTarget(this.ParentObject, true, false, false, 0, this.Projectile, null, null, null, null, true, true, null) != null || cell5.HasSolidObjectForMissile(this.ParentObject, null, this.Projectile, false, false))
									{
										break;
									}
								}
							}
							while (num2 > 0f && num2 < 79f && num3 > 0f && num3 < 24f && Path.Count < 400);
						}
					}
				}
			}
		}

		// Token: 0x0600C819 RID: 51225 RVA: 0x0048D540 File Offset: 0x0048B740
		public void Perform()
		{
			List<Cell> targetLine = this.TargetLine;
			this.Delay = 0;
			this.Target = null;
			this._TargetLine = null;
			if (targetLine.IsNullOrEmpty<Cell>())
			{
				return;
			}
			if (this.CanRefract)
			{
				this.Refract(targetLine);
			}
			this.FireLine(targetLine);
			base.UseEnergy(1000);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(base.Level), null, null, false);
		}

		// Token: 0x0600C81A RID: 51226 RVA: 0x0048D5B0 File Offset: 0x0048B7B0
		public virtual void AlertTargetLine()
		{
			if (this.Target == null)
			{
				return;
			}
			List<Cell> targetLine = this.TargetLine;
			if (targetLine.IsNullOrEmpty<Cell>())
			{
				return;
			}
			int i = 0;
			int count = targetLine.Count;
			while (i < count)
			{
				int j = 0;
				int count2 = targetLine[i].Objects.Count;
				while (j < count2)
				{
					GameObject gameObject = targetLine[i].Objects[j];
					if (gameObject.IsPlayer())
					{
						AutoAct.Interrupt("you are in the path of " + this.ParentObject.poss(this.Projectile.DisplayNameOnlyStripped, true), targetLine[i], null, true);
					}
					else if (gameObject.IsPotentiallyMobile())
					{
						gameObject.Brain.PushGoal(new FleeLocation(targetLine[i], 2, false, false));
					}
					j++;
				}
				i++;
			}
		}

		// Token: 0x0600C81B RID: 51227 RVA: 0x0048D684 File Offset: 0x0048B884
		public override void OnPaint(ScreenBuffer SB)
		{
			if (this.Target == null)
			{
				return;
			}
			List<Cell> targetLine = this.TargetLine;
			if (targetLine.IsNullOrEmpty<Cell>())
			{
				return;
			}
			int num = (int)(IComponent<GameObject>.frameTimerMS % (long)this.PaintDuration / (long)(this.PaintDuration / targetLine.Count));
			for (int i = 0; i < targetLine.Count; i++)
			{
				Cell cell = targetLine[i];
				if (cell.IsVisible() && cell != this.ParentObject.CurrentCell && cell.ParentZone == this.ParentObject.CurrentZone)
				{
					ConsoleChar consoleChar = SB[cell];
					if (i == num)
					{
						consoleChar.TileBackground = (consoleChar.Background = The.Color.DarkRed);
						consoleChar.TileForeground = (consoleChar.Detail = The.Color.Red);
					}
					else
					{
						consoleChar.TileBackground = (consoleChar.Background = The.Color.Red);
						consoleChar.TileForeground = (consoleChar.Detail = The.Color.DarkRed);
					}
					consoleChar.SetForeground('r');
				}
			}
		}

		// Token: 0x0600C81C RID: 51228 RVA: 0x0048D7A5 File Offset: 0x0048B9A5
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			Writer.WriteOptimized(this.Delay);
			Writer.Write(this.Target);
		}

		// Token: 0x0600C81D RID: 51229 RVA: 0x0048D7C7 File Offset: 0x0048B9C7
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			this.Delay = Reader.ReadOptimizedInt32();
			this.Target = Reader.ReadCell();
		}

		// Token: 0x0600C81E RID: 51230 RVA: 0x0048D7E9 File Offset: 0x0048B9E9
		public override bool FinalRender(RenderEvent E)
		{
			E.WantsToPaint = this.Target != null;
			return true;
		}

		// Token: 0x0600C81F RID: 51231 RVA: 0x0048D7FC File Offset: 0x0048B9FC
		public override bool Render(RenderEvent E)
		{
			if (this.Target != null)
			{
				int num = 500;
				if (IComponent<GameObject>.frameTimerMS % (long)num < (long)(num / 2))
				{
					E.ColorString = ((E.BackgroundString == "&r") ? "&Y" : "&r");
					E.BackgroundString = ((E.BackgroundString == "^R") ? "^Y" : "^R");
				}
			}
			return true;
		}

		// Token: 0x0600C820 RID: 51232 RVA: 0x0048D86E File Offset: 0x0048BA6E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C821 RID: 51233 RVA: 0x0048D894 File Offset: 0x0048BA94
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (this.Target == null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Distance <= this.GetRange(base.Level) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, this.IsGazeBased, false, true, null))
			{
				E.Add(this.Command, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C822 RID: 51234 RVA: 0x0048D910 File Offset: 0x0048BB10
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Delay > 0)
			{
				int num = this.Delay - 1;
				this.Delay = num;
				if (num <= 0)
				{
					this.Perform();
				}
				else
				{
					if (this.ParentObject.IsPlayer())
					{
						The.Core.RenderDelay(500, true, true, false);
					}
					base.UseEnergy(1000);
					this.AlertTargetLine();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C823 RID: 51235 RVA: 0x0048D97C File Offset: 0x0048BB7C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == this.Command && this.PickTarget())
			{
				base.UseEnergy(1000);
				this.AlertTargetLine();
				base.PlayWorldSound("sfx_ability_longBeam_attack_chargeUp", 1f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				base.DidXToY("focus", this.Projectile, null, null, null, null, this.ParentObject, null, false, false, false, false, false, null, this.ParentObject, null, true, false, this.Target == The.Player.CurrentCell, false, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C824 RID: 51236 RVA: 0x0048DA2A File Offset: 0x0048BC2A
		public virtual bool IsValidTarget(GameObject Object)
		{
			return Object != null && Object != this.ParentObject && Object.IsCombatObject();
		}

		// Token: 0x0600C825 RID: 51237 RVA: 0x0048DA40 File Offset: 0x0048BC40
		public virtual bool PickTarget()
		{
			if (this.Target != null)
			{
				return false;
			}
			int level = base.Level;
			List<Cell> list = base.PickLine(this.GetRange(level), AllowVis.OnlyVisible, new Predicate<GameObject>(this.IsValidTarget), false, false, true, false, this.ParentObject, this.Projectile, this.Label, false, true);
			if (list.IsNullOrEmpty<Cell>())
			{
				return false;
			}
			Cell cell = list.Last<Cell>();
			this.Target = cell;
			this.Delay = this.GetDelay(level);
			return true;
		}

		// Token: 0x0600C826 RID: 51238 RVA: 0x0048DAB8 File Offset: 0x0048BCB8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C827 RID: 51239 RVA: 0x0048DABC File Offset: 0x0048BCBC
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility(this.Label, this.Command, "Physical Mutations", null, "è", null, false, false, false, true, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C828 RID: 51240 RVA: 0x0048DB07 File Offset: 0x0048BD07
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C829 RID: 51241 RVA: 0x0048DB1E File Offset: 0x0048BD1E
		public static List<Cell> GetLine(Cell Origin, Cell Target, int Range = -1, GameObject Looker = null, bool IsGazeBased = false)
		{
			List<Cell> list = new List<Cell>();
			IDelayedLineMutation.FillLine(list, Origin, Target, Range, Looker, IsGazeBased);
			return list;
		}

		// Token: 0x0600C82A RID: 51242 RVA: 0x0048DB34 File Offset: 0x0048BD34
		public static void FillLine(List<Cell> Line, Cell Origin, Cell Target, int Range = -1, GameObject Looker = null, bool IsGazeBased = false)
		{
			List<Point> list = Zone.Line(Origin.X, Origin.Y, Target.X, Target.Y, true);
			int num = list.Count - 1;
			if (num <= 0)
			{
				Line.Add(Origin);
				return;
			}
			list.RemoveAt(0);
			if (Range == -1)
			{
				Range = num;
			}
			Cell cell = Origin;
			Line.EnsureCapacity<Cell>(Math.Clamp(Range, 1, 16));
			Line.Add(Origin);
			int num2 = 0;
			int num3 = Origin.X;
			int num4 = Origin.Y;
			while (Line.Count <= Range)
			{
				int num5 = num2 % num;
				num3 += list[num5].X - ((num5 > 0) ? list[num5 - 1].X : Origin.X);
				num4 += list[num5].Y - ((num5 > 0) ? list[num5 - 1].Y : Origin.Y);
				Cell cell2 = Origin.ParentZone.GetCell(num3, num4);
				if (cell2 == null || (Looker != null && !Looker.HasLOSTo(cell2, true, false, false, true, null)) || (IsGazeBased && cell2.IsBlackedOut()))
				{
					break;
				}
				if (cell2 != cell)
				{
					Line.Add(cell2);
					cell = cell2;
				}
				num2++;
			}
		}

		// Token: 0x04004E71 RID: 20081
		[NonSerialized]
		public List<Cell> _TargetLine;
	}
}
