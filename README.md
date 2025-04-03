# Secure Notes

A privacy-first anonymous note sharing service, with notes stored against a unique ID.

![Secure-Notes-01-01-2025_02_46_PM](https://github.com/user-attachments/assets/06b553e6-84c1-487f-a3cd-5f25c94f1cc5)

## Features

#### Guaranteed anonymity

Any notes you post are completely anonymous, with no connection to your identity. The server only receives the encrypted note content and a timestamp (if enabled).

#### Password-style search

Notes are stored with a unique identifier and can only be accessed by searching for this specific note ID. Like passwords, more complex note IDs provide greater security by reducing the chance of someone guessing them.

#### Database encryption

Notes are encrypted before being stored in the database, ensuring that even if a malicious actor gains control of the server, they cannot read your notes.

#### Encrypted URLs

Secure Note URLs contain no meaningful information, ensuring that if someone views your browser history, they cannot determine which notes you have accessed.

#### Direct access restriction

Notes can only be accessed by entering a note ID into the search bar. If someone discovers a URL you've visited, they still cannot access the note by entering that URL directly into their browser.

#### Optional timestamps

Timestamps are enabled by default when posting a note. If you're concerned that someone might identify you through posting times, you can disable this feature before submitting your note.

## Getting Started

Clone the project to your local machine.

```bash
git clone https://github.com/tom-draper/secure-notes.git
```

Create a `.env` file with new database login details, using the `DB_USERNAME` and `DB_PASSWORD` variables, following the same format as the `.env.example` file.

Launch the service using Docker compose and open `http://localhost:8080` in a browser.

```bash
docker compose up -d
```

## Example Use Cases

### Class Feedback

At the end of a semester, a lecturer requests feedback from their students. Students are instructed to access the Secure Notes platform hosted on the University's server, enter their module ID, and submit anonymous feedback about the course.

### Safeguarding

A substitute teacher at a Primary School notices a child in their class appears underweight. They access the school's Secure Notes portal designated for staff use and enter the student's ID. After anonymously registering their observation, they discover several previously submitted concerns about the same student, which prompts them to raise the issue with the Head of School.

### Questions for Speaker

At a yearly keynote event, audience members are encouraged to submit questions for the guest speaker. By scanning a QR code displayed on screen, attendees are directed to a Secure Notes page where they can enter the talk ID and submit their questions anonymously.

## Contributions

Contributions, issues and feature requests are welcome.

- Fork it (https://github.com/tom-draper/secure-notes)
- Create your feature branch (`git checkout -b my-new-feature`)
- Commit your changes (`git commit -am 'Add some feature'`)
- Push to the branch (`git push origin my-new-feature`)
- Create a new Pull Request
