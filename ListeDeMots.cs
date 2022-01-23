using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsNewPendu
{
    class ListeDeMots
    {
        private List<string> valeur;
        public List<string> Valeur { get => valeur; set => valeur = value; }

        public ListeDeMots()
        {
            valeur = new List<string>{ "imperméabilisation", "éligibilité", "possibilité", "couleur",
            "céréales", "projet", "friteur", "chanson", "recyclable", "chien", "pandémie" };
        }

        public string motsTire()
        {
            Random rendom = new Random();
            int index = rendom.Next(0, valeur.Count);
            string word = valeur[index];
            valeur.RemoveAt(index);
            return word;
        }
    }
}
