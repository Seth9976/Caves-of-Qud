using System;
using System.Collections.Generic;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F65 RID: 3941
	[Serializable]
	public class SocialRoles : IPart
	{
		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06009CFC RID: 40188 RVA: 0x0038DB8E File Offset: 0x0038BD8E
		// (set) Token: 0x06009CFB RID: 40187 RVA: 0x0038DB5B File Offset: 0x0038BD5B
		public string Roles
		{
			get
			{
				return this._Roles;
			}
			set
			{
				this._Roles = value;
				this._RoleList = null;
				if (this._Roles.Contains(","))
				{
					this.RoleList.Sort();
					this.UpdateRoles();
				}
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06009CFD RID: 40189 RVA: 0x0038DB98 File Offset: 0x0038BD98
		public List<string> RoleList
		{
			get
			{
				if (this._RoleList == null)
				{
					if (this._Roles.IsNullOrEmpty())
					{
						this._RoleList = new List<string>();
					}
					else
					{
						this._RoleList = new List<string>(this._Roles.Split(',', StringSplitOptions.None));
					}
				}
				return this._RoleList;
			}
		}

		// Token: 0x06009CFE RID: 40190 RVA: 0x0038DBE6 File Offset: 0x0038BDE6
		public void AddRole(string Role)
		{
			this.RoleList.Add(Role);
			this.RoleList.Sort();
			this.UpdateRoles();
		}

		// Token: 0x06009CFF RID: 40191 RVA: 0x0038DC05 File Offset: 0x0038BE05
		public void RequireRole(string Role)
		{
			if (!this.RoleList.Contains(Role))
			{
				this.AddRole(Role);
			}
		}

		// Token: 0x06009D00 RID: 40192 RVA: 0x0038DC1C File Offset: 0x0038BE1C
		public void RemoveRole(string Role)
		{
			this.RoleList.Remove(Role);
			if (this.RoleList.Count == 0)
			{
				this.ParentObject.RemovePart(this);
				return;
			}
			this.UpdateRoles();
		}

		// Token: 0x06009D01 RID: 40193 RVA: 0x0038DC4B File Offset: 0x0038BE4B
		public void UpdateRoles()
		{
			this._Roles = string.Join(",", this.RoleList.ToArray());
		}

		// Token: 0x06009D02 RID: 40194 RVA: 0x0038DC68 File Offset: 0x0038BE68
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009D03 RID: 40195 RVA: 0x0038DC86 File Offset: 0x0038BE86
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject && Cloning.IsCloning(E.Context))
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D04 RID: 40196 RVA: 0x0038DCB4 File Offset: 0x0038BEB4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.WithoutTitles)
			{
				int i = 0;
				int count = this.RoleList.Count;
				while (i < count)
				{
					string text = this.RoleList[i];
					if (text.Contains("="))
					{
						text = GameText.VariableReplace(text, this.ParentObject, null, E.NoColor);
					}
					E.AddTitle(text, 0);
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040E7 RID: 16615
		public string _Roles;

		// Token: 0x040040E8 RID: 16616
		[NonSerialized]
		private List<string> _RoleList;
	}
}
