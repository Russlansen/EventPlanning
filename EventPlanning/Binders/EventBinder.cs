using EventPlanning.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EventPlanning.Binders
{
    public class EventBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueProvider = bindingContext.ValueProvider;
            var ownerName = (string)valueProvider.GetValue("OwnerName").ConvertTo(typeof(string));
            var name = (string)valueProvider.GetValue("Name").ConvertTo(typeof(string));
            var dateResult = valueProvider.GetValue("Date").RawValue as string[];
            var limitResult = valueProvider.GetValue("Limit");
            int? limit = null;
            try
            {
                limit = (int?)limitResult.ConvertTo(typeof(int?));
            }
            catch (NullReferenceException) { }
            DateTime? date = null;
            if (!String.IsNullOrEmpty(dateResult[0]))
            {
                date = (DateTime)valueProvider.GetValue("Date").ConvertTo(typeof(DateTime));
            }
            var theme = new Dictionary<string, Dictionary<string, string>>();
            var count = 0;
            while (true)
            {
                var keyResult = valueProvider.GetValue("Theme[" + count + "].Key");
                var valueResult = valueProvider.GetValue("Theme[" + count + "].Value");
                var subThemeKeyResult = valueProvider.GetValue("Theme[" + count + "].SubTheme.Key");
                var subThemeValueResult = valueProvider.GetValue("Theme[" + count + "].SubTheme.Value");
                if (keyResult != null)
                {
                    var key = (string)keyResult.ConvertTo(typeof(string));
                    var value = (string)valueResult.ConvertTo(typeof(string));
                    if (key == "" && value == "") {
                        count++;
                        continue;
                    }
                    var mainTheme = new Dictionary<string, string>();
                    mainTheme.Add(key, value);
                    theme.Add(count.ToString(), mainTheme);
                    if (subThemeKeyResult != null)
                    {
                        var subThemeKey = (string)subThemeKeyResult.ConvertTo(typeof(string));
                        var subThemeValue = (string)subThemeValueResult.ConvertTo(typeof(string));
                        var subTheme = new Dictionary<string, string>();
                        subTheme.Add(subThemeKey, subThemeValue);
                        theme.Add("sub_" + count.ToString(), subTheme);
                    }
                    count++;
                }
                else
                {
                    break;
                }
            }
            
            var bind = new AddEventViewModel() { OwnerName = ownerName, Name = name, Date = date, Theme = theme, Limit = limit};

            return bind;
        }
    }
}