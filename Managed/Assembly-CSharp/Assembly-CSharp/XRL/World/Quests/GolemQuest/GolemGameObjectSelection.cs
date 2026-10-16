using System;
using System.Collections.Generic;
using ConsoleLib.Console;

namespace XRL.World.Quests.GolemQuest
{
	// Token: 0x02000B2E RID: 2862
	[Serializable]
	public abstract class GolemGameObjectSelection : GolemMaterialSelection<GameObject, string>
	{
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x0600731F RID: 29471 RVA: 0x002B8143 File Offset: 0x002B6343
		public virtual bool IsCarried
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06007320 RID: 29472 RVA: 0x002B8146 File Offset: 0x002B6346
		public virtual int Consumed
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06007321 RID: 29473 RVA: 0x002B814C File Offset: 0x002B634C
		// (set) Token: 0x06007322 RID: 29474 RVA: 0x002B81A4 File Offset: 0x002B63A4
		public override GameObject Material
		{
			get
			{
				if (this._Material == null && !this.Failed && !this.ObjectID.IsNullOrEmpty())
				{
					this._Material = The.ZoneManager.FindObjectByID(this.ObjectID);
					this.Failed = this._Material == null;
				}
				return this._Material;
			}
			set
			{
				this._Material = value;
				this.ObjectID = ((value != null) ? value.ID : null);
				this.ObjectDisplayName = ((value != null) ? this.GetNameFor(value) : null);
				this.ObjectIcon = ((value != null) ? new Renderable(value.Render) : null);
				this.Failed = false;
			}
		}

		// Token: 0x06007323 RID: 29475 RVA: 0x002B81FC File Offset: 0x002B63FC
		public override List<GameObject> GetValidMaterials()
		{
			this._ValidMaterials.Clear();
			if (this.IsCarried)
			{
				Predicate<GameObject> predicate = new Predicate<GameObject>(this.IsValid);
				using (List<GameObject>.Enumerator enumerator = base.GetValidHolders().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GameObject gameObject = enumerator.Current;
						gameObject.Inventory.GetObjects(this._ValidMaterials, predicate);
					}
					goto IL_009E;
				}
			}
			foreach (GameObject gameObject2 in The.ActiveZone.IterateObjects())
			{
				if (this.IsValid(gameObject2))
				{
					this._ValidMaterials.Add(gameObject2);
				}
			}
			IL_009E:
			return this._ValidMaterials;
		}

		// Token: 0x06007324 RID: 29476 RVA: 0x002B82C0 File Offset: 0x002B64C0
		public override string GetNameFor(GameObject Material)
		{
			return Material.DisplayName;
		}

		// Token: 0x06007325 RID: 29477 RVA: 0x002B82C8 File Offset: 0x002B64C8
		public override IRenderable GetIconFor(GameObject Material)
		{
			return Material.RenderForUI(null, false);
		}

		// Token: 0x06007326 RID: 29478 RVA: 0x002B82D4 File Offset: 0x002B64D4
		public override void Apply(GameObject Object)
		{
			base.Apply(Object);
			if (GameObject.Validate(this.Material))
			{
				Object.SetStringProperty(this.ID + "Blueprint", this.Material.Blueprint, false);
				Object.SetStringProperty(this.ID + "DisplayName", this.Material.Render.DisplayName, false);
				if (!this.Material.HasProperName)
				{
					Object.SetStringProperty(this.ID + "IndefiniteArticle", this.Material.a, false);
				}
			}
			if (this.Consumed > 0 && GameObject.Validate(this.Material))
			{
				this.Material.SplitStack(this.Consumed, null, false);
				this.Material.Obliterate(null, false, null);
			}
		}

		// Token: 0x06007327 RID: 29479 RVA: 0x002B83A8 File Offset: 0x002B65A8
		public GolemGameObjectSelection GetFirstConflict()
		{
			if (this.Material == null)
			{
				return null;
			}
			if (this.Consumed <= 0)
			{
				return null;
			}
			GolemQuestSystem system = base.System;
			Dictionary<string, GolemQuestSelection> dictionary = ((system != null) ? system.Selections : null);
			if (dictionary.IsNullOrEmpty<KeyValuePair<string, GolemQuestSelection>>())
			{
				return null;
			}
			foreach (KeyValuePair<string, GolemQuestSelection> keyValuePair in dictionary)
			{
				GolemGameObjectSelection golemGameObjectSelection = keyValuePair.Value as GolemGameObjectSelection;
				if (golemGameObjectSelection != null && golemGameObjectSelection != this && golemGameObjectSelection.Consumed > 0 && golemGameObjectSelection.Material == this.Material)
				{
					return golemGameObjectSelection;
				}
			}
			return null;
		}

		// Token: 0x06007328 RID: 29480 RVA: 0x002B8458 File Offset: 0x002B6658
		public override bool IsValid()
		{
			return base.IsValid() && this.GetFirstConflict() == null;
		}

		// Token: 0x06007329 RID: 29481 RVA: 0x002B8470 File Offset: 0x002B6670
		public override bool IsValid(GameObject Object)
		{
			if (!base.IsValid(Object))
			{
				return false;
			}
			GameObject gameObject = Object;
			if (this.IsCarried)
			{
				gameObject = Object.Holder;
				if (!GolemQuestSelection.IsValidHolder(gameObject))
				{
					return false;
				}
			}
			return base.System.Mound == null || base.System.Mound.ParentObject.InSameZone(gameObject);
		}

		// Token: 0x0600732A RID: 29482 RVA: 0x002B84C8 File Offset: 0x002B66C8
		public override string GetOptionChoice()
		{
			return this.ObjectDisplayName;
		}

		// Token: 0x0600732B RID: 29483 RVA: 0x002B84D0 File Offset: 0x002B66D0
		private bool IsObjectMatch(GameObject Object)
		{
			return Object.IDMatch(this.ObjectID);
		}

		// Token: 0x04003536 RID: 13622
		public string ObjectID;

		// Token: 0x04003537 RID: 13623
		public string ObjectDisplayName;

		// Token: 0x04003538 RID: 13624
		public Renderable ObjectIcon;

		// Token: 0x04003539 RID: 13625
		[NonSerialized]
		private bool Failed;

		// Token: 0x0400353A RID: 13626
		[NonSerialized]
		private GameObject _Material;

		// Token: 0x0400353B RID: 13627
		[NonSerialized]
		private List<GameObject> _ValidMaterials = new List<GameObject>(8);
	}
}
