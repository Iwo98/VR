# __Instrukcja użytkownika aplikacji MemoryGamesVR__


# Opis aplikacji
Projekt MemoryGamesVR to zestaw gier ćwiczących pamięć przestrzenną, połączonych w jedną aplikację. Projekt został wykonany w środowisku Unity, za pomocą języka C#. Aplikacja przeznaczona jest wyłącznie na VR, nie jest ona przystosowana do użytkowania jej na komputerze.
<br>

__Na aplikację składają się:__
- 7 gier trenujących pamięć przestrzenną
- Menu główne
    - Pozwala przejść do innych menu
    - Pozwala stworzyć lub odczytać zapis gry użytkownika
    - Pozwala wyjść z aplikacji
- Menu wyboru gry
    - Pozwala zagrać w jedną z gier na wybranym poziomie trudności
- Menu treningu
    - Pozwala na zagranie w trening - 8 gier wybranych w losowej kolejności, o poziomie trudności dostosowanym na podstawie wyników z poprzednich treningów użytkownika
    - Pozwala na wyświetlanie statystyk użytkownika i wyników poprzednich treningów
    - Moduł treningowy posiada wbudowany system adaptacyjnych poziomów trudności, które dostosowują się do wyników użytkownika w poprzednich treningach. Poziom trudności dostosowywany jest osobno dla każdej z gier.

## Uruchamianie aplikacji


<br><br>
# Opis menu


## Menu główne

<img title="Menu główne" alt="menu" src="Dokumentacje/img/images/menu.png">
<br>

Menu główne pozwala użytkownikowi na przejście do innych menu(wyboru gry lub treningu) poprzez kliknięcie odpowiednich drzwi. Menu logowania może zostać otwarte poprzez kliknięcie okna nad drzwiamy. Gracz może wyjść z gry klikają na bramę, która jest obecnie niewidoczna, ponieważ znajduje się po drugiej stronie dziedzińca.
<br><br><br><br>


<img title="Menu logowania 1" alt="menu logowania 1" src="Dokumentacje/img/images/menu_logowania1.png">
<br>

W menu logowania gracz musu wpisać za pomocą klawiatury swoją nazwę użytkownika oraz nacisnąć przucisk _"Zaloguj"_. Wyjście z tego menu jest możliwe poprzez wciśnięcie przycisku _"x"_ w prawym górnym rogu.
<br><br><br><br>

<img title="Menu logowania 2" alt="menu logowania 2" src="Dokumentacje/img/images/menu_logowania2.png">
<br>

Jeśli nie istnieje zapis gry o danej nazwie, gracz zostanie spytany ma on zostać utworzony.
<br><br><br><br>

<img title="Menu logowania 3" alt="menu logowania 3" src="Dokumentacje/img/images/menu_logowania3.png">
<br>

Menu użytkownika, pozwalające dostosować konto(np. zmienić avatar) lub wylogować się. Obecny gracz zostanie zalogowany nawet po restarcie aplikacji, aby tego uniknąć należy wylogować się przed wyłączeniem gry. Pliki zapisu znajduą się w [folderze](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html), który różnić się będzie dla różnych systemów operacyjnych. Pliki zapisu posiadają nazwę _"nazwa_użytkownika.dat"_. Mogą być one przenoszone miedzy urządzeniami, jednak muszą być umieszczone we odpowiednim folderze folderze. Dla celów odczytania ścieżki należy przyjąć: <br>
__Company name:__ ClickAndGo <br>
__Product name:__ MemoryGamesVR

___Przykład___ <br>
Użytkownik komputera z systemem Winows posiada konto o nazwie _uzytkownik1_ i stworzył w aplikacji zapis o nazwie _gracz_. W takim przypadku zapis można znaleźć w folderze: <br>
_uzytkownik1\AppData\LocalLow\ClickAndGo\MemoryGamesVR_ <br>
pod nazwą _gracz.dat_

<br><br><br><br>


## Menu wyboru gry
<img title="Menu wybór" alt="menu wybor" src="Dokumentacje/img/images/wybor_menu.png">
<br>

Menu wyboru gry pozwala użytkownikowi zagrać w dowolną grę na wybranym przez niego poziomie trudności.
<br><br><br><br>


## Menu treningu
<img title="Menu trening" alt="menu trening" src="Dokumentacje/img/images/trening_menu.png">
<br>

Menu treningu pozwala użytkownikowi zagrać w trening(8 gier w losowej kolejności, z poziomem trudności dobranym na podstawie wyników poprzednich treningów). Gracz ma również możliwość przeglądania wyników i statystyk z odbytych treningów.
<br><br><br><br>

<img title="Wyniki canvas" alt="wyniki canvas" src="Dokumentacje/img/images/canvas_wyniki.png">
<br>

Na koniec treningu pokazywane są graczowi wyniki osiągnięte w każdej z gier. Wyświetlane są odpowiednio nazwa gry, w nawiasie poziom trudności na jakim gracz prowadził rozgrywkę oraz zdobyte punkty.
<br><br><br><br>

<img title="Statystyki stół" alt="statystyki" src="Dokumentacje/img/images/menu_treningowe_statystyki.png">
<br>

Gracz ma możliwość obejrzeć swoje statystyki i wyniki z rozegranych treningów.
<br><br><br><br>

<img title="Statystyki canvas" alt="statystyki canvas" src="Dokumentacje/img/images/statystyki_canvas.png">
<br>

Gracz może obejrzeć statystyki dla pełnego treningu i każdej z gier z osobna. Wyświetlane są również wykresy, przedstawiajace postęp gracza w ostatnich 15 grach(lub mniejszej liczby, jeśli gracz nie rozegrał jeszcze 15 gier). Wyjście z tego menu jest możliwe poprzez wciśnięcie przycisku _"x"_ w prawym górnym rogu.
<br><br><br><br>

# Opis gier


## Świeczniki


## Uczeń alchemika
<img title="Alchemik scena" alt="alchemik scena" src="Dokumentacje/img/images/alchemist_scena.png">
<br>

Scena z gry _"Uczeń alchemika"_.
<br><br><br><br>

<img title="Alchemik zasady" alt="alchemik zasady" src="Dokumentacje/img/images/alchemist_zasady.png">
<br>

Gra _"Uczeń alchemika"_ polega na zapamiętaniu położenia mikstur na półkach. Mikstury posiadają różnie cechy - kolor, kształt, przedmiot w butelce - zależnie od poziomu trudności. Po pewnym czasie wszystkie mikstury stają się takie same, jednak pozostaną w tym samym miejscu. Celem gracza jest odtwarzania przepisów wyświetlających się na pergaminie, poprzez wrzucanie do kotła dobrych mikstur w odpowiedniej kolejności.
<br><br><br><br>

<img title="Alchemik start" alt="alchemik start" src="Dokumentacje/img/gify/potiony_start.gif">
<br>

Gracz zapamiętuje położenie mikstur na półkach, mikstury zmieniają się na takie same.
<br><br><br><br>

<img title="Alchemik przepis" alt="alchemik przepis" src="Dokumentacje/img/gify/potiony_przepis.gif">
<br>

Gracz odtwarza przepis wyświetlany na pergaminie, pierwsze 2 mikstury zostały wrzucone dobrze, 2 ostatnie źle.
<br><br><br><br>

__Punkty przyznawane są za:__
- Wrzucenie dobrej mikstury do kotła
- Bonusowe punkty za wykonanie całego przepisu poprawnie
- Przelicznik punktów jest większy dla wyższych poziomów trudności
<br>

__Poziomy trudności:__
1. 3 mikstury różnokolorowe
2. 4 mikstury różnokolorowe
3. 5 mikstur różnokolorowych
4. 5 mistur, 3 różnokolorowe, 2 z przedmiotami w śroku
5. 6 mistur, 4 różnokolorowe, 2 z przedmiotami w śroku
6. 7 mistur, 5 różnokolorowych, 2 z przedmiotami w śroku
7. 7 mikstur, 5 różokolorowych w 2 kształtach, 2 z przedmiotami w środku
8. 8 mikstur, 6 różnokolorowych w 2 kształtach, 2 z przedmiotami w środku
9. 8 mikstur, 5 różnokolorowych w 2 kształtach, 3 z przedmiotami w środku
10. 9 mikstur, 6 różnokolorowych w 2 kształtach, 3 z przedmiotami w środku
<br><br>

## Magiczny pojedynek
<img title="Pojedynek scena" alt="pojedynek scena" src="Dokumentacje/img/images/duel_scena.png">
<br>

Scena z gry _"Magiczny pojedynek"_.
<br><br><br><br>

<img title="Pojedynek zasady" alt="pojedynek zasady" src="Dokumentacje/img/images/duel_zasady.png">
<br>

Gra _"Magiczny pojedynek"_ polega na zapamiętaniu wzoru(zaklęcia) wyświetlanego w przestrzeni, a następnie odtworzeniu go ruchami ręki. Wzór znika dopiero po rozpoczęciu rysowania, więc gracz może sam zdecydować ile czasu potrzebuje na jego zapamiętanie. Wzory wyświetlane są na siatce sfer o rozmiarze 2x2 lub 3x3, zależnie od poziomu trudności. Również długość wzorów zwiększa się wraz z poziomem trudności. Na początku gry użytkownik musi zdecydować, którą ręką będzie wykonywał zadanie.
<br><br><br><br>

<img title="Zaklecie 1" alt="zaklecie 1" src="Dokumentacje/img/gify/spell1.gif">
<br>

Gracz poprawnie odtwarza zaklęcie o długości 4. Rysowanie rozpoczyna się poprzez wciśnięcie przycisku spustu(ang. trigger) na kontrolerze wybranej ręki oraz najechanie ręką na jedną ze sfer. Rysowanie kończy się po puszczeniu przycisku spustu. Gracz może również anulować rysowanie, poprzez puszczenie przycisku spustu, jeśli nie zrobił jeszcze żadnego połączenia między sferami.
<br><br><br><br>

<img title="Zaklecie 2" alt="zaklecie 2" src="Dokumentacje/img/gify/spell2.gif">
<br>

Gracz poprawnie odtwarza zaklęcie o długości 5.
<br><br><br><br>

__Punkty przyznawane są za:__ 
- Zgodność wykonanego zaklęcia ze wzorem
- Bonusowe punkty za wykonanie zaklęcia całkowicie poprawnie
- Przelicznik punktów jest większy dla wyższych poziomów trudności
<br>

__Poziomy trudności:__
1. Siatka sfer 2x2, długość zaklęcia 3
2. Siatka sfer 2x2, długość zaklęcia 4
3. Siatka sfer 2x2, długość zaklęcia 4-5
4. Siatka sfer 2x2, długość zaklęcia 5-6
5. Siatka sfer 3x3, długość zaklęcia 5-6
6. Siatka sfer 3x3, długość zaklęcia 6-7
7. Siatka sfer 3x3, długość zaklęcia 6-8
8. Siatka sfer 3x3, długość zaklęcia 7-9
9. Siatka sfer 3x3, długość zaklęcia 8-10
10. Siatka sfer 3x3, długość zaklęcia 9-11
<br><br>

## Strzelec wyborowy


## Co doszło?


## Ucieczka z lochu
<img title="Maze Runner scena" alt="maze runner scena" src="Dokumentacje/img/images/maze_runner_scena.png">
<br>

Scena z gry _"Ucieczka z lochu"_.
<br><br><br><br>

<img title="Maze Runner zasady" alt="maze runner zasady" src="Dokumentacje/img/images/maze_runner_zasady.png">
<br>

Gra _"Ucieczka z lochu"_ polega na zapamiętaniu mapy danego piętra i przechodzeniu kolejnych pięter unikając możliwie jak największej liczby pokoi z zagrożeniem. Przejście na kolejne piętro możliwe jest tylko i wyłącznie po uprzednim zebraniu wszystkich kluczy.
<br><br><br><br>

<img title="Maze Runner start" alt="maze runner start" src="Dokumentacje/img/images/maze_runner_mapa.png">
<br>

Gracz zapamiętuje rozłożenie kluczy i niebezpiecznych pokoi na piętrze. Następnie, po uprzednim zebraniu wszystkich kluczy, musi dostać się do pokoju z drabiną umożliwiającego dostanie się na kolejne piętro.
<br><br><br><br>

__Punkty przyznawane są za:__
- Zbieranie kluczy rozłożonych na piętrze
- Przechodzenie kolejnych pięter labiryntu
- Przelicznik punktów jest większy dla wyższych poziomów trudności
<br>

__Poziomy trudności:__
1. labirynt 5x5
2. labirynt 5x5
3. labirynt 5x5
4. labirynt 5x5
5. labirynt 7x7
6. labirynt 7x7
7. labirynt 7x7
8. labirynt 7x7
9. labirynt 9x9
10. labirynt 9x9
<br><br>

## Znikające przedmioty

