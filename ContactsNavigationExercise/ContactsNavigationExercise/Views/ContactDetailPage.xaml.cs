using ContactsNavigationExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactsNavigationExercise.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailPage : ContentPage
	{
        //Controladores de eventos para conocer en ContactsPage si se ha actualizado o añadido un contacto nuevo.
        public event EventHandler<Contact> ContactAdded;
        public event EventHandler<Contact> ContactUpdated;

        
        public ContactDetailPage()
        {
            InitializeComponent();
        }

        public ContactDetailPage (Contact contact )
		{
            if (contact == null) throw new ArgumentNullException(nameof(contact));

            InitializeComponent ();

            /**
             * Utilizamos BindingContext en vez del contacto directamente en la asignación, de esta manera si el usuario modifica algun valor y no pulsa despues "Save"
             * los cambios se revertiran de manera que no se actualizara la fila del ListView.
             * 
             * Utilizamos esta variable de manera temporal, en caso de ser necesario la actualización de datos posteriormente utilizaremos el evento para notificar a la vista.             
             * */
             
            BindingContext = new Contact
            {
                Name = contact.Name,
                SurName = contact.SurName,
                Id = contact.Id,
                Email = contact.Email,
                Phone = contact.Phone,
                IsBlocked = contact.IsBlocked                
            };

		}

        
    }
}