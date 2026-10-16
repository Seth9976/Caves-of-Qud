using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200056F RID: 1391
	internal readonly struct UxmlTypeDescription
	{
		// Token: 0x060029F1 RID: 10737 RVA: 0x0009732C File Offset: 0x0009552C
		public UxmlTypeDescription(Type type)
		{
			bool flag = !typeof(UxmlSerializedData).IsAssignableFrom(type);
			if (flag)
			{
				throw new ArgumentException();
			}
			this.type = type;
			this.attributeDescriptions = new List<UxmlDescription>();
			this.uxmlNameToIndex = new Dictionary<string, int>();
			this.cSharpNameToIndex = new Dictionary<string, int>();
			this.GenerateAttributeDescription(type);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00097388 File Offset: 0x00095588
		private void GenerateAttributeDescription(Type t)
		{
			bool flag = t.BaseType != null && t.BaseType != UxmlTypeDescription.s_UxmlSerializedDataType;
			if (flag)
			{
				UxmlTypeDescription description = UxmlDescriptionRegistry.GetDescription(t.BaseType);
				this.attributeDescriptions.AddRange(description.attributeDescriptions);
				foreach (KeyValuePair<string, int> keyValuePair in description.uxmlNameToIndex)
				{
					this.uxmlNameToIndex[keyValuePair.Key] = keyValuePair.Value;
				}
				foreach (KeyValuePair<string, int> keyValuePair2 in description.cSharpNameToIndex)
				{
					this.cSharpNameToIndex[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			UxmlAttributeNames[] array;
			bool flag2 = UxmlDescriptionCache.TryGetCachedDescription(t, out array);
			if (flag2)
			{
				foreach (UxmlAttributeNames uxmlAttributeNames in array)
				{
					FieldInfo field = t.GetField(uxmlAttributeNames.fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
					bool flag3 = null == field;
					if (flag3)
					{
						Debug.Log(t.DeclaringType.Name + ": " + uxmlAttributeNames.fieldName + " not found.");
					}
					string text = UxmlUtility.ValidateUxmlName(uxmlAttributeNames.uxmlName);
					bool flag4 = text != null;
					if (flag4)
					{
						Debug.LogError(string.Format("Invalid UXML name '{0}' for attribute '{1}' in type '{2}'. {3}", new object[]
						{
							uxmlAttributeNames.uxmlName,
							field.Name,
							field.DeclaringType.DeclaringType,
							text
						}));
					}
					else
					{
						int num;
						bool flag5 = this.uxmlNameToIndex.TryGetValue(uxmlAttributeNames.uxmlName, out num);
						string text2 = null;
						bool flag6 = flag5;
						if (flag6)
						{
							text2 = this.attributeDescriptions[num].overriddenCSharpName ?? this.attributeDescriptions[num].cSharpName;
						}
						UxmlDescription uxmlDescription = new UxmlDescription(field, uxmlAttributeNames, text2);
						bool flag7 = flag5;
						if (flag7)
						{
							this.attributeDescriptions[num] = uxmlDescription;
						}
						else
						{
							this.attributeDescriptions.Add(uxmlDescription);
							num = this.attributeDescriptions.Count - 1;
							this.uxmlNameToIndex[uxmlAttributeNames.uxmlName] = num;
						}
						this.cSharpNameToIndex[field.Name] = num;
					}
				}
			}
			else
			{
				FieldInfo[] fields = t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);
				bool flag8 = fields.Length == 0;
				if (!flag8)
				{
					foreach (FieldInfo fieldInfo in fields)
					{
						bool flag9 = fieldInfo.GetCustomAttribute<UxmlIgnoreAttribute>() != null;
						if (!flag9)
						{
							string name = fieldInfo.Name;
							ValueTuple<bool, string, string[]> uxmlNames = UxmlTypeDescription.GetUxmlNames(fieldInfo);
							bool flag10 = !uxmlNames.Item1;
							if (!flag10)
							{
								string item = uxmlNames.Item2;
								int num2;
								bool flag11 = this.uxmlNameToIndex.TryGetValue(item, out num2);
								string text3 = null;
								bool flag12 = flag11;
								if (flag12)
								{
									text3 = this.attributeDescriptions[num2].overriddenCSharpName ?? this.attributeDescriptions[num2].cSharpName;
								}
								UxmlDescription uxmlDescription2 = new UxmlDescription(uxmlNames.Item2, name, text3, fieldInfo, uxmlNames.Item3);
								bool flag13 = flag11;
								if (flag13)
								{
									this.attributeDescriptions[num2] = uxmlDescription2;
								}
								else
								{
									this.attributeDescriptions.Add(uxmlDescription2);
									num2 = this.attributeDescriptions.Count - 1;
									this.uxmlNameToIndex[item] = num2;
								}
								this.cSharpNameToIndex[fieldInfo.Name] = num2;
							}
						}
					}
				}
			}
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00097788 File Offset: 0x00095988
		[return: TupleElementNames(new string[] { "valid", "uxmlName", "obsoleteNames" })]
		internal static ValueTuple<bool, string, string[]> GetUxmlNames(FieldInfo fieldInfo)
		{
			List<string> list;
			ValueTuple<bool, string, string[]> valueTuple;
			using (CollectionPool<List<string>, string>.Get(out list))
			{
				HashSet<string> hashSet;
				using (CollectionPool<HashSet<string>, string>.Get(out hashSet))
				{
					IEnumerable<FormerlySerializedAsAttribute> customAttributes = fieldInfo.GetCustomAttributes<FormerlySerializedAsAttribute>();
					foreach (FormerlySerializedAsAttribute formerlySerializedAsAttribute in customAttributes)
					{
						bool flag = hashSet.Add(formerlySerializedAsAttribute.oldName);
						if (flag)
						{
							list.Add(formerlySerializedAsAttribute.oldName);
						}
					}
					UxmlAttributeAttribute customAttribute = fieldInfo.GetCustomAttribute<UxmlAttributeAttribute>();
					bool flag2 = customAttribute != null;
					if (flag2)
					{
						bool flag3 = customAttribute.obsoleteNames != null;
						if (flag3)
						{
							foreach (string text in (customAttribute != null) ? customAttribute.obsoleteNames : null)
							{
								bool flag4 = hashSet.Add(text);
								if (flag4)
								{
									list.Add(text);
								}
							}
						}
						bool flag5 = !string.IsNullOrWhiteSpace(customAttribute.name);
						if (flag5)
						{
							string text2 = UxmlUtility.ValidateUxmlName(customAttribute.name);
							bool flag6 = text2 != null;
							if (flag6)
							{
								Debug.LogError(string.Format("Invalid UXML name '{0}' for attribute '{1}' in type '{2}'. {3}", new object[]
								{
									customAttribute.name,
									fieldInfo.Name,
									fieldInfo.DeclaringType.DeclaringType,
									text2
								}));
								return new ValueTuple<bool, string, string[]>(false, null, null);
							}
							return new ValueTuple<bool, string, string[]>(true, customAttribute.name, UxmlTypeDescription.<GetUxmlNames>g__GetArray|7_0(list));
						}
					}
					UxmlObjectReferenceAttribute customAttribute2 = fieldInfo.GetCustomAttribute<UxmlObjectReferenceAttribute>();
					bool flag7 = customAttribute2 != null;
					if (flag7)
					{
						bool flag8 = !string.IsNullOrWhiteSpace(customAttribute2.name);
						if (flag8)
						{
							string text3 = UxmlUtility.ValidateUxmlName(customAttribute2.name);
							bool flag9 = text3 != null;
							if (flag9)
							{
								Debug.LogError(string.Format("Invalid UXML Object name '{0}' for attribute '{1}' in type '{2}'. {3}", new object[]
								{
									customAttribute2.name,
									fieldInfo.Name,
									fieldInfo.DeclaringType.DeclaringType,
									text3
								}));
								return new ValueTuple<bool, string, string[]>(false, null, null);
							}
							return new ValueTuple<bool, string, string[]>(true, customAttribute2.name, UxmlTypeDescription.<GetUxmlNames>g__GetArray|7_0(list));
						}
					}
					StringBuilder stringBuilder = GenericPool<StringBuilder>.Get();
					string name = fieldInfo.Name;
					for (int j = 0; j < name.Length; j++)
					{
						char c = name[j];
						bool flag10 = char.IsUpper(c);
						if (flag10)
						{
							c = char.ToLower(c);
							bool flag11 = j > 0;
							if (flag11)
							{
								stringBuilder.Append("-");
							}
						}
						stringBuilder.Append(c);
					}
					string text4 = stringBuilder.ToString();
					GenericPool<StringBuilder>.Release(stringBuilder.Clear());
					valueTuple = new ValueTuple<bool, string, string[]>(true, text4, UxmlTypeDescription.<GetUxmlNames>g__GetArray|7_0(list));
				}
			}
			return valueTuple;
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00097AD0 File Offset: 0x00095CD0
		[CompilerGenerated]
		internal static string[] <GetUxmlNames>g__GetArray|7_0(List<string> list)
		{
			bool flag = list.Count == 0;
			string[] array;
			if (flag)
			{
				array = Array.Empty<string>();
			}
			else
			{
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x040012D7 RID: 4823
		private static readonly Type s_UxmlSerializedDataType = typeof(UxmlSerializedData);

		// Token: 0x040012D8 RID: 4824
		public readonly Type type;

		// Token: 0x040012D9 RID: 4825
		public readonly List<UxmlDescription> attributeDescriptions;

		// Token: 0x040012DA RID: 4826
		public readonly Dictionary<string, int> uxmlNameToIndex;

		// Token: 0x040012DB RID: 4827
		public readonly Dictionary<string, int> cSharpNameToIndex;
	}
}
