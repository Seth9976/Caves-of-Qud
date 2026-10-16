using System;
using Newtonsoft.Json;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001242 RID: 4674
	[Serializable]
	public class Stacker : IPart
	{
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600B9C0 RID: 47552 RVA: 0x0043D1AA File Offset: 0x0043B3AA
		[JsonIgnore]
		public int Number
		{
			get
			{
				if (this.StackCount <= 0)
				{
					this.StackCount = 1;
				}
				return this.StackCount;
			}
		}

		// Token: 0x0600B9C1 RID: 47553 RVA: 0x0043D1C4 File Offset: 0x0043B3C4
		public bool Check()
		{
			if (this.ParentObject == null)
			{
				return false;
			}
			GameObject inInventory = this.ParentObject.InInventory;
			if (inInventory != null && this.CheckInventoryForStacking(inInventory))
			{
				return true;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			return currentCell != null && this.CheckCellForStacking(currentCell);
		}

		// Token: 0x0600B9C2 RID: 47554 RVA: 0x0043D210 File Offset: 0x0043B410
		public GameObject RemoveOne()
		{
			if (this.StackCount <= 1)
			{
				return this.ParentObject;
			}
			int stackCount = this.StackCount;
			this.StackCount = stackCount - 1;
			if (base.HasTag("AlwaysStack"))
			{
				GameObject gameObject = GameObject.Create(this.ParentObject.Blueprint);
				Temporary.CarryOver(this.ParentObject, gameObject, false);
				Phase.carryOver(this.ParentObject, gameObject);
				return gameObject;
			}
			GameObject gameObject2 = this.ParentObject.DeepCopy(true, false, null);
			gameObject2.Stacker.StackCount = 1;
			gameObject2.WasUnstackedFrom(this.ParentObject);
			return gameObject2;
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x0600B9C3 RID: 47555 RVA: 0x0043D29C File Offset: 0x0043B49C
		// (set) Token: 0x0600B9C4 RID: 47556 RVA: 0x0043D2A4 File Offset: 0x0043B4A4
		public int StackCount
		{
			get
			{
				return this._StackCount;
			}
			set
			{
				if (value != this._StackCount)
				{
					int stackCount = this._StackCount;
					this._StackCount = value;
					StackCountChangedEvent.Send(this.ParentObject, stackCount, this._StackCount);
				}
			}
		}

		// Token: 0x0600B9C5 RID: 47557 RVA: 0x0043D2DA File Offset: 0x0043B4DA
		public override void Attach()
		{
			this.ParentObject.Stacker = this;
		}

		// Token: 0x0600B9C6 RID: 47558 RVA: 0x0043D2E8 File Offset: 0x0043B4E8
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Stacker : null) == this)
			{
				this.ParentObject.Stacker = null;
			}
		}

		// Token: 0x0600B9C7 RID: 47559 RVA: 0x0043D30B File Offset: 0x0043B50B
		public override IPart DeepCopy(GameObject Parent)
		{
			return new Stacker
			{
				StackCount = this.StackCount,
				ParentObject = Parent
			};
		}

		// Token: 0x0600B9C8 RID: 47560 RVA: 0x0043D325 File Offset: 0x0043B525
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write(this.StackCount);
		}

		// Token: 0x0600B9C9 RID: 47561 RVA: 0x0043D333 File Offset: 0x0043B533
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.StackCount = Reader.ReadInt32();
		}

		// Token: 0x0600B9CA RID: 47562 RVA: 0x0043D341 File Offset: 0x0043B541
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B9CB RID: 47563 RVA: 0x0043D344 File Offset: 0x0043B544
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID || ID == BeforeDestroyObjectEvent.ID || ID == EnteredCellEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x0600B9CC RID: 47564 RVA: 0x0043D37A File Offset: 0x0043B57A
		public override bool HandleEvent(EnteredCellEvent E)
		{
			return (E.NoStack || !this.CheckCellForStacking(null)) && base.HandleEvent(E);
		}

		// Token: 0x0600B9CD RID: 47565 RVA: 0x0043D396 File Offset: 0x0043B596
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			return (E.NoStack || !this.CheckInventoryForStacking(E.Actor)) && base.HandleEvent(E);
		}

		// Token: 0x0600B9CE RID: 47566 RVA: 0x0043D3B8 File Offset: 0x0043B5B8
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.Number > 1)
			{
				GameObject gameObject;
				if (this.ParentObject.HasTag("AlwaysStack"))
				{
					gameObject = GameObject.Create(this.ParentObject.Blueprint);
					gameObject.Stacker.StackCount = this.StackCount - 1;
					Temporary.CarryOver(this.ParentObject, gameObject, false);
					Phase.carryOver(this.ParentObject, gameObject);
					this.StackCount = 1;
				}
				else
				{
					gameObject = this.ParentObject.DeepCopy(true, false, null);
					gameObject.Stacker.StackCount = this.StackCount - 1;
					this.StackCount = 1;
					this.ParentObject.WasUnstackedFrom(gameObject);
				}
				E.Actor.ReceiveObject(gameObject, false, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9CF RID: 47567 RVA: 0x0043D474 File Offset: 0x0043B674
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Single && !this.ParentObject.HasTag("SingleDisplay"))
			{
				if (this.ParentObject.HasTag("SelfRenderStackerDisplay"))
				{
					Event @event = Event.New("SelfRenderStackerDisplay", 0, 0, 0);
					this.ParentObject.FireEvent(@event);
					string stringParameter = @event.GetStringParameter("Display", null);
					if (!string.IsNullOrEmpty(stringParameter))
					{
						E.AddTag(stringParameter, 60);
					}
				}
				else if (this.ParentObject.HasTag("HasCustomStackerDisplay"))
				{
					if (this.Number <= 1)
					{
						E.AddTag(this.ParentObject.GetTag("StackerSingularPrefix", null) + this.Number.ToString() + this.ParentObject.GetTag("StackerSingularPostfix", null), 60);
					}
					else
					{
						E.AddTag(this.ParentObject.GetTag("StackerPluralPrefix", null) + this.Number.ToString() + this.ParentObject.GetTag("StackerPluralPostfix", null), 60);
					}
				}
				else if (this.Number > 1)
				{
					E.AddClause("x" + this.Number.ToString(), 60);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9D0 RID: 47568 RVA: 0x0043D5C0 File Offset: 0x0043B7C0
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			if (!E.Obliterate && this.StackCount > 1)
			{
				int stackCount = this.StackCount;
				this.StackCount = stackCount - 1;
				GameObject parentObject = this.ParentObject;
				if (parentObject != null)
				{
					GameObject inInventory = parentObject.InInventory;
					if (inInventory != null)
					{
						Inventory inventory = inInventory.Inventory;
						if (inventory != null)
						{
							inventory.FlushWeightCache();
						}
					}
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B9D1 RID: 47569 RVA: 0x0043D61E File Offset: 0x0043B81E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B9D2 RID: 47570 RVA: 0x0043D624 File Offset: 0x0043B824
		public GameObject SplitStack(int Count, GameObject OwningObject = null, bool NoRemove = false)
		{
			if (this.Number <= 1)
			{
				return null;
			}
			int num = this.Number - Count;
			if (num <= 0)
			{
				return null;
			}
			GameObject gameObject;
			if (this.ParentObject.HasTag("AlwaysStack"))
			{
				gameObject = GameObject.Create(this.ParentObject.Blueprint);
				Temporary.CarryOver(this.ParentObject, gameObject, false);
				Phase.carryOver(this.ParentObject, gameObject);
			}
			else
			{
				gameObject = this.ParentObject.DeepCopy(true, false, null);
			}
			if (!GameObject.Validate(gameObject))
			{
				gameObject.Obliterate(null, true, null);
				return null;
			}
			gameObject.Stacker.StackCount = num;
			this.StackCount -= num;
			gameObject.WasUnstackedFrom(this.ParentObject);
			if (OwningObject == null)
			{
				OwningObject = this.ParentObject.InInventory ?? this.ParentObject.Equipped;
				if (this.ParentObject.HasTag("AlwaysStack") && OwningObject != null && !NoRemove)
				{
					OwningObject.FireEvent(Event.New("CommandRemoveObject", "Object", this.ParentObject).SetSilent(true));
				}
			}
			Inventory inventory = ((OwningObject != null) ? OwningObject.Inventory : null);
			if (inventory != null)
			{
				gameObject.Physics.InInventory = inventory.ParentObject;
				inventory.Objects.Add(gameObject);
			}
			else
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					gameObject.CurrentCell = currentCell;
					currentCell.Objects.Add(gameObject);
				}
			}
			return gameObject;
		}

		// Token: 0x0600B9D3 RID: 47571 RVA: 0x0043D780 File Offset: 0x0043B980
		private bool CheckInventoryForStacking(GameObject obj)
		{
			if (GameObject.Validate(this.ParentObject) && !this.ParentObject.HasPropertyOrTag("NeverStack"))
			{
				Inventory inventory = ((obj != null) ? obj.Inventory : null);
				if (inventory != null && AllowInventoryStackEvent.Check(obj, this.ParentObject))
				{
					for (int i = inventory.Objects.Count - 1; i >= 0; i--)
					{
						GameObject gameObject = inventory.Objects[i];
						if (gameObject != this.ParentObject && gameObject.HasPart<Stacker>() && !gameObject.HasPropertyOrTag("NeverStack"))
						{
							bool flag = gameObject.HasTag("AlwaysStack") && this.ParentObject.Blueprint == gameObject.Blueprint;
							if (flag && gameObject.IsTemporary != this.ParentObject.IsTemporary)
							{
								flag = false;
							}
							if (flag || gameObject.SameAs(this.ParentObject))
							{
								bool flag2 = !flag;
								if (!flag2)
								{
									if (!gameObject.IsTemporary)
									{
										flag2 = true;
									}
									else
									{
										Temporary part = gameObject.GetPart<Temporary>();
										Temporary part2 = this.ParentObject.GetPart<Temporary>();
										ExistenceSupport part3 = gameObject.GetPart<ExistenceSupport>();
										ExistenceSupport part4 = this.ParentObject.GetPart<ExistenceSupport>();
										flag2 = ((part == null && part2 == null) || (part != null && part2 != null && part.SameAs(part2))) && ((part3 == null && part4 == null) || (part3 != null && part4 != null && part3.SameAs(part4)));
									}
								}
								if (flag2)
								{
									gameObject.Stacker.StackCount += this.StackCount;
									this.ParentObject.Obliterate(null, true, null);
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600B9D4 RID: 47572 RVA: 0x0043D92C File Offset: 0x0043BB2C
		private bool CheckCellForStacking(Cell C = null)
		{
			if (GameObject.Validate(this.ParentObject) && !this.ParentObject.HasPropertyOrTag("NeverStack"))
			{
				if (C == null)
				{
					C = this.ParentObject.CurrentCell;
				}
				if (C != null)
				{
					Cell.ObjectRack objects = this.ParentObject.CurrentCell.Objects;
					for (int i = objects.Count - 1; i >= 0; i--)
					{
						GameObject gameObject = objects[i];
						Stacker stacker = gameObject.Stacker;
						if (stacker != null && gameObject != this.ParentObject && !gameObject.HasPropertyOrTag("NeverStack") && ((gameObject.HasTag("AlwaysStack") && this.ParentObject.Blueprint == gameObject.Blueprint) || gameObject.SameAs(this.ParentObject)) && gameObject.HasPart<Temporary>() == this.ParentObject.HasPart<Temporary>())
						{
							stacker.StackCount += this.StackCount;
							this.ParentObject.Obliterate(null, true, null);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x04004AB1 RID: 19121
		[NonSerialized]
		public int _StackCount = 1;
	}
}
