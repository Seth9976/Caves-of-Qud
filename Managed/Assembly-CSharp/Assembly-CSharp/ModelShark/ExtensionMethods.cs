using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ModelShark
{
	// Token: 0x0200019A RID: 410
	public static class ExtensionMethods
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x000624CC File Offset: 0x000606CC
		public static void FillParameterizedTextFields(this Text[] textFields, ref List<ParameterizedTextField> parameterizedTextFields, string delimiter)
		{
			List<string> fieldNames = new List<string>();
			foreach (Text text in textFields)
			{
				string text2 = string.Format("{0}\\w*{0}", delimiter);
				foreach (object obj in Regex.Matches(text.text, text2, RegexOptions.IgnoreCase | RegexOptions.Multiline))
				{
					Match match = (Match)obj;
					string text3 = match.Value.Trim('%');
					if (!fieldNames.Contains(text3))
					{
						fieldNames.Add(text3);
					}
					bool flag = false;
					foreach (ParameterizedTextField parameterizedTextField in parameterizedTextFields)
					{
						if (text3 == parameterizedTextField.name)
						{
							parameterizedTextField.placeholder = match.Value;
							flag = true;
						}
					}
					if (!flag)
					{
						parameterizedTextFields.Add(new ParameterizedTextField
						{
							name = text3,
							placeholder = match.Value,
							value = string.Empty
						});
					}
				}
			}
			parameterizedTextFields.RemoveAll((ParameterizedTextField x) => !fieldNames.Contains(x.name));
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x00062638 File Offset: 0x00060838
		public static void FillParameterizedTextFields(this TextMeshProUGUI[] textFields, ref List<ParameterizedTextField> parameterizedTextFields, string delimiter)
		{
			List<string> fieldNames = new List<string>();
			foreach (TextMeshProUGUI tmp_Text in textFields)
			{
				string text = string.Format("{0}\\w*{0}", delimiter);
				foreach (object obj in Regex.Matches(tmp_Text.text, text, RegexOptions.IgnoreCase | RegexOptions.Multiline))
				{
					Match match = (Match)obj;
					string text2 = match.Value.Trim('%');
					if (!fieldNames.Contains(text2))
					{
						fieldNames.Add(text2);
					}
					bool flag = false;
					foreach (ParameterizedTextField parameterizedTextField in parameterizedTextFields)
					{
						if (text2 == parameterizedTextField.name)
						{
							parameterizedTextField.placeholder = match.Value;
							flag = true;
						}
					}
					if (!flag)
					{
						parameterizedTextFields.Add(new ParameterizedTextField
						{
							name = text2,
							placeholder = match.Value,
							value = string.Empty
						});
					}
				}
			}
			parameterizedTextFields.RemoveAll((ParameterizedTextField x) => !fieldNames.Contains(x.name));
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x000627A4 File Offset: 0x000609A4
		public static void FillDynamicImageFields(this DynamicImage[] imageFields, ref List<DynamicImageField> dynamicImageFields, string delimiter)
		{
			List<string> fieldNames = new List<string>();
			foreach (DynamicImage dynamicImage in imageFields)
			{
				string text = dynamicImage.placeholderName.Trim('%');
				if (!fieldNames.Contains(text))
				{
					fieldNames.Add(text);
				}
				Image placeholderImage = dynamicImage.PlaceholderImage;
				bool flag = false;
				foreach (DynamicImageField dynamicImageField in dynamicImageFields)
				{
					if (text == dynamicImageField.name)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					dynamicImageFields.Add(new DynamicImageField
					{
						name = text,
						placeholderSprite = placeholderImage.sprite,
						replacementSprite = null
					});
				}
			}
			dynamicImageFields.RemoveAll((DynamicImageField x) => !fieldNames.Contains(x.name));
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0006289C File Offset: 0x00060A9C
		public static void FillDynamicSectionFields(this DynamicSection[] sectionFields, ref List<DynamicSectionField> dynamicSectionFields, string delimiter)
		{
			List<string> fieldNames = new List<string>();
			foreach (DynamicSection dynamicSection in sectionFields)
			{
				string text = dynamicSection.placeholderName.Trim('%');
				if (!fieldNames.Contains(text))
				{
					fieldNames.Add(text);
				}
				GameObject gameObject = dynamicSection.gameObject;
				bool flag = false;
				foreach (DynamicSectionField dynamicSectionField in dynamicSectionFields)
				{
					if (text == dynamicSectionField.name)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					dynamicSectionFields.Add(new DynamicSectionField
					{
						name = text,
						isOn = gameObject.activeSelf
					});
				}
			}
			dynamicSectionFields.RemoveAll((DynamicSectionField x) => !fieldNames.Contains(x.name));
		}
	}
}
