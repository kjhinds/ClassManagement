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

            SubscribeToMessages();

            BindingContext = new EditListViewModel(listType);
            
        }

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<EditListViewModel>(this, "Done",
                (o) => ClosePage());
        }

        private void ClosePage()
        {
            MessagingCenter.Unsubscribe<EditListViewModel>(this, "Done");
            Navigation.PopModalAsync(false);
        }
    }
}
