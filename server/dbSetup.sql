CREATE TABLE
  IF NOT EXISTS accounts (
    id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
    name varchar(255) COMMENT 'User Name',
    email varchar(255) UNIQUE COMMENT 'User Email',
    picture varchar(255) COMMENT 'User Picture'
  ) default charset utf8mb4 COMMENT '';

CREATE TABLE
  albums (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    title VARCHAR(255) NOT NULL,
    description VARCHAR(255),
    coverImg VARCHAR(3000) NOT NULL,
    archived BOOLEAN DEFAULT false NOT NULL,
    category ENUM ('aesthetics', 'food', 'games', 'animals', 'misc') NOT NULL DEFAULT 'misc',
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
  );

CREATE TABLE
  pictures (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    imgUrl VARCHAR(3000) NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    albumId INT NOT NULL,
    FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
    FOREIGN KEY (albumId) REFERENCES albums (id) ON DELETE CASCADE
  );

CREATE TABLE
  albumMembers (
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    albumId INT NOT NULL,
    accountId VARCHAR(255),
    FOREIGN KEY (albumId) REFERENCES albums (id) ON DELETE CASCADE,
    FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
    UNIQUE (albumId, accountId) -- I can only collaborate on each album once
  );

DROP TABLE albums;

SELECT
  albums.*,
  accounts.*
FROM
  albums
  JOIN accounts ON accounts.id = albums.creatorId;

SELECT
  accounts.*
FROM
  albumMembers
  JOIN accounts ON accounts.id = albumMembers.accountId
WHERE
  albumId = 28;