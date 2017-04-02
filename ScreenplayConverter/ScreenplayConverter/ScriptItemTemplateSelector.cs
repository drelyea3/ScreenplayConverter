using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ScreenplayConverter
{
    public class ScriptItemTemplateSelector : DataTemplateSelector
    {
        Dictionary<string, DataTemplate> cache = new Dictionary<string, DataTemplate>();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            element.HorizontalAlignment = HorizontalAlignment.Stretch;

            var scriptItem = item as ScriptItem;
            if (scriptItem != null)
            {
                return Lookup(scriptItem.ItemType.ToString());
            }

            return null;
        }

        private DataTemplate Lookup(string itemType)
        {
            string key = itemType;
            DataTemplate template = null;
            if (!cache.TryGetValue(key, out template))
            {
                template = Application.Current.Resources[key + "Template"] as DataTemplate;
                if (template != null)
                {
                    cache.Add(key, template);
                }
            }
            return template;
        }
    }
}
