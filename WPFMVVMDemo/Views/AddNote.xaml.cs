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
using System.Windows.Shapes;
using Tracker_Software.ViewModels;

namespace Tracker_Software.Views
{
    
    public partial class AddNote : Window
    {
        public AddNote()
        {
            InitializeComponent();
        }

        public AddNote(AddNoteViewModel context)
            : this()
        {
            this.DataContext = context;
            context.Save += Save;
            context.Cancel += Cancel;
        }

        void Save(object sender, EventArgs e)
        {
            this.Close();
        }
        void Cancel(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
