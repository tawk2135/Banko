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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using BibliothequeBanque;
using Banque;

namespace Banque
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        Personnes personnes;
        public static string id = "";
        public MainWindow()
        {
            personnes = new Personnes();
            InitializeComponent();
        }
        int attempts = 1;
        private void TxtUtilisateur_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(TxtUtilisateur.Text == null && pswdBox.Password == null))
            {
                BtnOk.IsEnabled = true;
                id = TxtUtilisateur.Text;
            }
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string utilisateur = TxtUtilisateur.Text;
            string motPasse = pswdBox.Password;
            bool find = false;
            bool test = false;

            //Recuperation des valeurs saisies par l'utilisateur en lettre minuscule.
            //On verifie si les valeurs saisies sont vides ou des valeurs nulles.
            //S'il y a des valeus dans les txtbox

            for (int i = 0; i < personnes.Collectionpersonnes.Count; i++)
            {
                if (utilisateur == personnes.Collectionpersonnes[i].ID && motPasse == personnes.Collectionpersonnes[i].Password)
                {
                    // On affiche un message de bienvenue a l'utilisateur.
                    MessageBox.Show($"Bienvenue: {personnes.Collectionpersonnes[i].Nom}.", "Bienvenue",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    //On met fin a l'application 
                    this.Visibility = Visibility.Hidden;//Masquer le formulaire du login
                    Banques ZoneClient = new Banques();//Consructeur de la fenetre WPF
                    ZoneClient.ShowDialog();//Montrer la fenetre...
                    this.Visibility = Visibility.Visible;//Reafficher le formulaire du login apres la fermeture du formulaire du SGI
                    test = true;
                    TxtUtilisateur.Text = String.Empty;
                    pswdBox.Password = String.Empty;
                    break;
                }
            }
            for (int i = 0; i < personnes.Collectionpersonnes.Count; i++)
            {
                if (utilisateur == personnes.Collectionpersonnes[0].Nom && motPasse == personnes.Collectionpersonnes[0].Password)
                {

                    MessageBox.Show($"Bienvenue: {utilisateur}");

                    this.Visibility = Visibility.Hidden;//Masquer le formulaire du login
                    BANQUE ZoneAdmin = new BANQUE();//Consructeur de la fenetre WPF
                    ZoneAdmin.ShowDialog();//Montrer la fenetre...
                    this.Hide();
                    find = true;
                    TxtUtilisateur.Text = String.Empty;
                    pswdBox.Password = String.Empty;
                    break;
                }
            }
            //Il faut revenir la dessus si le nip est erroner sa ne marche pas...
            if ((!test) && (!find))
            {

                MessageBox.Show($"Accès Refuser, veuillez reessayer, " + attempts + "  tentative(s) sur trois");
                TxtUtilisateur.Clear();
                pswdBox.Clear();
                attempts += 1;
                if (attempts == 4)
                {
                    MessageBox.Show("Vous avez exceder le nombre de tentatives permis; veuillez reessayer plus tard");
                    this.Close();
                }
            }
        }
        private void BtnQuitter_Click(object sender, RoutedEventArgs e)
        {
            //On s'assure que c bien l'intention de l'utilisateur de quitter l'application
            MessageBoxResult reponse = MessageBox.Show("Desirez-vous reellement quitter cette application?", "Attention", MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            //Si tel est le cas , on met fin a l'application
            if (reponse == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }

        }
    }
}
