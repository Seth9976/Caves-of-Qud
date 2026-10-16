using System;
using System.Reflection;
using XRL.World;

namespace Sheeter
{
	// Token: 0x0200030E RID: 782
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	public class Property : BlueprintElement
	{
		// Token: 0x06001CE2 RID: 7394 RVA: 0x0008DE4C File Offset: 0x0008C04C
		public override string GetFrom(GameObjectBlueprint Blueprint)
		{
			PropertyInfo property = typeof(GameObjectBlueprint).GetProperty(this.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null)
			{
				return null;
			}
			object value = property.GetValue(Blueprint);
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}
	}
}
