using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL
{
	// Token: 0x0200052E RID: 1326
	[Serializable]
	public class MutationEntry : IPartEntry
	{
		// Token: 0x060038B6 RID: 14518 RVA: 0x00116848 File Offset: 0x00114A48
		public string GetStat()
		{
			if (!string.IsNullOrEmpty(this.Stat))
			{
				return this.Stat;
			}
			MutationCategory category = this.Category;
			if (category == null)
			{
				return null;
			}
			return category.Stat;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x0011686F File Offset: 0x00114A6F
		public string GetProperty()
		{
			if (!string.IsNullOrEmpty(this.Property))
			{
				return this.Property;
			}
			MutationCategory category = this.Category;
			if (category == null)
			{
				return null;
			}
			return category.Property;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x00116896 File Offset: 0x00114A96
		public string GetCategoryForceProperty()
		{
			MutationCategory category = this.Category;
			if (category == null)
			{
				return null;
			}
			return category.ForceProperty;
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x060038B9 RID: 14521 RVA: 0x001168A9 File Offset: 0x00114AA9
		// (set) Token: 0x060038BA RID: 14522 RVA: 0x001168B1 File Offset: 0x00114AB1
		[Obsolete("Use Name if you want the stable ID, or GetDisplayName() if you want a string for UI display.")]
		public string DisplayName
		{
			get
			{
				return this.Name;
			}
			set
			{
				this.Name = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x001168BA File Offset: 0x00114ABA
		// (set) Token: 0x060038BC RID: 14524 RVA: 0x001168C2 File Offset: 0x00114AC2
		[Obsolete("Use Variant field.")]
		public string Constructor
		{
			get
			{
				return this.Variant;
			}
			set
			{
				this.Variant = value;
			}
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x001168CB File Offset: 0x00114ACB
		public string GetForceProperty()
		{
			if (string.IsNullOrEmpty(this.ForceProperty))
			{
				this.ForceProperty = "MutationBonus_" + this.Class;
			}
			return this.ForceProperty;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x001168F6 File Offset: 0x00114AF6
		public string[] GetExclusions()
		{
			if (this._Exclusions == null)
			{
				if (!string.IsNullOrEmpty(this.Exclusions))
				{
					this._Exclusions = this.Exclusions.Split(',', StringSplitOptions.None);
				}
				else
				{
					this._Exclusions = new string[0];
				}
			}
			return this._Exclusions;
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x060038BF RID: 14527 RVA: 0x00116935 File Offset: 0x00114B35
		// (set) Token: 0x060038C0 RID: 14528 RVA: 0x0011693D File Offset: 0x00114B3D
		public string BearerDescription
		{
			get
			{
				return this.Snippet;
			}
			set
			{
				this.Snippet = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060038C1 RID: 14529 RVA: 0x00116948 File Offset: 0x00114B48
		public BaseMutation Mutation
		{
			get
			{
				BaseMutation baseMutation;
				if ((baseMutation = this._Mutation) == null)
				{
					baseMutation = (this._Mutation = Mutations.GetGenericMutation(this.Class, this.Variant, null));
				}
				return baseMutation;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x0011697A File Offset: 0x00114B7A
		public override IPart Instance
		{
			get
			{
				return this.Mutation;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060038C3 RID: 14531 RVA: 0x00116982 File Offset: 0x00114B82
		public Type MutationType
		{
			get
			{
				if (this._MutationType == null)
				{
					string text = "XRL.World.Parts.Mutation.";
					BaseMutation mutation = this.Mutation;
					this._MutationType = ModManager.ResolveType(text + ((mutation != null) ? mutation.ToString() : null), false, false, true);
				}
				return this._MutationType;
			}
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x001169BC File Offset: 0x00114BBC
		public List<string> GetVariants()
		{
			BaseMutation mutation = this.Mutation;
			if (mutation == null)
			{
				return null;
			}
			return mutation.GetVariants();
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x001169D0 File Offset: 0x00114BD0
		public bool HasVariants
		{
			get
			{
				bool flag = this._HasVariants.GetValueOrDefault();
				if (this._HasVariants == null)
				{
					BaseMutation mutation = this.Mutation;
					flag = mutation != null && mutation.HasVariants;
					this._HasVariants = new bool?(flag);
					return flag;
				}
				return flag;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x00116A18 File Offset: 0x00114C18
		public bool CanSelectVariant
		{
			get
			{
				bool? canSelectVariant = this._CanSelectVariant;
				bool? flag;
				if (canSelectVariant == null)
				{
					BaseMutation mutation = this.Mutation;
					flag = (this._CanSelectVariant = ((mutation != null) ? new bool?(mutation.CanSelectVariant) : null));
				}
				else
				{
					flag = canSelectVariant;
				}
				return flag ?? true;
			}
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x00116A73 File Offset: 0x00114C73
		public BaseMutation CreateInstance()
		{
			return BaseMutation.Create(this);
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x00116A7C File Offset: 0x00114C7C
		public IRenderable GetRenderable()
		{
			if (this.Tile != null)
			{
				return new Renderable
				{
					Tile = this.Tile,
					ColorString = "&" + this.Foreground,
					DetailColor = this.Detail[0]
				};
			}
			return null;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x00116ACC File Offset: 0x00114CCC
		public bool OkWith(MutationEntry Entry, bool CheckOther = true, bool allowMultipleDefects = false)
		{
			if (Entry == this || Entry == null)
			{
				return true;
			}
			if (CheckOther && !Entry.OkWith(this, false, allowMultipleDefects))
			{
				return false;
			}
			if (!allowMultipleDefects && !Options.DisableDefectLimit && Entry.IsDefect() && this.IsDefect())
			{
				return false;
			}
			foreach (string text in this.GetExclusions())
			{
				if (text == Entry.Name)
				{
					return false;
				}
				if (text.Length > 0 && text[0] == '*' && text.Length == Entry.Category.Name.Length + 1 && text.EndsWith(Entry.Category.Name))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x00116B7B File Offset: 0x00114D7B
		public bool IsDefect()
		{
			return this.Defect;
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x00116B84 File Offset: 0x00114D84
		public bool IsMental()
		{
			BaseMutation mutation = this.Mutation;
			if (mutation == null)
			{
				return false;
			}
			string mutationType = mutation.GetMutationType();
			bool? flag = ((mutationType != null) ? new bool?(mutationType.Contains("Mental")) : null);
			bool flag2 = true;
			return (flag.GetValueOrDefault() == flag2) & (flag != null);
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x00116BD8 File Offset: 0x00114DD8
		public bool IsPhysical()
		{
			BaseMutation mutation = this.Mutation;
			if (mutation == null)
			{
				return false;
			}
			string mutationType = mutation.GetMutationType();
			bool? flag = ((mutationType != null) ? new bool?(mutationType.Contains("Physical")) : null);
			bool flag2 = true;
			return (flag.GetValueOrDefault() == flag2) & (flag != null);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x00116C2C File Offset: 0x00114E2C
		public string GetDisplayName(bool WithAnnotations = false)
		{
			string text = this.XMLDisplayName ?? this.Name;
			if (WithAnnotations && this.IsDefect())
			{
				return text + " ({{r|D}})";
			}
			return text;
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x00116C64 File Offset: 0x00114E64
		public override void HandleXMLNode(XmlDataHelper Reader)
		{
			base.HandleXMLNode(Reader);
			this.Name = Reader.ParseAttribute<string>("Name", this.Name, false, null);
			this.XMLDisplayName = Reader.ParseAttribute<string>("DisplayName", this.XMLDisplayName, false, null);
			this.Variant = Reader.ParseAttribute<string>("Variant", this.Variant, false, null);
			this.Type = Reader.ParseAttribute<string>("Type", this.Type, false, null);
			this.Tile = Reader.ParseAttribute<string>("Tile", this.Tile, false, null);
			this.Foreground = Reader.ParseAttribute<string>("Foreground", this.Foreground, false, null);
			this.Detail = Reader.ParseAttribute<string>("Detail", this.Detail, false, null);
			this.Stat = Reader.ParseAttribute<string>("Stat", this.Stat, false, null);
			this.Property = Reader.ParseAttribute<string>("Property", this.Property, false, null);
			this.ForceProperty = Reader.ParseAttribute<string>("ForceProperty", this.ForceProperty, false, null);
			this.BiomeTable = Reader.ParseAttribute<string>("BiomeTable", this.BiomeTable, false, null);
			this.BiomeAdjective = Reader.ParseAttribute<string>("BiomeAdjective", this.BiomeAdjective, false, null);
			this.BiomeEpithet = Reader.ParseAttribute<string>("BiomeEpithet", this.BiomeEpithet, false, null);
			this.Maximum = Reader.ParseAttribute<int>("MaxSelected", this.Maximum, false, null);
			this.MaxLevel = Reader.ParseAttribute<int>("MaxLevel", this.MaxLevel, false, null);
			this.Ranked = Reader.ParseAttribute<bool>("Ranked", this.Ranked, false, null);
			this.Defect = Reader.ParseAttribute<bool>("Defect", this.Defect, false, null);
			this.Exclusions = Reader.ParseAttribute<string>("Exclusions", this.Exclusions, false, null);
			this.Prerelease = Reader.ParseAttribute<bool>("Prerelease", this.Prerelease, false, null);
			if (this.Snippet.IsNullOrEmpty())
			{
				this.Snippet = Reader.ParseAttribute<string>("BearerDescription", "", false, null);
			}
		}

		// Token: 0x04001C99 RID: 7321
		public MutationCategory Category;

		// Token: 0x04001C9A RID: 7322
		public string Exclusions;

		// Token: 0x04001C9B RID: 7323
		public string Variant;

		// Token: 0x04001C9C RID: 7324
		public string Type;

		// Token: 0x04001C9D RID: 7325
		public bool Ranked;

		// Token: 0x04001C9E RID: 7326
		public bool Defect;

		// Token: 0x04001C9F RID: 7327
		public string XMLDisplayName;

		// Token: 0x04001CA0 RID: 7328
		public string Help = "";

		// Token: 0x04001CA1 RID: 7329
		public string Stat = "";

		// Token: 0x04001CA2 RID: 7330
		public string Tile;

		// Token: 0x04001CA3 RID: 7331
		public string Foreground = "w";

		// Token: 0x04001CA4 RID: 7332
		public string Detail = "W";

		// Token: 0x04001CA5 RID: 7333
		public string Property = "";

		// Token: 0x04001CA6 RID: 7334
		public string ForceProperty;

		// Token: 0x04001CA7 RID: 7335
		private string[] _Exclusions;

		// Token: 0x04001CA8 RID: 7336
		public int Maximum = 1;

		// Token: 0x04001CA9 RID: 7337
		public string BiomeTable;

		// Token: 0x04001CAA RID: 7338
		public string BiomeAdjective = "";

		// Token: 0x04001CAB RID: 7339
		public string BiomeEpithet = "";

		// Token: 0x04001CAC RID: 7340
		public int MaxLevel = 10;

		// Token: 0x04001CAD RID: 7341
		public bool Prerelease;

		// Token: 0x04001CAE RID: 7342
		private BaseMutation _Mutation;

		// Token: 0x04001CAF RID: 7343
		private Type _MutationType;

		// Token: 0x04001CB0 RID: 7344
		private bool? _HasVariants;

		// Token: 0x04001CB1 RID: 7345
		private bool? _CanSelectVariant;
	}
}
