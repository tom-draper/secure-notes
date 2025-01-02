# Secure Notes

A privacy-first anonymous note sharing service, with notes stored against a unique ID.

![Secure-Notes-01-01-2025_02_46_PM](https://github.com/user-attachments/assets/06b553e6-84c1-487f-a3cd-5f25c94f1cc5)

## Features

#### Guaranteed Anonymity

Any notes you post are not tied to any form of user identification.

#### Password-style Search

Notes stored against a given identifier can only be accessed by searching against a note ID. The more complex a note ID is, the less likely another user is to guess it, in a similar way to a password.

#### Client-side encryption

The contents of notes and any note IDs you search for are encrypted using strong cryptographic algorithms _before_ it is sent to the server. This means anybody in control of the server, or attempting to intercept data before it reaches the server, cannot read your notes.

#### Encrypted URLs

All note URLs are encrypted. This means that if anybody reads your browser history, they will not know the notes you have accessed.

#### Direct access restriction

Notes can only ever be accessed by entering a note ID into the search bar. If a user gains access to a URL you have visited, they cannot enter the URL into their browser to visit that page directly.

#### Optional Timestamps

When posting a note, timestamps are enabled by default. If you think somebody could infer your activity through the time it was posted, you can disable it before posting.

## Getting Started

Clone the project to your local machine.

```bash
git clone https://github.com/tom-draper/secure-notes.git
```

Launch the service using Docker compose and open `http://localhost:8080` in a browser.

```bash
docker compose up -d
```

## Example Use Cases

### Class Feedback

At the end of a semester, a lecturer requests feedback from their students. They ask their students to open the Secure Notes instance hosted on a University campus server, enter the module ID and submit feedback anonymously.

### Safeguarding

A substitute teacher at a Primary School notices a child in the class appears underweight. They connect to the school's Secure Notes webpage set up for the staff to use and enter the student's ID. They register their observation anonymously with a new note and notice a number of concerning notes previously submitted before deciding to raise the issue with their superior.

### Questions for Speaker

At a yearly keynote event, the audience is encouraged to submit questions for the guest speaker. They scan a QR code on the screen which takes them to the Secure Notes URL for the talk to submit anonymous questions.
