using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ClassManagement
{
    public partial class EditListPage : ContentPage
    {
       

        public EditListPage(string listType)
        {
            InitializeComponent();
            

            BindingContext = new EditListViewModel(listType);
            
        }

        public void AddItem(object sender, EventArgs e)
        {
            ((EditListViewModel)BindingContext).AddItem(sender, e);
            ((Entry)sender).Text = "";
        }
    }
}
