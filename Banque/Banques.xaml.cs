using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BibliothequeBanque;
using System.IO;



namespace Banque
{
    /// <summary>
    /// Interaction logic for Banques.xaml
    /// </summary>

    public partial class Banques : Window
    {
        //Declaration des proprietes
        Personnes personnes;
        Clients clients;
        Transactions transactions;
        
        
        public Banques()
        {//Debut de la methode try Catch
            try
            {// declaration des variables
                personnes = new Personnes();
                clients = new Clients();
                transactions = new Transactions();
                //Debut du programme
                InitializeComponent();
                //conversion des chiffres 
                System.Globalization.CultureInfo culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
                culture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                //debut de la boucle for
                for (int i = 0; i < clients.ListesClients.Count; i++)
                {//Si le id du login est le meme que celle de la ligne du fichier txt, et le type de compte est Cheque
                    if (MainWindow.id == clients.ListesClients[i].ID && clients.ListesClients[i].TypeDeCompte == 'C')
                    {//Extraire l'information de la balance du client en lien
                        TxtCompteCheque.Text = string.Format("{0:0.##}", clients.ListesClients[i].Balance);
                    }
                }                //debut de la boucle for

                for (int i = 0; i < clients.ListesClients.Count; i++)
                {
                    if (MainWindow.id == clients.ListesClients[i].ID && clients.ListesClients[i].TypeDeCompte == 'E')
                    {
                        TxtSoldeEpargne.Text = string.Format("{0:0.##}", clients.ListesClients[i].Balance);

                    }
                }
                //Rendre disponible les element dont on a besoin.
                GroupBoxDep.IsEnabled = true;
                GrpRetrait.IsEnabled = false;
                GrpTransfert.IsEnabled = false;
                GrpPaiementFacture.IsEnabled = false;
                //Creer les items qui rentre dans les combobox
                CmbDepotCompte.SelectedItem = "Cheques";
                CmbRetrait.SelectedItem = "Cheques";
                CmbTransfert.Text = "Cheque";
                Cmbchoixcmppaiment.Text = "Cheque";
                CmbDepotCompte.Text = "Cheques";
                CmbDepotCompte.Items.Add("Cheques");
                CmbDepotCompte.Items.Add("Epargne");
                CmbRetrait.Items.Add("Cheques");
                CmbRetrait.Items.Add("Epargne");
                CmbTransfert.Items.Add("Cheque");
                CmbTransfert.Items.Add("Epargne");
                CmbChoixFournisseur.Items.Add("Hydro-Quebec");
                CmbChoixFournisseur.Items.Add("Bell");
                CmbChoixFournisseur.Items.Add("Ashley Madison");
                CmbChoixFournisseur.Items.Add("Tinder");
                CmbChoixFournisseur.Items.Add("Disney  Channel");
                Cmbchoixcmppaiment.Items.Add("Cheque");
                Cmbchoixcmppaiment.Items.Add("Epargne");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Deposit_Checked(object sender, RoutedEventArgs e)
        {                

            try
            {
                if (rbDeposit.IsChecked.Value && GroupBoxDep != null && GrpRetrait != null)
                {
                    GroupBoxDep.IsEnabled = true;
                    TxtCompteTransfert.Clear();
                    TxtMontantDuTransfert.Clear();
                    TxtRetrait.Clear();
                    TxtDepot.Clear();
                    GrpRetrait.IsEnabled = false;
                    GrpTransfert.IsEnabled = false;
                    GrpPaiementFacture.IsEnabled = false;
                    TxtMontantPaiement.Clear();
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Erreur");
            }
        }
        private void GrpRetrait_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        }
        private void rbWithdraw_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbWithdraw.IsChecked.Value)
                {                //Rendre disponible les element dont on a besoin.

                    GrpRetrait.IsEnabled = true;
                    TxtDepot.Clear();
                    TxtMontantDuTransfert.Clear();
                    TxtCompteTransfert.Clear();
                    GroupBoxDep.IsEnabled = false;
                    GrpTransfert.IsEnabled = false;
                    GrpPaiementFacture.IsEnabled = false;
                    TxtMontantPaiement.Clear();
                }
            }
            catch
            {
                MessageBox.Show("erreur");
            }
            }
        private void rbMoneyTransfer_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rbMoneyTransfer.IsChecked.Value)
                {                //Rendre disponible les element dont on a besoin.

                    GrpRetrait.IsEnabled = false;
                    GroupBoxDep.IsEnabled = false;
                    GrpTransfert.IsEnabled = true;
                    TxtDepot.Clear();
                    TxtRetrait.Clear();
                    TxtMontantPaiement.Clear();
                    GrpPaiementFacture.IsEnabled = false;
                }
            }
            catch
            {
                MessageBox.Show("Erreur");
            }
        }
        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
        private void rbPay_Checked(object sender, RoutedEventArgs e)
        {
            try
            {//Debut de la methode try catch
                if (rbPay.IsChecked.Value)
                {
                    grpboxdep.IsEnabled = false;
                    GrpRetrait.IsEnabled = false;
                    GrpTransfert.IsEnabled = false;
                    GrpPaiementFacture.IsEnabled = true;
                    TxtDepot.Clear();
                    TxtDepot.Clear();
                    TxtRetrait.Clear();
                    TxtMontantDuTransfert.Clear();
                    TxtCompteTransfert.Clear();
                }
            
            }
            catch
            {
                MessageBox.Show("Erreur");
            }
        }
        private void BtnDeposit_Click(object sender, RoutedEventArgs e)
        {
            try
            {//Debut de la methode try catch
                if ((CmbDepotCompte != null))
                {
                    for (int i = 0; i < clients.ListesClients.Count; i++)
                    {
                        if (MainWindow.id == clients.ListesClients[i].ID)
                        {
                            if ((CmbDepotCompte.Text == "Cheques" && clients.ListesClients[i].TypeDeCompte == 'C')
                                || (CmbDepotCompte.Text == "Epargne" && clients.ListesClients[i].TypeDeCompte == 'E'))
                            {
                                string errorMessage = BanqueMethode.DoDeposit(clients.ListesClients[i], TxtDepot.Text, clients.ListesClients[i].TypeDeCompte, MainWindow.id, transactions);
                                if (!string.IsNullOrEmpty(errorMessage))
                                {
                                    MessageBox.Show(errorMessage);
                                    return;
                                }

                                TxtDepot.Text = String.Empty;
                                clients.Save();

                                if (clients.ListesClients[i].TypeDeCompte == 'C')
                                {
                                    TxtCompteCheque.Text = (string.Format("{0:0.##}", clients.ListesClients[i].Balance));

                                }
                                else
                                {
                                    TxtSoldeEpargne.Text = (string.Format("{0:0.##}", clients.ListesClients[i].Balance));
                                }

                                break;
                            }
                        }
                    }
                    MessageBox.Show("Transaction acceptée");

                }


            }
            catch (Exception )
            {

                MessageBox.Show("Vous devez rentrer des chiffres dans les champs vides!");
                TxtDepot.Text = String.Empty;
                TxtDepot.Focus();
                return;
            }
        }




        private void BtnRetrait_Click(object sender, RoutedEventArgs e)
        {
            try
            {//Debut de la methode try catch
                char typeDeCompte = CmbRetrait.Text == "Cheques" ? 'C' : 'E';
                if (double.Parse(TxtRetrait.Text) > clients.ListesClients[0].Balance)
                {

                    MessageBox.Show($"Problemes techniques veuillez reessayer plus tard, Manque de fonds ");
                    MessageBox.Show($"Le montant du retrait dont vous avez le droit de retirer est de {clients.ListesClients[0].Balance}");
                    TxtRetrait.Text = clients.ListesClients[0].Balance.ToString();
                    return;

                }

                //Appel de la methode de retrait
                string errorMessage = BanqueMethode.DoRetrait(clients, TxtRetrait.Text, MainWindow.id, transactions, typeDeCompte, out double balance);
                //Validation du montant
                if (!string.IsNullOrEmpty(errorMessage))
                {
                    MessageBox.Show(errorMessage);
                    return;
                }

                TxtRetrait.Text = String.Empty;
                //Si le type de compte est cheque donc c'est le compte C dans le txtfile
                if (typeDeCompte == 'C')
                {
                    TxtCompteCheque.Text = balance.ToString();
                }
                else 
                {
                    TxtSoldeEpargne.Text = balance.ToString();
                }

                MessageBox.Show("Transaction Acceptée");
            }
            catch
            {
                MessageBox.Show("Vous devez mettre des chiffres dans les champs vides");
            }
        }

        private void CmbTransfert_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {//Debut de la methode try catch
                //Si on selectionne le compte Cheque le txtcomptetransfert devient Epargne et vice versa
                if ((string)CmbTransfert.SelectedValue == "Cheque")
                {
                    TxtCompteTransfert.Text = "Epargne";
                }
                else
                {
                    TxtCompteTransfert.Text = "Cheque";
                }
            }
            catch
            {
                MessageBox.Show("Erreur8");
            }
        }
        private void BtnTransfert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (double.Parse(TxtMontantDuTransfert.Text) > 100000)
                {
                    MessageBox.Show("Le montant du transfert ne peut exceder 100000$");
                    TxtMontantDuTransfert.Text = String.Empty;
                    return;
                }
                if ((CmbTransfert.Text == null) || (TxtMontantDuTransfert.Text == null))
                {
                    MessageBox.Show("Les champs ne doivent pas etre vide");
                    return;
                }
                if (double.Parse(TxtMontantDuTransfert.Text) <= 0)
                {
                    MessageBox.Show("Vous devez rentrer un montant superieur a 0$");
                    TxtMontantDuTransfert.Text = String.Empty;
                    return;
                }
                for (int i = 0; i < clients.ListesClients.Count; i++)
                {

                    if (MainWindow.id == clients.ListesClients[i].ID && CmbTransfert.Text == "Cheque" && clients.ListesClients[i].TypeDeCompte == 'C')
                    {
                        if (double.Parse(TxtMontantDuTransfert.Text) > clients.ListesClients[i].Balance)
                        {
                            MessageBox.Show($"Le montant du transfert ne peut pas etre superieur au solde du compte cheque");
                            TxtMontantDuTransfert.Text = string.Empty;
                            CmbTransfert.Text = String.Empty;
                            return;
                        }

                        double MontantTransfert = double.Parse(TxtMontantDuTransfert.Text);
                        clients.ListesClients[i].Balance = clients.ListesClients[i].Balance - MontantTransfert;
                        clients.Save();
                        for (int j = 0; j < clients.ListesClients.Count; j++)
                        {
                            if (MainWindow.id == clients.ListesClients[j].ID && CmbTransfert.Text == "Cheque" && clients.ListesClients[j].TypeDeCompte == 'E')
                            {
                                clients.ListesClients[j].Balance = clients.ListesClients[j].Balance + MontantTransfert;
                                //Appel de la methode pour sauvegarder le client
                                clients.Save();
                                //instaure les attribut de Transaction avant de les sauvegarder
                                Transaction transaction2 = new Transaction();
                                transaction2.Date = DateTime.Now;
                                transaction2.TypeDeTransaction = "Transfert";
                                transaction2.Montant = double.Parse(TxtMontantDuTransfert.Text);
                                transaction2.Balance = double.Parse(TxtCompteCheque.Text);
                                transaction2.TypeDeCompte = CmbTransfert.Text;
                                transaction2.ID = MainWindow.id;
                                //Sauvegarde dans un fichier texte
                                transactions.AjouterTransaction(transaction2);


                                TxtCompteCheque.Text = clients.ListesClients[i].Balance.ToString();
                                TxtSoldeEpargne.Text = clients.ListesClients[j].Balance.ToString();
                                break;
                            }
                        }
                        break;
                    }
                    //Si tout les criteres de validation sont accepter ...
                    if (MainWindow.id == clients.ListesClients[i].ID && CmbTransfert.Text == "Epargne" && clients.ListesClients[i].TypeDeCompte == 'E')
                    {//Si tout ces criteres sont aussi accepter...
                        if (double.Parse(TxtMontantDuTransfert.Text) > clients.ListesClients[i].Balance)
                        {
                            MessageBox.Show("Le montant du transfert ne peut pas etre superieur au solde du compte épargne");
                            TxtMontantDuTransfert.Text = string.Empty;
                            return;
                        }

                        double MontantTransfert = double.Parse(TxtMontantDuTransfert.Text);
                        clients.ListesClients[i].Balance = clients.ListesClients[i].Balance - MontantTransfert;
                        clients.Save();

                        for (int j = 0; j < clients.ListesClients.Count; j++)
                        {
                            if (MainWindow.id == clients.ListesClients[j].ID && CmbTransfert.Text == "Epargne" && clients.ListesClients[j].TypeDeCompte == 'C')
                            {
                                clients.ListesClients[j].Balance = clients.ListesClients[j].Balance + MontantTransfert;
                                clients.Save();
                                TxtCompteCheque.Text = clients.ListesClients[j].Balance.ToString();
                                TxtSoldeEpargne.Text = clients.ListesClients[i].Balance.ToString();
                                Transaction transaction = new Transaction();
                                transaction.Date = DateTime.Now;
                                transaction.TypeDeTransaction = "Transfert";
                                transaction.Montant = double.Parse(TxtMontantDuTransfert.Text);
                                transaction.Balance = double.Parse(TxtSoldeEpargne.Text);
                                transaction.TypeDeCompte = CmbTransfert.Text;
                                transaction.ID = MainWindow.id;
                                transactions.AjouterTransaction(transaction);


                                break;
                            }
                        }
                    }
                }
                MessageBox.Show("Transaction Acceptée");
                TxtMontantDuTransfert.Text = string.Empty;
            }
            catch
            {
                MessageBox.Show("Erreur 10");
            }
        }
        private void BtnPaiement_Click(object sender, RoutedEventArgs e)
        {
            if (double.Parse(TxtMontantPaiement.Text) > 10000)
            {
                MessageBox.Show("Le montant du paiement ne peut etre superieur a 10000$");
                TxtMontantPaiement.Text = String.Empty;
                TxtMontantPaiement.Focus();
                return;
            }
            if (Cmbchoixcmppaiment.Text == null || TxtMontantPaiement.Text == null || CmbChoixFournisseur.Text==null)
            {
                MessageBox.Show("Aucun champs ne peut etre vide");
                TxtMontantPaiement.Text = String.Empty;
                return;
            }
            for (int i = 0; i < clients.ListesClients.Count; i++)
            {
                if(CmbChoixFournisseur.Text!=null && Cmbchoixcmppaiment.Text!=null)
                {
                    double FraisDeTransaction = 1.25;

                    if (MainWindow.id==clients.ListesClients[i].ID && Cmbchoixcmppaiment.Text=="Cheque" && clients.ListesClients[i].TypeDeCompte == 'C')
                    {
                        double Total = clients.ListesClients[i].Balance - (double.Parse(TxtMontantPaiement.Text)+FraisDeTransaction);
                        clients.ListesClients[i].Balance = Total;
                        clients.Save();
                        TxtCompteCheque.Text = clients.ListesClients[i].Balance.ToString();
                        Transaction transaction = new Transaction();
                        transaction.Date = DateTime.Now;
                        transaction.TypeDeTransaction = "Paiement facture";
                        transaction.Montant = double.Parse(TxtMontantPaiement.Text);
                        transaction.Balance = double.Parse(TxtCompteCheque.Text);
                        transaction.TypeDeCompte = Cmbchoixcmppaiment.Text;
                        transaction.ID = MainWindow.id;
                        transactions.AjouterTransaction(transaction);
                        TxtMontantPaiement.Text = String.Empty;

                        break;
                    }
                    if (MainWindow.id == clients.ListesClients[i].ID && Cmbchoixcmppaiment.Text == "Epargne" && clients.ListesClients[i].TypeDeCompte == 'E')
                    {
                        double Total = clients.ListesClients[i].Balance - (double.Parse(TxtMontantPaiement.Text) + FraisDeTransaction);
                        clients.ListesClients[i].Balance = Total;
                        clients.Save();
                        TxtSoldeEpargne.Text = clients.ListesClients[i].Balance.ToString();
                        Transaction transaction1 = new Transaction();
                        transaction1.Date = DateTime.Now;
                        transaction1.TypeDeTransaction = "Paiement facture";
                        transaction1.Montant = double.Parse(TxtMontantPaiement.Text);
                        transaction1.Balance = double.Parse(TxtSoldeEpargne.Text);
                        transaction1.TypeDeCompte = Cmbchoixcmppaiment.Text;
                        transaction1.ID = MainWindow.id;
                        transactions.AjouterTransaction(transaction1);
                        TxtMontantPaiement.Text= String.Empty;

                        break;

                    }
                }

            }
            MessageBox.Show("Transaction accepter");
        }
    }
}




