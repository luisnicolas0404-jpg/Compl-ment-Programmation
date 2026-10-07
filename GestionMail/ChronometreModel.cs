using System;

namespace GestionMail
{
    // Le Model ne contient que la donnée brute, aucune logique d'affichage
    public class ChronometreModel
    {
        public TimeSpan TempsEcoule { get; set; } = TimeSpan.Zero;
    }
}