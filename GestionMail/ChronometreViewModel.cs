using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace GestionMail
{
    public class ChronometreViewModel : INotifyPropertyChanged
    {
        private readonly ChronometreModel modele = new ChronometreModel();
        private readonly DispatcherTimer timer;
        private bool estEnMarche;

        public ChronometreViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            DemarrerCommand = new RelayCommand(_ => Demarrer(), _ => !EstEnMarche);
            ArreterCommand = new RelayCommand(_ => Arreter(), _ => EstEnMarche);
            ReinitialiserCommand = new RelayCommand(_ => Reinitialiser(), _ => !EstEnMarche && modele.TempsEcoule > TimeSpan.Zero);
        }

        public RelayCommand DemarrerCommand { get; }
        public RelayCommand ArreterCommand { get; }
        public RelayCommand ReinitialiserCommand { get; }

        public bool EstEnMarche
        {
            get { return estEnMarche; }
            private set
            {
                estEnMarche = value;
                OnPropertyChanged(nameof(EstEnMarche));
                DemarrerCommand.RaiseCanExecuteChanged();
                ArreterCommand.RaiseCanExecuteChanged();
                ReinitialiserCommand.RaiseCanExecuteChanged();
            }
        }

        public string TempsAffiche => modele.TempsEcoule.ToString(@"hh\:mm\:ss");

        // Angles des aiguilles (0° = position 12h, sens horaire)
        public double AngleSecondes => modele.TempsEcoule.Seconds * 6;
        public double AngleMinutes => modele.TempsEcoule.Minutes * 6 + modele.TempsEcoule.Seconds * 0.1;
        public double AngleHeures => (modele.TempsEcoule.Hours % 12) * 30 + modele.TempsEcoule.Minutes * 0.5;

        private void Timer_Tick(object sender, EventArgs e)
        {
            modele.TempsEcoule = modele.TempsEcoule.Add(TimeSpan.FromSeconds(1));
            OnPropertyChanged(nameof(TempsAffiche));
            OnPropertyChanged(nameof(AngleSecondes));
            OnPropertyChanged(nameof(AngleMinutes));
            OnPropertyChanged(nameof(AngleHeures));
        }

        private void Demarrer()
        {
            timer.Start();
            EstEnMarche = true;
        }

        private void Arreter()
        {
            timer.Stop();
            EstEnMarche = false;
        }

        private void Reinitialiser()
        {
            modele.TempsEcoule = TimeSpan.Zero;
            OnPropertyChanged(nameof(TempsAffiche));
            OnPropertyChanged(nameof(AngleSecondes));
            OnPropertyChanged(nameof(AngleMinutes));
            OnPropertyChanged(nameof(AngleHeures));
            ReinitialiserCommand.RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nom)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nom));
        }
    }
}