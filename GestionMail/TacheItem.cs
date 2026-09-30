using System.ComponentModel;

namespace GestionMail
{
    public class TacheItem : INotifyPropertyChanged
    {
        private string titre;
        private bool isDone;

        public string Titre
        {
            get { return titre; }
            set { titre = value; OnPropertyChanged("Titre"); }
        }

        public bool IsDone
        {
            get { return isDone; }
            set { isDone = value; OnPropertyChanged("IsDone"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string nomPropriete)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nomPropriete));
        }
    }
}
