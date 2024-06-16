using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SoccerResults
{
    class Pruefungs
    {
        public static void Run()
        {
            // Festlegen des Pfads zum Verzeichnis "soccer-results"
            string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "soccer-results");

            // Überprüfen, ob das Verzeichnis "soccer-results" existiert
            if (!Directory.Exists(rootPath))
            {
                Console.WriteLine("Der Ordner 'soccer-results' existiert nicht.");
                return;
            }

            // Benutzer nach dem Namen der Liga fragen
            Console.WriteLine("Bitte geben Sie den Namen der Liga ein:");
            string leagueName = Console.ReadLine();
            string leaguePath = Path.Combine(rootPath, leagueName);

            // Überprüfen, ob das Verzeichnis der angegebenen Liga existiert
            if (!Directory.Exists(leaguePath))
            {
                Console.WriteLine($"Die Liga '{leagueName}' existiert nicht.");
                return;
            }

            // Benutzer nach der Anzahl der zu berücksichtigenden Spieltage fragen
            Console.WriteLine("Bitte geben Sie die Anzahl der zu berücksichtigenden Spieltage ein:");
            // Überprüfen, ob die Eingabe eine gültige Zahl ist und größer als 0 ist
            if (!int.TryParse(Console.ReadLine(), out int daysCount) || daysCount < 1)
            {
                Console.WriteLine("Ungültige Anzahl von Spieltagen.");
                return;
            }

            // Erstellen eines Liga-Objekts und Berechnen der Tabelle
            League league = new League(leaguePath);
            league.LoadMatches(daysCount); // Laden der Spiele
            league.CalculateStandings(); // Berechnen der Tabellenstände
            league.DisplayTable(); // Anzeigen der Tabelle

            // Manuell berechnete Daten für die Prüfung eingeben
            List<Team> expectedTeams = new List<Team>
            {
                new Team("TeamA") { Points = 7, Wins = 2, Draws = 1, Losses = 0, GoalsFor = 4, GoalsAgainst = 2, GoalDifference = 2 },
                new Team("TeamD") { Points = 5, Wins = 1, Draws = 2, Losses = 0, GoalsFor = 6, GoalsAgainst = 3, GoalDifference = 3 },
                new Team("TeamC") { Points = 2, Wins = 0, Draws = 2, Losses = 1, GoalsFor = 5, GoalsAgainst = 6, GoalDifference = -1 },
                new Team("TeamB") { Points = 1, Wins = 0, Draws = 1, Losses = 2, GoalsFor = 2, GoalsAgainst = 6, GoalDifference = -4 }
            };

            // Validieren der berechneten Ergebnisse gegen die erwarteten Ergebnisse
            bool isValid = ValidateResults(league.Teams, expectedTeams);

            Console.WriteLine();
            // Ausgabe des Validierungsergebnisses
            Console.WriteLine("Validierungsergebnis: " + (isValid ? "Positiv" : "Negativ"));
        }

        // Methode zum Validieren der berechneten Ergebnisse gegen die erwarteten Ergebnisse
        private static bool ValidateResults(List<Team> actualTeams, List<Team> expectedTeams)
        {
            // Überprüfen, ob die Anzahl der Teams übereinstimmt
            if (actualTeams.Count != expectedTeams.Count)
            {
                return false;
            }

            // Vergleichen der tatsächlichen und erwarteten Teamdaten
            for (int i = 0; i < actualTeams.Count; i++)
            {
                var actual = actualTeams[i];
                var expected = expectedTeams[i];

                // Überprüfen, ob alle relevanten Felder übereinstimmen
                if (actual.Name != expected.Name ||
                    actual.Points != expected.Points ||
                    actual.Wins != expected.Wins ||
                    actual.Draws != expected.Draws ||
                    actual.Losses != expected.Losses ||
                    actual.GoalsFor != expected.GoalsFor ||
                    actual.GoalsAgainst != expected.GoalsAgainst ||
                    actual.GoalDifference != expected.GoalDifference)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
