# __Dokumentacja techniczna aplikacji MemoryGamesVR__


__Na aplikację składają się:__
- 17 gier trenujących pamięć przestrzenną oraz w funkcje wysiłkowe
- Menu główne
    - Pozwala przejść do innych menu
    - Pozwala stworzyć lub odczytać zapis gry użytkownika
    - Pozwala wyjść z aplikacji
- Menu wyboru gry
    - Pozwala zagrać w jedną z gier na wybranym poziomie trudności
- Menu treningu
    - Pozwala stworzyć i zagrać w sesje treningową składającą się z rozgrzewki, głównej części trenigu w skład, którego wchodzi 8 gier wysiłkowych oraz 8 gier poznawczych oraz rozgrzewki.
<br>


# Zaimplementowane rozwiązania
## Porządkowanie plików w projekcie - TO BE UPDATED
Większość uzywanych przez nas zasobów znajduje się w folderze _MemoryGamesVR/Assets_ - są tam wszystkie skrypty, prefaby, sceny itd. Pliki w folderze _MemoryGamesVR/Assets_ uporządkowane są w następujący sposób:
- Materiały związane z grami są w osobnych folderach - np. skrypty, prefaby itd. wykorzystywane w grze Uczeń Alchemika znajdują się w folderze _AlchemistGame_
- Materiały związane z menu znajdują się w folderze _Candles_Menu_, razem z materiałami gry Świeczniki
- Skrypty wykorzystywane w wielu modułach znajdują się w folderze _GlobalScripts_ - np. skrypt _UserData.cs_ odpowiedzialny za zapis i odczyt danych z pliku zapisu użytkownika
- Prefaby wykorzystywane w wielu modułach znajdują się w folderze _GlobalPrefabs_ - np. _XR Rig_
- Zasoby wczytywane w skryptach znajdują się w folderze _Resources_ - np. obrazki awatarów użytkownika
    - Pozwala to na korzystanie z polecenia _Resources.Load("ścieżka do pliku w folderze Resources")_
- Paczki zasobów pobrane z Asset Store znajdują się w folderze _AssetStore_
- W przypadku rozwoju projektu pliki można dodatkowo uporządkować np. rozdzielić zasoby menu i gry _Świeczniki_ do osobnych folderów, stworzyć dodatkowy folder do przechowywania folderów zawierających materiały z gier


## Przechowywanie globalnych danych stałych
Przesyłanie danych między scenami w Unity jest dość skomplikowane, ponieważ obiekty starej sceny są niszczone, zanim nastąpi inicjalizacja obiektów na nowej scenie. Niektóre z tych danych będą wykorzystywane w wielu modułach/scenach(dane globalne) oraz będą miały zawsze taką wartość, od początku do końca działania programu(dane stałe). Wykorzystanym rozwiązaniem jest stworzenie pliku _ConstantGameValues.cs_, znajduje się on w folderze _GlobalScripts_. Zawiera on:
- Stałe wartości liczbowe - liczbę gier, liczbę gier w treningu i maksymalny poziom trudności gier
- Listę angielskich nazw gier
- Listę polskich nazw gier do wyświetlania
- Listę ścieżek do pliku sceny każdej z gier
- Listę ścieżek do pliku ikony 2D dla każdej z gier
- Listę ścieżek do avatarów gracza



## Wczytywanie i zapis danych użytkownika
Obsługa plików zapisu wykonywana jest w skrypcie _UserData.cs_, znajduje się on w folderze _GlobalScripts_. Zawiera on klasę _GameData_, która określa w jaki sposób wyglądać będą zapisywane dane. Skrypt ten odczytuje lub nadpisuje plik o nazwie opdowiadającej nazwie użytkownika, zapisanej w [_PlayerPrefs_](https://docs.unity3d.com/ScriptReference/PlayerPrefs.html):

<blockquote>
    string currentName = PlayerPrefs.GetString("username"); <br>
    string destination = Application.persistentDataPath + "/" + currentName + ".dat";
</blockquote>

[_Application.persistentDataPath_](https://docs.unity3d.com/ScriptReference/Application-persistentDataPath.html) będzie różnić się między systemami operacyjnymi, jednak pozwala zapisywać dane w określonym miejscu na różnych komputerach. Nazwa produktu oraz nazwa zespołu zostały podane na początku dokumentacji.

Domyślny użytkownik posiada nazwę _"default"_ - jeśli żaden inny użytkownik nie jest zalogowany, postęp będzie zapisywany dla użytkownika _"default"_. Z tego powodu również, gracz nie może stworzyć swojego konta o nazwie _"default"_, nie może on stworzyć również konta o pustej nazwie. Nazwa użytkownika składa się wyłącznie z liter i cyfr. Maksymalna długość nazwy użytkownika to 20 znaków, jednak ograniczenie to jest obecne wyłącznie z powodów wizualizacji nazwy w różnych komponentach aplikacji.


## Wybór gry - TO BE UPDATED
Wybór gry wymaga przesyłania danych między scenami - do gry trzeba przesłać wybrany poziom trudności, i otrzymać z powrotem informację na temat zdobytych przez gracza punktów. Wykonywane jest to w skrypcie _GameChoiceManager.cs_. Dane są przesyłane między scenami za pomocą _PlayerPrefs_:
- _curr_game_num_ - która z kolei gra jest rozgrywana obecnie(używane w module treningowym, numeracja zaczyna się od 0 i jest inkrementowana po skończeniu każdej gry w treningu)
- _is_training_ - jeśli _is_training = 1_ to rozpoczęto moduł treningowy, jeśli _is_training = 0_ to wybrano pojedynczą rozgrywkę w menu wyboru gry
- _game_id_X_ - numer ID gry X(X = 0-7) z kolei w treningu
- _game_difficulty_X_ - poziom trudności gry X(X = 0-7) z kolei w treningu
- _game_score_X_ - wynik gry X(X = 0-7) z kolei w treningu


## Obsługa VR
Obsługa VR jest prawie całkowicie zawarta w prefabie _XR Rig_ zajdującym się w folderze _GlobalPrefabs_. Wykorzystywana jest przez nas klasa _XRRayInteractor_, co pozwala na interakcję z otoczeniem na zasadzie raycastu. Najprostszym sposobem na implementację VR w nowym module jest dodanie klasy _XRGrabInteractible_ do obiektów, które mają być chwytane w VR. Jeśli obiekt wymaga interakcji na zasadzie klikania(np. drzwi) można ustawić przed nim canvas z niewidzialnym przyciskiem. Dodatkowo, skrypt _VRHandInputManager.cs_ w folderze _GlobalScripts_ pozwala na dostęp do stanu poszczególnych przycisków na dowolnym z kontrolerów VR, na ten moment w za pomocą skryptu można odczytać czy przycisk _Trigger_ jest wciśniętu na lewym i prawym kontrolerze, jednak w prosty sposób skrypt może zostać rozszerzony o odczyt innych przycisków.
<br><br>

# Zalecenia dotyczące rozwoju aplikacji
## Dodawanie nowych gier
Aplikacja MemoryGamesVR została stworzona w taki sposób, aby umożliwiała proste dodawanie nowych modułów gier. Aby dodać nową grę, należy wykonać następujące czynności:
1. W folderze _Assets_ stworzyć nowy folder, w którym przechowywane będą materiały związane z grą
2. Jeśli zostaną pobrane nowe pakiety z Asset Store, należy je umieścić w folderze _Assets/AssetStore_
3. W skrypcie _ConstantGameValues.cs_ należy:
    - Zmienić wartość _numberOfGames_ aby odpowiadała nowej liczbie gier
    - Dodać angielską nazwę gry do funkcji _initIdNames()_
    - Dodać polską nazwę gry do funkcji _initNames()_
    - Stworzyć ikonę 2D gry, zamieścić ją w folderze _Assets/Resources/Sprites/GameIcons_ i podać ścieżkę do niej w funkcji _initGameIcons2D()_
4.  W scenie _WyborGry_ należy:
    - Sworzyć ikonę 3D i umieścić w: otwórz _Assets/CandlesMenu/Scenes_ -> otwórz scene _WyborGry_ -> w obiekcie _LevelIndicator/CrystalBall/ItemHolder_ pośród innych ikonek.
    - Następnie tę samą ikonę 3D dodaj do skryptu znajdującego się w tej samej scenie: Otwórz obiekt _ChooseGameController_ i tam do skryptu o tej samej nazwie dodaj nową ikonę do listy _GameIcons_
5. Na scenie gry należy umieścić:
    - Obiekt _XR Rig_ znajdujący się w folderze _GlobalPrefabs_
    - Obiekt ze skryptem _ConstantGameValues.cs_  - skrypt _ConstantGameValues.cs_ powinien na każdej scenie być w osobnym obiekcie zamieszonym "na samej górze" na scenie. - obiekty na scenie ładują się od góry do dołu, a skrypt _ConstantGameValues.cs_ wykorzystywany jest w funkcji _Start()_ wielu innych skryptów. Aby uniknąć niepotrzebnych błędów, powinien być on zawsze wczytywany jako pierwszy na scenie.
    - Obiekt ze skryptem _GameChoiceManager.cs_ - odpowiada za obsługę wyboru następnej gry po skończonej rozgrywce
    - Obiekt ze skryptem _SceneFader.cs_ - do zmiennej _FadeOutUIImage_ należy przypisać obraz  _XR Rig/CamerOffset/MainCamera/CanvasSceneTransition/Black_ znajdujący się w dodanym na scenę obiekcie _XR Rig_
6. W głównym skrypcie gry należy zamieścić:
    - W funkcji _Start()_ kod obsługujący przesyłanie poziomu trudności do gry:
        <blockquote>
            if (PlayerPrefs.HasKey("curr_game_difficulty")) { <br>
                &emsp; difficulty = PlayerPrefs.GetInt("curr_game_difficulty"); <br>
            }
        </blockquote>
    - W funkcji kończącej rozgrywkę kod obsługujący zwracanie wyniku gry:
        <blockquote>
            GameChoiceManager game_manager = GameObject.FindObjectsOfType<GameChoiceManager>()[0]; <br>
            game_manager.endGameManagement(score);
        </blockquote>
7. Dodaj grę do _Build Settings_, aby to zrobić należy:
    - Otwórz scenę nowego poziomu w edytorze Unity
    - Otwórz kontekstowe menu klikając na File w górnym menu Unity -> następnie z tego menu wybierz opcję _Build Settings_
    - Dodaj nową scene do Buildu klikając na guzik _Add Open Scenes_

Jeśli wszystkie czynności zostaną wykonane poprawnie, to nowa gra powinna zostać w pełni zintegrowana z aplikacją.


## Zmiany w obecnych grach(Usuwanie, zmiana nazwy, zmiana kolejności itd.)
We wszystkich listach  w skrypcie _ConstantGameValues.cs_ zawierających dane na temat gier, wartości dotyczącej danej gry zawsze są na tej samej pozycji(np. wartości dotyczące gry Magiczny Pojedynek zawsze są na pozycji 3). Jest to ważne, ponieważ wiele innych skryptów korzysta z zależności, że pod tym samym indeksem znajdzie on informacje na temat tej samej gry w każdej z tych list.

Pojawia się również pewien problem w przypadku zmiany indeksowania gier(np. poprzez usunięcie gry albo przestawienie kolejności). Skrypt _UserData.cs_, aby stworzyć plik zapisu, wykorzystuje angielską nazwę gry oraz zapisuje wszystkie gry w kolejności określonej przez skrypt _ConstantGameValues.cs_. Zmiany w indeksowaniu gier bądź ich angielskich nazwach w _ConstantGameValues.cs_ spowodowałyby ich niezgodność z wcześniej zapisanym plikiem użytkownika, co prowadziłoby do błędnego odczytu danych lub innych błędów w programie. Jeśli zaistniałaby potrzeba zmiany indeksowania gier, należałoby uprzednio zimplementować w _UserData.cs_ funkcjonalność naprawy niezgodnych plików zapisu.

