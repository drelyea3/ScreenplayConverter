using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenplayConverter
{
    public class ScriptModel : INotifyPropertyChanged
    {
        private bool hasChanges;
        private ObservableCollection<ScriptItem> scriptItems = new ObservableCollection<ScriptItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        public bool HasChanges
        {
            get { return hasChanges; }
            private set
            {
                if (hasChanges != value)
                {
                    hasChanges = value;
                    RaiseChange("HasChanges");
                }
            }
        }

        public ObservableCollection<ScriptItem> Items
        {
            get { return scriptItems; }
        }

        public void ImportText(IEnumerable<string> scriptText)
        {
            Items.Clear();

            foreach (string text in scriptText)
            {
                var item = ScriptItemFactory.Create(text);
                if (item != null)
                {
                    Items.Add(item);
                }
            }
        }

        private void Reset()
        {
            HasChanges = false;
        }

        private void RaiseChange(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        internal void Delete(ScriptItem item)
        {
            scriptItems.Remove(item);
        }
    }

    public enum ScriptItemType
    {
        Unknown,
        Act,
        Scene,
        Character,
        Dialogue,
        Parenthetical,
        Action
    }

    public class ScriptItem
    {
        public ScriptItemType ItemType { get; set; }
        public string OriginalText { get; set; }
        public string Text { get; set; }

        public static ScriptItem Create(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            text = text.Trim();

            //if (IsAct())
            {

            }
            return null;
        }
    }
}
