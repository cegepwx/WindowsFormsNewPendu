using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsNewPendu
{
    public partial class PenduForm1 : Form
    {
        private string username;
        private int partiesPendu;
        private int gagnePoint = 0;
        private int numberEssaie=5;
        private string motChoisi;
        private char[] motPendu;
        private ListeDeMots words = new ListeDeMots();

        public PenduForm1()
        {
            InitializeComponent();
            label2.Text = label3.Text = label4.Text = label5.Text = label6.Text = label7.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = userInput.Text;
            commencer();
        }

        public void commencer()
        {
            partiesPendu++;
            numberEssaie = 5;
            label5.Text = "";
            afficherLabel6et7();
            textBox1.Text = "";
            this.afficherSessoin();
            this.tirerUnMots();
        }

        public void afficherLabel6et7()
        {
            label6.Text = "Vous avez encore " + numberEssaie + " essaie.";
            label7.Text = "Veuillez saisir votre essaie: ";
        }


        public void afficherSessoin()
        {
            label2.Text = "Le nom de l'utilisateur: " + username;
            label3.Text = "Le nombre de partie Pendu: " + partiesPendu;
            label4.Text = "Vous avez gagné " + gagnePoint+" point(S).";
        }

        //**Tirer un mots(motChoisi) et afficher le mots en "-----"
        public void tirerUnMots()
        {
            string motTemp = "";
            Random random = new Random();
            int index = random.Next(0, words.Valeur.Count);
            motChoisi = words.Valeur[index];
            motPendu = motChoisi.ToCharArray();

            for (int i = 0; i < motPendu.Length; i++)
            {
                motPendu[i] = '-';
                motTemp = motTemp + motPendu[i];
            }
            label5.Text = "Le mot est: " + motTemp;
            words.Valeur.RemoveAt(index);
            if (words.Valeur.Count == 0)
            {
                words = new ListeDeMots();
            }
        }

        public void choisirIndice()
        {
            if (motChoisi.Length > 10)
            {
                if (MessageBox.Show("Le mot comporte plus de 10 caractères.\n" +
                        "Voulez-vous avoir un indice? (Ceci vous pénalise d’un essai)\n",
                        "Indice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    avoirIndice();
                }
                else { userEssaie(); }
            }
            else { userEssaie(); }
        }

        public void avoirIndice()
        {
            Random random = new Random();
            int index = random.Next(0, motChoisi.Length);
            char chRandom = motChoisi[index];
            changerCharInmotPendu(chRandom);
            AfficherGagnant();
            //numberEssaie--;
            //textBox1.Text = "";

            if (numberEssaie == 0)
            {
                perduParti();
            }
            else
            {
                afficherLabel6et7();
            }
            numberEssaie--;
            textBox1.Text = "";
        }

        public void changerCharInmotPendu(char ch)
        {
            for (int i = 0; i < motChoisi.Length; i++)
            {
                if (ch.Equals(motChoisi[i]))
                {
                    motPendu[i] = ch;
                }
            }
        }

        public void AfficherGagnant()
        {
            string motTemp = "";
            for (int i = 0; i < motPendu.Length; i++)
            {
                motTemp += motPendu[i];
            }
            label5.Text ="Le mot est: "+ motTemp;
        }

        public void userEssaie()
        {
            //numberEssaie--;

            if (numberEssaie > 0)
            {
                afficherLabel6et7();
                textBox1.Focus();
                string input = textBox1.Text;
                if (input.Length > motChoisi.Length)
                {
                    input = input.Substring(0, motChoisi.Length);
                }
                trouverLetters(input);
                AfficherGagnant();
                gagnerPoints(input);
            }
            else 
            {
                perduParti();
            }
            textBox1.Text = "";
            numberEssaie--;
        }

        public void perduParti()
        {
            afficherLabel6et7();
            MessageBox.Show("Vous avez perdu.\nLe mot était: " + motChoisi);
            commencer();
        }

        public void trouverLetters(string saisir)
        {
            for (int i = 0; i < saisir.Length; i++)
            {
                for (int j = 0; j < motChoisi.Length; j++)
                {
                    if (saisir[i].Equals(motChoisi[j]))
                    {
                        motPendu[j] = motChoisi[j];
                    }
                }
            }
        }

        public void gagnerPoints(string saisirMots)
        {
            if (saisirMots.Equals(motChoisi))
            {
                gagnePoint++;
                MessageBox.Show("Vous avez gagné 1 partie.\n Vos points sont:" + gagnePoint);
                if (gagnePoint == 3)
                {
                    MessageBox.Show("Félicitation! Vous avez gagné!");
                    this.Close();
                }
                else commencer();
            }
        }

        private void textBox1_DoubleClick_1(object sender, EventArgs e)
        {
            choisirIndice();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            choisirIndice();
        }

        //private void textBox1_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        choisirIndice();

        //        e.Handled = true;
        //    }
        //}
    }
}
