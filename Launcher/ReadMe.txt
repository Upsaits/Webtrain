================================================================================
    MICROSOFT FOUNDATION CLASS LIBRARY : Launcher Projekt�bersicht
================================================================================

Der Anwendungs-Assistent hat diese Launcher-Anwendung erstellt. 
Diese Anwendung zeigt die prinzipielle Anwendung der Microsoft Foundation Classes 
und dient als Ausgangspunkt f�r die Erstellung Ihrer eigenen Anwendungen.

Diese Datei enth�lt die Zusammenfassung der Bestandteile aller Dateien, die 
Ihre Launcher-Anwendung bilden.

Launcher.vcproj
    Dies ist die Hauptprojektdatei f�r VC++-Projekte, die vom Anwendungs-Assistenten 
    erstellt wird. Sie enth�lt Informationen �ber die Version von Visual C++, mit der
    die Datei generiert wurde, �ber die Plattformen, Konfigurationen und Projektfeatures,
    die mit dem Anwendungs-Assistenten ausgew�hlt wurden.

Launcher.h
    Hierbei handelt es sich um die Haupt-Headerdatei der Anwendung. Diese enth�lt 
    andere projektspezifische Header (einschlie�lich Resource.h) und deklariert die
    CLauncherApp-Anwendungsklasse.

Launcher.cpp
    Hierbei handelt es sich um die Haupt-Quellcodedatei der Anwendung. Diese enth�lt die
    Anwendungsklasse CLauncherApp.

Launcher.rc
    Hierbei handelt es sich um eine Auflistung aller Ressourcen von Microsoft Windows, die 
    vom Programm verwendet werden. Sie enth�lt die Symbole, Bitmaps und Cursors, die im 
    Unterverzeichnis RES gespeichert sind. Diese Datei l�sst sich direkt in Microsoft
    Visual C++ bearbeiten. Ihre Projektressourcen befinden sich in 1031.

res\Launcher.ico
    Dies ist eine Symboldatei, die als Symbol f�r die Anwendung verwendet wird. Dieses 
    Symbol wird durch die Haupt-Ressourcendatei Launcher.rc eingebunden.

res\Launcher.rc2
    Diese Datei enth�lt Ressourcen, die nicht von Microsoft Visual C++ bearbeitet wurden.
    In dieser Datei werden alle Ressourcen gespeichert, die vom Ressourcen-Editor nicht bearbeitet 
    werden k�nnen.

/////////////////////////////////////////////////////////////////////////////

Der Anwendungs-Assistent erstellt eine Dialogklasse:
LauncherDlg.h, LauncherDlg.cpp - das Dialogfeld
    Diese Dateien enthalten die Klasse CLauncherDlg. Diese Klasse legt das
    Verhalten des Haupt-Dialogfelds der Anwendung fest. Die Vorlage des Dialog-
    felds befindet sich in Launcher.rc, die mit Microsoft Visual C++
    bearbeitet werden kann.
/////////////////////////////////////////////////////////////////////////////

Weitere Features:

ActiveX-Steuerelemente
    Die Anwendung unterst�tzt die Verwendung von ActiveX-Steuerelementen.
/////////////////////////////////////////////////////////////////////////////

Weitere Standarddateien:

StdAfx.h, StdAfx.cpp
    Mit diesen Dateien werden vorkompilierte Headerdateien (PCH) mit der Bezeichnung
    Launcher.pch und eine vorkompilierte Typdatei mit der Bezeichnung 
    StdAfx.obj erstellt.

Resource.h
    Dies ist die Standard-Headerdatei, die neue Ressourcen-IDs definiert.
    Microsoft Visual C++ liest und aktualisiert diese Datei.

/////////////////////////////////////////////////////////////////////////////

Weitere Hinweise:

Der Anwendungs-Assistent verwendet "TODO:", um die Stellen anzuzeigen, 
an denen Sie Erweiterungen oder Anpassungen vornehmen k�nnen.

Wenn Ihre Anwendung die MFC in einer gemeinsam genutzten DLL verwendet und 
eine andere als die aktuell auf dem Betriebssystem eingestellte Sprache verwendet, muss 
die entsprechend lokalisierte Ressource MFC70XXX.DLL von der Microsoft Visual C++ CD-ROM 
in das Verzeichnis system oder system32 kopiert und in MFCLOC.DLL umbenannt werden  
("XXX" steht f�r die Abk�rzung der Sprache. So enth�lt beispielsweise MFC70DEU.DLL die ins Deutsche 
�bersetzten Ressourcen.)  Anderenfalls werden einige Oberfl�chenelemente Ihrer Anwendung 
in der Sprache des Betriebssystems angezeigt.

/////////////////////////////////////////////////////////////////////////////
