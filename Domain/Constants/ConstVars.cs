namespace Domain.Constants
{
    public static class ConstVars
    {
        public const string UniqueMovieIndex = "Unique_Movie_Index";
        public const string UniqueDirectorIndex = "Unique_Director_Index";
        public const string FK_MovieDirectorId = "FK_Movie_Director_DirectorId";
        public const string UniqueActorIndex = "Unique_Actor_Index";
        public const int MovieTitleMaxLength = 80;
        public const int MovieDescMaxLength = 200;
        public const string MovieExistErrMsg = "A movie with that title already exists";
        public const string DirectorExistErrMsg =
            "A director with that name and date of birth already exists";
        public const string ActorExistErrMsg =
            "An actor with that name and date of birth already exists";
        public const string MovieDirectorFKErrMsg = "There is no director with that Id";
    }
}
