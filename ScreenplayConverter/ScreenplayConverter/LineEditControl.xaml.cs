using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScreenplayConverter
{
    /// <summary>
    /// Interaction logic for LineEditControl.xaml
    /// </summary>
    public partial class LineEditControl : UserControl
    {
        public ScriptItemType ItemType
        {
            get { return (ScriptItemType)GetValue(ItemTypeProperty); }
            set { SetValue(ItemTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTypeProperty =
            DependencyProperty.Register("ItemType", typeof(ScriptItemType), typeof(LineEditControl), new PropertyMetadata(ScriptItemType.Unknown));

        public LineEditControl()
        {
            InitializeComponent();
        }
    }
}
