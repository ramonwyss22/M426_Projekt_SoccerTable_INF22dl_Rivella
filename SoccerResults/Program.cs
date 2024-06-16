using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleTables;

namespace SoccerResults
{
    class Program
    {
        static void Main(string[] args)
        {
            // Überprüfen, ob das Programm im Prüfungsmodus ausgeführt werden soll
            if (args.Length > 0 && args[0] == "pruefungs")
            {
                Pruefungs.Run();
            }
            else
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
            }
        }
    }

    class League
    {
        public string Path { get; set; }
        public List<Match> Matches { get; set; }
        public List<Team> Teams { get; set; }

        public League(string path)
        {
            Path = path;
            Matches = new List<Match>();
            Teams = new List<Team>();
        }

        // Laden der Spiele aus den Dateien
        public void LoadMatches(int daysCount)
        {
            // Abrufen der Dateinamen der Spieltage und Begrenzen auf die angegebene Anzahl von Tagen
            string[] matchFiles = Directory.GetFiles(Path, "*.txt").OrderBy(f => f).Take(daysCount).ToArray();

            // Lesen der Spieltage aus den Dateien
            foreach (string file in matchFiles)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Matches.Add(new Match(line)); // Hinzufügen jedes Spiels zur Liste der Spiele
                    }
                }
            }
        }

        // Berechnen der Tabellenstände
        public void CalculateStandings()
        {
            // Aktualisieren der Teamstatistiken basierend auf den Spielen
            foreach (var match in Matches)
            {
                UpdateTeam(match.HomeTeam, match.HomeGoals, match.AwayGoals);
                UpdateTeam(match.AwayTeam, match.AwayGoals, match.HomeGoals);
            }

            // Sortieren der Teams nach Punkten, Tordifferenz (näher an 0 ist besser), Siegen und Namen
            Teams = Teams.OrderByDescending(t => t.Points)
                         .ThenBy(t => Math.Abs(t.GoalDifference)) // Änderung: Je näher die Tordifferenz an 0 ist, desto höher die Platzierung
                         .ThenByDescending(t => t.Wins)
                         .ThenBy(t => t.Name)
                         .ToList();
        }

        // Aktualisieren der Teamstatistiken
        private void UpdateTeam(string teamName, int goalsFor, int goalsAgainst)
        {
            // Suchen des Teams in der Liste
            Team team = Teams.FirstOrDefault(t => t.Name == teamName);
            // Wenn das Team nicht existiert, neues Team erstellen und zur Liste hinzufügen
            if (team == null)
            {
                team = new Team(teamName);
                Teams.Add(team);
            }

            // Aktualisieren der Statistiken des Teams
            team.MatchesPlayed++;
            team.GoalsFor += goalsFor;
            team.GoalsAgainst += goalsAgainst;
            team.GoalDifference = team.GoalsFor - team.GoalsAgainst;

            // Aktualisieren der Punkte basierend auf dem Spielergebnis
            if (goalsFor > goalsAgainst)
            {
                team.Wins++;
                team.Points += 3;
            }
            else if (goalsFor == goalsAgainst)
            {
                team.Draws++;
                team.Points += 1;
            }
            else
            {
                team.Losses++;
            }
        }

        // Anzeigen der Tabelle
        public void DisplayTable()
        {
            var table = new ConsoleTable("Rang", "Name", "Punkte", "Siege", "Niederlagen", "Unentschieden", "Tore", "Gegentore", "Tordifferenz");

            int rank = 1;
            foreach (var team in Teams)
            {
                // Hinzufügen einer Zeile zur Tabelle für jedes Team
                table.AddRow(
                    rank++.ToString(),
                    team.Name,
                    team.Points.ToString().PadLeft(7),
                    team.Wins.ToString().PadLeft(5),
                    team.Losses.ToString().PadLeft(11),
                    team.Draws.ToString().PadLeft(13),
                    team.GoalsFor.ToString().PadLeft(5),
                    team.GoalsAgainst.ToString().PadLeft(10),
                    team.GoalDifference.ToString().PadLeft(12)
                );
            }

            Console.WriteLine();
            table.Options.EnableCount = false; // Deaktivieren der Zeilenanzahlanzeige
            table.Write(Format.Alternative); // Alternative Formatierung der Tabelle
            Console.WriteLine();
        }
    }

    class Match
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }

        // Initialisieren eines Match-Objekts aus einer Ergebniszeile
        public Match(string matchResult)
        {
            var parts = matchResult.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int scoreIndex = Array.FindIndex(parts, p => p.Contains(":"));

            // HomeTeam und AwayTeam aus der Ergebniszeile extrahieren
            HomeTeam = string.Join(" ", parts.Take(scoreIndex));
            AwayTeam = string.Join(" ", parts.Skip(scoreIndex + 1));

            // Tore des Heim- und Auswärtsteams extrahieren und in Ganzzahlen umwandeln
            string[] score = parts[scoreIndex].Split(':');
            HomeGoals = int.Parse(score[0]);
            AwayGoals = int.Parse(score[1]);
        }
    }

    class Team
    {
        public string Name { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }

        // Initialisieren eines neuen Teams mit Standardwerten
        public Team(string name)
        {
            Name = name;
            MatchesPlayed = 0;
            Wins = 0;
            Draws = 0;
            Losses = 0;
            GoalsFor = 0;
            GoalsAgainst = 0;
            GoalDifference = 0;
            Points = 0;
        }
    }
}
