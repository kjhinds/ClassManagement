using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ClassManagement
{
    public partial class EditListPage : ContentPage
    {
        public EditListPage(String whichList)
        {
            InitializeComponent();
            if (whichList == "Edit Behavior List")
            {
                var listToEdit = new Tuple<SortableObservableCollection<string>>(Settings.BehaviorList);
                BindingContext = listToEdit;
            }
       
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            //var period = ((MenuItem)sender).BindingContext as Period;
            //SettingsData.BehaviorList.Remove(period);
        }
    }
}
