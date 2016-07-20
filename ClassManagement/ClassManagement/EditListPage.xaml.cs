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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<EditListViewModel>(this, "Done",
                (o) => ClosePage());
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<EditListViewModel>(this, "Done");
        }

        private void ClosePage()
        {
            Navigation.PopModalAsync(false);
        }
    }
}
