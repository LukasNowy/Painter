#Painter

Ziele:
- Formen zeichnen (Rechtecke, Quadrate, Kreise, Dreiecke,...)
- Eigene Farbe (RGB) auswählen
- Größe des Bildes (der Bitmap) ändern
- Bilder importieren und erstellte Bilder exportieren
- Radiergummi
- Gerade Linien zeichnen
- Mit Pinsel zeichnen

Funktion:
- Zuerst eine Bitmap mit beliebiger Größe und Hintergrundfarbe erstellen
- Auswählen welche Form man zeichnen möchte (Radio Buttons)
- Alle Formen werden in einer ListBox angezeigt
- Man kann ausgewählte Formen wieder löschen und die Farbe ändern
- Die Bitmap kann man über einen SaveFileDialog als Bild speichern
- Über einen OpenFileDialog kann man Bilder importieren

Verwendete Klassen:
- Geometrische_Formen (abstrakte Klasse)
- Rechteck (erbt von Geometrische_Formen)
- Quadrat (erbt von Geometrische_Formen)
- Kreis (erbt von Geometrische_Formen)
- Dreieck (erbt von Geometrische_Formen)
