using System;
using System.IO;

class FileSystemAnalyzer
{
    static void Main(string[] args)
    {
        // pfad zum testverzeichnis
        string path = @"C:\Users\Can Mert\Desktop\Test";    // eigenen pfad eingeben um zu testen

        // ruft AnalyzeDirectory auf um das verzeichnis zu analysieren
        var result = AnalyzeDirectory(path);

        // ausgabe der ergebnisse der analyse
        Console.WriteLine($"Anzahl der Dateien: {result.fileCount}");
        Console.WriteLine($"Anzahl der Verzeichnisse: {result.directoryCount}");
        Console.WriteLine($"Gesamtgröße in Bytes: {result.totalSize}");
        Console.WriteLine($"Gesamtgröße in GB: {result.totalSize / Math.Pow(2, 30):F2}");
        Console.WriteLine($"Gesamtgröße in TB: {result.totalSize / Math.Pow(2, 40):F2}");
    }

    // rekursive funktion zur analyse eines verzeichnisses
    static (int fileCount, int directoryCount, long totalSize) AnalyzeDirectory(string path)
    {
        // initialisierung der zähler für dateien, verzeichnisse und gesamtgröße
        int fileCount = 0;
        int directoryCount = 0;
        long totalSize = 0;

        try
        {
            // durchsuchen aller dateien im aktuellen verzeichnis
            foreach (var file in Directory.GetFiles(path))
            {
                fileCount++; // zählt jede datei
                totalSize += new FileInfo(file).Length; // addiert die größe jeder datei zur gesamtgröße
            }

            // durchsuchen aller unterverzeichnisse im aktuellen verzeichnis
            foreach (var directory in Directory.GetDirectories(path))
            {
                directoryCount++; // zählt jedes unterverzeichnis
                var subResult = AnalyzeDirectory(directory); // rekursiver aufruf für jedes unterverzeichnis

                // addiert die ergebnisse aus dem unterverzeichnis zu den gesamtzählern
                fileCount += subResult.fileCount;
                directoryCount += subResult.directoryCount;
                totalSize += subResult.totalSize;
            }
        }
        catch (Exception e)
        {
            // fehlermeldung, falls der zugriff auf das verzeichnis fehlschlägt
            Console.WriteLine($"Fehler beim Zugriff auf das Verzeichnis: {path}, Fehler: {e.Message}");
        }

        // rückgabe der gesamtanzahlen und der gesamtgröße
        return (fileCount, directoryCount, totalSize);
    }
}