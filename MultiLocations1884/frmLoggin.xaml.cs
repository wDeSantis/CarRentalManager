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
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace MultiLocations1884
{
    /// <summary>
    /// Logique d'interaction pour frmLoggin.xaml
    /// </summary>
    public partial class frmLoggin : Window
    {
        SqlConnection connexion;
        SqlCommand commande;
        public frmLoggin()
        {
            connexion = new SqlConnection("server=.; initial catalog=MultiLocations1884; integrated security=true");
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // création de la requête d'authentification
                string authentification = "SELECT * FROM Utilisateurs WHERE NomUtilisateur = @NomUtilisateur AND MotPasse = @MotPasse";
                // création de l'objet SqlCommand
                commande = new SqlCommand(authentification, connexion);
                commande.Parameters.AddWithValue("@NomUtilisateur", txtNomUti.Text);
                commande.Parameters.AddWithValue("@MotPasse", txtMotPasse.Password);
                // ouverture de connexion
                connexion.Open();
                // création du lecteur 
                SqlDataReader lecteur = commande.ExecuteReader();
                if (lecteur.Read())
                {
                    UtilisateurActif utilisateur = new UtilisateurActif();
                    utilisateur.IdUtilisateur = lecteur["IdUtilisateur"].ToString();
                    utilisateur.Prenom = lecteur["Prenom"].ToString();
                    utilisateur.Nom = lecteur["Nom"].ToString();
                    MessageBox.Show($"Bienvenue " + utilisateur.Prenom + " " + utilisateur.Nom, "Authentification", MessageBoxButton.OK, MessageBoxImage.Information);
                    // on ouvre la nouvelle fenêtre
                    frmLocations frmloc = new frmLocations();
                    frmloc.Show();
                    // on ferme le formulaire d'authentification
                    this.Close();

                }
                else
                {
                    // si le lecteur ne trouve pas un matching nom et mdp
                    MessageBox.Show("Les informations saisies ne permet pas de vous identifier.");
                    txtMotPasse.Password = string.Empty;
                    txtNomUti.Text = string.Empty;
                    txtNomUti.Focus();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                // fermeture de connexion
                connexion.Close(); 
            }
        }

        // méthode pour le bouton annuler
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Désirez-vous annuler le processus d'authentification?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Fermeture de l'application.");
                this.Close();
            }
        }


    }
}
