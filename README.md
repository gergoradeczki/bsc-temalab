# Témalabor 2021/22/1 - Teendőket kezelő alkalmazás

Egy teendőket kezelő alklamazásnak a backend és frontend kódjának az elkészítése volt a feladat.

Frontenden az alábbiakra van lehetőség:

- Meglévő teendő megtekintése
- Új teendő felvétele
- Meglévő teendő szerkesztése
  - pozíció oszlopban
  - név
  - határidő
  - leírás
  - állapot
- Meglévő teendő törlése
- Meglévő oszlop megtekintése
- Új oszlop felvétele
- Meglévő oszlop törlése

## Beüzemelés

### Repo klónozása

```bash
git clone git@github.com:gergoradeczki/temalabor-2021.git
cd temalabor-2021
```

### NPM modulok telepítése és frontend elindítása:

```bash
cd frontend
npm install
npm start
```
### Backend elindítása

#### Adatbázis kapcsolat

A szerver SQLite-ot használ, így nem szükséges külső adatbázishoz kapcsolódni.

#### Fordítás, futtatás

Nyissuk meg a Solution fájlt a `backend` mappában:

```bash
./temalabor-2021.sln
```

A megjelenő projektet fordítsd le és indítsd el.

## Felépítés

### Entitások

Az alkalmazás 2 entitást külöböztet meg: *Todo* és *Column*.

#### Todo

Egy teendőt reprezentál. Lehet neve, leírása, határideje és státusza. Ezen felül tudja magáról, hogy melyik oszlophoz tartozik.

#### Column

Egy oszlopot reprezentál. Van neve.

### Fronted

#### Megjelenés, forráskód

Megjelenítéshez MUI-t *(Material UI)* használok. A használt framework segítségével az operációs rendszer világos/sötét témájához igazodik a megjelenés.

A forráskód teljes mértékben, az elejétől kezdődően TypeScript-ben készült.

#### Felépítés

Az alábbi hierarchia szerint épül fel a frontend:

- App
  - Header
  - Board
    - Column
      - NewTodo
      - Todos
        - Todo

**App**

A React alkalmazás gyökere.

**Header**

A webes felület tetején található fejléc. Megjeleníti az alkalmazás nevét.

**Board**

Felel az elsődleges adatlekérdezésért. A kapott adatok alapján összeállítja az oszlopokat és a táblákat.

Új oszlop hozzáadása és elvétele is az ő felelőssége.

**Column**

A kapott adatok alapján megjeleníti a hozzá tartozó teendőket. Amennyiben egy teendőn *"halmaz"* művelet történik *(törlés, hozzáadás, sorrend módosítás)*, akkor ezeket is ő kezeli le.

**NewTodo**

Ezzel lehet új teendőt felvenni.

**Todos**

A kapott teendőket pozíciójuk alapján helyes, nővekvő sorrendbe teszi, majd kezdeményezi a megjelenítésüket. Teendő által kiváltott esemény továbbítása az oszlopnak.

**Todo**

Megjeleníti a kapott adatok alapján az adott teendőt. Belső állapoton keresztül állítható a szerkesztő mód és megjelenítő mód között. A rajta megjelenő gombokkal lehet eseményeket kiváltani.

#### Kommunikáció backenddel

A frontend kód `fetch()`-en keresztül éri el a backendet. Első betöltődéskor lekérdezi az összes oszlopot a hozzájuk tartozó teendőkkel együtt.

Minden, a UI-on végzett művelet csak akkor valósul meg, ha a fetch sikeres volt. Ezáltal, ha pl. leáll az adatbázis, akkor a UI-on végzett művelet nem fog inkonzisztens állapotot mutatni azzal, hogy pl. kitöröl egy elemet, miközben az adatbázis elérhetetlen.

### Backend

Felhasznált technológiák:

- szerver: ASP.NET
- adatelérés: Entity Framework
- adatbázis: SQLite

Futtatáskor, ha még nem létezik, akkor `temalabor.db` néve létre fog jönni az adatbázis és feltöltődik néhány előre definiált adattal. 

C# osztálydiagram:

![Osztály diagram](/figures/class-diagram.png)

Adatbázis ER diagram:

![ER diagram](/figures/er-diagram.png)

#### API interfész

A backend API interfészt biztosít kontrollereken keresztül a frontend számára. Külön címen lehet a teendőket, külön címen az oszlopokat elérni.

Oszlopokhoz:

- `/api/columns`: GET, POST, OPTIONS
- `/api/columns/{id}`: GET, DELETE, OPTIONS

Teendőkhöz:

- `/api/todos`: GET, POST, PUT, OPTIONS
- `/api/todos/{id}`: GET, DELETE, OPTIONS
- `/api/todos/swap`: PUT, OPTIONS