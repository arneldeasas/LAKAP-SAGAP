using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Core.Helpers
{
	public class EnumHelper
	{
		public static string GetEnumDescription(KitType value)
		{
			if(value != null)
			{
				var field = value.GetType().GetField(value.ToString());
				var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
				return attribute == null ? value.ToString() : attribute.Description;
			}
			return string.Empty;
		}
	}
}
