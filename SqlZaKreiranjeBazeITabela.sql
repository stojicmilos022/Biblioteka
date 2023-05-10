-- Kreiranje baze 

create database Biblioteka

use  biblioteka

-- Kreiranje tabele 
--drop table ClanoviBiblioteke
create table ClanoviBiblioteke
(
Id int Primary key Identity (1,1),
Ime nvarchar(25) not null,
Prezime nvarchar(25) not null)

-- punjenje tabele inicijalnim vrednostima

INSERT INTO ClanoviBiblioteke(ime, prezime)
VALUES 
    ('Ana', 'Anić'),
    ('Boris', 'Borić'),
    ('Cvijeta', 'Cvijetić'),
    ('Damir', 'Damić'),
    ('Ema', 'Emić'),
    ('Filip', 'Filipović'),
    ('Goran', 'Goranić'),
    ('Hana', 'Hanić'),
    ('Ivan', 'Ivanić'),
    ('Jasna', 'Jasnić'),
    ('Karlo', 'Karlić'),
    ('Lana', 'Lanić'),
    ('Maja', 'Majić'),
    ('Nina', 'Ninić'),
    ('Ozren', 'Ozrenić'),
    ('Petra', 'Petrović'),
    ('Roko', 'Roknić'),
    ('Sara', 'Sarić'),
    ('Tina', 'Tinić'),
    ('Uroš', 'Urošević'),
    ('Vedran', 'Vedranić'),
    ('Zvonimir', 'Zvonimirić'),
    ('Ana', 'Anić'),
    ('Boris', 'Borić'),
    ('Cvijeta', 'Cvijetić');

-- kreiranje tabele za knjige
--drop table knjige

create table Knjige
( Id int Primary key Identity (1,1),
ImeKnjige nvarchar(55) not null,
Autor nvarchar(25) not null,
GodinaIzdavanja int,
Id_Clana int 
foreign key (Id_Clana) REFERENCES ClanoviBiblioteke(id)
)

-- punjenje inicijalnim parametrima za knjige

INSERT INTO knjige (ImeKnjige, autor, GodinaIzdavanja, id_clana)
VALUES
    ('Ubiti pticu rugalicu', 'Harper Lee', 1960, 1),
    ('Lovac u žitu', 'J.D. Salinger', 1951, 2),
    ('1984', 'George Orwell', 1949, 3),
    ('Veliki Gatsby', 'F. Scott Fitzgerald', 1925, 4),
    ('Vjetar u kosi', 'Miloš Crnjanski', 1918, 5),
    ('Proces', 'Franz Kafka', 1925, 6),
    ('Rat i mir', 'Lev Tolstoj', 1869, 7),
    ('Idiot', 'Fjodor Dostojevski', 1869, 8),
    ('Kiklop', 'Ranko Marinković', 1965, 9),
    ('Povratak Filipa Latinovicza', 'Miroslav Krleža', 1932, 10),
    ('Zločin i kazna', 'Fjodor Dostojevski', 1866, 1),
    ('Gradovi na papiru', 'John Green', 2012, 2),
    ('Jelena, žena koje nema', 'Branko Ćopić', 1947, 3),
    ('Lolita', 'Vladimir Nabokov', 1955, 4),
    ('Leptir', 'Ahmed Muratović', 1970, 5),
    ('Most na Drini ćuprija', 'Ivo Andrić', 1945, 6),
    ('Hari Poter i Kamen mudraca', 'J.K. Rowling', 1997, 7),
    ('Ana Karenjina', 'Lav Tolstoj', 1877, 8),
    ('Zvjezdane staze', 'Gene Roddenberry', 1966, 10),
    ('Ana Frank - Dnevnik', 'Ana Frank', 1947, 1),
    ('Mali princ', 'Antoine de Saint-Exupéry', 1943, 2),
    ('Harry Potter i Red feniksa', 'J.K. Rowling', 2003, 3),
    ('Ime ruže', 'Umberto Eco', 1980, 4),
    ('Cvijet sa raskršća', 'Ivo Andrić', 1952, 5);
