using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ScreenplayConverter
{
    public class ScriptItemTypeValueConverter : IValueConverter
    {
        private static ScriptItemType[] itemTypes = { ScriptItemType.Act, ScriptItemType.Action, ScriptItemType.Character, ScriptItemType.Dialogue, ScriptItemType.Parenthetical, ScriptItemType.Scene};
        private static string[] itemText = { "ACT", "ACTION", "CHAR", "DLG", "PAREN", "SCENE" };

        private DataTemplate actTemplate = Application.Current.Resources["ActTemplate"] as DataTemplate;
        private DataTemplate sceneTemplate = Application.Current.Resources["SceneTemplate"] as DataTemplate;
        private DataTemplate characterTemplate = Application.Current.Resources["CharacterTemplate"] as DataTemplate;
        private DataTemplate dialogueTemplate = Application.Current.Resources["DialogueTemplate"] as DataTemplate;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemType = (ScriptItemType)value;

            if (targetType == typeof(DataTemplate))
            {
                switch (itemType)
                {
                    case ScriptItemType.Act:
                        return actTemplate;
                    case ScriptItemType.Scene:
                        return sceneTemplate;
                    case ScriptItemType.Character:
                        return characterTemplate;
                    case ScriptItemType.Dialogue:
                        return dialogueTemplate;
                }

                return null;
            }
            else if (targetType == typeof(object))
            {
                int index = 0;
                while (itemTypes[index] != itemType)
                {
                    ++index;
                }
                if (index < itemTypes.Length)
                {
                    return itemText[index];
                }
                return null;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var cc = value as ContentControl;
            if (cc != null)
            {
                var text = (string)cc.Content;
                int index = 0;
                while (itemText[index] != text)
                {
                    ++index;
                }
                if (index < itemTypes.Length)
                {
                    return itemTypes[index];
                }
            }
            return ScriptItemType.Unknown;
        }
    }
}
