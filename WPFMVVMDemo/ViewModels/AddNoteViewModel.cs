using System;
using Tracker_Software.ViewModels.Common;

namespace Tracker_Software.ViewModels
{
    public class AddNoteViewModel
    {
        public AddNoteViewModel()
        {
            SaveCommand = new SimpleRelayCommand(x => Save(this, new EventArgs()));
            CancelCommand = new SimpleRelayCommand(x => Cancel(this, new EventArgs()));
        }

        public string Note { get; set; }

        public SimpleRelayCommand SaveCommand { get; set; }

        public SimpleRelayCommand CancelCommand { get; set; }
        public event EventHandler Save;

        public event EventHandler Cancel;

        public void Reset()
        {
            Note = "";
        }
    }
}