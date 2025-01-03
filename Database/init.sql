-- Switch to the target database
\c securenotes;

-- Create the table
CREATE TABLE IF NOT EXISTS notes (
    id SERIAL PRIMARY KEY,
    noteid VARCHAR(1000) NOT NULL,
    timestamp TIMESTAMP,
    content TEXT NOT NULL
);