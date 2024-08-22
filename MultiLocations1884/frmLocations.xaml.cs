using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics.Eventing.Reader;
using System.Transactions;


namespace MultiLocations1884
{
    /// <summary>
    /// Logique d'interaction pour frmLocations.xaml
    /// </summary>
    public partial class frmLocations : Window
    {
        SqlConnection connexion;
        SqlDataAdapter da;
        DataSet dsMultiLocations1884 = new DataSet();
        DataRow enregistrement;
        SqlCommand commande;
        bool nouveau = false;


        public frmLocations()
        {
            connexion = new SqlConnection("server=.; initial catalog=MultiLocations1884; integrated security=true");
            InitializeComponent();
        }


        private void frmLocations1_Loaded(object sender, RoutedEventArgs e)
        {
            // création de la table Locations
            da = new SqlDataAdapter("sp_SelectLocations", connexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsMultiLocations1884, SchemaType.Mapped, "Locations");
            da.Fill(dsMultiLocations1884, "Locations");

            // création de la table contenant Véhicules non loués
            da = new SqlDataAdapter("sp_SelectNIVLibre", connexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsMultiLocations1884, SchemaType.Mapped, "NIVLibre");
            da.Fill(dsMultiLocations1884, "NIVLibre");

            // création de la table Termes_Location
            da = new SqlDataAdapter("sp_SelectTermes", connexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.FillSchema(dsMultiLocations1884, SchemaType.Mapped, "NoTerme");
            da.Fill(dsMultiLocations1884, "NoTerme");

            // association aux deux comboBox 
            lstNoTerme.ItemsSource = dsMultiLocations1884.Tables["NoTerme"].DefaultView;

        }


        //méthode qui charge les NIV en non location dans le ComboBox lstNIV
        private void ChargerNIVNonUtiliser()
        {
            try
            {
                // on remet on le comboBox NIV
                lstNIV.IsEnabled = true;
                lstNIV.Visibility = Visibility.Visible;
                // on vide le ComboBox
                lstNIV.ItemsSource = null;
                // vide la table NIVLibre
                dsMultiLocations1884.Tables["NIVLibre"].Clear();
                // création de l'objet SqlCommand et du type de requête
                commande = new SqlCommand("sp_SelectNIVLibre", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                // création de l'objet SqlDataAdapter
                SqlDataAdapter daNIVLibre = new SqlDataAdapter(commande);
                // ouverture de connexion
                connexion.Open();
                daNIVLibre.Fill(dsMultiLocations1884, "NIVLibre");
                // ajout des NIV au comboBox
                lstNIV.ItemsSource = dsMultiLocations1884.Tables["NIVLibre"].DefaultView;


            }
            catch (Exception ex)
            {
                // affichage de message en cas d'erreur
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // fermeture de la connexion
                connexion.Close();
            }
        }

        private void ChangerControleNIV()
        {
            if (nouveau)
            {
                // on remet le txt
                txtNIV.IsReadOnly = false;
                txtNIV.Visibility = Visibility.Hidden;
                // on enleve la lst
                lstNIV.IsEnabled = true;
                lstNIV.Visibility = Visibility.Visible;
            }
            else
            {
                // on remet le txt
                txtNIV.IsReadOnly = true;
                txtNIV.Visibility = Visibility.Visible;
                // on enleve la lst
                lstNIV.IsEnabled = false;
                lstNIV.Visibility = Visibility.Hidden;
            }

        }

        // méthode qui prépare l'insertion d'un nouveauEnregistrement
        private void PréparerNouveauEnregistrement()
        {
            // on remet vide les textbox
            txtNoLocation.Text = txtIdClient.Text = txtNIV.Text =
            txtDateDebutLoc.Text = txtDatePremPaiement.Text =
            txtMontantMensuel.Text = txtNbPaiementsMensuel.Text =
            txtOdomètre.Text = string.Empty;
            // on active les controles
            txtIdClient.IsReadOnly = txtMontantMensuel.IsReadOnly = txtNbPaiementsMensuel.IsReadOnly = txtOdomètre.IsReadOnly = false;
            txtDateDebutLoc.IsEnabled = txtDatePremPaiement.IsEnabled = true;
            // on met off l'insertion de clé primaire
            txtNoLocation.IsEnabled = false;
            lstNoTerme.IsEnabled = true;
            // on remet vide la lst NoTerme
            lstNoTerme.SelectedIndex = -1;
            lstNIV.SelectedIndex = -1;
        }


        // méthode qui vide les controles
        private void ViderControles()
        {
            // on remet vide les textbox
            txtIdClient.Text = txtNIV.Text = 
            txtDateDebutLoc.Text = txtDatePremPaiement.Text =
            txtMontantMensuel.Text = txtNbPaiementsMensuel.Text =
            txtOdomètre.Text = string.Empty;
            // on remet vide les lst
            lstNoTerme.SelectedIndex = -1;
            lstNIV.SelectedIndex = -1;
        }

        private void ResetControlesOff()
        {
            ViderControles();
            txtNoLocation.IsEnabled = true;
            // on remet les controles n'ont changeable 
            txtIdClient.IsReadOnly = txtNIV.IsReadOnly = txtMontantMensuel.IsReadOnly =
            txtNbPaiementsMensuel.IsReadOnly = txtOdomètre.IsReadOnly = true;
            lstNoTerme.IsEnabled = false;
            // meme chose pour datepickers
            txtDateDebutLoc.IsEnabled = txtDatePremPaiement.IsEnabled = false;
            // focus sur NoLocation
            txtNoLocation.Focus();
        }


        private void ResetControlesOn()
        {
            ViderControles();
            // on remet on/off les controles pertinants
            txtNoLocation.IsEnabled = true;
            txtMontantMensuel.IsReadOnly = txtNbPaiementsMensuel.IsReadOnly =
            txtOdomètre.IsReadOnly = false;
            txtIdClient.IsReadOnly = true;
            txtNIV.IsEnabled = true;
            txtNIV.IsReadOnly = true;
            lstNoTerme.IsEnabled = true;
            // meme chose pour datepickers
            txtDateDebutLoc.IsEnabled = txtDatePremPaiement.IsEnabled = true;
        }


        // méthode pour le bouton recherche
        private void btnRechercher_Click(object sender, RoutedEventArgs e)
        {
            ResetControlesOn();
            int numeroLocation;
            // si la valeur du numéro de location n'est pas vide
            if (!string.IsNullOrEmpty(txtNoLocation.Text) && int.TryParse(txtNoLocation.Text, out numeroLocation))
            {
                enregistrement = dsMultiLocations1884.Tables["Locations"].Rows.Find(txtNoLocation.Text.ToString());
                // si la valeur est présente dans la table
                if (enregistrement != null)
                {
                    // on met le NIV IS READ ONLY A TRUE
                    lstNIV.IsReadOnly = true;

                    // affichage des informations
                    txtIdClient.Text = enregistrement["IDClient"].ToString();
                    txtNIV.Text = enregistrement["NIV"].ToString();
                    lstNoTerme.SelectedValue = enregistrement["NoTerme"];
                    txtDateDebutLoc.Text = enregistrement["DateDébutLocation"].ToString();
                    txtDatePremPaiement.Text = enregistrement["DatePremierPaiement"].ToString();
                    // formater le montant avec seulement 2 chiffres après la virgule
                    decimal montantMensuel = Convert.ToDecimal(enregistrement["MontantMensuel"]);
                    txtMontantMensuel.Text = montantMensuel.ToString("F2");
                    txtNbPaiementsMensuel.Text = enregistrement["NbPaiementsMensuels"].ToString();
                    txtOdomètre.Text = enregistrement["OdomètreRetour"].ToString();
                }
                else
                {
                    // l'enregistrement n'a pas été trouvé dans la table
                    MessageBox.Show("Aucun enregistrement n'existe avec ce numéro de location. Veuillez entrer un numéro de location valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    // on efface le contenu et focus
                    txtNoLocation.Text = string.Empty;
                    txtNoLocation.Focus();
                }
            }
            else
            {
                // l'utilisateur n'a pas entrer de valeurs de numéro de location
                MessageBox.Show("Veuillez-entrer une valeur numérique avant d'appuyer sur recherche.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNoLocation.Focus();
            }

        }


        // méthode appelée lorsqu'on clique sur enregistrer
        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            // si ce n'est pas un nouvel enregistrement
            if (!nouveau)
            {
                    try
                {      
                    // affichage d'un message de confirmation
                    if (MessageBox.Show("Êtes-vous certain de vouloir enregistrer ces informations?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                            // enregistrement dans le dataSet
                            enregistrement["NoLocation"] = txtNoLocation.Text;
                            enregistrement["IDClient"] = txtIdClient.Text;
                            enregistrement["NIV"] = txtNIV.Text;
                            enregistrement["NoTerme"] = lstNoTerme.SelectedValue;
                            enregistrement["DateDébutLocation"] = txtDateDebutLoc.Text;
                            enregistrement["DatePremierPaiement"] = txtDatePremPaiement.Text;
                            enregistrement["MontantMensuel"] = txtMontantMensuel.Text;
                            enregistrement["NbPaiementsMensuels"] = txtNbPaiementsMensuel.Text;
                            enregistrement["OdomètreRetour"] = txtOdomètre.Text;
                            // enregistrement dans la bdd
                            EnregistrerLocation();
                            // affichage d'un message d'enregistrement réussie
                            MessageBox.Show("Enregistrement des informations réussies");
                            ResetControlesOff();
                    }                    

                }
                catch (Exception ex)
                {
                    // affichage d'un message en cas d'erreur
                    MessageBox.Show("Vous pouvez seulement entrer des valeurs numériques positives.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // si c'est une nouvelle location
            else
            {
                // affichage d'un message de confirmation
                if (MessageBox.Show("Êtes-vous certain de vouloir ajouter cet enregistrement?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        enregistrement = dsMultiLocations1884.Tables["Locations"].NewRow();
                        // enregistrement dans le dataSet
                        enregistrement["IDClient"] = txtIdClient.Text;
                        enregistrement["NIV"] = lstNIV.SelectedValue;
                        enregistrement["NoTerme"] = lstNoTerme.SelectedValue;
                        enregistrement["DateDébutLocation"] = txtDateDebutLoc.Text;
                        enregistrement["DatePremierPaiement"] = txtDatePremPaiement.Text;
                        enregistrement["MontantMensuel"] = txtMontantMensuel.Text;
                        enregistrement["NbPaiementsMensuels"] = txtNbPaiementsMensuel.Text;
                        enregistrement["OdomètreRetour"] = txtOdomètre.Text;
                        dsMultiLocations1884.Tables["Locations"].Rows.Add(enregistrement);
                        // enregistrement dans la bdd
                        EnregistrerNouvelleLocation();
                        // affichage d'un message
                        MessageBox.Show("Ajout de l'enregistrement réussi.");
                        // on remet les variables a false
                        // on remet le bouton rechercher a true
                        btnRechercher.IsEnabled = true;
                        nouveau = false;
                        ChangerControleNIV();
                        ResetControlesOff();
                    }
                    catch (Exception ex)
                    {
                        // affichage de message en cas d'erreur
                        MessageBox.Show("Vous pouvez seulement ajouter des valeurs numériques positives et le ID du client doit exister dans la base de données.", "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        // méthode qui enregistre dans la bdd la nouvelle location
        private void EnregistrerNouvelleLocation()
        {
            try
            {
                // création de l'objet SqlDataAdapter
                da = new SqlDataAdapter();
                // création de l'objet SqlCommand
                commande = new SqlCommand("sp_InsertLocations", connexion);
                // ajout des paramètres
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.AddWithValue("@IDClient", txtIdClient.Text);
                commande.Parameters.AddWithValue("@NIV", lstNIV.SelectedValue);
                commande.Parameters.AddWithValue("@NoTerme", lstNoTerme.SelectedValue);
                commande.Parameters.AddWithValue("@DateDébutLoc", txtDateDebutLoc.Text);
                commande.Parameters.AddWithValue("@DatePremPai", txtDatePremPaiement.Text);
                commande.Parameters.AddWithValue("@MontantMensuel", txtMontantMensuel.Text);
                commande.Parameters.AddWithValue("@NbPaiMensuels", txtNbPaiementsMensuel.Text);
                commande.Parameters.AddWithValue("@OdomRetour", txtOdomètre.Text);
                // association de l'objet à InsertCommand
                da.InsertCommand = commande;
                // ouverture de la connexion
                connexion.Open();
                // exécution de la requête
                da.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // gestion des messages d'erreur
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // fermeture de la connexion
                connexion.Close();
            }
        }


        // méthode qui enregistre dans la bdd les modifications apportées à la location existante
        private void EnregistrerLocation()
        {
            try
            {
                // récupération de l'enregistrement
                enregistrement = dsMultiLocations1884.Tables["Locations"].Rows.Find(txtNoLocation.Text.ToString());
                // crétion de l'objet SqlDataAdapter
                da = new SqlDataAdapter();
                // création de la requete et ajout
                commande = new SqlCommand("sp_UpdateLocations", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                // ajout des paramètres
                commande.Parameters.AddWithValue("@NoLocation", txtNoLocation.Text);
                commande.Parameters.AddWithValue("@NoTerme", lstNoTerme.SelectedValue);
                commande.Parameters.AddWithValue("@DateDebutLoc", txtDateDebutLoc.Text);
                commande.Parameters.AddWithValue("@DatePremPai", txtDatePremPaiement.Text);
                commande.Parameters.AddWithValue("@MontantMensuel", txtMontantMensuel.Text);
                commande.Parameters.AddWithValue("@NbPaiementsMens", txtNbPaiementsMensuel.Text);
                commande.Parameters.AddWithValue("@Odomètre", txtOdomètre.Text);
                // association à update command
                da.UpdateCommand = commande;
                // ouverture de la connexion
                connexion.Open();
                // exécution de la commande
                da.UpdateCommand.ExecuteNonQuery();
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connexion.Close();
            }
        }


        // méthode appelée lorsqu'on clique sur Nouveau
        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            // affichage d'un message de confirmation
            if (MessageBox.Show("Désirez-vous enregistrer un nouvel enregistrement?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                // on disable le btn rechercher
                btnRechercher.IsEnabled = false;
                // nouvel enregistrement
                nouveau = true;
                PréparerNouveauEnregistrement();
                // on cache le textbox NIV
                txtNIV.IsEnabled = false;
                txtNIV.Visibility = Visibility.Hidden;
                ChargerNIVNonUtiliser();
            }
            else
            {
                nouveau = false;
            }
        }

        // méthode appelée lorsqu'on clique sur annuler 
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            if (nouveau)
            {
                // affichage d'un message de confirmation
                if (MessageBox.Show("Désirez-vous annuler l'ajout d'un nouveau enregistrement?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // on remet le bouton rechercher
                    btnRechercher.IsEnabled = true;
                    ResetControlesOff();
                    nouveau = false;
                    ChangerControleNIV();
                }
            }
            else
            {
                // affichage d'un message de confirmation
                if (MessageBox.Show("Désirez-vous annuler les modifications apportées?", "Avertissement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ResetControlesOff();
                }
            }
        }
    }
    }

