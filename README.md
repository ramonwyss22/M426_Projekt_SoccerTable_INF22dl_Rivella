# 1. Instruktion zur Installation

### Angabe der Voraussetzungen
Um das Projekt "SoccerResults" zu installieren und auszuführen, benötigen Sie die folgenden Voraussetzungen:
- .NET SDK (Version 6.0 oder höher): Stellen Sie sicher, dass das .NET SDK auf Ihrem System installiert ist. 
- Git: Git muss installiert sein, um das Repository zu klonen.
- VisualStudio Code 
- VSC Erweiterung C#
  
### Installation der Abhängigkeiten

1. Projekt klonen:
   Öffnen Sie ein Terminal und klonen Sie das Repository in Ihr lokales Verzeichnis. Dies geschieht mit dem folgenden Befehl:
   git clone <repository-url>
   cd SoccerResults
   
   Man kann es auch im Github herunterladen und in einem lokalen Verzeichnis ablegen. 
   Danach muss man VisualStudio Code öffnen (Voraussetzungen bereits gemacht).

2. Abhängigkeiten wiederherstellen:
   Nachdem Sie das Projekt geklont haben, müssen Sie sicherstellen, dass alle Abhängigkeiten korrekt wiederhergestellt werden. Verwenden Sie dafür den folgenden Befehl:
   dotnet restore

   Dieser Befehl lädt alle erforderlichen NuGet-Pakete, die im Projekt benötigt werden.

3. Projekt bauen:
   Um sicherzustellen, dass alles korrekt eingerichtet ist und das Projekt gebaut werden kann, führen Sie den folgenden Befehl aus:
   dotnet build

   Dieser Befehl kompiliert den Code und erstellt die ausführbaren Dateien.

Durch das Befolgen dieser Schritte stellen Sie sicher, dass alle erforderlichen Komponenten und Abhängigkeiten für das Projekt "SoccerResults" korrekt installiert sind.







# 2. Instruktion zur Ausführung der Software

### Projektverzeichnis öffnen:
Sobald Sie alle Abhängigkeiten wiederhergestellt haben und das Projekt im Visual Studio Code bereit ist, öffnen Sie ein Terminal im Projektverzeichnis. Navigieren Sie in das Verzeichnis `SoccerResults`.

### Anwendung ausführen:
Geben Sie im Terminal den folgenden Befehl ein, um die Hauptanwendung auszuführen und die Tabelle für die wählbare Liga auszugeben: dotnet run

### Benutzereingaben tätigen:
Nach dem Start der Anwendung werden Sie aufgefordert, den Namen der Liga einzugeben. Geben Sie den Namen der gewünschten Liga ein (z.B. `bundesliga`) und drücken Sie die Eingabetaste.
Anschließend werden Sie aufgefordert, die Anzahl der zu berücksichtigenden Spieltage einzugeben. Geben Sie die gewünschte Anzahl der Spieltage ein (z.B. `2`) und drücken Sie die Eingabetaste. Um alle Spieltage zu berücksichtigen, müssen Sie einfach eine zu hohe Zahl eingeben, die größer ist als die Anzahl der vorhandenen Spieltage. Dann bekommen Sie das Ergebnis von allen möglichen Spieltagen.

### Ergebnisse ansehen:
Die Anwendung berechnet die Tabellenstände basierend auf den eingegebenen Daten und zeigt die Ergebnisse an. Die Ausgabe erfolgt in einer übersichtlichen Tabelle, die Rang, Name, Punkte, Siege, Niederlagen, Unentschieden, Tore, Gegentore und Tordifferenz der Teams darstellt.







# 3. Instruktionen zur Ausführung der Unittests

### Erstellen der Testdateien:
Stellen Sie sicher, dass die folgenden Testdateien im Verzeichnis `soccer-results/Liga` existieren: day01.txt, day02.txt, usw.

### Prüfungsfunktion ausführen:
Um die Prüfungsfunktion auszuführen, navigieren Sie in das Verzeichnis `SoccerResults`. Öffnen Sie ein Terminal im Projektverzeichnis und geben Sie den folgenden Befehl ein: dotnet run -- pruefungs

### Prüfungsergebnisse ansehen:
Nach dem Start der Anwendung werden Sie aufgefordert, den Namen der Liga einzugeben. Geben Sie den Namen der gewünschten Liga ein (z.B. `unittest-liga`) und drücken Sie die Eingabetaste.
Anschließend werden Sie aufgefordert, die Anzahl der zu berücksichtigenden Spieltage einzugeben. Geben Sie die gewünschte Anzahl der Spieltage ein (z.B. `3`) und drücken Sie die Eingabetaste.
Die Prüfungsfunktion lädt die Spiele der angegebenen Liga und Spieltage, berechnet die Tabellenstände und vergleicht sie mit den manuell eingegebenen Ergebnissen, welche man zuvor in der `Pruefungs.cs`-Datei eintragen und selbst berechnen muss. Das Validierungsergebnis wird am Ende angezeigt und gibt an, ob die berechneten Ergebnisse mit den erwarteten Ergebnissen übereinstimmen (Positiv oder Negativ).








# Clean Code
Der Code des Projekts "SoccerResults" folgt den Prinzipien von Clean Code:

- Formatierung: Der Code ist gut formatiert, was die Lesbarkeit und Wartbarkeit erhöht. Ein konsistenter Einrückungsstil wird beibehalten, und unnötige Leerzeilen werden vermieden.
- Benennung: Variablen- und Methodennamen sind klar und beschreibend, was die Verständlichkeit des Codes verbessert. Namen wie `LoadMatches`, `CalculateStandings` und `DisplayTable` machen deutlich, was die Methoden tun.
- Kommentare: Wesentliche Teile des Codes sind kommentiert, um deren Funktionalität zu erklären. Dies hilft anderen Entwicklern, den Code schneller zu verstehen.
- Wiederverwendbarkeit: Der Code ist modular aufgebaut, sodass Klassen und Methoden leicht wiederverwendet werden können. Die Struktur der Klassen `League`, `Match` und `Team` unterstützt die Wiederverwendbarkeit.
- Klarheit: Der Code ist strukturiert und verständlich, was die Fehlerbehebung und Erweiterung erleichtert. Jede Methode hat eine klar definierte Aufgabe, und die Verantwortlichkeiten sind gut verteilt.

# Simple Design
Die Anwendung "SoccerResults" ist nach dem Prinzip des Simple Design entwickelt:

- Einfachheit: Die Anwendung ist so einfach wie möglich gehalten und konzentriert sich auf die wesentlichen Funktionalitäten, die erforderlich sind, um die Aufgabe zu erfüllen. Es wurden keine unnötigen Funktionen oder komplexen Logiken implementiert.
- Keine unnötige Komplexität: Der Code vermeidet überflüssige Komplexität und bleibt auf das Wesentliche beschränkt. Die Anwendung folgt dem Prinzip "Keep It Simple, Stupid" (KISS), was die Wartung und Erweiterung der Anwendung erleichtert.

# Abschluss
Das Projekt "SoccerResults" ist eine gut strukturierte und leicht verständliche Anwendung, die den Prinzipien von Clean Code und Simple Design folgt. Die Installation und Ausführung der Anwendung sowie die Durchführung der Unittests sind einfach und klar beschrieben. Dies stellt sicher, dass das Projekt leicht von anderen Entwicklern übernommen und erweitert werden kann.
