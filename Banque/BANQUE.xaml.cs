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
using System.Globalization;
using BibliothequeBanque;

namespace Banque
{
    
    /// <summary>
    /// Interaction logic for BANQUE.xaml
    /// </summary>
    public partial class BANQUE : Window
    {//Appel des differents classes
        Clients clients;
        Transactions transactions; 
        public BANQUE()
        {
            try
            {
                InitializeComponent();

                this.Rbajoutinterets.IsChecked = true;
                // Implementation des classes
                transactions = new Transactions();
                clients = new Clients();
                //Debut de la boucle if Si la balance du guichet est plus petit ou egal a 15000 ajout de tranche de 5000$
                if (clients.ListesClients[0].Balance <= 15000)
                {
                    //Ajout de 5000$ sur la balance du guichet
                    clients.ListesClients[0].Balance = clients.ListesClients[0].Balance + 5000;
                    //Appel de la methode de sauvegarde
                    clients.Save();
                }

                LviewListeClients.ItemsSource = clients.ListesClients;
                System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
                culture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                lviewtransactions.ItemsSource = transactions.Transactionss;
                //Rendre les elements disponibles ou non-disponibe
                GrpDepotadmin.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("erreur");
            }
        }
        private void RbVersementGuichet_Checked(object sender, RoutedEventArgs e)
        {
            try
            {//Rendre les elements disponibles ou non-disponibe
                BtnAjoutInteret.IsEnabled = false;
                LviewListeClients.IsEnabled = false;
                GrpDepotadmin.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Erreur2");
                return;
            }         
        }
        private void RbAffichageDesSoldes_Checked(object sender, RoutedEventArgs e)
        {
            try
            {//Rendre les elements disponibles ou non-disponibe
                LviewListeClients.IsEnabled = true;
                BtnAjoutInteret.IsEnabled = false;
                GrpDepotadmin.IsEnabled = false;
                LviewListeClients.ItemsSource = clients.ListesClients;
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur3");
                return;
            }
        }
        private void BtnDepotGuichet_Click(object sender, RoutedEventArgs e)
        {
        }
        private void BtnAjoutInteret_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    for (int i = 1; i < clients.ListesClients.Count; i++)
                    {
                        double interet = .01;
                        double total = (clients.ListesClients[i].Balance * interet) + (clients.ListesClients[i].Balance);
                    clients.ListesClients[i].Balance =double.Parse( string.Format("{0:0.##}", total));
                    clients.Save();
                    }
                MessageBox.Show("Transaction accepter");
                LviewListeClients.Items.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("erreur 5 ");
                return;
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
        private void BtnDepotGuichet_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                //Si l'element TXTbox n'est pas vide
                //Si la balance du guichet est plus petit ou egal a 20000$ faire la difference entre 20000 et la balance disponible dans le guichet
                if (TxtmontantDepot.Text != null)
                {
                    if (clients.ListesClients[0].Balance <= 20000)
                    {
                        double montantlegale;
                        montantlegale = 20000 - clients.ListesClients[0].Balance;
                        if (double.Parse(TxtmontantDepot.Text) <= montantlegale)
                        {//Si l'element TXTmontant depot est plus petit ou egal au 20000$
                            double Total = clients.ListesClients[0].Balance + double.Parse(TxtmontantDepot.Text);
                            //S'assurer que le chiffre soit enregistre dans un format valide
                            clients.ListesClients[0].Balance = double.Parse(string.Format("{0:0.##}", Total));

                            //Appel de la methode sauvegarde
                            clients.Save();
                            //Rafraichir la liste 
                            LviewListeClients.Items.Refresh();
                            //Envoie de messagebox pour confirmer l'acceptation de la transactions
                            MessageBox.Show("Transaction accepter");
                        }
                        //ou
                        else
                        {//Refus de la transaction
                            //Message d'erreur
                            MessageBox.Show("Le montant du guichet ne peut pas exceder 20000$");
                            MessageBox.Show($"Le montant legale permis est de {montantlegale}$ ");
                            //et montrer les possibilites disponible
                            TxtmontantDepot.Text = $"{montantlegale}";
                            return;
                        }
                    }
                    TxtmontantDepot.Text = String.Empty;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur 6");
            }
        }
        private void Rbajoutinterets_Checked(object sender, RoutedEventArgs e)
        {
            try
            {//Rendre les elements disponibles ou non-disponibe
                if (Rbajoutinterets.IsChecked.Value)
                {
                    GrpDepotadmin.IsEnabled = false;
                    LviewListeClients.IsEnabled = false;
                    BtnAjoutInteret.IsEnabled = true;
                }
            }
            catch
            {
                MessageBox.Show("Erreur4");
            }
        }
    }
}