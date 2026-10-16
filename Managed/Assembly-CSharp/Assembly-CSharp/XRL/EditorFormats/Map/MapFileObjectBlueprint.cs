using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL.EditorFormats.Map
{
	// Token: 0x020005C6 RID: 1478
	public class MapFileObjectBlueprint
	{
		// Token: 0x06003EF9 RID: 16121 RVA: 0x001365C6 File Offset: 0x001347C6
		public MapFileObjectBlueprint(string Name, string Owner = null, string Part = null)
		{
			this.Name = Name;
			this.Owner = Owner;
			this.Part = Part;
		}

		// Token: 0x06003EFA RID: 16122 RVA: 0x001365E4 File Offset: 0x001347E4
		public MapFileObjectBlueprint(MapFileObjectBlueprint bp)
		{
			this.Name = bp.Name;
			this.Owner = bp.Owner;
			this.Part = bp.Part;
			if (!bp.Properties.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				this.Properties = new Dictionary<string, string>(bp.Properties);
			}
			if (!bp.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				this.IntProperties = new Dictionary<string, int>(bp.IntProperties);
			}
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x00136657 File Offset: 0x00134857
		public static bool operator ==(MapFileObjectBlueprint a, MapFileObjectBlueprint b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x00136660 File Offset: 0x00134860
		public static bool operator !=(MapFileObjectBlueprint a, MapFileObjectBlueprint b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0013666C File Offset: 0x0013486C
		public override int GetHashCode()
		{
			int num = 37;
			num = num * 53 + this.Name.GetHashCode();
			if (!this.Owner.IsNullOrEmpty())
			{
				num = num * 53 + this.Owner.GetHashCode();
			}
			if (!this.Part.IsNullOrEmpty())
			{
				num = num * 53 + this.Part.GetHashCode();
			}
			if (!this.Properties.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.Properties)
				{
					num = num * 53 + keyValuePair.Key.GetHashCode();
					num = num * 53 + keyValuePair.Value.GetHashCode();
				}
			}
			if (!this.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				foreach (KeyValuePair<string, int> keyValuePair2 in this.IntProperties)
				{
					num = num * 53 + keyValuePair2.Key.GetHashCode();
					num = num * 53 + keyValuePair2.Value.GetHashCode();
				}
			}
			return num;
		}

		// Token: 0x06003EFE RID: 16126 RVA: 0x001367AC File Offset: 0x001349AC
		public override bool Equals(object obj)
		{
			MapFileObjectBlueprint mapFileObjectBlueprint = obj as MapFileObjectBlueprint;
			return mapFileObjectBlueprint != null && (this.Name == mapFileObjectBlueprint.Name && (this.Owner == mapFileObjectBlueprint.Owner || (this.Owner.IsNullOrEmpty() && mapFileObjectBlueprint.Owner.IsNullOrEmpty())) && (this.Part == mapFileObjectBlueprint.Part || (this.Part.IsNullOrEmpty() && mapFileObjectBlueprint.Part.IsNullOrEmpty())) && this.Properties.PairEquals<string, string>(mapFileObjectBlueprint.Properties, true)) && this.IntProperties.PairEquals<string, int>(mapFileObjectBlueprint.IntProperties, true);
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x00136860 File Offset: 0x00134A60
		public bool HasProperty(string Key)
		{
			return (this.Properties != null && this.Properties.ContainsKey(Key)) || (this.IntProperties != null && this.IntProperties.ContainsKey(Key));
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x00136890 File Offset: 0x00134A90
		public GameObject Create(Action<GameObject> BeforeObjectCreated = null)
		{
			GameObject gameObject = GameObjectFactory.Factory.CreateObject(this.Name, BeforeObjectCreated);
			bool flag = false;
			if (!this.Owner.IsNullOrEmpty() && gameObject.Physics != null)
			{
				gameObject.Physics.Owner = this.Owner;
				flag = true;
			}
			if (!this.Part.IsNullOrEmpty())
			{
				Type type = ModManager.ResolveType("XRL.World.Parts." + this.Part, false, false, true);
				gameObject.AddPart(Activator.CreateInstance(type) as IPart, true, false);
				flag = true;
			}
			if (!this.Properties.IsNullOrEmpty<KeyValuePair<string, string>>())
			{
				flag = true;
				foreach (KeyValuePair<string, string> keyValuePair in this.Properties)
				{
					gameObject.Property[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			if (!this.IntProperties.IsNullOrEmpty<KeyValuePair<string, int>>())
			{
				flag = true;
				foreach (KeyValuePair<string, int> keyValuePair2 in this.IntProperties)
				{
					gameObject.IntProperty[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			if (flag && (gameObject.HasTag("Immutable") || gameObject.HasTag("ImmutableWhenUnexplored")))
			{
				gameObject.SetIntProperty("ForceMutableSave", 1, false);
			}
			return gameObject;
		}

		// Token: 0x04001F1B RID: 7963
		public string Name;

		// Token: 0x04001F1C RID: 7964
		public string Owner;

		// Token: 0x04001F1D RID: 7965
		public string Part;

		// Token: 0x04001F1E RID: 7966
		public Dictionary<string, string> Properties;

		// Token: 0x04001F1F RID: 7967
		public Dictionary<string, int> IntProperties;
	}
}
