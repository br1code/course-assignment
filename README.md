# Course Assignment

## Requirements

Develop simple Web application, in any language of your choosing, which manages "Course" records:

- The application should manage the following information about a Course: id, subject, courseNumber, description. All fields other than id are strings
- There should be a simple UI/UX to add/remove courses, search courses, and show list of current courses. This does not have to be fancy as we are not looking for a UI Designer.
- The application should store data in an external database or another data storage system.

Features:

- The application should allow user to search for a course by description, with partial matches like "Bio" would find "Introduction to Biology"
- The application should support deleting a Course
- The application should support inserting a new Course
- courseNumber must be formatted as a three-digit, zero-padded integer like "033". Adding records which are not three-digit numbers results in an validation message to the user
- The application should prevent inserting duplicate courses, where subject and number must be unique

Addtional Information:

- The application must be started with minimal setup using readily available libraries (e.g. `npm start` for Node.js) and sufficiently described in a README.md
- The application must be complete and sent as a zipped package over email or a github link
- The application will only be tested in Google Chrome

Example Course records:
1, "BIO", 101, "Introduction to Biology"
2, "MAT", 045, "Business Statistics"

Suggestions:

- Use an API to manage data and connect to the API from a front-end, Javascript application
- Show that tests have been used to validate behavior
- Runnable via Docker or Kubernetes (optional)
