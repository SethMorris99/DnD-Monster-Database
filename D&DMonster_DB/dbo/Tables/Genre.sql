CREATE TABLE Genre (
    GenreID INT IDENTITY(1,1) PRIMARY KEY,  -- Auto-incrementing primary key for the genre
    GenreName VARCHAR(50) NOT NULL,          -- Genre name (Sci-Fi, Fantasy, etc.)
    MonsterID INT,                          -- Foreign key referencing Monsters table
    CONSTRAINT FK_MonsterGenre FOREIGN KEY (MonsterID) REFERENCES Monster(MonsterID)
);
