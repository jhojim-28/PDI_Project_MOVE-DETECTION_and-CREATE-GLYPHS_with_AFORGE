using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;

namespace ProyectoCDM.MVM.View
{
    /// <summary>
    /// Lógica de interacción para PetRegisterView.xaml
    /// </summary>
    public partial class PetRegisterView : UserControl
    {
        public PetRegisterView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string nombremascota = txbnombremascota.Text;
            using (SqlConnection conection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\opali\Downloads\ProyectoCDM\ProyectoCDM\ProyectoCDM\DataBase\Bdd.mdf; Integrated Security = True; Connect Timeout = 30"))
            {
                conection.Open();

                SqlCommand cmd = new SqlCommand("insert into Mascota(NombreMascota) values('"+nombremascota+"')", conection);
                cmd.ExecuteNonQuery();

                conection.Close();
            }
            MessageBox.Show("Mascota: " + nombremascota + " Fue añadido correctamente, Vea la pestaña de View Security designar el acceso a las habitaciones Permitidas");
        }
    }
}
