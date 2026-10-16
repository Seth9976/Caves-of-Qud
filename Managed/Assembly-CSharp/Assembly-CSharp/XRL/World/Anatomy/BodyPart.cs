using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Occult.Engine.CodeGeneration;
using Qud.API;
using XRL.Language;
using XRL.Messages;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BBF RID: 3007
	[GenerateSerializationPartial(Write = "WriteValues", Read = "ReadValues")]
	[Serializable]
	public class BodyPart : IComposite
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06007774 RID: 30580 RVA: 0x002CF280 File Offset: 0x002CD480
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public virtual bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007775 RID: 30581 RVA: 0x002CF284 File Offset: 0x002CD484
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void WriteValues(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Type);
			Writer.WriteOptimized(this.VariantType);
			Writer.WriteOptimized(this.Description);
			Writer.WriteOptimized(this.DescriptionPrefix);
			Writer.WriteOptimized(this.Name);
			Writer.WriteOptimized(this.SupportsDependent);
			Writer.WriteOptimized(this.DependsOn);
			Writer.WriteOptimized(this.RequiresType);
			Writer.WriteOptimized(this.Manager);
			Writer.WriteOptimized(this.Category);
			Writer.WriteOptimized(this.Laterality);
			Writer.WriteOptimized(this.RequiresLaterality);
			Writer.WriteOptimized(this.Mobility);
			Writer.WriteOptimized(this.Flags);
			Writer.WriteOptimized(this.DefaultBehaviorBlueprint);
			Writer.WriteOptimized(this.Position);
			Writer.WriteOptimized(this._ID);
		}

		// Token: 0x06007776 RID: 30582 RVA: 0x002CF360 File Offset: 0x002CD560
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void ReadValues(SerializationReader Reader)
		{
			this.Type = Reader.ReadOptimizedString();
			this.VariantType = Reader.ReadOptimizedString();
			this.Description = Reader.ReadOptimizedString();
			this.DescriptionPrefix = Reader.ReadOptimizedString();
			this.Name = Reader.ReadOptimizedString();
			this.SupportsDependent = Reader.ReadOptimizedString();
			this.DependsOn = Reader.ReadOptimizedString();
			this.RequiresType = Reader.ReadOptimizedString();
			this.Manager = Reader.ReadOptimizedString();
			this.Category = Reader.ReadOptimizedInt32();
			this.Laterality = Reader.ReadOptimizedInt32();
			this.RequiresLaterality = Reader.ReadOptimizedInt32();
			this.Mobility = Reader.ReadOptimizedInt32();
			this.Flags = Reader.ReadOptimizedInt32();
			this.DefaultBehaviorBlueprint = Reader.ReadOptimizedString();
			this.Position = Reader.ReadOptimizedInt32();
			this._ID = Reader.ReadOptimizedInt32();
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06007777 RID: 30583 RVA: 0x002CF439 File Offset: 0x002CD639
		// (set) Token: 0x06007778 RID: 30584 RVA: 0x002CF446 File Offset: 0x002CD646
		public bool DefaultPrimary
		{
			get
			{
				return (this.Flags & 1) == 1;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1) : (this.Flags & -2));
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06007779 RID: 30585 RVA: 0x002CF464 File Offset: 0x002CD664
		// (set) Token: 0x0600777A RID: 30586 RVA: 0x002CF471 File Offset: 0x002CD671
		public bool PreferredPrimary
		{
			get
			{
				return (this.Flags & 2) == 2;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2) : (this.Flags & -3));
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x0600777B RID: 30587 RVA: 0x002CF48F File Offset: 0x002CD68F
		// (set) Token: 0x0600777C RID: 30588 RVA: 0x002CF49C File Offset: 0x002CD69C
		public bool Primary
		{
			get
			{
				return (this.Flags & 4) == 4;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4) : (this.Flags & -5));
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x0600777D RID: 30589 RVA: 0x002CF4BA File Offset: 0x002CD6BA
		// (set) Token: 0x0600777E RID: 30590 RVA: 0x002CF4C7 File Offset: 0x002CD6C7
		public bool Native
		{
			get
			{
				return (this.Flags & 8) == 8;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8) : (this.Flags & -9));
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x0600777F RID: 30591 RVA: 0x002CF4E5 File Offset: 0x002CD6E5
		// (set) Token: 0x06007780 RID: 30592 RVA: 0x002CF4F4 File Offset: 0x002CD6F4
		public bool Appendage
		{
			get
			{
				return (this.Flags & 16) == 16;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16) : (this.Flags & -17));
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06007781 RID: 30593 RVA: 0x002CF513 File Offset: 0x002CD713
		// (set) Token: 0x06007782 RID: 30594 RVA: 0x002CF522 File Offset: 0x002CD722
		public bool Integral
		{
			get
			{
				return (this.Flags & 32) == 32;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 32) : (this.Flags & -33));
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06007783 RID: 30595 RVA: 0x002CF541 File Offset: 0x002CD741
		// (set) Token: 0x06007784 RID: 30596 RVA: 0x002CF550 File Offset: 0x002CD750
		public bool Mortal
		{
			get
			{
				return (this.Flags & 64) == 64;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 64) : (this.Flags & -65));
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06007785 RID: 30597 RVA: 0x002CF56F File Offset: 0x002CD76F
		// (set) Token: 0x06007786 RID: 30598 RVA: 0x002CF584 File Offset: 0x002CD784
		public bool Abstract
		{
			get
			{
				return (this.Flags & 128) == 128;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 128) : (this.Flags & -129));
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06007787 RID: 30599 RVA: 0x002CF5A9 File Offset: 0x002CD7A9
		// (set) Token: 0x06007788 RID: 30600 RVA: 0x002CF5BE File Offset: 0x002CD7BE
		public bool Extrinsic
		{
			get
			{
				return (this.Flags & 256) == 256;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 256) : (this.Flags & -257));
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06007789 RID: 30601 RVA: 0x002CF5E3 File Offset: 0x002CD7E3
		// (set) Token: 0x0600778A RID: 30602 RVA: 0x002CF5F8 File Offset: 0x002CD7F8
		public bool Dynamic
		{
			get
			{
				return (this.Flags & 512) == 512;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 512) : (this.Flags & -513));
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600778B RID: 30603 RVA: 0x002CF61D File Offset: 0x002CD81D
		// (set) Token: 0x0600778C RID: 30604 RVA: 0x002CF632 File Offset: 0x002CD832
		public bool Plural
		{
			get
			{
				return (this.Flags & 1024) == 1024;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1024) : (this.Flags & -1025));
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x0600778D RID: 30605 RVA: 0x002CF657 File Offset: 0x002CD857
		// (set) Token: 0x0600778E RID: 30606 RVA: 0x002CF66C File Offset: 0x002CD86C
		public bool Mass
		{
			get
			{
				return (this.Flags & 2048) == 2048;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2048) : (this.Flags & -2049));
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x0600778F RID: 30607 RVA: 0x002CF691 File Offset: 0x002CD891
		// (set) Token: 0x06007790 RID: 30608 RVA: 0x002CF6A6 File Offset: 0x002CD8A6
		public bool Contact
		{
			get
			{
				return (this.Flags & 4096) == 4096;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4096) : (this.Flags & -4097));
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06007791 RID: 30609 RVA: 0x002CF6CB File Offset: 0x002CD8CB
		// (set) Token: 0x06007792 RID: 30610 RVA: 0x002CF6E0 File Offset: 0x002CD8E0
		public bool Thrown
		{
			get
			{
				return (this.Flags & 131072) == 131072;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 131072) : (this.Flags & -131073));
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06007793 RID: 30611 RVA: 0x002CF705 File Offset: 0x002CD905
		// (set) Token: 0x06007794 RID: 30612 RVA: 0x002CF71A File Offset: 0x002CD91A
		public bool IgnorePosition
		{
			get
			{
				return (this.Flags & 8192) == 8192;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8192) : (this.Flags & -8193));
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06007795 RID: 30613 RVA: 0x002CF73F File Offset: 0x002CD93F
		// (set) Token: 0x06007796 RID: 30614 RVA: 0x002CF754 File Offset: 0x002CD954
		public bool FirstSlotForEquipped
		{
			get
			{
				return (this.Flags & 16384) == 16384;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16384) : (this.Flags & -16385));
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06007797 RID: 30615 RVA: 0x002CF779 File Offset: 0x002CD979
		// (set) Token: 0x06007798 RID: 30616 RVA: 0x002CF78E File Offset: 0x002CD98E
		public bool FirstSlotForCybernetics
		{
			get
			{
				return (this.Flags & 32768) == 32768;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 32768) : (this.Flags & -32769));
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06007799 RID: 30617 RVA: 0x002CF7B3 File Offset: 0x002CD9B3
		// (set) Token: 0x0600779A RID: 30618 RVA: 0x002CF7C8 File Offset: 0x002CD9C8
		public bool FirstSlotForDefaultBehavior
		{
			get
			{
				return (this.Flags & 65536) == 65536;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 65536) : (this.Flags & -65537));
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x0600779B RID: 30619 RVA: 0x002CF7ED File Offset: 0x002CD9ED
		// (set) Token: 0x0600779C RID: 30620 RVA: 0x002CF7F8 File Offset: 0x002CD9F8
		public GameObject DefaultBehavior
		{
			get
			{
				return this._DefaultBehavior;
			}
			set
			{
				this._DefaultBehavior = value;
				if (this._DefaultBehavior != null)
				{
					GameObject defaultBehavior = this._DefaultBehavior;
					Body parentBody = this.ParentBody;
					defaultBehavior.CheckDefaultBehaviorGiganticness((parentBody != null) ? parentBody.ParentObject : null);
					if (this.ParentBody != null && this.ParentBody.ParentObject != null)
					{
						DecorateDefaultEquipmentEvent.Send(this.ParentBody.ParentObject, this.ParentBody);
						return;
					}
				}
				else
				{
					this.FirstSlotForDefaultBehavior = false;
				}
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x0600779D RID: 30621 RVA: 0x002CF864 File Offset: 0x002CDA64
		public GameObject Cybernetics
		{
			get
			{
				return this._Cybernetics;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600779E RID: 30622 RVA: 0x002CF86C File Offset: 0x002CDA6C
		public GameObject Equipped
		{
			get
			{
				return this._Equipped;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600779F RID: 30623 RVA: 0x002CF874 File Offset: 0x002CDA74
		public bool IsDismembered
		{
			get
			{
				return this.ParentPart == null && (this.ParentBody == null || this.ParentBody._Body != this);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060077A0 RID: 30624 RVA: 0x002CF89C File Offset: 0x002CDA9C
		// (set) Token: 0x060077A1 RID: 30625 RVA: 0x002CF8EC File Offset: 0x002CDAEC
		public int ID
		{
			get
			{
				if (this._ID == 0)
				{
					XRLGame game = The.Game;
					if (game != null)
					{
						XRLGame xrlgame = game;
						int num = xrlgame.BodyPartIDSequence + 1;
						xrlgame.BodyPartIDSequence = num;
						this._ID = num;
					}
					else
					{
						this._ID = BodyPart.IDPreloadSequence--;
					}
				}
				return this._ID;
			}
			set
			{
				this._ID = value;
			}
		}

		// Token: 0x060077A2 RID: 30626 RVA: 0x002CF8F5 File Offset: 0x002CDAF5
		public bool IDMatch(int TestID)
		{
			return this._ID == TestID;
		}

		// Token: 0x060077A3 RID: 30627 RVA: 0x002CF900 File Offset: 0x002CDB00
		public bool HasID()
		{
			return this._ID != 0;
		}

		// Token: 0x060077A4 RID: 30628 RVA: 0x002CF90B File Offset: 0x002CDB0B
		public BodyPart()
		{
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x002CF938 File Offset: 0x002CDB38
		public BodyPart(BodyPart Source)
			: this()
		{
			this.Type = Source.Type;
			this.VariantType = Source.VariantType;
			this.Description = Source.Description;
			this.DescriptionPrefix = Source.DescriptionPrefix;
			this.Name = Source.Name;
			this.SupportsDependent = Source.SupportsDependent;
			this.DependsOn = Source.DependsOn;
			this.RequiresType = Source.RequiresType;
			this.Manager = Source.Manager;
			this.Category = Source.Category;
			this.Laterality = Source.Laterality;
			this.RequiresLaterality = Source.RequiresLaterality;
			this.Mobility = Source.Mobility;
			this.Flags = Source.Flags;
			this.ParentBody = Source.ParentBody;
			this.DefaultBehaviorBlueprint = Source.DefaultBehaviorBlueprint;
			this.DefaultBehavior = Source.DefaultBehavior;
			this.Position = Source.Position;
			this._Cybernetics = Source._Cybernetics;
			this._Equipped = Source._Equipped;
			this.Parts = ((Source.Parts != null) ? new List<BodyPart>(Source.Parts) : null);
			this.Thrown = this.Type == "Thrown Weapon";
		}

		// Token: 0x060077A6 RID: 30630 RVA: 0x002CFA6D File Offset: 0x002CDC6D
		private BodyPart(int Alloc)
		{
			if (this.Parts == null)
			{
				this.Parts = new List<BodyPart>(Alloc);
			}
		}

		// Token: 0x060077A7 RID: 30631 RVA: 0x002CFAAD File Offset: 0x002CDCAD
		public BodyPart(Body ParentBody)
			: this()
		{
			this.ParentBody = ParentBody;
		}

		// Token: 0x060077A8 RID: 30632 RVA: 0x002CFABC File Offset: 0x002CDCBC
		public BodyPart(Body ParentBody, int Alloc)
			: this(Alloc)
		{
			this.ParentBody = ParentBody;
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x002CFACC File Offset: 0x002CDCCC
		public BodyPart(Body ParentBody, BodyPart ParentPart)
			: this()
		{
			this.ParentBody = ParentBody;
			this.ParentPart = ParentPart;
		}

		// Token: 0x060077AA RID: 30634 RVA: 0x002CFAE2 File Offset: 0x002CDCE2
		public BodyPart(BodyPartType Base, Body ParentBody)
			: this(ParentBody)
		{
			Base.ApplyTo(this);
			this.Thrown = this.Type == "Thrown Weapon";
		}

		// Token: 0x060077AB RID: 30635 RVA: 0x002CFB08 File Offset: 0x002CDD08
		public BodyPart(BodyPartType Base, Body ParentBody, int Alloc)
			: this(ParentBody, Alloc)
		{
			Base.ApplyTo(this);
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x002CFB19 File Offset: 0x002CDD19
		public BodyPart(string Base, Body ParentBody)
			: this(Anatomies.GetBodyPartTypeOrFail(Base), ParentBody)
		{
		}

		// Token: 0x060077AD RID: 30637 RVA: 0x002CFB28 File Offset: 0x002CDD28
		public BodyPart(string Base, Body ParentBody, int Alloc)
			: this(Anatomies.GetBodyPartTypeOrFail(Base), ParentBody, Alloc)
		{
		}

		// Token: 0x060077AE RID: 30638 RVA: 0x002CFB38 File Offset: 0x002CDD38
		public BodyPart(string Base, int Laterality, Body ParentBody)
			: this(Base, ParentBody)
		{
			this.ChangeLaterality(Laterality, false);
		}

		// Token: 0x060077AF RID: 30639 RVA: 0x002CFB4B File Offset: 0x002CDD4B
		public BodyPart(string Type, string Description, string Name, Body ParentBody)
			: this(ParentBody)
		{
			this.Type = Type;
			this.Description = Description ?? Type;
			this.Name = Name ?? Description.ToLower();
			this.Thrown = Type == "Thrown Weapon";
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x002CFB8C File Offset: 0x002CDD8C
		public BodyPart(BodyPartType Base, Body ParentBody, string DescriptionPrefix = null, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Mortal = null, bool? Appendage = null, bool? Integral = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null)
			: this(Base, ParentBody)
		{
			if (DescriptionPrefix != null)
			{
				this.DescriptionPrefix = DescriptionPrefix;
			}
			if (DefaultBehavior != null)
			{
				this.DefaultBehaviorBlueprint = DefaultBehavior;
			}
			if (SupportsDependent != null)
			{
				this.SupportsDependent = SupportsDependent;
			}
			if (DependsOn != null)
			{
				this.DependsOn = DependsOn;
			}
			if (RequiresType != null)
			{
				this.RequiresType = RequiresType;
			}
			if (Manager != null)
			{
				this.Manager = Manager;
			}
			if (Category != null)
			{
				this.Category = Category.Value;
			}
			if (RequiresLaterality != null)
			{
				this.RequiresLaterality = RequiresLaterality.Value;
			}
			if (Mobility != null)
			{
				this.Mobility = Mobility.Value;
			}
			if (Appendage != null)
			{
				this.Appendage = Appendage.Value;
			}
			if (Integral != null)
			{
				this.Integral = Integral.Value;
			}
			if (Mortal != null)
			{
				this.Mortal = Mortal.Value;
			}
			if (Abstract != null)
			{
				this.Abstract = Abstract.Value;
			}
			if (Extrinsic != null)
			{
				this.Extrinsic = Extrinsic.Value;
			}
			if (Dynamic != null)
			{
				this.Dynamic = Dynamic.Value;
			}
			if (Plural != null)
			{
				this.Plural = Plural.Value;
			}
			if (Mass != null)
			{
				this.Mass = Mass.Value;
			}
			if (Contact != null)
			{
				this.Contact = Contact.Value;
			}
			if (IgnorePosition != null)
			{
				this.IgnorePosition = IgnorePosition.Value;
			}
		}

		// Token: 0x060077B1 RID: 30641 RVA: 0x002CFD08 File Offset: 0x002CDF08
		public BodyPart(BodyPartType Base, int Laterality, Body ParentBody, string DescriptionPrefix = null, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Mortal = null, bool? Appendage = null, bool? Integral = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null)
			: this(Base, ParentBody)
		{
			this.ChangeLaterality(Laterality, false);
			if (DescriptionPrefix != null)
			{
				this.DescriptionPrefix = DescriptionPrefix;
			}
			if (DefaultBehavior != null)
			{
				this.DefaultBehaviorBlueprint = DefaultBehavior;
			}
			if (SupportsDependent != null)
			{
				this.SupportsDependent = SupportsDependent;
			}
			if (DependsOn != null)
			{
				this.DependsOn = DependsOn;
			}
			if (RequiresType != null)
			{
				this.RequiresType = RequiresType;
			}
			if (Manager != null)
			{
				this.Manager = Manager;
			}
			if (Category != null)
			{
				this.Category = Category.Value;
			}
			if (RequiresLaterality != null)
			{
				this.RequiresLaterality = RequiresLaterality.Value;
			}
			if (Mobility != null)
			{
				this.Mobility = Mobility.Value;
			}
			if (Appendage != null)
			{
				this.Appendage = Appendage.Value;
			}
			if (Integral != null)
			{
				this.Integral = Integral.Value;
			}
			if (Mortal != null)
			{
				this.Mortal = Mortal.Value;
			}
			if (Abstract != null)
			{
				this.Abstract = Abstract.Value;
			}
			if (Extrinsic != null)
			{
				this.Extrinsic = Extrinsic.Value;
			}
			if (Dynamic != null)
			{
				this.Dynamic = Dynamic.Value;
			}
			if (Plural != null)
			{
				this.Plural = Plural.Value;
			}
			if (Mass != null)
			{
				this.Mass = Mass.Value;
			}
			if (Contact != null)
			{
				this.Contact = Contact.Value;
			}
			if (IgnorePosition != null)
			{
				this.IgnorePosition = IgnorePosition.Value;
			}
		}

		// Token: 0x060077B2 RID: 30642 RVA: 0x002CFE8C File Offset: 0x002CE08C
		public BodyPart(string Base, int Laterality, Body ParentBody, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Mortal = null, bool? Appendage = null, bool? Integral = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null)
			: this(Base, Laterality, ParentBody)
		{
			if (DefaultBehavior != null)
			{
				this.DefaultBehaviorBlueprint = DefaultBehavior;
			}
			if (SupportsDependent != null)
			{
				this.SupportsDependent = SupportsDependent;
			}
			if (DependsOn != null)
			{
				this.DependsOn = DependsOn;
			}
			if (RequiresType != null)
			{
				this.RequiresType = RequiresType;
			}
			if (Manager != null)
			{
				this.Manager = Manager;
			}
			if (Category != null)
			{
				this.Category = Category.Value;
			}
			if (RequiresLaterality != null)
			{
				this.RequiresLaterality = RequiresLaterality.Value;
			}
			if (Mobility != null)
			{
				this.Mobility = Mobility.Value;
			}
			if (Appendage != null)
			{
				this.Appendage = Appendage.Value;
			}
			if (Integral != null)
			{
				this.Integral = Integral.Value;
			}
			if (Mortal != null)
			{
				this.Mortal = Mortal.Value;
			}
			if (Abstract != null)
			{
				this.Abstract = Abstract.Value;
			}
			if (Extrinsic != null)
			{
				this.Extrinsic = Extrinsic.Value;
			}
			if (Dynamic != null)
			{
				this.Dynamic = Dynamic.Value;
			}
			if (Plural != null)
			{
				this.Plural = Plural.Value;
			}
			if (Mass != null)
			{
				this.Mass = Mass.Value;
			}
			if (Contact != null)
			{
				this.Contact = Contact.Value;
			}
			if (IgnorePosition != null)
			{
				this.IgnorePosition = IgnorePosition.Value;
			}
		}

		// Token: 0x060077B3 RID: 30643 RVA: 0x002CFFFC File Offset: 0x002CE1FC
		public BodyPart Clone()
		{
			return new BodyPart(this);
		}

		// Token: 0x060077B4 RID: 30644 RVA: 0x002D0004 File Offset: 0x002CE204
		public BodyPartType TypeModel()
		{
			return Anatomies.GetBodyPartTypeOrFail(this.Type);
		}

		// Token: 0x060077B5 RID: 30645 RVA: 0x002D0011 File Offset: 0x002CE211
		public BodyPartType VariantTypeModel()
		{
			return Anatomies.GetBodyPartTypeOrFail(this.VariantType ?? this.Type);
		}

		// Token: 0x060077B6 RID: 30646 RVA: 0x002D0028 File Offset: 0x002CE228
		public BodyPartType GetVariantTypeModelIfExists()
		{
			return Anatomies.GetBodyPartType(this.VariantType ?? this.Type);
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x002D003F File Offset: 0x002CE23F
		public static int Sort(BodyPart a, BodyPart b)
		{
			return a.Position.CompareTo(b.Position);
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x002D0052 File Offset: 0x002CE252
		public bool ObjectEquippedOnThisOrAnyParent(GameObject Object)
		{
			return Object == this.Equipped || Object == this.Cybernetics || Object == this.DefaultBehavior || (this.ParentPart != null && this.ParentPart.ObjectEquippedOnThisOrAnyParent(Object));
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x002D008C File Offset: 0x002CE28C
		public BodyPart ChangeLaterality(int NewLaterality, bool Recursive = false)
		{
			BodyPartType bodyPartType = this.VariantTypeModel();
			if (this.IsLateralityConsistent(bodyPartType))
			{
				this.Laterality = NewLaterality;
				this.Description = XRL.World.Capabilities.Laterality.WithLateralityAdjective(bodyPartType.Description, this.Laterality, true);
				this.Name = XRL.World.Capabilities.Laterality.WithLateralityAdjective(bodyPartType.Name, this.Laterality, false);
				this.RequiresLaterality = bodyPartType.RequiresLateralityFor(this.Laterality);
			}
			else
			{
				string text = XRL.World.Capabilities.Laterality.StripLateralityAdjective(this.Description, this.Laterality, true);
				string text2 = XRL.World.Capabilities.Laterality.StripLateralityAdjective(this.Name, this.Laterality, false);
				this.Laterality = NewLaterality;
				this.Description = XRL.World.Capabilities.Laterality.WithLateralityAdjective(text, this.Laterality, true);
				this.Name = XRL.World.Capabilities.Laterality.WithLateralityAdjective(text2, this.Laterality, false);
				if (this.RequiresLaterality == 65535)
				{
					this.RequiresLaterality = bodyPartType.RequiresLateralityFor(this.Laterality);
				}
				else
				{
					this.RequiresLaterality |= bodyPartType.RequiresLateralityFor(this.Laterality);
				}
			}
			if (Recursive && this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].ChangeLaterality(NewLaterality, true);
					i++;
				}
			}
			return this;
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x002D01C0 File Offset: 0x002CE3C0
		public bool IsLateralityConsistent(BodyPartType UseType = null)
		{
			if (UseType == null)
			{
				UseType = this.VariantTypeModel();
			}
			return !(this.Description != XRL.World.Capabilities.Laterality.WithLateralityAdjective(UseType.Description, this.Laterality, true)) && !(this.Name != XRL.World.Capabilities.Laterality.WithLateralityAdjective(UseType.Name, this.Laterality, false)) && (this.RequiresLaterality == 65535 || this.RequiresLaterality == UseType.RequiresLateralityFor(this.Laterality));
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x002D0240 File Offset: 0x002CE440
		public bool IsLateralitySafeToChange(int ExpectLaterality = 0, Body BackupParentBody = null, BodyPart ChildPart = null)
		{
			if (this.Laterality != ExpectLaterality)
			{
				return false;
			}
			BodyPartType bodyPartType = this.VariantTypeModel();
			if (!this.IsLateralityConsistent(bodyPartType))
			{
				return false;
			}
			if (ChildPart != null)
			{
				if (!this.IsParentPartOf(ChildPart, BackupParentBody, true))
				{
					return false;
				}
				if (!ChildPart.IsLateralitySafeToChange(ExpectLaterality, BackupParentBody, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060077BC RID: 30652 RVA: 0x002D0288 File Offset: 0x002CE488
		public IEnumerable<BodyPart> LoopParts()
		{
			yield return this;
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					foreach (BodyPart bodyPart2 in bodyPart.LoopParts())
					{
						yield return bodyPart2;
					}
					IEnumerator<BodyPart> enumerator2 = null;
				}
				List<BodyPart>.Enumerator enumerator = default(List<BodyPart>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060077BD RID: 30653 RVA: 0x002D0298 File Offset: 0x002CE498
		public IEnumerable<BodyPart> LoopOtherParts()
		{
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					foreach (BodyPart bodyPart2 in bodyPart.LoopParts())
					{
						yield return bodyPart2;
					}
					IEnumerator<BodyPart> enumerator2 = null;
				}
				List<BodyPart>.Enumerator enumerator = default(List<BodyPart>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060077BE RID: 30654 RVA: 0x002D02A8 File Offset: 0x002CE4A8
		public IEnumerable<BodyPart> LoopSubparts()
		{
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					yield return bodyPart;
				}
				List<BodyPart>.Enumerator enumerator = default(List<BodyPart>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x060077BF RID: 30655 RVA: 0x002D02B8 File Offset: 0x002CE4B8
		public BodyPart GetParentPart()
		{
			return this.ParentPart;
		}

		// Token: 0x060077C0 RID: 30656 RVA: 0x002D02C0 File Offset: 0x002CE4C0
		public BodyPart GetPreviousPart()
		{
			Body parentBody = this.ParentBody;
			if (parentBody == null)
			{
				return null;
			}
			return parentBody.FindPreviousPartOf(this);
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x002D02D4 File Offset: 0x002CE4D4
		public BodyPart GetNextPart()
		{
			Body parentBody = this.ParentBody;
			if (parentBody == null)
			{
				return null;
			}
			return parentBody.FindNextPartOf(this);
		}

		// Token: 0x060077C2 RID: 30658 RVA: 0x002D02E8 File Offset: 0x002CE4E8
		public BodyPart FindByManager(string Manager)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindByManager(Manager);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			if (this.Manager == Manager)
			{
				return this;
			}
			return null;
		}

		// Token: 0x060077C3 RID: 30659 RVA: 0x002D0340 File Offset: 0x002CE540
		public BodyPart FindByManager(string Manager, string Type)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindByManager(Manager, Type);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			if (this.Manager == Manager && this.Type == Type)
			{
				return this;
			}
			return null;
		}

		// Token: 0x060077C4 RID: 30660 RVA: 0x002D03A8 File Offset: 0x002CE5A8
		public void FindByManager(string Manager, List<BodyPart> Store)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].FindByManager(Manager, Store);
					i++;
				}
			}
			if (this.Manager == Manager)
			{
				Store.Add(this);
			}
		}

		// Token: 0x060077C5 RID: 30661 RVA: 0x002D0400 File Offset: 0x002CE600
		public void FindByManager(string Manager, string Type, List<BodyPart> Store)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].FindByManager(Manager, Type, Store);
					i++;
				}
			}
			if (this.Manager == Manager && this.Type == Type)
			{
				Store.Add(this);
			}
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x002D0464 File Offset: 0x002CE664
		public BodyPart FindParentPartOf(BodyPart FindPart)
		{
			if (this.Parts != null)
			{
				if (this.Parts.Contains(FindPart))
				{
					return this;
				}
				foreach (BodyPart bodyPart in this.Parts)
				{
					BodyPart bodyPart2 = bodyPart.FindParentPartOf(FindPart);
					if (bodyPart2 != null)
					{
						return bodyPart2;
					}
				}
			}
			return null;
		}

		// Token: 0x060077C7 RID: 30663 RVA: 0x002D04D8 File Offset: 0x002CE6D8
		public bool IsParentPartOf(BodyPart FindPart)
		{
			return this.Parts != null && this.Parts.Contains(FindPart);
		}

		// Token: 0x060077C8 RID: 30664 RVA: 0x002D04F0 File Offset: 0x002CE6F0
		public bool IsParentPartOf(BodyPart FindPart, Body BackupParentBody, bool EvenIfDismembered)
		{
			if (this.IsParentPartOf(FindPart))
			{
				return true;
			}
			Body body = ((this.ParentBody != null) ? this.ParentBody : BackupParentBody);
			return body != null && body.ValidateDismemberedParentPart(this, FindPart);
		}

		// Token: 0x060077C9 RID: 30665 RVA: 0x002D052C File Offset: 0x002CE72C
		public bool IsAncestorPartOf(BodyPart FindPart)
		{
			if (this.Parts != null)
			{
				if (this.Parts.Contains(FindPart))
				{
					return true;
				}
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].IsAncestorPartOf(FindPart))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060077CA RID: 30666 RVA: 0x002D0580 File Offset: 0x002CE780
		public BodyPart FindPreviousPartOf(BodyPart FindPart)
		{
			if (this.Parts != null)
			{
				int num = this.Parts.IndexOf(FindPart);
				if (num != -1)
				{
					if (num <= 0)
					{
						return null;
					}
					return this.Parts[num - 1];
				}
				else
				{
					foreach (BodyPart bodyPart in this.Parts)
					{
						BodyPart bodyPart2 = bodyPart.FindPreviousPartOf(FindPart);
						if (bodyPart2 != null)
						{
							return bodyPart2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060077CB RID: 30667 RVA: 0x002D060C File Offset: 0x002CE80C
		public BodyPart FindNextPartOf(BodyPart FindPart)
		{
			if (this.Parts != null)
			{
				int num = this.Parts.IndexOf(FindPart);
				if (num != -1)
				{
					if (num >= this.Parts.Count - 1)
					{
						return null;
					}
					return this.Parts[num + 1];
				}
				else
				{
					foreach (BodyPart bodyPart in this.Parts)
					{
						BodyPart bodyPart2 = bodyPart.FindNextPartOf(FindPart);
						if (bodyPart2 != null)
						{
							return bodyPart2;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060077CC RID: 30668 RVA: 0x002D06A4 File Offset: 0x002CE8A4
		public void FindPartsEquipping(GameObject GO, List<BodyPart> Return)
		{
			if (this.Equipped == GO)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].FindPartsEquipping(GO, Return);
					i++;
				}
			}
		}

		// Token: 0x060077CD RID: 30669 RVA: 0x002D06F4 File Offset: 0x002CE8F4
		public BodyPart FindEquippedItem(GameObject GO)
		{
			if (this.Equipped == GO)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindEquippedItem(GO);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077CE RID: 30670 RVA: 0x002D0748 File Offset: 0x002CE948
		public BodyPart FindEquippedItem(string Blueprint)
		{
			if (this.Equipped != null && this.Equipped.Blueprint == Blueprint)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindEquippedItem(Blueprint);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077CF RID: 30671 RVA: 0x002D07AC File Offset: 0x002CE9AC
		public BodyPart FindDefaultOrEquippedItem(GameObject GO)
		{
			if (this.Equipped == GO)
			{
				return this;
			}
			if (this.DefaultBehavior == GO)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindDefaultOrEquippedItem(GO);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x002D0808 File Offset: 0x002CEA08
		public BodyPart FindDefaultOrEquippedItem(string Blueprint)
		{
			if (this.Equipped != null && this.Equipped.Blueprint == Blueprint)
			{
				return this;
			}
			if (this.DefaultBehavior != null && this.DefaultBehavior.Blueprint == Blueprint)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindDefaultOrEquippedItem(Blueprint);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x002D0888 File Offset: 0x002CEA88
		public GameObject FindEquipmentByEvent(string ID)
		{
			if (this.Equipped != null && this.Equipped.HasRegisteredEvent(ID) && !this.Equipped.FireEvent(ID))
			{
				return this.Equipped;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject = this.Parts[i].FindEquipmentByEvent(ID);
					if (gameObject != null)
					{
						return gameObject;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D2 RID: 30674 RVA: 0x002D08FC File Offset: 0x002CEAFC
		public GameObject FindEquipmentOrDefaultByBlueprint(string Blueprint)
		{
			if (this.Equipped != null && this.Equipped.Blueprint == Blueprint)
			{
				return this.Equipped;
			}
			if (this.DefaultBehavior != null && this.DefaultBehavior.Blueprint == Blueprint)
			{
				return this.DefaultBehavior;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject = this.Parts[i].FindEquipmentOrDefaultByBlueprint(Blueprint);
					if (gameObject != null)
					{
						return gameObject;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x002D0988 File Offset: 0x002CEB88
		public GameObject FindEquipmentOrDefaultByID(string ID)
		{
			if (this.Equipped != null && this.Equipped.IDMatch(ID))
			{
				return this.Equipped;
			}
			if (this.DefaultBehavior != null && this.DefaultBehavior.IDMatch(ID))
			{
				return this.DefaultBehavior;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject = this.Parts[i].FindEquipmentOrDefaultByID(ID);
					if (gameObject != null)
					{
						return gameObject;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x002D0A08 File Offset: 0x002CEC08
		public GameObject FindEquipmentByEvent(Event E)
		{
			if (this.Equipped != null && !this.Equipped.FireEvent(E))
			{
				return this.Equipped;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject = this.Parts[i].FindEquipmentByEvent(E);
					if (gameObject != null)
					{
						return gameObject;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D5 RID: 30677 RVA: 0x002D0A6C File Offset: 0x002CEC6C
		public GameObject FindEquipmentOrCyberneticsByEvent(string ID)
		{
			if (this.Equipped != null && this.Equipped.HasRegisteredEvent(ID) && !this.Equipped.FireEvent(ID))
			{
				return this.Equipped;
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && this.Cybernetics.HasRegisteredEvent(ID) && !this.Cybernetics.FireEvent(ID))
			{
				return this.Cybernetics;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject = this.Parts[i].FindEquipmentOrCyberneticsByEvent(ID);
					if (gameObject != null)
					{
						return gameObject;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D6 RID: 30678 RVA: 0x002D0B18 File Offset: 0x002CED18
		public GameObject FindEquipmentOrCyberneticsByEvent(Event E)
		{
			if (this.Equipped != null && !this.Equipped.FireEvent(E))
			{
				return this.Equipped;
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && !this.Cybernetics.FireEvent(E))
			{
				return this.Cybernetics;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject = this.Parts[i].FindEquipmentOrCyberneticsByEvent(E);
					if (gameObject != null)
					{
						return gameObject;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D7 RID: 30679 RVA: 0x002D0BA8 File Offset: 0x002CEDA8
		public GameObject FindEquippedItem(Predicate<GameObject> Filter)
		{
			if (this.Equipped != null && (Filter == null || Filter(this.Equipped)))
			{
				return this.Equipped;
			}
			if (this.DefaultBehavior != null && (Filter == null || Filter(this.DefaultBehavior)))
			{
				return this.DefaultBehavior;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject = this.Parts[i].FindEquippedItem(Filter);
					if (gameObject != null)
					{
						return gameObject;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077D8 RID: 30680 RVA: 0x002D0C30 File Offset: 0x002CEE30
		public bool HasEquippedItem(GameObject GO)
		{
			if (this.Equipped == GO)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].HasEquippedItem(GO))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060077D9 RID: 30681 RVA: 0x002D0C80 File Offset: 0x002CEE80
		public bool HasEquippedItem(string Blueprint)
		{
			if (this.Equipped != null && this.Equipped.Blueprint == Blueprint)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].HasEquippedItem(Blueprint))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060077DA RID: 30682 RVA: 0x002D0CE4 File Offset: 0x002CEEE4
		public bool HasEquippedItem(Predicate<GameObject> Filter)
		{
			if (this.Equipped != null && (Filter == null || Filter(this.Equipped)))
			{
				return true;
			}
			if (this.DefaultBehavior != null && (Filter == null || Filter(this.DefaultBehavior)))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].HasEquippedItem(Filter))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060077DB RID: 30683 RVA: 0x002D0D60 File Offset: 0x002CEF60
		public bool IsItemEquippedOnLimbType(GameObject GO, string FindType)
		{
			if (this.Equipped == GO && this.Type == FindType)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].IsItemEquippedOnLimbType(GO, FindType))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060077DC RID: 30684 RVA: 0x002D0DC0 File Offset: 0x002CEFC0
		public BodyPart FindCybernetics(GameObject GO)
		{
			if (this.Cybernetics == GO)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindCybernetics(GO);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x002D0E14 File Offset: 0x002CF014
		public bool IsItemImplantedInLimbType(GameObject GO, string FindType)
		{
			if (this.Cybernetics == GO && this.Type == FindType)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].IsItemImplantedInLimbType(GO, FindType))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x002D0E74 File Offset: 0x002CF074
		public BodyPart FindDefaultBehavior(GameObject GO)
		{
			if (this.DefaultBehavior == GO)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i].FindDefaultBehavior(GO);
					if (bodyPart != null)
					{
						return bodyPart;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060077DF RID: 30687 RVA: 0x002D0EC8 File Offset: 0x002CF0C8
		public bool IsItemDefaultBehaviorOnLimbType(GameObject GO, string FindType)
		{
			if (this.DefaultBehavior == GO && this.Type == FindType)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].IsItemDefaultBehaviorOnLimbType(GO, FindType))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060077E0 RID: 30688 RVA: 0x002D0F26 File Offset: 0x002CF126
		public void FlushTransientCaches()
		{
			Body parentBody = this.ParentBody;
			if (parentBody == null)
			{
				return;
			}
			GameObject parentObject = parentBody.ParentObject;
			if (parentObject == null)
			{
				return;
			}
			parentObject.FlushTransientCache();
		}

		// Token: 0x060077E1 RID: 30689 RVA: 0x002D0F44 File Offset: 0x002CF144
		public GameObject Unimplant(bool MoveToInventory = true)
		{
			GameObject cybernetics = this._Cybernetics;
			if (cybernetics != null)
			{
				UnimplantedEvent.Send(this.ParentBody.ParentObject, cybernetics, this, null, false);
				ImplantRemovedEvent.Send(this.ParentBody.ParentObject, cybernetics, this, null, false);
				if (this._Equipped == cybernetics)
				{
					this._Equipped = null;
					this.FirstSlotForEquipped = false;
				}
				if (MoveToInventory && this.ParentBody != null && this.ParentBody.ParentObject != null)
				{
					Inventory inventory = this.ParentBody.ParentObject.Inventory;
					if (inventory != null && !inventory.Objects.Contains(cybernetics))
					{
						inventory.AddObject(cybernetics, null, false, false, true, null);
					}
				}
				this._Cybernetics = null;
				this.FirstSlotForCybernetics = false;
				this.FlushTransientCaches();
			}
			return cybernetics;
		}

		// Token: 0x060077E2 RID: 30690 RVA: 0x002D0FF9 File Offset: 0x002CF1F9
		public GameObject Dismember(bool Obliterate = false)
		{
			if (this.ParentBody == null)
			{
				return null;
			}
			return this.ParentBody.Dismember(this, null, null, Obliterate, false, null);
		}

		// Token: 0x060077E3 RID: 30691 RVA: 0x002D1018 File Offset: 0x002CF218
		public void Implant(GameObject Cybernetic, bool ForDeepCopy = false, bool Silent = false)
		{
			if (this._Cybernetics != null)
			{
				this.Unimplant(true);
			}
			this._Cybernetics = Cybernetic;
			this.ParentBody.RecalculateFirstCybernetics(Cybernetic);
			if (Cybernetic.HasTag("CyberneticsUsesEqSlot"))
			{
				if (this._Equipped != null && !this.TryUnequip(false, false, false, false))
				{
					this.ForceUnequip(Silent, false, false);
				}
				this.Equip(Cybernetic, null, true, false, false, true);
			}
			ImplantedEvent.Send(this.ParentBody.ParentObject, Cybernetic, this, null, ForDeepCopy, Silent);
			ImplantAddedEvent.Send(this.ParentBody.ParentObject, Cybernetic, this, null, ForDeepCopy, Silent);
			this.ParentBody.RegenerateDefaultEquipment();
			if (!ForDeepCopy && !Silent && this.ParentBody.ParentObject.IsPlayer())
			{
				string baseDisplayName = Cybernetic.BaseDisplayName;
				JournalAPI.AddAccomplishment(string.Concat(new string[]
				{
					"You installed ",
					Cybernetic.IsPlural ? "some" : "a",
					" cybernetic ",
					baseDisplayName,
					" and came one step closer to the Grand Unification."
				}), "Gaze upon the sublime body of =name=! See the glistening " + baseDisplayName + "!", string.Concat(new string[]
				{
					"While wandering around  ",
					The.Player.CurrentZone.GetTerrainDisplayName(true),
					", =name= stumbled upon a clan of machine folk. Because of ",
					Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
					" <spice.elements.",
					The.Player.GetMythicDomain(5, null),
					".quality.!random>, they accepted ",
					The.Player.GetPronounProvider(false).Objective,
					" into their fold and helped ",
					The.Player.GetPronounProvider(false).Objective,
					" Become by installing ",
					Cybernetic.a,
					Cybernetic.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true),
					" implant."
				}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
			}
		}

		// Token: 0x060077E4 RID: 30692 RVA: 0x002D1224 File Offset: 0x002CF424
		public bool Equip(GameObject Item, int? EnergyCost = null, bool Silent = false, bool ForDeepCopy = false, bool Forced = false, bool SemiForced = false)
		{
			Event @event = Event.New("CommandEquipObject", 0, 0, 0);
			if (Silent)
			{
				@event.SetSilent(true);
			}
			if (ForDeepCopy)
			{
				@event.SetFlag("ForDeepCopy", true);
			}
			@event.SetParameter("Object", Item);
			@event.SetParameter("BodyPart", this);
			if (Forced)
			{
				@event.SetFlag("Forced", true);
			}
			if (SemiForced)
			{
				@event.SetFlag("SemiForced", true);
			}
			if (EnergyCost != null)
			{
				@event.SetParameter("EnergyCost", EnergyCost.Value);
			}
			return this.ParentBody.ParentObject.FireEvent(@event);
		}

		// Token: 0x060077E5 RID: 30693 RVA: 0x002D12C4 File Offset: 0x002CF4C4
		public bool DoEquip(GameObject GO, bool Silent = false, bool ForDeepCopy = false)
		{
			string text = null;
			bool flag = this.DoEquip(GO, ref text, Silent, ForDeepCopy, false, 0, null);
			if (!text.IsNullOrEmpty() && !Silent)
			{
				Body parentBody = this.ParentBody;
				bool flag2;
				if (parentBody == null)
				{
					flag2 = false;
				}
				else
				{
					GameObject parentObject = parentBody.ParentObject;
					bool? flag3 = ((parentObject != null) ? new bool?(parentObject.IsPlayer()) : null);
					bool flag4 = true;
					flag2 = (flag3.GetValueOrDefault() == flag4) & (flag3 != null);
				}
				if (flag2)
				{
					Popup.ShowFail(text, true, true, true);
				}
			}
			return flag;
		}

		// Token: 0x060077E6 RID: 30694 RVA: 0x002D1338 File Offset: 0x002CF538
		public bool DoEquip(GameObject GO, ref string FailureMessage, bool Silent = false, bool ForDeepCopy = false, bool UnequipOthers = false, int AutoEquipTry = 0, List<GameObject> WasUnequipped = null)
		{
			if (this.ParentBody == null)
			{
				MetricsManager.LogError("Cannot DoEquip without parentbody on part " + this.Name + " of type " + this.Type);
				return true;
			}
			this.ParentBody.WeightCache = -1;
			bool flag = GO.EquipAsDefaultBehavior();
			string usesSlots = GO.UsesSlots;
			if (!usesSlots.IsNullOrEmpty() && (!this.Thrown || usesSlots.Contains("Thrown Weapon")) && (!(this.Type == "Hand") || usesSlots.Contains("Hand")))
			{
				if (this.ParentBody.ParentObject.IsGiganticCreature)
				{
					if (this.Type != "Floating Nearby" && !GO.IsGiganticEquipment && !GO.HasPropertyOrTag("GiganticEquippable") && !GO.IsNatural())
					{
						FailureMessage = "This item is too small for you to equip.";
						return false;
					}
				}
				else if (this.Type != "Hand" && this.Type != "Missile Weapon" && this.Type != "Floating Nearby" && GO.IsGiganticEquipment && !GO.IsNatural())
				{
					FailureMessage = "This item is too large for you to equip.";
					return false;
				}
				List<string> list = usesSlots.CachedCommaExpansion();
				bool flag2 = true;
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					string text = list[i];
					string text2 = text;
					int codeFromAdjective = XRL.World.Capabilities.Laterality.GetCodeFromAdjective(ref text2);
					if ((text2 == this.Type || text2 == this.VariantType) && (codeFromAdjective == 0 || (this.Laterality & codeFromAdjective) == codeFromAdjective) && flag2)
					{
						flag2 = false;
					}
					else
					{
						int num = this.ParentBody.GetUnequippedPartCountExcept(text2, codeFromAdjective, this);
						int num2 = 0;
						bool flag3 = true;
						foreach (string text3 in list)
						{
							if ((text3 == this.Type || text3 == this.VariantType) && flag3)
							{
								flag3 = false;
							}
							else if (text3 == text2)
							{
								num2++;
							}
						}
						if (num2 > num)
						{
							if (UnequipOthers)
							{
								foreach (BodyPart bodyPart in this.ParentBody.GetPart(text2, codeFromAdjective))
								{
									if (bodyPart != this)
									{
										GameObject equipped = bodyPart.Equipped;
										if (equipped != null)
										{
											Event @event = Event.New("CommandUnequipObject", "BodyPart", bodyPart);
											if (Silent)
											{
												@event.SetSilent(true);
											}
											if (AutoEquipTry > 0)
											{
												@event.SetParameter("AutoEquipTry", AutoEquipTry);
											}
											if (!FailureMessage.IsNullOrEmpty())
											{
												@event.SetParameter("FailureMessage", FailureMessage);
											}
											if (ForDeepCopy)
											{
												@event.SetFlag("ForDeepCopy", true);
											}
											if (this.ParentBody.ParentObject.FireEvent(@event))
											{
												if (bodyPart.Equipped == null)
												{
													if (WasUnequipped != null && GameObject.Validate(ref equipped))
													{
														WasUnequipped.Add(equipped);
													}
													num++;
													if (num >= num2)
													{
														break;
													}
												}
											}
											else
											{
												string stringParameter = @event.GetStringParameter("FailureMessage", null);
												if (!stringParameter.IsNullOrEmpty() && stringParameter != FailureMessage)
												{
													FailureMessage = stringParameter;
												}
											}
										}
									}
								}
							}
							if (num2 > num)
							{
								if (num2 == num + 1)
								{
									FailureMessage = string.Concat(new string[]
									{
										"You need ",
										(num > 0) ? "another" : "a",
										" free ",
										text,
										" slot to equip that!"
									});
								}
								else
								{
									FailureMessage = string.Concat(new string[]
									{
										"You need another ",
										Grammar.Cardinal(num2 - num),
										" free ",
										text,
										" slots to equip that!"
									});
								}
								return false;
							}
						}
					}
					i++;
				}
				bool flag4 = false;
				int j = 0;
				int count2 = list.Count;
				while (j < count2)
				{
					string text4 = list[j];
					string text5 = text4;
					int codeFromAdjective2 = XRL.World.Capabilities.Laterality.GetCodeFromAdjective(ref text5);
					List<BodyPart> part = this.ParentBody.GetPart(text5, codeFromAdjective2);
					if (!flag4 && part.Contains(this))
					{
						flag4 = true;
					}
					else
					{
						BodyPart bodyPart2 = null;
						int num3 = int.MaxValue;
						int k = 0;
						int count3 = part.Count;
						while (k < count3)
						{
							BodyPart bodyPart3 = part[k];
							if (bodyPart3.Equipped == null && bodyPart3 != this)
							{
								int num4 = Math.Abs(bodyPart3.Position - this.Position);
								if (num4 < num3)
								{
									bodyPart2 = bodyPart3;
									num3 = num4;
								}
							}
							k++;
						}
						if (bodyPart2 != null)
						{
							if (flag)
							{
								bodyPart2.DefaultBehavior = GO;
							}
							else
							{
								bodyPart2._Equipped = GO;
							}
						}
						else
						{
							MetricsManager.LogError("internal inconsistency, no closest " + text4 + " found for " + GO.DebugName);
						}
					}
					j++;
				}
			}
			else if (this.Thrown)
			{
				if (this.ParentBody.ParentObject.IsGiganticCreature)
				{
					if (!GO.IsGiganticEquipment && !GO.HasPropertyOrTag("GiganticEquippable") && !GO.IsNatural())
					{
						FailureMessage = "This item is too small for you to equip.";
						return false;
					}
				}
				else if (GO.IsGiganticEquipment && !GO.IsNatural())
				{
					FailureMessage = "This item is too large for you to equip.";
					return false;
				}
			}
			else
			{
				if (this.ParentBody.ParentObject.IsGiganticCreature && this.Type != "Floating Nearby" && !GO.IsGiganticEquipment && !GO.HasPropertyOrTag("GiganticEquippable") && !GO.IsNatural())
				{
					FailureMessage = "This item is too small for you to equip.";
					return false;
				}
				int slotsRequiredFor = GO.GetSlotsRequiredFor(this.ParentBody.ParentObject, this.Type, false);
				if (slotsRequiredFor < 1)
				{
					FailureMessage = "This item is too small for you to equip.";
					return false;
				}
				if (slotsRequiredFor == 2)
				{
					List<BodyPart> part2 = this.ParentBody.GetPart(this.Type);
					BodyPart bodyPart4 = null;
					foreach (BodyPart bodyPart5 in part2)
					{
						if (bodyPart5 != this && bodyPart5.Equipped == null)
						{
							bodyPart4 = bodyPart5;
							break;
						}
					}
					if (bodyPart4 == null && UnequipOthers)
					{
						foreach (BodyPart bodyPart6 in part2)
						{
							if (bodyPart6 != this)
							{
								GameObject equipped2 = bodyPart6.Equipped;
								if (equipped2 != null)
								{
									Event event2 = Event.New("CommandUnequipObject", "BodyPart", bodyPart6);
									if (Silent)
									{
										event2.SetSilent(true);
									}
									if (AutoEquipTry > 0)
									{
										event2.SetParameter("AutoEquipTry", AutoEquipTry);
									}
									if (!FailureMessage.IsNullOrEmpty())
									{
										event2.SetParameter("FailureMessage", FailureMessage);
									}
									if (ForDeepCopy)
									{
										event2.SetFlag("ForDeepCopy", true);
									}
									if (this.ParentBody.ParentObject.FireEvent(event2))
									{
										if (bodyPart6.Equipped == null)
										{
											if (WasUnequipped != null && GameObject.Validate(ref equipped2))
											{
												WasUnequipped.Add(equipped2);
											}
											bodyPart4 = bodyPart6;
											break;
										}
									}
									else
									{
										string stringParameter2 = event2.GetStringParameter("FailureMessage", null);
										if (!stringParameter2.IsNullOrEmpty() && stringParameter2 != FailureMessage)
										{
											FailureMessage = stringParameter2;
										}
									}
								}
							}
						}
					}
					if (bodyPart4 == null)
					{
						FailureMessage = "You need another free " + Anatomies.GetBodyPartType(this.Type).Type + " slot to equip that!";
						return false;
					}
					if (flag)
					{
						bodyPart4.DefaultBehavior = GO;
					}
					else
					{
						bodyPart4._Equipped = GO;
					}
				}
				else if (slotsRequiredFor > 2)
				{
					List<BodyPart> part3 = this.ParentBody.GetPart(this.Type);
					List<BodyPart> list2 = new List<BodyPart>();
					foreach (BodyPart bodyPart7 in part3)
					{
						if (bodyPart7 != this && bodyPart7.Equipped == null)
						{
							list2.Add(bodyPart7);
							if (list2.Count >= slotsRequiredFor - 1)
							{
								break;
							}
						}
					}
					if (list2.Count < slotsRequiredFor - 1 && UnequipOthers)
					{
						foreach (BodyPart bodyPart8 in part3)
						{
							if (bodyPart8 != this)
							{
								GameObject equipped3 = bodyPart8.Equipped;
								if (equipped3 != null)
								{
									Event event3 = Event.New("CommandUnequipObject", "BodyPart", bodyPart8);
									if (Silent)
									{
										event3.SetSilent(true);
									}
									if (AutoEquipTry > 0)
									{
										event3.SetParameter("AutoEquipTry", AutoEquipTry);
									}
									if (!FailureMessage.IsNullOrEmpty())
									{
										event3.SetParameter("FailureMessage", FailureMessage);
									}
									if (ForDeepCopy)
									{
										event3.SetFlag("ForDeepCopy", true);
									}
									if (this.ParentBody.ParentObject.FireEvent(event3))
									{
										if (bodyPart8.Equipped == null)
										{
											if (WasUnequipped != null && GameObject.Validate(ref equipped3))
											{
												WasUnequipped.Add(equipped3);
											}
											list2.Add(bodyPart8);
											if (list2.Count >= slotsRequiredFor - 1)
											{
												break;
											}
										}
									}
									else
									{
										string stringParameter3 = event3.GetStringParameter("FailureMessage", null);
										if (!stringParameter3.IsNullOrEmpty() && stringParameter3 != FailureMessage)
										{
											FailureMessage = stringParameter3;
										}
									}
								}
							}
						}
					}
					int num5 = slotsRequiredFor - list2.Count - 1;
					if (num5 > 0)
					{
						if (num5 == 1)
						{
							FailureMessage = "You need another free " + Anatomies.GetBodyPartType(this.Type).Type + " slot to equip that!";
						}
						else
						{
							FailureMessage = string.Concat(new string[]
							{
								"You need another ",
								num5.ToString(),
								" free ",
								Anatomies.GetBodyPartType(this.Type).Type,
								" slots to equip that!"
							});
						}
						return false;
					}
					if (flag)
					{
						using (List<BodyPart>.Enumerator enumerator2 = list2.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								BodyPart bodyPart9 = enumerator2.Current;
								bodyPart9.DefaultBehavior = GO;
							}
							goto IL_09EC;
						}
					}
					foreach (BodyPart bodyPart10 in list2)
					{
						bodyPart10._Equipped = GO;
					}
				}
			}
			IL_09EC:
			this.ParentBody.WeightCache = -1;
			if (flag)
			{
				this.DefaultBehavior = GO;
				this.ParentBody.RecalculateFirstDefaultBehavior(GO);
			}
			else
			{
				this._Equipped = GO;
				this.ParentBody.RecalculateFirstEquipped(GO);
			}
			this.FlushTransientCaches();
			return true;
		}

		// Token: 0x060077E7 RID: 30695 RVA: 0x002D1E34 File Offset: 0x002D0034
		public void RecalculateFirstEquipped(GameObject Object, ref bool Found)
		{
			if (this._Equipped == null)
			{
				this.FirstSlotForEquipped = false;
			}
			else if (this._Equipped == Object)
			{
				this.FirstSlotForEquipped = !Found;
				Found = true;
			}
			List<BodyPart> parts = this.Parts;
			int num = ((parts != null) ? parts.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this.Parts[i].RecalculateFirstEquipped(Object, ref Found);
			}
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x002D1E9C File Offset: 0x002D009C
		public void RecalculateFirstCybernetics(GameObject Object, ref bool Found)
		{
			if (this._Cybernetics == null)
			{
				this.FirstSlotForCybernetics = false;
			}
			else if (this._Cybernetics == Object)
			{
				this.FirstSlotForCybernetics = !Found;
				Found = true;
			}
			List<BodyPart> parts = this.Parts;
			int num = ((parts != null) ? parts.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this.Parts[i].RecalculateFirstCybernetics(Object, ref Found);
			}
		}

		// Token: 0x060077E9 RID: 30697 RVA: 0x002D1F04 File Offset: 0x002D0104
		public void RecalculateFirstDefaultBehavior(GameObject Object, ref bool Found)
		{
			if (this._DefaultBehavior == null)
			{
				this.FirstSlotForDefaultBehavior = false;
			}
			else if (this._DefaultBehavior == Object)
			{
				this.FirstSlotForDefaultBehavior = !Found;
				Found = true;
			}
			List<BodyPart> parts = this.Parts;
			int num = ((parts != null) ? parts.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this.Parts[i].RecalculateFirstDefaultBehavior(Object, ref Found);
			}
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x002D1F6B File Offset: 0x002D016B
		private void ClearEquipped()
		{
			if (this.ParentBody != null)
			{
				this.ParentBody.WeightCache = -1;
			}
			this._Equipped = null;
			this.FirstSlotForEquipped = false;
			this.FlushTransientCaches();
		}

		// Token: 0x060077EB RID: 30699 RVA: 0x002D1F98 File Offset: 0x002D0198
		public bool TryUnequip(bool Silent = false, bool SemiForced = false, bool NoStack = false, bool NoTake = false)
		{
			Event @event = Event.New("CommandUnequipObject", "BodyPart", this);
			if (SemiForced)
			{
				@event.SetFlag("SemiForced", true);
			}
			if (NoStack)
			{
				@event.SetFlag("NoStack", true);
			}
			if (NoTake)
			{
				@event.SetFlag("NoTake", true);
			}
			if (Silent)
			{
				@event.SetSilent(true);
			}
			return this.ParentBody.ParentObject.FireEvent(@event);
		}

		// Token: 0x060077EC RID: 30700 RVA: 0x002D2004 File Offset: 0x002D0204
		public bool ForceUnequip(bool Silent = false, bool NoStack = false, bool NoTake = false)
		{
			Event @event = Event.New("CommandForceUnequipObject", "BodyPart", this);
			if (NoStack)
			{
				@event.SetFlag("NoStack", true);
			}
			if (NoTake)
			{
				@event.SetFlag("NoTake", true);
			}
			if (Silent)
			{
				@event.SetSilent(true);
			}
			return this.ParentBody.ParentObject.FireEvent(@event);
		}

		// Token: 0x060077ED RID: 30701 RVA: 0x002D2060 File Offset: 0x002D0260
		public void Unequip()
		{
			if (this.Equipped == null)
			{
				return;
			}
			GameObject equipped = this.Equipped;
			BodyPart.ToClearEquip.Clear();
			if (this.ParentBody == null)
			{
				this.FindPartsEquipping(equipped, BodyPart.ToClearEquip);
			}
			else
			{
				this.ParentBody.FindPartsEquipping(equipped, BodyPart.ToClearEquip);
			}
			int i = 0;
			int count = BodyPart.ToClearEquip.Count;
			while (i < count)
			{
				if (BodyPart.ToClearEquip[i].Equipped == equipped)
				{
					BodyPart.ToClearEquip[i].ClearEquipped();
				}
				i++;
			}
			BodyPart.ToClearEquip.Clear();
		}

		// Token: 0x060077EE RID: 30702 RVA: 0x002D20F4 File Offset: 0x002D02F4
		private GameObject MakeCopyWithMap(GameObject obj, Func<GameObject, GameObject> map)
		{
			if (map != null)
			{
				GameObject gameObject = map(obj);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			return obj.DeepCopy(false, false, map);
		}

		// Token: 0x060077EF RID: 30703 RVA: 0x002D211C File Offset: 0x002D031C
		public BodyPart DeepCopy(GameObject ParentObject = null, Body ParentBody = null, BodyPart ParentPart = null, Func<GameObject, GameObject> MapInv = null, bool CopyGameObjects = true)
		{
			BodyPart bodyPart = new BodyPart();
			bodyPart.Type = this.Type;
			bodyPart.VariantType = this.VariantType;
			bodyPart.Description = this.Description;
			bodyPart.DescriptionPrefix = this.DescriptionPrefix;
			bodyPart.Name = this.Name;
			bodyPart.SupportsDependent = this.SupportsDependent;
			bodyPart.DependsOn = this.DependsOn;
			bodyPart.RequiresType = this.RequiresType;
			bodyPart.Manager = this.Manager;
			bodyPart.Category = this.Category;
			bodyPart.Laterality = this.Laterality;
			bodyPart.RequiresLaterality = this.RequiresLaterality;
			bodyPart.Mobility = this.Mobility;
			bodyPart.Flags = this.Flags;
			bodyPart.Position = this.Position;
			bodyPart._ID = this._ID;
			bodyPart.ParentBody = ParentBody;
			bodyPart.ParentPart = ParentPart;
			bodyPart.DefaultBehaviorBlueprint = this.DefaultBehaviorBlueprint;
			if (CopyGameObjects && ParentObject != null)
			{
				if (this.DefaultBehavior != null)
				{
					GameObject gameObject = this.MakeCopyWithMap(this.DefaultBehavior, MapInv);
					if (gameObject.Physics != null)
					{
						ParentObject.DeepCopyInventoryObjectMap[this.DefaultBehavior] = gameObject;
						gameObject.Physics._Equipped = ParentObject;
						bodyPart.DefaultBehavior = gameObject;
						if (this.DefaultBehavior == this.Equipped || this.DefaultBehavior == this.Cybernetics)
						{
							Body.DeepCopyEquipMap[gameObject] = bodyPart;
						}
					}
				}
				if (this.Equipped != null && !ParentObject.DeepCopyInventoryObjectMap.ContainsKey(this.Equipped))
				{
					GameObject gameObject2 = this.MakeCopyWithMap(this.Equipped, MapInv);
					ParentObject.DeepCopyInventoryObjectMap[this.Equipped] = gameObject2;
					Body.DeepCopyEquipMap[gameObject2] = bodyPart;
				}
				if (this.Cybernetics != null && this.Cybernetics != this.Equipped && !ParentObject.DeepCopyInventoryObjectMap.ContainsKey(this.Cybernetics))
				{
					GameObject gameObject3 = this.MakeCopyWithMap(this.Cybernetics, MapInv);
					ParentObject.DeepCopyInventoryObjectMap[this.Cybernetics] = gameObject3;
					Body.DeepCopyEquipMap[gameObject3] = bodyPart;
				}
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart2 in this.Parts)
				{
					if (!bodyPart2.Extrinsic)
					{
						bodyPart.AddPart(bodyPart2.DeepCopy(ParentObject, ParentBody, this, MapInv, CopyGameObjects), -1, true);
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x060077F0 RID: 30704 RVA: 0x002D238C File Offset: 0x002D058C
		[Obsolete("Use Write(SerializationWriter)")]
		public void Save(SerializationWriter Writer)
		{
			this.Write(Writer);
		}

		// Token: 0x060077F1 RID: 30705 RVA: 0x002D2395 File Offset: 0x002D0595
		[Obsolete("Use Read(SerializationReader)")]
		public static BodyPart Load(SerializationReader Reader, Body ParentBody)
		{
			BodyPart bodyPart = new BodyPart();
			bodyPart.ParentBody = ParentBody;
			bodyPart.Read(Reader);
			return bodyPart;
		}

		// Token: 0x060077F2 RID: 30706 RVA: 0x002D23AC File Offset: 0x002D05AC
		public void Write(SerializationWriter Writer)
		{
			this.WriteValues(Writer);
			Writer.WriteGameObject(this._DefaultBehavior);
			Writer.WriteGameObject(this._Equipped);
			Writer.WriteGameObject(this._Cybernetics);
			List<BodyPart> parts = this.Parts;
			int num = ((parts != null) ? parts.Count : 0);
			Writer.WriteOptimized(num);
			for (int i = 0; i < num; i++)
			{
				this.Parts[i].Write(Writer);
			}
		}

		// Token: 0x060077F3 RID: 30707 RVA: 0x002D241C File Offset: 0x002D061C
		public void Read(SerializationReader Reader)
		{
			this.ReadValues(Reader);
			this._DefaultBehavior = Reader.ReadGameObject(null);
			this._Equipped = Reader.ReadGameObject(null);
			this._Cybernetics = Reader.ReadGameObject(null);
			this.Thrown = this.Type == "Thrown Weapon";
			int num = Reader.ReadOptimizedInt32();
			if (num > 0)
			{
				this.Parts = new List<BodyPart>(num);
				for (int i = 0; i < num; i++)
				{
					BodyPart bodyPart = new BodyPart(this.ParentBody, this);
					bodyPart.Read(Reader);
					this.Parts.Add(bodyPart);
				}
			}
		}

		// Token: 0x060077F4 RID: 30708 RVA: 0x002D24B0 File Offset: 0x002D06B0
		public int GetWeight()
		{
			int num = 0;
			if (this.Equipped != null && this.FirstSlotForEquipped)
			{
				CyberneticsBaseItem part = this.Equipped.GetPart<CyberneticsBaseItem>();
				if (part != null)
				{
					if (part.ImplantedOn == null)
					{
						num += this.Equipped.Weight;
					}
				}
				else
				{
					num += this.Equipped.Weight;
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetWeight();
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x060077F5 RID: 30709 RVA: 0x002D2548 File Offset: 0x002D0748
		public void Clear()
		{
			this.DefaultBehavior = null;
			this.ClearEquipped();
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						bodyPart.Clear();
					}
				}
			}
			this.Parts = null;
		}

		// Token: 0x060077F6 RID: 30710 RVA: 0x002D25A0 File Offset: 0x002D07A0
		public void StripAllEquipment(List<GameObject> Return)
		{
			if (this.Equipped != null)
			{
				Return.Add(this.Equipped);
				BeforeUnequippedEvent.Send(this.Equipped, this.ParentBody.ParentObject);
				UnequippedEvent.Send(this.Equipped, this.ParentBody.ParentObject);
				this.Unequip();
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in new List<BodyPart>(this.Parts))
				{
					bodyPart.StripAllEquipment(Return);
				}
			}
		}

		// Token: 0x060077F7 RID: 30711 RVA: 0x002D2644 File Offset: 0x002D0844
		public void GetPrimaryHandEquippedObjects(List<GameObject> Return)
		{
			if (this.Primary && this.Type == "Hand")
			{
				if (this.Equipped != null)
				{
					Return.Add(this.Equipped);
				}
				else if (this.DefaultBehavior != null)
				{
					Return.Add(this.DefaultBehavior);
				}
			}
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						bodyPart.GetPrimaryHandEquippedObjects(Return);
					}
				}
			}
		}

		// Token: 0x060077F8 RID: 30712 RVA: 0x002D26CC File Offset: 0x002D08CC
		public void GetPrimaryEquippedObjects(List<GameObject> Return)
		{
			if (this.Primary)
			{
				if (this.Equipped != null)
				{
					Return.Add(this.Equipped);
				}
				else if (this.DefaultBehavior != null)
				{
					Return.Add(this.DefaultBehavior);
				}
			}
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						bodyPart.GetPrimaryEquippedObjects(Return);
					}
				}
			}
		}

		// Token: 0x060077F9 RID: 30713 RVA: 0x002D2740 File Offset: 0x002D0940
		public void GetShield(ref GameObject obj, Predicate<GameObject> Filter, GameObject Attacker, GameObject Defender)
		{
			if (this.Equipped != null)
			{
				Shield part = this.Equipped.GetPart<Shield>();
				if (part != null && (part.WornOn == "*" || part.WornOn == this.Type) && (Filter == null || Filter(this.Equipped)))
				{
					if (obj == null)
					{
						obj = this.Equipped;
					}
					else
					{
						int @for = GetShieldBlockPreferenceEvent.GetFor(this.Equipped, Attacker, Defender, 0);
						int for2 = GetShieldBlockPreferenceEvent.GetFor(obj, Attacker, Defender, 0);
						if (@for > for2)
						{
							obj = this.Equipped;
						}
					}
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].GetShield(ref obj, Filter, Attacker, Defender);
					i++;
				}
			}
		}

		// Token: 0x060077FA RID: 30714 RVA: 0x002D2804 File Offset: 0x002D0A04
		public void GetShieldWithHighestAV(ref GameObject obj, ref int highestAV, Predicate<GameObject> Filter, GameObject Attacker, GameObject Defender)
		{
			if (this.Equipped != null)
			{
				Shield part = this.Equipped.GetPart<Shield>();
				if (part != null && (part.WornOn == "*" || part.WornOn == this.Type) && (Filter == null || Filter(this.Equipped)) && (obj == null || part.AV > highestAV || (part.AV == highestAV && 50.in100())))
				{
					obj = this.Equipped;
					highestAV = part.AV;
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].GetShieldWithHighestAV(ref obj, ref highestAV, Filter, Attacker, Defender);
					i++;
				}
			}
		}

		// Token: 0x060077FB RID: 30715 RVA: 0x002D28C8 File Offset: 0x002D0AC8
		public void SetAsPreferredDefault(bool force = false)
		{
			if (!force && this.ParentBody.ParentObject.IsPlayer())
			{
				if (The.Player.AreHostilesNearby())
				{
					Popup.Show("You can't switch primary limbs in combat.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				if (this.Abstract || this.Extrinsic)
				{
					Popup.Show("This body part cannot be set as your primary.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
			}
			this.ParentBody.ForeachPart(delegate(BodyPart p)
			{
				p.PreferredPrimary = false;
				return true;
			});
			this.PreferredPrimary = true;
			this.ParentBody.RecalculateFirsts();
		}

		// Token: 0x060077FC RID: 30716 RVA: 0x002D2970 File Offset: 0x002D0B70
		public void SetPrimaryScan(string WantType, ref bool PrimarySet, ref bool HasPreferredPrimary, ref BodyPart CurrentPrimaryPart)
		{
			if (PrimarySet)
			{
				this.Primary = false;
				this.DefaultPrimary = false;
			}
			else if (this.Type == WantType)
			{
				PrimarySet = true;
				this.DefaultPrimary = true;
				if (CurrentPrimaryPart == null)
				{
					this.Primary = true;
					CurrentPrimaryPart = this;
				}
			}
			if (this.PreferredPrimary)
			{
				HasPreferredPrimary = true;
				this.Primary = true;
				if (CurrentPrimaryPart != null && CurrentPrimaryPart != this)
				{
					CurrentPrimaryPart.Primary = false;
				}
				CurrentPrimaryPart = this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].SetPrimaryScan(WantType, ref PrimarySet, ref HasPreferredPrimary, ref CurrentPrimaryPart);
					i++;
				}
			}
		}

		// Token: 0x060077FD RID: 30717 RVA: 0x002D2A19 File Offset: 0x002D0C19
		private int GetPrimaryPickPriority()
		{
			if (this.PreferredPrimary && !this.DefaultPrimary)
			{
				return 20;
			}
			if (this.Equipped != null)
			{
				return 10;
			}
			return 8;
		}

		// Token: 0x060077FE RID: 30718 RVA: 0x002D2A3C File Offset: 0x002D0C3C
		public void ScanForWeapon(bool NeedPrimary, GameObject Target, ref GameObject Weapon, ref BodyPart EquippedOn, ref int PickPriority, ref int PossibleWeapons, ref bool HadPrimary, List<BodyPart> PartList = null)
		{
			GameObject gameObject = this.Equipped ?? this.DefaultBehavior;
			if (gameObject != null)
			{
				if (gameObject == Weapon)
				{
					if (this.Primary && !HadPrimary)
					{
						HadPrimary = true;
						PickPriority = this.GetPrimaryPickPriority();
					}
				}
				else
				{
					if (this.Equipped != null && this.DefaultBehavior != null && PreferDefaultBehaviorEvent.Check(this.ParentBody.ParentObject, Target, this))
					{
						gameObject = this.DefaultBehavior;
					}
					MeleeWeapon part = gameObject.GetPart<MeleeWeapon>();
					if (part != null && part.AttackFromPart(this))
					{
						PossibleWeapons++;
						if (PartList != null)
						{
							PartList.Add(this);
						}
						int num;
						if (this.Primary)
						{
							num = this.GetPrimaryPickPriority();
						}
						else if (gameObject.HasTag("PreferredDefault"))
						{
							num = 9;
						}
						else if (this.Type == "Hand")
						{
							num = 7;
						}
						else if (this != this.ParentBody._Body)
						{
							num = 6;
						}
						else
						{
							num = 5;
						}
						if (gameObject == this.DefaultBehavior && gameObject.HasTag("UndesirableWeapon"))
						{
							num -= 4;
						}
						if (Target != null && Target.IsDiggable() && gameObject != null && gameObject.HasPart<DiggingTool>())
						{
							num = this.GetPrimaryPickPriority() + 1;
						}
						bool flag = false;
						if (Weapon == null || num > PickPriority)
						{
							flag = true;
						}
						else if (num == PickPriority && Brain.IsNewWeaponBetter(gameObject, Weapon, this.ParentBody.ParentObject))
						{
							flag = true;
						}
						if (flag)
						{
							Weapon = gameObject;
							EquippedOn = this;
							PickPriority = num;
							HadPrimary = this.Primary;
						}
					}
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						bodyPart.ScanForWeapon(NeedPrimary, Target, ref Weapon, ref EquippedOn, ref PickPriority, ref PossibleWeapons, ref HadPrimary, PartList);
					}
					i++;
				}
			}
		}

		// Token: 0x060077FF RID: 30719 RVA: 0x002D2BFC File Offset: 0x002D0DFC
		public GameObject ThisPartWeaponOfType(string WeaponType, bool NeedPrimary)
		{
			if (!NeedPrimary || this.Primary || this.PreferredPrimary)
			{
				if (this.Equipped != null)
				{
					MeleeWeapon part = this.Equipped.GetPart<MeleeWeapon>();
					if (part != null && WeaponType == part.Skill && part.AttackFromPart(this))
					{
						return this.Equipped;
					}
				}
				else if (this.DefaultBehavior != null)
				{
					MeleeWeapon part2 = this.DefaultBehavior.GetPart<MeleeWeapon>();
					if (part2 != null && WeaponType == part2.Skill && part2.AttackFromPart(this))
					{
						return this.DefaultBehavior;
					}
				}
			}
			return null;
		}

		// Token: 0x06007800 RID: 30720 RVA: 0x002D2C88 File Offset: 0x002D0E88
		public GameObject ThisPartWeapon(Predicate<GameObject> Filter = null)
		{
			if (this.Equipped != null)
			{
				MeleeWeapon part = this.Equipped.GetPart<MeleeWeapon>();
				if (part != null && (Filter == null || Filter(this.Equipped)) && part.AttackFromPart(this))
				{
					return this.Equipped;
				}
			}
			else if (this.DefaultBehavior != null)
			{
				MeleeWeapon part2 = this.DefaultBehavior.GetPart<MeleeWeapon>();
				if (part2 != null && (Filter == null || Filter(this.DefaultBehavior)) && part2.AttackFromPart(this))
				{
					return this.DefaultBehavior;
				}
			}
			return null;
		}

		// Token: 0x06007801 RID: 30721 RVA: 0x002D2D08 File Offset: 0x002D0F08
		public GameObject GetWeaponOfType(string WeaponType, bool NeedPrimary)
		{
			GameObject gameObject = this.ThisPartWeaponOfType(WeaponType, NeedPrimary);
			if (gameObject != null)
			{
				return gameObject;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						gameObject = bodyPart.GetWeaponOfType(WeaponType, NeedPrimary);
						if (gameObject != null)
						{
							return gameObject;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007802 RID: 30722 RVA: 0x002D2D64 File Offset: 0x002D0F64
		public bool HasWeaponOfType(string WeaponType, bool NeedPrimary)
		{
			if (this.ThisPartWeaponOfType(WeaponType, NeedPrimary) != null)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null && bodyPart.HasWeaponOfType(WeaponType, NeedPrimary))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06007803 RID: 30723 RVA: 0x002D2DBC File Offset: 0x002D0FBC
		public GameObject GetWeaponOfTypeOnBodyPartOfType(string WeaponType, string BodyPartType, bool NeedPrimary)
		{
			if (this.Type == BodyPartType)
			{
				GameObject gameObject = this.ThisPartWeaponOfType(WeaponType, NeedPrimary);
				if (gameObject != null)
				{
					return gameObject;
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						GameObject weaponOfTypeOnBodyPartOfType = bodyPart.GetWeaponOfTypeOnBodyPartOfType(WeaponType, BodyPartType, NeedPrimary);
						if (weaponOfTypeOnBodyPartOfType != null)
						{
							return weaponOfTypeOnBodyPartOfType;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007804 RID: 30724 RVA: 0x002D2E28 File Offset: 0x002D1028
		public bool HasWeaponOfTypeOnBodyPartOfType(string WeaponType, string BodyPartType, bool NeedPrimary)
		{
			if (this.Type == BodyPartType && this.ThisPartWeaponOfType(WeaponType, NeedPrimary) != null)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null && bodyPart.HasWeaponOfType(WeaponType, NeedPrimary))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06007805 RID: 30725 RVA: 0x002D2E8C File Offset: 0x002D108C
		public GameObject GetWeapon(Predicate<GameObject> Filter = null)
		{
			GameObject gameObject = this.ThisPartWeapon(Filter);
			if (gameObject != null)
			{
				return gameObject;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						GameObject weapon = bodyPart.GetWeapon(Filter);
						if (weapon != null)
						{
							return weapon;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007806 RID: 30726 RVA: 0x002D2EE8 File Offset: 0x002D10E8
		public bool HasWeapon(Predicate<GameObject> Filter = null)
		{
			if (this.ThisPartWeapon(Filter) != null)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null && bodyPart.HasWeapon(Filter))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06007807 RID: 30727 RVA: 0x002D2F3C File Offset: 0x002D113C
		public bool IsPrimaryWeapon(GameObject Object)
		{
			if (this.Primary && (Object == this.Equipped || Object == this.DefaultBehavior))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].IsPrimaryWeapon(Object))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06007808 RID: 30728 RVA: 0x002D2FAC File Offset: 0x002D11AC
		public GameObject GetFirstValidWeapon(GameObject Target = null)
		{
			GameObject gameObject = null;
			GameObject defaultBehavior = this.DefaultBehavior;
			MeleeWeapon meleeWeapon = ((defaultBehavior != null) ? defaultBehavior.GetPart<MeleeWeapon>() : null);
			if (meleeWeapon != null && meleeWeapon.AttackFromPart(this))
			{
				gameObject = this.DefaultBehavior;
			}
			GameObject equipped = this.Equipped;
			MeleeWeapon meleeWeapon2 = ((equipped != null) ? equipped.GetPart<MeleeWeapon>() : null);
			if (meleeWeapon2 != null && meleeWeapon2.AttackFromPart(this))
			{
				if (gameObject == null)
				{
					return this.Equipped;
				}
				if (!PreferDefaultBehaviorEvent.Check(this.ParentBody.ParentObject, Target, this))
				{
					return this.Equipped;
				}
			}
			return gameObject;
		}

		// Token: 0x06007809 RID: 30729 RVA: 0x002D3028 File Offset: 0x002D1228
		public void ClearShieldBlocks()
		{
			if (this.Equipped != null)
			{
				Shield part = this.Equipped.GetPart<Shield>();
				if (part != null)
				{
					part.Blocks = 0;
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						bodyPart.ClearShieldBlocks();
					}
					i++;
				}
			}
		}

		// Token: 0x0600780A RID: 30730 RVA: 0x002D308C File Offset: 0x002D128C
		public int GetEquippedObjectCount()
		{
			int num = 0;
			if (this.Equipped != null && this.FirstSlotForEquipped)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetEquippedObjectCount();
					}
				}
			}
			return num;
		}

		// Token: 0x0600780B RID: 30731 RVA: 0x002D3104 File Offset: 0x002D1304
		public int GetEquippedObjectCount(Predicate<GameObject> Filter)
		{
			int num = 0;
			if (this.Equipped != null && Filter(this.Equipped) && this.FirstSlotForEquipped)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetEquippedObjectCount(Filter);
					}
				}
			}
			return num;
		}

		// Token: 0x0600780C RID: 30732 RVA: 0x002D318C File Offset: 0x002D138C
		public void GetEquippedObjects(List<GameObject> Return)
		{
			if (this.Equipped != null && !Return.Contains(this.Equipped))
			{
				Return.Add(this.Equipped);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetEquippedObjects(Return);
					}
				}
			}
		}

		// Token: 0x0600780D RID: 30733 RVA: 0x002D320C File Offset: 0x002D140C
		public List<GameObject> GetEquippedObjectsReadonly()
		{
			List<GameObject> list = null;
			if (this.Equipped != null)
			{
				list = Event.NewGameObjectList();
				list.Add(this.Equipped);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						if (list == null)
						{
							list = bodyPart.GetEquippedObjectsReadonly();
						}
						else
						{
							bodyPart.GetEquippedObjects(list);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600780E RID: 30734 RVA: 0x002D3294 File Offset: 0x002D1494
		public List<GameObject> GetEquippedObjectsReadonly(Predicate<GameObject> Filter)
		{
			if (Filter == null)
			{
				return this.GetEquippedObjectsReadonly();
			}
			List<GameObject> list = null;
			if (this.Equipped != null && Filter(this.Equipped))
			{
				list = Event.NewGameObjectList();
				list.Add(this.Equipped);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						if (list == null)
						{
							list = bodyPart.GetEquippedObjectsReadonly(Filter);
						}
						else
						{
							bodyPart.GetEquippedObjects(list, Filter);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600780F RID: 30735 RVA: 0x002D3338 File Offset: 0x002D1538
		public void GetEquippedObjects(List<GameObject> Return, Predicate<GameObject> Filter)
		{
			if (this.Equipped != null && !Return.Contains(this.Equipped) && Filter(this.Equipped))
			{
				Return.Add(this.Equipped);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetEquippedObjects(Return, Filter);
					}
				}
			}
		}

		// Token: 0x06007810 RID: 30736 RVA: 0x002D33C8 File Offset: 0x002D15C8
		public void GetEquippedObjectsExceptNatural(IList<GameObject> Return)
		{
			if (this.Equipped != null && !Return.Contains(this.Equipped) && !this.Equipped.HasTag("NaturalGear"))
			{
				Return.Add(this.Equipped);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetEquippedObjectsExceptNatural(Return);
					}
				}
			}
		}

		// Token: 0x06007811 RID: 30737 RVA: 0x002D345C File Offset: 0x002D165C
		public void GetEquippedObjectsExceptNatural(List<GameObject> Return, Predicate<GameObject> Filter)
		{
			if (this.Equipped != null && !Return.Contains(this.Equipped) && !this.Equipped.HasTag("NaturalGear") && Filter(this.Equipped))
			{
				Return.Add(this.Equipped);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetEquippedObjectsExceptNatural(Return, Filter);
					}
				}
			}
		}

		// Token: 0x06007812 RID: 30738 RVA: 0x002D3500 File Offset: 0x002D1700
		public void ForeachEquippedObject(Action<GameObject> Proc)
		{
			if (this.Equipped != null && this.FirstSlotForEquipped)
			{
				Proc(this.Equipped);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						bodyPart.ForeachEquippedObject(Proc);
					}
					i++;
				}
			}
		}

		// Token: 0x06007813 RID: 30739 RVA: 0x002D3564 File Offset: 0x002D1764
		public void SafeForeachEquippedObject(Action<GameObject> Proc)
		{
			if (this.Equipped != null && this.FirstSlotForEquipped)
			{
				Proc(this.Equipped);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in new List<BodyPart>(this.Parts))
				{
					if (bodyPart != null)
					{
						bodyPart.SafeForeachEquippedObject(Proc);
					}
				}
			}
		}

		// Token: 0x06007814 RID: 30740 RVA: 0x002D35E4 File Offset: 0x002D17E4
		public void ForeachDefaultBehavior(Action<GameObject> Proc)
		{
			if (this.DefaultBehavior != null && this.FirstSlotForDefaultBehavior)
			{
				Proc(this.DefaultBehavior);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null)
					{
						bodyPart.ForeachDefaultBehavior(Proc);
					}
					i++;
				}
			}
		}

		// Token: 0x06007815 RID: 30741 RVA: 0x002D3648 File Offset: 0x002D1848
		public void SafeForeachDefaultBehavior(Action<GameObject> Proc)
		{
			if (this.DefaultBehavior != null && this.FirstSlotForDefaultBehavior)
			{
				Proc(this.DefaultBehavior);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in new List<BodyPart>(this.Parts))
				{
					if (bodyPart != null)
					{
						bodyPart.SafeForeachDefaultBehavior(Proc);
					}
				}
			}
		}

		// Token: 0x06007816 RID: 30742 RVA: 0x002D36C8 File Offset: 0x002D18C8
		public bool HasInstalledCybernetics(string Blueprint = null, Predicate<GameObject> Filter = null)
		{
			if (this.Cybernetics != null && (Blueprint.IsNullOrEmpty() || this.Cybernetics.Blueprint == Blueprint) && (Filter == null || Filter(this.Cybernetics)))
			{
				return true;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null && bodyPart.HasInstalledCybernetics(Blueprint, Filter))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007817 RID: 30743 RVA: 0x002D3768 File Offset: 0x002D1968
		public int GetInstalledCyberneticsCount()
		{
			int num = 0;
			if (this.Cybernetics != null && this.ParentBody.FindCybernetics(this.Cybernetics) == this)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetInstalledCyberneticsCount();
					}
				}
			}
			return num;
		}

		// Token: 0x06007818 RID: 30744 RVA: 0x002D37EC File Offset: 0x002D19EC
		public int GetInstalledCyberneticsCount(Predicate<GameObject> Filter)
		{
			int num = 0;
			if (this.Cybernetics != null && Filter(this.Cybernetics) && this.ParentBody.FindCybernetics(this.Cybernetics) == this)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetInstalledCyberneticsCount(Filter);
					}
				}
			}
			return num;
		}

		// Token: 0x06007819 RID: 30745 RVA: 0x002D3880 File Offset: 0x002D1A80
		public void GetInstalledCybernetics(List<GameObject> Return)
		{
			if (this.Cybernetics != null && !Return.Contains(this.Cybernetics))
			{
				Return.Add(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetInstalledCybernetics(Return);
					}
				}
			}
		}

		// Token: 0x0600781A RID: 30746 RVA: 0x002D3900 File Offset: 0x002D1B00
		public List<GameObject> GetInstalledCyberneticsReadonly()
		{
			List<GameObject> list = null;
			if (this.Cybernetics != null)
			{
				list = Event.NewGameObjectList();
				list.Add(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						if (list == null)
						{
							list = bodyPart.GetInstalledCyberneticsReadonly();
						}
						else
						{
							bodyPart.GetInstalledCybernetics(list);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600781B RID: 30747 RVA: 0x002D3988 File Offset: 0x002D1B88
		public void GetInstalledCybernetics(List<GameObject> Return, Predicate<GameObject> Filter)
		{
			if (this.Cybernetics != null && !Return.Contains(this.Cybernetics) && Filter(this.Cybernetics))
			{
				Return.Add(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetInstalledCybernetics(Return, Filter);
					}
				}
			}
		}

		// Token: 0x0600781C RID: 30748 RVA: 0x002D3A18 File Offset: 0x002D1C18
		public bool AnyInstalledCybernetics()
		{
			if (this.Cybernetics != null)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null && bodyPart.AnyInstalledCybernetics())
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600781D RID: 30749 RVA: 0x002D3A6C File Offset: 0x002D1C6C
		public bool AnyInstalledCybernetics(Predicate<GameObject> Filter)
		{
			if (this.Cybernetics != null && Filter(this.Cybernetics))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null && bodyPart.AnyInstalledCybernetics(Filter))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600781E RID: 30750 RVA: 0x002D3AD0 File Offset: 0x002D1CD0
		public void ForeachInstalledCybernetics(Action<GameObject> aProc)
		{
			if (this.Cybernetics != null && this.ParentBody.FindCybernetics(this.Cybernetics) == this)
			{
				aProc(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.ForeachInstalledCybernetics(aProc);
					}
				}
			}
		}

		// Token: 0x0600781F RID: 30751 RVA: 0x002D3B58 File Offset: 0x002D1D58
		public void SafeForeachInstalledCybernetics(Action<GameObject> aProc)
		{
			if (this.Cybernetics != null && this.ParentBody.FindCybernetics(this.Cybernetics) == this)
			{
				aProc(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in new List<BodyPart>(this.Parts))
				{
					if (bodyPart != null)
					{
						bodyPart.ForeachInstalledCybernetics(aProc);
					}
				}
			}
		}

		// Token: 0x06007820 RID: 30752 RVA: 0x002D3BE4 File Offset: 0x002D1DE4
		public void GetEquippedObjectsAndInstalledCybernetics(List<GameObject> Return)
		{
			if (this.Equipped != null && !Return.Contains(this.Equipped))
			{
				Return.Add(this.Equipped);
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && !Return.Contains(this.Cybernetics))
			{
				Return.Add(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetEquippedObjectsAndInstalledCybernetics(Return);
					}
				}
			}
		}

		// Token: 0x06007821 RID: 30753 RVA: 0x002D3C94 File Offset: 0x002D1E94
		public List<GameObject> GetEquippedObjectsAndInstalledCyberneticsReadonly()
		{
			List<GameObject> list = null;
			if (this.Equipped != null)
			{
				list = Event.NewGameObjectList();
				list.Add(this.Equipped);
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped)
			{
				if (list == null)
				{
					list = Event.NewGameObjectList();
				}
				list.Add(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						if (list == null)
						{
							list = bodyPart.GetEquippedObjectsAndInstalledCyberneticsReadonly();
						}
						else
						{
							bodyPart.GetEquippedObjectsAndInstalledCybernetics(list);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06007822 RID: 30754 RVA: 0x002D3D48 File Offset: 0x002D1F48
		public void GetEquippedObjectsAndInstalledCybernetics(List<GameObject> Return, Predicate<GameObject> Filter)
		{
			if (this.Equipped != null && !Return.Contains(this.Equipped) && Filter(this.Equipped))
			{
				Return.Add(this.Equipped);
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && !Return.Contains(this.Cybernetics) && Filter(this.Cybernetics))
			{
				Return.Add(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.GetEquippedObjectsAndInstalledCybernetics(Return, Filter);
					}
				}
			}
		}

		// Token: 0x06007823 RID: 30755 RVA: 0x002D3E18 File Offset: 0x002D2018
		public bool Render(RenderEvent E)
		{
			if (this.Equipped != null)
			{
				this.Equipped.ComponentRender(E);
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped)
			{
				this.Cybernetics.ComponentRender(E);
			}
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].Render(E);
					}
				}
			}
			return true;
		}

		// Token: 0x06007824 RID: 30756 RVA: 0x002D3EA0 File Offset: 0x002D20A0
		public virtual bool AnyRegisteredEvent(string ID)
		{
			if (this.Equipped != null && this.Equipped.HasRegisteredEvent(ID))
			{
				return true;
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && this.Cybernetics.HasRegisteredEvent(ID))
			{
				return true;
			}
			if (this.DefaultBehavior != null && this.DefaultBehavior.HasRegisteredEvent(ID))
			{
				return true;
			}
			if (this.Parts != null)
			{
				using (List<BodyPart>.Enumerator enumerator = this.Parts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.AnyRegisteredEvent(ID))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007825 RID: 30757 RVA: 0x002D3F58 File Offset: 0x002D2158
		public virtual bool FireEvent(Event E)
		{
			bool flag = true;
			if (this.Equipped != null && this.FirstSlotForEquipped)
			{
				if (!this.Equipped.FireEvent(E))
				{
					flag = false;
				}
				if (E.ID == "EndTurn")
				{
					GameObject equipped = this.Equipped;
					if (equipped != null)
					{
						equipped.CleanEffects();
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && this.FirstSlotForCybernetics)
			{
				if (!this.Cybernetics.FireEvent(E))
				{
					flag = false;
				}
				if (E.ID == "EndTurn")
				{
					GameObject cybernetics = this.Cybernetics;
					if (cybernetics != null)
					{
						cybernetics.CleanEffects();
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			if (this.DefaultBehavior != null)
			{
				if (!this.DefaultBehavior.FireEvent(E))
				{
					flag = false;
				}
				if (E.ID == "EndTurn")
				{
					GameObject defaultBehavior = this.DefaultBehavior;
					if (defaultBehavior != null)
					{
						defaultBehavior.CleanEffects();
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					BodyPart bodyPart = this.Parts[i];
					if (bodyPart != null && !bodyPart.FireEvent(E))
					{
						flag = false;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x06007826 RID: 30758 RVA: 0x002D4088 File Offset: 0x002D2288
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Markup.Wrap(this.Description)).Append(": ");
			if (this.Equipped == null && this.DefaultBehavior == null)
			{
				stringBuilder.Append("{{K|-}}");
			}
			else if (this.Equipped == null)
			{
				stringBuilder.Append("{{K|").Append(this.DefaultBehavior.ShortDisplayName).Append("}}");
			}
			else
			{
				stringBuilder.Append("{{C|").Append(this.Equipped.ShortDisplayName).Append("}}");
			}
			if (this.Primary)
			{
				stringBuilder.Append(" {{G|*Primary}}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007827 RID: 30759 RVA: 0x002D4148 File Offset: 0x002D2348
		public void ToString(StringBuilder SB)
		{
			SB.Append(this.ToString());
			SB.Append("\n");
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					bodyPart.ToString(SB);
				}
			}
		}

		// Token: 0x06007828 RID: 30760 RVA: 0x002D41BC File Offset: 0x002D23BC
		public void UnequipPartAndChildren(bool Silent = false, IInventory Where = null, bool ForDeath = false)
		{
			if (this.Equipped != null && this.Equipped.IsReal && !this.Equipped.IsNatural())
			{
				if (this.ParentBody == null)
				{
					goto IL_0218;
				}
				GameObject equipped = this.Equipped;
				try
				{
					Event @event = Event.New("CommandForceUnequipObject", "BodyPart", this, "NoTake", 1);
					@event.SetSilent(true);
					this.ParentBody.ParentObject.FireEvent(@event);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("UnequipPartAndChildren unequip", ex, "game_exception");
				}
				try
				{
					if (!ForDeath || this.ParentBody.ParentObject.GetIntProperty("SuppressCorpseDrops", 0) <= 0)
					{
						if (Where == null && ForDeath)
						{
							Where = this.ParentBody.ParentObject.GetDropInventory();
						}
						if (Where == null)
						{
							Inventory inventory = this.ParentBody.ParentObject.Inventory;
							if (inventory != null)
							{
								Where = inventory;
							}
							else
							{
								Where = this.ParentBody.ParentObject.GetDropInventory();
							}
						}
						if (Where != null && (!ForDeath || DropOnDeathEvent.Check(equipped, Where, true, true, false, false, false, ".", "DropOnDeath")))
						{
							GameObject parentObject = this.ParentBody.ParentObject;
							GameObject parentObject2 = this.ParentBody.ParentObject;
							GameObject gameObject = equipped;
							string text = "CommandDropObject";
							bool flag = false;
							bool flag2 = false;
							bool flag3 = false;
							bool flag4 = true;
							IInventory inventory2 = Where;
							if (InventoryActionEvent.Check(parentObject, parentObject2, gameObject, text, flag, flag2, flag3, flag4, Silent, 0, 0, 0, null, null, null, inventory2) && !Silent && Where is Cell && Where.InventoryContains(equipped) && equipped.IsVisible())
							{
								GameObject gameObject2 = equipped;
								string text2 = "fall";
								int maxValue = int.MaxValue;
								string text3 = null;
								string text4 = null;
								string text5 = null;
								bool flag5 = false;
								bool flag6 = false;
								bool flag7 = false;
								bool flag8 = false;
								bool flag9 = false;
								bool flag10 = true;
								bool flag11 = true;
								bool flag12 = false;
								bool flag13 = false;
								string text6 = null;
								bool flag14 = false;
								bool flag15 = false;
								bool flag16 = true;
								GameObject parentObject3 = this.ParentBody.ParentObject;
								MessageQueue.AddPlayerMessage(gameObject2.Does(text2, maxValue, text3, text4, text5, flag5, flag6, flag7, flag8, flag9, flag10, flag11, flag12, flag13, text6, flag14, flag15, flag16, null, true, parentObject3, false) + " to the ground.", ColorCoding.ConsequentialColor(null, this.ParentBody.ParentObject), true);
							}
						}
					}
					goto IL_0218;
				}
				catch (Exception ex2)
				{
					MetricsManager.LogException("UnequipPartAndChildren drop", ex2, "game_exception");
					goto IL_0218;
				}
			}
			if (this.Equipped != null && this.ParentBody.ParentObject != null)
			{
				BeforeUnequippedEvent.Send(this.Equipped, this.ParentBody.ParentObject);
				UnequippedEvent.Send(this.Equipped, this.ParentBody.ParentObject);
			}
			this.Unequip();
			IL_0218:
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in new List<BodyPart>(this.Parts))
				{
					bodyPart.UnequipPartAndChildren(Silent, Where, ForDeath);
				}
			}
		}

		// Token: 0x06007829 RID: 30761 RVA: 0x002D4474 File Offset: 0x002D2674
		public bool RemoveUnmanagedPartsByVariantPrefix(string Prefix)
		{
			bool flag = false;
			if (this.Parts != null)
			{
				int i = 0;
				int num = this.Parts.Count;
				while (i < num)
				{
					if (this.Parts[i].RemoveUnmanagedPartsByVariantPrefix(Prefix))
					{
						flag = true;
						num = this.Parts.Count;
						i--;
					}
					i++;
				}
			}
			if (this.VariantType != this.Type && this.VariantType.StartsWith(Prefix) && this.ParentBody != null)
			{
				this.ParentBody.RemovePart(this, true);
				flag = true;
			}
			return flag;
		}

		// Token: 0x0600782A RID: 30762 RVA: 0x002D4504 File Offset: 0x002D2704
		public bool RemovePart(BodyPart Part, bool DoUpdate = true)
		{
			if (this.Parts == null)
			{
				return false;
			}
			if (this.Parts.Contains(Part))
			{
				Part.UnequipPartAndChildren(false, null, false);
				this.Parts.Remove(Part);
				if (Part.ParentPart == this)
				{
					Part.ParentPart = null;
				}
				if (DoUpdate && this.ParentBody != null)
				{
					this.ParentBody.UpdateBodyParts(0);
					this.ParentBody.RecalculateTypeArmor(Part.Type);
				}
				return true;
			}
			using (List<BodyPart>.Enumerator enumerator = this.Parts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.RemovePart(Part, DoUpdate))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600782B RID: 30763 RVA: 0x002D45C8 File Offset: 0x002D27C8
		public bool RemovePartByID(int removeID, bool DoUpdate = true)
		{
			BodyPart partByID = this.GetPartByID(removeID);
			return partByID != null && this.RemovePart(partByID, DoUpdate);
		}

		// Token: 0x0600782C RID: 30764 RVA: 0x002D45EC File Offset: 0x002D27EC
		private bool PositionOccupied(int Position)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Position == Position)
					{
						return true;
					}
					i++;
				}
			}
			return this.ParentBody != null && this.ParentBody.DismemberedPartHasPosition(this, Position, null);
		}

		// Token: 0x0600782D RID: 30765 RVA: 0x002D464C File Offset: 0x002D284C
		private bool PositionOccupied(int Position, BodyPart ExceptPart)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Position == Position && this.Parts[i] != ExceptPart)
					{
						return true;
					}
					i++;
				}
			}
			return this.ParentBody != null && this.ParentBody.DismemberedPartHasPosition(this, Position, ExceptPart);
		}

		// Token: 0x0600782E RID: 30766 RVA: 0x002D46BC File Offset: 0x002D28BC
		private void AssignPosition(BodyPart Part, int Position)
		{
			if (this.PositionOccupied(Position, Part))
			{
				if (this.Parts != null)
				{
					int i = 0;
					int count = this.Parts.Count;
					while (i < count)
					{
						if (this.Parts[i].Position >= Position)
						{
							this.Parts[i].Position++;
						}
						i++;
					}
				}
				if (this.ParentBody != null)
				{
					this.ParentBody.CheckDismemberedPartRenumberingOnPositionAssignment(this, Position, Part);
				}
			}
			Part.Position = Position;
		}

		// Token: 0x0600782F RID: 30767 RVA: 0x002D4740 File Offset: 0x002D2940
		private int NextPosition(BodyPart ExceptPart = null)
		{
			int num = ((this.ParentBody != null) ? this.ParentBody.GetLastDismemberedPartPosition(this, ExceptPart) : (-1));
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Position > num && this.Parts[i] != ExceptPart)
					{
						num = this.Parts[i].Position;
					}
					i++;
				}
			}
			return num + 1;
		}

		// Token: 0x06007830 RID: 30768 RVA: 0x002D47C0 File Offset: 0x002D29C0
		private int FindIndexForPosition(int Position)
		{
			if (this.Parts != null)
			{
				for (int i = 0; i < this.Parts.Count; i++)
				{
					if (this.Parts[i].Position >= Position)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06007831 RID: 30769 RVA: 0x002D4804 File Offset: 0x002D2A04
		public BodyPart AddPart(BodyPart NewPart, int Position = -1, bool DoUpdate = true)
		{
			if (this.Parts == null)
			{
				this.Parts = new List<BodyPart>();
			}
			if (this.Parts.IndexOf(NewPart) != -1)
			{
				throw new Exception("part for add already attached");
			}
			if (Position == -1)
			{
				Position = this.NextPosition(NewPart);
			}
			else
			{
				while (Position > 0 && !this.PositionOccupied(Position - 1, NewPart))
				{
					Position--;
				}
			}
			int num = this.FindIndexForPosition(Position);
			if (num == -1)
			{
				this.Parts.Add(NewPart);
			}
			else
			{
				this.Parts.Insert(num, NewPart);
			}
			this.AssignPosition(NewPart, Position);
			NewPart.ParentPart = this;
			if (this.ParentBody != null)
			{
				NewPart.ParentBody = this.ParentBody;
				if (DoUpdate)
				{
					this.ParentBody.UpdateBodyParts(0);
					this.ParentBody.RecalculateTypeArmor(NewPart.Type);
				}
			}
			return NewPart;
		}

		// Token: 0x06007832 RID: 30770 RVA: 0x002D48D0 File Offset: 0x002D2AD0
		public BodyPart AddPart(BodyPart NewPart, string InsertAfter, bool DoUpdate = true)
		{
			int num = this.LastPartTypePosition(InsertAfter, null);
			if (num != -1)
			{
				num++;
			}
			return this.AddPart(NewPart, num, DoUpdate);
		}

		// Token: 0x06007833 RID: 30771 RVA: 0x002D48F8 File Offset: 0x002D2AF8
		public BodyPart AddPart(BodyPart NewPart, string InsertAfter, string OrInsertBefore, bool DoUpdate = true)
		{
			if (OrInsertBefore == null)
			{
				return this.AddPart(NewPart, InsertAfter, true);
			}
			int num = this.LastPartTypePosition(InsertAfter, null);
			if (num == -1)
			{
				num = this.FirstPartTypePosition(OrInsertBefore, null);
			}
			else
			{
				num++;
			}
			return this.AddPart(NewPart, num, DoUpdate);
		}

		// Token: 0x06007834 RID: 30772 RVA: 0x002D4938 File Offset: 0x002D2B38
		public BodyPart AddPart(BodyPart NewPart, string[] InsertBefore, bool DoUpdate = true)
		{
			int num = -1;
			if (InsertBefore != null)
			{
				foreach (string text in InsertBefore)
				{
					num = this.FirstPartTypePosition(text, null);
					if (num != -1)
					{
						break;
					}
				}
			}
			return this.AddPart(NewPart, num, DoUpdate);
		}

		// Token: 0x06007835 RID: 30773 RVA: 0x002D4974 File Offset: 0x002D2B74
		public BodyPart AddPart(BodyPart NewPart, string InsertAfter, string[] OrInsertBefore, bool DoUpdate = true)
		{
			if (OrInsertBefore == null)
			{
				return this.AddPart(NewPart, InsertAfter, true);
			}
			int num = this.LastPartTypePosition(InsertAfter, null);
			if (num == -1)
			{
				foreach (string text in OrInsertBefore)
				{
					num = this.FirstPartTypePosition(text, null);
					if (num != -1)
					{
						break;
					}
				}
			}
			else
			{
				num++;
			}
			return this.AddPart(NewPart, num, DoUpdate);
		}

		// Token: 0x06007836 RID: 30774 RVA: 0x002D49CC File Offset: 0x002D2BCC
		public BodyPart AddPart(BodyPartType Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, null, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), -1, DoUpdate);
		}

		// Token: 0x06007837 RID: 30775 RVA: 0x002D4A18 File Offset: 0x002D2C18
		public BodyPart AddPart(string Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), -1, DoUpdate);
		}

		// Token: 0x06007838 RID: 30776 RVA: 0x002D4A64 File Offset: 0x002D2C64
		public BodyPart AddPartAt(BodyPartType Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, string InsertAfter = null, string OrInsertBefore = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, null, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), InsertAfter, OrInsertBefore, DoUpdate);
		}

		// Token: 0x06007839 RID: 30777 RVA: 0x002D4AB4 File Offset: 0x002D2CB4
		public BodyPart AddPartAt(string Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, string InsertAfter = null, string OrInsertBefore = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), InsertAfter, OrInsertBefore, DoUpdate);
		}

		// Token: 0x0600783A RID: 30778 RVA: 0x002D4B04 File Offset: 0x002D2D04
		public BodyPart AddPartAt(BodyPartType Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, string InsertAfter = null, string[] OrInsertBefore = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, null, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), InsertAfter, OrInsertBefore, DoUpdate);
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x002D4B54 File Offset: 0x002D2D54
		public BodyPart AddPartAt(string Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, string InsertAfter = null, string[] OrInsertBefore = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), InsertAfter, OrInsertBefore, DoUpdate);
		}

		// Token: 0x0600783C RID: 30780 RVA: 0x002D4BA4 File Offset: 0x002D2DA4
		public BodyPart AddPartAt(BodyPart InsertAfter, BodyPartType Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, null, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), InsertAfter.Position + 1, DoUpdate);
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x002D4BF8 File Offset: 0x002D2DF8
		public BodyPart AddPartAt(BodyPart InsertAfter, string Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), InsertAfter.Position + 1, DoUpdate);
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x002D4C4C File Offset: 0x002D2E4C
		public BodyPart AddPartAt(string[] InsertBefore, BodyPartType Base, int Laterality = 0, string DefaultBehavior = null, string SupportsDependent = null, string DependsOn = null, string RequiresType = null, string Manager = null, int? Category = null, int? RequiresLaterality = null, int? Mobility = null, bool? Appendage = null, bool? Integral = null, bool? Mortal = null, bool? Abstract = null, bool? Extrinsic = null, bool? Dynamic = null, bool? Plural = null, bool? Mass = null, bool? Contact = null, bool? IgnorePosition = null, bool DoUpdate = true)
		{
			return this.AddPart(new BodyPart(Base, Laterality, this.ParentBody, null, DefaultBehavior, SupportsDependent, DependsOn, RequiresType, Manager, Category, RequiresLaterality, Mobility, Mortal, Appendage, Integral, Abstract, Extrinsic, Dynamic, Plural, Mass, Contact, IgnorePosition), InsertBefore, DoUpdate);
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x002D4C98 File Offset: 0x002D2E98
		public int FirstPartTypePosition(string Type, BodyPart ExceptPart = null)
		{
			int num = -1;
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].Type == Type && this.Parts[i] != ExceptPart)
					{
						num = this.Parts[i].Position;
						break;
					}
					i++;
				}
			}
			if (this.ParentBody != null)
			{
				int firstDismemberedPartTypePosition = this.ParentBody.GetFirstDismemberedPartTypePosition(this, Type, ExceptPart);
				if (firstDismemberedPartTypePosition != -1 && (num == -1 || firstDismemberedPartTypePosition < num))
				{
					num = firstDismemberedPartTypePosition;
				}
			}
			return num;
		}

		// Token: 0x06007840 RID: 30784 RVA: 0x002D4D2C File Offset: 0x002D2F2C
		public int LastPartTypePosition(string Type, BodyPart ExceptPart = null)
		{
			int num = -1;
			if (Type == null)
			{
				return num;
			}
			if (this.Parts != null)
			{
				for (int i = this.Parts.Count - 1; i >= 0; i--)
				{
					if (this.Parts[i].Type == Type && this.Parts[i] != ExceptPart)
					{
						num = this.Parts[i].Position;
						break;
					}
				}
			}
			if (this.ParentBody != null)
			{
				int lastDismemberedPartTypePosition = this.ParentBody.GetLastDismemberedPartTypePosition(this, Type, ExceptPart);
				if (lastDismemberedPartTypePosition > num)
				{
					num = lastDismemberedPartTypePosition;
				}
			}
			return num;
		}

		// Token: 0x06007841 RID: 30785 RVA: 0x002D4DBC File Offset: 0x002D2FBC
		public BodyPart GetPart(Predicate<BodyPart> Filter)
		{
			if (Filter(this))
			{
				return this;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						BodyPart part = bodyPart.GetPart(Filter);
						if (part != null)
						{
							return part;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06007842 RID: 30786 RVA: 0x002D4E30 File Offset: 0x002D3030
		public bool AnyPart(Predicate<BodyPart> Filter)
		{
			if (Filter(this))
			{
				return true;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null && bodyPart.AnyPart(Filter))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007843 RID: 30787 RVA: 0x002D4EA4 File Offset: 0x002D30A4
		public void ForeachPart(Action<BodyPart> aProc)
		{
			aProc(this);
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						bodyPart.ForeachPart(aProc);
					}
				}
			}
		}

		// Token: 0x06007844 RID: 30788 RVA: 0x002D4F0C File Offset: 0x002D310C
		public bool ForeachPart(Predicate<BodyPart> pProc)
		{
			if (!pProc(this))
			{
				return false;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null && !bodyPart.ForeachPart(pProc))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06007845 RID: 30789 RVA: 0x002D4F80 File Offset: 0x002D3180
		public void SafeForeachPart(Action<BodyPart> aProc)
		{
			aProc(this);
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in new List<BodyPart>(this.Parts))
				{
					if (bodyPart != null)
					{
						bodyPart.ForeachPart(aProc);
					}
				}
			}
		}

		// Token: 0x06007846 RID: 30790 RVA: 0x002D4FEC File Offset: 0x002D31EC
		public bool SafeForeachPart(Predicate<BodyPart> pProc)
		{
			if (!pProc(this))
			{
				return false;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in new List<BodyPart>(this.Parts))
				{
					if (bodyPart != null && !bodyPart.ForeachPart(pProc))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06007847 RID: 30791 RVA: 0x002D5064 File Offset: 0x002D3264
		public int GetPartCount()
		{
			int num = 1;
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartCount();
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007848 RID: 30792 RVA: 0x002D50B8 File Offset: 0x002D32B8
		public int GetConcretePartCount()
		{
			int num = (this.Abstract ? 0 : 1);
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetConcretePartCount();
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007849 RID: 30793 RVA: 0x002D5118 File Offset: 0x002D3318
		public int GetAbstractPartCount()
		{
			int num = (this.Abstract ? 1 : 0);
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetAbstractPartCount();
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600784A RID: 30794 RVA: 0x002D5178 File Offset: 0x002D3378
		public int GetPartCount(string RequiredType)
		{
			int num = 0;
			if (this.Type == RequiredType)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartCount(RequiredType);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600784B RID: 30795 RVA: 0x002D51E0 File Offset: 0x002D33E0
		public int GetPartCount(string RequiredType, int RequiredLaterality)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetPartCount(RequiredType);
			}
			int num = 0;
			if (this.Type == RequiredType && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality))
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartCount(RequiredType, RequiredLaterality);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600784C RID: 30796 RVA: 0x002D5260 File Offset: 0x002D3460
		public int GetPartCount(Predicate<BodyPart> Filter)
		{
			int num = 0;
			if (Filter(this))
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartCount(Filter);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x002D52C0 File Offset: 0x002D34C0
		public int GetPartNameCount(string FindName)
		{
			int num = 0;
			if (this.Name == FindName)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartNameCount(FindName);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600784E RID: 30798 RVA: 0x002D5328 File Offset: 0x002D3528
		public int GetPartDescriptionCount(string FindDescription)
		{
			int num = 0;
			if (this.Description == FindDescription)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartDescriptionCount(FindDescription);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600784F RID: 30799 RVA: 0x002D5390 File Offset: 0x002D3590
		public int GetPartDescriptionCount(string FindDescription, string FindDescriptionPrefix)
		{
			int num = 0;
			if (this.Description == FindDescription && this.DescriptionPrefix.EqualsEmptyEqualsNull(FindDescriptionPrefix))
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartDescriptionCount(FindDescription, FindDescriptionPrefix);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007850 RID: 30800 RVA: 0x002D5404 File Offset: 0x002D3604
		public bool AnyCategoryParts(int FindCategory)
		{
			if (FindCategory == this.Category)
			{
				return true;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null && bodyPart.AnyCategoryParts(FindCategory))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06007851 RID: 30801 RVA: 0x002D5478 File Offset: 0x002D3678
		public int GetCategoryPartCount(int FindCategory)
		{
			int num = ((FindCategory == this.Category) ? 1 : 0);
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetCategoryPartCount(FindCategory);
					}
				}
			}
			return num;
		}

		// Token: 0x06007852 RID: 30802 RVA: 0x002D54E8 File Offset: 0x002D36E8
		public int GetCategoryPartCount(int FindCategory, string FindType)
		{
			int num = 0;
			if (FindCategory == this.Category && FindType == this.Type)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					num += bodyPart.GetCategoryPartCount(FindCategory, FindType);
				}
			}
			return num;
		}

		// Token: 0x06007853 RID: 30803 RVA: 0x002D5568 File Offset: 0x002D3768
		public int GetCategoryPartCount(int FindCategory, Predicate<BodyPart> Filter)
		{
			int num = 0;
			if (FindCategory == this.Category && Filter(this))
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetCategoryPartCount(FindCategory, Filter);
					}
				}
			}
			return num;
		}

		// Token: 0x06007854 RID: 30804 RVA: 0x002D55E4 File Offset: 0x002D37E4
		public int GetNativePartCount()
		{
			int num = (this.Native ? 1 : 0);
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetNativePartCount();
					}
				}
			}
			return num;
		}

		// Token: 0x06007855 RID: 30805 RVA: 0x002D5654 File Offset: 0x002D3854
		public int GetNativePartCount(string RequiredType)
		{
			int num = 0;
			if (this.Type == RequiredType && this.Native)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					num += bodyPart.GetNativePartCount(RequiredType);
				}
			}
			return num;
		}

		// Token: 0x06007856 RID: 30806 RVA: 0x002D56D0 File Offset: 0x002D38D0
		public int GetNativePartCount(Predicate<BodyPart> Filter)
		{
			int num = 0;
			if (this.Native && Filter(this))
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetNativePartCount(Filter);
					}
				}
			}
			return num;
		}

		// Token: 0x06007857 RID: 30807 RVA: 0x002D574C File Offset: 0x002D394C
		public int GetAddedPartCount()
		{
			int num = (this.Native ? 0 : 1);
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetAddedPartCount();
					}
				}
			}
			return num;
		}

		// Token: 0x06007858 RID: 30808 RVA: 0x002D57BC File Offset: 0x002D39BC
		public int GetAddedPartCount(string RequiredType)
		{
			int num = 0;
			if (this.Type == RequiredType && !this.Native)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					num += bodyPart.GetAddedPartCount(RequiredType);
				}
			}
			return num;
		}

		// Token: 0x06007859 RID: 30809 RVA: 0x002D5838 File Offset: 0x002D3A38
		public int GetAddedPartCount(Predicate<BodyPart> Filter)
		{
			int num = 0;
			if (!this.Native && Filter(this))
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetAddedPartCount(Filter);
					}
				}
			}
			return num;
		}

		// Token: 0x0600785A RID: 30810 RVA: 0x002D58B4 File Offset: 0x002D3AB4
		public int GetMortalPartCount()
		{
			int num = (this.Mortal ? 1 : 0);
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetMortalPartCount();
					}
				}
			}
			return num;
		}

		// Token: 0x0600785B RID: 30811 RVA: 0x002D5924 File Offset: 0x002D3B24
		public int GetMortalPartCount(string RequiredType)
		{
			int num = 0;
			if (this.Type == RequiredType && this.Mortal)
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					num += bodyPart.GetMortalPartCount(RequiredType);
				}
			}
			return num;
		}

		// Token: 0x0600785C RID: 30812 RVA: 0x002D59A0 File Offset: 0x002D3BA0
		public int GetMortalPartCount(Predicate<BodyPart> Filter)
		{
			int num = 0;
			if (this.Mortal && Filter(this))
			{
				num++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null)
					{
						num += bodyPart.GetMortalPartCount(Filter);
					}
				}
			}
			return num;
		}

		// Token: 0x0600785D RID: 30813 RVA: 0x002D5A1C File Offset: 0x002D3C1C
		public bool AnyMortalParts()
		{
			if (this.Mortal)
			{
				return true;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					if (bodyPart != null && bodyPart.AnyMortalParts())
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600785E RID: 30814 RVA: 0x002D5A8C File Offset: 0x002D3C8C
		public bool IsEquippedOnType(GameObject FindObj, string FindType)
		{
			if (FindObj == this.Equipped && FindType == this.Type)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].IsEquippedOnType(FindObj, FindType))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600785F RID: 30815 RVA: 0x002D5AF8 File Offset: 0x002D3CF8
		public bool IsEquippedOnCategory(GameObject FindObj, int FindCategory)
		{
			if (FindObj == this.Equipped && FindCategory == this.Category)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].IsEquippedOnCategory(FindObj, FindCategory))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06007860 RID: 30816 RVA: 0x002D5B60 File Offset: 0x002D3D60
		public bool IsEquippedOnPrimary(GameObject FindObj)
		{
			if (FindObj == this.Equipped && this.Primary)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].IsEquippedOnPrimary(FindObj))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06007861 RID: 30817 RVA: 0x002D5BC8 File Offset: 0x002D3DC8
		public bool IsImplantedInCategory(GameObject FindObj, int FindCategory)
		{
			if (FindObj == this.Cybernetics && FindCategory == this.Category)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].IsImplantedInCategory(FindObj, FindCategory))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x06007862 RID: 30818 RVA: 0x002D5C30 File Offset: 0x002D3E30
		public int GetTotalMobility()
		{
			int num = this.Mobility;
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetTotalMobility();
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007863 RID: 30819 RVA: 0x002D5C88 File Offset: 0x002D3E88
		public void MarkAllNative()
		{
			this.Native = true;
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].MarkAllNative();
					}
					i++;
				}
			}
		}

		// Token: 0x06007864 RID: 30820 RVA: 0x002D5CDC File Offset: 0x002D3EDC
		public void CategorizeAll(int ApplyCategory)
		{
			this.Category = ApplyCategory;
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].CategorizeAll(ApplyCategory);
					}
					i++;
				}
			}
		}

		// Token: 0x06007865 RID: 30821 RVA: 0x002D5D30 File Offset: 0x002D3F30
		public void CategorizeAllExcept(int ApplyCategory, int SkipCategory)
		{
			if (this.Category != SkipCategory)
			{
				this.Category = ApplyCategory;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].CategorizeAllExcept(ApplyCategory, SkipCategory);
					}
					i++;
				}
			}
		}

		// Token: 0x06007866 RID: 30822 RVA: 0x002D5D90 File Offset: 0x002D3F90
		public List<BodyPart> GetParts()
		{
			List<BodyPart> list = new List<BodyPart>(this.GetPartCount());
			list.Add(this);
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetParts(list);
					}
					i++;
				}
			}
			return list;
		}

		// Token: 0x06007867 RID: 30823 RVA: 0x002D5DF4 File Offset: 0x002D3FF4
		public void GetParts(List<BodyPart> Return)
		{
			Return.Add(this);
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetParts(Return);
					}
					i++;
				}
			}
		}

		// Token: 0x06007868 RID: 30824 RVA: 0x002D5E48 File Offset: 0x002D4048
		public void GetTopLevelDynamicParts(List<BodyPart> Return)
		{
			if (this.Dynamic)
			{
				Return.Add(this);
				return;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetTopLevelDynamicParts(Return);
					}
					i++;
				}
			}
		}

		// Token: 0x06007869 RID: 30825 RVA: 0x002D5EA8 File Offset: 0x002D40A8
		public void GetPartsSkippingDynamicTrees(List<BodyPart> Return)
		{
			if (!this.Dynamic)
			{
				Return.Add(this);
				if (this.Parts != null)
				{
					int i = 0;
					int count = this.Parts.Count;
					while (i < count)
					{
						if (this.Parts[i] != null)
						{
							this.Parts[i].GetPartsSkippingDynamicTrees(Return);
						}
						i++;
					}
				}
			}
		}

		// Token: 0x0600786A RID: 30826 RVA: 0x002D5F04 File Offset: 0x002D4104
		public void GetConcreteParts(List<BodyPart> Return)
		{
			if (!this.Abstract)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetConcreteParts(Return);
					}
					i++;
				}
			}
		}

		// Token: 0x0600786B RID: 30827 RVA: 0x002D5F60 File Offset: 0x002D4160
		public void GetAbstractParts(List<BodyPart> Return)
		{
			if (this.Abstract)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetAbstractParts(Return);
					}
					i++;
				}
			}
		}

		// Token: 0x0600786C RID: 30828 RVA: 0x002D5FBC File Offset: 0x002D41BC
		public void GetPartsEquippedOn(GameObject obj, List<BodyPart> Result)
		{
			if (this.Equipped == obj)
			{
				Result.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetPartsEquippedOn(obj, Result);
					}
					i++;
				}
			}
		}

		// Token: 0x0600786D RID: 30829 RVA: 0x002D601C File Offset: 0x002D421C
		public List<BodyPart> GetPartsEquippedOn(GameObject obj)
		{
			List<BodyPart> list = new List<BodyPart>(this.GetPartCountEquippedOn(obj));
			this.GetPartsEquippedOn(obj, list);
			return list;
		}

		// Token: 0x0600786E RID: 30830 RVA: 0x002D6040 File Offset: 0x002D4240
		public int GetPartCountEquippedOn(GameObject obj)
		{
			int num = 0;
			if (this.Equipped == obj)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetPartCountEquippedOn(obj);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x0600786F RID: 30831 RVA: 0x002D60A0 File Offset: 0x002D42A0
		public List<BodyPart> GetUnequippedPart(string RequiredType, List<BodyPart> Return)
		{
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && this.Equipped == null)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetUnequippedPart(RequiredType, Return);
					}
					i++;
				}
			}
			return Return;
		}

		// Token: 0x06007870 RID: 30832 RVA: 0x002D611C File Offset: 0x002D431C
		public List<BodyPart> GetUnequippedPart(string RequiredType, int RequiredLaterality, List<BodyPart> Return)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetUnequippedPart(RequiredType, Return);
			}
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality) && this.Equipped == null)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetUnequippedPart(RequiredType, RequiredLaterality, Return);
					}
					i++;
				}
			}
			return Return;
		}

		// Token: 0x06007871 RID: 30833 RVA: 0x002D61B4 File Offset: 0x002D43B4
		public int GetUnequippedPartCount(string RequiredType)
		{
			int num = 0;
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && this.Equipped == null)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetUnequippedPartCount(RequiredType);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007872 RID: 30834 RVA: 0x002D6230 File Offset: 0x002D4430
		public int GetUnequippedPartCount(string RequiredType, int RequiredLaterality)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetUnequippedPartCount(RequiredType);
			}
			int num = 0;
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality) && this.Equipped == null)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetUnequippedPartCount(RequiredType, RequiredLaterality);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007873 RID: 30835 RVA: 0x002D62C8 File Offset: 0x002D44C8
		public int GetUnequippedPartCountExcept(string RequiredType, BodyPart ExceptPart)
		{
			int num = 0;
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && this.Equipped == null && ExceptPart != this)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetUnequippedPartCountExcept(RequiredType, ExceptPart);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007874 RID: 30836 RVA: 0x002D6348 File Offset: 0x002D4548
		public int GetUnequippedPartCountExcept(string RequiredType, int RequiredLaterality, BodyPart ExceptPart)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetUnequippedPartCountExcept(RequiredType, ExceptPart);
			}
			int num = 0;
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality) && this.Equipped == null && ExceptPart != this)
			{
				num++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						num += this.Parts[i].GetUnequippedPartCountExcept(RequiredType, RequiredLaterality, ExceptPart);
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x06007875 RID: 30837 RVA: 0x002D63E4 File Offset: 0x002D45E4
		public List<BodyPart> GetPart(string RequiredType, List<BodyPart> Return)
		{
			if (this.Type == RequiredType || this.VariantType == RequiredType)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetPart(RequiredType, Return);
					}
					i++;
				}
			}
			return Return;
		}

		// Token: 0x06007876 RID: 30838 RVA: 0x002D6458 File Offset: 0x002D4658
		public List<BodyPart> GetPart(string RequiredType, int RequiredLaterality, List<BodyPart> Return)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetPart(RequiredType, Return);
			}
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality))
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetPart(RequiredType, RequiredLaterality, Return);
					}
					i++;
				}
			}
			return Return;
		}

		// Token: 0x06007877 RID: 30839 RVA: 0x002D64E6 File Offset: 0x002D46E6
		public IEnumerable<BodyPart> LoopPart(string RequiredType)
		{
			if (this.Type == RequiredType || this.VariantType == RequiredType)
			{
				yield return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int j = this.Parts.Count;
				while (i < j)
				{
					if (this.Parts[i] != null)
					{
						foreach (BodyPart bodyPart in this.Parts[i].LoopPart(RequiredType))
						{
							yield return bodyPart;
						}
						IEnumerator<BodyPart> enumerator = null;
					}
					int num = i;
					i = num + 1;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06007878 RID: 30840 RVA: 0x002D64FD File Offset: 0x002D46FD
		public IEnumerable<BodyPart> LoopPart(string RequiredType, int RequiredLaterality)
		{
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality))
			{
				yield return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int j = this.Parts.Count;
				while (i < j)
				{
					if (this.Parts[i] != null)
					{
						foreach (BodyPart bodyPart in this.Parts[i].LoopPart(RequiredType, RequiredLaterality))
						{
							yield return bodyPart;
						}
						IEnumerator<BodyPart> enumerator = null;
					}
					int num = i;
					i = num + 1;
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06007879 RID: 30841 RVA: 0x002D651C File Offset: 0x002D471C
		public BodyPart GetFirstPart(string RequiredType)
		{
			if (this.Type == RequiredType || this.VariantType == RequiredType)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstPart = this.Parts[i].GetFirstPart(RequiredType);
						if (firstPart != null)
						{
							return firstPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600787A RID: 30842 RVA: 0x002D6590 File Offset: 0x002D4790
		public BodyPart GetFirstPart(string RequiredType, int RequiredLaterality)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetFirstPart(RequiredType);
			}
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstPart = this.Parts[i].GetFirstPart(RequiredType, RequiredLaterality);
						if (firstPart != null)
						{
							return firstPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600787B RID: 30843 RVA: 0x002D661C File Offset: 0x002D481C
		public BodyPart GetFirstPart(Predicate<BodyPart> Filter)
		{
			if (Filter(this))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstPart = this.Parts[i].GetFirstPart(Filter);
						if (firstPart != null)
						{
							return firstPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x002D667C File Offset: 0x002D487C
		public BodyPart GetFirstPart(string RequiredType, Predicate<BodyPart> Filter)
		{
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && Filter(this))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstPart = this.Parts[i].GetFirstPart(RequiredType, Filter);
						if (firstPart != null)
						{
							return firstPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600787D RID: 30845 RVA: 0x002D66F8 File Offset: 0x002D48F8
		public BodyPart GetFirstPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetFirstPart(RequiredType, Filter);
			}
			if ((this.Type == RequiredType || this.VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality) && Filter(this))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstPart = this.Parts[i].GetFirstPart(RequiredType, RequiredLaterality, Filter);
						if (firstPart != null)
						{
							return firstPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600787E RID: 30846 RVA: 0x002D6790 File Offset: 0x002D4990
		public BodyPart GetFirstVariantPart(string RequiredType)
		{
			if (this.VariantType == RequiredType)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstVariantPart = this.Parts[i].GetFirstVariantPart(RequiredType);
						if (firstVariantPart != null)
						{
							return firstVariantPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600787F RID: 30847 RVA: 0x002D67F4 File Offset: 0x002D49F4
		public BodyPart GetFirstVariantPart(string RequiredType, int RequiredLaterality)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetFirstVariantPart(RequiredType);
			}
			if (this.VariantType == RequiredType && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstVariantPart = this.Parts[i].GetFirstVariantPart(RequiredType, RequiredLaterality);
						if (firstVariantPart != null)
						{
							return firstVariantPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x002D6874 File Offset: 0x002D4A74
		public BodyPart GetFirstVariantPart(Predicate<BodyPart> Filter)
		{
			if (Filter(this))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstVariantPart = this.Parts[i].GetFirstVariantPart(Filter);
						if (firstVariantPart != null)
						{
							return firstVariantPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007881 RID: 30849 RVA: 0x002D68D4 File Offset: 0x002D4AD4
		public BodyPart GetFirstVariantPart(string RequiredType, Predicate<BodyPart> Filter)
		{
			if (this.VariantType == RequiredType && Filter(this))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstVariantPart = this.Parts[i].GetFirstVariantPart(RequiredType, Filter);
						if (firstVariantPart != null)
						{
							return firstVariantPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x002D6944 File Offset: 0x002D4B44
		public BodyPart GetFirstVariantPart(string RequiredType, int RequiredLaterality, Predicate<BodyPart> Filter)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetFirstVariantPart(RequiredType, Filter);
			}
			if (this.VariantType == RequiredType && XRL.World.Capabilities.Laterality.Match(this, RequiredLaterality) && Filter(this))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart firstVariantPart = this.Parts[i].GetFirstVariantPart(RequiredType, RequiredLaterality, Filter);
						if (firstVariantPart != null)
						{
							return firstVariantPart;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x002D69D0 File Offset: 0x002D4BD0
		public BodyPart GetFirstAttachedPart(string RequiredType)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && (this.Parts[i].Type == RequiredType || this.Parts[i].VariantType == RequiredType))
					{
						return this.Parts[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x002D6A4C File Offset: 0x002D4C4C
		public BodyPart GetFirstAttachedPart(string RequiredType, Body BackupParentBody, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this.GetFirstAttachedPart(RequiredType);
			if (EvenIfDismembered)
			{
				Body body = this.ParentBody ?? BackupParentBody;
				if (body != null)
				{
					BodyPart firstDismemberedPartByType = body.GetFirstDismemberedPartByType(this, RequiredType);
					if (firstDismemberedPartByType != null && (bodyPart == null || firstDismemberedPartByType.Position < bodyPart.Position))
					{
						bodyPart = firstDismemberedPartByType;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06007885 RID: 30853 RVA: 0x002D6A94 File Offset: 0x002D4C94
		public BodyPart GetFirstAttachedPart(string RequiredType, int RequiredLaterality)
		{
			if (RequiredLaterality == 65535)
			{
				return this.GetFirstAttachedPart(RequiredType);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && (this.Parts[i].Type == RequiredType || this.Parts[i].VariantType == RequiredType) && XRL.World.Capabilities.Laterality.Match(this.Parts[i], RequiredLaterality))
					{
						return this.Parts[i];
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007886 RID: 30854 RVA: 0x002D6B34 File Offset: 0x002D4D34
		public BodyPart GetFirstAttachedPart(string RequiredType, int RequiredLaterality, Body BackupParentBody, bool EvenIfDismembered)
		{
			BodyPart bodyPart = this.GetFirstAttachedPart(RequiredType, RequiredLaterality);
			if (EvenIfDismembered)
			{
				Body body = this.ParentBody ?? BackupParentBody;
				if (body != null)
				{
					BodyPart firstDismemberedPartByTypeAndLaterality = body.GetFirstDismemberedPartByTypeAndLaterality(this, RequiredType, RequiredLaterality);
					if (firstDismemberedPartByTypeAndLaterality != null && (bodyPart == null || firstDismemberedPartByTypeAndLaterality.Position < bodyPart.Position))
					{
						bodyPart = firstDismemberedPartByTypeAndLaterality;
					}
				}
			}
			return bodyPart;
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x002D6B80 File Offset: 0x002D4D80
		public BodyPart GetPartByName(string RequiredPart)
		{
			if (this.Name == RequiredPart)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partByName = this.Parts[i].GetPartByName(RequiredPart);
						if (partByName != null)
						{
							return partByName;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007888 RID: 30856 RVA: 0x002D6BE4 File Offset: 0x002D4DE4
		public BodyPart GetPartByNameStartsWith(string RequiredPart)
		{
			if (this.Name != null && this.Name.StartsWith(RequiredPart))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partByNameStartsWith = this.Parts[i].GetPartByNameStartsWith(RequiredPart);
						if (partByNameStartsWith != null)
						{
							return partByNameStartsWith;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x002D6C50 File Offset: 0x002D4E50
		public BodyPart GetPartByNameWithoutCybernetics(string RequiredPart)
		{
			if (this.Name == RequiredPart && this.Cybernetics == null)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partByNameWithoutCybernetics = this.Parts[i].GetPartByNameWithoutCybernetics(RequiredPart);
						if (partByNameWithoutCybernetics != null)
						{
							return partByNameWithoutCybernetics;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600788A RID: 30858 RVA: 0x002D6CBC File Offset: 0x002D4EBC
		public BodyPart GetPartByDescription(string RequiredDescription)
		{
			if (this.Description == RequiredDescription)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partByDescription = this.Parts[i].GetPartByDescription(RequiredDescription);
						if (partByDescription != null)
						{
							return partByDescription;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600788B RID: 30859 RVA: 0x002D6D20 File Offset: 0x002D4F20
		public BodyPart GetPartByDescription(string RequiredDescription, string RequiredDescriptionPrefix)
		{
			if (this.Description == RequiredDescription && this.DescriptionPrefix.EqualsEmptyEqualsNull(RequiredDescriptionPrefix))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partByDescription = this.Parts[i].GetPartByDescription(RequiredDescription, RequiredDescriptionPrefix);
						if (partByDescription != null)
						{
							return partByDescription;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600788C RID: 30860 RVA: 0x002D6D94 File Offset: 0x002D4F94
		public BodyPart GetPartByDescriptionStartsWith(string RequiredDescription)
		{
			if (this.Description != null && this.Description.StartsWith(RequiredDescription))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partByDescriptionStartsWith = this.Parts[i].GetPartByDescriptionStartsWith(RequiredDescription);
						if (partByDescriptionStartsWith != null)
						{
							return partByDescriptionStartsWith;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600788D RID: 30861 RVA: 0x002D6E00 File Offset: 0x002D5000
		public BodyPart GetPartByID(int findID)
		{
			if (this.IDMatch(findID))
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partByID = this.Parts[i].GetPartByID(findID);
						if (partByID != null)
						{
							return partByID;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x0600788E RID: 30862 RVA: 0x002D6E60 File Offset: 0x002D5060
		public void GetPartBySupportsDependent(string findSupportsDependent, List<BodyPart> parts)
		{
			if (this.SupportsDependent == findSupportsDependent)
			{
				parts.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetPartBySupportsDependent(findSupportsDependent);
					}
					i++;
				}
			}
		}

		// Token: 0x0600788F RID: 30863 RVA: 0x002D6EC4 File Offset: 0x002D50C4
		public BodyPart GetPartBySupportsDependent(string findSupportsDependent)
		{
			if (this.SupportsDependent == findSupportsDependent)
			{
				return this;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						BodyPart partBySupportsDependent = this.Parts[i].GetPartBySupportsDependent(findSupportsDependent);
						if (partBySupportsDependent != null)
						{
							return partBySupportsDependent;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x06007890 RID: 30864 RVA: 0x002D6F28 File Offset: 0x002D5128
		protected void GetNamePosition(BodyPart FindPart, ref int Found, ref bool Done)
		{
			if (FindPart == this)
			{
				Found++;
				Done = true;
				return;
			}
			if (FindPart.Name == this.Name)
			{
				Found++;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetNamePosition(FindPart, ref Found, ref Done);
						if (Done)
						{
							break;
						}
					}
					i++;
				}
			}
		}

		// Token: 0x06007891 RID: 30865 RVA: 0x002D6FA4 File Offset: 0x002D51A4
		public int GetNamePosition()
		{
			int num = 0;
			bool flag = false;
			if (this.ParentBody != null)
			{
				this.ParentBody.GetBody().GetNamePosition(this, ref num, ref flag);
			}
			else
			{
				this.GetNamePosition(this, ref num, ref flag);
			}
			return num;
		}

		// Token: 0x06007892 RID: 30866 RVA: 0x002D6FE0 File Offset: 0x002D51E0
		protected void GetDescriptionPosition(BodyPart FindPart, ref int Found, ref bool Done)
		{
			if (FindPart == this)
			{
				Found++;
				Done = true;
				return;
			}
			if (FindPart.Description == this.Description && FindPart.DescriptionPrefix.EqualsEmptyEqualsNull(this.DescriptionPrefix))
			{
				Found++;
			}
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					bodyPart.GetDescriptionPosition(FindPart, ref Found, ref Done);
					if (Done)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06007893 RID: 30867 RVA: 0x002D707C File Offset: 0x002D527C
		public int GetDescriptionPosition()
		{
			int num = 0;
			bool flag = false;
			if (this.ParentBody != null)
			{
				this.ParentBody.GetBody().GetDescriptionPosition(this, ref num, ref flag);
			}
			else
			{
				this.GetDescriptionPosition(this, ref num, ref flag);
			}
			return num;
		}

		// Token: 0x06007894 RID: 30868 RVA: 0x002D70B8 File Offset: 0x002D52B8
		public string GetCardinalName()
		{
			if (this.IgnorePosition)
			{
				return this.Name;
			}
			int namePosition = this.GetNamePosition();
			if (namePosition == 1)
			{
				return this.Name;
			}
			return Event.NewStringBuilder(null).Append(this.Name).Append(" (")
				.Append(namePosition)
				.Append(')')
				.ToString();
		}

		// Token: 0x06007895 RID: 30869 RVA: 0x002D7114 File Offset: 0x002D5314
		public string GetOrdinalName()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			string text = (this.Abstract ? null : BodyPartCategory.GetColor(this.Category));
			if (text != null)
			{
				stringBuilder.Append("{{").Append(text).Append('|');
			}
			if (this.IgnorePosition || this.GetPartNameCount(this.Name) == 1)
			{
				stringBuilder.Append(this.Name);
			}
			else
			{
				stringBuilder.Append(Grammar.Ordinal(this.GetNamePosition())).Append(' ').Append(this.Name);
			}
			if (text != null)
			{
				stringBuilder.Append("}}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007896 RID: 30870 RVA: 0x002D71BC File Offset: 0x002D53BC
		public string GetCardinalDescription()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			string text = (this.Abstract ? null : BodyPartCategory.GetColor(this.Category));
			if (text != null)
			{
				stringBuilder.Append("{{").Append(text).Append('|');
			}
			if (!this.DescriptionPrefix.IsNullOrEmpty())
			{
				stringBuilder.Append(this.DescriptionPrefix).Append(' ');
			}
			stringBuilder.Append(this.Description);
			if (!this.IgnorePosition)
			{
				int descriptionPosition = this.GetDescriptionPosition();
				if (descriptionPosition != 1)
				{
					stringBuilder.Append(" (").Append(descriptionPosition).Append(')');
				}
			}
			if (text != null)
			{
				stringBuilder.Append("}}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007897 RID: 30871 RVA: 0x002D7274 File Offset: 0x002D5474
		public string GetOrdinalDescription()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			string text = (this.Abstract ? null : BodyPartCategory.GetColor(this.Category));
			if (text != null)
			{
				stringBuilder.Append("{{").Append(text).Append('|');
			}
			if (!this.IgnorePosition && this.GetPartDescriptionCount(this.Description) != 1)
			{
				int descriptionPosition = this.GetDescriptionPosition();
				stringBuilder.Append(ColorUtility.CapitalizeExceptFormatting(Grammar.Ordinal(descriptionPosition))).Append(' ');
			}
			if (!this.DescriptionPrefix.IsNullOrEmpty())
			{
				stringBuilder.Append(this.DescriptionPrefix).Append(' ');
			}
			stringBuilder.Append(this.Description);
			if (text != null)
			{
				stringBuilder.Append("}}");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06007898 RID: 30872 RVA: 0x002D7335 File Offset: 0x002D5535
		public bool IsConcretelyDependent()
		{
			return this.DependsOn != null;
		}

		// Token: 0x06007899 RID: 30873 RVA: 0x002D7340 File Offset: 0x002D5540
		public bool IsAbstractlyDependent()
		{
			return this.RequiresType != null;
		}

		// Token: 0x0600789A RID: 30874 RVA: 0x002D734B File Offset: 0x002D554B
		public bool IsDependent()
		{
			return this.IsConcretelyDependent() || this.IsAbstractlyDependent();
		}

		// Token: 0x0600789B RID: 30875 RVA: 0x002D7360 File Offset: 0x002D5560
		public bool IsConcretelyUnsupported(Body UseParentBody = null)
		{
			if (this.DependsOn == null)
			{
				return false;
			}
			if (UseParentBody != null)
			{
				return UseParentBody.GetPartBySupportsDependent(this.DependsOn) == null;
			}
			if (this.ParentBody != null)
			{
				return this.ParentBody.GetPartBySupportsDependent(this.DependsOn) == null;
			}
			return this.GetPartBySupportsDependent(this.DependsOn) == null;
		}

		// Token: 0x0600789C RID: 30876 RVA: 0x002D73B8 File Offset: 0x002D55B8
		public bool IsAbstractlyUnsupported(Body UseParentBody = null)
		{
			if (this.RequiresType == null)
			{
				return false;
			}
			if (UseParentBody != null)
			{
				return UseParentBody.GetFirstPart(this.RequiresType, this.RequiresLaterality) == null;
			}
			if (this.ParentBody != null)
			{
				return this.ParentBody.GetFirstPart(this.RequiresType, this.RequiresLaterality) == null;
			}
			return this.GetFirstPart(this.RequiresType, this.RequiresLaterality) == null;
		}

		// Token: 0x0600789D RID: 30877 RVA: 0x002D7420 File Offset: 0x002D5620
		public bool IsUnsupported(Body UseParentBody = null)
		{
			return this.IsConcretelyUnsupported(UseParentBody) || this.IsAbstractlyUnsupported(UseParentBody);
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x002D7434 File Offset: 0x002D5634
		public bool IsSeverable()
		{
			return !this.Abstract && this.Appendage && !this.Integral && !this.IsDependent();
		}

		// Token: 0x0600789F RID: 30879 RVA: 0x002D745F File Offset: 0x002D565F
		public bool SeverRequiresDecapitate()
		{
			return this.Mortal;
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x002D7467 File Offset: 0x002D5667
		public bool IsRegenerable()
		{
			return !this.Abstract && !this.IsDependent();
		}

		// Token: 0x060078A1 RID: 30881 RVA: 0x002D747E File Offset: 0x002D567E
		public bool IsRecoverable(Body UseParentBody = null)
		{
			return (this.IsDependent() && !this.IsUnsupported(UseParentBody)) || this.Abstract;
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x002D749C File Offset: 0x002D569C
		public void FindUnsupportedParts(ref List<BodyPart> Result)
		{
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].FindUnsupportedParts(ref Result);
					}
					i++;
				}
			}
			if (this.IsUnsupported(null))
			{
				if (Result == null)
				{
					Result = new List<BodyPart>();
				}
				Result.Add(this);
			}
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x002D7508 File Offset: 0x002D5708
		public List<BodyPart> FindUnsupportedParts()
		{
			List<BodyPart> list = null;
			this.FindUnsupportedParts(ref list);
			return list;
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x002D7520 File Offset: 0x002D5720
		public bool HasLaterality(int checkLaterality)
		{
			return (this.Laterality & checkLaterality) == checkLaterality;
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x002D752D File Offset: 0x002D572D
		[Obsolete]
		public bool IsFirstSlotForEquipped()
		{
			return this.FirstSlotForEquipped;
		}

		// Token: 0x060078A6 RID: 30886 RVA: 0x002D7535 File Offset: 0x002D5735
		[Obsolete]
		public bool IsFirstSlotForCybernetics()
		{
			return this.FirstSlotForCybernetics;
		}

		// Token: 0x060078A7 RID: 30887 RVA: 0x002D753D File Offset: 0x002D573D
		[Obsolete]
		public bool IsFirstSlotForDefaultBehavior()
		{
			return this.FirstSlotForDefaultBehavior;
		}

		// Token: 0x060078A8 RID: 30888 RVA: 0x002D7545 File Offset: 0x002D5745
		public bool IsDefaultBehaviorClearOfEquipped()
		{
			if (this.DefaultBehavior == null)
			{
				return false;
			}
			if (this.Equipped != null)
			{
				return false;
			}
			if (this.ParentBody == null)
			{
				return this.IsDefaultBehaviorClearOfEquipped(this.DefaultBehavior);
			}
			return this.ParentBody.IsDefaultBehaviorClearOfEquipped(this.DefaultBehavior);
		}

		// Token: 0x060078A9 RID: 30889 RVA: 0x002D7584 File Offset: 0x002D5784
		public bool IsDefaultBehaviorClearOfEquipped(GameObject GO)
		{
			if (GO == this.DefaultBehavior && this.Equipped != null)
			{
				return false;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (!this.Parts[i].IsDefaultBehaviorClearOfEquipped(GO))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x002D75DC File Offset: 0x002D57DC
		public void GetTypeArmorInfo(string ForType, ref GameObject First, ref int Count, ref int AV, ref int DV)
		{
			if (ForType == this.Type)
			{
				Count++;
				if (this.Equipped != null)
				{
					Armor part = this.Equipped.GetPart<Armor>();
					if (part != null && (part.WornOn == this.Type || part.WornOn == "*") && this.FirstSlotForEquipped)
					{
						if (First == null)
						{
							First = this.Equipped;
						}
						AV += part.AV;
						DV += part.DV;
					}
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetTypeArmorInfo(ForType, ref First, ref Count, ref AV, ref DV);
					}
					i++;
				}
			}
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x002D76B0 File Offset: 0x002D58B0
		public void RecalculateArmor()
		{
			if (this.Equipped != null)
			{
				Armor part = this.Equipped.GetPart<Armor>();
				if (part != null && (part.WornOn == this.Type || part.WornOn == "*") && this.FirstSlotForEquipped)
				{
					part.RecalculateArmor(null, null, false);
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].RecalculateArmor();
					}
					i++;
				}
			}
		}

		// Token: 0x060078AC RID: 30892 RVA: 0x002D774C File Offset: 0x002D594C
		public void RecalculateArmorExcept(GameObject obj)
		{
			if (this.Equipped != null && this.Equipped != obj)
			{
				Armor part = this.Equipped.GetPart<Armor>();
				if (part != null && (part.WornOn == this.Type || part.WornOn == "*") && this.FirstSlotForEquipped)
				{
					part.RecalculateArmor(null, null, false);
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].RecalculateArmorExcept(obj);
					}
					i++;
				}
			}
		}

		// Token: 0x060078AD RID: 30893 RVA: 0x002D77F0 File Offset: 0x002D59F0
		public void RecalculateTypeArmor(string ForType)
		{
			if (ForType == this.Type && this.Equipped != null)
			{
				Armor part = this.Equipped.GetPart<Armor>();
				if (part != null && (part.WornOn == this.Type || part.WornOn == "*") && this.FirstSlotForEquipped)
				{
					part.RecalculateArmor(null, null, false);
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].RecalculateTypeArmor(ForType);
					}
					i++;
				}
			}
		}

		// Token: 0x060078AE RID: 30894 RVA: 0x002D789C File Offset: 0x002D5A9C
		public void RecalculateTypeArmorExcept(string ForType, GameObject obj)
		{
			if (ForType == this.Type && this.Equipped != null && this.Equipped != obj)
			{
				Armor part = this.Equipped.GetPart<Armor>();
				if (part != null && (part.WornOn == this.Type || part.WornOn == "*") && this.FirstSlotForEquipped)
				{
					part.RecalculateArmor(null, null, false);
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].RecalculateTypeArmorExcept(ForType, obj);
					}
					i++;
				}
			}
		}

		// Token: 0x060078AF RID: 30895 RVA: 0x002D7950 File Offset: 0x002D5B50
		public bool CheckSlotEquippedMatch(GameObject obj, List<string> Slots)
		{
			if (obj == this.Equipped && (Slots.Contains(this.Type) || Slots.Contains(this.VariantType) || Slots.Contains("*")))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].CheckSlotEquippedMatch(obj, Slots))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B0 RID: 30896 RVA: 0x002D79D8 File Offset: 0x002D5BD8
		public bool CheckSlotEquippedMatch(GameObject obj, string Slot)
		{
			if (obj == this.Equipped && (Slot == this.Type || Slot == this.VariantType || Slot == "*"))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].CheckSlotEquippedMatch(obj, Slot))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B1 RID: 30897 RVA: 0x002D7A60 File Offset: 0x002D5C60
		public bool CheckSlotCyberneticsMatch(GameObject obj, List<string> Slots)
		{
			if (obj == this.Cybernetics && (Slots.Contains(this.Type) || Slots.Contains(this.VariantType) || Slots.Contains("*")))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].CheckSlotCyberneticsMatch(obj, Slots))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x002D7AE8 File Offset: 0x002D5CE8
		public bool CheckSlotCyberneticsMatch(GameObject obj, string Slot)
		{
			if (obj == this.Cybernetics && (Slot == this.Type || Slot == this.VariantType || Slot == "*"))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].CheckSlotCyberneticsMatch(obj, Slot))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B3 RID: 30899 RVA: 0x002D7B70 File Offset: 0x002D5D70
		public bool CheckSlotDefaultBehaviorMatch(GameObject obj, List<string> Slots)
		{
			if (obj == this.DefaultBehavior && (Slots.Contains(this.Type) || Slots.Contains(this.VariantType) || Slots.Contains("*")))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].CheckSlotDefaultBehaviorMatch(obj, Slots))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x002D7BF8 File Offset: 0x002D5DF8
		public bool CheckSlotDefaultBehaviorMatch(GameObject obj, string Slot)
		{
			if (obj == this.DefaultBehavior && (Slot == this.Type || Slot == this.VariantType || Slot == "*"))
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].CheckSlotDefaultBehaviorMatch(obj, Slot))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B5 RID: 30901 RVA: 0x002D7C80 File Offset: 0x002D5E80
		public bool HasReadyMissileWeapon(Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			MissileWeapon missileWeapon;
			if (this.Equipped != null && this.Equipped.TryGetPart<MissileWeapon>(out missileWeapon) && missileWeapon.FiresManually && missileWeapon.ValidSlotType(this.Type, false) && (Filter == null || Filter(this.Equipped)) && (PartFilter == null || PartFilter(missileWeapon)) && missileWeapon.ReadyToFire())
			{
				return true;
			}
			MissileWeapon missileWeapon2;
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && this.Cybernetics.TryGetPart<MissileWeapon>(out missileWeapon2) && missileWeapon2.FiresManually && missileWeapon2.ValidSlotType(this.Type, true) && (Filter == null || Filter(this.Cybernetics)) && (PartFilter == null || PartFilter(missileWeapon2)) && missileWeapon2.ReadyToFire())
			{
				return true;
			}
			MissileWeapon missileWeapon3;
			if (this.DefaultBehavior != null && this.DefaultBehavior.TryGetPart<MissileWeapon>(out missileWeapon3) && missileWeapon3.FiresManually && missileWeapon3.ValidSlotType(this.Type, false) && (Filter == null || Filter(this.DefaultBehavior)) && (PartFilter == null || PartFilter(missileWeapon3)) && this.IsDefaultBehaviorClearOfEquipped() && missileWeapon3.ReadyToFire())
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].HasReadyMissileWeapon(Filter, PartFilter))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B6 RID: 30902 RVA: 0x002D7DEC File Offset: 0x002D5FEC
		public bool HasMissileWeapon(Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			MissileWeapon missileWeapon;
			if (this.Equipped != null && this.Equipped.TryGetPart<MissileWeapon>(out missileWeapon) && missileWeapon.FiresManually && missileWeapon.ValidSlotType(this.Type, false) && (Filter == null || Filter(this.Equipped)) && (PartFilter == null || PartFilter(missileWeapon)))
			{
				return true;
			}
			MissileWeapon missileWeapon2;
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && this.Cybernetics.TryGetPart<MissileWeapon>(out missileWeapon2) && missileWeapon2.FiresManually && missileWeapon2.ValidSlotType(this.Type, true) && (Filter == null || Filter(this.Cybernetics)) && (PartFilter == null || PartFilter(missileWeapon2)))
			{
				return true;
			}
			MissileWeapon missileWeapon3;
			if (this.DefaultBehavior != null && this.DefaultBehavior.TryGetPart<MissileWeapon>(out missileWeapon3) && missileWeapon3.FiresManually && missileWeapon3.ValidSlotType(this.Type, false) && (Filter == null || Filter(this.DefaultBehavior)) && (PartFilter == null || PartFilter(missileWeapon3)) && this.IsDefaultBehaviorClearOfEquipped())
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].HasMissileWeapon(Filter, PartFilter))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B7 RID: 30903 RVA: 0x002D7F40 File Offset: 0x002D6140
		public void GetMissileWeapons(ref List<GameObject> List, Predicate<GameObject> Filter = null, Predicate<MissileWeapon> PartFilter = null)
		{
			MissileWeapon missileWeapon;
			if (this.Equipped != null && (List == null || !List.Contains(this.Equipped)) && this.Equipped.TryGetPart<MissileWeapon>(out missileWeapon) && missileWeapon.FiresManually && missileWeapon.ValidSlotType(this.Type, false) && (Filter == null || Filter(this.Equipped)) && (PartFilter == null || PartFilter(missileWeapon)) && (List == null || !List.Contains(this.Equipped)))
			{
				if (List == null)
				{
					List = Event.NewGameObjectList();
				}
				List.Add(this.Equipped);
			}
			MissileWeapon missileWeapon2;
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && (List == null || !List.Contains(this.Cybernetics)) && this.Cybernetics.TryGetPart<MissileWeapon>(out missileWeapon2) && missileWeapon2.FiresManually && missileWeapon2.ValidSlotType(this.Type, true) && (Filter == null || Filter(this.Cybernetics)) && (PartFilter == null || PartFilter(missileWeapon2)) && (List == null || !List.Contains(this.Cybernetics)))
			{
				if (List == null)
				{
					List = Event.NewGameObjectList();
				}
				List.Add(this.Cybernetics);
			}
			MissileWeapon missileWeapon3;
			if (this.DefaultBehavior != null && (List == null || !List.Contains(this.DefaultBehavior)) && this.DefaultBehavior.TryGetPart<MissileWeapon>(out missileWeapon3) && missileWeapon3.FiresManually && missileWeapon3.ValidSlotType(this.Type, false) && (Filter == null || Filter(this.DefaultBehavior)) && (PartFilter == null || PartFilter(missileWeapon3)) && (List == null || !List.Contains(this.DefaultBehavior)) && this.IsDefaultBehaviorClearOfEquipped())
			{
				if (List == null)
				{
					List = Event.NewGameObjectList();
				}
				List.Add(this.DefaultBehavior);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetMissileWeapons(ref List, Filter, PartFilter);
					}
					i++;
				}
			}
		}

		// Token: 0x060078B8 RID: 30904 RVA: 0x002D8150 File Offset: 0x002D6350
		public bool HasHeavyWeaponEquipped()
		{
			MissileWeapon missileWeapon;
			if (this.Equipped != null && this.Equipped.TryGetPart<MissileWeapon>(out missileWeapon) && missileWeapon.Skill == "HeavyWeapons")
			{
				return true;
			}
			MissileWeapon missileWeapon2;
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && this.Cybernetics.TryGetPart<MissileWeapon>(out missileWeapon2) && missileWeapon2.Skill == "HeavyWeapons")
			{
				return true;
			}
			MissileWeapon missileWeapon3;
			if (this.DefaultBehavior != null && this.DefaultBehavior.TryGetPart<MissileWeapon>(out missileWeapon3) && missileWeapon3.Skill == "HeavyWeapons")
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null && this.Parts[i].HasHeavyWeaponEquipped())
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078B9 RID: 30905 RVA: 0x002D8234 File Offset: 0x002D6434
		public GameObject GetFirstThrownWeapon(Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			ThrownWeapon thrownWeapon;
			if (this.Thrown && this.Equipped != null && (Filter == null || Filter(this.Equipped)) && (PartFilter == null || (this.Equipped.TryGetPart<ThrownWeapon>(out thrownWeapon) && PartFilter(thrownWeapon))))
			{
				return this.Equipped;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						GameObject firstThrownWeapon = this.Parts[i].GetFirstThrownWeapon(Filter, PartFilter);
						if (firstThrownWeapon != null)
						{
							return firstThrownWeapon;
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060078BA RID: 30906 RVA: 0x002D82CC File Offset: 0x002D64CC
		public void GetThrownWeapons(ref IList<GameObject> List, Predicate<GameObject> Filter = null, Predicate<ThrownWeapon> PartFilter = null)
		{
			ThrownWeapon thrownWeapon;
			if (this.Thrown && this.Equipped != null && (Filter == null || Filter(this.Equipped)) && (PartFilter == null || (this.Equipped.TryGetPart<ThrownWeapon>(out thrownWeapon) && PartFilter(thrownWeapon))))
			{
				if (List == null)
				{
					List = Event.NewGameObjectList();
				}
				List.Add(this.Equipped);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i] != null)
					{
						this.Parts[i].GetThrownWeapons(ref List, Filter, PartFilter);
					}
					i++;
				}
			}
		}

		// Token: 0x060078BB RID: 30907 RVA: 0x002D8370 File Offset: 0x002D6570
		public bool IsADefaultBehavior(GameObject obj)
		{
			if (obj == this.DefaultBehavior)
			{
				return obj != null;
			}
			if (this.Parts != null)
			{
				using (List<BodyPart>.Enumerator enumerator = this.Parts.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsADefaultBehavior(obj))
						{
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060078BC RID: 30908 RVA: 0x002D83E0 File Offset: 0x002D65E0
		[Obsolete]
		public bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060078BD RID: 30909 RVA: 0x002D83E4 File Offset: 0x002D65E4
		public void TurnTick(long TimeTick, int Amount)
		{
			if (this._Equipped != null && this.FirstSlotForEquipped)
			{
				this._Equipped.TurnTick(TimeTick, Amount);
			}
			if (this._Cybernetics != null && this._Cybernetics != this._Equipped && this.FirstSlotForCybernetics)
			{
				this._Cybernetics.TurnTick(TimeTick, Amount);
			}
			if (this._DefaultBehavior != null && this._DefaultBehavior != this._Equipped && this._DefaultBehavior != this._Cybernetics && this.FirstSlotForDefaultBehavior)
			{
				this._DefaultBehavior.TurnTick(TimeTick, Amount);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int num = this.Parts.Count;
				while (i < num)
				{
					BodyPart bodyPart = this.Parts[i];
					bodyPart.TurnTick(TimeTick, Amount);
					if (num != this.Parts.Count)
					{
						num = this.Parts.Count;
						if (i < num && this.Parts[i] != bodyPart)
						{
							i--;
						}
					}
					i++;
				}
			}
		}

		// Token: 0x060078BE RID: 30910 RVA: 0x002D84D8 File Offset: 0x002D66D8
		public void HandleEvent(EndTurnEvent E)
		{
			if (this._Equipped != null && this.FirstSlotForEquipped)
			{
				this._Equipped.HandleEvent(E);
			}
			if (this._Cybernetics != null && this._Cybernetics != this._Equipped && this.FirstSlotForCybernetics)
			{
				this._Cybernetics.HandleEvent(E);
			}
			if (this._DefaultBehavior != null && this._DefaultBehavior != this._Equipped && this._DefaultBehavior != this._Cybernetics && this.FirstSlotForDefaultBehavior)
			{
				this._DefaultBehavior.HandleEvent(E);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int num = this.Parts.Count;
				while (i < num)
				{
					BodyPart bodyPart = this.Parts[i];
					bodyPart.HandleEvent(E);
					if (num != this.Parts.Count)
					{
						num = this.Parts.Count;
						if (i < num && this.Parts[i] != bodyPart)
						{
							i--;
						}
					}
					i++;
				}
			}
		}

		// Token: 0x060078BF RID: 30911 RVA: 0x002D85CC File Offset: 0x002D67CC
		public bool WantEvent(int ID, int cascade)
		{
			if (!MinEvent.CascadeTo(cascade, 16) || (this.Flags & 131072) == 0)
			{
				if (this._Equipped != null && this.FirstSlotForEquipped && this._Equipped.WantEvent(ID, cascade))
				{
					return true;
				}
				if (this._Cybernetics != null && this._Cybernetics != this._Equipped && this.FirstSlotForCybernetics && this._Cybernetics.WantEvent(ID, cascade))
				{
					return true;
				}
				if (this._DefaultBehavior != null && this._DefaultBehavior != this._Equipped && this._DefaultBehavior != this._Cybernetics && this.FirstSlotForDefaultBehavior && this._DefaultBehavior.WantEvent(ID, cascade))
				{
					return true;
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].WantEvent(ID, cascade))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078C0 RID: 30912 RVA: 0x002D86BC File Offset: 0x002D68BC
		public bool HandleEvent(MinEvent E)
		{
			if (!E.CascadeTo(16) || (this.Flags & 131072) == 0)
			{
				int id = E.ID;
				int cascadeLevel = E.GetCascadeLevel();
				if (this._Equipped != null && this.FirstSlotForEquipped && this._Equipped.WantEvent(id, cascadeLevel) && !E.Dispatch(this._Equipped))
				{
					return false;
				}
				if (this._Cybernetics != null && this._Cybernetics != this._Equipped && this.FirstSlotForCybernetics && this._Cybernetics.WantEvent(id, cascadeLevel) && !E.Dispatch(this._Cybernetics))
				{
					return false;
				}
				if (this._DefaultBehavior != null && this._DefaultBehavior != this._Equipped && this._DefaultBehavior != this._Cybernetics && this.FirstSlotForDefaultBehavior && this._DefaultBehavior.WantEvent(id, cascadeLevel) && !E.Dispatch(this._DefaultBehavior))
				{
					return false;
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int num = this.Parts.Count;
				while (i < num)
				{
					BodyPart bodyPart = this.Parts[i];
					if (!bodyPart.HandleEvent(E))
					{
						return false;
					}
					if (num != this.Parts.Count)
					{
						num = this.Parts.Count;
						if (i < num && this.Parts[i] != bodyPart)
						{
							i--;
						}
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x060078C1 RID: 30913 RVA: 0x002D8818 File Offset: 0x002D6A18
		public bool ProcessQuerySlotList(QuerySlotListEvent E)
		{
			if (this.DefaultBehavior == null || !this.DefaultBehavior.HasRegisteredEvent("CanEquipOverDefaultBehavior") || this.DefaultBehavior.FireEvent(Event.New("CanEquipOverDefaultBehavior", "Subject", this.ParentBody.ParentObject, "Object", E.Object, "Part", this)))
			{
				QueryEquippableListEvent queryEquippableListEvent = QueryEquippableListEvent.FromPool(this.ParentBody.ParentObject, E.Object, this.Type, false, true);
				E.Object.HandleEvent(queryEquippableListEvent);
				if (queryEquippableListEvent.List.Count > 0)
				{
					E.SlotList.Add(this);
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int num = this.Parts.Count;
				while (i < num)
				{
					BodyPart bodyPart = this.Parts[i];
					if (!bodyPart.ProcessQuerySlotList(E))
					{
						return false;
					}
					if (this.Parts.Count != num)
					{
						num = this.Parts.Count;
						if (i < num && this.Parts[i] != bodyPart)
						{
							i--;
						}
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x060078C2 RID: 30914 RVA: 0x002D8928 File Offset: 0x002D6B28
		public bool ProcessExtrinsicValue(GetExtrinsicValueEvent E)
		{
			if (this.Equipped != null)
			{
				E.Value += this.Equipped.Value;
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped)
			{
				E.Value += this.Cybernetics.Value;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (!this.Parts[i].ProcessExtrinsicValue(E))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x060078C3 RID: 30915 RVA: 0x002D89BC File Offset: 0x002D6BBC
		public bool ProcessExtrinsicWeight(GetExtrinsicWeightEvent E)
		{
			if (this.Equipped != null)
			{
				E.Weight += this.Equipped.GetWeight();
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped)
			{
				E.Weight += this.Cybernetics.GetWeight();
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (!this.Parts[i].ProcessExtrinsicWeight(E))
					{
						return false;
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x060078C4 RID: 30916 RVA: 0x002D8A50 File Offset: 0x002D6C50
		private static void GetCreatureFromSpec(string Blueprint, string Type, string Tag, string Base, Predicate<GameObjectBlueprint> Filter, ref GameObject Creature)
		{
			if (Blueprint != null)
			{
				Creature = GameObject.Create(Blueprint);
				return;
			}
			if (Tag != null && Base != null)
			{
				if (Filter == null)
				{
					Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body") && BP.HasTag(Tag, false) && BP.DescendsFrom(Base));
					return;
				}
				Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body") && BP.HasTag(Tag, false) && BP.DescendsFrom(Base) && Filter(BP));
				return;
			}
			else if (Tag != null)
			{
				if (Filter == null)
				{
					Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body") && BP.HasTag(Tag, false));
					return;
				}
				Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body") && BP.HasTag(Tag, false) && Filter(BP));
				return;
			}
			else if (Base != null)
			{
				if (Filter == null)
				{
					Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body") && BP.DescendsFrom(Base));
					return;
				}
				Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body") && BP.DescendsFrom(Base) && Filter(BP));
				return;
			}
			else
			{
				if (Filter == null)
				{
					Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body"));
					return;
				}
				Creature = EncountersAPI.GetACreature((GameObjectBlueprint BP) => BP.HasPart("Body") && Filter(BP));
				return;
			}
		}

		// Token: 0x060078C5 RID: 30917 RVA: 0x002D8B80 File Offset: 0x002D6D80
		public static GameObject MakeSeveredBodyPart(string Blueprint = null, string Type = null, string Tag = null, string Base = null, Predicate<GameObjectBlueprint> Filter = null, GameObject PutIn = null)
		{
			GameObject gameObject = null;
			int num = 0;
			while (++num < 10)
			{
				try
				{
					BodyPart.GetCreatureFromSpec(Blueprint, Type, Tag, Base, Filter, ref gameObject);
					if (gameObject == null)
					{
						return null;
					}
					Body body = gameObject.Body;
					BodyPart bodyPart2;
					if (Type == null)
					{
						BodyPart.SeverWork.Clear();
						foreach (BodyPart bodyPart in body.LoopParts())
						{
							if (bodyPart.IsSeverable())
							{
								BodyPart.SeverWork.Add(bodyPart);
							}
						}
						bodyPart2 = BodyPart.SeverWork.GetRandomElement<BodyPart>(null);
					}
					else
					{
						bodyPart2 = body.GetFirstPart(Type);
					}
					if (bodyPart2 != null && bodyPart2.IsSeverable())
					{
						GameObject gameObject2 = bodyPart2.Dismember(false);
						if (gameObject2 != null)
						{
							if (PutIn != null)
							{
								PutIn.FireEvent(Event.New("CommandTakeObject", "Object", gameObject2, "EnergyCost", 0).SetSilent(true));
							}
							return gameObject2;
						}
					}
				}
				finally
				{
					if (gameObject != null)
					{
						gameObject.Obliterate(null, false, null);
					}
				}
				if (Blueprint != null)
				{
					return null;
				}
				continue;
			}
			return null;
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x002D8CA8 File Offset: 0x002D6EA8
		public static void MakeSeveredBodyParts(int Number, string Blueprint = null, string Type = null, string Tag = null, string Base = null, Predicate<GameObjectBlueprint> Filter = null, GameObject PutIn = null, List<GameObject> Return = null)
		{
			GameObject gameObject = null;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (++num2 < 10)
			{
				for (;;)
				{
					GameObject gameObject2 = gameObject;
					BodyPart.GetCreatureFromSpec(Blueprint, Type, Tag, Base, Filter, ref gameObject);
					if (gameObject != null)
					{
						Body body = gameObject.Body;
						BodyPart bodyPart2;
						if (Type == null)
						{
							BodyPart.SeverWork.Clear();
							foreach (BodyPart bodyPart in body.LoopParts())
							{
								if (bodyPart.IsSeverable())
								{
									BodyPart.SeverWork.Add(bodyPart);
								}
							}
							bodyPart2 = BodyPart.SeverWork.GetRandomElement<BodyPart>(null);
							if (gameObject == gameObject2)
							{
								if (num - BodyPart.SeverWork.Count >= Number / 2)
								{
									gameObject.Obliterate(null, false, null);
									gameObject = null;
									continue;
								}
							}
							else
							{
								num = BodyPart.SeverWork.Count;
							}
						}
						else
						{
							bodyPart2 = body.GetFirstPart(Type);
						}
						try
						{
							if (bodyPart2 != null && bodyPart2.IsSeverable())
							{
								GameObject gameObject3 = bodyPart2.Dismember(false);
								if (gameObject3 != null)
								{
									if (PutIn != null)
									{
										PutIn.FireEvent(Event.New("CommandTakeObject", "Object", gameObject3, "EnergyCost", 0).SetSilent(true));
									}
									if (Return != null)
									{
										Return.Add(gameObject3);
									}
									if (++num3 >= Number)
									{
										goto IL_014E;
									}
									continue;
								}
							}
						}
						finally
						{
							if (Blueprint == null)
							{
								gameObject.Obliterate(null, false, null);
								gameObject = null;
							}
						}
						break;
					}
					goto IL_014E;
				}
				if (Blueprint != null)
				{
					break;
				}
			}
			IL_014E:
			if (gameObject != null)
			{
				gameObject.Obliterate(null, false, null);
			}
		}

		// Token: 0x060078C7 RID: 30919 RVA: 0x002D8E2C File Offset: 0x002D702C
		public void TypeDump(StringBuilder SB)
		{
			SB.Append(this.Type).Append(' ').Append(this.VariantType)
				.Append(' ')
				.Append(this.Name)
				.Append('\n');
			if (this.Parts != null)
			{
				foreach (BodyPart bodyPart in this.Parts)
				{
					bodyPart.TypeDump(SB);
				}
			}
		}

		// Token: 0x060078C8 RID: 30920 RVA: 0x002D8EC0 File Offset: 0x002D70C0
		public void GetMobilityProvidingParts(List<BodyPart> Return)
		{
			if (this.Mobility > 0)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].GetMobilityProvidingParts(Return);
					i++;
				}
			}
		}

		// Token: 0x060078C9 RID: 30921 RVA: 0x002D8F10 File Offset: 0x002D7110
		public void GetConcreteMobilityProvidingParts(List<BodyPart> Return)
		{
			if (this.Mobility > 0 && !this.Abstract)
			{
				Return.Add(this);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].GetConcreteMobilityProvidingParts(Return);
					i++;
				}
			}
		}

		// Token: 0x060078CA RID: 30922 RVA: 0x002D8F68 File Offset: 0x002D7168
		public void GetContents(List<GameObject> Return)
		{
			if (this.Equipped != null && !Return.Contains(this.Equipped))
			{
				Return.Add(this.Equipped);
			}
			if (this.Cybernetics != null && this.Cybernetics != this.Equipped && !Return.Contains(this.Cybernetics))
			{
				Return.Add(this.Cybernetics);
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].GetContents(Return);
					i++;
				}
			}
		}

		// Token: 0x060078CB RID: 30923 RVA: 0x002D8FFC File Offset: 0x002D71FC
		public int GetPartDepth(BodyPart Part, int Depth)
		{
			if (Part != this)
			{
				if (this.Parts != null)
				{
					Depth++;
					int i = 0;
					int count = this.Parts.Count;
					while (i < count)
					{
						int partDepth = this.Parts[i].GetPartDepth(Part, Depth);
						if (partDepth != -1)
						{
							return partDepth;
						}
						i++;
					}
				}
				return -1;
			}
			if (!this.Contact && Depth == 1)
			{
				return 0;
			}
			return Depth;
		}

		// Token: 0x060078CC RID: 30924 RVA: 0x002D9060 File Offset: 0x002D7260
		public int GetMaximumElectricalConductivityOfMobilityBodyParts()
		{
			int num = ((this.Mobility > 0) ? this.GetElectricalConductivity() : 0);
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					int maximumElectricalConductivityOfMobilityBodyParts = this.Parts[i].GetMaximumElectricalConductivityOfMobilityBodyParts();
					if (maximumElectricalConductivityOfMobilityBodyParts > num)
					{
						num = maximumElectricalConductivityOfMobilityBodyParts;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x060078CD RID: 30925 RVA: 0x002D90BC File Offset: 0x002D72BC
		public int GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(out GameObject CriticalObject)
		{
			CriticalObject = null;
			int num = ((this.Mobility > 0) ? this.GetEffectiveElectricalConductivity(out CriticalObject) : 0);
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					GameObject gameObject;
					int maximumEffectiveElectricalConductivityOfMobilityBodyParts = this.Parts[i].GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(out gameObject);
					if (maximumEffectiveElectricalConductivityOfMobilityBodyParts > num)
					{
						num = maximumEffectiveElectricalConductivityOfMobilityBodyParts;
						CriticalObject = gameObject;
					}
					i++;
				}
			}
			return num;
		}

		// Token: 0x060078CE RID: 30926 RVA: 0x002D9124 File Offset: 0x002D7324
		public int GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts()
		{
			GameObject gameObject;
			return this.GetMaximumEffectiveElectricalConductivityOfMobilityBodyParts(out gameObject);
		}

		// Token: 0x060078CF RID: 30927 RVA: 0x002D9139 File Offset: 0x002D7339
		public int GetElectricalConductivity()
		{
			return BodyPartCategory.GetElectricalConductivity(this.Category);
		}

		// Token: 0x060078D0 RID: 30928 RVA: 0x002D9148 File Offset: 0x002D7348
		public int GetEffectiveElectricalConductivity(out GameObject CriticalObject)
		{
			CriticalObject = null;
			int num = BodyPartCategory.GetElectricalConductivity(this.Category);
			if (this.Equipped != null)
			{
				int electricalConductivity = this.Equipped.GetElectricalConductivity(null, 0);
				if (electricalConductivity < num)
				{
					num = electricalConductivity;
					CriticalObject = this.Equipped;
				}
			}
			return num;
		}

		// Token: 0x060078D1 RID: 30929 RVA: 0x002D918C File Offset: 0x002D738C
		public int GetEffectiveElectricalConductivity()
		{
			GameObject gameObject;
			return this.GetEffectiveElectricalConductivity(out gameObject);
		}

		// Token: 0x060078D2 RID: 30930 RVA: 0x002D91A4 File Offset: 0x002D73A4
		public int GetTotalElectricalConductivity()
		{
			int num = this.GetElectricalConductivity();
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					num += this.Parts[i].GetTotalElectricalConductivity();
					i++;
				}
			}
			return num;
		}

		// Token: 0x060078D3 RID: 30931 RVA: 0x002D91F0 File Offset: 0x002D73F0
		public void ModifyNameAndDescriptionRecursively(string NameMod, string DescMod)
		{
			this.Name = NameMod + "-" + this.Name;
			this.Description = DescMod + "-" + this.Description;
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					this.Parts[i].ModifyNameAndDescriptionRecursively(NameMod, DescMod);
					i++;
				}
			}
		}

		// Token: 0x060078D4 RID: 30932 RVA: 0x002D9260 File Offset: 0x002D7460
		public BodyPart ReduceParts(Func<BodyPart, BodyPart, BodyPart> Reduction, BodyPart Result = null)
		{
			Result = Reduction(Result, this);
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					Result = this.Parts[i].ReduceParts(Reduction, Result);
					i++;
				}
			}
			return Result;
		}

		// Token: 0x060078D5 RID: 30933 RVA: 0x002D92AD File Offset: 0x002D74AD
		public bool IsCategoryLive()
		{
			return BodyPartCategory.IsLiveCategory(this.Category);
		}

		// Token: 0x060078D6 RID: 30934 RVA: 0x002D92BA File Offset: 0x002D74BA
		public bool CanReceiveCyberneticImplant()
		{
			return !this.Extrinsic && this.Category == 1;
		}

		// Token: 0x060078D7 RID: 30935 RVA: 0x002D92D4 File Offset: 0x002D74D4
		public bool HasBlueprint(string Blueprint)
		{
			if (Blueprint.IsNullOrEmpty())
			{
				return false;
			}
			GameObject equipped = this.Equipped;
			if (((equipped != null) ? equipped.Blueprint : null) == Blueprint)
			{
				return true;
			}
			GameObject cybernetics = this.Cybernetics;
			if (((cybernetics != null) ? cybernetics.Blueprint : null) == Blueprint)
			{
				return true;
			}
			GameObject defaultBehavior = this.DefaultBehavior;
			if (((defaultBehavior != null) ? defaultBehavior.Blueprint : null) == Blueprint)
			{
				return true;
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					if (this.Parts[i].HasBlueprint(Blueprint))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x060078D8 RID: 30936 RVA: 0x002D9378 File Offset: 0x002D7578
		public BodyPart GetUnequippedPreferredBodyPartOrAlternate(string PreferredType, string AlternateType, ref BodyPart Alternate)
		{
			if (this.Equipped == null)
			{
				if (this.Type == PreferredType)
				{
					return this;
				}
				if (this.Type == AlternateType && Alternate == null)
				{
					Alternate = this;
				}
			}
			if (this.Parts != null)
			{
				int i = 0;
				int count = this.Parts.Count;
				while (i < count)
				{
					BodyPart unequippedPreferredBodyPartOrAlternate = this.Parts[i].GetUnequippedPreferredBodyPartOrAlternate(PreferredType, AlternateType, ref Alternate);
					if (unequippedPreferredBodyPartOrAlternate != null)
					{
						return unequippedPreferredBodyPartOrAlternate;
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060078D9 RID: 30937 RVA: 0x002D93ED File Offset: 0x002D75ED
		[Obsolete("Use IDMatch(int)")]
		public bool idMatch(int testID)
		{
			return this._ID == testID;
		}

		// Token: 0x040036DA RID: 14042
		public const int FLAG_DEFAULT_PRIMARY = 1;

		// Token: 0x040036DB RID: 14043
		public const int FLAG_PREFERRED_PRIMARY = 2;

		// Token: 0x040036DC RID: 14044
		public const int FLAG_PRIMARY = 4;

		// Token: 0x040036DD RID: 14045
		public const int FLAG_NATIVE = 8;

		// Token: 0x040036DE RID: 14046
		public const int FLAG_APPENDAGE = 16;

		// Token: 0x040036DF RID: 14047
		public const int FLAG_INTEGRAL = 32;

		// Token: 0x040036E0 RID: 14048
		public const int FLAG_MORTAL = 64;

		// Token: 0x040036E1 RID: 14049
		public const int FLAG_ABSTRACT = 128;

		// Token: 0x040036E2 RID: 14050
		public const int FLAG_EXTRINSIC = 256;

		// Token: 0x040036E3 RID: 14051
		public const int FLAG_DYNAMIC = 512;

		// Token: 0x040036E4 RID: 14052
		public const int FLAG_PLURAL = 1024;

		// Token: 0x040036E5 RID: 14053
		public const int FLAG_MASS = 2048;

		// Token: 0x040036E6 RID: 14054
		public const int FLAG_CONTACT = 4096;

		// Token: 0x040036E7 RID: 14055
		public const int FLAG_IGNORE_POSITION = 8192;

		// Token: 0x040036E8 RID: 14056
		public const int FLAG_FIRST_EQUIPPED = 16384;

		// Token: 0x040036E9 RID: 14057
		public const int FLAG_FIRST_CYBERNETIC = 32768;

		// Token: 0x040036EA RID: 14058
		public const int FLAG_FIRST_DEFAULT = 65536;

		// Token: 0x040036EB RID: 14059
		public const int FLAG_THROWN = 131072;

		// Token: 0x040036EC RID: 14060
		public string Type;

		// Token: 0x040036ED RID: 14061
		public string VariantType;

		// Token: 0x040036EE RID: 14062
		public string Description;

		// Token: 0x040036EF RID: 14063
		public string DescriptionPrefix;

		// Token: 0x040036F0 RID: 14064
		public string Name;

		// Token: 0x040036F1 RID: 14065
		public string SupportsDependent;

		// Token: 0x040036F2 RID: 14066
		public string DependsOn;

		// Token: 0x040036F3 RID: 14067
		public string RequiresType;

		// Token: 0x040036F4 RID: 14068
		public string Manager;

		// Token: 0x040036F5 RID: 14069
		public int Category = 1;

		// Token: 0x040036F6 RID: 14070
		public int Laterality;

		// Token: 0x040036F7 RID: 14071
		public int RequiresLaterality = 65535;

		// Token: 0x040036F8 RID: 14072
		public int Mobility;

		// Token: 0x040036F9 RID: 14073
		public int Flags = 4096;

		// Token: 0x040036FA RID: 14074
		[NonSerialized]
		public Body ParentBody;

		// Token: 0x040036FB RID: 14075
		[NonSerialized]
		public BodyPart ParentPart;

		// Token: 0x040036FC RID: 14076
		public string DefaultBehaviorBlueprint;

		// Token: 0x040036FD RID: 14077
		[NonSerialized]
		public GameObject _DefaultBehavior;

		// Token: 0x040036FE RID: 14078
		public int Position = -1;

		// Token: 0x040036FF RID: 14079
		[NonSerialized]
		public GameObject _Cybernetics;

		// Token: 0x04003700 RID: 14080
		[NonSerialized]
		public GameObject _Equipped;

		// Token: 0x04003701 RID: 14081
		[NonSerialized]
		public List<BodyPart> Parts;

		// Token: 0x04003702 RID: 14082
		public static int IDPreloadSequence = -1;

		// Token: 0x04003703 RID: 14083
		public int _ID;

		// Token: 0x04003704 RID: 14084
		[NonSerialized]
		private static List<BodyPart> ToClearEquip = new List<BodyPart>();

		// Token: 0x04003705 RID: 14085
		[NonSerialized]
		private static List<BodyPart> SeverWork = new List<BodyPart>(8);
	}
}
