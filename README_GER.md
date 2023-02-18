RAPROTWINGS ©2023 by Raptoreum und Germardies
16.02.2023
=============================================
Lightpapaer
-----------
RAPTORWINGS ist eine vollständig quelloffene, kostenlose GUI-Software.
Sie dient als Raptoreum-Dashboard und als einfaches Werkzeug für das Mining und die Verfolgung von RTM für den täglichen Benutzer.

Diese Software wurde geschrieben um der RTM Community folgendes zu bieten:
1. Eine Übersicht der eigenen Walletadressen mit Balance und Preisanzeige
2. Eine Grafische Oberfläche für lokales Mining
3. Eine Grafische Oberfläche für das Mining auf externen Geräten
4. Einer der einfachsten Wege in der Blockchain sowohl für neue als auch für erfahrene Miner, um mit dem Raptoreum-Mining zu beginnen, 
sowohl mit lokalen als auch mit cloudbasierten Implementierungen

Die Nutzung ist völlig kostenlos und ohne jegliche Gebühr.
Mining Gebühr wird von den Drittanbietern und der Pool-Server festgelegt.
Wenn Raptorwings sich als beliebt erweist, werden wir es weiter ausbauen.

API's: 
------
Diese GUI Software bedient sich während der Nutzung folgender Daten: 
 - API: https://explorer.raptoreum.com 
 - API: https://api.coingecko.com
 - API: https://raptorhash.com/
 - API: https://raptoreum.zone/
 - API: https://flockpool.com
 
DRITTANBIETER SOFTWARE: 
-----------------------
Es werden folgende Programme von Drittanbietern verwendet (In der aktuellen Rapworwings Version, im Packet enthalten): 
- SRBMINER-MULTI: https://srbminer.com
- Putty: https://putty.org
- RAPTORWINGS ist in Visual Basic mit Visual Studio Community Edition programmiert (https://visualstudio.microsoft.com/de/vs/)
 
HAFTUNGSAUSSLUSS
----------------
Für die Daten der API's sowie die Funktion weise der genutzten Drittanbieterprogramme wird keine Haftung übernommen. 
Sie sind mit der Nutzung dieser Software eigenständig verantwortlich. 
 
DATENSCHUTZ
-----------
Es werden durch diese Software keine Daten gesammelt, weitergeleitet oder extern gespeichert. 
Eine Datenerhebung findet nicht statt. 
Alle von Ihnen gespeicherten Daten werden ausschließlich auf Ihren Geräten abgelegt. 
 
HINWEIS
-------
- Dieses Software ist ein offizielles Programm von Raptoreum, Feathered Corp.
- Es werden Dateien auf Ihrem PC abgelegt "C:\Users\<user>\Appdata\Locale\Raptorwings\" welche der Speicherung Ihrer Daten dient.
 
BETEILIGTE
-------------------
Germardies - Code
Zlata Amaranth - Graphics
 
COPYRIGHT
---------
The MIT License (MIT)
Copyright (c) 2023 The Raptoreum developers (https://github.com/Raptor3um)
Copyright (c) 2023 Germardies (https://github.com/Germardies)

Änderungsliste
---------
Version 0.99h
Änderungen:
01) Meldung nach dem löschen einer Walletadresse wurde angepasst.
02) Meldung nach dem speichern der Walletliste wurde angepasst.
03) Meldung nach der Prüfung der Walletadresse wurde angepasst.
04) Meldung angepasst, die angezeigt wird, wenn man keine Walletadresse für das Mining angelegt hat.
05) Meldungen angepasst, die angezeigt werden, wenn Einträge beim speichern eines Wingsheets fehlen.
06) Meldung angepasst, die angezeigt wird, wenn man das Default Wingsheet überschreiben möchte.
07) Meldung angepasst, die angezeigt wird, wenn man ein MultinwingMining Gerät angelegt hat.
08) Meldung angepasst, die angezeigt wird, wenn man ein MultinwingMining Gerät löscht hat.
09) Meldung angepasst, die angezeigt wird, wenn etwas mit dem Dateisystem für das Mining nicht stimmt.
10) Medlung angepasst, die angezeigt wird, wenn man Versucht ohne Wallet zu Minen (MultiWingmining). 

Fehler behoben:
1) Button "Walletadresse löschen" führt keine Funktion mehr aus, wenn kein Eintrag in der Liste vorhanden ist.
Fehler gefunden von Discord User: OvErLoDe#4871

2) Button "RTM Explorer öffnen" führt keine Funktion mehr aus, wenn kein Eintrag in der Liste vorhanden ist.
Fehler gefunden von Discord-Benutzer OvErLoDe#4871

3) Miner im Hintergrund ausführen funktioniert wieder.
Fehler gefunden von Discord User: abdani#6797

4) Unter MultiWingMining wurde unter Standard immer noch "1 Core Donation" angezeigt (reiner Anzeigefehler). Fehler behoben
Fehler gefunden von Discord User: abdani#6797

5) Nach dem Löschen einer Wallet-Adresse wurde die Balance und der Preis nicht sofort angepasst. Dies liegt an dem 60 Sekunden
Intervall der API-Abfrage. Die Funktion wurde so umgeschrieben, dass eine Abfrage gestartet wird, wenn eine Adresse gelöscht 
wird, unabhängig vom Intervall.
Fehler gefunden von Discord User: all_danger#5769

6) Wenn unter MultiWingMining kein Gerät ausgewählt ist oder sich kein Gerät in der Liste befindet, wird beim Klick auf 
"Start Mining" nun eine Fehlermeldung ausgegeben.
Fehler gefunden von Discord User: abdani#6797

7) Doppelte Adressen in der Wallet-Übersicht werden nun verhindert.
Fehler gefunden von Discord User: ヾ((ﾒ`◣ ̧◢ ́) ﾉ ･ﾟ。゜゜#8220

8) Miner kann mehrfach gestartet werden: Normalerweise erkennt Raptorwings, ob der SRB Miner bereits läuft und färbt den 
"Start Mining Button" rot. Da dies nicht immer der Fall zu sein scheint, prüft Raptorwings beim Klick auf den 
"Start Mining"-Button nun auch, ob der SRB Miner als Windows-Task läuft.
Fehler gefunden von Discord User: ヾ((ﾒ`◣ ̧◢ ́) ﾉ ･ﾟ。゜゜#8220

9) Kleine englische Anpassungen in der Sprachdatei.
Fehler gefunden von Discord User: vladislav_kosko#1605

10) Kopfzeile in der Instruction.pdf wurde angepasst
Gefunden von Discord User: OvErLoDe#4871

11) Seite 4 der Instruction.pdf wurde angepasst
Gefunden von Discord User: OvErLoDe#4871

12) Fehler behoben: Im "Über"-Fenster war es möglich, auf die Weblinks zu klicken, was aber zu keiner weiteren Aktion führte.
Jetzt wird beim Anklicken der Weblinks der Standardbrowser mit der angeklickten Adresse geöffnet.
Fehler gefunden von Discord User: ヾ((ﾒ`◣ ̧◢ ́) ﾉ ･ﾟ。゜゜#8220