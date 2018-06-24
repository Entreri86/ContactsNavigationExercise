using ContactsNavigationExercise.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsNavigationExercise.Models
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsPage : ContentPage
	{

        private ObservableCollection<Contact> ContactList;

		public ContactsPage ()
		{
			InitializeComponent ();
            
            InitializeContactList();
            ContactsListView.ItemsSource = ContactList;//Asignamos la lista 

        }

        private void InitializeContactList()
        {
            ContactList = new ObservableCollection<Contact>
            {
                new Contact{ Name="Fernando", SurName="Otal Barja", Email="otal.fernando@gmail.com", Id=1, IsBlocked=false, Phone="634-58-69-36" },
                new Contact{ Name="Atenerix", SurName="Porculerix Vicietix", Email="atenea.mayo@outlook.com", Id=2, IsBlocked=false, Phone="666-88-99-99" }                
            };
        }

        private async void OnAddContact(object sender, EventArgs e)
        {
            //Creamos variable de la Pagina con un contacto nuevo y vacio para rellenar
            var ContactDetailPage = new ContactDetailPage(new Contact());
            
            //Nos subscribimos al evento ContactAdded para en el caso de que el usuario pulse el boton Added la vista se vea notificada y lo añada a la lista 
            ContactDetailPage.ContactAdded += (source, contact) => ContactList.Add(contact);

            await Navigation.PushAsync(new ContactDetailPage());//Lanzamos la pagina gestionada mediante el controlador de eventos.
        }

        private async Task OnSelectedContactAsync(object sender, SelectedItemChangedEventArgs e)
        {
            //Si el contacto seleccionado es null (ha seleccionado una fila vacia de la lista) salimos.
            if (ContactsListView.SelectedItem == null) return;

            var SelectedContact = e.SelectedItem as Contact;//Recogemos Item seleccionado.

            ContactsListView.SelectedItem = null;//Dejamos el objeto de la vista a null, para cuando volvamos a la Page poder seleccionarlo de nuevo.

            //Creamos variable de la Pagina con el contacto dado por parametro
            var ContactDetailPage = new ContactDetailPage(SelectedContact);

            //Nos subscribimos al evento ContactAdded para en el caso de que el usuario seleccione un contacto en la lista, este se pueda actualizar al disparar el evento.
            ContactDetailPage.ContactUpdated += (source, contact) =>
            {
                if (ContactList.Contains(contact))
                {
                    SelectedContact.Id = contact.Id;
                    SelectedContact.Name = contact.Name;
                    SelectedContact.SurName = contact.SurName;
                    SelectedContact.Phone = contact.Phone;
                    SelectedContact.Email = contact.Email;
                    SelectedContact.IsBlocked = contact.IsBlocked;
                }
            };

            await Navigation.PushAsync(ContactDetailPage);//Lanzamos la pagina gestionada mediante el controlador de eventos.
        }
        /**
         * Metodo para eliminar contactos de la lista y Vista.         
         * */
        private async Task OnDeleteContactAsync(object sender, EventArgs e)
        {

            //Manera larga para aprender la logica
            /**
            var MenuItem = sender as MenuItem;
            var Contact = MenuItem.CommandParameter as Contact;
            */
            //Manera corta, para dejarlo limpio el codigo
            var ContactToDelete = (sender as MenuItem)?.CommandParameter as Contact;
            
            //Mostramos alerta para confirmar que desea eliminarlo 
            if (await DisplayAlert("Warning", $"Are you sure to delete this {ContactToDelete.FullName}?","Yes","No"))
            {
                ContactList.Remove(ContactToDelete);
            }
        }
    }
}