# Campus Rental System

## Opis projektu

Campus Rental System to aplikacja konsolowa w C#, która obsługuje uczelnianą wypożyczalnię sprzętu.

System pozwala:

- dodawać użytkowników i urządzenia,
- wypożyczać oraz zwracać sprzęt,
- obsługiwać przypadki błędne,
- oznaczać sprzęt jako uszkodzony i przywracać go po serwisie,
- wyświetlać raport stanu wypożyczalni.

Scenariusz demonstracyjny znajduje się w pliku `CampusRentalSystem/Program.cs`.

## Struktura projektu i decyzje projektowe

Projekt został podzielony na trzy obszary:

- model domenowy: `CampusRentalSystem/Models`,
- logika biznesowa: `CampusRentalSystem/Services`,
- warstwa prezentacji: `CampusRentalSystem/Program.cs`.

Taki podział oddziela dane i reguły domeny od interfejsu użytkownika. Dzięki temu można zmieniać sposób prezentacji bez zmian w logice biznesowej.

## Kohezja, coupling i odpowiedzialności klas

### Kohezja (spójność)

- Klasa `Service` w `CampusRentalSystem/Services/Service.cs` odpowiada wyłącznie za operacje biznesowe wypożyczalni.
- Klasa `Rental` w `CampusRentalSystem/Models/Rentals/Rental.cs` odpowiada za stan pojedynczego wypożyczenia.
- Klasa `PenaltyCalculator` w `CampusRentalSystem/Services/PenaltyCalculator.cs` odpowiada tylko za wyliczanie kary.

Każda klas ma jedną odpowiedzialność.

### Coupling (sprzężenie)

- `Program.cs` wywołuje metody serwisu i obsługuje komunikaty.
- `CampusRentalSystem/Services/Service.cs` zwraca dane lub rzuca wyjątki.
- Wyjątki są zebrane w `CampusRentalSystem/Exceptions/DomainExceptions.cs`.

To ogranicza zależności między warstwą prezentacji i logiką domenową.

### Odpowiedzialności klas

- `Models/Devices`: reprezentacja urządzeń i ich właściwości.
- `Models/Users`: reprezentacja typów użytkowników i limitów wypożyczeń.
- `Models/Rentals`: reprezentacja wypożyczeń i ich statusu.
- `Services/Service`: reguły operacyjne systemu.
- `Services/PenaltyCalculator`: reguła naliczania kar.
- `Exceptions/DomainExceptions`: sygnalizacja błędnych.
