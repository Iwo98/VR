# Dokumentacja

## Aplikacja
### TODO
- [x] Docelowy moduł testu(losowanie 8 gier)
- [x] Wizualizacja wyników gier na wykresach
- [x] Zapis postępu gry(saves)
- [x] Dodanie możliwości stworzenia konta
- [ ] ~~Zapis postępów online~~
- [x] Jednolity układ współrzędnych przy zmianie scen (przód każdej ze scen powinien być zawsze w tę samą stronę)
- [x] Dodanie menu modułu treningowego

### Skrypty globalne
- ConstantGameValues.cs - skrypt przechowujący stałe informacje na temat gier, np. nazwy, ścieżki do scen i ikon
- GameChoiceManager.cs - skrypt odpowiedzialny za wybór odpowiedniej sceny gry i przetwarzanie informacji zwrotnej na temat zdobytych punktów
- LookWithMouse.cs - skrypt pozwalający na rozglądanie się kamerą za pomocą myszki. Dodane obecnie w poziomie - VanishingThings.
- MoveWithKeyboard.cs - skrypt pozwalający na poruszanie kamerą za pomocą klawiczy w i s. Dodane obecnie w poziomie - VanishingThings.
- SceneFader.cs - skrypt pozwalający na bardziej naturalne przechodzenie między scenami(efekt fade zamiast bezpośredniego ładowania)
- UserData.cs - skrypt odpowiedzialny za zapis i odczyt danych obecnego użytkownika do pliku .dat
- VRHandInputManager.cs - skrypt pozwala na przeglądanie które przyciski są obecnie wciśnięte na obu kontrolerach

## Menu Główne

### Skrypty
- ChooseGameController.cs - skrypt w menu wyboru gry, który uruchamia wybraną przez nas grę
- DoorController.cs - skrypt wywołujący animacje drzwi do wyboru gier i menu treningowego
- DoorExitController.cs - skrypt wywołujący animacje drzwi wyjścia z gry (podnoszenie bramy)
- Hand.cs - skrypt wykorzystywany w każdej grze, który odpowiada za animacje dłoni w VR
- HandController.cs - skrypt odczytujący wartości przycisków kontrolera
- KeyboardHandler.cs - skrypt odpowiedzialny za obsługę klawiatury, używanej w menu logowania
- LoginCanvasController.cs - skrypt odpowiedzialny za obsługę menu logowania
- PlotsCanvasController.cs - skrypt odpowiedzialny za wyświetlanie statystyk treningów użytkownika
- TrainingController.cs - skrypt odpowiedzialny za obsługę menu treningu

## Alchemist

### Skrypty
- MainGameAlchemist.cs - główny skrypt zarządzający, odpowiadę za sterowanie innymi elementami i zmianę stanu gry
- Potion.cs - skrypt definiujący zachowanie butelek z miksturami
- RecipeScroll.cs - skrypt zarządzający wyświetlaniem przpisów mikstru na zwoju
- ClockTimer.cs - skryp zarządzający Timerem w postaci zegara, pokazuje ile czasu pozostało w obecnej fazie

### TODO
- [ ] ~~Dodanie mechaniki wlewania mikstur do kotła (?) - możliwe pozostanie tylko przy wrzucaniu~~
- [x] Dodanie innych rodzajów składników, mechanika wrzucania ich do kotła
- [ ] ~~Ewentualne dodanie mechaniki przemieszania łyżką kotła po zakończeniu przepisu, przetestowanie czy ma to sens w grze na 30 sekund~~
- [x] Implementacja VR
- [x] Stworznie podstawowej scenerii do gry - pracownia alchemiczna
- [x] Poprawa scenerii gry i oświetlenia
- [x] Zbalansowanie przyznawanych punktów
- [x] Adaptacyjne poziomy trudności
- [x] Dodanie dźwięków


## Magic Duel

### Skrypty
- MainGameMagicDuel.cs - główny skrypt zarządzający, odpowiadę za sterowanie innymi elementami i zmianę stanu gry
- MagicSphere.cs - skrypt definiujący zachowanie Magicznych Sfer, które są łączone w celu odtworzenia wzoru
- SphereConnector.cs - skrypt definiujący zachowanie elementu łączącego Magiczne Sfery
- ManaBarTimer.cs - skrypt zarządzający Timerem w postaci paska Many, pokazuje ile czasu pozostało do końca gry
- destroyEffect.cs - skrypt odpowiadający z niszczenie efektów zaklęć tworzonych dla gracza i przeciwnika

### TODO
- [ ] ~~Zmiana mechaniki rysowania - obecna mechanika(grid ze sferami) zostanie wykorzystana w innej grze~~
- [x] Implementacja VR
- [x] Stworzenie poziomów trudności
- [x] Stworznie podstawowej scenerii do gry - wieża wrogiego maga
- [x] Poprawa scenerii gry i oświetlenia
- [x] Poprawa tworzenia i niszczenia efektów zaklęć
- [x] Zbalansowanie przyznawanych punktów
- [x] Adaptacyjne poziomy trudności
- [x] Dodanie dźwięków

## Maze Runner

### Skrypty
- MainMazeRunner.cs - główny skrypt zarządzający, generuje rozkład piętra zależnie od poziomu trudności, przykładową ścieżkę oraz przeszkody rozłożone na piętrze. Aktualizuje stan pokoju zależnie od tego, w którym miejscu aktualnie się znajdujemy.
- Door.cs - skrypt obsługujący drzwi w pokoju.
- Key.cs - skrypt obsługujący klucze do zbierania.
- Ladder.cs - skrypt obsługujący drabinę przenoszącą na kolejne piętro.
- StartButton.cs - skrypt obsługujący przycisk rozpoczynający grę.
- EndButton.cs - skrypt obsługujący przycisk powrotu do menu głównego.
- ClockAsTimer.cs - skrypt obsługujący timer gry.

## Candles

### Skrypty
- CandleController.cs - wszystko co związane ze świeczkami - zapalanie na różne kolory, gaszenie, wykrywanie kolizji z pochodnią i dłońmi
- CandlesGameController.cs - kontroler gry spawnujący świeczniki w zależności od poziomu, sterowanie UI (zasady, pozostały czas, wyniki końcowe itp.)
- Level.cs - klasa abstrakcyjna implementująca funkcje wszystkich poziomów - losowanie świeczek, podliczanie punktów
- LevelX.cs - poszczególne poziomy, informacja o ilości świeczek, ilości kolorów w grze, rozmieszczeniu pojedynczych/potrójnych świeczników
- TorchController.cs - zmiana koloru pochodni przy kliknięciu przycisku akcji, powrót pochodni do stojaka po upuszczeniu

## Perfect Shooter

### Skrypty
- DestroyAfterShoot.cs - skrypt odpowiedzialny za usuwanie celów po odpowiednim trafieniu, ma być również odpowiedzialny, za wyświetlanie celów tylko tych, które mamy trafić.
- ResetPosition.cs - jego celem jest resetowanie położenia strzał

### TODO
- [ ] Stworzenie obiektów, które będą pojawiały się w ilości zależnej od poziomu trudności
- [ ] Implementacja obiektów strzał oraz kuszy
- [ ] Implementacja systemu ładowania odpowiedniego typu strzał
- [ ] Ustawienie timerów pokazujących czas graczowi oraz odpowiedzialnych za resetowanie położenia


## What's New

### Skrypty
---

### TODO
- [ ] Dodatnie instrukcji przed rozpoczęciem gry
- [ ] Dodanie licznika czasu oraz ekranu podsumowania wyniku
- [ ] Implementacja róznych poziomów trudności
- [ ] Implemenacja losowości przedmiotów startowych i pojawiających się
- [ ] Adaptacja gry na VR

