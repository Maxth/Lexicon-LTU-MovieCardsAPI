# Movie Cards API

The primary objective is to build a robust .NET API using EF Core to manage movies, directors, actors, and genres, with a strict focus on exposing data only through DTOs.

## Required Entities

### Movie:
- `Id`
- `Title`
- `Rating`
- `ReleaseDate`
- `Description`

**Relationships:**
- One-to-Many with Director.
- Many-to-Many with Actor.
- Many-to-Many with Genre.

### Director:
- `Id`
- `Name`
- `DateOfBirth`

**Relationship:**
- One-to-One with `ContactInformation`.

### Actor:
- `Id`
- `Name`
- `DateOfBirth`

### Genre:
- `Id`
- `Name`

### ContactInformation:
- `Id`
- `Email`
- `PhoneNumber`

## Endpoints

### Movie:
- **GET** `/api/movies`: Return a list of `MovieDTO`.
- **GET** `/api/movies/{id}`: Return a single `MovieDTO` by ID.
- **POST** `/api/movies`: Create a new movie using a `MovieForCreationDTO`.
- **PUT** `/api/movies/{id}`: Update an existing movie with a `MovieForUpdateDTO`.
- **DELETE** `/api/movies/{id}`: Delete a movie by ID.

### Get Detailed Movie Information:
- **GET** `/api/movies/{id}/details`: Return a `MovieDetailsDTO` that combines data from `Movie`, `Director`, `ContactInformation`, `Genre`, and `Actor`.

### Add Actors to Existing Movie:
- **POST** `/api/movies/{id}/actors`: Add one or multiple actors to a specific movie.

### Partial Updates (PATCH):
- **PATCH** `/api/movies/{id}`: Enable partial updates to a movie's details using `JsonPatchDocument<MovieUpdateDto>`.

## Extended Functionality

### Searching and Filtering
- **Objective**: Allow users to search and filter movies through query string parameters.
- **Tasks**:
  - Implement search on the **GET** request for all movies.
  - Add query string parameters to search movies by `Title`, `Genre` (Extra: `ActorName`, `DirectorName`, and `ReleaseDate`).
  - Enable combining multiple search criteria in a single request.
  - Add optional query string parameters to include related entities (e.g., include all actors for a movie).

### Sorting
- **Objective**: Provide sorting functionality for movie listings.
- **Tasks**:
  - Implement sorting options on the **GET** `/api/movies` endpoint.
  - Allow sorting by:
    - `Title` (ascending/descending)
    - `ReleaseDate` (newest/oldest)
    - `Rating` (highest/lowest)
  - Support multiple sorting criteria in a single request.
  - Combine sorting with filtering and searching.
  - Validate and handle invalid sorting parameters gracefully.

### Custom Validation
- **Objective**: Enhance validation to ensure data integrity.
- **Tasks**:
  - Prevent adding duplicate actors with the same `Name` and `DateOfBirth`.
  - Validate that a movie's `ReleaseDate` is not set in the future.
  - Ensure that `Title` is unique across all movies.
  - Validate that `Rating` falls within an acceptable range (0 to 10).

## Repository Pattern

- **Objective**: Abstract the data access layer using the repository pattern.
- **Tasks**:
  - Create repository interfaces and concrete implementations for:
    - `IMovieRepository`
    - `IDirectorRepository`
    - `IActorRepository`
  - Implement the unit of work pattern to coordinate repository operations.
  - Inject repositories into services or controllers using dependency injection.
  - Ensure that all data access operations are performed through these repositories.

## AutoMapper Integration

- **Objective**: Automate mapping between your entities and DTOs using AutoMapper.
- **Tasks**:
  - Configure AutoMapper profiles for mapping entities to DTOs and vice versa.
  - Implement mappings for `Movie`, `Director`, `Actor`, `Genre`, and their corresponding DTOs.

## Status Codes and Error Handling

- **Objective**: Ensure that the API returns appropriate and consistent HTTP status codes along with meaningful error messages.
- **Tasks**:
  - Return standard HTTP status codes for various outcomes:
    - **200 OK** for successful GET, PATCH, and POST operations.
    - **201 Created** for successful resource creation.
    - **400 Bad Request** for validation errors and malformed requests.
    - **404 Not Found** when requested resources do not exist.
    - **409 Conflict** for concurrency and uniqueness conflicts.
    - **500 Internal Server Error** for unhandled exceptions.
  - (Extra: Implement a global error handling mechanism using middleware to catch and process exceptions uniformly.)
  - (Extra: Log errors appropriately for monitoring and diagnostics).

## Validation and Setup
- All tables should be seeded with random test data.
- Add validation for creating and updating a movie.
- The API should return correct status codes.

## Extra Tasks

### Integration with React App:
- **Objective**: Enhance the existing React application to utilize the new API features effectively.
- **Tasks**:
  - Update the frontend to support searching, filtering, and sorting of movies using the implemented API endpoints.
  - Implement forms and UI components to perform partial updates (PATCH operations) on movies, directors, and actors.
  - Add functionality to associate actors with movies through the UI, leveraging the **POST** `/api/movies/{id}/actors` endpoint.
  - Handle and display validation errors and status messages received from the API.
  - Ensure a responsive and user-friendly interface by providing loading states and error notifications.
