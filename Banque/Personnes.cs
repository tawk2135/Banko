using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace Banque
{
    public class Personnes
    {
        private string nom;
        private string id;
        private string password;
        public ObservableCollection<Personnes> personne;
        //Declaration des proprietes
        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        public Personnes(string ID, string Nom, string Password)//Constructeur de notre classe
        {
            nom = Nom;
            id = ID;
            password = Password;
        }
        public override string ToString()
        {
            return $"{Nom};{ID}";
        }

        //Creation de notre liste clients de type Client.
        public ObservableCollection<Personnes> Personne 
        {
            get { return personne; }
            set { personne = value; } 
        }//= new ObservableCollection<Personne>();

        static string filename = "Clients.txt";// nom du fichier.
        string line;//Servira a la recuperation de la lecturee d'une ligne dans le fichier texte
        string[] clientele;//Servira a decomposer la ligne lue et recueilli par la variable ligne.

        public Personnes() //Constructeur de notre classe
        {
            Personne = new ObservableCollection<Personnes>();
            if (!File.Exists(filename))//Si le fichier n'existe pas
            {
                //Lever une erreur avec un message specifique.
                throw new Exception($"Le fichier {filename} n'existe pas dans mon repertoire");
            }
            else
            {
                //Creation d'un nouveau lecteur de flux de donnees pour le filename
                StreamReader reader = new StreamReader(filename);
                //boucle effectuant la lecture de chacune des lignes du fichier.
                while ((line = reader.ReadLine()) != null)
                {
                    //Recuperation de chacun des elements de donnees dans le tableau clientele
                    clientele = line.Split(';');
                    personne.Add(new Personnes(clientele[0], clientele[1], clientele[2]));
                }
            }
        }
        public string AuthentifierUtilisateur(string utilisateur, string password)
        {
            string ID = "";
            foreach (Personnes personnes in personne)
            {  //Si les variables contiennent des valeurs, comparaisons avec les valeurs attendues.

                if (utilisateur == personnes.Nom && password == personnes.Password)
                {
                    //Si les valeurs saisies sont valides, nous modifions sa propriete
                    ID = personnes.Nom;
                }
            }
            return ID;
        }

    }
}
