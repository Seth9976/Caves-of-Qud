using System;
using System.Reflection;
using XRL.World;

namespace Sheeter
{
	// Token: 0x0200030D RID: 781
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class Field : BlueprintElement
	{
		// Token: 0x06001CE0 RID: 7392 RVA: 0x0008DE14 File Offset: 0x0008C014
		public override string GetFrom(GameObjectBlueprint Blueprint)
		{
			FieldInfo field = typeof(GameObjectBlueprint).GetField(this.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				return null;
			}
			object value = field.GetValue(Blueprint);
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}
	}
}
