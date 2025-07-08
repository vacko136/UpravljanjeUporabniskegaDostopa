# Upravljanje uporabniškega dostopa

## Opis projekta

Upravljanje uporabniškega dostopa je .NET 9 Web API projekt, ki omogoča upravljanje uporabnikov, virov in dostopnih pravic. Projekt je zgrajen z uporabo Clean Architecture 
in uporablja PostgreSQL za podatkovno bazo.

---

## Struktura projekta

- **UserAccessManagement.Domain** — entitete in poslovna logika
- **UserAccessManagement.Application** — vmesniki in servisne implementacije
- **UserAccessManagement.Infrastructure** — dostop do podatkov (Entity Framework Core)
- **UserAccessManagement.API** — Web API kontrolerji
- **UserAccessManagement.Tests** — enotni testi

---

## Navodila za zagon

1. Klonirajte repozitorij

```bash
git clone https://github.com/yourusername/user-access-management.git
cd user-access-management

2. Namestite in nastavite PostgreSQL

Ustvarite novo bazo podatkov, npr. UserAccessManagementDB
Posodobite povezovalni niz (connection string) v appsettings.json ali v okolijskih spremenljivkah:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=UserAccessManagementDB;Username=vaš_uporabnik;Password=vaše_geslo"
}

3. Izvedite migracije in ustvarite bazo

V terminalu v mapi UserAccessManagement.API (ali kjerkoli, kjer imate nastavljen ApplicationDbContext) zaženite: dotnet ef database update

4.Zaženite API
Zaženite API: dotnet run --project UserAccessManagement.API

5.Dostop do Swagger UI
Odprl sem vam bo Swagger UI

Enotni testi

Testi so napisani z uporabo xUnit in Moq. Za zagon testov v terminalu zaženite: dotnet test UserAccessManagement.Tests/UserAccessManagement.Tests.csproj

Glavne funkcionalnosti API-ja
Uporabniki (Users)
Pridobivanje vseh uporabnikov

Pridobivanje uporabnika po ID

Dodajanje novega uporabnika

Brisanje uporabnika

Viri (Resources)
Pridobivanje vseh virov

Pridobivanje vira po ID

Dodajanje novega vira

Brisanje vira

Dostop (Access Grants)
Dodeljevanje dostopa uporabniku do vira

Razveljavitev dostopa

Pridobivanje virov, do katerih ima uporabnik dostop


Uživajte pri uporabi!
David
