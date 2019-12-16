# README

## Database
For at systemet kan køre, kræver det at udvikleren opretter en database. Dette gøres ved følgende kommando:
`CREATE DATABASE BookiDev`

Hernæst oprettes tabeller. Til dette benyttes tables.sql filen, som eksekveres i SSMS, eller lignende programmer. Filen ligger i root mappen, i BookiAPI løsningen.

## Seeding af databasen
Efter oprettelse af databasen, kan denne med fordel seedes. Dette gøres ved at køre Program.cs, som ligger i BookiAPI.Seeds projektet. Dette program opretter flere forskellige ting i databasen, heriblandt tre employees og to customers. Log ind oplysninger til disse, er som følger:
* Employee 1: kongen@example.com/12345678
* Employee 2: dronningen@example.com/12345678
* Employee 3: prinsen@example.com/12345678
* Customer 1: kunde@example.com/12345678
* Customer 2: gaest@example.com/87654321

## Opstart af API server
Serveren startes ved at køre BookiAPI.RESTfulService projektet. Hernæst lytter serveren på følgende adresse: https://localhost:44314/.

## Opstart af web klient
Web klienten startes ved at køre BookiWeb projektet, i BookiWeb løsningen. Projektet åbner automatisk i browseren, på følgende adresse: https://localhost:44366/.

## Opstart af desktop klient
Desktop klienten startes ved at køre BookiDesktop.Client projektet, som ligger i BookiDesktop løsningen. Hernæst bliver man mødt af et login vindue, hvor der med fordel kan logges ind med en af de medarbejdere, som er oprettet efter kørsel af BookiAPI.Seeds projektet.
