using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace GestionMail
{
    public partial class TodoWindow : Window
    {
        private readonly ObservableCollection<TacheItem> taches = new ObservableCollection<TacheItem>();

        private readonly string cheminFichier = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "GestionMail", "todolist.txt");

        public TodoWindow()
        {
            InitializeComponent();
            LstTaches.ItemsSource = taches;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChargerTaches();
        }

        private void ChargerTaches()
        {
            taches.Clear();
            if (!File.Exists(cheminFichier)) return;

            try
            {
                foreach (string ligne in File.ReadAllLines(cheminFichier))
                {
                    if (string.IsNullOrWhiteSpace(ligne)) continue;
                    string[] parties = ligne.Split(new char[] { '|' }, 2);
                    if (parties.Length != 2) continue;

                    bool isDone = parties[0] == "1";
                    taches.Add(new TacheItem { Titre = parties[1], IsDone = isDone });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible de charger la to-do list : " + ex.Message);
            }
        }

        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            string titre = TxtNouvelleTache.Text.Trim();
            if (string.IsNullOrWhiteSpace(titre)) return;

            taches.Add(new TacheItem { Titre = titre, IsDone = false });
            TxtNouvelleTache.Clear();
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            TacheItem tacheSelectionnee = LstTaches.SelectedItem as TacheItem;
            if (tacheSelectionnee == null)
            {
                MessageBox.Show("Sélectionne d'abord une tâche dans la liste.");
                return;
            }
            taches.Remove(tacheSelectionnee);
        }

        private void BtnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dossier = Path.GetDirectoryName(cheminFichier);
                if (!Directory.Exists(dossier)) Directory.CreateDirectory(dossier);

                using (StreamWriter writer = new StreamWriter(cheminFichier, false))
                {
                    foreach (TacheItem tache in taches)
                        writer.WriteLine((tache.IsDone ? "1" : "0") + "|" + tache.Titre);
                }

                MessageBox.Show("To-do list enregistrée.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message);
            }
        }
    }
}