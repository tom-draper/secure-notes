CREATE TABLE IF NOT EXISTS notes (
    id SERIAL PRIMARY KEY,
    noteid VARCHAR(255) NOT NULL,
    timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    content TEXT NOT NULL
);